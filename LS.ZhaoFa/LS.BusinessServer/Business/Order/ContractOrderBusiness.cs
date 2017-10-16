using LS.BusinessServer.Model.Product;
using LS.BusinessServer.Model.Response;
using LS.DBServer.DALToEF;
using LS.DBServer.EF;
using LS.DBServer.EF.DBFactory;
using LS.DBServer.IDAL;
using LS.DBServer.Model.Config;
using LS.DBServer.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.BusinessServer.Business.Order
{
    /// <summary>
    /// 合同单 业务
    /// </summary>
    public class ContractOrderBusiness : BaseBusiness<ContractOrder>
    {
        /// <summary>
        /// 流程订单数据服务对象
        /// </summary>
        IBaseDal<UserOrder> userOrderDal = DBContentFacoty.GetDal<BaseDal<UserOrder>>();
        /// <summary>
        /// 合同单产品数据服务对象
        /// </summary>
        IBaseDal<ContractProduct> contractProductDal = DBContentFacoty.GetDal<BaseDal<ContractProduct>>();

        /// <summary>
        /// 添加/制作 合同单
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="makeUserId"></param>
        /// <param name="expireTime"></param>
        /// <param name="bProductDetailModel"></param>
        /// <returns></returns>
        public BReturnModel AddContractOrder(Guid orderId, Guid makeUserId, DateTime expireTime, IList<BProductDetailModel> bProductDetailModel)
        {
            var isHave = baseDal.GetListQuery(item => item.OrderId == orderId).Count();
            if (isHave > 0)
                return BReturnModel.ReturnError("当前流程单 已经存在对应的合同订单 不可重复提交");

            var userOrderModel = userOrderDal.GetItemById(orderId);
            if (userOrderModel == null)
            {
                return BReturnModel.ReturnError("未找到 对应的流程订单");
            }

            ContractOrder contractOrder = new ContractOrder()
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.Now,
                Flag = (int)BusinessOrderFlag.Undetermined,
                ExpireTime = expireTime,
                OrderId = orderId,
                ContractMakeUserId = makeUserId,
                UserId = userOrderModel.UserId
            };

            baseDal.Add(contractOrder);

            decimal totalPrice = 0;
            foreach (var item in bProductDetailModel)
            {
                ContractProduct contractProduct = new ContractProduct()
                {
                    Id = Guid.NewGuid(),
                    ContractOrderId = contractOrder.Id,
                    Count = item.Count,
                    Price = item.Price,
                    ProductName = item.ProductName,
                    Remarks = item.Remarks,
                    Specifications = item.Specifications,
                    Manufactor = item.Manufactor
                };
                totalPrice += (decimal)contractProduct.Price * (int)contractProduct.Count;
                contractProductDal.Add(contractProduct);
            }

            UserOrder userOrder = new UserOrder()
            {
                Id = orderId,
                CurrentStateTime = DateTime.Now,
                Flag = (int)OrderStateFlag.Contract,
                OriginalPrice = totalPrice,

                RealisticPrice = totalPrice  //现价  需要减去用户红包金额
            };

            userOrderDal.UpdateItemSelect(userOrder, new string[] { UserOrderPropertiesConfig.Flag, UserOrderPropertiesConfig.CurrentStateTime, UserOrderPropertiesConfig.OriginalPrice, UserOrderPropertiesConfig.RealisticPrice });

            int count = DBContent.SaveChanges();
            if (count > 0)
                return BReturnModel.ReturnOk();
            else
                return BReturnModel.ReturnError();
        }

        /// <summary>
        /// 修改 合同单状态
        /// </summary>
        /// <param name="id">合同单id</param>
        /// <param name="userId">操作用户id</param>
        /// <param name="remarks">备注</param>
        /// <param name="businessOrderFlag">需要修改的状态</param>
        /// <returns></returns>
        public BReturnModel UpdateContractOrderFlag(Guid id, Guid userId, string remarks, BusinessOrderFlag businessOrderFlag, bool isUser = false)
        {
            var contractOrder = baseDal.GetItemById(id);
            if (contractOrder == null)
                return BReturnModel.ReturnError("合同单不存在 请提交正确的id");

            if (isUser) //当前修改为用户操作
            {
                if (contractOrder.UserId != userId)
                    return BReturnModel.ReturnError("当前合同单 不属于当前操作用户");
                if (businessOrderFlag == BusinessOrderFlag.Invalid) //用户 取消合同单
                {
                    contractOrder.Flag = (int)BusinessOrderFlag.Invalid;
                    baseDal.UpdateItem(contractOrder);

                    UserOrder userOrder = new UserOrder()
                    {
                        Id = contractOrder.OrderId,
                        CancelTime = DateTime.Now,
                        Flag = (int)OrderStateFlag.UserCancelContract
                    };

                    userOrderDal.UpdateItemSelect(userOrder, new string[] { UserOrderPropertiesConfig.Flag, UserOrderPropertiesConfig.CancelTime });

                    int count = DBContent.SaveChanges();
                    if (count > 0)
                        return BReturnModel.ReturnOk();
                    return BReturnModel.ReturnError("没有更新数据");

                }

                else if (businessOrderFlag == BusinessOrderFlag.Effective) //用户确认合同
                {
                    contractOrder.Flag = (int)BusinessOrderFlag.Effective;
                    contractOrder.ReceiveTime = DateTime.Now;
                    baseDal.UpdateItem(contractOrder);

                    UserOrder userOrder = new UserOrder()
                    {
                        Id = contractOrder.OrderId,
                        CancelTime = DateTime.Now,
                        Flag = (int)OrderStateFlag.SignContract,
                        ReceiveTime = DateTime.Now
                    };

                    userOrderDal.UpdateItemSelect(userOrder, new string[] { UserOrderPropertiesConfig.Flag, UserOrderPropertiesConfig.CancelTime, UserOrderPropertiesConfig.ReceiveTime });

                    int count = DBContent.SaveChanges();
                    if (count > 0)
                        return BReturnModel.ReturnOk();
                    return BReturnModel.ReturnError("没有更新数据");
                }
            }

            else  //当前为 业务员操作
            {
                if (businessOrderFlag == BusinessOrderFlag.Invalid) //业务员 取消合同单
                {
                    contractOrder.Flag = (int)BusinessOrderFlag.Invalid;
                    baseDal.UpdateItem(contractOrder);

                    UserOrder userOrder = new UserOrder()
                    {
                        Id = contractOrder.OrderId,
                        CancelTime = DateTime.Now,
                        Flag = (int)OrderStateFlag.UserCancelContract
                    };

                    userOrderDal.UpdateItemSelect(userOrder, new string[] { UserOrderPropertiesConfig.Flag, UserOrderPropertiesConfig.CancelTime });

                    int count = DBContent.SaveChanges();
                    if (count > 0)
                        return BReturnModel.ReturnOk();
                    return BReturnModel.ReturnError("没有更新数据");

                }


            }

            return BReturnModel.ReturnOk();
        }
    }
}

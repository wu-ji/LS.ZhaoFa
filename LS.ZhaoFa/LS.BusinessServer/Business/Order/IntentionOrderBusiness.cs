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
    /// 意向单业务
    /// </summary>
    public class IntentionOrderBusiness : BaseBusiness<IntentionOrder>
    {
        IBaseDal<UserOrder> userOrderDal = DBContentFacoty.GetDal<BaseDal<UserOrder>>();

        /// <summary>
        /// 意向产品数据服务对象
        /// </summary>
        IBaseDal<IntentionProduct> intentionProductDal = DBContentFacoty.GetDal<BaseDal<IntentionProduct>>();

        /// <summary>
        /// 用户提交 意向单业务 
        /// </summary>
        /// <param name="orderId">关联的 流程订单id</param>
        /// <param name="userId">提交意向的 用户id</param>
        /// <param name="remarks">意向备注</param>
        /// <param name="bProductDetailModel">意向产品 集合</param>
        /// <returns></returns>
        public string AddIntentionOrder(Guid orderId, Guid userId, string remarks, IList<BProductDetailModel> bProductDetailModel)
        {
            var isHave = baseDal.GetListQuery(item => item.OrderId == orderId).Count();
            if (isHave > 0)
                return "该流程订单已经存在 对应的意向订单 请勿重复提交";
            IntentionOrder intentionOrder = new IntentionOrder()
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.Now,
                Flag = (int)BusinessOrderFlag.Undetermined,
                OrderId = orderId,
                IntentionUserId = userId,
                Remarks = remarks
            };
            baseDal.Add(intentionOrder);

            foreach (var item in bProductDetailModel)
            {
                IntentionProduct intentionProduct = new IntentionProduct()
                {
                    Id = Guid.NewGuid(),
                    Count = item.Count,
                    Specifications = item.Specifications,
                    Manufactor = item.Manufactor,
                    Name = item.ProductName,
                    Price = item.Price,
                    IntentionOrderId = intentionOrder.Id,
                    Remarks = item.Remarks
                };
                intentionProductDal.Add(intentionProduct);

            }
            UserOrder userOrder = new UserOrder()
            {
                Id = intentionOrder.OrderId,
                Flag = (int)OrderStateFlag.Intention,
                //OrderId = "1",

                CurrentStateTime = DateTime.Now
            };

            userOrderDal.UpdateItemSelect(userOrder, new string[] { UserOrderPropertiesConfig.Flag, UserOrderPropertiesConfig.CurrentStateTime });
            int count = DBContent.SaveChanges();
            if (count > 0)
                return BReturnModel.IsOk;
            return BReturnModel.Error;
        }

        /// <summary>
        /// 修改意向订单状态
        /// </summary>
        /// <param name="id">订单id</param>
        /// <param name="userId">用户</param>
        /// <param name="remarks"></param>
        /// <param name="businessOrderFlag"></param>
        /// <param name="isUser">标志是否是用户操作</param>
        /// <returns></returns>
        public string UpdateIntentionOrderFlag(Guid id, Guid userId, string remarks, BusinessOrderFlag businessOrderFlag, bool isUser = false)
        {
            var intentionOrder = baseDal.GetItemById(id);
            if (businessOrderFlag == BusinessOrderFlag.Effective)  //确认有效操作
            {
                intentionOrder.Id = id;
                intentionOrder.Flag = (int)businessOrderFlag;
                intentionOrder.IntentionReceiveUserId = userId;
                intentionOrder.ReceiveTime = DateTime.Now;
                intentionOrder.ReceiveRemarks = remarks;
                intentionOrder.FinalRecordUserId = userId;
                intentionOrder.FinalRecordRemarks = remarks;
                intentionOrder.FinalRecordTime = DateTime.Now;

                baseDal.UpdateItem(intentionOrder);

                int count = DBContent.SaveChanges();
                if (count > 0)
                    return BReturnModel.IsOk;
            }

            else if (businessOrderFlag == BusinessOrderFlag.Invalid) //无效操作
            {
                intentionOrder.Id = id;
                intentionOrder.Flag = (int)businessOrderFlag;
                if (isUser)
                {
                    intentionOrder.Remarks = "用户取消 :" + remarks;
                }
                else
                {
                    intentionOrder.FinalRecordUserId = userId;
                    intentionOrder.FinalRecordRemarks = remarks;
                    intentionOrder.FinalRecordTime = DateTime.Now;
                }

                baseDal.UpdateItem(intentionOrder);

                UserOrder userOrder = new UserOrder()
                {
                    Id = intentionOrder.OrderId,
                    Flag = (int)OrderStateFlag.UserTermination,
                    CancelTime = DateTime.Now
                };

                userOrderDal.UpdateItemSelect(userOrder, new string[] { UserOrderPropertiesConfig.Flag, UserOrderPropertiesConfig.CancelTime });

                int count = DBContent.SaveChanges();
                if (count > 0)
                    return BReturnModel.IsOk;
            }

            return BReturnModel.Error;
        }
    }
}

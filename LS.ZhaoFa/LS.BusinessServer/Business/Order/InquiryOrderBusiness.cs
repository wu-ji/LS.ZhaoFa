using LS.DBServer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LS.BusinessServer.BusinessFactory;
using LS.DBServer.Model.Enum;
using LS.DBServer.IDAL;
using LS.DBServer.EF.DBFactory;
using LS.DBServer.DALToEF;
using LS.BusinessServer.Model.Response;
using LS.DBServer.Model.Config;
using LS.UtilityTools;

namespace LS.BusinessServer.Business.Order
{
    /// <summary>
    /// 询价单业务
    /// </summary>
    public class InquiryOrderBusiness : BaseBusiness<InquiryOrder>
    {
        IBaseDal<UserOrder> userOrderDal = DBContentFacoty.GetDal<BaseDal<UserOrder>>();

        /// <summary>
        /// 询价 产品图片数据服务层对象
        /// </summary>
        IBaseDal<InquiryProductImg> inquiryProductImgDal = DBContentFacoty.GetDal<BaseDal<InquiryProductImg>>();


        /// <summary>
        /// 用户 提交询价单业务
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="productName">产品名称</param>
        /// <param name="remarks">备注</param>
        /// <param name="price">用户提交的参考价格</param>
        /// <param name="productImgList">用户提交的 图片路径数组</param>
        /// <param name="orderFlag">订单状态 此处提交(询价单 状态码)</param>
        /// <param name="price">支付状态 此处提交(未支付)</param>
        /// <returns></returns>
        public bool AddUserInquiryOrder(Guid userId, string remarks, decimal price, List<string> productImgList, OrderStateFlag orderFlag = OrderStateFlag.Inquiry, OrderPayStateFlag payFlag = OrderPayStateFlag.Unpaid)
        {
            UserOrder userOrder = new UserOrder()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                OriginalPrice = price,
                RealisticPrice = price,
                Flag = (int)orderFlag,
                PayFlag = (int)payFlag,
                CreateTime = DateTime.Now,
                BusinessOrderId = OrderHelp.GetOrderIdOne(4),
                Description = remarks

            };
            userOrderDal.Add(userOrder);

            InquiryOrder inquiryOrder = new InquiryOrder()
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.Now,
                Price = price,
                OrderId = userOrder.Id,
                Remarks = remarks,
                Flag = (int)BusinessOrderFlag.Undetermined,
                UserId = userId
            };
            baseDal.Add(inquiryOrder);
            foreach (var urlItem in productImgList)
            {
                InquiryProductImg inquiryProductImg = new InquiryProductImg()
                {
                    Id = Guid.NewGuid(),
                    ImgUrl = urlItem,
                    OrderId = userOrder.Id,
                    Remarks = remarks,
                    InquiryOrdertId = inquiryOrder.Id
                };

                inquiryProductImgDal.Add(inquiryProductImg);
            }
            int count = DBContent.SaveChanges();
            if (count > 0)
                return true;
            return false;
        }


        /// <summary>
        /// 修改 询价单状态业务
        /// </summary>
        /// <param name="orderId">业务订单id</param>
        /// <param name="userId">修改用户id</param>
        /// <param name="remarks">修改备注</param>
        /// <param name="ConfirmerTime">报价时间</param>
        /// <param name="businessOrderFlag">状态码</param>
        /// <returns>处理消息</returns>
        public string UpdateInquiryOrderFlag(Guid id, Guid userId, string remarks, BusinessOrderFlag businessOrderFlag, DateTime? ConfirmerTime = null)
        {
            int count = 0;
            if (businessOrderFlag == BusinessOrderFlag.Effective)
            {
                var inquiryOrderModel = baseDal.GetItemById(id);
                if (inquiryOrderModel.Flag == (int)BusinessOrderFlag.Undetermined) //该询价单为待确认订单
                {
                    inquiryOrderModel.Flag = (int)businessOrderFlag;
                    inquiryOrderModel.ConfirmerUserId = userId;
                    inquiryOrderModel.ConfirmerTime = ConfirmerTime;
                    inquiryOrderModel.ConfirmerRemarks = remarks;

                }

                baseDal.UpdateItem(inquiryOrderModel);


                count = DBContent.SaveChanges();
                //count = baseDal.UpdateList(item => item.Id == id, update => new InquiryOrder() { Flag = (int)businessOrderFlag, ConfirmerUserId = userId, ConfirmerRemarks = remarks, ConfirmerTime = ConfirmerTime });

            }
            else if (businessOrderFlag == BusinessOrderFlag.Invalid)
            {
                var inquiryOrderModel = baseDal.GetItemById(id);
                if (inquiryOrderModel.Flag == (int)BusinessOrderFlag.Undetermined) //该询价单为待确认订单
                {
                    inquiryOrderModel.Flag = (int)businessOrderFlag;
                    inquiryOrderModel.ConfirmerRemarks = remarks;
                }

                baseDal.UpdateItem(inquiryOrderModel);

                UserOrder userOrder = new UserOrder()
                {
                    Id = inquiryOrderModel.OrderId,
                    Flag = (int)OrderStateFlag.InquiryInvalid,
                    //OrderId = "1",
                    CancelTime = DateTime.Now,
                    CurrentStateTime = DateTime.Now
                };

                userOrderDal.UpdateItemSelect(userOrder, new string[] { UserOrderPropertiesConfig.Flag, UserOrderPropertiesConfig.CancelTime, UserOrderPropertiesConfig.CurrentStateTime });

                count = DBContent.SaveChanges();
            }
            if (count > 0)
                return BReturnModel.IsOk;
            return BReturnModel.Error;
        }

        /// <summary>
        /// 根据id 查找询价单详情
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="isUser"></param>
        /// <returns></returns>
        public BReturnModel GetInquiryOrderDetail(Guid id, Guid userId, bool isUser = false)
        {
            InquiryOrder inquiryOrder = baseDal.GetItemById(id);
            if (inquiryOrder == null)
                return BReturnModel.ReturnError("未找到该询价单 请提交正确的id");

            if (isUser)
            {
                if (inquiryOrder.UserId != userId)
                    return BReturnModel.ReturnError("当前询价单 不属于查看的用户");
            }

            UserOrder userOrder = userOrderDal.GetItemById(inquiryOrder.OrderId);
            userOrder.InquiryOrder = inquiryOrder;
            return BReturnModel.ReturnOk("查找成功", userOrder);
        }

        /// <summary>
        /// 根据关键字 查询询价订单 集合
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isUser"></param>
        /// <returns></returns>
        public BReturnModel GetInquiryOrderListByKeyWord(string key, bool isUser = false)
        {
            return BReturnModel.ReturnOk();
        }
    }
}

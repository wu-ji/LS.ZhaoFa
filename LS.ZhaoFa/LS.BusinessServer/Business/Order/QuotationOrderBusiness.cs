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
    /// 报价订单业务服务对象
    /// </summary>
    public class QuotationOrderBusiness : BaseBusiness<QuotationOrder>
    {
        /// <summary>
        /// 流程订单数据服务对象
        /// </summary>
        IBaseDal<UserOrder> userOrderDal = DBContentFacoty.GetDal<BaseDal<UserOrder>>();

        /// <summary>
        /// 报价产品图片数据服务对象
        /// </summary>
        IBaseDal<QuotationProductImg> quotationProductImgDal = DBContentFacoty.GetDal<BaseDal<QuotationProductImg>>();

        /// <summary>
        /// 报价产品 数据服务对象
        /// </summary>
        IBaseDal<QuotationProduct> quotationProductDal = DBContentFacoty.GetDal<BaseDal<QuotationProduct>>();

        /// <summary>
        /// 添加 报价订单
        /// </summary>
        /// <param name="userId">操作员id</param>
        /// <param name="orderId">关联的流程订单id</param>
        /// <param name="remarks">备注</param>
        /// <param name="QuotationUserId">操作员id</param>
        /// <param name="expireTime">过期时间</param>
        /// <param name="bProductDetailModel">关联的产品模型</param>
        /// <returns>响应消息</returns>
        public BReturnModel AddQuotationOrder(Guid orderId, string remarks, Guid QuotationUserId, DateTime expireTime, IList<BProductDetailModel> bProductDetailModel)
        {
            var isHave = baseDal.GetListQuery(item => item.OrderId == orderId).Count();
            if (isHave > 0)
            {
                return BReturnModel.ReturnError("orderId流程订单已经存在 对应的报价订单 请勿重复提交");
            }
             var userOrderModel =  userOrderDal.GetItemById(orderId);
            if (userOrderModel == null)
            {
                return BReturnModel.ReturnError("未查询到 对应的流程订单");
            }
            QuotationOrder quotationOrder = new QuotationOrder()
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.Now,
                ExpireTime = expireTime,
                Remarks = remarks,
                OrderId = orderId,
                QuotationUserId = QuotationUserId,
                Flag = (int)BusinessOrderFlag.Undetermined,
                UserId = userOrderModel.UserId
            };

            baseDal.Add(quotationOrder);
            foreach (var item in bProductDetailModel)
            {
                QuotationProduct quotationProduct = new QuotationProduct()
                {
                    Id = Guid.NewGuid(),
                    Manufactor = item.Manufactor,
                    Specifications = item.Specifications,
                    QuotationOrderId = quotationOrder.Id,
                    Price = item.Price,
                    Remarks = item.Remarks,
                    ProductName = item.ProductName,
                };
                quotationProductDal.Add(quotationProduct);


                foreach (var itemImg in item.ImgUrl)
                {
                    QuotationProductImg quotationProductImg = new QuotationProductImg()
                    {
                        Id = Guid.NewGuid(),
                        ImgUrl = itemImg,
                        QuotationProductId = quotationProduct.Id
                    };

                    quotationProductImgDal.Add(quotationProductImg);
                }
            }
            UserOrder userOrder = new UserOrder()
            {
                Id = orderId,
                Flag = (int)OrderStateFlag.Offer,
                CurrentStateTime = DateTime.Now
            };

            userOrderDal.UpdateItemSelect(userOrder, new string[] { UserOrderPropertiesConfig.Flag, UserOrderPropertiesConfig.CurrentStateTime });
            int count = DBContent.SaveChanges();
            if (count > 0)
                return BReturnModel.ReturnOk();

            return BReturnModel.ReturnError();
        }

    }
}

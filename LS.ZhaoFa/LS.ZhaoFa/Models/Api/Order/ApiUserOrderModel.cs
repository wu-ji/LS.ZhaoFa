using LS.ZhaoFa.Models.Api.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS.ZhaoFa.Models.Api.Order
{
    /// <summary>
    /// api层 用户订单详情通用模型 (所有时间参数 均不需要提交 其他参数根据具体步骤提交后端需要的数据)
    /// </summary>
    public class ApiUserOrderModel
    {
        /// <summary>
        /// 订单主键id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 订单业务id(订单号)
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 订单原价格(不需要提交 具体价格由服务器计算)
        /// </summary>
        public decimal OriginalPrice { get; set; }

        /// <summary>
        /// 订单现价格(不需要提交 具体价格由服务器计算)
        /// </summary>
        public decimal RealisticPrice { get; set; }

        /// <summary>
        /// 订单使用的优惠券id
        /// </summary>
        public Guid CouponId { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public int Flag { get; set; }

        /// <summary>
        /// 订单支付状态 0-未支付 10-已支付
        /// </summary>
        public int PayFlag { get; set; }

        /// <summary>
        /// 订单关联的用户地址id
        /// </summary>
        public int OrderAddressId { get; set; }

        /// <summary>
        /// 订单提交时间
        /// </summary>
        public DateTime SubmissionTime { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime PayingTime { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime CompletionTime { get; set; }

        /// <summary>
        /// 订单接收时间
        /// </summary>
        public DateTime ReceiveTime { get; set; }

        /// <summary>
        /// 订单当前状态发生的时间
        /// </summary>
        public DateTime CurrentStateTime { get; set; }

        /// <summary>
        ///  订单对应的产品数组 (报价时 只用上传图片 和备注)
        /// </summary>
        public IList<ApiUserProductModel> ProductList { get; set; }
    }
}
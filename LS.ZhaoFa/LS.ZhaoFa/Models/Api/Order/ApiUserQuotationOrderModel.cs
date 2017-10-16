using LS.ZhaoFa.Models.Api.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS.ZhaoFa.Models.Api.Order
{
    /// <summary>
    /// api层 报价单 通用模型
    /// </summary>
    public class ApiUserQuotationOrderModel : ApiBaseOrderModel
    {

        /// <summary>
        /// 报价人id(下报价单时必须)
        /// </summary>
        public Guid QuotationUserId { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpireTime { get; set; }

    }
}
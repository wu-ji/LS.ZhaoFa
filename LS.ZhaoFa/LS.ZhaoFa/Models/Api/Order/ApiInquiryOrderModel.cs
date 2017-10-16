using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS.ZhaoFa.Models.Api.Order
{
    /// <summary>
    /// api层 询价订单模型
    /// </summary>
    public class ApiInquiryOrderModel : ApiBaseOrderModel
    {
        /// <summary>
        /// 接收(确认)该询价的用户id
        /// </summary>
        public Guid ConfirmerUserId { get; set; }

        /// <summary>
        /// 确认备注
        /// </summary>
        public string ConfirmerRemarks { get; set; }

        /// <summary>
        /// 业务订单状态标记 0-无效 1-待定 2-有效
        /// </summary>
        public int Flag { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS.ZhaoFa.Models.Api.Order
{
    /// <summary>
    /// api层 意向订单数据模型
    /// </summary>
    public class ApiIntentionOrderModel : ApiBaseOrderModel
    {
        /// <summary>
        /// 意向订单 确认人id(用户提交订单时不需要)
        /// </summary>
        public Guid IntentionReceiveUserId { get; set; }

        /// <summary>
        /// 接收时间
        /// </summary>
        public DateTime ReceiveTime { get; set; }

        /// <summary>
        /// 接收备注
        /// </summary>
        public string ReceiveRemarks { get; set; }
    }
}
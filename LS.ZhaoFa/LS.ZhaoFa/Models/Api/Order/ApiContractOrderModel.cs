using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS.ZhaoFa.Models.Api.Order
{
    /// <summary>
    /// api层 合同订单 模型
    /// </summary>
    public class ApiContractOrderModel : ApiBaseOrderModel
    {
        /// <summary>
        /// 合同制作人id(下单时必须)
        /// </summary>
        public Guid ContractMakeUserId { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpireTime { get; set; }

        /// <summary>
        /// 接收时间
        /// </summary>
        public DateTime ReceiveTime { get; set; }
    }
}
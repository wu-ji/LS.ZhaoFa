using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.DBServer.EF
{
     public partial class UserOrder
    {
        /// <summary>
        /// 关联的 询价订单
        /// </summary>
        public InquiryOrder InquiryOrder { get; set; }

        /// <summary>
        /// 关联的 报价订单
        /// </summary>
        public QuotationOrder QuotationOrder { get; set; }

        /// <summary>
        /// 关联的 意向订单
        /// </summary>
        public IntentionOrder IntentionOrder { get; set; }

        /// <summary>
        /// 关联的 合同单
        /// </summary>
        public ContractOrder ContractOrder { get; set; }
    }
}

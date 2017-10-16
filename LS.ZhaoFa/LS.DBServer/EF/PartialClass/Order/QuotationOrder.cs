using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.DBServer.EF
{
    public partial class QuotationOrder
    {
        /// <summary>
        /// 关联的 报价产品
        /// </summary>
        public QuotationProduct QuotationProduct { get; set; }
    }
}

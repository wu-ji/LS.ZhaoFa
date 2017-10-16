using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.DBServer.EF
{
    public partial  class IntentionOrder
    {
        /// <summary>
        /// 关联的 意向产品
        /// </summary>
        public IntentionProduct IntentionProduct { get; set; }
    }
}

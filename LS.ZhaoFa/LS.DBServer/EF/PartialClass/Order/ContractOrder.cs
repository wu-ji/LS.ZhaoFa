using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.DBServer.EF
{
    public partial class ContractOrder
    {
        /// <summary>
        /// 关联的 合同产品
        /// </summary>
        public ContractProduct ContractProduct { get; set; }
    }
}

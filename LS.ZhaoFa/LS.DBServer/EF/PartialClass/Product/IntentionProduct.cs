using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.DBServer.EF
{
   public partial class QuotationProduct
    {
        /// <summary>
        /// 关联的 产品图片
        /// </summary>
        public QuotationProductImg QuotationProductImg { get; set; }
    }
}

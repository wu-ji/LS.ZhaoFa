using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.DBServer.EF
{
    public partial  class InquiryOrder
    {
        /// <summary>
        /// 关联的 询价产品图片
        /// </summary>
        public InquiryProductImg InquiryProductImg { get; set; }
    }
}

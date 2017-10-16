using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.DBServer.Model.Enum
{
    /// <summary>
    /// 业务订单状态 如 (合同单 询价单) 
    /// </summary>
    public enum BusinessOrderFlag
    {
        /// <summary>
        /// 无效
        /// </summary>
        Invalid = 0,

        /// <summary>
        /// 待定
        /// </summary>
        Undetermined = 1,

        /// <summary>s
        /// 有效
        /// </summary>
        Effective = 2
    }
}

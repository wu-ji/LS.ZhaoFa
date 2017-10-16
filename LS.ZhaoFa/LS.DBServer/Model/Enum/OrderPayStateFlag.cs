using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.DBServer.Model.Enum
{
    /// <summary>
    /// 系统订单 支付状态
    /// </summary>
    public enum OrderPayStateFlag
    {
        /// <summary>
        /// 未支付 0
        /// </summary>
        Unpaid = 0,

        /// <summary>
        /// 已支付 10s
        /// </summary>
        AlreadyPaid = 10
    }
}

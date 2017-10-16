using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS.DBServer.Model.Enum
{
    /// <summary>
    /// 用户账号状态枚举
    /// </summary>
    public enum UserFlag
    {
        /// <summary>
        /// 被删除
        /// </summary>
        Delete = -1,
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 0,

    }
}
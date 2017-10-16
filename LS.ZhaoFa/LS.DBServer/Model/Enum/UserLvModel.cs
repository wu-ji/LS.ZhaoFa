using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LS.DBServer.Model.Enum
{
    /// <summary>
    /// 管理员等级 枚举
    /// </summary>
    public enum UserLvModel
    {
        /// <summary>
        /// 普通用户
        /// </summary>
        [Description("普通用户")]
        User = 1,

        /// <summary>
        /// 业务员
        /// </summary>
        [Description("业务员")]
        Counterman = 2,

        /// <summary>
        /// 软文管理员
        /// </summary>
        [Description("软文管理员")]
        SoftWenAdmin = 3,

        /// <summary>
        /// 系统管理员
        /// </summary>
        [Description("系统管理员")]
        Admin = 4
    }
}
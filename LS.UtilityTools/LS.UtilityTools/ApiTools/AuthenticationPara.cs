using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.UtilityTools.ApiTools
{
    /// <summary>
    /// api平台 的身份验证模式参数对象
    /// </summary>
    public class AuthenticationPara
    {
        /// <summary>
        /// 用户身份验证模式
        /// </summary>
        public readonly static string UserAuthentication = "UserScheme";
        /// <summary>
        /// 管理员 身份验证模式
        /// </summary>
        public readonly static string AdminAuthentication = "AdminScheme";

        /// <summary>
        /// 业务员 身份验证模式
        /// </summary>
        public readonly static string CountermanAuthentication = "CountermanScheme";

        /// <summary>
        /// 软文管理员 身份验证模式
        /// </summary>
        public readonly static string SoftWenAdminAuthentication = "SoftWenAdminScheme";
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS.ZhaoFa.Models.Api.Enum
{
    /// <summary>
    /// 注册方式 枚举
    /// </summary>
    public enum ApiRegisterFlag
    {
        /// <summary>
        /// 通过密码
        /// </summary>
        ByPwd = 0,
        /// <summary>
        /// 通过验证码
        /// </summary>
        ByValidate = 1
    }
}
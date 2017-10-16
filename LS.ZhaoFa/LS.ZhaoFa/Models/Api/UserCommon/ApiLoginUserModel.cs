using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS.ZhaoFa.Models.Api.UserCommon
{
    /// <summary>
    /// 用户登陆请求模型
    /// </summary>
    public class ApiLoginUserModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 密码 -可以为固定密码 或者手机邮箱验证码
        /// </summary>
        public string Pwd { get; set; }
        /// <summary>
        /// 本次登陆 关联的验证码
        /// </summary>
        public string Validate { get; set; }
    }
}
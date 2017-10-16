using LS.ZhaoFa.Models.Api.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS.ZhaoFa.Models.Api.UserCommon
{
    /// <summary>
    /// 用户注册模型
    /// </summary>
    public class ApiUserRegisterModel
    {
        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserAccount { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户密码 或者验证码
        /// </summary>
        public string UserPwd { get; set; }
        /// <summary>
        /// 用户注册类型 0-通过密码和手机号(账号) 1-通过手机号(账号)和验证码
        /// </summary>
        public ApiRegisterFlag Type { get; set; }


    }
}
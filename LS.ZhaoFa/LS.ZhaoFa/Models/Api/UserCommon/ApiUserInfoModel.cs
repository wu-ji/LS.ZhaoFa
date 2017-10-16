using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS.ZhaoFa.Models.Api.UserCommon
{
    /// <summary>
    /// api层用户模型
    /// </summary>
    public class ApiUserInfoModel
    {
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户等级 -普通用户 业务员 管理员(用户操作 请忽略)
        /// </summary>
        public string UserLv { get; set; }
        /// <summary>
        /// 用户头像地址
        /// </summary>
        public string UserImg { get; set; }
        /// <summary>
        /// 用户对应平台唯一标识(用户操作 请忽略)
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 用户账号(用户操作 请忽略)
        /// </summary>
        public string UserAccount { get; set; }

        /// <summary>
        /// 公司信息
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPwd { get; set; }

        /// <summary>
        /// 用户对应服务器的登陆凭证(应在 请求头中  此处不是必须)
        /// </summary>
        public string Token { get; set; }

    }
}
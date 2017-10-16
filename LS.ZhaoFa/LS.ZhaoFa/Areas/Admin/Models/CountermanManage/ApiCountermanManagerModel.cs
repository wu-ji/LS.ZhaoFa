using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LS.ZhaoFa.Areas.Admin.Models.CountermanManage
{
    /// <summary>
    /// api层管理员 对业务员管理模型
    /// </summary>
    public class ApiCountermanManagerModel
    {
        /// <summary>
        /// 主键 唯一标识(新增时 不需要)
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 业务员账号
        /// </summary>
        public string UserAccount { get; set; }

        /// <summary>
        /// 业务员密码
        /// </summary>
        public string UserPassWord { get; set; }

        /// <summary>
        /// 业务员昵称(非必须)
        /// </summary>
        public string UserName { get; set; }
    }
}
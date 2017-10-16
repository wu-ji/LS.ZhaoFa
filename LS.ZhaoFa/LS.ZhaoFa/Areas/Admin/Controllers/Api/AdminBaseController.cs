using LS.DBServer.EF;
using LS.UtilityTools.ApiTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace LS.ZhaoFa.Areas.Admin.Controllers.Api
{
    /// <summary>
    /// 管理员 控制器基类
    /// </summary>
    public class AdminBaseController : ApiController
    {
        /// <summary>
        /// 获取当前登陆的用户数据
        /// </summary>
        /// <returns></returns>
        [NonAction]
        protected UserInfo GetCurrentUserInfo()
        {
            return HttpContext.Current.Items[ApiCachePara.CacheUserKey] as UserInfo;
        }
    }
}

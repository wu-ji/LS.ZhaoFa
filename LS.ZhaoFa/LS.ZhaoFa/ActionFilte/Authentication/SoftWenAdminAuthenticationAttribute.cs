using LS.DBServer.EF;
using LS.UtilityTools;
using LS.UtilityTools.ApiTools;
using LS.ZhaoFa.Models.Api.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace LS.ZhaoFa.ActionFilte.Authentication
{
    /// <summary>
    /// 软文管理者 身份过滤器
    /// </summary>
    public class SoftWenAdminAuthenticationAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 控制器前 过滤
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                string returnJson = JsonConvert.SerializeObject(ApiReturnModel.ReturnError("请提交身份凭证 token 存放在请求头中的Authorization里"));

                actionContext.Response = new System.Net.Http.HttpResponseMessage();
                actionContext.Response.Content = new StringContent(returnJson);

                return;
            }

            #region 测试阶段凭证
            if (actionContext.Request.Headers.Authorization.Parameter == "123")
            {
                HttpContext.Current.Items[ApiCachePara.CacheUserKey] = new UserInfo()
                {
                    Id = new Guid("6AA3D73D-81CD-4260-879A-5A9AD80D6053"),
                    UserName = "wuji",
                    UserAccount = "wuji",
                    UserLv = 3,
                    CreateTime = DateTime.Now
                };
                return;
            }
            #endregion

            var scheme = actionContext.Request.Headers.Authorization.Scheme;
            if (scheme == AuthenticationPara.SoftWenAdminAuthentication)
            {
                var token = actionContext.Request.Headers.Authorization.Parameter;
                if (CacheHelper.GetCache($"{token}-{scheme}") == null) //没有对应的 缓存数据
                {
                    string returnJson = JsonConvert.SerializeObject(ApiReturnModel.ReturnIdentityInvalid("token失效 请重新登陆"));

                    actionContext.Response = new System.Net.Http.HttpResponseMessage();
                    actionContext.Response.Content = new StringContent(returnJson);

                    return;
                }
                else
                {
                    var userInfo = CacheHelper.GetCache($"{token}-{scheme}") as UserInfo;
                    HttpContext.Current.Items[ApiCachePara.CacheUserKey] = userInfo;

                    return;
                }

            }

            else
            {
                string returnJson = JsonConvert.SerializeObject(ApiReturnModel.ReturnError("Scheme 错误"));

                actionContext.Response = new System.Net.Http.HttpResponseMessage();
                actionContext.Response.Content = new StringContent(returnJson);

                return;
            }
        }
    }
}
using LS.DBServer.EF;
using LS.UtilityTools;
using LS.UtilityTools.ApiTools;
using LS.ZhaoFa.Models.Api.Common;
using LS.ZhaoFa.Models.Api.UserCommon;
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
    /// 管理员身份过滤
    /// </summary>
    public class AdminAuthenticationAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 
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
                    Id = new Guid("e4605c37-eafe-4ede-bcf9-9813124e8dc2"),
                    UserName = "wuji",
                    UserAccount = "wuji",
                    UserLv = 4,
                    CreateTime = DateTime.Now
                };
                return;
            }
            #endregion

            var scheme = actionContext.Request.Headers.Authorization.Scheme;
            if (scheme == AuthenticationPara.AdminAuthentication)
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
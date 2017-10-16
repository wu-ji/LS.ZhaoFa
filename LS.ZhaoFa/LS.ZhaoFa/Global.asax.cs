using LS.UtilityTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LS.ZhaoFa
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

           

            LogHelp.WriteLog();
        }

        /// <summary>
        /// 所有请求开始前 目前用来对 跨域进行设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var HttpMethod = Request.HttpMethod.ToUpper();
            if (HttpMethod == "OPTIONS") //如果是  options方式请求 直接返回 
            {
                Response.End();
            }
        }
    }
}

using System.Web.Mvc;

namespace LS.ZhaoFa.Areas.SoftWenAdmin
{
    /// <summary>
    /// 软文管理员
    /// </summary>
    public class SoftWenAdminAreaRegistration : AreaRegistration 
    {
        /// <summary>
        /// 
        /// </summary>
        public override string AreaName 
        {
            get 
            {
                return "SoftWenAdmin";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SoftWenAdmin_default",
                "SoftWenAdmin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
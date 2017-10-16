using System.Web.Mvc;

namespace LS.ZhaoFa.Areas.Counterman
{
    public class CountermanAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Counterman";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Counterman_default",
                "Counterman/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
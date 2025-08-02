using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Areas.Frameworks
{
    public class FrameworksAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Frameworks";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Frameworks_default",
                "Frameworks/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
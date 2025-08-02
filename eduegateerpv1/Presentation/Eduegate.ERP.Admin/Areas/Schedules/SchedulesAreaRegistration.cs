using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Areas.Schedules
{
    public class SchedulesAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Schedules";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Schedules_default",
                "Schedules/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
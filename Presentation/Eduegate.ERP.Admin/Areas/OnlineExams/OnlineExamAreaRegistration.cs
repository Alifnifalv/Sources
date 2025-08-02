using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Areas.OnlineExams
{
    public class OnlineExamAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "OnlineExams";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "OnlineExams_default",
                "OnlineExams/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }

    }
}
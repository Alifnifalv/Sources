using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Areas.DataFeed
{
    public class DataFeedAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DataFeed";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DataFeed_default",
                "DataFeed/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
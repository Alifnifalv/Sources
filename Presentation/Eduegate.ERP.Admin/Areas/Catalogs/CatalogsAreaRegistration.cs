using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Areas.Catalogs
{
    public class CatalogsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Catalogs";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Catalogs_default",
                "Catalogs/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Eduegate.OnlineExam.Portal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RegisterGlobalFilters(GlobalFilters.Filters);
        }
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            string isRequireHttpsAttribute = System.Configuration.ConfigurationManager.AppSettings["RequireHttpsAttribute"];

            filters.Add(new HandleErrorAttribute());
            if ((string.IsNullOrEmpty(isRequireHttpsAttribute) == true ? "0" : isRequireHttpsAttribute) == "1")
            {
                filters.Add(new RequireHttpsAttribute());
            }
        }
    }
}
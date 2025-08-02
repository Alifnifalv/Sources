using System.Web;
using System.Web.Mvc;
using Eduegate.Framework.Mvc.ActionFitlers;

namespace Eduegate.ERP.Admin
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new HandleJsonError());
        }
    }
}

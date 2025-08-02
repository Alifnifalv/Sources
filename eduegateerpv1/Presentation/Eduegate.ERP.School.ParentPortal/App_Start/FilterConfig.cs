using Eduegate.Framework.Mvc.ActionFitlers;
using System.Web;
using System.Web.Mvc;

namespace Eduegate.ERP.School.ParentPortal
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

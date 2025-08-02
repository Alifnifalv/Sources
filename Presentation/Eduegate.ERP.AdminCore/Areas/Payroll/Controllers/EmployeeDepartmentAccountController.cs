using Eduegate.ERP.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Eduegate.ERP.AdminCore.Areas.Payroll.Controllers
{
    [Area("Payroll")]
    public  class EmployeeDepartmentAccountController : BaseSearchController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}

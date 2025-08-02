using Microsoft.AspNetCore.Mvc;
using Eduegate.ERP.Admin.Controllers;

namespace Eduegate.ERP.Admin.Areas.AI.Controllers
{
    [Area("AI")]
    public class VirtualTeacherController : BaseSearchController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Teaching()
        {
            return View();
        }

    }
}
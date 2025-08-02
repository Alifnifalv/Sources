using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Areas.Settings.Controllers
{
    public class PrintSettingController : Controller
    {
        // GET: Settings/PrintSetting
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Configure()
        {
            return View();
        }
    }
}
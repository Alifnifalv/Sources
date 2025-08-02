using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace Eduegate.ERP.Admin.Areas.Schedules.Controllers
{
    [Area("Schedules")]
    public class ScheduleController : Controller
    {
        // GET: Schedules/Schedule
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Show()
        {
            return View();
        }
    }
}
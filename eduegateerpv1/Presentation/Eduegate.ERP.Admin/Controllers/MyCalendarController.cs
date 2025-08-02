using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Controllers
{
    public class MyCalendarController : Controller
    {
        // GET: MyCalendar
        public ActionResult Index(string parameters = "")
        {
            return View();
        }

        public ActionResult Create(string parameters = "")
        {
            return View();
        }
    }
}
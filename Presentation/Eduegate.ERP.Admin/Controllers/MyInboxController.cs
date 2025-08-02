using Eduegate.Frameworks.Mvc.Controllers;
using Eduegate.Services.Client.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Controllers
{
    public class MyInboxController : BaseController
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

        public JsonResult GetAlerts()
        {
            var alerts = ClientFactory.NotificationServiceClient(CallContext).GetAlerts(CallContext.LoginID.Value);
            return Json(alerts, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            var alerts = ClientFactory.NotificationServiceClient(CallContext).GetAlert(IID);
            return View(alerts);
        }
    }
}
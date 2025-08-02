using Eduegate.Frameworks.Mvc.Controllers;
using Eduegate.Services.Client.Factory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Controllers
{
    public class ActivityController : BaseController
    {
        // GET: Activity
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetActivities()
        {
            if (ConfigurationManager.ConnectionStrings["EduegatedERP_LoggerContext"] != null)
            {
                var alerts = ClientFactory.LoggingServicesClient(CallContext).GetActivitiesByLoginID(CallContext.LoginID.Value);
                return Json(alerts, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            var alerts = ClientFactory.LoggingServicesClient(CallContext).GetActivity(IID);
            return View(alerts);
        }
    }
}
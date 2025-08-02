using Eduegate.Frameworks.Mvc.Controllers;
using Eduegate.Services.Client.Factory;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Eduegate.OnlineExam.Portal.Controllers
{
    public class ExamSettingController : BaseController
    {
        // GET: Setting
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ScreenFieldSetting()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetViews()
        {
            return Json(ClientFactory.SettingServiceClient(CallContext).GetViews().Where(a=> a.ViewTypeID == 1), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSettingValueByKey(string settingKey)
        {
            try
            {
                var settingValue = ClientFactory.SettingServiceClient(CallContext).GetSettingValueByKey(settingKey);
                return Json(settingValue, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { IsError = true, UserMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
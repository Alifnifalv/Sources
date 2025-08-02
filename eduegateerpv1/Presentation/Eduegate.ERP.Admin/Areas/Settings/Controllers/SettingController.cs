using Eduegate.Framework;
using Eduegate.Frameworks.Mvc.Controllers;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Settings;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Areas.Settings.Controllers
{
    public class SettingController : BaseController
    {
        // GET: Setting
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Configure()
        {
            return View(new List<SettingConfigureGroupViewModel>());
        }

        [HttpGet]
        public JsonResult GetSettings()
        {
            return Json(SettingConfigureGroupViewModel.ToGroupVM(ClientFactory.SettingServiceClient(CallContext).GetAllSettings()), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveSettings(SettingConfigureViewModel setting)
        {
            try
            {
                var updatedVM = ClientFactory.SettingServiceClient(CallContext).SaveSetting(SettingConfigureViewModel.ToDTO(setting));
                return Json(updatedVM, JsonRequestBehavior.AllowGet);
            }catch(Exception ex)
            {
                Eduegate.Logger.LogHelper<SettingController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
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
                Eduegate.Logger.LogHelper<SettingController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
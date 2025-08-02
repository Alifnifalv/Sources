using Eduegate.Application.Mvc;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Eduegate.Signup.Portal.Controllers
{
    public class SettingController : BaseController
    {
        // GET: Setting
        public ActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public ActionResult Configure()
        //{
        //    return View(new List<SettingConfigureGroupViewModel>());
        //}

        //[HttpGet]
        //public JsonResult GetSettings()
        //{
        //    return Json(SettingConfigureGroupViewModel.ToGroupVM(ClientFactory.SettingServiceClient(CallContext).GetAllSettings()));
        //}

        //[HttpPost]
        //public JsonResult SaveSettings(SettingConfigureViewModel setting)
        //{
        //    try
        //    {
        //        var updatedVM = ClientFactory.SettingServiceClient(CallContext).SaveSetting(SettingConfigureViewModel.ToDTO(setting));
        //        return Json(updatedVM);
        //    }catch(Exception ex)
        //    {
        //        Eduegate.Logger.LogHelper<SettingController>.Fatal(ex.Message.ToString(), ex);
        //        return Json(new { IsError = true, UserMessage = ex.Message });
        //    }
        //}

        [HttpGet]
        public ActionResult ScreenFieldSetting()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetViews()
        {
            var views = ClientFactory.SettingServiceClient(CallContext).GetViews();
            return Ok(views == null ? null : views.Where(a=> a.ViewTypeID == 1));
        }

        [HttpGet]
        public JsonResult GetSettingValueByKey(string settingKey)
        {
            try
            {
                var settingValue = ClientFactory.SettingServiceClient(CallContext).GetSettingValueByKey(settingKey);
                return Json(settingValue);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<SettingController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message });
            }
        }

    }
}
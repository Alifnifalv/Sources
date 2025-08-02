using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Application.Mvc;
using Eduegate.Domain.Entity.Models;
using Microsoft.IdentityModel.Tokens;
using Eduegate.Services.Client.Factory;

namespace Eduegate.OnlineExam.Portal.Controllers
{
    public class ExamSettingController : BaseController
    {
        // GET: Setting
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ScreenFieldSetting()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetViews()
        {
            return new JsonResult(ClientFactory.SettingServiceClient(CallContext).GetViews().Where(a=> a.ViewTypeID == 1));
        }

        [HttpGet]
        public JsonResult GetSettingValueByKey(string settingKey)
        {
            try
            {
                var settingValue = ClientFactory.SettingServiceClient(CallContext).GetSettingValueByKey(settingKey);
                return new JsonResult(settingValue);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { IsError = true, UserMessage = ex.Message });
            }
        }

    }
}
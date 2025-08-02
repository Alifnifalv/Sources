using Eduegate.ERP.Admin.Controllers;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Web.Library.HR.Payroll;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Domain.Entity.HR.Payroll;
using Eduegate.Services.Contracts.CommonDTO;
using Eduegate.ERP.Admin.Areas.Settings.Controllers;
namespace Eduegate.ERP.AdminCore.Areas.Payroll.Controllers
{
    [Area("Payroll")]
    public class PayrollController : BaseSearchController
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetSettingValueByKey(string settingKey)
        {
            try
            {
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
                var settingValue = ClientFactory.SettingServiceClient(CallContext).GetSettingValueByKey(settingKey);

                DateTime dateTime = DateTime.ParseExact(settingValue, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                string formattedDate = dateTime.ToString(dateFormat);
                Console.WriteLine(formattedDate);  
                return Json(formattedDate);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<SettingController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message });
            }
        }
    }
}

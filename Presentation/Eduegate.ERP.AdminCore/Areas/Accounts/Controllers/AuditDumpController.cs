using Microsoft.AspNetCore.Mvc;
using Eduegate.Services.Client.Factory;
using Eduegate.Application.Mvc;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Web.Library.ViewModels.Frameworks;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Contracts.Accounts.Accounting;

namespace Eduegate.ERP.Admin.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    public class AuditDumpController : BaseController
    {
        // GET: Schools/Mark
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetFiscalYearByFiscalYear(int fiscalYearID)
        {
            var datas = ClientFactory.AccountingServiceClient(CallContext).GetFiscalYearByFiscalYear(fiscalYearID);

            return Json(datas);
        }

        public ActionResult AuditDump(int budgetID = 0)
        {
            return View();
        }

        [HttpPost]
        public IActionResult DownloadDatas([FromBody] AuditDataDTO dto)
        {
            var data = ClientFactory.AccountingServiceClient(CallContext).DownloadDatas(dto);

            if (data.ContentBytes == null || data.ContentBytes.Length == 0)
            {
                return BadRequest("No data found to download.");
            }

            Response.Headers["Content-Disposition"] = $"attachment; filename={data.FileName}";

            return File(data.ContentBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", data.FileName);
        }

    }
}
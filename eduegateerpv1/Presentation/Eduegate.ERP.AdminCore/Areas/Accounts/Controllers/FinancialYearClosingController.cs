using Microsoft.AspNetCore.Mvc;
using Eduegate.Services.Client.Factory;
using Eduegate.Application.Mvc;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Web.Library.ViewModels.Frameworks;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Accounting;

namespace Eduegate.ERP.Admin.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    public class FinancialYearClosingController : BaseController
    {
        // GET: Schools/Mark
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetFiscalYearDetails()
        {
            var viewModelList = ClientFactory.AccountingServiceClient(CallContext).GetFiscalYearDetails();

            return Json(viewModelList);
        }

        public ActionResult FinancialYearClosing(int budgetID = 0)
        {
            return View();
        }

        //public JsonResult GetSmartTreeView(SmartViewType type, long? parentID = null, string searchText = "")
        //{
        //    var vm = SmartTreeViewViewModel.ToVM(
        //        ClientFactory.SmartViewServiceClient(CallContext)
        //                     .GetSmartTreeView(type, parentID, searchText));
        //    return Json(vm);
        //}

        public JsonResult GetSmartTreeView(SmartViewType type, long? parentID = null, string searchText = "")
        {
            var vm = SmartTreeViewViewModel.ToVM(
                ClientFactory.SmartViewServiceClient(CallContext)
                             .GetSmartTreeView(type, parentID, searchText));
            return Json(vm);
        }

        [HttpPost]
        public ActionResult SaveFYCEntries([FromBody] FiscalYearDTO dto)
        {
            var message = new OperationResultDTO();
            message = ClientFactory.AccountingServiceClient(CallContext).SaveFYCEntries(dto.Company_ID,dto.Prv_FiscalYear_ID,dto.FiscalYear_ID);

            if (message.operationResult == OperationResult.Error)
            {
                return Json(new { IsError = true, Response = message.Message });
            }
            else
            {
                return Json(new { IsError = false, Response = message.Message });
            }
        }
    }
}
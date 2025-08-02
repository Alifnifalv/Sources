using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper;
using Eduegate.Frameworks.Enums;
using Eduegate.Infrastructure.Enums;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Contracts.Framework;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Accounts;
using Eduegate.Web.Library.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Domain;

namespace Eduegate.ERP.Admin.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    public class ReceiptAllocationController : BaseSearchController
    {
        private string dateTimeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateTimeFormat");

        private static string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }

        private string ReferenceServiceUrl = string.Concat(ServiceHost, Constants.REFERENCE_DATA_SERVICE);

        // GET: Inventories/InventoryDetails
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.RVInvoice);
            return View(new SearchListViewModel
            {
                ControllerName = "Accounts/" + Eduegate.Infrastructure.Enums.SearchView.RVInvoiceAllocation.ToString(),
                ViewName = Eduegate.Infrastructure.Enums.SearchView.RVInvoiceAllocation,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                IsEditableLink = true,
                InfoBar = @"<li class='status-label-mobile'>
                                        <div class='right status-label'>
                                            <div class='status-label-color'><label class='status-color-label  green'></label>Average Quantity</div>
                                            <div class='status-label-color'><label class='status-color-label orange'></label>More Quantity</div>
                                            <div class='status-label-color'><label class='status-color-label red'></label>Less Quantity</div>
                                        </div>
                                    </li>",
                IsChild = false,
                HasChild = true,
                FilterColumns = metadata.QuickFilterColumns,
                HasFilters = metadata.HasFilters,
                UserValues = metadata.UserValues
            });
        }

        public ActionResult Allocate(string invoiceIDs)
        {
            var viewModel = CRUDMasterDetailViewModel.FromDTO(ClientFactory.FrameworkServiceClient(CallContext).GetScreenMetadata((long)Screens.RVInvoiceAllocation));
            viewModel.ReferenceIIDs = invoiceIDs;
            TempData["viewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(viewModel);
            return RedirectToAction("CreateMasterDetail", "CRUD", new { screen = Screens.RVInvoiceAllocation.ToString(), area = "Frameworks", ID = -1, parameters = "InvoiceIDs =" + invoiceIDs });
        }

        [HttpGet]
        public ActionResult Get(long ID=0)
        {
            var receipts = ClientFactory.AccountingTransactionServiceClient(CallContext).GetAllocatedReceivables(ID);
            List<ReceivableDTO> alloctions = null;
            var isAllocation = false;
            if (receipts.PaidAmount > 0 && receipts.ReferenceReceivables.Count > 0)
            {
                alloctions = receipts.ReferenceReceivables;
                isAllocation = true;               
            }
            else
            {
                alloctions = ClientFactory.AccountingTransactionServiceClient(CallContext).GetCustomerPendingInvoices(receipts.AccountID.Value);
            }

            var vm = RVInvoiceAllocationViewModel.ToVM(receipts, alloctions);

            if (isAllocation)
            {
                foreach (var detail in vm.DetailViewModel)
                {
                    detail.IsRowSelected = true;
                }
            }

            return Json(vm);
        }
    }
}
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Framework.Helper;
using Eduegate.Web.Library.ViewModels.Accounts;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.ViewModels.AccountTransactions;
using Eduegate.Domain;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Accounts.Accounting;
using Eduegate.ERP.Admin.Helpers;

namespace Eduegate.ERP.Admin.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    public class PVInvoiceController : BaseSearchController
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
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.PVInvoice);
            return View(new SearchListViewModel
            {
                ControllerName = "Accounts/" + Eduegate.Infrastructure.Enums.SearchView.PVInvoice.ToString(),
                ViewName = Eduegate.Infrastructure.Enums.SearchView.PVInvoice,
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

        [HttpGet]
        public async Task<IActionResult> ChildList(long ID)
        {
            // call the service and get the meta data
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList(Services.Contracts.Enums.SearchView.PVInvoice);

            return PartialView("_ChildList", new SearchListViewModel
            {
                ViewName = Eduegate.Infrastructure.Enums.SearchView.PVInvoice,
                HeaderList = metadata.Columns,
                // SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                FilterColumns = metadata.QuickFilterColumns,
                RuntimeFilter = Convert.ToString("HeadIID=" + ID.ToString()).Trim(), // child view must have this column
                IsChild = true,
                ParentViewName = Eduegate.Infrastructure.Enums.SearchView.PVInvoice,
                IsEditableLink = false
            });
        }


        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {

            var view = Eduegate.Infrastructure.Enums.SearchView.PVInvoice;
            if (runtimeFilter.Trim().IsNotNullOrEmpty())
                view = Eduegate.Infrastructure.Enums.SearchView.PVInvoice;
            else
                runtimeFilter = runtimeFilter + " DocumentTypeID=" + 1517;

            return base.SearchData(view, currentPage, orderBy, runtimeFilter);

        }


        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.PVInvoiceSummary);
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View();
        }
        [HttpGet]
        public ActionResult Create(long ID = 0)
        {
            var vm = new PVInvoiceViewModel()
            {
                DetailViewModel = new List<PVInvoiceDetailViewModel>() { new PVInvoiceDetailViewModel()
                {
                    PaymentDueDate=DateTime.Now.ToString(dateTimeFormat)
                }
           },
                MasterViewModel = new PVInvoiceMasterViewModel()
                {
                    TransactionDate = DateTime.Now.ToString(dateTimeFormat),
                    DocumentStatus = new KeyValueViewModel() { Key = "106", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentStatuses.New) },
                    TransactionStatus = new KeyValueViewModel() { Key = "1", Value = Convert.ToString(Eduegate.Framework.Enums.TransactionStatus.New) },
                    Currency = KeyValueViewModel.ToViewModel(ClientFactory.ReferenceDataServiceClient(CallContext).GetDefaultCurrencyWithName()),
                    DocumentType = new KeyValueViewModel() { Key = "1517", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.Payments) },
                    DocumentReferenceTypeID = (int)Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.Payments

                }
            };

            return Json(vm);
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpPost]
        public JsonResult Save(string model)
        {
            var jsonData = JObject.Parse(model);
            var pvInvoiceViewModel = JsonConvert.DeserializeObject<PVInvoiceViewModel>(jsonData.SelectToken("data").ToString());

            try
            {
                if (pvInvoiceViewModel != null)
                {
                    if (pvInvoiceViewModel.DetailViewModel != null)
                    {
                        pvInvoiceViewModel.DetailViewModel = pvInvoiceViewModel.DetailViewModel.Where(x => !string.IsNullOrEmpty(x.InvoiceNumber)).ToList();
                    }

                    AccountTransactionHeadDTO HeadDTO = new PVInvoiceViewModel().ToAccountTransactionHeadDTO(pvInvoiceViewModel);

                    AccountTransactionHeadDTO dto = ClientFactory.AccountingTransactionServiceClient(CallContext).SaveAccountTransactionHead(HeadDTO);
                    pvInvoiceViewModel = new PVInvoiceViewModel().ToPVInvoiceViewModel(dto);

                    if (pvInvoiceViewModel.MasterViewModel.IsError && !string.IsNullOrEmpty(pvInvoiceViewModel.MasterViewModel.ErrorCode))
                    {
                        return Json(new { IsError = true, UserMessage = PortalWebHelper.GetErrorMessage(pvInvoiceViewModel.MasterViewModel.ErrorCode), data = pvInvoiceViewModel });
                    }
                    if (pvInvoiceViewModel.MasterViewModel.IsTransactionCompleted)
                    {
                        return Json(new { IsError = true, UserMessage = "Cannot update the trasaction, it's already processed.", data = pvInvoiceViewModel });
                    }

                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<PVInvoiceController>.Fatal(exception.Message.ToString(), exception);
                return Json(new { IsError = !false, UserMessage = exception.Message.ToString() });
            }

            return Json(pvInvoiceViewModel);
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            AccountTransactionHeadDTO dto = ClientFactory.AccountingTransactionServiceClient(CallContext).GetAccountTransactionHeadById(ID);
            PVInvoiceViewModel pvInvoiceViewModel = new PVInvoiceViewModel().ToPVInvoiceViewModel(dto);
            return Json(pvInvoiceViewModel);
        }


        [HttpGet]
        public ActionResult GetPayablesByAccountId(long AccountID, Eduegate.Services.Contracts.Enums.DocumentReferenceTypes DocumentType)
        {
            var PayablesDTO = ClientFactory.AccountingTransactionServiceClient(CallContext).GetPayablesByAccountId(AccountID, DocumentType);
            return Json(PayablesDTO);
        }

        [HttpGet]
        public ActionResult GetPendingInvoices(long supplierID)
        {
            var dtos = ClientFactory.AccountingTransactionServiceClient(CallContext).GetSupplierPendingInvoices(supplierID);
            var rvInvoiceViewModel = new PayableViewModel().ToVM(dtos);
            return Json(rvInvoiceViewModel);
        }
    }
}

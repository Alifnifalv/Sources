using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Framework.Helper;
using Eduegate.Web.Library.ViewModels.Accounts;
using Eduegate.Web.Library.ViewModels.AccountTransactions;
using Eduegate.Services.Client.Factory;
using Eduegate.Domain;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Eduegate.ERP.Admin.Helpers;
using Eduegate.Services.Contracts;
using Eduegate.TransactionEgine.Accounting;
using Eduegate.Domain.Entity.Models;
using Eduegate.Services.Contracts.Accounts.Accounting;
using Eduegate.Domain.Entity.School.Models;

namespace Eduegate.ERP.Admin.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    public class RVInvoiceController : BaseSearchController
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
                ControllerName = "Accounts/" + Eduegate.Infrastructure.Enums.SearchView.RVInvoice.ToString(),
                ViewName = Eduegate.Infrastructure.Enums.SearchView.RVInvoice,
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
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList(Services.Contracts.Enums.SearchView.RVInvoice);

            return PartialView("_ChildList", new SearchListViewModel
            {
                ViewName = Eduegate.Infrastructure.Enums.SearchView.RVInvoice,
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
                ParentViewName = Eduegate.Infrastructure.Enums.SearchView.RVInvoice,
                IsEditableLink = false
            });
        }


        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {

            var view = Eduegate.Infrastructure.Enums.SearchView.RVInvoice;
            if (runtimeFilter.Trim().IsNotNullOrEmpty())
                view = Eduegate.Infrastructure.Enums.SearchView.RVInvoice;
            else
                runtimeFilter = runtimeFilter + " DocumentTypeID=" + 1511;

            return base.SearchData(view, currentPage, orderBy, runtimeFilter);

        }


        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.RVInvoiceSummary);
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
            var vm = new RVInvoiceViewModel()
            {
                DetailViewModel = new List<RVInvoiceDetailViewModel>() { new RVInvoiceDetailViewModel()
                    {
                        PaymentDueDate=DateTime.Now.ToString(dateTimeFormat)
                    }
                },
                MasterViewModel = new RVInvoiceMasterViewModel()
                {
                    TransactionDate = DateTime.Now.ToString(dateTimeFormat),
                    DocumentStatus = new KeyValueViewModel() { Key = "82", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentStatuses.New) },
                    TransactionStatus = new KeyValueViewModel() { Key = "1", Value = Convert.ToString(Eduegate.Framework.Enums.TransactionStatus.New) },
                    Currency = KeyValueViewModel.ToViewModel(ClientFactory.ReferenceDataServiceClient(CallContext).GetDefaultCurrencyWithName()),
                    DocumentType = new KeyValueViewModel() { Key = "54", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.Receipts) },
                    DocumentReferenceTypeID = (int)Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.Receipts

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
            var rvInvoiceViewModel = JsonConvert.DeserializeObject<RVInvoiceViewModel>(jsonData.SelectToken("data").ToString());

            try
            {
                if (rvInvoiceViewModel != null)
                {
                    if (rvInvoiceViewModel.DetailViewModel != null && rvInvoiceViewModel.DetailViewModel.Any(x => x.IsRowSelected == true))
                    {
                        if (rvInvoiceViewModel.DetailViewModel.Any(x => x.IsRowSelected==true && x.InvoiceNumber!=null && (x.Amount ?? 0) <= 0))
                        {
                            return Json(new { IsError = true, UserMessage = "The amount cannot be zero for selected entry!" });
                        }
                        if (rvInvoiceViewModel.DetailViewModel.Any(x => x.IsRowSelected == true && (x.Amount ?? 0) > ((x.InvoiceAmount ?? 0) - (x.PaidAmount ?? 0))))
                        {
                            return Json(new { IsError = true, UserMessage = "The amount cannot exceed the balance to pay!" });
                        }
                    }
                    else
                        return Json(new { IsError = true, UserMessage = "No rows have been selected or found!" });

                    var headDTO = new RVInvoiceViewModel().ToAccountTransactionHeadDTO(rvInvoiceViewModel);
                    var dto = ClientFactory.AccountingTransactionServiceClient(CallContext).SaveAccountTransactionHead(headDTO);
                    rvInvoiceViewModel = new RVInvoiceViewModel().ToRVInvoiceViewModel(dto);

                    if (rvInvoiceViewModel.MasterViewModel.IsError && !string.IsNullOrEmpty(rvInvoiceViewModel.MasterViewModel.ErrorCode))
                    {
                        return Json(new { IsError = true, UserMessage = PortalWebHelper.GetErrorMessage(rvInvoiceViewModel.MasterViewModel.ErrorCode), data = rvInvoiceViewModel });
                    }
                    if (rvInvoiceViewModel.MasterViewModel.IsTransactionCompleted)
                    {
                        return Json(new { IsError = true, UserMessage = "Cannot update the trasaction, it's already processed.", data = rvInvoiceViewModel });
                    }
                    var accountTransactionReceivablesMaps = new List<AccountTransactionReceivablesMapDTO>();
                   
                    foreach (var transactionDetails in dto.AccountTransactionDetails)
                    {
                        if (transactionDetails.Amount != 0 && transactionDetails.ReceivableID.HasValue && transactionDetails.ReceivableID != 0 && dto.AccountTransactionHeadIID != 0)
                        {
                            accountTransactionReceivablesMaps.Add(new AccountTransactionReceivablesMapDTO { HeadID = dto.AccountTransactionHeadIID, ReceivableID = transactionDetails.ReceivableID, Amount = transactionDetails.Amount });
                            break;
                        }

                    }
                    new AccountingBase().AddAccountTransactionReceivablesMaps(accountTransactionReceivablesMaps);
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<RVInvoiceController>.Fatal(exception.Message.ToString(), exception);
                return Json(new { IsError = !false, UserMessage = exception.Message.ToString() });
            }

            return Json(rvInvoiceViewModel);
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            var dto = ClientFactory.AccountingTransactionServiceClient(CallContext).GetAccountTransactionHeadById(ID);
            var rvInvoiceViewModel = new RVInvoiceViewModel().ToRVInvoiceViewModel(dto);
            return Json(rvInvoiceViewModel);
        }

        [HttpGet]
        public ActionResult GetPendingInvoices(long customerID)
        {
            var dtos = ClientFactory.AccountingTransactionServiceClient(CallContext).GetCustomerPendingInvoices(customerID);
            var rvInvoiceViewModel = new ReceivableViewModel().ToVM(dtos);
            return Json(rvInvoiceViewModel);
        }

        #region GetNextTransactionNumberByMonthYear
        [HttpPost]
        public JsonResult GetNextTransactionNumberByMonthYear(RVMissionViewModel vm)
        {
            if (vm != null && vm.MasterViewModel != null && vm.MasterViewModel.DocumentType != null
                                                && vm.MasterViewModel.PaymentModes != null
                                                && vm.MasterViewModel.TransactionDate != null)
            {
                var dto = new TransactionNumberDTO();
                dto.DocumentTypeID = Convert.ToInt32(vm.MasterViewModel.DocumentType.Key);
                dto.Month = (Convert.ToDateTime(vm.MasterViewModel.TransactionDate)).Month;
                dto.Year = (Convert.ToDateTime(vm.MasterViewModel.TransactionDate)).Year;
                string NextTransactionNumber = ClientFactory.MutualServiceClient(CallContext).GetNextTransactionNumberByMonthYear(dto);
                return Json(NextTransactionNumber);
            }
            return Json("");

        }
        #endregion
    }
}

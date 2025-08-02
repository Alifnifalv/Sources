using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Framework.Helper;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Web.Library.ViewModels.AccountTransactions;
using Eduegate.Services.Client.Factory;
using Eduegate.Domain;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Eduegate.Services.Contracts.Accounts.Accounting;
using Eduegate.ERP.Admin.Helpers;
using Eduegate.TransactionEgine.Accounting;
using OfficeOpenXml.Export.ToDataTable;
using Humanizer;
using System.Text.RegularExpressions;
using Microsoft.Identity.Client;

namespace Eduegate.ERP.Admin.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    public class RegularCreditController : BaseSearchController
    {

        private string dateTimeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateTimeFormat");

        private static string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }

        private string ReferenceServiceUrl = string.Concat(ServiceHost, Constants.REFERENCE_DATA_SERVICE);

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.RegularCredit);
            return View(new SearchListViewModel
            {
                ControllerName = "Accounts/" + Eduegate.Infrastructure.Enums.SearchView.RegularCredit.ToString(),
                ViewName = Eduegate.Infrastructure.Enums.SearchView.RegularCredit,
                HeaderList = metadata.Columns,
                // SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
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
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList(Services.Contracts.Enums.SearchView.RegularCredit);

            return PartialView("_ChildList", new SearchListViewModel
            {
                ViewName = Eduegate.Infrastructure.Enums.SearchView.RegularCredit,
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
                ParentViewName = Eduegate.Infrastructure.Enums.SearchView.RegularCredit,
                IsEditableLink = false
            });
        }


        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {

            var view = Eduegate.Infrastructure.Enums.SearchView.RegularCredit;
            if (runtimeFilter.Trim().IsNotNullOrEmpty())
                view = Eduegate.Infrastructure.Enums.SearchView.RegularCredit;
            else
                runtimeFilter = runtimeFilter + " DocumentTypeID=" + 1519;
            return base.SearchData(view, currentPage, orderBy, runtimeFilter);

        }


        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.RegularCreditSummary);
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
            var vm = new RegularCreditViewModel()
            {
                DetailViewModel = new List<RegularCreditDetailViewModel>() { new RegularCreditDetailViewModel() { } },
                MasterViewModel = new RegularCreditMasterViewModel()
                {
                    TransactionDate = DateTime.Now.ToString(dateTimeFormat),
                    DocumentStatus = new KeyValueViewModel() { Key = "114", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentStatuses.New) },
                    TransactionStatus = new KeyValueViewModel() { Key = "1", Value = Convert.ToString(Eduegate.Framework.Enums.TransactionStatus.New) },
                    Currency = KeyValueViewModel.ToViewModel(ClientFactory.ReferenceDataServiceClient(CallContext).GetDefaultCurrencyWithName()),
                    DocumentType = new KeyValueViewModel() { Key = "1519", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.CreditNote) },

                }
            };

            return Json(vm);
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }
        #region GetNextTransactionNumberByMonthYear
        [HttpPost]
        public JsonResult GetNextTransactionNumberByMonthYear(RegularCreditViewModel vm)
        {
            if (vm != null && vm.MasterViewModel != null && vm.MasterViewModel.DocumentType != null
                                                //&& vm.MasterViewModel.PaymentModes !=null 
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


        //[HttpGet]
        //public ActionResult GetReceivablesByAccountId(long AccountID)
        //{
        //    var ReceivablesDTO = new AccountingTransactionServiceClient(CallContext).GetReceivablesByAccountId(AccountID, Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.DistributionJobs);
        //    return Json(ReceivablesDTO);
        //}


        [HttpPost]
        public JsonResult Save(string model)
        {

            var jsonData = JObject.Parse(model);
            var regularCreditViewModel = JsonConvert.DeserializeObject<RegularCreditViewModel>(jsonData.SelectToken("data").ToString());
           
            try
            {
                if (regularCreditViewModel != null)
                {
                    if (regularCreditViewModel.DetailViewModel != null)
                    {
                        regularCreditViewModel.DetailViewModel = regularCreditViewModel.DetailViewModel
                            .Where(x => x.Account != null && !string.IsNullOrEmpty(x.Account.Key)).ToList();

                        if (regularCreditViewModel.DetailViewModel.Any(x => x.Account == null /*&& x.VendorCustomerAccounts == null*/))
                        {
                            return Json(new { IsError = true, UserMessage = "Please select  account" });
                        }
                        if (regularCreditViewModel.DetailViewModel.Any(x => (x.Credit == 0 || x.Credit == null) && (x.Debit == 0 || x.Debit == null)))
                        {
                            return Json(new { IsError = true, UserMessage = "Please enter Debit or Credit amount" });
                        }
                        if (regularCreditViewModel.DetailViewModel.Any(x => x.Credit > 0 && x.Debit > 0))
                        {
                            return Json(new { IsError = true, UserMessage = "Both credit and Debit cannot be allowed for an item" });
                        }
                        if (regularCreditViewModel.DetailViewModel.Sum(x => x.Credit) != regularCreditViewModel.DetailViewModel.Sum(x => x.Debit))
                        {
                            return Json(new { IsError = true, UserMessage = "Credit Amount and Debit amount should be equal" });
                        }

                    }
                    var receivableID = regularCreditViewModel.MasterViewModel.ReceivableID;

                    var HeadDTO = new RegularCreditViewModel().ToAccountTransactionHeadDTO(regularCreditViewModel);
                    var dto = ClientFactory.AccountingTransactionServiceClient(CallContext).SaveAccountTransactionHead(HeadDTO);
                    if (!string.IsNullOrEmpty(dto.Remarks))
                    {
                        var remarksData = ParseRemarks(dto.Remarks);

                        regularCreditViewModel.MasterViewModel.Remarks = remarksData.Remarks;
                        regularCreditViewModel.MasterViewModel.ReferenceHeadID = remarksData.ReferenceHeadID;
                        regularCreditViewModel.MasterViewModel.ReceivableID = remarksData.ReceivableID;
                        regularCreditViewModel.MasterViewModel.PendingAmount = remarksData.Amount;
                        regularCreditViewModel.MasterViewModel.InvoiceID = new KeyValueViewModel() { Key = $"{remarksData.ReferenceHeadID}#{remarksData.ReceivableID}", Value = $"{remarksData.InvoiceNumber} ({Math.Round((remarksData.Amount??0), 2)})" };
                    }
                    if (regularCreditViewModel.MasterViewModel.IsError && !string.IsNullOrEmpty(regularCreditViewModel.MasterViewModel.ErrorCode))
                    {
                        return Json(new { IsError = true, UserMessage = PortalWebHelper.GetErrorMessage(regularCreditViewModel.MasterViewModel.ErrorCode), data = regularCreditViewModel });
                    }
                    if (regularCreditViewModel.MasterViewModel.IsTransactionCompleted)
                    {
                        return Json(new { IsError = true, UserMessage = "Cannot update the trasaction, it's already processed.", data = regularCreditViewModel });
                    }
                    if (regularCreditViewModel.MasterViewModel.ReceivableID != 0)
                    {
                        var accountTransactionReceivablesMaps = new List<AccountTransactionReceivablesMapDTO>();
                      var  accountID = regularCreditViewModel.MasterViewModel.AccountID == null ? long.Parse(regularCreditViewModel.MasterViewModel.Account?.Key) : regularCreditViewModel.MasterViewModel.AccountID;
                        foreach (var transactionDetails in dto.AccountTransactionDetails)
                        {
                            if (transactionDetails.AccountID != null && transactionDetails.AccountID == accountID && transactionDetails.Amount != 0 && regularCreditViewModel.MasterViewModel.ReceivableID.HasValue && regularCreditViewModel.MasterViewModel.ReceivableID != 0 && dto.AccountTransactionHeadIID != 0)
                            {
                                accountTransactionReceivablesMaps.Add(new AccountTransactionReceivablesMapDTO { HeadID = dto.AccountTransactionHeadIID, ReceivableID = regularCreditViewModel.MasterViewModel.ReceivableID, Amount = -1 * transactionDetails.Amount });
                                break;

                            }
                        }
                        if (accountTransactionReceivablesMaps.Count() > 0)
                            new AccountingBase().AddAccountTransactionReceivablesMaps(accountTransactionReceivablesMaps);
                    }
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<RegularCreditController>.Fatal(exception.Message.ToString(), exception);
                return Json(new { IsError = !false, UserMessage = exception.Message.ToString() });
            }

            return Json(regularCreditViewModel);
        }
        public JsonResult GetCustomerPendingInvoices(long accountID, int branchID)
        {
            return Json(ClientFactory.AccountingTransactionServiceClient(CallContext).GetCustomerPendingInvoices(accountID, branchID));

        }

        public JsonResult GetAccountTransactionsDetails(long headID, long receivableID)
        {
            var transaction = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().GetAccountTransactionHeadById(headID);
            var detailViewModelList = new List<RegularCreditDetailViewModel>();

            if (transaction != null && transaction.AccountTransactionDetails.Count() > 0)
            {
                foreach (var detailDTOItem in transaction.AccountTransactionDetails)
                {
                    var detailViewModel = new RegularCreditDetailViewModel();

                    detailViewModel.Amount = detailDTOItem.Amount;
                    detailViewModel.AccountID = detailDTOItem.AccountID;
                    //detailViewModel.AccountTransactionDetailIID = detailDTOItem.AccountTransactionDetailIID;
                    //detailViewModel.AccountTransactionHeadID = transaction.AccountTransactionHeadIID;
                    detailViewModel.CostCenterID = detailDTOItem.CostCenterID;
                    detailViewModel.UpdatedBy = detailDTOItem.UpdatedBy;
                    detailViewModel.UpdatedDate = detailDTOItem.UpdatedDate;
                    detailViewModel.InvoiceAmount = Convert.ToDouble(detailDTOItem.InvoiceAmount);
                    detailViewModel.CreatedDate = detailDTOItem.CreatedDate;
                    detailViewModel.InvoiceNumber = detailDTOItem.InvoiceNumber;
                    detailViewModel.PaidAmount = Convert.ToDouble(detailDTOItem.PaidAmount);
                    //detailViewModel.DueDate = detailDTOItem.PaymentDue;
                    detailViewModel.ReferenceNumber = detailDTOItem.ReferenceNumber;
                    detailViewModel.Remarks = detailDTOItem.Remarks;
                    detailViewModel.ReturnAmount = Convert.ToDouble(detailDTOItem.ReturnAmount);

                    detailViewModel.ReceivableID = detailDTOItem.ReceivableID.HasValue && detailDTOItem.ReceivableID != 0 ? detailDTOItem.ReceivableID : receivableID;
                    detailViewModel.SubLedgerID = detailDTOItem.SubLedgerID;

                    if (detailDTOItem.CostCenter != null)
                        detailViewModel.CostCenter = new KeyValueViewModel() { Key = detailDTOItem.CostCenter.Key, Value = detailDTOItem.CostCenter.Value };

                    if (detailDTOItem.Account != null)
                        detailViewModel.Account = new KeyValueViewModel() { Key = detailDTOItem.Account.Key, Value = detailDTOItem.Account.Value };

                    if (detailDTOItem.AccountGroup != null)
                        detailViewModel.AccountGroup = new KeyValueViewModel() { Key = detailDTOItem.AccountGroup.Key, Value = detailDTOItem.AccountGroup.Value };

                    if (detailDTOItem.SubLedger != null)
                        detailViewModel.AccountSubLedgers = new KeyValueViewModel() { Key = detailDTOItem.SubLedger.Key, Value = detailDTOItem.SubLedger.Value };


                    if (detailDTOItem.Budget != null)
                        detailViewModel.Budget = new KeyValueViewModel() { Key = detailDTOItem.Budget.Key, Value = detailDTOItem.Budget.Value };

                    if (detailViewModel.Amount > 0)
                    {
                        detailViewModel.Debit = detailViewModel.Amount;
                        detailViewModel.DebitTotal = (detailViewModel.Amount < 0 ? detailViewModel.Amount * -1 : detailViewModel.Amount);

                    }
                    else
                    {
                        detailViewModel.Credit = -1 * detailViewModel.Amount;
                        detailViewModel.CreditTotal = (detailViewModel.Amount < 0 ? detailViewModel.Amount * -1 : detailViewModel.Amount);
                    }

                    detailViewModelList.Add(detailViewModel);
                }
            }
            return Json(detailViewModelList);
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            
            AccountTransactionHeadDTO dto = ClientFactory.AccountingTransactionServiceClient(CallContext).GetAccountTransactionHeadById(ID);
            RegularCreditViewModel regularCreditViewModel = new RegularCreditViewModel().ToRegularCreditViewModel(dto);
            if (!string.IsNullOrEmpty(dto.Remarks))
            { 
            var remarksData = ParseRemarks(dto.Remarks);
     
                regularCreditViewModel.MasterViewModel.Remarks = remarksData.Remarks;
                regularCreditViewModel.MasterViewModel.ReferenceHeadID = remarksData.ReferenceHeadID;
                regularCreditViewModel.MasterViewModel.ReceivableID = remarksData.ReceivableID;
                regularCreditViewModel.MasterViewModel.PendingAmount = remarksData.Amount;
                regularCreditViewModel.MasterViewModel.InvoiceID = new KeyValueViewModel() { Key = $"{remarksData.ReferenceHeadID}#{remarksData.ReceivableID}", Value = $"{remarksData.InvoiceNumber} ({Math.Round((remarksData.Amount??0), 2)})" };
            }
            return Json(regularCreditViewModel);
        }

        private class RemarksData
        {
            public string Remarks { get; set; }
            public long? ReferenceHeadID { get; set; }
            public long? ReceivableID { get; set; }
            public string InvoiceNumber { get; set; }
            public double? Amount { get; set; }
          
        }

        private static RemarksData ParseRemarks(string remarks)
        {
            var pattern = @"^(?:(.*?)#)?(\d+)#(\d+)#([A-Za-z0-9\-]+) \(([\d.]+)\)";
            var match = Regex.Match(remarks, pattern);

            if (match.Success)
            {
                return new RemarksData
                {
                    Remarks= match.Groups[1].Success ? match.Groups[1].Value : "",
                    ReferenceHeadID =long.Parse( match.Groups[2].Value),
                    ReceivableID = long.Parse(match.Groups[3].Value),
                    InvoiceNumber = match.Groups[4].Value,
                    Amount = double.Parse(match.Groups[5].Value)
                };
            }

            return null;
        }


    }
}

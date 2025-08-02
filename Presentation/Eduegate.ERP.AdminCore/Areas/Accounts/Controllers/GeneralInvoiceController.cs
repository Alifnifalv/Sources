using Eduegate.ERP.Admin.Areas.Accounts.Controllers;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.ERP.Admin.Helpers;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Accounts.Accounting;
using Eduegate.Services.Contracts;
using Eduegate.Web.Library.ViewModels.AccountTransactions;
using Eduegate.Web.Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Eduegate.Services.Client.Factory;
using Eduegate.Framework.Helper;
using Eduegate.Web.Library.Accounts.AccountTransactions;
using Eduegate.TransactionEgine.Accounting.ViewModels;
using Eduegate.TransactionEgine.Accounting;
using KeyValueViewModel = Eduegate.Web.Library.ViewModels.KeyValueViewModel;
using System.Linq;
using ServiceStack;
using ClientFactory = Eduegate.Services.Client.Factory.ClientFactory;
namespace Eduegate.ERP.AdminCore.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    public class GeneralInvoiceController : BaseSearchController
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
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.GeneralInvoice);
            return View(new SearchListViewModel
            {
                ControllerName = "Accounts/" + Eduegate.Infrastructure.Enums.SearchView.GeneralInvoice.ToString(),
                ViewName = Eduegate.Infrastructure.Enums.SearchView.GeneralInvoice,
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
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList(Services.Contracts.Enums.SearchView.GeneralInvoice);

            return PartialView("_ChildList", new SearchListViewModel
            {
                ViewName = Eduegate.Infrastructure.Enums.SearchView.GeneralInvoice,
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
                ParentViewName = Eduegate.Infrastructure.Enums.SearchView.GeneralInvoice,
                IsEditableLink = false
            });
        }


        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {

            var view = Eduegate.Infrastructure.Enums.SearchView.GeneralInvoice;
            if (runtimeFilter.Trim().IsNotNullOrEmpty())
                view = Eduegate.Infrastructure.Enums.SearchView.GeneralInvoice;
            else
                runtimeFilter = runtimeFilter + " DocumentTypeID=" + 1520;
            return base.SearchData(view, currentPage, orderBy, runtimeFilter);

        }


        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.GeneralInvoiceSummary);
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
            var vm = new GeneralInvoiceViewModel()
            {
                DetailViewModel = new List<GeneralInvoiceDetailViewModel>() { new GeneralInvoiceDetailViewModel() { } },
                MasterViewModel = new GeneralInvoiceMasterViewModel()
                {
                    TransactionDate = DateTime.Now.ToString(dateTimeFormat),
                    DocumentStatus = new KeyValueViewModel() { Key = "90", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentStatuses.New) },
                    TransactionStatus = new KeyValueViewModel() { Key = "1", Value = Convert.ToString(Eduegate.Framework.Enums.TransactionStatus.New) },
                    Currency = KeyValueViewModel.ToViewModel(ClientFactory.ReferenceDataServiceClient(CallContext).GetDefaultCurrencyWithName()),
                    DocumentType = new KeyValueViewModel() { Key = "51", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.AccountSalesInvoice) },
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
            var generalInvoiceViewModel = JsonConvert.DeserializeObject<GeneralInvoiceViewModel>(jsonData.SelectToken("data").ToString());

            try
            {
                if (generalInvoiceViewModel != null)
                {
                    if (generalInvoiceViewModel.DetailViewModel != null)
                    {
                        generalInvoiceViewModel.DetailViewModel = generalInvoiceViewModel.DetailViewModel
                            .Where(x => x.Account != null && !string.IsNullOrEmpty(x.Account.Key)).ToList();

                        if (generalInvoiceViewModel.DetailViewModel.Any(x => x.Account == null/* && x.VendorCustomerAccounts == null*/))
                        {
                            return Json(new { IsError = true, UserMessage = "Please select  account" });
                        }
                        if (generalInvoiceViewModel.DetailViewModel.Any(x => (x.Credit == 0 || x.Credit == null) && (x.Debit == 0 || x.Debit == null)))
                        {
                            return Json(new { IsError = true, UserMessage = "Please enter Debit or Credit amount" });
                        }
                        if (generalInvoiceViewModel.DetailViewModel.Any(x => x.Credit > 0 && x.Debit > 0))
                        {
                            return Json(new { IsError = true, UserMessage = "Both credit and Debit cannot be allowed for an item" });
                        }
                        if (generalInvoiceViewModel.DetailViewModel.Sum(x => x.Credit) != generalInvoiceViewModel.DetailViewModel.Sum(x => x.Debit))
                        {
                            return Json(new { IsError = true, UserMessage = "Credit Amount and Debit amount should be equal" });
                        }

                    }
                    AccountTransactionHeadDTO HeadDTO = new GeneralInvoiceViewModel().ToAccountTransactionHeadDTO(generalInvoiceViewModel);

                    AccountTransactionHeadDTO dto = ClientFactory.AccountingTransactionServiceClient(CallContext).SaveAccountTransactionHead(HeadDTO);
                    generalInvoiceViewModel = new GeneralInvoiceViewModel().ToGeneralInvoiceViewModel(dto);
                    bool IsDBOperationSuccess = false;
                    if (generalInvoiceViewModel.MasterViewModel.IsError && !string.IsNullOrEmpty(generalInvoiceViewModel.MasterViewModel.ErrorCode))
                    {
                        return Json(new { IsError = true, UserMessage = PortalWebHelper.GetErrorMessage(generalInvoiceViewModel.MasterViewModel.ErrorCode), data = generalInvoiceViewModel });
                    }
                    if (generalInvoiceViewModel.MasterViewModel.IsTransactionCompleted)
                    {
                        return Json(new { IsError = true, UserMessage = "Cannot update the trasaction, it's already processed.", data = generalInvoiceViewModel });
                    }
                    var vm = AccountTransactionHeadViewModel.ToVM(dto);

                    string customerGroupIDs = new Domain.Setting.SettingBL().GetSettingValue<string>("CUSTOMER_GROUP_IDS");
                    List<string> customerGroupIDList = customerGroupIDs.Split(',')
                                                  //.Select(long.Parse)
                                                  .ToList();
                    
                    var detail = generalInvoiceViewModel.DetailViewModel
                                .FirstOrDefault(x => x.AccountGroup!=null && x.AccountGroup.Key!=null && customerGroupIDList.Contains(x.AccountGroup.Key));
                    var amount = detail?.Amount;
                    IsDBOperationSuccess = new AccountingBase().AddReceivable(vm, dto.AccountID, amount, AccountingBase.CONST_DEBIT, true, false);
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<GeneralInvoiceController>.Fatal(exception.Message.ToString(), exception);
                return Json(new { IsError = !false, UserMessage = exception.Message.ToString() });
            }

            return Json(generalInvoiceViewModel);
        }


        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            AccountTransactionHeadDTO dto = ClientFactory.AccountingTransactionServiceClient(CallContext).GetAccountTransactionHeadById(ID);
            GeneralInvoiceViewModel generalInvoiceViewModel = new GeneralInvoiceViewModel().ToGeneralInvoiceViewModel(dto);
            return Json(generalInvoiceViewModel);
        }
    }
}

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
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Accounts.Accounting;
using Eduegate.ERP.Admin.Helpers;

namespace Eduegate.ERP.Admin.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    public class PVRegularPaymentController : BaseSearchController
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
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.PVRegularPayment);
            return View(new SearchListViewModel
            {
                ControllerName = "Accounts/" + Eduegate.Infrastructure.Enums.SearchView.PVRegularPayment.ToString(),
                ViewName = Eduegate.Infrastructure.Enums.SearchView.PVRegularPayment,
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
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList(Services.Contracts.Enums.SearchView.PVRegularPayment);

            return PartialView("_ChildList", new SearchListViewModel
            {
                ViewName = Eduegate.Infrastructure.Enums.SearchView.PVRegularPayment,
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
                ParentViewName = Eduegate.Infrastructure.Enums.SearchView.PVRegularPayment,
                IsEditableLink = false
            });
        }


        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {

            var view = Eduegate.Infrastructure.Enums.SearchView.PVRegularPayment;
            if (runtimeFilter.Trim().IsNotNullOrEmpty())
                view = Eduegate.Infrastructure.Enums.SearchView.PVRegularPayment;
            else
                runtimeFilter = runtimeFilter + " DocumentTypeID=" + 1518;
            return base.SearchData(view, currentPage, orderBy, runtimeFilter);

        }


        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.PVRegularPaymentSummary);
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
            var vm = new PVRegularPaymentViewModel()
            {
                DetailViewModel = new List<PVRegularPaymentDetailViewModel>() { new PVRegularPaymentDetailViewModel() { } },
                MasterViewModel = new PVRegularPaymentMasterViewModel()
                {
                    TransactionDate = DateTime.Now.ToString(dateTimeFormat),
                    DocumentStatus = new KeyValueViewModel() { Key = "114", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentStatuses.New) },
                    TransactionStatus = new KeyValueViewModel() { Key = "1", Value = Convert.ToString(Eduegate.Framework.Enums.TransactionStatus.New) },
                    Currency = KeyValueViewModel.ToViewModel(ClientFactory.ReferenceDataServiceClient(CallContext).GetDefaultCurrencyWithName()),
                    DocumentType = new KeyValueViewModel() { Key = "1518", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.Payments) },

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
            var pvRegularReceiptViewModel = JsonConvert.DeserializeObject<PVRegularPaymentViewModel>(jsonData.SelectToken("data").ToString());

            try
            {
                if (pvRegularReceiptViewModel != null)
                {
                    if (pvRegularReceiptViewModel.DetailViewModel != null)
                    {
                        pvRegularReceiptViewModel.DetailViewModel = pvRegularReceiptViewModel.DetailViewModel
                                .Where(x => x.Account != null && !string.IsNullOrEmpty(x.Account.Key)).ToList();

                        //if (pvRegularReceiptViewModel.DetailViewModel.Any(x => x.AccountTypes == null))
                        //{
                        //    return Json(new { IsError = true, UserMessage = "Please select  AccountTypes" });
                        //}

                        //if (pvRegularReceiptViewModel.DetailViewModel.Any(x => x.Account == null  && x.VendorCustomerAccounts == null))
                        //{
                        //    return Json(new { IsError = true, UserMessage = "Please select  account" });
                        //}.

                        if (pvRegularReceiptViewModel.DetailViewModel.Any(x => x.Credit == 0 && x.Debit == 0))
                        {
                            return Json(new { IsError = true, UserMessage = "Please enter Debit or Credit amount" });
                        }

                        if (pvRegularReceiptViewModel.DetailViewModel.Any(x => x.Credit > 0 && x.Debit > 0))
                        {
                            return Json(new { IsError = true, UserMessage = "Both credit and Debit cannot be allowed for an item" });
                        }
                        if (pvRegularReceiptViewModel.DetailViewModel.Sum(x => x.Credit) != pvRegularReceiptViewModel.DetailViewModel.Sum(x => x.Debit))
                        {
                            return Json(new { IsError = true, UserMessage = "Credit Amount and Debit amount should be equal" });
                        }

                    }



                    AccountTransactionHeadDTO HeadDTO = new PVRegularPaymentViewModel().ToAccountTransactionHeadDTO(pvRegularReceiptViewModel);

                    AccountTransactionHeadDTO dto = ClientFactory.AccountingTransactionServiceClient(CallContext).SaveAccountTransactionHead(HeadDTO);
                    pvRegularReceiptViewModel = new PVRegularPaymentViewModel().ToPVRegularPaymentViewModel(dto);
                    if (pvRegularReceiptViewModel.MasterViewModel.IsError && !string.IsNullOrEmpty(pvRegularReceiptViewModel.MasterViewModel.ErrorCode))
                    {
                        return Json(new { IsError = true, UserMessage = PortalWebHelper.GetErrorMessage(pvRegularReceiptViewModel.MasterViewModel.ErrorCode), data = pvRegularReceiptViewModel });
                    }

                    if (pvRegularReceiptViewModel.MasterViewModel.IsTransactionCompleted)
                    {
                        return Json(new { IsError = true, UserMessage = "Cannot update the trasaction, it's already processed.", data = pvRegularReceiptViewModel });
                    }

                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<PVRegularPaymentController>.Fatal(exception.Message.ToString(), exception);
                return Json(new { IsError = !false, UserMessage = exception.Message.ToString() });
            }
            return Json(pvRegularReceiptViewModel);
        }


        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            AccountTransactionHeadDTO dto = ClientFactory.AccountingTransactionServiceClient(CallContext).GetAccountTransactionHeadById(ID);
            PVRegularPaymentViewModel pvRegularReceiptViewModel = new PVRegularPaymentViewModel().ToPVRegularPaymentViewModel(dto);
            return Json(pvRegularReceiptViewModel);
        }


        public JsonResult GetAccountTypes()
        {
            var vM = new List<KeyValueViewModel>();
            vM.Add(new KeyValueViewModel() { Key = "1", Value = "Chart of accounts" });
            vM.Add(new KeyValueViewModel() { Key = "2", Value = "Vendor customer accounts" });

            return Json(vM);
        }


        [HttpGet]
        public JsonResult GetVendorCustomerAccounts(string searchText)
        {
            List<KeyValueViewModel> vendorCustomerList = new List<KeyValueViewModel>();
            try
            {
                var DTOList = ClientFactory.AccountingTransactionServiceClient(CallContext).GetVendorCustomerAccounts(searchText);

                foreach (var dto in DTOList)
                {
                    vendorCustomerList.Add(new KeyValueViewModel { Key = dto.Key, Value = dto.Value });
                }

            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<PVRegularPaymentController>.Fatal(exception.Message.ToString(), exception);
            }
            return Json(vendorCustomerList);
        }
        [HttpGet]
        public JsonResult GetVendorCustomerAccountsSearch(string lookupName, string searchText)
        {
            List<KeyValueViewModel> vendorCustomerList = new List<KeyValueViewModel>();
            try
            {
                var DTOList = ClientFactory.AccountingTransactionServiceClient(CallContext).GetVendorCustomerAccounts(searchText);

                foreach (var dto in DTOList)
                {
                    vendorCustomerList.Add(new KeyValueViewModel { Key = dto.Key, Value = dto.Value });
                }

            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<PVRegularPaymentController>.Fatal(exception.Message.ToString(), exception);
            }
            return Json(new { LookUpName = lookupName, Data = vendorCustomerList });
        }


    }
}

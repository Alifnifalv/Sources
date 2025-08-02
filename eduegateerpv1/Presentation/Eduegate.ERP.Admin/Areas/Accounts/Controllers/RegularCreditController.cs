using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Framework.Helper;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Web.Library.ViewModels.AccountTransactions;
using Eduegate.Services.Client.Factory;

namespace Eduegate.ERP.Admin.Areas.Accounts.Controllers
{
    public class RegularCreditController : BaseSearchController
    {

        private string dateTimeFormat = ConfigurationExtensions.GetAppConfigValue("DateTimeFormat");

        private static string ServiceHost { get { return ConfigurationExtensions.GetAppConfigValue("ServiceHost"); } }

        private string ReferenceServiceUrl = string.Concat(ServiceHost, Constants.REFERENCE_DATA_SERVICE);

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.RegularCredit);
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
        public ActionResult ChildList(long ID)
        {
            // call the service and get the meta data
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList(Services.Contracts.Enums.SearchView.RegularCredit);

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
        public JsonResult SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {

            var view = Eduegate.Infrastructure.Enums.SearchView.RegularCredit;
            if (runtimeFilter.Trim().IsNotNullOrEmpty())
                view = Eduegate.Infrastructure.Enums.SearchView.RegularCredit;
            else
                runtimeFilter = runtimeFilter + " DocumentTypeID=" + 1519;
            return base.SearchData(view, currentPage, orderBy, runtimeFilter);

        }


        [HttpGet]
        public JsonResult SearchSummaryData()
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

            return Json(vm, JsonRequestBehavior.AllowGet);
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
                return Json(NextTransactionNumber, JsonRequestBehavior.AllowGet);
            }
            return Json("", JsonRequestBehavior.AllowGet);

        }
        #endregion


        //[HttpGet]
        //public ActionResult GetReceivablesByAccountId(long AccountID)
        //{
        //    var ReceivablesDTO = new AccountingTransactionServiceClient(CallContext).GetReceivablesByAccountId(AccountID, Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.DistributionJobs);
        //    return Json(ReceivablesDTO, JsonRequestBehavior.AllowGet);
        //}


        [HttpPost]
        public JsonResult Save(RegularCreditViewModel regularCreditViewModel)
        {
            try
            {
                if (regularCreditViewModel != null)
                {
                    if (regularCreditViewModel.DetailViewModel != null)
                    {
                        if (regularCreditViewModel.DetailViewModel.Any(x => x.Account == null /*&& x.VendorCustomerAccounts == null*/))
                        {
                            return Json(new { IsError = true, UserMessage = "Please select  account" }, JsonRequestBehavior.AllowGet);
                        }
                        if (regularCreditViewModel.DetailViewModel.Any(x => (x.Credit == 0 || x.Credit == null) && (x.Debit == 0 || x.Debit == null)))
                        {
                            return Json(new { IsError = true, UserMessage = "Please enter Debit or Credit amount" }, JsonRequestBehavior.AllowGet);
                        }
                        if (regularCreditViewModel.DetailViewModel.Any(x => x.Credit > 0 && x.Debit > 0))
                        {
                            return Json(new { IsError = true, UserMessage = "Both credit and Debit cannot be allowed for an item" }, JsonRequestBehavior.AllowGet);
                        }
                        if (regularCreditViewModel.DetailViewModel.Sum(x => x.Credit) != regularCreditViewModel.DetailViewModel.Sum(x => x.Debit))
                        {
                            return Json(new { IsError = true, UserMessage = "Credit Amount and Debit amount should be equal" }, JsonRequestBehavior.AllowGet);
                        }

                    }
                        AccountTransactionHeadDTO HeadDTO = new RegularCreditViewModel().ToAccountTransactionHeadDTO(regularCreditViewModel);

                    AccountTransactionHeadDTO dto = ClientFactory.AccountingTransactionServiceClient(CallContext).SaveAccountTransactionHead(HeadDTO);
                    regularCreditViewModel = new RegularCreditViewModel().ToRegularCreditViewModel(dto);
                    if (regularCreditViewModel.MasterViewModel.IsTransactionCompleted)
                    {
                        return Json(new { IsError = true, UserMessage = "Cannot update the trasaction, it's already processed.", data = regularCreditViewModel }, JsonRequestBehavior.AllowGet);
                    }

                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<RegularCreditController>.Fatal(exception.Message.ToString(), exception);
                return Json(new { IsError = !false, UserMessage = exception.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
            return Json(regularCreditViewModel, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            AccountTransactionHeadDTO dto = ClientFactory.AccountingTransactionServiceClient(CallContext).GetAccountTransactionHeadById(ID);
            RegularCreditViewModel regularCreditViewModel = new RegularCreditViewModel().ToRegularCreditViewModel(dto);
            return Json(regularCreditViewModel, JsonRequestBehavior.AllowGet);
        }

    }
}

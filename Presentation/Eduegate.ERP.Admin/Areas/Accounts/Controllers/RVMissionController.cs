using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Framework.Helper;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.ViewModels.AccountTransactions;
using System.Globalization;

namespace Eduegate.ERP.Admin.Areas.Accounts.Controllers
{
    public class RVMissionController : BaseSearchController
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
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.RVMission);
            return View(new SearchListViewModel
            {
                ControllerName = "Accounts/" + Eduegate.Infrastructure.Enums.SearchView.RVMission.ToString(),
                ViewName = Eduegate.Infrastructure.Enums.SearchView.RVMission,
                HeaderList = metadata.Columns,
                //SummaryHeaderList = metadata.SummaryColumns,
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
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList(Services.Contracts.Enums.SearchView.RVMission);

            return PartialView("_ChildList", new SearchListViewModel
            {
                ViewName = Eduegate.Infrastructure.Enums.SearchView.RVMission,
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
                ParentViewName = Eduegate.Infrastructure.Enums.SearchView.RVMission,
                IsEditableLink = false
            });
        }


        [HttpGet]
        public JsonResult SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {

            var view = Eduegate.Infrastructure.Enums.SearchView.RVMission;
            if (runtimeFilter.Trim().IsNotNullOrEmpty())
                view = Eduegate.Infrastructure.Enums.SearchView.RVMission;
            else
                runtimeFilter = runtimeFilter + " DocumentTypeID=" + 1510;
            return base.SearchData(view, currentPage, orderBy, runtimeFilter);

        }


        [HttpGet]
        public JsonResult SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.RVMissionSummary);
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
            var vm = new RVMissionViewModel()
            {
                DetailViewModel = new List<RVMissionDetailViewModel>() { new RVMissionDetailViewModel() {

                    PaymentDueDate=DateTime.Now.ToString(dateTimeFormat)
                } },
                MasterViewModel = new RVMissionMasterViewModel()
                {
                    TransactionDate = DateTime.Now.ToString(dateTimeFormat),
                    DocumentStatus = new KeyValueViewModel() { Key = "74", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentStatuses.New) },
                    TransactionStatus = new KeyValueViewModel() { Key = "1", Value = Convert.ToString(Eduegate.Framework.Enums.TransactionStatus.New) },
                    Currency = KeyValueViewModel.ToViewModel(ClientFactory.ReferenceDataServiceClient(CallContext).GetDefaultCurrencyWithName()),
                    DocumentType = new KeyValueViewModel() { Key = "1510", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.Receipts) },
                    DocumentReferenceTypeID = (int)Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.Receipts
                }
            };

            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Get_AllDrivers_Accounts(string lookupName, string searchText)
        {
            List<KeyValueViewModel> driverAccount = new List<KeyValueViewModel>();
            try
            {
                var DTOList = ClientFactory.AccountingTransactionServiceClient(CallContext).Get_AllDrivers_Accounts(searchText);

                foreach (var dto in DTOList)
                {
                    driverAccount.Add(new KeyValueViewModel { Key = dto.Key, Value = dto.Value });
                }

            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<RVMissionController>.Fatal(exception.Message.ToString(), exception);
            }
            return Json(new { LookUpName = lookupName, Data = driverAccount }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Get_AllSuppliers_Accounts(string lookupName, string searchText)
        {
            List<KeyValueViewModel> supplierAccount = new List<KeyValueViewModel>();
            try
            {
                var DTOList = ClientFactory.AccountingTransactionServiceClient(CallContext).Get_AllSuppliers_Accounts(searchText);

                foreach (var dto in DTOList)
                {
                    supplierAccount.Add(new KeyValueViewModel { Key = dto.Key, Value = dto.Value });
                }

            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<RVMissionController>.Fatal(exception.Message.ToString(), exception);
            }
            return Json(new { LookUpName = lookupName, Data = supplierAccount }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult Get_AllCustomers_Accounts(string lookupName, string searchText)
        {
            List<KeyValueViewModel> customerAccount = new List<KeyValueViewModel>();
            try
            {
                var DTOList = ClientFactory.AccountingTransactionServiceClient(CallContext).Get_AllCustomers_Accounts(searchText);

                foreach (var dto in DTOList)
                {
                    customerAccount.Add(new KeyValueViewModel { Key = dto.Key, Value = dto.Value });
                }

            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<RVMissionController>.Fatal(exception.Message.ToString(), exception);
            }
            return Json(new { LookUpName = lookupName, Data = customerAccount }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetChildAccounts_ByParentAccountId(string lookupName, string searchText)
        {
            List<KeyValueViewModel> debitAccount = new List<KeyValueViewModel>();
            try
            {
                long ParentAccountId = GetGLAccountIDFromSettings("GLACC_CASH_CASHDIVIDENTS");
                var DTOList = ClientFactory. AccountingTransactionServiceClient(CallContext).GetChildAccounts_ByParentAccountId(searchText, ParentAccountId);

                foreach (var dto in DTOList)
                {
                    debitAccount.Add(new KeyValueViewModel { Key = dto.Key, Value = dto.Value });
                }

            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<RVMissionController>.Fatal(exception.Message.ToString(), exception);
            }
            return Json(new { LookUpName = lookupName, Data = debitAccount }, JsonRequestBehavior.AllowGet);
        }

        public long GetGLAccountIDFromSettings(string SettingCode)
        {
            long GLAccountId = 0;
            var SettingDTO = ClientFactory.SettingServiceClient(null).GetSettingDetail(SettingCode);
            if (SettingDTO != null)
            {
                GLAccountId = Convert.ToInt64(SettingDTO.SettingValue);
            }
            return GLAccountId;
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
            var dateFormat = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("DateFormat");
            if (vm !=null && vm.MasterViewModel != null && vm.MasterViewModel.DocumentType !=null 
                                                && vm.MasterViewModel.PaymentModes !=null 
                                                && vm.MasterViewModel.TransactionDate !=null)
            {
                var dto = new TransactionNumberDTO();
                dto.DocumentTypeID = Convert.ToInt32( vm.MasterViewModel.DocumentType.Key);
                //dto.Month = (Convert.ToDateTime(vm.MasterViewModel.TransactionDate)).Month;
                //dto.Year = (Convert.ToDateTime(vm.MasterViewModel.TransactionDate)).Year;
                dto.Month = DateTime.ParseExact(vm.MasterViewModel.TransactionDate, dateFormat, CultureInfo.InvariantCulture).Month;
                dto.Year = DateTime.ParseExact(vm.MasterViewModel.TransactionDate, dateFormat, CultureInfo.InvariantCulture).Year;
                dto.PaymentMode = vm.MasterViewModel.PaymentModes.Value;          

                string NextTransactionNumber = ClientFactory.MutualServiceClient(CallContext).GetNextTransactionNumberByMonthYear(dto);
                return Json(NextTransactionNumber, JsonRequestBehavior.AllowGet);
            }

            return Json("", JsonRequestBehavior.AllowGet);

        }
        #endregion


        [HttpGet]
        public ActionResult GetReceivablesByAccountId(long AccountID, Eduegate.Services.Contracts.Enums.DocumentReferenceTypes DocumentType)
        {
            var ReceivablesDTO = ClientFactory.AccountingTransactionServiceClient(CallContext).GetReceivablesByAccountId(AccountID,DocumentType);
            return Json(ReceivablesDTO, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Save(RVMissionViewModel rvMissionViewModel)
        {
            try
            {
                if (rvMissionViewModel != null)
                {
                    if (rvMissionViewModel.DetailViewModel != null)
                    {
                        //if (rvMissionViewModel.MasterViewModel.Amount != rvMissionViewModel.DetailViewModel.Sum(x => x.Amount))
                        //{
                        //    return Json(new { IsError = true, UserMessage = "Amount should be equal" }, JsonRequestBehavior.AllowGet);
                        //}
                    }
                        AccountTransactionHeadDTO HeadDTO = new RVMissionViewModel().ToAccountTransactionHeadDTO(rvMissionViewModel);

                    AccountTransactionHeadDTO dto = ClientFactory.AccountingTransactionServiceClient(CallContext).SaveAccountTransactionHead(HeadDTO);
                    rvMissionViewModel = new RVMissionViewModel().ToRVMissionViewModel(dto);

                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<RVMissionController>.Fatal(exception.Message.ToString(), exception);
                return Json(new { IsError = !false, UserMessage = exception.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
            return Json(rvMissionViewModel, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            AccountTransactionHeadDTO dto = ClientFactory.AccountingTransactionServiceClient(CallContext).GetAccountTransactionHeadById(ID);
            RVMissionViewModel rvMissionViewModel = new RVMissionViewModel().ToRVMissionViewModel(dto);
            return Json(rvMissionViewModel, JsonRequestBehavior.AllowGet);
        }

    }
}

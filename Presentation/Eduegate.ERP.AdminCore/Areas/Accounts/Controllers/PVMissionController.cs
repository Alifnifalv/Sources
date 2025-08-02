using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Framework.Helper;
using Eduegate.Web.Library.ViewModels.Common;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.ViewModels.AccountTransactions;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Domain;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Accounts.Accounting;

namespace Eduegate.ERP.Admin.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    public class PVMissionController : BaseSearchController
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
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.PVMission);
            return View(new SearchListViewModel
            {
                ControllerName = "Accounts/" + Eduegate.Infrastructure.Enums.SearchView.PVMission.ToString(),
                ViewName = Eduegate.Infrastructure.Enums.SearchView.PVMission,
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
        public async Task<IActionResult> ChildList(long ID)
        {
            // call the service and get the meta data
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList(Services.Contracts.Enums.SearchView.PVMissionPayment);

            return PartialView("_ChildList", new SearchListViewModel
            {
                ViewName = Eduegate.Infrastructure.Enums.SearchView.PVMission,
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
                ParentViewName = Eduegate.Infrastructure.Enums.SearchView.PVMission,
                IsEditableLink = false
            });
        }


        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {

            var view = Eduegate.Infrastructure.Enums.SearchView.PVMission;
            if (runtimeFilter.Trim().IsNotNullOrEmpty())
                view = Eduegate.Infrastructure.Enums.SearchView.PVMission;
            else
                runtimeFilter = runtimeFilter + " DocumentTypeID=" + 1516;
            return base.SearchData(view, currentPage, orderBy, runtimeFilter);

        }


        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.PVMissionSummary);
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
            var viewModel = new CRUDMasterDetailViewModel();
            viewModel.Name = "PVMission";
            viewModel.ListActionName = "PVMission";
            viewModel.ListButtonDisplayName = "Lists";
            viewModel.DisplayName = "PVMission Entry";
            viewModel.PrintPreviewReportName = "PVMission";
            viewModel.JsControllerName = "AccountVoucherController";
            viewModel.Model = new PVMissionViewModel()
            {
                DetailViewModel = new List<PVMissionDetailViewModel>() { new PVMissionDetailViewModel() {

                    PaymentDueDate=DateTime.Now.ToString(dateTimeFormat)
                } },
                MasterViewModel = new PVMissionMasterViewModel()
                {
                    TransactionDate = DateTime.Now.ToString(dateTimeFormat),
                    DocumentStatus = new KeyValueViewModel() { Key = "98", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentStatuses.New) },
                    TransactionStatus = new KeyValueViewModel() { Key = "1", Value = Convert.ToString(Eduegate.Framework.Enums.TransactionStatus.New) },
                    Currency = KeyValueViewModel.ToViewModel(ClientFactory.ReferenceDataServiceClient(CallContext).GetDefaultCurrencyWithName()),
                    DocumentType = new KeyValueViewModel() { Key = "1516", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.Payments) },
                    DocumentReferenceTypeID = (int)Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.Payments
                }
            };

            var PVMissionTransactionHeadViewModel = new PVMissionMasterViewModel();
            PVMissionTransactionHeadViewModel.IsSummaryPanel = false;
            viewModel.MasterViewModel = PVMissionTransactionHeadViewModel;
            viewModel.DetailViewModel = new PVMissionDetailViewModel();
            viewModel.IID = ID;

            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "DocumentType", Url = "Mutual/GetDocumentTypesByID?referenceType=PaymentVoucherMission" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "PaymentModes", Url = "Mutual/GetEntitlements?type="+ EntityTypes.Customer });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "TransactionStatus", Url = "Mutual/GetLookUpData?lookType=TransactionStatus" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "DocumentStatus", Url = "Mutual/GetLookUpData?lookType=AssetEntryDocumentStatuses" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Accounts", Url = "Mutual/GetLookUpData?lookType=Account" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Currency", Url = "Mutual/GetLookUpData?lookType=Currency" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "CostCenter", Url = "Mutual/GetLookUpData?lookType=CostCenter" });

            TempData["viewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(viewModel);
            return RedirectToAction("CreateMasterDetail", "CRUD", new { area = "" });

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
            if(vm !=null && vm.MasterViewModel != null && vm.MasterViewModel.DocumentType !=null 
                                                && vm.MasterViewModel.PaymentModes !=null 
                                                && vm.MasterViewModel.TransactionDate !=null)
            {
                var dto = new TransactionNumberDTO();
                dto.DocumentTypeID = Convert.ToInt32( vm.MasterViewModel.DocumentType.Key);
                dto.Month = (Convert.ToDateTime(vm.MasterViewModel.TransactionDate)).Month;
                dto.Year = (Convert.ToDateTime(vm.MasterViewModel.TransactionDate)).Year;
                string NextTransactionNumber = ClientFactory.MutualServiceClient(CallContext).GetNextTransactionNumberByMonthYear(dto);
                return Json(NextTransactionNumber);
            }
            return Json("");

        }
        #endregion


        [HttpGet]
        public ActionResult GetReceivablesByAccountId(long AccountID, Eduegate.Services.Contracts.Enums.DocumentReferenceTypes DocumentType)
        {
            var ReceivablesDTO = ClientFactory.AccountingTransactionServiceClient(CallContext).GetReceivablesByAccountId(AccountID,DocumentType);
            return Json(ReceivablesDTO);
        }


        [HttpPost]
        public JsonResult Save(string model)
        {
            var jsonData = JObject.Parse(model);
            var pvMissionViewModel = JsonConvert.DeserializeObject<PVMissionViewModel>(jsonData.SelectToken("data").ToString());

            try
            {
                if (pvMissionViewModel != null)
                {
                    pvMissionViewModel.DetailViewModel = pvMissionViewModel.DetailViewModel.Where(x => !string.IsNullOrEmpty(x.InvoiceNumber)).ToList();

                    if (pvMissionViewModel.DetailViewModel != null)
                    {
                        if (pvMissionViewModel.MasterViewModel.Amount != pvMissionViewModel.DetailViewModel.Sum(x => x.Amount))
                        {
                            return Json(new { IsError = true, UserMessage = "Amount should be equal" });
                        }
                    }

                    var HeadDTO = new PVMissionViewModel().ToAccountTransactionHeadDTO(pvMissionViewModel);
                    var dto = ClientFactory.AccountingTransactionServiceClient(CallContext).SaveAccountTransactionHead(HeadDTO);
                    pvMissionViewModel = new PVMissionViewModel().ToPVMissionViewModel(dto);

                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<PVMissionController>.Fatal(exception.Message.ToString(), exception);
                return Json(new { IsError = !false, UserMessage = exception.Message.ToString() });
            }

            return Json(pvMissionViewModel);
        }


        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            AccountTransactionHeadDTO dto = ClientFactory.AccountingTransactionServiceClient(CallContext).GetAccountTransactionHeadById(ID);
            PVMissionViewModel pvMissionViewModel = new PVMissionViewModel().ToPVMissionViewModel(dto);
            return Json(pvMissionViewModel);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Framework.Helper;
using System.Data;
using Eduegate.Web.Library.ViewModels.Accounts;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Client.Factory;

namespace Eduegate.ERP.Admin.Areas.Accounts.Controllers
{
    public class AssetRemovalController : BaseSearchController
    {
        private string dateTimeFormat = ConfigurationExtensions.GetAppConfigValue("DateTimeFormat");

        private static string ServiceHost { get { return ConfigurationExtensions.GetAppConfigValue("ServiceHost"); } }

        private string ReferenceServiceUrl = string.Concat(ServiceHost, Constants.REFERENCE_DATA_SERVICE);

        // GET: Inventories/InventoryDetails
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.AssetRemoval);
            return View(new SearchListViewModel
            {
                //ControllerName = Eduegate.Eduegate.Infrastructure.Enums.SearchView.AssetEntry.ToString(),
                ControllerName = "Accounts/" + Eduegate.Infrastructure.Enums.SearchView.AssetRemoval.ToString(),
                ViewName = Eduegate.Infrastructure.Enums.SearchView.AssetRemoval,
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
        public ActionResult ChildList(long ID)
        {
            // call the service and get the meta data
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList(Services.Contracts.Enums.SearchView.AssetRemovalBranch);

            return PartialView("_ChildList", new SearchListViewModel
            {
                ViewName = Eduegate.Infrastructure.Enums.SearchView.AssetRemovalBranch,
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
                ParentViewName = Eduegate.Infrastructure.Enums.SearchView.AssetRemoval,
                IsEditableLink = false
            });
        }


        [HttpGet]
        public JsonResult SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {

            var view = Eduegate.Infrastructure.Enums.SearchView.AssetRemoval;
            if (runtimeFilter.Trim().IsNotNullOrEmpty())
                view = Eduegate.Infrastructure.Enums.SearchView.AssetRemovalBranch;
            else
                runtimeFilter = runtimeFilter + " DocumentTypeID=" + 1402;
            return base.SearchData(view, currentPage, orderBy, runtimeFilter);

        }

        [HttpGet]
        public JsonResult SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.AssetRemovalSummary);
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
           var vm = new AssetDisposalViewModel()
            {
                DetailViewModel = new List<AssetDisposalDetailViewModel>() { new AssetDisposalDetailViewModel() { } },
                MasterViewModel = new AssetDisposalMasterViewModel()
                {
                    EntryDate = DateTime.Now.ToString(dateTimeFormat),
                    DocumentStatus = new KeyValueViewModel() { Key = "38", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentStatuses.New) },
                    DocumentType = new KeyValueViewModel() { Key = "1402", Value = Convert.ToString(Eduegate.Framework.Enums.DocumentReferenceTypes.AssetRemoval) },
                    TransactionStatus = new KeyValueViewModel() { Key = "1", Value = Convert.ToString(Eduegate.Framework.Enums.TransactionStatus.New) }
                }
            };

            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpGet]
        public ActionResult GetAccountByID(long ID = 0)
        {
            var account = ClientFactory.AccountingServiceClient(CallContext).GetAccount(ID);
            return Json(account, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetAssetByID(long ID = 0)
        {
            AssetDTO dto = ClientFactory.FixedAssetServiceClient(CallContext).GetAssetById(ID);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Save(AssetDisposalViewModel assetDisposalViewModel)
        {
            string successMessage = "Assets is created/updated successfully";
            try
            {
                if (assetDisposalViewModel != null)
                {
                    if (assetDisposalViewModel.DetailViewModel != null)
                    {
                        if (assetDisposalViewModel.DetailViewModel.Any(x => x.AssetGlAccount == null))
                        {
                            return Json(new { IsError = true, UserMessage = "Please select Asset or account" }, JsonRequestBehavior.AllowGet);
                        }
                        if (assetDisposalViewModel.DetailViewModel.Any(x => x.AssetCode != null && x.Quantity == 0))
                        {
                            return Json(new { IsError = true, UserMessage = "Quantity need to be entered for Asset" }, JsonRequestBehavior.AllowGet);
                        }
                        if (assetDisposalViewModel.DetailViewModel.Any(x => (x.Credit == 0 || x.Credit == null) && (x.Debit == 0 || x.Debit == null)))
                        {
                            return Json(new { IsError = true, UserMessage = "Please enter Debit or Credit amount" }, JsonRequestBehavior.AllowGet);
                        }
                        if (assetDisposalViewModel.DetailViewModel.Any(x => x.AssetCode != null && x.StartDate == null))
                        {
                            return Json(new { IsError = true, UserMessage = "Please select Asset Start date" }, JsonRequestBehavior.AllowGet);
                        }
                        if (assetDisposalViewModel.DetailViewModel.Sum(x => x.Credit * (x.Quantity == 0 ? 1 : x.Quantity)) != assetDisposalViewModel.DetailViewModel.Sum(x => x.Debit * (x.Quantity == 0 ? 1 : x.Quantity)))
                        {
                            return Json(new { IsError = true, UserMessage = "Credit Amount and Debit amount should be equal. Consider Asset Value * Quatity" }, JsonRequestBehavior.AllowGet);
                        }

                        if (assetDisposalViewModel.DetailViewModel.Any(x => x.Credit > 0 && x.Debit > 0))
                        {
                            return Json(new { IsError = true, UserMessage = "Both credit and Debit cannot be allowed for an item" }, JsonRequestBehavior.AllowGet);
                        }
                        
                        if (assetDisposalViewModel.DetailViewModel.Any(x => x.AssetCode != null && x.AssetGlAccount != null && x.AssetGlAccID != Convert.ToInt64(x.AssetGlAccount.Key)))
                        {
                            return Json(new { IsError = true, UserMessage = "Asset Code's GL Account cannot be changed" }, JsonRequestBehavior.AllowGet);
                        }
                        if (assetDisposalViewModel.DetailViewModel.Where(x => x.AssetCode != null).Count() > 1)
                        {
                            return Json(new { IsError = true, UserMessage = " Maximum number of asset code should be one" }, JsonRequestBehavior.AllowGet);
                        }
                        
                    }
                        AssetTransactionHeadDTO HeadDTO = new AssetDisposalViewModel().ToAssetTransactionHeadDTO(assetDisposalViewModel);
                        AssetTransactionHeadDTO dto = ClientFactory.FixedAssetServiceClient().SaveAssetTransaction(HeadDTO);
                        assetDisposalViewModel = new AssetDisposalViewModel().ToAssetTransactionViewModel(dto);
                    
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<AssetRemovalController>.Fatal(exception.Message.ToString(), exception);
                return Json(new { IsError = !false, UserMessage = exception.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
            return Json(assetDisposalViewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            AssetTransactionHeadDTO dto = ClientFactory.FixedAssetServiceClient(CallContext).GetAssetTransactionHeadById(ID);
            AssetDisposalViewModel assetDisposalViewModel = new AssetDisposalViewModel().ToAssetTransactionViewModel(dto);
            return Json(assetDisposalViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}

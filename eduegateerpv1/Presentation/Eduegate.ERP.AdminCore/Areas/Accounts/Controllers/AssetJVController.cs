using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Framework.Helper;
using Eduegate.Web.Library.ViewModels.Accounts;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Client.Factory;
using Eduegate.Domain;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Eduegate.ERP.Admin.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    public class AssetJVController : BaseSearchController
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
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.AssetJV);
            return View(new SearchListViewModel
            {
                ControllerName = "Accounts/" + Eduegate.Infrastructure.Enums.SearchView.AssetJV.ToString(),
                ViewName = Eduegate.Infrastructure.Enums.SearchView.AssetJV,
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
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList(Services.Contracts.Enums.SearchView.AssetJVBranch);

            return PartialView("_ChildList", new SearchListViewModel
            {
                ViewName = Eduegate.Infrastructure.Enums.SearchView.AssetJVBranch,
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
                ParentViewName = Eduegate.Infrastructure.Enums.SearchView.AssetJV,
                IsEditableLink = false
            });
        }


        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {

            var view = Eduegate.Infrastructure.Enums.SearchView.AssetJV;
            if (runtimeFilter.Trim().IsNotNullOrEmpty())
                view = Eduegate.Infrastructure.Enums.SearchView.AssetJVBranch;
            else
                runtimeFilter = runtimeFilter + " DocumentTypeID=" + 1401;

            return base.SearchData(view, currentPage, orderBy, runtimeFilter);

        }


        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.AssetJVSummary);
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
            var vm = new AssetDepreciationViewModel()
            {
                DetailViewModel = new List<AssetDepreciationDetailViewModel>() { new AssetDepreciationDetailViewModel()
                {
                    AssetStartDate=DateTime.Now.ToString(dateTimeFormat)
                }
           },
                MasterViewModel = new AssetDepreciationMasterViewModel()
                {
                    EntryDate = DateTime.Now.ToString(dateTimeFormat),
                    DocumentStatus = new KeyValueViewModel() { Key = "37", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentStatuses.New) },
                    DocumentType = new KeyValueViewModel() { Key = "1401", Value = Convert.ToString(Eduegate.Framework.Enums.DocumentReferenceTypes.AssetDepreciation) },
                    TransactionStatus = new KeyValueViewModel() { Key = "1", Value = Convert.ToString(Eduegate.Framework.Enums.TransactionStatus.New) }


                }
            };

            return Json(vm);
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
            return Json(account);
        }

        [HttpGet]
        public ActionResult GetAssetByID(long ID = 0)
        {
            AssetDTO dto = ClientFactory.FixedAssetServiceClient(CallContext).GetAssetById(ID);
            return Json(dto);
        }

        [HttpPost]
        public JsonResult Save(string model)
        {
            var jsonData = JObject.Parse(model);
            var assetDepreciationViewModel = JsonConvert.DeserializeObject<AssetDepreciationViewModel>(jsonData.SelectToken("data").ToString());
            string successMessage = "Assets is created/updated successfully";
            
            try
            {
                if (assetDepreciationViewModel != null)
                {
                    if (assetDepreciationViewModel.DetailViewModel != null)
                    {
                        assetDepreciationViewModel.DetailViewModel = assetDepreciationViewModel.DetailViewModel.Where(x => x.AssetCode != null).ToList();

                        if (assetDepreciationViewModel.DetailViewModel.Any(x => x.AssetCode == null))
                        {
                            return Json(new { IsError = true, UserMessage = "Please select AssetCode" });
                        }
                        if (assetDepreciationViewModel.DetailViewModel.Any(x => x.AssetCode != null && x.StartDate == null))
                        {
                            return Json(new { IsError = true, UserMessage = "Please select Depreciation Start date" });
                        }
                        if (assetDepreciationViewModel.DetailViewModel.Any(x => x.AssetCode != null && x.Quantity == 0))
                        {
                            return Json(new { IsError = true, UserMessage = "Quantity need to be entered for Asset" });
                        }
                    }
                    AssetTransactionHeadDTO HeadDTO = new AssetDepreciationViewModel().ToAssetTransactionHeadDTO(assetDepreciationViewModel);
                    AssetTransactionHeadDTO dto = ClientFactory.FixedAssetServiceClient(CallContext).SaveAssetTransaction(HeadDTO);
                    assetDepreciationViewModel = new AssetDepreciationViewModel().ToAssetTransactionViewModel(dto);
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<AssetJVController>.Fatal(exception.Message.ToString(), exception);
                return Json(new { IsError = !false, UserMessage = exception.Message.ToString() });
            }
            return Json(assetDepreciationViewModel);
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            AssetTransactionHeadDTO dto = ClientFactory.FixedAssetServiceClient(CallContext).GetAssetTransactionHeadById(ID);
            AssetDepreciationViewModel assetDepreciationViewModel = new AssetDepreciationViewModel().ToAssetTransactionViewModel(dto);
            return Json(assetDepreciationViewModel);
        }

    }
}

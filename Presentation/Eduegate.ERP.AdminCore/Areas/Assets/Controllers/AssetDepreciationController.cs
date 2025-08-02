using Eduegate.ERP.Admin.Controllers;
using Eduegate.ERP.Admin.Helpers;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Web.Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Web.Library.Accounts.Assets;
using System.Globalization;

namespace Eduegate.ERP.Admin.Areas.Assets.Controllers
{
    [Area("Assets")]
    public class AssetDepreciationController : BaseSearchController
    {
        private string dateTimeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateTimeFormat");
        private string dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Infrastructure.Enums.SearchView.AssetDepreciation);
            return View(new SearchListViewModel
            {
                ControllerName = "Assets/" + Infrastructure.Enums.SearchView.AssetDepreciation.ToString(),
                ViewName = Infrastructure.Enums.SearchView.AssetDepreciation,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                IsCommentLink = true,
                IsChild = false,
                HasChild = true,
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                UserValues = metadata.UserValues,
            });
        }
        [HttpGet]
        public async Task<IActionResult> ChildList(long ID)
        {
            // call the service and get the meta data
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList(Services.Contracts.Enums.SearchView.PurchaseSKU);

            return PartialView("_ChildList", new SearchListViewModel
            {
                ControllerName = "ProductSKU",
                ViewName = Infrastructure.Enums.SearchView.PurchaseSKU,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                FilterColumns = metadata.QuickFilterColumns,
                RuntimeFilter = Convert.ToString("HeadIID=" + ID.ToString()).Trim(), // child view must have this column
                IsChild = true,
                ParentViewName = Infrastructure.Enums.SearchView.AssetDepreciation,
                IsEditableLink = false
            });
        }

        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {

            var view = Infrastructure.Enums.SearchView.AssetDepreciation;
            if (runtimeFilter.Trim().IsNotNullOrEmpty())
            {
                view = Infrastructure.Enums.SearchView.PurchaseSKU;
                runtimeFilter = runtimeFilter + " AND CompanyID = " + CallContext.CompanyID.ToString();
            }
            else
            {
                runtimeFilter = " CompanyID = " + CallContext.CompanyID.ToString();
            }

            return base.SearchData(view, currentPage, orderBy, runtimeFilter);

        }

        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Infrastructure.Enums.SearchView.AssetDepreciationSummary);
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
                DetailViewModel = new List<AssetDepreciationDetailViewModel>() { new AssetDepreciationDetailViewModel() { } },
                MasterViewModel = new AssetDepreciationMasterViewModel()
                {
                    TransactionDateString = DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture),
                    DocumentReferenceTypeID = DocumentReferenceTypes.AssetDepreciation,
                    DocumentStatus = new KeyValueViewModel() { Key = "3", Value = Convert.ToString(DocumentStatuses.Approved) }
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
        public ActionResult Get(long ID = 0)
        {
            var vm = AssetDepreciationViewModel.ToVM(ClientFactory.FixedAssetServiceClient(CallContext).GetAssetTransaction(ID, false, false));

            if (vm != null)
                vm.DetailViewModel.Add(new AssetDepreciationDetailViewModel());

            return Json(vm);
        }

        [HttpPost]
        public JsonResult Save([FromBody] AssetDepreciationViewModel model)
        {
            try
            {
                var vm = model;

                var updatedVM = new AssetDepreciationViewModel();

                if (vm != null)
                {
                    vm.MasterViewModel.DocumentReferenceTypeID = Services.Contracts.Enums.DocumentReferenceTypes.AssetDepreciation;

                    //long? assetCategoryID = vm.MasterViewModel.AssetCategories != null ? !string.IsNullOrEmpty(vm.MasterViewModel.AssetCategory.Key) ? long.Parse(vm.MasterViewModel.AssetCategory.Key) : null : null;
                    //if (!assetCategoryID.HasValue && assetCategoryID == 0)
                    //{
                    //    throw new Exception("Please select atleast one category!");
                    //}

                    var result = ClientFactory.FixedAssetServiceClient(CallContext).SaveAssetTransactions(AssetDepreciationViewModel.ToDTO(vm));
                    updatedVM = AssetDepreciationViewModel.ToVM(result);
                    updatedVM.DetailViewModel.Add(new AssetDepreciationDetailViewModel());
                }

                if (updatedVM.MasterViewModel.IsError && !string.IsNullOrEmpty(updatedVM.MasterViewModel.ErrorCode))
                {
                    return Json(new { IsError = true, UserMessage = PortalWebHelper.GetErrorMessage(updatedVM.MasterViewModel.ErrorCode), data = vm });
                }

                // once TransactionStatus completed we should not allow any operation on any transaction..
                if (updatedVM.MasterViewModel.IsTransactionCompleted)
                {
                    return Json(new { IsError = true, UserMessage = "Cannot update the trasaction, it's already processed.", data = vm });
                }

                if (updatedVM.MasterViewModel.IsError == true)
                {
                    return Json(new { IsError = true, UserMessage = PortalWebHelper.GetErrorMessage(updatedVM.MasterViewModel.ErrorCode), data = vm });
                }

                return Json(updatedVM);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<AssetDepreciationController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message });
            }

        }

    }
}
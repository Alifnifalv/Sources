using Eduegate.ERP.Admin.Controllers;
using Eduegate.ERP.Admin.Helpers;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Eduegate.Web.Library.Accounts.Assets;

namespace Eduegate.ERP.Admin.Areas.Assets.Controllers
{
    [Area("Assets")]
    public class AssetTransferRequestController : BaseSearchController
    {
        private string dateTimeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateTimeFormat");

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Infrastructure.Enums.SearchView.AssetTransferRequest);
            return View(new SearchListViewModel
            {
                ControllerName = "Assets/" + Infrastructure.Enums.SearchView.AssetTransferRequest.ToString(),
                ViewName = Infrastructure.Enums.SearchView.AssetTransferRequest,
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
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public async Task<IActionResult> ChildList(long ID)
        {
            // call the service and get the meta data
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList(Services.Contracts.Enums.SearchView.PurchaseSKU);

            return PartialView("_ChildList", new SearchListViewModel
            {
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
                ParentViewName = Infrastructure.Enums.SearchView.AssetTransferRequest,
                IsEditableLink = false
            });
        }

        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            var view = Infrastructure.Enums.SearchView.AssetTransferRequest;
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
            return base.SearchSummaryData(Infrastructure.Enums.SearchView.AssetTransferRequestSummary);
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
            viewModel.Name = "AssetTransferRequest";
            viewModel.ListActionName = "AssetTransferRequest";
            viewModel.ListButtonDisplayName = "Lists";
            viewModel.DisplayName = "Asset Transfer Request";
            viewModel.PrintPreviewReportName = "AssetTransferRequest";
            var vm = new AssetTransferRequestViewModel()
            {
                DetailViewModel = new List<AssetTransferRequestDetailViewModel>() { new AssetTransferRequestDetailViewModel() { } },
                MasterViewModel = new AssetTransferRequestMasterViewModel()
                {
                    TransactionDateString = DateTime.Now.ToString(dateTimeFormat),
                    DocumentStatus = new KeyValueViewModel() { Key = "3", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentStatuses.Approved) }
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
            var vm = AssetTransferRequestViewModel.ToVM(ClientFactory.FixedAssetServiceClient(CallContext).GetAssetTransaction(ID, false, false));

            if (vm != null)
                vm.DetailViewModel.Add(new AssetTransferRequestDetailViewModel());

            return Json(vm);
        }

        [HttpPost]
        public JsonResult Save(string model)
        {
            try
            {
                var jsonData = JObject.Parse(model);
                var vm = JsonConvert.DeserializeObject<AssetTransferRequestViewModel>(jsonData.SelectToken("data").ToString());

                var updatedVM = new AssetTransferRequestViewModel();

                if (vm != null)
                {
                    var result = ClientFactory.FixedAssetServiceClient(CallContext).SaveAssetTransactions(AssetTransferRequestViewModel.ToDTO(vm));
                    updatedVM = AssetTransferRequestViewModel.ToVM(result);

                    if (updatedVM != null)
                        updatedVM.DetailViewModel.Add(new AssetTransferRequestDetailViewModel());
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
                Eduegate.Logger.LogHelper<AssetTransferRequestController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message });
            }
        }

    }
}
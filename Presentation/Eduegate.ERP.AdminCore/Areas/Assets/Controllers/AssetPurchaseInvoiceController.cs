using Eduegate.ERP.Admin.Controllers;
using Eduegate.ERP.Admin.Helpers;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Eduegate.Web.Library.Accounts.Assets;

namespace Eduegate.ERP.Admin.Areas.Assets.Controllers
{
    [Area("Assets")]
    public class AssetPurchaseInvoiceController : BaseSearchController
    {
        private string dateTimeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateTimeFormat");

        // GET: Assets/AssetPurchaseInvoice
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Infrastructure.Enums.SearchView.AssetPurchaseInvoice);

            return View(new SearchListViewModel
            {
                ControllerName = "Assets/" + Infrastructure.Enums.SearchView.AssetPurchaseInvoice,
                ViewName = Infrastructure.Enums.SearchView.AssetPurchaseInvoice,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                IsCommentLink = true,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                InfoBar = @"<li class='status-label-mobile'>
                                                        <div class='right status-label'>
                                                            <div class='status-label-color'><label class='status-color-label  green'></label>Complete</div>
                                                            <div class='status-label-color'><label class='status-color-label red'></label>Fail</div>
                                                            <div class='status-label-color'><label class='status-color-label orange'></label>Others</div>
                                                        </div>
                                                    </li>",
                IsChild = false,
                HasChild = true,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public async Task<IActionResult> ChildList(long ID)
        {
            // call the service and get the meta data
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList(Services.Contracts.Enums.SearchView.AssetPurchaseInvoice);

            return PartialView("_ChildList", new SearchListViewModel
            {
                ControllerName = "AssetEntry",
                ViewName = Infrastructure.Enums.SearchView.AssetPurchaseInvoice,
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
                ParentViewName = Infrastructure.Enums.SearchView.AssetPurchaseInvoice,
                IsEditableLink = false
            });
        }

        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            var view = Infrastructure.Enums.SearchView.AssetPurchaseInvoice;
            if (runtimeFilter.Trim().IsNotNullOrEmpty())
            {
                view = Infrastructure.Enums.SearchView.AssetPurchaseInvoice;
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
            return base.SearchSummaryData(Infrastructure.Enums.SearchView.AssetPurchaseInvoiceSummary);
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View();
        }

        public ActionResult Create(long ID = 0)
        {
            var vm = new AssetPurchaseInvoiceViewModel()
            {
                DetailViewModel = new List<AssetPurchaseInvoiceDetailViewModel>() { new AssetPurchaseInvoiceDetailViewModel() { } },
                MasterViewModel = new AssetPurchaseInvoiceMasterViewModel()
                {
                    TransactionDateString = DateTime.Now.ToString(dateTimeFormat),
                    DocumentReferenceTypeID = Services.Contracts.Enums.DocumentReferenceTypes.AssetEntryPurchase,
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
            var vm = AssetPurchaseInvoiceViewModel.ToVM(ClientFactory.FixedAssetServiceClient(CallContext).GetAssetTransaction(ID, true, false));
            if (vm != null)
            {
                vm.DetailViewModel.Add(new AssetPurchaseInvoiceDetailViewModel());
            }
            return Json(vm);
        }

        [HttpPost]
        public JsonResult Save(string model)
        {
            try
            {
                var jsonData = JObject.Parse(model);
                var vm = JsonConvert.DeserializeObject<AssetPurchaseInvoiceViewModel>(jsonData.SelectToken("data").ToString());

                var updatedVM = new AssetPurchaseInvoiceViewModel();
                var userMessage = string.Empty;

                if (vm != null)
                {
                    vm.MasterViewModel.DocumentReferenceTypeID = Services.Contracts.Enums.DocumentReferenceTypes.AssetEntryPurchase;
                    var result = ClientFactory.FixedAssetServiceClient(CallContext).SaveAssetTransactions(AssetPurchaseInvoiceViewModel.ToDTO(vm));

                    updatedVM = AssetPurchaseInvoiceViewModel.ToVM(result);
                }

                if (updatedVM != null)
                {
                    updatedVM.DetailViewModel.Add(new AssetPurchaseInvoiceDetailViewModel());
                }

                if (updatedVM.DetailViewModel.IsNotNull() && updatedVM.DetailViewModel.Count > 0 && updatedVM.IsError == true)
                {
                    for (int lineCounter = 0; lineCounter < updatedVM.DetailViewModel.Count; lineCounter++)
                    {
                        if (updatedVM.DetailViewModel[lineCounter].IsError)
                        {
                            // set error on detail
                            vm.DetailViewModel[lineCounter].IsError = true;

                            // Loopthrough serials for this line to set error
                            for (int serialCounter = 0; serialCounter < updatedVM.DetailViewModel[lineCounter].TransactionSerialMaps.Count; serialCounter++)
                            {
                                if (updatedVM.DetailViewModel[lineCounter].TransactionSerialMaps[serialCounter].IsError)
                                {
                                    // Set error and errormsg for serial number
                                    vm.DetailViewModel[lineCounter].TransactionSerialMaps[serialCounter].IsError = true;
                                    vm.DetailViewModel[lineCounter].TransactionSerialMaps[serialCounter].ErrorMessage = PortalWebHelper.GetErrorMessage(updatedVM.DetailViewModel[lineCounter].TransactionSerialMaps[serialCounter].ErrorMessage);
                                }
                            }
                        }
                    }
                    userMessage = PortalWebHelper.GetErrorMessage(updatedVM.ErrorCode);
                    return Json(new { IsError = true, UserMessage = userMessage, data = vm });
                }

                // once TransactionStatus completed we should not allow any operation on any transaction..
                if (updatedVM.MasterViewModel.IsTransactionCompleted)
                {
                    return Json(new { IsError = true, UserMessage = "Cannot update the trasaction, it's already processed.", data = vm });
                }

                return Json(updatedVM);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<AssetPurchaseInvoiceController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message });
            }
        }

    }
}
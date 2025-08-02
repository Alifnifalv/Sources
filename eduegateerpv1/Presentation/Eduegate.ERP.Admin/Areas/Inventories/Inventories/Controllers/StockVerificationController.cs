using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Common;
using Eduegate.Web.Library.ViewModels.Inventory;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Areas.Inventories.Controllers
{
    public class StockVerificationController : BaseSearchController
    {
        private string dateTimeFormat = ConfigurationExtensions.GetAppConfigValue("DateTimeFormat");

        // GET: Inventories/StockVerification
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Infrastructure.Enums.SearchView.StockVerification);

            return View(new SearchListViewModel
            {
                ControllerName = "Inventories/" + Infrastructure.Enums.SearchView.StockVerification,
                ViewName = Infrastructure.Enums.SearchView.StockVerification,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                //                InfoBar = @"<li class='status-label-mobile'>
                //                                        <div class='right status-label'>
                //                                            <div class='status-label-color'><label class='status-color-label  green'></label>Active</div>
                //                                            <div class='status-label-color'><label class='status-color-label red'></label>In Active</div>
                //                                        </div>
                //                                    </li>"
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public JsonResult SearchData(int currentPage = 1, string orderBy = "")
        {
            return base.SearchData(Infrastructure.Enums.SearchView.StockVerification, currentPage, orderBy);
        }

        [HttpGet]
        public JsonResult SearchSummaryData()
        {
            return base.SearchSummaryData(Infrastructure.Enums.SearchView.StockVerificationSummary);
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View();
        }

        public ActionResult Create(long ID = 0)
        {
            var viewModel = new CRUDMasterDetailViewModel();
            viewModel.Name = "StockVerification";
            viewModel.ListActionName = "StockVerification";
            viewModel.DisplayName = "Stock Verification";
            viewModel.Model = new StockVerificationViewModel()
            {
                DetailViewModel = new List<StockVerificationDetailViewModel>() { new StockVerificationDetailViewModel() { } },
                MasterViewModel = new StockVerificationMasterViewModel() { VerificationDate = DateTime.Now.ToString(dateTimeFormat), }
            };

            viewModel.MasterViewModel = new StockVerificationMasterViewModel();
            viewModel.DetailViewModel = new StockVerificationDetailViewModel();
            viewModel.IID = ID;

            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Branch", Url = "Mutual/GetLookUpData?lookType=Branch" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Employee", Url = "Mutual/GetLookUpData?lookType=Employee" });
            TempData["viewModel"] = viewModel;
            return RedirectToAction("CreateMasterDetail", "CRUD", new { area = "" });
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpGet]
        public ActionResult Get(DateTime verficationDate)
        {
            var vm = StockVerificationViewModel.FromDTO(ClientFactory.InventoryServiceClient(CallContext).GetStockVerification(verficationDate));
            if (vm != null)
            {
                vm.DetailViewModel.Add(new StockVerificationDetailViewModel());
            }
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(StockVerificationViewModel vm)
        {
            try
            {
                var updatedVM = new StockVerificationViewModel();

                if (vm != null)
                {
                    var result = ClientFactory.InventoryServiceClient(CallContext).SaveStockVerification(StockVerificationViewModel.ToDTO(vm));
                    //var result = ServiceHelper.HttpPostRequest(ServiceHost + Eduegate.Framework.Helper.Constants.PRODUCT_DETAIL_SERVICE_NAME + "SaveTransactions", TransactionViewModel.ToSalesOrderDTO(vm), this.CallContext);
                    updatedVM = StockVerificationViewModel.FromDTO(result);
                }
                if (updatedVM != null)
                {
                    updatedVM.DetailViewModel.Add(new StockVerificationDetailViewModel());
                }
                return Json(updatedVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<SalesOrderController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
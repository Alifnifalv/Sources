using Eduegate.ERP.Admin.Controllers;
using Eduegate.ERP.Admin.Helpers;
using Eduegate.Framework.Extensions;
using Eduegate.Infrastructure.Enums;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Common;
using Eduegate.Web.Library.ViewModels.Inventory;
using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Domain;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Eduegate.ERP.Admin.Areas.Inventories.Controllers
{
    [Area("Inventories")]
    public class StockVerificationController : BaseSearchController
    {
        private string dateTimeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateTimeFormat");

        // GET: Inventories/StockVerification
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Infrastructure.Enums.SearchView.StockVerification);

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
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "")
        {
            return base.SearchData(Infrastructure.Enums.SearchView.StockVerification, currentPage, orderBy);
        }

        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
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
                MasterViewModel = new StockVerificationMasterViewModel(),
            };

            viewModel.MasterViewModel = new StockVerificationMasterViewModel();
            viewModel.DetailViewModel = new StockVerificationDetailViewModel();
            viewModel.IID = ID;

            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Branch", Url = "Mutual/GetLookUpData?lookType=Branch" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Employee", Url = "Mutual/GetLookUpData?lookType=Employee" });
            TempData["viewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(viewModel);
            return RedirectToAction("CreateMasterDetail", "CRUD", new { area = "" });
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            try
            {
                var vm = StockVerificationViewModel.FromDTOtoVM(ClientFactory.InventoryServiceClient(CallContext).GetStockVerification(ID));

                return Json(vm);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<PurchaseInvoiceController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Save(string model)
        {
            try
            {
                var jsonData = JObject.Parse(model);
                var vm = JsonConvert.DeserializeObject<StockVerificationViewModel>(jsonData.SelectToken("data").ToString());
                var updatedVM = new StockVerificationViewModel();

                //if (vm != null && vm.MasterViewModel.HeadIID != 0)
                //{
                //    return Json(new { IsError = true, UserMessage = "Can't edit stock verification screen please use (stock updation) screen for any updations." });
                //}

                if(vm != null)
                {
                    var result = ClientFactory.InventoryServiceClient(CallContext).SaveStockVerification(StockVerificationViewModel.FromVmToDTO(vm));
                    if (result != null)
                    {
                        updatedVM = StockVerificationViewModel.FromDTOtoVM(result);
                    }
                }

                return Json(updatedVM);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<StockVerificationController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult FillPhysicalStockDataINStockUpdate(long ID = 0)
        {
            try
            {
                var vm = StockUpdationViewModel.FromStockVerificationVM(ClientFactory.InventoryServiceClient(CallContext).GetStockVerification(ID));
                return Json(vm);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<PurchaseInvoiceController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message });
            }

        }

        [HttpGet]
        public ActionResult FillProductItemsForPhyscicalStockVerification(long ID, string dateString)
        {
            try
            {
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
                var date = DateTime.ParseExact(dateString, dateFormat, CultureInfo.InvariantCulture);
                var vm = ClientFactory.InventoryServiceClient(CallContext).GetProductListsByBranchID(ID, date);
                return Json(vm);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<PurchaseInvoiceController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message });
            }

        }

    }
}
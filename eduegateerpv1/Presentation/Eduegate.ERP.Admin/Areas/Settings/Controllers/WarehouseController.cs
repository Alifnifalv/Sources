using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.Search;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Services.Client.Factory;
using Eduegate.ERP.Admin.Controllers;

namespace Eduegate.ERP.Admin.Areas.Settings.Controllers
{
    public class WarehouseController : BaseSearchController
    {
        // GET: Warehouse
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.Warehouse);

            return View(new SearchListViewModel
            {
                ControllerName = Eduegate.Infrastructure.Enums.SearchView.Warehouse.ToString(),
                ViewName = Eduegate.Infrastructure.Enums.SearchView.Warehouse,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                //InfoBar = "",
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public JsonResult SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            runtimeFilter = " CompanyId = " + CallContext.CompanyID.ToString();
            return base.SearchData(Eduegate.Infrastructure.Enums.SearchView.Warehouse, currentPage, orderBy, runtimeFilter);
        }

        [HttpGet]
        public JsonResult SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.WarehouseSummary);
        }

        [HttpGet]
        public ActionResult Create(long ID = 0)
        {
            var viewModel = new CRUDViewModel();
            viewModel.Name = "Warehouse";
            viewModel.ListActionName = "Warehouse";
            viewModel.ViewModel = new WarehouseViewModel();
            viewModel.IID = ID;
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "ProductFamilyType", Url = "Mutual/GetLookUpData?lookType=ProductFamilyType" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "WarehouseStatus", Url = "Mutual/GetLookUpData?lookType=WarehouseStatus" });
            TempData["viewModel"] = viewModel;
            return RedirectToAction("Create", "CRUD");
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View("DetailedView");
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            var vm = WarehouseViewModel.FromDTO(ClientFactory.ReferenceDataServiceClient(CallContext).GetWarehouse(ID));
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(WarehouseViewModel vm)
        {
            var updatedVM = WarehouseViewModel.FromDTO(ClientFactory.ReferenceDataServiceClient(CallContext).SaveWarehouse(WarehouseViewModel.ToDTO(vm)));
            return Json(updatedVM, JsonRequestBehavior.AllowGet);
        }
    }
}
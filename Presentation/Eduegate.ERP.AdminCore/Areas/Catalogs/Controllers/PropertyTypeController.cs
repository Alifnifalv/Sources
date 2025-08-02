using Eduegate.ERP.Admin.Controllers;
//using Eduegate.Framework.Web.Library.Common;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Eduegate.ERP.Admin.Areas.Catalogs.Controllers
{
    [Area("Catalogs")]
    public class PropertyTypeController : BaseSearchController
    {
        // GET: PropertyType
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            // call the service and get the meta data
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList(Eduegate.Services.Contracts.Enums.SearchView.PropertyType);
            return View(new SearchListViewModel
            {
                ControllerName = Infrastructure.Enums.SearchView.PropertyType.ToString(),
                ViewName = Infrastructure.Enums.SearchView.PropertyType,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                //InfoBar = ""
                FilterColumns = metadata.QuickFilterColumns,
                HasFilters = metadata.HasFilters,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "")
        {
            return base.SearchData(Infrastructure.Enums.SearchView.PropertyType, currentPage, orderBy);
        }

        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Infrastructure.Enums.SearchView.PropertyTypeSummary);
        }

        [HttpGet]
        public ActionResult Create(long ID = 0)
        {
            var viewModel = new CRUDViewModel();
            viewModel.Name = "PropertyType";
            viewModel.ListActionName = "PropertyType";
            var propertyVM = new PropertyTypeViewModel();
            propertyVM.Properties = new List<KeyValueViewModel>();
            viewModel.ViewModel = propertyVM;
            //viewModel.ViewModel = new PropertyViewModel();
            viewModel.IID = ID;
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Properties", Url = "Mutual/GetLazyLookUpData?lookType=" + LookUpTypes.Property.ToString() });
            TempData["viewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(viewModel);
            return RedirectToAction("Create", "CRUD");
        }

        [HttpGet]
        public ActionResult Edit(byte ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpGet]
        public ActionResult Get(byte ID = 0)
        {
            var vm = PropertyTypeViewModel.ToVM(ClientFactory.ProductDetailServiceClient(CallContext).GetPropertyType(ID));
            return Json(vm);
        }

        [HttpPost]
        public JsonResult Save([FromBody] PropertyTypeViewModel vm)
        {
            var updatedVM = PropertyTypeViewModel.ToVM(ClientFactory.ProductDetailServiceClient(CallContext).SavePropertyType(PropertyTypeViewModel.ToDTO(vm)));
            return Json(updatedVM);
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View("DetailedView");
        }
    }
}

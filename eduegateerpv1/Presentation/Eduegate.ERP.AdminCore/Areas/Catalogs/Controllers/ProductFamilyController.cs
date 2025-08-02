using Eduegate.ERP.Admin.Controllers;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Eduegate.ERP.Admin.Areas.Catalogs.Controllers
{
    [Area("Catalogs")]
    public class ProductFamilyController : BaseSearchController
    {
        // GET: ProductFamily
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            // call the service and get the meta data
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList(Eduegate.Services.Contracts.Enums.SearchView.ProductFamily); ;
            return View(new SearchListViewModel
            {
                ControllerName = Infrastructure.Enums.SearchView.ProductFamily.ToString(),
                ViewName = Infrastructure.Enums.SearchView.ProductFamily,
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
            return base.SearchData(Infrastructure.Enums.SearchView.ProductFamily, currentPage, orderBy);
        }

        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Infrastructure.Enums.SearchView.ProductFamilySummary);
        }

        [HttpGet]
        public ActionResult Create(long ID = 0)
        {
            var viewModel = new CRUDViewModel();
            viewModel.Name = "ProductFamily";
            viewModel.ListActionName = "ProductFamily";
            viewModel.ViewModel = new ProductFamilyViewModel();
            viewModel.IID = ID;
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "ProductFamilyType", Url = "Mutual/GetLookUpData?lookType=ProductFamilyType" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Property", Url = "Mutual/GetLookUpData?lookType=Property" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "PropertyType", Url = "Mutual/GetLookUpData?lookType=PropertyType" });
            TempData["viewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(viewModel);
            return RedirectToAction("Create", "CRUD");
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            var vm = ProductFamilyViewModel.FromDTO(ClientFactory.ProductDetailServiceClient(CallContext).GetProductFamily(ID.ToString()));
            return Json(vm);
        }

        [HttpPost]
        public JsonResult Save([FromBody] ProductFamilyViewModel vm)
        {
            var updatedVM = ProductFamilyViewModel.FromDTO(ClientFactory.ProductDetailServiceClient(CallContext).SaveProductFamily(ProductFamilyViewModel.ToDTO(vm)));
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
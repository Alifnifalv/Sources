using Microsoft.AspNetCore.Mvc;
using Eduegate.Web.Library;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Services.Client.Factory;
using Eduegate.ERP.Admin.Controllers;
using System.Threading.Tasks;

namespace Eduegate.ERP.Admin.Areas.Settings.Controllers
{
    [Area("Settings")]
    public class DepartmentController : BaseSearchController
    {
        // GET: Department
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            // call the service and get the meta data
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.Department);

            return View(new SearchListViewModel
            {
                ControllerName = Eduegate.Infrastructure.Enums.SearchView.Department.ToString(),
                ViewName = Eduegate.Infrastructure.Enums.SearchView.Department,
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
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            runtimeFilter = " CompanyId = " + CallContext.CompanyID.ToString();
            return base.SearchData(Eduegate.Infrastructure.Enums.SearchView.Department, currentPage, orderBy, runtimeFilter);
        }

        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.DepartmentSummary);
        }

        [HttpGet]
        public ActionResult Create(long ID = 0)
        {
            //var viewModel = new CRUDViewModel();
            //viewModel.Name = "Department";
            //viewModel.ListActionName = "Department";
            //viewModel.ViewModel = new DepartmentViewModel();
            //viewModel.IID = ID;
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "DepartmentStatus", Url = "Mutual/GetLookUpData?lookType=DepartmentStatus" });
            //TempData["viewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(viewModel);
            return Json(new DepartmentViewModel());
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
            var vm = DepartmentViewModel.FromDTO(ClientFactory.ReferenceDataServiceClient(CallContext).GetDepartment(ID));
            return Json(vm);
        }

        [HttpPost]
        public JsonResult Save([FromBody] DepartmentViewModel vm)
        {
            var updatedVM = DepartmentViewModel.FromDTO(ClientFactory.ReferenceDataServiceClient(CallContext).SaveDepartment(DepartmentViewModel.ToDTO(vm)));
            return Json(updatedVM);
        }
    }
}
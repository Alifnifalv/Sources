using Eduegate.ERP.Admin.Controllers;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Enums.Schedulers;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Settings;
using Microsoft.AspNetCore.Mvc;

namespace Eduegate.ERP.Admin.Areas.Inventories.Controllers
{
    [Area("Inventories")]
    public class StockDocumentTypeController : BaseSearchController
    {
        // GET: DocumentType
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Infrastructure.Enums.SearchView.StockDocumentType);

            return View(new SearchListViewModel
            {
                ControllerName = Infrastructure.Enums.SearchView.StockDocumentType.ToString(),
                ViewName = Infrastructure.Enums.SearchView.StockDocumentType,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                InfoBar = @"<li class='status-label-mobile'>
                                        <div class='right status-label'>
                                            <div class='status-label-color'><label class='status-color-label orange'></label>Draft</div>
                                            <div class='status-label-color'><label class='status-color-label  green'></label>Active</div>
                                            <div class='status-label-color'><label class='status-color-label red'></label>In Active</div>
                                        </div>
                                    </li>",
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "")
        {
            var runtimeFilter = " CompanyID = " + CallContext.CompanyID.ToString();
            return base.SearchData(Infrastructure.Enums.SearchView.StockDocumentType, currentPage, orderBy, runtimeFilter);
        }

        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Infrastructure.Enums.SearchView.StockDocumentTypeSummary);
        }

        [HttpGet]
        public ActionResult Create(long ID = 0)
        {
            return Json(new DocumentTypeViewModel());
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            var vm = DocumentTypeViewModel.ToVM(ClientFactory.MetadataServiceClient(CallContext).GetDocumentType(ID, SchedulerTypes.StockDocumentType.ToString()));
            return Json(vm);
        }

        [HttpPost]
        public JsonResult Save([FromBody] DocumentTypeViewModel vm)
        {
            var dto = DocumentTypeViewModel.ToDTO(vm, SchedulerTypes.StockDocumentType);
            dto.System = Systems.Stock.ToString();
            var updatedVM = DocumentTypeViewModel.ToVM(ClientFactory.MetadataServiceClient(CallContext).SaveDocumentType(dto));
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
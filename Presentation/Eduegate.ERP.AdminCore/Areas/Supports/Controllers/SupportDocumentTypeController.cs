using Eduegate.ERP.Admin.Controllers;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Settings;
using Microsoft.AspNetCore.Mvc;

namespace Eduegate.ERP.Admin.Areas.Supports.Controllers
{
    [Area("Supports")]
    public class SupportDocumentTypeController : BaseSearchController
    {
        // GET: Supports/SupportDocumentType
        public ActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> List()
        {
            // call the service and get the meta data
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList(Eduegate.Services.Contracts.Enums.SearchView.SupportDocumentType);

            return View(new SearchListViewModel
            {
                ViewName = Eduegate.Infrastructure.Enums.SearchView.SupportDocumentType,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                UserValues = metadata.UserValues,
                InfoBar = @"<li class='status-label-mobile'>
                                        <div class='right status-label'>
                                            <div class='status-label-color'><label class='status-color-label orange'></label>Draft</div>
                                            <div class='status-label-color'><label class='status-color-label  green'></label>Active</div>
                                            <div class='status-label-color'><label class='status-color-label red'></label>In Active</div>
                                        </div>
                                    </li>"
            });
        }

        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "")
        {
            return base.SearchData(Eduegate.Infrastructure.Enums.SearchView.SupportDocumentType, currentPage, orderBy);
        }

        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.SupportDocumentTypeSummary);
        }

        [HttpGet]
        public ActionResult Create(long ID = 0)
        {
            var viewModel = new CRUDViewModel();
            viewModel.Name = "SupportDocumentType";
            viewModel.ListActionName = "SupportDocumentType";
            viewModel.DisplayName = "Document Configuration";
            viewModel.ListButtonDisplayName = "Lists";
            var vm = new DocumentTypeViewModel();
            viewModel.ViewModel = vm;
            viewModel.IID = ID;
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "ReferenceType", Url = "Mutual/GetLookUpData?lookType=SupportDocumentTypes" });
            TempData["viewModel"] = viewModel;
            return RedirectToAction("Create", "CRUD", new { area = "" });
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            var vm = DocumentTypeViewModel.ToVM(ClientFactory.MetadataServiceClient(CallContext).GetDocumentType(ID));
            return Json(vm);
        }

        [HttpPost]
        public JsonResult Save(DocumentTypeViewModel vm)
        {
            var dto = DocumentTypeViewModel.ToDTO(vm);
            dto.System = Systems.Support.ToString();
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
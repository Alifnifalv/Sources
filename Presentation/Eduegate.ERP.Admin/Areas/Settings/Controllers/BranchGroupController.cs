using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.Search;
using Eduegate.Web.Library;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Services.Client.Factory;
using Eduegate.ERP.Admin.Controllers;

namespace Eduegate.ERP.Admin.Areas.Settings.Controllers
{
    public class BranchGroupController : BaseSearchController
    {
        // GET: BranchGroup
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.BranchGroup);

            return View(new SearchListViewModel
            {
                ControllerName = Eduegate.Infrastructure.Enums.SearchView.BranchGroup.ToString(),
                ViewName = Eduegate.Infrastructure.Enums.SearchView.BranchGroup,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                InfoBar = @"<li class='status-label-mobile'>
                                        <div class='right status-label'>
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
        public ActionResult Create(long ID = 0)
        {
            var viewModel = new CRUDViewModel();
            viewModel.Name = "BranchGroup";
            viewModel.ListActionName = "BranchGroup";
            viewModel.ViewModel = new BranchGroupViewModel();
            viewModel.IID = ID;
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "BranchGroupStatus", Url = "Mutual/GetLookUpData?lookType=BranchGroupStatus" });
            TempData["viewModel"] = viewModel;
            return RedirectToAction("Create", "CRUD");
        }

        [HttpGet]
        public JsonResult SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            runtimeFilter = " CompanyId = " + CallContext.CompanyID.ToString();
            return base.SearchData(Eduegate.Infrastructure.Enums.SearchView.BranchGroup, currentPage, orderBy, runtimeFilter);
        }

        [HttpGet]
        public JsonResult SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.BranchGroupSummary);
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
            var vm = BranchGroupViewModel.FromDTO(ClientFactory.ReferenceDataServiceClient(CallContext).GetBranchGroup(ID));
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(BranchGroupViewModel vm)
        {
            var updatedVM = BranchGroupViewModel.FromDTO(ClientFactory.ReferenceDataServiceClient(CallContext).SaveBranchGroup(BranchGroupViewModel.ToDTO(vm)));
            return Json(updatedVM, JsonRequestBehavior.AllowGet);
        }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.Search;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Services.Client.Factory;
using Eduegate.ERP.Admin.Controllers;

namespace Eduegate.ERP.Admin.Areas.Settings.Controllers
{
    public class BranchController : BaseSearchController
    {
        // GET: Branch
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            // call the service and get the meta data
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.Branch);
            return View(new SearchListViewModel
            {
                ControllerName = Eduegate.Infrastructure.Enums.SearchView.Branch.ToString(),
                ViewName = Eduegate.Infrastructure.Enums.SearchView.Branch,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = metadata.MultilineEnabled,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                //InfoBar = ""
                FilterColumns = metadata.QuickFilterColumns,
                HasFilters = metadata.HasFilters,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public JsonResult SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            runtimeFilter = " CompanyId = " + CallContext.CompanyID.ToString();
            return base.SearchData(Eduegate.Infrastructure.Enums.SearchView.Branch, currentPage, orderBy, runtimeFilter);
        }

        [HttpGet]
        public JsonResult SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.BranchSummary);
        }

        [HttpGet]
        public ActionResult Create(long ID = 0)
        {
            var viewModel = new CRUDViewModel();
            viewModel.Name = "Branch";
            viewModel.ListActionName = "Branch";
            viewModel.ViewModel = new BranchViewModel();
            viewModel.IID = ID;
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "BranchGroup", Url = "Mutual/GetLookUpData?lookType=BranchGroup" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Warehouse", Url = "Mutual/GetLookUpData?lookType=Warehouse" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "PriceList", Url = "Mutual/GetLookUpData?lookType=PriceList" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "BranchStatus", Url = "Mutual/GetLookUpData?lookType=BranchStatus" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "DocumentType", Url = "Mutual/GetDocumentTypesBySystem?system=Stock" });
            TempData["viewModel"] = viewModel;
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
            var vm = BranchViewModel.FromDTO(ClientFactory.ReferenceDataServiceClient(CallContext).GetBranch(ID),
                CultureDataInfoViewModel.FromDTO(ClientFactory.ProductDetailServiceClient(CallContext).GetCultureList()));
            vm.LogoUrl = string.Format("{0}//{1}//{2}",
               ConfigurationExtensions.GetAppConfigValue("ImageHostUrl").ToString(), EduegateImageTypes.Branch.ToString(), vm.Logo);
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(BranchViewModel vm)
        {
            var cultures = CultureDataInfoViewModel.FromDTO(ClientFactory.ProductDetailServiceClient(CallContext).GetCultureList());
            var updatedVM = BranchViewModel.FromDTO(ClientFactory.ReferenceDataServiceClient(CallContext).SaveBranch(
                BranchViewModel.ToDTO(vm, CallContext, cultures)), cultures);
            return Json(updatedVM, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View("DetailedView");
        }
    }
}
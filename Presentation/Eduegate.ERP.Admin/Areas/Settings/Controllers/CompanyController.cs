using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.Search;
using Eduegate.Web.Library;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Services.Client.Factory;
using Eduegate.ERP.Admin.Controllers;

namespace Eduegate.ERP.Admin.Areas.Settings.Controllers
{
    public class CompanyController : BaseSearchController
    {
        // GET: Company
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            // call the service and get the meta data
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.Company);

            return View(new SearchListViewModel
            {
                ControllerName = Eduegate.Infrastructure.Enums.SearchView.Company.ToString(),
                ViewName = Eduegate.Infrastructure.Enums.SearchView.Company,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                //InfoBar = ""
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public JsonResult SearchData(int currentPage = 1, string orderBy = "")
        {
            return base.SearchData(Eduegate.Infrastructure.Enums.SearchView.Company, currentPage, orderBy);
        }

        [HttpGet]
        public JsonResult SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.CompanySummary);
        }

        [HttpGet]
        public ActionResult Create(long ID = 0)
        {
            var viewModel = new CRUDViewModel();
            viewModel.Name = "Company";
            viewModel.ListActionName = "Company";
            viewModel.ViewModel = new CompanyViewModel();
            viewModel.IID = ID;
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Currency", Url = "Mutual/GetLookUpData?lookType=Currency" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Language", Url = "Mutual/GetLookUpData?lookType=Language" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Country", Url = "Mutual/GetLookUpData?lookType=Country" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "CompanyStatus", Url = "Mutual/GetLookUpData?lookType=CompanyStatus" });
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
            var vm = CompanyViewModel.FromDTO(ClientFactory.ReferenceDataServiceClient(CallContext).GetCompany(ID));
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(CompanyViewModel vm)
        {
            var updatedVM = CompanyViewModel.FromDTO(ClientFactory.ReferenceDataServiceClient(CallContext).SaveCompany(CompanyViewModel.ToDTO(vm)));
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
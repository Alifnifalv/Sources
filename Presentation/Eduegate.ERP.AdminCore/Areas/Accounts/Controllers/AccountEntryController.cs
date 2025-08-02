using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Accounts;
using Eduegate.Services.Client.Factory;

namespace Eduegate.ERP.Admin.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    public class AccountEntryController : BaseSearchController
    {
        // GET: Accounts/Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.AccountEntry);

            return View(new SearchListViewModel()
            {
                ControllerName = "Accounts/" + Eduegate.Infrastructure.Enums.SearchView.AccountEntry.ToString(),
                ViewName = Eduegate.Infrastructure.Enums.SearchView.AccountEntry,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                FilterColumns = metadata.QuickFilterColumns,
                HasFilters = metadata.HasFilters,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "")
        {
            return base.SearchData(Eduegate.Infrastructure.Enums.SearchView.AccountEntry, currentPage, orderBy);
        }

        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.AccountEntrySummary);
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View();
        }

        [HttpGet]
        public ActionResult Create(long ID = 0)
        {
            var viewModel = new CRUDViewModel();
            viewModel.Name = "AccountEntry";
            viewModel.ListActionName = "AccountEntry";
            viewModel.ViewModel = new AccountViewModel();
            viewModel.IID = ID;
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "AccountGroup", Url = "Mutual/GetLookUpData?lookType=AccountGroup" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "AccountBehavior", Url = "Mutual/GetLookUpData?lookType=AccountBehavior" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Accounts", Url = "Mutual/GetLookUpData?lookType=Account" });

            TempData["viewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(viewModel);
            return RedirectToAction("Create", "CRUD", new { area = "" });
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpGet]
        public ActionResult Get(int ID = 0)
        {
            var vm = AccountViewModel.FromDTO(ClientFactory.AccountingServiceClient(CallContext).GetAccount(ID));
            return Json(vm);
        }

        [HttpPost]
        public JsonResult Save([FromBody] AccountViewModel vm)
        {
            var updatedVM = AccountViewModel.FromDTO(ClientFactory.AccountingServiceClient(CallContext).SaveAccount(AccountViewModel.ToDTO(vm)));
            return Json(updatedVM);
        }
    }
}
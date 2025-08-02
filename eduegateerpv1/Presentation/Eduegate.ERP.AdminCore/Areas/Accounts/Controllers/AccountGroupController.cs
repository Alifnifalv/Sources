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
    public class AccountGroupController : BaseSearchController
    {
        // GET: Accounts/Group
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.AccountGroup);

            return View(new SearchListViewModel()
            {
                ControllerName = "Accounts/" + Eduegate.Infrastructure.Enums.SearchView.AccountGroup.ToString(),
                ViewName = Eduegate.Infrastructure.Enums.SearchView.AccountGroup,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                HasFilters = metadata.HasFilters,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "")
        {
            return base.SearchData(Eduegate.Infrastructure.Enums.SearchView.AccountGroup, currentPage, orderBy);
        }

        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.AccountGroupSummary);
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
            viewModel.Name = "AccountGroup";
            viewModel.ListActionName = "AccountGroup";
            viewModel.ViewModel = new AccountGroupViewModel();
            viewModel.IID = ID;

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
            var vm = AccountGroupViewModel.FromDTO(ClientFactory.AccountingServiceClient(CallContext).GetAccountGroup(ID));
            return Json(vm);
        }

        [HttpPost]
        public JsonResult Save([FromBody] AccountGroupViewModel vm)
        {
            var updatedVM = AccountGroupViewModel.FromDTO(ClientFactory.AccountingServiceClient(CallContext).SaveAccountGroup(AccountGroupViewModel.ToDTO(vm)));
            return Json(updatedVM);
        }
    }
}
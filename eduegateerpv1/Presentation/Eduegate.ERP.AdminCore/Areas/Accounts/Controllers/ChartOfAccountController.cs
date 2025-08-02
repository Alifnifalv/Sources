using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Accounts;
using Eduegate.Services.Client.Factory;
using System.Threading.Tasks;

namespace Eduegate.ERP.Admin.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    public class ChartOfAccountController : BaseSearchController
    {
        // GET: Accounts/ChartOfAccount
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.ChartOfAccount);

            return View(new SearchListViewModel()
            {
                ControllerName = "Accounts/" + Eduegate.Infrastructure.Enums.SearchView.ChartOfAccount.ToString(),
                ViewName = Eduegate.Infrastructure.Enums.SearchView.ChartOfAccount,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "")
        {
            return base.SearchData(Eduegate.Infrastructure.Enums.SearchView.ChartOfAccount, currentPage, orderBy);
        }

        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.ChartOfAccountSummary);
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
            viewModel.Name = "ChartOfAccount";
            viewModel.ListActionName = "ChartOfAccount";
            viewModel.ViewModel = new ChartOfAccountViewModel() { Details = new List<ChartOfAccountDetailViewModel>() { new ChartOfAccountDetailViewModel() } };
            viewModel.IID = ID;
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Account", Url = "Mutual/GetLookUpData?lookType=Account" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "IncomeOrBalance", Url = "Mutual/GetLookUpData?lookType=IncomeOrBalance" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "ChartRowType", Url = "Mutual/GetLookUpData?lookType=ChartRowType" });

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
            var vm = ChartOfAccountViewModel.ToViewModel(ClientFactory.AccountingServiceClient(CallContext).GetChartOfAccount(ID));
            return Json(vm);
        }

        [HttpPost]
        public JsonResult Save([FromBody] ChartOfAccountViewModel vm)
        {
            var updatedVM = ChartOfAccountViewModel.ToViewModel(ClientFactory.AccountingServiceClient(CallContext).SaveChartOfAccount(ChartOfAccountViewModel.ToDTO(vm)));
            return Json(updatedVM);
        }

        [HttpGet]
        public ActionResult ViewChartOfAccount()
        {
            Helpers.PortalWebHelper.GetDefaultSettingsToViewBag(CallContext, ViewBag);
            return View(new ChartOfAccountViewModel());
        }

        public JsonResult GetChartOfAccount()
        {
            var vm = new ChartOfAccountViewModel();
            vm.Details = new List<ChartOfAccountDetailViewModel>();
            var accounts = ClientFactory.AccountingServiceClient(CallContext).GetAllChartOfAccount();
            var groupedVm = accounts.GroupBy(a => a.AccountGroupID).ToList();

            foreach (var group in groupedVm)
            {
                var groupKey = group.Key;

                vm.Details.Add(new ChartOfAccountDetailViewModel()
                {
                    AccountGroupID = group.Key,
                    AccountGroupName = group.FirstOrDefault().AccountGroupName,
                    ChartRowType = "Header",
                    Balance = group.Sum(a=> a.Balance)
                });

                foreach (var groupedItem in group)
                {
                    vm.Details.Add(new ChartOfAccountDetailViewModel()
                    {
                        AccountCode = groupedItem.AccountCode,
                        AccountID = groupedItem.AccountID,
                        Name = groupedItem.Name,
                        AccountGroupName = groupedItem.AccountGroupName,
                        AccountGroupID = groupedItem.AccountGroupID,
                        Balance = groupedItem.Balance
                    });
                }
            }           
            
            return Json(vm);
        }
    }
}
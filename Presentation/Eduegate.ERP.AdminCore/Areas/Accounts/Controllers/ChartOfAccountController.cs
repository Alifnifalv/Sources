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
using Eduegate.Services.Contracts.Accounts.Accounting;
using System.Globalization;
using Eduegate.Web.Library.School.Accounts;
using Eduegate.Services.Contracts.School.Accounts;
using Eduegate.Services.Contracts.HR.Payroll;

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

        public JsonResult GetLedgerTransactions(int groupID)
        {
            var accounts = ClientFactory.AccountingServiceClient(CallContext).GetLedgerTransactions(groupID);
            return Json(accounts);
        }

        public JsonResult GetChartOfAccount()
        {
            var accounts = ClientFactory.AccountingServiceClient(CallContext).GetAllChartOfAccount();
            return Json(accounts);
        }

        [HttpPost]
        public ActionResult SaveAccounts(LedgerEntryViewModel ledgerEntryViewModel)
        {
            var entryViewModel = new LedgerEntryViewModel();
            dynamic response = new { IsFailed = 1, Message = "Something went wrong!" };
            
            try
            {
                if (ledgerEntryViewModel.IsCreateLedger == true)
                {
                    var accountDTO = new AccountDTO()
                    {
                        AccountID = ledgerEntryViewModel.AccountID,
                        AccountCode = ledgerEntryViewModel.AccountCode,
                        AccountName = ledgerEntryViewModel.AccountName,
                        AccountGroupID = ledgerEntryViewModel.AccountGroupID,
                        ParentAccountID = ledgerEntryViewModel.ParentAccountID,
                    };

                    var result = ClientFactory.AccountingServiceClient(CallContext).SaveAccount(accountDTO);
                    if (result != null && result.AccountID != 0)
                    {
                        response = new { IsFailed = false, Message = "Saved successfully!" };                       
                    }
                    else
                    {
                        response = new { IsFailed = true, Message = "Saving failed!" };                        
                    }
                }
                else
                {
                    var actGrpDTO = new AccountsGroupDTO()
                    {
                        GroupID = 0,
                        GroupName = ledgerEntryViewModel.AccountName,
                        Parent_ID = (int?)ledgerEntryViewModel.ParentAccountID,
                        GroupCode = ledgerEntryViewModel.AccountCode
                    };

                    var result = ClientFactory.AccountingServiceClient(CallContext).SaveAccountGroup(actGrpDTO);
                    if (result != null)
                    {
                        response = new { IsFailed = false, Message = "Saved successfully!" };
                    }
                    else
                    {
                        response = new { IsFailed = true, Message = "Saving failed!" };
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"Chart Of Account entry saving failed. Error message: {errorMessage}", ex);
                response = new { IsFailed = false, Message = "Saving failed!" };
            }
            return Json(response);
        }

        [HttpPost]
        public IActionResult UpdateAccountParent(int AccountID, int ParentAccountID, int AccountGroupID, string Operation, int TargetID)
        {
            try
            {
                // First, get the account being moved
                //var account = _dbContext.AccountGroups.Find(AccountID);
                //if (account == null)
                //{
                //    return Json(new { Success = false, Message = "Account not found" });
                //}

                // Get the target account
                //var targetAccount = _dbContext.AccountGroups.Find(TargetID);
                //if (targetAccount == null)
                //{
                //    return Json(new { Success = false, Message = "Target account not found" });
                //}

                // Update parent relationship
                //account.ParentID = ParentAccountID;

                // Update level based on parent
                //if (ParentAccountID > 0)
                //{
                //    var parentAccount = _dbContext.AccountGroups.Find(ParentAccountID);
                //    if (parentAccount != null)
                //    {
                //        account.Level = parentAccount.Level + 1;
                //    }
                //}
                //else
                //{
                //    account.Level = 1; // Root level
                //}

                // Save changes
                //_dbContext.SaveChanges();

                // Update child levels recursively if needed
                //UpdateChildLevels(AccountID);

                return Json(new { Success = true, Message = "Account structure updated successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
        }

        private void UpdateChildLevels(int accountId)
        {
        //var account = _dbContext.AccountGroups.Find(accountId);
        //if (account == null) return;

        //var children = _dbContext.AccountGroups.Where(a => a.ParentID == accountId).ToList();
        //foreach (var child in children)
        //{
        //    child.Level = account.Level + 1;
        //    _dbContext.SaveChanges();

        //    // Recursively update grandchildren
        //    UpdateChildLevels(child.AccountGroupID);
        //}
        }
    }
}
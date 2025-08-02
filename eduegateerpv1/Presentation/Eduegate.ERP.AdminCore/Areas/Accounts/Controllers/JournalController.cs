using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Accounts;
using Eduegate.Web.Library.ViewModels.Common;
using Eduegate.Web.Library.ViewModels.Inventory;
using Eduegate.Services.Client.Factory;
using Eduegate.Domain;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Eduegate.ERP.Admin.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    public class JournalController : BaseSearchController
    {
        private string dateTimeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateTimeFormat");

        // GET: Inventories/SalesOrder
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.Journal);

            return View(new SearchListViewModel
            {
                ControllerName = "Accounts/" + Eduegate.Infrastructure.Enums.SearchView.Journal,
                ViewName = Eduegate.Infrastructure.Enums.SearchView.Journal,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                //                InfoBar = @"<li class='status-label-mobile'>
                //                                        <div class='right status-label'>
                //                                            <div class='status-label-color'><label class='status-color-label  green'></label>Active</div>
                //                                            <div class='status-label-color'><label class='status-color-label red'></label>In Active</div>
                //                                        </div>
                //                                    </li>"
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "")
        {
            return base.SearchData(Eduegate.Infrastructure.Enums.SearchView.Journal, currentPage, orderBy);
        }

        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.JournalSummary);
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View();
        }

        public ActionResult Create(long ID = 0)
        {
            //var viewModel = new CRUDMasterDetailViewModel();
            //viewModel.Name = "Journal";
            //viewModel.ListActionName = "Journal";
            //viewModel.ListButtonDisplayName = "Lists";
            //viewModel.DisplayName = "Journal";
            var vm = new JournalEntryViewModel()
            {
                DetailViewModel = new List<JournalDetailViewModel>() { new JournalDetailViewModel() { } },
                MasterViewModel = new JournalMasterViewModel() { TransactionDate = DateTime.Now.ToString(dateTimeFormat) }
            };

            //viewModel.MasterViewModel = new JournalMasterViewModel();
            //viewModel.DetailViewModel = new JournalDetailViewModel();
            //viewModel.SummaryViewModel = new SummaryDetailedViewModel();
            //viewModel.IID = ID;
            //viewModel.EntityType = Framework.Enums.EntityTypes.Transaction;

            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Branch", Url = "Mutual/GetLookUpData?lookType=Branch" });
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "DocumentType", Url = "Mutual/GetDocumentTypesByID?referenceType=Journal" });
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Accounts", Url = "Mutual/GetLookUpData?lookType=Account" });
            //TempData["viewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(viewModel);
            //return RedirectToAction("CreateMasterDetail", "CRUD", new { area = "" });
            return Json(vm);
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            var vm = JournalEntryViewModel.FromDTO(ClientFactory.AccountingTransactionServiceClient(CallContext).GetAccountTransactionHeadById(ID));
            if (vm != null)
            {
                vm.DetailViewModel.Add(new JournalDetailViewModel());
            }
            return Json(vm);
        }

        [HttpPost]
        public JsonResult Save(string model)
        {
            try
            {
                var jsonData = JObject.Parse(model);
                var vm = JsonConvert.DeserializeObject<JournalEntryViewModel>(jsonData.SelectToken("data").ToString());

                var updatedVM = new JournalEntryViewModel();

                if (vm != null)
                {
                    if (vm.DetailViewModel != null)
                    {
                        vm.DetailViewModel = vm.DetailViewModel.Where(x => x.Account != null && !string.IsNullOrEmpty(x.Account.Key)).ToList();

                        if (vm.DetailViewModel.Any(x => x.Account == null /*&& x.VendorCustomerAccounts == null*/))
                        {
                            return Json(new { IsError = true, UserMessage = "Please select  account" });
                        }
                        if (vm.DetailViewModel.Any(x => (x.Credit == 0 || x.Credit == null) && (x.Debit == 0 || x.Debit == null)))
                        {
                            return Json(new { IsError = true, UserMessage = "Please enter Debit or Credit amount" });
                        }
                        if (vm.DetailViewModel.Any(x => x.Credit > 0 && x.Debit > 0))
                        {
                            return Json(new { IsError = true, UserMessage = "Both credit and Debit cannot be allowed for an item" });
                        }
                        if (vm.DetailViewModel.Sum(x => x.Credit) != vm.DetailViewModel.Sum(x => x.Debit))
                        {
                            return Json(new { IsError = true, UserMessage = "Credit Amount and Debit amount should be equal" });
                        }

                    }


                    var result = ClientFactory.AccountingTransactionServiceClient(CallContext).SaveAccountTransactionHead(JournalEntryViewModel.ToDTO(vm));
                    //var result = ServiceHelper.HttpPostRequest(ServiceHost + Eduegate.Framework.Helper.Constants.PRODUCT_DETAIL_SERVICE_NAME + "SaveTransactions", TransactionViewModel.ToSalesOrderDTO(vm), this.CallContext);
                    updatedVM = JournalEntryViewModel.FromDTO(result);
                }

                if (updatedVM != null)
                {
                    updatedVM.DetailViewModel.Add(new JournalDetailViewModel());
                }
                if (updatedVM.MasterViewModel.IsTransactionCompleted)
                {
                    return Json(new { IsError = true, UserMessage = "Cannot update the trasaction, it's already processed.", data = vm });
                }
                return Json(updatedVM);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<JournalController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message });
            }
        }
    }
}
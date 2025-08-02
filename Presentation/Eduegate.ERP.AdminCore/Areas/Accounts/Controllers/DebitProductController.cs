using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Framework.Helper;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.ViewModels.AccountTransactions;
using Eduegate.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Eduegate.Application.Mvc;
using Eduegate.Domain;
using Newtonsoft.Json.Linq;
using Eduegate.Web.Library.ViewModels.Accounts;
using Eduegate.Services.Contracts.Accounts.Accounting;
using Eduegate.ERP.Admin.Helpers;

namespace Eduegate.ERP.Admin.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    public class DebitProductController : BaseSearchController
    {
        private string dateTimeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateTimeFormat");

        private static string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }

        private string ReferenceServiceUrl = string.Concat(ServiceHost, Constants.REFERENCE_DATA_SERVICE);

        // GET: Inventories/InventoryDetails
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.DebitProduct);
            return View(new SearchListViewModel
            {
                ControllerName = "Accounts/" + Eduegate.Infrastructure.Enums.SearchView.DebitProduct.ToString(),
                ViewName = Eduegate.Infrastructure.Enums.SearchView.DebitProduct,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                IsEditableLink = true,
                InfoBar = @"<li class='status-label-mobile'>
                                        <div class='right status-label'>
                                            <div class='status-label-color'><label class='status-color-label  green'></label>Average Quantity</div>
                                            <div class='status-label-color'><label class='status-color-label orange'></label>More Quantity</div>
                                            <div class='status-label-color'><label class='status-color-label red'></label>Less Quantity</div>
                                        </div>
                                    </li>",
                IsChild = false,
                HasChild = true,
                FilterColumns = metadata.QuickFilterColumns,
                HasFilters = metadata.HasFilters,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public async Task<IActionResult> ChildList(long ID)
        {
            // call the service and get the meta data
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList(Services.Contracts.Enums.SearchView.DebitProduct);

            return PartialView("_ChildList", new SearchListViewModel
            {
                ViewName = Eduegate.Infrastructure.Enums.SearchView.DebitProduct,
                HeaderList = metadata.Columns,
                // SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                FilterColumns = metadata.QuickFilterColumns,
                RuntimeFilter = Convert.ToString("HeadIID=" + ID.ToString()).Trim(), // child view must have this column
                IsChild = true,
                ParentViewName = Eduegate.Infrastructure.Enums.SearchView.DebitProduct,
                IsEditableLink = false
            });
        }


        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {

            var view = Eduegate.Infrastructure.Enums.SearchView.DebitProduct;
            if (runtimeFilter.Trim().IsNotNullOrEmpty())
                view = Eduegate.Infrastructure.Enums.SearchView.DebitProduct;
            else
                runtimeFilter = runtimeFilter + " DocumentTypeID=" + 1521;

            return base.SearchData(view, currentPage, orderBy, runtimeFilter);

        }


        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.DebitProductSummary);
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
            var vm = new DebitProductViewModel()
            {
                DetailViewModel = new List<DebitProductDetailViewModel>() { new DebitProductDetailViewModel()
                {
                    //PaymentDueDate=DateTime.Now.ToString(dateTimeFormat)
                }
           },
                MasterViewModel = new DebitProductMasterViewModel()
                {
                    TransactionDate = DateTime.Now.ToString(dateTimeFormat),
                    DocumentStatus = new KeyValueViewModel() { Key = "82", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentStatuses.New) },
                    TransactionStatus = new KeyValueViewModel() { Key = "1", Value = Convert.ToString(Eduegate.Framework.Enums.TransactionStatus.New) },
                    Currency = KeyValueViewModel.ToViewModel(ClientFactory.ReferenceDataServiceClient(CallContext).GetDefaultCurrencyWithName()),
                    DocumentType = new KeyValueViewModel() { Key = "1521", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.DebitProduct) },
                    DocumentReferenceTypeID = (int)Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.DebitProduct

                }
            };

            return Json(vm);
        }

        #region GetNextTransactionNumberByMonthYear
        [HttpPost]
        public JsonResult GetNextTransactionNumberByMonthYear(DebitProductViewModel vm)
        {
            if (vm != null && vm.MasterViewModel != null && vm.MasterViewModel.DocumentType != null
                                                //&& vm.MasterViewModel.PaymentModes !=null 
                                                && vm.MasterViewModel.TransactionDate != null)
            {
                var dto = new TransactionNumberDTO();
                dto.DocumentTypeID = Convert.ToInt32(vm.MasterViewModel.DocumentType.Key);
                dto.Month = (Convert.ToDateTime(vm.MasterViewModel.TransactionDate)).Month;
                dto.Year = (Convert.ToDateTime(vm.MasterViewModel.TransactionDate)).Year;

                string NextTransactionNumber = ClientFactory.MutualServiceClient(CallContext).GetNextTransactionNumberByMonthYear(dto);
                return Json(NextTransactionNumber);
            }
            return Json("");

        }
        #endregion

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpPost]
        public JsonResult Save(string model)
        {
            var jsonData = JObject.Parse(model);
            var debitProductViewModel = JsonConvert.DeserializeObject<DebitProductViewModel>(jsonData.SelectToken("data").ToString());

            try
            {
                if (debitProductViewModel != null)
                {
                    if (debitProductViewModel.DetailViewModel != null)
                    {
                        debitProductViewModel.DetailViewModel = debitProductViewModel.DetailViewModel.Where(x => x.SKUID != null).ToList();
                    }

                    AccountTransactionHeadDTO HeadDTO = new DebitProductViewModel().ToAccountTransactionHeadDTO(debitProductViewModel);
                    AccountTransactionHeadDTO dto = ClientFactory.AccountingTransactionServiceClient(CallContext).SaveAccountTransactionHead(HeadDTO);
                    debitProductViewModel = new DebitProductViewModel().ToDebitProductViewModel(dto);
                    if (debitProductViewModel.MasterViewModel.IsError && !string.IsNullOrEmpty(debitProductViewModel.MasterViewModel.ErrorCode))
                    {
                        return Json(new { IsError = true, UserMessage = PortalWebHelper.GetErrorMessage(debitProductViewModel.MasterViewModel.ErrorCode), data = debitProductViewModel });
                    }

                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<DebitProductController>.Fatal(exception.Message.ToString(), exception);
                return Json(new { IsError = !false, UserMessage = exception.Message.ToString() });
            }

            return Json(debitProductViewModel);
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            AccountTransactionHeadDTO dto = ClientFactory.AccountingTransactionServiceClient(CallContext).GetAccountTransactionHeadById(ID);
            DebitProductViewModel debitProductViewModel = new DebitProductViewModel().ToDebitProductViewModel(dto);
            return Json(debitProductViewModel);
        }

        [HttpGet]
        public ActionResult GetProductSKUMapByID(long ProductSKUMapIID)
        {
            var AccountTransactionHeadDTO = ClientFactory.AccountingTransactionServiceClient(CallContext).GetProductSKUMapByID(ProductSKUMapIID);
            return Json(AccountTransactionHeadDTO);
        }

    }
}

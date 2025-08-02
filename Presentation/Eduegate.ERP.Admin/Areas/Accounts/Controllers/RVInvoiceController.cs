using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Framework.Helper;
using Eduegate.Web.Library.ViewModels.Accounts;
using Eduegate.Web.Library.ViewModels.AccountTransactions;
using Eduegate.Services.Client.Factory;

namespace Eduegate.ERP.Admin.Areas.Accounts.Controllers
{
    public class RVInvoiceController : BaseSearchController
    {
        private string dateTimeFormat = ConfigurationExtensions.GetAppConfigValue("DateTimeFormat");

        private static string ServiceHost { get { return ConfigurationExtensions.GetAppConfigValue("ServiceHost"); } }

        private string ReferenceServiceUrl = string.Concat(ServiceHost, Constants.REFERENCE_DATA_SERVICE);

        // GET: Inventories/InventoryDetails
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.RVInvoice);
            return View(new SearchListViewModel
            {
                ControllerName = "Accounts/" + Eduegate.Infrastructure.Enums.SearchView.RVInvoice.ToString(),
                ViewName = Eduegate.Infrastructure.Enums.SearchView.RVInvoice,
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
        public ActionResult ChildList(long ID)
        {
            // call the service and get the meta data
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList(Services.Contracts.Enums.SearchView.RVInvoice);

            return PartialView("_ChildList", new SearchListViewModel
            {
                ViewName = Eduegate.Infrastructure.Enums.SearchView.RVInvoice,
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
                ParentViewName = Eduegate.Infrastructure.Enums.SearchView.RVInvoice,
                IsEditableLink = false
            });
        }


        [HttpGet]
        public JsonResult SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {

            var view = Eduegate.Infrastructure.Enums.SearchView.RVInvoice;
            if (runtimeFilter.Trim().IsNotNullOrEmpty())
                view = Eduegate.Infrastructure.Enums.SearchView.RVInvoice;
            else
                runtimeFilter = runtimeFilter + " DocumentTypeID=" + 1511;

            return base.SearchData(view, currentPage, orderBy, runtimeFilter);

        }


        [HttpGet]
        public JsonResult SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.RVInvoiceSummary);
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
            var vm = new RVInvoiceViewModel()
            {
                DetailViewModel = new List<RVInvoiceDetailViewModel>() { new RVInvoiceDetailViewModel()
                    {
                        PaymentDueDate=DateTime.Now.ToString(dateTimeFormat)
                    }
                },
                MasterViewModel = new RVInvoiceMasterViewModel()
                {
                    TransactionDate = DateTime.Now.ToString(dateTimeFormat),
                    DocumentStatus = new KeyValueViewModel() { Key = "82", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentStatuses.New) },
                    TransactionStatus = new KeyValueViewModel() { Key = "1", Value = Convert.ToString(Eduegate.Framework.Enums.TransactionStatus.New) },
                    Currency = KeyValueViewModel.ToViewModel(ClientFactory.ReferenceDataServiceClient(CallContext).GetDefaultCurrencyWithName()),
                    DocumentType = new KeyValueViewModel() { Key = "1511", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.Receipts) },
                    DocumentReferenceTypeID = (int)Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.Receipts

                }
            };

            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpPost]
        public JsonResult Save(RVInvoiceViewModel rvInvoiceViewModel)
        {
            try
            {
                if (rvInvoiceViewModel != null)
                {
                    if (rvInvoiceViewModel.DetailViewModel != null)
                    {
                        //if (rvInvoiceViewModel.MasterViewModel.Amount != rvInvoiceViewModel.DetailViewModel.Sum(x => x.Amount))
                        //{
                        //    return Json(new { IsError = true, UserMessage = "Amount should be equal" }, JsonRequestBehavior.AllowGet);
                        //}
                    }
                    var headDTO = new RVInvoiceViewModel().ToAccountTransactionHeadDTO(rvInvoiceViewModel);
                    var dto = ClientFactory.AccountingTransactionServiceClient(CallContext).SaveAccountTransactionHead(headDTO);
                    rvInvoiceViewModel = new RVInvoiceViewModel().ToRVInvoiceViewModel(dto);

                    if (rvInvoiceViewModel.MasterViewModel.IsTransactionCompleted)
                    {
                        return Json(new { IsError = true, UserMessage = "Cannot update the trasaction, it's already processed.", data = rvInvoiceViewModel }, JsonRequestBehavior.AllowGet);
                    }

                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<RVInvoiceController>.Fatal(exception.Message.ToString(), exception);
                return Json(new { IsError = !false, UserMessage = exception.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }

            return Json(rvInvoiceViewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            var dto = ClientFactory.AccountingTransactionServiceClient(CallContext).GetAccountTransactionHeadById(ID);
            var rvInvoiceViewModel = new RVInvoiceViewModel().ToRVInvoiceViewModel(dto);
            return Json(rvInvoiceViewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPendingInvoices(long customerID)
        {
            var dtos = ClientFactory.AccountingTransactionServiceClient(CallContext).GetCustomerPendingInvoices(customerID);
            var rvInvoiceViewModel = new ReceivableViewModel().ToVM(dtos);
            return Json(rvInvoiceViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}

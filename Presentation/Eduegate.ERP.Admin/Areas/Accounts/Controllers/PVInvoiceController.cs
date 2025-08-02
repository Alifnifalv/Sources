using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Framework.Helper;
using Eduegate.Web.Library.ViewModels.Accounts;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.ViewModels.AccountTransactions;

namespace Eduegate.ERP.Admin.Areas.Accounts.Controllers
{
    public class PVInvoiceController : BaseSearchController
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
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.PVInvoice);
            return View(new SearchListViewModel
            {
                ControllerName = "Accounts/" + Eduegate.Infrastructure.Enums.SearchView.PVInvoice.ToString(),
                ViewName = Eduegate.Infrastructure.Enums.SearchView.PVInvoice,
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
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList(Services.Contracts.Enums.SearchView.PVInvoice);

            return PartialView("_ChildList", new SearchListViewModel
            {
                ViewName = Eduegate.Infrastructure.Enums.SearchView.PVInvoice,
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
                ParentViewName = Eduegate.Infrastructure.Enums.SearchView.PVInvoice,
                IsEditableLink = false
            });
        }


        [HttpGet]
        public JsonResult SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {

            var view = Eduegate.Infrastructure.Enums.SearchView.PVInvoice;
            if (runtimeFilter.Trim().IsNotNullOrEmpty())
                view = Eduegate.Infrastructure.Enums.SearchView.PVInvoice;
            else
                runtimeFilter = runtimeFilter + " DocumentTypeID=" + 1517;

            return base.SearchData(view, currentPage, orderBy, runtimeFilter);

        }


        [HttpGet]
        public JsonResult SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.PVInvoiceSummary);
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
            var vm = new PVInvoiceViewModel()
            {
                DetailViewModel = new List<PVInvoiceDetailViewModel>() { new PVInvoiceDetailViewModel()
                {
                    PaymentDueDate=DateTime.Now.ToString(dateTimeFormat)
                }
           },
                MasterViewModel = new PVInvoiceMasterViewModel()
                {
                    TransactionDate = DateTime.Now.ToString(dateTimeFormat),
                    DocumentStatus = new KeyValueViewModel() { Key = "106", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentStatuses.New) },
                    TransactionStatus = new KeyValueViewModel() { Key = "1", Value = Convert.ToString(Eduegate.Framework.Enums.TransactionStatus.New) },
                    Currency = KeyValueViewModel.ToViewModel(ClientFactory.ReferenceDataServiceClient(CallContext).GetDefaultCurrencyWithName()),
                    DocumentType = new KeyValueViewModel() { Key = "1517", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.Payments) },
                    DocumentReferenceTypeID = (int)Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.Payments

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
        public JsonResult Save(PVInvoiceViewModel pvInvoiceViewModel)
        {
            try
            {
                if (pvInvoiceViewModel != null)
                {
                    if (pvInvoiceViewModel.DetailViewModel != null)
                    {
                        
                    }
                    AccountTransactionHeadDTO HeadDTO = new PVInvoiceViewModel().ToAccountTransactionHeadDTO(pvInvoiceViewModel);

                    AccountTransactionHeadDTO dto = ClientFactory.AccountingTransactionServiceClient(CallContext).SaveAccountTransactionHead(HeadDTO);
                    pvInvoiceViewModel = new PVInvoiceViewModel().ToPVInvoiceViewModel(dto);

                    if (pvInvoiceViewModel.MasterViewModel.IsTransactionCompleted)
                    {
                        return Json(new { IsError = true, UserMessage = "Cannot update the trasaction, it's already processed.", data = pvInvoiceViewModel }, JsonRequestBehavior.AllowGet);
                    }

                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<PVInvoiceController>.Fatal(exception.Message.ToString(), exception);
                return Json(new { IsError = !false, UserMessage = exception.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
            return Json(pvInvoiceViewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            AccountTransactionHeadDTO dto = ClientFactory.AccountingTransactionServiceClient(CallContext).GetAccountTransactionHeadById(ID);
            PVInvoiceViewModel pvInvoiceViewModel = new PVInvoiceViewModel().ToPVInvoiceViewModel(dto);
            return Json(pvInvoiceViewModel, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetPayablesByAccountId(long AccountID, Eduegate.Services.Contracts.Enums.DocumentReferenceTypes DocumentType)
        {
            var PayablesDTO = ClientFactory.AccountingTransactionServiceClient(CallContext).GetPayablesByAccountId(AccountID, DocumentType);
            return Json(PayablesDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPendingInvoices(long supplierID)
        {
            var dtos = ClientFactory.AccountingTransactionServiceClient(CallContext).GetSupplierPendingInvoices(supplierID);
            var rvInvoiceViewModel = new PayableViewModel().ToVM(dtos);
            return Json(rvInvoiceViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}

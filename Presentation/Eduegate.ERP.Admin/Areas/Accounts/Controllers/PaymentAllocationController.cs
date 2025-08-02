using Microsoft.AspNet.Identity;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.Enums;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eduegate.Infrastructure.Enums;

namespace Eduegate.ERP.Admin.Areas.Accounts.Controllers
{
    public class PaymentAllocationController : BaseSearchController
    {
        private string dateTimeFormat = ConfigurationExtensions.GetAppConfigValue("DateTimeFormat");

        private static string ServiceHost { get { return ConfigurationExtensions.GetAppConfigValue("ServiceHost"); } }

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
                ControllerName = "Accounts/" + Eduegate.Infrastructure.Enums.SearchView.PVInvoiceAllocation.ToString(),
                ViewName = Eduegate.Infrastructure.Enums.SearchView.PVInvoiceAllocation,
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

        public ActionResult Allocate(string invoiceIDs)
        {
            return RedirectToAction("Create", "CRUD", new { screen = Screens.PVInvoiceAllocation.ToString(), area = "Frameworks",  ID = 0, parameters= "InvoiceIDs =" + invoiceIDs });
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            var receivableIds = new List<long>();
            receivableIds.Add(ID);
            var receipts = ClientFactory.AccountingTransactionServiceClient(CallContext).GetPayables(receivableIds);
            var alloctions = ClientFactory.AccountingTransactionServiceClient(CallContext).GetSupplierPendingInvoices(receipts[0].AccountID.Value);
            var vm = PVInvoiceAllocationViewModel.ToVM(receipts, alloctions);
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
    }
}
using Eduegate.ERP.Admin.Controllers;
using Eduegate.ERP.Admin.Helpers;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Inventory.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Areas.Inventories.Controllers
{
    public class ServiceEntrycontroller : BaseSearchController
    {
        private string dateTimeFormat = ConfigurationExtensions.GetAppConfigValue("DateTimeFormat");

        // GET: Inventories/GoodsReturnNote
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Infrastructure.Enums.SearchView.ServiceEntry);

            return View(new SearchListViewModel
            {
                ControllerName = "Inventories/" + Infrastructure.Enums.SearchView.ServiceEntry,
                ViewName = Infrastructure.Enums.SearchView.ServiceEntry,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsCommentLink = true,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                InfoBar = @"<li class='status-label-mobile'>
                                        <div class='right status-label'>
                                            <div class='status-label-color'><label class='status-color-label  green'></label>Active</div>
                                            <div class='status-label-color'><label class='status-color-label red'></label>In Active</div>
                                        </div>
                                    </li>",
                IsChild = false,
                HasChild = true,
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                UserValues = metadata.UserValues
            });
        }
        [HttpGet]
        public ActionResult ChildList(long ID)
        {
            // call the service and get the meta data
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList(Services.Contracts.Enums.SearchView.PurchaseSKU);

            return PartialView("_ChildList", new SearchListViewModel
            {
                ControllerName = "ProductSKU",
                ViewName = Infrastructure.Enums.SearchView.PurchaseSKU,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                FilterColumns = metadata.QuickFilterColumns,
                RuntimeFilter = Convert.ToString("HeadIID=" + ID.ToString()).Trim(), // child view must have this column
                IsChild = true,
                ParentViewName = Infrastructure.Enums.SearchView.ServiceEntry,
                IsEditableLink = false
            });
        }


        [HttpGet]
        public JsonResult SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            var view = Infrastructure.Enums.SearchView.ServiceEntry;
            if (runtimeFilter.Trim().IsNotNullOrEmpty())
            {
                view = Infrastructure.Enums.SearchView.PurchaseSKU;
                runtimeFilter = runtimeFilter + " AND CompanyID = " + CallContext.CompanyID.ToString();
            }
            else
            {
                runtimeFilter = " CompanyID = " + CallContext.CompanyID.ToString();
            }

            return base.SearchData(view, currentPage, orderBy, runtimeFilter);
        }

        [HttpGet]
        public JsonResult SearchSummaryData()
        {
            return base.SearchSummaryData(Infrastructure.Enums.SearchView.ServiceEntrySummary);
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
            var vm = new ServiceEntryViewModel()
            {
                DetailViewModel = new List<ServiceEntryDetailViewModel>() { new ServiceEntryDetailViewModel() { } },
                MasterViewModel = new ServiceEntryMasterViewModel()
                {
                    TransactionDate = DateTime.Now.ToString(dateTimeFormat),
                    Validity = DateTime.Now.ToString(dateTimeFormat),
                    //Currency = KeyValueViewModel.ToViewModel(ClientFactory.ReferenceDataServiceClient(CallContext).GetDefaultCurrencyWithName()),
                    TransactionStatus = new KeyValueViewModel() { Key = "1", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.TransactionStatus.None) },
                    DocumentStatus = new KeyValueViewModel() { Key = "1", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentStatuses.Draft) },
                }
            };

            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(ServiceEntryViewModel vm)
        {
            try
            {
                var updatedVM = new ServiceEntryViewModel();

                if (vm != null)
                {
                    var result = ClientFactory.TransactionServiceClient(CallContext).SaveTransactions(ServiceEntryViewModel.FromVMToTransactionDTO(vm));
                    updatedVM = ServiceEntryViewModel.FromTransactionDTOToVM(result);
                    updatedVM.DetailViewModel.Add(new ServiceEntryDetailViewModel());
                    // once TransactionStatus completed we should not allow any operation on any transaction..
                    if (updatedVM.MasterViewModel.IsTransactionCompleted)
                    {
                        return Json(new { IsError = true, UserMessage = "Cannot update the trasaction, it's already processed.", data = vm }, JsonRequestBehavior.AllowGet);
                    }

                    if (updatedVM.MasterViewModel.IsError == true)
                    {
                        return Json(new { IsError = true, UserMessage = PortalWebHelper.GetErrorMessage(updatedVM.MasterViewModel.ErrorCode), data = vm }, JsonRequestBehavior.AllowGet);
                    }
                }

                return Json(updatedVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<PurchaseReturnRequestController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            try
            {
                var vm = ServiceEntryViewModel.FromTransactionDTOToVM(ClientFactory.TransactionServiceClient(CallContext).GetTransaction(ID, false, false));
                if (vm != null)
                {
                    vm.DetailViewModel.Add(new ServiceEntryDetailViewModel());
                }
                return Json(vm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<ServiceEntrycontroller>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
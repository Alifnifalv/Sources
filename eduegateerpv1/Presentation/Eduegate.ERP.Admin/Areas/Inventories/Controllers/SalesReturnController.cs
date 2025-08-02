using Eduegate.ERP.Admin.Controllers;
using Eduegate.ERP.Admin.Helpers;
using Eduegate.Framework.Extensions;
//using Eduegate.Framework.Web.Library.Common;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Inventory;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Areas.Inventories.Controllers
{
    public class SalesReturnController : BaseSearchController
    {
        private string dateTimeFormat = ConfigurationExtensions.GetAppConfigValue("DateTimeFormat");

        // GET: Inventories/PurchaseOrder
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Infrastructure.Enums.SearchView.SalesReturn);

            return View(new SearchListViewModel
            {
                ControllerName = "Inventories/" + Infrastructure.Enums.SearchView.SalesReturn,
                ViewName = Infrastructure.Enums.SearchView.SalesReturn,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                IsCommentLink = true,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                //                InfoBar = @"<li class='status-label-mobile'>
                //                                        <div class='right status-label'>
                //                                            <div class='status-label-color'><label class='status-color-label  green'></label>Active</div>
                //                                            <div class='status-label-color'><label class='status-color-label red'></label>In Active</div>
                //                                        </div>
                //                                    </li>"
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
                ParentViewName = Infrastructure.Enums.SearchView.SalesReturn,
                IsEditableLink = false
            });
        }

        [HttpGet]
        public JsonResult SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            var view = Infrastructure.Enums.SearchView.SalesReturn;
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
            return base.SearchSummaryData(Infrastructure.Enums.SearchView.SalesReturnSummary);
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View();
        }

        public ActionResult Create(long ID = 0)
        {
            var vm = new SalesReturnViewModel()
            {
                DetailViewModel = new List<SalesReturnDetailViewModel>() { new SalesReturnDetailViewModel() { } },
                MasterViewModel = new SalesReturnMasterViewModel()
                {
                    TransactionDate = DateTime.Now.ToString(dateTimeFormat),
                    TransactionStatus = new KeyValueViewModel() { Key = "1", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentStatuses.Draft) },
                    //Currency = KeyValueViewModel.ToViewModel(ClientFactory.ReferenceDataServiceClient(CallContext).GetDefaultCurrencyWithName())
                }
            };

            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            var vm = TransactionViewModel.FromSalesReturnDTO(ClientFactory.TransactionServiceClient(CallContext).GetTransaction(ID, false, false));
            if (vm != null)
            {
                vm.DetailViewModel.Add(new SalesReturnDetailViewModel());
            }
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(SalesReturnViewModel vm)
        {
            try
            {
                var updatedVM = new SalesReturnViewModel();

                if (vm != null)
                {
                    var result = ClientFactory.TransactionServiceClient(CallContext).SaveTransactions(TransactionViewModel.ToSalesReturnDTO(vm));
                    //var result = ServiceHelper.HttpPostRequest(ServiceHost + Eduegate.Framework.Helper.Constants.PRODUCT_DETAIL_SERVICE_NAME + "SaveTransactions", TransactionViewModel.ToSalesReturnDTO(vm), this.CallContext);
                    updatedVM = TransactionViewModel.FromSalesReturnDTO(result);
                }

                if (updatedVM != null)
                {
                    updatedVM.DetailViewModel.Add(new SalesReturnDetailViewModel());
                }

                // once TransactionStatus completed we should not allow any operation on any transaction..
                if (updatedVM.MasterViewModel.IsTransactionCompleted)
                {
                    return Json(new { IsError = true, UserMessage = "Cannot update the trasaction, it's already processed.", data = vm }, JsonRequestBehavior.AllowGet);
                }

                if (updatedVM.MasterViewModel.IsError == true)
                {
                    return Json(new { IsError = true, UserMessage = PortalWebHelper.GetErrorMessage(updatedVM.MasterViewModel.ErrorCode), data = vm }, JsonRequestBehavior.AllowGet);
                }
                if (!updatedVM.MasterViewModel.IsTransactionCompleted && updatedVM.MasterViewModel.TransactionHeadIID != 0 && updatedVM.MasterViewModel.Student!=null && !string.IsNullOrEmpty(updatedVM.MasterViewModel.Student.Key))
                {
                    var feeData = ClientFactory.SchoolServiceClient(CallContext).AutoReceiptAccountTransactionSync(0, updatedVM.MasterViewModel.TransactionHeadIID, 0, 0, 1); 
                   
                }
                return Json(updatedVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<SalesReturnController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
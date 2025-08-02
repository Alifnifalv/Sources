using Eduegate.ERP.Admin.Controllers;
using Eduegate.ERP.Admin.Helpers;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Warehouses;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Common;
using Eduegate.Web.Library.ViewModels.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Domain;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Eduegate.Web.Library.ViewModels.Inventory.Purchase;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Domain.Repository.Payroll;
using Eduegate.Domain.Payroll;

namespace Eduegate.ERP.Admin.Areas.Inventories.Controllers
{
    [Area("Inventories")]
    public class PurchaseRequestController : BaseSearchController
    {
        private string dateTimeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateTimeFormat");

        // GET: Inventories/PurchaseOrder
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Infrastructure.Enums.SearchView.PurchaseRequest);

            return View(new SearchListViewModel
            {
                ControllerName = "Inventories/" + Infrastructure.Enums.SearchView.PurchaseRequest,
                ViewName = Infrastructure.Enums.SearchView.PurchaseRequest,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                IsCommentLink = true,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                InfoBar = @"<li class='status-label-mobile'>
                                                        <div class='right status-label'>
                                                            <div class='status-label-color'><label class='status-color-label  green'></label>Complete</div>
                                                            <div class='status-label-color'><label class='status-color-label red'></label>Fail</div>
                                                            <div class='status-label-color'><label class='status-color-label orange'></label>Others</div>
                                                        </div>
                                                    </li>",
                IsChild = false,
                HasChild = true,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public async Task<IActionResult> ChildList(long ID)
        {
            // call the service and get the meta data
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList(Services.Contracts.Enums.SearchView.PurchaseSKU);

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
                ParentViewName = Infrastructure.Enums.SearchView.SalesInvoice,
                IsEditableLink = false
            });
        }

        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            var view = Infrastructure.Enums.SearchView.PurchaseRequest;
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
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Infrastructure.Enums.SearchView.PurchaseRequestSummary);
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View();
        }

        public ActionResult Create(long ID = 0)
        {
            var vm = new PurchaseRequestViewModel()
            {
                DetailViewModel = new List<PurchaseRequestDetailViewModel>() { new PurchaseRequestDetailViewModel() { } },
                MasterViewModel = new PurchaseRequestMasterViewModel()
                {
                    TransactionDate = DateTime.Now.ToString(dateTimeFormat),
                    DocumentStatus = new KeyValueViewModel() { Key = "1", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentStatuses.Draft) }
                }
            };
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
            var vm = TransactionViewModel.FromPurchaseRequestDTO(ClientFactory.TransactionServiceClient(CallContext).GetTransaction(ID, true, false));
            if (vm != null)
            {
                vm.DetailViewModel.Add(new PurchaseRequestDetailViewModel());
            }
            return Json(vm);
        }

        [HttpPost]
        public JsonResult Save(string model)
        {
            try
            {
                var jsonData = JObject.Parse(model);
                var vm = JsonConvert.DeserializeObject<PurchaseRequestViewModel>(jsonData.SelectToken("data").ToString());

                var updatedVM = new PurchaseRequestViewModel();
                var userMessage = string.Empty;
                if (vm != null)
                {
                    vm.MasterViewModel.DocumentReferenceTypeID = Services.Contracts.Enums.DocumentReferenceTypes.PurchaseRequest;
                    var result = ClientFactory.TransactionServiceClient(CallContext).SaveTransactions(TransactionViewModel.ToPurchaseRequestDTO(vm));

                    updatedVM = TransactionViewModel.FromPurchaseRequestDTO(result);
                }

                if (updatedVM != null)
                {
                    updatedVM.DetailViewModel.Add(new PurchaseRequestDetailViewModel());
                }

                if (updatedVM.DetailViewModel.IsNotNull() && updatedVM.DetailViewModel.Count > 0 && updatedVM.IsError == true)
                {
                    for (int lineCounter = 0; lineCounter < updatedVM.DetailViewModel.Count; lineCounter++)
                    {
                        if (updatedVM.DetailViewModel[lineCounter].IsError)
                        {
                            // set error on detail
                            vm.DetailViewModel[lineCounter].IsError = true;

                            // Loopthrough serials for this line to set error
                            for (int serialCounter = 0; serialCounter < updatedVM.DetailViewModel[lineCounter].SKUDetails.Count; serialCounter++)
                            {
                                if (updatedVM.DetailViewModel[lineCounter].SKUDetails[serialCounter].IsError)
                                {
                                    // Set error and errormsg for serial number
                                    vm.DetailViewModel[lineCounter].SKUDetails[serialCounter].IsError = true;
                                    vm.DetailViewModel[lineCounter].SKUDetails[serialCounter].ErrorMessage = PortalWebHelper.GetErrorMessage(updatedVM.DetailViewModel[lineCounter].SKUDetails[serialCounter].ErrorMessage);
                                }
                            }
                        }
                    }
                    userMessage = PortalWebHelper.GetErrorMessage(updatedVM.ErrorCode);
                    return Json(new { IsError = true, UserMessage = userMessage, data = vm });
                }

                return Json(updatedVM);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<PurchaseOrderController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message });
            }
        }


        [HttpGet]
        public JsonResult GetEmployeeDetailsByEmployeeID(long? employeeID)
        {
            var getEmpDetails = new EmployeeBL(CallContext).GetEmployeeDetailsByEmployeeID(employeeID);

            return Json(getEmpDetails);
        }

    }
}
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Inventory;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Inventory;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Areas.Inventories.Controllers
{
    public class InventoryDetailsController : BaseSearchController
    {
        // GET: Inventories/InventoryDetails
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Infrastructure.Enums.SearchView.InventoryDetails);
            return View(new SearchListViewModel
            {
                ControllerName = "Inventories/" + Infrastructure.Enums.SearchView.InventoryDetails.ToString(),
                ViewName = Infrastructure.Enums.SearchView.InventoryDetails,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                IsEditableLink = false,
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
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList(Services.Contracts.Enums.SearchView.InventoryBranch);

            var filter = "ProductSKUMapIID=" + Convert.ToString(ID);
            filter = filter + " AND companyid = " + CallContext.CompanyID.ToString();

            return PartialView("_ChildList", new SearchListViewModel
            {
                ViewName = Infrastructure.Enums.SearchView.InventoryBranch,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                FilterColumns = metadata.QuickFilterColumns,
                RuntimeFilter = filter,
                IsChild = true,
                ParentViewName = Infrastructure.Enums.SearchView.InventoryDetails,
                IsEditableLink = false
            });
        }


        [HttpGet]
        public JsonResult SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            var view = Infrastructure.Enums.SearchView.InventoryDetails;

            if (runtimeFilter.Trim().IsNotNullOrEmpty())
                view = Infrastructure.Enums.SearchView.InventoryBranch;

            string filter = runtimeFilter;

            if (runtimeFilter.IsNullOrEmpty())
                filter = "ProductSKUMapIID in (Select ProductSKUMapID From[inventory].[ProductInventories] Where BranchID in (";
            else
                filter = filter + " AND BranchID in (";

            var claims = ClientFactory.SecurityServiceClient(CallContext).GetClaimsByType(CallContext.LoginID.Value, Eduegate.Services.Contracts.Enums.ClaimType.Branches);

            foreach (var claim in claims)
            {
                filter = string.Concat(filter, claim.ResourceName, ",");
            }

            filter = string.Concat(filter, "-1)");

            if (runtimeFilter.IsNullOrEmpty())
            {
                filter = string.Concat(filter, ")");
                filter = filter + " AND CompanyID = " + CallContext.CompanyID.ToString();
            }

            return base.SearchData(view, currentPage, orderBy, filter);
        }

        [HttpGet]
        public JsonResult SearchSummaryData()
        {
            return base.SearchSummaryData(Infrastructure.Enums.SearchView.InventoryDetailsSummary);
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            ViewBag.ReportName = "ItemManagement";
            return View();
        }

        public ActionResult Edit(long ID)
        {
            //TODO : we have to do the SKU editing
            var skuDetails = ClientFactory.ProductCatalogServiceClient(CallContext).GetProductAndSKUByID(ID);
            return RedirectToAction("Edit", "Product", new { area = "", ID = skuDetails.ProductIID });
        }

        [HttpPost]
        public JsonResult GetTransactionDetails(TransactionDetailParamViewModel parameter)
        {
            var transaction = ClientFactory.TransactionServiceClient(CallContext).GetTransactionDetails(
                new TransactionSummaryParameterDTO()
                {
                    DocuementTypeID = parameter.DocumentTypeID,
                    DateFrom = parameter.DateFrom,
                    DateTo = parameter.DateTo
                });
            return Json(transaction == null ? new TransactionSummaryDetailDTO() { TransactionCount = 0, Amount = 0 } : transaction);
        }

        [HttpPost]
        public JsonResult GetSupplierTransactionDetails(TransactionDetailParamViewModel parameter)
        {
            var transaction = ClientFactory.TransactionServiceClient(CallContext).GetSupplierTransactionDetails(
                new TransactionSummaryParameterDTO()
                {
                    LoginID = CallContext.LoginID.Value,
                    DocuementTypeID = parameter.DocumentTypeID,
                    DateFrom = parameter.DateFrom,
                    DateTo = parameter.DateTo
                });
            return Json(transaction == null ? new TransactionSummaryDetailDTO() { TransactionCount = 0, Amount = 0 } : transaction);
        }

        [HttpPost]
        public JsonResult UpdateInventory(List<UpdateInventoryViewModel> vms)
        {
            try
            {
                var updatedVM = new List<UpdateInventoryViewModel>();

                if (vms != null)
                {
                    var result = ClientFactory.TransactionServiceClient(CallContext).UpdateProductInventory(UpdateInventoryViewModel.ToDTO(vms, CallContext));
                    updatedVM = UpdateInventoryViewModel.ToVM(result);
                }

                return Json(updatedVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<PurchaseOrderController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetTaxDetails(int documentID)
        {
            try
            {
                var result = ClientFactory.ReferenceDataServiceClient(CallContext).GetTaxTemplateDetails(documentID);
                return Json(TaxDetailsViewModel.ToVM(result, null), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<PurchaseOrderController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetProductSKUInventoryDetail(long skuIID, int? documentReferenceTypeID, long branchID)
        {
            try
            {
                documentReferenceTypeID = documentReferenceTypeID.HasValue ? documentReferenceTypeID.Value : (int)DocumentReferenceTypes.All;
                var result = ClientFactory.ProductCatalogServiceClient(CallContext).GetProductSKUInventoryDetail(skuIID, (DocumentReferenceTypes)documentReferenceTypeID, branchID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<PurchaseOrderController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult GetUnitDataByUnitGroup(int groupID)
        {
            var unitData = ClientFactory.SchoolServiceClient(CallContext).GetUnitDataByUnitGroup(groupID);
            return Json(unitData, JsonRequestBehavior.AllowGet);

        }

    }
}
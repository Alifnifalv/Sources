using Eduegate.ERP.Admin.Controllers;
using Eduegate.ERP.Admin.Helpers;
using Eduegate.Framework.Extensions;
//using Eduegate.Framework.Web.Library.Common;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Common;
using Eduegate.Web.Library.ViewModels.Inventory;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Areas.Inventories.Controllers
{
    public class BranchTransferRequestController : BaseSearchController
    {
        private string dateTimeFormat = ConfigurationExtensions.GetAppConfigValue("DateTimeFormat");

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Infrastructure.Enums.SearchView.BranchTransferRequest);
            return View(new SearchListViewModel
            {
                ControllerName = "Inventories/" + Infrastructure.Enums.SearchView.BranchTransferRequest.ToString(),
                ViewName = Infrastructure.Enums.SearchView.BranchTransferRequest,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                IsCommentLink = true,
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
                ParentViewName = Infrastructure.Enums.SearchView.BranchTransferRequest,
                IsEditableLink = false
            });
        }

        [HttpGet]
        public JsonResult SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            var view = Infrastructure.Enums.SearchView.BranchTransferRequest;
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
            return base.SearchSummaryData(Infrastructure.Enums.SearchView.BranchTransferRequestSummary);
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
            var viewModel = new CRUDMasterDetailViewModel();
            viewModel.Name = "BranchTransferRequest";
            viewModel.ListActionName = "BranchTransferRequest";
            viewModel.ListButtonDisplayName = "Lists";
            viewModel.DisplayName = "Branch Transfer Request";
            viewModel.PrintPreviewReportName = "BranchTransferRequest";
            var vm = new BranchTransferRequestViewModel()
            {
                DetailViewModel = new List<BranchTransferRequestDetailViewModel>() { new BranchTransferRequestDetailViewModel() { } },
                MasterViewModel = new BranchTransferRequestMasterViewModel()
                {
                    TransactionDate = DateTime.Now.ToString(dateTimeFormat),
                    DocumentStatus = new KeyValueViewModel() { Key = "3", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentStatuses.Approved) }
                }
            };

            //viewModel.MasterViewModel = new BranchTransferRequestMasterViewModel() { IsSummaryPanel = false};
            //viewModel.DetailViewModel = new BranchTransferRequestDetailViewModel();
            //viewModel.SummaryViewModel = new SummaryViewModel();
            //viewModel.IID = ID;
            //viewModel.EntityType = Framework.Enums.EntityTypes.Transaction;

            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Branch", Url = "Mutual/GetLookUpData?lookType=Branch" });
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "DocumentType", Url = "Mutual/GetDocumentTypesByID?referenceType=BranchTransferRequest" });
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "DocumentType", Url = "Mutual/GetDocumentTypesByReferenceAndBranch?referenceType=BranchTransferRequest", IsOnInit = false });
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Currency", Url = "Mutual/GetLookUpData?lookType=Currency" });
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Supplier", Url = "Mutual/GetSuppliers" });
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "EntitlementMaster", Url = "Mutual/GetEntitlements?type=Supplier" });
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "DeliveryType", Url = "Mutual/GetDeliveryTypes" });
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "DocumentStatus", Url = "Mutual/GetLookUpData?lookType=BranchTransferRequestDocumentStatus" });
            //TempData["viewModel"] = viewModel;
            //return RedirectToAction("CreateMasterDetail", "CRUD", new { area = "" });
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
            var vm = TransactionViewModel.FromDTOToBranchTransferRequestVM(ClientFactory.TransactionServiceClient(CallContext).GetTransaction(ID, false, false));

            if (vm != null)
                vm.DetailViewModel.Add(new BranchTransferRequestDetailViewModel());

            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(BranchTransferRequestViewModel vm)
        {
            try
            {
                var updatedVM = new BranchTransferRequestViewModel();

                if (vm != null)
                {
                    var result = ClientFactory.ProductDetailServiceClient(CallContext).SaveTransactions(TransactionViewModel.ToDTOFromBranchTransferRequestVM(vm));
                    updatedVM = TransactionViewModel.FromDTOToBranchTransferRequestVM(result);

                    if (updatedVM != null)
                        updatedVM.DetailViewModel.Add(new BranchTransferRequestDetailViewModel());
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

                return Json(updatedVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<BranchTransferRequestController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
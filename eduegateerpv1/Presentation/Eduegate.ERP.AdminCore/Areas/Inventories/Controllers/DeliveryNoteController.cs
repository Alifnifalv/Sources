using Eduegate.ERP.Admin.Controllers;
using Eduegate.ERP.Admin.Helpers;
using Eduegate.Framework.Extensions;
//using Eduegate.Framework.Web.Library.Common;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Warehouses;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Common;
using Eduegate.Web.Library.ViewModels.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Application.Mvc;
using Eduegate.Domain;

namespace Eduegate.ERP.Admin.Areas.Inventories.Controllers
{
    [Area("Inventories")]
    public class DeliveryNoteController : BaseSearchController
    {
        private string dateTimeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateTimeFormat");

        // GET: Inventories/PurchaseOrder
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Infrastructure.Enums.SearchView.DeliveryNote);

            return View(new SearchListViewModel
            {
                ControllerName = "Inventories/" + Infrastructure.Enums.SearchView.DeliveryNote,
                ViewName = Infrastructure.Enums.SearchView.DeliveryNote,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                IsCommentLink = true,
                //IsMultilineEnabled = metadata.MultilineEnabled,
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
                ParentViewName = Infrastructure.Enums.SearchView.DeliveryNote,
                IsEditableLink = false
            });
        }

        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            var view = Infrastructure.Enums.SearchView.DeliveryNote;
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
            return base.SearchSummaryData(Infrastructure.Enums.SearchView.DeliveryNoteSummary);
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View();
        }

        public ActionResult Create(long ID = 0)
        {
            var vm = new DeliveryNoteViewModel()
            {
                DetailViewModel = new List<DeliveryNoteDetailViewModel>() { new DeliveryNoteDetailViewModel() { } },
                MasterViewModel = new DeliveryNoteMasterViewModel()
                {
                    TransactionDate = DateTime.Now.ToString(dateTimeFormat),
                    //Currency = KeyValueViewModel.ToViewModel(ClientFactory.ReferenceDataServiceClient(CallContext).GetDefaultCurrencyWithName()),
                    DocumentStatus = new KeyValueViewModel() { Key = "3", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentStatuses.Approved) }
                }
            };

            //DeliveryNoteMasterViewModel DeliveryNoteViewModel = new DeliveryNoteMasterViewModel();
            //DeliveryNoteViewModel.DocumentReferenceTypeID = Services.Contracts.Enums.DocumentReferenceTypes.DeliveryNote;
            //vm.MasterViewModel = DeliveryNoteViewModel;
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
            var vm = TransactionViewModel.FromDeliveryNoteDTO(ClientFactory.TransactionServiceClient(CallContext).GetTransaction(ID, true, false));
            if (vm != null)
            {
                vm.DetailViewModel.Add(new DeliveryNoteDetailViewModel());
            }
            return Json(vm);
        }

        [HttpPost]
        public JsonResult Save([FromBody] DeliveryNoteViewModel vm)
        {
            try
            {
                var updatedVM = new DeliveryNoteViewModel();
                var userMessage = string.Empty;
                if (vm != null)
                {
                    // ALLOWING TO EDIT TRANSACTION  WHEN STATUS IS NOT COMPLETED
                    vm.MasterViewModel.DocumentReferenceTypeID = Services.Contracts.Enums.DocumentReferenceTypes.GoodsDeliveryNotes;
                    var result = ClientFactory.TransactionServiceClient(CallContext).SaveTransactions(TransactionViewModel.ToDeliveryNoteDTO(vm));
                    updatedVM = TransactionViewModel.FromDeliveryNoteDTO(result);
                }

                if (updatedVM != null)
                {
                    updatedVM.DetailViewModel.Add(new DeliveryNoteDetailViewModel());
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

                // once TransactionStatus completed we should not allow any operation on any transaction..
                if (updatedVM.MasterViewModel.IsTransactionCompleted)
                {
                    return Json(new { IsError = true, UserMessage = "Cannot update the trasaction, it's already processed.", data = vm });
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
        public ActionResult GenerateInvoiceScreenForJob(long ID = 0)
        {
            try
            {
                var viewModel = new CRUDMasterDetailViewModel();
                viewModel.Name = "DeliveryNote";
                viewModel.ListActionName = "DeliveryNote";
                viewModel.ListButtonDisplayName = "Lists";
                viewModel.DisplayName = "Delivery Note";
                viewModel.PrintPreviewReportName = "DeliveryNote";
                viewModel.MasterViewModel = new DeliveryNoteMasterViewModel();
                viewModel.DetailViewModel = new DeliveryNoteDetailViewModel();
                viewModel.SummaryViewModel = new SummaryDetailedViewModel();
                viewModel.IID = 0;
                viewModel.EntityType = Framework.Enums.EntityTypes.Transaction.ToString();

                viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Branch", Url = "Mutual/GetLookUpData?lookType=Branch", CallBack = "BranchChange(null,null);" });
                viewModel.Urls.Add(new UrlViewModel() { LookUpName = "DocumentType", Url = "Mutual/GetDocumentTypesByReferenceAndBranch?referenceType=DeliveryNote", IsOnInit = false });
                viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Currency", Url = "Mutual/GetLookUpData?lookType=Currency" });
                //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Supplier", Url = "Mutual/GetCustomer" });
                viewModel.Urls.Add(new UrlViewModel() { LookUpName = "EntitlementMaster", Url = "Mutual/GetEntitlements?type=Customer" });
                //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Entitlement", Url = "Mutual/GetEntitlements?type=Customer" });
                viewModel.Urls.Add(new UrlViewModel() { LookUpName = "DeliveryType", Url = "Mutual/GetDeliveryTypes" });
                viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Customer", Url = "Customer/GetCustomerByCustomerIdAndCR?searchText=" + null }); // Customers
                viewModel.Urls.Add(new UrlViewModel() { LookUpName = "SalesMan", Url = "Payroll/Employee/GetEmployeesByRoleID?roleID=2" }); // (Sales man)
                //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "SerialList", Url = "Inventory/GetProductInventorySerialMaps?lookupName=SerialList&productSKUMapID=&searchText=" + null });
                viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Areas", Url = "Mutual/GetLookUpData?lookType=Area" });
                viewModel.Urls.Add(new UrlViewModel() { LookUpName = "TransactionStatus", Url = "Mutual/GetLookUpData?lookType=TransactionStatus" });
                viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Country", Url = "Mutual/GetLookUpData?lookType=Country" });
                viewModel.Urls.Add(new UrlViewModel() { LookUpName = "DocumentStatus", Url = "Mutual/GetLookUpData?lookType=DeliveryNoteDOCUMENTREFERENCESTATUSES" });

                // handle edit case 
                // if we have a SI for this JobId then we have to bind it
                var vm = GetTransactionByJobEntryHeadId(ID);


                if (vm.IsNotNull() && vm.MasterViewModel.IsNotNull() && vm.MasterViewModel.TransactionHeadIID > 0)
                {
                    viewModel.Model = new DeliveryNoteViewModel()
                    {
                        DetailViewModel = vm.DetailViewModel,
                        MasterViewModel = vm.MasterViewModel
                    };
                }

                else
                {
                    List<DeliveryNoteDetailViewModel> DeliveryNoteDetails = new List<DeliveryNoteDetailViewModel>();

                    var jobDetail = ClientFactory.WarehouseServiceClient(CallContext).GetJobOperationDetails(ID);

                    if (jobDetail.IsNotNull())
                    {
                        if (jobDetail.Detail.IsNotNull() && jobDetail.Detail.Count > 0)
                        {
                            DeliveryNoteDetailViewModel sidVM = null;

                            foreach (JobOperationDetailDTO jodDTO in jobDetail.Detail)
                            {
                                sidVM = new DeliveryNoteDetailViewModel();
                                sidVM.SKUID = new KeyValueViewModel();
                                sidVM.SKUDetails = null;
                                sidVM.WarrantyDate = null;
                                sidVM.SKUID.Key = jodDTO.ProductSKUID.ToString();
                                sidVM.SKUID.Value = jodDTO.ProductDescription;
                                sidVM.Description = jodDTO.ProductDescription;
                                sidVM.Quantity = Convert.ToInt32(jodDTO.Quantity);
                                sidVM.UnitPrice = Convert.ToDouble(jodDTO.Price);
                                sidVM.Amount = sidVM.Quantity * sidVM.UnitPrice;
                                sidVM.IsSerialNumber = jodDTO.IsSerialNumber;
                                sidVM.PartNo = jodDTO.PartNo;
                                DeliveryNoteDetails.Add(sidVM);
                            }
                        }
                    }

                    var transactionDetail = ClientFactory.TransactionServiceClient(CallContext).GetTransaction(jobDetail.TransactionHeadId.Value, false, false);

                    var DeliveryNoteMasterViewModel = new DeliveryNoteMasterViewModel()
                    {
                        JobEntryHeadID = ID,
                        Branch = new KeyValueViewModel() { Key = jobDetail.BranchID.ToString(), Value = jobDetail.BranchName },
                        BranchID = jobDetail.BranchID,
                        Remarks = jobDetail.Description,
                        TransactionDate = jobDetail.JobDate.IsNotNull() ? Convert.ToDateTime(jobDetail.JobDate).ToString(dateTimeFormat) : null,
                        DeliveryDate = jobDetail.DeliveryDate.IsNotNull() ? Convert.ToDateTime(jobDetail.DeliveryDate).ToString(dateTimeFormat) : null,
                        Customer = new KeyValueViewModel() { Key = jobDetail.Customer.Key, Value = jobDetail.Customer.Value },
                        ReferenceTransactionHeaderID = jobDetail.TransactionHeadId.Value,
                        ReferenceTransactionNo = transactionDetail.TransactionHead.TransactionNo,

                        //DeliveryDetails = transactionDetail.OrderContactMaps.IsNotNull() && transactionDetail.OrderContactMaps.Count > 0 ? DeliveryAddressViewModel.FromOrderContactDTOToVM(transactionDetail.OrderContactMaps.Where(a => a.IsShippingAddress == true).FirstOrDefault()) : null,

                        DocumentStatus = new KeyValueViewModel() { Key = "3", Value = Eduegate.Services.Contracts.Enums.DocumentStatuses.Approved.ToString() },


                        DeliveryMethod = new KeyValueViewModel() { Key = transactionDetail.TransactionHead.DeliveryMethodID.Value.ToString(), Value = transactionDetail.TransactionHead.DeliveryTypeName },
                        //Entitlements = transactionDetail.TransactionHead.Entitlements.Count > 0 ? KeyValueViewModel.FromDTO(transactionDetail.TransactionHead.Entitlements) : null,
                        //Entitlements = new List<KeyValueViewModel>() { new KeyValueViewModel() { Key = transactionDetail.TransactionHead.EntitlementID.Value.ToString(), Value = transactionDetail.TransactionHead.TransactionHeadEntitlementMaps.Where(a => a.EntitlementID == transactionDetail.TransactionHead.EntitlementID.Value).Select(a => a.EntitlementName).FirstOrDefault() } },

                        //Currency = new KeyValueViewModel() { Key = transactionDetail.TransactionHead.CurrencyID.Value.ToString(), Value = transactionDetail.TransactionHead.CurrencyName },
                        DeliveryCharge = transactionDetail.TransactionHead.DeliveryCharge.HasValue ? transactionDetail.TransactionHead.DeliveryCharge.Value : default(decimal),
                        Discount = transactionDetail.TransactionHead.DiscountAmount,
                        DiscountPercentage = transactionDetail.TransactionHead.DiscountPercentage,

                        // Map Transaction Head Entitlement Map
                        TransactionHeadEntitlementMaps = transactionDetail.TransactionHead.TransactionHeadEntitlementMaps.Select(x => TransactionHeadEntitlementMapViewModel.ToVm(x)).ToList(),
                    };

                    // Clearing orderID from OrderContactMap as it is updating existing one.
                    //DeliveryNoteMasterViewModel.DeliveryDetails.OrderContactMapID = 0;
                    //if (transactionDetail.OrderContactMaps.IsNotNull() && transactionDetail.OrderContactMaps.Count > 0)
                    //{
                    //    DeliveryNoteMasterViewModel.DeliveryDetails = DeliveryAddressViewModel.FromOrderContactDTOToVM(transactionDetail.OrderContactMaps.Where(a => a.IsShippingAddress == true).FirstOrDefault());
                    //}

                    viewModel.Model = new DeliveryNoteViewModel()
                    {
                        DetailViewModel = DeliveryNoteDetails,
                        MasterViewModel = DeliveryNoteMasterViewModel,
                    };
                }


                TempData["viewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(viewModel);
                return RedirectToAction("CreateMasterDetail", "CRUD", new { area = "" });
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<DeliveryNoteController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message });
            }
        }


        public DeliveryNoteViewModel GetTransactionByJobEntryHeadId(long jobEntryHeadId)
        {
            var vm = TransactionViewModel.FromDeliveryNoteDTO(ClientFactory.TransactionServiceClient(CallContext).GetTransactionByJobEntryHeadId(jobEntryHeadId));
            if (vm != null)
            {
                vm.DetailViewModel.Add(new DeliveryNoteDetailViewModel());
            }
            return vm;
        }

        [HttpPost]
        public JsonResult SaveTransactions(TransactionViewModel transactionModel)
        {
            bool result = false;

            try
            {
                if (transactionModel != null)
                {
                    var transaction = TransactionViewModel.ToDTO(transactionModel);

                    if (!transactionModel.DocumentTypeID.HasValue)
                    {
                        var settingDetail = ClientFactory.SettingServiceClient(CallContext).GetSettingDetail(transactionModel.SettingCode);

                        if (settingDetail.SettingValue.IsNotNullOrEmpty())
                        {
                            transaction.TransactionHead.DocumentTypeID = Convert.ToInt32(settingDetail.SettingValue);
                        }
                    }

                    if (!transactionModel.BranchID.HasValue)
                    {
                        var branchSettings = ClientFactory.SettingServiceClient(CallContext).GetSettingDetailByCompany("POSBRANCHID", CallContext.CompanyID.Value);

                        if (!string.IsNullOrEmpty(branchSettings.SettingValue))
                            transactionModel.BranchID = long.Parse(branchSettings.SettingValue);
                    }

                    var trans = ClientFactory.TransactionServiceClient(CallContext).SaveTransactions(transaction);
                    var userMessage = "Saved sucessfully.";

                    if (trans.TransactionHead.ErrorCode.IsNotNullOrEmpty())
                    {
                        userMessage = PortalWebHelper.GetErrorMessage(trans.TransactionHead.ErrorCode);
                        return Json(new { IsError = true, Message = userMessage });
                    }

                    return Json(new { IsError = result, Message = userMessage, Transaction = TransactionViewModel.ToVM(trans) });
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<DeliveryNoteController>.Fatal(exception.Message.ToString(), exception);
                return Json(new { IsError = !result, Transaction = transactionModel, UserMessage = exception.Message.ToString() });
            }

            return Json(new { IsError = !result, Transaction = transactionModel, Message = "Transaction Done Successfully", Data = @"" });
        }
    }
}
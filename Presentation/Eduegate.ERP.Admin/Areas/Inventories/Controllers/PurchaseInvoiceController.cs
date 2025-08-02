using Eduegate.ERP.Admin.Controllers;
using Eduegate.ERP.Admin.Helpers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
//using Eduegate.Framework.Web.Library.Common;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Warehouses;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Common;
using Eduegate.Web.Library.ViewModels.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Areas.Inventories.Controllers
{
    public class PurchaseInvoiceController : BaseSearchController
    {
        private string dateTimeFormat = ConfigurationExtensions.GetAppConfigValue("DateTimeFormat");

        [HttpGet]
        // GET: Inventories/PurchaseInvoice
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Infrastructure.Enums.SearchView.PurchaseInvoice);
            return View(new SearchListViewModel
            {
                ControllerName = "Inventories/" + Infrastructure.Enums.SearchView.PurchaseInvoice,
                ViewName = Infrastructure.Enums.SearchView.PurchaseInvoice,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                IsCommentLink = true,
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                InfoBar = @"<li class='status-label-mobile'>
                                        <div class='right status-label'>
                                            <div class='status-label-color'><label class='status-color-label  green'></label>Complete</div>
                                            <div class='status-label-color'><label class='status-color-label red'></label>Fail</div>
                                             <div class='status-label-color'><label class='status-color-label Yellow'></label>Others</div>
                                        </div>
                                    </li>",

                IsChild = false,
                HasChild = true,
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
                ParentViewName = Infrastructure.Enums.SearchView.PurchaseInvoice,
                IsEditableLink = false
            });
        }

        [HttpGet]
        public JsonResult SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            var view = Infrastructure.Enums.SearchView.PurchaseInvoice;
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
            return base.SearchSummaryData(Infrastructure.Enums.SearchView.PurchaseInvoiceSummary);
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
            var vm = new PurchaseInvoiceViewModel()
            {
                DetailViewModel = new List<PurchaseInvoiceDetailViewModel>() { new PurchaseInvoiceDetailViewModel() { } },
                MasterViewModel = new PurchaseInvoiceMasterViewModel()
                {
                    TransactionDate = DateTime.Now.ToString(dateTimeFormat),
                    Validity = DateTime.Now.ToString(dateTimeFormat),
                    //Currency = KeyValueViewModel.ToViewModel(ClientFactory.ReferenceDataServiceClient(CallContext).GetDefaultCurrencyWithName()),
                    DocumentStatus = new KeyValueViewModel() { Key = "1", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentStatuses.Draft) },
                    //Allocations = new BranchWiseAllocationViewModel()
                }
            };

            //var purchaseInvoiceMaterViewModel = new PurchaseInvoiceMasterViewModel();
            //vm.MasterViewModel.Allocations = new BranchWiseAllocationViewModel();
            //vm.MasterViewModel.Allocations.Allocations = new List<QuantityAllocationViewModel>() { new QuantityAllocationViewModel() };
            vm.MasterViewModel.DocumentReferenceTypeID = Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.PurchaseInvoice;
            //vm.MasterViewModel = purchaseInvoiceMaterViewModel;
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(PurchaseInvoiceViewModel vm)
        {
            try
            {
                var updatedVM = new PurchaseInvoiceViewModel();

                if (vm != null)
                {
                    // Deleting empty allocations
                    //vm.MasterViewModel.Allocations.Allocations.ForEach(a => a.AllocatedQuantity.RemoveAll(aq => aq < 1));   

                    bool isError = false;
                    string message = "";
                    foreach (var line in vm.DetailViewModel)
                    {
                        line.IsError = false;
                        if (line.Quantity > 0 && line.SKUID.IsNotNull() && line.SKUDetails.Count > 0)
                        {
                            foreach (var serialDetail in line.SKUDetails)
                            {
                                serialDetail.IsError = false;
                                serialDetail.ErrorMessage = "";

                                if ((!line.IsSerailNumberAutoGenerated) && (serialDetail.SerialNo.IsNullOrEmpty() || serialDetail.SerialNo.Trim().Length < 1) && line.IsSerialNumberOnPurchase)
                                {
                                    isError = true;
                                    // error in line
                                    line.IsError = true;

                                    // error in serial number
                                    serialDetail.IsError = true;
                                    serialDetail.ErrorMessage = "Required";
                                }
                                else if (line.ProductLength.IsNotNull() && line.ProductLength != 0 && line.ProductTypeName == Framework.Enums.ProductTypes.Digital.ToString() && serialDetail.SerialNo.Trim().Length != line.ProductLength)
                                {

                                    isError = true;
                                    // error in line
                                    line.IsError = true;

                                    // error in serial number
                                    serialDetail.IsError = true;
                                    serialDetail.ErrorMessage = "Please enter " + line.ProductLength + " character";
                                }
                                if (!isError)
                                    SerialNumberCheck(vm, false, ref isError, ref message);
                                if (isError)
                                {
                                    isError = true;
                                    line.IsError = true;
                                    serialDetail.IsError = true;
                                    serialDetail.ErrorMessage = message;
                                }
                            }
                        }
                    }

                    if (isError)
                    {
                        return Json(new { IsError = true, UserMessage = "Error with serial numbers!", data = vm }, JsonRequestBehavior.AllowGet);
                    }

                    if (vm.DetailViewModel.Count > 0)
                    {
                        foreach (var x in vm.DetailViewModel)
                            if(x.SKUID != null)
                            {
                                if(x.SKUID.Key != null && (x.Unit.Key == null || x.Fraction == 0))
                                {
                                    return Json(new { IsError = true, UserMessage = "Unit or Fraction is not found for " + x.Description, data = vm }, JsonRequestBehavior.AllowGet);
                                }
                            }
                    }

                    vm.MasterViewModel.DocumentReferenceTypeID = Services.Contracts.Enums.DocumentReferenceTypes.PurchaseInvoice;
                    var result = ClientFactory.TransactionServiceClient(CallContext).SaveTransactions(PurchaseInvoiceViewModel.FromVMToTransactionDTO(vm));

                    var userMessage = string.Empty;
                    //Save & map into Result Additional Expense Trans Map 

                    if (vm.MasterViewModel.AdditionalExpTransMaps.Count > 0)
                    {
                        var DTOData = FromVMToAdditionalExpenseTransDTO(vm.MasterViewModel.AdditionalExpTransMaps, int.Parse(vm.MasterViewModel.Currency.Key));
                        if (!updatedVM.MasterViewModel.IsTransactionCompleted)
                        {
                            ClientFactory.AccountingTransactionServiceClient(CallContext).AdditionalExpensesTransactionsMap(DTOData, 0, result.TransactionHead.HeadIID, short.Parse(vm.MasterViewModel.DocumentStatus.Key));
                        }
                        result.TransactionHead.AdditionalExpensesTransactionsMaps = DTOData;

                    }

                    if (result.ErrorCode.IsNotNullOrEmpty())
                    {
                        // Fill vm with proper error messages coming up with updated vm
                        updatedVM = PurchaseInvoiceViewModel.FromTransactionDTOToVM(result);

                        if (updatedVM.DetailViewModel.IsNotNull() && updatedVM.DetailViewModel.Count > 0)
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
                        }
                        userMessage = PortalWebHelper.GetErrorMessage(result.ErrorCode);
                        return Json(new { IsError = true, UserMessage = userMessage, data = vm }, JsonRequestBehavior.AllowGet);
                    }
                    updatedVM = PurchaseInvoiceViewModel.FromTransactionDTOToVM(result);
                    updatedVM.DetailViewModel.Add(new PurchaseInvoiceDetailViewModel());

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
                Eduegate.Logger.LogHelper<PurchaseInvoiceController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public List<AdditionalExpensesTransactionsMapDTO> FromVMToAdditionalExpenseTransDTO(List<AdditionalExpTransMapViewModel> additionalExpTransMaps, int currencyID)
        {
            var lstAdditionalExpnceList = new List<AdditionalExpensesTransactionsMapDTO>();
            // Map AdditionalExpTransMaps
            if (additionalExpTransMaps.IsNotNull() && additionalExpTransMaps.Count > 0)
            {
                foreach (var item in additionalExpTransMaps)
                {
                    if (item.AdditionalExpense.IsNotNull() && !string.IsNullOrWhiteSpace(item.AdditionalExpense.Key))
                    {
                        var additionalExpensesTransactionsMapDTO = new AdditionalExpensesTransactionsMapDTO();

                        additionalExpensesTransactionsMapDTO.AdditionalExpenseID = item.AdditionalExpense != null ? Convert.ToInt32(item.AdditionalExpense.Key) : default(Int32);
                        additionalExpensesTransactionsMapDTO.ProvisionalAccountID = item.ProvisionalAccount != null ? Convert.ToInt64(item.ProvisionalAccount.Key) : default(Int64);
                        additionalExpensesTransactionsMapDTO.ForeignCurrencyID = item.Currency != null ? Convert.ToInt32(item.Currency.Key) : default(Int32);
                        additionalExpensesTransactionsMapDTO.LocalCurrencyID = currencyID;
                        additionalExpensesTransactionsMapDTO.Currency = item.Currency != null ? item.Currency.Value : null;
                        additionalExpensesTransactionsMapDTO.AdditionalExpense = item.AdditionalExpense != null ? item.AdditionalExpense.Value : null;
                        additionalExpensesTransactionsMapDTO.ProvisionalAccount = item.ProvisionalAccount != null ? item.ProvisionalAccount.Value : null;
                        additionalExpensesTransactionsMapDTO.ExchangeRate = item.ExchangeRate;
                        additionalExpensesTransactionsMapDTO.ForeignAmount = item.ForeignAmount;
                        additionalExpensesTransactionsMapDTO.LocalAmount = item.LocalAmount;
                        additionalExpensesTransactionsMapDTO.FiscalYearID = item.FiscalYearID;
                        additionalExpensesTransactionsMapDTO.AccountTransactionHeadID = item.AccountTransactionHeadID;
                        additionalExpensesTransactionsMapDTO.RefInventoryTransactionHeadID = item.RefInventoryTransactionHeadID;
                        additionalExpensesTransactionsMapDTO.RefAccountTransactionHeadID = item.RefAccountTransactionHeadID;
                        additionalExpensesTransactionsMapDTO.ISAffectSupplier = item.IsAffectSupplier.HasValue ? item.IsAffectSupplier.Value : false;

                        // add into AdditionalExpensesTransactionsMaps list
                        lstAdditionalExpnceList.Add(additionalExpensesTransactionsMapDTO);
                    }
                }
            }
            return lstAdditionalExpnceList;
        }

        [HttpPost]
        public JsonResult SerialKeyDuplicationCheck(PurchaseInvoiceViewModel vm)
        {
            var message = "";
            bool isError = false;
            SerialNumberCheck(vm, true, ref isError, ref message);
            return Json(new { IsError = isError, ErrorMessage = message }, JsonRequestBehavior.AllowGet);
        }

        private void SerialNumberCheck(PurchaseInvoiceViewModel vm, bool isKeyCheck, ref bool isError, ref string message)
        {
            isError = false;
            message = "";
            if (vm != null)
            {
                foreach (var line in vm.DetailViewModel)
                {
                    if (line.Quantity > 0 && line.SKUID.IsNotNull() && line.SKUDetails.Count > 0)
                    {
                        foreach (var serialDetail in line.SKUDetails.Where(x => x.SerialNo.IsNotNullOrEmpty()))
                        {
                            if ((!line.IsSerailNumberAutoGenerated) && (line.SKUDetails.Where(l => l.SerialNo == serialDetail.SerialNo).Count() > 1) && line.IsSerialNumberOnPurchase)
                            {
                                // Check for duplicate in same line
                                isError = true;
                                line.IsError = true;
                                message = "Serial number duplicated";
                            }

                            // Check for duplicate in other lines, if current line is not augenerated
                            if ((!line.IsSerailNumberAutoGenerated) && serialDetail.SerialNo.IsNotNull() && line.IsSerialNumberOnPurchase)
                            {
                                foreach (var line1 in vm.DetailViewModel)
                                {
                                    if (line1.SKUID != line.SKUID)
                                    {
                                        if (line1.SKUDetails.Any(l => l.SerialNo == serialDetail.SerialNo))
                                        {
                                            isError = true;
                                            line.IsError = true;
                                            message = "Serial number duplicated in other Sku's";
                                        }
                                    }
                                }
                            }
                            if (!isError && isKeyCheck)
                            {
                                var hash = ClientFactory.SettingServiceClient(CallContext).GetSettingDetail("SERIALPASSPHRASE");
                                isError = ClientFactory.TransactionServiceClient(CallContext).ProductSerialCountCheck(hash.SettingValue, serialDetail.ProductSKUMapID, serialDetail.SerialNo, serialDetail.ProductSerialID);
                                if (isError) message = "Serial key already updated in system";
                            }

                            if (isError) break;
                        }

                    }
                    if (isError) break;
                }
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
                var transactionDTO = ClientFactory.TransactionServiceClient(CallContext).GetTransaction(ID, false, false);
                transactionDTO.TransactionHead.AdditionalExpensesTransactionsMaps = ClientFactory.AccountingTransactionServiceClient(CallContext).GetAdditionalExpensesTransactions(null, 0, transactionDTO.TransactionHead.HeadIID);
                var vm = PurchaseInvoiceViewModel.FromTransactionDTOToVM(transactionDTO); ;

                if (vm != null)
                {
                    vm.DetailViewModel.Add(new PurchaseInvoiceDetailViewModel());
                }

                return Json(vm, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<PurchaseInvoiceController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GenerateInvoiceScreenForJob(long ID = 0)
        {
            try
            {
                var viewModel = new CRUDMasterDetailViewModel();
                viewModel.Name = "PurchaseInvoice";
                viewModel.ListActionName = "PurchaseInvoice";
                viewModel.ListButtonDisplayName = "Lists";
                viewModel.DisplayName = "Purchase Invoice";
                viewModel.PrintPreviewReportName = "PurchaseInvoicePreview";
                var purchaseInvoiceDetails = new List<PurchaseInvoiceDetailViewModel>();
                var allocationList = new List<QuantityAllocationViewModel>();
                viewModel.MasterViewModel = new PurchaseInvoiceMasterViewModel();
                viewModel.DetailViewModel = new PurchaseInvoiceDetailViewModel();
                viewModel.SummaryViewModel = new SummaryDetailedViewModel();
                viewModel.IID = 0;
                viewModel.EntityType = Framework.Enums.EntityTypes.Transaction.ToString();
                var purchaseInvoiceMaterViewModel = new PurchaseInvoiceMasterViewModel();
                //purchaseInvoiceMaterViewModel.Allocations = new BranchWiseAllocationViewModel();
                //purchaseInvoiceMaterViewModel.Allocations.Allocations = new List<QuantityAllocationViewModel>() { new QuantityAllocationViewModel() };


                var vm = GetTransactionByJobEntryHeadId(ID);


                if (vm.IsNotNull() && vm.MasterViewModel.IsNotNull() && vm.MasterViewModel.TransactionHeadIID > 0)
                {
                    viewModel.Model = new PurchaseInvoiceViewModel()
                    {
                        DetailViewModel = vm.DetailViewModel,
                        MasterViewModel = vm.MasterViewModel
                    };
                }
                else
                {
                    var jobDetail = ClientFactory.WarehouseServiceClient(CallContext).GetJobOperationDetails(ID);
                    var branches = ClientFactory.ReferenceDataServiceClient(CallContext).GetLookUpData(Eduegate.Services.Contracts.Enums.LookUpTypes.Branch, string.Empty, 0, 0);
                    branches.Insert(0, new KeyValueDTO() { Key = null, Value = string.Empty });

                    if (jobDetail.IsNotNull())
                    {
                        if (jobDetail.Detail.IsNotNull() && jobDetail.Detail.Count > 0)
                        {
                            foreach (JobOperationDetailDTO jodDTO in jobDetail.Detail)
                            {
                                var vmPI = new PurchaseInvoiceDetailViewModel();
                                vmPI.SKUDetails = null;
                                var allocationVM = new QuantityAllocationViewModel();
                                vmPI.SKUID = new KeyValueViewModel();

                                vmPI.SKUID.Key = jodDTO.ProductSKUID.ToString();
                                vmPI.SKUID.Value = jodDTO.ProductDescription;
                                vmPI.Description = jodDTO.ProductDescription;
                                vmPI.Quantity = jodDTO.Quantity.Value;
                                vmPI.UnitPrice =(jodDTO.Price);
                                vmPI.Amount = (vmPI.Quantity * vmPI.UnitPrice);
                                vmPI.IsSerialNumberOnPurchase = jodDTO.IsSerialNumberOnPurchase;

                                allocationVM.ProductID = Convert.ToInt32(jodDTO.ProductSKUID);
                                allocationVM.ProductName = jodDTO.ProductDescription;
                                allocationVM.Quantity = Convert.ToInt32(jodDTO.Quantity);
                                allocationVM.BranchIDs = new List<long>();
                                allocationVM.BranchName = new List<string>();
                                allocationVM.AllocatedQuantity = new List<decimal>();

                                foreach (var branch in branches)
                                {
                                    allocationVM.BranchIDs.Add(Convert.ToInt32(branch.Key));
                                    allocationVM.BranchName.Add(branch.Value);
                                    allocationVM.AllocatedQuantity.Add(0);
                                }

                                purchaseInvoiceDetails.Add(vmPI);
                                allocationList.Add(allocationVM);
                            }
                        }

                        var branchWiseAllocation = new BranchWiseAllocationViewModel();
                        branchWiseAllocation.Allocations = allocationList;

                        var transactionDetail = ClientFactory.TransactionServiceClient(CallContext).GetTransaction(jobDetail.TransactionHeadId.Value, false, false);

                        var purchaseInvoiceMasterViewModel = new PurchaseInvoiceMasterViewModel()
                        {
                            JobEntryHeadID = ID,
                            Branch = new KeyValueViewModel() { Key = jobDetail.BranchID.ToString(), Value = jobDetail.BranchName },
                            BranchID = jobDetail.BranchID,
                            Remarks = transactionDetail.TransactionHead.Description,
                            Reference = transactionDetail.TransactionHead.Reference,
                            TransactionDate = jobDetail.JobDate.IsNotNull() ? Convert.ToDateTime(jobDetail.JobDate).ToString(dateTimeFormat) : null,
                            ReferenceTransactionHeaderID = jobDetail.TransactionHeadId.Value.ToString(),
                            ReferenceTransactionNo = transactionDetail.TransactionHead.TransactionNo,
                            DeliveryMethod = transactionDetail.TransactionHead.DeliveryMethodID.IsNotNull() ? new KeyValueViewModel() { Key = transactionDetail.TransactionHead.DeliveryMethodID.Value.ToString(), Value = transactionDetail.TransactionHead.DeliveryTypeName } : new KeyValueViewModel(),
                            //Currency = new KeyValueViewModel() { Key = transactionDetail.TransactionHead.CurrencyID.Value.ToString(), Value = transactionDetail.TransactionHead.CurrencyName },
                            // Map Transaction Head Entitlement Map
                            TransactionHeadEntitlementMaps = transactionDetail.TransactionHead.TransactionHeadEntitlementMaps.Select(x => TransactionHeadEntitlementMapViewModel.ToVm(x)).ToList(),
                            //Allocations = branchWiseAllocation,
                            Supplier = new KeyValueViewModel() { Key = transactionDetail.TransactionHead.SupplierID.ToString(), Value = transactionDetail.TransactionHead.SupplierName },
                        };


                        viewModel.Model = new PurchaseInvoiceViewModel()
                        {
                            DetailViewModel = purchaseInvoiceDetails,
                            MasterViewModel = purchaseInvoiceMasterViewModel,
                        };
                    }
                }

                viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Branch", Url = "Mutual/GetLookUpData?lookType=Branch" });
                viewModel.Urls.Add(new UrlViewModel() { LookUpName = "DocumentType", Url = "Mutual/GetDocumentTypesByReferenceAndBranch?referenceType=PurchaseInvoice", IsOnInit = false });
                viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Currency", Url = "Mutual/GetLookUpData?lookType=Currency" });
                viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Supplier", Url = "Mutual/GetSuppliers" });
                viewModel.Urls.Add(new UrlViewModel() { LookUpName = "EntitlementMaster", Url = "Mutual/GetEntitlements?type=Supplier" });
                //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Entitlement", Url = "Mutual/GetEntitlements?type=Supplier" });
                viewModel.Urls.Add(new UrlViewModel() { LookUpName = "DeliveryType", Url = "Mutual/GetDeliveryTypes" });
                viewModel.Urls.Add(new UrlViewModel() { LookUpName = "DocumentStatus", Url = "Mutual/GetLookUpData?lookType=PURCHASEINVOICEDOCUMENTREFERENCESTATUSES" });
                TempData["viewModel"] = viewModel;
                return RedirectToAction("CreateMasterDetail", "CRUD", new { area = "" });
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<SalesInvoiceController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public PurchaseInvoiceViewModel GetTransactionByJobEntryHeadId(long jobEntryHeadId)
        {
            var vm = PurchaseInvoiceViewModel.FromTransactionDTOToVM(ClientFactory.TransactionServiceClient(CallContext).GetTransactionByJobEntryHeadId(jobEntryHeadId));
            if (vm != null)
            {
                vm.DetailViewModel.Add(new PurchaseInvoiceDetailViewModel());
            }
            return vm;
        }

    }
}
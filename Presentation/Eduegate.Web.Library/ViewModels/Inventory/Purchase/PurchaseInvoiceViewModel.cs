using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Inventory;
using System.Globalization;
using Eduegate.Services.Contracts.Accounts.Taxes;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    public class PurchaseInvoiceViewModel : BaseMasterViewModel
    {
        public PurchaseInvoiceViewModel()
        {
            MasterViewModel = new PurchaseInvoiceMasterViewModel();
            DetailViewModel = new List<PurchaseInvoiceDetailViewModel>() { new PurchaseInvoiceDetailViewModel() };
        }

        public PurchaseInvoiceMasterViewModel MasterViewModel { get; set; }
        public List<PurchaseInvoiceDetailViewModel> DetailViewModel { get; set; }

        /* Below mapper is returnig null data.Need to work on this
        
        public static TransactionDTO FromVMToTransactionDTO(PurchaseInvoiceViewModel vm)
        {
            Mapper<PurchaseInvoiceViewModel, TransactionDTO>.CreateMap();
            Mapper<PurchaseInvoiceMasterViewModel, TransactionHeadDTO>.CreateMap();
            Mapper<PurchaseInvoiceDetailViewModel, TransactionDetailDTO>.CreateMap();
            return Mapper<PurchaseInvoiceViewModel, TransactionDTO>.Map(vm);
        }
         */

        public static TransactionDTO FromVMToTransactionDTO(PurchaseInvoiceViewModel vm)
        {
            if (vm != null)
            {
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
                var transaction = new TransactionDTO();
                transaction.TransactionHead = new TransactionHeadDTO();

                //Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
                //Mapper<PurchaseInvoiceMasterViewModel, TransactionHeadDTO>.CreateMap();
                //Mapper<TransactionHeadEntitlementMapViewModel, TransactionHeadEntitlementMapDTO>.CreateMap();

                //transaction.TransactionHead = new TransactionHeadDTO();

                //if (vm.MasterViewModel.Branch.Key == null)
                //{
                //    throw new Exception("Select Branch!");
                //}

                //if (vm.MasterViewModel.DocumentType.Key == null)
                //{
                //    throw new Exception("Select Document Type!");
                //}

                if (vm.MasterViewModel.Currency.Key == null)
                {
                    throw new Exception("Select Any Currency!");
                }

                if (vm.MasterViewModel.Supplier == null || vm.MasterViewModel.Supplier.Key == null)
                {
                    throw new Exception("Select Supplier!");
                }

               
                if (vm.DetailViewModel.Sum(y => y.Amount) == 0)
                {
                    throw new Exception("Please enter product details");
                }

                if (vm.DetailViewModel.FindAll(x => (x.Amount == 0) && !(x.SKUID == null || x.SKUID.Key == null)).Count() > 0)
                    throw new Exception("Please enter amount for the selected product !");

                if (vm.DetailViewModel.FindAll(x => (x.Unit == null || x.Unit.Key == null) && !(x.SKUID == null || x.SKUID.Key == null)).Count() > 0)
                {
                    throw new Exception("Please select unit for the selected product !");
                }

                if (vm.MasterViewModel.TransactionHeadEntitlementMaps.Sum(x => x.Amount ?? 0) != (vm.DetailViewModel.Sum(y => y.Amount != null ? y.Amount : 0) - (vm.MasterViewModel.Discount != null ? vm.MasterViewModel.Discount : 0)))
                {
                    throw new Exception(" Payment Amount should be equal to the grand total!");
                }

                if (vm.MasterViewModel.AdditionalExpTransMaps.FindAll(x => x.LocalAmount > 0 && (x.AdditionalExpense == null || x.AdditionalExpense.Key == null || x.Currency == null || x.Currency.Key == null || x.ProvisionalAccount == null || x.ProvisionalAccount.Key == null)).Count() > 0)
                    throw new Exception("AdditionalExpense,ProvisionalAccount and Currency should not be empty for non zero amount!");

                if (vm.MasterViewModel.AdditionalExpTransMaps.FindAll(x => (!x.LocalAmount.HasValue || x.LocalAmount == 0) && !(x.AdditionalExpense == null || x.AdditionalExpense.Key == null) && (!(x.ProvisionalAccount == null || x.ProvisionalAccount.Key == null) || !(x.Currency == null || x.Currency.Key == null))).Count() > 0)
                    throw new Exception("Please enter amount for the selected Additional Expense !");

                transaction.TransactionHead.HeadIID = vm.MasterViewModel.TransactionHeadIID;
                transaction.TransactionHead.CompanyID = vm.MasterViewModel.CompanyID;
                transaction.TransactionHead.DocumentTypeID = Convert.ToInt32(vm.MasterViewModel.DocumentType.Key);
                transaction.TransactionHead.BranchID = vm.MasterViewModel.Branch == null ? (long?)null : Convert.ToInt32(vm.MasterViewModel.Branch.Key);
                transaction.TransactionHead.TransactionNo = vm.MasterViewModel.TransactionNo;
                transaction.TransactionHead.TransactionDate = vm.MasterViewModel.TransactionDate != null ? DateTime.ParseExact(vm.MasterViewModel.TransactionDate, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                transaction.TransactionHead.DueDate = vm.MasterViewModel.Validity != null ? DateTime.ParseExact(vm.MasterViewModel.Validity, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                transaction.TransactionHead.Description = vm.MasterViewModel.Remarks;
                transaction.TransactionHead.Reference = vm.MasterViewModel.Reference;
                transaction.TransactionHead.CurrencyID = vm.MasterViewModel.Currency != null ? Convert.ToInt32(vm.MasterViewModel.Currency.Key) : (int?)null;
                transaction.TransactionHead.SupplierID = vm.MasterViewModel.Supplier != null && vm.MasterViewModel.Supplier.Key != null ? Convert.ToInt32(vm.MasterViewModel.Supplier.Key) : (long?)null;
                //transaction.TransactionHead.DeliveryTypeID = Convert.ToInt32(vm.MasterViewModel.DeliveryType);
                transaction.TransactionHead.IsShipment = vm.MasterViewModel.IsShipment;
                //transaction.TransactionHead.EntitlementID = Convert.ToInt16(vm.MasterViewModel.Entitlement);
                transaction.TransactionHead.ReferenceHeadID = !string.IsNullOrEmpty(vm.MasterViewModel.ReferenceTransactionHeaderID) ? Convert.ToInt32(vm.MasterViewModel.ReferenceTransactionHeaderID) : (long?)null;
                transaction.TransactionHead.JobEntryHeadID = vm.MasterViewModel.JobEntryHeadID != default(long) ? vm.MasterViewModel.JobEntryHeadID : (long?)null;
                transaction.TransactionHead.DocumentReferenceTypeID = vm.MasterViewModel.DocumentReferenceTypeID;
                transaction.TransactionHead.CreatedBy = vm.CreatedBy;
                transaction.TransactionHead.UpdatedBy = vm.UpdatedBy;
                transaction.TransactionHead.TransactionStatusID = Convert.ToByte(vm.MasterViewModel.TransactionStatus.Key);
                //transaction.TransactionHead.DeliveryMethodID = vm.MasterViewModel.DeliveryMethod != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DeliveryMethod.Key) ? Convert.ToByte(vm.MasterViewModel.DeliveryMethod.Key) : (short?)null : (short?)null;

                transaction.TransactionHead.TransactionStatusID = vm.MasterViewModel.TransactionStatus != null ? !string.IsNullOrEmpty(vm.MasterViewModel.TransactionStatus.Key) ? byte.Parse(vm.MasterViewModel.TransactionStatus.Key) : (byte?)null : (byte?)null;

                //transaction.TransactionHead.Entitlements = vm.MasterViewModel.Entitlements.Count > 0 ? KeyValueViewModel.ToDTO(vm.MasterViewModel.Entitlements) : null;

                //transaction.TransactionHead.EntitlementID = vm.MasterViewModel.Entitlements.Count > 0 ? Convert.ToInt16(vm.MasterViewModel.Entitlements[0].Key) : (short?)null;

                transaction.TransactionHead.DocumentStatusID = vm.MasterViewModel.DocumentStatus != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DocumentStatus.Key) ? short.Parse(vm.MasterViewModel.DocumentStatus.Key) : (short?)null : (short?)null;
                transaction.TransactionHead.DeliveryCharge = vm.MasterViewModel.DeliveryCharge;
                transaction.TransactionHead.DiscountAmount = vm.MasterViewModel.Discount;
                transaction.TransactionHead.DiscountPercentage = vm.MasterViewModel.DiscountPercentage;
                transaction.TransactionHead.ExchangeRate = vm.MasterViewModel.ExchangeRate;
                transaction.TransactionHead.ForeignAmount = vm.MasterViewModel.ForeignInvoiceAmount;
                transaction.TransactionHead.InvoiceForeignAmount = vm.MasterViewModel.ForeignInvoiceAmount;
                transaction.TransactionHead.InvoiceLocalAmount = vm.MasterViewModel.LocalInvoiceAmount;

                // Map TransactionHeadEntitlementMaps
                if (vm.MasterViewModel.TransactionHeadEntitlementMaps.IsNotNull() && vm.MasterViewModel.TransactionHeadEntitlementMaps.Count > 0)
                {
                    transaction.TransactionHead.TransactionHeadEntitlementMaps = new List<TransactionHeadEntitlementMapDTO>();

                    foreach (var item in vm.MasterViewModel.TransactionHeadEntitlementMaps)
                    {
                        if (item.Entitlement.IsNotNull() && !string.IsNullOrWhiteSpace(item.Entitlement.Key))
                        {
                            var transactionHeadEntitlementMapDTO = new TransactionHeadEntitlementMapDTO();
                            transactionHeadEntitlementMapDTO.TransactionHeadEntitlementMapID = item.TransactionHeadEntitlementMapID;
                            transactionHeadEntitlementMapDTO.TransactionHeadID = vm.MasterViewModel.TransactionHeadIID;
                            transactionHeadEntitlementMapDTO.EntitlementID = item.Entitlement != null ? Convert.ToByte(item.Entitlement.Key) : default(byte);
                            transactionHeadEntitlementMapDTO.EntitlementName = item.Entitlement != null ? item.Entitlement.Value : null;
                            transactionHeadEntitlementMapDTO.Amount = item.Amount;

                            // add into TransactionHeadEntitlementMaps list
                            transaction.TransactionHead.TransactionHeadEntitlementMaps.Add(transactionHeadEntitlementMapDTO);
                        }
                    }
                }


                if (vm.MasterViewModel.TaxDetails != null && vm.MasterViewModel.TaxDetails.Taxes.Count > 0)
                {
                    transaction.TransactionHead.TaxDetails = new List<TaxDetailsDTO>();
                    transaction.TransactionHead.TaxDetails = vm.MasterViewModel.TaxDetails.Taxes.Select(x => TaxDetailsViewModel.ToDTO(x)).ToList();
                }

                transaction.TransactionDetails = new List<TransactionDetailDTO>();
                if (vm.DetailViewModel != null && vm.DetailViewModel.Count > 0)
                {
                    foreach (var transactionDetail in vm.DetailViewModel)
                    {
                        if (transactionDetail.SKUID != null && transactionDetail.SKUID.Key != null && transactionDetail.Quantity == 0)
                        {
                            throw new Exception("Quantity should not be zero for " + transactionDetail.Description);
                        }

                        if (transactionDetail.SKUID != null && transactionDetail.SKUID.Key != null && transactionDetail.ForeignRate == 0 || transactionDetail.ForeignRate == null)
                        {
                            throw new Exception("ForeignRate should not be zero for " + transactionDetail.Description);
                        }

                        if (transactionDetail.SKUID != null && transactionDetail.SKUID.Key != null)
                        {
                            var transactionDetailDTO = new TransactionDetailDTO();

                            transactionDetailDTO.DetailIID = transactionDetail.TransactionDetailID;
                            transactionDetailDTO.HeadID = transactionDetail.TransactionHead;
                            transactionDetailDTO.ProductSKUMapID = Convert.ToInt32(transactionDetail.SKUID.Key);
                            transactionDetailDTO.Quantity = Convert.ToDecimal(transactionDetail.Quantity);
                            transactionDetailDTO.Amount = Convert.ToDecimal(transactionDetail.Amount);
                            transactionDetailDTO.UnitPrice = Convert.ToDecimal(transactionDetail.UnitPrice);
                            //transactionDetailDTO.UnitID = currently hard coding it to 1
                            transactionDetailDTO.UnitID = transactionDetail.UnitID;
                            transactionDetailDTO.CreatedBy = transactionDetail.CreatedBy;
                            transactionDetailDTO.UpdatedBy = transactionDetail.UpdatedBy;
                            transactionDetailDTO.TaxPercentage = transactionDetail.TaxPercentage;
                            transactionDetailDTO.TaxTemplateID = string.IsNullOrEmpty(transactionDetail.TaxTemplate) ? (int?)null : int.Parse(transactionDetail.TaxTemplate);
                            transactionDetailDTO.HasTaxInclusive = transactionDetail.HasTaxInclusive;
                            transactionDetailDTO.InclusiveTaxAmount = transactionDetail.InclusiveTaxAmount;
                            transactionDetailDTO.ExclusiveTaxAmount = transactionDetail.ExclusiveTaxAmount;
                            //transactionDetailDTO.CostCenterID = transactionDetail.CostCenter != null ? !string.IsNullOrEmpty(transactionDetail.CostCenter.Key) ? Convert.ToInt32(transactionDetail.CostCenter.Key) : (int?)null : (int?)null;
                            transactionDetailDTO.WarrantyStartDate = transactionDetail.WarrantyStartDateString != null ? DateTime.ParseExact(transactionDetail.WarrantyStartDateString, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                            transactionDetailDTO.WarrantyEndDate = transactionDetail.WarrantyEndDateString != null ? DateTime.ParseExact(transactionDetail.WarrantyEndDateString, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;

                            if (transactionDetail.SKUDetails != null && transactionDetail.SKUDetails.Count > 0)
                            {
                                Mapper<ProductSKUDetailsViewModel, ProductSerialMapDTO>.CreateMap();
                                var dto = Mapper<ProductSKUDetailsViewModel, ProductSerialMapDTO>.Map(transactionDetail.SKUDetails);
                                transactionDetailDTO.SKUDetails = dto.ToList();
                            }
                            transactionDetailDTO.LandingCost = Convert.ToDecimal(transactionDetail.LandingCost);
                            transactionDetailDTO.LastCostPrice = Convert.ToDecimal(transactionDetail.LastCostPrice);
                            transactionDetailDTO.Fraction = Convert.ToDecimal(transactionDetail.Fraction);
                            transactionDetailDTO.ForeignAmount = Convert.ToDecimal(transactionDetail.ForeignAmount);
                            transactionDetailDTO.ForeignRate = Convert.ToDecimal(transactionDetail.ForeignRate);
                            transactionDetailDTO.ExchangeRate = Convert.ToDecimal(transactionDetail.ExchangeRate);
                            transactionDetailDTO.UnitGroupID = transactionDetail.UnitGroupID;

                            //if (vm.MasterViewModel.Allocations.Allocations != null && vm.MasterViewModel.Allocations.Allocations.Count > 0)
                            //{
                            //    QuantityAllocationViewModel allocationToMap = vm.MasterViewModel.Allocations.Allocations.Where(x => x.ProductID == transactionDetailDTO.ProductSKUMapID).FirstOrDefault();
                            //    transactionDetailDTO.TransactionAllocations = new List<TransactionAllocationDTO>();
                            //    if (allocationToMap != null && allocationToMap.BranchIDs != null)
                            //    {
                            //        for (int i = 0; i < allocationToMap.BranchIDs.Count; i++)
                            //        {

                            //            if (allocationToMap.AllocatedQuantity[i] > 0)
                            //            {
                            //                var transactionAllocation = new TransactionAllocationDTO();
                            //                transactionAllocation.TrasactionDetailID = transactionDetail.TransactionDetailID;
                            //                transactionAllocation.BranchID = allocationToMap.BranchIDs[i];
                            //                transactionAllocation.Quantity = allocationToMap.AllocatedQuantity[i];
                            //                transactionDetailDTO.TransactionAllocations.Add(transactionAllocation); 
                            //            } 

                            //        }
                            //    }
                            //}

                            transaction.TransactionDetails.Add(transactionDetailDTO);
                        }
                    }
                }

                if (transaction.TransactionDetails.Count == 0)
                {
                    throw new Exception("Select atleast one product!");
                }

                return transaction;
            }
            else return new TransactionDTO();
        }

        public static PurchaseInvoiceViewModel FromTransactionDTOToVM(TransactionDTO dto)
        {
            if (dto != null)
            {
                var purchaseInVoice = new PurchaseInvoiceViewModel();
                purchaseInVoice.MasterViewModel = new PurchaseInvoiceMasterViewModel();
                purchaseInVoice.DetailViewModel = new List<PurchaseInvoiceDetailViewModel>();
                //purchaseInVoice.MasterViewModel.Allocations = new BranchWiseAllocationViewModel();
                //purchaseInVoice.MasterViewModel.Allocations.Allocations = new List<QuantityAllocationViewModel>();

                Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
                Mapper<TransactionHeadDTO, PurchaseInvoiceMasterViewModel>.CreateMap();
                Mapper<TransactionHeadEntitlementMapDTO, TransactionHeadEntitlementMapViewModel>.CreateMap();
                purchaseInVoice.MasterViewModel = Mapper<TransactionHeadDTO, PurchaseInvoiceMasterViewModel>.Map(dto.TransactionHead);
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                purchaseInVoice.MasterViewModel.IsError = dto.TransactionHead.IsError;
                purchaseInVoice.MasterViewModel.ErrorCode = dto.TransactionHead.ErrorCode;
                purchaseInVoice.MasterViewModel.TransactionHeadIID = dto.TransactionHead.HeadIID;
                purchaseInVoice.MasterViewModel.CompanyID = dto.TransactionHead.CompanyID;
                purchaseInVoice.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = dto.TransactionHead.DocumentTypeID.ToString(), Value = dto.TransactionHead.DocumentTypeName };
                purchaseInVoice.MasterViewModel.Branch = new KeyValueViewModel() { Key = dto.TransactionHead.BranchID.ToString(), Value = dto.TransactionHead.BranchName };
                purchaseInVoice.MasterViewModel.TransactionNo = dto.TransactionHead.TransactionNo;
                purchaseInVoice.MasterViewModel.TransactionDate = dto.TransactionHead.TransactionDate != null ? Convert.ToDateTime(dto.TransactionHead.TransactionDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                purchaseInVoice.MasterViewModel.Validity = dto.TransactionHead.DueDate != null ? Convert.ToDateTime(dto.TransactionHead.DueDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                purchaseInVoice.MasterViewModel.Remarks = dto.TransactionHead.Description;
                purchaseInVoice.MasterViewModel.Reference = dto.TransactionHead.Reference;
                purchaseInVoice.MasterViewModel.Currency = new KeyValueViewModel();
                purchaseInVoice.MasterViewModel.Currency.Key = dto.TransactionHead.CurrencyID.ToString();
                purchaseInVoice.MasterViewModel.Currency.Value = dto.TransactionHead.CurrencyName;

                purchaseInVoice.MasterViewModel.Supplier = new KeyValueViewModel();

                if (dto.TransactionHead.SupplierID.HasValue)
                {
                    purchaseInVoice.MasterViewModel.Supplier.Key = dto.TransactionHead.SupplierID.ToString();
                    purchaseInVoice.MasterViewModel.Supplier.Value = dto.TransactionHead.SupplierName;
                }

                //purchaseInVoice.MasterViewModel.DeliveryType = dto.TransactionHead.DeliveryTypeID.ToString();
                purchaseInVoice.MasterViewModel.IsShipment = dto.TransactionHead.IsShipment;
                //purchaseInVoice.MasterViewModel.Entitlement = dto.TransactionHead.EntitlementID.ToString();

                purchaseInVoice.MasterViewModel.ReferenceTransactionHeaderID = dto.TransactionHead.ReferenceHeadID.ToString();
                purchaseInVoice.MasterViewModel.ReferenceHeadID = dto.TransactionHead.ReferenceHeadID;
                purchaseInVoice.MasterViewModel.CreatedBy = dto.TransactionHead.CreatedBy;
                purchaseInVoice.MasterViewModel.UpdatedBy = Convert.ToInt32(dto.TransactionHead.UpdatedBy);
                purchaseInVoice.MasterViewModel.JobEntryHeadID = dto.TransactionHead.JobEntryHeadID.IsNotNull() ? Convert.ToInt32(dto.TransactionHead.JobEntryHeadID) : default(long);
                //purchaseInVoice.MasterViewModel.Allocations = new BranchWiseAllocationViewModel();
                //purchaseInVoice.MasterViewModel.Allocations.Allocations = new List<QuantityAllocationViewModel>();
                //purchaseInVoice.MasterViewModel.ErrorCode = dto.ErrorCode;
                //purchaseInVoice.MasterViewModel.TransactionStatus = dto.TransactionHead.TransactionStatusID.ToString();

                purchaseInVoice.MasterViewModel.IsTransactionCompleted = dto.TransactionHead.IsTransactionCompleted;
                purchaseInVoice.MasterViewModel.Discount = dto.TransactionHead.DiscountAmount;
                purchaseInVoice.MasterViewModel.DiscountPercentage = dto.TransactionHead.DiscountPercentage;
                purchaseInVoice.MasterViewModel.DeliveryCharge = dto.TransactionHead.DeliveryCharge.HasValue ? dto.TransactionHead.DeliveryCharge.Value : 0;
                purchaseInVoice.MasterViewModel.ExchangeRate = dto.TransactionHead.ExchangeRate;
                purchaseInVoice.MasterViewModel.TotalLandingCost = dto.TransactionHead.TotalLandingCost;
                purchaseInVoice.MasterViewModel.ForeignAmount = dto.TransactionHead.ForeignAmount;

                purchaseInVoice.MasterViewModel.ForeignInvoiceAmount = dto.TransactionHead.InvoiceForeignAmount;
                purchaseInVoice.MasterViewModel.LocalInvoiceAmount = dto.TransactionHead.InvoiceLocalAmount;              

                //purchaseInVoice.MasterViewModel.Entitlements = dto.TransactionHead.Entitlements.Count > 0
                //    ? KeyValueViewModel.FromDTO(dto.TransactionHead.Entitlements) : null;
                purchaseInVoice.MasterViewModel.ReferenceTransactionNo = dto.TransactionHead.ReferenceTransactionNo;

                //if (dto.TransactionHead.DeliveryMethodID > 0)
                //{
                //    purchaseInVoice.MasterViewModel.DeliveryMethod.Key = dto.TransactionHead.DeliveryMethodID.ToString();
                //    purchaseInVoice.MasterViewModel.DeliveryMethod.Value = dto.TransactionHead.DeliveryTypeName;
                //}

                if (dto.TransactionHead.TransactionStatusID > 0)
                {
                    purchaseInVoice.MasterViewModel.TransactionStatus.Key = dto.TransactionHead.TransactionStatusID.ToString();
                    purchaseInVoice.MasterViewModel.TransactionStatus.Value = dto.TransactionHead.TransactionStatusName;
                }

                if (dto.TransactionHead.DocumentStatusID.HasValue)
                {
                    purchaseInVoice.MasterViewModel.DocumentStatus.Key = dto.TransactionHead.DocumentStatusID.ToString();
                    purchaseInVoice.MasterViewModel.DocumentStatus.Value = dto.TransactionHead.DocumentStatusName;
                }

                // Map TransactionHeadEntitlementMapViewModel
                if (dto.TransactionHead.TransactionHeadEntitlementMaps != null && dto.TransactionHead.TransactionHeadEntitlementMaps.Count > 0)
                {
                    purchaseInVoice.MasterViewModel.TransactionHeadEntitlementMaps =
                        dto.TransactionHead.TransactionHeadEntitlementMaps.Select(x => TransactionHeadEntitlementMapViewModel.ToVm(x)).ToList();
                }
                else
                {
                    purchaseInVoice.MasterViewModel.TransactionHeadEntitlementMaps = new List<TransactionHeadEntitlementMapViewModel>(){
                         new TransactionHeadEntitlementMapViewModel()
                     };
                }

                // Map TransactionHeadEntitlementMapViewModel
                if (dto.TransactionHead.AdditionalExpensesTransactionsMaps != null && dto.TransactionHead.AdditionalExpensesTransactionsMaps.Count > 0)
                {
                    purchaseInVoice.MasterViewModel.AdditionalExpTransMaps =
                        dto.TransactionHead.AdditionalExpensesTransactionsMaps.Select(x => AdditionalExpTransMapViewModel.ToVm(x)).ToList();
                }
                else
                {
                    purchaseInVoice.MasterViewModel.AdditionalExpTransMaps = new List<AdditionalExpTransMapViewModel>(){
                         new AdditionalExpTransMapViewModel()
                     };
                }

                if (dto.TransactionHead.TaxDetails != null && dto.TransactionHead.TaxDetails.Count > 0)
                {
                    purchaseInVoice.MasterViewModel.TaxDetails = new TaxDetailsViewModel() { Taxes = dto.TransactionHead.TaxDetails.Select(x => TaxDetailsViewModel.ToVM(x)).ToList() };
                }

                if (dto.TransactionDetails != null && dto.TransactionDetails.Count > 0)
                {
                    foreach (var transactionDetail in dto.TransactionDetails)
                    {
                        var purchaseInvoiceDetail = new PurchaseInvoiceDetailViewModel();

                        purchaseInvoiceDetail.TransactionDetailID = transactionDetail.DetailIID;
                        purchaseInvoiceDetail.TransactionHead = Convert.ToInt32(transactionDetail.HeadID);
                        purchaseInvoiceDetail.SKUID = new KeyValueViewModel();
                        purchaseInvoiceDetail.SKUID.Key = transactionDetail.ProductSKUMapID.ToString();
                        purchaseInvoiceDetail.SKUID.Value = transactionDetail.SKU;
                        purchaseInvoiceDetail.Description = transactionDetail.SKU;
                        purchaseInvoiceDetail.Quantity = transactionDetail.Quantity.Value;
                        purchaseInvoiceDetail.Amount = transactionDetail.Amount;
                        purchaseInvoiceDetail.UnitPrice =transactionDetail.UnitPrice;
                        purchaseInvoiceDetail.UnitID = Convert.ToInt32(transactionDetail.UnitID);
                        purchaseInvoiceDetail.Unit = new KeyValueViewModel() { Key = transactionDetail.UnitID.ToString(), Value = transactionDetail.Unit };
                        purchaseInvoiceDetail.CreatedBy = Convert.ToInt32(transactionDetail.CreatedBy);
                        purchaseInvoiceDetail.UpdatedBy = Convert.ToInt32(transactionDetail.UpdatedBy);
                        purchaseInvoiceDetail.SKUDetails = null;
                        purchaseInvoiceDetail.IsSerialNumberOnPurchase = transactionDetail.IsSerialNumberOnPurchase;
                        purchaseInvoiceDetail.ProductLength = transactionDetail.ProductLength;
                        purchaseInvoiceDetail.ProductTypeName = transactionDetail.ProductTypeName;
                        purchaseInvoiceDetail.IsError = transactionDetail.IsError;
                        purchaseInvoiceDetail.PartNo = transactionDetail.PartNo;
                        purchaseInvoiceDetail.TaxTemplateID = transactionDetail.TaxTemplateID;
                        purchaseInvoiceDetail.TaxPercentage = transactionDetail.TaxPercentage;
                        purchaseInvoiceDetail.TaxTemplate = transactionDetail.TaxTemplateID.ToString();
                        purchaseInvoiceDetail.LandingCost = transactionDetail.LandingCost;
                        purchaseInvoiceDetail.LastCostPrice = transactionDetail.LastCostPrice;
                        purchaseInvoiceDetail.CostCenterID = transactionDetail.CostCenterID;
                        purchaseInvoiceDetail.Fraction = transactionDetail.Fraction;
                        purchaseInvoiceDetail.ForeignRate = transactionDetail.ForeignRate;
                        purchaseInvoiceDetail.ForeignAmount =transactionDetail.ForeignAmount;
                        purchaseInvoiceDetail.ExchangeRate = (float)transactionDetail.ExchangeRate;
                        //if (transactionDetail.CostCenter != null)
                        //{
                        //    purchaseInvoiceDetail.CostCenter = new KeyValueViewModel() { Key = transactionDetail.CostCenter.Key, Value = transactionDetail.CostCenter.Value };

                        //}
                        purchaseInvoiceDetail.UnitGroupID = transactionDetail.UnitGroupID;
                        purchaseInvoiceDetail.ProductCode = transactionDetail.ProductCode;
                        purchaseInvoiceDetail.UnitDTO = new List<KeyValueViewModel>();
                        if (transactionDetail.UnitDTO != null && transactionDetail.UnitDTO.Count > 0)
                        {
                            foreach (KeyValueDTO val in transactionDetail.UnitDTO)
                            {
                                purchaseInvoiceDetail.UnitDTO.Add(new KeyValueViewModel()
                                {
                                    Key = val.Key,
                                    Value = val.Value
                                });
                            }
                        }
                        purchaseInvoiceDetail.UnitList = new List<UnitsViewModel>();
                        if (transactionDetail.UnitList != null && transactionDetail.UnitList.Count > 0)
                        {
                            foreach (var val in transactionDetail.UnitList)
                            {
                                purchaseInvoiceDetail.UnitList.Add(new UnitsViewModel()
                                {
                                    UnitID = val.UnitID,
                                    UnitGroupID = val.UnitGroupID,
                                    UnitCode = val.UnitCode,
                                    UnitName = val.UnitName,
                                    Fraction = val.Fraction
                                });
                            }
                        }
                        purchaseInvoiceDetail.WarrantyStartDateString = transactionDetail.WarrantyStartDate != null ? Convert.ToDateTime(transactionDetail.WarrantyStartDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                        purchaseInvoiceDetail.WarrantyEndDateString = transactionDetail.WarrantyEndDate != null ? Convert.ToDateTime(transactionDetail.WarrantyEndDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;

                        if (transactionDetail.SKUDetails != null && transactionDetail.SKUDetails.Count > 0)
                        {
                            Mapper<ProductSerialMapDTO, ProductSKUDetailsViewModel>.CreateMap();
                            var vm = Mapper<ProductSerialMapDTO, ProductSKUDetailsViewModel>.Map(transactionDetail.SKUDetails);
                            purchaseInvoiceDetail.SKUDetails = vm.ToList();
                        }

                        if (transactionDetail.TransactionAllocations != null && transactionDetail.TransactionAllocations.Count > 0)
                        {
                            var allocation = new QuantityAllocationViewModel();
                            allocation.ProductName = transactionDetail.SKU;
                            allocation.ProductID = (long)transactionDetail.ProductSKUMapID;
                            allocation.Quantity = transactionDetail.Quantity.IsNotNull() ? (decimal)transactionDetail.Quantity : 0;
                            allocation.AllocatedQuantity = new List<decimal>();
                            allocation.BranchIDs = new List<long>();
                            allocation.BranchName = new List<string>();

                            foreach (var transactionDetailAllocation in transactionDetail.TransactionAllocations)
                            {
                                allocation.AllocatedQuantity.Add(transactionDetailAllocation.Quantity != null ? (decimal)transactionDetailAllocation.Quantity : 0);
                                allocation.BranchIDs.Add(transactionDetailAllocation.BranchID);
                            }
                            //purchaseInVoice.MasterViewModel.Allocations.Allocations.Add(allocation);

                        }

                        purchaseInVoice.DetailViewModel.Add(purchaseInvoiceDetail);
                    }
                }

                return purchaseInVoice;
            }
            else return new PurchaseInvoiceViewModel();
        }
    }
}

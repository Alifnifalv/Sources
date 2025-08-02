using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Commons;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    public class AssetEntryViewModel : BaseMasterViewModel
    {
        public AssetEntryViewModel()
        {
            MasterViewModel = new PurchaseInvoiceMasterViewModel();
            DetailViewModel = new List<PurchaseInvoiceDetailViewModel>();
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
                var transaction = new TransactionDTO();
                transaction.TransactionHead = new TransactionHeadDTO();

                Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
                Mapper<PurchaseInvoiceMasterViewModel, TransactionHeadDTO>.CreateMap();
                Mapper<TransactionHeadEntitlementMapViewModel, TransactionHeadEntitlementMapDTO>.CreateMap();
                
                transaction.TransactionHead = Mapper<PurchaseInvoiceMasterViewModel, TransactionHeadDTO>.Map(vm.MasterViewModel);

                transaction.TransactionHead.HeadIID = vm.MasterViewModel.TransactionHeadIID;
                transaction.TransactionHead.DocumentTypeID = Convert.ToInt32(vm.MasterViewModel.DocumentType);
                transaction.TransactionHead.BranchID = Convert.ToInt32(vm.MasterViewModel.Branch);
                //transaction.TransactionHead.TransactionNo = vm.MasterViewModel.TransactionNo;
                //transaction.TransactionHead.TransactionDate = vm.MasterViewModel.TransactionDate != null ? Convert.ToDateTime(vm.MasterViewModel.TransactionDate) : (DateTime?)null;
                transaction.TransactionHead.DueDate = vm.MasterViewModel.Validity != null ? Convert.ToDateTime(vm.MasterViewModel.Validity) : (DateTime?)null;
                transaction.TransactionHead.Description = vm.MasterViewModel.Remarks;
                transaction.TransactionHead.CurrencyID =  vm.MasterViewModel.Currency != null ? Convert.ToInt32(vm.MasterViewModel.Currency.Key) : (int?) null;
                transaction.TransactionHead.SupplierID = vm.MasterViewModel.Supplier != null ? Convert.ToInt32(vm.MasterViewModel.Supplier.Key) : (long?)null; 
                //transaction.TransactionHead.DeliveryTypeID = Convert.ToInt32(vm.MasterViewModel.DeliveryType);
                //transaction.TransactionHead.IsShipment = vm.MasterViewModel.IsShipment;
                //transaction.TransactionHead.EntitlementID = Convert.ToInt16(vm.MasterViewModel.Entitlement);
                transaction.TransactionHead.ReferenceHeadID = vm.MasterViewModel.ReferenceTransactionHeaderID != null ? Convert.ToInt32(vm.MasterViewModel.ReferenceTransactionHeaderID) : (long?)null;
                transaction.TransactionHead.JobEntryHeadID =  vm.MasterViewModel.JobEntryHeadID != default(long) ? vm.MasterViewModel.JobEntryHeadID : (long?)null;
               transaction.TransactionHead.DocumentReferenceTypeID = vm.MasterViewModel.DocumentReferenceTypeID;
                //transaction.TransactionHead.CreatedBy = vm.CreatedBy;
                //transaction.TransactionHead.UpdatedBy = vm.UpdatedBy;
                //transaction.TransactionHead.TransactionStatusID = Convert.ToByte(vm.MasterViewModel.TransactionStatus);
                //transaction.TransactionHead.DeliveryTypeID = vm.MasterViewModel.DeliveryType != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DeliveryType.Key) ? Convert.ToInt32(vm.MasterViewModel.DeliveryType.Key) : (int?)null : (int?)null;

                transaction.TransactionHead.TransactionStatusID = vm.MasterViewModel.TransactionStatus != null ? !string.IsNullOrEmpty(vm.MasterViewModel.TransactionStatus.Key) ? byte.Parse(vm.MasterViewModel.TransactionStatus.Key) : (byte?)null : (byte?)null;

                //transaction.TransactionHead.Entitlements = vm.MasterViewModel.Entitlements.Count > 0 ? KeyValueViewModel.ToDTO(vm.MasterViewModel.Entitlements) : null;

                //transaction.TransactionHead.EntitlementID = vm.MasterViewModel.Entitlements.Count > 0 ? Convert.ToInt16(vm.MasterViewModel.Entitlements[0].Key) : (short?)null;

                transaction.TransactionHead.DocumentStatusID = vm.MasterViewModel.DocumentStatus != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DocumentStatus.Key) ? short.Parse(vm.MasterViewModel.DocumentStatus.Key) : (short?)null : (short?)null;

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
                            transactionHeadEntitlementMapDTO.EntitlementID = item.Entitlement != null ? Convert.ToInt16(item.Entitlement.Key) : default(short);
                            transactionHeadEntitlementMapDTO.EntitlementName = item.Entitlement != null ? item.Entitlement.Value : null;
                            transactionHeadEntitlementMapDTO.Amount = item.Amount;

                            // add into TransactionHeadEntitlementMaps list
                            transaction.TransactionHead.TransactionHeadEntitlementMaps.Add(transactionHeadEntitlementMapDTO);
                        }
                    }
                }

                transaction.TransactionDetails = new List<TransactionDetailDTO>();
                if (vm.DetailViewModel != null && vm.DetailViewModel.Count > 0)
                {
                    foreach (var transactionDetail in vm.DetailViewModel)
                    {
                        if (transactionDetail.SKUID != null && transactionDetail.SKUID != null)
                        {
                            var transactionDetailDTO = new TransactionDetailDTO();

                            transactionDetailDTO.DetailIID = transactionDetail.TransactionDetailID;
                            transactionDetailDTO.HeadID = transactionDetail.TransactionHead;
                            transactionDetailDTO.ProductSKUMapID = Convert.ToInt32(transactionDetail.SKUID.Key);
                            transactionDetailDTO.Quantity = Convert.ToDecimal(transactionDetail.Quantity);
                            transactionDetailDTO.Amount = Convert.ToDecimal(transactionDetail.Amount);
                            transactionDetailDTO.UnitPrice = Convert.ToDecimal(transactionDetail.UnitPrice);
                            transactionDetailDTO.UnitID = 1;//currently hard coding it to 1
                            //transactionDetailDTO.UnitID = transactionDetail.UnitID;
                            transactionDetailDTO.CreatedBy = transactionDetail.CreatedBy;
                            transactionDetailDTO.UpdatedBy = transactionDetail.UpdatedBy;

                            if (transactionDetail.SKUDetails != null && transactionDetail.SKUDetails.Count > 0 )
                            {
                                Mapper<ProductSKUDetailsViewModel, ProductSerialMapDTO>.CreateMap();
                                var dto = Mapper<ProductSKUDetailsViewModel, ProductSerialMapDTO>.Map(transactionDetail.SKUDetails);
                                transactionDetailDTO.SKUDetails = dto.ToList();
                            }

                            if (vm.MasterViewModel.Allocations.Allocations != null && vm.MasterViewModel.Allocations.Allocations.Count > 0)
                            {
                                QuantityAllocationViewModel allocationToMap = vm.MasterViewModel.Allocations.Allocations.Where(x => x.ProductID == transactionDetailDTO.ProductSKUMapID).FirstOrDefault();
                                transactionDetailDTO.TransactionAllocations = new List<TransactionAllocationDTO>();
                                if (allocationToMap != null && allocationToMap.BranchIDs != null)
                                {
                                    for (int i = 0; i < allocationToMap.BranchIDs.Count; i++)
                                    {

                                        if (allocationToMap.AllocatedQuantity[i] > 0)
                                        {
                                            var transactionAllocation = new TransactionAllocationDTO();
                                            transactionAllocation.TrasactionDetailID = transactionDetail.TransactionDetailID;
                                            transactionAllocation.BranchID = allocationToMap.BranchIDs[i];
                                            transactionAllocation.Quantity = allocationToMap.AllocatedQuantity[i];
                                            transactionDetailDTO.TransactionAllocations.Add(transactionAllocation); 
                                        } 
                                        
                                    }
                                }
                            }

                            transaction.TransactionDetails.Add(transactionDetailDTO);
                        }
                    }
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
                purchaseInVoice.MasterViewModel.Allocations = new BranchWiseAllocationViewModel();
                purchaseInVoice.MasterViewModel.Allocations.Allocations = new List<QuantityAllocationViewModel>();

                Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
                Mapper<TransactionHeadDTO, PurchaseInvoiceMasterViewModel>.CreateMap();
                Mapper<TransactionHeadEntitlementMapDTO, TransactionHeadEntitlementMapViewModel>.CreateMap();
                purchaseInVoice.MasterViewModel = Mapper<TransactionHeadDTO, PurchaseInvoiceMasterViewModel>.Map(dto.TransactionHead);
                var dateTimeFormat = ConfigurationExtensions.GetAppConfigValue("DateTimeFormat");

                purchaseInVoice.MasterViewModel.TransactionHeadIID = dto.TransactionHead.HeadIID;
                //purchaseInVoice.MasterViewModel.DocumentType = dto.TransactionHead.DocumentTypeID.ToString();
                //purchaseInVoice.MasterViewModel.Branch = dto.TransactionHead.BranchID.ToString();
                purchaseInVoice.MasterViewModel.TransactionNo = dto.TransactionHead.TransactionNo;
                purchaseInVoice.MasterViewModel.TransactionDate = dto.TransactionHead.TransactionDate != null ? Convert.ToDateTime(dto.TransactionHead.TransactionDate).ToString(dateTimeFormat) : null;
                purchaseInVoice.MasterViewModel.Validity = dto.TransactionHead.DueDate != null ? Convert.ToDateTime(dto.TransactionHead.DueDate).ToString(dateTimeFormat) : null;
                purchaseInVoice.MasterViewModel.Remarks = dto.TransactionHead.Description;
                purchaseInVoice.MasterViewModel.Currency = new KeyValueViewModel();
                purchaseInVoice.MasterViewModel.Currency.Key = dto.TransactionHead.CurrencyID.ToString();
                purchaseInVoice.MasterViewModel.Currency.Value = dto.TransactionHead.CurrencyName;

                purchaseInVoice.MasterViewModel.Supplier = new KeyValueViewModel();
                purchaseInVoice.MasterViewModel.Supplier.Key = dto.TransactionHead.SupplierID.ToString();
                purchaseInVoice.MasterViewModel.Supplier.Value = dto.TransactionHead.SupplierName;

                //purchaseInVoice.MasterViewModel.DeliveryType = dto.TransactionHead.DeliveryTypeID.ToString();
                purchaseInVoice.MasterViewModel.IsShipment = dto.TransactionHead.IsShipment;
                //purchaseInVoice.MasterViewModel.Entitlement = dto.TransactionHead.EntitlementID.ToString();

                purchaseInVoice.MasterViewModel.ReferenceTransactionHeaderID = dto.TransactionHead.ReferenceHeadID.ToString();
                purchaseInVoice.MasterViewModel.CreatedBy = dto.TransactionHead.CreatedBy;
                purchaseInVoice.MasterViewModel.UpdatedBy = Convert.ToInt32(dto.TransactionHead.UpdatedBy);
                purchaseInVoice.MasterViewModel.JobEntryHeadID = dto.TransactionHead.JobEntryHeadID.IsNotNull() ? Convert.ToInt32(dto.TransactionHead.JobEntryHeadID) : default(long);
                purchaseInVoice.MasterViewModel.Allocations = new BranchWiseAllocationViewModel();
                purchaseInVoice.MasterViewModel.Allocations.Allocations = new List<QuantityAllocationViewModel>();
                purchaseInVoice.MasterViewModel.ErrorCode = dto.ErrorCode;

                //purchaseInVoice.MasterViewModel.TransactionStatus = dto.TransactionHead.TransactionStatusID.ToString();

                purchaseInVoice.MasterViewModel.IsTransactionCompleted = dto.TransactionHead.IsTransactionCompleted;

                //purchaseInVoice.MasterViewModel.Entitlements = dto.TransactionHead.Entitlements.Count > 0
                //    ? KeyValueViewModel.FromDTO(dto.TransactionHead.Entitlements) : null;

                if (dto.TransactionHead.DeliveryTypeID > 0)
                {
                    //purchaseInVoice.MasterViewModel.DeliveryType.Key = dto.TransactionHead.DeliveryTypeID.ToString();
                    //purchaseInVoice.MasterViewModel.DeliveryType.Value = dto.TransactionHead.DeliveryTypeName;
                }

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
                        purchaseInvoiceDetail.Quantity =  Convert.ToDouble(transactionDetail.Quantity);
                        purchaseInvoiceDetail.Amount = Convert.ToDouble(transactionDetail.Amount);
                        purchaseInvoiceDetail.UnitPrice = Convert.ToDouble(transactionDetail.UnitPrice);
                        purchaseInvoiceDetail.UnitID = Convert.ToInt32(transactionDetail.UnitID);
                        purchaseInvoiceDetail.CreatedBy = Convert.ToInt32(transactionDetail.CreatedBy);
                        purchaseInvoiceDetail.UpdatedBy = Convert.ToInt32(transactionDetail.UpdatedBy);
                        purchaseInvoiceDetail.SKUDetails = null;
                        purchaseInvoiceDetail.IsSerialNumberOnPurchase = transactionDetail.IsSerialNumberOnPurchase;
                        purchaseInvoiceDetail.ProductLength = transactionDetail.ProductLength;
                        purchaseInvoiceDetail.ProductTypeName = transactionDetail.ProductTypeName;
                        purchaseInvoiceDetail.IsError = transactionDetail.IsError;

                        if (transactionDetail.SKUDetails != null && transactionDetail.SKUDetails.Count > 0)
                        {
                            Mapper<ProductSerialMapDTO, ProductSKUDetailsViewModel>.CreateMap();
                            var vm = Mapper<ProductSerialMapDTO, ProductSKUDetailsViewModel>.Map(transactionDetail.SKUDetails);
                            purchaseInvoiceDetail.SKUDetails = vm.ToList();
                        }

                        if(transactionDetail.TransactionAllocations != null  && transactionDetail.TransactionAllocations.Count>0)
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
                            purchaseInVoice.MasterViewModel.Allocations.Allocations.Add(allocation);
                            
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

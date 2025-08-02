using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    public class AssetPurchaseOrderViewModel : BaseMasterViewModel
    {
        public AssetPurchaseOrderViewModel()
        {
            MasterViewModel = new AssetPurchaseOrderMasterViewModel();
            DetailViewModel = new List<AssetPurchaseOrderDetailViewModel>() { new AssetPurchaseOrderDetailViewModel() { } };
        }

        public AssetPurchaseOrderMasterViewModel MasterViewModel { get; set; }
        public List<AssetPurchaseOrderDetailViewModel> DetailViewModel { get; set; }

        public static AssetPurchaseOrderViewModel ToVM(TransactionDTO dto)
        {
            if (dto != null)
            {
                var purchaseOrder = new AssetPurchaseOrderViewModel();
                purchaseOrder.MasterViewModel = new AssetPurchaseOrderMasterViewModel();
                purchaseOrder.DetailViewModel = new List<AssetPurchaseOrderDetailViewModel>();

                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                purchaseOrder.MasterViewModel.IsError = dto.TransactionHead.IsError;
                purchaseOrder.MasterViewModel.ErrorCode = dto.TransactionHead.ErrorCode;
                purchaseOrder.MasterViewModel.TransactionHeadIID = dto.TransactionHead.HeadIID;
                purchaseOrder.MasterViewModel.CompanyID = dto.TransactionHead.CompanyID;
                purchaseOrder.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = dto.TransactionHead.DocumentTypeID.ToString(), Value = dto.TransactionHead.DocumentTypeName };
                purchaseOrder.MasterViewModel.Branch = new KeyValueViewModel() { Key = dto.TransactionHead.BranchID.ToString(), Value = dto.TransactionHead.BranchName };
                purchaseOrder.MasterViewModel.TransactionNo = dto.TransactionHead.TransactionNo;
                purchaseOrder.MasterViewModel.TransactionDate = dto.TransactionHead.TransactionDate != null ? Convert.ToDateTime(dto.TransactionHead.TransactionDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                purchaseOrder.MasterViewModel.Validity = dto.TransactionHead.DueDate != null ? Convert.ToDateTime(dto.TransactionHead.DueDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                purchaseOrder.MasterViewModel.Remarks = dto.TransactionHead.Reference;
                purchaseOrder.MasterViewModel.Currency = new KeyValueViewModel();
                purchaseOrder.MasterViewModel.Currency.Key = dto.TransactionHead.CurrencyID.ToString();
                purchaseOrder.MasterViewModel.Currency.Value = dto.TransactionHead.CurrencyName.IsNotNull() ? dto.TransactionHead.CurrencyName.ToString() : null;
                purchaseOrder.MasterViewModel.Supplier = new KeyValueViewModel();
                purchaseOrder.MasterViewModel.Supplier.Key = dto.TransactionHead.SupplierID.ToString();
                purchaseOrder.MasterViewModel.Supplier.Value = dto.TransactionHead.SupplierName;
                purchaseOrder.MasterViewModel.IsShipment = dto.TransactionHead.IsShipment;
                purchaseOrder.MasterViewModel.DeliveryDate = dto.TransactionHead.DeliveryDate != null ? Convert.ToDateTime(dto.TransactionHead.DeliveryDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                purchaseOrder.MasterViewModel.CreatedBy = dto.TransactionHead.CreatedBy;
                purchaseOrder.MasterViewModel.UpdatedBy = Convert.ToInt32(dto.TransactionHead.UpdatedBy);
                purchaseOrder.MasterViewModel.JobStatus = Convert.ToString(dto.TransactionHead.JobStatusID != null ? dto.TransactionHead.JobStatusID : null);
                purchaseOrder.MasterViewModel.InvoiceStatus = dto.TransactionHead.InvoiceStatus.IsNotNull() ? dto.TransactionHead.InvoiceStatus : string.Empty;
                purchaseOrder.MasterViewModel.Discount = dto.TransactionHead.DiscountAmount.IsNotNull() ? dto.TransactionHead.DiscountAmount : null;
                purchaseOrder.MasterViewModel.DiscountPercentage = dto.TransactionHead.DiscountPercentage.IsNotNull() ? dto.TransactionHead.DiscountPercentage : null;
                purchaseOrder.MasterViewModel.DocumentReferenceTypeID = Services.Contracts.Enums.DocumentReferenceTypes.PurchaseOrder;
                purchaseOrder.MasterViewModel.ReferenceHeadID = dto.TransactionHead.ReferenceHeadID;
                purchaseOrder.MasterViewModel.IsTransactionCompleted = dto.TransactionHead.IsTransactionCompleted;
                purchaseOrder.MasterViewModel.ExchangeRate = dto.TransactionHead.ExchangeRate;

                if (dto.TransactionHead.DeliveryMethodID > 0)
                {
                    purchaseOrder.MasterViewModel.DeliveryMethod.Key = dto.TransactionHead.DeliveryMethodID.ToString();
                    purchaseOrder.MasterViewModel.DeliveryMethod.Value = dto.TransactionHead.DeliveryTypeName;
                }

                if (dto.TransactionHead.DocumentStatusID.HasValue)
                {
                    purchaseOrder.MasterViewModel.DocumentStatus.Key = dto.TransactionHead.DocumentStatusID.ToString();
                    purchaseOrder.MasterViewModel.DocumentStatus.Value = dto.TransactionHead.DocumentStatusName;
                }

                // Map TransactionHeadEntitlementMapViewModel
                if (dto.TransactionHead.TransactionHeadEntitlementMaps != null && dto.TransactionHead.TransactionHeadEntitlementMaps.Count > 0)
                {
                    purchaseOrder.MasterViewModel.TransactionHeadEntitlementMaps =
                        dto.TransactionHead.TransactionHeadEntitlementMaps.Select(x => TransactionHeadEntitlementMapViewModel.ToVm(x)).ToList();
                }
                else
                {
                    purchaseOrder.MasterViewModel.TransactionHeadEntitlementMaps = new List<TransactionHeadEntitlementMapViewModel>() { new TransactionHeadEntitlementMapViewModel() };
                }
                if (dto.TransactionDetails != null && dto.TransactionDetails.Count > 0)
                {
                    foreach (var transactionDetail in dto.TransactionDetails)
                    {
                        var purchaseOrderDetail = new AssetPurchaseOrderDetailViewModel();

                        purchaseOrderDetail.TransactionDetailID = transactionDetail.DetailIID;
                        purchaseOrderDetail.TransactionHead = Convert.ToInt32(transactionDetail.HeadID);
                        purchaseOrderDetail.SKUID = new KeyValueViewModel();
                        purchaseOrderDetail.SKUID.Key = transactionDetail.ProductSKUMapID.ToString();
                        purchaseOrderDetail.SKUID.Value = transactionDetail.SKU;
                        purchaseOrderDetail.Description = transactionDetail.SKU;
                        purchaseOrderDetail.Quantity = Convert.ToDouble(transactionDetail.Quantity);
                        purchaseOrderDetail.Amount = Convert.ToDouble(transactionDetail.Amount);
                        purchaseOrderDetail.UnitPrice = Convert.ToDouble(transactionDetail.UnitPrice);
                        purchaseOrderDetail.UnitID = Convert.ToInt32(transactionDetail.UnitID);
                        purchaseOrderDetail.CreatedBy = Convert.ToInt32(transactionDetail.CreatedBy);
                        purchaseOrderDetail.UpdatedBy = Convert.ToInt32(transactionDetail.UpdatedBy);
                        purchaseOrderDetail.IsSerialNumberOnPurchase = transactionDetail.IsSerialNumberOnPurchase;
                        purchaseOrderDetail.ProductLength = transactionDetail.ProductLength;
                        purchaseOrderDetail.ProductTypeName = transactionDetail.ProductTypeName;
                        purchaseOrderDetail.PartNo = transactionDetail.PartNo;
                        purchaseOrderDetail.Fraction = Convert.ToDouble(transactionDetail.Fraction);
                        purchaseOrderDetail.ForeignRate = transactionDetail.ForeignRate;
                        purchaseOrderDetail.ForeignAmount = transactionDetail.ForeignAmount;
                        purchaseOrderDetail.ExchangeRate = transactionDetail.ExchangeRate;
                        purchaseOrderDetail.Unit = new KeyValueViewModel() { Key = transactionDetail.UnitID.ToString(), Value = transactionDetail.Unit };
                        purchaseOrderDetail.CostCenterID = transactionDetail.CostCenterID;

                        purchaseOrderDetail.UnitGroupID = transactionDetail.UnitGroupID;
                        purchaseOrderDetail.ProductCode = transactionDetail.ProductCode;
                        purchaseOrderDetail.UnitDTO = new List<KeyValueViewModel>();
                        if (transactionDetail.UnitDTO != null && transactionDetail.UnitDTO.Count > 0)
                        {
                            foreach (KeyValueDTO val in transactionDetail.UnitDTO)
                            {
                                purchaseOrderDetail.UnitDTO.Add(new KeyValueViewModel()
                                {
                                    Key = val.Key,
                                    Value = val.Value
                                });
                            }
                        }
                        purchaseOrderDetail.UnitList = new List<UnitsViewModel>();
                        if (transactionDetail.UnitList != null && transactionDetail.UnitList.Count > 0)
                        {
                            foreach (var val in transactionDetail.UnitList)
                            {
                                purchaseOrderDetail.UnitList.Add(new UnitsViewModel()
                                {
                                    UnitID = val.UnitID,
                                    UnitGroupID = val.UnitGroupID,
                                    UnitCode = val.UnitCode,
                                    UnitName = val.UnitName,
                                    Fraction = val.Fraction
                                });
                            }
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
                        }

                        purchaseOrder.DetailViewModel.Add(purchaseOrderDetail);
                    }
                }

                return purchaseOrder;
            }
            else return new AssetPurchaseOrderViewModel();
        }

        public static TransactionDTO ToDTO(AssetPurchaseOrderViewModel vm)
        {
            if (vm != null)
            {
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
                var transaction = new TransactionDTO();
                transaction.TransactionHead = new TransactionHeadDTO();
                transaction.TransactionDetails = new List<TransactionDetailDTO>();
                transaction.ShipmentDetails = new ShipmentDetailDTO();

                if (vm.MasterViewModel.DocumentType == null || vm.MasterViewModel.DocumentType.Key == null)
                {
                    throw new Exception("Select Document Type!");
                }

                if (vm.MasterViewModel.Currency == null || vm.MasterViewModel.Currency.Key == null)
                {
                    throw new Exception("Select Any Currency!");
                }

                if (vm.MasterViewModel.Supplier == null || vm.MasterViewModel.Supplier.Key == null)
                {
                    throw new Exception("Select Supplier!");
                }

                if (vm.DetailViewModel.FindAll(x => x.SKUID != null).Count() <= 0)
                {
                    throw new Exception("Please enter product details!");
                }

                if (vm.DetailViewModel.FindAll(x => (x.Unit == null || x.Unit.Key == null) && !(x.SKUID == null || x.SKUID.Key == null)).Count() > 0)
                {
                    throw new Exception("Please select unit for the selected product !");
                }
                transaction.TransactionHead.HeadIID = vm.MasterViewModel.TransactionHeadIID;
                transaction.TransactionHead.CompanyID = vm.MasterViewModel.CompanyID;
                transaction.TransactionHead.DocumentTypeID = vm.MasterViewModel.DocumentType != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DocumentType.Key) ? short.Parse(vm.MasterViewModel.DocumentType.Key) : (int?)null : null;
                transaction.TransactionHead.BranchID = vm.MasterViewModel.Branch != null && !string.IsNullOrEmpty(vm.MasterViewModel.Branch.Key) ? long.Parse(vm.MasterViewModel.Branch.Key) : (long?)null;
                transaction.TransactionHead.TransactionNo = vm.MasterViewModel.TransactionNo;
                transaction.TransactionHead.TransactionDate = vm.MasterViewModel.TransactionDate != null ? DateTime.ParseExact(vm.MasterViewModel.TransactionDate, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                transaction.TransactionHead.DueDate = vm.MasterViewModel.Validity != null ? DateTime.ParseExact(vm.MasterViewModel.Validity, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                transaction.TransactionHead.Reference = vm.MasterViewModel.Remarks;
                transaction.TransactionHead.CurrencyID = vm.MasterViewModel.Currency != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Currency.Key) ? Convert.ToInt32(vm.MasterViewModel.Currency.Key) : (int?)null : (int?)null;
                transaction.TransactionHead.SupplierID = vm.MasterViewModel.Supplier != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Supplier.Key) ? Convert.ToInt32(vm.MasterViewModel.Supplier.Key) : (long?)null : (long?)null;
                transaction.TransactionHead.IsShipment = vm.MasterViewModel.IsShipment;
                transaction.TransactionHead.DeliveryDate = vm.MasterViewModel.DeliveryDate != null ? DateTime.ParseExact(vm.MasterViewModel.DeliveryDate, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                transaction.TransactionHead.CreatedBy = vm.CreatedBy;
                transaction.TransactionHead.UpdatedBy = vm.UpdatedBy;
                transaction.TransactionHead.JobStatusID = Convert.ToInt16(!string.IsNullOrEmpty(vm.MasterViewModel.JobStatus));

                transaction.TransactionHead.DeliveryMethodID = vm.MasterViewModel.DeliveryMethod != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DeliveryMethod.Key) ? Convert.ToByte(vm.MasterViewModel.DeliveryMethod.Key) : (short?)null : (short?)null;
                transaction.TransactionHead.DeliveryTypeName = vm.MasterViewModel.DeliveryMethod != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DeliveryMethod.Value) ? vm.MasterViewModel.DeliveryMethod.Value.ToString() : null : null;

                transaction.TransactionHead.DocumentStatusID = vm.MasterViewModel.DocumentStatus != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DocumentStatus.Key) ? short.Parse(vm.MasterViewModel.DocumentStatus.Key) : (short?)null : (short?)null;
                transaction.TransactionHead.DocumentStatusName = vm.MasterViewModel.DocumentStatus != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DocumentStatus.Value) ? vm.MasterViewModel.DocumentStatus.Value.ToString() : null : null;
                transaction.TransactionHead.DiscountAmount = vm.MasterViewModel.Discount.IsNotNull() ? vm.MasterViewModel.Discount : null;
                transaction.TransactionHead.DiscountPercentage = vm.MasterViewModel.DiscountPercentage.IsNotNull() ? vm.MasterViewModel.DiscountPercentage : null;
                transaction.TransactionHead.ExchangeRate = vm.MasterViewModel.ExchangeRate;
                transaction.TransactionHead.ReferenceHeadID = vm.MasterViewModel.ReferenceHeadID;

                transaction.IgnoreEntitlementCheck = true;

                if (vm.DetailViewModel != null && vm.DetailViewModel.Count > 0)
                {
                    foreach (var transactionDetail in vm.DetailViewModel)
                    {
                        if (transactionDetail.SKUID != null && transactionDetail.SKUID != null)
                        {
                            var transactionDetailDTO = new TransactionDetailDTO();

                            transactionDetailDTO.DetailIID = transactionDetail.TransactionDetailID;
                            transactionDetailDTO.HeadID = transactionDetail.TransactionHead;
                            transactionDetailDTO.ProductSKUMapID = transactionDetail.SKUID != null ? !string.IsNullOrEmpty(transactionDetail.SKUID.Key) ? Convert.ToInt32(transactionDetail.SKUID.Key) : (long?)null : (long?)null;
                            transactionDetailDTO.Quantity = Convert.ToDecimal(transactionDetail.Quantity);
                            transactionDetailDTO.Amount = Convert.ToDecimal(transactionDetail.Amount);
                            transactionDetailDTO.UnitPrice = Convert.ToDecimal(transactionDetail.UnitPrice);

                            transactionDetailDTO.UnitID = transactionDetail.UnitID;
                            transactionDetailDTO.Fraction = Convert.ToDecimal(transactionDetail.Fraction);
                            transactionDetailDTO.ForeignAmount = Convert.ToDecimal(transactionDetail.ForeignAmount);
                            transactionDetailDTO.ForeignRate = Convert.ToDecimal(transactionDetail.ForeignRate);
                            transactionDetailDTO.ExchangeRate = Convert.ToDecimal(transactionDetail.ExchangeRate);
                            transactionDetailDTO.UnitGroupID = transactionDetail.UnitGroupID;

                            transactionDetailDTO.CreatedBy = transactionDetail.CreatedBy;
                            transactionDetailDTO.UpdatedBy = transactionDetail.UpdatedBy;                            

                            transaction.TransactionDetails.Add(transactionDetailDTO);
                        }
                    }
                }

                return transaction;
            }
            else return new TransactionDTO();
        }
    }
}

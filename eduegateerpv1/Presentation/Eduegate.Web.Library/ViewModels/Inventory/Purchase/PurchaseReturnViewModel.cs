using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Eduegate.Domain;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    public class PurchaseReturnViewModel : BaseMasterViewModel
    {
        public PurchaseReturnViewModel()
        {
            MasterViewModel = new PurchaseReturnMasterViewModel();
            DetailViewModel = new List<PurchaseReturnDetailViewModel>() { new PurchaseReturnDetailViewModel() };
        }

        public PurchaseReturnMasterViewModel MasterViewModel { get; set; }
        public List<PurchaseReturnDetailViewModel> DetailViewModel { get; set; }


        public static TransactionDTO FromVMToTransactionDTO(PurchaseReturnViewModel vm)
        {
            if (vm != null)
            {
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
                var transaction = new TransactionDTO();
                transaction.TransactionHead = new TransactionHeadDTO();

                if (vm.MasterViewModel.Branch.Key == null)
                {
                    throw new Exception("Select Branch!");
                }

                if (vm.MasterViewModel.DocumentType.Key == null)
                {
                    throw new Exception("Select Document Type!");
                }

                if (vm.MasterViewModel.Currency.Key == null)
                {
                    throw new Exception("Select Any Currency!");
                }

                if (vm.MasterViewModel.Supplier.Key == null)
                {
                    throw new Exception("Select Supplier!");
                }

                //if (vm.MasterViewModel.ReturnMethod.Key == null)
                //{
                //    throw new Exception("Select Any Return Method!");
                //}

                if (vm.MasterViewModel.DocumentStatus.Key == null)
                {
                    throw new Exception("Select Any Document Status!");
                }
                if (vm.DetailViewModel.FindAll(x => x.SKUID != null).Count() <= 0)
                    throw new Exception("Please enter product details!");

                if (vm.DetailViewModel.Sum(y => y.Amount) <= 0)
                {
                    throw new Exception("Amount should not be zero!");
                }
                if (vm.DetailViewModel.FindAll(x => (x.Amount == 0) && !(x.SKUID == null || x.SKUID.Key == null)).Count() > 0)
                    throw new Exception("Please enter amount for the selected product !");

                if (vm.DetailViewModel.FindAll(x => (x.Unit == null || x.Unit.Key == null) && !(x.SKUID == null || x.SKUID.Key == null)).Count() > 0)
                {
                    throw new Exception("Please select unit for the selected product !");
                }
                if (Convert.ToDouble(Math.Round(vm.MasterViewModel.TransactionHeadEntitlementMaps.Sum(x => x.Amount ?? 0), 3, MidpointRounding.AwayFromZero)) != (Math.Round(vm.DetailViewModel.Sum(y => y.Amount) - Convert.ToDouble(vm.MasterViewModel.Discount ?? 0), 3, MidpointRounding.AwayFromZero)))
                {
                    throw new Exception(" Payment Amount should be equal to the grand total!");
                }

                transaction.TransactionHead.HeadIID = vm.MasterViewModel.TransactionHeadIID;
                transaction.TransactionHead.CompanyID = vm.MasterViewModel.CompanyID;
                //transaction.TransactionHead.DocumentTypeID = Convert.ToInt32(vm.MasterViewModel.DocumentType.Key);
                transaction.TransactionHead.DocumentTypeID = vm.MasterViewModel.DocumentType != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DocumentType.Key) ? Convert.ToInt32(vm.MasterViewModel.DocumentType.Key) : (int?)null : (int?)null;
                transaction.TransactionHead.BranchID = vm.MasterViewModel.Branch != null && !string.IsNullOrEmpty(vm.MasterViewModel.Branch.Key) ? Convert.ToInt32(vm.MasterViewModel.Branch.Key) : (long?)null;
                transaction.TransactionHead.TransactionNo = vm.MasterViewModel.TransactionNo;
                transaction.TransactionHead.TransactionDate = vm.MasterViewModel.TransactionDate != null ? DateTime.ParseExact(vm.MasterViewModel.TransactionDate, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                transaction.TransactionHead.DueDate = vm.MasterViewModel.Validity != null ? DateTime.ParseExact(vm.MasterViewModel.Validity, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                transaction.TransactionHead.Description = vm.MasterViewModel.Remarks;
                transaction.TransactionHead.Reference = vm.MasterViewModel.Reference;
                transaction.TransactionHead.CurrencyID = Convert.ToInt32(vm.MasterViewModel.Currency.Key);
                transaction.TransactionHead.SupplierID = Convert.ToInt32(vm.MasterViewModel.Supplier.Key);
                //transaction.TransactionHead.DeliveryTypeID = Convert.ToInt32(vm.MasterViewModel.DeliveryType);
                transaction.TransactionHead.IsShipment = vm.MasterViewModel.IsShipment;
                //transaction.TransactionHead.EntitlementID = Convert.ToInt16(vm.MasterViewModel.Entitlement);
                transaction.TransactionHead.ReferenceHeadID = !string.IsNullOrEmpty(vm.MasterViewModel.ReferenceTransactionHeaderID) ? Convert.ToInt32(vm.MasterViewModel.ReferenceTransactionHeaderID) : (long?)null;
                transaction.TransactionHead.CreatedBy = vm.CreatedBy;
                transaction.TransactionHead.UpdatedBy = vm.UpdatedBy;
                //transaction.TransactionHead.TransactionStatusID = Convert.ToByte(vm.MasterViewModel.TransactionStatus);

                transaction.TransactionHead.DeliveryTypeID = vm.MasterViewModel.DeliveryType != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DeliveryType.Key) ? Convert.ToInt32(vm.MasterViewModel.DeliveryType.Key) : (int?)null : (int?)null;
                transaction.TransactionHead.ReturnMethodID = vm.MasterViewModel.ReturnMethod != null ? !string.IsNullOrEmpty(vm.MasterViewModel.ReturnMethod.Key) ? Convert.ToInt32(vm.MasterViewModel.ReturnMethod.Key) : (int?)null : (int?)null;

                transaction.TransactionHead.TransactionStatusID = vm.MasterViewModel.TransactionStatus != null ? !string.IsNullOrEmpty(vm.MasterViewModel.TransactionStatus.Key) ? byte.Parse(vm.MasterViewModel.TransactionStatus.Key) : (byte?)null : (byte?)null;
                transaction.TransactionHead.DocumentStatusID = vm.MasterViewModel.DocumentStatus != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DocumentStatus.Key) ? byte.Parse(vm.MasterViewModel.DocumentStatus.Key) : (byte?)null : (byte?)null;

                transaction.TransactionHead.ExchangeRate = vm.MasterViewModel.ExchangeRate;
                transaction.TransactionHead.DiscountAmount = vm.MasterViewModel.Discount.IsNotNull() ? vm.MasterViewModel.Discount : null;
                transaction.TransactionHead.DiscountPercentage = vm.MasterViewModel.DiscountPercentage.IsNotNull() ? vm.MasterViewModel.DiscountPercentage : null;
                //transaction.TransactionHead.Entitlements = vm.MasterViewModel.Entitlements.Count > 0 ? KeyValueViewModel.ToDTO(vm.MasterViewModel.Entitlements) : null;
                //transaction.TransactionHead.EntitlementID = vm.MasterViewModel.Entitlements.Count > 0 ? Convert.ToInt16(vm.MasterViewModel.Entitlements[0].Key) : (short?)null;
                // Map TransactionHeadEntitlementMaps

                if (vm.MasterViewModel.TransactionHeadEntitlementMaps.IsNotNull() && vm.MasterViewModel.TransactionHeadEntitlementMaps.Count > 0)
                {
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

                        if (transactionDetail.SKUID != null && transactionDetail.SKUID.Key != null && transactionDetail.Quantity > transactionDetail.AvailableQuantity)
                        {
                            throw new Exception("Please check the availble quantity for " + transactionDetail.Description);
                        }

                        if (transactionDetail.SKUID != null && transactionDetail.SKUID != null)
                        {
                            var transactionDetailDTO = new TransactionDetailDTO();

                            transactionDetailDTO.DetailIID = transactionDetail.TransactionDetailID;
                            transactionDetailDTO.HeadID = transactionDetail.TransactionHead;
                            transactionDetailDTO.ProductSKUMapID = Convert.ToInt32(transactionDetail.SKUID.Key);
                            transactionDetailDTO.Quantity = Convert.ToDecimal(transactionDetail.Quantity);
                            transactionDetailDTO.Amount = Convert.ToDecimal(transactionDetail.Amount);
                            transactionDetailDTO.UnitPrice = Convert.ToDecimal(transactionDetail.UnitPrice);
                            transactionDetailDTO.UnitID = transactionDetail.UnitID;
                            transactionDetailDTO.CreatedBy = transactionDetail.CreatedBy;
                            transactionDetailDTO.UpdatedBy = transactionDetail.UpdatedBy;
                            transactionDetailDTO.Fraction = Convert.ToDecimal(transactionDetail.Fraction);
                            transactionDetailDTO.ForeignAmount = Convert.ToDecimal(transactionDetail.ForeignAmount);
                            transactionDetailDTO.ForeignRate = Convert.ToDecimal(transactionDetail.ForeignRate);
                            transactionDetailDTO.ExchangeRate = Convert.ToDecimal(transactionDetail.ExchangeRate);
                            transactionDetailDTO.UnitGroupID = transactionDetail.UnitGroupID;
                            //transactionDetailDTO.CostCenterID = transactionDetail.CostCenter != null ? !string.IsNullOrEmpty(transactionDetail.CostCenter.Key) ? Convert.ToInt32(transactionDetail.CostCenter.Key) : (int?)null : (int?)null;

                            transaction.TransactionDetails.Add(transactionDetailDTO);
                        }
                    }
                }
                return transaction;
            }
            else return new TransactionDTO();
        }

        public static PurchaseReturnViewModel FromTransactionDTOToVM(TransactionDTO dto)
        {
            if (dto != null)
            {
                var purchaseReturn = new PurchaseReturnViewModel();
                purchaseReturn.MasterViewModel = new PurchaseReturnMasterViewModel();
                purchaseReturn.DetailViewModel = new List<PurchaseReturnDetailViewModel>();

                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                purchaseReturn.MasterViewModel.TransactionHeadIID = dto.TransactionHead.HeadIID;
                purchaseReturn.MasterViewModel.CompanyID = dto.TransactionHead.CompanyID;
                purchaseReturn.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = dto.TransactionHead.DocumentTypeID.ToString(), Value = dto.TransactionHead.DocumentTypeName };
                purchaseReturn.MasterViewModel.Branch = new KeyValueViewModel() { Key = dto.TransactionHead.BranchID.ToString(), Value = dto.TransactionHead.BranchName };
                purchaseReturn.MasterViewModel.TransactionNo = dto.TransactionHead.TransactionNo;
                purchaseReturn.MasterViewModel.TransactionDate = dto.TransactionHead.TransactionDate != null ? Convert.ToDateTime(dto.TransactionHead.TransactionDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                purchaseReturn.MasterViewModel.Validity = dto.TransactionHead.DueDate != null ? Convert.ToDateTime(dto.TransactionHead.DueDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                purchaseReturn.MasterViewModel.Remarks = dto.TransactionHead.Description;
                purchaseReturn.MasterViewModel.Reference = dto.TransactionHead.Reference;
                purchaseReturn.MasterViewModel.Currency = new KeyValueViewModel();
                purchaseReturn.MasterViewModel.Currency.Key = dto.TransactionHead.CurrencyID.ToString();
                purchaseReturn.MasterViewModel.Currency.Value = dto.TransactionHead.CurrencyName;
                purchaseReturn.MasterViewModel.Supplier = new KeyValueViewModel();
                purchaseReturn.MasterViewModel.Supplier.Key = dto.TransactionHead.SupplierID.ToString();
                purchaseReturn.MasterViewModel.Supplier.Value = dto.TransactionHead.SupplierName;
                //purchaseReturn.MasterViewModel.DeliveryType = dto.TransactionHead.DeliveryTypeID.ToString();
                purchaseReturn.MasterViewModel.IsShipment = dto.TransactionHead.IsShipment;
                purchaseReturn.MasterViewModel.Discount = dto.TransactionHead.DiscountAmount;
                //purchaseReturn.MasterViewModel.Entitlement = dto.TransactionHead.EntitlementID.ToString();
                purchaseReturn.MasterViewModel.ReferenceTransactionHeaderID = dto.TransactionHead.ReferenceHeadID.ToString();
                purchaseReturn.MasterViewModel.CreatedBy = dto.TransactionHead.CreatedBy;
                purchaseReturn.MasterViewModel.UpdatedBy = Convert.ToInt32(dto.TransactionHead.UpdatedBy);
                //purchaseReturn.MasterViewModel.TransactionStatus = dto.TransactionHead.TransactionStatusID.ToString();
                purchaseReturn.MasterViewModel.DocumentStatus = new KeyValueViewModel() { Key = dto.TransactionHead.DocumentStatusID.ToString(), Value = dto.TransactionHead.DocumentStatusName };
                purchaseReturn.MasterViewModel.IsTransactionCompleted = dto.TransactionHead.IsTransactionCompleted;
                purchaseReturn.MasterViewModel.ExchangeRate = dto.TransactionHead.ExchangeRate;
                purchaseReturn.MasterViewModel.Discount = dto.TransactionHead.DiscountAmount.IsNotNull() ? dto.TransactionHead.DiscountAmount : null;
                purchaseReturn.MasterViewModel.DiscountPercentage = dto.TransactionHead.DiscountPercentage.IsNotNull() ? dto.TransactionHead.DiscountPercentage : null;

                //purchaseReturn.MasterViewModel.Entitlements = dto.TransactionHead.Entitlements.Count > 0
                //    ? KeyValueViewModel.FromDTO(dto.TransactionHead.Entitlements) : null;

                if (dto.TransactionHead.DeliveryTypeID > 0)
                {
                    purchaseReturn.MasterViewModel.DeliveryType.Key = dto.TransactionHead.DeliveryTypeID.ToString();
                    purchaseReturn.MasterViewModel.DeliveryType.Value = dto.TransactionHead.DeliveryTypeName;
                }

                if (dto.TransactionHead.TransactionStatusID > 0)
                {
                    purchaseReturn.MasterViewModel.TransactionStatus.Key = dto.TransactionHead.TransactionStatusID.ToString();
                    purchaseReturn.MasterViewModel.TransactionStatus.Value = dto.TransactionHead.TransactionStatusName;
                }

                if (dto.TransactionHead.DocumentStatusID > 0)
                {
                    purchaseReturn.MasterViewModel.DocumentStatus.Key = dto.TransactionHead.DocumentStatusID.ToString();
                    purchaseReturn.MasterViewModel.DocumentStatus.Value = dto.TransactionHead.DocumentStatusName;
                }
                if (dto.TransactionHead.ReturnMethodID > 0)
                {
                    purchaseReturn.MasterViewModel.ReturnMethod = new KeyValueViewModel();
                    purchaseReturn.MasterViewModel.ReturnMethod.Key = dto.TransactionHead.ReturnMethodID.ToString();
                    purchaseReturn.MasterViewModel.ReturnMethod.Value = dto.TransactionHead.ReturnMethodName;
                }

                // Map TransactionHeadEntitlementMapViewModel
                if (dto.TransactionHead.TransactionHeadEntitlementMaps != null && dto.TransactionHead.TransactionHeadEntitlementMaps.Count > 0)
                {
                    purchaseReturn.MasterViewModel.TransactionHeadEntitlementMaps =
                        dto.TransactionHead.TransactionHeadEntitlementMaps.Select(x => TransactionHeadEntitlementMapViewModel.ToVm(x)).ToList();
                }

                if (dto.TransactionDetails != null && dto.TransactionDetails.Count > 0)
                {
                    foreach (var transactionDetail in dto.TransactionDetails)
                    {
                        var purchaseReturnDetail = new PurchaseReturnDetailViewModel();

                        purchaseReturnDetail.TransactionDetailID = transactionDetail.DetailIID;
                        purchaseReturnDetail.TransactionHead = Convert.ToInt32(transactionDetail.HeadID);
                        purchaseReturnDetail.SKUID = new KeyValueViewModel();
                        purchaseReturnDetail.SKUID.Key = transactionDetail.ProductSKUMapID.ToString();
                        purchaseReturnDetail.SKUID.Value = transactionDetail.SKU;
                        purchaseReturnDetail.Description = transactionDetail.SKU;
                        purchaseReturnDetail.Quantity = Convert.ToDouble(transactionDetail.Quantity);
                        purchaseReturnDetail.Amount = Convert.ToDouble(transactionDetail.Amount);
                        purchaseReturnDetail.UnitPrice = Convert.ToDouble(transactionDetail.UnitPrice);
                        purchaseReturnDetail.UnitID = Convert.ToInt32(transactionDetail.UnitID);
                        purchaseReturnDetail.CreatedBy = Convert.ToInt32(transactionDetail.CreatedBy);
                        purchaseReturnDetail.UpdatedBy = Convert.ToInt32(transactionDetail.UpdatedBy);
                        purchaseReturnDetail.PartNo = transactionDetail.PartNo;

                        purchaseReturnDetail.CostCenterID = transactionDetail.CostCenterID;

                        purchaseReturnDetail.Fraction = Convert.ToDouble(transactionDetail.Fraction);
                        purchaseReturnDetail.ForeignAmount = Convert.ToDecimal(transactionDetail.ForeignAmount);
                        purchaseReturnDetail.ForeignRate = Convert.ToDecimal(transactionDetail.ForeignRate);
                        purchaseReturnDetail.ExchangeRate = Convert.ToDecimal(transactionDetail.ExchangeRate);
                        purchaseReturnDetail.Unit = new KeyValueViewModel() { Key = transactionDetail.UnitID.ToString(), Value = transactionDetail.Unit };
                        //if (transactionDetail.CostCenter != null)
                        //{
                        //    purchaseReturnDetail.CostCenter = new KeyValueViewModel() { Key = transactionDetail.CostCenter.Key, Value = transactionDetail.CostCenter.Value };

                        //}
                        purchaseReturnDetail.UnitGroupID = transactionDetail.UnitGroupID;
                        purchaseReturnDetail.ProductCode = transactionDetail.ProductCode;
                        purchaseReturnDetail.UnitDTO = new List<KeyValueViewModel>();
                        if (transactionDetail.UnitDTO != null && transactionDetail.UnitDTO.Count > 0)
                        {
                            foreach (KeyValueDTO val in transactionDetail.UnitDTO)
                            {
                                purchaseReturnDetail.UnitDTO.Add(new KeyValueViewModel()
                                {
                                    Key = val.Key,
                                    Value = val.Value
                                });
                            }
                        }
                        purchaseReturnDetail.UnitList = new List<UnitsViewModel>();
                        if (transactionDetail.UnitList != null && transactionDetail.UnitList.Count > 0)
                        {
                            foreach (var val in transactionDetail.UnitList)
                            {
                                purchaseReturnDetail.UnitList.Add(new UnitsViewModel()
                                {
                                    UnitID = val.UnitID,
                                    UnitGroupID = val.UnitGroupID,
                                    UnitCode = val.UnitCode,
                                    UnitName = val.UnitName,
                                    Fraction = val.Fraction
                                });
                            }
                        }

                        purchaseReturn.DetailViewModel.Add(purchaseReturnDetail);
                    }
                }

                return purchaseReturn;
            }
            else return new PurchaseReturnViewModel();
        }
    }
}
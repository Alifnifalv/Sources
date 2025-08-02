using Eduegate.Domain;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Catalog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Eduegate.Web.Library.ViewModels.Inventory.Purchase
{
    public class ServiceEntryViewModel : BaseMasterViewModel
    {
        public ServiceEntryViewModel()
        {
            MasterViewModel = new ServiceEntryMasterViewModel();
            DetailViewModel = new List<ServiceEntryDetailViewModel>() { new ServiceEntryDetailViewModel() };
        }
        public ServiceEntryMasterViewModel MasterViewModel { get; set; }
        public List<ServiceEntryDetailViewModel> DetailViewModel { get; set; }


        public static TransactionDTO FromVMToTransactionDTO(ServiceEntryViewModel vm)
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

                if (vm.DetailViewModel.FindAll(x => x.SKUID != null).Count() <= 0)
                    throw new Exception("Please enter product details!");

                if (vm.DetailViewModel.Sum(y => y.Amount) <= 0)
                {
                    throw new Exception("Amount should not be zero!");
                }
                if (vm.DetailViewModel.FindAll(x => (x.Amount == 0) && !(x.SKUID == null || x.SKUID.Key == null)).Count() > 0)
                {
                    throw new Exception("Amount should not be zero for the selected product !");
                }

                if (vm.DetailViewModel.FindAll(x => (x.Unit == null || x.Unit.Key == null) && !(x.SKUID == null || x.SKUID.Key == null)).Count() > 0)
                {
                    throw new Exception("Please select unit for the selected product !");
                }
                transaction.TransactionHead.HeadIID = vm.MasterViewModel.TransactionHeadIID;
                transaction.TransactionHead.CompanyID = vm.MasterViewModel.CompanyID;
                //transaction.TransactionHead.DocumentTypeID = Convert.ToInt32(vm.MasterViewModel.DocumentType.Key);
                transaction.TransactionHead.DocumentTypeID = vm.MasterViewModel.DocumentType != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DocumentType.Key) ? Convert.ToInt32(vm.MasterViewModel.DocumentType.Key) : (int?)null : (int?)null;
                transaction.TransactionHead.BranchID = vm.MasterViewModel.Branch != null && !string.IsNullOrEmpty(vm.MasterViewModel.Branch.Key) ? long.Parse(vm.MasterViewModel.Branch.Key) : (long?)null;
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
                transaction.TransactionHead.ExchangeRate = Convert.ToDecimal(vm.MasterViewModel.ExchangeRate);

                transaction.TransactionHead.DiscountAmount = vm.MasterViewModel.Discount;
                transaction.TransactionHead.DiscountPercentage = vm.MasterViewModel.DiscountPercentage;
                transaction.TransactionHead.LocalDiscount = vm.MasterViewModel.LocalDiscount;
                //transaction.TransactionHead.Entitlements = vm.MasterViewModel.Entitlements.Count > 0 ? KeyValueViewModel.ToDTO(vm.MasterViewModel.Entitlements) : null;
                //transaction.TransactionHead.EntitlementID = vm.MasterViewModel.Entitlements.Count > 0 ? Convert.ToInt16(vm.MasterViewModel.Entitlements[0].Key) : (short?)null;
                // Map TransactionHeadEntitlementMaps

                //if (vm.MasterViewModel.TransactionHeadEntitlementMaps.IsNotNull() && vm.MasterViewModel.TransactionHeadEntitlementMaps.Count > 0)
                //{
                //    foreach (var item in vm.MasterViewModel.TransactionHeadEntitlementMaps)
                //    {
                //        if (item.Entitlement.IsNotNull() && !string.IsNullOrWhiteSpace(item.Entitlement.Key))
                //        {
                //            var transactionHeadEntitlementMapDTO = new TransactionHeadEntitlementMapDTO();
                //            transactionHeadEntitlementMapDTO.TransactionHeadEntitlementMapID = item.TransactionHeadEntitlementMapID;
                //            transactionHeadEntitlementMapDTO.TransactionHeadID = vm.MasterViewModel.TransactionHeadIID;
                //            transactionHeadEntitlementMapDTO.EntitlementID = item.Entitlement != null ? Convert.ToByte(item.Entitlement.Key) : default(byte);
                //            transactionHeadEntitlementMapDTO.EntitlementName = item.Entitlement != null ? item.Entitlement.Value : null;
                //            transactionHeadEntitlementMapDTO.Amount = item.Amount;

                //            // add into TransactionHeadEntitlementMaps list
                //            transaction.TransactionHead.TransactionHeadEntitlementMaps.Add(transactionHeadEntitlementMapDTO);
                //        }
                //    }
                //}

                transaction.IgnoreEntitlementCheck = true;

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
                          
                            transactionDetailDTO.CreatedBy = transactionDetail.CreatedBy;
                            transactionDetailDTO.UpdatedBy = transactionDetail.UpdatedBy;

                            //transactionDetailDTO.CostCenterID = transactionDetail.CostCenter != null ? !string.IsNullOrEmpty(transactionDetail.CostCenter.Key) ? Convert.ToInt32(transactionDetail.CostCenter.Key) : (int?)null : (int?)null;
                            transactionDetailDTO.WarrantyStartDate = transactionDetail.WarrantyStartDateString != null ? DateTime.ParseExact(transactionDetail.WarrantyStartDateString, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                            transactionDetailDTO.WarrantyEndDate = transactionDetail.WarrantyEndDateString != null ? DateTime.ParseExact(transactionDetail.WarrantyEndDateString, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                            transactionDetailDTO.UnitID = transactionDetail.UnitID;
                            transactionDetailDTO.Fraction = Convert.ToDecimal(transactionDetail.Fraction);
                            transactionDetailDTO.ForeignAmount = Convert.ToDecimal(transactionDetail.ForeignAmount);
                            transactionDetailDTO.ForeignRate = Convert.ToDecimal(transactionDetail.ForeignRate);
                            transactionDetailDTO.ExchangeRate = Convert.ToDecimal(transactionDetail.ExchangeRate);
                            transactionDetailDTO.UnitGroupID = transactionDetail.UnitGroupID;
                            transaction.TransactionDetails.Add(transactionDetailDTO);
                        }
                    }
                }
                return transaction;
            }
            else return new TransactionDTO();
        }

        public static ServiceEntryViewModel FromTransactionDTOToVM(TransactionDTO dto)
        {
            if (dto != null)
            {
                var serviceEntry = new ServiceEntryViewModel();
                serviceEntry.MasterViewModel = new ServiceEntryMasterViewModel();
                serviceEntry.DetailViewModel = new List<ServiceEntryDetailViewModel>();

                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                serviceEntry.MasterViewModel.TransactionHeadIID = dto.TransactionHead.HeadIID;
                serviceEntry.MasterViewModel.CompanyID = dto.TransactionHead.CompanyID;
                serviceEntry.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = dto.TransactionHead.DocumentTypeID.ToString(), Value = dto.TransactionHead.DocumentTypeName };
                serviceEntry.MasterViewModel.Branch = new KeyValueViewModel() { Key = dto.TransactionHead.BranchID.ToString(), Value = dto.TransactionHead.BranchName };
                serviceEntry.MasterViewModel.TransactionNo = dto.TransactionHead.TransactionNo;
                serviceEntry.MasterViewModel.TransactionDate = dto.TransactionHead.TransactionDate != null ? Convert.ToDateTime(dto.TransactionHead.TransactionDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                serviceEntry.MasterViewModel.Validity = dto.TransactionHead.DueDate != null ? Convert.ToDateTime(dto.TransactionHead.DueDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                serviceEntry.MasterViewModel.Remarks = dto.TransactionHead.Description;
                serviceEntry.MasterViewModel.Reference = dto.TransactionHead.Reference;
                serviceEntry.MasterViewModel.Currency = new KeyValueViewModel();
                serviceEntry.MasterViewModel.Currency.Key = dto.TransactionHead.CurrencyID.ToString();
                serviceEntry.MasterViewModel.Currency.Value = dto.TransactionHead.CurrencyName;
                serviceEntry.MasterViewModel.Supplier = new KeyValueViewModel();
                serviceEntry.MasterViewModel.Supplier.Key = dto.TransactionHead.SupplierID.ToString();
                serviceEntry.MasterViewModel.Supplier.Value = dto.TransactionHead.SupplierName;
                //serviceEntry.MasterViewModel.DeliveryType = dto.TransactionHead.DeliveryTypeID.ToString();
                serviceEntry.MasterViewModel.IsShipment = dto.TransactionHead.IsShipment;
                //serviceEntry.MasterViewModel.Entitlement = dto.TransactionHead.EntitlementID.ToString();
                serviceEntry.MasterViewModel.ReferenceTransactionHeaderID = dto.TransactionHead.ReferenceHeadID.ToString();
                serviceEntry.MasterViewModel.CreatedBy = dto.TransactionHead.CreatedBy;
                serviceEntry.MasterViewModel.UpdatedBy = Convert.ToInt32(dto.TransactionHead.UpdatedBy);
                //serviceEntry.MasterViewModel.TransactionStatus = dto.TransactionHead.TransactionStatusID.ToString();
                serviceEntry.MasterViewModel.DocumentStatus = new KeyValueViewModel() { Key = dto.TransactionHead.DocumentStatusID.ToString(), Value = dto.TransactionHead.DocumentStatusName };
                serviceEntry.MasterViewModel.IsTransactionCompleted = dto.TransactionHead.IsTransactionCompleted;

                serviceEntry.MasterViewModel.ExchangeRate = Convert.ToDecimal(dto.TransactionHead.ExchangeRate);

                serviceEntry.MasterViewModel.Discount = dto.TransactionHead.DiscountAmount;
                serviceEntry.MasterViewModel.DiscountPercentage = dto.TransactionHead.DiscountPercentage;
                serviceEntry.MasterViewModel.LocalDiscount = dto.TransactionHead.LocalDiscount;
                //serviceEntry.MasterViewModel.Entitlements = dto.TransactionHead.Entitlements.Count > 0
                //    ? KeyValueViewModel.FromDTO(dto.TransactionHead.Entitlements) : null;

                if (dto.TransactionHead.DeliveryTypeID > 0)
                {
                    serviceEntry.MasterViewModel.DeliveryType.Key = dto.TransactionHead.DeliveryTypeID.ToString();
                    serviceEntry.MasterViewModel.DeliveryType.Value = dto.TransactionHead.DeliveryTypeName;
                }

                if (dto.TransactionHead.TransactionStatusID > 0)
                {
                    serviceEntry.MasterViewModel.TransactionStatus.Key = dto.TransactionHead.TransactionStatusID.ToString();
                    serviceEntry.MasterViewModel.TransactionStatus.Value = dto.TransactionHead.TransactionStatusName;
                }

                if (dto.TransactionHead.DocumentStatusID > 0)
                {
                    serviceEntry.MasterViewModel.DocumentStatus.Key = dto.TransactionHead.DocumentStatusID.ToString();
                    serviceEntry.MasterViewModel.DocumentStatus.Value = dto.TransactionHead.DocumentStatusName;
                }
                if (dto.TransactionHead.ReturnMethodID > 0)
                {
                    serviceEntry.MasterViewModel.ReturnMethod = new KeyValueViewModel();
                    serviceEntry.MasterViewModel.ReturnMethod.Key = dto.TransactionHead.ReturnMethodID.ToString();
                    serviceEntry.MasterViewModel.ReturnMethod.Value = dto.TransactionHead.ReturnMethodName;
                }

                // Map TransactionHeadEntitlementMapViewModel
                if (dto.TransactionHead.TransactionHeadEntitlementMaps != null && dto.TransactionHead.TransactionHeadEntitlementMaps.Count > 0)
                {
                    serviceEntry.MasterViewModel.TransactionHeadEntitlementMaps =
                        dto.TransactionHead.TransactionHeadEntitlementMaps.Select(x => TransactionHeadEntitlementMapViewModel.ToVm(x)).ToList();
                }
                else
                {
                    serviceEntry.MasterViewModel.TransactionHeadEntitlementMaps = new List<TransactionHeadEntitlementMapViewModel>() { new TransactionHeadEntitlementMapViewModel() };
                }

                if (dto.TransactionDetails != null && dto.TransactionDetails.Count > 0)
                {
                    foreach (var transactionDetail in dto.TransactionDetails)
                    {
                        var serviceEntryDetail = new ServiceEntryDetailViewModel();

                        serviceEntryDetail.TransactionDetailID = transactionDetail.DetailIID;
                        serviceEntryDetail.TransactionHead = Convert.ToInt32(transactionDetail.HeadID);
                        serviceEntryDetail.SKUID = new KeyValueViewModel();
                        serviceEntryDetail.SKUID.Key = transactionDetail.ProductSKUMapID.ToString();
                        serviceEntryDetail.SKUID.Value = transactionDetail.SKU;
                        serviceEntryDetail.Description = transactionDetail.SKU;
                        serviceEntryDetail.Quantity = Convert.ToDouble(transactionDetail.Quantity);
                        serviceEntryDetail.Amount = Convert.ToDouble(transactionDetail.Amount);
                        serviceEntryDetail.UnitPrice = Convert.ToDouble(transactionDetail.UnitPrice);
                        serviceEntryDetail.UnitID = Convert.ToInt32(transactionDetail.UnitID);
                        serviceEntryDetail.CreatedBy = Convert.ToInt32(transactionDetail.CreatedBy);
                        serviceEntryDetail.UpdatedBy = Convert.ToInt32(transactionDetail.UpdatedBy);
                        serviceEntryDetail.PartNo = transactionDetail.PartNo;

                        serviceEntryDetail.CostCenterID = transactionDetail.CostCenterID;

                        serviceEntryDetail.Fraction = Convert.ToDouble(transactionDetail.Fraction);
                        serviceEntryDetail.ForeignAmount = Convert.ToDecimal(transactionDetail.ForeignAmount);
                        serviceEntryDetail.ForeignRate = Convert.ToDecimal(transactionDetail.ForeignRate);
                        serviceEntryDetail.ExchangeRate = Convert.ToDecimal(transactionDetail.ExchangeRate);
                        serviceEntryDetail.Unit = new KeyValueViewModel() { Key = transactionDetail.UnitID.ToString(), Value = transactionDetail.Unit };
                        serviceEntryDetail.UnitGroupID = transactionDetail.UnitGroupID;
                        serviceEntryDetail.ProductCode = transactionDetail.ProductCode;
                        serviceEntryDetail.UnitDTO = new List<KeyValueViewModel>();
                        if (transactionDetail.UnitDTO != null && transactionDetail.UnitDTO.Count > 0)
                        {
                            foreach (KeyValueDTO val in transactionDetail.UnitDTO)
                            {
                                serviceEntryDetail.UnitDTO.Add(new KeyValueViewModel()
                                {
                                    Key = val.Key,
                                    Value = val.Value
                                });
                            }
                        }
                        serviceEntryDetail.UnitList = new List<UnitsViewModel>();
                        if (transactionDetail.UnitList != null && transactionDetail.UnitList.Count > 0)
                        {
                            foreach (var val in transactionDetail.UnitList)
                            {
                                serviceEntryDetail.UnitList.Add(new UnitsViewModel()
                                {
                                    UnitID = val.UnitID,
                                    UnitGroupID = val.UnitGroupID,
                                    UnitCode = val.UnitCode,
                                    UnitName = val.UnitName,
                                    Fraction = val.Fraction
                                });
                            }
                        }

                        serviceEntryDetail.WarrantyStartDateString = transactionDetail.WarrantyStartDate != null ? Convert.ToDateTime(transactionDetail.WarrantyStartDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                        serviceEntryDetail.WarrantyEndDateString = transactionDetail.WarrantyEndDate != null ? Convert.ToDateTime(transactionDetail.WarrantyEndDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;

                        serviceEntry.DetailViewModel.Add(serviceEntryDetail);
                    }
                }

                return serviceEntry;
            }
            else return new ServiceEntryViewModel();
        }
    }
}
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Catalog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Eduegate.Web.Library.ViewModels.Inventory.Purchase
{
    public class GoodsReceivedNoteViewModel : BaseMasterViewModel
    {
        public GoodsReceivedNoteViewModel()
        {
            MasterViewModel = new GoodsReceivedNoteMasterViewModel();
            DetailViewModel = new List<GoodsReceivedNoteDetailViewModel>() { new GoodsReceivedNoteDetailViewModel() };
        }
        public GoodsReceivedNoteMasterViewModel MasterViewModel { get; set; }
        public List<GoodsReceivedNoteDetailViewModel> DetailViewModel { get; set; }


        public static TransactionDTO FromVMToTransactionDTO(GoodsReceivedNoteViewModel vm)
        {
            if (vm != null)
            {
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
                var transaction = new TransactionDTO();
                transaction.TransactionHead = new TransactionHeadDTO();

                if (vm.MasterViewModel.Branch.Key == null)
                {
                    throw new Exception("Select branch!");
                }

                if (vm.MasterViewModel.DocumentType.Key == null)
                {
                    throw new Exception("Select document type!");
                }

                if (vm.MasterViewModel.Currency.Key == null)
                {
                    throw new Exception("Select any currency!");
                }

                if (vm.MasterViewModel.Supplier.Key == null)
                {
                    throw new Exception("Select supplier!");
                }

                if (vm.MasterViewModel.DocumentStatus.Key == null)
                {
                    throw new Exception("Select document status!");
                }

                if (vm.DetailViewModel.FindAll(x => x.SKUID != null).Count() <= 0)
                {
                    throw new Exception("Please enter product details!");
                }

                if (vm.DetailViewModel.Sum(y => y.Amount) <= 0)
                {
                    throw new Exception("Amount should not be zero!");
                }

                if (vm.DetailViewModel.FindAll(x => (x.Amount == 0) && !(x.SKUID == null || x.SKUID.Key == null)).Count() > 0)
                {
                    throw new Exception("Please enter amount for the selected product !");
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
                transaction.TransactionHead.DiscountAmount = vm.MasterViewModel.Discount.IsNotNull() ? vm.MasterViewModel.Discount : null;
                transaction.TransactionHead.DeliveryTypeID = vm.MasterViewModel.DeliveryType != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DeliveryType.Key) ? Convert.ToInt32(vm.MasterViewModel.DeliveryType.Key) : (int?)null : (int?)null;
                transaction.TransactionHead.ReturnMethodID = vm.MasterViewModel.ReturnMethod != null ? !string.IsNullOrEmpty(vm.MasterViewModel.ReturnMethod.Key) ? Convert.ToInt32(vm.MasterViewModel.ReturnMethod.Key) : (int?)null : (int?)null;
                transaction.TransactionHead.DiscountPercentage = vm.MasterViewModel.DiscountPercentage.IsNotNull() ? vm.MasterViewModel.DiscountPercentage : null;
                transaction.TransactionHead.TransactionStatusID = vm.MasterViewModel.TransactionStatus != null ? !string.IsNullOrEmpty(vm.MasterViewModel.TransactionStatus.Key) ? byte.Parse(vm.MasterViewModel.TransactionStatus.Key) : (byte?)null : (byte?)null;
                transaction.TransactionHead.DocumentStatusID = vm.MasterViewModel.DocumentStatus != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DocumentStatus.Key) ? byte.Parse(vm.MasterViewModel.DocumentStatus.Key) : (byte?)null : (byte?)null;
                transaction.TransactionHead.ExchangeRate = vm.MasterViewModel.ExchangeRate;

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
                        if (transactionDetail.SKUID != null && transactionDetail.Quantity == 0)
                        {
                            throw new Exception("Quantity should not be zero for "+ transactionDetail.Description);
                        }

                        if (transactionDetail.SKUID != null && transactionDetail.ForeignRate == 0 || transactionDetail.ForeignRate == null)
                        {
                            throw new Exception("ForeignRate should not be zero for " + transactionDetail.Description);
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
                            
                            transactionDetailDTO.CreatedBy = transactionDetail.CreatedBy;
                            transactionDetailDTO.UpdatedBy = transactionDetail.UpdatedBy;

                           // transactionDetailDTO.CostCenterID = transactionDetail.CostCenter != null ? !string.IsNullOrEmpty(transactionDetail.CostCenter.Key) ? Convert.ToInt32(transactionDetail.CostCenter.Key) : (int?)null : (int?)null;
                            transactionDetailDTO.WarrantyStartDate = transactionDetail.WarrantyStartDateString != null ? DateTime.ParseExact(transactionDetail.WarrantyStartDateString, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                            transactionDetailDTO.WarrantyEndDate = transactionDetail.WarrantyEndDateString != null ? DateTime.ParseExact(transactionDetail.WarrantyEndDateString, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                            transactionDetailDTO.UnitID = transactionDetail.UnitID;
                            transactionDetailDTO.Fraction = Convert.ToDecimal(transactionDetail.Fraction);
                            transactionDetailDTO.ForeignRate = (decimal?)transactionDetail.ForeignRate;
                            transactionDetailDTO.ForeignAmount = transactionDetail.ForeignAmount;
                            transactionDetailDTO.ExchangeRate = transactionDetail.ExchangeRate;
                            transactionDetailDTO.UnitGroupID = transactionDetail.UnitGroupID;
                            transaction.TransactionDetails.Add(transactionDetailDTO);
                        }
                    }
                }
                return transaction;
            }
            else return new TransactionDTO();
        }

        public static GoodsReceivedNoteViewModel FromTransactionDTOToVM(TransactionDTO dto)
        {
            if (dto != null)
            {
                var goodsReceiveNote = new GoodsReceivedNoteViewModel();
                goodsReceiveNote.MasterViewModel = new GoodsReceivedNoteMasterViewModel();
                goodsReceiveNote.DetailViewModel = new List<GoodsReceivedNoteDetailViewModel>();

                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                goodsReceiveNote.MasterViewModel.TransactionHeadIID = dto.TransactionHead.HeadIID;
                goodsReceiveNote.MasterViewModel.CompanyID = dto.TransactionHead.CompanyID;
                goodsReceiveNote.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = dto.TransactionHead.DocumentTypeID.ToString(), Value = dto.TransactionHead.DocumentTypeName };
                goodsReceiveNote.MasterViewModel.Branch = new KeyValueViewModel() { Key = dto.TransactionHead.BranchID.ToString(), Value = dto.TransactionHead.BranchName };
                goodsReceiveNote.MasterViewModel.TransactionNo = dto.TransactionHead.TransactionNo;
                goodsReceiveNote.MasterViewModel.TransactionDate = dto.TransactionHead.TransactionDate != null ? Convert.ToDateTime(dto.TransactionHead.TransactionDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                goodsReceiveNote.MasterViewModel.Validity = dto.TransactionHead.DueDate != null ? Convert.ToDateTime(dto.TransactionHead.DueDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                goodsReceiveNote.MasterViewModel.Remarks = dto.TransactionHead.Description;
                goodsReceiveNote.MasterViewModel.Reference = dto.TransactionHead.Reference;
                goodsReceiveNote.MasterViewModel.Currency = new KeyValueViewModel();
                goodsReceiveNote.MasterViewModel.Currency.Key = dto.TransactionHead.CurrencyID.ToString();
                goodsReceiveNote.MasterViewModel.Currency.Value = dto.TransactionHead.CurrencyName;
                goodsReceiveNote.MasterViewModel.Supplier = new KeyValueViewModel();
                goodsReceiveNote.MasterViewModel.Supplier.Key = dto.TransactionHead.SupplierID.ToString();
                goodsReceiveNote.MasterViewModel.Supplier.Value = dto.TransactionHead.SupplierName;
                //goodsReceiveNote.MasterViewModel.DeliveryType = dto.TransactionHead.DeliveryTypeID.ToString();
                goodsReceiveNote.MasterViewModel.IsShipment = dto.TransactionHead.IsShipment;
                //goodsReceiveNote.MasterViewModel.Entitlement = dto.TransactionHead.EntitlementID.ToString();
                goodsReceiveNote.MasterViewModel.ReferenceTransactionHeaderID = dto.TransactionHead.ReferenceHeadID.ToString();
                goodsReceiveNote.MasterViewModel.CreatedBy = dto.TransactionHead.CreatedBy;
                goodsReceiveNote.MasterViewModel.UpdatedBy = Convert.ToInt32(dto.TransactionHead.UpdatedBy);
                //goodsReceiveNote.MasterViewModel.TransactionStatus = dto.TransactionHead.TransactionStatusID.ToString();
                goodsReceiveNote.MasterViewModel.DocumentStatus = new KeyValueViewModel() { Key = dto.TransactionHead.DocumentStatusID.ToString(), Value = dto.TransactionHead.DocumentStatusName };
                goodsReceiveNote.MasterViewModel.IsTransactionCompleted = dto.TransactionHead.IsTransactionCompleted;
                goodsReceiveNote.MasterViewModel.Discount = dto.TransactionHead.DiscountAmount.IsNotNull() ? dto.TransactionHead.DiscountAmount : null;
                goodsReceiveNote.MasterViewModel.ExchangeRate = dto.TransactionHead.ExchangeRate;
                goodsReceiveNote.MasterViewModel.DiscountPercentage = dto.TransactionHead.DiscountPercentage.IsNotNull() ? dto.TransactionHead.DiscountPercentage : null;

                //goodsReceiveNote.MasterViewModel.Entitlements = dto.TransactionHead.Entitlements.Count > 0
                //    ? KeyValueViewModel.FromDTO(dto.TransactionHead.Entitlements) : null;

                if (dto.TransactionHead.DeliveryTypeID > 0)
                {
                    goodsReceiveNote.MasterViewModel.DeliveryType.Key = dto.TransactionHead.DeliveryTypeID.ToString();
                    goodsReceiveNote.MasterViewModel.DeliveryType.Value = dto.TransactionHead.DeliveryTypeName;
                }

                if (dto.TransactionHead.TransactionStatusID > 0)
                {
                    goodsReceiveNote.MasterViewModel.TransactionStatus.Key = dto.TransactionHead.TransactionStatusID.ToString();
                    goodsReceiveNote.MasterViewModel.TransactionStatus.Value = dto.TransactionHead.TransactionStatusName;
                }

                if (dto.TransactionHead.DocumentStatusID > 0)
                {
                    goodsReceiveNote.MasterViewModel.DocumentStatus.Key = dto.TransactionHead.DocumentStatusID.ToString();
                    goodsReceiveNote.MasterViewModel.DocumentStatus.Value = dto.TransactionHead.DocumentStatusName;
                }
                if (dto.TransactionHead.ReturnMethodID > 0)
                {
                    goodsReceiveNote.MasterViewModel.ReturnMethod = new KeyValueViewModel();
                    goodsReceiveNote.MasterViewModel.ReturnMethod.Key = dto.TransactionHead.ReturnMethodID.ToString();
                    goodsReceiveNote.MasterViewModel.ReturnMethod.Value = dto.TransactionHead.ReturnMethodName;
                }

                // Map TransactionHeadEntitlementMapViewModel
                if (dto.TransactionHead.TransactionHeadEntitlementMaps != null && dto.TransactionHead.TransactionHeadEntitlementMaps.Count > 0)
                {
                    goodsReceiveNote.MasterViewModel.TransactionHeadEntitlementMaps =
                        dto.TransactionHead.TransactionHeadEntitlementMaps.Select(x => TransactionHeadEntitlementMapViewModel.ToVm(x)).ToList();
                }
                else
                {
                    goodsReceiveNote.MasterViewModel.TransactionHeadEntitlementMaps = new List<TransactionHeadEntitlementMapViewModel>() { new TransactionHeadEntitlementMapViewModel() };
                }

                if (dto.TransactionDetails != null && dto.TransactionDetails.Count > 0)
                {
                    foreach (var transactionDetail in dto.TransactionDetails)
                    {
                        var goodsReceiveNoteDetail = new GoodsReceivedNoteDetailViewModel();

                        goodsReceiveNoteDetail.TransactionDetailID = transactionDetail.DetailIID;
                        goodsReceiveNoteDetail.TransactionHead = Convert.ToInt32(transactionDetail.HeadID);
                        goodsReceiveNoteDetail.SKUID = new KeyValueViewModel();
                        goodsReceiveNoteDetail.SKUID.Key = transactionDetail.ProductSKUMapID.ToString();
                        goodsReceiveNoteDetail.SKUID.Value = transactionDetail.SKU;
                        goodsReceiveNoteDetail.Description = transactionDetail.SKU;
                        goodsReceiveNoteDetail.Quantity = Convert.ToDouble(transactionDetail.Quantity);
                        goodsReceiveNoteDetail.Amount = Convert.ToDouble(transactionDetail.Amount);
                        goodsReceiveNoteDetail.UnitPrice = Convert.ToDouble(transactionDetail.UnitPrice);
                       
                        goodsReceiveNoteDetail.CreatedBy = Convert.ToInt32(transactionDetail.CreatedBy);
                        goodsReceiveNoteDetail.UpdatedBy = Convert.ToInt32(transactionDetail.UpdatedBy);
                        goodsReceiveNoteDetail.PartNo = transactionDetail.PartNo;

                        goodsReceiveNoteDetail.UnitID = Convert.ToInt32(transactionDetail.UnitID);
                        goodsReceiveNoteDetail.Fraction = Convert.ToDouble(transactionDetail.Fraction);
                        goodsReceiveNoteDetail.ForeignRate = (float?)transactionDetail.ForeignRate;
                        goodsReceiveNoteDetail.ForeignAmount = transactionDetail.ForeignAmount;
                        goodsReceiveNoteDetail.ExchangeRate = transactionDetail.ExchangeRate;
                        goodsReceiveNoteDetail.Unit = new KeyValueViewModel() { Key = transactionDetail.UnitID.ToString(), Value = transactionDetail.Unit };
                       
                        goodsReceiveNoteDetail.CostCenterID = transactionDetail.CostCenterID;

                        //if (transactionDetail.CostCenter != null)
                        //{
                        //    goodsReceiveNoteDetail.CostCenter = new KeyValueViewModel() { Key = transactionDetail.CostCenter.Key, Value = transactionDetail.CostCenter.Value };

                        //}
                        goodsReceiveNoteDetail.UnitGroupID = transactionDetail.UnitGroupID;
                        goodsReceiveNoteDetail.ProductCode = transactionDetail.ProductCode;
                        goodsReceiveNoteDetail.UnitDTO = new List<KeyValueViewModel>();
                        if (transactionDetail.UnitDTO!=null && transactionDetail.UnitDTO.Count > 0)
                        {
                            foreach (KeyValueDTO val in transactionDetail.UnitDTO)
                            {
                                goodsReceiveNoteDetail.UnitDTO.Add(new KeyValueViewModel()
                                {
                                    Key = val.Key,
                                    Value = val.Value
                                });
                            }
                        }
                        goodsReceiveNoteDetail.UnitList = new List<UnitsViewModel>();
                        if (transactionDetail.UnitList !=null && transactionDetail.UnitList.Count > 0)
                        {
                            foreach (var val in transactionDetail.UnitList)
                            {
                                goodsReceiveNoteDetail.UnitList.Add(new UnitsViewModel()
                                {
                                    UnitID = val.UnitID,
                                    UnitGroupID = val.UnitGroupID,
                                    UnitCode = val.UnitCode,
                                    UnitName = val.UnitName,
                                    Fraction = val.Fraction
                                });
                            }
                        }

                        goodsReceiveNoteDetail.WarrantyStartDateString = transactionDetail.WarrantyStartDate != null ? Convert.ToDateTime(transactionDetail.WarrantyStartDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                        goodsReceiveNoteDetail.WarrantyEndDateString = transactionDetail.WarrantyEndDate != null ? Convert.ToDateTime(transactionDetail.WarrantyEndDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;

                        goodsReceiveNote.DetailViewModel.Add(goodsReceiveNoteDetail);
                    }
                }

                return goodsReceiveNote;
            }
            else return new GoodsReceivedNoteViewModel();
        }
    }
}
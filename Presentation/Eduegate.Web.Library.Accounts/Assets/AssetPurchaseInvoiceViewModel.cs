using System;
using System.Collections.Generic;
using System.Globalization;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Accounts.Assets;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Web.Library.ViewModels;

namespace Eduegate.Web.Library.Accounts.Assets
{
    public class AssetPurchaseInvoiceViewModel : BaseMasterViewModel
    {
        public AssetPurchaseInvoiceViewModel() 
        {
            MasterViewModel = new AssetPurchaseInvoiceMasterViewModel();
            DetailViewModel = new List<AssetPurchaseInvoiceDetailViewModel>() { new AssetPurchaseInvoiceDetailViewModel() };
        }

        public AssetPurchaseInvoiceMasterViewModel MasterViewModel { get; set; }
        public List<AssetPurchaseInvoiceDetailViewModel> DetailViewModel { get; set; }

        public static AssetPurchaseInvoiceViewModel ToVM(AssetTransactionDTO dto)
        {
            if (dto != null)
            {
                var assetPurchaseInvoice = new AssetPurchaseInvoiceViewModel();
                assetPurchaseInvoice.MasterViewModel = new AssetPurchaseInvoiceMasterViewModel();
                assetPurchaseInvoice.DetailViewModel = new List<AssetPurchaseInvoiceDetailViewModel>();

                Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
                Mapper<AssetTransactionHeadDTO, AssetPurchaseInvoiceMasterViewModel>.CreateMap();
                assetPurchaseInvoice.MasterViewModel = Mapper<AssetTransactionHeadDTO, AssetPurchaseInvoiceMasterViewModel>.Map(dto.AssetTransactionHead);
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                assetPurchaseInvoice.MasterViewModel.TransactionHeadIID = dto.AssetTransactionHead.HeadIID;
                assetPurchaseInvoice.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = dto.AssetTransactionHead.DocumentTypeID.ToString(), Value = dto.AssetTransactionHead.DocumentTypeName };
                assetPurchaseInvoice.MasterViewModel.Branch = new KeyValueViewModel() { Key = dto.AssetTransactionHead.BranchID.ToString(), Value = dto.AssetTransactionHead.BranchName }; ;
                assetPurchaseInvoice.MasterViewModel.TransactionNo = dto.AssetTransactionHead.TransactionNo;
                assetPurchaseInvoice.MasterViewModel.TransactionDateString = dto.AssetTransactionHead.EntryDate.HasValue ?  dto.AssetTransactionHead.EntryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                assetPurchaseInvoice.MasterViewModel.Remarks = dto.AssetTransactionHead.Remarks;
                assetPurchaseInvoice.MasterViewModel.Supplier = dto.AssetTransactionHead.SupplierID.HasValue ? new KeyValueViewModel()
                {
                    Key = dto.AssetTransactionHead.SupplierID.ToString(),
                    Value = dto.AssetTransactionHead.SupplierName
                } : new KeyValueViewModel();
                assetPurchaseInvoice.MasterViewModel.IsTransactionCompleted = dto.AssetTransactionHead.IsTransactionCompleted;
                assetPurchaseInvoice.MasterViewModel.POReferenceTransactionHeadID = dto.AssetTransactionHead.ReferenceHeadID.ToString();
                assetPurchaseInvoice.MasterViewModel.ErrorCode = dto.ErrorCode;

                assetPurchaseInvoice.MasterViewModel.CreatedBy = dto.AssetTransactionHead.CreatedBy;
                assetPurchaseInvoice.MasterViewModel.CreatedDate = dto.AssetTransactionHead.CreatedDate;
                assetPurchaseInvoice.MasterViewModel.UpdatedBy = dto.AssetTransactionHead.UpdatedBy;
                assetPurchaseInvoice.MasterViewModel.UpdatedDate = dto.AssetTransactionHead.UpdatedDate;

                if (dto.AssetTransactionHead.ProcessingStatusID > 0)
                {
                    assetPurchaseInvoice.MasterViewModel.TransactionStatus.Key = dto.AssetTransactionHead.ProcessingStatusID.ToString();
                    assetPurchaseInvoice.MasterViewModel.TransactionStatus.Value = dto.AssetTransactionHead.ProcessingStatusName;
                }

                if (dto.AssetTransactionHead.DocumentStatusID.HasValue)
                {
                    assetPurchaseInvoice.MasterViewModel.DocumentStatus.Key = dto.AssetTransactionHead.DocumentStatusID.ToString();
                    assetPurchaseInvoice.MasterViewModel.DocumentStatus.Value = dto.AssetTransactionHead.DocumentStatusName;
                }

                if (dto.AssetTransactionDetails != null && dto.AssetTransactionDetails.Count > 0)
                {
                    foreach (var transactionDetail in dto.AssetTransactionDetails)
                    {
                        var astDetail = new AssetPurchaseInvoiceDetailViewModel();

                        astDetail.TransactionDetailID = transactionDetail.DetailIID;
                        astDetail.TransactionHead = Convert.ToInt32(transactionDetail.HeadID);
                        astDetail.SKUID = new KeyValueViewModel()
                        {
                            Key = transactionDetail.ProductSKUMapID.ToString(),
                            Value = transactionDetail.SKU
                        };
                        astDetail.Asset = transactionDetail.AssetID.HasValue ? new KeyValueViewModel()
                        {
                            Key = transactionDetail.AssetID.ToString(),
                            Value = transactionDetail.AssetName
                        } : new KeyValueViewModel();
                        astDetail.Description = transactionDetail.SKU;
                        astDetail.Quantity = Convert.ToInt32(transactionDetail.Quantity);
                        astDetail.Amount = transactionDetail.Amount;
                        astDetail.IsError = transactionDetail.IsError;
                        astDetail.IsRequiredSerialNumber = transactionDetail.IsRequiredSerialNumber;

                        astDetail.CreatedBy = transactionDetail.CreatedBy;
                        astDetail.CreatedDate = transactionDetail.CreatedDate;
                        astDetail.UpdatedBy = transactionDetail.UpdatedBy;
                        astDetail.UpdatedDate = transactionDetail.UpdatedDate;

                        astDetail.TransactionSerialMaps = new List<AssetPurchaseInvoiceSerialMapViewModel>();
                        if (transactionDetail.AssetTransactionSerialMaps != null)
                        {
                            var serialNo = 0;
                            foreach (var serialMap in transactionDetail.AssetTransactionSerialMaps)
                            {
                                serialNo ++;
                                astDetail.TransactionSerialMaps.Add(new AssetPurchaseInvoiceSerialMapViewModel()
                                {
                                    AssetTransactionSerialMapIID = serialMap.AssetTransactionSerialMapIID,
                                    SerialNo = serialNo,
                                    TransactionDetailID = serialMap.TransactionDetailID,
                                    AssetSerialMapID = serialMap.AssetSerialMapID,
                                    AssetID = serialMap.AssetID,
                                    AssetSequenceCode = serialMap.AssetSequenceCode,
                                    AssetSerialNumber = serialMap.SerialNumber,
                                    AssetTag = serialMap.AssetTag,
                                    CostPrice = serialMap.CostPrice,
                                    ExpectedLife = serialMap.ExpectedLife,
                                    DepreciationRate = serialMap.DepreciationRate,
                                    ExpectedScrapValue = serialMap.ExpectedScrapValue,
                                    AccumulatedDepreciationAmount = serialMap.AccumulatedDepreciationAmount,
                                    FirstUseDateString = serialMap.DateOfFirstUse.HasValue ? serialMap.DateOfFirstUse.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                                    SupplierID = serialMap.SupplierID,
                                    BillNumber = serialMap.BillNumber,
                                    BillDateString = serialMap.BillDate.HasValue ? serialMap.BillDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                                    Supplier = serialMap.SupplierID.HasValue ? new KeyValueViewModel()
                                    {
                                        Key = serialMap.SupplierID.ToString(),
                                        Value = serialMap.SupplierName
                                    } : new KeyValueViewModel(),
                                    CreatedBy = serialMap.CreatedBy,
                                    CreatedDate = serialMap.CreatedDate,
                                    UpdatedBy = serialMap.UpdatedBy,
                                    UpdatedDate = serialMap.UpdatedDate,
                                });
                            }
                        }

                        assetPurchaseInvoice.DetailViewModel.Add(astDetail);
                    }
                }

                return assetPurchaseInvoice;
            }
            else return new AssetPurchaseInvoiceViewModel();
        }

        public static AssetTransactionDTO ToDTO(AssetPurchaseInvoiceViewModel vm)
        {
            if (vm != null)
            {
                var transaction = new AssetTransactionDTO();
                transaction.AssetTransactionHead = new AssetTransactionHeadDTO();

                Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
                Mapper<AssetPurchaseInvoiceMasterViewModel, TransactionHeadDTO>.CreateMap();

                transaction.AssetTransactionHead = Mapper<AssetPurchaseInvoiceMasterViewModel, AssetTransactionHeadDTO>.Map(vm.MasterViewModel);
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                transaction.AssetTransactionHead.HeadIID = vm.MasterViewModel.TransactionHeadIID;
                transaction.AssetTransactionHead.DocumentTypeID = Convert.ToInt32(vm.MasterViewModel.DocumentType.Key);
                transaction.AssetTransactionHead.BranchID = vm.MasterViewModel.Branch != null && !string.IsNullOrEmpty(vm.MasterViewModel.Branch.Key) ? long.Parse(vm.MasterViewModel.Branch.Key) : (long?)null;
                transaction.AssetTransactionHead.TransactionNo = vm.MasterViewModel.TransactionNo;
                transaction.AssetTransactionHead.EntryDate = string.IsNullOrEmpty(vm.MasterViewModel.TransactionDateString) ? DateTime.Now : DateTime.ParseExact(vm.MasterViewModel.TransactionDateString, dateFormat, CultureInfo.InvariantCulture);
                transaction.AssetTransactionHead.Remarks = vm.MasterViewModel.Remarks;
                transaction.AssetTransactionHead.SupplierID = vm.MasterViewModel.Supplier != null && !string.IsNullOrEmpty(vm.MasterViewModel.Supplier.Key) ? Convert.ToInt32(vm.MasterViewModel.Supplier.Key) : (long?)null;
                transaction.AssetTransactionHead.ReferenceHeadID = vm.MasterViewModel.ReferenceTransactionHeaderID != null ? Convert.ToInt32(vm.MasterViewModel.ReferenceTransactionHeaderID) : (long?)null;
                transaction.AssetTransactionHead.DocumentReferenceTypeID = vm.MasterViewModel.DocumentReferenceTypeID;
                transaction.AssetTransactionHead.ProcessingStatusID = vm.MasterViewModel.TransactionStatus != null && !string.IsNullOrEmpty(vm.MasterViewModel.TransactionStatus.Key) ? !string.IsNullOrEmpty(vm.MasterViewModel.TransactionStatus.Key) ? byte.Parse(vm.MasterViewModel.TransactionStatus.Key) : (byte?)null : (byte?)null;
                transaction.AssetTransactionHead.DocumentStatusID = vm.MasterViewModel.DocumentStatus != null && !string.IsNullOrEmpty(vm.MasterViewModel.DocumentStatus.Key) ? !string.IsNullOrEmpty(vm.MasterViewModel.DocumentStatus.Key) ? short.Parse(vm.MasterViewModel.DocumentStatus.Key) : (short?)null : (short?)null;

                transaction.AssetTransactionHead.CreatedBy = vm.CreatedBy;
                transaction.AssetTransactionHead.CreatedDate = vm.CreatedDate;
                transaction.AssetTransactionHead.UpdatedBy = vm.UpdatedBy;
                transaction.AssetTransactionHead.UpdatedDate = vm.UpdatedDate;

                transaction.AssetTransactionDetails = new List<AssetTransactionDetailsDTO>();
                if (vm.DetailViewModel != null && vm.DetailViewModel.Count > 0)
                {
                    foreach (var transactionDetail in vm.DetailViewModel)
                    {
                        var rowNumber = 0;
                        if (transactionDetail.SKUID != null && transactionDetail.SKUID != null)
                        {
                            rowNumber++;
                            if (transactionDetail.Asset == null || string.IsNullOrEmpty(transactionDetail.Asset.Key))
                            {
                                throw new Exception($"Asset is required but not selected in row {rowNumber}.");
                            }

                            var transactionDetailDTO = new AssetTransactionDetailsDTO();

                            transactionDetailDTO.DetailIID = transactionDetail.TransactionDetailID;
                            transactionDetailDTO.HeadID = transactionDetail.TransactionHead;
                            transactionDetailDTO.ProductSKUMapID = long.Parse(transactionDetail.SKUID.Key);
                            transactionDetailDTO.Quantity = Convert.ToInt32(transactionDetail.Quantity);
                            transactionDetailDTO.Amount = transactionDetail.Amount;
                            transactionDetailDTO.AssetID = transactionDetail.Asset == null || string.IsNullOrEmpty(transactionDetail.Asset.Key) ? (long?)null : long.Parse(transactionDetail.Asset.Key);
                            transactionDetailDTO.AccountID = transactionDetail.AccountID;
                            transactionDetailDTO.AssetGlAccID = transactionDetail.AssetGlAccID;
                            transactionDetailDTO.AccumulatedDepGLAccID = transactionDetail.AccumulatedDepGLAccID;
                            transactionDetailDTO.DepreciationExpGLAccID = transactionDetail.DepreciationExpGLAccID;
                            transactionDetailDTO.IsRequiredSerialNumber = transactionDetail.IsRequiredSerialNumber;

                            transactionDetailDTO.CreatedBy = transactionDetail.CreatedBy;
                            transactionDetailDTO.CreatedDate = transactionDetail.CreatedDate;
                            transactionDetailDTO.UpdatedBy = transactionDetail.UpdatedBy;
                            transactionDetailDTO.UpdatedDate = transactionDetail.UpdatedDate;

                            transactionDetailDTO.AssetTransactionSerialMaps = new List<AssetTransactionSerialMapDTO>();
                            if (transactionDetail.TransactionSerialMaps != null)
                            {
                                foreach (var serialMap in transactionDetail.TransactionSerialMaps)
                                {
                                    if (serialMap.DepreciationRate.HasValue)
                                    {
                                        if (transactionDetailDTO.IsRequiredSerialNumber == true && string.IsNullOrEmpty(serialMap.AssetSerialNumber))
                                        {
                                            throw new Exception($"Serial number is required for the asset. Please enter all serial numbers based on the quantity in row {rowNumber}.");
                                        }

                                        transactionDetailDTO.AssetTransactionSerialMaps.Add(new AssetTransactionSerialMapDTO()
                                        {
                                            AssetTransactionSerialMapIID = serialMap.AssetTransactionSerialMapIID,
                                            TransactionDetailID = serialMap.TransactionDetailID,
                                            AssetSerialMapID = serialMap.AssetSerialMapID,
                                            AssetID = serialMap.AssetID.HasValue ? serialMap.AssetID : transactionDetailDTO.AssetID,
                                            AssetSequenceCode = serialMap.AssetSequenceCode,
                                            SerialNumber = serialMap.AssetSerialNumber,
                                            AssetTag = serialMap.AssetTag,
                                            CostPrice = serialMap.CostPrice,
                                            ExpectedLife = serialMap.ExpectedLife,
                                            DepreciationRate = serialMap.DepreciationRate,
                                            ExpectedScrapValue = serialMap.ExpectedScrapValue,
                                            AccumulatedDepreciationAmount = serialMap.AccumulatedDepreciationAmount,
                                            DateOfFirstUse = string.IsNullOrEmpty(serialMap.FirstUseDateString) ? (DateTime?)null : DateTime.ParseExact(serialMap.FirstUseDateString, dateFormat, CultureInfo.InvariantCulture),
                                            SupplierID = serialMap.Supplier != null && !string.IsNullOrEmpty(serialMap.Supplier.Key) ? Convert.ToInt32(serialMap.Supplier.Key) : (long?)null,
                                            BillNumber = serialMap.BillNumber,
                                            BillDate = string.IsNullOrEmpty(serialMap.BillDateString) ? (DateTime?)null : DateTime.ParseExact(serialMap.BillDateString, dateFormat, CultureInfo.InvariantCulture),
                                            CreatedBy = serialMap.CreatedBy,
                                            CreatedDate = serialMap.CreatedDate,
                                            UpdatedBy = serialMap.UpdatedBy,
                                            UpdatedDate = serialMap.UpdatedDate,
                                        });
                                    }
                                }

                                if (transactionDetailDTO.AssetTransactionSerialMaps.Count != transactionDetailDTO.Quantity)
                                {
                                    throw new Exception($"Mismatch between quantity and serial number mapping in row {rowNumber}.");
                                }
                            }

                            transaction.AssetTransactionDetails.Add(transactionDetailDTO);
                        }
                    }
                }
                return transaction;
            }
            else return new AssetTransactionDTO();
        }

    }
}
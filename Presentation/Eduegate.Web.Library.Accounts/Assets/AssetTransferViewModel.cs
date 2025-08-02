using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Accounts.Assets;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Eduegate.Web.Library.Accounts.Assets
{
    public class AssetTransferViewModel : BaseMasterViewModel
    {
        public AssetTransferViewModel()
        {
            MasterViewModel = new AssetTransferMasterViewModel();
            DetailViewModel = new List<AssetTransferDetailViewModel>() { new AssetTransferDetailViewModel() };
        }

        public AssetTransferMasterViewModel MasterViewModel { get; set; }
        public List<AssetTransferDetailViewModel> DetailViewModel { get; set; }

        public static AssetTransferViewModel ToVM(AssetTransactionDTO dto)
        {
            if (dto.IsNotNull())
            {
                var at = new AssetTransferViewModel();
                at.MasterViewModel = new AssetTransferMasterViewModel();
                at.DetailViewModel = new List<AssetTransferDetailViewModel>();
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                at.MasterViewModel.TransactionHeadIID = dto.AssetTransactionHead.HeadIID;
                at.MasterViewModel.CompanyID = dto.AssetTransactionHead.CompanyID;
                at.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = dto.AssetTransactionHead.DocumentTypeID.ToString(), Value = dto.AssetTransactionHead.DocumentTypeName };
                at.MasterViewModel.Branch = new KeyValueViewModel() { Key = dto.AssetTransactionHead.BranchID.ToString(), Value = dto.AssetTransactionHead.BranchName };
                at.MasterViewModel.ToBranch = dto.AssetTransactionHead.ToBranchID.ToString();
                at.MasterViewModel.TransactionNo = dto.AssetTransactionHead.TransactionNo;
                at.MasterViewModel.TransactionDateString = dto.AssetTransactionHead.EntryDate.HasValue ? dto.AssetTransactionHead.EntryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                at.MasterViewModel.ReferenceTransactionHeaderID = dto.AssetTransactionHead.ReferenceHeadID.ToString();
                at.MasterViewModel.ReferenceTransactionNo = dto.AssetTransactionHead.ReferenceTransactionNo;
                at.MasterViewModel.IsTransactionCompleted = dto.AssetTransactionHead.IsTransactionCompleted;
                at.MasterViewModel.DocumentStatus = new KeyValueViewModel();
                at.MasterViewModel.DocumentStatus.Key = dto.AssetTransactionHead.DocumentStatusID.ToString();
                at.MasterViewModel.DocumentStatus.Value = dto.AssetTransactionHead.DocumentStatusName;
                at.MasterViewModel.Remarks = dto.AssetTransactionHead.Remarks;
                at.MasterViewModel.DocumentReferenceTypeID = dto.AssetTransactionHead.DocumentReferenceTypeID;

                at.MasterViewModel.CreatedBy = dto.AssetTransactionHead.CreatedBy;
                at.MasterViewModel.CreatedDate = dto.AssetTransactionHead.CreatedDate;
                at.MasterViewModel.UpdatedBy = dto.AssetTransactionHead.UpdatedBy;
                at.MasterViewModel.UpdatedDate = dto.AssetTransactionHead.UpdatedDate;

                if (dto.AssetTransactionDetails != null && dto.AssetTransactionDetails.Count > 0)
                {
                    foreach (var transactionDetail in dto.AssetTransactionDetails)
                    {
                        var atDetail = new AssetTransferDetailViewModel();

                        atDetail.TransactionDetailID = transactionDetail.DetailIID;
                        atDetail.TransactionHeadID = transactionDetail.HeadID;
                        atDetail.Asset = new KeyValueViewModel();
                        atDetail.Asset.Key = transactionDetail.AssetID.ToString();
                        atDetail.Asset.Value = transactionDetail.AssetCode;
                        atDetail.AssetDescription = transactionDetail.AssetName;
                        atDetail.Quantity = transactionDetail.Quantity;
                        atDetail.CutOffDateString = transactionDetail.CutOffDate.HasValue ? transactionDetail.CutOffDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                        atDetail.AvailableQuantity = transactionDetail.AvailableAssetQuantity;

                        atDetail.CreatedBy = transactionDetail.CreatedBy;
                        atDetail.CreatedDate = transactionDetail.CreatedDate;
                        atDetail.UpdatedBy = transactionDetail.UpdatedBy;
                        atDetail.UpdatedDate = transactionDetail.UpdatedDate;

                        atDetail.TransactionSerialMaps = new List<AssetTransactionSerialMapViewModel>();
                        if (transactionDetail.AssetTransactionSerialMaps != null && transactionDetail.AssetTransactionSerialMaps.Count > 0)
                        {
                            //Mapper<AssetTransactionSerialMapDTO, AssetTransactionSerialMapViewModel>.CreateMap();
                            //var vm = Mapper<AssetTransactionSerialMapDTO, AssetTransactionSerialMapViewModel>.Map(transactionDetail.AssetTransactionSerialMaps);
                            //atDetail.TransactionSerialMaps = vm.ToList();
                            foreach (var serialMap in transactionDetail.AssetTransactionSerialMaps)
                            {
                                if (serialMap.AssetSerialMapID.HasValue)
                                {
                                    atDetail.TransactionSerialMaps.Add(new AssetTransactionSerialMapViewModel()
                                    {
                                        AssetTransactionSerialMapIID = serialMap.AssetTransactionSerialMapIID,
                                        TransactionDetailID = serialMap.TransactionDetailID,
                                        AssetID = serialMap.AssetID.HasValue ? serialMap.AssetID : transactionDetail.AssetID,
                                        AssetSerialMapID = serialMap.AssetSerialMapID,
                                        AssetSerialMap = serialMap.AssetSerialMapID.HasValue ? new KeyValueViewModel()
                                        {
                                            Key = serialMap.AssetSerialMapID.ToString(),
                                            Value = serialMap.AssetSequenceCode
                                        } : new KeyValueViewModel(),

                                        AssetSequenceCode = serialMap.AssetSequenceCode,
                                        SerialNumber = serialMap.SerialNumber,
                                        AssetTag = serialMap.AssetTag,
                                        CostPrice = serialMap.CostPrice,
                                        NetValue = serialMap.NetValue,
                                        ExpectedLife = serialMap.ExpectedLife,
                                        DepreciationRate = serialMap.DepreciationRate,
                                        ExpectedScrapValue = serialMap.ExpectedScrapValue,
                                        AccumulatedDepreciationAmount = serialMap.AccumulatedDepreciationAmount,
                                        FirstUseDateString = serialMap.DateOfFirstUse.HasValue ? serialMap.DateOfFirstUse.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                                        LastDepDateString = serialMap.DepToDate.HasValue ? serialMap.DepToDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                                        CreatedBy = serialMap.CreatedBy,
                                        CreatedDate = serialMap.CreatedDate,
                                        UpdatedBy = serialMap.UpdatedBy,
                                        UpdatedDate = serialMap.UpdatedDate,
                                    });
                                }
                            }
                        }

                        at.DetailViewModel.Add(atDetail);
                    }
                }

                return at;
            }
            else
                return new AssetTransferViewModel();
        }

        public static AssetTransactionDTO ToDTO(AssetTransferViewModel vm)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            if (vm.IsNotNull())
            {
                var transactionDTO = new AssetTransactionDTO();
                transactionDTO.AssetTransactionHead = new AssetTransactionHeadDTO();
                transactionDTO.AssetTransactionDetails = new List<AssetTransactionDetailsDTO>();

                if (vm.MasterViewModel.IsNotNull())
                {
                    if (vm.MasterViewModel.DocumentType == null || string.IsNullOrEmpty(vm.MasterViewModel.DocumentType.Key) || string.IsNullOrEmpty(vm.MasterViewModel.DocumentType.Value))
                    {
                        throw new Exception("Select Document Type!");
                    }

                    transactionDTO.AssetTransactionHead.HeadIID = vm.MasterViewModel.TransactionHeadIID;
                    transactionDTO.AssetTransactionHead.CompanyID = vm.MasterViewModel.CompanyID;
                    transactionDTO.AssetTransactionHead.DocumentTypeID = Convert.ToInt32(vm.MasterViewModel.DocumentType.Key);
                    transactionDTO.AssetTransactionHead.BranchID = Convert.ToInt32(vm.MasterViewModel.Branch.Key);
                    transactionDTO.AssetTransactionHead.ToBranchID = Convert.ToInt32(vm.MasterViewModel.ToBranch);
                    transactionDTO.AssetTransactionHead.TransactionNo = vm.MasterViewModel.TransactionNo;
                    transactionDTO.AssetTransactionHead.EntryDate = string.IsNullOrEmpty(vm.MasterViewModel.TransactionDateString) ? (DateTime?)null : DateTime.ParseExact(vm.MasterViewModel.TransactionDateString, dateFormat, CultureInfo.InvariantCulture);
                    transactionDTO.AssetTransactionHead.CreatedBy = vm.CreatedBy;
                    transactionDTO.AssetTransactionHead.UpdatedBy = vm.UpdatedBy;
                    transactionDTO.AssetTransactionHead.ReferenceHeadID = !string.IsNullOrEmpty(vm.MasterViewModel.ReferenceTransactionHeaderID) ? Convert.ToInt32(vm.MasterViewModel.ReferenceTransactionHeaderID) : (long?)null;
                    transactionDTO.AssetTransactionHead.DocumentStatusID = Convert.ToInt32(vm.MasterViewModel.DocumentStatus.Key);
                    transactionDTO.AssetTransactionHead.Remarks = vm.MasterViewModel.Remarks;
                    transactionDTO.AssetTransactionHead.DocumentReferenceTypeID = vm.MasterViewModel.DocumentReferenceTypeID;

                    if (transactionDTO.AssetTransactionHead.BranchID == transactionDTO.AssetTransactionHead.ToBranchID)
                    {
                        throw new Exception("From Branch and To Branch cannot be the same for a transfer.");
                    }

                    transactionDTO.AssetTransactionHead.CreatedBy = vm.CreatedBy;
                    transactionDTO.AssetTransactionHead.CreatedDate = vm.CreatedDate;
                    transactionDTO.AssetTransactionHead.UpdatedBy = vm.UpdatedBy;
                    transactionDTO.AssetTransactionHead.UpdatedDate = vm.UpdatedDate;
                }
                if (vm.DetailViewModel.IsNotNull() && vm.DetailViewModel.Count > 0)
                {
                    foreach (var transactionDetail in vm.DetailViewModel)
                    {
                        if (transactionDetail.Asset != null && !string.IsNullOrEmpty(transactionDetail.Asset.Key))
                        {
                            if (string.IsNullOrEmpty(transactionDetail.CutOffDateString))
                            {
                                throw new Exception("Cut-off date is required!");
                            }

                            var transactionDetailDTO = new AssetTransactionDetailsDTO();

                            transactionDetailDTO.DetailIID = transactionDetail.TransactionDetailID;
                            transactionDetailDTO.HeadID = transactionDetail.TransactionHeadID;
                            transactionDetailDTO.AssetID = transactionDetail.Asset != null ? !string.IsNullOrEmpty(transactionDetail.Asset.Key) ? Convert.ToInt32(transactionDetail.Asset.Key) : (long?)null : (long?)null;
                            transactionDetailDTO.Quantity = transactionDetail.Quantity;
                            transactionDetailDTO.CutOffDate = string.IsNullOrEmpty(transactionDetail.CutOffDateString) ? (DateTime?)null : DateTime.ParseExact(transactionDetail.CutOffDateString, dateFormat, CultureInfo.InvariantCulture);
                            transactionDetailDTO.DocumentReferenceTypeID = vm.MasterViewModel.DocumentReferenceTypeID;

                            transactionDetailDTO.CreatedBy = transactionDetail.CreatedBy;
                            transactionDetailDTO.CreatedDate = transactionDetail.CreatedDate;
                            transactionDetailDTO.UpdatedBy = transactionDetail.UpdatedBy;
                            transactionDetailDTO.UpdatedDate = transactionDetail.UpdatedDate;

                            if (transactionDetailDTO.IsRequiredSerialNumber == true && transactionDetail.TransactionSerialMaps.Count == 0)
                            {
                                throw new Exception("Serial number is required for this asset! Please enter all serial numbers based on the quantity!");
                            }

                            transactionDetailDTO.AssetTransactionSerialMaps = new List<AssetTransactionSerialMapDTO>();
                            if (transactionDetail.TransactionSerialMaps != null && transactionDetail.TransactionSerialMaps.Count > 0)
                            {
                                foreach (var serialMap in transactionDetail.TransactionSerialMaps)
                                {
                                    if (serialMap.AssetSerialMap != null && !string.IsNullOrEmpty(serialMap.AssetSerialMap.Key))
                                    {
                                        if (transactionDetailDTO.IsRequiredSerialNumber == true && string.IsNullOrEmpty(serialMap.SerialNumber))
                                        {
                                            throw new Exception("Serial number is required for this asset! Please enter all serial numbers based on the quantity!");
                                        }

                                        transactionDetailDTO.AssetTransactionSerialMaps.Add(new AssetTransactionSerialMapDTO()
                                        {
                                            AssetTransactionSerialMapIID = serialMap.AssetTransactionSerialMapIID,
                                            TransactionDetailID = serialMap.TransactionDetailID,
                                            AssetID = serialMap.AssetID.HasValue ? serialMap.AssetID : transactionDetailDTO.AssetID,
                                            AssetSerialMapID = serialMap.AssetSerialMap == null || string.IsNullOrEmpty(serialMap.AssetSerialMap.Key) ? (long?)null : long.Parse(serialMap.AssetSerialMap.Key),
                                            AssetSequenceCode = serialMap.AssetSequenceCode,
                                            SerialNumber = serialMap.SerialNumber,
                                            AssetTag = serialMap.AssetTag,
                                            CostPrice = serialMap.CostPrice,
                                            NetValue = serialMap.NetValue,
                                            ExpectedLife = serialMap.ExpectedLife,
                                            DepreciationRate = serialMap.DepreciationRate,
                                            ExpectedScrapValue = serialMap.ExpectedScrapValue,
                                            AccumulatedDepreciationAmount = serialMap.AccumulatedDepreciationAmount,
                                            DateOfFirstUse = string.IsNullOrEmpty(serialMap.FirstUseDateString) ? (DateTime?)null : DateTime.ParseExact(serialMap.FirstUseDateString, dateFormat, CultureInfo.InvariantCulture),
                                            DepToDate = string.IsNullOrEmpty(serialMap.LastDepDateString) ? (DateTime?)null : DateTime.ParseExact(serialMap.LastDepDateString, dateFormat, CultureInfo.InvariantCulture),
                                            PreviousAcculatedDepreciationAmount = serialMap.AccumulatedDepreciationAmount,
                                            CreatedBy = serialMap.CreatedBy,
                                            CreatedDate = serialMap.CreatedDate,
                                            UpdatedBy = serialMap.UpdatedBy,
                                            UpdatedDate = serialMap.UpdatedDate,
                                        });
                                    }
                                }
                            }

                            transactionDTO.AssetTransactionDetails.Add(transactionDetailDTO);
                        }
                    }
                }

                if (transactionDTO.AssetTransactionDetails.Count() == 0)
                {
                    throw new Exception("Select at least one asset to initiate a transfer!");
                }

                return transactionDTO;
            }
            else
            {
                return new AssetTransactionDTO();
            }
        }

    }
}
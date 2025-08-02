using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Accounts.Assets;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Eduegate.Web.Library.Accounts.Assets
{
    public class AssetDisposalViewModel : BaseMasterViewModel
    {
        public AssetDisposalViewModel()
        {
            MasterViewModel = new AssetDisposalMasterViewModel();
            DetailViewModel = new List<AssetDisposalDetailViewModel>() { new AssetDisposalDetailViewModel() };
        }

        public AssetDisposalMasterViewModel MasterViewModel { get; set; }
        public List<AssetDisposalDetailViewModel> DetailViewModel { get; set; }

        public static AssetDisposalViewModel ToVM(AssetTransactionDTO dto)
        {
            if (dto.IsNotNull())
            {
                var ad = new AssetDisposalViewModel();
                ad.MasterViewModel = new AssetDisposalMasterViewModel();
                ad.DetailViewModel = new List<AssetDisposalDetailViewModel>();
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                ad.MasterViewModel.TransactionHeadIID = dto.AssetTransactionHead.HeadIID;
                ad.MasterViewModel.CompanyID = dto.AssetTransactionHead.CompanyID;
                ad.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = dto.AssetTransactionHead.DocumentTypeID.ToString(), Value = dto.AssetTransactionHead.DocumentTypeName };
                ad.MasterViewModel.Branch = new KeyValueViewModel() { Key = dto.AssetTransactionHead.BranchID.ToString(), Value = dto.AssetTransactionHead.BranchName };
                ad.MasterViewModel.TransactionNo = dto.AssetTransactionHead.TransactionNo;
                ad.MasterViewModel.TransactionDateString = dto.AssetTransactionHead.EntryDate != null ? Convert.ToDateTime(dto.AssetTransactionHead.EntryDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                ad.MasterViewModel.IsTransactionCompleted = dto.AssetTransactionHead.IsTransactionCompleted;
                ad.MasterViewModel.DocumentStatus = new KeyValueViewModel();
                ad.MasterViewModel.DocumentStatus.Key = dto.AssetTransactionHead.DocumentStatusID.ToString();
                ad.MasterViewModel.DocumentStatus.Value = dto.AssetTransactionHead.DocumentStatusName;
                ad.MasterViewModel.Remarks = dto.AssetTransactionHead.Remarks;

                ad.MasterViewModel.CreatedBy = dto.AssetTransactionHead.CreatedBy;
                ad.MasterViewModel.CreatedDate = dto.AssetTransactionHead.CreatedDate;
                ad.MasterViewModel.UpdatedBy = dto.AssetTransactionHead.UpdatedBy;
                ad.MasterViewModel.UpdatedDate = dto.AssetTransactionHead.UpdatedDate;

                if (dto.AssetTransactionDetails != null && dto.AssetTransactionDetails.Count > 0)
                {
                    foreach (var transactionDetail in dto.AssetTransactionDetails)
                    {
                        var atDetail = new AssetDisposalDetailViewModel();

                        atDetail.TransactionDetailID = transactionDetail.DetailIID;
                        atDetail.TransactionHeadID = Convert.ToInt32(transactionDetail.HeadID);
                        atDetail.Asset = transactionDetail.AssetID.HasValue ? new KeyValueViewModel()
                        {
                            Key = transactionDetail.AssetID.ToString(),
                            Value = transactionDetail.AssetCode,
                        } : new KeyValueViewModel();
                        atDetail.AssetDescription = transactionDetail.AssetName;
                        atDetail.Quantity = transactionDetail.Quantity;

                        var serialMapData = transactionDetail.AssetTransactionSerialMaps.FirstOrDefault();
                        if (serialMapData != null)
                        {
                            atDetail.AssetTransactionSerialMapID = serialMapData.AssetTransactionSerialMapIID;
                            atDetail.AssetSerialMapID = serialMapData.AssetSerialMapID;
                            atDetail.AssetSerialMap =
                            atDetail.AssetSerialMap = serialMapData.AssetSerialMapID.HasValue ? new KeyValueViewModel()
                            {
                                Key = serialMapData.AssetSerialMapID.ToString(),
                                Value = serialMapData.AssetSerialMapSequenceCode
                            } : new KeyValueViewModel();
                            atDetail.AssetSequenceCode = serialMapData.AssetSerialMapSequenceCode;
                            atDetail.LastDepDateString = serialMapData.LastDepreciationDate.HasValue ? serialMapData.LastDepreciationDate.Value.ToString(dateFormat) : null;
                            atDetail.AccumulatedDepreciationAmount = serialMapData.AccumulatedDepreciationAmount;
                            atDetail.LastNetValue = serialMapData.NetValue;
                            atDetail.DisposibleAmount = serialMapData.DisposibleAmount;
                            atDetail.DifferenceAmount = serialMapData.DifferenceAmount;
                        }

                        atDetail.CreatedBy = transactionDetail.CreatedBy;
                        atDetail.CreatedDate = transactionDetail.CreatedDate;
                        atDetail.UpdatedBy = transactionDetail.UpdatedBy;
                        atDetail.UpdatedDate = transactionDetail.UpdatedDate;

                        ad.DetailViewModel.Add(atDetail);
                    }
                }

                return ad;
            }
            else
                return new AssetDisposalViewModel();
        }

        public static AssetTransactionDTO ToDTO(AssetDisposalViewModel vm)
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
                    transactionDTO.AssetTransactionHead.TransactionNo = vm.MasterViewModel.TransactionNo;
                    transactionDTO.AssetTransactionHead.EntryDate = string.IsNullOrEmpty(vm.MasterViewModel.TransactionDateString) ? (DateTime?)null : DateTime.ParseExact(vm.MasterViewModel.TransactionDateString, dateFormat, CultureInfo.InvariantCulture);
                    transactionDTO.AssetTransactionHead.CreatedBy = vm.CreatedBy;
                    transactionDTO.AssetTransactionHead.UpdatedBy = vm.UpdatedBy;
                    transactionDTO.AssetTransactionHead.DocumentStatusID = Convert.ToInt32(vm.MasterViewModel.DocumentStatus.Key);
                    transactionDTO.AssetTransactionHead.Remarks = vm.MasterViewModel.Remarks;
                }

                if (vm.DetailViewModel.IsNotNull() && vm.DetailViewModel.Count > 0)
                {
                    foreach (var transactionDetail in vm.DetailViewModel)
                    {
                        if (transactionDetail.Asset != null && !string.IsNullOrEmpty(transactionDetail.Asset.Key))
                        {
                            var transactionDetailDTO = new AssetTransactionDetailsDTO();

                            transactionDetailDTO.DetailIID = transactionDetail.TransactionDetailID;
                            transactionDetailDTO.HeadID = transactionDetail.TransactionHeadID;
                            transactionDetailDTO.AssetID = transactionDetail.Asset == null || string.IsNullOrEmpty(transactionDetail.Asset.Key) ? (long?)null : Convert.ToInt32(transactionDetail.Asset.Key);
                            transactionDetailDTO.CostAmount = transactionDetail.CostAmount;
                            transactionDetailDTO.Quantity = transactionDetail.Quantity ?? 1;

                            transactionDetailDTO.CreatedBy = transactionDetail.CreatedBy;
                            transactionDetailDTO.CreatedDate = transactionDetail.CreatedDate;
                            transactionDetailDTO.UpdatedBy = transactionDetail.UpdatedBy;
                            transactionDetailDTO.UpdatedDate = transactionDetail.UpdatedDate;

                            transactionDetailDTO.AssetTransactionSerialMaps = new List<AssetTransactionSerialMapDTO>();

                            var serialMapID = transactionDetail.AssetSerialMap == null || string.IsNullOrEmpty(transactionDetail.AssetSerialMap.Key) ? (long?)null : long.Parse(transactionDetail.AssetSerialMap.Key);
                            if (!serialMapID.HasValue)
                            {
                                throw new Exception("Serial selection is required for asset disposal!");
                            }

                            transactionDetailDTO.AssetTransactionSerialMaps.Add(new AssetTransactionSerialMapDTO()
                            {
                                AssetTransactionSerialMapIID = transactionDetail.AssetTransactionSerialMapID.HasValue ? transactionDetail.AssetTransactionSerialMapID.Value : 0,
                                TransactionDetailID = transactionDetail.TransactionDetailID,
                                AssetID = transactionDetailDTO.AssetID,
                                AssetSerialMapID = serialMapID,
                                CostPrice = transactionDetail.CostAmount,
                                LastDepreciationDate = string.IsNullOrEmpty(transactionDetail.LastDepDateString) ? (DateTime?)null : DateTime.ParseExact(transactionDetail.LastDepDateString, dateFormat, CultureInfo.InvariantCulture),
                                AccumulatedDepreciationAmount = transactionDetail.AccumulatedDepreciationAmount,
                                NetValue = transactionDetail.LastNetValue,
                                DisposibleAmount = transactionDetail.DisposibleAmount,
                                DifferenceAmount = transactionDetail.DifferenceAmount,
                            });

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
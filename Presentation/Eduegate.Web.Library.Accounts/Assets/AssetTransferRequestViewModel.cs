using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Accounts.Assets;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Eduegate.Web.Library.Accounts.Assets
{
    public class AssetTransferRequestViewModel : BaseMasterViewModel
    {
        public AssetTransferRequestViewModel()
        {
            MasterViewModel = new AssetTransferRequestMasterViewModel();
            DetailViewModel = new List<AssetTransferRequestDetailViewModel>() {  new AssetTransferRequestDetailViewModel() };
        }

        public AssetTransferRequestMasterViewModel MasterViewModel { get; set; }
        public List<AssetTransferRequestDetailViewModel> DetailViewModel { get; set; }

        public static AssetTransferRequestViewModel ToVM(AssetTransactionDTO dto)
        {

            if (dto.IsNotNull())
            {
                var at = new AssetTransferRequestViewModel();
                at.MasterViewModel = new AssetTransferRequestMasterViewModel();
                at.DetailViewModel = new List<AssetTransferRequestDetailViewModel>();
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                at.MasterViewModel.TransactionHeadIID = dto.AssetTransactionHead.HeadIID;
                at.MasterViewModel.CompanyID = dto.AssetTransactionHead.CompanyID;
                at.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = dto.AssetTransactionHead.DocumentTypeID.ToString(), Value = dto.AssetTransactionHead.DocumentTypeName };
                at.MasterViewModel.Branch = new KeyValueViewModel() { Key = dto.AssetTransactionHead.BranchID.ToString(), Value = dto.AssetTransactionHead.BranchName };
                at.MasterViewModel.ToBranch = dto.AssetTransactionHead.ToBranchID.ToString();
                at.MasterViewModel.TransactionNo = dto.AssetTransactionHead.TransactionNo;
                at.MasterViewModel.TransactionDateString = dto.AssetTransactionHead.EntryDate != null ? Convert.ToDateTime(dto.AssetTransactionHead.EntryDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                at.MasterViewModel.IsTransactionCompleted = dto.AssetTransactionHead.IsTransactionCompleted;
                at.MasterViewModel.DocumentStatus = new KeyValueViewModel();
                at.MasterViewModel.DocumentStatus.Key = dto.AssetTransactionHead.DocumentStatusID.ToString();
                at.MasterViewModel.DocumentStatus.Value = dto.AssetTransactionHead.DocumentStatusName.ToString();

                at.MasterViewModel.CreatedBy = dto.AssetTransactionHead.CreatedBy;
                at.MasterViewModel.CreatedDate = dto.AssetTransactionHead.CreatedDate;
                at.MasterViewModel.UpdatedBy = dto.AssetTransactionHead.UpdatedBy;
                at.MasterViewModel.UpdatedDate = dto.AssetTransactionHead.UpdatedDate;

                if (dto.AssetTransactionDetails != null && dto.AssetTransactionDetails.Count > 0)
                {
                    foreach (var transactionDetail in dto.AssetTransactionDetails)
                    {
                        var atDetail = new AssetTransferRequestDetailViewModel();

                        atDetail.TransactionDetailID = transactionDetail.DetailIID;
                        atDetail.TransactionHeadID = Convert.ToInt32(transactionDetail.HeadID);
                        atDetail.Asset = transactionDetail.AssetID.HasValue ? new KeyValueViewModel()
                        {
                            Key = transactionDetail.AssetID.ToString(),
                            Value = transactionDetail.AssetCode,
                        } : new KeyValueViewModel();
                        atDetail.AssetDescription = transactionDetail.AssetName;
                        atDetail.AvailableQuantity = transactionDetail.AvailableAssetQuantity;
                        atDetail.Quantity = transactionDetail.Quantity;

                        atDetail.CreatedBy = transactionDetail.CreatedBy;
                        atDetail.CreatedDate = transactionDetail.CreatedDate;
                        atDetail.UpdatedBy = transactionDetail.UpdatedBy;
                        atDetail.UpdatedDate = transactionDetail.UpdatedDate;

                        at.DetailViewModel.Add(atDetail);
                    }
                }

                return at;
            }
            else
                return new AssetTransferRequestViewModel();
        }

        public static AssetTransactionDTO ToDTO(AssetTransferRequestViewModel vm)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            if (vm.IsNotNull())
            {
                var transactionDTO = new AssetTransactionDTO();
                transactionDTO.AssetTransactionHead = new AssetTransactionHeadDTO();
                transactionDTO.AssetTransactionDetails = new List<AssetTransactionDetailsDTO>();

                if (vm.MasterViewModel.IsNotNull())
                {
                    if (vm.MasterViewModel.DocumentType == null || vm.MasterViewModel.DocumentType.Key == null)
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
                    transactionDTO.AssetTransactionHead.DocumentStatusID = Convert.ToInt32(vm.MasterViewModel.DocumentStatus.Key);

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
                            var transactionDetailDTO = new AssetTransactionDetailsDTO();

                            transactionDetailDTO.DetailIID = transactionDetail.TransactionDetailID;
                            transactionDetailDTO.HeadID = transactionDetail.TransactionHeadID;
                            transactionDetailDTO.AssetID = transactionDetail.Asset != null ? !string.IsNullOrEmpty(transactionDetail.Asset.Key) ? Convert.ToInt32(transactionDetail.Asset.Key) : (long?)null : (long?)null;
                            transactionDetailDTO.Quantity = Convert.ToInt32(transactionDetail.Quantity);
                            //transactionDetailDTO.UnitID = 1;//currently hard coding it to 1
                            
                            transactionDetailDTO.CreatedBy = transactionDetail.CreatedBy;
                            transactionDetailDTO.CreatedDate = transactionDetail.CreatedDate;
                            transactionDetailDTO.UpdatedBy = transactionDetail.UpdatedBy;
                            transactionDetailDTO.UpdatedDate = transactionDetail.UpdatedDate;

                            transactionDTO.AssetTransactionDetails.Add(transactionDetailDTO);
                        }
                    }
                }

                if (transactionDTO.AssetTransactionDetails.Count() == 0)
                {
                    throw new Exception("Select at least one asset to initiate a transfer request!");
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Accounting;

namespace Eduegate.Web.Library.ViewModels.Accounts
{
    public class AssetDepreciationViewModel : BaseMasterViewModel
    {
        public AssetDepreciationViewModel()
        {
            DetailViewModel = new List<AssetDepreciationDetailViewModel>();
            MasterViewModel = new AssetDepreciationMasterViewModel();
        }

        public List<AssetDepreciationDetailViewModel> DetailViewModel { get; set; }
        public AssetDepreciationMasterViewModel MasterViewModel { get; set; }


        public AssetTransactionHeadDTO ToAssetTransactionHeadDTO(AssetDepreciationViewModel viewModel)
        {
            AssetDepreciationMasterViewModel masterViewModel = viewModel.MasterViewModel;
            List<AssetDepreciationDetailViewModel> DeatilViewModelList = viewModel.DetailViewModel;

            AssetTransactionHeadDTO headDTO = new AssetTransactionHeadDTO();

            headDTO.DocumentStatusID = masterViewModel.DocumentStatus != null ? !string.IsNullOrEmpty(masterViewModel.DocumentStatus.Key) ? Convert.ToInt32(masterViewModel.DocumentStatus.Key) : (long?)null : (long?)null;
            headDTO.DocumentTypeID = masterViewModel.DocumentType != null ? !string.IsNullOrEmpty(masterViewModel.DocumentType.Key) ? Convert.ToInt32(masterViewModel.DocumentType.Key) : (int?)null : (int?)null;
            headDTO.ProcessingStatusID = masterViewModel.TransactionStatus != null ? !string.IsNullOrEmpty(masterViewModel.TransactionStatus.Key) ? Convert.ToByte( masterViewModel.TransactionStatus.Key) :(Byte?)null : (Byte?)null;


            headDTO.CreatedBy = masterViewModel.CreatedBy;
            headDTO.CreatedDate = masterViewModel.CreatedDate;
            headDTO.EntryDate = Convert.ToDateTime(masterViewModel.EntryDate.ToString());
            headDTO.Remarks = masterViewModel.Remarks;
            headDTO.UpdatedBy = masterViewModel.UpdatedBy;
            headDTO.UpdatedDate = masterViewModel.UpdatedDate;
            headDTO.HeadIID = masterViewModel.HeadIID;

            headDTO.AssetTransactionDetails = new List<AssetTransactionDetailsDTO>();

            foreach (AssetDepreciationDetailViewModel detailVMItem in DeatilViewModelList)
            {
                AssetTransactionDetailsDTO detailDTO = new AssetTransactionDetailsDTO();

                detailDTO.AssetID = detailVMItem.AssetCode != null ? !string.IsNullOrEmpty(detailVMItem.AssetCode.Key) ? Convert.ToInt32(detailVMItem.AssetCode.Key) : (long?)null : (long?)null;
                detailDTO.AccountID = detailVMItem.AssetGlAccount != null ? !string.IsNullOrEmpty(detailVMItem.AssetGlAccount.Key) ? Convert.ToInt32(detailVMItem.AssetGlAccount.Key) : (long?)null : (long?)null;

                detailDTO.Amount = detailVMItem.Amount;

                //if (detailVMItem.Credit > 0)
                //{
                //    detailDTO.Amount = detailVMItem.Credit;
                //}
                //else
                //{
                //    detailDTO.Amount = -1 * detailVMItem.Debit;
                //}
                //detailDTO.AssetID = detailVMItem.AssetID;
                detailDTO.Quantity = detailVMItem.Quantity;
                detailDTO.StartDate = Convert.ToDateTime(detailVMItem.StartDate);
                detailDTO.UpdatedBy = detailVMItem.UpdatedBy;
                detailDTO.UpdatedDate = detailVMItem.UpdatedDate;
                detailDTO.CreatedBy = detailVMItem.CreatedBy;
                detailDTO.CreatedDate = detailVMItem.CreatedDate;
                detailDTO.DetailIID = detailVMItem.DetailIID;
                detailDTO.HeadID = detailVMItem.HeadID;

                headDTO.AssetTransactionDetails.Add(detailDTO);
            }
            return headDTO;
        }

        public AssetDepreciationViewModel ToAssetTransactionViewModel(AssetTransactionHeadDTO headDTO)
        {
            AssetDepreciationViewModel assetDepreciationViewModel = new AssetDepreciationViewModel();
            assetDepreciationViewModel.MasterViewModel = new AssetDepreciationMasterViewModel();
            assetDepreciationViewModel.DetailViewModel = new List<AssetDepreciationDetailViewModel>();

            assetDepreciationViewModel.MasterViewModel.DocumentStatusID = headDTO.DocumentStatusID;
            assetDepreciationViewModel.MasterViewModel.DocumentTypeID = headDTO.DocumentTypeID;
            assetDepreciationViewModel.MasterViewModel.EntryDate = headDTO.EntryDate.ToString();
            assetDepreciationViewModel.MasterViewModel.ProcessingStatusID = headDTO.ProcessingStatusID;
            assetDepreciationViewModel.MasterViewModel.Remarks = headDTO.Remarks;
            assetDepreciationViewModel.MasterViewModel.UpdatedBy = headDTO.UpdatedBy;
            assetDepreciationViewModel.MasterViewModel.UpdatedDate = headDTO.UpdatedDate;
            assetDepreciationViewModel.MasterViewModel.HeadIID = headDTO.HeadIID;

            if (headDTO.DocumentStatus != null)
                assetDepreciationViewModel.MasterViewModel.DocumentStatus = new KeyValueViewModel() { Key = headDTO.DocumentStatus.Key, Value = headDTO.DocumentStatus.Value };
            if (headDTO.DocumentType != null)
                assetDepreciationViewModel.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = headDTO.DocumentType.Key, Value = headDTO.DocumentType.Value };
            if (headDTO.TransactionStatus != null)
                assetDepreciationViewModel.MasterViewModel.TransactionStatus = new KeyValueViewModel() { Key = headDTO.TransactionStatus.Key, Value = headDTO.TransactionStatus.Value };

            foreach (AssetTransactionDetailsDTO detailDTOItem in headDTO.AssetTransactionDetails)
            {
                AssetDepreciationDetailViewModel detailViewModel = new AssetDepreciationDetailViewModel();

                detailViewModel.Amount = detailDTOItem.Amount;
                detailViewModel.Quantity = detailDTOItem.Quantity != null ? Convert.ToInt32(detailDTOItem.Quantity) : 0;
                detailViewModel.TotalDepreciationAmount = (detailDTOItem.Amount * detailViewModel.Quantity);

                detailViewModel.AssetID = detailDTOItem.AssetID;                
                detailViewModel.StartDate = detailDTOItem.StartDate.ToString();
                detailViewModel.UpdatedBy = detailDTOItem.UpdatedBy;
                detailViewModel.UpdatedDate = detailDTOItem.UpdatedDate;
                detailViewModel.AccountID = detailDTOItem.AccountID;
                detailViewModel.CreatedBy = detailDTOItem.Createdby;
                detailViewModel.CreatedDate = detailDTOItem.CreatedDate;
                detailViewModel.DetailIID = detailDTOItem.DetailIID;
                detailViewModel.HeadID = detailDTOItem.HeadID;
                detailViewModel.AssetGlAccID = detailDTOItem.AccountID != null ? Convert.ToInt64(detailDTOItem.AccountID) : 0;
                //if (detailViewModel.Amount > 0)
                //{
                //    detailViewModel.Credit = detailViewModel.Amount;
                //}
                //else
                //{
                //    detailViewModel.Debit = -1 * detailViewModel.Amount;
                //}

                //Set Asset Object
                if (detailDTOItem.Asset != null)
                {
                    detailViewModel.AssetCategoryID = detailDTOItem.Asset.AssetCategoryID;
                    if (detailDTOItem.Asset.AssetCategory != null)
                    {
                        detailViewModel.CategoryName = detailDTOItem.Asset.AssetCategory.Value;
                    }
                    detailViewModel.AccumulatedDepreciation = detailDTOItem.Asset.AccumulatedDepreciation != null ? Convert.ToDecimal(detailDTOItem.Asset.AccumulatedDepreciation) : 0;
                    //detailViewModel.AssetCode = detailDTOItem.Asset.AssetCode;
                    detailViewModel.Description = detailDTOItem.Asset.Description;
                    detailViewModel.DepreciationYears = detailDTOItem.Asset.DepreciationYears;
                    detailViewModel.AssetStartDate = Convert.ToString( detailDTOItem.Asset.StartDate);
                    detailViewModel.AssetValue = detailDTOItem.Asset.AssetValue <0 ?(detailDTOItem.Asset.AssetValue *-1):detailDTOItem.Asset.AssetValue;
                    detailViewModel.AssetQuanity = detailDTOItem.Asset.Quantity;
                    detailViewModel.NetDeprecition = detailViewModel.AccumulatedDepreciation + detailDTOItem.Amount;
                }

                //Set Account Object
                if (detailDTOItem.Account != null)
                {
                    detailViewModel.AccountID = detailDTOItem.Account.AccountID;
                    detailViewModel.Description = detailDTOItem.Account.AccountName;
                }
                if (detailDTOItem.AssetCode != null)
                    detailViewModel.AssetCode = new KeyValueViewModel() { Key = detailDTOItem.AssetCode.Key, Value = detailDTOItem.AssetCode.Value };
                if (detailDTOItem.AssetGlAccount != null)
                {
                    detailViewModel.AssetGlAccount = new KeyValueViewModel() { Key = detailDTOItem.AssetGlAccount.Key, Value = detailDTOItem.AssetGlAccount.Value };
                }

                assetDepreciationViewModel.DetailViewModel.Add(detailViewModel);
            }
            return assetDepreciationViewModel;
        }
    }
}

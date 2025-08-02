using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Accounting;

namespace Eduegate.Web.Library.ViewModels.Accounts
{
    public class AssetDisposalViewModel : BaseMasterViewModel
    {
        public AssetDisposalViewModel()
        {
            DetailViewModel = new List<AssetDisposalDetailViewModel>();
            MasterViewModel = new AssetDisposalMasterViewModel();
        }

        public List<AssetDisposalDetailViewModel> DetailViewModel { get; set; }
        public AssetDisposalMasterViewModel MasterViewModel { get; set; }

        public AssetTransactionHeadDTO ToAssetTransactionHeadDTO(AssetDisposalViewModel viewModel)
        {
            AssetDisposalMasterViewModel masterViewModel = viewModel.MasterViewModel;
            List<AssetDisposalDetailViewModel> DeatilViewModelList = viewModel.DetailViewModel;

            AssetTransactionHeadDTO headDTO = new AssetTransactionHeadDTO();

            headDTO.DocumentStatusID = masterViewModel.DocumentStatus != null ? !string.IsNullOrEmpty(masterViewModel.DocumentStatus.Key) ? Convert.ToInt32(masterViewModel.DocumentStatus.Key) : (long?)null : (long?)null;
            headDTO.DocumentTypeID = masterViewModel.DocumentType != null ? !string.IsNullOrEmpty(masterViewModel.DocumentType.Key) ? Convert.ToInt32(masterViewModel.DocumentType.Key) : (int?)null : (int?)null;
            headDTO.ProcessingStatusID = masterViewModel.TransactionStatus != null ? !string.IsNullOrEmpty(masterViewModel.TransactionStatus.Key) ? Convert.ToByte(masterViewModel.TransactionStatus.Key) : (Byte?)null : (Byte?)null;

            headDTO.CreatedBy = masterViewModel.CreatedBy;
            headDTO.CreatedDate = masterViewModel.CreatedDate;
            headDTO.EntryDate = Convert.ToDateTime(masterViewModel.EntryDate.ToString());
            headDTO.Remarks = masterViewModel.Remarks;
            headDTO.UpdatedBy = masterViewModel.UpdatedBy;
            headDTO.UpdatedDate = masterViewModel.UpdatedDate;
            headDTO.HeadIID = masterViewModel.HeadIID;

            headDTO.AssetTransactionDetails = new List<AssetTransactionDetailsDTO>();

            foreach (AssetDisposalDetailViewModel detailVMItem in DeatilViewModelList)
            {
                AssetTransactionDetailsDTO detailDTO = new AssetTransactionDetailsDTO();

                detailDTO.AssetID = detailVMItem.AssetCode != null ? !string.IsNullOrEmpty(detailVMItem.AssetCode.Key) ? Convert.ToInt32(detailVMItem.AssetCode.Key) : (long?)null : (long?)null;
                detailDTO.AccountID = detailVMItem.AssetGlAccount != null ? !string.IsNullOrEmpty(detailVMItem.AssetGlAccount.Key) ? Convert.ToInt32(detailVMItem.AssetGlAccount.Key) : (long?)null : (long?)null;


                if (detailVMItem.Credit > 0)
                {
                    detailDTO.Amount = detailVMItem.Credit;
                }
                else
                {
                    detailDTO.Amount = -1 * detailVMItem.Debit;
                }
                detailDTO.Quantity = detailVMItem.Quantity;
                detailDTO.StartDate = detailVMItem.StartDate != null ? Convert.ToDateTime(detailVMItem.StartDate) : (DateTime?)null;
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

        public AssetDisposalViewModel ToAssetTransactionViewModel(AssetTransactionHeadDTO headDTO)
        {
            AssetDisposalViewModel assetDisposalViewModel = new AssetDisposalViewModel();
            assetDisposalViewModel.MasterViewModel = new AssetDisposalMasterViewModel();
            assetDisposalViewModel.DetailViewModel = new List<AssetDisposalDetailViewModel>();

            assetDisposalViewModel.MasterViewModel.DocumentStatusID = headDTO.DocumentStatusID;
            assetDisposalViewModel.MasterViewModel.DocumentTypeID = headDTO.DocumentTypeID;
            assetDisposalViewModel.MasterViewModel.EntryDate = headDTO.EntryDate.ToString();
            assetDisposalViewModel.MasterViewModel.ProcessingStatusID = headDTO.ProcessingStatusID;
            assetDisposalViewModel.MasterViewModel.Remarks = headDTO.Remarks;
            assetDisposalViewModel.MasterViewModel.UpdatedBy = headDTO.UpdatedBy;
            assetDisposalViewModel.MasterViewModel.UpdatedDate = headDTO.UpdatedDate;
            assetDisposalViewModel.MasterViewModel.HeadIID = headDTO.HeadIID;

            if (headDTO.DocumentStatus != null)
                assetDisposalViewModel.MasterViewModel.DocumentStatus = new KeyValueViewModel() { Key = headDTO.DocumentStatus.Key, Value = headDTO.DocumentStatus.Value };
            if (headDTO.DocumentType != null)
                assetDisposalViewModel.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = headDTO.DocumentType.Key, Value = headDTO.DocumentType.Value };
            if (headDTO.TransactionStatus != null)
                assetDisposalViewModel.MasterViewModel.TransactionStatus = new KeyValueViewModel() { Key = headDTO.TransactionStatus.Key, Value = headDTO.TransactionStatus.Value };

            foreach (AssetTransactionDetailsDTO detailDTOItem in headDTO.AssetTransactionDetails)
            {
                AssetDisposalDetailViewModel detailViewModel = new AssetDisposalDetailViewModel();

                detailViewModel.Amount = detailDTOItem.Amount;
                detailViewModel.AssetID = detailDTOItem.AssetID;
                detailViewModel.Quantity = detailDTOItem.Quantity != null ? Convert.ToInt32(detailDTOItem.Quantity) : 0;
                detailViewModel.StartDate = detailDTOItem.StartDate.ToString();
                detailViewModel.UpdatedBy = detailDTOItem.UpdatedBy;
                detailViewModel.UpdatedDate = detailDTOItem.UpdatedDate;
                detailViewModel.AccountID = detailDTOItem.AccountID;
                detailViewModel.CreatedBy = detailDTOItem.Createdby;
                detailViewModel.CreatedDate = detailDTOItem.CreatedDate;
                detailViewModel.DetailIID = detailDTOItem.DetailIID;
                detailViewModel.HeadID = detailDTOItem.HeadID;
                detailViewModel.AssetGlAccID = detailDTOItem.AccountID != null ? Convert.ToInt64(detailDTOItem.AccountID) : 0;

                

                if (detailViewModel.Amount > 0)
                {
                    detailViewModel.Credit = detailViewModel.Amount;
                    detailViewModel.CreditTotal = (detailViewModel.Quantity == 0 ? 1 : detailViewModel.Quantity) * (detailViewModel.Amount < 0 ? detailViewModel.Amount * -1 : detailViewModel.Amount);

                }
                else
                {
                    detailViewModel.Debit = -1 * detailViewModel.Amount;
                    detailViewModel.DebitTotal = (detailViewModel.Quantity == 0 ? 1 : detailViewModel.Quantity) * (detailViewModel.Amount < 0 ? detailViewModel.Amount * -1 : detailViewModel.Amount);

                }

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

                assetDisposalViewModel.DetailViewModel.Add(detailViewModel);
            }
            return assetDisposalViewModel;
        }
    }
}

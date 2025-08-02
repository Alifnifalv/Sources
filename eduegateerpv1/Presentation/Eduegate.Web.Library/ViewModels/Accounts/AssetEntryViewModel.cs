using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Accounting;

namespace Eduegate.Web.Library.ViewModels.Accounts
{
    public class AssetEntryViewModel : BaseMasterViewModel
    {
        public AssetEntryViewModel()
        {
            DetailViewModel = new List<AssetEntryDetailViewModel>();
            MasterViewModel = new AssetEntryMasterViewModel();
        }

        public List<AssetEntryDetailViewModel> DetailViewModel { get; set; }
        public AssetEntryMasterViewModel MasterViewModel { get; set; }

        public AssetTransactionHeadDTO ToAssetTransactionHeadDTO(AssetEntryViewModel viewModel)
        {
            AssetEntryMasterViewModel masterViewModel = viewModel.MasterViewModel;
            List<AssetEntryDetailViewModel> DeatilViewModelList = viewModel.DetailViewModel;

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

            foreach (AssetEntryDetailViewModel detailVMItem in DeatilViewModelList)
            {
                AssetTransactionDetailsDTO detailDTO = new AssetTransactionDetailsDTO();

                detailDTO.AssetID = detailVMItem.AssetCode != null ? !string.IsNullOrEmpty(detailVMItem.AssetCode.Key) ? Convert.ToInt32(detailVMItem.AssetCode.Key) : (long?)null : (long?)null;
                detailDTO.AccountID = detailVMItem.AssetGlAccount != null ? !string.IsNullOrEmpty(detailVMItem.AssetGlAccount.Key) ? Convert.ToInt32(detailVMItem.AssetGlAccount.Key) : (long?)null : (long?)null;

                //detailDTO.Amount = detailVMItem.Amount;

                if(detailVMItem.Credit>0)
                {
                    detailDTO.Amount = detailVMItem.Credit;
                }
                else
                {
                    detailDTO.Amount = -1 * detailVMItem.Debit;
                }
                //detailDTO.AssetID = detailVMItem.AssetID;
                detailDTO.Quantity = detailVMItem.Quantity;
                detailDTO.StartDate =detailVMItem.StartDate !=null? Convert.ToDateTime(detailVMItem.StartDate): (DateTime?)null;
                detailDTO.UpdatedBy = detailVMItem.UpdatedBy;
                detailDTO.UpdatedDate = detailVMItem.UpdatedDate;
                //detailDTO.AccountID = detailVMItem.AccountID;
                detailDTO.CreatedBy = detailVMItem.CreatedBy;
                detailDTO.CreatedDate = detailVMItem.CreatedDate;
                detailDTO.DetailIID = detailVMItem.DetailIID;
                detailDTO.HeadID = detailVMItem.HeadID;

                headDTO.AssetTransactionDetails.Add(detailDTO);
            }
            return headDTO;
        }

        public AssetEntryViewModel ToAssetTransactionViewModel(AssetTransactionHeadDTO headDTO)
        {
            AssetEntryViewModel assetEntryViewModel = new AssetEntryViewModel();
            assetEntryViewModel.MasterViewModel = new AssetEntryMasterViewModel();
            assetEntryViewModel.DetailViewModel = new List< AssetEntryDetailViewModel>();

            assetEntryViewModel.MasterViewModel.DocumentStatusID = headDTO.DocumentStatusID;
            assetEntryViewModel.MasterViewModel.DocumentTypeID = headDTO.DocumentTypeID;
            assetEntryViewModel.MasterViewModel.EntryDate =headDTO.EntryDate.ToString();
            assetEntryViewModel.MasterViewModel.ProcessingStatusID = headDTO.ProcessingStatusID;
            assetEntryViewModel.MasterViewModel.Remarks = headDTO.Remarks;
            assetEntryViewModel.MasterViewModel.UpdatedBy = headDTO.UpdatedBy;
            assetEntryViewModel.MasterViewModel.UpdatedDate = headDTO.UpdatedDate;
            assetEntryViewModel.MasterViewModel.HeadIID = headDTO.HeadIID;

            if(headDTO.DocumentStatus!=null)
                assetEntryViewModel.MasterViewModel.DocumentStatus = new KeyValueViewModel() { Key = headDTO.DocumentStatus.Key, Value = headDTO.DocumentStatus.Value }  ;
            if(headDTO.DocumentType !=null)
                assetEntryViewModel.MasterViewModel.DocumentType =new  KeyValueViewModel() { Key = headDTO.DocumentType.Key, Value = headDTO.DocumentType.Value };
            if (headDTO.TransactionStatus != null)
                assetEntryViewModel.MasterViewModel.TransactionStatus = new KeyValueViewModel() { Key = headDTO.TransactionStatus.Key, Value = headDTO.TransactionStatus.Value };

            foreach (AssetTransactionDetailsDTO detailDTOItem in headDTO.AssetTransactionDetails)
            {
                AssetEntryDetailViewModel detailViewModel = new AssetEntryDetailViewModel();

                detailViewModel.Amount = detailDTOItem.Amount;
                detailViewModel.AssetID = detailDTOItem.AssetID;
                detailViewModel.Quantity = detailDTOItem.Quantity !=null?Convert.ToInt32( detailDTOItem.Quantity):0;
                detailViewModel.StartDate = detailDTOItem.StartDate.ToString();
                detailViewModel.UpdatedBy = detailDTOItem.UpdatedBy;
                detailViewModel.UpdatedDate = detailDTOItem.UpdatedDate;
                detailViewModel.AccountID = detailDTOItem.AccountID;
                detailViewModel.CreatedBy = detailDTOItem.Createdby;
                detailViewModel.CreatedDate = detailDTOItem.CreatedDate;
                detailViewModel.DetailIID = detailDTOItem.DetailIID;
                detailViewModel.HeadID = detailDTOItem.HeadID;
                detailViewModel.AssetGlAccID = detailDTOItem.AccountID !=null? Convert.ToInt64( detailDTOItem.AccountID):0;
                if (detailViewModel.Amount > 0)
                {
                    detailViewModel.Credit = detailViewModel.Amount;
                    detailViewModel.CreditTotal =  (detailViewModel.Quantity == 0? 1: detailViewModel.Quantity) * (detailViewModel.Amount <0? detailViewModel.Amount * -1: detailViewModel.Amount);
                }
                else
                {
                    detailViewModel.Debit = -1*detailViewModel.Amount;
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
                    detailViewModel.AccumulatedDepreciation = detailDTOItem.Asset.AccumulatedDepreciation!=null? Convert.ToDecimal( detailDTOItem.Asset.AccumulatedDepreciation):0;
                    //detailViewModel.AssetCode = detailDTOItem.Asset.AssetCode;
                    detailViewModel.Description = detailDTOItem.Asset.Description;
                }

                //Set Account Object
                if (detailDTOItem.Account != null)
                {
                    detailViewModel.AccountID = detailDTOItem.Account.AccountID;
                    detailViewModel.Description = detailDTOItem.Account.AccountName;                    
                }
                if(detailDTOItem.AssetCode !=null)
                    detailViewModel.AssetCode = new KeyValueViewModel() { Key = detailDTOItem.AssetCode.Key, Value = detailDTOItem.AssetCode.Value };
                if (detailDTOItem.AssetGlAccount != null)
                {
                    detailViewModel.AssetGlAccount = new KeyValueViewModel() { Key = detailDTOItem.AssetGlAccount.Key, Value = detailDTOItem.AssetGlAccount.Value };
                }

                assetEntryViewModel.DetailViewModel.Add(detailViewModel);
            }
            return assetEntryViewModel;
        }
    }
}


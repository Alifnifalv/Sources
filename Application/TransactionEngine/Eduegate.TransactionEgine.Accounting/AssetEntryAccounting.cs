using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.TransactionEgine.Accounting.Interfaces;
using Eduegate.TransactionEgine.Accounting.ViewModels;
using Eduegate.Services.Contracts.Accounts.Assets;

namespace Eduegate.TransactionEgine.Accounting
{
    public class AssetEntryAccounting : AccountingBase, IAssetEntryTransactions
    {
        private Action<string> _logError;

        public AssetEntryAccounting(Action<string> logError)
        {
            _logError = logError;
        }

        public Framework.Enums.DocumentReferenceTypes ReferenceTypes
        {
            get { return Framework.Enums.DocumentReferenceTypes.AssetEntry; }
        }
        private void WriteLog(string message, Exception ex = null)
        {
            if (_logError != null)
                _logError(message);
        }
        public void Process(AssetTransactionHeadDTO assetTransactionHead)
        {
            try
            {
                long transactionHeadIID = assetTransactionHead.HeadIID;
               
                var AccountTransactionViewModelList = new List<ViewModels.AccountTransactionViewModel>();
                
                //Asset's Asset Value
                foreach (var detailItem_Asset in assetTransactionHead.AssetTransactionDetails.Where(x=>x.AssetID !=null).ToList())
                {
                    var amount =  detailItem_Asset.Quantity * detailItem_Asset.Amount;
                    if (amount < 0) amount = -1 * amount;
                    string description = "Asset Entry: " + detailItem_Asset.Asset.AssetCode + ": " + detailItem_Asset.Asset.Description + "- Asset Value";
                    Create_Asset_TransactionHeadViewModel((long) detailItem_Asset.AccountID, amount, CONST_DEBIT, transactionHeadIID, description);
                }
                //Account Values- Other than Asset Values.
                foreach (var detailItem_Asset in assetTransactionHead.AssetTransactionDetails.Where(x => x.AssetID == null).ToList())
                {
                    var amount = detailItem_Asset.Amount.Value;
                    if (amount < 0) amount = -1 * amount;
                    string description = "Asset Entry: " + "Other amounts";
                    Create_Asset_TransactionHeadViewModel((long)detailItem_Asset.AccountID, amount, CONST_CREDIT, transactionHeadIID, description);
                }
                //Add to DB
                bool IsDBOperationSuccess = false;
                if (AccountTransactionViewModelList != null && AccountTransactionViewModelList.Count > 0)
                {
                    var AccountTransactionsDTOList = AccountTransactionViewModel.ConvertViewModelToDTO(AccountTransactionViewModelList);
                    IsDBOperationSuccess = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().AddAccountTransactions(AccountTransactionsDTOList);
                }

                //if (IsDBOperationSuccess)
                //{                    
                //    //Update ProcessingStatusID and DocumentStatusID in AssetTransactionHead
                //    UpdateAssetTransactionHead(assetTransactionHead, Eduegate.Framework.Enums.TransactionStatus.Complete, Services.Contracts.Enums.DocumentStatuses.Completed);
                //}
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message.ToString(), ex);
                WriteLog("Exception occured in AssetEntryAccounting-Process.:" + ex.Message, ex);
            }
        }

    }
}

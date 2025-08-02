using System;
using System.Collections.Generic;
using Eduegate.TransactionEgine.Accounting.Interfaces;
using Eduegate.TransactionEgine.Accounting.ViewModels;
using Eduegate.Services.Contracts.Accounting;

namespace Eduegate.TransactionEgine.Accounting
{
    public class AssetDepreciationAccounting : AccountingBase, IAssetDepreciationTransactions
    {
        private Action<string> _logError;

        public AssetDepreciationAccounting(Action<string> logError)
        {
            _logError = logError;
        }

        public Framework.Enums.DocumentReferenceTypes ReferenceTypes
        {
            get { return Framework.Enums.DocumentReferenceTypes.AssetDepreciation; }
        }
        private void WriteLog(string message, Exception ex = null)
        {
            if (_logError != null)
                _logError(message);
        }
        public void Process(AssetTransactionHeadDTO assetTransactionHead)
        {

            WriteLog("AssetDepreciationAccounting-AccountingProcess started for TransactionID:" + assetTransactionHead.HeadIID.ToString());

            try
            {
                long transactionHeadIID = assetTransactionHead.HeadIID;
                List<ViewModels.AccountTransactionViewModel> AccountTransactionViewModelList = new List<ViewModels.AccountTransactionViewModel>();

                foreach (var detailItem_Asset in assetTransactionHead.AssetTransactionDetails)
                {
                    var amount = detailItem_Asset.Amount;
                    if (amount < 0) amount = -1 * amount;
                    string description = "Asset Depreciation: " + detailItem_Asset.Asset.AssetCode + ": " + detailItem_Asset.Asset.Description + "- Depreciation Account";
                    Create_Asset_TransactionHeadViewModel((long)detailItem_Asset.Asset.DepreciationExpGLAccId, amount, CONST_DEBIT, transactionHeadIID, description);

                    description = "Asset Depreciation: " + detailItem_Asset.Asset.AssetCode + ": " + detailItem_Asset.Asset.Description + "- Accumulated Depreciation Account";
                    Create_Asset_TransactionHeadViewModel((long)detailItem_Asset.Asset.AccumulatedDepGLAccID, amount, CONST_CREDIT, transactionHeadIID, description);
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
                //     //Update ProcessingStatusID and DocumentStatusID in AssetTransactionHead
                //    UpdateAssetTransactionHead(assetTransactionHead, Eduegate.Framework.Enums.TransactionStatus.Complete, Services.Contracts.Enums.DocumentStatuses.Completed);

                //}
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message.ToString(), ex);
                WriteLog("Exception occured in AssetDepreciationAccounting-Process.:" + ex.Message, ex);
            }
        }

    }
}

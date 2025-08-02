using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.TransactionEgine.Accounting.Interfaces;
using Eduegate.TransactionEgine.Accounting.ViewModels;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Framework.Enums;

namespace Eduegate.TransactionEgine.Accounting
{
    public class AssetRemovalAccounting : AccountingBase, IAssetRemovalTransactions
    {
        private Action<string> _logError;

        public AssetRemovalAccounting(Action<string> logError)
        {
            _logError = logError;
        }

        public Framework.Enums.DocumentReferenceTypes ReferenceTypes
        {
            get { return Framework.Enums.DocumentReferenceTypes.AssetRemoval; }
        }
        private void WriteLog(string message, Exception ex = null)
        {
            if (_logError != null)
                _logError(message);
        }
        public void Process(AssetTransactionHeadDTO assetTransactionHead)
        {
            WriteLog("AssetRemovalAccounting-AccountingProcess started for TransactionID:" + assetTransactionHead.HeadIID.ToString());

            try
            {
                long transactionHeadIID = assetTransactionHead.HeadIID;
                var AccountTransactionViewModelList = new List<ViewModels.AccountTransactionViewModel>();

                //NOTE: For asset removal there will be only one "Asset" in the details. i.e. in one tranascation only asset can be removed
                //This should be validated in the Removal screen


                //If Cash or amount of sale > (Assets Value - Acc Depreciation) then
                //DB  Cash Credit etc…
                //DB Accumulated depreciation for the Asset
                //CR  Assets GL Code
                //CR  Profit / Loss from Assets sold


                //If Cash or amount of sale < (Assets - Acc Depreciation) then
                //DB  Cash Credit etc…
                //DB Accumulated depreciation for the Asset
                //DB  Profit / Loss from Assets sold
                //CR  Assets GL Code
                var detailItem_Asset = assetTransactionHead.AssetTransactionDetails.Where(x => x.AssetID != null).FirstOrDefault();
                int Quantity = (int) detailItem_Asset.Quantity;
                long assetId = (long)detailItem_Asset.AssetID;

                var assetDTO= new Eduegate.TransactionEgine.ClientFactory.ClientFactory().GetAssetTransactionHeadById(assetId);
                var assetDetail = assetDTO.AssetTransactionDetails.FirstOrDefault();

                string AssetName = assetDetail.AssetCode + ": " + assetDetail.Asset.Description;

                var totalAssetValue = Convert.ToDecimal(assetDTO.AssetTransactionDetails.Sum(a => a.Amount));
                decimal AssetValue = (totalAssetValue < 0? (totalAssetValue * -1) : totalAssetValue) * Quantity;
                var AccumulatedDepreciation = Convert.ToDecimal(new Eduegate.TransactionEgine.ClientFactory.ClientFactory().GetAccumulatedDepreciation(assetDetail.Asset.AssetIID, (int)DocumentTypes.AssetDepreciation));//For One Unit

                decimal SaleAmount = Convert.ToDecimal( detailItem_Asset.Amount<0? (detailItem_Asset.Amount*-1): detailItem_Asset.Amount);
                SaleAmount = (SaleAmount * Quantity);
                decimal TotalAccumulatedDepreciation = AccumulatedDepreciation * Quantity;
                decimal CurrentAssetValue = AssetValue - TotalAccumulatedDepreciation;
                decimal Profit_Loss_Amount = CurrentAssetValue - SaleAmount;
                var GL_PROFIT_LOSS_AccId = GetGLAccountId(GetSettingByCode(GL_PROFIT_LOSS_CODE));//Profit and Loss account

                string description = "Asset Removal: " + AssetName + "- ProfitLoss Amount. SaleAmount:" + SaleAmount.ToString() + " AssetValue:"+ AssetValue.ToString()
                                        + " CurrentAssetValue = AssetValue - AccumulatedDepreciation: " + CurrentAssetValue.ToString()
                                        + " AccumulatedDepreciation:" + AccumulatedDepreciation.ToString();

                if (SaleAmount >CurrentAssetValue )
                {
                    //Debit
                    if (Profit_Loss_Amount < 0) Profit_Loss_Amount = -1 * Profit_Loss_Amount;
                    Create_Asset_TransactionHeadViewModel(GL_PROFIT_LOSS_AccId, Profit_Loss_Amount, CONST_CREDIT, transactionHeadIID, description);
                }
                else
                {
                    //Credit
                    if (Profit_Loss_Amount < 0) Profit_Loss_Amount = -1 * Profit_Loss_Amount;
                    Create_Asset_TransactionHeadViewModel(GL_PROFIT_LOSS_AccId, Profit_Loss_Amount, CONST_DEBIT, transactionHeadIID, description);
                }

                Create_Asset_TransactionHeadViewModel((long)detailItem_Asset.Asset.AccumulatedDepGLAccID, TotalAccumulatedDepreciation, CONST_DEBIT, transactionHeadIID, "Asset Removal: " + AssetName+ "- Accumulated Depreciation");
                Create_Asset_TransactionHeadViewModel((long)detailItem_Asset.Asset.AssetGlAccID, AssetValue, CONST_CREDIT, transactionHeadIID,"Asset Removal: " + AssetName+ "- Asset Value :"  + AssetValue.ToString() + " Quantity : "+ Quantity.ToString());

                //Other amounts
                foreach (var detailItemNonAsset in assetTransactionHead.AssetTransactionDetails.Where(x => x.AssetID == null).ToList())
                {
                    var amount = Convert.ToDecimal(detailItemNonAsset.Amount);
                    if (amount < 0) amount = -1 * amount;
                    Create_Asset_TransactionHeadViewModel((long)detailItemNonAsset.AccountID, amount, CONST_DEBIT, transactionHeadIID, "Asset Removal: " + AssetName + "- Other amounts");
                }
                //Add to DB
                bool IsDBOperationSuccess = false;
                if (AccountTransactionViewModelList != null && AccountTransactionViewModelList.Count > 0)
                {
                    var AccountTransactionsDTOList = AccountTransactionViewModel.ConvertViewModelToDTO(AccountTransactionViewModelList);
                    IsDBOperationSuccess = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().AddAccountTransactions(AccountTransactionsDTOList);
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message.ToString(), ex);
                WriteLog("Exception occured in SalesReturnAccounting-Process.:" + ex.Message, ex);
            }
        }

    }
}

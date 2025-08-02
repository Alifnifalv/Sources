using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Framework.Enums;
using Eduegate.TransactionEgine.Accounting;
using Eduegate.Services.Contracts.Accounts.Assets;

namespace Eduegate.TransactionEngineCore
{
    public class InventoryJobTransaction : TransactionBase
    {
        public InventoryJobTransaction(Action<string> logError)
        {
            _logError = logError;
        }

        private List<AssetTransactionHeadDTO> GetAssetTransactions(DocumentReferenceTypes referenceTypes, TransactionStatus transactionStatus)
        {
            var _AssetTransactionHeadDTO = new List<AssetTransactionHeadDTO>();
            _AssetTransactionHeadDTO = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().GetAssetTransactionHeads(referenceTypes, transactionStatus);

            if (_AssetTransactionHeadDTO == null)
            {
                return null;
            }

            return _AssetTransactionHeadDTO;
        }

        public void StartProcess(DocumentReferenceTypes referenceTypes, TransactionStatus status)
        {
            WriteLog("Geting Asset Transactions - " + referenceTypes.ToString() + " " + status.ToString());
            var transactions = GetAssetTransactions(referenceTypes, status);
            WriteLog(" Asset Transaction found - " + transactions.Count());

            try
            {
                //for each start
                foreach (var assetTransaction in transactions)
                {
                    try
                    {
                        switch (referenceTypes)
                        {
                            case DocumentReferenceTypes.All:
                                break;

                            case DocumentReferenceTypes.ReceiptVoucherMission:
                                new AssetEntryAccounting(_logError).Process(assetTransaction);
                                break;
                            case DocumentReferenceTypes.ReceiptVoucherInvoice:
                                new AssetDepreciationAccounting(_logError).Process(assetTransaction);
                                break;
                            case DocumentReferenceTypes.ReceiptVoucherRegularReceipt:
                                new AssetRemovalAccounting(_logError).Process(assetTransaction);
                                break;

                            default:
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        //Eduegate.Logger.LogHelper<string>.Fatal(ex.Message.ToString(), ex);
                        WriteLog(ex.Message.ToString());
                    }
                }
                //for each end
            }
            catch (Exception ex)
            {
                //Eduegate.Logger.LogHelper<string>.Fatal(ex.Message.ToString(), ex);
                WriteLog(ex.Message.ToString());
            }
        }
    }
}

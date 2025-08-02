using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.TransactionEngineCore.Interfaces;
using Eduegate.TransactionEngineCore.ViewModels;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Framework.Enums;
using Eduegate.TransactionEgine.Accounting;

namespace Eduegate.TransactionEngineCore
{
    public class FixedAssetTransaction : TransactionBase
    {
        public FixedAssetTransaction(Action<string> logError)
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
            if (!IsTransactionEnabled(referenceTypes))
            {
                return;
            }

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

                            case DocumentReferenceTypes.AssetEntry:
                                new AssetEntryAccounting(_logError).Process(assetTransaction);
                                break;
                            case DocumentReferenceTypes.AssetDepreciation:
                                new AssetDepreciationAccounting(_logError).Process(assetTransaction);
                                break;
                            case DocumentReferenceTypes.AssetRemoval:
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

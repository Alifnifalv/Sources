using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.TransactionEgine.Accounting;
using Eduegate.Framework.Enums;
using Eduegate.TransactionEgine.Accounting.ViewModels;
using Eduegate.Framework;

namespace Eduegate.TransactionEngineCore
{
    public class AssetTransaction : AssetTransactionBase
    {
        private CallContext _callContext;

        public AssetTransaction(Action<string> logError, CallContext context = null)
        {
            _logError = logError;
            _callContext = context;
        }

        private AssetTransactionHeadViewModel GetAssetTransactionByID(long headID)
        {
            var _AssetTransactionHeadViewModel = new List<AssetTransactionHeadViewModel>();
            var _TransactionHeadDTO = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().GetAssetTransactionDetail(headID);

            if (_TransactionHeadDTO == null)
            {
                return null;
            }

            return AssetTransactionHeadViewModel.FromDTO(_TransactionHeadDTO);
        }

        private List<AssetTransactionHeadViewModel> GetAllAssetTransaction(DocumentReferenceTypes referenceTypes, TransactionStatus transactionStatus)
        {
            var _AssetTransactionHeadViewModel = new List<AssetTransactionHeadViewModel>();
            var _TransactionHeadDTO = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().GetAllAssetTransaction(referenceTypes, transactionStatus);

            if (_TransactionHeadDTO == null)
            {
                return null;
            }

            // convert TransactionDetail into TransactionDetailDTO
            foreach (var item in _TransactionHeadDTO)
            {
                _AssetTransactionHeadViewModel.Add(AssetTransactionHeadViewModel.FromDTO(item));
            }

            return _AssetTransactionHeadViewModel;
        }

        public void StartProcess(DocumentReferenceTypes referenceTypes, TransactionStatus status, long? transactionID = null, long? loginID = null)
        {
            if (!IsAssetTransactionEnabled(referenceTypes))
            {
                return;
            }

            var lastTransactionCount = new Domain.Setting.SettingBL().GetSettingValue<int>("MaxFetchCount");

            while (true)
            {
                if (lastTransactionCount == new Domain.Setting.SettingBL().GetSettingValue<int>("MaxFetchCount"))
                {
                    lastTransactionCount = Process(referenceTypes, status, transactionID, loginID);
                }
                else
                {
                    break;
                }
            }
        }

        private int Process(DocumentReferenceTypes referenceTypes, TransactionStatus status, long? transactionID = null, long? loginID = null)
        {
            //WriteLog("Method GetAllTransaction is calling for referenceTypes= " + referenceTypes + "and status= " + status);
            //LogHelper<Transaction>.Info("GetAllTransaction called for referenceTypes" + referenceTypes + "and status= " + status);
            WriteLog("Geting Transactions - " + referenceTypes.ToString() + " " + status.ToString());
            var transactions = new List<AssetTransactionHeadViewModel>();

            if (transactionID.HasValue)
            {
                transactions.Add(GetAssetTransactionByID(transactionID.Value));
            }
            else
            {
                transactions = GetAllAssetTransaction(referenceTypes, status);
            }

            WriteLog(" Transaction found - " + transactions.Count());
            var enabledAccountProcessing = new Domain.Setting.SettingBL(null).GetSettingValue<bool>("EnableAccountProcessing", false);

            try
            {
                //for each start
                foreach (var transaction in transactions)
                {
                    transaction.UpdatedBy = loginID.HasValue ? Convert.ToInt32(loginID) : null;

                    //should continue with next transaction
                    try
                    {
                        switch (transaction.DocumentReferenceTypeID)
                        {
                            case DocumentReferenceTypes.All:
                                break;
                            case DocumentReferenceTypes.AssetTransferIssue:
                                new AssetTransferIssue(_logError).Process(transaction);
                                if (enabledAccountProcessing)
                                {
                                }
                                break;
                            case DocumentReferenceTypes.AssetTransferReceipt:
                                new AssetTransferReceipt(_logError).Process(transaction);
                                if (enabledAccountProcessing)
                                {
                                    new AssetTransferReceiptAccounting(_logError, _callContext).Process(transaction);
                                }
                                break;
                            case DocumentReferenceTypes.AssetRemoval:
                                new AssetDisposal(_logError).Process(transaction);
                                if (enabledAccountProcessing)
                                {
                                    new AssetDisposalAccounting(_logError, _callContext).Process(transaction);
                                }
                                break;
                            case DocumentReferenceTypes.AssetEntryPurchase:
                                new AssetPurchaseInvoice(_logError, _callContext).Process(transaction);
                                if (enabledAccountProcessing)
                                {
                                    new AssetPurchaseInvoiceAccounting(_logError, _callContext).Process(transaction);
                                }
                                break;
                            case DocumentReferenceTypes.AssetDepreciation:
                                new AssetDepreciation(_logError, _callContext).Process(transaction);
                                if (enabledAccountProcessing)
                                {
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        //Eduegate.Logger.LogHelper<string>.Fatal(ex.Message.ToString(), ex);
                        WriteLog(ex.Message.ToString());
                        //throw ex;
                        throw;
                    }
                }
                //for each end
            }
            catch (Exception ex)
            {
                //Eduegate.Logger.LogHelper<string>.Fatal(ex.Message.ToString(), ex);
                WriteLog(ex.Message.ToString());
                //throw ex;
                throw;
            }

            return transactions.Count;
        }
    }
}

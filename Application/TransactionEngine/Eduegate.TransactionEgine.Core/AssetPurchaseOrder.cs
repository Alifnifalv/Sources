using System;
using Eduegate.Framework.Enums;
using Eduegate.TransactionEngineCore.Interfaces;

namespace Eduegate.TransactionEngineCore
{
    public class AssetPurchaseOrder : TransactionBase, ITransactions
    {
        public AssetPurchaseOrder(Action<string> logError)
        {
            _logError = logError;
        }

        public Framework.Enums.DocumentReferenceTypes ReferenceTypes
        {
            get { return Framework.Enums.DocumentReferenceTypes.PurchaseOrder; }
        }

        public void Process(ViewModels.TransactionHeadViewModel transaction)
        {
            #region Actual Code
            WriteLog("Asset purchase order-Process started for TransactionID:" + transaction.HeadIID.ToString());

            try
            {
                WriteLog("Processing asset purchase orders :" + transaction.HeadIID.ToString());

                switch ((Eduegate.Framework.Enums.TransactionStatus)transaction.TransactionStatusID)
                {
                    case Eduegate.Framework.Enums.TransactionStatus.New:
                    case Eduegate.Framework.Enums.TransactionStatus.Edit:
                        WriteLog("Processing new transaction.");
                        ProcessNew(transaction);
                        break;
                    case Eduegate.Framework.Enums.TransactionStatus.IntitiateReprocess:
                        switch ((Eduegate.Services.Contracts.Enums.DocumentStatuses)transaction.DocumentStatusID)
                        {
                            case Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled:
                                WriteLog("Cancelling transaction.");
                                ProcessCancellation(transaction);
                                break;
                        }
                        break;
                }

                WriteLog("Completed:" + transaction.HeadIID.ToString());
            }
            catch (Exception ex)
            {
                UpdateTransactionHead(transaction, Eduegate.Framework.Enums.TransactionStatus.Failed, Services.Contracts.Enums.DocumentStatuses.Draft);
                WriteLog(ex.Message.ToString(), ex);
                WriteLog("Exception occured in purchase orders-Process.:" + ex.Message, ex);
            }
            #endregion
        }

        public void ProcessNew(ViewModels.TransactionHeadViewModel transaction)
        {
            WriteLog("AssetPurchaseOrder-Process started for TransactionID:" + transaction.HeadIID.ToString());

            bool isSuccess = false;

            try
            {
                // If Transaction is Edited, revert the last one and process current one
                if (transaction.TransactionStatusID == byte.Parse(((int)(TransactionStatus.Edit)).ToString()))
                {
                    isSuccess = RevertPurchaseOrderTransaction(transaction.HeadIID);
                    WriteLog("Inventory reverted?: " + isSuccess.ToString());
                }

                Schedulers.Process(transaction);
                UpdateTransactionHead(transaction, Eduegate.Framework.Enums.TransactionStatus.Complete,Services.Contracts.Enums.DocumentStatuses.Completed);
            }
            catch (Exception ex)
            {
                UpdateTransactionHead(transaction, Eduegate.Framework.Enums.TransactionStatus.Failed,Services.Contracts.Enums.DocumentStatuses.Draft);
                WriteLog(ex.Message.ToString(), ex);
                WriteLog("Exception occured in AssetPurchaseOrder-Process.:" + ex.Message, ex);
                TransactionProcessingFailed(transaction, ex.Message);
            }
        }

        public bool ProcessCancellation(ViewModels.TransactionHeadViewModel transaction)
        {
            return new Eduegate.TransactionEgine.ClientFactory.ClientFactory().CancelTransaction(transaction.HeadIID);
        }

        private bool RevertPurchaseOrderTransaction(long transactionHeadID)
        {
            return true;
        }
    }
}

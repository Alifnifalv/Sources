using Eduegate.TransactionEngineCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Enums;
using Eduegate.TransactionEngineCore.Interfaces;
using Eduegate.TransactionEngineCore.ViewModels;

namespace Eduegate.TransactionEgineCore
{
    public class PurchaseQuotation : TransactionBase, ITransactions
    {
        public PurchaseQuotation(Action<string> logError)
        {
            _logError = logError;
        }

        public DocumentReferenceTypes ReferenceTypes => Framework.Enums.DocumentReferenceTypes.PurchaseQuote;

        public void Process(TransactionHeadViewModel transaction)
        {
            WriteLog("Purchase order-Process started for TransactionID:" + transaction.HeadIID.ToString());

            try
            {
                WriteLog("Processing purchase orders :" + transaction.HeadIID.ToString());

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
                TransactionProcessingFailed(transaction, ex.Message);
            }
        }
        public void ProcessNew(TransactionEngineCore.ViewModels.TransactionHeadViewModel transaction)
        {
            WriteLog("PurchaseOrder-Process started for TransactionID:" + transaction.HeadIID.ToString());

            bool isSuccess = false;

            try
            {
                // If Transaction is Edited, revert the last one and process current one
                if (transaction.TransactionStatusID == byte.Parse(((int)(TransactionStatus.Edit)).ToString()))
                {
                    isSuccess = RevertrTransaction(transaction.HeadIID);
                    WriteLog("Inventory reverted?: " + isSuccess.ToString());
                }

                Schedulers.Process(transaction);
                UpdateTransactionHead(transaction, Eduegate.Framework.Enums.TransactionStatus.Complete, Services.Contracts.Enums.DocumentStatuses.Completed);
            }
            catch (Exception ex)
            {
                UpdateTransactionHead(transaction, Eduegate.Framework.Enums.TransactionStatus.Failed, Services.Contracts.Enums.DocumentStatuses.Draft);
                WriteLog(ex.Message.ToString(), ex);
                WriteLog("Exception occured in PurchaseOrder-Process.:" + ex.Message, ex);
            }
        }

        public bool ProcessCancellation(TransactionEngineCore.ViewModels.TransactionHeadViewModel transaction)
        {
            return new Eduegate.TransactionEgine.ClientFactory.ClientFactory().CancelTransaction(transaction.HeadIID);
        }

        private bool RevertrTransaction(long transactionHeadID)
        {
            return true;
        }
    }
}

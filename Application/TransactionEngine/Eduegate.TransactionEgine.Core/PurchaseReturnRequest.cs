using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Eduegate.Services.Contracts.Enums;
using Eduegate.TransactionEngineCore.Interfaces;

namespace Eduegate.TransactionEngineCore
{
    public class PurchaseReturnRequest: TransactionBase, ITransactions
    {
        public PurchaseReturnRequest(Action<string> logError)
        {
            _logError = logError;
        }

        public Framework.Enums.DocumentReferenceTypes ReferenceTypes
        {
            get { return Framework.Enums.DocumentReferenceTypes.PurchaseReturnRequest; }
        }

        public void Process(ViewModels.TransactionHeadViewModel transaction)
        {
            try
            {
                WriteLog("Processing Sales order :" + transaction.HeadIID.ToString());

                switch ((Eduegate.Framework.Enums.TransactionStatus)transaction.TransactionStatusID)
                {
                    case Eduegate.Framework.Enums.TransactionStatus.New:
                        WriteLog("Processing new transaction.");
                        ProcessNew(transaction);
                        break;
                    case Eduegate.Framework.Enums.TransactionStatus.IntitiateReprocess:
                        switch ((DocumentStatuses)transaction.DocumentStatusID)
                        {
                            case DocumentStatuses.Cancelled:
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
                WriteLog("Exception occured in SalesOrder-Process.:" + ex.Message, ex);
                TransactionProcessingFailed(transaction, ex.Message);
            }
        }

        public bool ProcessNew(ViewModels.TransactionHeadViewModel transaction)
        {
            bool isSuccess = true;
            using (var scope = new TransactionScope())
            {
                Schedulers.Process(transaction);
                scope.Complete();
            }

            UpdateTransactionHead(transaction, Eduegate.Framework.Enums.TransactionStatus.Complete, Services.Contracts.Enums.DocumentStatuses.Completed);
            return isSuccess;
        }

        public bool ProcessCancellation(ViewModels.TransactionHeadViewModel transaction)
        {
            using (var scope = new TransactionScope())
            {
                var result = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().CancelTransaction(transaction.HeadIID);
                scope.Complete();
                return result;
            }
        }

        private bool RevertSalesOrderTransaction(long transactionHeadID)
        {
            return true;
        }
    }
}

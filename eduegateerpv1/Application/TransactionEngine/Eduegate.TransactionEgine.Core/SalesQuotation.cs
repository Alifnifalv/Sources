using System;
using System.Linq;
using System.Transactions;
using Eduegate.Framework.Enums;
using Eduegate.Services.Contracts.Enums;
using Eduegate.TransactionEngineCore.Interfaces;

namespace Eduegate.TransactionEngineCore
{
    public class SalesQuotation : TransactionBase, ITransactions
    {
        public SalesQuotation(Action<string> logError)
        {
            _logError = logError;
        }

        public Framework.Enums.DocumentReferenceTypes ReferenceTypes
        {
            get { return Framework.Enums.DocumentReferenceTypes.PurchaseOrder; }
        }

        public void Process(ViewModels.TransactionHeadViewModel transaction)
        {        
            try
            {
                WriteLog("Processing Sales Quotation :" + transaction.HeadIID.ToString());

                switch((Eduegate.Framework.Enums.TransactionStatus)transaction.TransactionStatusID)
                {
                    case Eduegate.Framework.Enums.TransactionStatus.New:
                        WriteLog("Processing new transaction.");
                        ProcessNew(transaction);
                        break;
                    case  Eduegate.Framework.Enums.TransactionStatus.IntitiateReprocess:
                        switch((DocumentStatuses)transaction.DocumentStatusID)
                        {
                            case DocumentStatuses.Cancelled :
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
                UpdateTransactionHead(transaction, Eduegate.Framework.Enums.TransactionStatus.Failed,Services.Contracts.Enums.DocumentStatuses.Draft);
                TransactionProcessingFailed(transaction, ex.Message);
                WriteLog("Exception occured in SalesQuotation-Process.:" + ex.Message, ex);
            }
        }

        public bool ProcessNew(ViewModels.TransactionHeadViewModel transaction)
        {
            bool isSuccess = true;
            // check if order is digital or physical
            var transactionDetail = transaction.TransactionDetails;
            var productDetail = new Eduegate.TransactionEgine.ClientFactory.ClientFactory(null).GetProduct(Convert.ToInt64(transactionDetail.FirstOrDefault().ProductID));

            if (productDetail != null)
            {
                using (var scope = new TransactionScope())
                {
                    switch ((Framework.Enums.ProductTypes)(productDetail.QuickCreate.ProductTypeID))
                    {
                        case ProductTypes.Digital:
                            Schedulers.CreateSalesInvoice(transaction);
                            break;
                        default:
                            // Create job
                            // If Transaction is Edited, revert the last one and process current one                        
                            Schedulers.Process(transaction);
                            break;
                    }
                    scope.Complete();
                }
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

        private bool RevertSalesQuotationTransaction(long transactionHeadID)
        {
            return true;
        }
    }
}

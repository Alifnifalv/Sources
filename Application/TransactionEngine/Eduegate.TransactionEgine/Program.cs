using System;
using System.Configuration;
using System.ServiceProcess;
using System.Threading.Tasks;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.TransactionEngineCore;

namespace Eduegate.TransactionEgineConsole
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var enableAccountProcessing = new Domain.Setting.SettingBL(null).GetSettingValue<bool>("EnableAccountProcessing", false);

            var transaction = new Transaction(WriteLog);
            transaction.StartProcess(DocumentReferenceTypes.BranchTransfer, TransactionStatus.New);
            transaction.StartProcess(DocumentReferenceTypes.PurchaseInvoice, TransactionStatus.New);
            transaction.StartProcess(DocumentReferenceTypes.PurchaseOrder, TransactionStatus.New);
            transaction.StartProcess(DocumentReferenceTypes.SalesOrder, TransactionStatus.New);
            transaction.StartProcess(DocumentReferenceTypes.SalesInvoice, TransactionStatus.New);
            transaction.StartProcess(DocumentReferenceTypes.SalesReturn, TransactionStatus.New);
            transaction.StartProcess(DocumentReferenceTypes.SalesReturnRequest, TransactionStatus.New);
            transaction.StartProcess(DocumentReferenceTypes.PurchaseReturn, TransactionStatus.New);
            transaction.StartProcess(DocumentReferenceTypes.PurchaseReturnRequest, TransactionStatus.New);

            transaction.StartProcess(DocumentReferenceTypes.PurchaseInvoice, TransactionStatus.Edit);
            transaction.StartProcess(DocumentReferenceTypes.SalesOrder, TransactionStatus.Edit);
            transaction.StartProcess(DocumentReferenceTypes.SalesInvoice, TransactionStatus.Edit);
            transaction.StartProcess(DocumentReferenceTypes.SalesReturn, TransactionStatus.Edit);

            transaction.StartProcess(DocumentReferenceTypes.SalesOrder, TransactionStatus.IntitiateReprocess);
            transaction.StartProcess(DocumentReferenceTypes.SalesInvoice, TransactionStatus.IntitiateReprocess);

            transaction.StartProcess(DocumentReferenceTypes.OrderChangeRequest, TransactionStatus.New);

            if (enableAccountProcessing)
            {
                ProcessAssetTransactions();
                ProcessAcoountTransactions();//RV, PV, Debit Note, Credit Note
                ProcessMissionJobs();//Mission job Driver assignment and return
            }

            Console.WriteLine("Processing completed, press any key to continue!!!!");
            Console.ReadKey();
        }

        private static void ProcessAssetTransactions()
        {
            var assetTransaction = new FixedAssetTransaction(WriteLog);
            assetTransaction.StartProcess(DocumentReferenceTypes.AssetEntry, TransactionStatus.New);
            assetTransaction.StartProcess(DocumentReferenceTypes.AssetRemoval, TransactionStatus.New);
            assetTransaction.StartProcess(DocumentReferenceTypes.AssetDepreciation, TransactionStatus.New);
        }

        private static void ProcessAcoountTransactions()
        {
            var accountTransaction = new AccountTransaction(WriteLog);
            accountTransaction.StartProcess(DocumentReferenceTypes.SalesInvoice, TransactionStatus.New);
            accountTransaction.StartProcess(DocumentReferenceTypes.AccountSalesInvoice, TransactionStatus.New);
            accountTransaction.StartProcess(DocumentReferenceTypes.Receipts, TransactionStatus.New);
            //accountTransaction.StartProcess(DocumentReferenceTypes.ReceiptVoucherMission, TransactionStatus.New);
            //accountTransaction.StartProcess(DocumentReferenceTypes.ReceiptVoucherInvoice, TransactionStatus.New);
            //accountTransaction.StartProcess(DocumentReferenceTypes.ReceiptVoucherRegularReceipt, TransactionStatus.New);
            accountTransaction.StartProcess(DocumentReferenceTypes.Payments, TransactionStatus.New);
            //accountTransaction.StartProcess(DocumentReferenceTypes.PaymentVoucherInvoice, TransactionStatus.New);
            //accountTransaction.StartProcess(DocumentReferenceTypes.PaymentVoucherRegularReceipt, TransactionStatus.New);
            accountTransaction.StartProcess(DocumentReferenceTypes.CreditNoteRegular, TransactionStatus.New);
            accountTransaction.StartProcess(DocumentReferenceTypes.DebitNoteProduct, TransactionStatus.New);
            accountTransaction.StartProcess(DocumentReferenceTypes.DebitNoteRegular, TransactionStatus.New);
            accountTransaction.StartProcess(DocumentReferenceTypes.AccountPurchaseInvoice, TransactionStatus.New);
            accountTransaction.StartProcess(DocumentReferenceTypes.Journal, TransactionStatus.New);
        }

        private static void ProcessMissionJobs()
        {
            var jobTransaction = new MIssionJobTransaction(WriteLog);
            jobTransaction.StartProcess(DocumentReferenceTypes.DistributionJobs, TransactionStatus.New);//Driver Assignment Jobs
            jobTransaction.StartProcess(DocumentReferenceTypes.ServiceJobs, TransactionStatus.New); //Driver Assignment Jobs-Return
        }

        private static void WriteLog(string message)
        {
            try
            {
                //new LoggingService.LoggingServiceClient().WriteLogAsync(message);                
            }
            catch
            {

            }

            Console.WriteLine(message);
        }
    }
}

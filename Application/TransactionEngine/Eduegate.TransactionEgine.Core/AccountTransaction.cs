using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.TransactionEgine.Accounting;
using Eduegate.TransactionEngineCore.ViewModels;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Framework.Enums;
using Eduegate.Domain;
using Eduegate.Services.Contracts.Accounts.Accounting;

namespace Eduegate.TransactionEngineCore
{
    public class AccountTransaction : TransactionBase
    {
        public AccountTransaction(Action<string> logError)
        {
            _logError = logError;
        }

        private List<AccountTransactionHeadDTO> GetAccountTransactions(DocumentReferenceTypes referenceTypes, TransactionStatus transactionStatus)
        {
            var transactions = new List<AccountTransactionHeadDTO>();
            transactions = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().GetAccountTransactionHeads(referenceTypes, transactionStatus);

            if (transactions == null)
            {
                return null;
            }

            return transactions;
        }

        private AccountTransactionHeadDTO GetTransactionByID(long headID)
        {
            var transaction = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().GetAccountTransactionHeadById(headID);

            if (transaction == null)
            {
                return null;
            }

            return transaction;
        }

        public void StartProcess(DocumentReferenceTypes referenceTypes, TransactionStatus status, long? transactionID = null)
        {
            if(!IsTransactionEnabled(referenceTypes))
            {
                return;
            }

            var lastTransactionCount = new Domain.Setting.SettingBL().GetSettingValue<int>("MaxFetchCount");

            while(true)
            {
                if (lastTransactionCount == new Domain.Setting.SettingBL().GetSettingValue<int>("MaxFetchCount"))
                {
                    lastTransactionCount = Process(referenceTypes, status, transactionID);
                }
                else
                {
                    break;
                }
            }
           
        }

        public int Process(DocumentReferenceTypes referenceTypes, TransactionStatus status, long? transactionID = null)
        {
            WriteLog("Geting Accounting Transactions - " + referenceTypes.ToString() + " " + status.ToString());

            var transactions = new List<AccountTransactionHeadDTO>();

            if (transactionID.HasValue)
            {
                transactions.Add(GetTransactionByID(transactionID.Value));
            }
            else
            {
                transactions = GetAccountTransactions(referenceTypes, status);
            }

            WriteLog(" Accounting Transaction found - " + transactions.Count());

            try
            {
                //for each start
                foreach (var accountTransaction in transactions)
                {
                    try
                    {
                        switch (accountTransaction.DocumentReferenceTypeID)
                        {
                            case Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.All:
                                break;
                            case Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.AccountPurchaseInvoice:
                                new PurchaseVoucherAccounting(_logError).Process(accountTransaction);
                                break;
                            case Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.AccountSalesInvoice:
                                new GeneralInvoiceAccounting(_logError).Process(accountTransaction);
                                break;
                            case Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.ReceiptVoucherMission:
                                new RVMissionAccounting(_logError).Process(accountTransaction);
                                break;
                            case Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.ReceiptVoucherInvoice:
                            case Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.Receipts:
                                new Receipts(_logError).Process(accountTransaction);
                                break;
                            case Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.ReceiptVoucherRegularReceipt:
                                new RVRegularReceiptsAccounting(_logError).Process(accountTransaction);
                                break;
                            case Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.PaymentVoucherInvoice:
                            case Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.Payments:
                                new Payments(_logError).Process(accountTransaction);
                                break;
                            case Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.PaymentVoucherRegularReceipt:
                                new PVRegularReceiptsAccounting(_logError).Process(accountTransaction);
                                break;
                            case Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.CreditNoteRegular:
                                new CreditNoteRegularAccounting(_logError).Process(accountTransaction);
                                break;
                            case Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.DebitNoteProduct:
                                new DebitNoteProductAccounting(_logError).Process(accountTransaction);
                                break;
                            case Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.DebitNoteRegular:
                                new DebitNoteRegularAccounting(_logError).Process(accountTransaction);
                                break;
                            case Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.Journal:
                                new Journals(_logError).Process(accountTransaction);
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

            return transactions.Count;
        }
    }
}

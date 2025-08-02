using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.TransactionEgine.Accounting.Interfaces;
using Eduegate.TransactionEgine.Accounting.ViewModels;
using Eduegate.Services.Contracts.Accounting;

namespace Eduegate.TransactionEgine.Accounting
{
    public class JournalEntryAccounting : AccountingBase, IJournalEntryAccounting
    {
        private Action<string> _logError;

        public JournalEntryAccounting(Action<string> logError)
        {
            _logError = logError;
        }

        public Framework.Enums.DocumentReferenceTypes ReferenceTypes
        {
            get { return Framework.Enums.DocumentReferenceTypes.ReceiptVoucherInvoice; }
        }
        private void WriteLog(string message, Exception ex = null)
        {
            if (_logError != null)
                _logError(message);
        }

        public void Process(AccountTransactionHeadDTO accountTransactionHead)
        {
            WriteLog("JournalEntryAccounting-AccountingProcess started for TransactionID:" + accountTransactionHead.AccountTransactionHeadIID.ToString());

            try
            {
                long transactionHeadIID = accountTransactionHead.AccountTransactionHeadIID;
                string Description = "JournalEntryAccounting: Transaction-" + transactionHeadIID.ToString() + " ";
                List<ViewModels.AccountTransactionViewModel> AccountTransactionViewModelList = new List<ViewModels.AccountTransactionViewModel>();

                long DebitAccount = Convert.ToInt64(accountTransactionHead.AccountID);
                long creditAccount = Convert.ToInt64(accountTransactionHead.AccountTransactionDetails.FirstOrDefault().AccountID);
                var headerExchangeRate = accountTransactionHead.ExchangeRate!=null? Convert.ToDecimal(accountTransactionHead.ExchangeRate) :1;

                var amount = Convert.ToDecimal(accountTransactionHead.AccountTransactionDetails.Sum(x => x.Amount * headerExchangeRate)); //Amount should be converted to local currency.Exchange rate should be available
                Create_Accounts_TransactionHeadViewModel(DebitAccount, amount, CONST_DEBIT, transactionHeadIID, Description);
                Create_Accounts_TransactionHeadViewModel(creditAccount, amount, CONST_CREDIT, transactionHeadIID, Description);


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
                UpdateAccountTransactionProcessStatus(accountTransactionHead.AccountTransactionHeadIID, Eduegate.Framework.Enums.TransactionStatus.Failed);
                WriteLog(ex.Message.ToString(), ex);
                WriteLog("Exception occured in JournalEntryAccounting-Process.:" + ex.Message, ex);
            }
        }

    }
}

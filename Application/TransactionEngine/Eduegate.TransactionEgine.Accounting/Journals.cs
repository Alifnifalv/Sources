using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.TransactionEgine.Accounting.Interfaces;
using Eduegate.TransactionEgine.Accounting.ViewModels;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Contracts.Accounts.Accounting;

namespace Eduegate.TransactionEgine.Accounting
{
    public class Journals : AccountingBase, IRVInvoiceAccounting
    {
        private Action<string> _logError;

        public Journals(Action<string> logError)
        {
            _logError = logError;
        }

        public Framework.Enums.DocumentReferenceTypes ReferenceTypes
        {
            get { return Framework.Enums.DocumentReferenceTypes.Journal; }
        }

        private void WriteLog(string message, Exception ex = null)
        {
            if (_logError != null)
                _logError(message);
        }

        public void Process(AccountTransactionHeadDTO accountTransactionHead)
        {
            WriteLog("Journal-AccountingProcess started for TransactionID:" + accountTransactionHead.AccountTransactionHeadIID.ToString());

            try
            {
                long transactionHeadIID = accountTransactionHead.AccountTransactionHeadIID;
                var transactionVM = new List<ViewModels.AccountTransactionViewModel>();
                var headVM = TransactionHeadViewModel.FromDTO(accountTransactionHead);

                foreach (var detail in accountTransactionHead.AccountTransactionDetails)
                {
                    MergeAccountTransactions(transactionVM, headVM, detail.AccountID, Math.Abs(detail.Amount.Value), 
                        detail.Amount.Value > 0 ? CONST_DEBIT : CONST_CREDIT, detail.Description, null, null, null, true);
                }

                //Add to DB
                bool IsDBOperationSuccess = false;
                if (transactionVM != null && transactionVM.Count > 0)
                {
                    var AccountTransactionsDTOList = AccountTransactionViewModel.ConvertViewModelToDTO(transactionVM);
                    IsDBOperationSuccess = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().AddAccountTransactions(AccountTransactionsDTOList);
                }

                UpdateAccountTransactionProcessStatus(accountTransactionHead.AccountTransactionHeadIID, Eduegate.Framework.Enums.TransactionStatus.Complete);
                WriteLog("Journal-Process completed!!");
            }
            catch (Exception ex)
            {
                UpdateAccountTransactionProcessStatus(accountTransactionHead.AccountTransactionHeadIID, Eduegate.Framework.Enums.TransactionStatus.Failed);
                WriteLog(ex.Message.ToString(), ex);
                WriteLog("Exception occured in PVInvoiceAccounting-Process.:" + ex.Message, ex);
            }
        }
    }
}

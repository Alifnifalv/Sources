using System;
using System.Collections.Generic;
using Eduegate.TransactionEgine.Accounting.Interfaces;
using Eduegate.TransactionEgine.Accounting.ViewModels;
using Eduegate.Services.Contracts.Accounting;

namespace Eduegate.TransactionEgine.Accounting
{
    public class DebitNoteRegularAccounting : AccountingBase, IRVInvoiceAccounting
    {
        private Action<string> _logError;

        public DebitNoteRegularAccounting(Action<string> logError)
        {
            _logError = logError;
        }

        public Framework.Enums.DocumentReferenceTypes ReferenceTypes
        {
            get { return Framework.Enums.DocumentReferenceTypes.DebitNoteRegular; }
        }
        private void WriteLog(string message, Exception ex = null)
        {
            if (_logError != null)
                _logError(message);
        }
        public void Process(AccountTransactionHeadDTO accountTransactionHead)
        {
            WriteLog("DebitNoteRegularAccounting-AccountingProcess started for TransactionID:" + accountTransactionHead.AccountTransactionHeadIID.ToString());
            try
            {
                long transactionHeadIID = accountTransactionHead.AccountTransactionHeadIID;
                string Description = "DebitNoteRegularAccounting: Transaction-" + transactionHeadIID.ToString() + " ";
                List<ViewModels.AccountTransactionViewModel> AccountTransactionViewModelList = new List<ViewModels.AccountTransactionViewModel>();

                foreach (AccountTransactionDetailsDTO detailDto in accountTransactionHead.AccountTransactionDetails)
                {
                    long accountID = Convert.ToInt64(detailDto.AccountID);
                    var amount = Convert.ToDecimal(detailDto.Amount * (detailDto.ExchangeRate == null ? 1 : detailDto.ExchangeRate)); //Amount should be converted to local currency.Exchange rate should be available
                    if (detailDto.Amount < 0)//Debit amount
                    {
                        Create_Accounts_TransactionHeadViewModel(accountID, amount, CONST_DEBIT, transactionHeadIID, Description);
                    }
                    else//Credit amount
                    {
                        Create_Accounts_TransactionHeadViewModel(accountID, amount, CONST_CREDIT, transactionHeadIID, Description);
                    }
                }
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
                WriteLog("Exception occured in DebitNoteRegularAccounting-Process.:" + ex.Message, ex);
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.TransactionEgine.Accounting.Interfaces;
using Eduegate.TransactionEgine.Accounting.ViewModels;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Contracts.Accounts.Accounting;

namespace Eduegate.TransactionEgine.Accounting
{
    public class RVMissionAccounting : AccountingBase, IRVMissionAccounting
    {
        private Action<string> _logError;

        public RVMissionAccounting(Action<string> logError)
        {
            _logError = logError;
        }

        public Framework.Enums.DocumentReferenceTypes ReferenceTypes
        {
            get { return Framework.Enums.DocumentReferenceTypes.ReceiptVoucherMission; }
        }
        private void WriteLog(string message, Exception ex = null)
        {
            if (_logError != null)
                _logError(message);
        }
        public void Process(AccountTransactionHeadDTO accountTransactionHead)
        {
            WriteLog("RVMissionAccounting-AccountingProcess started for TransactionID:" + accountTransactionHead.AccountTransactionHeadIID.ToString());

            try
            {
                long transactionHeadIID = accountTransactionHead.AccountTransactionHeadIID;
                string Description = "RVMissionAccounting: Transaction-" + transactionHeadIID.ToString() + " ";
                var transactionVM = new List<ViewModels.AccountTransactionViewModel>();

                long DebitAccount = Convert.ToInt64( accountTransactionHead.AccountID);
                long creditAccount = Convert.ToInt64(accountTransactionHead.AccountTransactionDetails.FirstOrDefault().AccountID);
                var headerExchangeRate = accountTransactionHead.ExchangeRate.HasValue ? accountTransactionHead.ExchangeRate.Value : 1;

                var amount = accountTransactionHead.AccountTransactionDetails.Sum(x => x.Amount * headerExchangeRate); //Amount should be converted to local currency.Exchange rate should be available
                Create_Accounts_TransactionHeadViewModel(DebitAccount, amount, CONST_DEBIT, transactionHeadIID, Description);
                Create_Accounts_TransactionHeadViewModel(creditAccount, amount, CONST_CREDIT, transactionHeadIID, Description);

                
                //Add to DB
                bool IsDBOperationSuccess = false;
                if (transactionVM != null && transactionVM.Count > 0)
                {
                    var AccountTransactionsDTOList = AccountTransactionViewModel.ConvertViewModelToDTO(transactionVM);
                    IsDBOperationSuccess = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().AddAccountTransactions(AccountTransactionsDTOList);
                }

                
            }
            catch (Exception ex)
            {
                UpdateAccountTransactionProcessStatus(accountTransactionHead.AccountTransactionHeadIID, Eduegate.Framework.Enums.TransactionStatus.Failed);
                WriteLog(ex.Message.ToString(), ex);
                WriteLog("Exception occured in RVMissionAccounting-Process.:" + ex.Message, ex);
            }
        }


    }
}

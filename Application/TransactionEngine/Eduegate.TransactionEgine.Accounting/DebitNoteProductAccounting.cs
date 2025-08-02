using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.TransactionEgine.Accounting.Interfaces;
using Eduegate.TransactionEgine.Accounting.ViewModels;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Contracts.Accounts.Accounting;

namespace Eduegate.TransactionEgine.Accounting
{
    public class DebitNoteProductAccounting : AccountingBase, IRVInvoiceAccounting
    {
        private Action<string> _logError;

        public DebitNoteProductAccounting(Action<string> logError)
        {
            _logError = logError;
        }

        public Framework.Enums.DocumentReferenceTypes ReferenceTypes
        {
            get { return Framework.Enums.DocumentReferenceTypes.DebitNoteProduct; }
        }
        private void WriteLog(string message, Exception ex = null)
        {
            if (_logError != null)
                _logError(message);
        }
        public void Process(AccountTransactionHeadDTO accountTransactionHead)
        {
            WriteLog("DebitNoteProductAccounting-AccountingProcess started for TransactionID:" + accountTransactionHead.AccountTransactionHeadIID.ToString());
            try
            {
                long transactionHeadIID = accountTransactionHead.AccountTransactionHeadIID;
                string Description = "DebitNoteProductAccounting: Transaction-" + transactionHeadIID.ToString() + " ";
                List<ViewModels.AccountTransactionViewModel> AccountTransactionViewModelList = new List<ViewModels.AccountTransactionViewModel>();

                long supplierGLAccount = Convert.ToInt64(accountTransactionHead.AccountID);
                long creditAccount = Convert.ToInt64(accountTransactionHead.AccountTransactionDetails.FirstOrDefault().AccountID);
                var GL_PURCHASE_RETURN_AccId = GetGLAccountId(GetSettingByCode(GL_PURCHASERETURN_CODE));
                long inventoryAccount = Convert.ToInt64(accountTransactionHead.AccountTransactionDetails.FirstOrDefault().AccountID);

                decimal amount = Convert.ToDecimal(accountTransactionHead.AccountTransactionDetails.Sum(x => x.Amount * (x.ExchangeRate == null ? 1 : x.ExchangeRate))); //Amount should be converted to local currency.Exchange rate should be available

                Create_Accounts_TransactionHeadViewModel(supplierGLAccount, amount, CONST_DEBIT, transactionHeadIID, Description);
                Create_Accounts_TransactionHeadViewModel(GL_PURCHASE_RETURN_AccId, amount, CONST_CREDIT, transactionHeadIID, Description);
                Create_Accounts_TransactionHeadViewModel(GL_PURCHASE_RETURN_AccId, amount, CONST_DEBIT, transactionHeadIID, Description);
                Create_Accounts_TransactionHeadViewModel(inventoryAccount, amount, CONST_CREDIT, transactionHeadIID, Description);

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
                WriteLog("Exception occured in DebitNoteProductAccounting-Process.:" + ex.Message, ex);
            }
        }

    }
}

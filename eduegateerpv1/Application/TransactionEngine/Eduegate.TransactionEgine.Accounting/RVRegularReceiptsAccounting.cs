using System;
using System.Collections.Generic;
using Eduegate.TransactionEgine.Accounting.Interfaces;
using Eduegate.TransactionEgine.Accounting.ViewModels;
using Eduegate.Services.Contracts.Accounting;

namespace Eduegate.TransactionEgine.Accounting
{
    public class RVRegularReceiptsAccounting : AccountingBase, IRVRegularReceiptAccounting
    {
        private Action<string> _logError;

        public RVRegularReceiptsAccounting(Action<string> logError)
        {
            _logError = logError;
        }

        public Framework.Enums.DocumentReferenceTypes ReferenceTypes
        {
            get { return Framework.Enums.DocumentReferenceTypes.ReceiptVoucherRegularReceipt; }
        }

        private void WriteLog(string message, Exception ex = null)
        {
            if (_logError != null)
                _logError(message);
        }

        public void Process(AccountTransactionHeadDTO accountTransactionHead)
        {
            WriteLog("RVRegularReceiptsAccounting-AccountingProcess started for TransactionID:" + accountTransactionHead.AccountTransactionHeadIID.ToString());

            try
            {
                long transactionHeadIID = accountTransactionHead.AccountTransactionHeadIID;
                string Description = "RVRegularReceiptsAccounting: Transaction-" + transactionHeadIID.ToString() + " ";
                var accountTransactionViewModelList = new List<ViewModels.AccountTransactionViewModel>();

                foreach (AccountTransactionDetailsDTO detailDto in accountTransactionHead.AccountTransactionDetails)
                {
                    long accountID = Convert.ToInt64(detailDto.AccountID);
                    var amount = detailDto.Amount * (detailDto.ExchangeRate.HasValue? detailDto.ExchangeRate.Value : 1); //Amount should be converted to local currency.Exchange rate should be available

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
                if (accountTransactionViewModelList != null && accountTransactionViewModelList.Count > 0)
                {
                    var accountTransactionsDTOList = AccountTransactionViewModel.ConvertViewModelToDTO(accountTransactionViewModelList);
                    IsDBOperationSuccess = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().AddAccountTransactions(accountTransactionsDTOList);
                }

            }
            catch (Exception ex)
            {
                UpdateAccountTransactionProcessStatus(accountTransactionHead.AccountTransactionHeadIID, Eduegate.Framework.Enums.TransactionStatus.Failed);
                WriteLog(ex.Message.ToString(), ex);
                WriteLog("Exception occured in RVRegularReceiptsAccounting-Process.:" + ex.Message, ex);
            }
        }
    }
}

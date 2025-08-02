using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.TransactionEgine.Accounting.Interfaces;
using Eduegate.TransactionEgine.Accounting.ViewModels;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Contracts.Accounts.Accounting;

namespace Eduegate.TransactionEgine.Accounting
{
    public class Payments : AccountingBase, IRVInvoiceAccounting
    {
        private Action<string> _logError;

        public Payments(Action<string> logError)
        {
            _logError = logError;
        }

        public Framework.Enums.DocumentReferenceTypes ReferenceTypes
        {
            get { return Framework.Enums.DocumentReferenceTypes.PaymentVoucherInvoice; }
        }
        private void WriteLog(string message, Exception ex = null)
        {
            if (_logError != null)
                _logError(message);
        }
        public void Process(AccountTransactionHeadDTO accountTransactionHead)
        {
            WriteLog("PVInvoiceAccounting-AccountingProcess started for TransactionID:" + accountTransactionHead.AccountTransactionHeadIID.ToString());

            try
            {
                //clear the previous data if it's an edit after processing
                ClearPostedData(accountTransactionHead.AccountTransactionHeadIID);

                long transactionHeadIID = accountTransactionHead.AccountTransactionHeadIID;
                string Description = "PVInvoiceAccounting: Transaction-" + transactionHeadIID.ToString() + " ";
                var transactionVM = new List<ViewModels.AccountTransactionViewModel>();

                long creditAccount = Convert.ToInt64(accountTransactionHead.AccountID);
                long DebitAccount = Convert.ToInt64(accountTransactionHead.AccountTransactionDetails.FirstOrDefault().AccountID);
                var headerExchangeRate = accountTransactionHead.ExchangeRate != null ? accountTransactionHead.ExchangeRate : 1;

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

                if (IsDBOperationSuccess)
                {
                    var vm = AccountTransactionHeadViewModel.ToVM(accountTransactionHead);
                    IsDBOperationSuccess = AddReceivable(vm, accountTransactionHead.AccountID, accountTransactionHead.Amount, CONST_CREDIT);

                    ////Add Receivables
                    //if (transaction.AccountID.HasValue)
                    //{                        
                    //    IsDBOperationSuccess = AddReceivable(vm, vm.AccountID, vm.Amount);
                    //}

                    //foreach (var detail in transaction.AccountTransactionDetails)
                    //{
                    //    var detailVM = AccountTransactionDetailViewModel.ToVM(detail);
                    //    IsDBOperationSuccess = AddReceivable(vm, detailVM.AccountID, detailVM.Amount);
                    //}
                }

                UpdateAccountTransactionProcessStatus(accountTransactionHead.AccountTransactionHeadIID, Eduegate.Framework.Enums.TransactionStatus.Complete);
                WriteLog("Payment-Process completed!!");
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

using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.TransactionEgine.Accounting.Interfaces;
using Eduegate.TransactionEgine.Accounting.ViewModels;
using Eduegate.Services.Contracts.Accounting;

namespace Eduegate.TransactionEgine.Accounting
{
    public class Receipts : AccountingBase, IRVInvoiceAccounting
    {
        private Action<string> _logError;

        public Receipts(Action<string> logError)
        {
            _logError = logError;
        }

        public Framework.Enums.DocumentReferenceTypes ReferenceTypes
        {
            get { return Framework.Enums.DocumentReferenceTypes.Receipts; }
        }
        private void WriteLog(string message, Exception ex = null)
        {
            if (_logError != null)
                _logError(message);
        }

        public void Process(AccountTransactionHeadDTO accountTransactionHead)
        {
            WriteLog("Receipts-AccountingProcess started for TransactionID:" + accountTransactionHead.AccountTransactionHeadIID.ToString());

            try
            {
                //clear the previous data if it's an edit after processing
                ClearPostedData(accountTransactionHead.AccountTransactionHeadIID);

                long transactionHeadIID = accountTransactionHead.AccountTransactionHeadIID;
                string Description = "Receipts: Transaction-" + transactionHeadIID.ToString() + " ";
                var transactionVM = new List<ViewModels.AccountTransactionViewModel>();
                var headVM = TransactionHeadViewModel.FromDTO(accountTransactionHead);

                long creditAccount = Convert.ToInt64(accountTransactionHead.AccountID);
                long? debitAccount = GetGLAccountId(GetSettingByCode(GL_SALES_CODE));
                var vatAccount = GetGLAccountId(GetSettingByCode(GL_VATACC_CODE));

                if (accountTransactionHead.AccountTransactionDetails.Count > 0)
                {
                    debitAccount = Convert.ToInt64(accountTransactionHead.AccountTransactionDetails.FirstOrDefault().AccountID);
                }

                var headerExchangeRate = accountTransactionHead.ExchangeRate!=null?Convert.ToDecimal(accountTransactionHead.ExchangeRate) :1;

                var amount = accountTransactionHead.AmountPaid.HasValue && accountTransactionHead.AmountPaid.Value > 0 ?
                    accountTransactionHead.AmountPaid : Convert.ToDecimal(accountTransactionHead.AccountTransactionDetails.Sum(x => x.Amount * headerExchangeRate)); //Amount should be converted to local currency.Exchange rate should be available
                MergeAccountTransactions(transactionVM, headVM, creditAccount, amount, CONST_CREDIT, headVM.Description);
                

                //check any receipts is referred in the a/c, if so tax should be taken from there.
                var receivableIds = new List<long>();
                foreach (var detail in accountTransactionHead.AccountTransactionDetails)
                {
                    if (detail.ReferenceReceiptID.HasValue)
                    {
                        receivableIds.Add(detail.ReferenceReceiptID.Value);
                    }
                }

                if(receivableIds.Count == 0)
                {
                    debitAccount = GetGLAccountId(GetSettingByCode(GL_SALES_WITH_VATACC_CODE));
                }

                //headVM.Description + ",CUS#" + accountTransactionHead.Account.Value
                var description = headVM.Description;

                if (accountTransactionHead.Account != null)
                {
                    description = description + "," + accountTransactionHead.AccountCode + ":" + accountTransactionHead.Account.Value;
                }

                MergeAccountTransactions(transactionVM, headVM, debitAccount, amount, CONST_DEBIT, description);

                bool IsDBOperationSuccess = false;
                if (transactionVM != null && transactionVM.Count > 0)
                {
                    var transactionEntries = AccountTransactionViewModel.ConvertViewModelToDTO(transactionVM);
                    IsDBOperationSuccess = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().AddAccountTransactions(transactionEntries);
                }

                if (IsDBOperationSuccess)
                {
                    var clientFactory = new Eduegate.TransactionEgine.ClientFactory.ClientFactory();

                    if (receivableIds.Count > 0)
                    {

                        var receivables = clientFactory.GetReceivables(receivableIds);

                        foreach (var receivable in receivables)
                        {
                            var paidReceivable = accountTransactionHead.AccountTransactionDetails.Where(a => a.ReferenceReceiptID == receivable.ReceivableIID).FirstOrDefault();
                            receivable.PaidAmount = receivable.PaidAmount + Convert.ToDecimal(paidReceivable.Amount);
                        }

                        clientFactory.SaveReceivables(receivables);
                    }
                }

                ////Add to DB
                //bool IsDBOperationSuccess = false;
                //if (transactionVM != null && transactionVM.Count > 0)
                //{
                //    var AccountTransactionsDTOList = AccountTransactionViewModel.ConvertViewModelToDTO(transactionVM);
                //    IsDBOperationSuccess = clientFactory.AddAccountTransactions(AccountTransactionsDTOList);
                //}

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

                WriteLog("Receipts-Process completed!!");
                UpdateAccountTransactionProcessStatus(accountTransactionHead.AccountTransactionHeadIID, Eduegate.Framework.Enums.TransactionStatus.Complete);
            }
            catch (Exception ex)
            {
                UpdateAccountTransactionProcessStatus(accountTransactionHead.AccountTransactionHeadIID, Eduegate.Framework.Enums.TransactionStatus.Failed);
                WriteLog(ex.Message.ToString(), ex);
                WriteLog("Exception occured in Receipts-Process.:" + ex.Message, ex);
            }
        }

    }
}

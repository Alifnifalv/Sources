using Eduegate.Services.Contracts.Accounting;
using Eduegate.TransactionEgine.Accounting.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.TransactionEgine.Accounting
{
    public class SalesVoucherAccounting : AccountingBase
    {
        private Action<string> _logError;

        public SalesVoucherAccounting(Action<string> logError)
        {
            _logError = logError;
        }

        public Framework.Enums.DocumentReferenceTypes ReferenceTypes
        {
            get { return Framework.Enums.DocumentReferenceTypes.SalesInvoice; }
        }

        private void WriteLog(string message, Exception ex = null)
        {
            if (_logError != null)
                _logError(message);
        }

        public void Process(AccountTransactionHeadDTO transaction)
        {
            WriteLog("SalesVoucher-AccountingProcess started for TransactionID:" + transaction.AccountTransactionHeadIID.ToString());

            try
            {
                //clear the previous data if it's an edit after processing
                ClearPostedData(transaction.AccountTransactionHeadIID);

                //for each start
                //for each end
                long? salesAccount = GetGLAccountId(GetSettingByCode(GL_SALES_CODE));
                var vatAccount = GetGLAccountId(GetSettingByCode(GL_VATACC_CODE));
                var salesDiscount = GetGLAccountId(GetSettingByCode(GL_SALES_DISCOUNT_CODE));
                decimal taxAmount = 0;
                string description;

                var headVM = TransactionHeadViewModel.FromDTO(transaction);
                var transactionVM = new List<ViewModels.AccountTransactionViewModel>();
                MergeAccountTransactions(transactionVM, headVM, transaction.AccountID, transaction.Amount, CONST_DEBIT,  transaction.Remarks, 
                        transaction.TaxAmount, null, transaction.DiscountAmount);
                //SaveAccountTransactions(transactionVM, transaction.AccountID, transaction.Amount, CONST_DEBIT, transaction.AccountTransactionHeadIID, transaction.Remarks);

                //Update the tax Amount
                if (transaction.TaxDetails != null && transaction.TaxDetails.Count > 0)
                {
                    description = transaction.Remarks;

                    if (transaction.Account != null)
                    {
                        description = description + "," + transaction.AccountCode + ":" + transaction.Account.Value;
                    }

                    taxAmount = Convert.ToDecimal(transaction.TaxDetails.Sum(a => a.ExclusiveTaxAmount));
                    MergeAccountTransactions(transactionVM, headVM, vatAccount, taxAmount, CONST_CREDIT, description);
                }

                if(transaction.DiscountAmount.HasValue && transaction.DiscountAmount.Value != 0)
                {
                    MergeAccountTransactions(transactionVM, headVM, salesDiscount, 
                        transaction.DiscountAmount.Value, CONST_CREDIT, transaction.Remarks);
                }

                var detail = transaction.AccountTransactionDetails.FirstOrDefault();               
                var detailAccount = salesAccount;

                if(detail.AccountID.HasValue)
                {
                    detailAccount = detail.AccountID;
                }

                var detailVM = AccountTransactionDetailViewModel.ToVM(detail);
                description = detailVM.Remarks;

                //if(transaction.Account != null)
                //{
                //    description = description + "," + transaction.AccountCode + ":" + transaction.Account.Value;
                //}

                MergeAccountTransactions(transactionVM, headVM, detailAccount, detailVM.Amount.Value - taxAmount, CONST_CREDIT, description);
                     
                bool IsDBOperationSuccess = false;
                if (transactionVM != null && transactionVM.Count > 0)
                {
                    var transactionEntries = AccountTransactionViewModel.ConvertViewModelToDTO(transactionVM);
                    IsDBOperationSuccess = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().AddAccountTransactions(transactionEntries);
                }

                if (IsDBOperationSuccess)
                {
                    var vm = AccountTransactionHeadViewModel.ToVM(transaction);
                    IsDBOperationSuccess = AddReceivable(vm, transaction.AccountID, transaction.Amount, CONST_DEBIT);
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

                UpdateAccountTransactionProcessStatus(transaction.AccountTransactionHeadIID, Eduegate.Framework.Enums.TransactionStatus.Complete);
                WriteLog("SalesVoucher-AccountingProcess completed for TransactionID:" + transaction.AccountTransactionHeadIID.ToString());
            }
            catch (Exception ex)
            {
                UpdateAccountTransactionProcessStatus(transaction.AccountTransactionHeadIID, Eduegate.Framework.Enums.TransactionStatus.Failed);
                WriteLog(ex.Message.ToString(), ex);
                //Eduegate.Logger.LogHelper<string>.Fatal(ex.Message.ToString(), ex);
                WriteLog(ex.Message.ToString());
            }
        }
    }
}

using Eduegate.Services.Contracts.Accounts.Accounting;
using Eduegate.TransactionEgine.Accounting.Interfaces;
using Eduegate.TransactionEgine.Accounting.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.TransactionEgine.Accounting
{
    public class GeneralInvoiceAccounting : AccountingBase, IRVInvoiceAccounting
    {
        private Action<string> _logError;

        public GeneralInvoiceAccounting(Action<string> logError)
        {
            _logError = logError;
        }

        public Framework.Enums.DocumentReferenceTypes ReferenceTypes
        {
            get { return Framework.Enums.DocumentReferenceTypes.AccountSalesInvoice; }
        }
        private void WriteLog(string message, Exception ex = null)
        {
            if (_logError != null)
                _logError(message);
        }
        public void Process(AccountTransactionHeadDTO accountTransactionHead)
        {
            WriteLog("AccountSales/GeneralInvoiceAccounting-AccountingProcess started for TransactionID:" + accountTransactionHead.AccountTransactionHeadIID.ToString());
            try
            {
                long transactionHeadIID = accountTransactionHead.AccountTransactionHeadIID;
                string Description = "AccountSales/GeneralInvoiceAccounting: Transaction-" + transactionHeadIID.ToString() + " ";
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
                if (IsDBOperationSuccess)
                {
                    var vm = AccountTransactionHeadViewModel.ToVM(accountTransactionHead);
                    IsDBOperationSuccess = AddReceivable(vm, accountTransactionHead.AccountID, accountTransactionHead.Amount, CONST_DEBIT);

                }

                UpdateAccountTransactionProcessStatus(accountTransactionHead.AccountTransactionHeadIID, Eduegate.Framework.Enums.TransactionStatus.Complete);
                WriteLog("SalesVoucher-AccountingProcess completed for TransactionID:" + accountTransactionHead.AccountTransactionHeadIID.ToString());
            }
            catch (Exception ex)
            {
                UpdateAccountTransactionProcessStatus(accountTransactionHead.AccountTransactionHeadIID, Eduegate.Framework.Enums.TransactionStatus.Failed);
                WriteLog(ex.Message.ToString(), ex);
                //Eduegate.Logger.LogHelper<string>.Fatal(ex.Message.ToString(), ex);
                WriteLog(ex.Message.ToString());
            }


        }

    }
}

using System;
using System.Collections.Generic;
using Eduegate.TransactionEgine.Accounting.Interfaces;
using Eduegate.TransactionEgine.Accounting.ViewModels;

namespace Eduegate.TransactionEgine.Accounting
{
    public class SalesOrderAccounting : AccountingBase, ITransactions
    {
        private Action<string> _logError;

        public SalesOrderAccounting(Action<string> logError)
        {
            _logError = logError;
        }

        public Framework.Enums.DocumentReferenceTypes ReferenceTypes
        {
            get { return Framework.Enums.DocumentReferenceTypes.PurchaseOrder; }
        }
        private void WriteLog(string message, Exception ex = null)
        {
            if (_logError != null)
                _logError(message);
        }
        public void Process(ViewModels.TransactionHeadViewModel transaction)
        {
            WriteLog("SalesOrder-AccountingProcess started for TransactionID:" + transaction.HeadIID.ToString());
            try
            {
                long transactionHeadIID = transaction.HeadIID;
                string Description = "Sales Order: Transaction-" + transactionHeadIID.ToString()+" ";
                var transactionVM = new List<ViewModels.AccountTransactionViewModel>();

                var GL_ORDER_IN_PROCESS_AccId = GetGLAccountId(GetSettingByCode(GL_ORDER_IN_PROCESS_CODE));

                var TransactionHeadEntitlementMapsList = GetTransactionHeadEntitlementMaps(transactionHeadIID);
                foreach (var entitlementMapDTO in TransactionHeadEntitlementMapsList)
                {
                    decimal EntitlementMapValue = 0;
                    if (entitlementMapDTO.Amount != null)
                    {
                        EntitlementMapValue = Convert.ToDecimal(entitlementMapDTO.Amount);
                    }
                    switch (entitlementMapDTO.EntitlementName)
                    {
                        case ENTITLEMENT_VISAMASTERCARD:
                            {
                                var GL_VISA_MASTERCARD_AccId = GetGLAccountId(GetSettingByCode(GL_VISA_MASTERCARD_CODE));
                                MergeAccountTransactions(transactionVM, transaction, GL_VISA_MASTERCARD_AccId, EntitlementMapValue, CONST_DEBIT, Description+ "Entitlement_VisaMastercard");
                                MergeAccountTransactions(transactionVM, transaction, GL_ORDER_IN_PROCESS_AccId, EntitlementMapValue, CONST_CREDIT, Description + "Entitlement_VisaMastercard");
                            }
                            break;
                        case ENTITLEMENT_PAYPAL:
                            {
                                var GL_PAYPAL_AccId = GetGLAccountId(GetSettingByCode(GL_PAYPAL_CODE));
                                MergeAccountTransactions(transactionVM, transaction, GL_PAYPAL_AccId, EntitlementMapValue, CONST_DEBIT, Description + "Entitlement_Paypal");
                                MergeAccountTransactions(transactionVM, transaction, GL_ORDER_IN_PROCESS_AccId, EntitlementMapValue, CONST_CREDIT, Description + "Entitlement_Paypal");
                            }
                            break;
                      
                    }
                }

                //Add to DB
                bool IsDBOperationSuccess = false;
                if (transactionVM != null && transactionVM.Count > 0)
                {
                    var AccountTransactionsDTOList = AccountTransactionViewModel.ConvertViewModelToDTO(transactionVM);
                    IsDBOperationSuccess = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().AddAccountTransactions(AccountTransactionsDTOList);
                }

                if(IsDBOperationSuccess)
                {
                    //update TransactionHeadAccountProcessingStatus flag
                }
            }
            catch (Exception ex)
            {
                UpdateAccountTransactionProcessStatus(transaction.HeadIID, Eduegate.Framework.Enums.TransactionStatus.Failed);
                WriteLog(ex.Message.ToString(), ex);
                WriteLog("Exception occured in SalesOrderAccounting-Process.:" + ex.Message, ex);
            }
        }
        //private void CreateCashPaidAccountEntries(List<KeyValueViewModel> AccountTransactionViewModelList, long CreditAccId, long DebitAccId, decimal EntitlementMapValue, long transactionHeadIID)
        //{
        //    new ServiceAccessUtils().CreateAccountTransactionViewModel(DebitAccId, EntitlementMapValue, CONST_Debit, transactionHeadIID));
        //    new ServiceAccessUtils().CreateAccountTransactionViewModel(CreditAccId, EntitlementMapValue, CONST_Credit, transactionHeadIID));
        //}

    }
}

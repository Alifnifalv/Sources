using System;
using System.Collections.Generic;
using Eduegate.TransactionEgine.Accounting.Interfaces;
using Eduegate.TransactionEgine.Accounting.ViewModels;

namespace Eduegate.TransactionEgine.Accounting
{
    public class SalesReturnAccounting : AccountingBase, ITransactions
    {
        private Action<string> _logError;

        public SalesReturnAccounting(Action<string> logError)
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
            WriteLog("SalesReturn-AccountingProcess started for TransactionID:" + transaction.HeadIID.ToString());
            try
            {
                long transactionHeadIID = transaction.HeadIID;
                string Description = "Sales Return: Transaction-" + transactionHeadIID.ToString() +" ";
                var transactionVM = new List<ViewModels.AccountTransactionViewModel>();

                //Cost from Cost Center COGS
                decimal TotalCostCenterValue = GetCostCenterPrice(transaction);

                //Common
                var GL_ORDER_IN_PROCESS_AccId = GetGLAccountId(GetSettingByCode(GL_ORDER_IN_PROCESS_CODE));
                var GL_INVENTORY_AccId = GetGLAccountId(GetSettingByCode(GL_INVENTORY_CODE));
                var GL_REFUND_IN_PROCESS_AccId = GetGLAccountId(GetSettingByCode(GL_REFUND_IN_PROCESS_CODE));
                var GL_COGS_AccId = GetGLAccountId(GetSettingByCode(GL_COGS_CODE));
                //Common

                var TransactionHeadEntitlementMapsList = GetTransactionHeadEntitlementMaps(transactionHeadIID);
                foreach (var entitlementMapDTO in TransactionHeadEntitlementMapsList)
                {
                    long? CustomerGLAccountId = null;
                    if (transaction.CustomerID != null)
                    {
                        CustomerGLAccountId = GetCustomerGLAccountId((long)transaction.CustomerID, (int)entitlementMapDTO.EntitlementID);
                        if (CustomerGLAccountId == 0)
                        {
                            UpdateAccountTransactionProcessStatus(transaction.HeadIID, Eduegate.Framework.Enums.TransactionStatus.Failed);
                            return;
                        }
                    }
                    decimal EntitlementMapValue = 0;
                    if (entitlementMapDTO.Amount != null)
                    {
                        EntitlementMapValue = Convert.ToDecimal(entitlementMapDTO.Amount);
                    }
                    switch (entitlementMapDTO.EntitlementName)
                    {
                        case ENTITLEMENT_VISAMASTERCARD:
                            {
                                MergeAccountTransactions(transactionVM, transaction, GL_INVENTORY_AccId, EntitlementMapValue, CONST_DEBIT, Description+ "Entitlement_VisaMastercard");
                                MergeAccountTransactions(transactionVM, transaction, GL_REFUND_IN_PROCESS_AccId, EntitlementMapValue, CONST_CREDIT, Description + "Entitlement_VisaMastercard");
                                MergeAccountTransactions(transactionVM, transaction, GL_COGS_AccId, TotalCostCenterValue, CONST_CREDIT, Description + "Entitlement_VisaMastercard");
                                MergeAccountTransactions(transactionVM, transaction, CustomerGLAccountId, TotalCostCenterValue, CONST_DEBIT, Description + "Entitlement_VisaMastercard");
                            }
                            break;
                        case ENTITLEMENT_PAYPAL:
                            {
                                MergeAccountTransactions(transactionVM, transaction, GL_INVENTORY_AccId, EntitlementMapValue, CONST_DEBIT, Description + "Entitlement_Paypal");
                                MergeAccountTransactions(transactionVM, transaction, GL_REFUND_IN_PROCESS_AccId, EntitlementMapValue, CONST_CREDIT, Description + "Entitlement_Paypal");
                                MergeAccountTransactions(transactionVM, transaction, GL_COGS_AccId, TotalCostCenterValue, CONST_CREDIT, Description + "Entitlement_Paypal");
                                MergeAccountTransactions(transactionVM, transaction, CustomerGLAccountId, TotalCostCenterValue, CONST_DEBIT, Description + "Entitlement_Paypal");
                            }
                            break;
                        
                        case ENTITLEMENT_VOUCHER:
                            {
                                var GL_REFUND_VOUCHER_AccId = GetGLAccountId(GetSettingByCode(GL_REFUND_VOUCHER_CODE));
                                MergeAccountTransactions(transactionVM, transaction, GL_REFUND_VOUCHER_AccId, EntitlementMapValue, CONST_CREDIT, Description + "Entitlement_Voucher");
                                MergeAccountTransactions(transactionVM, transaction, GL_REFUND_IN_PROCESS_AccId, EntitlementMapValue, CONST_DEBIT, Description + "Entitlement_Voucher");
                            }
                            break;
                        case ENTITLEMENT_VOUCHER_EXPIRES:
                            {
                                var GL_REFUND_VOUCHER_INCOME_AccId = GetGLAccountId(GetSettingByCode(GL_REFUND_VOUCHER_INCOME_CODE));
                                MergeAccountTransactions(transactionVM, transaction, GL_REFUND_VOUCHER_INCOME_AccId, EntitlementMapValue, CONST_CREDIT, Description + "Entitlement_Voucher_Expires");
                                var GL_REFUND_VOUCHER_AccId = GetGLAccountId(GetSettingByCode(GL_REFUND_VOUCHER_CODE));
                                MergeAccountTransactions(transactionVM, transaction, GL_REFUND_VOUCHER_AccId, EntitlementMapValue, CONST_DEBIT, Description + "Entitlement_Voucher_Expires");
                            }
                            break;
                        case ENTITLEMENT_COD:
                            {
                                var GL_COD_RECEIVABLES_AccId = GetGLAccountId(GetSettingByCode(GL_COD_RECEIVABLES_CODE));
                                MergeAccountTransactions(transactionVM, transaction, GL_COD_RECEIVABLES_AccId, EntitlementMapValue, CONST_CREDIT, Description + "Entitlement_COD");
                                MergeAccountTransactions(transactionVM, transaction, GL_COGS_AccId, TotalCostCenterValue, CONST_CREDIT, Description + "Entitlement_COD");
                                MergeAccountTransactions(transactionVM, transaction, GL_INVENTORY_AccId, EntitlementMapValue, CONST_DEBIT, Description + "Entitlement_COD");
                                MergeAccountTransactions(transactionVM, transaction, CustomerGLAccountId, TotalCostCenterValue, CONST_DEBIT, Description + "Entitlement_COD");
                            }
                            break;
                        case ENTITLEMENT_CREDIT:
                            {
                                var GL_CUSTOMER_ACCOUNT_AccId = GetGLAccountId(GetSettingByCode(GL_CUSTOMER_ACCOUNT_CODE));
                                MergeAccountTransactions(transactionVM, transaction, GL_CUSTOMER_ACCOUNT_AccId, EntitlementMapValue, CONST_CREDIT, Description + "Entitlement_Credit");
                                MergeAccountTransactions(transactionVM, transaction, GL_COGS_AccId, TotalCostCenterValue, CONST_CREDIT, Description + "Entitlement_Credit");
                                MergeAccountTransactions(transactionVM, transaction, GL_INVENTORY_AccId, EntitlementMapValue, CONST_DEBIT, Description + "Entitlement_Credit");
                                MergeAccountTransactions(transactionVM, transaction, CustomerGLAccountId, TotalCostCenterValue, CONST_DEBIT, Description + "Entitlement_Credit");
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

                if (IsDBOperationSuccess)
                {
                   // / Add Receivables
                    long? CustomerGLAccountId = null;
                    if (transaction.CustomerID != null)
                    {
                        CustomerGLAccountId = GetCustomerGLAccountId((long)transaction.CustomerID, -1); //Customer Root Account
                        if (CustomerGLAccountId == 0)
                        {
                            WriteLog("CustomerGLAccountId not found:" + transaction.CustomerID.ToString());
                            return;
                        }
                    }
                    //Here it need to add an entry to Payable table- If any amount need to be returned to the customer
                    //Calculate the amount based on the Received Amount (from Receivable table) and the return amount

                    //IsDBOperationSuccess = AddInvetoryReceivable(transaction, CustomerGLAccountId);
                    //update TransactionHeadAccountProcessingStatus flag
                }
            }
            catch (Exception ex)
            {
                UpdateAccountTransactionProcessStatus(transaction.HeadIID, Eduegate.Framework.Enums.TransactionStatus.Failed);
                WriteLog(ex.Message.ToString(), ex);
                WriteLog("Exception occured in SalesReturnAccounting-Process.:" + ex.Message, ex);
            }
        }

    }
}

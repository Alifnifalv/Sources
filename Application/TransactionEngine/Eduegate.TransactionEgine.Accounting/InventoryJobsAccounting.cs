using System;
using System.Collections.Generic;
using Eduegate.TransactionEgine.Accounting.Interfaces;
using Eduegate.TransactionEgine.Accounting.ViewModels;

namespace Eduegate.TransactionEgine.Accounting
{
    public class InventoryJobsAccounting : AccountingBase, ITransactions
    {
        private Action<string> _logError;

        public InventoryJobsAccounting(Action<string> logError)
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
            WriteLog("PurchaseInvoice-AccountingProcess started for TransactionID:" + transaction.HeadIID.ToString());

            try
            {
                long transactionHeadIID = transaction.HeadIID;
                string Description = "Sales Order: Transaction-" + transactionHeadIID.ToString() + " ";
                var transactionVM = new List<ViewModels.AccountTransactionViewModel>();

                decimal PurchaseSuspendedAmount = 0;
                PurchaseSuspendedAmount = GetPurchaseSuspendedAmount(null);

                //Common
                var GL_SUSPENDED_AccId = GetGLAccountId(GetSettingByCode(GL_SUSPENDED_CODE));
                var GL_PURCHASE_AccId = GetGLAccountId(GetSettingByCode(GL_PURCHASE_CODE));
                var GL_PURCHASE_ADDITIONAL_AccId = GetGLAccountId(GetSettingByCode(GL_PURCHASE_ADDITIONAL_CODE));
                var GL_INVENTORY_AccId = GetGLAccountId(GetSettingByCode(GL_INVENTORY_CODE));
                //Common

                var TransactionHeadEntitlementMapsList = GetTransactionHeadEntitlementMaps(transactionHeadIID);
                foreach (var entitlementMapDTO in TransactionHeadEntitlementMapsList)
                {
                    long? SupplierGLAccountId = null;
                    if (transaction.SupplierID != null)
                    {
                        SupplierGLAccountId = GetSupplierGLAccountId((long)transaction.SupplierID,(int) entitlementMapDTO.EntitlementID);
                        if (SupplierGLAccountId == 0)
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

                    Description = Description + entitlementMapDTO.EntitlementName;
                    MergeAccountTransactions(transactionVM, transaction, SupplierGLAccountId, EntitlementMapValue, CONST_CREDIT,  Description);
                    MergeAccountTransactions(transactionVM, transaction, GL_SUSPENDED_AccId, PurchaseSuspendedAmount, CONST_CREDIT,  Description);
                    MergeAccountTransactions(transactionVM, transaction, GL_PURCHASE_AccId, EntitlementMapValue, CONST_DEBIT,  Description);
                    MergeAccountTransactions(transactionVM, transaction, GL_PURCHASE_ADDITIONAL_AccId, PurchaseSuspendedAmount, CONST_DEBIT,  Description);
                    MergeAccountTransactions(transactionVM, transaction, GL_INVENTORY_AccId, EntitlementMapValue, CONST_DEBIT,  Description);
                    MergeAccountTransactions(transactionVM, transaction, GL_PURCHASE_AccId, EntitlementMapValue, CONST_CREDIT,  Description);
                    MergeAccountTransactions(transactionVM, transaction, GL_PURCHASE_ADDITIONAL_AccId, PurchaseSuspendedAmount, CONST_CREDIT,  Description);
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
                    //Add Payables
                    long? SupplierGLAccountId = null;
                    if (transaction.SupplierID != null)
                    {
                        SupplierGLAccountId = GetSupplierGLAccountId((long)transaction.SupplierID, -1); //Supplier Root Account
                        if (SupplierGLAccountId == 0)
                        {
                            WriteLog("SupplierGLAccountId not found:" + transaction.SupplierID.ToString());
                            return;
                        }
                    }
                    IsDBOperationSuccess = AddInvetoryPayable(transaction, SupplierGLAccountId , CONST_DEBIT);

                    //update TransactionHeadAccountProcessingStatus flag
                }
            }
            catch (Exception ex)
            {
                UpdateAccountTransactionProcessStatus(transaction.HeadIID, Eduegate.Framework.Enums.TransactionStatus.Failed);
                WriteLog(ex.Message.ToString(), ex);
                WriteLog("Exception occured in PurchaseInvoiceAccounting-Process.:" + ex.Message, ex);
            }
        }

    }
}

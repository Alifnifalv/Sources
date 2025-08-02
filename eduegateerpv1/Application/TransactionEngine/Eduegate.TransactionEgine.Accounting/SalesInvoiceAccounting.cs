using System;
using System.Collections.Generic;
using Eduegate.Domain.Repository;
using Eduegate.TransactionEgine.Accounting.Interfaces;
using Eduegate.TransactionEgine.Accounting.ViewModels;

namespace Eduegate.TransactionEgine.Accounting
{
    public class SalesInvoiceAccounting : AccountingBase, ITransactions
    {
        private Action<string> _logError;

        public SalesInvoiceAccounting(Action<string> logError)
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
            WriteLog("SalesInvoice-AccountingProcess started for TransactionID:" + transaction.HeadIID.ToString());

            try
            {
                long transactionHeadIID = transaction.HeadIID;
                var transactionVM = new List<ViewModels.AccountTransactionViewModel>();

                //Cost from Cost Center COGS
                decimal toalAmount = GetCostCenterPrice(transaction);

                //Common
                var GL_ORDER_IN_PROCESS_AccId = GetGLAccountId(GetSettingByCode(GL_ORDER_IN_PROCESS_CODE));
                var GL_COGS_AccId = GetGLAccountId(GetSettingByCode(GL_COGS_CODE));
                var inventoryAccount = GetGLAccountId(GetSettingByCode(GL_INVENTORY_CODE));
                var GL_SALES_DISCOUNT_AccId = GetGLAccountId(GetSettingByCode(GL_SALES_DISCOUNT_CODE));
                var GL_DRIVER_CASH_COLLECTION_AccId = GetGLAccountId(GetSettingByCode(GL_DRIVER_CASH_COLLECTION_CODE));
                var hasMultiGLForCustomer = false;
                var CREDITCARDID = GetSettingByCode(SETTING_CREDITCARDID);
                bool ISAutoReceipt = false;
                if (transaction.CreatedBy.HasValue)
                {
                    hasMultiGLForCustomer = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().HasClaimAccess(
                        (long)Framework.Helper.Enums.HasClaims.HASMULTIGLFORCUSTOMER, transaction.CreatedBy.Value);
                }

                var entitlementMap = GetTransactionHeadEntitlementMaps(transactionHeadIID);
                decimal paidAmount = 0;
                bool IsCredit = false;
                //------ Checking whether it is online sale
                bool IsOnlineSale = false;
                if (transaction.ReferenceHeadID.HasValue && transaction.ReferenceHeadID.Value != 0)
                {
                    var transDetails = new TransactionRepository().GetTransaction(transaction.ReferenceHeadID.Value);
                    if (transDetails != null)
                    {

                        var onlineStoreDocumentTypeID = GetSettingByCode(SETTING_DOCUMENT_ONLINESTORE);
                        if (transDetails.DocumentTypeID == int.Parse(onlineStoreDocumentTypeID))
                        {
                            IsOnlineSale = true;
                        }
                    }
                }
                else
                    IsOnlineSale = false;

                foreach (var entitlement in entitlementMap)
                {
                    string description = "Sales Invoice: Transaction-" + transactionHeadIID.ToString() + " " + entitlement.EntitlementName;
                    long? customerGLAccountId = null;

                    if (byte.Parse(CREDITCARDID) == entitlement.EntitlementID)
                    { ISAutoReceipt = true; }

                    if (transaction.CustomerID != null)
                    {
                        //customerGLAccountId = GetCustomerGLAccountId((long)transaction.CustomerID, hasMultiGLForCustomer ? entitlement.EntitlementID : (int?)null);
                        customerGLAccountId = GetCustomerGLAccountId((long)transaction.CustomerID, entitlement.EntitlementID);

                        if (customerGLAccountId == 0)
                        {
                            UpdateAccountTransactionProcessStatus(transaction.HeadIID, Eduegate.Framework.Enums.TransactionStatus.Failed);
                            return;
                        }
                    }

                    decimal entitlementAmount = 0;

                    if (entitlement.Amount != null)
                    {
                        entitlementAmount = Convert.ToDecimal(entitlement.Amount);
                    }

                    switch (entitlement.EntitlementName)
                    {
                        case ENTITLEMENT_VISAMASTERCARD:
                            {
                                MergeAccountTransactions(transactionVM, transaction, GL_ORDER_IN_PROCESS_AccId, entitlementAmount, CONST_DEBIT, description);
                                MergeAccountTransactions(transactionVM, transaction, GL_COGS_AccId, toalAmount, CONST_DEBIT, description);
                                MergeAccountTransactions(transactionVM, transaction, inventoryAccount, entitlementAmount, CONST_CREDIT, description);
                                MergeAccountTransactions(transactionVM, transaction, customerGLAccountId, toalAmount, CONST_CREDIT, description);
                            }
                            break;
                        case ENTITLEMENT_PAYPAL:
                            {
                                MergeAccountTransactions(transactionVM, transaction, GL_ORDER_IN_PROCESS_AccId, entitlementAmount, CONST_DEBIT, description);
                                MergeAccountTransactions(transactionVM, transaction, GL_COGS_AccId, toalAmount, CONST_DEBIT, description);
                                MergeAccountTransactions(transactionVM, transaction, inventoryAccount, entitlementAmount, CONST_CREDIT, description);
                                MergeAccountTransactions(transactionVM, transaction, customerGLAccountId, toalAmount, CONST_CREDIT, description);
                            }
                            break;
                        case ENTITLEMENT_VOUCHER:
                            {
                                var voucherTypeId = GetVoucherTypeId((long)transaction.HeadIID);
                                var GL_MARKETING_VOUCHER_AccId = GetGLAccountId(GetSettingByCode(GL_MARKETING_VOUCHER_CODE));
                                var GL_VOUCHER_EXPENSE_AccId = GetGLAccountId(GetSettingByCode(GL_VOUCHER_EXPENSE_CODE));
                                var GL_REFUND_VOUCHER_AccId = GetGLAccountId(GetSettingByCode(GL_REFUND_VOUCHER_CODE));

                                switch (voucherTypeId)
                                {
                                    case Eduegate.Services.Contracts.Enums.VoucherTypes.Marketing:
                                        MergeAccountTransactions(transactionVM, transaction, GL_MARKETING_VOUCHER_AccId, entitlementAmount, CONST_DEBIT, description);
                                        break;
                                    case Eduegate.Services.Contracts.Enums.VoucherTypes.Free:
                                        MergeAccountTransactions(transactionVM, transaction, GL_REFUND_VOUCHER_AccId, entitlementAmount, CONST_DEBIT, description);
                                        break;
                                    case Eduegate.Services.Contracts.Enums.VoucherTypes.Loyalty:
                                        MergeAccountTransactions(transactionVM, transaction, GL_VOUCHER_EXPENSE_AccId, entitlementAmount, CONST_DEBIT, description);
                                        break;
                                }

                                MergeAccountTransactions(transactionVM, transaction, GL_COGS_AccId, toalAmount, CONST_DEBIT, description);
                                MergeAccountTransactions(transactionVM, transaction, inventoryAccount, entitlementAmount, CONST_CREDIT, description);
                                MergeAccountTransactions(transactionVM, transaction, customerGLAccountId, toalAmount, CONST_CREDIT, description);
                            }

                            break;
                        case ENTITLEMENT_CASH:
                            {
                                var cashAccountID = GetGLAccountId(GetSettingByCode(GL_CASH_CODE));
                                MergeAccountTransactions(transactionVM, transaction, cashAccountID, entitlementAmount, CONST_DEBIT, description);
                                MergeAccountTransactions(transactionVM, transaction, inventoryAccount, -1 * entitlementAmount, CONST_CREDIT, description);
                                paidAmount = paidAmount + entitlementAmount;
                            }
                            break;
                        case ENTITLEMENT_COD:
                            {
                                var GL_COD_RECEIVABLES_AccId = GetGLAccountId(GetSettingByCode(GL_COD_RECEIVABLES_CODE));
                                MergeAccountTransactions(transactionVM, transaction, GL_COD_RECEIVABLES_AccId, entitlementAmount, CONST_DEBIT, description);
                                MergeAccountTransactions(transactionVM, transaction, GL_COGS_AccId, toalAmount, CONST_DEBIT, description);
                                MergeAccountTransactions(transactionVM, transaction, inventoryAccount, entitlementAmount, CONST_CREDIT, description);
                                MergeAccountTransactions(transactionVM, transaction, customerGLAccountId, toalAmount, CONST_CREDIT, description);
                            }
                            break;
                        case ENTITLEMENT_CREDIT:
                            {
                                IsCredit = true;
                                //var GL_CUSTOMER_ACCOUNT_AccId = GetGLAccountId(GetSettingByCode(GL_CUSTOMER_ACCOUNT_CODE));
                                //MergeAccountTransactions(transactionVM, transaction, GL_CUSTOMER_ACCOUNT_AccId, entitlementAmount, CONST_DEBIT, description);
                                //MergeAccountTransactions(transactionVM, transaction, GL_COGS_AccId, toalAmount, CONST_DEBIT, description);
                                //MergeAccountTransactions(transactionVM, transaction, inventoryAccount, entitlementAmount, CONST_CREDIT, description);
                                //MergeAccountTransactions(transactionVM, transaction, customerGLAccountId, toalAmount, CONST_CREDIT, description);
                                //MergeAccountTransactions(transactionVM, transaction, GL_SALES_DISCOUNT_AccId, entitlementAmount, CONST_DEBIT, description);
                            }
                            break;
                            //case ENTITLEMENT_CREDITCARD:
                            //    {
                            //        var GL_CUSTOMER_ACCOUNT_AccId = GetGLAccountId(GetSettingByCode(GL_CUSTOMER_ACCOUNT_CODE));
                            //        MergeAccountTransactions(transactionVM, transaction, GL_CUSTOMER_ACCOUNT_AccId, entitlementAmount, CONST_DEBIT, description);
                            //        MergeAccountTransactions(transactionVM, transaction, GL_COGS_AccId, toalAmount, CONST_DEBIT, description);
                            //        MergeAccountTransactions(transactionVM, transaction, inventoryAccount, entitlementAmount, CONST_CREDIT, description);
                            //        MergeAccountTransactions(transactionVM, transaction, customerGLAccountId, toalAmount, CONST_CREDIT, description);
                            //        MergeAccountTransactions(transactionVM, transaction, GL_SALES_DISCOUNT_AccId, entitlementAmount, CONST_DEBIT, description);
                            //    }
                            //    break;
                    }
                }
                if (IsCredit && ISAutoReceipt == false && IsOnlineSale == false)//Sales Invoice Account posting modification for adding fee due at first then selling the products
                {
                    AccountTransMerge(0, transaction.HeadIID, transaction.TransactionDate.Value, 11);
                }
                else
                {
                    //Add to DB
                    bool IsDBOperationSuccess = false;
                    if (transactionVM != null && transactionVM.Count > 0)
                    {
                        var transactionEntries = AccountTransactionViewModel.ConvertViewModelToDTO(transactionVM);
                        //delete if the data existing
                        var headIDs = new List<long>();
                        IsDBOperationSuccess = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().AddAccountTransactions(transactionEntries);
                    }

                    if (IsDBOperationSuccess)
                    {
                        //foreach (var entitlement in entitlementMap)
                        //{
                        //Add Receivables
                        long? customerGLAccountId = null;
                        if (transaction.CustomerID != null)
                        {
                            customerGLAccountId = GetCustomerGLAccountId((long)transaction.CustomerID, -1); //Customer Root Account
                            if (customerGLAccountId == 0)
                            {
                                WriteLog("CustomerGLAccountId not found:" + transaction.CustomerID.ToString());
                                return;
                            }
                        }

                        IsDBOperationSuccess = AddInvetoryReceivable(transaction, customerGLAccountId, paidAmount, CONST_DEBIT);
                        //}
                    }
                }
                //if (ISAutoReceipt == false)

                //    AccountTransactionSync(0, transaction.HeadIID, 11);
            }
            catch (Exception ex)
            {
                UpdateAccountTransactionProcessStatus(transaction.HeadIID, Eduegate.Framework.Enums.TransactionStatus.Failed);
                WriteLog(ex.Message.ToString(), ex);
                WriteLog("Exception occured in SalesInvoiceAccounting-Process.:" + ex.Message, ex);
            }
        }
    }
}

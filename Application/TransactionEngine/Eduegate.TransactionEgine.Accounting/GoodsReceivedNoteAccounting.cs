using Eduegate.TransactionEgine.Accounting.Interfaces;
using Eduegate.TransactionEgine.Accounting.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.TransactionEgine.Accounting
{
   public class GoodsReceivedNoteAccounting : AccountingBase, ITransactions
    {
        private Action<string> _logError;

        public GoodsReceivedNoteAccounting(Action<string> logError)
        {
            _logError = logError;
        }

        public Framework.Enums.DocumentReferenceTypes ReferenceTypes
        {
            get { return Framework.Enums.DocumentReferenceTypes.GoodsReceivedNotes; }
        }
        private void WriteLog(string message, Exception ex = null)
        {
            if (_logError != null)
                _logError(message);
        }
        public void Process(ViewModels.TransactionHeadViewModel transaction)
        {
            WriteLog("GoodsReceivedNoteAccounting-AccountingProcess started for TransactionID:" + transaction.HeadIID.ToString());

            try
            {
                long transactionHeadIID = transaction.HeadIID;
                string Description = "GoodsReceivedNoteAccounting: Transaction-" + transactionHeadIID.ToString() + " ";
                var transactionVM = new List<ViewModels.AccountTransactionViewModel>();


                //Purchase Return
                //Select purchase invoice then
                //CR - Inventory
                //DB - Supplier
                //DB - Purchase
                //CR - Purchase

                //Common
                var GL_PURCHASE_AccId = GetGLAccountId(GetSettingByCode(GL_PURCHASE_CODE));
                var GL_INVENTORY_AccId = GetGLAccountId(GetSettingByCode(GL_INVENTORY_CODE));
                long? SupplierGLAccountId = null;
                if (transaction.SupplierID != null)
                {
                    SupplierGLAccountId = GetSupplierGLAccountId((long)transaction.SupplierID, -1); //root account
                    if (SupplierGLAccountId == 0)
                    {
                        UpdateAccountTransactionProcessStatus(transaction.HeadIID, Eduegate.Framework.Enums.TransactionStatus.Failed);
                        return;
                    }
                }
                //List<EntitlementMapDTO> TransactionHeadEntitlementMapsList = GetTransactionHeadEntitlementMaps(transactionHeadIID); //assuming there will be only COD amount
                //decimal amount = Convert.ToInt64(TransactionHeadEntitlementMapsList.Sum(x => x.EntitlementAmount));

                decimal amount = Convert.ToInt64(transaction.TransactionDetails.Sum(x => x.Quantity * x.UnitPrice));

                MergeAccountTransactions(transactionVM, transaction, GL_INVENTORY_AccId, amount, CONST_CREDIT, Description);
                MergeAccountTransactions(transactionVM, transaction, SupplierGLAccountId, amount, CONST_DEBIT, Description);
                MergeAccountTransactions(transactionVM, transaction, GL_PURCHASE_AccId, amount, CONST_DEBIT, Description);
                MergeAccountTransactions(transactionVM, transaction, GL_PURCHASE_AccId, amount, CONST_CREDIT, Description);

                //Add to DB
                bool IsDBOperationSuccess = false;
                if (transactionVM != null && transactionVM.Count > 0)
                {
                    var AccountTransactionsDTOList = AccountTransactionViewModel.ConvertViewModelToDTO(transactionVM);
                    IsDBOperationSuccess = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().AddAccountTransactions(AccountTransactionsDTOList);
                }

                if (IsDBOperationSuccess)
                {
                    // There should be an entry to receivables if some amount is to be collected from supplier
                    //Calculate the amount based on the Paid Amount (from Pyables table) and the return amount

                    //IsDBOperationSuccess = AddInvetoryPayable(transaction, SupplierGLAccountId);
                    //update TransactionHeadAccountProcessingStatus flag
                }
            }
            catch (Exception ex)
            {
                UpdateAccountTransactionProcessStatus(transaction.HeadIID, Eduegate.Framework.Enums.TransactionStatus.Failed);
                WriteLog(ex.Message.ToString(), ex);
                WriteLog("Exception occured in GoodsReceivedNoteAccounting-Process.:" + ex.Message, ex);
            }
        }

    }
}


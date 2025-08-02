using System;
using Eduegate.Domain.Mappers.School.Accounts;
using Eduegate.Framework;
using Eduegate.TransactionEgine.Accounting.Interfaces;
using Eduegate.TransactionEgine.Accounting.ViewModels;

namespace Eduegate.TransactionEgine.Accounting
{
    public class AssetTransferReceiptAccounting : AccountingBase, IAssetTransactions
    {
        private Action<string> _logError;
        private CallContext _callContext;

        public AssetTransferReceiptAccounting(Action<string> logError, CallContext context = null)
        {
            _logError = logError;
            _callContext = context;
        }

        public Framework.Enums.DocumentReferenceTypes ReferenceTypes
        {
            get { return Framework.Enums.DocumentReferenceTypes.AssetTransferReceipt; }
        }

        private void WriteLog(string message, Exception ex = null)
        {
            if (_logError != null)
                _logError(message);
        }

        public void Process(AssetTransactionHeadViewModel transaction)
        {
            WriteLog("AssetTransferReceipt-AccountingProcess started for TransactionID:" + transaction.HeadIID.ToString());
            try
            {
                DateTime currentDate = DateTime.Now;
                int type = transaction.DocumentTypeID.HasValue ? transaction.DocumentTypeID.Value : 0;
                int loginID = _callContext.LoginID.HasValue ? (int)_callContext.LoginID.Value : 0;

                AccountEntryMapper.Mapper(_callContext).AccountMergeWithMultipleTransactionIDs(transaction.HeadIID.ToString(), currentDate, loginID, type);
            }
            catch (Exception ex)
            {
                UpdateAccountTransactionProcessStatus(transaction.HeadIID, Eduegate.Framework.Enums.TransactionStatus.Failed);
                WriteLog(ex.Message.ToString(), ex);
                WriteLog("Exception occured in AssetTransferReceiptAccounting-Process.:" + ex.Message, ex);
            }
        }

        //private void CreateCashPaidAccountEntries(List<KeyValueViewModel> AccountTransactionViewModelList, long CreditAccId, long DebitAccId, decimal EntitlementMapValue, long transactionHeadIID)
        //{
        //    new ServiceAccessUtils().CreateAccountTransactionViewModel(DebitAccId, EntitlementMapValue, CONST_Debit, transactionHeadIID));
        //    new ServiceAccessUtils().CreateAccountTransactionViewModel(CreditAccId, EntitlementMapValue, CONST_Credit, transactionHeadIID));
        //}

    }
}
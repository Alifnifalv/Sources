using System;
using Eduegate.Domain.Mappers.School.Accounts;
using Eduegate.Framework;
using Eduegate.TransactionEgine.Accounting.Interfaces;
using Eduegate.TransactionEgine.Accounting.ViewModels;

namespace Eduegate.TransactionEgine.Accounting
{
    public class AssetDisposalAccounting : AccountingBase, IAssetTransactions
    {
        private Action<string> _logError;
        private CallContext _callContext;

        public AssetDisposalAccounting(Action<string> logError, CallContext context = null)
        {
            _logError = logError;
            _callContext = context;
        }

        public Framework.Enums.DocumentReferenceTypes ReferenceTypes
        {
            get { return Framework.Enums.DocumentReferenceTypes.AssetRemoval; }
        }

        private void WriteLog(string message, Exception ex = null)
        {
            if (_logError != null)
                _logError(message);
        }

        public void Process(AssetTransactionHeadViewModel transaction)
        {
            WriteLog("AssetDisposal-AccountingProcess started for TransactionID:" + transaction.HeadIID.ToString());
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
                WriteLog("Exception occured in AssetDisposalAccounting-Process.:" + ex.Message, ex);
            }
        }

    }
}
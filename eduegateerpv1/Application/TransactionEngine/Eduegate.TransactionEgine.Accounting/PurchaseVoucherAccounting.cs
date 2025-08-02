using Eduegate.Services.Contracts.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.TransactionEgine.Accounting
{
    public class PurchaseVoucherAccounting : AccountingBase
    {
        private Action<string> _logError;

        public PurchaseVoucherAccounting(Action<string> logError)
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

        public void Process(AccountTransactionHeadDTO accountTransactionHead)
        {
            //WriteLog("Geting Asset Transactions - " + referenceTypes.ToString() + " " + status.ToString());
            //var transactions = GetAssetTransactions(referenceTypes, status);
            //WriteLog(" Asset Transaction found - " + transactions.Count());

            try
            {
                //for each start
                //for each end
            }
            catch (Exception ex)
            {
                //Eduegate.Logger.LogHelper<string>.Fatal(ex.Message.ToString(), ex);
                WriteLog(ex.Message.ToString());
            }
        }
    }
}

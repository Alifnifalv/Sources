using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.TransactionEgine.Accounting.Interfaces;
using Eduegate.TransactionEgine.Accounting.ViewModels;
using Eduegate.Services.Contracts.Accounting;

namespace Eduegate.TransactionEgine.Accounting
{
    public class MissionJobReturnAccounting : AccountingBase, IJobEntryTransactions
    {
        private Action<string> _logError;

        public MissionJobReturnAccounting(Action<string> logError)
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
        public void Process(JobEntryHeadAccountingDTO jobEntryHeadAccountingDTO)
        {
            var transaction = TransactionHeadViewModel.FromDTO(jobEntryHeadAccountingDTO.TransactionHeadDTO);
            WriteLog("MissionJobReturnAccounting-AccountingProcess started for TransactionID:" + transaction.HeadIID.ToString());

            try
            {
                long transactionHeadIID = transaction.HeadIID;
                var TransactionHeadEntitlementMapsList = GetTransactionHeadEntitlementMaps(transactionHeadIID); //assuming there will be only COD amount
                var amount = Convert.ToDecimal(TransactionHeadEntitlementMapsList.Sum(x => x.Amount));

                string Description = "MissionJobReturnAccounting: Transaction-" + transactionHeadIID.ToString() + " ";
                var AccountTransactionViewModelList = new List<ViewModels.AccountTransactionViewModel>();

                var GL_COD_RECEIVABLES_AccId = GetGLAccountId(GetSettingByCode(GL_COD_RECEIVABLES_CODE));
                var driverAccountId = Convert.ToInt64(jobEntryHeadAccountingDTO.AccountID);

                Create_MissionJob_TransactionHeadViewModel(GL_COD_RECEIVABLES_AccId, amount, CONST_DEBIT, transactionHeadIID, Description);
                Create_MissionJob_TransactionHeadViewModel(driverAccountId, amount, CONST_CREDIT, transactionHeadIID, Description);


                //Add to DB
                bool IsDBOperationSuccess = false;
                if (AccountTransactionViewModelList != null && AccountTransactionViewModelList.Count > 0)
                {
                    var AccountTransactionsDTOList = AccountTransactionViewModel.ConvertViewModelToDTO(AccountTransactionViewModelList);
                    IsDBOperationSuccess = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().AddAccountTransactions(AccountTransactionsDTOList);
                }
                
            }
            catch (Exception ex)
            {
                UpdateAccountTransactionProcessStatus(transaction.HeadIID, Eduegate.Framework.Enums.TransactionStatus.Failed);
                WriteLog(ex.Message.ToString(), ex);
                WriteLog("Exception occured in MissionJobReturnAccounting-Process.:" + ex.Message, ex);
            }
        }

    }
}

using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.TransactionEngineCore;
using Eduegate.TransactionEngineCore.Interfaces;
using Eduegate.TransactionEngineCore.ViewModels;
using System;
using System.Collections.Generic;

namespace Eduegate.TransactionEgineCore
{
    public class ServiceEntry : TransactionBase, ITransactions
    {
        public ServiceEntry(Action<string> logError)
        {
            _logError = logError;
        }


        public DocumentReferenceTypes ReferenceTypes
        {
            get { return DocumentReferenceTypes.ServiceEntry; }
        }

        public void Process(TransactionEngineCore.ViewModels.TransactionHeadViewModel transaction)
        {
            WriteLog("ServiceEntry-process started for TransactionID:" + transaction.HeadIID.ToString());

            try
            {
                WriteLog("Processing ServiceEntry :" + transaction.HeadIID.ToString());

                switch ((Eduegate.Framework.Enums.TransactionStatus)transaction.TransactionStatusID)
                {
                    case Eduegate.Framework.Enums.TransactionStatus.New:
                    case Eduegate.Framework.Enums.TransactionStatus.Edit:
                        WriteLog("Processing new ServiceEntry.");
                        ProcessNew(transaction);

                        break;
                    case Eduegate.Framework.Enums.TransactionStatus.IntitiateReprocess:
                        switch ((Eduegate.Services.Contracts.Enums.DocumentStatuses)transaction.DocumentStatusID)
                        {
                            case Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled:
                                WriteLog("Cancelling ServiceEntry.");
                                ProcessCancellation(transaction);
                                break;
                        }

                        break;
                }

                WriteLog("Completed:" + transaction.HeadIID.ToString());
            }
            catch (Exception ex)
            {
                UpdateTransactionHead(transaction, Eduegate.Framework.Enums.TransactionStatus.Failed, Services.Contracts.Enums.DocumentStatuses.Draft);
                WriteLog(ex.Message.ToString(), ex);
                WriteLog("Exception occured in ServiceEntry-Process.:" + ex.Message, ex);
                TransactionProcessingFailed(transaction, ex.Message);
            }
        }

        public void ProcessNew(TransactionHeadViewModel transaction)
        {
            UpdateTransactionHead(transaction, TransactionStatus.Complete);

        }

        public bool ProcessCancellation(TransactionEngineCore.ViewModels.TransactionHeadViewModel transaction)
        {
            return new Eduegate.TransactionEgine.ClientFactory.ClientFactory().CancelTransaction(transaction.HeadIID);
        }

        #region Private Methods

        private bool UpdateTransactionHead(TransactionHeadViewModel transactions, TransactionStatus transactionStatus)
        {
            //bool isSuccess = false;
            TransactionHeadDTO dto = new TransactionHeadDTO();
            dto.HeadIID = transactions.HeadIID;
            dto.TransactionStatusID = (byte)transactionStatus;
            // call the service
            //string uri = string.Concat(TransactionService + "UpdateTransactionHead");
            //string serviceResult = ServiceHelper.HttpPostRequest(uri, dto);

            //if (serviceResult == "true")
            //{
            //    isSuccess = true;
            //}
            //return isSuccess;

            // call the service
            return new Eduegate.TransactionEgine.ClientFactory.ClientFactory(null).UpdateTransactionHead(dto);
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using Eduegate.Framework.Enums;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Accounts.Assets;
using Eduegate.TransactionEgine.Accounting.ViewModels;
using Eduegate.TransactionEngineCore.Interfaces;

namespace Eduegate.TransactionEngineCore
{
    public class AssetDisposal : AssetTransactionBase, IAssetTransactions
    {
        public AssetDisposal(Action<string> logError)
        {
            _logError = logError;
        }

        public Framework.Enums.DocumentReferenceTypes ReferenceTypes
        {
            get { return Framework.Enums.DocumentReferenceTypes.AssetRemoval; }
        }

        public void Process(AssetTransactionHeadViewModel transaction)
        {
            WriteLog("Asset removal-Process started for TransactionID:" + transaction.HeadIID.ToString());

            try
            {
                WriteLog("Processing asset removal:" + transaction.HeadIID.ToString());

                switch ((Eduegate.Framework.Enums.TransactionStatus)transaction.ProcessingStatusID)
                {
                    case Eduegate.Framework.Enums.TransactionStatus.New:
                    case Eduegate.Framework.Enums.TransactionStatus.Edit:
                        WriteLog("Processing new transaction.");
                        ProcessNew(transaction);
                        break;
                    case Eduegate.Framework.Enums.TransactionStatus.IntitiateReprocess:
                        switch ((Eduegate.Services.Contracts.Enums.DocumentStatuses)transaction.DocumentStatusID)
                        {
                            case Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled:
                                WriteLog("Cancelling transaction.");
                                ProcessCancellation(transaction);
                                break;
                        }
                        break;
                }

                WriteLog("Completed:" + transaction.HeadIID.ToString());
            }
            catch (Exception ex)
            {
                UpdateAssetTransactionHead(transaction, Eduegate.Framework.Enums.TransactionStatus.Failed, Services.Contracts.Enums.DocumentStatuses.Draft);
                WriteLog(ex.Message.ToString(), ex);
                WriteLog("Exception occured in asset removal-Process.:" + ex.Message, ex);
                AssetTransactionProcessingFailed(transaction, ex.Message);
            }
        }

        public bool ProcessCancellation(AssetTransactionHeadViewModel transaction)
        {
            return new Eduegate.TransactionEgine.ClientFactory.ClientFactory().CancelAssetTransaction(transaction.HeadIID);
        }

        public void ProcessNew(AssetTransactionHeadViewModel transaction)
        {
            WriteLog("AssetDisposal-Process started for TransactionID:" + transaction.HeadIID.ToString());
            bool isSuccess = false;

            try
            {
                // check if order is digital or physical
                var transactionDetail = transaction.AssetTransactionDetails;

                // If Transaction is Edited, revert the last one and process current one
                if (transaction.ProcessingStatusID == byte.Parse(((int)(TransactionStatus.Edit)).ToString()))
                {
                    isSuccess = RevertAssetDisposal(transaction.HeadIID);
                    WriteLog("Asset Disposal reverted?: " + isSuccess.ToString());
                }

                //0. Check Quantity
                WriteLog("Checking availability");
                //isSuccess = CheckAssetAvailability(transaction);
                isSuccess = true;
                WriteLog("Is Available? :" + isSuccess.ToString());

                //1. Update Product Inventory
                if (isSuccess)
                {
                    WriteLog("Calling AssetDisposal-TranferAssetInventory");
                    isSuccess = UpdateInAssetInventory(transaction);
                    WriteLog("Asset Dispossed?: " + isSuccess.ToString());
                }
                else
                {
                    isSuccess = UpdateAssetTransactionHead(transaction, TransactionStatus.Failed,Services.Contracts.Enums.DocumentStatuses.Draft);

                    if (isSuccess)
                    {
                        WriteLog("Method Update AssetTransactionHead(with Failed status) is Success for HeadID= " + transaction.HeadIID);
                        return;
                    }
                    else
                    {
                        WriteLog("Method Update AssetTransactionHead(with Failed status) is Failed for HeadID= " + transaction.HeadIID);
                        return;
                    }
                }

                //2. Save into inventory transactions
                if (isSuccess)
                {
                    WriteLog("Method Update AssetInventory is Success for HeadId= " + transaction.HeadIID);
                    WriteLog("Method Save AssetInventoryTransactions is Calling for HeadId= " + transaction.HeadIID);

                    isSuccess = SaveAssetInventoryTransactions(transaction);
                }
                else
                {
                    WriteLog("Method Save AssetInventoryTransactions is Failed for HeadId= " + transaction.HeadIID);
                    return;
                }

                //3. Update the status of the existing transaction head
                if (isSuccess)
                {
                    WriteLog("Method Save AssetInventoryTransactions is Success for HeadId= " + transaction.HeadIID);
                    WriteLog("Method Update AssetTransactionHead is Calling for HeadId= " + transaction.HeadIID);

                    UpdateAssetTransactionHead(transaction, TransactionStatus.Complete,Services.Contracts.Enums.DocumentStatuses.Completed);
                }
                else
                {
                    WriteLog("Method Update AssetTransactionHead is Failed for HeadId= " + transaction.HeadIID);
                    return;
                }

            }
            catch (Exception ex)
            {
                UpdateAssetTransactionHead(transaction, Eduegate.Framework.Enums.TransactionStatus.Failed,Services.Contracts.Enums.DocumentStatuses.Draft);
                WriteLog(ex.Message.ToString(), ex);
                WriteLog("Exception occured in Asset removal-Process.:" + ex.Message, ex);
            }
        }

        public bool UpdateInAssetInventory(AssetTransactionHeadViewModel transactions)
        {
            try
            {
                UpdateAssetInventory(transactions, DocumentReferenceTypes.AssetRemoval, transactions.BranchID.Value);
                return true;
            }
            catch
            {
                return false;
            }
        }
       
        private bool RevertAssetDisposal(long transactionHeadID)
        {
            try
            {
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SaveAssetInventoryTransactions(AssetTransactionHeadViewModel transaction)
        {
            var listDTO = new List<AssetInventoryTransactionDTO>();
            int serNo = 1;
            foreach (var item in transaction.AssetTransactionDetails)
            {
                var invetoryTransactionDTO = new AssetInventoryTransactionDTO()
                {
                    AssetTransactionHeadID = transaction.HeadIID,
                    DocumentTypeID = transaction.DocumentTypeID,
                    TransactionNo = transaction.TransactionNo,
                    TransactionDate = transaction.EntryDate,
                    TransactionDateString = transaction.EntryDate.Value.ToLongDateString(),
                    AccountID = transaction.AccountID,
                    AssetID = item.AssetID,
                    AssetSerialMapID = item.AssetSerialMapID,
                    Quantity = -1 * item.Quantity,
                    Amount = -1 * item.Amount,
                    BatchID = item.BatchID,
                    BranchID = transaction.ToBranchID,
                    SerialNo = serNo,
                    CompanyID = transaction.CompanyID,
                };

                // add the invetoryTransactionDTO in list
                listDTO.Add(invetoryTransactionDTO);

                serNo++;
            }

            // call the service
            return new Eduegate.TransactionEgine.ClientFactory.ClientFactory().SaveAssetInventoryTransactions(listDTO);
        }

    }
}
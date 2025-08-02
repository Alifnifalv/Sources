using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.TransactionEngineCore.Interfaces;
using Eduegate.TransactionEngineCore.ViewModels;
using System.Linq;

namespace Eduegate.TransactionEngineCore
{
    public class SalesReturn : TransactionBase, ITransactions
    {
        public DocumentReferenceTypes ReferenceTypes
        {
            get { return DocumentReferenceTypes.SalesReturn; }
        }

        public SalesReturn(Action<string> logError)
        {
            _logError = logError;
        }

        public void Process(TransactionHeadViewModel transaction)
        {
            WriteLog("Sales return-Process started for TransactionID:" + transaction.HeadIID.ToString());

            try
            {
                WriteLog("Processing Sales return :" + transaction.HeadIID.ToString());

                switch ((Eduegate.Framework.Enums.TransactionStatus)transaction.TransactionStatusID)
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
                UpdateTransactionHead(transaction, Eduegate.Framework.Enums.TransactionStatus.Failed, Services.Contracts.Enums.DocumentStatuses.Draft);
                WriteLog(ex.Message.ToString(), ex);
                WriteLog("Exception occured in Sales return-Process.:" + ex.Message, ex);
                TransactionProcessingFailed(transaction, ex.Message);
            }
        }

        public void ProcessNew(TransactionHeadViewModel transaction)
        {
            WriteLog("Processing Sales return :" + transaction.HeadIID.ToString());

            if (UpdateProductInventory(transaction))
            {
                //update sales invoice status to return
                // get the reference transaction
                long? invoiceID = null;
                TransactionDTO referenceTransaction = null;

                if (transaction.ReferenceHeadID.HasValue)
                {
                    referenceTransaction = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().GetTransaction(transaction.ReferenceHeadID.Value, true);
                    invoiceID = referenceTransaction.TransactionHead.HeadIID;

                    if ((DocumentReferenceTypes)Enum.Parse(typeof(DocumentReferenceTypes), referenceTransaction.TransactionHead.DocumentReferenceType) == DocumentReferenceTypes.SalesReturnRequest)
                    {
                        referenceTransaction = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().GetTransaction(transaction.ReferenceHeadID.Value, true);
                        invoiceID = referenceTransaction.TransactionHead.HeadIID;
                    }
                }

                //2. Save into inventory transactions
                if (invoiceID.HasValue)
                {
                    var status = Services.Contracts.Enums.DocumentStatuses.Returned;

                    //make sure it's partail
                    if (referenceTransaction != null && referenceTransaction.TransactionDetails.Count() > 0)
                    {
                        if (referenceTransaction.TransactionDetails.Count() != transaction.TransactionDetails.Count() ||
                            referenceTransaction.TransactionDetails.Sum(a => a.Quantity) != transaction.TransactionDetails.Sum(a => a.Quantity))
                        {
                            status = Services.Contracts.Enums.DocumentStatuses.PartialReturn;
                        }
                    }

                    if (!UpdateTransactionHead(invoiceID.Value, Eduegate.Framework.Enums.TransactionStatus.Complete, status))
                    {
                        throw new Exception("Failed for transaction : " + transaction.HeadIID + ", not able to update the invoice status.");
                    }
                }

                if (SaveInvetoryTransactions(transaction))
                {
                    //3. Update the status of the existing transaction head
                    if (!UpdateTransactionHead(transaction, TransactionStatus.Complete))
                    {
                        throw new Exception("Failed for transaction : " + transaction.HeadIID + ", not able to update the transaction status.");
                    }
                }
                else
                {
                    throw new Exception("Failed for transaction : " + transaction.HeadIID + ", not able to create transaction log.");
                }
            }
            else
            {
                throw new Exception("Failed for transaction : " + transaction.HeadIID + ", not able to update the invoice  status.");
            }
        }

        public bool ProcessCancellation(ViewModels.TransactionHeadViewModel transaction)
        {
            return new Eduegate.TransactionEgine.ClientFactory.ClientFactory().CancelTransaction(transaction.HeadIID);
        }

        #region Private Methods
        private bool UpdateProductInventory(TransactionHeadViewModel transactions)
        {
            WriteLog("Updating product inventory :" + transactions.HeadIID.ToString() + "-(" + transactions.TransactionDetails.Count.ToString() + ")");
            bool isSuccess = false;
            var listDTO = new List<ProductInventoryDTO>();

            foreach (var transaction in transactions.TransactionDetails)
            {
                var productInventoryDTO = new Services.Contracts.ProductInventoryDTO();
                productInventoryDTO.ProductSKUMapID = (long)transaction.ProductSKUMapID;
                productInventoryDTO.Quantity = (transaction.Quantity);
                productInventoryDTO.ReferenceTypes = ReferenceTypes;
                productInventoryDTO.CompanyID = transactions.CompanyID;
                productInventoryDTO.BranchID = transactions.BranchID;
                productInventoryDTO.Batch = transaction.BatchID;
                productInventoryDTO.SerialKeys = new List<string>();
                productInventoryDTO.isActive = 0; // temporary update
                productInventoryDTO.HeadID = transaction.HeadID; // temporary update
                if (!string.IsNullOrEmpty(transaction.SerialNumber))
                    productInventoryDTO.SerialKeys.Add(transaction.SerialNumber);

                if (transaction.SerialKeyMaps != null)
                {
                    foreach (var serialMap in transaction.SerialKeyMaps)
                    {
                        productInventoryDTO.SerialKeys.Add(serialMap.SerialNo);
                    }
                }

                // add in list object
                listDTO.Add(productInventoryDTO);
            }

            var result = new Eduegate.TransactionEgine.ClientFactory.ClientFactory(null).ProcessProductInventory(listDTO);

            if (listDTO.IsNotNull() && listDTO.Count > 0)
            {
                isSuccess = true;
            }

            return isSuccess;
        }

        private bool SaveInvetoryTransactions(TransactionHeadViewModel transaction)
        {
            WriteLog("Save inventory transactions:" + transaction.HeadIID.ToString() + "-(" + transaction.TransactionDetails.Count.ToString() + ")");
            var listDTO = new List<InvetoryTransactionDTO>();

            foreach (var itm in transaction.TransactionDetails)
            {
                var invetoryTransactionDTO = new InvetoryTransactionDTO();
                invetoryTransactionDTO.CompanyID = transaction.CompanyID;
                invetoryTransactionDTO.HeadID = transaction.HeadIID;
                invetoryTransactionDTO.DocumentTypeID = transaction.DocumentTypeID;
                invetoryTransactionDTO.TransactionNo = transaction.TransactionNo;
                invetoryTransactionDTO.TransactionDate = transaction.TransactionDate.Value.ToLongDateString();
                invetoryTransactionDTO.Description = transaction.Description;

                // in DB FK_InvetoryTransactions_ProductSKUMaps mapping between  InvetoryTransaction.ProductID & TransactionDetail.ProductSKUMapID 
                invetoryTransactionDTO.ProductSKUMapID = itm.ProductSKUMapID;
                invetoryTransactionDTO.UnitID = itm.UnitID;
                invetoryTransactionDTO.Cost = itm.UnitPrice;
                invetoryTransactionDTO.Quantity = itm.Quantity;
                invetoryTransactionDTO.Amount = itm.Amount;
                invetoryTransactionDTO.ExchangeRate = itm.ExchangeRate;
                // add the invetoryTransactionDTO in list
                listDTO.Add(invetoryTransactionDTO);
            }

            // call the service
            return new Eduegate.TransactionEgine.ClientFactory.ClientFactory(null).SaveInvetoryTransactions(listDTO);
        }

        private bool UpdateTransactionHead(TransactionHeadViewModel transaction, TransactionStatus transactionStatus)
        {
            WriteLog("Update transaction status:" + transaction.HeadIID.ToString() + "-(" + transactionStatus.ToString() + ")");
            var dto = new TransactionHeadDTO()
            {
                HeadIID = transaction.HeadIID,
                TransactionStatusID = (byte)transactionStatus
            };
            // call the service
            return new Eduegate.TransactionEgine.ClientFactory.ClientFactory(null).UpdateTransactionHead(dto);
        }

        #endregion
    }
}

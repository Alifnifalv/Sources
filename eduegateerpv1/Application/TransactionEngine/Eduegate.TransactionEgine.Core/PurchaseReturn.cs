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

namespace Eduegate.TransactionEngineCore
{
    public class PurchaseReturn : TransactionBase, ITransactions
    {

        public PurchaseReturn(Action<string> logError)
        {
            _logError = logError;
        }


        public DocumentReferenceTypes ReferenceTypes
        {
            get { return DocumentReferenceTypes.PurchaseReturn; }
        }

        public void Process(ViewModels.TransactionHeadViewModel transaction)
        {
            WriteLog("Purchase return-process started for TransactionID:" + transaction.HeadIID.ToString());

            try
            {
                WriteLog("Processing purchase return :" + transaction.HeadIID.ToString());

                switch ((Eduegate.Framework.Enums.TransactionStatus)transaction.TransactionStatusID)
                {
                    case Eduegate.Framework.Enums.TransactionStatus.New:
                    case Eduegate.Framework.Enums.TransactionStatus.Edit:
                        WriteLog("Processing new purchase return.");
                        ProcessNew(transaction);

                        break;
                    case Eduegate.Framework.Enums.TransactionStatus.IntitiateReprocess:
                        switch ((Eduegate.Services.Contracts.Enums.DocumentStatuses)transaction.DocumentStatusID)
                        {
                            case Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled:
                                WriteLog("Cancelling purchase return.");
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
                WriteLog("Exception occured in purchase return-Process.:" + ex.Message, ex);
                TransactionProcessingFailed(transaction, ex.Message);
            }
        }

        public void ProcessNew(TransactionHeadViewModel transaction)
        {
            bool isSuccess = false;

            //LogHelper<Transaction>.Info("Calling SalesOrder-UpdateProductInventory");
            isSuccess = UpdateProductInventory(transaction);
            //LogHelper<Transaction>.Info("Inventory updated?: " + isSuccess.ToString());

            //2. Save into inventory transactions
            if (isSuccess)
            {
                isSuccess = SaveInvetoryTransactions(transaction);
            }
            else
            {
                throw new Exception("Failed for transaction : " + transaction.HeadIID + ", not able to update the product inventory.");
            }


            //3. Update the status of the existing transaction head
            if (isSuccess)
            {
                UpdateTransactionHead(transaction, TransactionStatus.Complete);
            }
            else
            {
                throw new Exception("Failed to update transaction : " + transaction.HeadIID + ", not able to update the product inventory.");
            }
        }

        public bool ProcessCancellation(ViewModels.TransactionHeadViewModel transaction)
        {
            return new Eduegate.TransactionEgine.ClientFactory.ClientFactory().CancelTransaction(transaction.HeadIID);
        }

        #region Private Methods
        private bool UpdateProductInventory(TransactionHeadViewModel transactions)
        {
            bool isSuccess = false;
            List<ProductInventoryDTO> listDTO = new List<ProductInventoryDTO>();

            foreach (var transaction in transactions.TransactionDetails)
            {
                ProductInventoryDTO productInventoryDTO = new Services.Contracts.ProductInventoryDTO();
                productInventoryDTO.ProductSKUMapID = (long)transaction.ProductSKUMapID;
                productInventoryDTO.Quantity = (transaction.Quantity);
                productInventoryDTO.ReferenceTypes = ReferenceTypes;
                productInventoryDTO.CompanyID = transactions.CompanyID;
                productInventoryDTO.BranchID = transactions.BranchID;
                productInventoryDTO.Batch = transaction.BatchID;

                // add in list object
                listDTO.Add(productInventoryDTO);
            }

            var result = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().ProcessProductInventory(listDTO);

            if (listDTO.IsNotNull() && listDTO.Count > 0)
            {
                isSuccess = true;
            }
            return isSuccess;
        }

        private bool SaveInvetoryTransactions(TransactionHeadViewModel transaction)
        {
            //bool isSuccess = false;
            var listDTO = new List<InvetoryTransactionDTO>();

            foreach (var itm in transaction.TransactionDetails)
            {
                var invetoryTransactionDTO = new InvetoryTransactionDTO()
                    {
                        HeadID = transaction.HeadIID,
                        DocumentTypeID = transaction.DocumentTypeID,
                        TransactionNo = transaction.TransactionNo,
                        TransactionDate = transaction.TransactionDate.Value.ToLongDateString(),
                        Description = transaction.Description,
                        CompanyID = transaction.CompanyID,
                        ProductSKUMapID = itm.ProductSKUMapID,
                        UnitID = itm.UnitID,
                        Cost = itm.UnitPrice,
                        Quantity = itm.Quantity,
                        Amount = itm.Amount,
                        ExchangeRate = itm.ExchangeRate,
                        BranchID = transaction.BranchID,
                        CurrencyID = transaction.CurrencyID,
                        Rate= itm.UnitPrice,
                };

                listDTO.Add(invetoryTransactionDTO);
            }

            // call the service
            //string uri = string.Concat(TransactionService + "SaveInvetoryTransactions");
            //string serviceResult = ServiceHelper.HttpPostRequest(uri, listDTO);
            //if (serviceResult == "true")
            //{
            //    isSuccess = true;
            //}

            //return isSuccess;

            return new Eduegate.TransactionEgine.ClientFactory.ClientFactory().SaveInvetoryTransactions(listDTO);
        }

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

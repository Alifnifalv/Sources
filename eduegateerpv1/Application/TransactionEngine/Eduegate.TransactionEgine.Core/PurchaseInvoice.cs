using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Settings;
using Eduegate.TransactionEngineCore.Interfaces;
using Eduegate.TransactionEngineCore.ViewModels;

namespace Eduegate.TransactionEngineCore
{
    public class PurchaseInvoice : TransactionBase, ITransactions
    {
        public PurchaseInvoice(Action<string> logError)
        {
            _logError = logError;
        }

        public Framework.Enums.DocumentReferenceTypes ReferenceTypes
        {
            get { return DocumentReferenceTypes.PurchaseInvoice; }
        }

        public void Process(TransactionHeadViewModel transaction)
        {
            #region Actual Code
            WriteLog("Purchase invoice-Process started for TransactionID:" + transaction.HeadIID.ToString());

            try
            {
                WriteLog("Processing Sales invoice :" + transaction.HeadIID.ToString());

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
                WriteLog("Exception occured in purchase invocie-Process.:" + ex.Message, ex);
                TransactionProcessingFailed(transaction, ex.Message);
            }
            #endregion
        }


        public void ProcessNew(TransactionHeadViewModel head)
        {
            try
            {
                bool isSuccess = false;
                WriteLog("Processing purchase invoice :" + head.HeadIID.ToString());      

                // If Transaction is Edited, revert the last one and process current one
                if (head.TransactionStatusID == byte.Parse(((int)(TransactionStatus.Edit)).ToString()))
                {
                    isSuccess = RevertSalesTransaction(head.HeadIID);
                }

                //1. Update Product Inventory
                int referenceTypeID=0;
                if (head.ReferenceHeadID.HasValue && head.ReferenceHeadID.Value != 0)
                {
                    referenceTypeID = new Eduegate.TransactionEgine.ClientFactory.ClientFactory(null)
                        .GetDocumentTypesByHeadId(head.ReferenceHeadID.Value).ReferenceTypeID.Value;
                    
                }
                if (referenceTypeID == 0 || referenceTypeID != 15 && referenceTypeID != 20)//if its not GRN against purchase invoice & its not SEN
                    isSuccess = UpdateProductInventory(head, ReferenceTypes);
                else
                    isSuccess = true;

                if (isSuccess)
                {
                    //2. Save into inventory transactions0
                    isSuccess = SaveInvetoryTransactions(head);
                    UpdateTransactionHead(head, TransactionStatus.Complete, Services.Contracts.Enums.DocumentStatuses.Completed);
                }
                else
                    throw new Exception("Failed for transaction : " + head.HeadIID + ", not able to update the product inventory.");

            }
            catch (Exception ex)
            {
                UpdateTransactionHead(head, TransactionStatus.Failed,Services.Contracts.Enums.DocumentStatuses.Draft);
                WriteLog("Exception occured in purchase invoice-Process.:" + ex.Message, ex);
            }
        }

        public bool ProcessCancellation(ViewModels.TransactionHeadViewModel transaction)
        {
            return new Eduegate.TransactionEgine.ClientFactory.ClientFactory().CancelTransaction(transaction.HeadIID);
        }

       
        public bool SaveInvetoryTransactions(TransactionHeadViewModel transactions)
        {
            var listDTO = new List<InvetoryTransactionDTO>();

            foreach (var itm in transactions.TransactionDetails)
            {
                listDTO.Add(new InvetoryTransactionDTO() {
                    CompanyID = transactions.CompanyID,
                    BranchID = transactions.BranchID,
                    HeadID = transactions.HeadIID,
                    DocumentTypeID = transactions.DocumentTypeID,
                    TransactionNo = transactions.TransactionNo,
                    TransactionDate = transactions.TransactionDate.Value.ToLongDateString(),
                    Description = transactions.Description,
                    ProductSKUMapID = itm.ProductSKUMapID,
                    UnitID = itm.UnitID,
                    Cost = itm.Amount,
                    Quantity = itm.Quantity,
                    Amount = itm.Amount,
                    ExchangeRate = itm.ExchangeRate,
                    LandingCost=itm.LastCostPrice,
                    LastCostPrice=itm.LastCostPrice,
                    Fraction=itm.Fraction  ,
                    OriginalQty= itm.Quantity ,
                    Rate=itm.UnitPrice

                });
            }
       
            return new Eduegate.TransactionEgine.ClientFactory.ClientFactory().SaveInvetoryTransactions(listDTO);
        }

        /// <summary>
        /// Revert Sales Transaction = 
        /// 1. Add to inventory again(using purchase updatetransaction for this)
        /// 2. Update inventory transactions for this headid as IsProcess = true
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns>bool</returns>
        private bool RevertSalesTransaction(long transactionHeadID)
        {
            var vm = new TransactionHeadViewModel();
            vm.TransactionDetails = new List<TransactionDetailViewModel>();

            //LogHelper<SalesOrder>.Info("Calling SalesOrder-RevertSalesTransaction for HeadID:" + transactionHeadID.ToString());
            bool isSuccess = false;

            // Get quantity of last inventory transaction for this transactionheadID
            string uri = string.Concat(TransactionService + "GetInventoryTransactions?transactionHeadID=" + transactionHeadID);
            var inventoryTransactions = ServiceHelper.HttpGetRequest<List<InvetoryTransactionDTO>>(uri);

            // Prepare new transaction object with quantities from inventory transaction
            foreach (var trans in inventoryTransactions)
            {
                //var transDetail = cloneTrans.TransactionDetails.Where(td => td.ProductSKUMapID == trans.ProductID && td.HeadID == trans.HeadID).Single();
                vm.TransactionDetails.Add(new TransactionDetailViewModel()
                {
                    Quantity = trans.Quantity,
                    HeadID = trans.HeadID,
                    Amount = trans.Amount,
                    UnitID = trans.UnitID,
                    ProductSKUMapID = trans.ProductSKUMapID,
                });
            }

            // Update product inventory with last transaction's quantity (Add stock again)
            isSuccess = new SalesInvoice(_logError).UpdateProductInventory(vm, DocumentReferenceTypes.SalesInvoice);

            // Update inventory transaction
            if (isSuccess)
                isSuccess = ReprocessInventoryTransaction(transactionHeadID);

            return isSuccess;
        }

        private bool ReprocessInventoryTransaction(long transactionHeadID)
        {
            bool isSuccess = false;
            try
            {
                //LogHelper<SalesOrder>.Info("Calling SalesOrder-UpdateInventoryTransaction for HeadID:" + transactionHeadID.ToString());
                string uri = string.Concat(TransactionService + "ReprocessInventoryTransaction?transactionHeadID=" + transactionHeadID);
                isSuccess = Convert.ToBoolean(ServiceHelper.HttpGetRequest(uri));
                //LogHelper<SalesOrder>.Info("InventoryTransaction updated? :" + isSuccess.ToString());
            }
            catch (Exception)
            {
                //LogHelper<SalesOrder>.Fatal("Exception in SalesOrder-UpdateInventorytransaction." + ex.Message, ex);
                isSuccess = false;
            }
            return isSuccess;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.TransactionEngineCore.Interfaces;
using Eduegate.TransactionEngineCore.ViewModels;

namespace Eduegate.TransactionEngineCore
{
    public class BranchTransfer : TransactionBase, ITransactions
    {
        public BranchTransfer(Action<string> logError)
        {
            _logError = logError;
        }

        public Framework.Enums.DocumentReferenceTypes ReferenceTypes
        {
            get { return Framework.Enums.DocumentReferenceTypes.BranchTransfer; }
        }

        public void Process(ViewModels.TransactionHeadViewModel transaction)
        {
            WriteLog("Branch transfer-Process started for TransactionID:" + transaction.HeadIID.ToString());

            try
            {
                WriteLog("Processing branch transfer :" + transaction.HeadIID.ToString());

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
                WriteLog("Exception occured in branch transfer-Process.:" + ex.Message, ex);
                TransactionProcessingFailed(transaction, ex.Message);
            }
        }

        public bool ProcessCancellation(ViewModels.TransactionHeadViewModel transaction)
        {
            return new Eduegate.TransactionEgine.ClientFactory.ClientFactory().CancelTransaction(transaction.HeadIID);
        }

        public void ProcessNew(ViewModels.TransactionHeadViewModel transaction)
        {
            WriteLog("BranchTransfer-Process started for TransactionID:" + transaction.HeadIID.ToString());
            bool isSuccess = false;

            try
            {
                // check if order is digital or physical
                var transactionDetail = transaction.TransactionDetails;

                // If Transaction is Edited, revert the last one and process current one
                if (transaction.TransactionStatusID == byte.Parse(((int)(TransactionStatus.Edit)).ToString()))
                {
                    isSuccess = RevertBranchTransaction(transaction.HeadIID);
                    WriteLog("Transfer reverted?: " + isSuccess.ToString());
                }

                //0. Check Quantity
                WriteLog("Checking availability");
                isSuccess = CheckAvailibility(transaction);
                WriteLog("Is Available? :" + isSuccess.ToString());

                //1. Update Product Inventory
                if (isSuccess)
                {
                    WriteLog("Calling BranchTransfer-TranferProductInventory");
                    isSuccess = TransferInventory(transaction);
                    WriteLog("Inventory transferred?: " + isSuccess.ToString());
                }
                else
                {
                    isSuccess = UpdateTransactionHead(transaction, TransactionStatus.Failed,Services.Contracts.Enums.DocumentStatuses.Draft);

                    if (isSuccess)
                    {
                        WriteLog("Method UpdateTransactionHead(with Failed status) is Success for HeadId= " + transaction.HeadIID);
                        return;
                    }
                    else
                    {
                        WriteLog("Method UpdateTransactionHead(with Failed status) is Failed for HeadId= " + transaction.HeadIID);
                        return;
                    }
                }

                //2. Save into inventory transactions
                if (isSuccess)
                {
                    WriteLog("Method UpdateProductInventory is Success for HeadId= " + transaction.HeadIID);
                    WriteLog("Method SaveInvetoryTransactions is Calling for HeadId= " + transaction.HeadIID);

                    isSuccess = SaveInvetoryTransactions(transaction);
                }
                else
                {
                    WriteLog("Method SaveInvetoryTransactions is Failed for HeadId= " + transaction.HeadIID);
                    return;
                }

                //3. Update the status of the existing transaction head
                if (isSuccess)
                {
                    WriteLog("Method SaveInvetoryTransactions is Success for HeadId= " + transaction.HeadIID);
                    WriteLog("Method UpdateTransactionHead is Calling for HeadId= " + transaction.HeadIID);

                    UpdateTransactionHead(transaction, TransactionStatus.Complete,Services.Contracts.Enums.DocumentStatuses.Completed);
                }
                else
                {
                    WriteLog("Method UpdateTransactionHead is Failed for HeadId= " + transaction.HeadIID);
                    return;
                }

            }
            catch (Exception ex)
            {
                UpdateTransactionHead(transaction, Eduegate.Framework.Enums.TransactionStatus.Failed,Services.Contracts.Enums.DocumentStatuses.Draft);
                WriteLog(ex.Message.ToString(), ex);
                WriteLog("Exception occured in SalesInvoice-Process.:" + ex.Message, ex);
            }
        }

        public bool TransferInventory(TransactionHeadViewModel transactions)
        {
            try
            {
                UpdateProductInventory(transactions, DocumentReferenceTypes.BranchTransfer, transactions.ToBranchID.Value);
                return true;
            }
            catch
            {
                return false;
            }
        }
       
        private bool RevertBranchTransaction(long transactionHeadID)
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

        public bool SaveInvetoryTransactions(TransactionHeadViewModel transaction)
        {
            var listDTO = new List<InvetoryTransactionDTO>();
            int serNo = 1;
            foreach (var itm in transaction.TransactionDetails)
            {
                var invetoryTransactionDTO = new InvetoryTransactionDTO()
                {
                    HeadID = transaction.HeadIID,
                    DocumentTypeID = transaction.DocumentTypeID,
                    TransactionNo = transaction.TransactionNo,
                    TransactionDate = transaction.TransactionDate.Value.ToLongDateString(),
                    Description = transaction.Description,
                    ProductSKUMapID = itm.ProductSKUMapID,
                    UnitID = itm.UnitID,
                    Cost = -1 * itm.UnitPrice,
                    Quantity = -1 * itm.Quantity,
                    Amount = -1 * itm.Amount,
                    ExchangeRate = itm.ExchangeRate,
                    BatchID = itm.BatchID,
                    BranchID = transaction.BranchID,
                    SerialNo = serNo,
                    CompanyID = transaction.CompanyID,
                };

                // add the invetoryTransactionDTO in list
                listDTO.Add(invetoryTransactionDTO);

                var invetoryTransactionDTO2 = new InvetoryTransactionDTO()
                {
                    HeadID = transaction.HeadIID,
                    DocumentTypeID = transaction.DocumentTypeID,
                    TransactionNo = transaction.TransactionNo,
                    TransactionDate = transaction.TransactionDate.Value.ToLongDateString(),
                    Description = transaction.Description,
                    ProductSKUMapID = itm.ProductSKUMapID,
                    UnitID = itm.UnitID,
                    Cost = itm.UnitPrice,
                    Quantity = itm.Quantity,
                    Amount = itm.Amount,
                    ExchangeRate = itm.ExchangeRate,
                    BatchID = itm.BatchID,
                    BranchID = transaction.ToBranchID,
                    SerialNo = serNo,
                    CompanyID = transaction.CompanyID,
                };

                // add the invetoryTransactionDTO in list
                listDTO.Add(invetoryTransactionDTO2);
                serNo++;
            }

            // call the service
            return new Eduegate.TransactionEgine.ClientFactory.ClientFactory().SaveInvetoryTransactions(listDTO);
        }       
    }
}

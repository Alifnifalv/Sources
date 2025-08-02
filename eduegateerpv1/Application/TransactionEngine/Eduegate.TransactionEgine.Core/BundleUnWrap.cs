using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.TransactionEngineCore.Interfaces;
using Eduegate.TransactionEngineCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.TransactionEngineCore
{
    public class BundleUnWrap : TransactionBase, ITransactions
    {
        public BundleUnWrap(Action<string> logError)
        {
            _logError = logError;
        }

        public Framework.Enums.DocumentReferenceTypes ReferenceTypes
        {
            get { return DocumentReferenceTypes.BundleUnWrap; }
        }

        public Framework.Enums.DocumentReferenceTypes BundleItemReferenceTypes
        {
            get { return DocumentReferenceTypes.BundleWrap; }
        }

        public void Process(TransactionHeadViewModel transaction)
        {
            WriteLog("BundleUnWrap-Process started for TransactionID:" + transaction.HeadIID.ToString());

            try
            {
                WriteLog("Processing Bundle Un Wrap :" + transaction.HeadIID.ToString());

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
                TransactionProcessingFailed(transaction, ex.Message);
                WriteLog(ex.Message.ToString(), ex);
                WriteLog("Exception occured in BundleUnWrap-Process.:" + ex.Message, ex);
            }
        }

        private void ProcessNew(TransactionHeadViewModel transaction)
        {
            bool isSuccess = true;

            if (transaction.ReferenceHeadID.IsNotNull())
            {
                if (!IsProcessTransaction(transaction))
                {
                    // update transaction to Failed and Draft
                    UpdateTransactionHead(transaction, TransactionStatus.Failed, Services.Contracts.Enums.DocumentStatuses.Draft);
                }
            }

            // check if order is digital or physical
            var transactionDetail = transaction.TransactionDetails;
            var hasDigitalKey = new Eduegate.TransactionEgine.ClientFactory.ClientFactory(null).HasDigitalProduct(transaction.HeadIID);

            // If digital order && user is blocked, move transaction to failed and return
            if (hasDigitalKey)
            {
                // Get customer detail
                var customerDetail = new Eduegate.TransactionEgine.ClientFactory.ClientFactory(null).GetCustomerDetails(Convert.ToInt64(transaction.CustomerID), false);

                if (customerDetail.Customer.Settings.IsBlocked == true)
                {
                    UpdateTransactionHead(transaction, Eduegate.Framework.Enums.TransactionStatus.Failed, Services.Contracts.Enums.DocumentStatuses.Draft);
                    return;
                }
            }

            //bool isSuccessQty = true;
            // If Transaction is Edited, revert the last one and process current one
            if (transaction.TransactionStatusID == byte.Parse(((int)(TransactionStatus.Edit)).ToString()))
            {
                isSuccess = RevertSalesTransaction(transaction.HeadIID);
                WriteLog("Inventory reverted?: " + isSuccess.ToString());
            }

            //foreach (var trans in transaction.TransactionDetails)
            //{
            //    var bundleItems = new Eduegate.TransactionEgine.ClientFactory.ClientFactory(null)
            //        .GetProductBundleItemDetail(trans.ProductSKUMapID.Value);
            //    //0. Check Quantity
            //    WriteLog("Checking availability");
            //    isSuccess = CheckAvailibilityItems(transaction.BranchID.Value, trans.Quantity.Value, bundleItems);

            //    if (!isSuccess)
            //    {
            //        isSuccessQty = false;
            //        break ;
            //    }

            //    WriteLog("Is Available? :" + isSuccess.ToString());
            //}

            //if (!isSuccessQty)

            //    isSuccess = false;
            //else
            //    isSuccess = true;

            //1. Update Product Inventory
            if (isSuccess)
            {
                WriteLog("Calling BundleUnWrap-UpdateProductInventory");
                foreach (var trans in transaction.TransactionDetails)
                {
                    var bundleItems = new Eduegate.TransactionEgine.ClientFactory.ClientFactory(null)
                                .GetProductBundleItemDetail(trans.ProductSKUMapID.Value);
                    isSuccess = UpdateBundleProductInventory(transaction, trans.Quantity.Value, bundleItems, BundleItemReferenceTypes);
                    if (isSuccess)
                        isSuccess = SaveBundleInventoryTransactions(transaction, trans.Quantity.Value, bundleItems);
                }

                isSuccess = UpdateProductInventory(transaction, ReferenceTypes);
                WriteLog("Inventory updated?: " + isSuccess.ToString());
            }
            else
            {
                isSuccess = UpdateTransactionHead(transaction, TransactionStatus.Failed, Services.Contracts.Enums.DocumentStatuses.Draft);

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

                UpdateTransactionHead(transaction, TransactionStatus.Complete, Services.Contracts.Enums.DocumentStatuses.Completed);

                if (hasDigitalKey)
                {
                    var emailNotificationTypeDetail = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().GetEmailNotificationType(Services.Contracts.Enums.EmailNotificationTypes.OrderDelivery);

                    // if customer not blocked process else 
                    var result = AddOrderNotification(transaction.HeadIID, Convert.ToInt64(transaction.CustomerID), emailNotificationTypeDetail, transaction.TransactionNo);
                }
            }
            else
            {
                WriteLog("Method UpdateTransactionHead is Failed for HeadId= " + transaction.HeadIID);
                return;
            }
        }

        public bool ProcessCancellation(ViewModels.TransactionHeadViewModel transaction)
        {
            return new Eduegate.TransactionEgine.ClientFactory.ClientFactory().CancelTransaction(transaction.HeadIID);
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

            WriteLog("Calling BundleUnWrap-RevertSalesTransaction for HeadID:" + transactionHeadID.ToString());
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
            isSuccess = new PurchaseInvoice(_logError).UpdateProductInventory(vm, DocumentReferenceTypes.BundleUnWrap);
            return isSuccess;
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
                    Cost = itm.UnitPrice,
                    Quantity = itm.Quantity,
                    Amount = itm.Amount,
                    ExchangeRate = itm.ExchangeRate,
                    BatchID = itm.BatchID,
                    BranchID = transaction.BranchID,
                    SerialNo = serNo,
                    CompanyID = transaction.CompanyID,
                };

                // add the invetoryTransactionDTO in list
                listDTO.Add(invetoryTransactionDTO);
                serNo++;
            }

            // call the service
            return new Eduegate.TransactionEgine.ClientFactory.ClientFactory().SaveInvetoryTransactions(listDTO);
        }

        public bool SaveBundleInventoryTransactions(TransactionHeadViewModel transaction, decimal qty, List<ProductBundleDTO> transactiondtl)
        {
            var listDTO = new List<InvetoryTransactionDTO>();
            int serNo = 1;

            foreach (var itm in transactiondtl)
            {
                var invetoryTransactionDTO = new InvetoryTransactionDTO()
                {
                    HeadID = transaction.HeadIID,
                    DocumentTypeID = transaction.DocumentTypeID,
                    TransactionNo = transaction.TransactionNo,
                    TransactionDate = transaction.TransactionDate.Value.ToLongDateString(),
                    Description = transaction.Description,
                    ProductSKUMapID = itm.FromProductSKUMapID,
                    Cost = itm.CostPrice,
                    Quantity = qty * itm.Quantity,
                    Amount = itm.CostPrice * (qty * itm.Quantity),
                    //ExchangeRate = itm.ExchangeRate,
                    //BatchID = itm.BatchID,
                    BranchID = transaction.BranchID,
                    SerialNo = serNo,
                    CompanyID = transaction.CompanyID,
                };

                // add the invetoryTransactionDTO in list
                listDTO.Add(invetoryTransactionDTO);
                serNo++;
            }

            // call the service
            return new Eduegate.TransactionEgine.ClientFactory.ClientFactory().SaveInvetoryTransactions(listDTO);
        }

        public string AddOrderNotification(long orderID, long customerID, EmailNotificationTypeDTO emailNotificationTypeDetail, string transactionNumber)
        {

            // Create notification object
            var notificationDTO = new EmailNotificationDTO();

            // Get customer emailID from customerID
            var customerEmailID = new Eduegate.TransactionEgine.ClientFactory.ClientFactory(null).GetCustomerDetails(customerID, false).LoginEmailID;
            notificationDTO.ToEmailID = customerEmailID;


            notificationDTO.EmailNotificationType = Eduegate.Services.Contracts.Enums.EmailNotificationTypes.OrderDelivery; //TODO: this should be based on payment status
            notificationDTO.AdditionalParameters = new List<Eduegate.Services.Contracts.Commons.KeyValueParameterDTO>();
            notificationDTO.FromEmailID = new Domain.Setting.SettingBL().GetSettingValue<string>("EmailFrom").ToString();
            notificationDTO.Subject = emailNotificationTypeDetail.IsNotNull() ? string.Concat(emailNotificationTypeDetail.EmailSubject, " | ", transactionNumber, " ") : string.Empty;


            // Add order detail
            notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.OrderConfirmation.Keys.OrderID, ParameterValue = orderID.ToString() });

            //notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO()
            //{
            //    ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.Subject,
            //    ParameterValue = emailNotificationTypeDetail.IsNotNull() ? string.Concat(emailNotificationTypeDetail.EmailSubject, " | ", transactionNumber, " ") : string.Empty,
            //});

            notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.OrderConfirmation.Keys.OrderHistoryURL, ParameterValue = "" });

            // add notification
            var notificationReponse = new Eduegate.TransactionEgine.ClientFactory.ClientFactory(null).SaveEmailData(notificationDTO);
            //var notificationReponse = Task<EmailNotificationDTO>.Factory.StartNew(() => new NotificationServiceClient(null).SaveEmailData(notificationDTO));
            return notificationReponse.ToString();
        }

        private bool IsProcessTransaction(TransactionHeadViewModel transaction)
        {
            bool exit = true;

            // Don't process if SI exists for the SO(referenceHeadID)
            var transDetail = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().GetTransaction(Convert.ToInt64(transaction.ReferenceHeadID), true, false);
            if (transDetail.IsNotNull())
            {
                if (transDetail.TransactionDetails.Count > 0)
                {
                    // Don't process if quantities do not match for SO and SI
                    if (transaction.TransactionDetails.Any(t => t.Quantity != (transDetail.TransactionDetails.Where(ch => ch.ProductSKUMapID == t.ProductSKUMapID).FirstOrDefault().Quantity)))
                    {
                        exit = false;
                    }
                }
                else
                {
                    // Don't process if no items available in SO
                    exit = false;
                }
            }

            // Don't process if transactionstatus !comlete/!confirmed
            if ((transDetail.TransactionHead.TransactionStatusID != (int)Services.Contracts.Enums.TransactionStatus.Complete || transDetail.TransactionHead.TransactionStatusID != (int)Services.Contracts.Enums.TransactionStatus.Confirmed))
            {
                exit = false;
            }

            return exit;
        }
    }
}

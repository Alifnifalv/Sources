using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper;
using Eduegate.Framework.Services;
using Eduegate.Platforms.SubscriptionManager;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Enums.Logging;
using Eduegate.TransactionEngineCore.ViewModels;

namespace Eduegate.TransactionEngineCore
{
    public class TransactionBase
    {
        protected string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        protected string TransactionService { get { return ServiceHost + Constants.TRANSACTION_SERVICE_NAME; } }
        protected string SettingService { get { return ServiceHost + Constants.SETTING_SERVICE_NAME; } }
        protected Action<string> _logError;

        public bool UpdateTransactionHead(TransactionHeadViewModel transactions, Eduegate.Framework.Enums.TransactionStatus transactionStatus, DocumentStatuses documentStatus)
        {
            // call the service
            return new Eduegate.TransactionEgine.ClientFactory.ClientFactory(null).UpdateTransactionHead(
                new TransactionHeadDTO()
                {
                    HeadIID = transactions.HeadIID,
                    TransactionStatusID = (byte)transactionStatus,
                    DocumentStatusID = (short)documentStatus
                });
        }

        public bool UpdateTransactionHead(long headID, Eduegate.Framework.Enums.TransactionStatus transactionStatus, DocumentStatuses documentStatus)
        {
            // call the service
            return new Eduegate.TransactionEgine.ClientFactory.ClientFactory(null).UpdateTransactionHead(
                new TransactionHeadDTO()
                {
                    HeadIID = headID,
                    TransactionStatusID = (byte)transactionStatus,
                    DocumentStatusID = (short)documentStatus
                });
        }
        public bool CheckAvailibilityItems(long branchID, decimal bundleQuantity, List<ProductBundleDTO> bundleItems)
        {
            bool isSuccess = true;
            foreach (var transaction in bundleItems)
            {
                // Convert into decimal
                var quantity = new Eduegate.TransactionEgine.ClientFactory.ClientFactory(null).CheckAvailibility(branchID, transaction.FromProductSKUMapID.Value);

                if ((transaction.Quantity * bundleQuantity) > quantity)
                {
                    return isSuccess = false;
                }
            }

            return isSuccess;
        }

        public bool CheckAvailibility(TransactionHeadViewModel transactions)
        {
            bool isSuccess = true;
            foreach (var transaction in transactions.TransactionDetails)
            {
                // Convert into decimal
                var quantity = new Eduegate.TransactionEgine.ClientFactory.ClientFactory(null).CheckAvailibility(transactions.BranchID.Value, transaction.ProductSKUMapID.Value);

                if (transaction.Quantity > quantity)
                {
                    return isSuccess = false;
                }
            }

            return isSuccess;
        }

      
        public bool CheckAvailibilityBundleItem(TransactionHeadViewModel transactions)
        {
            bool isSuccess = true;
            foreach (var transaction in transactions.TransactionDetails)
            {
                // Convert into decimal
                var quantity = new Eduegate.TransactionEgine.ClientFactory.ClientFactory(null).CheckAvailibility(transactions.BranchID.Value, transaction.ProductSKUMapID.Value);

                if (transaction.Quantity > quantity)
                {
                    return isSuccess = false;
                }
            }

            return isSuccess;
        }

        public bool CheckAvailibilityBundleItem(long branchID, decimal bundleQuantity, long ProductSKUMapID)
        {
            bool isSuccess = true;
           
           
                // Convert into decimal
                var quantity = new Eduegate.TransactionEgine.ClientFactory.ClientFactory(null).CheckAvailibility(branchID, ProductSKUMapID);

                if (bundleQuantity > quantity)
                {
                    return isSuccess = false;
                }
            
            return isSuccess;
        }

     
        public bool UpdateProductInventory(TransactionHeadViewModel transactions, Eduegate.Framework.Enums.DocumentReferenceTypes referenceType, long? toBranchID = null)
        {
            WriteLog("Updating product inventory :" + transactions.HeadIID.ToString() + "-(" + transactions.TransactionDetails.Count.ToString() + ")");

            try
            {
                var listDTO = new List<ProductInventoryDTO>();

                foreach (var transaction in transactions.TransactionDetails)
                {
                    // add in list object
                    listDTO.Add(new Services.Contracts.ProductInventoryDTO()
                    {
                        CompanyID = transactions.CompanyID,
                        BranchID = transactions.BranchID.HasValue ? transactions.BranchID : 1,
                        ProductSKUMapID = transaction.ProductSKUMapID.Value,
                        Quantity = transaction.Quantity,
                        ReferenceTypes = referenceType,
                        Batch = transaction.BatchID,
                        CostPrice = transaction.LastCostPrice,
                        ToBranchID = toBranchID,
                        TransactioDetailID = transaction.DetailIID,
                        TransactioHeadID = transaction.HeadID.Value,
                        Fraction=transaction.Fraction,
                        HeadID = transaction.HeadID,
                    });
                }

                // call the service
                var updatedInventory = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().ProcessProductInventory(listDTO);
                int count = 0;

                foreach (var inventory in updatedInventory)
                {
                    transactions.TransactionDetails[count].BatchID = inventory.Batch;
                    transactions.TransactionDetails[count].UnitPrice = inventory.CostPrice;
                    count++;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool UpdateBundleProductInventory(TransactionHeadViewModel transactions, decimal quantity, List<ProductBundleDTO> BundleItems, Eduegate.Framework.Enums.DocumentReferenceTypes referenceType, int? mainCount = null, long? toBranchID = null)
        {
            WriteLog("Updating product inventory :" + transactions.HeadIID.ToString() + "-(" + transactions.TransactionDetails.Count.ToString() + ")");

            try
            {
                var listDTO = new List<ProductInventoryDTO>();

                foreach (var transaction in BundleItems)
                {
                    // add in list object
                    listDTO.Add(new Services.Contracts.ProductInventoryDTO()
                    {
                        CompanyID = transactions.CompanyID,
                        BranchID = transactions.BranchID.HasValue ? transactions.BranchID : 1,
                        ProductSKUMapID = transaction.FromProductSKUMapID.Value,
                        Quantity = quantity * transaction.Quantity,
                        ReferenceTypes = referenceType,
                        //Batch = transaction.BatchID,
                        CostPrice = transaction.CostPrice,
                        //ToBranchID = toBranchID,
                        //TransactioDetailID = transaction.DetailIID,
                        TransactioHeadID = transactions.HeadIID,
                    });
                }

                // call the service
                var updatedInventory = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().ProcessProductInventory(listDTO);
                int count = 0;

                foreach (var inventory in updatedInventory)
                {
                    var transactionDetail = transactions.TransactionDetails[mainCount ?? 0];

                    if (transactionDetail.TransactionAllocations.Count <= count)
                    {
                        transactionDetail.TransactionAllocations.Add(new TransactionAllocationViewModel());
                    }

                    transactionDetail.TransactionAllocations[count].BatchID = inventory.Batch;
                    transactionDetail.TransactionAllocations[count].UnitPrice = inventory.CostPrice;

                    count++;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        protected void WriteLog(string message, Exception ex = null)
        {
            if (_logError != null)
                _logError(message);
        }

        public static void TransactionProcessingFailed(TransactionHeadViewModel transactionHead, string errorMessage)
        {
            var activities = new List<Services.Contracts.Logging.ActivityDTO>();
            activities.Add(new Services.Contracts.Logging.ActivityDTO()
            {
                ReferenceID = transactionHead.HeadIID.ToString(),
                Description = transactionHead.TransactionNo + " processing failed," + errorMessage,
                ActionStatusID = (int)ActionStatuses.Failed,
                ActionTypeID = (int)ActivityTypes.Transactions,
                ActivityTypeID = (int)ActivityTypes.Transactions,
                //CreatedBy = context.LoginID.Value,
                CreatedDate = DateTime.Now
            });

            Task.Factory.StartNew(() => new Eduegate.TransactionEgine.ClientFactory.ClientFactory().SaveActivitiesAsynch(activities));
            SubscriptionHandler.Notify(new SubscriptionDetail()
            {
                SubScriptionType = SubscriptionTypes.NewActivity,
                Data = activities
            });
        }

        public static void TransactionProcessingCompleted(TransactionHeadViewModel transactionHead, string errorMessage)
        {
            var activities = new List<Services.Contracts.Logging.ActivityDTO>();
            activities.Add(new Services.Contracts.Logging.ActivityDTO()
            {
                ReferenceID = transactionHead.HeadIID.ToString(),
                Description = transactionHead.TransactionNo + " processing successfully completed",
                ActionStatusID = (int)ActionStatuses.Success,
                ActionTypeID = (int)ActivityTypes.Transactions,
                ActivityTypeID = (int)ActivityTypes.Transactions,
                //CreatedBy = context.LoginID.Value,
                CreatedDate = DateTime.Now
            });

            Task.Factory.StartNew(() => new Eduegate.TransactionEgine.ClientFactory.ClientFactory().SaveActivitiesAsynch(activities));
            SubscriptionHandler.Notify(new SubscriptionDetail()
            {
                SubScriptionType = SubscriptionTypes.NewActivity,
                Data = activities
            });
        }

        public static bool IsTransactionEnabled(Eduegate.Framework.Enums.DocumentReferenceTypes referenceType)
        {
            var enabledTransaction = new Domain.Setting.SettingBL().GetSettingValue<string>("EnabledTransactions");

            if (!string.IsNullOrEmpty(enabledTransaction))
            {
                if (enabledTransaction.Split(',').Contains(referenceType.ToString()))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return true;
            }
        }
    }
}

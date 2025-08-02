using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Helper;
using Eduegate.Platforms.SubscriptionManager;
using Eduegate.Services.Contracts.Accounts.Assets;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Enums.Logging;
using Eduegate.TransactionEgine.Accounting.ViewModels;

namespace Eduegate.TransactionEngineCore
{
    public class AssetTransactionBase
    {
        protected string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        protected string TransactionService { get { return ServiceHost + Constants.TRANSACTION_SERVICE_NAME; } }
        protected string SettingService { get { return ServiceHost + Constants.SETTING_SERVICE_NAME; } }
        protected Action<string> _logError;

        private CallContext _callContext;
        public AssetTransactionBase(CallContext context = null)
        {
            _callContext = context;
        }

        public bool UpdateAssetTransactionHead(AssetTransactionHeadViewModel transactions, Eduegate.Framework.Enums.TransactionStatus transactionStatus, DocumentStatuses documentStatus)
        {
            // call the service
            return new Eduegate.TransactionEgine.ClientFactory.ClientFactory(null).UpdateAssetTransactionHead(
                new AssetTransactionHeadDTO()
                {
                    HeadIID = transactions.HeadIID,
                    ProcessingStatusID = (byte)transactionStatus,
                    DocumentStatusID = (short)documentStatus
                });
        }

        public bool UpdateAssetTransactionHead(long headID, Eduegate.Framework.Enums.TransactionStatus transactionStatus, DocumentStatuses documentStatus)
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

        public bool CheckAssetAvailability(AssetTransactionHeadViewModel transaction)
        {
            bool isSuccess = true;
            foreach (var detail in transaction.AssetTransactionDetails)
            {
                // Convert into decimal
                var quantity = new Eduegate.TransactionEgine.ClientFactory.ClientFactory(null).CheckAssetAvailability(transaction.BranchID.Value, detail.AssetID.Value);

                if (detail.Quantity > quantity)
                {
                    return isSuccess = false;
                }
            }

            return isSuccess;
        }
      
        public bool CheckAssetAvailabilityBundleItem(AssetTransactionHeadViewModel transactions)
        {
            bool isSuccess = true;
            foreach (var transaction in transactions.AssetTransactionDetails)
            {
                // Convert into decimal
                var quantity = new Eduegate.TransactionEgine.ClientFactory.ClientFactory(null).CheckAssetAvailability(transactions.BranchID.Value, transaction.AssetID.Value);

                if (transaction.Quantity > quantity)
                {
                    return isSuccess = false;
                }
            }

            return isSuccess;
        }

        public bool UpdateAssetInventory(AssetTransactionHeadViewModel transaction, Eduegate.Framework.Enums.DocumentReferenceTypes referenceType, long? toBranchID = null)
        {
            WriteLog("Updating asset inventory :" + transaction.HeadIID.ToString() + "-(" + transaction.AssetTransactionDetails.Count.ToString() + ")");

            try
            {
                var assetInventoryDTOs = new List<AssetInventoryDTO>();
                var assetTransactionSerialMapDTOs = new List<AssetTransactionSerialMapDTO>();

                foreach (var detail in transaction.AssetTransactionDetails)
                {
                    var assetSerialMapIDs = new List<long>();
                    var assetSerialMapDTOs = new List<AssetSerialMapDTO>();

                    detail.AssetTransactionSerialMaps.ForEach(map =>
                    {
                        if (map.AssetSerialMapID > 0)
                        {
                            assetSerialMapIDs.Add(map.AssetSerialMapID);
                        }
                        else
                        {
                            assetSerialMapDTOs.Add(new AssetSerialMapDTO()
                            {
                                AssetSerialMapIID = map.AssetSerialMapID,
                                AssetID = map.AssetID,
                                AssetSequenceCode = map.AssetSequenceCode,
                                SerialNumber = map.SerialNumber,
                                AssetTag = map.AssetTag,
                                CostPrice = map.CostPrice,
                                ExpectedLife = map.ExpectedLife,
                                DepreciationRate = map.DepreciationRate,
                                ExpectedScrapValue = map.ExpectedScrapValue,
                                AccumulatedDepreciationAmount = map.AccumulatedDepreciationAmount,
                                DateOfFirstUse = map.DateOfFirstUse,
                                SupplierID = map.SupplierID,
                                BillNumber = map.BillNumber,
                                BillDate = map.BillDate,
                                IsActive = true,
                            });
                        }
                    });

                    // add in list object
                    assetInventoryDTOs.Add(new AssetInventoryDTO()
                    {
                        AssetInventoryIID = 0,
                        CompanyID = transaction.CompanyID,
                        BranchID = transaction.BranchID.HasValue ? transaction.BranchID.Value : 1,
                        AssetID = detail.AssetID.Value,
                        Quantity = detail.Quantity,
                        DocumentReferenceType = referenceType,
                        Batch = detail.BatchID,
                        CostAmount = detail.CostAmount,
                        ToBranchID = toBranchID,
                        TransactioDetailID = detail.DetailIID,
                        TransactioHeadID = detail.HeadID.Value,
                        HeadID = detail.HeadID,
                        CutOffDate = detail.CutOffDate,
                        UpdatedBy = transaction.UpdatedBy,
                        AssetSerialMapIDs = assetSerialMapIDs,
                        AssetSerialMapDTOs = assetSerialMapDTOs,
                    });
                }

                // call the service
                var updatedInventory = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().ProcessAssetInventory(assetInventoryDTOs);
                int count = 0;

                var fullAssetSerialMapDTOs = new List<AssetSerialMapDTO>();
                foreach (var inventory in updatedInventory)
                {
                    transaction.AssetTransactionDetails[count].BatchID = inventory.Batch;
                    transaction.AssetTransactionDetails[count].CostAmount = inventory.CostAmount;

                    if (referenceType == Eduegate.Framework.Enums.DocumentReferenceTypes.AssetTransferReceipt)
                    {
                        UpdateAssetSerialMapsInventoryID(inventory.AssetSerialMapIDs, inventory);
                    }
                    else if (referenceType == Eduegate.Framework.Enums.DocumentReferenceTypes.AssetRemoval)
                    {
                        inventory.IsSerialMapDisposed = true;
                        UpdateAssetSerialMapsStatus(inventory.AssetSerialMapIDs, inventory);
                    }
                    else if (referenceType == Eduegate.Framework.Enums.DocumentReferenceTypes.AssetEntryPurchase)
                    {
                        fullAssetSerialMapDTOs.AddRange(inventory.AssetSerialMapDTOs);
                    }

                    count++;
                }

                if (referenceType == Eduegate.Framework.Enums.DocumentReferenceTypes.AssetEntryPurchase)
                {
                    InsertAssetSerialMapList(fullAssetSerialMapDTOs);
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

        public static void AssetTransactionProcessingFailed(AssetTransactionHeadViewModel transactionHead, string errorMessage)
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

        public static void AssetTransactionProcessingCompleted(AssetTransactionHeadViewModel transactionHead, string errorMessage)
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

        public static bool IsAssetTransactionEnabled(Eduegate.Framework.Enums.DocumentReferenceTypes referenceType)
        {
            var enabledTransaction = new Domain.Setting.SettingBL().GetSettingValue<string>("EnabledAssetTransactions");

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

        public void UpdateAssetSerialMapsInventoryID(List<long> assetSerialMapIDs, AssetInventoryDTO assetInventory)
        {
            if (assetInventory.AssetInventoryIID > 0)
            {
                var result = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().UpdateAssetSerialMapsInventoryID(assetSerialMapIDs, assetInventory);
            }
        }

        public void UpdateAssetSerialMapsStatus(List<long> assetSerialMapIDs, AssetInventoryDTO assetInventory)
        {
            if (assetInventory.AssetInventoryIID > 0)
            {
                var result = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().UpdateAssetSerialMapsStatus(assetSerialMapIDs, assetInventory);
            }
        }

        public void InsertAssetSerialMapList(List<AssetSerialMapDTO> assetSerialMaps)
        {
            if (assetSerialMaps.Count > 0)
            {
                var result = new Eduegate.TransactionEgine.ClientFactory.ClientFactory(_callContext).InsertAssetSerialMapList(assetSerialMaps);
            }
        }

    }
}
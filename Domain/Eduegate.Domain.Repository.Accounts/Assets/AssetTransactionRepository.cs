using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using Eduegate.Domain.Entity.Models.ValueObjects;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper;
using Eduegate.Framework.Logs;
using Eduegate.Services.Contracts.Accounts.Assets;
using Eduegate.Domain.Entity.Accounts;
using Eduegate.Domain.Entity.Accounts.Models.Assets;
using Eduegate.Domain.Repository.Settings;
using Eduegate.Domain.Entity.Accounts.Models.Mutual;
using Eduegate.Framework.Enums;
using Eduegate.Domain.Entity;
using Eduegate.Framework.Helper.Enums;

namespace Eduegate.Domain.Repository.Accounts.Assets
{
    public class AssetTransactionRepository
    {

        //public IDictionary<string, string> GetCustomerSummary(long referenceId)
        //{
        //    using (var db = new dbEduegateAccountsContext())
        //    {
        //        var transaction = new Dictionary<string, string>();
        //        transaction.Add("LastMonthTotalOrders",
        //            db.AssetTransactionHeads.Where(x => x.CustomerID == referenceId
        //                && x.EntryDate.Value.Month == DateTime.Now.Month
        //                && x.EntryDate.Value.Year == DateTime.Now.Year).Count().ToString());

        //        //transaction.Add("LastMonthTotalSales",
        //        //    db.AssetTransactionHeads.Where(x => x.CustomerID == referenceId
        //        //        && x.EntryDate.Value.Month == DateTime.Now.Month
        //        //        && x.EntryDate.Value.Year == DateTime.Now.Year).Sum(x => x.PaidAmount).ToString());

        //        transaction.Add("TotalOrders",
        //            db.AssetTransactionHeads.Where(x => x.CustomerID == referenceId).Count().ToString());

        //        //transaction.Add("TotalSales",
        //        //    db.AssetTransactionHeads.Where(x => x.CustomerID == referenceId).Sum(x => x.PaidAmount).ToString());

        //        return transaction;
        //    }
        //}

        /// <summary>
        /// get all the transaction based on DocumentReferenceTypes and TransactionStatus
        /// </summary>
        /// <param name="referenceTypes">enum</param>
        /// <param name="transactionStatus">enum</param>
        /// <returns>list of AssetTransactionHead</returns>
        public List<AssetTransactionHead> GetAllTransaction(DocumentReferenceTypes referenceTypes, Eduegate.Framework.Enums.TransactionStatus transactionStatus)
        {
            using (dbEduegateAccountsContext db = new dbEduegateAccountsContext())
            {
                return (from th in db.AssetTransactionHeads
                        join td in db.AssetTransactionDetails on th.HeadIID equals td.HeadID
                        join dt in db.DocumentTypes on th.DocumentTypeID equals dt.DocumentTypeID
                        join rt in db.DocumentReferenceTypes on dt.ReferenceTypeID equals rt.ReferenceTypeID
                        //join ta in db.TransactionAllocations on td.DetailIID equals ta.TrasactionDetailID into tdljta
                        //from ta in tdljta.DefaultIfEmpty()
                        where
                        (rt.ReferenceTypeID == (int)(DocumentReferenceTypes)referenceTypes)
                        && td.AssetID.HasValue
                        // check if TransactionStatusID is New(1)
                        && ((int)Eduegate.Framework.Enums.TransactionStatus.New == (int)(Eduegate.Framework.Enums.TransactionStatus)transactionStatus ?
                            (th.ProcessingStatusID == (int)(Eduegate.Framework.Enums.TransactionStatus)transactionStatus || th.ProcessingStatusID.Equals(null)) :
                                th.ProcessingStatusID == (int)(Eduegate.Framework.Enums.TransactionStatus)transactionStatus)
                        select th)
                             .Include(h => h.AssetTransactionDetails.Select(d => d.AssetTransactionSerialMaps))
                             .Include(h => h.DocumentType)
                             .Take(new Domain.Setting.SettingBL(null).GetSettingValue<int>("MaxFetchCount"))
                             .Distinct()
                             .AsNoTracking()
                             .ToList();
            }
        }

        /// <summary>
        /// after getting the data based on New status update the quantity in ProductInventories table
        /// </summary>
        /// <param name="AssetID">long datatype</param>
        /// <param name="quantity">decimal datatype</param>
        /// <returns>if success true else false</returns>
        public bool IncreaseAssetInventory(AssetInventory assetInventory, dbEduegateAccountsContext dbContext = null)
        {
            bool ret = false;

            using (dbEduegateAccountsContext db = (dbContext == null ? new dbEduegateAccountsContext() : dbContext))
            {
                try
                {
                    var maxMatchID = db.AssetInventories.Where(i => i.AssetID == assetInventory.AssetID && i.BranchID == assetInventory.BranchID)
                        .Max(a => (long?)a.Batch) ?? 0;
                    assetInventory.Batch = maxMatchID + 1;
                    assetInventory.Quantity = (assetInventory.Quantity);
                    assetInventory.CreatedDate = DateTime.Now;
                    assetInventory.OriginalQty = assetInventory.OriginalQty;
                    assetInventory.CostAmount = assetInventory.CostAmount;
                    assetInventory.HeadID = assetInventory.HeadID;
                    assetInventory.IsActive = assetInventory.IsActive;

                    if (assetInventory.BranchID == null || assetInventory.BranchID == 0)
                    {
                        assetInventory.BranchID = 1; //the default branch
                    }

                    db.AssetInventories.Add(assetInventory);

                    db.SaveChanges();
                    ret = true;
                }
                catch (Exception ex)
                {
                    ret = false;
                    SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to update AssetInventories. AssetID:" + assetInventory.AssetID.ToString(), TrackingCode.TransactionEngine);
                    throw;
                }
            }

            return ret;
        }

        public bool DecreaseAssetInventory(AssetInventory assetInventory, dbEduegateAccountsContext dbContext = null)
        {
            bool ret = false;

            using (dbEduegateAccountsContext db = (dbContext == null ? new dbEduegateAccountsContext() : dbContext))
            {
                try
                {
                    //if the asset inventory is available from multiple batches it should deduct from the first and remaing goes to next
                    var inventories = db.AssetInventories.Where(i => i.AssetID == assetInventory.AssetID
                        && i.BranchID == assetInventory.BranchID).OrderBy(a => a.Batch).AsNoTracking();

                    if (inventories.IsNotNull())
                    {
                        var descreaseTotalQuantity = assetInventory.Quantity;

                        foreach (var inventory in inventories)
                        {
                            var descreaseQuantity = descreaseTotalQuantity <= inventory.Quantity ? descreaseTotalQuantity : inventory.Quantity;
                            inventory.Quantity = inventory.Quantity - descreaseQuantity;
                            descreaseTotalQuantity -= descreaseQuantity;
                            inventory.UpdatedBy = assetInventory.UpdatedBy;
                            inventory.UpdatedDate = DateTime.Now;

                            db.Entry(inventory).State = EntityState.Modified;

                            if (descreaseTotalQuantity <= 0)
                                break;
                        }

                        if (descreaseTotalQuantity != 0)
                        {
                            throw new Exception("Asset inventory not available to reduce. AssetID:" + assetInventory.AssetID.ToString());
                        }
                    }
                    else
                    {
                        throw new Exception("Asset inventory not available to reduce. AssetID:" + assetInventory.AssetID.ToString());
                    }

                    db.SaveChanges();
                    ret = true;
                }
                catch (Exception ex)
                {
                    ret = false;
                    SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to update AssetInventories. AssetID:" + assetInventory.AssetID.ToString(), TrackingCode.TransactionEngine);
                    throw new Exception("Not able to update AssetInventories. AssetID:" + assetInventory.AssetID.ToString());
                }
            }

            return ret;
        }

        /// <summary>
        /// after getting the data based on New status update the quantity in ProductInventories table
        /// </summary>
        /// <param name="AssetID">long datatype</param>
        /// <param name="quantity">decimal datatype</param>
        /// <returns>if success true else false</returns>
        public bool InsertAssetInventoryNegativeQuantity(AssetInventory assetInventory, dbEduegateAccountsContext dbContext = null)
        {
            bool ret = false;

            using (dbEduegateAccountsContext db = (dbContext == null ? new dbEduegateAccountsContext() : dbContext))
            {
                try
                {
                    var maxMatchID = db.AssetInventories.Where(i => i.AssetID == assetInventory.AssetID && i.BranchID == assetInventory.BranchID)
                        .Max(a => (long?)a.Batch) ?? 0;
                    assetInventory.Batch = maxMatchID + 1;
                    assetInventory.Quantity = - assetInventory.Quantity;
                    assetInventory.CreatedBy = assetInventory.CreatedBy;
                    assetInventory.CreatedDate = DateTime.Now;
                    assetInventory.OriginalQty = assetInventory.OriginalQty;
                    assetInventory.CostAmount = assetInventory.CostAmount;
                    assetInventory.HeadID = assetInventory.HeadID;
                    assetInventory.IsActive = assetInventory.IsActive;

                    if (assetInventory.BranchID == null || assetInventory.BranchID == 0)
                    {
                        assetInventory.BranchID = 1; //the default branch
                    }

                    db.AssetInventories.Add(assetInventory);

                    db.SaveChanges();
                    ret = true;
                }
                catch (Exception ex)
                {
                    ret = false;
                    SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to update AssetInventories. AssetID:" + assetInventory.AssetID.ToString(), TrackingCode.TransactionEngine);
                    throw;
                }
            }

            return ret;
        }

        public bool UpdateAssetInventory(AssetInventory assetInventory)
        {
            bool ret = false;

            using (dbEduegateAccountsContext db = new dbEduegateAccountsContext())
            {
                try
                {
                    /** Commented existing logic;
                        from now on every purchase will be just added with new BatchID 
                    **/
                    AssetInventory query = db.AssetInventories.Where(i => i.AssetID == assetInventory.AssetID && i.Batch == assetInventory.Batch && i.BranchID == assetInventory.BranchID)
                        .AsNoTracking()
                        .FirstOrDefault();

                    if (query == null)
                        db.AssetInventories.Add(assetInventory);
                    else
                        query.Quantity = assetInventory.Quantity;

                    db.SaveChanges();
                    ret = true;
                }
                catch (Exception ex)
                {
                    ret = false;
                    SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to update AssetInventories. assetID:" + assetInventory.AssetID.ToString(), TrackingCode.TransactionEngine);
                    throw;
                }
            }

            return ret;
        }

        /// <summary>
        /// if quantity is updated suceessfully in ProductInventories table then add new entry in InvetoryTransactions table
        /// </summary>
        /// <param name="invetoryTransaction">object of InvetoryTransaction entity</param>
        /// <returns>bolean </returns>
        public bool UpdateAssetInventoryTransactions(AssetInventoryTransaction inventoryTransaction)
        {
            bool isSuccess = false;
            dbEduegateAccountsContext db = new dbEduegateAccountsContext();

            try
            {
                // Add InvetoryTransactions
                db.AssetInventoryTransactions.Add(inventoryTransaction);
                db.SaveChanges();

                if (inventoryTransaction.AssetInvetoryTransactionIID > 0)
                {
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to Add AssetInventoryTransactions.", TrackingCode.TransactionEngine);
                throw;
            }
            return isSuccess;
        }

        /// <summary>
        /// after getting success of AddInvetoryTransactions method we have to update status in AssetTransactionHead table from New to InProgress
        /// </summary>
        /// <param name="headId">long datatype</param>
        /// <param name="transactionStatusId">byte datatype</param>
        /// <returns>boolean value </returns>
        public bool UpdateAssetTransactionHead(AssetTransactionHead transactionHead)
        {
            bool isSuccess = false;
            dbEduegateAccountsContext db = new dbEduegateAccountsContext();

            try
            {
                AssetTransactionHead query = db.AssetTransactionHeads.Where(x => x.HeadIID == transactionHead.HeadIID)
                    .AsNoTracking().FirstOrDefault();

                if (query.IsNotNull())
                {
                    if (query.DocumentStatusID == (byte)Eduegate.Services.Contracts.Enums.TransactionStatus.Returned ||
                        query.DocumentStatusID == (byte)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled ||
                        query.DocumentStatusID == (byte)Eduegate.Services.Contracts.Enums.DocumentStatuses.PartialReturn)
                        return isSuccess;

                    query.ProcessingStatusID = transactionHead.ProcessingStatusID;

                    query.DocumentStatusID = transactionHead.DocumentStatusID;

                    //if (!string.IsNullOrEmpty(transactionHead.Description))
                    //{
                    //    query.Description = (string.IsNullOrEmpty(transactionHead.Description) ? string.Empty : transactionHead.Description + ",") + transactionHead.Description;
                    //}

                    query.UpdatedBy = transactionHead.UpdatedBy;
                    query.UpdatedDate = DateTime.Now;

                    db.Entry(query).State = EntityState.Modified;

                    db.SaveChanges();
                }
                isSuccess = true;
            }
            catch (Exception ex)
            {
                isSuccess = false;
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not ablet to update AssetTransactionHead. HeadIID:" + transactionHead.HeadIID.ToString(), TrackingCode.TransactionEngine);
                throw;
            }
            return isSuccess;
        }


        /// <summary>
        /// get the AssetInventory detail based on AssetID
        /// </summary>
        /// <param name="assetInventory">AssetInventory object</param>
        /// <returns>object of AssetInventory</returns>
        public AssetInventory GetAssetInventoryDetail(AssetInventory assetInventory)
        {
            dbEduegateAccountsContext db = new dbEduegateAccountsContext();
            try
            {
                AssetInventory query = db.AssetInventories.Where(x => x.AssetID == assetInventory.AssetID)
                    .AsNoTracking()
                    .FirstOrDefault();
                return query;
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not ablet to update AssetInventories. AssetID:" + assetInventory.AssetID.ToString(), TrackingCode.TransactionEngine);
                throw;
            }
        }

        /// <summary>
        /// get the quantity based on assetID
        /// </summary>
        /// <param name="assetID"> long datatype</param>
        /// <returns>quantity</returns>
        public decimal CheckAssetAvailability(long branchID, long assetID)
        {
            decimal quantity = 0;

            dbEduegateAccountsContext db = new dbEduegateAccountsContext();
            try
            {
                var totalQuantity = db.AssetInventories.Where(x => x.BranchID == branchID && x.AssetID == assetID).AsNoTracking().Sum(a => a.Quantity);
                quantity = totalQuantity == null ? 0 : totalQuantity.Value;
            }
            catch (Exception ex)
            {
                quantity = 0;
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get AssetInventories. AssetID:" + assetID.ToString(), TrackingCode.TransactionEngine);
                throw;
            }

            return quantity;
        }

        public bool CheckAssetAvailability(long? branchID, List<AssetTransactionDetailsDTO> transactions)
        {
            bool isSuccess = true;
            foreach (var transaction in transactions)
            {
                // Convert into decimal
                var quantity = CheckAssetAvailability(branchID.Value, transaction.AssetID.Value);

                if (transaction.Quantity > quantity)
                {
                    return isSuccess = false;
                }
            }

            return isSuccess;
        }

        //public bool CheckAvailibilityBundleItem(long branchID, List<TransactionDetailDTO> transaction)
        //{
        //    bool isSuccess = true;
        //    foreach (var trans in transaction)
        //    {
        //        var bundleItems = GetProductBundleItemDetails(trans.ProductSKUMapID.Value);
        //        //0. Check Quantity

        //        isSuccess = CheckAvailibilityItems(branchID, trans.Quantity.Value, bundleItems);

        //        if (!isSuccess)
        //        {

        //            break;
        //        }


        //    }
        //    return isSuccess;
        //}

        //public bool CheckAvailibilityItems(long branchID, decimal bundleQuantity, List<ProductBundleDTO> bundleItems)
        //{
        //    bool isSuccess = true;
        //    foreach (var transaction in bundleItems)
        //    {
        //        // Convert into decimal
        //        var quantity = CheckAvailibility(branchID, transaction.FromProductSKUMapID.Value);

        //        if ((transaction.Quantity * bundleQuantity) > quantity)
        //        {
        //            return isSuccess = false;
        //        }
        //    }

        //    return isSuccess;
        //}


        /// <summary>
        /// get the transaction detail based on Id
        /// </summary>
        /// <param name="headId">long datatype</param>
        /// <returns>return the single object of AssetTransactionHead</returns>
        public AssetTransactionHead GetAssetTransactionDetail(long headID)
        {
            using (dbEduegateAccountsContext db = new dbEduegateAccountsContext())
            {
                var result = db.AssetTransactionHeads.Where(a => a.HeadIID == headID)
                             .Include(h => h.AssetTransactionDetails).ThenInclude(d => d.AssetTransactionSerialMaps)
                             .Include(h => h.DocumentType)
                             .AsNoTracking()
                    .FirstOrDefault();
                return result;
            }
        }

        /// <summary>
        /// Saving the Transaction with one transaction header and muliple transaction details 
        /// </summary>
        /// <param name="transaction">Transaction details</param>
        /// <returns>Updated transaction details</returns>

        public AssetTransactionHead SaveAssetTransactions(AssetTransactionHead transaction, List<AssetSerialMap> productInventorySerialMapsDigi = null)
        {
            try
            {
                //////////TTTTTTTTTTTOOOOOOOOODOOOOOOOOOOOOOOOOOOOOOOO
                ///Wrong way of doing it, it should be refactored
                /////////////TTTTTTTTTTTOOOOOOOOODOOOOOOOOOOOOOOOOOOOOOO

                if (transaction.IsNotNull())
                {
                    using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
                    {
                        if (transaction.HeadIID <= 0)
                        {
                            dbContext.AssetTransactionHeads.Add(transaction);
                        }
                        else
                        {
                            // get head from db and update
                            AssetTransactionHead dbTransactionHead = dbContext.AssetTransactionHeads.Where(x => x.HeadIID == transaction.HeadIID)
                                .Include(i => i.AssetTransactionDetails).ThenInclude(i => i.AssetTransactionSerialMaps)
                                .AsNoTracking()
                                .FirstOrDefault();

                            if (dbTransactionHead.IsNotNull())
                            {
                                dbTransactionHead.HeadIID = transaction.HeadIID;
                                dbTransactionHead.CompanyID = transaction.CompanyID;
                                dbTransactionHead.DocumentTypeID = transaction.DocumentTypeID;
                                dbTransactionHead.EntryDate = transaction.EntryDate;
                                //dbTransactionHead.CustomerID = transaction.CustomerID;
                                //dbTransactionHead.StudentID = transaction.StudentID;
                                //dbTransactionHead.StaffID = transaction.StaffID;
                                //dbTransactionHead.Description = transaction.Description;
                                dbTransactionHead.TransactionNo = transaction.TransactionNo;
                                dbTransactionHead.SupplierID = transaction.SupplierID;
                                dbTransactionHead.ProcessingStatusID = transaction.ProcessingStatusID;
                                dbTransactionHead.DocumentStatusID = transaction.DocumentStatusID;
                                dbTransactionHead.CreatedBy = transaction.CreatedBy;
                                dbTransactionHead.UpdatedBy = transaction.UpdatedBy;
                                dbTransactionHead.CreatedDate = transaction.CreatedDate;
                                dbTransactionHead.UpdatedDate = transaction.UpdatedDate;
                                dbTransactionHead.BranchID = transaction.BranchID;
                                dbTransactionHead.ToBranchID = transaction.ToBranchID;
                                dbTransactionHead.ReferenceHeadID = transaction.ReferenceHeadID;
                                dbTransactionHead.Reference = transaction.Reference;
                                //if (transaction.DocumentStatusID.Value == (int)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled)
                                //{
                                //    dbTransactionHead.DocumentCancelledDate = DateTime.Now;
                                //}

                                dbContext.Entry(dbTransactionHead).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }

                            //TransactionDetails won't be null if it is null we should not update DB atleast one record must be exist in DB.
                            if (transaction.AssetTransactionDetails.IsNotNull() && transaction.AssetTransactionDetails.Count > 0)
                            {
                                // newly added transaction detail lines 
                                List<AssetTransactionDetail> transactionToInsert = transaction.AssetTransactionDetails.Where(x => x.DetailIID == default(long)).ToList();

                                // collect modified transaction detail lines 
                                List<AssetTransactionDetail> transactionToUpdate = (from transDetails in transaction.AssetTransactionDetails
                                                                               join dbTransaction in dbTransactionHead.AssetTransactionDetails on transDetails.DetailIID equals dbTransaction.DetailIID
                                                                               select transDetails).ToList();

                                var transDetailIIDs = new HashSet<long>(transaction.AssetTransactionDetails.Select(x => x.DetailIID));

                                // collect transaction details to be deleted
                                List<AssetTransactionDetail> transactionDetailsToDelete = dbTransactionHead.AssetTransactionDetails.Where(x => !transDetailIIDs.Contains(x.DetailIID)).ToList();

                                // update transaction lines
                                if (transactionToUpdate != null)
                                {
                                    foreach (AssetTransactionDetail tDetail in transactionToUpdate)
                                    {
                                        AssetTransactionDetail transactionDetail = dbContext.AssetTransactionDetails.Where(x => x.DetailIID == tDetail.DetailIID).FirstOrDefault();

                                        transactionDetail.DetailIID = tDetail.DetailIID;
                                        transactionDetail.HeadID = tDetail.HeadID;
                                        transactionDetail.Quantity = tDetail.Quantity;
                                        transactionDetail.Amount = tDetail.Amount;
                                        transactionDetail.CutOffDate = tDetail.CutOffDate;

                                        transactionDetail.CreatedBy = tDetail.UpdatedBy;
                                        transactionDetail.UpdatedBy = tDetail.UpdatedBy;
                                        transactionDetail.UpdatedBy = tDetail.UpdatedBy;
                                        transactionDetail.UpdatedDate = tDetail.UpdatedDate;

                                        if (transactionDetail.AssetTransactionSerialMaps != null && transactionDetail.AssetTransactionSerialMaps.Count > 0)
                                        {
                                            dbContext.AssetTransactionSerialMaps.RemoveRange(transactionDetail.AssetTransactionSerialMaps);
                                        }

                                        transactionDetail.AssetTransactionSerialMaps.Clear();
                                        transactionDetail.AssetTransactionSerialMaps = tDetail.AssetTransactionSerialMaps;
                                    }
                                }

                                // isnert new transaction lines
                                if (transactionToInsert != null)
                                {
                                    foreach (AssetTransactionDetail tDetail in transactionToInsert)
                                    {
                                        AssetTransactionDetail transactionDetail = tDetail;
                                        transactionDetail.HeadID = dbTransactionHead.HeadIID;
                                        dbContext.AssetTransactionDetails.Add(transactionDetail);
                                    }
                                }


                                // Delete serial maps and allocation if deleting transaction lines
                                List<AssetTransactionSerialMap> productSerialMapsToDelete = new List<AssetTransactionSerialMap>();
                                if (transactionDetailsToDelete != null)
                                {
                                    foreach (AssetTransactionDetail tDetail in transactionDetailsToDelete)
                                    {
                                        dbTransactionHead.AssetTransactionDetails.Remove(tDetail);

                                        // add into a collection
                                        if (tDetail.AssetTransactionSerialMaps != null && tDetail.AssetTransactionSerialMaps.Count > 0)
                                        {
                                            productSerialMapsToDelete.AddRange(tDetail.AssetTransactionSerialMaps);
                                        }
                                    }
                                }

                                // delete serial maps
                                if (productSerialMapsToDelete != null && productSerialMapsToDelete.Count > 0)
                                {
                                    dbContext.AssetTransactionSerialMaps.RemoveRange(productSerialMapsToDelete);
                                }
                            }
                        }

                        if (productInventorySerialMapsDigi != null && productInventorySerialMapsDigi.Count > 0)
                        {
                            productInventorySerialMapsDigi.ForEach(p => dbContext.Entry(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified);
                        }

                        //workflow rules
                        //dbContext.WorkflowTransactionRuleApproverMaps.RemoveRange(dbContext.WorkflowTransactionRuleApproverMaps
                        //    .Where(a => a.WorkflowTransactionHeadRuleMap.WorkflowTransactionHeadMap.TransactionHeadID == transaction.HeadIID));
                        //dbContext.WorkflowTransactionHeadRuleMaps.RemoveRange(dbContext.WorkflowTransactionHeadRuleMaps
                        //    .Where(a => a.WorkflowTransactionHeadMap.TransactionHeadID == transaction.HeadIID));
                        //dbContext.WorkflowTransactionHeadMaps.RemoveRange(dbContext.WorkflowTransactionHeadMaps
                        //    .Where(a => a.TransactionHeadID == transaction.HeadIID));
                        ////add the new workflow definition.
                        //dbContext.WorkflowTransactionHeadMaps.AddRange(transaction.WorkflowTransactionHeadMaps);
                        dbContext.SaveChanges();


                        //Insert GRN entry in inventory.ProductInventories and inventory.InvetoryTransactions Tables
                        var docTypes = dbContext.DocumentTypes.ToList();
                        var grnDocTypeID = docTypes.Find(x => x.TransactionNoPrefix == "GRN-").DocumentTypeID;

                        var settingDatas = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TRANSACTION_DOC_STS_ID_SUBMITTED");
                        var submittedStatusID = long.Parse(settingDatas);

                        if (transaction.DocumentTypeID == grnDocTypeID && transaction.DocumentStatusID == submittedStatusID)
                        {
                            //GRNStockUpdation(transaction);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<AssetTransactionRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return transaction;
        }
        
        public AssetTransactionHead GetAssetTransaction(long headIID)
        {
            try
            {
                using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
                {
                    AssetTransactionHead transaction = dbContext.AssetTransactionHeads.Where(h => h.HeadIID == headIID)
                                                   .Include(x => x.DocumentType)
                                                   .Include(x => x.AssetTransactionDetails).ThenInclude(y => y.AssetTransactionSerialMaps).ThenInclude(i => i.Supplier)
                                                   .AsNoTracking()
                                                   .FirstOrDefault();

                    return transaction;
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<AssetTransactionRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }
        }



        
        public List<AssetTransactionHead> GetChildAssetTransactions(long parentHeadID)
        {
            using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
            {
                return (from trans in dbContext.AssetTransactionHeads
                        where trans.ReferenceHeadID == parentHeadID && (trans.ProcessingStatusID != (int)Framework.Enums.TransactionStatus.Cancelled)
                        select trans)
                        .Include(t => t.AssetTransactionDetails)
                        .AsNoTracking()
                        .ToList();
            }
        }

        public List<AssetTransactionHead> GetChildTransaction(long parentHeadID)
        {
            using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
            {
                return (from trans in dbContext.AssetTransactionHeads
                        join dt in dbContext.DocumentTypes
                        on trans.DocumentTypeID equals dt.DocumentTypeID
                        where trans.ReferenceHeadID == parentHeadID && dt.ReferenceTypeID == 5
                        select trans)
                        .Include(t => t.AssetTransactionDetails)
                        .Include(t => t.DocumentType)
                        //.Include(t => t.DeliveryType)
                        //.Include(t => t.Branch)
                        //.Include(t => t.Employee)
                        //.Include(t => t.TransactionStatus)
                        //.Include(t => t.JobEntryHeads)
                        //.Include(t => t.JobStatus)
                        //.Include(t => t.Customer)
                        //.Include(t => t.Currency)
                        //.Include(t => t.Supplier)
                        //.Include(i => i.JobEntryHeads).ThenInclude(i => i.JobStatus)
                        .AsNoTracking()
                        .ToList();
            }
        }

        //public ProductInventorySerialMap GetUnUsedSerialKey(long productSKUMapID, int companyID, List<string> exceptions = null)
        //{
        //    using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
        //    {
        //        if (exceptions != null && exceptions.Count > 0)
        //            return dbContext.ProductInventorySerialMaps.Where(x => x.ProductSKUMapID == productSKUMapID && x.CompanyID == companyID &&
        //                 (x.Used == false || x.Used == null) && !exceptions.Contains(x.SerialNo)).OrderByDescending(a => a.ProductInventorySerialMapIID)
        //                 .AsNoTracking()
        //                 .FirstOrDefault();
        //        else
        //            return dbContext.ProductInventorySerialMaps.Where(x => x.ProductSKUMapID == productSKUMapID && x.CompanyID == companyID &&
        //                       (x.Used == false || x.Used == null)).OrderByDescending(a => a.ProductInventorySerialMapIID)
        //                       .AsNoTracking()
        //                       .FirstOrDefault();
        //    }
        //}

        //public List<ProductInventorySerialMap> GetProductInventorySerialMaps(long productSKUMapID, string searchText, int pageSize, bool serialKeyUsed = false, List<string> exceptions = null)
        //{
        //    using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
        //    {
        //        var serialMaps = new List<ProductInventorySerialMap>();
        //        if (serialKeyUsed)
        //        {
        //            if (searchText.IsNotNullOrEmpty())
        //            {
        //                serialMaps = dbContext.ProductInventorySerialMaps.Where(x => x.ProductSKUMapID == productSKUMapID && (x.Used == true) && x.SerialNo.Contains(searchText)).Take(pageSize)
        //                    .AsNoTracking()
        //                    .ToList();
        //            }
        //            else
        //            {
        //                serialMaps = dbContext.ProductInventorySerialMaps.Where(x => x.ProductSKUMapID == productSKUMapID && (x.Used == true)).Take(pageSize)
        //                    .AsNoTracking()
        //                    .ToList();
        //            }
        //        }
        //        else
        //        {
        //            if (searchText.IsNotNullOrEmpty())
        //            {
        //                serialMaps = dbContext.ProductInventorySerialMaps.Where(x => x.ProductSKUMapID == productSKUMapID &&
        //                    (x.Used == false || x.Used == null) && x.SerialNo.Contains(searchText)).Take(pageSize)
        //                    .AsNoTracking()
        //                    .ToList();
        //            }
        //            else
        //            {
        //                serialMaps = dbContext.ProductInventorySerialMaps.Where(x => x.ProductSKUMapID == productSKUMapID &&
        //                    (x.Used == false || x.Used == null)).Take(pageSize)
        //                    .AsNoTracking()
        //                    .ToList();
        //            }
        //        }
        //        return serialMaps;
        //    }
        //}

        public long GetNextBatch(long assetID, long? branchID)
        {
            using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
            {
                var batchEntry = dbContext.AssetInventories.Where(x => x.AssetID == assetID && x.BranchID == branchID).AsNoTracking().Max(a => (long?)a.Batch);
                return !batchEntry.HasValue ? 1 : batchEntry.Value + 1;
            }
        }

        public decimal GetAssetCostPrice(long assetID, long batchID, CostSetting costSetting)
        {
            using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
            {
                decimal? costPrice;

                switch (costSetting)
                {
                    case CostSetting.Average:
                        costPrice = dbContext.AssetInventories.Where(x => x.AssetID == assetID).AsNoTracking().Average(a => a.Quantity * a.CostAmount);
                        break;
                    case CostSetting.FIFO:
                        costPrice = dbContext.AssetInventories.Where(x => x.AssetID == assetID && x.Batch == batchID).AsNoTracking().Average(a => a.CostAmount * a.Quantity);
                        break;
                    default:
                        costPrice = dbContext.AssetInventories.Where(x => x.AssetID == assetID).AsNoTracking().Average(a => a.CostAmount * a.Quantity);
                        break;
                }

                return !costPrice.HasValue ? 0 : costPrice.Value;
            }
        }

        public decimal GetAssetCostPrice(long headID, long assetID)
        {
            using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
            {
                var transaction = dbContext.AssetInventories.Where(a => a.HeadID == headID && a.AssetID == assetID)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (transaction != null)
                    return transaction.CostAmount.Value;
                else
                    return 0;
            }
        }

        public Transaction GetTransactionDetails(List<int?> docuementTypeIDs, DateTime dateFrom, DateTime dateTo)
        {
            using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
            {
                var trans = new Transaction();

                var queryCount = (
                    from thead in dbContext.AssetTransactionHeads
                    where docuementTypeIDs.Contains(thead.DocumentTypeID) && thead.EntryDate >= dateFrom && thead.EntryDate <= dateTo
                    && thead.ProcessingStatusID == 5 && thead.DocumentStatusID == 5
                    select thead.HeadIID
                    );

                trans.TransactionCount = queryCount.Count();

                //var queryAmount = (
                //    from thead in dbContext.AssetTransactionHeads
                //    join entitlement in dbContext.TransactionHeadEntitlementMaps on thead.HeadIID equals entitlement.TransactionHeadID
                //    where docuementTypeIDs.Contains(thead.DocumentTypeID) && thead.EntryDate >= dateFrom && thead.EntryDate <= dateTo
                //    && thead.ProcessingStatusID == 5 && thead.DocumentStatusID == 5
                //    select entitlement.Amount
                //    );

                //trans.Amount = queryAmount.Sum();
                return trans;
            }
        }

        public Transaction GetSupplierTransactionDetails(long loginID, List<int?> docuementTypeIDs, DateTime dateFrom, DateTime dateTo)
        {
            using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
            {
                var trans = new Transaction();

                var queryCount = (
                    from thead in dbContext.AssetTransactionHeads
                    join supplier in dbContext.Suppliers on thead.SupplierID equals supplier.SupplierIID
                    where docuementTypeIDs.Contains(thead.DocumentTypeID) && thead.EntryDate >= dateFrom && thead.EntryDate <= dateTo
                    && thead.ProcessingStatusID == 5 && thead.DocumentStatusID == 5 && supplier.LoginID == loginID
                    select thead.HeadIID
                    );

                trans.TransactionCount = queryCount.Count();

                //var queryAmount = (
                //    from thead in dbContext.AssetTransactionHeads
                //    join entitlement in dbContext.TransactionHeadEntitlementMaps on thead.HeadIID equals entitlement.TransactionHeadID
                //    join supplier in dbContext.Suppliers on thead.SupplierID equals supplier.SupplierIID
                //    where docuementTypeIDs.Contains(thead.DocumentTypeID) && thead.EntryDate >= dateFrom && thead.EntryDate <= dateTo
                //    && thead.ProcessingStatusID == 5 && thead.DocumentStatusID == 5 && supplier.LoginID == loginID
                //    select entitlement.Amount
                //    );

                //trans.Amount = queryAmount.Sum();
                return trans;
            }
        }

        public List<AssetInventory> GetAssetInventories(long assetID, long branchID)
        {
            using (dbEduegateAccountsContext db = new dbEduegateAccountsContext())
            {
                // Returns only SKUs from batches that have quantity
                return db.AssetInventories.Where(p => p.AssetID == assetID && p.Quantity != null && p.Quantity > 0 && p.BranchID == branchID).OrderBy(p => p.Batch)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public List<Transaction> GetTransactions(long skuID, int count = 20)
        {
            using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
            {
                return (
                    from thead in dbContext.AssetTransactionHeads
                    join tdetail in dbContext.AssetTransactionDetails on thead.HeadIID equals tdetail.HeadID
                    join doc in dbContext.DocumentTypes on thead.DocumentTypeID equals doc.DocumentTypeID
                    //where tdetail.ProductSKUMapID == skuID
                    group new { thead, tdetail, doc } by new { doc.TransactionTypeName, thead.TransactionNo, thead.CreatedDate } into grp
                    select new Transaction()
                    {
                        TransactionTypeName = grp.Key.TransactionTypeName,
                        TransactionNo = grp.Key.TransactionNo,
                        CreatedDate = grp.Key.CreatedDate,
                        Amount = grp.Sum(a => a.tdetail.Amount)
                    }
                    )
                    .Take(count)
                    .AsNoTracking()
                    .ToList();
            }
        }
        
        public List<AssetTransactionHead> GetTransactionDetails(IList<Nullable<long>> transactionHeadID)
        {
            using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
            {
                var transactionheads = dbContext.AssetTransactionHeads.Where(x => transactionHeadID.Contains(x.HeadIID))
                    //.Include(x => x.JobStatus).Include(x => x.Customer).Include(x => x.OrderContactMaps)
                    .AsNoTracking()
                    .ToList();
                return transactionheads;
            }
        }

        public DocumentReferenceType GetDocumentReferenceTypeByHeadId(long headId)
        {
            using (dbEduegateAccountsContext db = new dbEduegateAccountsContext())
            {
                var result = from th in db.AssetTransactionHeads
                             join
                                dt in db.DocumentTypes on th.DocumentTypeID equals dt.DocumentTypeID
                             join
                                drf in db.DocumentReferenceTypes on dt.ReferenceTypeID equals drf.ReferenceTypeID
                             where th.HeadIID == headId
                             select drf;

                return result
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }

        public Entity.Accounts.Models.Inventory.TransactionStatus GetTransactionStatus(byte transactionStatusId)
        {
            using (var db = new dbEduegateAccountsContext())
            {
                return db.TransactionStatuses.Where(x => x.TransactionStatusID == transactionStatusId).FirstOrDefault();
            }
        }

        public DocumentStatus GetDocumentStatus(long statusID)
        {
            try
            {
                using (dbEduegateAccountsContext db = new dbEduegateAccountsContext())
                {
                    return db.DocumentStatuses.Where(d => d.DocumentStatusID == statusID)
                        .AsNoTracking()
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"Asset transaction repository GetDocumentStatus method failed. Error message: {errorMessage}", ex);

                throw;
            }
        }

        public List<DocumentReferenceStatusMap> GetDocumentStatusesByReferenceType(int referenceTypeID)
        {
            try
            {
                using (dbEduegateAccountsContext db = new dbEduegateAccountsContext())
                {
                    return db.DocumentReferenceStatusMaps.Where(d => d.ReferenceTypeID == referenceTypeID)
                        .Include(d => d.DocumentStatus)
                        .AsNoTracking()
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"Asset transaction repository GetDocumentStatusesByReferenceType method failed. Error message: {errorMessage}", ex);

                throw;
            }
        }

        public AssetTransactionHead GetOrderByinvoiceId(long HeadId)
        {
            using (var db = new dbEduegateAccountsContext())
            {

                var Reference = (from th in db.AssetTransactionHeads
                                 join thead in db.AssetTransactionHeads on th.HeadIID equals thead.ReferenceHeadID
                                 where thead.HeadIID == HeadId
                                 select thead)
                                 .AsNoTracking()
                                 .FirstOrDefault();
                var ReferenceId = Reference.ReferenceHeadID;

                return db.AssetTransactionHeads.Where(x => x.HeadIID == ReferenceId)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }

        public List<AssetTransactionHead> GetAllTransactionsBySerialKey(string serialKey)
        {
            using (var db = new dbEduegateAccountsContext())
            {
                var transactions = (from th in db.AssetTransactionHeads
                                    join td in db.AssetTransactionDetails
                                    on th.HeadIID equals td.HeadID
                                    join psm in db.AssetTransactionSerialMaps
                                    on td.DetailIID equals psm.TransactionDetailID
                                    where psm.SerialNumber == serialKey
                                    select th)

                        .Union
                        (from td in db.AssetTransactionDetails
                         join
                             th in db.AssetTransactionHeads on td.HeadID equals th.HeadIID
                         //where td.SerialNumber == serialKey
                         select th)
                               //.Include(x => x.Customer)
                               //.Include(x => x.Supplier)
                               //.Include(x => x.DeliveryType)
                               //.Include(x => x.Currency)
                               //.Include(x => x.TransactionStatus)
                               //.Include(x => x.EntityTypeEntitlement)
                               //.Include(x => x.DocumentReferenceStatusMap)
                               .AsNoTracking()
                               .ToList();

                return transactions;
            }
        }

        public List<AssetTransactionHead> GetParentTransactions(List<long> heads)
        {
            using (dbEduegateAccountsContext db = new dbEduegateAccountsContext())
            {
                return db.AssetTransactionHeads.Where(x => heads.Contains((long)x.HeadIID))
                    //.Include(x => x.Customer)
                    //.Include(x => x.Supplier)
                    //.Include(x => x.DeliveryType)
                    //.Include(x => x.Currency)
                    //.Include(x => x.TransactionStatus)
                    //.Include(x => x.EntityTypeEntitlement)
                    //.Include(x => x.DocumentReferenceStatusMap)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public bool CancelAssetTransferReceiptTransaction(long headID)
        {
            using (var db = new dbEduegateAccountsContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //update the status
                        var query = db.AssetTransactionHeads.Where(x => x.HeadIID == headID).AsNoTracking().FirstOrDefault();

                        if (query.IsNotNull())
                        {
                            query.ProcessingStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.Complete;
                            query.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;

                            query.UpdatedBy = query.UpdatedBy;
                            query.UpdatedDate = DateTime.Now;
                            //query.DocumentCancelledDate = DateTime.Now;

                            db.Entry(query).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }

                        //if invoice already generated intiate cancellation for that
                        var invoices = db.AssetTransactionHeads.Where(x => x.ReferenceHeadID == headID);
                        bool isInvocieGenerated = false;

                        foreach (var invoice in invoices)
                        {
                            if (invoice.ProcessingStatusID == (byte)Eduegate.Framework.Enums.TransactionStatus.Complete)
                            {
                                // If SI Exists it should Generate SRR
                                if (invoice.DocumentType.ReferenceTypeID == (int)DocumentReferenceTypes.SalesInvoice)
                                {
                                    AssetTransactionHead newSRR = CloneTransaction(headID);
                                    newSRR.HeadIID = 0;
                                    newSRR.DocumentTypeID = int.Parse(new SettingRepository().GetSettingDetail("SRRDOCUMENTTYPEID", query.CompanyID.Value).SettingValue);
                                    newSRR.EntryDate = DateTime.Now;
                                    newSRR.TransactionNo = GetNextTransactionNumber(newSRR.DocumentTypeID.Value);
                                    newSRR.DocumentStatusID = (int)Eduegate.Services.Contracts.Enums.DocumentStatuses.Approved;
                                    newSRR.ProcessingStatusID = (int)Eduegate.Framework.Enums.TransactionStatus.New;
                                    newSRR.ReferenceHeadID = invoice.HeadIID;
                                    db.AssetTransactionHeads.Add(newSRR);
                                    db.Entry(newSRR).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                                    if (newSRR.AssetTransactionDetails != null)
                                    {
                                        foreach (var detail in newSRR.AssetTransactionDetails)
                                        {
                                            db.Entry(detail).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                    }
                                }
                                else if (invoice.DocumentType.ReferenceTypeID == (int)DocumentReferenceTypes.PurchaseOrder)
                                {
                                    invoice.ProcessingStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.IntitiateReprocess;
                                    invoice.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;

                                    invoice.UpdatedBy = query.UpdatedBy;
                                    invoice.UpdatedDate = DateTime.Now;
                                    db.Entry(invoice).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                }
                                isInvocieGenerated = true;
                            }
                            else
                            {
                                invoice.ProcessingStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.Complete;
                                invoice.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;
                                invoice.UpdatedBy = query.UpdatedBy;
                                invoice.UpdatedDate = DateTime.Now;
                                db.Entry(invoice).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                                foreach (var detail in invoice.AssetTransactionDetails)
                                {
                                    foreach (var serial in detail.AssetTransactionSerialMaps)
                                    {
                                        var inventory = db.AssetSerialMaps.Where(a => a.SerialNumber == serial.SerialNumber)
                                            .AsNoTracking()
                                            .FirstOrDefault();
                                        inventory.IsActive = false;

                                        db.Entry(inventory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                    }
                                }
                            }
                        }

                        // cancel all the jobs
                        var jobs = db.JobEntryHeads.Where(x => x.TransactionHeadID == headID);

                        //foreach (var job in jobs)
                        //{
                        //    job.JobStatusID = (int)Eduegate.Services.Contracts.Enums.JobStatuses.Cancel;
                        //    db.Entry(job).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        //}

                        //if (!isInvocieGenerated) //if generated inventeroy already moved
                        //{
                        //    //revert back the inventory
                        //    var shoppingCartMap = db.TransactionHeadShoppingCartMaps.Where(a => a.TransactionHeadID == headID)
                        //        .AsNoTracking()
                        //        .FirstOrDefault();

                        //    if (shoppingCartMap != null)
                        //    {
                        //        var shoppingCart = db.ShoppingCarts.Where(a => a.ShoppingCartIID == shoppingCartMap.ShoppingCartID)
                        //            .AsNoTracking()
                        //            .FirstOrDefault();

                        //        if (shoppingCart != null && shoppingCart.IsInventoryBlocked != null && shoppingCart.IsInventoryBlocked.Value && shoppingCart.BlockedHeadID.HasValue)
                        //        {
                        //            // revert back the inventory
                        //            var branchTransfer = CloneTransaction(shoppingCart.BlockedHeadID.Value);
                        //            var branchID = branchTransfer.BranchID;
                        //            branchTransfer.EntryDate = DateTime.Now;
                        //            branchTransfer.BranchID = branchTransfer.ToBranchID;
                        //            branchTransfer.ToBranchID = branchID;
                        //            branchTransfer.ProcessingStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.New;
                        //            branchTransfer.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Approved;
                        //            branchTransfer.TransactionNo = GetNextTransactionNumber(branchTransfer.DocumentTypeID.Value);
                        //            branchTransfer.CreatedDate = DateTime.Now;
                        //            branchTransfer.UpdatedDate = DateTime.Now;
                        //            //branchTransfer.ReferenceHeadID = shoppingCart.BlockedHeadID;
                        //            db.AssetTransactionHeads.Add(branchTransfer);
                        //            db.Entry(branchTransfer).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                        //            if (branchTransfer.TransactionHeadEntitlementMaps != null)
                        //            {
                        //                foreach (var entitlement in branchTransfer.TransactionHeadEntitlementMaps)
                        //                {
                        //                    db.Entry(entitlement).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        //                }
                        //            }

                        //            if (branchTransfer.OrderContactMaps != null)
                        //            {
                        //                foreach (var contact in branchTransfer.OrderContactMaps)
                        //                {
                        //                    db.Entry(contact).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        //                }
                        //            }

                        //            if (branchTransfer.AssetTransactionDetails != null)
                        //            {
                        //                foreach (var detail in branchTransfer.AssetTransactionDetails)
                        //                {
                        //                    db.Entry(detail).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        //                }
                        //            }

                        //            shoppingCart.IsInventoryBlocked = false;
                        //            db.Entry(shoppingCart).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        //        }
                        //    }
                        //}

                        db.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        var abc = ex;
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public AssetTransactionHead CloneTransaction(long headID)
        {
            using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
            {
                var head = dbContext.AssetTransactionHeads.Where(a => a.HeadIID == headID)
                    .Include(i => i.AssetTransactionDetails).ThenInclude(i => i.AssetTransactionSerialMaps)
                    .AsNoTracking()
                    .FirstOrDefault();

                return head;
            }
        }

        public string GetNextTransactionNumber(int documentTypeID)
        {
            using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
            {
                var documentType = dbContext.DocumentTypes.Where(x => x.DocumentTypeID == documentTypeID)
                    .AsNoTracking()
                    .FirstOrDefault();
                if (documentType != null)
                {
                    documentType.LastTransactionNo = documentType.LastTransactionNo.HasValue ? documentType.LastTransactionNo + 1 : 1;

                    dbContext.Entry(documentType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    dbContext.SaveChanges();
                    return documentType.TransactionNoPrefix + documentType.LastTransactionNo.ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        public bool CancelSalesInvoiceTransaction(long headID)
        {
            using (var db = new dbEduegateAccountsContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var query = db.AssetTransactionHeads.Where(x => x.HeadIID == headID)
                            .AsNoTracking()
                            .FirstOrDefault();

                        if (query.IsNotNull())
                        {
                            //generate a sales return request
                            query.ProcessingStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.Complete;
                            query.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;

                            query.UpdatedBy = query.UpdatedBy;
                            query.UpdatedDate = DateTime.Now;
                            //query.DocumentCancelledDate = DateTime.Now;

                            db.Entry(query).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                            foreach (var detail in db.AssetTransactionDetails.Where(x => x.HeadID == headID))
                            {
                                foreach (var serial in detail.AssetTransactionSerialMaps)
                                {
                                    var inventory = db.AssetSerialMaps.Where(a => a.SerialNumber == serial.SerialNumber)
                                        .AsNoTracking()
                                        .FirstOrDefault();
                                    inventory.IsActive = false;

                                    db.Entry(inventory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                }
                            }

                            // cancel all the jobs
                            //var jobs = db.JobEntryHeads.Where(x => x.TransactionHeadID == headID);

                            //foreach (var job in jobs)
                            //{
                            //    job.JobStatusID = (int)Eduegate.Services.Contracts.Enums.JobStatuses.Cancel;
                            //    db.Entry(job).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            //}

                            AssetTransactionHead newSRR = CloneTransaction(headID);
                            newSRR.HeadIID = 0;
                            newSRR.DocumentTypeID = int.Parse(new SettingRepository().GetSettingDetail("SRRDOCUMENTTYPEID", query.CompanyID.Value).SettingValue);
                            newSRR.EntryDate = DateTime.Now;
                            newSRR.TransactionNo = GetNextTransactionNumber(newSRR.DocumentTypeID.Value);
                            newSRR.DocumentStatusID = (int)Eduegate.Services.Contracts.Enums.DocumentStatuses.Approved;
                            newSRR.ProcessingStatusID = (int)Eduegate.Framework.Enums.TransactionStatus.New;
                            newSRR.ReferenceHeadID = query.HeadIID;
                            db.AssetTransactionHeads.Add(newSRR);
                            db.Entry(newSRR).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                            //if (newSRR.TransactionHeadEntitlementMaps != null)
                            //{
                            //    foreach (var entitlment in newSRR.TransactionHeadEntitlementMaps)
                            //    {
                            //        db.Entry(entitlment).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            //    }
                            //}

                            //if (newSRR.OrderContactMaps != null)
                            //{
                            //    foreach (var contact in newSRR.OrderContactMaps)
                            //    {
                            //        db.Entry(contact).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            //    }
                            //}

                            if (newSRR.AssetTransactionDetails != null)
                            {
                                foreach (var detail in newSRR.AssetTransactionDetails)
                                {
                                    db.Entry(detail).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                }
                            }

                        }

                        db.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public bool CancelSalesQuotationTransaction(long headID)
        {
            using (var db = new dbEduegateAccountsContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //update the status
                        var query = db.AssetTransactionHeads.Where(x => x.HeadIID == headID)
                            .AsNoTracking()
                            .FirstOrDefault();

                        if (query.IsNotNull())
                        {
                            query.ProcessingStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.Complete;
                            query.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;

                            query.UpdatedBy = query.UpdatedBy;
                            query.UpdatedDate = DateTime.Now;
                            //query.DocumentCancelledDate = DateTime.Now;

                            db.Entry(query).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }

                        //if invoice already generated intiate cancellation for that
                        var invoices = db.AssetTransactionHeads.Where(x => x.ReferenceHeadID == headID);
                        bool isInvocieGenerated = false;

                        foreach (var invoice in invoices)
                        {
                            if (invoice.ProcessingStatusID == (byte)Eduegate.Framework.Enums.TransactionStatus.Complete)
                            {
                                // If SI Exists it should Generate SRR
                                if (invoice.DocumentType.ReferenceTypeID == (int)DocumentReferenceTypes.SalesInvoice)
                                {
                                    AssetTransactionHead newSRR = CloneTransaction(headID);
                                    newSRR.HeadIID = 0;
                                    newSRR.DocumentTypeID = int.Parse(new SettingRepository().GetSettingDetail("SRRDOCUMENTTYPEID", query.CompanyID.Value).SettingValue);
                                    newSRR.EntryDate = DateTime.Now;
                                    newSRR.TransactionNo = GetNextTransactionNumber(newSRR.DocumentTypeID.Value);
                                    newSRR.DocumentStatusID = (int)Eduegate.Services.Contracts.Enums.DocumentStatuses.Approved;
                                    newSRR.ProcessingStatusID = (int)Eduegate.Framework.Enums.TransactionStatus.New;
                                    newSRR.ReferenceHeadID = invoice.HeadIID;
                                    db.AssetTransactionHeads.Add(newSRR);
                                    db.Entry(newSRR).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                                    //if (newSRR.TransactionHeadEntitlementMaps != null)
                                    //{
                                    //    foreach (var entitlment in newSRR.TransactionHeadEntitlementMaps)
                                    //    {
                                    //        db.Entry(entitlment).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                    //    }
                                    //}

                                    //if (newSRR.OrderContactMaps != null)
                                    //{
                                    //    foreach (var contact in newSRR.OrderContactMaps)
                                    //    {
                                    //        db.Entry(contact).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                    //    }
                                    //}

                                    if (newSRR.AssetTransactionDetails != null)
                                    {
                                        foreach (var detail in newSRR.AssetTransactionDetails)
                                        {
                                            db.Entry(detail).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                    }
                                }
                                else if (invoice.DocumentType.ReferenceTypeID == (int)DocumentReferenceTypes.PurchaseOrder)
                                {
                                    invoice.ProcessingStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.IntitiateReprocess;
                                    invoice.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;

                                    invoice.UpdatedBy = query.UpdatedBy;
                                    invoice.UpdatedDate = DateTime.Now;
                                    db.Entry(invoice).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                }
                                isInvocieGenerated = true;
                            }
                            else
                            {
                                invoice.ProcessingStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.Complete;
                                invoice.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;
                                invoice.UpdatedBy = query.UpdatedBy;
                                invoice.UpdatedDate = DateTime.Now;
                                db.Entry(invoice).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                                foreach (var detail in invoice.AssetTransactionDetails)
                                {
                                    foreach (var serial in detail.AssetTransactionSerialMaps)
                                    {
                                        var inventory = db.AssetSerialMaps.Where(a => a.SerialNumber == serial.SerialNumber)
                                            .AsNoTracking()
                                            .FirstOrDefault();
                                        inventory.IsActive = false;

                                        db.Entry(inventory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                    }
                                }
                            }
                        }

                        // cancel all the jobs
                        //var jobs = db.JobEntryHeads.Where(x => x.TransactionHeadID == headID);

                        //foreach (var job in jobs)
                        //{
                        //    job.JobStatusID = (int)Eduegate.Services.Contracts.Enums.JobStatuses.Cancel;
                        //    db.Entry(job).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        //}

                        //if (!isInvocieGenerated) //if generated inventeroy already moved
                        //{
                        //    //revert back the inventory
                        //    var shoppingCartMap = db.TransactionHeadShoppingCartMaps.Where(a => a.TransactionHeadID == headID)
                        //        .AsNoTracking()
                        //        .FirstOrDefault();

                        //    if (shoppingCartMap != null)
                        //    {
                        //        var shoppingCart = db.ShoppingCarts.Where(a => a.ShoppingCartIID == shoppingCartMap.ShoppingCartID)
                        //            .AsNoTracking()
                        //            .FirstOrDefault();

                        //        if (shoppingCart != null && shoppingCart.IsInventoryBlocked != null && shoppingCart.IsInventoryBlocked.Value && shoppingCart.BlockedHeadID.HasValue)
                        //        {
                        //            // revert back the inventory
                        //            var branchTransfer = CloneTransaction(shoppingCart.BlockedHeadID.Value);
                        //            var branchID = branchTransfer.BranchID;
                        //            branchTransfer.EntryDate = DateTime.Now;
                        //            branchTransfer.BranchID = branchTransfer.ToBranchID;
                        //            branchTransfer.ToBranchID = branchID;
                        //            branchTransfer.ProcessingStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.New;
                        //            branchTransfer.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Approved;
                        //            branchTransfer.TransactionNo = GetNextTransactionNumber(branchTransfer.DocumentTypeID.Value);
                        //            branchTransfer.CreatedDate = DateTime.Now;
                        //            branchTransfer.UpdatedDate = DateTime.Now;
                        //            //branchTransfer.ReferenceHeadID = shoppingCart.BlockedHeadID;
                        //            db.AssetTransactionHeads.Add(branchTransfer);
                        //            db.Entry(branchTransfer).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                        //            if (branchTransfer.TransactionHeadEntitlementMaps != null)
                        //            {
                        //                foreach (var entitlement in branchTransfer.TransactionHeadEntitlementMaps)
                        //                {
                        //                    db.Entry(entitlement).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        //                }
                        //            }

                        //            if (branchTransfer.OrderContactMaps != null)
                        //            {
                        //                foreach (var contact in branchTransfer.OrderContactMaps)
                        //                {
                        //                    db.Entry(contact).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        //                }
                        //            }

                        //            if (branchTransfer.AssetTransactionDetails != null)
                        //            {
                        //                foreach (var detail in branchTransfer.AssetTransactionDetails)
                        //                {
                        //                    db.Entry(detail).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        //                }
                        //            }

                        //            shoppingCart.IsInventoryBlocked = false;
                        //            db.Entry(shoppingCart).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        //        }
                        //    }
                        //}

                        db.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        var abc = ex;
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public bool CancelDeliveryNoteTransaction(long headID)
        {
            using (var db = new dbEduegateAccountsContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //update the status
                        var query = db.AssetTransactionHeads.Where(x => x.HeadIID == headID)
                            .AsNoTracking()
                            .FirstOrDefault();

                        if (query.IsNotNull())
                        {
                            query.ProcessingStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.Complete;
                            query.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;

                            query.UpdatedBy = query.UpdatedBy;
                            query.UpdatedDate = DateTime.Now;
                            //query.DocumentCancelledDate = DateTime.Now;

                            db.Entry(query).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }

                        //if invoice already generated intiate cancellation for that
                        var invoices = db.AssetTransactionHeads.Where(x => x.ReferenceHeadID == headID);
                        bool isInvocieGenerated = false;

                        foreach (var invoice in invoices)
                        {
                            if (invoice.ProcessingStatusID == (byte)Eduegate.Framework.Enums.TransactionStatus.Complete)
                            {
                                // If SI Exists it should Generate SRR
                                if (invoice.DocumentType.ReferenceTypeID == (int)DocumentReferenceTypes.SalesInvoice)
                                {
                                    AssetTransactionHead newSRR = CloneTransaction(headID);
                                    newSRR.HeadIID = 0;
                                    newSRR.DocumentTypeID = int.Parse(new SettingRepository().GetSettingDetail("SRRDOCUMENTTYPEID", query.CompanyID.Value).SettingValue);
                                    newSRR.EntryDate = DateTime.Now;
                                    newSRR.TransactionNo = GetNextTransactionNumber(newSRR.DocumentTypeID.Value);
                                    newSRR.DocumentStatusID = (int)Eduegate.Services.Contracts.Enums.DocumentStatuses.Approved;
                                    newSRR.ProcessingStatusID = (int)Eduegate.Framework.Enums.TransactionStatus.New;
                                    newSRR.ReferenceHeadID = invoice.HeadIID;
                                    db.AssetTransactionHeads.Add(newSRR);
                                    db.Entry(newSRR).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                                    //if (newSRR.TransactionHeadEntitlementMaps != null)
                                    //{
                                    //    foreach (var entitlment in newSRR.TransactionHeadEntitlementMaps)
                                    //    {
                                    //        db.Entry(entitlment).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                    //    }
                                    //}

                                    //if (newSRR.OrderContactMaps != null)
                                    //{
                                    //    foreach (var contact in newSRR.OrderContactMaps)
                                    //    {
                                    //        db.Entry(contact).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                    //    }
                                    //}

                                    if (newSRR.AssetTransactionDetails != null)
                                    {
                                        foreach (var detail in newSRR.AssetTransactionDetails)
                                        {
                                            db.Entry(detail).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                    }
                                }
                                else if (invoice.DocumentType.ReferenceTypeID == (int)DocumentReferenceTypes.PurchaseOrder)
                                {
                                    invoice.ProcessingStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.IntitiateReprocess;
                                    invoice.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;

                                    invoice.UpdatedBy = query.UpdatedBy;
                                    invoice.UpdatedDate = DateTime.Now;
                                    db.Entry(invoice).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                }
                                isInvocieGenerated = true;
                            }
                            else
                            {
                                invoice.ProcessingStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.Complete;
                                invoice.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;
                                invoice.UpdatedBy = query.UpdatedBy;
                                invoice.UpdatedDate = DateTime.Now;
                                db.Entry(invoice).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                                foreach (var detail in invoice.AssetTransactionDetails)
                                {
                                    foreach (var serial in detail.AssetTransactionSerialMaps)
                                    {
                                        var inventory = db.AssetSerialMaps.FirstOrDefault(a => a.SerialNumber == serial.SerialNumber);
                                        inventory.IsActive = false;

                                        db.Entry(inventory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                    }
                                }
                            }
                        }

                        // cancel all the jobs
                        //var jobs = db.JobEntryHeads.Where(x => x.TransactionHeadID == headID);

                        //foreach (var job in jobs)
                        //{
                        //    job.JobStatusID = (int)Eduegate.Services.Contracts.Enums.JobStatuses.Cancel;
                        //    db.Entry(job).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        //}

                        //if (!isInvocieGenerated) //if generated inventeroy already moved
                        //{
                        //    //revert back the inventory
                        //    var shoppingCartMap = db.TransactionHeadShoppingCartMaps.Where(a => a.TransactionHeadID == headID)
                        //        .AsNoTracking()
                        //        .FirstOrDefault();

                        //    if (shoppingCartMap != null)
                        //    {
                        //        var shoppingCart = db.ShoppingCarts.Where(a => a.ShoppingCartIID == shoppingCartMap.ShoppingCartID)
                        //            .AsNoTracking()
                        //            .FirstOrDefault();

                        //        if (shoppingCart != null && shoppingCart.IsInventoryBlocked != null && shoppingCart.IsInventoryBlocked.Value && shoppingCart.BlockedHeadID.HasValue)
                        //        {
                        //            // revert back the inventory
                        //            var branchTransfer = CloneTransaction(shoppingCart.BlockedHeadID.Value);
                        //            var branchID = branchTransfer.BranchID;
                        //            branchTransfer.EntryDate = DateTime.Now;
                        //            branchTransfer.BranchID = branchTransfer.ToBranchID;
                        //            branchTransfer.ToBranchID = branchID;
                        //            branchTransfer.ProcessingStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.New;
                        //            branchTransfer.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Approved;
                        //            branchTransfer.TransactionNo = GetNextTransactionNumber(branchTransfer.DocumentTypeID.Value);
                        //            branchTransfer.CreatedDate = DateTime.Now;
                        //            branchTransfer.UpdatedDate = DateTime.Now;
                        //            //branchTransfer.ReferenceHeadID = shoppingCart.BlockedHeadID;
                        //            db.AssetTransactionHeads.Add(branchTransfer);
                        //            db.Entry(branchTransfer).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                        //            if (branchTransfer.TransactionHeadEntitlementMaps != null)
                        //            {
                        //                foreach (var entitlement in branchTransfer.TransactionHeadEntitlementMaps)
                        //                {
                        //                    db.Entry(entitlement).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        //                }
                        //            }

                        //            if (branchTransfer.OrderContactMaps != null)
                        //            {
                        //                foreach (var contact in branchTransfer.OrderContactMaps)
                        //                {
                        //                    db.Entry(contact).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        //                }
                        //            }

                        //            if (branchTransfer.AssetTransactionDetails != null)
                        //            {
                        //                foreach (var detail in branchTransfer.AssetTransactionDetails)
                        //                {
                        //                    db.Entry(detail).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        //                }
                        //            }

                        //            shoppingCart.IsInventoryBlocked = false;
                        //            db.Entry(shoppingCart).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        //        }
                        //    }
                        //}

                        db.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        var abc = ex;
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public bool CancelPurchaseOrderTransaction(long headID)
        {
            using (var db = new dbEduegateAccountsContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //update the status
                        var query = db.AssetTransactionHeads.Where(x => x.HeadIID == headID)
                            .AsNoTracking()
                            .FirstOrDefault();

                        if (query.IsNotNull())
                        {
                            query.ProcessingStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.Complete;
                            query.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;

                            query.UpdatedBy = query.UpdatedBy;
                            query.UpdatedDate = DateTime.Now;
                            //query.DocumentCancelledDate = DateTime.Now;

                            db.Entry(query).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }

                        //if invoice already generated intiate cancellation for that
                        var invoices = db.AssetTransactionHeads.Where(x => x.ReferenceHeadID == headID);

                        foreach (var invoice in invoices)
                        {
                            if (invoice.ProcessingStatusID == (byte)Eduegate.Framework.Enums.TransactionStatus.Complete)
                            {
                                var prrDocument = new SettingRepository().GetSettingDetail("PRRDOCUMENTTYPEID", query.CompanyID.Value);

                                if (prrDocument != null)
                                {
                                    AssetTransactionHead newPRR = CloneTransaction(headID);
                                    newPRR.HeadIID = 0;
                                    newPRR.DocumentTypeID = int.Parse(prrDocument.SettingValue);
                                    newPRR.EntryDate = DateTime.Now;
                                    newPRR.TransactionNo = GetNextTransactionNumber(newPRR.DocumentTypeID.Value);
                                    newPRR.DocumentStatusID = (int)Eduegate.Services.Contracts.Enums.DocumentStatuses.Approved;
                                    newPRR.ProcessingStatusID = (int)Eduegate.Framework.Enums.TransactionStatus.New;
                                    newPRR.ReferenceHeadID = invoice.HeadIID;
                                    db.AssetTransactionHeads.Add(newPRR);
                                    db.Entry(newPRR).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                                    //if (newPRR.TransactionHeadEntitlementMaps != null)
                                    //{
                                    //    foreach (var entitlment in newPRR.TransactionHeadEntitlementMaps)
                                    //    {
                                    //        db.Entry(entitlment).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                    //    }
                                    //}

                                    //if (newPRR.OrderContactMaps != null)
                                    //{
                                    //    foreach (var contact in newPRR.OrderContactMaps)
                                    //    {
                                    //        db.Entry(contact).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                    //    }
                                    //}

                                    if (newPRR.AssetTransactionDetails != null)
                                    {
                                        foreach (var detail in newPRR.AssetTransactionDetails)
                                        {
                                            db.Entry(detail).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                invoice.ProcessingStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.Complete;
                                invoice.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;
                                invoice.UpdatedBy = query.UpdatedBy;
                                invoice.UpdatedDate = DateTime.Now;
                                db.Entry(invoice).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                                foreach (var detail in invoice.AssetTransactionDetails)
                                {
                                    foreach (var serial in detail.AssetTransactionSerialMaps)
                                    {
                                        var inventory = db.AssetSerialMaps.FirstOrDefault(a => a.SerialNumber == serial.SerialNumber);
                                        inventory.IsActive = false;

                                        db.Entry(inventory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                    }
                                }
                            }
                        }

                        // cancel all the jobs
                        //var jobs = db.JobEntryHeads.Where(x => x.TransactionHeadID == headID);

                        //foreach (var job in jobs)
                        //{
                        //    job.JobStatusID = (int)Eduegate.Services.Contracts.Enums.JobStatuses.Cancel;
                        //    db.Entry(job).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        //}

                        db.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        var abc = ex;
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public bool CancelPurchaseQuoteTransaction(long headID)
        {
            using (var db = new dbEduegateAccountsContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //update the status
                        var query = db.AssetTransactionHeads.Where(x => x.HeadIID == headID)
                            .AsNoTracking()
                            .FirstOrDefault();

                        if (query.IsNotNull())
                        {
                            query.ProcessingStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.Complete;
                            query.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;

                            query.UpdatedBy = query.UpdatedBy;
                            query.UpdatedDate = DateTime.Now;
                            //query.DocumentCancelledDate = DateTime.Now;

                            db.Entry(query).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }

                        //if invoice already generated intiate cancellation for that
                        var invoices = db.AssetTransactionHeads.Where(x => x.ReferenceHeadID == headID);

                        foreach (var invoice in invoices)
                        {
                            if (invoice.ProcessingStatusID == (byte)Eduegate.Framework.Enums.TransactionStatus.Complete)
                            {
                                var prrDocument = new SettingRepository().GetSettingDetail("PRRDOCUMENTTYPEID", query.CompanyID.Value);

                                if (prrDocument != null)
                                {
                                    AssetTransactionHead newPRR = CloneTransaction(headID);
                                    newPRR.HeadIID = 0;
                                    newPRR.DocumentTypeID = int.Parse(prrDocument.SettingValue);
                                    newPRR.EntryDate = DateTime.Now;
                                    newPRR.TransactionNo = GetNextTransactionNumber(newPRR.DocumentTypeID.Value);
                                    newPRR.DocumentStatusID = (int)Eduegate.Services.Contracts.Enums.DocumentStatuses.Approved;
                                    newPRR.ProcessingStatusID = (int)Eduegate.Framework.Enums.TransactionStatus.New;
                                    newPRR.ReferenceHeadID = invoice.HeadIID;
                                    db.AssetTransactionHeads.Add(newPRR);
                                    db.Entry(newPRR).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                                    //if (newPRR.TransactionHeadEntitlementMaps != null)
                                    //{
                                    //    foreach (var entitlment in newPRR.TransactionHeadEntitlementMaps)
                                    //    {
                                    //        db.Entry(entitlment).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                    //    }
                                    //}

                                    //if (newPRR.OrderContactMaps != null)
                                    //{
                                    //    foreach (var contact in newPRR.OrderContactMaps)
                                    //    {
                                    //        db.Entry(contact).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                    //    }
                                    //}

                                    if (newPRR.AssetTransactionDetails != null)
                                    {
                                        foreach (var detail in newPRR.AssetTransactionDetails)
                                        {
                                            db.Entry(detail).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                invoice.ProcessingStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.Complete;
                                invoice.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;
                                invoice.UpdatedBy = query.UpdatedBy;
                                invoice.UpdatedDate = DateTime.Now;
                                db.Entry(invoice).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                                foreach (var detail in invoice.AssetTransactionDetails)
                                {
                                    foreach (var serial in detail.AssetTransactionSerialMaps)
                                    {
                                        var inventory = db.AssetSerialMaps.FirstOrDefault(a => a.SerialNumber == serial.SerialNumber);
                                        inventory.IsActive = false;

                                        db.Entry(inventory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                    }
                                }
                            }
                        }

                        // cancel all the jobs
                        //var jobs = db.JobEntryHeads.Where(x => x.TransactionHeadID == headID);

                        //foreach (var job in jobs)
                        //{
                        //    job.JobStatusID = (int)Eduegate.Services.Contracts.Enums.JobStatuses.Cancel;
                        //    db.Entry(job).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        //}

                        db.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        var abc = ex;
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public bool CancelPurchaseTenderTransaction(long headID)
        {
            using (var db = new dbEduegateAccountsContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //update the status
                        var query = db.AssetTransactionHeads.Where(x => x.HeadIID == headID).AsNoTracking().FirstOrDefault();

                        if (query.IsNotNull())
                        {
                            query.ProcessingStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.Complete;
                            query.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;

                            query.UpdatedBy = query.UpdatedBy;
                            query.UpdatedDate = DateTime.Now;
                            //query.DocumentCancelledDate = DateTime.Now;

                            db.Entry(query).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }

                        //if invoice already generated intiate cancellation for that
                        var invoices = db.AssetTransactionHeads.Where(x => x.ReferenceHeadID == headID);

                        foreach (var invoice in invoices)
                        {
                            if (invoice.ProcessingStatusID == (byte)Eduegate.Framework.Enums.TransactionStatus.Complete)
                            {
                                var prrDocument = new SettingRepository().GetSettingDetail("PRRDOCUMENTTYPEID", query.CompanyID.Value);

                                if (prrDocument != null)
                                {
                                    AssetTransactionHead newPRR = CloneTransaction(headID);
                                    newPRR.HeadIID = 0;
                                    newPRR.DocumentTypeID = int.Parse(prrDocument.SettingValue);
                                    newPRR.EntryDate = DateTime.Now;
                                    newPRR.TransactionNo = GetNextTransactionNumber(newPRR.DocumentTypeID.Value);
                                    newPRR.DocumentStatusID = (int)Eduegate.Services.Contracts.Enums.DocumentStatuses.Approved;
                                    newPRR.ProcessingStatusID = (int)Eduegate.Framework.Enums.TransactionStatus.New;
                                    newPRR.ReferenceHeadID = invoice.HeadIID;
                                    db.AssetTransactionHeads.Add(newPRR);
                                    db.Entry(newPRR).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                                    //if (newPRR.TransactionHeadEntitlementMaps != null)
                                    //{
                                    //    foreach (var entitlment in newPRR.TransactionHeadEntitlementMaps)
                                    //    {
                                    //        db.Entry(entitlment).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                    //    }
                                    //}

                                    //if (newPRR.OrderContactMaps != null)
                                    //{
                                    //    foreach (var contact in newPRR.OrderContactMaps)
                                    //    {
                                    //        db.Entry(contact).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                    //    }
                                    //}

                                    if (newPRR.AssetTransactionDetails != null)
                                    {
                                        foreach (var detail in newPRR.AssetTransactionDetails)
                                        {
                                            db.Entry(detail).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                invoice.ProcessingStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.Complete;
                                invoice.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;
                                invoice.UpdatedBy = query.UpdatedBy;
                                invoice.UpdatedDate = DateTime.Now;
                                db.Entry(invoice).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                                foreach (var detail in invoice.AssetTransactionDetails)
                                {
                                    foreach (var serial in detail.AssetTransactionSerialMaps)
                                    {
                                        var inventory = db.AssetSerialMaps.Where(a => a.SerialNumber == serial.SerialNumber).AsNoTracking().FirstOrDefault();
                                        inventory.IsActive = false;

                                        db.Entry(inventory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                    }
                                }
                            }
                        }

                        // cancel all the jobs
                        //var jobs = db.JobEntryHeads.Where(x => x.TransactionHeadID == headID);

                        //foreach (var job in jobs)
                        //{
                        //    job.JobStatusID = (int)Eduegate.Services.Contracts.Enums.JobStatuses.Cancel;
                        //    db.Entry(job).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        //}

                        db.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        var abc = ex;
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public bool CancelPurchaseInvoiceTransaction(long headID)
        {
            using (var db = new dbEduegateAccountsContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var query = db.AssetTransactionHeads.Where(x => x.HeadIID == headID)
                            .AsNoTracking()
                            .FirstOrDefault();

                        if (query.IsNotNull())
                        {
                            //generate a sales return request
                            query.ProcessingStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.Complete;
                            query.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;

                            query.UpdatedBy = query.UpdatedBy;
                            query.UpdatedDate = DateTime.Now;
                            //query.DocumentCancelledDate = DateTime.Now;

                            db.Entry(query).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                            foreach (var detail in db.AssetTransactionDetails.Where(x => x.HeadID == headID))
                            {
                                foreach (var serial in detail.AssetTransactionSerialMaps)
                                {
                                    var inventory = db.AssetSerialMaps.Where(a => a.SerialNumber == serial.SerialNumber)
                                        .AsNoTracking()
                                        .FirstOrDefault();
                                    inventory.IsActive = false;

                                    db.Entry(inventory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                }
                            }

                            // cancel all the jobs
                            //var jobs = db.JobEntryHeads.Where(x => x.TransactionHeadID == headID);

                            //foreach (var job in jobs)
                            //{
                            //    job.JobStatusID = (int)Eduegate.Services.Contracts.Enums.JobStatuses.Cancel;
                            //    db.Entry(job).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            //}

                            //Create a PRR
                            var prrDocument = new SettingRepository().GetSettingDetail("PRRDOCUMENTTYPEID", query.CompanyID.Value);

                            if (prrDocument != null)
                            {
                                AssetTransactionHead newPRR = CloneTransaction(headID);
                                newPRR.HeadIID = 0;
                                newPRR.DocumentTypeID = int.Parse(prrDocument.SettingValue);
                                newPRR.EntryDate = DateTime.Now;
                                newPRR.TransactionNo = GetNextTransactionNumber(newPRR.DocumentTypeID.Value);
                                newPRR.DocumentStatusID = (int)Eduegate.Services.Contracts.Enums.DocumentStatuses.Approved;
                                newPRR.ProcessingStatusID = (int)Eduegate.Framework.Enums.TransactionStatus.New;
                                newPRR.ReferenceHeadID = query.HeadIID;
                                db.AssetTransactionHeads.Add(newPRR);
                                db.Entry(newPRR).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                                //if (newPRR.TransactionHeadEntitlementMaps != null)
                                //{
                                //    foreach (var entitlment in newPRR.TransactionHeadEntitlementMaps)
                                //    {
                                //        db.Entry(entitlment).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                //    }
                                //}

                                //if (newPRR.OrderContactMaps != null)
                                //{
                                //    foreach (var contact in newPRR.OrderContactMaps)
                                //    {
                                //        db.Entry(contact).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                //    }
                                //}

                                if (newPRR.AssetTransactionDetails != null)
                                {
                                    foreach (var detail in newPRR.AssetTransactionDetails)
                                    {
                                        db.Entry(detail).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                    }
                                }
                            }
                        }

                        db.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
        public AssetTransactionHead GetDeliveryDetails(long Id)
        {
            using (var db = new dbEduegateAccountsContext())
            {
                var entity = db.AssetTransactionHeads.Where(a => a.HeadIID == Id)
                    //.Include(x => x.OrderContactMaps)
                    .AsNoTracking()
                    .FirstOrDefault();
                return entity;
            }
        }

        //public List<DeliveryTypes1> GetDeliveryOptions()
        //{
        //    using (var dbContext = new dbEduegateAccountsContext())
        //    {
        //        return dbContext.DeliveryTypes1
        //            .AsNoTracking()
        //            .ToList();
        //    }
        //}

        public bool UpdateProduceSerialKeyStatus(List<string> serialKeys, bool usedFlag)
        {
            using (var db = new dbEduegateAccountsContext())
            {
                try
                {
                    foreach (var serialMap in db.AssetSerialMaps.Where(a => serialKeys.Contains(a.SerialNumber)))
                    {
                        serialMap.IsActive = usedFlag;

                        db.Entry(serialMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }

                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        //public bool IsChangeRequestDetailProcessed(long changeRequestDetailID)
        //{
        //    using (dbEduegateAccountsContext db = new dbEduegateAccountsContext())
        //    {
        //        return db.AssetTransactionDetails.Where(d => d.ParentDetailID == changeRequestDetailID)
        //            .AsNoTracking()
        //            .Any();
        //    }
        //}

        public AssetTransactionDTO GetTransactionDTO(long headIID)
        {
            AssetTransactionDTO transactionDTO = new AssetTransactionDTO();
            transactionDTO.AssetTransactionHead = new AssetTransactionHeadDTO();
            transactionDTO.AssetTransactionDetails = new List<AssetTransactionDetailsDTO>();
            AssetTransactionDetailsDTO transactionDetailDTO = null;

            using (var dbContext = new dbEduegateAccountsContext())
            {
                var transactionModel = dbContext.AssetTransactionHeads
                    .Include(x => x.AssetTransactionDetails)
                    //.Include(x => x.AssetTransactionSerialMaps)
                    .Include(x => x.AssetTransactionDetails)
                    //.Include(x => x.ProductSKUMap)
                    //.Include(x => x.Supplier)
                    //.Include(x => x.Currency)
                    //.Include(x => x.Customer)
                    .Include(x => x.DocumentType)
                    //.Include(x => x.TransactionShipments)
                    //.Include(x => x.TransactionHead2)
                    .AsNoTracking()
                    .FirstOrDefault(x => x.HeadIID == headIID);

                if (transactionModel != null)
                {
                    transactionDTO.AssetTransactionHead.HeadIID = transactionModel.HeadIID;
                    //transactionDTO.AssetTransactionHead.Description = transactionModel.Description;
                    transactionDTO.AssetTransactionHead.Reference = transactionModel.Reference;
                    //transactionDTO.AssetTransactionHead.CustomerID = transactionModel.CustomerID;
                    //transactionDTO.AssetTransactionHead.SupplierLogiID = transactionModel.Supplier?.LoginID;
                    transactionDTO.AssetTransactionHead.EntryDate = transactionModel.EntryDate.IsNull() ? (DateTime?)null : Convert.ToDateTime(transactionModel.EntryDate);
                    transactionDTO.AssetTransactionHead.TransactionNo = transactionModel.TransactionNo;
                    //transactionDTO.AssetTransactionHead.ReferenceTransactionNo = transactionModel.TransactionHead2 != null ? transactionModel.TransactionHead2.TransactionNo : null ;
                    transactionDTO.AssetTransactionHead.ProcessingStatusID = transactionModel.ProcessingStatusID;
                    transactionDTO.AssetTransactionHead.DocumentStatusID = transactionModel.DocumentStatusID;
                    transactionDTO.AssetTransactionHead.DocumentTypeID = transactionModel.DocumentTypeID;
                    //transactionDTO.AssetTransactionHead.DiscountAmount = transactionModel.DiscountAmount;
                    //transactionDTO.AssetTransactionHead.DiscountPercentage = transactionModel.DiscountPercentage;
                    transactionDTO.AssetTransactionHead.BranchID = transactionModel.BranchID != null ? (long)transactionModel.BranchID : default(long);
                    transactionDTO.AssetTransactionHead.ToBranchID = transactionModel.ToBranchID != null ? (long)transactionModel.ToBranchID : default(long);
                    //transactionDTO.AssetTransactionHead.CurrencyID = transactionModel.CurrencyID != null ? (int)transactionModel.CurrencyID : default(int);
                    //transactionDTO.AssetTransactionHead.DeliveryDate = transactionModel.DeliveryDate != null ? (DateTime)transactionModel.DeliveryDate : (DateTime?)null;
                    //transactionDTO.AssetTransactionHead.DeliveryTypeID = transactionModel.DeliveryMethodID != null ? (short)transactionModel.DeliveryMethodID : default(short);
                    //transactionDTO.AssetTransactionHead.DueDate = transactionModel.DueDate != null ? (DateTime)transactionModel.DueDate : (DateTime?)null;
                    //transactionDTO.AssetTransactionHead.EntitlementID = transactionModel.EntitlementID;
                    //transactionDTO.AssetTransactionHead.IsShipment = transactionModel.IsShipment != null ? (bool)transactionModel.IsShipment : default(bool);
                    transactionDTO.AssetTransactionHead.ReferenceHeadID = transactionModel.ReferenceHeadID;
                    //transactionDTO.AssetTransactionHead.DocumentTypeName = transactionModel.DocumentType?.TransactionTypeName ?? default(string);

                    //var supplier = transactionModel.Supplier;

                    //if (supplier != null)
                    //    transactionDTO.AssetTransactionHead.SupplierName = string.Concat(supplier.FirstName + " " + supplier.MiddleName + " " + supplier.LastName);

                    //stored in list for common mail send functionality
                    //transactionDTO.AssetTransactionHead.SupplierList = new List<KeyValueDTO>();
                    //if (transactionModel.Supplier != null)
                    //{
                    //    transactionDTO.AssetTransactionHead.SupplierList.Add(new KeyValueDTO()
                    //    {
                    //        Key = transactionModel.Supplier.SupplierIID.ToString(),
                    //        Value = transactionModel.Supplier.FirstName.ToString()
                    //    });
                    //}

                    //var currency = transactionModel.Currency;

                    //if (currency != null)
                    //    transactionDTO.AssetTransactionHead.CurrencyName = currency.Name;

                    //var customer = transactionModel.Customer;

                    //if (customer != null)
                    //    transactionDTO.AssetTransactionHead.CustomerName = string.Concat(customer.FirstName + customer.MiddleName + " " + customer.LastName);

                    if (transactionModel.AssetTransactionDetails != null && transactionModel.AssetTransactionDetails.Count > 0)
                    {
                        foreach (var transactionDetail in transactionModel.AssetTransactionDetails)
                        {
                            transactionDetailDTO = new AssetTransactionDetailsDTO();
                            //transactionDetailDTO.SKUDetails = new List<ProductSerialMapDTO>();

                            transactionDetailDTO.DetailIID = transactionDetail.DetailIID;
                            transactionDetailDTO.HeadID = transactionDetail.HeadID;
                            //transactionDetailDTO.ProductID = transactionDetail.ProductID;

                            try
                            {
                                //var searchQuery = string.Concat("select * from [catalog].[ProductListBySKU] where productskumapiid = ", transactionDetail.ProductSKUMapID);
                                //var product = dbContext.ProductListBySKUs.FromSqlRaw($@"{searchQuery}")
                                //    .AsNoTracking()
                                //    .FirstOrDefault();

                                //if (product != null)
                                //{
                                //    transactionDetailDTO.ProductName = product.SKU;
                                //    transactionDetailDTO.ImageFile = product.ImageFile;
                                //    transactionDetailDTO.SellingQuantityLimit = product.Quantity;

                                //    if (product.Quantity != null && product.SellingQuantityLimit != null)
                                //    {
                                //        if (product.SellingQuantityLimit > product.Quantity)
                                //            transactionDetailDTO.SellingQuantityLimit = product.SellingQuantityLimit;
                                //    }
                                //}
                            }
                            catch (Exception ex)
                            {

                            }

                            //transactionDetailDTO.ProductSKUMapID = transactionDetail.ProductSKUMapID;
                            //transactionDetailDTO.UnitID = transactionDetail.UnitID;
                            //transactionDetailDTO.UnitPrice = transactionDetail.UnitPrice;
                            //transactionDetailDTO.Quantity = transactionDetail.Quantity;
                            //transactionDetailDTO.DiscountPercentage = transactionDetail.DiscountPercentage;
                            transactionDetailDTO.Amount = transactionDetail.Amount;

                            if (transactionDetail.AssetTransactionSerialMaps != null && transactionDetail.AssetTransactionSerialMaps.Count > 0)
                            {
                                foreach (var serailMap in transactionDetail.AssetTransactionSerialMaps)
                                {
                                    var skuDetail = new AssetTransactionSerialMapDTO();
                                    skuDetail.SerialNumber = serailMap.SerialNumber;
                                    //transactionDetailDTO.SKUDetails.Add(skuDetail);
                                }
                            }
                            transactionDTO.AssetTransactionDetails.Add(transactionDetailDTO);
                        }
                    }

                    //if (transactionModel.TransactionShipments.IsNotNull() && transactionModel.TransactionShipments.Count > 0)
                    //{
                    //    foreach (var ts in transactionModel.TransactionShipments)
                    //    {
                    //        transactionDTO.ShipmentDetails = new ShipmentDetailDTO()
                    //        {
                    //            TransactionShipmentIID = ts.TransactionShipmentIID,
                    //            TransactionHeadID = ts.TransactionHeadID,
                    //            SupplierIDFrom = ts.SupplierIDFrom,
                    //            SupplierIDTo = ts.SupplierIDTo,
                    //            ShipmentReference = ts.ShipmentReference,
                    //            FreightCareer = ts.FreightCarrier,
                    //            ClearanceTypeID = ts.ClearanceTypeID,
                    //            AirWayBillNo = ts.AirWayBillNo,
                    //            FrieghtCharges = ts.FreightCharges,
                    //            BrokerCharges = ts.BrokerCharges,
                    //            AdditionalCharges = ts.AdditionalCharges,
                    //            Weight = ts.Weight,
                    //            NoOfBoxes = ts.NoOfBoxes,
                    //            BrokerAccount = ts.BrokerAccount,
                    //            Remarks = ts.Description,
                    //            CreatedBy = ts.CreatedBy,
                    //            UpdatedBy = ts.UpdatedBy,
                    //            CreatedDate = ts.CreatedDate,
                    //            UpdatedDate = ts.UpdatedDate,
                    //            //TimeStamps = ts.TimeStamps,
                    //        };
                    //    }
                    //}
                }
            }

            return transactionDTO;
        }

        public DocumentType GetDocumentType(int documentTypeID)
        {
            using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
            {
                return dbContext.DocumentTypes
                    .Where(a => a.DocumentTypeID == documentTypeID)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }

        public DocumentType SaveDocumentType(DocumentType entity)
        {
            DocumentType updatedEntity = null;

            try
            {
                using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
                {
                    // get maximum ID from DocumentTypes
                    if (entity.DocumentTypeID <= 0)
                    {
                        var documentTypeId = dbContext.DocumentTypes.Max(x => (int?)x.DocumentTypeID);
                        documentTypeId = (documentTypeId.HasValue ? documentTypeId : 0) + 1;
                        entity.DocumentTypeID = documentTypeId.Value;
                    }

                    dbContext.DocumentTypes.Add(entity);

                    if (entity.DocumentTypeID > 0 && dbContext.DocumentTypes.Any(x => x.DocumentTypeID == entity.DocumentTypeID))
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    else
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                    //if (entity.Workflow != null)
                    //{
                    //    dbContext.Entry(entity.Workflow).State = EntityState.Unchanged;
                    //}

                    dbContext.SaveChanges();
                    updatedEntity = dbContext.DocumentTypes
                        //.Include(a => a.Workflow)
                        .Where(x => x.DocumentTypeID == entity.DocumentTypeID).AsNoTracking().FirstOrDefault();
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<AssetTransactionRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return updatedEntity;
        }

        public DocumentReferenceType GetDocumentReferenceTypesByDocumentType(int documentTypeID)
        {
            using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
            {
                return (from drt in dbContext.DocumentReferenceTypes
                        join dt in dbContext.DocumentTypes on drt.ReferenceTypeID equals dt.ReferenceTypeID
                        where dt.DocumentTypeID == documentTypeID
                        select drt).SingleOrDefault();
            }
        }

        public DocumentReferenceStatusMap GetDocumentReferenceStatusMap(int documentReferenceTypeID)
        {
            try
            {
                using (dbEduegateAccountsContext db = new dbEduegateAccountsContext())
                {
                    return db.DocumentReferenceStatusMaps.Where(d => d.DocumentReferenceStatusMapID == documentReferenceTypeID)
                        .Include(d => d.DocumentStatus)
                        .AsNoTracking()
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the banners", Framework.Helper.TrackingCode.ERP);
                throw ex;
            }
        }

        public bool UpdateAssetSerialMapsInventoryID(long serialMapID, AssetInventoryDTO assetInventory)
        {
            bool ret = false;
             
            using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
            {
                try
                {
                    var assetSerialMap = dbContext.AssetSerialMaps.Where(i => i.AssetSerialMapIID == serialMapID).FirstOrDefault();

                    assetSerialMap.AssetInventoryID = assetInventory.AssetInventoryIID;
                    assetSerialMap.CutOffDate = assetInventory.CutOffDate;
                    assetSerialMap.UpdatedBy = assetInventory.UpdatedBy;
                    assetInventory.UpdatedDate = DateTime.Now;

                    //dbContext.AssetSerialMaps.Add(assetSerialMap);

                    dbContext.Entry(assetSerialMap).State = EntityState.Modified;

                    dbContext.SaveChanges();
                    ret = true;
                }
                catch (Exception ex)
                {
                    ret = false;
                    SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to update AssetSerialMaps AssetInventoryID. AssetSerialMapID:" + serialMapID.ToString() + " and AssetInventoryID:" + assetInventory.AssetInventoryIID, TrackingCode.TransactionEngine);
                    throw;
                }
            }

            return ret;
        }

        public bool UpdateAssetSerialMapsStatus(long serialMapID, AssetInventoryDTO assetInventory)
        {
            bool result = false;

            using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
            {
                try
                {
                    var assetSerialMap = dbContext.AssetSerialMaps.Where(i => i.AssetSerialMapIID == serialMapID).FirstOrDefault();

                    assetSerialMap.AssetInventoryID = assetInventory.AssetInventoryIID;
                    assetSerialMap.IsActive = false;
                    assetSerialMap.IsDisposed = assetInventory.IsSerialMapDisposed;
                    assetSerialMap.UpdatedBy = assetInventory.UpdatedBy;
                    assetInventory.UpdatedDate = DateTime.Now;

                    dbContext.Entry(assetSerialMap).State = EntityState.Modified;

                    dbContext.SaveChanges();
                    result = true;
                }
                catch (Exception ex)
                {
                    result = false;
                    SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to update AssetSerialMaps AssetInventoryID and Active status. AssetSerialMapID:" + serialMapID.ToString() + " and AssetInventoryID:" + assetInventory.AssetInventoryIID, TrackingCode.TransactionEngine);
                    throw;
                }
            }

            return result;
        }

    }
}
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using Eduegate.Domain.Entity.CustomEntity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity.Models.Inventory;
using Eduegate.Domain.Entity.Models.ValueObjects;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Logs;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Domain.Entity;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Framework;
using Eduegate.Framework.Translator;
using System.Globalization;
using Eduegate.Services.Contracts.Inventory;
using Microsoft.IdentityModel.Tokens;

namespace Eduegate.Domain.Repository
{
    public class TransactionRepository
    {

        public IDictionary<string, string> GetCustomerSummary(long referenceId)
        {
            using (var db = new dbEduegateERPContext())
            {
                var transaction = new Dictionary<string, string>();
                transaction.Add("LastMonthTotalOrders",
                    db.TransactionHeads.Where(x => x.CustomerID == referenceId
                        && x.TransactionDate.Value.Month == DateTime.Now.Month
                        && x.TransactionDate.Value.Year == DateTime.Now.Year).Count().ToString());

                //transaction.Add("LastMonthTotalSales",
                //    db.TransactionHeads.Where(x => x.CustomerID == referenceId
                //        && x.TransactionDate.Value.Month == DateTime.Now.Month
                //        && x.TransactionDate.Value.Year == DateTime.Now.Year).Sum(x => x.PaidAmount).ToString());

                transaction.Add("TotalOrders",
                    db.TransactionHeads.Where(x => x.CustomerID == referenceId).Count().ToString());

                //transaction.Add("TotalSales",
                //    db.TransactionHeads.Where(x => x.CustomerID == referenceId).Sum(x => x.PaidAmount).ToString());

                return transaction;
            }
        }

        /// <summary>
        /// get all the transaction based on DocumentReferenceTypes and TransactionStatus
        /// </summary>
        /// <param name="referenceTypes">enum</param>
        /// <param name="transactionStatus">enum</param>
        /// <returns>list of TransactionHead</returns>
        public List<TransactionHead> GetAllTransaction(DocumentReferenceTypes referenceTypes, Eduegate.Framework.Enums.TransactionStatus transactionStatus)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {

                return (from th in db.TransactionHeads
                        join td in db.TransactionDetails on th.HeadIID equals td.HeadID
                        join dt in db.DocumentTypes on th.DocumentTypeID equals dt.DocumentTypeID
                        join rt in db.DocumentReferenceTypes on dt.ReferenceTypeID equals rt.ReferenceTypeID
                        join ta in db.TransactionAllocations on td.DetailIID equals ta.TrasactionDetailID into tdljta
                        from ta in tdljta.DefaultIfEmpty()
                        where
                        (rt.ReferenceTypeID == (int)(DocumentReferenceTypes)referenceTypes)
                        && td.ProductSKUMapID.HasValue
                        // check if TransactionStatusID is New(1)
                        && ((int)Eduegate.Framework.Enums.TransactionStatus.New == (int)(Eduegate.Framework.Enums.TransactionStatus)transactionStatus ?
                            (th.TransactionStatusID == (int)(Eduegate.Framework.Enums.TransactionStatus)transactionStatus || th.TransactionStatusID.Equals(null)) :
                                th.TransactionStatusID == (int)(Eduegate.Framework.Enums.TransactionStatus)transactionStatus)
                        select th)
                             .Include(h => h.TransactionDetails.Select(d => d.TransactionAllocations))
                             .Include(h => h.TransactionDetails.Select(d => d.ProductSerialMaps))
                             .Include(h => h.TransactionHeadEntitlementMaps)
                             .Include(h => h.EntityTypeEntitlement)
                             .Include(h => h.DocumentType)
                             .Include(h => h.TransactionHeadEntitlementMaps.Select(d => d.EntityTypeEntitlement))
                             .Include(x => x.TransactionHeadShoppingCartMaps.Select(y => y.ShoppingCart.ShoppingCartVoucherMaps.Select(z => z.Voucher)))
                             .Include(x => x.Customer)
                             .Include(x => x.TaxTransactions)
                             .Take(new Domain.Setting.SettingBL(null).GetSettingValue<int>("MaxFetchCount"))
                             .Distinct()
                             .AsNoTracking()
                             .ToList();
            }
        }

        /// <summary>
        /// after getting the data based on New status update the quantity in ProductInventories table
        /// </summary>
        /// <param name="productSKUMapID">long datatype</param>
        /// <param name="quantity">decimal datatype</param>
        /// <returns>if success true else false</returns>
        public bool IncreaseProductInventory(ProductInventory productInventory, dbEduegateERPContext dbContext = null)
        {
            bool ret = false;

            using (dbEduegateERPContext db = (dbContext == null ? new dbEduegateERPContext() : dbContext))
            {
                try
                {
                    var maxMatchID = db.ProductInventories.Where(i => i.ProductSKUMapID == productInventory.ProductSKUMapID && i.BranchID == productInventory.BranchID)
                        .Max(a => (long?)a.Batch) ?? 0;
                    productInventory.Batch = maxMatchID + 1;
                    productInventory.Quantity = (productInventory.Quantity);
                    productInventory.CreatedDate = DateTime.Now;
                    productInventory.OriginalQty = productInventory.OriginalQty;
                    productInventory.CostPrice = productInventory.CostPrice;
                    productInventory.HeadID = productInventory.HeadID;
                    productInventory.isActive = productInventory.isActive;
                    if (productInventory.BranchID == null)
                    {
                        productInventory.BranchID = 1; //the default branch
                    }

                    db.ProductInventories.Add(productInventory);

                    db.SaveChanges();
                    ret = true;
                }
                catch (Exception ex)
                {
                    ret = false;
                    SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to update ProductInventories. ProductSKUMapID:" + productInventory.ProductSKUMapID.ToString(), TrackingCode.TransactionEngine);
                    throw;
                }
            }

            return ret;
        }

        public bool DecreaseProductInventory(ProductInventory productInventory, dbEduegateERPContext dbContext = null)
        {
            bool ret = false;

            using (dbEduegateERPContext db = (dbContext == null ? new dbEduegateERPContext() : dbContext))
            {
                try
                {
                    //if the inventory is available from multiple batches it should deduct from the first and remaing goes to next
                    var inventories = db.ProductInventories.Where(i => i.ProductSKUMapID == productInventory.ProductSKUMapID
                        && i.BranchID == productInventory.BranchID).OrderBy(a => a.Batch).AsNoTracking();

                    if (inventories.IsNotNull())
                    {
                        var descreaseTotalQuantity = productInventory.Quantity;

                        foreach (var inventory in inventories)
                        {
                            var descreaseQuantity = descreaseTotalQuantity <= inventory.Quantity ? descreaseTotalQuantity : inventory.Quantity;
                            inventory.Quantity = inventory.Quantity - descreaseQuantity;
                            descreaseTotalQuantity -= descreaseQuantity;
                            inventory.UpdatedDate = DateTime.Now;

                            db.Entry(inventory).State = EntityState.Modified;

                            if (descreaseTotalQuantity <= 0)
                                break;
                        }

                        if (descreaseTotalQuantity != 0)
                        {
                            throw new Exception("Inventory not available to reduce. ProductSKUMapID:" + productInventory.ProductSKUMapID.ToString());
                        }
                    }
                    else
                    {
                        throw new Exception("Inventory not available to reduce. ProductSKUMapID:" + productInventory.ProductSKUMapID.ToString());
                    }

                    db.SaveChanges();
                    ret = true;
                }
                catch (Exception ex)
                {
                    ret = false;
                    SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to update ProductInventories. ProductSKUMapID:" + productInventory.ProductSKUMapID.ToString(), TrackingCode.TransactionEngine);
                    throw new Exception("Not able to update ProductInventories. ProductSKUMapID:" + productInventory.ProductSKUMapID.ToString());
                }
            }

            return ret;
        }

        public bool UpdateProductInventory(ProductInventory productInventory)
        {
            bool ret = false;

            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                try
                {
                    /** Commented existing logic;
                        from now on every purchase will be just added with new BatchID 
                    **/
                    ProductInventory query = db.ProductInventories.Where(i => i.ProductSKUMapID == productInventory.ProductSKUMapID && i.Batch == productInventory.Batch && i.BranchID == productInventory.BranchID)
                        .AsNoTracking()
                        .FirstOrDefault();

                    if (query == null)
                        db.ProductInventories.Add(productInventory);
                    else
                        query.Quantity = productInventory.Quantity;

                    db.SaveChanges();
                    ret = true;
                }
                catch (Exception ex)
                {
                    ret = false;
                    SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to update ProductInventories. ProductSKUMapID:" + productInventory.ProductSKUMapID.ToString(), TrackingCode.TransactionEngine);
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
        public bool UpdateInvetoryTransactions(InvetoryTransaction invetoryTransaction)
        {
            bool isSuccess = false;
            dbEduegateERPContext db = new dbEduegateERPContext();

            try
            {
                // Add InvetoryTransactions
                db.InvetoryTransactions.Add(invetoryTransaction);
                db.SaveChanges();

                if (invetoryTransaction.InventoryTransactionIID > 0)
                {
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to Add InvetoryTransactions.", TrackingCode.TransactionEngine);
                throw;
            }
            return isSuccess;
        }

        public bool updateAgainstGRNInvetoryTransactions(InvetoryTransaction invetoryTransaction, long? referenceHeadID)
        {
            bool isSuccess = false;
            dbEduegateERPContext db = new dbEduegateERPContext();

            var getTrans = db.InvetoryTransactions.Where(x => x.HeadID == referenceHeadID && x.ProductSKUMapID == invetoryTransaction.ProductSKUMapID)
                .AsNoTracking()
                .FirstOrDefault();

            try
            {
                // update InvetoryTransactions  ::: Cost, Amount, Rate, Discount, CurrencyID, ExchangeRate
                getTrans.Cost = invetoryTransaction.Cost;
                getTrans.Amount = invetoryTransaction.Amount;
                getTrans.Rate = invetoryTransaction.Rate;
                getTrans.Discount = invetoryTransaction.Discount;
                getTrans.CurrencyID = invetoryTransaction.CurrencyID;
                getTrans.ExchangeRate = invetoryTransaction.ExchangeRate;
                getTrans.LastCostPrice = invetoryTransaction.LastCostPrice != null ? invetoryTransaction.LastCostPrice : null;
                getTrans.LandingCost = invetoryTransaction.LandingCost != null ? invetoryTransaction.LandingCost : null;

                db.Entry(getTrans).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();

                if (getTrans.InventoryTransactionIID > 0)
                {
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to Update InvetoryTransactions.", TrackingCode.TransactionEngine);
                throw;
            }
            return isSuccess;
        }

        public bool updateProductInventoryCostPrice(InvetoryTransaction invetoryTransaction, long? referenceHeadID)
        {
            bool isSuccess = false;
            dbEduegateERPContext db = new dbEduegateERPContext();

            var getPrdctInventories = db.ProductInventories.Where(x => x.HeadID == referenceHeadID && x.ProductSKUMapID == invetoryTransaction.ProductSKUMapID)
                .AsNoTracking().FirstOrDefault();

            try
            {
                // update CostPrice in product inventory
                getPrdctInventories.CostPrice = invetoryTransaction.Rate;
                db.Entry(getPrdctInventories).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                isSuccess = true;
            }
            catch (Exception ex)
            {
                isSuccess = false;
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to Update InvetoryTransactions.", TrackingCode.TransactionEngine);
                throw;
            }
            return isSuccess;
        }



        /// <summary>
        /// after getting success of AddInvetoryTransactions method we have to update status in TransactionHead table from New to InProgress
        /// </summary>
        /// <param name="headId">long datatype</param>
        /// <param name="transactionStatusId">byte datatype</param>
        /// <returns>boolean value </returns>
        public bool UpdateTransactionHead(TransactionHead transactionHead)
        {
            bool isSuccess = false;
            dbEduegateERPContext db = new dbEduegateERPContext();

            try
            {
                TransactionHead query = db.TransactionHeads.Where(x => x.HeadIID == transactionHead.HeadIID)
                    .AsNoTracking().FirstOrDefault();

                if (query.IsNotNull())
                {
                    if (query.DocumentStatusID == (byte)Eduegate.Services.Contracts.Enums.TransactionStatus.Returned ||
                        query.DocumentStatusID == (byte)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled ||
                        query.DocumentStatusID == (byte)Eduegate.Services.Contracts.Enums.DocumentStatuses.PartialReturn)
                        return isSuccess;

                    query.TransactionStatusID = transactionHead.TransactionStatusID;

                    query.DocumentStatusID = transactionHead.DocumentStatusID;

                    if (!string.IsNullOrEmpty(transactionHead.Description))
                    {
                        query.Description = (string.IsNullOrEmpty(transactionHead.Description) ? string.Empty : transactionHead.Description + ",") + transactionHead.Description;
                    }

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
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not ablet to update TransactionHead. HeadIID:" + transactionHead.HeadIID.ToString(), TrackingCode.TransactionEngine);
                throw;
            }
            return isSuccess;
        }


        /// <summary>
        /// get the ProductInventory detail based on ProductSKUMapID
        /// </summary>
        /// <param name="productInventory">ProductInventory object</param>
        /// <returns>object of ProductInventory</returns>
        public ProductInventory GetProductInventoryDetail(ProductInventory productInventory)
        {
            dbEduegateERPContext db = new dbEduegateERPContext();
            try
            {
                ProductInventory query = db.ProductInventories.Where(x => x.ProductSKUMapID == productInventory.ProductSKUMapID)
                    .AsNoTracking()
                    .FirstOrDefault();
                return query;
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not ablet to update ProductInventories. ProductSKUMapID:" + productInventory.ProductSKUMapID.ToString(), TrackingCode.TransactionEngine);
                throw;
            }
        }


        /// <summary>
        /// get the quantity based on productSKUMapID
        /// </summary>
        /// <param name="productSKUMapID"> long datatype</param>
        /// <returns>quantity</returns>
        public decimal CheckAvailibility(long branchID, long productSKUMapID)
        {
            decimal quantity = 0;

            dbEduegateERPContext db = new dbEduegateERPContext();
            try
            {
                //ProductInventory query = db.ProductInventories.FirstOrDefault(x => x.BranchID == branchID && x.ProductSKUMapID == productSKUMapID);
                //quantity = query == null ? 0 : Convert.ToDecimal(query.Quantity);
                var totalQuantity = db.ProductInventories.Where(x => x.BranchID == branchID && x.ProductSKUMapID == productSKUMapID).AsNoTracking().Sum(a => a.Quantity);
                quantity = totalQuantity == null ? 0 : totalQuantity.Value;
            }
            catch (Exception ex)
            {
                quantity = 0;
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get ProductInventories. ProductSKUMapID:" + productSKUMapID.ToString(), TrackingCode.TransactionEngine);
                throw;
            }

            return quantity;
        }

        public bool CheckAvailibility(long? branchID, List<TransactionDetailDTO> transactions)
        {
            bool isSuccess = true;
            foreach (var transaction in transactions)
            {
                // Convert into decimal
                var quantity = CheckAvailibility(branchID.Value, transaction.ProductSKUMapID.Value);

                if (transaction.Quantity > quantity)
                {
                    return isSuccess = false;
                }
            }

            return isSuccess;
        }

        public TransactionDetailDTO CheckAvailibilityBundleItem(long branchID, List<TransactionDetailDTO> transaction)
        {
            var transactionDTO = new TransactionDetailDTO();

            foreach (var trans in transaction)
            {
                var bundleItems = GetProductBundleItemDetails(trans.ProductSKUMapID.Value);
                //0. Check Quantity

                transactionDTO = CheckAvailibilityItems(branchID, trans.Quantity.Value, bundleItems);

                if (transactionDTO.IsError)
                {

                    break;
                }


            }
            return transactionDTO;
        }
        public TransactionDetailDTO CheckAvailibilityItems(long branchID, decimal bundleQuantity, List<ProductBundleDTO> bundleItems)
        {
            var transactionDTO = new TransactionDetailDTO();

            foreach (var transaction in bundleItems)
            {
                // Convert into decimal
                var quantity = CheckAvailibility(branchID, transaction.FromProductSKUMapID.Value);

                if ((transaction.Quantity * bundleQuantity) > quantity)
                {
                    transactionDTO = new TransactionDetailDTO();

                    transactionDTO.IsError = true;
                    transactionDTO.ProductName = transaction.FromProduct.Value;

                    return transactionDTO;
                }
            }

            return transactionDTO;
        }


        /// <summary>
        /// get the transaction detail based on Id
        /// </summary>
        /// <param name="headId">long datatype</param>
        /// <returns>return the single object of TransactionHead</returns>
        public TransactionHead GetTransactionDetail(long headId)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var result = db.TransactionHeads.Where(a => a.HeadIID == headId)
                             .Include(h => h.TransactionDetails).ThenInclude(d => d.TransactionAllocations)
                             .Include(h => h.TransactionDetails).ThenInclude(d => d.ProductSerialMaps)
                             .Include(h => h.TransactionHeadEntitlementMaps)
                             .Include(h => h.EntityTypeEntitlement)
                             .Include(h => h.TaxTransactions)
                             .Include(h => h.DocumentType)
                             .Include(h => h.TransactionHeadEntitlementMaps).ThenInclude(d => d.EntityTypeEntitlement)
                             .Include(x => x.TransactionHeadShoppingCartMaps).ThenInclude(y => y.ShoppingCart.ShoppingCartVoucherMaps).ThenInclude(z => z.Voucher)
                             .Include(x => x.Customer)
                             .AsNoTracking()
                    .FirstOrDefault();
                return result;
            }
        }

        public int? GetFinancialYearID(DateTime? transactionDate)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var result = db.FiscalYears.Where(a => a.StartDate <= transactionDate && a.EndDate >= transactionDate)
                             .AsNoTracking()
                    .FirstOrDefault()?.FiscalYear_ID;
                return result;
            }
        }

        /// <summary>
        /// Saving the Transaction with one transaction header and muliple transaction details 
        /// </summary>
        /// <param name="transaction">Transaction details</param>
        /// <returns>Updated transaction details</returns>

        public TransactionHead SaveTransactions(TransactionHead transaction, List<ProductInventorySerialMap> productInventorySerialMapsDigi = null)
        {
            try
            {
                //////////TTTTTTTTTTTOOOOOOOOODOOOOOOOOOOOOOOOOOOOOOOO
                ///Wrong way of doing it, it should be refactored
                /////////////TTTTTTTTTTTOOOOOOOOODOOOOOOOOOOOOOOOOOOOOOO

                if (transaction.IsNotNull())
                {
                    transaction.FiscalYear_ID = GetFinancialYearID(transaction.TransactionDate);
                    using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                    {

                        if (transaction.HeadIID <= 0)
                        {
                            dbContext.TransactionHeads.Add(transaction);
                        }
                        else
                        {
                            // get head from db and update
                            TransactionHead dbTransactionHead = dbContext.TransactionHeads.Where(x => x.HeadIID == transaction.HeadIID)
                                .Include(i => i.TransactionShipments)
                                .Include(i => i.TransactionHeadEntitlementMaps)
                                .Include(i => i.TransactionDetails).ThenInclude(i => i.TransactionAllocations)
                                .Include(i => i.TransactionDetails).ThenInclude(i => i.ProductSerialMaps)
                                .AsNoTracking()
                                .FirstOrDefault();

                            if (dbTransactionHead.IsNotNull())
                            {
                                dbTransactionHead.HeadIID = transaction.HeadIID;
                                dbTransactionHead.CompanyID = transaction.CompanyID;
                                dbTransactionHead.DocumentTypeID = transaction.DocumentTypeID;
                                dbTransactionHead.TransactionDate = transaction.TransactionDate;
                                dbTransactionHead.CustomerID = transaction.CustomerID;
                                dbTransactionHead.StudentID = transaction.StudentID;
                                dbTransactionHead.StaffID = transaction.StaffID;
                                dbTransactionHead.Description = transaction.Description;
                                dbTransactionHead.TransactionNo = transaction.TransactionNo;
                                dbTransactionHead.SupplierID = transaction.SupplierID;
                                dbTransactionHead.TransactionStatusID = transaction.TransactionStatusID;
                                dbTransactionHead.DocumentStatusID = transaction.DocumentStatusID;
                                dbTransactionHead.DiscountAmount = transaction.DiscountAmount;
                                dbTransactionHead.DiscountPercentage = transaction.DiscountPercentage;
                                dbTransactionHead.CreatedBy = transaction.CreatedBy;
                                dbTransactionHead.UpdatedBy = transaction.UpdatedBy;
                                dbTransactionHead.CreatedDate = transaction.CreatedDate;
                                dbTransactionHead.UpdatedDate = transaction.UpdatedDate;
                                dbTransactionHead.BranchID = transaction.BranchID;
                                dbTransactionHead.ToBranchID = transaction.ToBranchID;
                                dbTransactionHead.CurrencyID = transaction.CurrencyID;
                                dbTransactionHead.DeliveryDate = transaction.DeliveryDate;
                                dbTransactionHead.DeliveryMethodID = transaction.DeliveryMethodID;
                                dbTransactionHead.DeliveryTypeID = transaction.DeliveryTypeID;
                                dbTransactionHead.DueDate = transaction.DueDate;
                                dbTransactionHead.EntitlementID = transaction.EntitlementID;
                                dbTransactionHead.IsShipment = transaction.IsShipment;
                                dbTransactionHead.ReferenceHeadID = transaction.ReferenceHeadID;
                                //EmployeeID was not saving before added now
                                dbTransactionHead.EmployeeID = transaction.EmployeeID;
                                dbTransactionHead.DeliveryCharge = transaction.DeliveryCharge;
                                dbTransactionHead.JobStatusID = transaction.JobStatusID;
                                dbTransactionHead.ReceivingMethodID = transaction.ReceivingMethodID;
                                dbTransactionHead.ReturnMethodID = transaction.ReturnMethodID;
                                dbTransactionHead.Reference = transaction.Reference;
                                dbTransactionHead.CurrencyID = transaction.CurrencyID;
                                dbTransactionHead.ExchangeRate = transaction.ExchangeRate;
                                dbTransactionHead.TransactionDate = transaction.TransactionDate;
                                if (transaction.DocumentStatusID.Value == (int)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled)
                                {
                                    dbTransactionHead.DocumentCancelledDate = DateTime.Now;
                                }

                                dbContext.Entry(dbTransactionHead).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }

                            // add or update shipments
                            if (transaction.TransactionShipments.IsNotNull() && transaction.TransactionShipments.Count > 0)
                            {
                                foreach (TransactionShipment ts in transaction.TransactionShipments)
                                {
                                    if (ts.TransactionShipmentIID <= 0)
                                        dbContext.Entry(ts).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                    else
                                        dbContext.Entry(ts).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                }
                            }

                            // add or update TransactionHeadEntitlementMaps
                            if (transaction.TransactionHeadEntitlementMaps.IsNotNull() && transaction.TransactionHeadEntitlementMaps.Count > 0)
                            {
                                foreach (var transactionHeadEntitlementMap in transaction.TransactionHeadEntitlementMaps)
                                {
                                    if (transaction.HeadIID > 0)
                                    {
                                        transactionHeadEntitlementMap.TransactionHeadID = transaction.HeadIID;
                                    }

                                    if (transactionHeadEntitlementMap.TransactionHeadEntitlementMapIID > 0)
                                        dbContext.Entry(transactionHeadEntitlementMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                    else
                                        dbContext.Entry(transactionHeadEntitlementMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                }
                            }

                            // add or update order contact map
                            if (transaction.OrderContactMaps.IsNotNull() && transaction.OrderContactMaps.Count > 0)
                            {
                                foreach (OrderContactMap ocmEntity in transaction.OrderContactMaps)
                                {
                                    if (ocmEntity.OrderContactMapIID <= 0)
                                        dbContext.Entry(ocmEntity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                    else
                                        dbContext.Entry(ocmEntity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                }
                            }

                            if (transaction.TaxTransactions.IsNotNull() && transaction.TaxTransactions.Count > 0)
                            {
                                foreach (TaxTransaction tax in transaction.TaxTransactions)
                                {
                                    if (tax.TaxTransactionIID <= 0)
                                        dbContext.Entry(tax).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                    else
                                        dbContext.Entry(tax).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                }
                            }

                            //TransactionDetails won't be null if it is null we should not update DB atleast one record must be exist in DB.
                            if (transaction.TransactionDetails.IsNotNull() && transaction.TransactionDetails.Count > 0)
                            {
                                // newly added transaction detail lines 
                                List<TransactionDetail> transactionToInsert = transaction.TransactionDetails.Where(x => x.DetailIID == default(long)).ToList();

                                // collect modified transaction detail lines 
                                List<TransactionDetail> transactionToUpdate = (from transDetails in transaction.TransactionDetails
                                                                               join dbTransaction in dbTransactionHead.TransactionDetails on transDetails.DetailIID equals dbTransaction.DetailIID
                                                                               select transDetails).ToList();

                                var transDetailIIDs = new HashSet<long>(transaction.TransactionDetails.Select(x => x.DetailIID));

                                // collect transaction details to be deleted
                                List<TransactionDetail> transactionDetailsToDelete = dbTransactionHead.TransactionDetails.Where(x => !transDetailIIDs.Contains(x.DetailIID)).ToList();

                                // update transaction lines
                                if (transactionToUpdate != null)
                                {
                                    foreach (TransactionDetail tDetail in transactionToUpdate)
                                    {
                                        TransactionDetail transactionDetail = dbContext.TransactionDetails.Where(x => x.DetailIID == tDetail.DetailIID).FirstOrDefault();

                                        transactionDetail.DetailIID = tDetail.DetailIID;
                                        transactionDetail.HeadID = tDetail.HeadID;
                                        transactionDetail.ProductID = tDetail.ProductID;
                                        transactionDetail.ProductSKUMapID = tDetail.ProductSKUMapID;
                                        transactionDetail.Quantity = tDetail.Quantity;
                                        transactionDetail.Amount = tDetail.Quantity * tDetail.UnitPrice;
                                        transactionDetail.UnitPrice = tDetail.UnitPrice;
                                        transactionDetail.UnitID = tDetail.UnitID;
                                        transactionDetail.DiscountPercentage = tDetail.DiscountPercentage;
                                        transactionDetail.ExchangeRate = tDetail.ExchangeRate;
                                        transactionDetail.CreatedBy = tDetail.UpdatedBy;
                                        transactionDetail.UpdatedBy = tDetail.UpdatedBy;
                                        transactionDetail.UpdatedBy = tDetail.UpdatedBy;
                                        transactionDetail.UpdatedDate = tDetail.UpdatedDate;
                                        transactionDetail.TaxAmount1 = tDetail.TaxAmount1;
                                        transactionDetail.TaxAmount2 = tDetail.TaxAmount2;
                                        transactionDetail.HasTaxInclusive = tDetail.HasTaxInclusive;
                                        transactionDetail.ExclusiveTaxAmount = tDetail.ExclusiveTaxAmount;
                                        transactionDetail.InclusiveTaxAmount = tDetail.InclusiveTaxAmount;
                                        transactionDetail.TaxTemplateID = tDetail.TaxTemplateID;
                                        transactionDetail.LandingCost = tDetail.LandingCost;
                                        transactionDetail.LastCostPrice = tDetail.LastCostPrice;
                                        transactionDetail.Fraction = tDetail.Fraction;
                                        transactionDetail.ForeignRate = tDetail.ForeignRate;
                                        transactionDetail.ForeignAmount = tDetail.ForeignAmount;
                                        transactionDetail.ExchangeRate = tDetail.ExchangeRate;
                                        if (transactionDetail.ProductSerialMaps != null && transactionDetail.ProductSerialMaps.Count > 0)
                                        {
                                            dbContext.ProductSerialMaps.RemoveRange(transactionDetail.ProductSerialMaps);
                                        }

                                        if (transactionDetail.TransactionAllocations != null && transactionDetail.TransactionAllocations.Count > 0)
                                        {
                                            dbContext.TransactionAllocations.RemoveRange(transactionDetail.TransactionAllocations);
                                        }

                                        transactionDetail.ProductSerialMaps.Clear();
                                        transactionDetail.ProductSerialMaps = tDetail.ProductSerialMaps;

                                        if (tDetail.TransactionAllocations.IsNotNull() && tDetail.TransactionAllocations.Count > 0)
                                        {
                                            transactionDetail.TransactionAllocations = tDetail.TransactionAllocations.Where(a => a.Quantity > 0).ToList();
                                        }


                                    }
                                }

                                // isnert new transaction lines
                                if (transactionToInsert != null)
                                {
                                    foreach (TransactionDetail tDetail in transactionToInsert)
                                    {
                                        TransactionDetail transactionDetail = tDetail;
                                        transactionDetail.HeadID = dbTransactionHead.HeadIID;
                                        dbContext.TransactionDetails.Add(transactionDetail);
                                    }
                                }


                                // Delete serial maps and allocation if deleting transaction lines
                                List<ProductSerialMap> productSerialMapsToDelete = new List<ProductSerialMap>();
                                List<TransactionAllocation> transactionAllocationToDelete = new List<TransactionAllocation>();
                                if (transactionDetailsToDelete != null)
                                {
                                    foreach (TransactionDetail tDetail in transactionDetailsToDelete)
                                    {
                                        dbContext.TransactionDetails.Remove(tDetail);

                                        // add into a collection
                                        if (tDetail.ProductSerialMaps != null && tDetail.ProductSerialMaps.Count > 0)
                                        {
                                            productSerialMapsToDelete.AddRange(tDetail.ProductSerialMaps);
                                        }

                                        // add into a collection 
                                        if (tDetail.TransactionAllocations != null && tDetail.TransactionAllocations.Count > 0)
                                        {
                                            transactionAllocationToDelete.AddRange(tDetail.TransactionAllocations);
                                        }
                                    }
                                }

                                // delete serial maps
                                if (productSerialMapsToDelete != null && productSerialMapsToDelete.Count > 0)
                                {
                                    dbContext.ProductSerialMaps.RemoveRange(productSerialMapsToDelete);
                                }

                                // dlete allocations
                                if (transactionAllocationToDelete != null && transactionAllocationToDelete.Count > 0)
                                {
                                    dbContext.TransactionAllocations.RemoveRange(transactionAllocationToDelete);
                                }
                            }
                        }

                        if (productInventorySerialMapsDigi != null && productInventorySerialMapsDigi.Count > 0)
                        {
                            productInventorySerialMapsDigi.ForEach(p => dbContext.Entry(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified);
                        }

                        //workflow rules
                        dbContext.WorkflowTransactionRuleApproverMaps.RemoveRange(dbContext.WorkflowTransactionRuleApproverMaps
                            .Where(a => a.WorkflowTransactionHeadRuleMap.WorkflowTransactionHeadMap.TransactionHeadID == transaction.HeadIID));
                        dbContext.WorkflowTransactionHeadRuleMaps.RemoveRange(dbContext.WorkflowTransactionHeadRuleMaps
                            .Where(a => a.WorkflowTransactionHeadMap.TransactionHeadID == transaction.HeadIID));
                        dbContext.WorkflowTransactionHeadMaps.RemoveRange(dbContext.WorkflowTransactionHeadMaps
                            .Where(a => a.TransactionHeadID == transaction.HeadIID));
                        //add the new workflow definition.
                        dbContext.WorkflowTransactionHeadMaps.AddRange(transaction.WorkflowTransactionHeadMaps);
                        dbContext.SaveChanges();


                        //Insert GRN entry in inventory.ProductInventories and inventory.InvetoryTransactions Tables
                        var docTypes = dbContext.DocumentTypes.ToList();
                        var grnDocTypeID = docTypes.Find(x => x.TransactionNoPrefix == "GRN-").DocumentTypeID;

                        var settingDatas = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TRANSACTION_DOC_STS_ID_SUBMITTED");
                        var submittedStatusID = long.Parse(settingDatas);

                        if (transaction.DocumentTypeID == grnDocTypeID && transaction.DocumentStatusID == submittedStatusID)
                        {
                            GRNStockUpdation(transaction);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return transaction;
        }

        //
        public void GRNStockUpdation(TransactionHead transaction)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var getTransDetails = transaction.TransactionDetails.Where(s => s.HeadID == transaction.HeadIID).ToList();

                //check data already exist in inventory.InvetoryTransactions table
                var invTransactions = dbContext.InvetoryTransactions.Where(d => d.HeadID == transaction.HeadIID)
                    .AsNoTracking()
                    .ToList();

                if (invTransactions.Count > 0)
                {
                    //remove
                    foreach (var prdctInvList in invTransactions)
                    {
                        var getPrdctInvtry = dbContext.ProductInventories.Where(x => x.ProductSKUMapID == prdctInvList.ProductSKUMapID &&
                        x.BranchID == prdctInvList.BranchID && x.Batch == prdctInvList.BatchID);

                        //delete existing data and for-entry
                        if (getPrdctInvtry != null)
                        {
                            dbContext.ProductInventories.RemoveRange(getPrdctInvtry);
                        }
                    }
                    dbContext.InvetoryTransactions.RemoveRange(invTransactions);
                    dbContext.SaveChanges();
                }


                foreach (var listData in getTransDetails)
                {
                    var getLastBatchbySKUID = dbContext.ProductInventories.Where(x => x.ProductSKUMapID == listData.ProductSKUMapID && x.BranchID == transaction.BranchID)
                        .AsNoTracking()
                        .ToList()
                        .LastOrDefault();

                    var lastBatch = getLastBatchbySKUID != null ? getLastBatchbySKUID.Batch : 0;

                    //var toPrdctInventories = new ProductInventory()
                    //{
                    //    ProductSKUMapID = (long)listData.ProductSKUMapID,
                    //    CompanyID = transaction.CompanyID,
                    //    BranchID = transaction.BranchID,
                    //    CostPrice = listData.UnitPrice,
                    //    Batch = lastBatch + 1,
                    //    OriginalQty = listData.Quantity,
                    //    Quantity = listData.Quantity * (listData.Fraction ?? 0),
                    //};

                    //dbContext.Entry(toPrdctInventories).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    //dbContext.SaveChanges();

                    var toInventoryTransactions = new InvetoryTransaction()
                    {
                        DocumentTypeID = transaction.DocumentTypeID,
                        TransactionNo = transaction.TransactionNo,
                        TransactionDate = DateTime.Now,
                        ProductSKUMapID = listData.ProductSKUMapID,
                        BatchID = lastBatch + 1,
                        UnitID = listData.UnitID,
                        Cost = listData.Amount,
                        Rate = listData.UnitPrice,
                        Quantity = listData.Quantity,
                        Amount = listData.Amount,
                        BranchID = transaction.BranchID,
                        CompanyID = transaction.CompanyID,
                        HeadID = transaction.HeadIID,
                        OriginalQty = listData.Quantity * (listData.Fraction ?? 0),
                        Fraction = listData.Fraction,
                    };

                    dbContext.Entry(toInventoryTransactions).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                    dbContext.SaveChanges();
                }
            }
        }

        public List<TransactionHead> GetTransactionByJobID(long jobEntryHeadIID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var jobdetails = (from jed in dbContext.JobEntryDetails
                                  join jh in dbContext.JobEntryHeads on jed.ParentJobEntryHeadID equals jh.JobEntryHeadIID
                                  join th in dbContext.TransactionHeads on jh.TransactionHeadID equals th.HeadIID
                                  join thead in dbContext.TransactionHeads on th.HeadIID equals thead.ReferenceHeadID
                                  where jed.JobEntryHeadID == jobEntryHeadIID & thead.DocumentTypeID == 8
                                  select thead)
                                  .Include(x => x.DocumentType)
                                  .ToList();
                return jobdetails;

            }
        }

        public TransactionHead GetOrderByJobID(long jobEntryHeadIID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var jobdetails = (from th in dbContext.TransactionHeads
                                  join thead in dbContext.JobEntryHeads on th.HeadIID equals thead.TransactionHeadID
                                  where thead.JobEntryHeadIID == jobEntryHeadIID
                                  select th)
                                  .Include(x => x.OrderContactMaps)
                                  .Include(x => x.Customer)
                                  .AsNoTracking()
                                  .FirstOrDefault();
                return jobdetails;
            }
        }

        public TransactionHead GetTransaction(long headIID)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    TransactionHead transaction = dbContext.TransactionHeads.Where(h => h.HeadIID == headIID)
                                                   .Include(x => x.DocumentType)
                                                   .Include(x => x.DeliveryType)
                                                   .Include(x => x.Employee1)
                                                   .Include(x => x.TransactionStatus)
                                                   .Include(x => x.JobStatus)
                                                   .Include(x => x.Customer)
                                                   .Include(x => x.ReceivingMethod)
                                                   .Include(x => x.ReturnMethod)
                                                   .Include(x => x.Currency)
                                                   .Include(x => x.OrderContactMaps)
                                                   .Include(x => x.TransactionShipments)
                                                   .Include(x => x.Department)
                                                   .Include(x => x.ApprovedByNavigation)
                                                   .Include(x => x.TaxTransactions).ThenInclude(x => x.TaxType)

                                                   .Include(x => x.TransactionDetails).ThenInclude(y => y.ProductSerialMaps)
                                                   .Include(x => x.TransactionDetails).ThenInclude(y => y.TransactionAllocations)

                                                   .Include(x => x.DocumentReferenceStatusMap).ThenInclude(y => y.DocumentStatus)

                                                   .Include(x => x.Branch).Include(x => x.EntityTypeEntitlement)

                                                   .Include(x => x.Employee).Include(x => x.DeliveryTypes1)

                                                   .Include(x => x.JobEntryHeads).ThenInclude(y => y.JobStatus)

                                                   .Include(x => x.TransactionHeadEntitlementMaps).ThenInclude(y => y.EntityTypeEntitlement)

                                                   .Include(x => x.TransactionHeadShoppingCartMaps)
                                                   .ThenInclude(y => y.ShoppingCart)
                                                   .ThenInclude(z => z.ShoppingCartVoucherMaps)
                                                   .ThenInclude(z => z.Voucher)

                                                   .Include(x => x.Supplier).ThenInclude(y => y.ReceivingMethod)
                                                   .Include(x => x.Supplier).ThenInclude(y => y.ReturnMethod)
                                                   .Include(x => x.RFQSupplierRequestMapHeads)

                                                   .AsNoTracking()
                                                   .FirstOrDefault();

                    if (transaction != null)
                    {
                        try
                        {
                            transaction.Employee = new Employee()
                            {
                                EmployeeIID = transaction.EmployeeID.HasValue ? transaction.Employee.EmployeeIID : 0,
                                EmployeeName = transaction.EmployeeID.HasValue ? transaction.Employee.EmployeeCode + " - " + transaction.Employee.FirstName + " " + transaction.Employee.MiddleName + " " + transaction.Employee.LastName : "-",
                            };
                        }
                        catch { }

                        try
                        {
                            transaction.DeliveryType = new DeliveryType()
                            {
                                DeliveryTypeID = transaction.DeliveryMethodID.HasValue ? transaction.DeliveryType.DeliveryTypeID : (short)0,
                                DeliveryMethod = transaction.DeliveryMethodID.HasValue ? transaction.DeliveryType.DeliveryMethod : "-",
                            };
                        }
                        catch { }

                        try
                        {
                            transaction.EntityTypeEntitlement = new EntityTypeEntitlement()
                            {
                                EntitlementID = transaction.EntitlementID.HasValue ? transaction.EntityTypeEntitlement.EntitlementID : (byte)0,
                                EntitlementName = transaction.EntitlementID.HasValue ? transaction.EntityTypeEntitlement.EntitlementName : "-",
                            };
                        }
                        catch { }
                    }

                    return transaction;
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<TransactionRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }
        }



        /// <summary>
        /// GetProductBundleItemDetail
        /// </summary>
        /// <param name="productSKUMapID"></param>
        /// <returns></returns>
        public List<ProductBundle> GetProductBundleItemDetail(long productSKUMapID)
        {
            var bundleList = new List<ProductBundle>();

            dbEduegateERPContext db = new dbEduegateERPContext();
            try
            {
                bundleList = (from pb in db.ProductBundles.AsEnumerable()
                              join p in db.Products on pb.ToProductID equals p.ProductIID
                              join ps in db.ProductSKUMaps on pb.ToProductSKUMapID equals ps.ProductSKUMapIID
                              where (pb.ToProductSKUMapID == productSKUMapID)
                              group pb by new { pb.FromProductID, pb.FromProductSKUMapID, pb.Quantity, pb.CostPrice } into productGroup
                              select new ProductBundle()
                              {
                                  FromProductID = productGroup.Key.FromProductID,
                                  FromProductSKUMapID = productGroup.Key.FromProductSKUMapID,
                                  Quantity = productGroup.Key.Quantity,
                                  CostPrice = productGroup.Key.CostPrice,


                              }).ToList();
            }
            catch (Exception ex)
            {
                bundleList = null;
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "No Items found to . ProductSKUMapID:" + productSKUMapID.ToString(), TrackingCode.TransactionEngine);
                throw;
            }

            return bundleList;
        }



        /// <summary>
        /// GetProductBundleItemDetail
        /// </summary>
        /// <param name="productSKUMapID"></param>
        /// <returns></returns>
        public List<ProductBundleDTO> GetProductBundleItemDetails(long productSKUMapID)
        {
            var bundleList = new List<ProductBundle>();

            dbEduegateERPContext db = new dbEduegateERPContext();
            var productBundleDTO = new List<ProductBundleDTO>();
            try
            {
                //bundleList = (from pb in db.ProductBundles.AsEnumerable()
                //              join p in db.Products on pb.ToProductID equals p.ProductIID
                //              join ps in db.ProductSKUMaps on pb.ToProductSKUMapID equals ps.ProductSKUMapIID
                //              where (pb.ToProductSKUMapID == productSKUMapID)
                //              group pb by new { pb.FromProductID, pb.FromProductSKUMapID, pb.Quantity, pb.CostPrice } into productGroup
                //              select new ProductBundle()
                //              {
                //                  FromProductID = productGroup.Key.FromProductID,
                //                  FromProductSKUMapID = productGroup.Key.FromProductSKUMapID,
                //                  Quantity = productGroup.Key.Quantity,
                //                  CostPrice = productGroup.Key.CostPrice,


                //              }).ToList();

                bundleList = db.ProductBundles
                    .Where(a => a.ToProductSKUMapID == productSKUMapID)
                    .Include(a => a.ToProduct)
                    .Include(a => a.FromProduct)
                    .Include(a => a.ToProductSKUMap)
                    .GroupBy(pb => new { pb.FromProductID, pb.FromProductSKUMapID, pb.Quantity, pb.CostPrice })
                    .Select(pb => new ProductBundle
                    {
                        FromProductID = pb.Key.FromProductID,
                        FromProductSKUMapID = pb.Key.FromProductSKUMapID,
                        Quantity = pb.Key.Quantity,
                        CostPrice = pb.Key.CostPrice,
                        FromProduct = pb.FirstOrDefault().FromProduct
                    })
                    .AsNoTracking()
                    .ToList();


                foreach (ProductBundle detail in bundleList)
                {
                    productBundleDTO.Add(new ProductBundleDTO()
                    {
                        FromProductID = detail.FromProductID,
                        Quantity = detail.Quantity,
                        FromProductSKUMapID = detail.FromProductSKUMapID,
                        CostPrice = detail.CostPrice,
                        FromProduct = new KeyValueDTO() { 
                            Key = detail.FromProductID.ToString(),
                            Value = detail.FromProduct?.ProductName
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                bundleList = null;
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "No Items found to . ProductSKUMapID:" + productSKUMapID.ToString(), TrackingCode.TransactionEngine);
                throw;
            }

            return productBundleDTO;
        }


        public List<TransactionHead> GetChildTransactions(long parentHeadID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return (from trans in dbContext.TransactionHeads
                            //where trans.ReferenceHeadID == parentHeadID && (trans.TransactionStatusID == 5 || trans.TransactionStatusID == 7)
                        where trans.ReferenceHeadID == parentHeadID && (trans.TransactionStatusID != (int)Framework.Enums.TransactionStatus.Cancelled)
                        select trans)
                                               .Include(t => t.TransactionDetails)
                                               .AsNoTracking()
                                               .ToList();
            }
        }

        public List<TransactionHead> GetChildTransaction(long parentHeadID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return (from trans in dbContext.TransactionHeads
                        join dt in dbContext.DocumentTypes
                        on trans.DocumentTypeID equals dt.DocumentTypeID
                        where trans.ReferenceHeadID == parentHeadID && dt.ReferenceTypeID == 5
                        select trans)
                        .Include(t => t.TransactionDetails)
                        .Include(t => t.DocumentType)
                        .Include(t => t.DeliveryType)
                        .Include(t => t.Branch).Include(t => t.EntityTypeEntitlement)
                        .Include(t => t.Employee).Include(t => t.DeliveryTypes1)
                        .Include(t => t.TransactionStatus)
                        .Include(t => t.JobEntryHeads)
                        .Include(t => t.JobStatus)
                        .Include(t => t.Customer)
                        .Include(t => t.Currency)
                        .Include(t => t.Supplier)
                        .Include(i => i.JobEntryHeads).ThenInclude(i => i.JobStatus)
                        .AsNoTracking()
                        .ToList();
            }
        }

        public ProductSKUDetail GetTransactionProductWithSKUName(long productSKUMapID)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    string searchQuery = "select * from catalog.ProductSkuSearchview where ProductSKUMapIID=" + productSKUMapID;
                    ProductSKUDetail skuDetail = dbContext.ProductSKUDetails.FromSqlRaw($@"{searchQuery}")
                        .AsNoTracking()
                        .FirstOrDefault();
                    return skuDetail;
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<TransactionRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }
        }

        public List<TransactionAllocation> GetTransactionAllocations(long transactionDetailID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return
                    (from ta in dbContext.TransactionAllocations
                     where ta.TrasactionDetailID == transactionDetailID
                     select ta)
                     .AsNoTracking()
                     .ToList();
            }
        }

        public ProductInventorySerialMap GetUnUsedSerialKey(long productSKUMapID, int companyID, List<string> exceptions = null)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (exceptions != null && exceptions.Count > 0)
                    return dbContext.ProductInventorySerialMaps.Where(x => x.ProductSKUMapID == productSKUMapID && x.CompanyID == companyID &&
                         (x.Used == false || x.Used == null) && !exceptions.Contains(x.SerialNo)).OrderByDescending(a => a.ProductInventorySerialMapIID)
                         .AsNoTracking()
                         .FirstOrDefault();
                else
                    return dbContext.ProductInventorySerialMaps.Where(x => x.ProductSKUMapID == productSKUMapID && x.CompanyID == companyID &&
                               (x.Used == false || x.Used == null)).OrderByDescending(a => a.ProductInventorySerialMapIID)
                               .AsNoTracking()
                               .FirstOrDefault();
            }
        }

        public List<ProductInventorySerialMap> GetProductInventorySerialMaps(long productSKUMapID, string searchText, int pageSize, bool serialKeyUsed = false, List<string> exceptions = null)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var serialMaps = new List<ProductInventorySerialMap>();
                if (serialKeyUsed)
                {
                    if (searchText.IsNotNullOrEmpty())
                    {
                        serialMaps = dbContext.ProductInventorySerialMaps.Where(x => x.ProductSKUMapID == productSKUMapID && (x.Used == true) && x.SerialNo.Contains(searchText)).Take(pageSize)
                            .AsNoTracking()
                            .ToList();
                    }
                    else
                    {
                        serialMaps = dbContext.ProductInventorySerialMaps.Where(x => x.ProductSKUMapID == productSKUMapID && (x.Used == true)).Take(pageSize)
                            .AsNoTracking()
                            .ToList();
                    }
                }
                else
                {
                    if (searchText.IsNotNullOrEmpty())
                    {
                        serialMaps = dbContext.ProductInventorySerialMaps.Where(x => x.ProductSKUMapID == productSKUMapID &&
                            (x.Used == false || x.Used == null) && x.SerialNo.Contains(searchText)).Take(pageSize)
                            .AsNoTracking()
                            .ToList();
                    }
                    else
                    {
                        serialMaps = dbContext.ProductInventorySerialMaps.Where(x => x.ProductSKUMapID == productSKUMapID &&
                            (x.Used == false || x.Used == null)).Take(pageSize)
                            .AsNoTracking()
                            .ToList();
                    }
                }
                return serialMaps;
            }
        }

        public long GetNextBatch(long productSKUMapID, long? branchID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var batchEntry = dbContext.ProductInventories.Where(x => x.ProductSKUMapID == productSKUMapID && x.BranchID == branchID).AsNoTracking().Max(a => (long?)a.Batch);
                return !batchEntry.HasValue ? 1 : batchEntry.Value + 1;
            }
        }

        public decimal GetCostPrice(long productSKUMapID, long batchID, CostSetting costSetting)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                decimal? costPrice;

                switch (costSetting)
                {
                    case CostSetting.Average:
                        costPrice = dbContext.ProductInventories.Where(x => x.ProductSKUMapID == productSKUMapID).AsNoTracking().Average(a => a.Quantity * a.CostPrice);
                        break;
                    case CostSetting.FIFO:
                        costPrice = dbContext.ProductInventories.Where(x => x.ProductSKUMapID == productSKUMapID && x.Batch == batchID).AsNoTracking().Average(a => a.CostPrice * a.Quantity);
                        break;
                    default:
                        costPrice = dbContext.ProductInventories.Where(x => x.ProductSKUMapID == productSKUMapID).AsNoTracking().Average(a => a.CostPrice * a.Quantity);
                        break;
                }

                return !costPrice.HasValue ? 0 : costPrice.Value;
            }
        }

        public decimal GetCostPrice(long headID, long productSKUMapID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var transaction = dbContext.InvetoryTransactions.Where(a => a.HeadID == headID && a.ProductSKUMapID == productSKUMapID)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (transaction != null)
                    return transaction.Cost.Value;
                else
                    return 0;
            }
        }

        public Transaction GetTransactionDetails(List<int?> docuementTypeIDs, DateTime dateFrom, DateTime dateTo)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var trans = new Transaction();

                var queryCount = (
                    from thead in dbContext.TransactionHeads
                    where docuementTypeIDs.Contains(thead.DocumentTypeID) && thead.TransactionDate >= dateFrom && thead.TransactionDate <= dateTo
                    && thead.TransactionStatusID == 5 && thead.DocumentStatusID == 5
                    select thead.HeadIID
                    );

                trans.TransactionCount = queryCount.Count();

                var queryAmount = (
                    from thead in dbContext.TransactionHeads
                    join entitlement in dbContext.TransactionHeadEntitlementMaps on thead.HeadIID equals entitlement.TransactionHeadID
                    where docuementTypeIDs.Contains(thead.DocumentTypeID) && thead.TransactionDate >= dateFrom && thead.TransactionDate <= dateTo
                    && thead.TransactionStatusID == 5 && thead.DocumentStatusID == 5
                    select entitlement.Amount
                    );

                trans.Amount = queryAmount.Sum();
                return trans;
            }
        }

        public Transaction GetSupplierTransactionDetails(long loginID, List<int?> docuementTypeIDs, DateTime dateFrom, DateTime dateTo)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var trans = new Transaction();

                var queryCount = (
                    from thead in dbContext.TransactionHeads
                    join supplier in dbContext.Suppliers on thead.SupplierID equals supplier.SupplierIID
                    where docuementTypeIDs.Contains(thead.DocumentTypeID) && thead.TransactionDate >= dateFrom && thead.TransactionDate <= dateTo
                    && thead.TransactionStatusID == 5 && thead.DocumentStatusID == 5 && supplier.LoginID == loginID
                    select thead.HeadIID
                    );

                trans.TransactionCount = queryCount.Count();

                var queryAmount = (
                    from thead in dbContext.TransactionHeads
                    join entitlement in dbContext.TransactionHeadEntitlementMaps on thead.HeadIID equals entitlement.TransactionHeadID
                    join supplier in dbContext.Suppliers on thead.SupplierID equals supplier.SupplierIID
                    where docuementTypeIDs.Contains(thead.DocumentTypeID) && thead.TransactionDate >= dateFrom && thead.TransactionDate <= dateTo
                    && thead.TransactionStatusID == 5 && thead.DocumentStatusID == 5 && supplier.LoginID == loginID
                    select entitlement.Amount
                    );

                trans.Amount = queryAmount.Sum();
                return trans;
            }
        }


        public List<ProductInventory> GetProductInventories(long productSKUMapID, long branchID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                // Returns only SKUs from batches that have quantity
                return db.ProductInventories.Where(p => p.ProductSKUMapID == productSKUMapID && p.Quantity != null && p.Quantity > 0 && p.BranchID == branchID).OrderBy(p => p.Batch)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public List<Transaction> GetTransactions(long skuID, int count = 20)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return (
                    from thead in dbContext.TransactionHeads
                    join tdetail in dbContext.TransactionDetails on thead.HeadIID equals tdetail.HeadID
                    join doc in dbContext.DocumentTypes on thead.DocumentTypeID equals doc.DocumentTypeID
                    where tdetail.ProductSKUMapID == skuID
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

        public List<TransactionHeadEntitlementMap> GetTransactionHeadEntitlementMapsByHeadId(long headId)
        {
            using (var db = new dbEduegateERPContext())
            {
                return db.TransactionHeadEntitlementMaps.Where(x => x.TransactionHeadID == headId)
                    .AsNoTracking()
                    .ToList();
            }
        }


        public List<TransactionHead> GetTransactionDetails(IList<Nullable<long>> transactionHeadID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var transactionheads = dbContext.TransactionHeads.Where(x => transactionHeadID.Contains(x.HeadIID))
                    .Include(x => x.JobStatus).Include(x => x.Customer).Include(x => x.OrderContactMaps)
                    .AsNoTracking()
                    .ToList();
                return transactionheads;
            }
        }

        public DocumentReferenceType GetDocumentReferenceTypeByHeadId(long headId)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var result = from th in db.TransactionHeads
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

        /// <summary>
        /// Get Entitlements using transactionHeadId
        /// </summary>
        /// <param name="headId"></param>
        /// <returns></returns>
        public List<EntityTypeEntitlement> GetEntitlementsByHeadId(long headId)
        {
            using (var db = new dbEduegateERPContext())
            {
                return db.TransactionHeadEntitlementMaps
                        .Join(db.EntityTypeEntitlements, em => em.EntitlementID, e => e.EntitlementID, (em, e) => new { em, e })
                        .Where(x => x.em.TransactionHeadID == headId).Select(x => x.e)
                        .AsNoTracking()
                        .ToList();
            }
        }

        public List<Tuple<string, Nullable<decimal>>> GetTransactionEntitlementByHeadId(long headId)
        {
            using (var db = new dbEduegateERPContext())
            {
                var result = (from em in db.TransactionHeadEntitlementMaps
                              join
                                e in db.EntityTypeEntitlements on em.EntitlementID equals e.EntitlementID
                              where em.TransactionHeadID == headId
                              select new { EntityTypeEntitlements = e, TransactionHeadEntitlementMaps = em }
                               ).AsEnumerable().Select(c => new Tuple<string, Nullable<decimal>>(c.EntityTypeEntitlements.EntitlementName, c.TransactionHeadEntitlementMaps.Amount));
                var tuple = result.ToList();
                return tuple;
            }
        }

        public TransactionHead GetTransactionByJobEntryHeadId(long jobEntryHeadId)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    TransactionHead transaction = (from trans in dbContext.TransactionHeads
                                                   join th in dbContext.JobEntryHeads on trans.JobEntryHeadID equals th.JobEntryHeadIID
                                                   where th.JobEntryHeadIID == jobEntryHeadId
                                                   select trans)
                                                   .Include(x => x.DocumentType)
                                                   .Include(x => x.DeliveryType)
                                                   .Include(x => x.Branch)
                                                   .Include(x => x.EntityTypeEntitlement)
                                                   .Include(x => x.Employee)
                                                   .Include(x => x.DeliveryTypes1)
                                                   .Include(i => i.Supplier)
                                                   .Include(i => i.Currency)
                                                   .Include(i => i.Customer)
                                                   .Include(i => i.OrderContactMaps)
                                                   .Include(i => i.TransactionDetails).ThenInclude(i => i.ProductSerialMaps)
                                                   .Include(i => i.TransactionDetails).ThenInclude(i => i.TransactionAllocations)
                                                   .Include(i => i.TransactionHeadEntitlementMaps).ThenInclude(i => i.EntityTypeEntitlement)
                                                   .Include(i => i.TransactionShipments)
                                                   .AsNoTracking()
                                                   .FirstOrDefault();

                    if (transaction != null)
                    {
                        //dbContext.Entry(transaction).Reference(a => a.Supplier).Load();
                        //dbContext.Entry(transaction).Reference(a => a.Currency).Load();
                        //dbContext.Entry(transaction).Reference(a => a.Customer).Load();
                        //dbContext.Entry(transaction).Collection(a => a.OrderContactMaps).Load();
                        //dbContext.Entry(transaction).Collection(a => a.TransactionDetails).Load();
                        //dbContext.Entry(transaction).Collection(a => a.TransactionHeadEntitlementMaps).Load();

                        if (transaction.TransactionHeadEntitlementMaps != null)
                        {
                            //foreach (var map in transaction.TransactionHeadEntitlementMaps)
                            //{
                            //    dbContext.Entry(map).Reference(a => a.EntityTypeEntitlement).Load();
                            //}
                        }

                        if (transaction.TransactionDetails != null && transaction.TransactionDetails.Count > 0)
                        {
                            //foreach (var detail in transaction.TransactionDetails)
                            //{
                            //    dbContext.Entry(detail).Collection(a => a.ProductSerialMaps).Load();
                            //    dbContext.Entry(detail).Collection(a => a.TransactionAllocations).Load();
                            //}
                        }
                        transaction.Employee = new Employee()
                        {
                            EmployeeIID = transaction.EmployeeID != null ? transaction.Employee.EmployeeIID : 0,
                            EmployeeName = transaction.EmployeeID != null ? transaction.Employee.EmployeeName : "-",
                        };

                        transaction.DeliveryType = new DeliveryType()
                        {
                            DeliveryTypeID = transaction.DeliveryMethodID != null ? transaction.DeliveryType.DeliveryTypeID : (short)0,
                            DeliveryMethod = transaction.DeliveryMethodID != null ? transaction.DeliveryType.DeliveryMethod : "-",
                        };

                        transaction.EntityTypeEntitlement = new EntityTypeEntitlement()
                        {
                            EntitlementID = transaction.EntitlementID != null ? transaction.EntityTypeEntitlement.EntitlementID : (byte)0,
                            EntitlementName = transaction.EntitlementID != null ? transaction.EntityTypeEntitlement.EntitlementName : "-",
                        };

                        //dbContext.Entry(transaction).Collection(a => a.TransactionShipments).Load();
                    }

                    return transaction;
                }
            }

            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<TransactionRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }
        }

        public decimal DigitalAmountCustomerCheck(Int64 customerID, long referenceType, int companyID)
        {
            DateTime startDate = DateTime.Now.AddDays(-1);
            DateTime endDate = DateTime.Now;
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var result = (from d in db.TransactionDetails
                              join h in db.TransactionHeads on d.HeadID equals h.HeadIID
                              join s in db.ProductSKUMaps on d.ProductSKUMapID equals s.ProductSKUMapIID
                              join p in db.Products on s.ProductID equals p.ProductIID
                              join dd in db.DocumentTypes on h.DocumentTypeID equals dd.DocumentTypeID
                              where dd.ReferenceTypeID == referenceType && h.CustomerID == customerID && h.CompanyID == companyID // is null condition has to be removed as compnayid cannot be null as it has to be handeld by ERP
                              && (ProductTypes)p.ProductTypeID == ProductTypes.Digital && (h.TransactionDate >= startDate && h.TransactionDate <= endDate)
                              select d.Amount).Sum();
                return result.HasValue ? result.Value : 0;
            }
        }


        public Entity.Models.TransactionStatus GetTransactionStatus(byte transactionStatusId)
        {
            using (var db = new dbEduegateERPContext())
            {
                return db.TransactionStatuses.Where(x => x.TransactionStatusID == transactionStatusId).FirstOrDefault();
            }
        }

        public TransactionHead GetOrderByinvoiceId(long HeadId)
        {
            using (var db = new dbEduegateERPContext())
            {

                var Reference = (from th in db.TransactionHeads
                                 join thead in db.TransactionHeads on th.HeadIID equals thead.ReferenceHeadID
                                 where thead.HeadIID == HeadId
                                 select thead)
                                 .AsNoTracking()
                                 .FirstOrDefault();
                var ReferenceId = Reference.ReferenceHeadID;

                return db.TransactionHeads.Where(x => x.HeadIID == ReferenceId)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }


        public TransactionHead GetTransactionDetailsByJob(long jobID, Services.Contracts.Enums.DocumentReferenceTypes documentReferenceType)
        {
            using (var db = new dbEduegateERPContext())
            {

                var transactionHead = (from th in db.TransactionHeads
                                       from jobs in db.JobEntryHeads.Where(x => x.TransactionHeadID == th.ReferenceHeadID || x.TransactionHeadID == th.HeadIID)
                                       join doctypes in db.DocumentTypes on th.DocumentTypeID equals doctypes.DocumentTypeID
                                       join docreftypes in db.DocumentReferenceTypes on doctypes.ReferenceTypeID equals docreftypes.ReferenceTypeID
                                       where jobs.JobEntryHeadIID == jobID && docreftypes.ReferenceTypeID == (int)documentReferenceType
                                       select th)
                                 .Include(x => x.DocumentType)
                                                   .Include(x => x.DeliveryType)
                                                   .Include(x => x.Branch)
                                                   .Include(x => x.EntityTypeEntitlement)
                                                   .Include(x => x.Employee)
                                                   .Include(x => x.Currency)
                                                   .Include(x => x.DeliveryTypes1)
                                                   .Include(x => x.DocumentReferenceStatusMap)
                                                   .Include(x => x.TransactionStatus)
                                                   .Include(x => x.Customer)
                                                   .Include(x => x.Supplier)
                                                   .Include(i => i.JobEntryHeads).ThenInclude(i => i.JobStatus)
                                 .AsNoTracking()
                                 .FirstOrDefault();
                if (transactionHead == null) //get SO3 for an PO3 using ReferenceHeadID
                {
                    var References = (from th in db.TransactionHeads
                                      join jobs in db.JobEntryHeads on th.HeadIID equals jobs.TransactionHeadID
                                      join thead in db.TransactionHeads on th.ReferenceHeadID equals thead.HeadIID
                                      join doctypes in db.DocumentTypes on thead.DocumentTypeID equals doctypes.DocumentTypeID
                                      join docreftypes in db.DocumentReferenceTypes on doctypes.ReferenceTypeID equals docreftypes.ReferenceTypeID
                                      where jobs.JobEntryHeadIID == jobID && docreftypes.ReferenceTypeID == (int)documentReferenceType
                                      select thead)
                                 .Include(x => x.DocumentType)
                                 .Include(x => x.DeliveryType)
                                                   .Include(x => x.Branch)
                                                   .Include(x => x.EntityTypeEntitlement)
                                                   .Include(x => x.Employee)
                                                   .Include(x => x.Currency)
                                                   .Include(x => x.DeliveryTypes1)
                                                   .Include(x => x.DocumentReferenceStatusMap)
                                                   .Include(x => x.TransactionStatus)
                                                   .Include(x => x.Customer)
                                                   .Include(x => x.Supplier)
                                 .AsNoTracking()
                                 .FirstOrDefault();
                    return References;
                }
                else
                {
                    return transactionHead;
                }
                //return transactionHead;
            }
        }

        public List<TransactionHead> GetinvoiceByOrderId(long HeadId)
        {
            using (var db = new dbEduegateERPContext())
            {

                var heads = (from th in db.TransactionHeads
                             join
                                thead in db.TransactionHeads on
                                th.ReferenceHeadID equals thead.HeadIID
                             join doctype in db.DocumentTypes on
                             th.DocumentTypeID equals doctype.DocumentTypeID
                             where thead.HeadIID == HeadId && doctype.ReferenceTypeID == 5 //SI document type may varry,but referencetype wont
                             select th)
                            .Include(x => x.TransactionStatus)
                            .Include(x => x.DocumentReferenceStatusMap)
                            .Include(i => i.DocumentReferenceStatusMap).ThenInclude(i => i.DocumentStatus)
                            .AsNoTracking()
                            .ToList();
                return heads;
            }
        }

        public bool HasDigitalProduct(long headID)
        {
            using (var db = new dbEduegateERPContext())
            {
                return (from d in db.TransactionDetails
                        join h in db.TransactionHeads on d.HeadID equals h.HeadIID
                        join sku in db.ProductSKUMaps on d.ProductSKUMapID equals sku.ProductSKUMapIID
                        join prd in db.Products on sku.ProductID equals prd.ProductIID
                        where h.HeadIID == headID && prd.ProductTypeID == (long)ProductTypes.Digital
                        select d
                            )
                            .AsNoTracking().Any();
            }
        }

        public List<TransactionDetail> GetOrderByJobId(long jobId)
        {
            using (var db = new dbEduegateERPContext())
            {

                var order = (from pskumap in db.ProductSKUMaps
                             join det in db.TransactionDetails on pskumap.ProductSKUMapIID equals det.ProductSKUMapID
                             join heads in db.JobEntryHeads on det.HeadID equals heads.TransactionHeadID
                             where heads.JobEntryHeadIID == jobId
                             select det)
                             .Include(x => x.ProductSKUMap)
                             .AsNoTracking()
                             .ToList();
                return order;

            }
        }

        public List<TransactionHead> GetAllTransactionsBySerialKey(string serialKey)
        {
            using (var db = new dbEduegateERPContext())
            {
                var transactions = (from th in db.TransactionHeads
                                    join td in db.TransactionDetails
                                    on th.HeadIID equals td.HeadID
                                    join psm in db.ProductSerialMaps
                                    on td.DetailIID equals psm.DetailID
                                    where psm.SerialNo == serialKey
                                    select th)

                        .Union
                        (from td in db.TransactionDetails
                         join
                             th in db.TransactionHeads on td.HeadID equals th.HeadIID
                         where td.SerialNumber == serialKey
                         select th)
                               .Include(x => x.Customer)
                               .Include(x => x.Supplier)
                               .Include(x => x.DeliveryType)
                               .Include(x => x.Currency)
                               .Include(x => x.TransactionStatus)
                               .Include(x => x.EntityTypeEntitlement)
                               .Include(x => x.DocumentReferenceStatusMap)
                               .AsNoTracking()
                               .ToList();

                return transactions;
            }
        }

        public List<TransactionHead> GetParentTransactions(List<long> heads)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.TransactionHeads.Where(x => heads.Contains((long)x.HeadIID))
                    .Include(x => x.Customer)
                    .Include(x => x.Supplier)
                    .Include(x => x.DeliveryType)
                    .Include(x => x.Currency)
                    .Include(x => x.TransactionStatus)
                    .Include(x => x.EntityTypeEntitlement)
                    .Include(x => x.DocumentReferenceStatusMap)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public bool CancelSalesOrderTransaction(long headID)
        {
            using (var db = new dbEduegateERPContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //update the status
                        var query = db.TransactionHeads.Where(x => x.HeadIID == headID).AsNoTracking().FirstOrDefault();

                        if (query.IsNotNull())
                        {
                            query.TransactionStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.Complete;
                            query.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;

                            query.UpdatedBy = query.UpdatedBy;
                            query.UpdatedDate = DateTime.Now;
                            query.DocumentCancelledDate = DateTime.Now;
                            db.Entry(query).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }

                        //if invoice already generated intiate cancellation for that
                        var invoices = db.TransactionHeads.Where(x => x.ReferenceHeadID == headID);
                        bool isInvocieGenerated = false;

                        foreach (var invoice in invoices)
                        {
                            if (invoice.TransactionStatusID == (byte)Eduegate.Framework.Enums.TransactionStatus.Complete)
                            {
                                // If SI Exists it should Generate SRR
                                if (invoice.DocumentType.ReferenceTypeID == (int)DocumentReferenceTypes.SalesInvoice)
                                {
                                    TransactionHead newSRR = CloneTransaction(headID);
                                    newSRR.HeadIID = 0;
                                    newSRR.DocumentTypeID = int.Parse(new SettingRepository().GetSettingDetail("SRRDOCUMENTTYPEID", query.CompanyID.Value).SettingValue);
                                    newSRR.TransactionDate = DateTime.Now;
                                    newSRR.TransactionNo = GetNextTransactionNumber(newSRR.DocumentTypeID.Value);
                                    newSRR.DocumentStatusID = (int)Eduegate.Services.Contracts.Enums.DocumentStatuses.Approved;
                                    newSRR.TransactionStatusID = (int)Eduegate.Framework.Enums.TransactionStatus.New;
                                    newSRR.ReferenceHeadID = invoice.HeadIID;
                                    db.TransactionHeads.Add(newSRR);
                                    db.Entry(newSRR).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                                    if (newSRR.TransactionHeadEntitlementMaps != null)
                                    {
                                        foreach (var entitlment in newSRR.TransactionHeadEntitlementMaps)
                                        {
                                            db.Entry(entitlment).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                    }

                                    if (newSRR.OrderContactMaps != null)
                                    {
                                        foreach (var contact in newSRR.OrderContactMaps)
                                        {
                                            db.Entry(contact).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                    }

                                    if (newSRR.TransactionDetails != null)
                                    {
                                        foreach (var detail in newSRR.TransactionDetails)
                                        {
                                            db.Entry(detail).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                    }
                                }
                                else if (invoice.DocumentType.ReferenceTypeID == (int)DocumentReferenceTypes.PurchaseOrder)
                                {
                                    invoice.TransactionStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.IntitiateReprocess;
                                    invoice.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;

                                    invoice.UpdatedBy = query.UpdatedBy;
                                    invoice.UpdatedDate = DateTime.Now;
                                    db.Entry(invoice).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                }
                                isInvocieGenerated = true;
                            }
                            else
                            {
                                invoice.TransactionStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.Complete;
                                invoice.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;
                                invoice.UpdatedBy = query.UpdatedBy;
                                invoice.UpdatedDate = DateTime.Now;
                                db.Entry(invoice).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                                foreach (var detail in invoice.TransactionDetails)
                                {
                                    foreach (var serial in detail.ProductSerialMaps)
                                    {
                                        var inventory = db.ProductInventorySerialMaps.Where(a => a.SerialNo == serial.SerialNo)
                                            .AsNoTracking()
                                            .FirstOrDefault();
                                        inventory.Used = false;
                                        db.Entry(inventory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                    }
                                }
                            }
                        }

                        // cancel all the jobs
                        var jobs = db.JobEntryHeads.Where(x => x.TransactionHeadID == headID);

                        foreach (var job in jobs)
                        {
                            job.JobStatusID = (int)Eduegate.Services.Contracts.Enums.JobStatuses.Cancel;
                            db.Entry(job).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }

                        if (!isInvocieGenerated) //if generated inventeroy already moved
                        {
                            //revert back the inventory
                            var shoppingCartMap = db.TransactionHeadShoppingCartMaps.Where(a => a.TransactionHeadID == headID)
                                .AsNoTracking()
                                .FirstOrDefault();

                            if (shoppingCartMap != null)
                            {
                                var shoppingCart = db.ShoppingCarts.Where(a => a.ShoppingCartIID == shoppingCartMap.ShoppingCartID)
                                    .AsNoTracking()
                                    .FirstOrDefault();

                                if (shoppingCart != null && shoppingCart.IsInventoryBlocked != null && shoppingCart.IsInventoryBlocked.Value && shoppingCart.BlockedHeadID.HasValue)
                                {
                                    // revert back the inventory
                                    var branchTransfer = CloneTransaction(shoppingCart.BlockedHeadID.Value);
                                    var branchID = branchTransfer.BranchID;
                                    branchTransfer.TransactionDate = DateTime.Now;
                                    branchTransfer.BranchID = branchTransfer.ToBranchID;
                                    branchTransfer.ToBranchID = branchID;
                                    branchTransfer.TransactionStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.New;
                                    branchTransfer.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Approved;
                                    branchTransfer.TransactionNo = GetNextTransactionNumber(branchTransfer.DocumentTypeID.Value);
                                    branchTransfer.CreatedDate = DateTime.Now;
                                    branchTransfer.UpdatedDate = DateTime.Now;
                                    //branchTransfer.ReferenceHeadID = shoppingCart.BlockedHeadID;
                                    db.TransactionHeads.Add(branchTransfer);
                                    db.Entry(branchTransfer).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                                    if (branchTransfer.TransactionHeadEntitlementMaps != null)
                                    {
                                        foreach (var entitlement in branchTransfer.TransactionHeadEntitlementMaps)
                                        {
                                            db.Entry(entitlement).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                    }

                                    if (branchTransfer.OrderContactMaps != null)
                                    {
                                        foreach (var contact in branchTransfer.OrderContactMaps)
                                        {
                                            db.Entry(contact).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                    }

                                    if (branchTransfer.TransactionDetails != null)
                                    {
                                        foreach (var detail in branchTransfer.TransactionDetails)
                                        {
                                            db.Entry(detail).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                    }

                                    shoppingCart.IsInventoryBlocked = false;
                                    db.Entry(shoppingCart).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                }
                            }
                        }

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

        public TransactionHead CloneTransaction(long headID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var head = dbContext.TransactionHeads.Where(a => a.HeadIID == headID)
                    .Include(i => i.TransactionDetails).ThenInclude(i => i.ProductSerialMaps)
                    .Include(i => i.TransactionDetails).ThenInclude(i => i.TransactionAllocations)
                    .Include(i => i.TransactionHeadEntitlementMaps)
                    .Include(i => i.OrderContactMaps)
                    .AsNoTracking()
                    .FirstOrDefault();
                //dbContext.Entry(head).Collection(a => a.TransactionDetails).Load();
                //dbContext.Entry(head).Collection(a => a.TransactionHeadEntitlementMaps).Load();
                //dbContext.Entry(head).Collection(a => a.OrderContactMaps).Load();

                //foreach (var detail in head.TransactionDetails)
                //{
                //    dbContext.Entry(detail).Collection(a => a.ProductSerialMaps).Load();
                //    dbContext.Entry(detail).Collection(a => a.TransactionAllocations).Load();
                //}

                return head;
            }
        }

        public string GetNextTransactionNumber(int documentTypeID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
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
            using (var db = new dbEduegateERPContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var query = db.TransactionHeads.Where(x => x.HeadIID == headID)
                            .AsNoTracking()
                            .FirstOrDefault();

                        if (query.IsNotNull())
                        {
                            //generate a sales return request
                            query.TransactionStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.Complete;
                            query.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;

                            query.UpdatedBy = query.UpdatedBy;
                            query.UpdatedDate = DateTime.Now;
                            query.DocumentCancelledDate = DateTime.Now;
                            db.Entry(query).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                            foreach (var detail in db.TransactionDetails.Where(x => x.HeadID == headID))
                            {
                                foreach (var serial in detail.ProductSerialMaps)
                                {
                                    var inventory = db.ProductInventorySerialMaps.Where(a => a.SerialNo == serial.SerialNo)
                                        .AsNoTracking()
                                        .FirstOrDefault();
                                    inventory.Used = false;
                                    db.Entry(inventory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                }
                            }

                            // cancel all the jobs
                            var jobs = db.JobEntryHeads.Where(x => x.TransactionHeadID == headID);

                            foreach (var job in jobs)
                            {
                                job.JobStatusID = (int)Eduegate.Services.Contracts.Enums.JobStatuses.Cancel;
                                db.Entry(job).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }

                            TransactionHead newSRR = CloneTransaction(headID);
                            newSRR.HeadIID = 0;
                            newSRR.DocumentTypeID = int.Parse(new SettingRepository().GetSettingDetail("SRRDOCUMENTTYPEID", query.CompanyID.Value).SettingValue);
                            newSRR.TransactionDate = DateTime.Now;
                            newSRR.TransactionNo = GetNextTransactionNumber(newSRR.DocumentTypeID.Value);
                            newSRR.DocumentStatusID = (int)Eduegate.Services.Contracts.Enums.DocumentStatuses.Approved;
                            newSRR.TransactionStatusID = (int)Eduegate.Framework.Enums.TransactionStatus.New;
                            newSRR.ReferenceHeadID = query.HeadIID;
                            db.TransactionHeads.Add(newSRR);
                            db.Entry(newSRR).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                            if (newSRR.TransactionHeadEntitlementMaps != null)
                            {
                                foreach (var entitlment in newSRR.TransactionHeadEntitlementMaps)
                                {
                                    db.Entry(entitlment).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                }
                            }

                            if (newSRR.OrderContactMaps != null)
                            {
                                foreach (var contact in newSRR.OrderContactMaps)
                                {
                                    db.Entry(contact).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                }
                            }

                            if (newSRR.TransactionDetails != null)
                            {
                                foreach (var detail in newSRR.TransactionDetails)
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
            using (var db = new dbEduegateERPContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //update the status
                        var query = db.TransactionHeads.Where(x => x.HeadIID == headID)
                            .AsNoTracking()
                            .FirstOrDefault();

                        if (query.IsNotNull())
                        {
                            query.TransactionStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.Complete;
                            query.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;

                            query.UpdatedBy = query.UpdatedBy;
                            query.UpdatedDate = DateTime.Now;
                            query.DocumentCancelledDate = DateTime.Now;
                            db.Entry(query).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }

                        //if invoice already generated intiate cancellation for that
                        var invoices = db.TransactionHeads.Where(x => x.ReferenceHeadID == headID);
                        bool isInvocieGenerated = false;

                        foreach (var invoice in invoices)
                        {
                            if (invoice.TransactionStatusID == (byte)Eduegate.Framework.Enums.TransactionStatus.Complete)
                            {
                                // If SI Exists it should Generate SRR
                                if (invoice.DocumentType.ReferenceTypeID == (int)DocumentReferenceTypes.SalesInvoice)
                                {
                                    TransactionHead newSRR = CloneTransaction(headID);
                                    newSRR.HeadIID = 0;
                                    newSRR.DocumentTypeID = int.Parse(new SettingRepository().GetSettingDetail("SRRDOCUMENTTYPEID", query.CompanyID.Value).SettingValue);
                                    newSRR.TransactionDate = DateTime.Now;
                                    newSRR.TransactionNo = GetNextTransactionNumber(newSRR.DocumentTypeID.Value);
                                    newSRR.DocumentStatusID = (int)Eduegate.Services.Contracts.Enums.DocumentStatuses.Approved;
                                    newSRR.TransactionStatusID = (int)Eduegate.Framework.Enums.TransactionStatus.New;
                                    newSRR.ReferenceHeadID = invoice.HeadIID;
                                    db.TransactionHeads.Add(newSRR);
                                    db.Entry(newSRR).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                                    if (newSRR.TransactionHeadEntitlementMaps != null)
                                    {
                                        foreach (var entitlment in newSRR.TransactionHeadEntitlementMaps)
                                        {
                                            db.Entry(entitlment).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                    }

                                    if (newSRR.OrderContactMaps != null)
                                    {
                                        foreach (var contact in newSRR.OrderContactMaps)
                                        {
                                            db.Entry(contact).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                    }

                                    if (newSRR.TransactionDetails != null)
                                    {
                                        foreach (var detail in newSRR.TransactionDetails)
                                        {
                                            db.Entry(detail).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                    }
                                }
                                else if (invoice.DocumentType.ReferenceTypeID == (int)DocumentReferenceTypes.PurchaseOrder)
                                {
                                    invoice.TransactionStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.IntitiateReprocess;
                                    invoice.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;

                                    invoice.UpdatedBy = query.UpdatedBy;
                                    invoice.UpdatedDate = DateTime.Now;
                                    db.Entry(invoice).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                }
                                isInvocieGenerated = true;
                            }
                            else
                            {
                                invoice.TransactionStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.Complete;
                                invoice.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;
                                invoice.UpdatedBy = query.UpdatedBy;
                                invoice.UpdatedDate = DateTime.Now;
                                db.Entry(invoice).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                                foreach (var detail in invoice.TransactionDetails)
                                {
                                    foreach (var serial in detail.ProductSerialMaps)
                                    {
                                        var inventory = db.ProductInventorySerialMaps.Where(a => a.SerialNo == serial.SerialNo)
                                            .AsNoTracking()
                                            .FirstOrDefault();
                                        inventory.Used = false;
                                        db.Entry(inventory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                    }
                                }
                            }
                        }

                        // cancel all the jobs
                        var jobs = db.JobEntryHeads.Where(x => x.TransactionHeadID == headID);

                        foreach (var job in jobs)
                        {
                            job.JobStatusID = (int)Eduegate.Services.Contracts.Enums.JobStatuses.Cancel;
                            db.Entry(job).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }

                        if (!isInvocieGenerated) //if generated inventeroy already moved
                        {
                            //revert back the inventory
                            var shoppingCartMap = db.TransactionHeadShoppingCartMaps.Where(a => a.TransactionHeadID == headID)
                                .AsNoTracking()
                                .FirstOrDefault();

                            if (shoppingCartMap != null)
                            {
                                var shoppingCart = db.ShoppingCarts.Where(a => a.ShoppingCartIID == shoppingCartMap.ShoppingCartID)
                                    .AsNoTracking()
                                    .FirstOrDefault();

                                if (shoppingCart != null && shoppingCart.IsInventoryBlocked != null && shoppingCart.IsInventoryBlocked.Value && shoppingCart.BlockedHeadID.HasValue)
                                {
                                    // revert back the inventory
                                    var branchTransfer = CloneTransaction(shoppingCart.BlockedHeadID.Value);
                                    var branchID = branchTransfer.BranchID;
                                    branchTransfer.TransactionDate = DateTime.Now;
                                    branchTransfer.BranchID = branchTransfer.ToBranchID;
                                    branchTransfer.ToBranchID = branchID;
                                    branchTransfer.TransactionStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.New;
                                    branchTransfer.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Approved;
                                    branchTransfer.TransactionNo = GetNextTransactionNumber(branchTransfer.DocumentTypeID.Value);
                                    branchTransfer.CreatedDate = DateTime.Now;
                                    branchTransfer.UpdatedDate = DateTime.Now;
                                    //branchTransfer.ReferenceHeadID = shoppingCart.BlockedHeadID;
                                    db.TransactionHeads.Add(branchTransfer);
                                    db.Entry(branchTransfer).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                                    if (branchTransfer.TransactionHeadEntitlementMaps != null)
                                    {
                                        foreach (var entitlement in branchTransfer.TransactionHeadEntitlementMaps)
                                        {
                                            db.Entry(entitlement).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                    }

                                    if (branchTransfer.OrderContactMaps != null)
                                    {
                                        foreach (var contact in branchTransfer.OrderContactMaps)
                                        {
                                            db.Entry(contact).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                    }

                                    if (branchTransfer.TransactionDetails != null)
                                    {
                                        foreach (var detail in branchTransfer.TransactionDetails)
                                        {
                                            db.Entry(detail).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                    }

                                    shoppingCart.IsInventoryBlocked = false;
                                    db.Entry(shoppingCart).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                }
                            }
                        }

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
            using (var db = new dbEduegateERPContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //update the status
                        var query = db.TransactionHeads.Where(x => x.HeadIID == headID)
                            .AsNoTracking()
                            .FirstOrDefault();

                        if (query.IsNotNull())
                        {
                            query.TransactionStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.Complete;
                            query.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;

                            query.UpdatedBy = query.UpdatedBy;
                            query.UpdatedDate = DateTime.Now;
                            query.DocumentCancelledDate = DateTime.Now;
                            db.Entry(query).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }

                        //if invoice already generated intiate cancellation for that
                        var invoices = db.TransactionHeads.Where(x => x.ReferenceHeadID == headID);
                        bool isInvocieGenerated = false;

                        foreach (var invoice in invoices)
                        {
                            if (invoice.TransactionStatusID == (byte)Eduegate.Framework.Enums.TransactionStatus.Complete)
                            {
                                // If SI Exists it should Generate SRR
                                if (invoice.DocumentType.ReferenceTypeID == (int)DocumentReferenceTypes.SalesInvoice)
                                {
                                    TransactionHead newSRR = CloneTransaction(headID);
                                    newSRR.HeadIID = 0;
                                    newSRR.DocumentTypeID = int.Parse(new SettingRepository().GetSettingDetail("SRRDOCUMENTTYPEID", query.CompanyID.Value).SettingValue);
                                    newSRR.TransactionDate = DateTime.Now;
                                    newSRR.TransactionNo = GetNextTransactionNumber(newSRR.DocumentTypeID.Value);
                                    newSRR.DocumentStatusID = (int)Eduegate.Services.Contracts.Enums.DocumentStatuses.Approved;
                                    newSRR.TransactionStatusID = (int)Eduegate.Framework.Enums.TransactionStatus.New;
                                    newSRR.ReferenceHeadID = invoice.HeadIID;
                                    db.TransactionHeads.Add(newSRR);
                                    db.Entry(newSRR).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                                    if (newSRR.TransactionHeadEntitlementMaps != null)
                                    {
                                        foreach (var entitlment in newSRR.TransactionHeadEntitlementMaps)
                                        {
                                            db.Entry(entitlment).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                    }

                                    if (newSRR.OrderContactMaps != null)
                                    {
                                        foreach (var contact in newSRR.OrderContactMaps)
                                        {
                                            db.Entry(contact).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                    }

                                    if (newSRR.TransactionDetails != null)
                                    {
                                        foreach (var detail in newSRR.TransactionDetails)
                                        {
                                            db.Entry(detail).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                    }
                                }
                                else if (invoice.DocumentType.ReferenceTypeID == (int)DocumentReferenceTypes.PurchaseOrder)
                                {
                                    invoice.TransactionStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.IntitiateReprocess;
                                    invoice.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;

                                    invoice.UpdatedBy = query.UpdatedBy;
                                    invoice.UpdatedDate = DateTime.Now;
                                    db.Entry(invoice).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                }
                                isInvocieGenerated = true;
                            }
                            else
                            {
                                invoice.TransactionStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.Complete;
                                invoice.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;
                                invoice.UpdatedBy = query.UpdatedBy;
                                invoice.UpdatedDate = DateTime.Now;
                                db.Entry(invoice).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                                foreach (var detail in invoice.TransactionDetails)
                                {
                                    foreach (var serial in detail.ProductSerialMaps)
                                    {
                                        var inventory = db.ProductInventorySerialMaps.FirstOrDefault(a => a.SerialNo == serial.SerialNo);
                                        inventory.Used = false;
                                        db.Entry(inventory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                    }
                                }
                            }
                        }

                        // cancel all the jobs
                        var jobs = db.JobEntryHeads.Where(x => x.TransactionHeadID == headID);

                        foreach (var job in jobs)
                        {
                            job.JobStatusID = (int)Eduegate.Services.Contracts.Enums.JobStatuses.Cancel;
                            db.Entry(job).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }

                        if (!isInvocieGenerated) //if generated inventeroy already moved
                        {
                            //revert back the inventory
                            var shoppingCartMap = db.TransactionHeadShoppingCartMaps.Where(a => a.TransactionHeadID == headID)
                                .AsNoTracking()
                                .FirstOrDefault();

                            if (shoppingCartMap != null)
                            {
                                var shoppingCart = db.ShoppingCarts.Where(a => a.ShoppingCartIID == shoppingCartMap.ShoppingCartID)
                                    .AsNoTracking()
                                    .FirstOrDefault();

                                if (shoppingCart != null && shoppingCart.IsInventoryBlocked != null && shoppingCart.IsInventoryBlocked.Value && shoppingCart.BlockedHeadID.HasValue)
                                {
                                    // revert back the inventory
                                    var branchTransfer = CloneTransaction(shoppingCart.BlockedHeadID.Value);
                                    var branchID = branchTransfer.BranchID;
                                    branchTransfer.TransactionDate = DateTime.Now;
                                    branchTransfer.BranchID = branchTransfer.ToBranchID;
                                    branchTransfer.ToBranchID = branchID;
                                    branchTransfer.TransactionStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.New;
                                    branchTransfer.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Approved;
                                    branchTransfer.TransactionNo = GetNextTransactionNumber(branchTransfer.DocumentTypeID.Value);
                                    branchTransfer.CreatedDate = DateTime.Now;
                                    branchTransfer.UpdatedDate = DateTime.Now;
                                    //branchTransfer.ReferenceHeadID = shoppingCart.BlockedHeadID;
                                    db.TransactionHeads.Add(branchTransfer);
                                    db.Entry(branchTransfer).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                                    if (branchTransfer.TransactionHeadEntitlementMaps != null)
                                    {
                                        foreach (var entitlement in branchTransfer.TransactionHeadEntitlementMaps)
                                        {
                                            db.Entry(entitlement).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                    }

                                    if (branchTransfer.OrderContactMaps != null)
                                    {
                                        foreach (var contact in branchTransfer.OrderContactMaps)
                                        {
                                            db.Entry(contact).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                    }

                                    if (branchTransfer.TransactionDetails != null)
                                    {
                                        foreach (var detail in branchTransfer.TransactionDetails)
                                        {
                                            db.Entry(detail).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                    }

                                    shoppingCart.IsInventoryBlocked = false;
                                    db.Entry(shoppingCart).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                }
                            }
                        }

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
            using (var db = new dbEduegateERPContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //update the status
                        var query = db.TransactionHeads.Where(x => x.HeadIID == headID)
                            .AsNoTracking()
                            .FirstOrDefault();

                        if (query.IsNotNull())
                        {
                            query.TransactionStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.Complete;
                            query.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;

                            query.UpdatedBy = query.UpdatedBy;
                            query.UpdatedDate = DateTime.Now;
                            query.DocumentCancelledDate = DateTime.Now;
                            db.Entry(query).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }

                        //if invoice already generated intiate cancellation for that
                        var invoices = db.TransactionHeads.Where(x => x.ReferenceHeadID == headID);

                        foreach (var invoice in invoices)
                        {
                            if (invoice.TransactionStatusID == (byte)Eduegate.Framework.Enums.TransactionStatus.Complete)
                            {
                                var prrDocument = new SettingRepository().GetSettingDetail("PRRDOCUMENTTYPEID", query.CompanyID.Value);

                                if (prrDocument != null)
                                {
                                    TransactionHead newPRR = CloneTransaction(headID);
                                    newPRR.HeadIID = 0;
                                    newPRR.DocumentTypeID = int.Parse(prrDocument.SettingValue);
                                    newPRR.TransactionDate = DateTime.Now;
                                    newPRR.TransactionNo = GetNextTransactionNumber(newPRR.DocumentTypeID.Value);
                                    newPRR.DocumentStatusID = (int)Eduegate.Services.Contracts.Enums.DocumentStatuses.Approved;
                                    newPRR.TransactionStatusID = (int)Eduegate.Framework.Enums.TransactionStatus.New;
                                    newPRR.ReferenceHeadID = invoice.HeadIID;
                                    db.TransactionHeads.Add(newPRR);
                                    db.Entry(newPRR).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                                    if (newPRR.TransactionHeadEntitlementMaps != null)
                                    {
                                        foreach (var entitlment in newPRR.TransactionHeadEntitlementMaps)
                                        {
                                            db.Entry(entitlment).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                    }

                                    if (newPRR.OrderContactMaps != null)
                                    {
                                        foreach (var contact in newPRR.OrderContactMaps)
                                        {
                                            db.Entry(contact).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                    }

                                    if (newPRR.TransactionDetails != null)
                                    {
                                        foreach (var detail in newPRR.TransactionDetails)
                                        {
                                            db.Entry(detail).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                invoice.TransactionStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.Complete;
                                invoice.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;
                                invoice.UpdatedBy = query.UpdatedBy;
                                invoice.UpdatedDate = DateTime.Now;
                                db.Entry(invoice).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                                foreach (var detail in invoice.TransactionDetails)
                                {
                                    foreach (var serial in detail.ProductSerialMaps)
                                    {
                                        var inventory = db.ProductInventorySerialMaps.FirstOrDefault(a => a.SerialNo == serial.SerialNo);
                                        inventory.Used = false;
                                        db.Entry(inventory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                    }
                                }
                            }
                        }

                        // cancel all the jobs
                        var jobs = db.JobEntryHeads.Where(x => x.TransactionHeadID == headID);

                        foreach (var job in jobs)
                        {
                            job.JobStatusID = (int)Eduegate.Services.Contracts.Enums.JobStatuses.Cancel;
                            db.Entry(job).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }

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
            using (var db = new dbEduegateERPContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //update the status
                        var query = db.TransactionHeads.Where(x => x.HeadIID == headID)
                            .AsNoTracking()
                            .FirstOrDefault();

                        if (query.IsNotNull())
                        {
                            query.TransactionStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.Complete;
                            query.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;

                            query.UpdatedBy = query.UpdatedBy;
                            query.UpdatedDate = DateTime.Now;
                            query.DocumentCancelledDate = DateTime.Now;
                            db.Entry(query).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }

                        //if invoice already generated intiate cancellation for that
                        var invoices = db.TransactionHeads.Where(x => x.ReferenceHeadID == headID);

                        foreach (var invoice in invoices)
                        {
                            if (invoice.TransactionStatusID == (byte)Eduegate.Framework.Enums.TransactionStatus.Complete)
                            {
                                var prrDocument = new SettingRepository().GetSettingDetail("PRRDOCUMENTTYPEID", query.CompanyID.Value);

                                if (prrDocument != null)
                                {
                                    TransactionHead newPRR = CloneTransaction(headID);
                                    newPRR.HeadIID = 0;
                                    newPRR.DocumentTypeID = int.Parse(prrDocument.SettingValue);
                                    newPRR.TransactionDate = DateTime.Now;
                                    newPRR.TransactionNo = GetNextTransactionNumber(newPRR.DocumentTypeID.Value);
                                    newPRR.DocumentStatusID = (int)Eduegate.Services.Contracts.Enums.DocumentStatuses.Approved;
                                    newPRR.TransactionStatusID = (int)Eduegate.Framework.Enums.TransactionStatus.New;
                                    newPRR.ReferenceHeadID = invoice.HeadIID;
                                    db.TransactionHeads.Add(newPRR);
                                    db.Entry(newPRR).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                                    if (newPRR.TransactionHeadEntitlementMaps != null)
                                    {
                                        foreach (var entitlment in newPRR.TransactionHeadEntitlementMaps)
                                        {
                                            db.Entry(entitlment).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                    }

                                    if (newPRR.OrderContactMaps != null)
                                    {
                                        foreach (var contact in newPRR.OrderContactMaps)
                                        {
                                            db.Entry(contact).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                    }

                                    if (newPRR.TransactionDetails != null)
                                    {
                                        foreach (var detail in newPRR.TransactionDetails)
                                        {
                                            db.Entry(detail).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                invoice.TransactionStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.Complete;
                                invoice.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;
                                invoice.UpdatedBy = query.UpdatedBy;
                                invoice.UpdatedDate = DateTime.Now;
                                db.Entry(invoice).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                                foreach (var detail in invoice.TransactionDetails)
                                {
                                    foreach (var serial in detail.ProductSerialMaps)
                                    {
                                        var inventory = db.ProductInventorySerialMaps.FirstOrDefault(a => a.SerialNo == serial.SerialNo);
                                        inventory.Used = false;
                                        db.Entry(inventory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                    }
                                }
                            }
                        }

                        // cancel all the jobs
                        var jobs = db.JobEntryHeads.Where(x => x.TransactionHeadID == headID);

                        foreach (var job in jobs)
                        {
                            job.JobStatusID = (int)Eduegate.Services.Contracts.Enums.JobStatuses.Cancel;
                            db.Entry(job).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }

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
            using (var db = new dbEduegateERPContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //update the status
                        var query = db.TransactionHeads.Where(x => x.HeadIID == headID).AsNoTracking().FirstOrDefault();

                        if (query.IsNotNull())
                        {
                            query.TransactionStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.Complete;
                            query.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;

                            query.UpdatedBy = query.UpdatedBy;
                            query.UpdatedDate = DateTime.Now;
                            query.DocumentCancelledDate = DateTime.Now;
                            db.Entry(query).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }

                        //if invoice already generated intiate cancellation for that
                        var invoices = db.TransactionHeads.Where(x => x.ReferenceHeadID == headID);

                        foreach (var invoice in invoices)
                        {
                            if (invoice.TransactionStatusID == (byte)Eduegate.Framework.Enums.TransactionStatus.Complete)
                            {
                                var prrDocument = new SettingRepository().GetSettingDetail("PRRDOCUMENTTYPEID", query.CompanyID.Value);

                                if (prrDocument != null)
                                {
                                    TransactionHead newPRR = CloneTransaction(headID);
                                    newPRR.HeadIID = 0;
                                    newPRR.DocumentTypeID = int.Parse(prrDocument.SettingValue);
                                    newPRR.TransactionDate = DateTime.Now;
                                    newPRR.TransactionNo = GetNextTransactionNumber(newPRR.DocumentTypeID.Value);
                                    newPRR.DocumentStatusID = (int)Eduegate.Services.Contracts.Enums.DocumentStatuses.Approved;
                                    newPRR.TransactionStatusID = (int)Eduegate.Framework.Enums.TransactionStatus.New;
                                    newPRR.ReferenceHeadID = invoice.HeadIID;
                                    db.TransactionHeads.Add(newPRR);
                                    db.Entry(newPRR).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                                    if (newPRR.TransactionHeadEntitlementMaps != null)
                                    {
                                        foreach (var entitlment in newPRR.TransactionHeadEntitlementMaps)
                                        {
                                            db.Entry(entitlment).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                    }

                                    if (newPRR.OrderContactMaps != null)
                                    {
                                        foreach (var contact in newPRR.OrderContactMaps)
                                        {
                                            db.Entry(contact).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                    }

                                    if (newPRR.TransactionDetails != null)
                                    {
                                        foreach (var detail in newPRR.TransactionDetails)
                                        {
                                            db.Entry(detail).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                invoice.TransactionStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.Complete;
                                invoice.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;
                                invoice.UpdatedBy = query.UpdatedBy;
                                invoice.UpdatedDate = DateTime.Now;
                                db.Entry(invoice).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                                foreach (var detail in invoice.TransactionDetails)
                                {
                                    foreach (var serial in detail.ProductSerialMaps)
                                    {
                                        var inventory = db.ProductInventorySerialMaps.Where(a => a.SerialNo == serial.SerialNo).AsNoTracking().FirstOrDefault();
                                        inventory.Used = false;
                                        db.Entry(inventory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                    }
                                }
                            }
                        }

                        // cancel all the jobs
                        var jobs = db.JobEntryHeads.Where(x => x.TransactionHeadID == headID);

                        foreach (var job in jobs)
                        {
                            job.JobStatusID = (int)Eduegate.Services.Contracts.Enums.JobStatuses.Cancel;
                            db.Entry(job).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }

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
            using (var db = new dbEduegateERPContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var query = db.TransactionHeads.Where(x => x.HeadIID == headID)
                            .AsNoTracking()
                            .FirstOrDefault();

                        if (query.IsNotNull())
                        {
                            //generate a sales return request
                            query.TransactionStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.Complete;
                            query.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Cancelled;

                            query.UpdatedBy = query.UpdatedBy;
                            query.UpdatedDate = DateTime.Now;
                            query.DocumentCancelledDate = DateTime.Now;
                            db.Entry(query).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                            foreach (var detail in db.TransactionDetails.Where(x => x.HeadID == headID))
                            {
                                foreach (var serial in detail.ProductSerialMaps)
                                {
                                    var inventory = db.ProductInventorySerialMaps.Where(a => a.SerialNo == serial.SerialNo)
                                        .AsNoTracking()
                                        .FirstOrDefault();
                                    inventory.Used = false;
                                    db.Entry(inventory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                }
                            }

                            // cancel all the jobs
                            var jobs = db.JobEntryHeads.Where(x => x.TransactionHeadID == headID);

                            foreach (var job in jobs)
                            {
                                job.JobStatusID = (int)Eduegate.Services.Contracts.Enums.JobStatuses.Cancel;
                                db.Entry(job).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }

                            //Create a PRR
                            var prrDocument = new SettingRepository().GetSettingDetail("PRRDOCUMENTTYPEID", query.CompanyID.Value);

                            if (prrDocument != null)
                            {
                                TransactionHead newPRR = CloneTransaction(headID);
                                newPRR.HeadIID = 0;
                                newPRR.DocumentTypeID = int.Parse(prrDocument.SettingValue);
                                newPRR.TransactionDate = DateTime.Now;
                                newPRR.TransactionNo = GetNextTransactionNumber(newPRR.DocumentTypeID.Value);
                                newPRR.DocumentStatusID = (int)Eduegate.Services.Contracts.Enums.DocumentStatuses.Approved;
                                newPRR.TransactionStatusID = (int)Eduegate.Framework.Enums.TransactionStatus.New;
                                newPRR.ReferenceHeadID = query.HeadIID;
                                db.TransactionHeads.Add(newPRR);
                                db.Entry(newPRR).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                                if (newPRR.TransactionHeadEntitlementMaps != null)
                                {
                                    foreach (var entitlment in newPRR.TransactionHeadEntitlementMaps)
                                    {
                                        db.Entry(entitlment).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                    }
                                }

                                if (newPRR.OrderContactMaps != null)
                                {
                                    foreach (var contact in newPRR.OrderContactMaps)
                                    {
                                        db.Entry(contact).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                    }
                                }

                                if (newPRR.TransactionDetails != null)
                                {
                                    foreach (var detail in newPRR.TransactionDetails)
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
        public TransactionHead GetDeliveryDetails(long Id)
        {
            using (var db = new dbEduegateERPContext())
            {
                var entity = db.TransactionHeads.Where(a => a.HeadIID == Id)
                    .Include(x => x.OrderContactMaps)
                    .AsNoTracking()
                    .FirstOrDefault();
                return entity;
            }
        }

        public List<DeliveryTypes1> GetDeliveryOptions()
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                return dbContext.DeliveryTypes1
                    .AsNoTracking()
                    .ToList();
            }
        }

        public bool UpdateProduceSerialKeyStatus(List<string> serialKeys, bool usedFlag)
        {
            using (var db = new dbEduegateERPContext())
            {
                try
                {
                    foreach (var serialMap in db.ProductInventorySerialMaps.Where(a => serialKeys.Contains(a.SerialNo)))
                    {
                        serialMap.Used = usedFlag;
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

        public bool IsChangeRequestDetailProcessed(long changeRequestDetailID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.TransactionDetails.Where(d => d.ParentDetailID == changeRequestDetailID)
                    .AsNoTracking()
                    .Any();
            }
        }

        public decimal? BestSellersCountBySkuID(long SkuID, int companyID, int referenceID, long productTypeID, long documentStatusID, byte transactionStatusID)
        {
            using (var db = new dbEduegateERPContext())
            {
                return (from th in db.TransactionHeads
                        join td in db.TransactionDetails on th.HeadIID equals td.HeadID
                        join sk in db.ProductSKUMaps on td.ProductSKUMapID equals sk.ProductSKUMapIID
                        join p in db.Products on sk.ProductID equals p.ProductIID
                        join dc in db.DocumentTypes on th.DocumentTypeID equals dc.DocumentTypeID
                        where th.CompanyID == companyID && dc.ReferenceTypeID == referenceID && td.ProductSKUMapID == SkuID
                        && th.DocumentStatusID == documentStatusID && th.TransactionStatusID == transactionStatusID
                        select td)
                        .AsNoTracking()
                        .ToList()
                        .Sum(x => x.Quantity);
            }
        }

        public int? GetJobStatusByHeadID(long headID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if (db.JobEntryHeads.Where(a => a.TransactionHeadID == headID).Any())
                {
                    return db.JobEntryHeads.Where(a => a.TransactionHeadID == headID)
                        .AsNoTracking()
                        .FirstOrDefault().JobStatusID;
                }
                else
                {
                    return default(int?);
                }
            }
        }

        public void UpdateOrderDeliveryTextHeadMap(List<OrderDeliveryDisplayHeadMap> entity)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                foreach (var item in entity)
                {
                    dbContext.OrderDeliveryDisplayHeadMaps.Add(item);
                }
                dbContext.SaveChanges();
            }
        }

        public List<OrderDeliveryDisplayHeadMap> GetOrderDeliveryTextByHeadID(long headID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.OrderDeliveryDisplayHeadMaps.Where(x => x.HeadID == headID)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public TransactionDTO GetTransactionDTO(long headIID)
        {
            TransactionDTO transactionDTO = new TransactionDTO();
            transactionDTO.TransactionHead = new TransactionHeadDTO();
            transactionDTO.TransactionDetails = new List<TransactionDetailDTO>();
            TransactionDetailDTO transactionDetailDTO = null;

            using (var dbContext = new dbEduegateERPContext())
            {
                var transactionModel = dbContext.TransactionHeads
                    .Include(x => x.TransactionDetails)
                    //.Include(x => x.ProductSerialMaps)
                    .Include(x => x.TransactionDetails)
                    //.Include(x => x.ProductSKUMap)
                    .Include(x => x.Supplier)
                    .Include(x => x.Currency)
                    .Include(x => x.Customer)
                    .Include(x => x.DocumentType)
                    .Include(x => x.TransactionShipments)
                    .Include(x => x.TransactionHead2)
                    .AsNoTracking()
                    .FirstOrDefault(x => x.HeadIID == headIID);

                if (transactionModel != null)
                {
                    transactionDTO.TransactionHead.HeadIID = transactionModel.HeadIID;
                    transactionDTO.TransactionHead.Description = transactionModel.Description;
                    transactionDTO.TransactionHead.Reference = transactionModel.Reference;
                    transactionDTO.TransactionHead.CustomerID = transactionModel.CustomerID;
                    transactionDTO.TransactionHead.SupplierID = transactionModel.SupplierID;
                    transactionDTO.TransactionHead.SupplierLogiID = transactionModel.Supplier?.LoginID;
                    transactionDTO.TransactionHead.TransactionDate = transactionModel.TransactionDate.IsNull() ? (DateTime?)null : Convert.ToDateTime(transactionModel.TransactionDate);
                    transactionDTO.TransactionHead.TransactionNo = transactionModel.TransactionNo;
                    transactionDTO.TransactionHead.ReferenceTransactionNo = transactionModel.TransactionHead2 != null ? transactionModel.TransactionHead2.TransactionNo : null;
                    transactionDTO.TransactionHead.TransactionStatusID = transactionModel.TransactionStatusID;
                    transactionDTO.TransactionHead.DocumentStatusID = transactionModel.DocumentStatusID;
                    transactionDTO.TransactionHead.DocumentTypeID = transactionModel.DocumentTypeID;
                    transactionDTO.TransactionHead.DiscountAmount = transactionModel.DiscountAmount;
                    transactionDTO.TransactionHead.DiscountPercentage = transactionModel.DiscountPercentage;
                    transactionDTO.TransactionHead.BranchID = transactionModel.BranchID != null ? (long)transactionModel.BranchID : default(long);
                    transactionDTO.TransactionHead.ToBranchID = transactionModel.ToBranchID != null ? (long)transactionModel.ToBranchID : default(long);
                    transactionDTO.TransactionHead.CurrencyID = transactionModel.CurrencyID != null ? (int)transactionModel.CurrencyID : default(int);
                    transactionDTO.TransactionHead.DeliveryDate = transactionModel.DeliveryDate != null ? (DateTime)transactionModel.DeliveryDate : (DateTime?)null;
                    transactionDTO.TransactionHead.DeliveryTypeID = transactionModel.DeliveryMethodID != null ? (short)transactionModel.DeliveryMethodID : default(short);
                    transactionDTO.TransactionHead.DueDate = transactionModel.DueDate != null ? (DateTime)transactionModel.DueDate : (DateTime?)null;
                    transactionDTO.TransactionHead.EntitlementID = transactionModel.EntitlementID;
                    transactionDTO.TransactionHead.IsShipment = transactionModel.IsShipment != null ? (bool)transactionModel.IsShipment : default(bool);
                    transactionDTO.TransactionHead.ReferenceHeadID = transactionModel.ReferenceHeadID;
                    transactionDTO.TransactionHead.DocumentTypeName = transactionModel.DocumentType?.TransactionTypeName ?? default(string);

                    var supplier = transactionModel.Supplier;

                    if (supplier != null)
                        transactionDTO.TransactionHead.SupplierName = string.Concat(supplier.FirstName + " " + supplier.MiddleName + " " + supplier.LastName);

                    //stored in list for common mail send functionality
                    transactionDTO.TransactionHead.SupplierList = new List<KeyValueDTO>();
                    if (transactionModel.Supplier != null)
                    {
                        transactionDTO.TransactionHead.SupplierList.Add(new KeyValueDTO()
                        {
                            Key = transactionModel.Supplier.SupplierIID.ToString(),
                            Value = transactionModel.Supplier.FirstName.ToString()
                        });
                    }

                    var currency = transactionModel.Currency;

                    if (currency != null)
                        transactionDTO.TransactionHead.CurrencyName = currency.Name;

                    var customer = transactionModel.Customer;

                    if (customer != null)
                        transactionDTO.TransactionHead.CustomerName = string.Concat(customer.FirstName + customer.MiddleName + " " + customer.LastName);

                    if (transactionModel.TransactionDetails != null && transactionModel.TransactionDetails.Count > 0)
                    {
                        foreach (var transactionDetail in transactionModel.TransactionDetails)
                        {
                            transactionDetailDTO = new TransactionDetailDTO();
                            transactionDetailDTO.SKUDetails = new List<ProductSerialMapDTO>();

                            transactionDetailDTO.DetailIID = transactionDetail.DetailIID;
                            transactionDetailDTO.HeadID = transactionDetail.HeadID;
                            transactionDetailDTO.ProductID = transactionDetail.ProductID;

                            try
                            {
                                var searchQuery = string.Concat("select * from [catalog].[ProductListBySKU] where productskumapiid = ", transactionDetail.ProductSKUMapID);
                                var product = dbContext.ProductListBySKUs.FromSqlRaw($@"{searchQuery}")
                                    .AsNoTracking()
                                    .FirstOrDefault();

                                if (product != null)
                                {
                                    transactionDetailDTO.ProductName = product.SKU;
                                    transactionDetailDTO.ImageFile = product.ImageFile;
                                    transactionDetailDTO.SellingQuantityLimit = product.Quantity;

                                    if (product.Quantity != null && product.SellingQuantityLimit != null)
                                    {
                                        if (product.SellingQuantityLimit > product.Quantity)
                                            transactionDetailDTO.SellingQuantityLimit = product.SellingQuantityLimit;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }

                            transactionDetailDTO.ProductSKUMapID = transactionDetail.ProductSKUMapID;
                            transactionDetailDTO.UnitID = transactionDetail.UnitID;
                            transactionDetailDTO.UnitPrice = transactionDetail.UnitPrice;
                            transactionDetailDTO.Quantity = transactionDetail.Quantity;
                            transactionDetailDTO.DiscountPercentage = transactionDetail.DiscountPercentage;
                            transactionDetailDTO.Amount = transactionDetail.Amount;

                            if (transactionDetail.ProductSerialMaps != null && transactionDetail.ProductSerialMaps.Count > 0)
                            {
                                foreach (var serailMap in transactionDetail.ProductSerialMaps)
                                {
                                    var skuDetail = new ProductSerialMapDTO();
                                    skuDetail.SerialNo = serailMap.SerialNo;
                                    transactionDetailDTO.SKUDetails.Add(skuDetail);
                                }
                            }
                            transactionDTO.TransactionDetails.Add(transactionDetailDTO);
                        }
                    }

                    if (transactionModel.TransactionShipments.IsNotNull() && transactionModel.TransactionShipments.Count > 0)
                    {
                        foreach (var ts in transactionModel.TransactionShipments)
                        {
                            transactionDTO.ShipmentDetails = new ShipmentDetailDTO()
                            {
                                TransactionShipmentIID = ts.TransactionShipmentIID,
                                TransactionHeadID = ts.TransactionHeadID,
                                SupplierIDFrom = ts.SupplierIDFrom,
                                SupplierIDTo = ts.SupplierIDTo,
                                ShipmentReference = ts.ShipmentReference,
                                FreightCareer = ts.FreightCarrier,
                                ClearanceTypeID = ts.ClearanceTypeID,
                                AirWayBillNo = ts.AirWayBillNo,
                                FrieghtCharges = ts.FreightCharges,
                                BrokerCharges = ts.BrokerCharges,
                                AdditionalCharges = ts.AdditionalCharges,
                                Weight = ts.Weight,
                                NoOfBoxes = ts.NoOfBoxes,
                                BrokerAccount = ts.BrokerAccount,
                                Remarks = ts.Description,
                                CreatedBy = ts.CreatedBy,
                                UpdatedBy = ts.UpdatedBy,
                                CreatedDate = ts.CreatedDate,
                                UpdatedDate = ts.UpdatedDate,
                                //TimeStamps = ts.TimeStamps,
                            };
                        }
                    }
                }
            }

            return transactionDTO;
        }

        public List<HistoryHeader> GetOrderHistoryDetailsWithPagination(string branchID, string customerIID, int pageNumber, int pageSize)
        {
            var userHistoryList = new List<HistoryHeader>();
            HistoryHeader historyHeader = null;
            int branchIDTo = Convert.ToInt16(branchID);
            long customerID = Convert.ToInt64(customerIID);

            using (var dbContext = new dbEduegateERPContext())
            {
                var historyDetails = new List<TransactionHead>();
                var histDetails = (from transactionHead in dbContext.TransactionHeads
                                   join docType in dbContext.DocumentTypes on transactionHead.DocumentTypeID equals docType.DocumentTypeID
                                   join branch in dbContext.Branches on transactionHead.BranchID equals branch.BranchIID
                                   //join job in dbContext.JobEntryHeads on transactionHead.HeadIID equals job.TransactionHeadID
                                   where (transactionHead.BranchID == branchIDTo || branch.IsMarketPlace == true)
                                    && transactionHead.CustomerID == customerID
                                    && transactionHead.DocumentTypeID == 100
                                   //&& (docType.ReferenceTypeID == 1 || docType.ReferenceTypeID == 3)
                                   select new
                                   {
                                       transactionHead,
                                       //job.JobStatusID,
                                       //job.TransactionHeadID
                                   })
                                  .OrderByDescending(x => x.transactionHead.HeadIID)
                                  .AsNoTracking()
                                  .ToList();

                if (histDetails.IsNotNull() && histDetails.Count > 0)
                {
                    Branch branch = null;

                    foreach (var history in histDetails)
                    {
                        historyHeader = new HistoryHeader();
                        var shoppingcart = (from cartMaps in dbContext.TransactionHeadShoppingCartMaps
                                            where cartMaps.TransactionHeadID == history.transactionHead.HeadIID
                                            select cartMaps)
                                            .Include(x => x.ShoppingCart)
                                            .AsNoTracking()
                                            .FirstOrDefault();
                        //var voucherOrder = (from voucher in dbContext.ShoppingCartVoucherMaps
                        //                    where voucher.ShoppingCartID == shoppingcart.ShoppingCartID
                        //                    select voucher).FirstOrDefault();
                        historyHeader.DeliveryTypeID = history.transactionHead.DeliveryTypeID.HasValue
                            ? history.transactionHead.DeliveryTypeID.Value : 0;
                        historyHeader.TransactionOrderIID = history.transactionHead.HeadIID;
                        historyHeader.DocumentTypeID = history.transactionHead.DocumentTypeID;
                        historyHeader.CustomerID = history.transactionHead.CustomerID;
                        historyHeader.Description = history.transactionHead.Description;
                        historyHeader.SupplierID = history.transactionHead.SupplierID;
                        historyHeader.TransactionDate = history.transactionHead.TransactionDate;
                        historyHeader.TransactionNo = history.transactionHead.TransactionNo;
                        historyHeader.TransactionStatus = (int)history.transactionHead.TransactionStatusID;
                        historyHeader.DocumentStatusID = history.transactionHead.DocumentStatusID;
                        historyHeader.ActualOrderStatus = OnlineStoreGetTransactionStatus(history.transactionHead.HeadIID);
                        historyHeader.StudentID = history.transactionHead.StudentID;
                        historyHeader.AcademicYearID = history.transactionHead.AcademicYearID;
                        historyHeader.SchoolID = history.transactionHead.SchoolID;

                        historyHeader.CartPaymentMethod = shoppingcart.IsNotNull() ?
                            (from shoppingCart in dbContext.ShoppingCarts
                             where shoppingCart.ShoppingCartIID == shoppingcart.ShoppingCartID
                             select shoppingCart.PaymentMethod).FirstOrDefault().ToString() : null;
                        try
                        {
                            System.Type type = typeof(Eduegate.Framework.Payment.EntitlementType);
                            historyHeader.PaymentMethod = Enum.GetName(type, history.transactionHead.EntitlementID)?.ToString();
                        }
                        catch (Exception)
                        {
                            historyHeader.PaymentMethod = "";
                        }

                        historyHeader.DiscountAmount = history.transactionHead.DiscountAmount;
                        historyHeader.DiscountPercentage = history.transactionHead.DiscountPercentage;
                        historyHeader.ParentTransactionOrderIID = history.transactionHead.ReferenceHeadID != null
                                ? history.transactionHead.ReferenceHeadID : null;

                        historyHeader.LoyaltyPoints = (from a in dbContext.TransactionHeadPointsMaps
                                                       join b in dbContext.TransactionHeads on a.TransactionHeadID equals b.HeadIID
                                                       where b.HeadIID == history.transactionHead.HeadIID
                                                       select a.LoyaltyPoints).FirstOrDefault();

                        //if (voucherOrder != null)
                        //{
                        //    var vouchers = dbContext
                        //        .TransactionHeadEntitlementMaps
                        //        .Where(a => a.EntitlementID == (short)Eduegate.Framework.Payment.EntitlementType.Voucher &&
                        //        a.TransactionHeadID == history.transactionHead.HeadIID)
                        //        .FirstOrDefault();
                        //    if (vouchers.IsNotNull() && vouchers.Amount.HasValue)
                        //    {
                        //        dbContext.Entry(voucherOrder).Reference(a => a.Voucher).Load();
                        //        historyHeader.VoucherAmount = vouchers.Amount.Value;
                        //        historyHeader.VoucherNo = voucherOrder.Voucher.VoucherNo;
                        //    }
                        //}

                        try
                        {
                            historyHeader.DeliveryCharge = (decimal)history.transactionHead.DeliveryCharge;
                        }
                        catch (Exception ex)
                        {
                            historyHeader.DeliveryCharge = 0;
                        }

                        if (shoppingcart != null && shoppingcart.ShoppingCart != null && shoppingcart.ShoppingCart.BranchID.HasValue)
                        {
                            if (branch == null || branch.BranchIID != shoppingcart.ShoppingCart.BranchID)
                            {
                                branch = dbContext.Branches
                                    .Where(x => x.BranchIID == shoppingcart.ShoppingCart.BranchID)
                                    .FirstOrDefault();
                            }

                            historyHeader.BranchCode = branch.BranchCode;
                            historyHeader.BranchName = branch.BranchName;
                        }

                        var contact = dbContext.OrderContactMaps
                            .FirstOrDefault(x => x.OrderID == history.transactionHead.HeadIID && x.IsShippingAddress.Value);
                        historyHeader.DeliveryAddress = contact == null ? null :
                            new OrderContactMap()
                            {
                                AddressLine1 = contact.AddressLine1,
                                AddressLine2 = contact.AddressLine2,
                                FirstName = contact.FirstName,
                                AreaID = contact.AreaID,
                                Area = contact.Area,
                                BuildingNo = contact.BuildingNo,
                                LocationID = contact.LocationID,
                                LandmarkId = contact.LandmarkId,
                                LandMark = contact.LandMark,
                                MobileNo1 = contact.MobileNo1,
                                ContactID = contact.ContactID,
                                OrderContactMapIID = contact.OrderContactMapIID,
                            };

                        //var jobEntry = dbContext.JobEntryHeads
                        //    .Where(x => x.TransactionHeadID == history.TransactionHeadID)
                        //    .FirstOrDefault();
                        //historyHeader.UpdatedDate = history.transactionHead.JobEntryHead.UpdatedDate;
                        userHistoryList.Add(historyHeader);
                    }
                }
            }

            return userHistoryList;
        }

        public List<HistoryHeader> GetOrderHistoryItemDetails(string documentTypeID, string customerIID, int pageNumber, int pageSize, long orderID, int companyID)
        {
            List<HistoryHeader> userHistoryList = new List<HistoryHeader>();
            HistoryHeader historyHeader = null;
            int documentID = Convert.ToInt16(documentTypeID);
            long customerID = Convert.ToInt64(customerIID);

            using (var dbContext = new dbEduegateERPContext())
            {
                var historyDetails = new List<TransactionHead>();

                historyDetails = (from transactionHead in dbContext.TransactionHeads
                                  where
                                  //transactionHead.DocumentTypeID == documentID &&
                                  //transactionHead.CustomerID == customerID && 
                                  transactionHead.HeadIID == orderID
                                  select transactionHead)
                                  .Include(x => x.TransactionDetails)
                                  .AsNoTracking()
                                  .ToList();

                if (historyDetails.IsNotNull() && historyDetails.Count > 0)
                {
                    foreach (var history in historyDetails)
                    {
                        historyHeader = new HistoryHeader();

                        historyHeader.TransactionOrderIID = history.HeadIID;
                        historyHeader.DeliveryTypeID = history.DeliveryTypeID.HasValue ? history.DeliveryTypeID.Value : 0;
                        historyHeader.DeliveryDays = history.DeliveryDays.HasValue ? history.DeliveryDays.Value : 0;

                        if (history.TransactionDetails.IsNotNull() && history.TransactionDetails.Count > 0)
                        {
                            historyHeader.OrderDetails = new List<HistoryDetail>();

                            foreach (var transactionDetail in history.TransactionDetails)
                            {

                                var productDetail = new HistoryDetail();

                                productDetail.TransactionIID = transactionDetail.DetailIID;
                                productDetail.ProductID = transactionDetail.ProductID;

                                var skuDetails = dbContext.ProductSKUMaps.Where(a => a.ProductSKUMapIID == transactionDetail.ProductSKUMapID).AsNoTracking().FirstOrDefault();
                                var product = dbContext.Products.Where(x => x.ProductIID == transactionDetail.ProductID).AsNoTracking().FirstOrDefault();
                                productDetail.ProductName = skuDetails.IsNotNull() ? skuDetails.SKUName : product.IsNotNull() ? product.ProductName : null;

                                var productImageMaps = dbContext.ProductImageMaps.Where(p => p.ProductSKUMapID == transactionDetail.ProductSKUMapID).AsNoTracking().FirstOrDefault();
                                productDetail.ProductImageUrl = productImageMaps.IsNotNull() ? productImageMaps.ImageFile : null;

                                productDetail.ProductSKUMapID = transactionDetail.ProductSKUMapID;
                                productDetail.Quantity = transactionDetail.Quantity;
                                productDetail.UnitID = transactionDetail.UnitID;
                                productDetail.DiscountPercentage = transactionDetail.DiscountPercentage;
                                productDetail.UnitPrice = transactionDetail.UnitPrice;
                                decimal UnitPriceAmount = Convert.ToDecimal(transactionDetail.Amount);
                                productDetail.Amount = UnitPriceAmount;
                                productDetail.ExchangeRate = transactionDetail.ExchangeRate;
                                productDetail.SerialNumber = transactionDetail.SerialNumber != null ? transactionDetail.SerialNumber : "";
                                productDetail.PartNumber = skuDetails.IsNotNull() ? skuDetails.PartNo : string.Empty;
                                productDetail.BarCode = skuDetails.IsNotNull() ? skuDetails.BarCode : string.Empty;
                                historyHeader.OrderDetails.Add(productDetail);
                            }
                        }

                        userHistoryList.Add(historyHeader);
                    }
                }
            }

            return userHistoryList;
        }

        public int OnlineStoreGetTransactionStatus(long headID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var deliveryType = GetDeliveryType(headID);
                if (deliveryType == DeliveryTypes.Email)
                {
                    var referenceHead = dbContext.TransactionHeads.Where(a => a.ReferenceHeadID == headID).AsNoTracking().FirstOrDefault();
                    if (referenceHead.IsNotNull())
                    {
                        switch (referenceHead.TransactionStatusID)
                        {
                            case (int)Services.Contracts.Enums.TransactionStatus.Delivered:
                                return (int)Eduegate.Services.Contracts.Enums.ActualOrderStatus.Delivered;
                            case (int)Services.Contracts.Enums.TransactionStatus.Failed:
                                return (int)Eduegate.Services.Contracts.Enums.ActualOrderStatus.Failed;
                            case (int)Services.Contracts.Enums.TransactionStatus.Complete:
                                return (int)Eduegate.Services.Contracts.Enums.ActualOrderStatus.Delivered;
                            default:
                                return (int)Eduegate.Services.Contracts.Enums.ActualOrderStatus.Picking;//Other status apart from success & failed.
                        }
                    }
                    else
                        return (int)Eduegate.Services.Contracts.Enums.ActualOrderStatus.Picking;//Other status apart from success & failed.
                }
                else
                {
                    var head = dbContext.JobEntryHeads.Where(a => a.TransactionHeadID == headID).AsNoTracking().FirstOrDefault();
                    if (head.IsNotNull())
                    {
                        if (head.JobStatusID == (int)Eduegate.Services.Contracts.Enums.JobStatuses.FailedReceived || head.JobStatusID == (int)Eduegate.Services.Contracts.Enums.JobStatuses.FailedReceiving)
                            return (int)Eduegate.Services.Contracts.Enums.ActualOrderStatus.Failed;
                        else
                            return head.JobStatusID.HasValue ? (int)head.JobStatusID : 1;
                    }
                    else
                        return (int)Eduegate.Services.Contracts.Enums.ActualOrderStatus.OrderPlaced;//Other status apart from success & failed.
                }
            }
        }

        public DeliveryTypes GetDeliveryType(long headID)
        {
            using (var db = new dbEduegateERPContext())
            {
                var deliveryType = db.TransactionHeads.Where(a => a.HeadIID == headID).Select(a => a.DeliveryTypeID).FirstOrDefault();
                return deliveryType.HasValue ? (DeliveryTypes)(deliveryType) : DeliveryTypes.None;
            }
        }

        public bool InsertWrapToInventoryTransactions(InvetoryTransaction invetoryTransaction, string type)
        {
            bool isSuccess = false;
            dbEduegateERPContext db = new dbEduegateERPContext();

            try
            {
                //Get ProductBundles from ProductSKUMapID
                var bundleDatas = db.ProductBundles.Where(x => x.ToProductSKUMapID == invetoryTransaction.ProductSKUMapID).AsNoTracking().ToList();
                decimal? mainQty = 0;
                mainQty = invetoryTransaction.Quantity;

                // Add InvetoryTransactions
                if (bundleDatas.Count > 0)
                {
                    foreach (var dat in bundleDatas)
                    {
                        var bundleTransactions = db.InvetoryTransactions.Where(a => a.HeadID == invetoryTransaction.HeadID && a.ProductSKUMapID == dat.FromProductSKUMapID).FirstOrDefault();

                        if (bundleTransactions != null)
                        {
                            bundleTransactions.Amount = null;
                            bundleTransactions.Quantity = type == "wrap" ? -(mainQty * dat.Quantity) : Math.Abs((int)(mainQty * dat.Quantity));
                            bundleTransactions.ProductSKUMapID = dat.FromProductSKUMapID;

                            db.Entry(invetoryTransaction).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                }
                if (invetoryTransaction.InventoryTransactionIID > 0)
                {
                    isSuccess = true;
                }

            }
            catch (Exception ex)
            {
                isSuccess = false;
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to Add InvetoryTransactions.", TrackingCode.TransactionEngine);
                throw;
            }
            return isSuccess;
        }

        public List<HistoryHeader> GetScheduledOrderHistoryDetailsWithPagination(string branchID, string customerIID, int pageNumber, int pageSize)
        {
            var userHistoryList = new List<HistoryHeader>();
            HistoryHeader historyHeader = null;
            int branchIDTo = Convert.ToInt16(branchID);
            long customerID = Convert.ToInt64(customerIID);

            using (var dbContext = new dbEduegateERPContext())
            {
                var historyDetails = new List<TransactionHead>();
                var histDetails = (from transactionHead in dbContext.TransactionHeads
                                   join docType in dbContext.DocumentTypes on transactionHead.DocumentTypeID equals docType.DocumentTypeID
                                   join branch in dbContext.Branches on transactionHead.BranchID equals branch.BranchIID
                                   where (transactionHead.BranchID == branchIDTo || branch.IsMarketPlace == true)
                                    && transactionHead.CustomerID == customerID
                                    && (docType.ReferenceTypeID == 1 || docType.ReferenceTypeID == 3)
                                   select new { transactionHead })
                                  .OrderByDescending(x => x.transactionHead.HeadIID)
                                  .AsNoTracking().ToList();

                var scheduledelivery = new Domain.Setting.SettingBL(null).GetSettingValue<string>("SUBSCRIPTION_DELIVERY_ID");

                if (histDetails.IsNotNull() && histDetails.Count > 0)
                {
                    Branch branch = null;

                    foreach (var history in histDetails)
                    {
                        historyHeader = new HistoryHeader();
                        var shoppingcart = (from cartMaps in dbContext.TransactionHeadShoppingCartMaps
                                            where cartMaps.TransactionHeadID == history.transactionHead.HeadIID
                                            select cartMaps)
                                            .Include(x => x.ShoppingCart)
                                            .AsNoTracking()
                                            .FirstOrDefault();
                        if (history.transactionHead.DeliveryTypeID == int.Parse(scheduledelivery))
                        {
                            var timeslot = dbContext.DeliveryTypeTimeSlotMaps.Where(a => a.DeliveryTypeTimeSlotMapIID == shoppingcart.ShoppingCart.DeliveryTimeslotID).AsNoTracking().FirstOrDefault();
                            var scheduledDays = dbContext.ShoppingCartWeekDayMaps.Where(a => a.ShoppingCartID == shoppingcart.ShoppingCart.ShoppingCartIID).AsNoTracking().ToList();


                            historyHeader.DeliveryTypeID = history.transactionHead.DeliveryTypeID.HasValue
                                ? history.transactionHead.DeliveryTypeID.Value : 0;
                            historyHeader.TransactionOrderIID = history.transactionHead.HeadIID;
                            historyHeader.DocumentTypeID = history.transactionHead.DocumentTypeID;
                            historyHeader.CustomerID = history.transactionHead.CustomerID;
                            historyHeader.Description = history.transactionHead.Description;
                            historyHeader.SupplierID = history.transactionHead.SupplierID;
                            historyHeader.TransactionDate = history.transactionHead.TransactionDate;
                            historyHeader.TransactionNo = history.transactionHead.TransactionNo;
                            historyHeader.TransactionStatus = (int)history.transactionHead.TransactionStatusID;
                            historyHeader.DocumentStatusID = history.transactionHead.DocumentStatusID;
                            historyHeader.ActualOrderStatus = OnlineStoreGetTransactionStatus(history.transactionHead.HeadIID);
                            historyHeader.StudentID = history.transactionHead.StudentID;
                            historyHeader.AcademicYearID = history.transactionHead.AcademicYearID;
                            historyHeader.SchoolID = history.transactionHead.SchoolID;
                            historyHeader.StartDate = shoppingcart.ShoppingCart.StartDate;
                            historyHeader.EndDate = shoppingcart.ShoppingCart.EndDate;
                            historyHeader.SubscriptionType = shoppingcart.ShoppingCart.SubscriptionType?.SubscriptionName;
                            historyHeader.DeliveryTimeSlots = timeslot.IsNotNull() ? timeslot.TimeFrom + "-" + timeslot.TimeTo : null;

                            historyHeader.SubciptionDeliveryDays = scheduledDays.IsNotNull() ? scheduledDays : null;

                            historyHeader.CartPaymentMethod = shoppingcart.IsNotNull() ?
                                (from shoppingCart in dbContext.ShoppingCarts
                                 where shoppingCart.ShoppingCartIID == shoppingcart.ShoppingCartID
                                 select shoppingCart.PaymentMethod).FirstOrDefault().ToString() : null;
                            try
                            {
                                System.Type type = typeof(Eduegate.Framework.Payment.EntitlementType);
                                historyHeader.PaymentMethod = Enum.GetName(type, history.transactionHead.EntitlementID).ToString();
                            }
                            catch (Exception)
                            {
                                historyHeader.PaymentMethod = "";
                            }

                            historyHeader.DiscountAmount = history.transactionHead.DiscountAmount;
                            historyHeader.DiscountPercentage = history.transactionHead.DiscountPercentage;
                            historyHeader.ParentTransactionOrderIID = history.transactionHead.ReferenceHeadID != null
                                    ? history.transactionHead.ReferenceHeadID : null;

                            userHistoryList.Add(historyHeader);
                        }
                    }
                }
            }

            return userHistoryList;
        }

        public List<HistoryHeader> GetCanteenNormalOrderHistoryDetailsWithPagination(string branchID, string customerIID, int pageNumber, int pageSize)
        {
            var userHistoryList = new List<HistoryHeader>();
            HistoryHeader historyHeader = null;
            int branchIDTo = Convert.ToInt16(branchID);
            long customerID = Convert.ToInt64(customerIID);

            using (var dbContext = new dbEduegateERPContext())
            {
                var historyDetails = new List<TransactionHead>();
                var histDetails = (from transactionHead in dbContext.TransactionHeads
                                   join docType in dbContext.DocumentTypes on transactionHead.DocumentTypeID equals docType.DocumentTypeID
                                   join branch in dbContext.Branches on transactionHead.BranchID equals branch.BranchIID
                                   where (transactionHead.BranchID == branchIDTo || branch.IsMarketPlace == true)
                                    && transactionHead.CustomerID == customerID
                                    && transactionHead.DocumentTypeID == 55
                                    && transactionHead.DeliveryTypeID != 14
                                   select new { transactionHead })
                                  .OrderByDescending(x => x.transactionHead.HeadIID)
                                  .AsNoTracking()
                                  .ToList();

                var scheduledelivery = new Domain.Setting.SettingBL(null).GetSettingValue<string>("SUBSCRIPTION_DELIVERY_ID");

                if (histDetails.IsNotNull() && histDetails.Count > 0)
                {
                    Branch branch = null;

                    foreach (var history in histDetails)
                    {
                        historyHeader = new HistoryHeader();
                        var shoppingcart = (from cartMaps in dbContext.TransactionHeadShoppingCartMaps
                                            where cartMaps.TransactionHeadID == history.transactionHead.HeadIID
                                            select cartMaps)
                                            .Include(x => x.ShoppingCart)
                                            .AsNoTracking()
                                            .FirstOrDefault();
                        //if (history.transactionHead.DeliveryTypeID == int.Parse(scheduledelivery.SettingValue))
                        //{
                        var timeslot = dbContext.DeliveryTypeTimeSlotMaps.Where(a => a.DeliveryTypeTimeSlotMapIID == shoppingcart.ShoppingCart.DeliveryTimeslotID).AsNoTracking().FirstOrDefault();
                        var scheduledDays = dbContext.ShoppingCartWeekDayMaps.Where(a => a.ShoppingCartID == shoppingcart.ShoppingCart.ShoppingCartIID).AsNoTracking().ToList();


                        historyHeader.DeliveryTypeID = history.transactionHead.DeliveryTypeID.HasValue
                            ? history.transactionHead.DeliveryTypeID.Value : 0;
                        historyHeader.TransactionOrderIID = history.transactionHead.HeadIID;
                        historyHeader.DocumentTypeID = history.transactionHead.DocumentTypeID;
                        historyHeader.CustomerID = history.transactionHead.CustomerID;
                        historyHeader.Description = history.transactionHead.Description;
                        historyHeader.SupplierID = history.transactionHead.SupplierID;
                        historyHeader.TransactionDate = history.transactionHead.TransactionDate;
                        historyHeader.TransactionNo = history.transactionHead.TransactionNo;
                        historyHeader.TransactionStatus = (int)history.transactionHead.TransactionStatusID;
                        historyHeader.DocumentStatusID = history.transactionHead.DocumentStatusID;
                        historyHeader.ActualOrderStatus = OnlineStoreGetTransactionStatus(history.transactionHead.HeadIID);
                        historyHeader.StudentID = history.transactionHead.StudentID;
                        historyHeader.AcademicYearID = history.transactionHead.AcademicYearID;
                        historyHeader.SchoolID = history.transactionHead.SchoolID;
                        historyHeader.StartDate = shoppingcart.ShoppingCart.StartDate;
                        historyHeader.EndDate = shoppingcart.ShoppingCart.EndDate;
                        historyHeader.SubscriptionType = shoppingcart.ShoppingCart.SubscriptionType?.SubscriptionName;
                        historyHeader.DeliveryTimeSlots = timeslot.IsNotNull() ? timeslot.TimeFrom + "-" + timeslot.TimeTo : null;

                        historyHeader.SubciptionDeliveryDays = scheduledDays.IsNotNull() ? scheduledDays : null;

                        historyHeader.CartPaymentMethod = shoppingcart.IsNotNull() ?
                            (from shoppingCart in dbContext.ShoppingCarts
                             where shoppingCart.ShoppingCartIID == shoppingcart.ShoppingCartID
                             select shoppingCart.PaymentMethod).FirstOrDefault().ToString() : null;
                        try
                        {
                            System.Type type = typeof(Eduegate.Framework.Payment.EntitlementType);
                            historyHeader.PaymentMethod = Enum.GetName(type, history.transactionHead.EntitlementID).ToString();
                        }
                        catch (Exception)
                        {
                            historyHeader.PaymentMethod = "";
                        }

                        historyHeader.DiscountAmount = history.transactionHead.DiscountAmount;
                        historyHeader.DiscountPercentage = history.transactionHead.DiscountPercentage;
                        historyHeader.ParentTransactionOrderIID = history.transactionHead.ReferenceHeadID != null
                                ? history.transactionHead.ReferenceHeadID : null;

                        userHistoryList.Add(historyHeader);
                        //}
                    }
                }
            }

            return userHistoryList;
        }


        #region RFQ-- Start
        public List<TransactionDetailDTO> GetProductsByPurchaseRequestID(List<long> request_IDs)
        {
            var toDto = new List<TransactionDetailDTO>();
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var getData = dbContext.TransactionDetails.Where(x => request_IDs.Contains((long)x.HeadID))
                    .Include(x => x.ProductSKUMap).ThenInclude(y => y.Product)
                    .Include(x => x.Unit)
                    .ToList();

                if (getData != null || getData.Count > 0)
                {
                    foreach (var item in getData)
                    {
                        toDto.Add(new TransactionDetailDTO()
                        {
                            SKUID = new KeyValueDTO() { Key = item.ProductSKUMapID.ToString(), Value = item.ProductSKUMap?.Product?.ProductName },
                            ProductCode = item.ProductSKUMap?.Product?.ProductCode,
                            Description = item.ProductSKUMap?.Product?.ProductDescription,
                            Quantity = item.Quantity,
                            Amount = item.Amount,
                            Unit = item.Unit?.UnitCode,
                            UnitID = item.UnitID,
                            UnitGroupID = item.UnitGroupID,
                            ProductID = item.ProductID,
                            UnitPrice = item.UnitPrice,
                            Fraction = item.Fraction,
                            ForeignRate = item.ForeignRate,
                            Remark = item.Remark,
                        });
                    }
                }
            }
            return toDto;
        }

        //Save into Mapping table
        public string SaveRFQMappingData(TransactionDTO dto)
        {
            var toDto = dto as TransactionDTO;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var entity = new List<RFQSupplierRequestMap>();

                if (dto.TransactionHead.HeadIID != 0)
                {
                    var getData = dbContext.RFQSupplierRequestMaps.Where(x => x.HeadID == dto.TransactionHead.HeadIID).ToList();
                    dbContext.RFQSupplierRequestMaps.RemoveRange(getData);
                }

                foreach (var s in toDto.TransactionHead.SupplierList)
                {
                    foreach (var r in toDto.TransactionHead.PurchaseRequests)
                    {
                        entity.Add(new RFQSupplierRequestMap()
                        {
                            SupplierID = long.Parse(s.Key),
                            PurchaseRequestID = int.Parse(r.Key),
                            HeadID = toDto.TransactionHead.HeadIID,
                            TenderID = toDto.TransactionHead.TenderID,
                        });
                    }

                    // Save notification against Supplier 
                    if (toDto.TransactionHead.DocumentStatusID != 1)
                    {
                        var alertstatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULTALERTSTATUSID");

                        var notification = new List<NotificationAlert>();

                        notification.Add(new NotificationAlert()
                        {
                            NotificationAlertIID = 0,
                            Message = "You have a " + toDto.TransactionHead.TransactionNo + " from " + toDto.TransactionHead.BranchName,
                            FromLoginID = toDto.TransactionHead.UpdatedBy.HasValue ? toDto.TransactionHead.UpdatedBy : toDto.TransactionHead.CreatedBy,
                            ToLoginID = dbContext.Suppliers.FirstOrDefault(x => x.SupplierIID == long.Parse(s.Key)).LoginID,
                            NotificationDate = DateTime.Now,
                            CreatedDate = DateTime.Now,
                            AlertStatusID = int.Parse(alertstatus),
                        });

                        //Check notification already sent against the supplier
                        var alert = dbContext.NotificationAlerts.Where(x => x.ToLoginID == notification.FirstOrDefault().ToLoginID && notification.FirstOrDefault().Message == x.Message).ToList();
                        if (alert.Count <= 0)
                        {
                            new NotificationRepository().SaveAlerts(notification);
                        }
                    }
                }

                dbContext.RFQSupplierRequestMaps.AddRange(entity);
                dbContext.SaveChanges();
            }
            return "RFQ mapping data saved successfully";
        }

        public TransactionHeadDTO GetSuppliersAndPurchaseReqByHeadID(long HeadID)
        {
            var toDto = new TransactionHeadDTO();
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var getData = dbContext.RFQSupplierRequestMaps.Where(x => x.HeadID == HeadID)
                    .Include(x => x.Supplier)
                    .Include(x => x.PurchaseRequest)
                    .Include(x => x.Tender)
                    .AsNoTracking()
                    .ToList();

                if (getData != null && getData.Any())
                {
                    var suppliers = getData.Select(x => x.Supplier).Distinct().ToList();
                    var purchaseRequests = getData.Select(x => x.PurchaseRequest).Distinct().ToList();

                    foreach (var supplier in suppliers)
                    {
                        toDto.SupplierList.Add(new KeyValueDTO()
                        {
                            Key = supplier.SupplierIID.ToString(),
                            Value = supplier.FirstName + " " + supplier.MiddleName + " " + supplier.LastName,
                        });
                    }

                    foreach (var purchaseRequest in purchaseRequests)
                    {
                        toDto.PurchaseRequests.Add(new KeyValueDTO()
                        {
                            Key = purchaseRequest.HeadIID.ToString(),
                            Value = purchaseRequest.TransactionNo.ToString(),
                        });
                    }
                }

                toDto.TenderID = getData.FirstOrDefault().TenderID;

                toDto.Tender = toDto.TenderID.HasValue ?
                               new KeyValueDTO()
                               {
                                   Key = getData.FirstOrDefault().TenderID.ToString()
                               ,
                                   Value = getData.FirstOrDefault().Tender.Name.ToString()
                               } : null;

            }
            return toDto;
        }

        //Update Quotation from supplier
        public long UpdateQuotation(TransactionDTO dto)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var entity = db.TransactionHeads
                    .Include(x => x.TransactionDetails)
                    .FirstOrDefault(x => x.HeadIID == dto.TransactionHead.HeadIID);

                if (entity != null)
                {
                    entity.Remarks = dto.TransactionHead.Remarks;
                    entity.UpdatedDate = DateTime.Now;
                    entity.DocumentStatusID = dto.TransactionHead.DocumentStatusID;
                }

                foreach (var detail in dto.TransactionDetails)
                {
                    var tranDetail = entity.TransactionDetails.FirstOrDefault(d => d.DetailIID == detail.DetailIID);

                    tranDetail.Quantity = detail.Quantity;
                    tranDetail.UnitPrice = detail.UnitPrice;
                    tranDetail.Amount = detail.Amount;
                    tranDetail.UpdatedDate = DateTime.Now;

                    db.Entry(tranDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();

                return entity.HeadIID;
            }
        }

        //Quotation Compare
        public List<TransactionDetailDTO> FillQuotationItemList(List<long> quotation_IDs)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var toDto = new List<TransactionDetailDTO>();
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var getData = dbContext.TransactionDetails.Where(x => quotation_IDs.Contains((long)x.HeadID))
                    .Include(x => x.TransactionHead).ThenInclude(z => z.Supplier)
                    .Include(x => x.TransactionHead).ThenInclude(y => y.Currency)
                    .Include(x => x.ProductSKUMap).ThenInclude(y => y.Product)
                    .Include(x => x.Unit)
                    .ToList();

                if (getData != null || getData.Count > 0)
                {
                    foreach (var item in getData)
                    {
                        var refTransaction = dbContext.TransactionDetails
                            .FirstOrDefault(t => t.HeadID == item.TransactionHead.ReferenceHeadID && t.ProductSKUMapID == item.ProductSKUMapID);

                        toDto.Add(new TransactionDetailDTO()
                        {
                            DetailIID = item.DetailIID,
                            HeadID = item.HeadID,
                            SKUID = new KeyValueDTO() { Key = item.ProductSKUMapID.ToString(), Value = item.ProductSKUMap?.Product?.ProductName },
                            ProductCode = item.ProductSKUMap?.Product?.ProductCode,
                            Description = item.ProductSKUMap?.Product?.ProductDescription,
                            Quantity = item.Quantity,
                            Amount = item.Amount,
                            Unit = item.Unit?.UnitCode,
                            UnitID = item.UnitID,
                            UnitGroupID = item.UnitGroupID,
                            ProductID = item.ProductID,
                            UnitPrice = item.UnitPrice,
                            QuotationNo = item.TransactionHead.TransactionNo,
                            SupplierID = item.TransactionHead.SupplierID,
                            Supplier = item.TransactionHead.Supplier.FirstName,
                            SupplierCode = item.TransactionHead.Supplier.SupplierCode,
                            Fraction = item.Fraction,
                            ForeignRate = item.ForeignRate,
                            CurrencyName = item.TransactionHead.Currency?.DisplayCode,

                            //For Bid Opening
                            SubmittedDateString = item.TransactionHead.TransactionDate.HasValue ? item.TransactionHead.TransactionDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                            QTDiscount = item.TransactionHead.DiscountAmount,
                            GrossAmount = refTransaction?.Amount,
                        });
                    }
                }
            }
            return toDto;
        }

        public List<KeyValueDTO> FillQuotationsByRFQ(string rfqHeadIID)
        {
            var toDto = new List<KeyValueDTO>();
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var headID = long.Parse(rfqHeadIID);
                var submittedID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TRANSACTION_DOC_STS_ID_SUBMITTED");
                var getData = dbContext.TransactionHeads.Where(x => x.ReferenceHeadID == headID && x.DocumentStatusID == long.Parse(submittedID))
                    .AsNoTracking()
                    .ToList();

                foreach (var val in getData)
                {
                    toDto.Add(new KeyValueDTO()
                    {
                        Key = val.HeadIID.ToString(),
                        Value = val.TransactionNo.ToString()
                    });
                }
            }
            return toDto;
        }

        #endregion


        #region Bid Opening Module 

        public List<long> GetQuotationIDsByTenderID(long? tenderIID)
        {
            var data = new List<long>();
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var IIDs = dbContext.RFQSupplierRequestMaps.Where(a => a.TenderID == tenderIID)
                .Select(a => a.HeadID).ToList();

                data = dbContext.TransactionHeads.Where(x => IIDs.Contains(x.ReferenceHeadID)).Select(x => x.HeadIID)
                    .ToList();
            }
            return data;
        }


        //Save Bid Item list 
        public TransactionDTO SaveBidApprovalItemList(TransactionDTO dto)
        {
            var toDto = dto as TransactionDTO;

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                //Check already approved the bid
                var checkList = dbContext.BidApprovalHeads.FirstOrDefault(x => x.TenderID == toDto.TransactionHead.TenderID && x.ApproverAuthID == toDto.TransactionHead.CreatedBy);

                if (checkList == null)
                {
                    var entity = new BidApprovalHead()
                    {
                        TenderID = toDto.TransactionHead.TenderID,
                        TransactionNo = GetNextTransactionNumber(toDto.TransactionHead.DocumentTypeID.Value),
                        ApproverAuthID = toDto.TransactionHead.CreatedBy,
                        CreatedBy = toDto.TransactionHead.CreatedBy,
                        CreatedDate = DateTime.Now,
                        TotalQuantity = toDto.TransactionHead.TransactionDetails.Count(),
                        DocumentTypeID = toDto.TransactionHead.DocumentTypeID,
                        DocumentStatusID = (long?)Eduegate.Services.Contracts.Enums.DocumentStatuses.Approved,
                        NetAmount = toDto.TransactionHead.TransactionDetails.Sum(x => x.Amount),
                    };

                    dbContext.BidApprovalHeads.Add(entity);

                    entity.BidApprovalDetails = new List<BidApprovalDetail>();

                    foreach (var detail in toDto.TransactionHead.TransactionDetails)
                    {
                        entity.BidApprovalDetails.Add(new BidApprovalDetail()
                        {
                            BidApprovalID = entity.BidApprovalIID,
                            ReferenceHeadID = detail.HeadID,
                            ProductID = detail.ProductID,
                            ProductSKUMapID = long.Parse(detail.SKUID.Key.ToString()),
                            Quantity = detail.Quantity,
                            UnitID = detail.UnitID,
                            UnitPrice = detail.UnitPrice,
                            Amount = detail.Amount,
                            Fraction = detail.Fraction,
                            ForiegnRate = detail.ForeignRate,
                            UnitGroupID = detail.UnitGroupID
                        });
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    dbContext.SaveChanges();

                    var updateLog = dbContext.TenderAuthenticationLogs.FirstOrDefault(x => x.TenderID == entity.TenderID && x.AuthenticationID == entity.ApproverAuthID);

                    updateLog.IsTenderApproved = true;
                    dbContext.Entry(updateLog).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbContext.SaveChanges();

                    toDto.IsError = false;
                    toDto.ReturnMessage = "Approved successfully";
                }
                else
                {
                    toDto.IsError = true;
                    toDto.ReturnMessage = "This bid has already received your approval.";
                }

            }
            return toDto;
        }

        public List<Eduegate.Services.Contracts.Payroll.EmployeeDTO> GetQTSubmissionEmployeeMaiIDs(long headIID)
        {
            var returnDTO = new List<Eduegate.Services.Contracts.Payroll.EmployeeDTO>();
            using (var dbContext = new dbEduegateERPContext())
            {
                var purchaseReqHeadID = dbContext.TransactionHeads
                    .Include(x => x.TransactionHead2).ThenInclude(x => x.RFQSupplierRequestMapHeads)
                    .FirstOrDefault(x => x.HeadIID == headIID).TransactionHead2.RFQSupplierRequestMapHeads.FirstOrDefault().PurchaseRequestID;

                var getEmployee = dbContext.TransactionHeads
                    .Include(x => x.Employee)
                    .Include(x => x.ApprovedByNavigation)
                    .FirstOrDefault(x => x.HeadIID == purchaseReqHeadID);

                returnDTO = new List<Eduegate.Services.Contracts.Payroll.EmployeeDTO>();
                if (getEmployee != null)
                {
                    if (getEmployee.Employee != null)
                    {
                        returnDTO.Add(new Eduegate.Services.Contracts.Payroll.EmployeeDTO()
                        {
                            LoginID = getEmployee.Employee.LoginID,
                            EmployeeIID = getEmployee.Employee.EmployeeIID,
                            EmployeeName = getEmployee.Employee.FirstName + " " + getEmployee.Employee.MiddleName + " " + getEmployee.Employee.LastName,
                            WorkEmail = getEmployee.Employee.WorkEmail
                        });
                    }

                    if (getEmployee.ApprovedByNavigation != null)
                    {
                        returnDTO.Add(new Eduegate.Services.Contracts.Payroll.EmployeeDTO()
                        {
                            LoginID = getEmployee.ApprovedByNavigation.LoginID,
                            EmployeeIID = getEmployee.ApprovedByNavigation.EmployeeIID,
                            EmployeeName = getEmployee.ApprovedByNavigation.FirstName + " " + getEmployee.ApprovedByNavigation.MiddleName + " " + getEmployee.ApprovedByNavigation.LastName,
                            WorkEmail = getEmployee.ApprovedByNavigation.WorkEmail
                        });
                    }
                }
            }

            return returnDTO;
        }

        public List<KeyValueDTO> FillBidLookUpByRFQ(string rfqHeadIID)
        {
            var toDto = new List<KeyValueDTO>();
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var headID = long.Parse(rfqHeadIID);
                var approvedStsID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TRANSACTION_DOC_STS_ID_APPROVED");
                var QTHeadIds = dbContext.TransactionHeads.Where(x => x.ReferenceHeadID == headID)
                                .Select(x => x.HeadIID)
                                .ToList();

                var getData = dbContext.BidApprovalHeads
                    .Include(x => x.BidApprovalDetails)
                    .Where(x => x.BidApprovalDetails.Any(y => QTHeadIds.Contains((long)y.ReferenceHeadID)) &&
                                x.DocumentStatusID == long.Parse(approvedStsID))
                    .ToList();

                foreach (var val in getData)
                {
                    toDto.Add(new KeyValueDTO()
                    {
                        Key = val.BidApprovalIID.ToString(),
                        Value = val.TransactionNo.ToString()
                    });
                }
            }
            return toDto;
        }

        public List<TransactionDetailDTO> FillBidItemList(string bidApprovalIID)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var toDto = new List<TransactionDetailDTO>();
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var getData = dbContext.BidApprovalDetails.Where(x => x.BidApprovalID == long.Parse(bidApprovalIID)).ToList();

                if (getData != null || getData.Count > 0)
                {
                    foreach (var item in getData)
                    {
                        var tranDetail = dbContext.TransactionDetails
                            .Include(x => x.TransactionHead).ThenInclude(z => z.Supplier)
                            .Include(x => x.ProductSKUMap).ThenInclude(y => y.Product)
                            .Include(x => x.Unit)
                            .FirstOrDefault(t => t.HeadID == item.ReferenceHeadID && t.ProductSKUMapID == item.ProductSKUMapID);

                        toDto.Add(new TransactionDetailDTO()
                        {
                            DetailIID = item.DetailIID,
                            HeadID = item.ReferenceHeadID,
                            SKUID = new KeyValueDTO() { Key = item.ProductSKUMapID.ToString(), Value = tranDetail.ProductSKUMap?.Product?.ProductName },
                            ProductCode = tranDetail.ProductSKUMap?.Product?.ProductCode,
                            Description = tranDetail.ProductSKUMap?.Product?.ProductDescription,
                            Quantity = item.Quantity,
                            Amount = item.Amount,
                            Unit = tranDetail.Unit?.UnitCode,
                            UnitID = item.UnitID,
                            UnitGroupID = item.UnitGroupID,
                            ProductID = item.ProductID,
                            UnitPrice = item.UnitPrice,
                            QuotationNo = tranDetail.TransactionHead.TransactionNo,
                            SupplierID = tranDetail.TransactionHead.SupplierID,
                            Supplier = tranDetail.TransactionHead.Supplier?.FirstName,
                            SupplierCode = tranDetail.TransactionHead.Supplier?.SupplierCode,
                            Fraction = item.Fraction,
                            ForeignRate = item.ForiegnRate,
                        });
                    }
                }
            }
            return toDto;
        }

        #endregion


        public Eduegate.Services.Contracts.Inventory.TransactionSummaryDetailDTO GetTransactionCountsForDashboard(int docTypeID,byte? schoolID)
        {
            var result = new TransactionSummaryDetailDTO();

            using (var dbContext = new dbEduegateERPContext())
            {
                var transactions = dbContext.TransactionDetails
                    .Include(x => x.TransactionHead)
                    .AsNoTracking()
                    .Where(x => x.TransactionHead.DocumentTypeID == docTypeID && x.TransactionHead.SchoolID == schoolID).AsNoTracking().ToList();

                var dailyAmount = transactions?.Where(y => y.TransactionHead.TransactionDate.Value.Date == DateTime.Now.Date).Sum(s => s.Amount);
                var monthlyAmount = transactions?.Where(y => y.TransactionHead.TransactionDate.Value.Month == DateTime.Now.Month && y.TransactionHead.TransactionDate.Value.Year == DateTime.Now.Year).Sum(s => s.Amount);
                var yearlyAmount = transactions?.Where(y => y.TransactionHead.TransactionDate.Value.Year == DateTime.Now.Year).Sum(s => s.Amount);

                result.YearlyAmount = yearlyAmount;
                result.MonthlyAmount = monthlyAmount;
                result.DailyAmount = dailyAmount;

                return result;
            }
        }
    }
}
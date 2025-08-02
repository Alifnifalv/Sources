using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using Eduegate.Domain.Entity.CustomEntity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity.Models.ValueObjects;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper;
using Eduegate.Framework.Logs;

namespace Eduegate.Domain.Repository
{
    public class FixedAssetsRepository
    {

        public List<AssetCategory> GetAssetCategories()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.AssetCategories.ToList();
            }
        }

        public List<Asset> GetAssetCodes()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Assets.ToList();
            }
        }

        public List<Asset> SaveAssets(List<Asset> assetList)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                List<Asset> assetsInDB = dbContext.Assets.ToList();
                foreach (Asset assetItem in assetList)
                {
                    long assetIID = assetItem.AssetIID;
                    var assetCode = assetItem.AssetCode;
                    if (assetIID == 0)
                    {
                        Asset assetItemInDB_ByAssetCode = assetsInDB.Where(x => x.AssetCode == assetCode ).FirstOrDefault();
                        if (assetItemInDB_ByAssetCode != null)
                        {
                            //Duplicate Asset Code
                            return assetList;
                        }
                        dbContext.Assets.Add(assetItem);
                    }
                    else
                    {
                        //Check asset code duplicate
                        Asset assetItemInDB_ByAssetCode = assetsInDB.Where(x => x.AssetCode == assetCode && x.AssetIID != assetIID).FirstOrDefault();
                        if (assetItemInDB_ByAssetCode != null)
                        {
                            //Duplicate Asset Code
                            return assetList;
                        }

                        Asset assetItemInDB = assetsInDB.Where(x => x.AssetIID == assetIID).FirstOrDefault();
                        assetItemInDB.AssetCode = assetItem.AssetCode;
                        assetItemInDB.AccumulatedDepGLAccID = assetItem.AccumulatedDepGLAccID;
                        assetItemInDB.AssetCategoryID = assetItem.AssetCategoryID;
                        assetItemInDB.AssetGlAccID = assetItem.AssetGlAccID;
                        assetItemInDB.DepreciationExpGLAccId = assetItem.DepreciationExpGLAccId;
                        assetItemInDB.DepreciationYears = assetItem.DepreciationYears;
                        assetItemInDB.Description = assetItem.Description;
                        assetItemInDB.UpdatedBy = assetItem.UpdatedBy;
                        assetItemInDB.UpdatedDate = assetItem.UpdatedDate;
                    }
                }
                dbContext.SaveChanges();
            }

            return assetList;
        }

        public Asset GetAssetById(long AssetId)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Assets
                                    .Where(x => x.AssetIID == AssetId)
                                    .Include(x => x.Account)
                                    .Include(x => x.Account1)
                                    .Include(x => x.Account2)
                                    .Include(x => x.AssetCategory)
                                    .Include(x => x.AssetTransactionDetails)
                                    .Include("AssetTransactionDetails.AssetTransactionHead")
                                    .FirstOrDefault();
            }
        }

        public decimal GetAccumulatedDepreciation(long AssetId, int documentTypeID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var a = dbContext.AssetTransactionDetails
                                                                       .Include("AssetTransactionHead")
                                                                       .Where(x => x.AssetID == AssetId && x.AssetTransactionHead.DocumentTypeID == documentTypeID)
                                                               .Sum(x => x.Amount);


                decimal AccumulatedDepreciation = Convert.ToDecimal(dbContext.AssetTransactionDetails
                                                                       .Include("AssetTransactionHead")
                                                                       .Where(x => x.AssetID == AssetId && x.AssetTransactionHead.DocumentTypeID == documentTypeID)
                                                               .Sum(x => x.Amount));
                return AccumulatedDepreciation;
            }
        }

        public bool DeleteAsset(long AssetId)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                dbContext.Assets.Remove(dbContext.Assets.Where(x => x.AssetIID == AssetId).FirstOrDefault());
                dbContext.SaveChanges();
            }
            return true;
        }

        public AssetTransactionHead SaveAssetTransaction(AssetTransactionHead assetTransactionHead)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                  var assetTransactionHeadInDB=  dbContext.AssetTransactionHeads
                        .Include(x => x.AssetTransactionDetails)
                        .Where(x => x.HeadIID == assetTransactionHead.HeadIID).FirstOrDefault();
                    if(assetTransactionHeadInDB==null) //ADD
                    {
                        dbContext.AssetTransactionHeads.Add(assetTransactionHead);
                    }
                    else //EDIT
                    {
                        #region EDIT
                        assetTransactionHeadInDB.Remarks = assetTransactionHead.Remarks;
                        assetTransactionHeadInDB.DocumentStatusID = assetTransactionHead.DocumentStatusID;
                        assetTransactionHeadInDB.DocumentTypeID = assetTransactionHead.DocumentTypeID;
                        assetTransactionHeadInDB.UpdatedBy = assetTransactionHead.UpdatedBy;
                        assetTransactionHeadInDB.UpdatedDate = assetTransactionHead.UpdatedDate;
                        if(assetTransactionHeadInDB.AssetTransactionDetails==null)
                        {
                            assetTransactionHeadInDB.AssetTransactionDetails = new List<AssetTransactionDetail>();
                        }
                        foreach (AssetTransactionDetail detailItem in assetTransactionHead.AssetTransactionDetails)
                        {

                           var assetTransactionDetailInDb= assetTransactionHeadInDB.AssetTransactionDetails.Where(x => x.DetailIID == detailItem.DetailIID).FirstOrDefault();
                            if(detailItem.DetailIID == 0)
                            {
                                assetTransactionHeadInDB.AssetTransactionDetails.Add(detailItem);
                            }
                            else
                            {
                                assetTransactionDetailInDb.Amount = detailItem.Amount;
                                assetTransactionDetailInDb.AssetID = detailItem.AssetID;
                                assetTransactionDetailInDb.Quantity = detailItem.Quantity;
                                assetTransactionDetailInDb.StartDate = detailItem.StartDate;
                                assetTransactionDetailInDb.UpdatedBy = detailItem.UpdatedBy;
                                assetTransactionDetailInDb.UpdatedDate = detailItem.UpdatedDate;
                                assetTransactionDetailInDb.AccountID = detailItem.AccountID;

                            }
                        }
                        #endregion

                        #region DELETE
                        var transDetailIIDs = new HashSet<long>(assetTransactionHead.AssetTransactionDetails.Select(x => x.DetailIID));
                        var DeletableIds = new HashSet<long>(assetTransactionHeadInDB.AssetTransactionDetails.Where(x => !transDetailIIDs.Contains(x.DetailIID)).Select(a => a.DetailIID));
                        foreach (long id in DeletableIds)
                        {
                            var deleteItem = assetTransactionHeadInDB.AssetTransactionDetails.Where(x => x.DetailIID == id).FirstOrDefault();
                            if (deleteItem != null)
                            {
                                assetTransactionHeadInDB.AssetTransactionDetails.Remove(deleteItem);
                            }
                        }
                        #endregion
                    }


                    dbContext.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return assetTransactionHead;
        }

        public AssetTransactionHead GetAssetTransactionHeadById(long HeadID)
        {
            AssetTransactionHead assetTransactionHead = null;
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    assetTransactionHead = dbContext.AssetTransactionHeads
                                                    .Include(x => x.AssetTransactionDetails)
                                                    .Include("AssetTransactionDetails.Asset")
                                                    .Include("AssetTransactionDetails.Asset.AssetCategory")
                                                    .Include("AssetTransactionDetails.Account")
                                                    .Include(x => x.DocumentReferenceStatusMap)
                                                    .Include("DocumentReferenceStatusMap.DocumentStatus")
                                                    .Include(x => x.DocumentType)
                                                    .Include(x => x.TransactionStatus)
                                                    .Where(x => x.HeadIID == HeadID).FirstOrDefault();

                }
            }
            catch (Exception)
            {

                throw;
            }
            return assetTransactionHead;
        }

        public List<AssetTransactionHead> GetAssetTransactionHeads(DocumentReferenceTypes referenceType, Eduegate.Framework.Enums.TransactionStatus transactionStatus)
        {
            //long HeadID = 0;
            List<AssetTransactionHead> assetTransactionHeadList = null;
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    assetTransactionHeadList = dbContext.AssetTransactionHeads
                        .Where(x=>x.ProcessingStatusID == (int)transactionStatus && x.DocumentType.DocumentReferenceType.ReferenceTypeID == (int)referenceType)                        
                                                    .Include(x => x.AssetTransactionDetails)
                                                    .Include("AssetTransactionDetails.Asset")
                                                    .Include("AssetTransactionDetails.Asset.AssetCategory")
                                                    .Include("AssetTransactionDetails.Account")
                                                    .Include(x => x.DocumentReferenceStatusMap)
                                                    .Include("DocumentReferenceStatusMap.DocumentStatus")
                                                    .Include(x => x.DocumentType)
                                                    .Include(x => x.TransactionStatus)
                                                    .Include(x => x.DocumentType)
                                                    .Include("DocumentType.DocumentReferenceType")
                                                    .ToList();

                }
            }
            catch (Exception)
            {

                throw;
            }
            return assetTransactionHeadList;
        }

        public bool UpdateAssetTransactionHead(AssetTransactionHead assetTransactionHead)
        {
            bool isSuccess = false;
            dbEduegateERPContext db = new dbEduegateERPContext();

            try
            {
                AssetTransactionHead query = db.AssetTransactionHeads.FirstOrDefault(x => x.HeadIID == assetTransactionHead.HeadIID);

                if (query.IsNotNull())
                {
                    if (query.ProcessingStatusID == (byte)Eduegate.Services.Contracts.Enums.TransactionStatus.Complete ||
                        query.ProcessingStatusID == (byte)Eduegate.Services.Contracts.Enums.TransactionStatus.Confirmed ||
                        query.ProcessingStatusID == (byte)Eduegate.Services.Contracts.Enums.TransactionStatus.Cancelled)
                        return isSuccess;

                    query.ProcessingStatusID = assetTransactionHead.ProcessingStatusID;
                    query.DocumentStatusID = assetTransactionHead.DocumentStatusID;
                    query.UpdatedBy = assetTransactionHead.UpdatedBy;
                    query.UpdatedDate = DateTime.Now;

                    db.SaveChanges();
                }
                isSuccess = true;
            }
            catch (Exception ex)
            {
                isSuccess = false;
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not ablet to update TransactionHead. HeadIID:" + assetTransactionHead.HeadIID.ToString(), TrackingCode.TransactionEngine);
                throw;
            }
            return isSuccess;
        }
        public List<Asset> GetAssetCodesSearch(string SearchText)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Assets
                    .Take(Framework.Extensions.ConfigurationExtensions.GetAppConfigValue<int>("MaxFetchCount"))
                    .Where(x=>x.AssetCode.Contains(SearchText)).ToList();
            }
        }
        public List<Account> GetAccountCodesSearch(string SearchText)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                //return dbContext.Accounts
                //    .Take(Framework.Extensions.ConfigurationExtensions.GetAppConfigValue<int>("MaxFetchCount"))
                //    .Where(x => x.AccountName.Contains(SearchText)).ToList();

                return dbContext.Accounts.Where(x => x.AccountName != null || x.AccountName.Contains(SearchText)).ToList();
            }
        }

        public Asset GetAssetByAssetCode(string AssetCode)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Assets
                    .Where(x => x.AssetCode == AssetCode)
                       .Include(x => x.Account)
                                    .Include(x => x.Account1)
                                    .Include(x => x.Account2)
                                    .Include(x => x.AssetCategory)
                                    .Include(x => x.AssetTransactionDetails)
                                    .Include("AssetTransactionDetails.AssetTransactionHead")
                    .FirstOrDefault();
            }
        }

    }
}

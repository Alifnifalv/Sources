using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Domain.Entity.Accounts;
using Eduegate.Domain.Entity.Accounts.Models.Accounts;
using Eduegate.Domain.Entity.Accounts.Models.Assets;
using Eduegate.Framework;

namespace Eduegate.Domain.Repository
{
    public class FixedAssetsRepository
    {
        public List<AssetCategory> GetAssetCategories()
        {
            using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
            {
                return dbContext.AssetCategories.AsNoTracking().ToList();
            }
        }

        public List<Asset> GetAssetCodes()
        {
            using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
            {
                return dbContext.Assets.AsNoTracking().ToList();
            }
        }

        public List<Asset> SaveAssets(List<Asset> assetList)
        {
            using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
            {
                List<Asset> assetsInDB = dbContext.Assets.AsNoTracking().ToList();
                foreach (Asset assetItem in assetList)
                {
                    long assetIID = assetItem.AssetIID;
                    var assetCode = assetItem.AssetCode;
                    if (assetIID == 0)
                    {
                        Asset assetItemInDB_ByAssetCode = assetsInDB.Where(x => x.AssetCode == assetCode).FirstOrDefault();
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
            using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
            {
                return dbContext.Assets
                                    .Where(x => x.AssetIID == AssetId)
                                    .Include(x => x.AssetGlAcc)
                                    .Include(x => x.AccumulatedDepGLAcc)
                                    .Include(x => x.DepreciationExpGLAcc)
                                    .Include(x => x.AssetCategory)
                                    .Include(x => x.AssetTransactionDetails)
                                    .Include(i => i.AssetTransactionDetails).ThenInclude(i => i.Head)
                                    .AsNoTracking()
                                    .FirstOrDefault();
            }
        }

        public decimal GetAccumulatedDepreciation(long AssetId, int documentTypeID)
        {
            using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
            {
                var a = dbContext.AssetTransactionDetails
                    .Include(i => i.Head)
                    .AsNoTracking()
                    .Where(x => x.AssetID == AssetId && x.Head.DocumentTypeID == documentTypeID)
                    .Sum(x => x.Amount);


                decimal AccumulatedDepreciation = Convert.ToDecimal(dbContext.AssetTransactionDetails
                    .Include(i => i.Head)
                    .AsNoTracking()
                    .Where(x => x.AssetID == AssetId && x.Head.DocumentTypeID == documentTypeID)
                    .Sum(x => x.Amount));

                return AccumulatedDepreciation;
            }
        }

        public bool DeleteAsset(long AssetId)
        {
            using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
            {
                dbContext.Assets.Remove(dbContext.Assets.Where(x => x.AssetIID == AssetId).AsNoTracking().FirstOrDefault());
                dbContext.SaveChanges();
            }
            return true;
        }

        public AssetTransactionHead SaveAssetTransaction(AssetTransactionHead entity)
        {
            try
            {
                using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
                {
                    dbContext.AssetTransactionHeads.Add(entity);
                    if (entity.HeadIID == 0)
                    {
                        dbContext.Entry(entity).State = EntityState.Added;
                    }
                    else
                    {
                        foreach (var detail in entity.AssetTransactionDetails)
                        {
                            if (detail.DetailIID == 0)
                            {
                                dbContext.Entry(detail).State = EntityState.Added;
                            }
                            else
                            {
                                foreach (var serialMap in detail.AssetTransactionSerialMaps)
                                {
                                    if (serialMap.AssetTransactionSerialMapIID == 0)
                                    {
                                        dbContext.Entry(detail).State = EntityState.Added;
                                    }
                                    else
                                    {
                                        dbContext.Entry(detail).State = EntityState.Modified;
                                    }
                                }

                                dbContext.Entry(detail).State = EntityState.Modified;
                            }
                        }

                        dbContext.Entry(entity).State = EntityState.Modified;
                    }
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"Asset transaction saving failed, Trans no : {entity.TransactionNo}. Error message: {errorMessage}", ex);

                throw;
            }

            return entity;
        }

        public AssetTransactionHead GetAssetTransactionHeadById(long HeadID)
        {
            AssetTransactionHead assetTransactionHead = null;
            try
            {
                using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
                {
                    assetTransactionHead = dbContext.AssetTransactionHeads
                                                    .Include(x => x.AssetTransactionDetails)
                                                    .Include(i => i.AssetTransactionDetails).ThenInclude(i => i.Account)
                                                    .Include(i => i.AssetTransactionDetails).ThenInclude(i => i.Asset).ThenInclude(i => i.AssetGroup)
                                                    .Include(i => i.AssetTransactionDetails).ThenInclude(i => i.Asset).ThenInclude(i => i.AssetCategory)
                                                    .Include(x => x.DocumentStatus)
                                                    .Include(i => i.DocumentStatus).ThenInclude(i => i.DocumentStatus)
                                                    .Include(x => x.DocumentType)
                                                    .Include(x => x.ProcessingStatus)
                                                    .Where(x => x.HeadIID == HeadID)
                                                    .AsNoTracking()
                                                    .FirstOrDefault();

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
                using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
                {
                    assetTransactionHeadList = dbContext.AssetTransactionHeads
                        .Where(x => x.ProcessingStatusID == (int)transactionStatus && x.DocumentType.ReferenceType.ReferenceTypeID == (int)referenceType)
                                                    .Include(x => x.AssetTransactionDetails)
                                                    .Include(i => i.AssetTransactionDetails).ThenInclude(i => i.Asset)
                                                    .Include(i => i.AssetTransactionDetails).ThenInclude(i => i.Asset).ThenInclude(i => i.AssetCategory)
                                                    .Include(i => i.AssetTransactionDetails).ThenInclude(i => i.Account)
                                                    .Include(x => x.DocumentStatus)
                                                    .Include(i => i.DocumentStatus).ThenInclude(i => i.DocumentStatus)
                                                    .Include(x => x.DocumentType)
                                                    .Include(x => x.ProcessingStatus)
                                                    .Include(x => x.DocumentType)
                                                    .Include(i => i.DocumentType).ThenInclude(i => i.ReferenceType)
                                                    .AsNoTracking()
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
            dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext();

            try
            {
                AssetTransactionHead query = dbContext.AssetTransactionHeads.Where(x => x.HeadIID == assetTransactionHead.HeadIID).AsNoTracking().FirstOrDefault();

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

                    dbContext.Entry(query).State = EntityState.Modified;

                    dbContext.SaveChanges();
                }
                isSuccess = true;
            }
            catch (Exception ex)
            {
                isSuccess = false;

                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"Not ablet to update TransactionHead. HeadIID: " + assetTransactionHead.HeadIID.ToString() + " And the error message is: " + errorMessage, ex);

                throw;
            }
            return isSuccess;
        }
        public List<Asset> GetAssetCodesSearch(string SearchText)
        {
            using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
            {
                return dbContext.Assets
                    .Take(new Domain.Setting.SettingBL(null).GetSettingValue<int>("MaxFetchCount"))
                    .Where(x => x.AssetCode.Contains(SearchText))
                    .AsNoTracking()
                    .ToList();
            }
        }

        public List<Account> GetAccountCodesSearch(string SearchText)
        {
            using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
            {
                //return dbContext.Accounts
                //    .Take(new Domain.Setting.SettingBL(null).GetSettingValue<int>("MaxFetchCount"))
                //    .Where(x => x.AccountName.Contains(SearchText)).ToList();

                return dbContext.Accounts.Where(x => x.AccountName != null || x.AccountName.Contains(SearchText)).AsNoTracking().ToList();
            }
        }

        public Asset GetAssetByAssetCode(string AssetCode)
        {
            using (dbEduegateAccountsContext dbContext = new dbEduegateAccountsContext())
            {
                return dbContext.Assets
                    .Where(x => x.AssetCode == AssetCode)
                       .Include(x => x.AssetGlAcc)
                                    .Include(x => x.AccumulatedDepGLAcc)
                                    .Include(x => x.DepreciationExpGLAcc)
                                    .Include(x => x.AssetCategory)
                                    .Include(x => x.AssetTransactionDetails)
                                    .Include(i => i.AssetTransactionDetails).ThenInclude(i => i.Head)
                                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }

        public string GetNextTransactionNumberByDocument(int documentTypeID)
        {
            using (var dbContext = new dbEduegateAccountsContext())
            {
                var documentType = dbContext.DocumentTypes.Where(x => x.DocumentTypeID == documentTypeID).AsNoTracking().FirstOrDefault();
                if (documentType != null)
                {
                    documentType.LastTransactionNo = documentType.LastTransactionNo.HasValue ? documentType.LastTransactionNo + 1 : 1;

                    dbContext.Entry(documentType).State = EntityState.Modified;
                    dbContext.SaveChanges();

                    return documentType.TransactionNoPrefix + " - " + documentType.LastTransactionNo.ToString();
                }
                else
                {
                    return null;
                }
            }
        }

    }
}

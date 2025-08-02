using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using System.Data.Entity;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Domain.Repository
{
    public class PriceSettingsRepository
    {
        public List<ProductPriceListProductMap> GetProductPriceSettings(long productIID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.ProductPriceListProductMaps.Where(x => x.ProductID == productIID)
                    .Include(x => x.ProductPriceListProductQuantityMaps).ToList();
            }
        }

        public List<ProductPriceListProductMap> SaveProductPrice(List<ProductPriceListProductMap> entities, long productIID, List<ProductPriceListProductMap> deleteEntities)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    if (deleteEntities.IsNotNull() && deleteEntities.Count > 0)
                    {
                        foreach (var delete in deleteEntities)
                        {
                            var pplpm = dbContext.ProductPriceListProductMaps.Where(x => x.ProductPriceListProductMapIID == delete.ProductPriceListProductMapIID).FirstOrDefault();

                            if (pplpm.ProductPriceListProductQuantityMaps.IsNotNull() && pplpm.ProductPriceListProductQuantityMaps.Count > 0)
                            {
                                dbContext.ProductPriceListProductQuantityMaps.RemoveRange(pplpm.ProductPriceListProductQuantityMaps);
                            }

                            dbContext.ProductPriceListProductMaps.Remove(pplpm);
                        }

                        dbContext.SaveChanges();
                    }
                    if (entities.IsNotNull() && entities.Count > 0)
                    {
                        foreach (var entity in entities)
                        {
                            if (entity.ProductPriceListProductMapIID <= 0)
                            {
                                dbContext.ProductPriceListProductMaps.Add(entity);
                                dbContext.Entry(entity).State = EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(entity).State = EntityState.Modified;
                            }

                            if (entity.ProductPriceListProductQuantityMaps.IsNotNull() && entity.ProductPriceListProductQuantityMaps.Count > 0)
                            {
                                var currentData = entity.ProductPriceListProductQuantityMaps
                                    .Where(x => x.ProductPriceListProductQuantityMapIID > 0 && x.ProductPriceListProductMapID == entity.ProductPriceListProductMapIID).ToList();

                                foreach (var quantityMapEntity in entity.ProductPriceListProductQuantityMaps)
                                {
                                    if (quantityMapEntity.ProductPriceListProductQuantityMapIID <= 0)
                                    {
                                        dbContext.ProductPriceListProductQuantityMaps.Add(quantityMapEntity);
                                        dbContext.Entry(quantityMapEntity).State = EntityState.Added;
                                    }
                                    else
                                    {
                                        dbContext.Entry(quantityMapEntity).State = EntityState.Modified;
                                    }
                                }

                                //Getting db product quantity data to compare with the current data and product quantity is empty uusing this data we can delete product quantity
                                var dbData = dbContext.ProductPriceListProductQuantityMaps.Where(x => x.ProductPriceListProductMapID == entity.ProductPriceListProductMapIID).ToList();
                                var entitiesToDelete = dbData.Where(x => x.ProductPriceListProductMapID == entity.ProductPriceListProductMapIID).Except(currentData).ToList();

                                if (entitiesToDelete.Count > 0)
                                    dbContext.ProductPriceListProductQuantityMaps.RemoveRange(entitiesToDelete);
                            }
                            else
                            {
                                dbContext.ProductPriceListProductQuantityMaps
                                    .RemoveRange(dbContext.ProductPriceListProductQuantityMaps.Where(x => x.ProductPriceListProductMapID == entity.ProductPriceListProductMapIID).ToList());
                            }
                        }

                        dbContext.SaveChanges();
                    }
                }

                return GetProductPriceSettings(productIID);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<PriceSettingsRepository>.Fatal(ex.Message.ToString(), ex);
                throw ex;
            }
        }

        public ProductPriceListProductMap GetProductPrice(long id)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.ProductPriceListProductMaps.Where(x => x.ProductPriceListProductMapIID == id).FirstOrDefault();
            }
        }



        public ProductPriceListBrandMap SaveBrandPrice(ProductPriceListBrandMap entity)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                db.ProductPriceListBrandMaps.Add(entity);

                if (entity.ProductPriceListBrandMapIID > 0)
                    db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                else
                    db.Entry(entity).State = System.Data.Entity.EntityState.Added;

                db.SaveChanges();
                return GetBrandPrice(entity.ProductPriceListBrandMapIID);
            }
        }
        public ProductPriceListBrandMap GetBrandPrice(long id)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.ProductPriceListBrandMaps.Where(x => x.ProductPriceListBrandMapIID == id).FirstOrDefault();
            }
        }
        public ProductPriceListBranchMap GetBranchMaps(long id)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.ProductPriceListBranchMaps
                    .Include(x => x.ProductPriceList.ProductPriceListBranchMaps.Select(y => y.Branch))
                    .Where(x => x.ProductPriceListBranchMapIID == id).FirstOrDefault();
            }
        }

        public IEnumerable<ProductPriceListBranchMap> SaveBranchMaps(List<ProductPriceListBranchMap> entities)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if (entities.Count == 0)
                {
                    return null;
                }

                // Get the record from db ProductPriceListBranchMap
                long productPriceListID = (long)entities[0].ProductPriceListID;
                var dbBranchMaps =
                    db.ProductPriceListBranchMaps.Where(x => x.ProductPriceListID == productPriceListID).ToList();

                foreach (var branchMap in dbBranchMaps)
                {
                    // Check is this record existing in current request
                    bool check = entities.Any(x => x.ProductPriceListBranchMapIID == branchMap.ProductPriceListBranchMapIID);
                    if (check)
                    {
                        // Update
                        db.Entry(branchMap).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        // Delete
                        //db.ProductPriceListBranchMaps.Remove(branchMap);
                        db.Entry(branchMap).State = System.Data.Entity.EntityState.Deleted;
                    }
                }

                // check in current request is any record with IID zero(0)
                var paymentMapsInsert =
                    entities.Where(x => x.ProductPriceListBranchMapIID == default(long)).ToList();
                // Add it
                db.ProductPriceListBranchMaps.AddRange(paymentMapsInsert);

                db.SaveChanges();

                return GetProductPriceListBranchMapsByProductPriceListID((long)entities[0].ProductPriceListID);
            }
        }

        private IEnumerable<ProductPriceListBranchMap> GetProductPriceListBranchMapsByProductPriceListID(long productPriceListID)
        {
            using (var db = new dbEduegateERPContext())
            {
                if (productPriceListID == 0) return null;
                return db.ProductPriceListBranchMaps
                    .Include(x => x.ProductPriceList.ProductPriceListBranchMaps.Select(y => y.Branch))
                    .Where(x => x.ProductPriceListID == productPriceListID)
                    .ToList();
            }
        }

        public ProductPriceListCategoryMap SaveCategoryPrice(ProductPriceListCategoryMap entity)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                db.ProductPriceListCategoryMaps.Add(entity);

                if (entity.ProductPriceListCategoryMapIID > 0)
                    db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                else
                    db.Entry(entity).State = System.Data.Entity.EntityState.Added;

                db.SaveChanges();
                return GetCategoryPrice(entity.ProductPriceListCategoryMapIID);
            }
        }

        public ProductPriceListCategoryMap GetCategoryPrice(long id)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.ProductPriceListCategoryMaps
                    .Where(x => x.ProductPriceListCategoryMapIID == id).FirstOrDefault();
            }
        }

        public List<ProductPriceListCustomerGroupMap> SaveCustomerGroupPrice(List<ProductPriceListCustomerGroupMap> customerGroupPriceSettings, long IID, List<ProductPriceListCustomerGroupMap> deleteEntities)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if (deleteEntities.IsNotNull() && deleteEntities.Count > 0)
                {
                    foreach (var del in deleteEntities)
                    {
                        var pplcgm = db.ProductPriceListCustomerGroupMaps.Where(x => x.ProductPriceListCustomerGroupMapIID == del.ProductPriceListCustomerGroupMapIID).FirstOrDefault();

                        db.ProductPriceListCustomerGroupMaps.Remove(pplcgm);
                    }

                    db.SaveChanges();
                }
                if (customerGroupPriceSettings.IsNotNull() && customerGroupPriceSettings.Count > 0)
                {
                    foreach (var cgpMap in customerGroupPriceSettings)
                    {
                        if (cgpMap.ProductPriceListCustomerGroupMapIID <= 0)
                        {
                            db.ProductPriceListCustomerGroupMaps.Add(cgpMap);
                            db.Entry(cgpMap).State = System.Data.Entity.EntityState.Added;
                        }
                        else
                        {
                            db.ProductPriceListCustomerGroupMaps.Add(cgpMap);
                            db.Entry(cgpMap).State = System.Data.Entity.EntityState.Modified;
                        }
                    }

                    long? skuMapID = customerGroupPriceSettings[0].ProductSKUMapID;
                    var dbData = db.ProductPriceListCustomerGroupMaps.Where(x => x.ProductSKUMapID == skuMapID).ToList();

                    if (dbData.IsNotNull() && dbData.Count > 0)
                    {
                        // Check is this record existing in current request ProductPriceListCustomerGroupMaps
                        foreach (var item in dbData)
                        {
                            // Check is this record existing in current request ProductPriceListCustomerGroupMaps
                            bool check = customerGroupPriceSettings
                                .Any(x => x.ProductPriceListCustomerGroupMapIID == item.ProductPriceListCustomerGroupMapIID);

                            if (!check)
                                db.ProductPriceListCustomerGroupMaps.Remove(item);  // Delete
                        }
                    }
                }

                db.SaveChanges();
                return customerGroupPriceSettings;
            }
        }

        public ProductPriceListCustomerGroupMap GetCustomerGroupPrice(long id)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.ProductPriceListCustomerGroupMaps.Where(x => x.ProductPriceListCustomerGroupMapIID == id).FirstOrDefault();
            }
        }

        public List<ProductPriceListSKUMap> GetProductPriceListForSKU(long skuID, int companyID = 0)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (skuID > 0)
                    return dbContext.ProductPriceListSKUMaps.Where(x => x.ProductSKUID == skuID && x.CompanyID == companyID)
                        .Include(x => x.ProductPriceListSKUQuantityMaps).ToList();
                else
                    return new List<ProductPriceListSKUMap>();
            }
        }
        public List<ProductBundle> GetProductbundles(long skuID, int companyID = 0)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (skuID > 0)
                    return dbContext.ProductBundles.Where(x => x.ToProductSKUMapID == skuID /*&& x.FromProductID == companyID*/)
                        /*.Include(x => x.ProductSKUMap)*/.ToList();
                else
                    return new List<ProductBundle>();
            }
        }

        public List<ProductCategoryMap> GetProductcategorie(long skuID, int companyID = 0)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (skuID > 0)
                    return dbContext.ProductCategoryMaps.Where(x => x.ProductCategoryMapIID == skuID /*&& x.FromProductID == companyID*/)
                        /*.Include(x => x.ProductSKUMap)*/.ToList();
                else
                    return new List<ProductCategoryMap>();
            }
        }

        /// <summary>
        /// not useful just keep it for refrence
        /// </summary>
        /// <param name="skuID"></param>
        /// <returns></returns>
        public List<ProductPriceListSKUMap> ProductPriceListSKUMapsBySkuId(long skuID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var list = (from sku in db.ProductSKUMaps
                            join
                            pplsm in db.ProductPriceListSKUMaps on sku.ProductSKUMapIID equals pplsm.ProductSKUID
                            join
                            ppl in db.ProductPriceLists on pplsm.ProductPriceListID equals ppl.ProductPriceListIID
                            join
                            pplbm in db.ProductPriceListBranchMaps on ppl.ProductPriceListIID equals pplbm.ProductPriceListID
                            join
                            pcm in db.ProductCategoryMaps on sku.ProductID equals pcm.ProductID
                            join
                            c in db.Categories on pcm.CategoryID equals c.CategoryIID
                            join
                            s in db.Suppliers on pplbm.BranchID equals s.BranchID

                            where sku.ProductSKUMapIID == skuID
                            select new ProductPriceListSKUMap
                            {
                                ProductPriceListItemMapIID = pplsm.ProductPriceListItemMapIID,
                                CompanyID = pplsm.CompanyID,
                                ProductPriceListID = pplsm.ProductPriceListID,
                                ProductSKUID = pplsm.ProductSKUID,
                                UnitGroundID = pplsm.UnitGroundID,
                                CustomerGroupID = pplsm.CustomerGroupID,
                                SellingQuantityLimit = pplsm.SellingQuantityLimit,
                                Amount = pplsm.Amount,
                                SortOrder = pplsm.SortOrder,
                                CreatedBy = pplsm.CreatedBy,
                                UpdatedBy = pplsm.UpdatedBy,
                                CreatedDate = pplsm.CreatedDate,
                                UpdatedDate = pplsm.UpdatedDate,
                                //TimeStamps = pplsm.TimeStamps,
                                PricePercentage = pplsm.PricePercentage,

                                Price = c.Profit != null ? pplsm.Cost + pplsm.Cost * (c.Profit / 100)
                                : s.Profit != null ? pplsm.Cost + pplsm.Cost * (s.Profit / 100) : pplsm.Price,

                                Discount = pplsm.Discount,
                                DiscountPercentage = pplsm.DiscountPercentage,
                                Cost = pplsm.Cost
                            }).ToList();
                return list;
            }
        }

        public List<ProductPriceListCategoryMap> GetCategoryPriceSettings(long categoryID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (categoryID > 0)
                    return dbContext.ProductPriceListCategoryMaps.Where(x => x.CategoryID == categoryID).ToList();
                else
                    return new List<ProductPriceListCategoryMap>();
            }
        }

        public List<ProductPriceListSKUQuantityMap> GetProductPriceListSkuQuantityMaps(long productPriceSkuMapID)
        {
            using (dbEduegateERPContext dbContext
                = new dbEduegateERPContext())
            {
                return dbContext.ProductPriceListSKUQuantityMaps.Where(x => x.ProductPriceListSKUMapID == productPriceSkuMapID).ToList();
            }
        }

        public List<ProductPriceListSKUMap> SaveSKUPrice(List<ProductPriceListSKUMap> entities, long skuIID, List<ProductPriceListSKUMap> deleteEntites)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    if (deleteEntites.IsNotNull() && deleteEntites.Count > 0)
                    {
                        foreach (var delen in deleteEntites)
                        {
                            var pplsm = dbContext.ProductPriceListSKUMaps.Where(x => x.ProductPriceListItemMapIID == delen.ProductPriceListItemMapIID).FirstOrDefault();

                            if (pplsm.ProductPriceListSKUQuantityMaps.IsNotNull() && pplsm.ProductPriceListSKUQuantityMaps.Count > 0)
                            {
                                dbContext.ProductPriceListSKUQuantityMaps.RemoveRange(pplsm.ProductPriceListSKUQuantityMaps);
                            }

                            dbContext.ProductPriceListSKUMaps.Remove(pplsm);
                        }

                        dbContext.SaveChanges();
                    }
                    if (entities.IsNotNull() && entities.Count > 0)
                    {
                        foreach (var skuMapEntity in entities)
                        {
                            if (skuMapEntity.ProductPriceListItemMapIID <= 0)
                            {
                                dbContext.ProductPriceListSKUMaps.Add(skuMapEntity);
                                dbContext.Entry(skuMapEntity).State = EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(skuMapEntity).State = EntityState.Modified;
                            }

                            if (skuMapEntity.ProductPriceListSKUQuantityMaps.IsNotNull() && skuMapEntity.ProductPriceListSKUQuantityMaps.Count > 0)
                            {
                                var currentData = skuMapEntity.ProductPriceListSKUQuantityMaps
                                    .Where(x => x.ProductPriceListSKUQuantityMapIID > 0 && x.ProductPriceListSKUMapID == skuMapEntity.ProductPriceListItemMapIID).ToList();

                                foreach (var skuQuantityMapEntity in skuMapEntity.ProductPriceListSKUQuantityMaps)
                                {
                                    if (skuQuantityMapEntity.ProductPriceListSKUQuantityMapIID <= 0)
                                    {
                                        dbContext.ProductPriceListSKUQuantityMaps.Add(skuQuantityMapEntity);
                                        dbContext.Entry(skuQuantityMapEntity).State = EntityState.Added;
                                    }
                                    else
                                    {
                                        dbContext.Entry(skuQuantityMapEntity).State = EntityState.Modified;
                                    }
                                }

                                //Getting db sku quantity data to compare with the current data and sku quantity is empty uusing this data we can delete sku quantity
                                var dbData = dbContext.ProductPriceListSKUQuantityMaps.Where(x => x.ProductPriceListSKUMapID == skuMapEntity.ProductPriceListItemMapIID).ToList();

                                var entitiesToDelete = dbData.Where(x => x.ProductPriceListSKUMapID == skuMapEntity.ProductPriceListItemMapIID).Except(currentData).ToList();

                                if (entitiesToDelete.Count > 0)
                                    dbContext.ProductPriceListSKUQuantityMaps.RemoveRange(entitiesToDelete);
                            }
                            else
                            {
                                dbContext.ProductPriceListSKUQuantityMaps
                                    .RemoveRange(dbContext.ProductPriceListSKUQuantityMaps.Where(x => x.ProductPriceListSKUMapID == skuMapEntity.ProductPriceListItemMapIID).ToList());
                            }
                        }

                        dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<PriceSettingsRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return GetProductPriceListForSKU(skuIID);
        }

        public string GetPriceDescription(long priceListID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var productPrice = dbContext.ProductPriceLists.Where(x => x.ProductPriceListIID == priceListID).FirstOrDefault();

                if (productPrice.IsNotNull())
                    return productPrice.PriceDescription;
                else
                    return string.Empty;
            }
        }

        public List<ProductPriceListCustomerGroupMap> GetCustomerGroupPriceSettingsForSKU(long skuID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var pplcgMaps = dbContext.ProductPriceListCustomerGroupMaps.Where(x => x.ProductSKUMapID == skuID)
                    .Include(x => x.Brand).Include(x => x.Category).Include(x => x.CustomerGroup).Include(x => x.ProductPriceList)
                    .Include(x => x.Product).Include(x => x.ProductSKUMap).ToList();

                return pplcgMaps;
            }
        }

        public string GetCustomerGroupName(long customerGroupID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var customerGroup = dbContext.CustomerGroups.Where(x => x.CustomerGroupIID == customerGroupID).FirstOrDefault();

                if (customerGroup.IsNotNull())
                    return customerGroup.GroupName;
                else
                    return string.Empty;
            }
        }

        public List<ProductPriceList> GetProductPriceListBySku(long IID)
        {
            List<ProductPriceList> ppls = new List<ProductPriceList>();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (IID > 0)
                {
                    ppls = (from pplsm in dbContext.ProductPriceListSKUMaps
                            join ppl in dbContext.ProductPriceLists on pplsm.ProductPriceListID equals ppl.ProductPriceListIID
                            where pplsm.ProductSKUID == IID
                            select ppl).ToList();
                }

                return ppls;
            }
        }

        public List<ProductPriceList> GetProductPriceLists(int? companyID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (!companyID.HasValue)
                {
                    return dbContext.ProductPriceLists.OrderBy(a => a.PriceDescription).ToList();
                }
                else
                {
                    return dbContext.ProductPriceLists.Where(x => x.CompanyID == companyID).OrderBy(a => a.PriceDescription).ToList();
                }
            }
        }

        public List<Branch> GetProductBranchLists(int CompanyID = 0)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Branches.Where(a=> a.CompanyID == CompanyID && a.StatusID == 1).ToList();
            }
        }

        public List<ProductPriceListCategoryMap> SaveCategoryPriceSettings(List<ProductPriceListCategoryMap> entities, long categoryIID, List<ProductPriceListCategoryMap> deleteEntities)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if (deleteEntities.IsNotNull() && deleteEntities.Count > 0)
                {
                    foreach (var del in deleteEntities)
                    {
                        var pplcm = db.ProductPriceListCategoryMaps.Where(x => x.ProductPriceListCategoryMapIID == del.ProductPriceListCategoryMapIID).FirstOrDefault();

                        db.ProductPriceListCategoryMaps.Remove(pplcm);
                    }

                    db.SaveChanges();
                }
                if (entities.IsNotNull() && entities.Count > 0)
                {
                    foreach (var categoryMapEntity in entities)
                    {
                        if (categoryMapEntity.ProductPriceListCategoryMapIID <= 0)
                        {
                            db.ProductPriceListCategoryMaps.Add(categoryMapEntity);
                            db.Entry(categoryMapEntity).State = EntityState.Added;
                        }
                        else
                        {
                            db.Entry(categoryMapEntity).State = EntityState.Modified;
                        }
                    }

                    db.SaveChanges();
                }
            }
            return GetCategoryPriceSettings(categoryIID);
        }


        public List<ProductPriceListBrandMap> GetBrandPriceSettings(long brandID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (brandID > 0)
                    return dbContext.ProductPriceListBrandMaps.Where(x => x.BrandID == brandID).ToList();
                else
                    return new List<ProductPriceListBrandMap>();
            }
        }

        public List<ProductPriceListBrandMap> SaveBrandPriceSettings(List<ProductPriceListBrandMap> entities, long brandIID, List<ProductPriceListBrandMap> deleteEntities)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if (deleteEntities.IsNotNull() && deleteEntities.Count > 0)
                {
                    foreach (var del in deleteEntities)
                    {
                        var pplbm = db.ProductPriceListBrandMaps.Where(x => x.ProductPriceListBrandMapIID == del.ProductPriceListBrandMapIID).FirstOrDefault();

                        db.ProductPriceListBrandMaps.Remove(pplbm);
                    }
                    db.SaveChanges();
                }

                if (entities.IsNotNull() && entities.Count > 0)
                {
                    foreach (var brandMapEntity in entities)
                    {
                        if (brandMapEntity.ProductPriceListBrandMapIID <= 0)
                        {
                            db.ProductPriceListBrandMaps.Add(brandMapEntity);
                            db.Entry(brandMapEntity).State = EntityState.Added;
                        }
                        else
                        {
                            db.Entry(brandMapEntity).State = EntityState.Modified;
                        }
                    }

                    db.SaveChanges();
                }
            }
            return GetBrandPriceSettings(brandIID);
        }

        public List<ProductBundle> Saveproductbundlesettings(List<ProductBundle> entities, long BundleIID, List<ProductBundle> deleteEntities)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if (deleteEntities.IsNotNull() && deleteEntities.Count > 0)
                {
                    foreach (var del in deleteEntities)
                    {
                        var pplbm = db.ProductBundles.Where(x => x.BundleIID == del.BundleIID).FirstOrDefault();

                        db.ProductBundles.Remove(pplbm);
                    }
                    db.SaveChanges();
                }

                if (entities.IsNotNull() && entities.Count > 0)
                {
                    foreach (var brandMapEntity in entities)
                    {
                        if (brandMapEntity.BundleIID <= 0)
                        {
                            db.ProductBundles.Add(brandMapEntity);
                            db.Entry(brandMapEntity).State = EntityState.Added;
                        }
                        else
                        {
                            db.Entry(brandMapEntity).State = EntityState.Modified;
                        }
                    }

                    db.SaveChanges();
                }
            }
            return GetProductbundleSettings(BundleIID);
        }

        public List<ProductCategoryMap> Savecategoeries(List<ProductCategoryMap> entities, long productID, List<ProductCategoryMap> existEntities)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                //if (existEntities.IsNotNull() && existEntities.Count > 0)
                //{
                //    foreach (var del in existEntities)
                //    {
                //        var pplbm = db.ProductCategoryMaps.Where(x => x.ProductID == del.ProductID).FirstOrDefault();

                //        db.ProductCategoryMaps.Remove(pplbm);
                //    }
                //    db.SaveChanges();
                //}

                if (existEntities.IsNotNull() && existEntities.Count > 0)
                {
                    //if (entities.Any(x => existEntities.Any(y => y.CategoryID == x.CategoryID && y.ProductID == )))
                    foreach (var entity in entities)
                    {
                        var filterExistData = existEntities.Find(x => x.ProductID == entity.ProductID && x.CategoryID == entity.CategoryID);

                        if (filterExistData != null)
                        {
                            entity.ProductCategoryMapIID = filterExistData.ProductCategoryMapIID;
                        }

                    }
                }

                if (entities.IsNotNull() && entities.Count > 0)
                {
                    foreach (var brandMapEntity in entities)
                    {
                        if (brandMapEntity.ProductCategoryMapIID <= 0)
                        {
                            db.ProductCategoryMaps.Add(brandMapEntity);
                            db.Entry(brandMapEntity).State = EntityState.Added;
                        }
                        else
                        {
                            db.Entry(brandMapEntity).State = EntityState.Modified;
                        }
                    }

                    db.SaveChanges();
                }
            }
            return Getcategories(productID);
        }

        public List<ProductCategoryMap> Getcategories(long productID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (productID > 0)
                    return dbContext.ProductCategoryMaps.Where(x => x.ProductID == productID).ToList();
                else
                    return new List<ProductCategoryMap>();
            }
        }

        public List<ProductAllergyMap> SaveAllergies(List<ProductAllergyMap> entities, long productID, List<ProductAllergyMap> existEntities)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {

                if (existEntities.IsNotNull() && existEntities.Count > 0)
                {
                    //if (entities.Any(x => existEntities.Any(y => y.CategoryID == x.CategoryID && y.ProductID == )))
                    foreach (var entity in entities)
                    {
                        var filterExistData = existEntities.Find(x => x.ProductID == entity.ProductID && x.AllergyID == entity.AllergyID);

                        if (filterExistData != null)
                        {
                            entity.ProductAllergyMapIID = filterExistData.ProductAllergyMapIID;
                        }

                    }
                }

                if (entities.IsNotNull() && entities.Count > 0)
                {
                    foreach (var brandMapEntity in entities)
                    {
                        if (brandMapEntity.ProductAllergyMapIID <= 0)
                        {
                            db.ProductAllergyMaps.Add(brandMapEntity);
                            db.Entry(brandMapEntity).State = EntityState.Added;
                        }
                        else
                        {
                            db.Entry(brandMapEntity).State = EntityState.Modified;
                        }
                    }

                    db.SaveChanges();
                }
            }
            return GetAllergies(productID);
        }

        public List<ProductAllergyMap> GetAllergies(long productID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (productID > 0)
                    return dbContext.ProductAllergyMaps.Where(x => x.ProductID == productID).ToList();
                else
                    return new List<ProductAllergyMap>();
            }
        }

        public List<ProductSKURackMap> Savecrackentities(List<ProductSKURackMap> entities, long productID, List<ProductSKURackMap> deleteEntities)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if (deleteEntities.IsNotNull() && deleteEntities.Count > 0)
                {
                    foreach (var del in deleteEntities)
                    {
                        var pplbm = db.ProductSKURackMaps.Where(x => x.ProductID == del.ProductID).FirstOrDefault();

                        db.ProductSKURackMaps.Remove(pplbm);
                    }
                    db.SaveChanges();
                }

                if (entities.IsNotNull() && entities.Count > 0)
                {
                    foreach (var brandMapEntity in entities)
                    {
                        if (brandMapEntity.ProductSKURackMapIID <= 0)
                        {
                            db.ProductSKURackMaps.Add(brandMapEntity);
                            db.Entry(brandMapEntity).State = EntityState.Added;
                        }
                        else
                        {
                            db.Entry(brandMapEntity).State = EntityState.Modified;
                        }
                    }

                    db.SaveChanges();
                }
            }
            return Savecrackentities(productID);
        }
        public List<ProductSKURackMap> Savecrackentities(long productID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (productID > 0)
                    return dbContext.ProductSKURackMaps.Where(x => x.ProductID == productID).ToList();
                else
                    return new List<ProductSKURackMap>();
            }
        }
        public List<ProductBundle> GetProductbundleSettings(long BundleIID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (BundleIID > 0)
                    return dbContext.ProductBundles.Where(x => x.BundleIID == BundleIID).ToList();
                else
                    return new List<ProductBundle>();
            }
        }
        public List<ProductPriceListCustomerGroupMap> GetCustomerGroupPriceSettingsForProduct(long productIID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var pplcgMaps = dbContext.ProductPriceListCustomerGroupMaps.Where(x => x.ProductID == productIID)
                    .Include(x => x.Brand).Include(x => x.Category).Include(x => x.CustomerGroup).Include(x => x.ProductPriceList)
                    .Include(x => x.Product).Include(x => x.ProductSKUMap).ToList();

                return pplcgMaps;
            }
        }

        public List<ProductPriceList> GetProductPriceListByProduct(long IID)
        {
            List<ProductPriceList> ppls = new List<ProductPriceList>();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (IID > 0)
                {
                    ppls = (from pplpm in dbContext.ProductPriceListProductMaps
                            join ppl in dbContext.ProductPriceLists on pplpm.ProductPriceListID equals ppl.ProductPriceListIID
                            where pplpm.ProductID == IID
                            select ppl).ToList();
                }

                return ppls;
            }
        }

        public List<ProductPriceListCustomerGroupMap> SaveProductCustomerGroupPrice(List<ProductPriceListCustomerGroupMap> customerGroupPriceSettings, long productID, List<ProductPriceListCustomerGroupMap> deleteEntities)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if (deleteEntities.IsNotNull() && deleteEntities.Count > 0)
                {
                    foreach (var dele in deleteEntities)
                    {
                        var pplcgm = db.ProductPriceListCustomerGroupMaps.Where(x => x.ProductPriceListCustomerGroupMapIID == dele.ProductPriceListCustomerGroupMapIID).FirstOrDefault();

                        db.ProductPriceListCustomerGroupMaps.Remove(pplcgm);
                    }
                    db.SaveChanges();
                }
                if (customerGroupPriceSettings.IsNotNull() && customerGroupPriceSettings.Count > 0)
                {
                    foreach (var cgpMap in customerGroupPriceSettings)
                    {
                        if (cgpMap.ProductPriceListCustomerGroupMapIID <= 0)
                        {
                            db.ProductPriceListCustomerGroupMaps.Add(cgpMap);
                            db.Entry(cgpMap).State = System.Data.Entity.EntityState.Added;
                        }
                        else
                        {
                            db.ProductPriceListCustomerGroupMaps.Add(cgpMap);
                            db.Entry(cgpMap).State = System.Data.Entity.EntityState.Modified;
                        }
                    }

                    var dbData = db.ProductPriceListCustomerGroupMaps.Where(x => x.ProductID == productID).ToList();

                    if (dbData.IsNotNull() && dbData.Count > 0)
                    {
                        // Check is this record existing in current request ProductPriceListCustomerGroupMaps
                        foreach (var item in dbData)
                        {
                            // Check is this record existing in current request ProductPriceListCustomerGroupMaps
                            bool check = customerGroupPriceSettings
                                .Any(x => x.ProductPriceListCustomerGroupMapIID == item.ProductPriceListCustomerGroupMapIID);

                            if (!check)
                                db.ProductPriceListCustomerGroupMaps.Remove(item);  // Delete
                        }
                    }
                }

                db.SaveChanges();
                return customerGroupPriceSettings;
            }
        }



        public bool UpdateSupplierSKUInventory(SKUDTO sku, long productPriceListID, long branchID, int companyID)
        {
            var exit = false;
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                // Update pricelistskumap
                var pricelistMap = db.ProductPriceListSKUMaps.Where(x => x.ProductPriceListID == productPriceListID && x.ProductSKUID == sku.ProductSKUMapID).FirstOrDefault();

                pricelistMap.Cost = sku.CostPrice;
                pricelistMap.Price = sku.SellingPrice;

                // Update quantity in inventory
                var skuInventoryDetails = db.ProductInventories.Where(x => x.BranchID == branchID && x.ProductSKUMapID == sku.ProductSKUMapID).FirstOrDefault();

                if (skuInventoryDetails.IsNotNull())
                {
                    skuInventoryDetails.Quantity = sku.Quantity;
                }
                else
                {
                    var productInventory = new ProductInventory();
                    productInventory.Batch = 1;
                    productInventory.ProductSKUMapID = sku.ProductSKUMapID;
                    productInventory.BranchID = branchID;
                    productInventory.Quantity = sku.Quantity;
                    productInventory.CostPrice = sku.CostPrice;
                    productInventory.CompanyID = companyID;

                    db.ProductInventories.Add(productInventory);
                    //db.Entry(productInventory).State = EntityState.Added;
                   
                }

                

                db.SaveChanges();
                exit = true; 
            }
            return exit;

        }

        public ProductPriceListSKUMap GetProductPriceListSKUMap(long productPriceListID, long skuID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
               
                return db.ProductPriceListSKUMaps.Where(x => x.ProductPriceListID == productPriceListID && x.ProductSKUID == skuID).FirstOrDefault();
            }
        }
    }
}

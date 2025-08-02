using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Repository
{
    public class CategoryRepository
    {
        public List<CategoryTag> GetCategoryTags()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.CategoryTags.AsNoTracking().ToList<CategoryTag>();
            }
        }

        public List<Category> GetCategoryByParentID(long? parentID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var categories = dbContext.Categories.Where(a => (long?)a.ParentCategoryID == parentID && a.IsActive == true).AsNoTracking().ToList<Category>();
                //foreach (var category in categories)
                //{
                //    dbContext.Entry(category).Collection(a => a.CategoryImageMaps).Load();
                //}
                return categories;
            }
        }

        public List<Category> GetParentCategoryID(long? parentID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var categories = dbContext.Categories.Where(a => (long?)a.ParentCategoryID == null && a.IsActive == true).AsNoTracking().ToList<Category>();
                //foreach (var category in categories)
                //{
                //    dbContext.Entry(category).Collection(a => a.CategoryImageMaps).Load();
                //}
                return categories;
            }
        }

        public List<Category> GetCategoryByParentIDWithImage(long? parentID, string cultureCode)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var categories = dbContext.Categories.Where(a => (long?)a.ParentCategoryID == parentID && a.IsActive == true).OrderBy(a => a.CategoryName)
                    .Include(i => i.CategoryImageMaps)
                    .AsNoTracking()
                    .ToList<Category>();
                //foreach (var category in categories)
                //{
                //    dbContext.Entry(category).Collection(a => a.CategoryImageMaps).Load();
                //}
                if (cultureCode.IsNotNullOrEmpty() && cultureCode != "en")
                {
                    List<CategoryCultureData> categoryCulture = (from catCulture in dbContext.CategoryCultureDatas
                                                                 join culture in dbContext.Cultures on catCulture.CultureID equals culture.CultureID
                                                                 where culture.CultureCode == cultureCode
                                                                 select catCulture).AsNoTracking().ToList();
                    if (categoryCulture.Any())
                    {
                        foreach (Category cat in categories)
                        {
                            var result = categoryCulture.Where(x => x.CategoryID == cat.CategoryIID).FirstOrDefault();
                            if (result.IsNotNull())
                                cat.CategoryName = result.CategoryName;
                        }
                    }
                }
                return categories;
            }
        }

        public Category GetCategoryByID(long categoryID, string cultureCode)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var catagory = dbContext.Categories.Where(a => a.CategoryIID == categoryID)
                    .Include(i => i.CategoryImageMaps)
                    .AsNoTracking().FirstOrDefault();
                //if (catagory != null)
                //{
                //    dbContext.Entry(catagory).Collection(a => a.CategoryImageMaps).Load();
                //}

                if (cultureCode.IsNotNullOrEmpty() && cultureCode != "en")
                {
                    var result = from cc in dbContext.CategoryCultureDatas
                                 join culture in dbContext.Cultures on cc.CultureID equals culture.CultureID
                                 where cc.CategoryID == catagory.CategoryIID && culture.CultureCode == cultureCode
                                 select cc;
                    if (result.Any())
                        catagory.CategoryName = result.FirstOrDefault().CategoryName;
                }
                return catagory;
            }
        }

        public List<Category> GetSubCategories(long categoryID, string cultureCode)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var categories = dbContext.Categories.Where(a => a.ParentCategoryID == categoryID && a.IsActive == true)
                    .Include(i => i.CategoryImageMaps)
                    .AsNoTracking()
                    .ToList();

                //foreach (var category in categories)
                //{
                //    dbContext.Entry(category).Collection(a => a.CategoryImageMaps).Load();
                //}

                if (cultureCode.IsNotNullOrEmpty() && cultureCode != "en")
                {
                    List<CategoryCultureData> categoryCulture = (from catCulture in dbContext.CategoryCultureDatas
                                                                 join cat in dbContext.Categories on catCulture.CategoryID equals cat.CategoryIID
                                                                 join culture in dbContext.Cultures on catCulture.CultureID equals culture.CultureID
                                                                 where culture.CultureCode == cultureCode && cat.ParentCategoryID == categoryID
                                                                 select catCulture).AsNoTracking().ToList();
                    if (categoryCulture.Any())
                    {
                        foreach (Category cat in categories)
                        {
                            var result = categoryCulture.Where(x => x.CategoryID == cat.CategoryIID).FirstOrDefault();
                            if (result.IsNotNull())
                                cat.CategoryName = result.CategoryName;
                        }
                    }
                }

                return categories;
            }
        }

        public List<Category> GetSubCategories(long categoryID, string tagName, string cultureCode)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var categories = (from category in dbContext.Categories
                                  join tagMap in dbContext.CategoryTagMaps on category.CategoryIID equals tagMap.CategoryID
                                  join tag in dbContext.CategoryTags on tagMap.CategoryTagID equals tag.CategoryTagIID
                                  where tag.TagName.ToUpper().Equals(tagName.ToUpper()) && category.ParentCategoryID == categoryID
                                  select category)
                                  .Include(i => i.CategoryImageMaps)
                                  .ToList();
                //foreach (var category in categories)
                //{
                //    dbContext.Entry(category).Collection(a => a.CategoryImageMaps).Load();
                //}

                if (cultureCode.IsNotNullOrEmpty() && cultureCode != "en")
                {
                    List<CategoryCultureData> categoryCulture = (from catCulture in dbContext.CategoryCultureDatas
                                                                 join culture in dbContext.Cultures on catCulture.CultureID equals culture.CultureID
                                                                 where culture.CultureCode == cultureCode
                                                                 select catCulture).AsNoTracking().ToList();
                    if (categoryCulture.Any())
                    {
                        foreach (Category cat in categories)
                        {
                            var result = categoryCulture.Where(x => x.CategoryID == cat.CategoryIID).FirstOrDefault();
                            if (result.IsNotNull())
                                cat.CategoryName = result.CategoryName;
                        }
                    }
                }

                return categories;
            }
        }

        public List<Category> GetSubCategories(long categoryID, List<string> tagName, string cultureCode, int noOfRecords)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                try
                {
                    if (noOfRecords == 0) noOfRecords = 100000; //forinfinity, it's just for avoiding another query, find alternative
                    var categories = (from category in dbContext.Categories

                                      join cultureCategory in dbContext.CategoryCultureDatas on category.CategoryIID equals cultureCategory.CategoryID into categoryCultureData
                                      from cultureCategory in categoryCultureData.DefaultIfEmpty()
                                      join culture in dbContext.Cultures on cultureCategory.CultureID equals culture.CultureID into cutlureMaster
                                      from culture in cutlureMaster.Where(a => a.CultureCode == cultureCode).DefaultIfEmpty()

                                      join tagMap in dbContext.CategoryTagMaps on category.CategoryIID equals tagMap.CategoryID
                                      join tag in dbContext.CategoryTags on tagMap.CategoryTagID equals tag.CategoryTagIID

                                      where tagName.Contains(tag.TagName.Trim().ToUpper()) && category.ParentCategoryID == categoryID
                                      //&& culture.CultureCode == cultureCode
                                      select new
                                      {
                                          CategoryIID = category.CategoryIID,
                                          ParentCategoryID = category.ParentCategoryID,
                                          CategoryCode = category.CategoryCode,
                                          CategoryName = cultureCode == "en" ? category.CategoryName : !string.IsNullOrEmpty(cultureCategory.CategoryName) ? cultureCategory.CategoryName : category.CategoryName,
                                          ImageName = category.ImageName,
                                          ThumbnailImageName = category.ImageName,
                                          SeoMetadataID = category.SeoMetadataID,
                                          IsInNavigationMenu = category.IsInNavigationMenu,
                                          IsActive = category.IsActive,
                                          Created = category.Created,
                                          CreatedBy = category.CreatedBy,
                                          Updated = category.Updated,
                                          UpdatedBy = category.UpdatedBy,
                                          //TimeStamps = category.TimeStamps,
                                          CultureID = category.CultureID,
                                      }).Take(noOfRecords).ToList().Select(category => new Category
                                      {
                                          CategoryIID = category.CategoryIID,
                                          ParentCategoryID = category.ParentCategoryID,
                                          CategoryCode = category.CategoryCode,
                                          CategoryName = category.CategoryName,
                                          ImageName = category.ImageName,
                                          ThumbnailImageName = category.ImageName,
                                          SeoMetadataID = category.SeoMetadataID,
                                          IsInNavigationMenu = category.IsInNavigationMenu,
                                          IsActive = category.IsActive,
                                          Created = category.Created,
                                          CreatedBy = category.CreatedBy,
                                          Updated = category.Updated,
                                          UpdatedBy = category.UpdatedBy,
                                          //TimeStamps = category.TimeStamps,
                                          CultureID = category.CultureID,
                                      }).ToList();

                    foreach (var category in categories)
                    {
                        category.CategoryImageMaps = dbContext.CategoryImageMaps.Where(x => x.CategoryID == category.CategoryIID).AsNoTracking().ToList();
                    }

                    return categories;
                }
                catch (Exception ex)
                {
                    throw new Exception("Database repository error.", ex);
                }
            }
        }

        public List<Category> GetCategoryList(List<long> categoryIDs)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return (from category in dbContext.Categories
                                  select category).Where(a=> categoryIDs.Contains(a.CategoryIID)).AsNoTracking().ToList();
            }
        }

        public List<Category> GetCategoryList(string cultureCode, string searchText,bool isReporting=false)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var categories = LoadCategories(cultureCode,isReporting, searchText);
                if (cultureCode.IsNotNullOrEmpty() && cultureCode != "en")
                {
                    List<CategoryCultureData> categoryCulture  = (from catCulture in dbContext.CategoryCultureDatas
                                                                                   join culture in dbContext.Cultures on catCulture.CultureID equals culture.CultureID
                                                                                   where culture.CultureCode == cultureCode
                                                                                   select catCulture).AsNoTracking().ToList(); ;
                    if (categoryCulture.Any())
                    {
                        foreach (Category cat in categories)
                        {
                            var result = categoryCulture.Where(x => x.CategoryID == cat.CategoryIID).FirstOrDefault();
                            if (result.IsNotNull())
                                cat.CategoryName = result.CategoryName;
                        }
                    }
                }

                return categories;
            }
        }

        public List<Category> LoadCategories(string cultureCode, bool isReporting, string searchText)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext()){
                 List<Category> categories;
                var maxCount = new Domain.Setting.SettingBL(null).GetSettingValue<int>("Online_Store_MaxFetchCount", 200);

                if (searchText.IsNullOrEmpty())
                {
                    if (!isReporting)
                    {
                        categories = (from category in dbContext.Categories
                                      select category).Take(maxCount).AsNoTracking().ToList();
                    }
                    else
                    {
                        categories = (from category in dbContext.Categories
                                      where category.IsReporting == isReporting
                                      select category).Take(maxCount).AsNoTracking().ToList();
                    }
                }
                else
                {
                    if (!isReporting)
                    {
                        categories = (from category in dbContext.Categories
                                      select category).Where(a=> a.CategoryName.Contains(searchText)).Take(maxCount).AsNoTracking().ToList();
                    }
                    else
                    {
                        categories = (from category in dbContext.Categories
                                      where category.IsReporting == isReporting
                                      select category).Where(a=> a.CategoryName.Contains(searchText)).Take(maxCount).AsNoTracking().ToList();
                    }
                }

                 return categories;
            }
           
        }



        public List<CategoryImageMap> GetCategoryImage(Eduegate.Services.Contracts.Enums.ImageTypes? imageType, long categoryID, int companyID = 0)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var categoryImageMap = new List<CategoryImageMap>();

                if (imageType == null && categoryID != 0)
                {
                    if (companyID == 0)
                    {
                        categoryImageMap = dbContext.CategoryImageMaps.Where(a => a.CategoryID == categoryID)
                            .Include(i => i.ImageType)
                            .OrderBy(x => x.SerialNo == null).ThenBy(x => x.SerialNo).AsNoTracking().ToList();
                    }
                    else
                    {
                        categoryImageMap = dbContext.CategoryImageMaps.Where(a => a.CategoryID == categoryID && a.CompanyID == companyID)
                            .Include(i => i.ImageType)
                            .OrderBy(x => x.SerialNo == null).ThenBy(x => x.SerialNo).AsNoTracking().ToList();
                    }
                }
                else if (imageType == null)
                {
                    if (categoryID == 0)
                    {
                        categoryImageMap = dbContext.CategoryImageMaps
                            .Include(i => i.ImageType)
                            .OrderBy(x => x.SerialNo == null).ThenBy(x => x.SerialNo).AsNoTracking().ToList();
                    }
                    else if (companyID == 0)
                    {
                        categoryImageMap = dbContext.CategoryImageMaps.Where(a => a.CategoryID == categoryID)
                            .Include(i => i.ImageType)
                            .OrderBy(x => x.SerialNo == null).ThenBy(x => x.SerialNo).AsNoTracking().ToList();
                    }
                    else
                    {
                        categoryImageMap = dbContext.CategoryImageMaps.Where(a => a.CategoryID == categoryID && a.CompanyID == companyID)
                            .Include(i => i.ImageType)
                            .OrderBy(x => x.SerialNo == null).ThenBy(x => x.SerialNo).AsNoTracking().ToList();
                    }
                }
                else
                {
                    if (companyID == 0)
                    {
                        categoryImageMap = dbContext.CategoryImageMaps.Where(a => a.CategoryID == categoryID && a.ImageTypeID == (int)imageType)
                            .Include(i => i.ImageType)
                            .OrderBy(x => x.SerialNo == null).ThenBy(x => x.SerialNo).AsNoTracking().ToList();
                    }
                    else
                    {
                        categoryImageMap = dbContext.CategoryImageMaps
                            .Where(a => a.CategoryID == categoryID && a.CompanyID == companyID && a.ImageTypeID == (int)imageType)
                            .Include(i => i.ImageType)
                            .OrderBy(x => x.SerialNo == null).ThenBy(x => x.SerialNo).AsNoTracking().ToList();
                    }
                }
                
                //foreach (var map in categoryImageMap)
                //{
                //    dbContext.Entry(map).Reference(a => a.ImageType).Load();
                //}

                return categoryImageMap;
            }
        }

        public List<ProductTagDTO> GetCategoryTags(long categoryID)
        {
            var productTagList = new List<ProductTagDTO>();
            var productTag = new ProductTagDTO()
            {
                TagIID = 1,
                TagName = "Top Selling"
            };
            productTagList.Add(productTag);

            productTag = new ProductTagDTO()
            {
                TagIID = 2,
                TagName = "High Quality"
            };
            productTagList.Add(productTag);

            productTag = new ProductTagDTO()
            {
                TagIID = 2,
                TagName = "Recommended"
            };
            productTagList.Add(productTag);

            return productTagList;
        }

        public List<Category> GetSiteMapCategoryListByCompanyID(int compnayID)//compnayID has to be joined when ERP is updated in a proper way
        {
            var categories = new List<Category>();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                categories = (from category in dbContext.Categories
                              join catmap in dbContext.ProductCategoryMaps on category.CategoryIID equals catmap.CategoryID
                              join p in dbContext.Products on catmap.ProductID equals p.ProductIID
                              join s in dbContext.ProductSKUMaps on p.ProductIID equals s.ProductSKUMapIID
                              join pl in dbContext.ProductPriceListSKUMaps on s.ProductSKUMapIID equals pl.ProductSKUID
                              where pl.CompanyID == compnayID && category.IsActive == true
                              select category).Distinct().AsNoTracking().ToList();
            }
            return categories;
        }

        public List<Category> GetCategoryBySkuId(long skuId)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var categories = (from sku in db.ProductSKUMaps
                                  join
                                  pcm in db.ProductCategoryMaps on sku.ProductID equals pcm.ProductID
                                  join
                                  c in db.Categories on pcm.CategoryID equals c.CategoryIID
                                  where sku.ProductSKUMapIID == skuId
                                  select c)
                                  .AsNoTracking()
                                  .ToList();
                return categories;
            }
        }

        public List<CategorySetting> GetCategorySettings(long categoryID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var result = new List<CategorySetting>();
                var categoryGlobalList = db.CategorySettings.Where(x => x.CategoryID == null).ToList();

                if (categoryID > 0)
                {
                    var categorySettingList = db.CategorySettings.Where(x => x.CategoryID == categoryID).AsNoTracking().ToList();
                    if (categorySettingList.IsNotNull() && categorySettingList.Count > 0)
                    {
                        foreach (var settingItem in categoryGlobalList)
                        {
                            var item = categorySettingList.Where(x => x.SettingCode == settingItem.SettingCode).FirstOrDefault();

                            if (item != null)
                            {
                                settingItem.CategorySettingsID = item.CategorySettingsID;
                                settingItem.SettingValue = item.IsNotNull() ? item.SettingValue : settingItem.SettingValue;
                            }

                            settingItem.CategoryID = categoryID;
                            result.Add(settingItem);
                        }
                    }
                }

                if (result.Count == 0)
                {
                    categoryGlobalList.ForEach(x => x.CategorySettingsID = 0);
                    result.AddRange(categoryGlobalList);
                }
                return result;
            }
        }

        public CategorySetting GetCategorySettings(long companyID, string categoryCode, string settingCode)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.CategorySettings.Where(x => x.Category.CategoryCode == categoryCode && x.SettingCode == settingCode).AsNoTracking().FirstOrDefault();
            }
        }
    }
}

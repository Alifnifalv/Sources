using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Domain.Entity.Models.ValueObjects;

namespace Eduegate.Domain.Repository
{

    public class BlinkLegacyRepository
    {
        public List<Domain.Entity.ProductMaster> GetProducts(int pageNumber, int pageSize,int cultureID=1,int companyID =1)
        {
            using (Eduegate.Domain.Entity.Models.dbBlinkContext dbContext = new Eduegate.Domain.Entity.Models.dbBlinkContext())
            {
                dbContext.Database.CommandTimeout = 0;


                var productMaster = (from a in dbContext.Products
                                     join b in dbContext.Brands on a.BrandID equals b.BrandIID
                                     join d in dbContext.ProductSKUMaps on a.ProductIID equals d.ProductID
                                     join c in dbContext.ProductSKUCultureDatas on d.ProductSKUMapIID equals c.ProductSKUMapID into LJProductCulture
                                     from LJPC in LJProductCulture.DefaultIfEmpty()
                                     join m in dbContext.SeoMetadatas on d.SeoMetadataID equals m.SEOMetadataIID into LJMetadata
                                     from LJMeta in LJMetadata.DefaultIfEmpty()
                                     join mc in dbContext.SeoMetadataCultureDatas on d.SeoMetadataID equals mc.SEOMetadataID into LJMetaCulture
                                     from LJMC in LJMetaCulture.DefaultIfEmpty()
                                    
                                     join ps in dbContext.ProductPriceListSKUMaps on d.ProductSKUMapIID equals ps.ProductSKUID 
                                     where ps.IsActive==true && b.StatusID==(int)Eduegate.Services.Contracts.Enums.BrandStatuses.Active && a.IsOnline != null && a.IsOnline == true && d.StatusID != null && d.StatusID == 2
                                     && (ps.CompanyID != null && ps.CompanyID == companyID)

                                     select new Domain.Entity.ProductMaster
                                     {
                                         ProductID = a.ProductIID,
                                         SkuID = d.ProductSKUMapIID,
                                         ProductName =cultureID == 1 ? d.SKUName : LJPC.ProductSKUName,
                                         ProductNameAr = cultureID == 1 ? LJPC.ProductSKUName : d.SKUName,
                                         ProductDescription = "",
                                         ProductPrice =0,
                                         ProductPartNo = d.PartNo,
                                         ProductCode = d.ProductSKUCode,
                                         RefBrandID = (long)a.BrandID,
                                         DisableListing = d.HideSKU,
                                         ProductKeywordsEn = cultureID == 1 ? LJMeta.MetaKeywords : LJMC.MetaKeywords,
                                         ProductDiscountPrice = 0,
                                         ProductActive = true,
                                         ProductCreatedOn=d.CreatedDate
                                     })
                                    .OrderBy(a => a.ProductID).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                return productMaster;
            }
        }

        public List<Domain.Entity.ProductMaster> GetProductsIntl(int pageNumber, int pageSize, int country)
        {
            using (Eduegate.Domain.Entity.Models.dbBlinkContext dbContext = new Eduegate.Domain.Entity.Models.dbBlinkContext())
            {
                dbContext.Database.CommandTimeout = 0;

                var categoryMasterActive = (from c in dbContext.CategoryMasters
                                            join p in dbContext.ProductCategories on c.CategoryID equals p.RefProductCategoryCategoryID

                                            where c.CategoryActiveKSA == true

                                            select new Domain.Entity.ProductMaster
                                            {
                                                ProductID = p.RefProductCategoryProductID,
                                            }).Distinct().ToList();


                var productMaster = (from a in dbContext.ProductMasters
                                     join d in dbContext.BrandMasters on a.RefBrandID equals d.BrandID
                                     join al in dbContext.ProductMasterIntls on a.ProductID equals al.RefProductMasterID
                                     where d.BrandActive == true && al.ProductActive == true && al.RefCountryID == country
                                     select new Domain.Entity.ProductMaster
                                     {
                                         ProductID = a.ProductID,
                                         ProductName = a.ProductName,
                                         ProductDescription = a.ProductDescription,
                                         ProductCategoryAll = a.ProductCategoryAll,
                                         ProductPrice = al.ProductPrice,
                                         SupplierID = al.RefSupplierID,
                                         ProductAvailableQuantity = al.ProductAvailableQty,
                                         ProductPartNo = a.ProductPartNo,
                                         ProductActiveAr = al.ProductActive,
                                         ProductCode = a.ProductCode,
                                         ProductColor = a.ProductColor,
                                         ProductColorAr = a.ProductColorAr,
                                         ProductDiscountPrice = al.ProductDiscountPrice,
                                         ProductMadeIn = a.ProductMadeIn,
                                         ProductThumbnailMaster = a.ProductThumbnailMaster,
                                         ProductWarranty = a.ProductWarranty,
                                         ProductWeight = a.ProductWeight,
                                         RefBrandID = a.RefBrandID,
                                         ProductListingImageMaster = a.ProductListingImageMaster,
                                         DisableListing = a.DisableListing,
                                         ProductActive = al.ProductActive,
                                         ProductModel = a.ProductModel,
                                         QuantityDiscount = al.QuantityDiscount,
                                         DeliveryDays = al.DeliveryDays,
                                         ProductKeywordsEn = a.ProductKeywordsEn,
                                         ProductNameAr = a.ProductNameAr,
                                         ProductKeywordsAr = a.ProductKeywordsAr,
                                     })
                                     .Where(a => a.ProductActive == true)
                                    .OrderBy(a => a.ProductID).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                var CatList = new List<Domain.Entity.ProductMaster>();


                productMaster = (from m in productMaster
                                 join r in categoryMasterActive on m.ProductID equals r.ProductID
                                 select new Domain.Entity.ProductMaster
                                 {
                                     ProductID = m.ProductID,
                                     ProductName = m.ProductName,
                                     ProductDescription = m.ProductDescription,
                                     ProductCategoryAll = m.ProductCategoryAll,
                                     ProductPrice = m.ProductPrice,
                                     SupplierID = m.SupplierID,
                                     ProductAvailableQuantity = m.ProductAvailableQuantity,
                                     ProductPartNo = m.ProductPartNo,
                                     ProductActiveAr = m.ProductActiveAr,
                                     ProductCode = m.ProductCode,
                                     ProductColor = m.ProductColor,
                                     ProductColorAr = m.ProductColorAr,
                                     ProductDiscountPrice = m.ProductDiscountPrice,
                                     ProductMadeIn = m.ProductMadeIn,
                                     ProductThumbnailMaster = m.ProductThumbnailMaster,
                                     ProductWarranty = m.ProductWarranty,
                                     ProductWeight = m.ProductWeight,
                                     RefBrandID = m.RefBrandID,
                                     ProductListingImageMaster = m.ProductListingImageMaster,
                                     DisableListing = m.DisableListing,
                                     ProductActive = m.ProductActive,
                                     ProductModel = m.ProductModel,
                                     QuantityDiscount = m.QuantityDiscount,
                                     DeliveryDays = m.DeliveryDays,
                                     ProductKeywordsEn = m.ProductKeywordsEn,
                                     ProductNameAr = m.ProductNameAr,
                                     ProductKeywordsAr = m.ProductKeywordsAr,
                                 }).ToList();
                return productMaster;
            }
        }

        public List<ProductSearchKeyword> GetKeywords(int pageNumber, int pageSize)
        {

            using (Eduegate.Domain.Entity.Models.dbBlinkContext dbContext = new Eduegate.Domain.Entity.Models.dbBlinkContext())
            {
                return dbContext.ProductSearchKeywords
                    .OrderBy(a => a.LogID)
                    .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList()
                    .ToList<ProductSearchKeyword>();
            }
        }

        public List<ProductSearchKeywordsAr> GetKeywordsAr(int pageNumber, int pageSize)
        {
            using (Eduegate.Domain.Entity.Models.dbBlinkContext dbContext = new Eduegate.Domain.Entity.Models.dbBlinkContext())
            {
                return dbContext.ProductSearchKeywordsArs
                   .OrderBy(a => a.LogID)
                   .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList()
                   .ToList<ProductSearchKeywordsAr>();

            }
        }

        public List<ProductSearchKeywordsIntl> GetKeywordsIntl(int pageNumber, int pageSize)
        {

            using (Eduegate.Domain.Entity.Models.dbBlinkContext dbContext = new Eduegate.Domain.Entity.Models.dbBlinkContext())
            {
                return dbContext.ProductSearchKeywordsIntls
                    .OrderBy(a => a.LogID)
                    .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList()
                    .ToList<ProductSearchKeywordsIntl>();
            }
        }

        public List<ProductSearchKeywordsArIntl> GetKeywordsArIntl(int pageNumber, int pageSize)
        {
            using (Eduegate.Domain.Entity.Models.dbBlinkContext dbContext = new Eduegate.Domain.Entity.Models.dbBlinkContext())
            {
                return dbContext.ProductSearchKeywordsArIntls
                   .OrderBy(a => a.LogID)
                   .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList()
                   .ToList<ProductSearchKeywordsArIntl>();

            }
        }

        public Domain.Entity.ProductMaster GetProducts(long productID,int cultureID=1,int companyID=1)
        {
            using (Eduegate.Domain.Entity.Models.dbBlinkContext dbContext = new Eduegate.Domain.Entity.Models.dbBlinkContext())
            {
                dbContext.Database.CommandTimeout = 0;

                var productMaster = (from a in dbContext.Products
                                     join b in dbContext.Brands on a.BrandID equals b.BrandIID
                                     join d in dbContext.ProductSKUMaps on a.ProductIID equals d.ProductID
                                     join c in dbContext.ProductSKUCultureDatas on d.ProductSKUMapIID equals c.ProductSKUMapID into LJProductCulture
                                     from LJPC in LJProductCulture.DefaultIfEmpty()
                                     join m in dbContext.SeoMetadatas on d.SeoMetadataID equals m.SEOMetadataIID into LJMetadata
                                     from LJMeta in LJMetadata.DefaultIfEmpty()
                                     join mc in dbContext.SeoMetadataCultureDatas on d.SeoMetadataID equals mc.SEOMetadataID into LJMetaCulture
                                     from LJMC in LJMetaCulture.DefaultIfEmpty()
                                     
                                     join ps in dbContext.ProductPriceListSKUMaps on d.ProductSKUMapIID equals ps.ProductSKUID
                                     where ps.IsActive == true && b.StatusID == (int)Eduegate.Services.Contracts.Enums.BrandStatuses.Active && d.ProductSKUMapIID == productID && a.IsOnline != null && a.IsOnline == true && d.StatusID != null && d.StatusID == 2
                                      && (ps.CompanyID != null && ps.CompanyID == companyID) 
                                     select new Domain.Entity.ProductMaster
                                     {
                                         ProductID = a.ProductIID,
                                         SkuID = d.ProductSKUMapIID,
                                         ProductName = cultureID == 1 ? d.SKUName : LJPC.ProductSKUName,
                                         ProductNameAr = cultureID == 1 ? LJPC.ProductSKUName : d.SKUName,
                                         ProductDescription = "",
                                         ProductPrice = 0,
                                         ProductPartNo = d.PartNo,
                                         ProductCode = d.ProductSKUCode,
                                         RefBrandID = (long)a.BrandID,
                                         DisableListing = d.HideSKU,
                                         ProductKeywordsEn = cultureID == 1 ? LJMeta.MetaKeywords : LJMC.MetaKeywords,
                                         ProductDiscountPrice = 0,
                                         ProductActive = true,
                                         ProductCreatedOn = d.CreatedDate
                                     }).FirstOrDefault();
                return productMaster;
            }
        }

        public Domain.Entity.ProductMaster GetProductsIntl(long productID, int country)
        {
            using (Eduegate.Domain.Entity.Models.dbBlinkContext dbContext = new Eduegate.Domain.Entity.Models.dbBlinkContext())
            {
                dbContext.Database.CommandTimeout = 0;

                var categoryMasterActive = (from c in dbContext.CategoryMasters
                                            join p in dbContext.ProductCategories on c.CategoryID equals p.RefProductCategoryCategoryID

                                            where c.CategoryActiveKSA == true

                                            select new Domain.Entity.ProductMaster
                                            {
                                                ProductID = p.RefProductCategoryProductID,
                                            }).Distinct().ToList();


                var productMaster = (from a in dbContext.ProductMasters
                                     join d in dbContext.BrandMasters on a.RefBrandID equals d.BrandID
                                     join al in dbContext.ProductMasterIntls on a.ProductID equals al.RefProductMasterID
                                     where d.BrandActive == true && al.ProductActive == true && al.RefCountryID == country

                                     where d.BrandActive == true
                                     select new Domain.Entity.ProductMaster
                                     {
                                         ProductID = a.ProductID,
                                         ProductName = a.ProductName,
                                         ProductDescription = a.ProductDescription,
                                         ProductCategoryAll = a.ProductCategoryAll,
                                         ProductPrice = al.ProductPrice,
                                         SupplierID = al.RefSupplierID,
                                         ProductAvailableQuantity = al.ProductAvailableQty,
                                         ProductPartNo = a.ProductPartNo,
                                         ProductActiveAr = al.ProductActive,
                                         ProductCode = a.ProductCode,
                                         ProductColor = a.ProductColor,
                                         ProductColorAr = a.ProductColorAr,
                                         ProductDiscountPrice = al.ProductDiscountPrice,
                                         ProductMadeIn = a.ProductMadeIn,
                                         ProductThumbnailMaster = a.ProductThumbnailMaster,
                                         ProductWarranty = a.ProductWarranty,
                                         ProductWeight = a.ProductWeight,
                                         RefBrandID = a.RefBrandID,
                                         ProductListingImageMaster = a.ProductListingImageMaster,
                                         DisableListing = a.DisableListing,
                                         ProductActive = al.ProductActive,
                                         ProductModel = a.ProductModel,
                                         QuantityDiscount = al.QuantityDiscount,
                                         DeliveryDays = al.DeliveryDays,
                                         ProductKeywordsEn = a.ProductKeywordsEn,
                                         ProductNameAr = a.ProductNameAr,
                                         ProductKeywordsAr = a.ProductKeywordsAr,
                                     })
                                     .Where(a => a.ProductActive == true)
                                    .OrderBy(a => a.ProductID).ToList();
                var CatList = new List<Domain.Entity.ProductMaster>();


                productMaster = (from m in productMaster
                                 join r in categoryMasterActive on m.ProductID equals r.ProductID
                                 select new Domain.Entity.ProductMaster
                                 {
                                     ProductID = m.ProductID,
                                     ProductName = m.ProductName,
                                     ProductDescription = m.ProductDescription,
                                     ProductCategoryAll = m.ProductCategoryAll,
                                     ProductPrice = m.ProductPrice,
                                     SupplierID = m.SupplierID,
                                     ProductAvailableQuantity = m.ProductAvailableQuantity,
                                     ProductPartNo = m.ProductPartNo,
                                     ProductActiveAr = m.ProductActiveAr,
                                     ProductCode = m.ProductCode,
                                     ProductColor = m.ProductColor,
                                     ProductColorAr = m.ProductColorAr,
                                     ProductDiscountPrice = m.ProductDiscountPrice,
                                     ProductMadeIn = m.ProductMadeIn,
                                     ProductThumbnailMaster = m.ProductThumbnailMaster,
                                     ProductWarranty = m.ProductWarranty,
                                     ProductWeight = m.ProductWeight,
                                     RefBrandID = m.RefBrandID,
                                     ProductListingImageMaster = m.ProductListingImageMaster,
                                     DisableListing = m.DisableListing,
                                     ProductActive = m.ProductActive,
                                     ProductModel = m.ProductModel,
                                     QuantityDiscount = m.QuantityDiscount,
                                     DeliveryDays = m.DeliveryDays,
                                     ProductKeywordsEn = m.ProductKeywordsEn,
                                     ProductNameAr = m.ProductNameAr,
                                     ProductKeywordsAr = m.ProductKeywordsAr,
                                 }).Where(a => a.ProductID == productID).ToList();

                if (productMaster.Count == 0)
                {
                    Domain.Entity.ProductMaster vEmptyProduct = new Entity.ProductMaster()
                    {

                        ProductID = 0,
                        ProductName = string.Empty,
                        ProductDescription = string.Empty,
                        ProductCategoryAll = string.Empty,
                        ProductPrice = 0,
                        SupplierID = 0,
                        ProductAvailableQuantity = 0,
                        ProductPartNo = string.Empty,
                        ProductActiveAr = false,
                        ProductCode = string.Empty,
                        ProductColor = string.Empty,
                        ProductColorAr = string.Empty,
                        ProductDiscountPrice = 0,
                        ProductMadeIn = string.Empty,
                        ProductThumbnailMaster = string.Empty,
                        ProductWarranty = string.Empty,
                        ProductWeight = 0,
                        RefBrandID = 0,
                        ProductListingImageMaster = string.Empty,
                        DisableListing = false,
                        ProductActive = false,
                        ProductModel = string.Empty,
                        QuantityDiscount = false,
                        DeliveryDays = 0,
                        ProductKeywordsEn = string.Empty,
                        SKU = string.Empty,
                        ProductNameAr = string.Empty,
                        ProductKeywordsAr = string.Empty,
                    };
                    return vEmptyProduct;
                }
                return productMaster.FirstOrDefault();

                //var productMaster = (from a in (dbContext.ProductMasters.Select(x => new
                //{
                //    x.ProductID,
                //    x.ProductName,
                //    x.ProductDescription,
                //    x.ProductCategoryAll,
                //    x.ProductPrice,
                //    x.SupplierID,
                //    x.ProductAvailableQuantity,
                //    x.ProductPartNo,
                //    x.ProductActiveAr,
                //    x.ProductActive,
                //    x.ProductCode,
                //    x.ProductColor,
                //    x.ProductColorAr,
                //    x.ProductCostPrice,
                //    x.ProductDiscountPrice,
                //    x.ProductMadeIn,
                //    x.ProductThumbnailMaster,
                //    x.ProductWarranty,
                //    x.ProductWeight,
                //    x.RefBrandID,
                //    x.ProductListingImageMaster,
                //    x.DisableListing,
                //    x.ProductModel,
                //}))
                //                     select new Domain.Entity.ProductMaster
                //                     {
                //                         ProductID = a.ProductID,
                //                         ProductName = a.ProductName,
                //                         ProductDescription = a.ProductDescription,
                //                         ProductCategoryAll = a.ProductCategoryAll,
                //                         ProductPrice = a.ProductPrice,
                //                         SupplierID = a.SupplierID,
                //                         ProductAvailableQuantity = a.ProductAvailableQuantity,
                //                         ProductPartNo = a.ProductPartNo,
                //                         ProductActiveAr = a.ProductActiveAr,
                //                         ProductCode = a.ProductCode,
                //                         ProductColor = a.ProductColor,
                //                         ProductColorAr = a.ProductColorAr,
                //                         ProductDiscountPrice = a.ProductDiscountPrice,
                //                         ProductMadeIn = a.ProductMadeIn,
                //                         ProductThumbnailMaster = a.ProductThumbnailMaster,
                //                         ProductWarranty = a.ProductWarranty,
                //                         ProductWeight = a.ProductWeight,
                //                         RefBrandID = a.RefBrandID,
                //                         ProductListingImageMaster = a.ProductListingImageMaster,
                //                         DisableListing = a.DisableListing,
                //                         ProductActive = a.ProductActive,
                //                         ProductModel = a.ProductModel
                //                     }).Where(a => a.ProductID == productID)
                //                    .FirstOrDefault();

            }
        }

        public List<Domain.Entity.Models.Category> GetCategory(int pageNumber, int pageSize)
        {
            using (Eduegate.Domain.Entity.Models.dbBlinkContext dbContext = new Eduegate.Domain.Entity.Models.dbBlinkContext())
            {
                var categoryMaster = (from a in (dbContext.Categories.Select(x => new
                {
                    x.CategoryIID,
                    x.CategoryName,
                    x.CategoryCode,

                }))
                                      select new Eduegate.Domain.Entity.Models.Category
                                      {
                                          CategoryIID = a.CategoryIID,
                                          CategoryName = a.CategoryName,
                                          CategoryCode = a.CategoryCode,
                                      })
                                    .OrderBy(a => a.CategoryIID).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                return categoryMaster;
            }
        }

        public Domain.Entity.Models.Category GetCategory(long categoryID)
        {
            using (Eduegate.Domain.Entity.Models.dbBlinkContext dbContext = new Eduegate.Domain.Entity.Models.dbBlinkContext())
            {
                var categories = dbContext.Categories.Where(a => (long?)a.CategoryIID == categoryID && a.IsActive == true ).FirstOrDefault();
                return categories;
            }
        }

        public CategoryCultureDatas GetCategoryCulture(long categoryID,int cultureID =1)
        {
            using (Eduegate.Domain.Entity.Models.dbBlinkContext dbContext = new Eduegate.Domain.Entity.Models.dbBlinkContext())
            {
                var categories = (from c in dbContext.Categories
                                  join cc in dbContext.CategoryCultureDatas on c.CategoryIID equals cc.CategoryID into LJCC
                                  from LLJCC in LJCC.DefaultIfEmpty()
                                  join s in dbContext.SeoMetadatas on c.SeoMetadataID equals s.SEOMetadataIID into LJSS
                                  from LLJSS in LJSS.DefaultIfEmpty()
                                  join ss in dbContext.SeoMetadataCultureDatas on c.SeoMetadataID equals ss.SEOMetadataID into LJSM
                                  from LLJSM in LJSM.DefaultIfEmpty()
                                  where c.CategoryIID == categoryID && c.IsActive == true 
                                  select new CategoryCultureDatas
                                  {
                                      CategoryID = c.CategoryIID,
                                      CategoryCode = c.CategoryCode,
                                      CategoryName = cultureID == 1 ? c.CategoryName : LLJCC.CategoryName,
                                      CategoryKeyWords = cultureID == 1 ? LLJSS.MetaKeywords : LLJSM.MetaKeywords,
                                      IsActive = (bool)c.IsActive
                                  }).FirstOrDefault();

                return categories;
               
            }
        }

        public long GetTotalProducts(int companyID)
        {
                using (Eduegate.Domain.Entity.Models.dbBlinkContext dbContext = new Eduegate.Domain.Entity.Models.dbBlinkContext())
                {
                    var productMaster = (from a in dbContext.Products
                                          join b in dbContext.Brands on a.BrandID equals b.BrandIID
                                          join d in dbContext.ProductSKUMaps on a.ProductIID equals d.ProductID
                                          join c in dbContext.ProductSKUCultureDatas on d.ProductSKUMapIID equals c.ProductSKUMapID into LJProductCulture
                                          from LJPC in LJProductCulture.DefaultIfEmpty()
                                          join m in dbContext.SeoMetadatas on d.SeoMetadataID equals m.SEOMetadataIID into LJMetadata
                                          from LJMeta in LJMetadata.DefaultIfEmpty()
                                          join mc in dbContext.SeoMetadataCultureDatas on d.SeoMetadataID equals mc.SEOMetadataID into LJMetaCulture
                                          from LJMC in LJMetaCulture.DefaultIfEmpty()
                                          join ps in dbContext.ProductPriceListSKUMaps on d.ProductSKUMapIID equals ps.ProductSKUID
                                          where b.StatusID == (int)Eduegate.Services.Contracts.Enums.BrandStatuses.Active && a.IsOnline != null && a.IsOnline == true && d.StatusID != null && d.StatusID == 2
                                          && (ps.CompanyID != null && ps.CompanyID == companyID)
                                          select new
                                          {
                                              d.ProductSKUMapIID
                                          });
                    return productMaster.Count();
                }
        }

        public long GetTotalCategories()
        {
            using (Eduegate.Domain.Entity.Models.dbBlinkContext dbContext = new Eduegate.Domain.Entity.Models.dbBlinkContext())
            {
                return dbContext.CategoryMasters.Count();
            }
        }

        public long GetTotalKeywords()
        {
            using (Eduegate.Domain.Entity.Models.dbBlinkContext dbContext = new Eduegate.Domain.Entity.Models.dbBlinkContext())
            {
                return dbContext.ProductSearchKeywords.Count();
            }
        }

        public BrandDTO GetBrand(long brandID)
        {
            using (Eduegate.Domain.Entity.Models.dbBlinkContext dbContext = new Eduegate.Domain.Entity.Models.dbBlinkContext())
            {
                var brands = (from a in (dbContext.Brands.Select(x => new
                {
                    x.BrandIID,
                    x.BrandCode,
                    x.BrandName,
                    x.Descirption,
                  
                }))
                              select new BrandDTO
                               {
                                   BrandIID = a.BrandIID,
                                   BrandCode = a.BrandCode,
                                   BrandName = a.BrandName,
                                   Description = a.Descirption,
                               }).Where(a => a.BrandIID == brandID)
                                    .FirstOrDefault();
                return brands;
            }
        }

        public BrandDTO GetBrandCultureData(long brandID,int cultureID=1)
        {
            using (Eduegate.Domain.Entity.Models.dbBlinkContext dbContext = new Eduegate.Domain.Entity.Models.dbBlinkContext())
            {
                var result = (from b in dbContext.Brands
                              join c in dbContext.BrandCultureDatas on b.BrandIID equals c.BrandID into LJB
                              from LLJB in LJB.DefaultIfEmpty()
                              join s in dbContext.SeoMetadatas on b.SEOMetadataID equals s.SEOMetadataIID into LJSS
                              from LLJSS in LJSS.DefaultIfEmpty()
                              join ss in dbContext.SeoMetadataCultureDatas on b.SEOMetadataID equals ss.SEOMetadataID into LJS
                              from LLJS in LJS.DefaultIfEmpty()
                              where (b.BrandIID !=null && b.BrandIID == brandID) //&& LLJB.CultureID == cultureID && LLJS.CultureID == cultureID
                              select new BrandDTO {
                                  BrandIID = b.BrandIID,
                                  BrandCode = b.BrandCode,
                                  BrandName = cultureID==1 ? b.BrandName : LLJB.BrandName,
                                  BrandKeywordsEn = cultureID == 1 ? LLJSS.MetaKeywords : LLJS.MetaKeywords,
                              } ).FirstOrDefault();
                return result;
            }
        }

        public int GetNewArrival(long productID, int country)
        {
            if (country == 10000)
            {
                using (Eduegate.Domain.Entity.Models.dbBlinkContext dbContext = new Eduegate.Domain.Entity.Models.dbBlinkContext())
                {
                    var newArrival = dbContext.vwProductNewArrivals.Where(a => a.ProductID == productID).FirstOrDefault();
                    if (newArrival == null)
                        return 0;
                    else
                        return newArrival.NewArrival;
                }
            }
            else
            {
                using (Eduegate.Domain.Entity.Models.dbBlinkContext dbContext = new Eduegate.Domain.Entity.Models.dbBlinkContext())
                {
                    var newArrival = dbContext.vwProductNewArrivalsIntls.Where(a => a.ProductID == productID && a.RefCountryID == country).FirstOrDefault();
                    if (newArrival == null)
                        return 0;
                    else
                        return newArrival.NewArrival;
                }
            }

        }

        public int GetProductAvailableQtyIntl(long productID, int country, int availableQtyKwt)
        {
            if (country == 10000)
            {
                return availableQtyKwt;

            }
            else
            {
                using (Eduegate.Domain.Entity.Models.dbBlinkContext dbContext = new Eduegate.Domain.Entity.Models.dbBlinkContext())
                {
                    var productAvailableQty = dbContext.vwProductListSearchListingIntls.Where(a => a.ProductID == productID && a.RefCountryID == country).FirstOrDefault();
                    if (productAvailableQty == null)
                        return 0;
                    else
                        return productAvailableQty.ProductAvailableQuantity;
                }
            }

        }

        public int? GetProductSoldQty(long productID, int country)
        {
            if (country == 10000)
            {
                using (Eduegate.Domain.Entity.Models.dbBlinkContext dbContext = new Eduegate.Domain.Entity.Models.dbBlinkContext())
                {
                    var query = dbContext.vwProductOrderByDates.Where(a => a.RefOrderProductID == productID);

                    if (query.Count() > 0)
                        return query.Sum(a => a.OrderQuantity);
                    else
                        return null;

                }
            }
            else
            {
                using (Eduegate.Domain.Entity.Models.dbBlinkContext dbContext = new Eduegate.Domain.Entity.Models.dbBlinkContext())
                {
                    var query = dbContext.vwProductOrderByDateIntls.Where(a => a.RefOrderProductID == productID && a.RefCountryID == country);

                    if (query.Count() > 0)
                        return query.Sum(a => a.OrderQuantity);
                    else
                        return null;

                }
            }

        }

        public Domain.Entity.CategoryMaster GetCategoryProductLevel(long categoryID, long productID, int country)
        {
            if (country == 10000)
            {
                using (Eduegate.Domain.Entity.Models.dbBlinkContext dbContext = new Eduegate.Domain.Entity.Models.dbBlinkContext())
                {
                    var categoryMaster = (from c in dbContext.CategoryMasters
                                          where c.CategoryActive == true && c.CategoryID == categoryID

                                          select new Domain.Entity.CategoryMaster
                                          {
                                              CategoryID = c.CategoryID,
                                              CategoryNameEn = c.CategoryNameEn,
                                              CategoryDescriptionEn = c.CategoryDescriptionEn,
                                              CategoryCode = c.CategoryCode,
                                              SeoKeywords = c.SeoKeywords,
                                              CategoryActive = c.CategoryActive,
                                              CategoryLevel = c.CategoryLevel,
                                              CategoryNameAr = c.CategoryNameAr,
                                              SeoKeywordsAr = c.SeoKeywordsAr
                                          }).Where(a => a.CategoryActive == true)
                                        .OrderBy(a => a.CategoryID).ToList();

                    var categoryMasterActive = (from c in dbContext.CategoryMasters
                                                join p in dbContext.ProductCategories on c.CategoryID equals p.RefProductCategoryCategoryID

                                                where c.CategoryActive == true && p.RefProductCategoryProductID == productID

                                                select new Domain.Entity.CategoryMaster
                                                {
                                                    CategoryID = p.RefProductCategoryCategoryID,
                                                }).Distinct().ToList();
                    var CatList = new List<Domain.Entity.CategoryMaster>();

                    categoryMaster = (from m in categoryMaster
                                      join r in categoryMasterActive on m.CategoryID equals r.CategoryID
                                      select new Domain.Entity.CategoryMaster
                                      {
                                          CategoryID = m.CategoryID,
                                          CategoryNameEn = m.CategoryNameEn,
                                          CategoryDescriptionEn = m.CategoryDescriptionEn,
                                          CategoryCode = m.CategoryCode,
                                          SeoKeywords = m.SeoKeywords,
                                          CategoryActive = m.CategoryActive,
                                          CategoryLevel = m.CategoryLevel,
                                          CategoryNameAr = m.CategoryNameAr,
                                          SeoKeywordsAr = m.SeoKeywordsAr
                                      }).ToList();
                    return categoryMaster.FirstOrDefault();
                }
            }
            else
            {
                using (Eduegate.Domain.Entity.Models.dbBlinkContext dbContext = new Eduegate.Domain.Entity.Models.dbBlinkContext())
                {
                    var categoryMaster = (from c in dbContext.CategoryMasters
                                          where c.CategoryActive == true && c.CategoryID == categoryID

                                          select new Domain.Entity.CategoryMaster
                                          {
                                              CategoryID = c.CategoryID,
                                              CategoryNameEn = c.CategoryNameEn,
                                              CategoryDescriptionEn = c.CategoryDescriptionEn,
                                              CategoryCode = c.CategoryCode,
                                              SeoKeywords = c.SeoKeywords,
                                              CategoryActive = c.CategoryActive,
                                              CategoryLevel = c.CategoryLevel,
                                              CategoryNameAr = c.CategoryNameAr,
                                              SeoKeywordsAr = c.SeoKeywordsAr
                                          }).Where(a => a.CategoryActive == true)
                                        .OrderBy(a => a.CategoryID).ToList();

                    var categoryMasterActive = (from c in dbContext.CategoryMasters
                                                join p in dbContext.ProductCategories on c.CategoryID equals p.RefProductCategoryCategoryID

                                                where c.CategoryActiveKSA == true && p.RefProductCategoryProductID == productID

                                                select new Domain.Entity.CategoryMaster
                                                {
                                                    CategoryID = p.RefProductCategoryCategoryID,
                                                }).Distinct().ToList();
                    var CatList = new List<Domain.Entity.CategoryMaster>();

                    categoryMaster = (from m in categoryMaster
                                      join r in categoryMasterActive on m.CategoryID equals r.CategoryID
                                      select new Domain.Entity.CategoryMaster
                                      {
                                          CategoryID = m.CategoryID,
                                          CategoryNameEn = m.CategoryNameEn,
                                          CategoryDescriptionEn = m.CategoryDescriptionEn,
                                          CategoryCode = m.CategoryCode,
                                          SeoKeywords = m.SeoKeywords,
                                          CategoryActive = m.CategoryActive,
                                          CategoryLevel = m.CategoryLevel,
                                          CategoryNameAr = m.CategoryNameAr,
                                          SeoKeywordsAr = m.SeoKeywordsAr
                                      }).ToList();
                    return categoryMaster.FirstOrDefault();
                }
            }

        }

        public bool UpdateEntityChangeTrackerLog()
        {
            using (dbBlinkContext dbContext = new dbBlinkContext())
            {
                dbContext.Database.CommandTimeout = 0;
                try
                {
                    //var queues = from element in dbContext.EntityChangeTrackerLogs where element.SyncedOn == null select element;
                    var queues = dbContext.EntityChangeTrackerLogs.Where(a => a.SyncedOn == null).ToList();
                    foreach (var queue in queues)
                    {
                        var result = new List<ProductMaster>();
                        if (queue.EntityChangeTrackerType == 0) //categoryid
                        {
                            result = dbContext.ProductMasters.Where(a => a.ProductCategoryAll.Contains(queue.EntityChangeTrackerTypeID.ToString())).ToList();
                            //result = dbContext.ProductMasters.Where(a => a.ProductCategoryAll.Contains(queue.EntityChangeTrackerTypeID.ToString())).Take(100).ToList();
                        }
                        else if (queue.EntityChangeTrackerType == 1) //brand
                        {
                            result = dbContext.ProductMasters.Where(a => a.RefBrandID == queue.EntityChangeTrackerTypeID).ToList();

                        }
                        else if (queue.EntityChangeTrackerType == 2)//supplier
                        {
                            result = dbContext.ProductMasters.Where(a => a.SupplierID == queue.EntityChangeTrackerTypeID).ToList();
                        }

                        var vEntity = dbContext.EntityChangeTrackerLogs.Where(a => a.EntityChangeTrackerLogID == queue.EntityChangeTrackerLogID).FirstOrDefault();
                        vEntity.SyncedOn = DateTime.Now;
                        dbContext.SaveChanges();
                        if (result.Count != 0)
                        {
                            foreach (var a in result)
                            {
                                var vExists = dbContext.EntityChangeTrackers.Any(b => b.ProcessedID == (long)a.ProductID && b.TrackerStatusID == 1);
                                if (!vExists)
                                {
                                    var vEntityChangeTracker = new EntityChangeTracker();
                                    vEntityChangeTracker.EntityID = 1;
                                    vEntityChangeTracker.OperationTypeID = 2;
                                    vEntityChangeTracker.ProcessedID = a.ProductID;
                                    vEntityChangeTracker.ProcessedFields = "";
                                    vEntityChangeTracker.TrackerStatusID = 1;
                                    vEntityChangeTracker.CreatedDate = DateTime.Now;
                                    //vEntityChangeTracker.EntityChangeTrackersQueues.Add(new EntityChangeTrackersQueue() { 
                                    //    //EntityChangeTrackeID = vEntityChangeTracker.EntityChangeTrackerIID,
                                    //    IsReprocess = false,
                                    //    CreatedDate = DateTime.Now
                                    //});
                                    dbContext.EntityChangeTrackers.Add(vEntityChangeTracker);
                                    dbContext.SaveChanges();
                                    var id = vEntityChangeTracker.EntityChangeTrackerIID;

                                    dbContext.EntityChangeTrackersQueues.Add(new EntityChangeTrackersQueue()
                                    {
                                        EntityChangeTrackeID = id,
                                        IsReprocess = false,
                                        CreatedDate = DateTime.Now
                                    });

                                    dbContext.SaveChanges();
                                }
                            }


                        }
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }


        public string CategoryAllList(long productID,int siteID)
        {
            var categoryString = "";
            using (Eduegate.Domain.Entity.Models.dbBlinkContext dbContext = new Eduegate.Domain.Entity.Models.dbBlinkContext())
            {
                var categoryMaster = (from c in dbContext.ProductCategoryMaps
                                      join cc in dbContext.Categories on c.CategoryID equals cc.CategoryIID
                                      join cm in dbContext.MenuLinkCategoryMaps on c.CategoryID equals cm.CategoryID 
                                      where cc.IsActive == true && c.ProductID == productID && cm.SiteID==siteID
                                      select new Domain.Entity.CategoryMaster
                                      {
                                          CategoryID = (int)c.CategoryID
                                         
                                      })
                                         .OrderBy(a => a.CategoryID).ToList();

                if (categoryMaster != null) {
                    foreach (var item in categoryMaster)
                    {
                        categoryString +=   item.CategoryID +  "\\";
                    }
                    if (!string.IsNullOrEmpty(categoryString)) {

                        categoryString = "\\" + categoryString;
                    }
                }
                return categoryString;
            }
        }
    }
}

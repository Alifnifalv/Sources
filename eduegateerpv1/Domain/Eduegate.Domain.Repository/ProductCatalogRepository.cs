using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.CustomEntity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Helper.Enums;
using Microsoft.EntityFrameworkCore;
using Eduegate.Framework.Extensions;
using System.Data;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Domain.Entity.Models.ValueObjects;
using Eduegate.Domain.Entity.Catalogs.Models.ValueObjects;
using Microsoft.Data.SqlClient;

namespace Eduegate.Domain.Repository
{
    public class ProductCatalogRepository 
    {
        public Eduegate.Services.Contracts.Catalog.ProductsDTO GetProductDetails(Eduegate.Services.Contracts.Eduegates.ProductSearchInfoDTO searchInfo)
        {
            //if (searchInfo != null)
            //{
            //    var productsDTO = new Eduegate.Services.Contracts.Eduegates.ProductsDTO();

            //    using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            //    {
            //        if (searchInfo.ProductName != null)
            //        {
            //            productsDTO.ProductCount = dbContext.ProductMasters.Where(x => x.ProductName.Contains(searchInfo.ProductName)).Count();
            //            productsDTO.Products = (from a in (dbContext.ProductMasters.Where(x => x.ProductName.Contains(searchInfo.ProductName)).Select(x => new { x.ProductID, x.ProductName }).OrderBy(x => x.ProductID).Skip((searchInfo.PageDetails.PageNumber - 1) * searchInfo.PageDetails.PageSize).Take(searchInfo.PageDetails.PageSize))
            //                                    select new Eduegate.Services.Contracts.Eduegates.ProductDetailDTO { ProductIID = a.ProductID, ProductName = a.ProductName }).ToList();
            //            return productsDTO;
            //        }

            //        productsDTO.Products = (from a in (dbContext.ProductMasters.Select(x => new { x.ProductID, x.ProductName }).OrderBy(x => x.ProductID).Skip((searchInfo.PageDetails.PageNumber - 1) * searchInfo.PageDetails.PageSize).Take(searchInfo.PageDetails.PageSize))
            //                                select new Eduegate.Services.Contracts.Eduegates.ProductDetailDTO { ProductIID = a.ProductID, ProductName = a.ProductName }).ToList();
            //        return productsDTO;
            //    }
            //}
            return new Eduegate.Services.Contracts.Catalog.ProductsDTO();
        }

        public List<ProductSKUDetail> GetProductListWithSKU(string searchText, int dataSize)
        {
            var productList = new List<ProductSKUDetail>();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (!string.IsNullOrEmpty(searchText))
                {
                    var searchQuery = string.Concat("select top " + dataSize + " * from [catalog].[ProductListBySKU] where SKU like '" + "%" + searchText + "%" + "' or BarCode like'" + "%" + searchText + "%" + "' or PartNo like'" + "%" + searchText + "%" + "'");
                    productList = dbContext.ProductSKUDetails.FromSqlRaw($@"{searchQuery}").AsNoTracking().ToList();
                }
                else
                {
                    var searchQuery = string.Concat("select top " + dataSize + " * from [catalog].[ProductListBySKU]");
                    productList = dbContext.ProductSKUDetails.FromSqlRaw($@"{searchQuery}").AsNoTracking().ToList();
                }
            }

            return productList;
        }

        public List<SearchResult> GetProductByCategory(long CategoryId, long MenuId, decimal? ConvertedPrice, int pageNumber, int pageSize, string sortBy, Eduegate.Services.Contracts.Enums.ProductStatuses productStatus)
        {
            dbEduegateERPContext db = new dbEduegateERPContext();

            // Query to get product list
            List<SearchResult> list = new List<SearchResult>();
            switch (MenuId)
            {
                case (int)ProductListType.Latest:
                    #region Latest
                    // for LATEST
                    var subQry = (from p in db.Products
                                  orderby p.Created.Value.Date
                                  select new
                                  {
                                      Created = p.Created.Value.Date
                                  }).Distinct().OrderByDescending(x => x.Created).Take(4).ToList();

                    var zero = subQry.ElementAt(0);
                    var last = subQry.ElementAt(subQry.Count - 1);

                    var qryProduct = (from p in db.Products
                                      join b in db.Brands on p.BrandID equals b.BrandIID
                                      join psm in db.ProductSKUMaps on p.ProductIID equals psm.ProductID
                                      join pim in db.ProductImageMaps on psm.ProductSKUMapIID equals pim.ProductSKUMapID into psmLJ
                                      from pim in psmLJ.DefaultIfEmpty()

                                      join pplsm in db.ProductPriceListSKUMaps on psm.ProductSKUMapIID equals pplsm.ProductSKUID into one
                                      from pplsm in one.DefaultIfEmpty()

                                      join ppl in db.ProductPriceLists on pplsm.ProductPriceListID equals ppl.ProductPriceListIID into two
                                      from ppl in two.DefaultIfEmpty()

                                      join pi in db.ProductInventories on psm.ProductSKUMapIID equals pi.ProductSKUMapID into productStock
                                      from pi in productStock.DefaultIfEmpty()

                                      where p.Created.Value.Date >= last.Created && p.Created.Value.Date <= zero.Created
                                       && (pim.Sequence == null || pim.Sequence == 1) && (pim.ProductImageTypeID == null || pim.ProductImageTypeID == 39)
                                      select new
                                      {
                                          p.Created,
                                          p.ProductIID,
                                          p.ProductCode,
                                          p.ProductName,
                                          b.BrandIID,
                                          b.BrandName,
                                          psm.ProductPrice,
                                          pim.ImageFile,
                                          psm.ProductSKUCode,
                                          psm.ProductSKUMapIID,
                                          DiscountedPrice = (ppl.Price != null && ((ppl.StartDate != null && ppl.EndDate == null && DateTime.Now >= ppl.StartDate) || (DateTime.Now >= ppl.StartDate && DateTime.Now <= ppl.EndDate) || (ppl.StartDate == null && ppl.EndDate == null))) ? ppl.Price :
                                                       (ppl.PricePercentage != null && ((ppl.StartDate != null && ppl.EndDate == null && DateTime.Now >= ppl.StartDate) || (DateTime.Now >= ppl.StartDate && DateTime.Now <= ppl.EndDate) || (ppl.StartDate == null && ppl.EndDate == null))) ? (decimal?)((decimal?)psm.ProductPrice * (100 - (decimal?)ppl.PricePercentage)) / 100 :
                                                       psm.ProductPrice,
                                          p.StatusID,
                                          HasStock = (pi.Quantity != null && pi.Quantity > 0) ? true : false,
                                      });
                    if (productStatus != Services.Contracts.Enums.ProductStatuses.All)
                    {
                        sbyte statusID = (sbyte)productStatus;
                        qryProduct = qryProduct.Where(x => x.StatusID == statusID);
                    }
                    var toalCount = qryProduct.Count();

                    switch (sortBy)
                    {
                        case "Price low to high":
                            qryProduct = qryProduct.OrderBy(x => x.ProductPrice).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                            break;
                        case "Price high to low":
                            qryProduct = qryProduct.OrderByDescending(x => x.ProductPrice).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                            break;
                        case "Latest":
                        default:
                            qryProduct = qryProduct.OrderByDescending(x => x.Created).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                            break;
                    }

                    qryProduct.ToList().ForEach(x =>
                    {
                        list.Add(new SearchResult
                        {
                            ProductIID = x.ProductIID,
                            SKUID = x.ProductSKUMapIID,
                            ProductCode = x.ProductCode,
                            ProductName = x.ProductName,
                            ProductStatus = Convert.ToSByte(x.StatusID),
                            ProductPrice = x.ProductPrice * ConvertedPrice,
                            //SortOrder = x.SortOrder,
                            ProductSKUCode = x.ProductSKUCode,
                            ImageFile = x.ImageFile,
                            //Sequence = x.Sequence,
                            //CategoryIID = x.CategoryIID,
                            ProductCount = toalCount,
                            BrandIID = x.BrandIID,
                            BrandName = x.BrandName,
                            DiscountedPrice = x.DiscountedPrice * ConvertedPrice,
                            HasStock = x.HasStock
                        });
                    });
                    #endregion
                    break;
                case (long)ProductListType.Sale:
                    #region Sale

                    var qryProductSale = (from pplsm in db.ProductPriceListSKUMaps
                                          join ppl in db.ProductPriceLists on pplsm.ProductPriceListID equals ppl.ProductPriceListIID
                                          join psm in db.ProductSKUMaps on pplsm.ProductSKUID equals psm.ProductSKUMapIID
                                          join p in db.Products on psm.ProductID equals p.ProductIID
                                          join b in db.Brands on p.BrandID equals b.BrandIID
                                          join pim in db.ProductImageMaps on psm.ProductSKUMapIID equals pim.ProductSKUMapID into psmLJ
                                          from pim in psmLJ.DefaultIfEmpty()

                                          join pi in db.ProductInventories on psm.ProductSKUMapIID equals pi.ProductSKUMapID into productStock
                                          from pi in productStock.DefaultIfEmpty()

                                          where (pim.ProductImageTypeID == null || pim.ProductImageTypeID == 39)
                                          && (ppl.PricePercentage != null || ppl.Price != null) && (ppl.StartDate == null || DateTime.Now >= ppl.StartDate)
                                          && (ppl.EndDate == null || DateTime.Now <= ppl.EndDate)
                                          select new
                                          {
                                              p.Created,
                                              p.ProductIID,
                                              p.ProductCode,
                                              p.ProductName,
                                              b.BrandIID,
                                              b.BrandName,
                                              psm.ProductPrice,
                                              pim.ImageFile,
                                              psm.ProductSKUCode,
                                              psm.ProductSKUMapIID,

                                              // if Price and Percentage both in ProductPriceLists table then we will consider Price
                                              DiscountedPrice = (ppl.Price != null && (
                                                (ppl.StartDate != null && ppl.EndDate == null && DateTime.Now >= ppl.StartDate)
                                                || (DateTime.Now >= ppl.StartDate && DateTime.Now <= ppl.EndDate)
                                                || (ppl.StartDate == null && ppl.EndDate == null))) ? ppl.Price :
                                                (ppl.PricePercentage != null && (
                                                (ppl.StartDate != null && ppl.EndDate == null && DateTime.Now >= ppl.StartDate)
                                                || (DateTime.Now >= ppl.StartDate && DateTime.Now <= ppl.EndDate)
                                                || (ppl.StartDate == null && ppl.EndDate == null))) ?
                                                (decimal?)((decimal?)psm.ProductPrice * (100 - (decimal?)ppl.PricePercentage)) / 100 :
                                                psm.ProductPrice,

                                              p.StatusID,
                                              HasStock = (pi.Quantity != null && pi.Quantity > 0) ? true : false,
                                          });

                    if (productStatus != Services.Contracts.Enums.ProductStatuses.All)
                    {
                        sbyte statusID = (sbyte)productStatus;
                        qryProductSale = qryProductSale.Where(x => x.StatusID == statusID);
                    }
                    var toalCountSale = qryProductSale.Count();

                    switch (sortBy)
                    {
                        case "Latest":
                            qryProductSale = qryProductSale.OrderByDescending(x => x.Created).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                            break;
                        case "Price low to high":
                            qryProductSale = qryProductSale.OrderBy(x => x.ProductPrice).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                            break;
                        case "Price high to low":
                            qryProductSale = qryProductSale.OrderByDescending(x => x.ProductPrice).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                            break;
                        default:
                            qryProductSale = qryProductSale.OrderBy(x => x.ProductName).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                            break;
                    }

                    qryProductSale.ToList().ForEach(x =>
                    {
                        list.Add(new SearchResult
                        {
                            ProductIID = x.ProductIID,
                            SKUID = x.ProductSKUMapIID,
                            ProductCode = x.ProductCode,
                            ProductName = x.ProductName,
                            ProductStatus = Convert.ToSByte(x.StatusID),
                            ProductPrice = x.ProductPrice * ConvertedPrice,
                            //SortOrder = x.SortOrder,
                            ProductSKUCode = x.ProductSKUCode,
                            ImageFile = x.ImageFile,
                            //Sequence = x.Sequence,
                            //CategoryIID = x.CategoryIID,
                            DiscountedPrice = x.DiscountedPrice * ConvertedPrice,
                            HasStock = x.HasStock,
                            ProductCount = toalCountSale,
                            BrandIID = x.BrandIID,
                            BrandName = x.BrandName,
                        });
                    });
                    #endregion
                    break;
                case (long)ProductListType.Designer:
                    var qryProductDesigner = (from p in db.Products
                                              join b in db.Brands on p.BrandID equals b.BrandIID
                                              join psm in db.ProductSKUMaps on p.ProductIID equals psm.ProductID
                                              join pim in db.ProductImageMaps on psm.ProductSKUMapIID equals pim.ProductSKUMapID into psmLJ
                                              from pim in psmLJ.DefaultIfEmpty()

                                              join ljpplsm in db.ProductPriceListSKUMaps on psm.ProductSKUMapIID equals ljpplsm.ProductSKUID into ljrpplsm
                                              from pplsm in ljrpplsm.Where(y => y.ProductSKUID == psm.ProductSKUMapIID).DefaultIfEmpty()

                                              join ppl in db.ProductPriceLists on pplsm.ProductPriceListID equals ppl.ProductPriceListIID into ljppl
                                              from ljrppl in ljppl.Where(f => f.ProductPriceListIID == pplsm.ProductPriceListID).DefaultIfEmpty()

                                              join pi in db.ProductInventories on psm.ProductSKUMapIID equals pi.ProductSKUMapID into productStock
                                              from pi in productStock.DefaultIfEmpty()

                                              where b.BrandIID == CategoryId
                                              && (pim.Sequence == null || pim.Sequence == 1) && (pim.ProductImageTypeID == null || pim.ProductImageTypeID == 39)
                                              select new
                                              {
                                                  p.Created,
                                                  p.ProductIID,
                                                  p.ProductCode,
                                                  p.ProductName,
                                                  p.StatusID,
                                                  b.BrandIID,
                                                  b.BrandName,
                                                  psm.ProductPrice,
                                                  pim.ImageFile,
                                                  psm.ProductSKUCode,
                                                  psm.ProductSKUMapIID,
                                                  DiscountedPrice = (ljrppl.Price != null && ((ljrppl.StartDate != null && ljrppl.EndDate == null && DateTime.Now >= ljrppl.StartDate) || (DateTime.Now >= ljrppl.StartDate && DateTime.Now <= ljrppl.EndDate) || (ljrppl.StartDate == null && ljrppl.EndDate == null))) ? ljrppl.Price :
                                                       (ljrppl.PricePercentage != null && ((ljrppl.StartDate != null && ljrppl.EndDate == null && DateTime.Now >= ljrppl.StartDate) || (DateTime.Now >= ljrppl.StartDate && DateTime.Now <= ljrppl.EndDate) || (ljrppl.StartDate == null && ljrppl.EndDate == null))) ? (decimal?)((decimal?)psm.ProductPrice * (100 - (decimal?)ljrppl.PricePercentage)) / 100 :
                                                       psm.ProductPrice,
                                                  HasStock = (pi.Quantity != null && pi.Quantity > 0) ? true : false,
                                                  //psm.Sequence,
                                                  b.Descirption,
                                                  b.LogoFile
                                              });
                    if (productStatus != Services.Contracts.Enums.ProductStatuses.All)
                    {
                        sbyte statusID = (sbyte)productStatus;
                        qryProductDesigner = qryProductDesigner.Where(x => x.StatusID == statusID);
                    }
                    var toalCountDesigner = qryProductDesigner.Count();

                    switch (sortBy)
                    {
                        case "Latest":
                            qryProductDesigner = qryProductDesigner.OrderByDescending(x => x.Created).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                            break;
                        case "Price low to high":
                            qryProductDesigner = qryProductDesigner.OrderBy(x => x.ProductPrice).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                            break;
                        case "Price high to low":
                            qryProductDesigner = qryProductDesigner.OrderByDescending(x => x.ProductPrice).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                            break;
                        default:
                            qryProductDesigner = qryProductDesigner.OrderBy(x => x.ProductName).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                            break;
                    }

                    qryProductDesigner.ToList().ForEach(x =>
                    {
                        list.Add(new SearchResult
                        {
                            ProductIID = x.ProductIID,
                            SKUID = x.ProductSKUMapIID,
                            ProductCode = x.ProductCode,
                            ProductName = x.ProductName,
                            ProductPrice = x.ProductPrice * ConvertedPrice,
                            ProductStatus = Convert.ToSByte(x.StatusID),
                            //SortOrder = x.SortOrder,
                            ProductSKUCode = x.ProductSKUCode,
                            ImageFile = x.ImageFile,
                            //Sequence = x.Sequence,
                            ProductCount = toalCountDesigner,
                            BrandIID = x.BrandIID,
                            BrandName = x.BrandName,
                            Descirption = x.Descirption,
                            LogoFile = x.LogoFile,
                            DiscountedPrice = Convert.ToDecimal(x.DiscountedPrice * ConvertedPrice),
                            HasStock = x.HasStock,
                        });
                    });
                    break;
                default:
                    var qryProductDefault = (from p in db.Products
                                             join b in db.Brands on p.BrandID equals b.BrandIID
                                             join psm in db.ProductSKUMaps on p.ProductIID equals psm.ProductID
                                             join pim in db.ProductImageMaps on psm.ProductSKUMapIID equals pim.ProductSKUMapID into psmLJ
                                             from pim in psmLJ.DefaultIfEmpty()

                                             join pplsm in db.ProductPriceListSKUMaps on psm.ProductSKUMapIID equals pplsm.ProductSKUID into one
                                             from pplsm in one.DefaultIfEmpty()

                                             join ppl in db.ProductPriceLists on pplsm.ProductPriceListID equals ppl.ProductPriceListIID into two
                                             from ppl in two.DefaultIfEmpty()

                                             join pcm in db.ProductCategoryMaps on p.ProductIID equals pcm.ProductID
                                             join c in db.Categories on pcm.CategoryID equals c.CategoryIID

                                             join pi in db.ProductInventories on psm.ProductSKUMapIID equals pi.ProductSKUMapID into productStock
                                             from pi in productStock.DefaultIfEmpty()

                                             where (pcm.CategoryID == CategoryId || c.ParentCategoryID == CategoryId)
                                             && (pim.Sequence == null || pim.Sequence == 1) && (pim.ProductImageTypeID == null || pim.ProductImageTypeID == 39)
                                             // && pplsm.SortOrder == 1 
                                             select new
                                             {
                                                 p.Created,
                                                 p.ProductIID,
                                                 p.ProductCode,
                                                 p.ProductName,
                                                 p.StatusID,
                                                 psm.ProductPrice,
                                                 pplsm.SortOrder,
                                                 psm.ProductSKUCode,
                                                 pim.ImageFile,
                                                 pim.Sequence,
                                                 c.CategoryIID,
                                                 b.BrandIID,
                                                 b.BrandName,
                                                 psm.ProductSKUMapIID,
                                                 DiscountedPrice = (ppl.Price != null && ((ppl.StartDate != null && ppl.EndDate == null && DateTime.Now >= ppl.StartDate) || (DateTime.Now >= ppl.StartDate && DateTime.Now <= ppl.EndDate) || (ppl.StartDate == null && ppl.EndDate == null))) ? ppl.Price :
                                                       (ppl.PricePercentage != null && ((ppl.StartDate != null && ppl.EndDate == null && DateTime.Now >= ppl.StartDate) || (DateTime.Now >= ppl.StartDate && DateTime.Now <= ppl.EndDate) || (ppl.StartDate == null && ppl.EndDate == null))) ? (decimal?)((decimal?)psm.ProductPrice * (100 - (decimal?)ppl.PricePercentage)) / 100 :
                                                       psm.ProductPrice,
                                                 HasStock = (pi.Quantity != null && pi.Quantity > 0) ? true : false,

                                             }).AsNoTracking();
                    if (productStatus != Services.Contracts.Enums.ProductStatuses.All)
                    {
                        sbyte statusID = (sbyte)productStatus;
                        qryProductDefault = qryProductDefault.Where(x => x.StatusID == statusID);
                    }
                    var toalCountDefault = qryProductDefault.Count();

                    switch (sortBy)
                    {
                        case "Latest":
                            qryProductDefault = qryProductDefault.OrderByDescending(x => x.Created).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                            break;
                        case "Price low to high":
                            qryProductDefault = qryProductDefault.OrderBy(x => x.ProductPrice).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                            break;
                        case "Price high to low":
                            qryProductDefault = qryProductDefault.OrderByDescending(x => x.ProductPrice).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                            break;
                        default:
                            qryProductDefault = qryProductDefault.OrderBy(x => x.ProductName).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                            break;
                    }


                    qryProductDefault.ToList().ForEach(x =>
                    {
                        list.Add(new SearchResult
                        {
                            ProductIID = x.ProductIID,
                            SKUID = x.ProductSKUMapIID,
                            ProductCode = x.ProductCode,
                            ProductName = x.ProductName,
                            ProductPrice = x.ProductPrice * ConvertedPrice,
                            ProductStatus = Convert.ToSByte(x.StatusID),
                            SortOrder = x.SortOrder,
                            ProductSKUCode = x.ProductSKUCode,
                            ImageFile = x.ImageFile,
                            Sequence = x.Sequence,
                            CategoryIID = x.CategoryIID,
                            ProductCount = toalCountDefault,
                            BrandIID = x.BrandIID,
                            BrandName = x.BrandName,
                            DiscountedPrice = x.DiscountedPrice * ConvertedPrice,
                            HasStock = x.HasStock
                        });
                    });

                    break;
            }

            return list;
        }

        public long CreateProduct(Product product)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    dbContext.Products.Add(product);
                    dbContext.SaveChanges();
                }
            }

            catch (Exception)
            {
                throw;
            }
            return product.ProductIID;
        }

        public List<SearchResult> GetProductsBySearchCriteria(string searchText, int pageNumber, int pageSize, string sortBy)
        {
            var productList = new List<SearchResult>();
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                searchText = searchText.Replace("|", "''");
                var searchQuery = string.Concat("select * from [catalog].[ProductFullTextSearchView] where contains(*, '\"", searchText, "*\"')");
                productList = dbContext.SearchResults.FromSqlRaw($@"{searchQuery}").AsNoTracking().ToList().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            return productList;
        }

        public Category GetCategoryTags(long categoryID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var entity = dbContext.Categories.Where(x => x.CategoryIID == categoryID)
                    .Include(i => i.CategoryImageMaps)
                    .AsNoTracking()
                    .FirstOrDefault();
                dbContext.Entry(entity).Collection(a => a.CategoryImageMaps).Load();
                return entity;
            }
        }

        public Category GetCategory(long categoryID, int companyID = 0)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var entity = dbContext.Categories.Where(x => x.CategoryIID == categoryID)
                    .Include(a => a.CategoryImageMaps)
                    .Include(a => a.CategoryTagMaps).ThenInclude(i => i.CategoryTag)
                    .Include(a => a.CategoryCultureDatas)
                    .Include(a => a.CategorySettings)
                    .AsNoTracking()
                    //.Include(a => a.Class)
                    .FirstOrDefault();

                if (companyID != 0)
                    entity.CategoryImageMaps = entity.CategoryImageMaps.Where(a => a.CompanyID == companyID).ToList();

                //foreach (var tags in entity.CategoryTagMaps)
                //{
                //    dbContext.Entry(tags).Reference(a => a.CategoryTag).Load();
                //}

                return entity;
            }
        }

        public Category SaveCategory(Category entity, int companyID, List<long> childCategories = null, long categoryMenuID = 0)
        {
            Category updatedEntity = null;
            bool isNewCategory = false;
            try
            {
                using (var dbContext = new dbEduegateERPContext())
                {
                    dbContext.Categories.Add(entity);

                    if (entity.CategoryIID == 0)
                    {
                        dbContext.Entry(entity).State = EntityState.Added;
                        isNewCategory = true;
                    }
                    else
                    {
                        dbContext.Entry(entity).State = EntityState.Modified;
                        dbContext.Entry(entity).Property(x => x.CategoryCode).IsModified = false;
                        dbContext.CategoryImageMaps.RemoveRange(dbContext.CategoryImageMaps
                            .Where(a => a.CategoryID == entity.CategoryIID && a.CompanyID == companyID &&
                             !entity.CategoryImageMaps.Select(x => x.CategoryImageMapIID).Contains(a.CategoryImageMapIID)));

                        dbContext.CategoryCultureDatas.RemoveRange(
                            dbContext.CategoryCultureDatas.Where(a => a.CategoryID == entity.CategoryIID &&
                             !entity.CategoryCultureDatas.Select(x => x.CategoryID.ToString() + "_" + x.CultureID.ToString())
                                .Contains(a.CategoryID.ToString() + "_" + a.CultureID.ToString())));
                    }

                    foreach (var imageMap in entity.CategoryImageMaps)
                    {
                        if (imageMap.CategoryImageMapIID == 0)
                        {
                            dbContext.Entry(imageMap).State = EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(imageMap).State = EntityState.Modified;
                        }
                    }

                    foreach (var tagMap in entity.CategoryTagMaps)
                    {
                        dbContext.Entry(tagMap).State = EntityState.Added;

                        if (tagMap.CategoryTagID == null || tagMap.CategoryTagID == 0)
                        {
                            dbContext.Entry(tagMap.CategoryTag).State = EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(tagMap.CategoryTag).State = EntityState.Unchanged;
                        }
                    }

                    dbContext.CategoryTagMaps.RemoveRange(dbContext.CategoryTagMaps.Where(a => a.CategoryID == entity.CategoryIID));

                    foreach (var cultureData in entity.CategoryCultureDatas)
                    {
                        if (dbContext.CategoryCultureDatas.Any(x => x.CultureID == cultureData.CultureID &&
                             x.CategoryID == cultureData.CategoryID))
                        {
                            dbContext.Entry(cultureData).State = EntityState.Modified;
                        }
                        else
                        {
                            dbContext.Entry(cultureData).State = EntityState.Added;
                        }
                    }

                    var categorySettingsListIDS = entity.CategorySettings.Select(x => x.CategorySettingsID).ToList();
                    var categorySettingsList = dbContext.CategorySettings.Where(x => x.CategoryID == entity.CategoryIID && !categorySettingsListIDS.Contains(x.CategorySettingsID)).ToList();

                    if (categorySettingsList.IsNotNull())
                    {
                        dbContext.CategorySettings.RemoveRange(categorySettingsList);
                    }

                    foreach (var settingMap in entity.CategorySettings)
                    {
                        if (settingMap.SettingValue != null)
                        {
                            if (settingMap.CategorySettingsID > 0) { dbContext.Entry(settingMap).State = EntityState.Modified; }
                            else { dbContext.Entry(settingMap).State = EntityState.Added; }
                        }
                    }

                    if (entity.CategoryIID != 0)
                    {
                        //update parent
                        var updateExistingChilds = dbContext.Categories.Where(x => childCategories.Contains(x.CategoryIID)).ToList();
                        if (updateExistingChilds != null)
                        {
                            foreach (var childEntity in updateExistingChilds)
                            {
                                childEntity.ParentCategoryID = entity.CategoryIID;
                            }
                        }

                        //update parent
                        var clearExistingChilds = dbContext.Categories
                            .Where(x => !childCategories.Contains(x.CategoryIID) && x.ParentCategoryID == entity.CategoryIID).ToList();
                        if (clearExistingChilds != null)
                        {
                            foreach (var childEntity in clearExistingChilds)
                            {
                                childEntity.ParentCategoryID = null;
                            }
                        }
                    }

                    //if ((entity.IsInNavigationMenu.HasValue && entity.IsInNavigationMenu.Value) ||
                    //    (entity.IsShowWebsite.HasValue && entity.IsShowWebsite.Value))
                    //{
                    //    if (!dbContext.MenuLinks.Any(x => x.MenuLinkIID == categoryMenuID))
                    //    {
                    //        dbContext.MenuLinks.Add(new MenuLink()
                    //        {
                    //            MenuLinkIID = categoryMenuID,
                    //            MenuLinkTypeID = 1,
                    //            MenuName = "Categories"
                    //        });
                    //    }

                    //    dbContext.MenuLinkCategoryMaps.RemoveRange(dbContext.MenuLinkCategoryMaps.Where(a => a.CategoryID == entity.CategoryIID));
                    //    entity.MenuLinkCategoryMaps.Add(new MenuLinkCategoryMap()
                    //    {
                    //        CategoryID = entity.CategoryIID,
                    //        MenuLinkID = categoryMenuID
                    //    });
                    //}

                    dbContext.SaveChanges();

                    if (isNewCategory)
                    {
                        var updateExistingChilds = dbContext.Categories.Where(x => childCategories.Contains(x.CategoryIID)).ToList();
                        if (updateExistingChilds != null)
                        {
                            foreach (var childEntity in updateExistingChilds)
                            {
                                childEntity.ParentCategoryID = entity.CategoryIID;
                            }
                        }
                    }

                    updatedEntity = GetCategory(entity.CategoryIID, companyID);
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductCatalogRepository>.Fatal(exception.Message.ToString(), exception);
                throw;
            }

            return updatedEntity;
        }


        public ProductSKUDetail GetProductAndSKUByID(long productSKUMapID)
        {
            var productList = new ProductSKUDetail();
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (productSKUMapID > 0)
                {
                    string searchQuery = "select * from catalog.ProductListBySKU where ProductSKUMapIID=" + productSKUMapID;
                    productList = dbContext.ProductSKUDetails.FromSqlRaw($@"{searchQuery}").AsNoTracking().FirstOrDefault();
                }
            }
            return productList;
        }


        public List<ProductPriceListType> GetProductPriceListTypes()
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.ProductPriceListTypes.AsNoTracking().ToList();
            }
        }

        public List<ProductPriceListLevel> GetProductPriceListLevels()
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.ProductPriceListLevels.AsNoTracking().ToList();
            }
        }

        public List<Product> GetProductsByCategory(long categoryID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var list = from pro in dbContext.Products
                           join pcm in dbContext.ProductCategoryMaps on pro.ProductIID equals pcm.ProductID
                           where pcm.CategoryID == categoryID
                           select pro;
                return list.ToList();
            }
        }

        public List<ProductSKUMap> GetProductSKUDetailsByCategory(long categoryID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {

                var list = (from psm in dbContext.ProductSKUMaps
                           join pcm in dbContext.ProductCategoryMaps on psm.ProductID equals pcm.ProductID
                           where pcm.CategoryID == categoryID
                        && pcm.ProductID != null && psm.ProductID != null // condition applied due to garbage data in skumaps
                           select psm).AsNoTracking().ToList();

                return list;
            }
        }

        public List<Product> GetProductsByBrand(long brandID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var list = (from pro in dbContext.Products
                           where pro.BrandID == brandID
                           select pro).AsNoTracking().ToList();
                return list;
            }
        }

        public List<ProductSKUMap> GetProductSKUDetailsByBrand(long brandID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var list = (from psm in dbContext.ProductSKUMaps
                           join pro in dbContext.Products on psm.ProductID equals pro.ProductIID
                           where pro.BrandID == brandID
                        && psm.ProductID != null
                           select psm).AsNoTracking().ToList();

                return list;
            }
        }

        public List<ProductImageMap> GetProductDetailImages(long skuID)
        {
            var productImageMapList = new List<ProductImageMap>();

            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                productImageMapList = db.ProductImageMaps.OrderBy(a => a.Sequence).Where(a => a.ProductSKUMapID == skuID && a.ProductImageTypeID == 3).AsNoTracking().ToList();
            }
            return productImageMapList;
        }

        public List<ProductPropertyMap> GetProductKeyFeatures(long skuID, int cultureID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                try
                {

                    var productPropertyMaps = db.ProductPropertyMaps.Where(a => a.ProductSKUMapID == skuID && (a.PropertyID != null || (a.PropertyID == null && !string.IsNullOrEmpty(a.Value))))
                        .Include(i => i.Property)
                        .Include(i => i.PropertyType)
                        .AsNoTracking()
                        .ToList();
                    //db.Entry(productPropertyMaps).Reference(a => a.Property).Load();
                    //foreach (var productPropertyMap in productPropertyMaps)
                    //{
                    //    db.Entry(productPropertyMap).Reference(a => a.Property).Load();
                    //    //db.Entry(productPropertyMap.Property).Collection(a=> a.)
                    //    db.Entry(productPropertyMap).Reference(a => a.PropertyType).Load();
                    //}
                    if (cultureID != 1)
                    {
                        foreach (ProductPropertyMap propertyMap in productPropertyMaps)
                        {
                            var propertyTypeCultureData = from proprtyTypeCulture in db.PropertyTypeCultureDatas
                                                          join propertyType in db.PropertyTypes on proprtyTypeCulture.PropertyTypeID equals propertyType.PropertyTypeID
                                                          join cultures in db.Cultures on proprtyTypeCulture.CultureID equals cultures.CultureID
                                                          where proprtyTypeCulture.PropertyTypeID == propertyMap.PropertyType.PropertyTypeID
                                                          && cultures.CultureID== cultureID
                                                          select proprtyTypeCulture.PropertyTypeName;
                            if (propertyTypeCultureData.Any())
                            {
                                propertyMap.PropertyType.PropertyTypeName = propertyTypeCultureData.FirstOrDefault();
                            }
                        }
                    }
                    return productPropertyMaps;

                }
                catch (Exception ex)
                {
                    return new List<ProductPropertyMap>();
                }

            }
        }

        public List<ProductSKuVariantsCode> GetProductVariants(long skuID, long cultureID, string cultureCode)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var productID = db.ProductSKUMaps.Where(a => a.ProductSKUMapIID == skuID).Select(b => b.ProductID).FirstOrDefault();
                var productSKUVariantList = (from psv in db.ProductSKUVariants
                                             join sku in db.ProductSKUMaps on psv.ProductSKUMapIID equals sku.ProductSKUMapIID
                                             join c in db.Cultures on psv.CultureID equals c.CultureID
                                             where sku.StatusID == 2 && psv.ProductIID == productID && c.CultureCode == cultureCode
                                             select new ProductSKuVariantsCode
                                             {
                                                 ProductSKUMapIID = psv.ProductSKUMapIID,
                                                 ProductCode = sku.ProductSKUCode,
                                                 PropertyTypeName = psv.PropertyTypeName,
                                                 PropertyName = psv.PropertyName,
                                                 ProductIID = psv.ProductIID,
                                                 CultureID = psv.CultureID
                                             }).AsNoTracking().ToList();

                return productSKUVariantList;
            }
        }

        public ProductInventoryConfig GetProductDetailsDescription(long skuID, long cultureID, string cultureCode)
        {
            var productInventoryConfig = new ProductInventoryConfig();
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var exists = db.ProductInventorySKUConfigMaps.Any(a => a.ProductSKUMapID == skuID);
                if (exists)
                {
                    productInventoryConfig = (from p in db.ProductInventoryConfigs
                                              join b in db.ProductInventorySKUConfigMaps on p.ProductInventoryConfigIID equals b.ProductInventoryConfigID
                                              where b.ProductSKUMapID == skuID
                                              select p).Include(x => x.ProductInventoryConfigCultureDatas).FirstOrDefault();
                }
                else
                {
                    var productID = db.ProductSKUMaps.Where(a => a.ProductSKUMapIID == skuID).AsNoTracking().Select(b => b.ProductID).FirstOrDefault();
                    productInventoryConfig = (from p in db.ProductInventoryConfigs
                                              join b in db.ProductInventoryProductConfigMaps on p.ProductInventoryConfigIID equals b.ProductInventoryConfigID
                                              where b.ProductID == productID
                                              select p).Include(x => x.ProductInventoryConfigCultureDatas).AsNoTracking().FirstOrDefault();
                }
            }
            return productInventoryConfig;
        }

        public ProductInventoryConfigCultureData GetProductDescriptionCultureID(long skuID, long cultureID)
        {
            var cultureConfig = new ProductInventoryConfigCultureData();
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var exists = db.ProductInventorySKUConfigMaps.Any(a => a.ProductSKUMapID == skuID);
                if (exists)
                {
                    var config = (from p in db.ProductInventoryConfigs
                                  join b in db.ProductInventorySKUConfigMaps on p.ProductInventoryConfigIID equals b.ProductInventoryConfigID
                                  where b.ProductSKUMapID == skuID
                                  select p).AsNoTracking().FirstOrDefault();

                    if (db.ProductInventoryConfigCultureDatas.Where(a => a.ProductInventoryConfigID == config.ProductInventoryConfigIID).AsNoTracking().Any())
                    {
                        cultureConfig = db.ProductInventoryConfigCultureDatas.Where(a => a.ProductInventoryConfigID == config.ProductInventoryConfigIID && a.CultureID == cultureID).AsNoTracking().FirstOrDefault();
                    }
                }
            }
            return cultureConfig;
        }

        #region product sku and quantity view

        public DataTable ProductSKUSearch(int pageIndex, int pageSize, string searchText, string searchVal,
            string searchBy, string sortBy, string pageType, bool isCategory, out int totalRecords)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetDefaultConnectionString()))
                using (SqlCommand cmd = new SqlCommand("catalog.spcProductQuickSearchWithPagination", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@searchtext", SqlDbType.Text));
                    adapter.SelectCommand.Parameters["@searchtext"].Value = searchText;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@pageIndex", SqlDbType.Int));
                    adapter.SelectCommand.Parameters["@pageIndex"].Value = pageIndex;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@pageSize", SqlDbType.Int));
                    adapter.SelectCommand.Parameters["@pageSize"].Value = pageSize;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@searchBy", SqlDbType.Int));
                    adapter.SelectCommand.Parameters["@searchBy"].Value = pageIndex;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@sortBy", SqlDbType.Int));
                    adapter.SelectCommand.Parameters["@sortBy"].Value = pageIndex;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@pageType", SqlDbType.Int));
                    adapter.SelectCommand.Parameters["@pageType"].Value = pageIndex;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@isCategory", SqlDbType.Int));
                    adapter.SelectCommand.Parameters["@isCategory"].Value = pageIndex;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@searchVal", SqlDbType.Int));
                    adapter.SelectCommand.Parameters["@searchVal"].Value = pageIndex;

                    DataSet dt = new DataSet();
                    adapter.Fill(dt);

                    if (dt.Tables.Count > 0)
                    {
                        totalRecords = int.Parse(dt.Tables[1].Rows[0][0].ToString());
                        return dt.Tables[0];
                    }
                }

                totalRecords = 0;
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ProductSKUSearch(string searchText, int dataSize, long? productTypeID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetDefaultConnectionString()))
                using (SqlCommand cmd = new SqlCommand("catalog.spcProductQuickSearch", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@searchtext", SqlDbType.Text));
                    adapter.SelectCommand.Parameters["@searchtext"].Value = searchText;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@topcount", SqlDbType.Int));
                    adapter.SelectCommand.Parameters["@topcount"].Value = dataSize;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@productTypeID", SqlDbType.BigInt));
                    adapter.SelectCommand.Parameters["@productTypeID"].Value = productTypeID;

                    DataSet dt = new DataSet();
                    adapter.Fill(dt);

                    if (dt.Tables.Count > 0)
                    {
                        return dt.Tables[0];
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProductSKUMap> ProductSKUByCategoryID(long categoryID, int dataSize)
        {
            var productList = new List<ProductSKUMap>();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                productList = (from p in dbContext.Products
                               join s in dbContext.ProductSKUMaps on p.ProductIID equals s.ProductID
                               join b in dbContext.ProductCategoryMaps on p.ProductIID equals b.ProductID
                               where b.CategoryID == categoryID
                               select s).AsNoTracking().ToList();
             
                return productList;
            }
        }

        public ProductSKUDetail GetProductSKUDetail(long skuIID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var searchQuery = string.Concat("select * from [catalog].[ProductSKUDetailView] where ProductSKUMapIID =", skuIID);
                return dbContext.ProductSKUDetails.FromSqlRaw($@"{searchQuery}").AsNoTracking().FirstOrDefault();
            }
        }

        public ProductSKUDetail GetProductSKUInventoryDetail(int companyID, long skuIID,long branchID = 0)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var searchQuery = branchID == 0 ? string.Concat("select * from [catalog].[ProductSKUQuantityView] where ProductSKUMapIID =", 
                    skuIID  , " and CompanyID = ", companyID.ToString())
                    : string.Concat("select * from [catalog].[ProductSKUQuantityView] where ProductSKUMapIID =" , skuIID , " AND BranchID=" , branchID.ToString() ,
                    " and CompanyID = " , companyID.ToString());
                return dbContext.ProductSKUDetails.FromSqlRaw($@"{searchQuery}").AsNoTracking().FirstOrDefault();
            }
        }

        public decimal GetCostPrice(long skuIID, DocumentReferenceTypes referenceType, long branchID)
        {
            decimal? costPrice = null;
            int? referenceTypeID = (int)referenceType;
            var transactionCompleted = (int)Services.Contracts.Enums.TransactionStatus.Complete;
            var documentStatusCompleted = (int)Services.Contracts.Enums.DocumentStatuses.Completed;
            bool fromTransaction = false;

            switch (referenceType)
            {

                case DocumentReferenceTypes.SalesOrder:
                    fromTransaction = false;
                    break;
                case DocumentReferenceTypes.PurchaseInvoice:
                    referenceTypeID = (int)DocumentReferenceTypes.PurchaseInvoice;
                    fromTransaction = true;
                    break;
                case DocumentReferenceTypes.SalesReturn:
                    break;
                case DocumentReferenceTypes.PurchaseReturn:
                    break;
                case DocumentReferenceTypes.SalesInvoice:
                    fromTransaction = false;
                    break;
                case DocumentReferenceTypes.PurchaseOrder:
                    // Get price from PI
                    referenceTypeID = (int)DocumentReferenceTypes.PurchaseInvoice;
                    fromTransaction = true;
                    break;
                case DocumentReferenceTypes.SalesReturnRequest:
                    break;
                case DocumentReferenceTypes.PurchaseReturnRequest:
                    break;
            }

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (fromTransaction)
                {
                    var transactions = (from th in dbContext.TransactionHeads
                                        join td in dbContext.TransactionDetails on th.HeadIID equals td.HeadID
                                        join dt in dbContext.DocumentTypes on th.DocumentTypeID equals dt.DocumentTypeID
                                        where dt.ReferenceTypeID == referenceTypeID && td.ProductSKUMapID == skuIID
                                        && th.BranchID == branchID && th.TransactionStatusID == transactionCompleted
                                        && th.DocumentStatusID == documentStatusCompleted
                                        select td).AsNoTracking().ToList();


                    if (transactions.IsNotNull() && transactions.Count() > 0)
                    {
                        // Get latest price
                        var transactionUnitPrice = transactions.OrderByDescending(x => x.DetailIID).FirstOrDefault().UnitPrice;
                        costPrice = transactionUnitPrice;
                    }
                    else
                    {
                        costPrice = (from ppsm in dbContext.ProductPriceListSKUMaps
                                     join ppbm in dbContext.ProductPriceListBranchMaps on ppsm.ProductPriceListID equals ppbm.ProductPriceListID
                                     where ppsm.ProductSKUID == skuIID && ppbm.ProductPriceListID == ppsm.ProductPriceListID
                                     select ppsm.Cost
                                     ).FirstOrDefault();

                        //costPrice = dbContext.ProductPriceListSKUMaps.Where(x => x.ProductSKUID == skuIID).Select(y => y.Cost).Average();

                        if (costPrice.IsNull())
                            costPrice = 0;
                    }
                }
                else
                {
                    costPrice = (from ppsm in dbContext.ProductPriceListSKUMaps
                                 join ppbm in dbContext.ProductPriceListBranchMaps on ppsm.ProductPriceListID equals ppbm.ProductPriceListID
                                 where ppsm.ProductSKUID == skuIID && ppbm.ProductPriceListID == ppsm.ProductPriceListID
                                 && ppbm.BranchID == branchID // consider branch?
                                 select ppsm.Cost
                                     ).FirstOrDefault();


                    if (costPrice.IsNull())
                        costPrice = 0;
                }
            }

            return (decimal)costPrice;
        }

        public decimal GetUnitPrice(long skuIID, DocumentReferenceTypes referenceType, long branchID)
        {
            decimal? unitPrice = null;
            int? referenceTypeID = (int)referenceType;
            
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                unitPrice = (from ppsm in dbContext.ProductPriceListSKUMaps
                                 where (ppsm.ProductSKUID == skuIID && ppsm.ProductPriceListID == 1)
                                 select ppsm.Price)
                                 .FirstOrDefault();


                    if (unitPrice.IsNull())
                        unitPrice = 0;
            }

            return (decimal)unitPrice;
        }

        public ProductDTO GetUnitIDs(long skuIID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var datasDTO = new ProductDTO();

                var prdct = dbContext.Products.Where(x => x.ProductSKUMaps.Select(s => s.ProductSKUMapIID == skuIID).FirstOrDefault()).AsNoTracking().FirstOrDefault();

                datasDTO = new ProductDTO()
                {
                    ProductID = prdct.ProductIID,
                    PurchaseUnitID = prdct.PurchaseUnitID,
                    SellingUnitID = prdct.SellingUnitID,
                    PurchaseUnitGroupID=prdct.PurchaseUnitGroupID,
                    SellingUnitGroupID=prdct.SellingUnitGroupID,
                    ProductCode= prdct.ProductCode
                };

                return datasDTO;
            }
        }

        public Location GetLocationDetail(long locationID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Locations.Where(l => l.LocationIID == locationID).AsNoTracking().FirstOrDefault();
            }
        }

        public Location GetLocationDetailByBarCode(string barcode)
        {
            var location = new Location();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                location = dbContext.Locations.Where(l => l.Barcode == barcode).AsNoTracking().FirstOrDefault();
            }

            return location;
        }

        public Location GetProductSKULocation(long skuID)
        {
            var location = new Location();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var productLocationMap = dbContext.ProductLocationMaps.Where(l => l.ProductSKUMapID == skuID).AsNoTracking().FirstOrDefault();

                if (productLocationMap.IsNotNull())
                    location = productLocationMap.Location;
            }

            return location;
        }

        public ProductLocationMap GetProductLocationMap(long locationID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.ProductLocationMaps.Where(l => l.LocationID == locationID).AsNoTracking().FirstOrDefault();
            }
        }

        public ProductSKUDetail GetProductBySKU(long skuID, long cultureID, string cultureCode, int companyID)
        {
            var productSKUDetail = new ProductSKUDetail();
            decimal exchangeRate = UtilityRepository.GetExchangeRate(companyID, cultureCode);
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if (cultureCode == "en")
                {
                    productSKUDetail = (from p in db.Products
                                        join b in db.Brands on p.BrandID equals b.BrandIID into aa
                                        from b in aa.DefaultIfEmpty()

                                        join psm in db.ProductSKUMaps on p.ProductIID equals psm.ProductID

                                        join pplsm in db.ProductPriceListSKUMaps on psm.ProductSKUMapIID equals pplsm.ProductSKUID into one
                                        from pplsm in one.DefaultIfEmpty()

                                        join ppl in db.ProductPriceLists on pplsm.ProductPriceListID equals ppl.ProductPriceListIID into two
                                        from ppl in two.DefaultIfEmpty()
                                        where psm.ProductSKUMapIID == skuID
                                        //&& b.StatusID == (int)Eduegate.Services.Contracts.Enums.BrandStatuses.Active
                                        select new ProductSKUDetail()
                                        {
                                            ProductIID = (int)psm.ProductID,
                                            PartNo = psm.PartNo,
                                            ProductName = psm.SKUName,
                                            ProductPrice = psm.ProductPrice * exchangeRate,
                                            ProductSKUMapIID = psm.ProductSKUMapIID,
                                            SKU = psm.SKUName,
                                            ProductSKUCode = psm.ProductSKUCode,
                                            BrandCode = b.BrandCode,
                                            BrandName = b.BrandName,
                                            Weight = p.Weight,
                                            Calorie = p.Calorie

                                        }).AsNoTracking().FirstOrDefault();
                }
                else
                {
                    productSKUDetail = (from p in db.Products
                                        join b in db.Brands on p.BrandID equals b.BrandIID
                                        join psm in db.ProductSKUMaps on p.ProductIID equals psm.ProductID

                                        join pplsm in db.ProductPriceListSKUMaps on psm.ProductSKUMapIID equals pplsm.ProductSKUID into one
                                        from pplsm in one.DefaultIfEmpty()

                                        join ppl in db.ProductPriceLists on pplsm.ProductPriceListID equals ppl.ProductPriceListIID into two
                                        from ppl in two.DefaultIfEmpty()

                                        join pculture in db.ProductSKUCultureDatas on psm.ProductSKUMapIID equals pculture.ProductSKUMapID into three
                                        from pculture in three.Where(f => f.CultureID == cultureID).DefaultIfEmpty()
                                        //join culture in db.Cultures on pculture.CultureID equals culture.CultureID

                                        where psm.ProductSKUMapIID == skuID && b.StatusID == (int)Eduegate.Services.Contracts.Enums.BrandStatuses.Active
                                        //&& culture.CultureCode == cultureCode
                                        select new ProductSKUDetail()
                                        {
                                            ProductIID = (int)psm.ProductID,
                                            PartNo = psm.PartNo,
                                            ProductName = pculture.ProductSKUName,
                                            ProductPrice = psm.ProductPrice * exchangeRate,
                                            ProductSKUMapIID = psm.ProductSKUMapIID,
                                            SKU = pculture.ProductSKUName,
                                            ProductSKUCode = psm.ProductSKUCode,
                                            BrandCode = b.BrandCode,
                                            BrandName = b.BrandName,
                                            Weight = p.Weight,
                                            Calorie = p.Calorie

                                        }).AsNoTracking().FirstOrDefault();
                }

            }
            return productSKUDetail;
        }

        public decimal GetProductPrice(long skuID)
        {
            decimal productPrice = 0;
            var productMaster = new ProductDTO();
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                productPrice = (from p in db.Products

                                join psm in db.ProductSKUMaps on p.ProductIID equals psm.ProductID

                                join pplsm in db.ProductPriceListSKUMaps on psm.ProductSKUMapIID equals pplsm.ProductSKUID into one
                                from pplsm in one.DefaultIfEmpty()

                                join ppl in db.ProductPriceLists on pplsm.ProductPriceListID equals ppl.ProductPriceListIID into two
                                from ppl in two.DefaultIfEmpty()
                                where psm.ProductSKUMapIID == skuID
                                select (decimal)((ppl.Price != null && ((ppl.StartDate != null && ppl.EndDate == null && DateTime.Now >= ppl.StartDate) ||
                                                       (DateTime.Now >= ppl.StartDate && DateTime.Now <= ppl.EndDate) || (ppl.StartDate == null && ppl.EndDate == null))) ?
                                                       ppl.Price :
                                                       (ppl.PricePercentage != null && ((ppl.StartDate != null && ppl.EndDate == null && DateTime.Now >= ppl.StartDate) ||
                                                       (DateTime.Now >= ppl.StartDate && DateTime.Now <= ppl.EndDate) || (ppl.StartDate == null && ppl.EndDate == null))) ?
                                                       ((decimal?)((decimal?)psm.ProductPrice * (100 - (decimal?)ppl.PricePercentage)) / 100) :
                                                       psm.ProductPrice)).FirstOrDefault();

            }
            return productPrice;
        }

        public decimal GetProductSellingPrice(long skuID)
        {
            decimal productPrice = 0;
            var productMaster = new ProductDTO();
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                productPrice = (from p in db.Products

                                join psm in db.ProductSKUMaps on p.ProductIID equals psm.ProductID

                                join pplsm in db.ProductPriceListSKUMaps on psm.ProductSKUMapIID equals pplsm.ProductSKUID into one
                                from pplsm in one.DefaultIfEmpty()

                                join ppl in db.ProductPriceLists on pplsm.ProductPriceListID equals ppl.ProductPriceListIID into two
                                from ppl in two.DefaultIfEmpty()
                                where psm.ProductSKUMapIID == skuID
                                select (decimal)((pplsm.Discount != null && pplsm.Discount > 0 && ((ppl.StartDate != null && ppl.EndDate == null && DateTime.Now >= ppl.StartDate) ||
                                                       (DateTime.Now >= ppl.StartDate && DateTime.Now <= ppl.EndDate) || (ppl.StartDate == null && ppl.EndDate == null)))
                                                       ?
                                                       pplsm.Discount
                                                       :
                                                       (pplsm.Price != null && pplsm.Price > 0 && ((ppl.StartDate != null && ppl.EndDate == null && DateTime.Now >= ppl.StartDate) ||
                                                       (DateTime.Now >= ppl.StartDate && DateTime.Now <= ppl.EndDate) || (ppl.StartDate == null && ppl.EndDate == null)))
                                                       ?
                                                       pplsm.Price
                                                       :
                                                       psm.ProductPrice)).FirstOrDefault();

            }
            return productPrice;
        }

        public long GetBrandIDbySKUID(long skuID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return (from p in db.Products
                        join b in db.Brands on p.BrandID equals b.BrandIID
                        join psm in db.ProductSKUMaps on p.ProductIID equals psm.ProductID
                        where psm.ProductSKUMapIID == skuID
                        select b.BrandIID).FirstOrDefault();
            }
        }
        public string GetBrandbySkuID(long skuID, long cultureID)
        {
            var brand = "";
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if (cultureID == 1)
                {
                    brand = (from p in db.Products
                             join b in db.Brands on p.BrandID equals b.BrandIID
                             join psm in db.ProductSKUMaps on p.ProductIID equals psm.ProductID
                             where psm.ProductSKUMapIID == skuID
                             select b.BrandName.ToString()).AsNoTracking().FirstOrDefault();
                }
                else
                {
                    brand = (from p in db.Products
                             join b in db.Brands on p.BrandID equals b.BrandIID
                             join psm in db.ProductSKUMaps on p.ProductIID equals psm.ProductID
                             join bs in db.BrandCultureDatas on b.BrandIID equals bs.BrandID
                             where psm.ProductSKUMapIID == skuID && bs.CultureID == cultureID
                             select bs.BrandName.ToString()).AsNoTracking().FirstOrDefault();
                }
            }
            return brand;
        }
        public long GetQuantitybySkuID(long skuID, long branchID)
        {
            long quantity = 0;
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                try
                {
                    quantity = (from p in db.Products
                                join psm in db.ProductSKUMaps on p.ProductIID equals psm.ProductID
                                join pi in db.ProductInventories on psm.ProductSKUMapIID equals pi.ProductSKUMapID
                                where psm.ProductSKUMapIID == skuID && pi.BranchID == branchID
                                select (long?)pi.Quantity ?? 0).DefaultIfEmpty().Sum();
                }
                catch (Exception)
                {
                    quantity = 0;
                }
            }
            return quantity;
        }

        public bool reduceQuantity(long skuID, long quantity, long branchID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var productInventory = (from p in db.Products
                                        join psm in db.ProductSKUMaps on p.ProductIID equals psm.ProductID
                                        join pi in db.ProductInventories on psm.ProductSKUMapIID equals pi.ProductSKUMapID
                                        where psm.ProductSKUMapIID == skuID && pi.BranchID == branchID && pi.Quantity > 0
                                        select pi).AsNoTracking().FirstOrDefault();

                if (productInventory != null)
                {
                    productInventory.Quantity = productInventory.Quantity - quantity;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }



        #endregion

        public DataTable GetProductDeliveryTypeList(long skuID,int companyID, long branchID=0)
        {
            DataTable cartDeliveryList = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetDefaultConnectionString()))
                using (SqlCommand cmd = new SqlCommand("inventory.spcProductDeliveryDisplay", conn))
                {
                    cmd.CommandTimeout = 0;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@SkuID", SqlDbType.BigInt));
                    adapter.SelectCommand.Parameters["@SkuID"].Value = skuID;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@BranchID", SqlDbType.BigInt));
                    adapter.SelectCommand.Parameters["@BranchID"].Value = branchID;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@CompanyID", SqlDbType.BigInt));
                    adapter.SelectCommand.Parameters["@CompanyID"].Value = companyID;

                    DataSet dt = new DataSet();
                    adapter.Fill(dt);
                    cartDeliveryList = dt.Tables[0];
                }

            }
            catch (Exception)
            {
            }

            return cartDeliveryList;
        }

        public int GetProductType(long skuID)
        {
            int productType;
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                productType = (from p in db.Products
                               join psm in db.ProductSKUMaps on p.ProductIID equals psm.ProductID
                               where psm.ProductSKUMapIID == skuID
                               select (int)p.ProductTypeID).Take(1).FirstOrDefault();
            }
            return productType;
        }

        public byte ProductSkuIDActiveCheck(long skuID)
        {
            byte productType;
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                productType = (from p in db.Products
                               join psm in db.ProductSKUMaps on p.ProductIID equals psm.ProductID
                               where psm.ProductSKUMapIID == skuID 
                               select (byte)psm.StatusID).FirstOrDefault();
            }
            return productType;
        }

        public DataTable GetActiveDealProducts(long branchID, long noOfProducts)
        {
            //DataSet dsDeal = new DataSet();

            DataTable dealProducts = new DataTable();
            //DataTable dealDetails = new DataTable();
            //dsDeal.Tables.Add(dealDetails);
            //dsDeal.Tables.Add(dealProducts);
            try
            {
                using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetDefaultConnectionString()))
                using (SqlCommand cmd = new SqlCommand("catalog.spcGetActiveDealProducts", conn))
                {
                    cmd.CommandTimeout = 0;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@InventoryBranch", SqlDbType.BigInt));
                    adapter.SelectCommand.Parameters["@InventoryBranch"].Value = branchID;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@NoOfDeals", SqlDbType.BigInt));
                    adapter.SelectCommand.Parameters["@NoOfDeals"].Value = noOfProducts;

                    DataSet dt = new DataSet();
                    adapter.Fill(dt);
                    return dt.Tables[0];
                }

            }
            catch (Exception)
            {
            }

            return dealProducts;
        }

        public List<ProductVideoMap> GetProductVideos(long skuID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.ProductVideoMaps.Where(a => a.ProductSKUMapID == skuID).OrderByDescending(a => a.Sequence).AsNoTracking().ToList();
            }
        }

        public List<ProductSKUDetail> GetProductByTag(List<string> tagName, string cultureCode, int noOfRecords, long categoryID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (noOfRecords == 0) noOfRecords = 100000;
                var products = (from product in dbContext.ProductSKUMaps
                                //join productMaster in dbContext.Products on product.ProductID equals productMaster.ProductIID
                                join category in dbContext.ProductCategoryMaps on product.ProductID equals category.ProductID
                                join cultureProduct in dbContext.ProductSKUCultureDatas on product.ProductSKUMapIID equals cultureProduct.ProductSKUMapID into categoryCultureData
                                from cultureProduct in categoryCultureData.DefaultIfEmpty()
                                join culture in dbContext.Cultures on cultureProduct.CultureID equals culture.CultureID into cutlureMaster
                                from culture in cutlureMaster.Where(a => a.CultureCode == cultureCode).DefaultIfEmpty()

                                join tagMap in dbContext.ProductSKUTagMaps on product.ProductSKUMapIID equals tagMap.ProductSKuMapID
                                join tag in dbContext.ProductSKUTags on tagMap.ProductSKUTagID equals tag.ProductSKUTagIID

                                where tagName.Contains(tag.TagName.Trim().ToUpper()) && category.CategoryID == categoryID
                                select new
                                {
                                    ProductSKUID = product.ProductSKUMapIID,
                                    ProductName = cultureCode == "en" ? product.SKUName : !string.IsNullOrEmpty(cultureProduct.ProductSKUName) ? cultureProduct.ProductSKUName : product.SKUName,
                                    ProductSKUCode = product.ProductSKUCode
                                }).Take(noOfRecords).ToList().Select(product => new ProductSKUDetail
                                {

                                    ProductSKUMapIID = product.ProductSKUID,
                                    ProductName = product.ProductName,
                                    ProductSKUCode = product.ProductSKUCode

                                }).ToList();

                products = products.OrderBy(a => Guid.NewGuid()).ToList();
                return products;
            }
        }

        public List<ProductSKUDetail> GetProductByTagWithoutCategory(List<string> tagName, string cultureCode, int noOfRecords)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (noOfRecords == 0) noOfRecords = 100000;
                var products = (from product in dbContext.ProductSKUMaps
                                //join productMaster in dbContext.Products on product.ProductID equals productMaster.ProductIID
                                //join category in dbContext.ProductCategoryMaps on product.ProductID equals category.ProductID
                                join cultureProduct in dbContext.ProductSKUCultureDatas on product.ProductSKUMapIID equals cultureProduct.ProductSKUMapID into categoryCultureData
                                from cultureProduct in categoryCultureData.DefaultIfEmpty()
                                join culture in dbContext.Cultures on cultureProduct.CultureID equals culture.CultureID into cutlureMaster
                                from culture in cutlureMaster.Where(a => a.CultureCode == cultureCode).DefaultIfEmpty()

                                join tagMap in dbContext.ProductSKUTagMaps on product.ProductSKUMapIID equals tagMap.ProductSKuMapID
                                join tag in dbContext.ProductSKUTags on tagMap.ProductSKUTagID equals tag.ProductSKUTagIID

                                where tag.TagName.Trim().ToUpper() == tagName.FirstOrDefault().ToUpper() //&& category.CategoryID == categoryID
                                select new
                                {
                                    ProductSKUID = product.ProductSKUMapIID,
                                    ProductName = cultureCode == "en" ? product.SKUName : !string.IsNullOrEmpty(cultureProduct.ProductSKUName) ? cultureProduct.ProductSKUName : product.SKUName,
                                    ProductSKUCode = product.ProductSKUCode
                                }).Take(noOfRecords).ToList().Select(product => new ProductSKUDetail
                                {

                                    ProductSKUMapIID = product.ProductSKUID,
                                    ProductName = product.ProductName,
                                    ProductSKUCode = product.ProductSKUCode

                                }).ToList();

                products = products.OrderBy(a => Guid.NewGuid()).ToList();
                return products;
            }
        }

        public DataTable ProductSearch(string searchText, int dataSize)
        {
            try 
            {
                using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetDefaultConnectionString()))
                using (SqlCommand cmd = new SqlCommand("catalog.spcProductSearch", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@searchtext", SqlDbType.Text));
                    adapter.SelectCommand.Parameters["@searchtext"].Value = searchText;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@topcount", SqlDbType.Int));
                    adapter.SelectCommand.Parameters["@topcount"].Value = dataSize;

                    DataSet dt = new DataSet();
                    adapter.Fill(dt);

                    if (dt.Tables.Count > 0)
                    {
                        return dt.Tables[0];
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetProductSKUSiteMap(long productSKUMapID, int siteID, bool isActive)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var result = (from sku in dbContext.ProductSKUSiteMaps
                              where sku.ProductSKUMapID == productSKUMapID && sku.SiteID == siteID
                              select sku);
                if (result.Count() > 0)
                {
                    result.ToList().ForEach(x => { x.IsActive = isActive; x.UpdatedDate = DateTime.Now; });
                    dbContext.SaveChanges();
                }
               else {
                    var entity = new ProductSKUSiteMap() {
                        ProductSKUMapID = productSKUMapID,
                        SiteID = siteID,
                        IsActive = isActive,
                        UpdatedDate = DateTime.Now 
                    };
                    dbContext.ProductSKUSiteMaps.Add(entity);
                    dbContext.SaveChanges();
                }
            }
        }

        public List<Category> OnlineStoreGetCategory()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var entity = dbContext.Categories
                    .Include(a => a.CategoryImageMaps)
                    .Include(a => a.CategoryTagMaps)
                    .Include(a => a.CategoryCultureDatas)
                    .Include(a => a.CategorySettings)
                    .Where(x => x.IsActive == true)
                    .OrderBy(x => x.CategoryName)
                    .AsNoTracking()
                    .ToList();

                //if (companyID != 0)
                //    entity.CategoryImageMaps = entity.CategoryImageMaps.Where(a => a.CompanyID == companyID).ToList();

                //foreach (var tags in entity.CategoryTagMaps)
                //{
                //    dbContext.Entry(tags).Reference(a => a.CategoryTag).Load();
                //}

                return entity;
            }
        }

        public List<Brand> GetActiveBrands()
        {
            using (var db = new dbEduegateERPContext())
            {
                return db.Brands
                    .Include(x => x.BrandImageMaps)
                    .Include(x => x.BrandTagMaps)
                    .Include(x => x.BrandCultureDatas)
                    .OrderBy(x => x.BrandName)
                    .Where(x => x.StatusID == 1)
                    .AsNoTracking()
                    .ToList();
            }
        }
        public List<ProductDetailValueObject> OnlineStoreProductSKUSearch(int pageIndex, int pageSize, string searchText, string searchVal,
            string searchBy, string sortBy, string pageType, bool isCategory, out int totalRecords,
            string languageCode = null, long? customerID = null, short? statusID = 2)
        {
            try
            {
                var products = new List<ProductDetailValueObject>();

                using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetDefaultConnectionString()))
                using (SqlCommand cmd = new SqlCommand("catalog.spcProductQuickSearchWithPagination", conn))
                {
                    var orderBy = "SKUName ASC";
                    switch (sortBy)
                    {
                        case "price-low":
                            orderBy = "ProductPrice asc";
                            break;
                        case "price-high":
                            orderBy = "ProductPrice desc";
                            break;
                        case "new-arrivals":
                            orderBy = "ProcessedDate desc";
                            break;
                    }

                    var adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@searchtext", SqlDbType.NVarChar));
                    adapter.SelectCommand.Parameters["@searchtext"].Value = searchText;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@pageIndex", SqlDbType.Int));
                    adapter.SelectCommand.Parameters["@pageIndex"].Value = pageIndex;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@pageSize", SqlDbType.Int));
                    adapter.SelectCommand.Parameters["@pageSize"].Value = pageSize;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@searchBy", SqlDbType.Text));
                    adapter.SelectCommand.Parameters["@searchBy"].Value = searchBy;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@sortBy", SqlDbType.Text));
                    adapter.SelectCommand.Parameters["@sortBy"].Value = orderBy;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@pageType", SqlDbType.Text));
                    adapter.SelectCommand.Parameters["@pageType"].Value = pageType;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@isCategory", SqlDbType.Bit));
                    adapter.SelectCommand.Parameters["@isCategory"].Value = isCategory;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@searchVal", SqlDbType.Text));
                    adapter.SelectCommand.Parameters["@searchVal"].Value = searchVal;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@languageCode", SqlDbType.Text));
                    adapter.SelectCommand.Parameters["@languageCode"].Value = languageCode;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@customerID", SqlDbType.BigInt));
                    adapter.SelectCommand.Parameters["@customerID"].Value = customerID;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@statusID", SqlDbType.SmallInt));
                    adapter.SelectCommand.Parameters["@statusID"].Value = statusID;

                    var dt = new DataSet();
                    adapter.Fill(dt);

                    if (dt.Tables.Count > 0)
                    {
                        totalRecords = int.Parse(dt.Tables[1].Rows[0][0].ToString());
                        var datas = dt.Tables[0];

                        foreach (DataRow data in datas.Rows)
                        {
                            products.Add(new ProductDetailValueObject()
                            {
                                ProductSKUMapIID = long.Parse(data["ProductSKUMapIID"].ToString()),
                                IsActive = datas.Columns.Contains("StatusID") && data["StatusID"].ToString() == "2" ? true : false,
                                ProductName = data["SKU"].ToString(),
                                ProductID = long.Parse(data["ProductIID"].ToString()),
                                SKUID = long.Parse(data["ProductSKUMapIID"].ToString()),
                                BrandCode = datas.Columns.Contains("BrandCode") && data["BrandCode"] != null ? data["BrandCode"].ToString() : null,
                                BrandID = datas.Columns.Contains("BrandIID") && data["BrandIID"] != null && !string.IsNullOrEmpty(data["BrandIID"].ToString()) ? Convert.ToInt64(data["BrandIID"].ToString()) : (long?)null,
                                CategoryCode = datas.Columns.Contains("CategoryCode") && data["CategoryCode"] != null ? data["CategoryCode"].ToString() : null,
                                CategoryID = datas.Columns.Contains("CategoryID") && data["CategoryID"] != null && !string.IsNullOrEmpty(data["CategoryID"].ToString()) ? Convert.ToInt64(data["CategoryID"].ToString()) : (long?)null,
                                AdditionalInfo1 = datas.Columns.Contains("AdditionalInfo1") && data["AdditionalInfo1"] != null ? data["AdditionalInfo1"].ToString() : null,
                                AdditionalInfo2 = datas.Columns.Contains("AdditionalInfo2") && data["AdditionalInfo2"] != null ? data["AdditionalInfo2"].ToString() : null,
                                ProductDescription = datas.Columns.Contains("ProductDescription") && data["ProductDescription"] != null ? data["ProductDescription"].ToString() : null,
                                AlertMessage = datas.Columns.Contains("AlertMessage") && data["AlertMessage"] != null ? data["AlertMessage"].ToString() : null,
                            });
                        }
                    }
                }

                totalRecords = 0;
                return products;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.SearchData;
using Eduegate.Framework.Helper;
using Eduegate.Domain.Entity;
using Eduegate.Domain.Mappers.CategoryCulture;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Framework.Enums;
using System.Data;

namespace Eduegate.Domain
{
    public class BlinkLegacyBL
    {
        private BlinkLegacyRepository repository = new BlinkLegacyRepository();

        private ProductDetailRepository repositoryProductDetail = new ProductDetailRepository();

        private Eduegate.Framework.CallContext _callContext { get; set; }

        public BlinkLegacyBL(Eduegate.Framework.CallContext context)
        {
            _callContext = context;
        }

        public List<SearchCatalogDTO> GetProductCatalogs(int pageNumber, int pageSize)
        {
            var productsDTO = new List<SearchCatalogDTO>();
            foreach (var product in repository.GetProducts(pageNumber, pageSize))
            {
                productsDTO.Add(FromProductEntityToAdhocDTO(product));
            }
            return productsDTO;
        }

        public List<KeywordsDTO> GetKeywords(int pageNumber, int pageSize,string lng,int country)
        {
            if (country == 10000)
            {
                if (lng == "en")
                {
                    return ToKeywordDTOs(repository.GetKeywords(pageNumber, pageSize));
                }
                else
                {
                    return ToKeywordDTOsAr(repository.GetKeywordsAr(pageNumber, pageSize));
                }
            }
            else
            {
                if (lng == "en")
                {
                    return ToKeywordDTOsIntl(repository.GetKeywordsIntl(pageNumber, pageSize));
                }
                else
                {
                    return ToKeywordDTOsArIntl(repository.GetKeywordsArIntl(pageNumber, pageSize));
                }
            }
          
           
        }

        public List<KeywordsDTO> ToKeywordDTOs(List<ProductSearchKeyword> keywords)
        {
            var dtos = new List<KeywordsDTO>();
            foreach (var keyword in keywords)
            {
                dtos.Add(new KeywordsDTO()
                {
                    Keyword = keyword.Keyword,
                    LogID = keyword.LogID
                });
            }

            return dtos;
        }

        public List<KeywordsDTO> ToKeywordDTOsAr(List<ProductSearchKeywordsAr> keywords)
        {
            var dtos = new List<KeywordsDTO>();
            foreach (var keyword in keywords)
            {
                dtos.Add(new KeywordsDTO()
                {
                    Keyword = keyword.KeywordAr,
                    LogID = keyword.LogID
                });
            }

            return dtos;
        }

        public List<KeywordsDTO> ToKeywordDTOsIntl(List<ProductSearchKeywordsIntl> keywords)
        {
            var dtos = new List<KeywordsDTO>();
            foreach (var keyword in keywords)
            {
                dtos.Add(new KeywordsDTO()
                {
                    Keyword = keyword.Keyword,
                    LogID = keyword.LogID
                });
            }

            return dtos;
        }

        public List<KeywordsDTO> ToKeywordDTOsArIntl(List<ProductSearchKeywordsArIntl> keywords)
        {
            var dtos = new List<KeywordsDTO>();
            foreach (var keyword in keywords)
            {
                dtos.Add(new KeywordsDTO()
                {
                    Keyword = keyword.KeywordAr,
                    LogID = keyword.LogID
                });
            }

            return dtos;
        }

        public SearchCatalogDTO GetProductCatalog(long productID)
        {
            var result = FromProductEntityToAdhocDTO(repository.GetProducts(productID));
            return result;
        }

        public List<ProductDTO> GetProducts(int pageNumber, int pageSize, int country, int cultureID,int companyID,int siteID)
        {
            var productsDTO = new List<ProductDTO>();
            foreach (var product in repository.GetProducts(pageNumber, pageSize, cultureID, companyID))
                {
                    productsDTO.Add(FromProductEntity(product, country, cultureID, companyID, siteID));
                }
            return productsDTO;
        }

        public ProductDTO GetProducts(long productID, int country, int cultureID = 1, int companyID = 1,int siteID=1)
        {
            return FromProductEntity(repository.GetProducts(productID, cultureID, companyID), country, cultureID, companyID, siteID);
        }

        public ProductDTO FromProductEntity(Domain.Entity.ProductMaster entity,int country,int cultureID= 1,int companyID=1,int siteID=1)
        {
            if (entity != null)
            {
                var productInventory = new ProductDetailBL(this._callContext).GetInventoryDetailsSKUID(entity.SkuID, 0, true, companyID);
                var skuTags = new ProductDetailBL(this._callContext).GetProductSKUTags(entity.SkuID, companyID);
                var categoryTags = new ProductDetailBL(this._callContext).GetCategoryProductSKUTags(entity.SkuID);
                entity.ProductCategoryAll = CategoryAllList(entity.ProductID, siteID);
                var productImages = GetAllProductImages(entity.SkuID);
                var listingImage =  productImages.Where(a => a.ProductImageTypeID == 8).Select(a => a.ImageFile).FirstOrDefault();
                var thumpNailImage = productImages.Where(a => a.ProductImageTypeID == 1).Select(a => a.ImageFile).FirstOrDefault();
                var soldQuantiy = new TransactionRepository().BestSellersCountBySkuID(entity.SkuID, companyID,(int)Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.SalesOrder, (long)ProductTypes.Physical, (long)DocumentStatuses.Completed, (byte)Eduegate.Framework.Enums.TransactionStatus.Complete);
                entity.ProductListingImageMaster = listingImage;
                entity.ProductThumbnailMaster = thumpNailImage;
                var brand = repository.GetBrandCultureData(long.Parse(entity.RefBrandID.ToString()), cultureID);
                if (brand == null)
                {
                    brand = new BrandDTO();
                    brand.BrandIID = 0;
                    brand.BrandCode = "";
                    brand.BrandPosition = 0;
                    brand.BrandName = "";
                    brand.Description = "";
                    brand.BrandKeywordsEn = "";
                    brand.BrandNameAr = "";
                    brand.BrandKeywordsAr = "";
                }
                return new ProductDTO()
                {
                    ProductName = entity.ProductName,
                    ProductID = entity.ProductID,
                    ProductPrice = productInventory.ProductPricePrice.ToString(),
                    ProductCode = entity.ProductCode,
                    ProductCategoryAll = entity.ProductCategoryAll,
                    BrandName = brand.BrandName,
                    ProductThumbnail = entity.ProductThumbnailMaster,
                    BrandCode = brand.BrandCode,
                    BrandID = int.Parse(brand.BrandIID.ToString()),
                    BrandNameEn = brand.Description,
                    BrandPosition = brand.BrandPosition,
                    DisableListing = entity.DisableListing,
                    ProductActive = entity.ProductActive,
                    ProductActiveAr = entity.ProductActiveAr,
                    ProductColor = entity.ProductColor,
                    ProductDiscountPrice = productInventory.ProductDiscountPrice > 0 ? productInventory.ProductDiscountPrice : productInventory.ProductPricePrice,
                    ProductListingImage = entity.ProductListingImageMaster,
                    ProductMadeIn = entity.ProductMadeIn,
                    ProductModel = entity.ProductModel,
                    ProductSoldQty = soldQuantiy.HasValue ? (int)soldQuantiy.Value : 0,
                    ProductWarranty = entity.ProductWarranty,
                    ProductWeight = entity.ProductWeight,
                    ProductAvailableQuantity = (int)productInventory.Quantity,
                    QuantityDiscount = entity.QuantityDiscount,
                    DeliveryDays = entity.DeliveryDays,
                    NewArrival = 0,// repository.GetNewArrival(entity.ProductID,country),
                    CategoryIDs = (string.IsNullOrEmpty(entity.ProductCategoryAll)) ? new List<string>() :
                    new List<string>(entity.ProductCategoryAll.Split('\\').Where(a => !string.IsNullOrEmpty(a))),
                    SKU = entity.SkuID.ToString(),
                    Descirption = "",
                    BrandKeyWords = brand.BrandKeywordsEn,
                    ProductKeyWordsEn = entity.ProductKeywordsEn,
                    ProductNameAr = entity.ProductNameAr,
                    BrandNameAr = brand.BrandNameAr,
                    BrandKeyWordsAr = brand.BrandKeywordsAr,
                    ProductKeyWordsAr = entity.ProductKeywordsAr,
                    SKUTags = skuTags,
                    CategoryTags = categoryTags,
                    ProductPartNo = entity.ProductPartNo,
                    ProductCreatedDate = entity.ProductCreatedOn.HasValue ? entity.ProductCreatedOn.Value.ToString() : string.Empty,
                    DeliveryTypesList = GetDeliveryList(entity.SkuID, companyID, cultureID),
                    ProductNameCulture = entity.ProductNameAr,
                    SKUProperties = BuildSKUProperties(entity.SkuID,cultureID)
                };
            }
            else
            {
                return new ProductDTO();
            }
           
        }

        List<string> BuildSKUProperties(long skuID, int cultureID)
        {
            var skuPropertiesList = new List<string>();
            var propertiesList = new ProductBL(_callContext).GetProductKeyFeatures(skuID,cultureID);
            foreach (var item in propertiesList)
            {
                skuPropertiesList.Add(string.Concat(item.PropertyIID.ToString(),"_",item.FeatureName,"||",item.PropertyTypeID,"_",item.FeatureValue));
            }
            return skuPropertiesList;
        }

        public  List<string> GetDeliveryList(long SkuID, int companyID,int cultureID)
        {
            DataTable delResult = new ProductBL(_callContext).GetProductDeliveryType(SkuID,companyID);
            List<string> delList = new List<string>();
            foreach (var item in delResult.AsEnumerable())
            {
                delList.Add(item["DeliveryTypeID"].ToString() + "_" + item["DeliveryTypeName"].ToString());
            }
            return delList;
        }

        public List<ProductImageMap> GetProductImages(long skuID)
        {
            return repositoryProductDetail.GetProductImages(skuID);
        }
        public List<ProductImageMap> GetAllProductImages(long skuID)
        {
            return repositoryProductDetail.GetAllProductImages(skuID);
        }

        public SearchCatalogDTO FromProductEntityToAdhocDTO(Domain.Entity.ProductMaster entity)
        {
            var brand = repository.GetBrand(long.Parse(entity.RefBrandID.ToString()));

            var dto = new SearchCatalogDTO()
            {
                ProductName = entity.ProductName,
                ProductID = entity.ProductID,
                ProductPrice = entity.ProductPrice,
                ProductCode = entity.ProductCode,
                ProductCategoryAll = entity.ProductCategoryAll,
                BrandName = brand.BrandName,
                ProductThumbnail = entity.ProductThumbnailMaster,
                BrandCode = brand.BrandCode,
                BrandID = brand.BrandIID,
                BrandNameEn = brand.Description,
                BrandPosition = short.Parse(brand.BrandPosition.ToString()),
                DisableListing = entity.DisableListing.Value,
                ProductActive = entity.ProductActive,
                ProductActiveAr = entity.ProductActiveAr.Value,
                ProductColor = entity.ProductColor,
                ProductDiscountPrice = entity.ProductDiscountPrice,
                ProductListingImage = entity.ProductListingImageMaster,
                ProductMadeIn = entity.ProductMadeIn,
                ProductModel = entity.ProductModel,
                ProductSoldQty = repository.GetProductSoldQty(entity.ProductID,10000),
                ProductWarranty = entity.ProductWarranty,
                ProductWeight = entity.ProductWeight.Value,
                ProductAvailableQuantity = entity.ProductAvailableQuantity,
                QuantityDiscount = entity.QuantityDiscount.Value,
                DeliveryDays = entity.DeliveryDays.Value,
                NewArrival = repository.GetNewArrival(entity.ProductID, 10000),
                ProductKeywordsEn = entity.ProductKeywordsEn,
                BrandKeywordsEn = brand.BrandKeywordsEn,
                
            };
          
            return dto;
        }

        public BrandDTO ToBrandDTO(BrandMaster brand)
        {
            return new BrandDTO()
            {
                BrandCode = brand.BrandCode,
                BrandIID = brand.BrandID,
                BrandLogo = brand.BrandLogo,
                BrandName = brand.BrandNameEn,
                
                
            };
        }
        public ProductCategoryDTO GetCategory(long categoryID)
        {
            return FromCategoryEntity(repository.GetCategory(categoryID));
        }

        public ProductCategoryDTO GetCategoryCulture(long categoryID,int cultureID =1)
        {
            var productCategoryDTO = new ProductCategoryDTO();
            var mapper = CategoryCultureMapper.Mapper();
            return mapper.ToDTO(repository.GetCategoryCulture(categoryID, cultureID));
        }

        public List<ProductCategoryDTO> GetCategory(int pageNumber, int pageSize)
        {
            var categoryDTO = new List<ProductCategoryDTO>();
            foreach (var category in repository.GetCategory(pageNumber, pageSize))
            {
                categoryDTO.Add(FromCategoryEntity(category));
            }

            return categoryDTO;
        }

        public ProductCategoryDTO FromCategoryEntity(Domain.Entity.Models.Category entity)
        {
            if (entity == null) return null;

            return new ProductCategoryDTO()
            {
                CategoryCode = entity.CategoryCode,
                CategoryID = entity.CategoryIID,
                CategoryName = entity.CategoryName,
                Active = (bool)entity.IsActive,
                SeoKeyWords = "",
                SeoKeyWordsAr = "",
            };
        }

        public long GetTotalProducts(int companyID)
        {
            return repository.GetTotalProducts(companyID);
        }

        public long GetTotalCategories()
        {
            return repository.GetTotalCategories();
        }

        public long GetTotalKeywords()
        {
            return repository.GetTotalKeywords();
        }
        public ProductCategoryDTO GetCategoryProductLevel(long categoryID, long productID, int country)
        {
            //return FromCategoryEntity(repository.GetCategoryProductLevel(categoryID, productID, country));
            return null;
        }

        public ProductCategoryDTO FromCategoryProductlevelEntity(Domain.Entity.CategoryMaster entity)
        {
            if (entity == null) return null;

            return new ProductCategoryDTO()
            {
                CategoryCode = entity.CategoryCode,
                CategoryID = entity.CategoryID,
                CategoryName = entity.CategoryNameEn,
                Level = entity.CategoryLevel,
                Active = entity.CategoryActive,
                CategoryHierarchies = (string.IsNullOrEmpty(entity.CategoryBreadCrumbs)) ? new List<string>() :
                       new List<string>(entity.CategoryBreadCrumbs.Split('\\').Where(a => !string.IsNullOrEmpty(a))),
                SeoKeyWords = entity.SeoKeywords,
                CategoryNameAr = entity.CategoryNameAr,
                SeoKeyWordsAr = entity.SeoKeywordsAr,
            };
        }

        public bool UpdateEntityChangeTrackerLog()
        {
            return repository.UpdateEntityChangeTrackerLog();
        }

        public string CategoryAllList(long productID,int siteID)
        {
            return repository.CategoryAllList(productID, siteID);
        }

    }
}

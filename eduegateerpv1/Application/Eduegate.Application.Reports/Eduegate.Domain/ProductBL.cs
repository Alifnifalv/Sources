using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.CustomEntity;
using Eduegate.Domain.Repository;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Extensions;
using Eduegate.Framework;
using Eduegate.Domain.Entity.Models;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Framework.Enums;
using Eduegate.Domain.Mappers;
using Eduegate.Services.Contracts.ProductDetail;
using Eduegate.Services.Contracts.SearchData;
using System.Data;
using Eduegate.Globalization;
using Eduegate.Domain.Entity.Models.ValueObjects;
using Eduegate.Domain.Mappers.SolrMapper;

namespace Eduegate.Domain
{
    public class ProductBL
    {
        private string ImageRootUrl = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("ImageRootUrl");
        private static ProductCatalogRepository productCatalogRepository = new ProductCatalogRepository();
        private Eduegate.Framework.CallContext _callContext { get; set; }

        public ProductBL(Eduegate.Framework.CallContext context)
        {
            _callContext = context;
        }

        public static List<ProductDTO> GetProducts()
        {
            //var productsDTO = new List<ProductDTO>();
            //foreach (var product in productCatalog.GetProducts())
            //{
            //    productsDTO.Add(new ProductDTO()
            //    {
            //        ProductCode = product.ProductCode,
            //        ProductID = product.ProductID,
            //        CategoryIDs = (string.IsNullOrEmpty(product.ProductCategoryAll)) ? new List<string>() :
            //        new List<string>(product.ProductCategoryAll.Split('\\').Where(a => !string.IsNullOrEmpty(a)))
            //    }
            //);
            //}

            //return productsDTO;
            return null;
        }

        public static List<ProductCategoryDTO> GetCategory()
        {
            //var categoryDTO = new List<ProductCategoryDTO>();
            //foreach (var category in productCatalog.GetCategory())
            //{
            //    categoryDTO.Add(new ProductCategoryDTO()
            //    {
            //        CategoryCode = category.CategoryCode,
            //        CategoryID = category.CategoryID,
            //        Level = category.CategoryLevel,
            //        Active = category.CategoryActive,
            //        CategoryHierarchies = (string.IsNullOrEmpty(category.CategoryBreadCrumbs)) ? new List<string>() :
            //               new List<string>(category.CategoryBreadCrumbs.Split('\\').Where(a => !string.IsNullOrEmpty(a)))
            //    });
            //}

            //return categoryDTO;
            return null;
        }

        public static List<ProductDTO> GetProductList(string searchText)
        {
            //List<ProductDTO> products = new List<ProductDTO>();
            //var productMasterDetails = productCatalogRepository.GetProductsList(searchText);
            //foreach (var detail in productMasterDetails)
            //{
            //    var product = new ProductDTO();
            //    product.ProductID = detail.ProductID;
            //    product.ProductName = detail.ProductName;
            //    products.Add(product);
            //}
            //return products;
            return null;
        }

        public List<POSProductDTO> GetProductListWithSKU(string searchText, int dataSize)
        {
            var products = new List<POSProductDTO>();
            var productList = productCatalogRepository.GetProductListWithSKU(searchText, dataSize);

            foreach (var product in productList)
            {
                var productDTO = new POSProductDTO();
                productDTO.ProductIID = product.ProductIID;
                productDTO.ProductSKUMapIID = product.ProductSKUMapIID;
                productDTO.Sequence = product.Sequence;
                productDTO.SKU = product.SKU;
                productDTO.ProductName = product.ProductName;
                productDTO.SellingQuantityLimit = product.SellingQuantityLimit;
                productDTO.ProductPrice = product.ProductPrice;
                productDTO.Quantity = product.Quantity;
                productDTO.ImageFile = product.ImageFile;
                productDTO.Barcode = product.Barcode;
                productDTO.PartNo = product.PartNo;
                products.Add(productDTO);
            }

            return products;
        }

        public static List<ProductDTO> GetProductGroups(string searchText)
        {
            //List<ProductDTO> products = new List<ProductDTO>();
            //var productMasterDetails = productCatalogRepository.GetProductGroups(searchText);
            //foreach (var detail in productMasterDetails)
            //{
            //    var product = new ProductDTO();
            //    product.ProductGroup = detail.ProductGroup;
            //    products.Add(product);
            //}
            //return products;
            return null;
        }

        public static ProductDTO GetProduct(long productID)
        {
            //var product = productCatalogRepository.GetProduct(productID);
            //var productDTO = new ProductDTO();
            //productDTO.ProductID = product.ProductID;
            //productDTO.ProductName = product.ProductName;
            //productDTO.Price = product.ProductPrice;
            //productDTO.DiscountedPrice = Eduegate.Framework.Helper.Utility.FormatDecimal(Convert.ToDecimal(product.ProductDiscountPrice), 3);
            //productDTO.DiscountPercentage = product.ProductDiscountPercent.HasValue ? product.ProductDiscountPercent.Value : 0;
            //return productDTO;
            return null;
        }
        public static ProductDTO GetProductGroupDetails(string productGroup)
        {
            //var product = productCatalogRepository.GetProductGroupDetails(productGroup);
            //var productDTO = new ProductDTO();
            //productDTO.ProductGroup = product.ProductGroup;

            //return productDTO;
            return null;
        }

        public List<ProductDTO> GetProductByCategory(long CategoryId, long MenuId, int pageNumber, int pageSize, CallContext context, string sortBy, ProductStatuses productStatus)
        {
            // get the converted price 
            //decimal? ConvertedPrice = UtilityRepository.GetCurrencyPrice(context);

            //var lists = productCatalog.GetProductByCategory(CategoryId, MenuId, ConvertedPrice, pageNumber, pageSize, sortBy, productStatus);
            //List<ProductDTO> list = new List<ProductDTO>();

            //lists.ForEach(x =>
            //    {
            //        list.Add(new ProductDTO
            //        {
            //            ProductID = x.ProductIID,
            //            SkuID = x.SKUID,
            //            ProductCode = x.ProductCode,
            //            ProductName = x.ProductName,
            //            ProductStatus = (Services.Contracts.Enums.ProductStatuses)x.ProductStatus,
            //            ProductPrice = Eduegate.Framework.Helper.Utility.FormatDecimal(Convert.ToDecimal(x.ProductPrice), 3),
            //            SortOrder = x.SortOrder,
            //            //SKU = x.SKU,
            //            ImageFile = ImageRootUrl + (x.ImageFile.IsNull() ? string.Empty : x.ImageFile.Replace("\\", "/")),
            //            Sequence = x.Sequence,
            //            CategoryIID = x.CategoryIID,
            //            ProductCount = x.ProductCount,
            //            DiscountedPrice = x.DiscountedPrice.IsNull() ? "0" : Eduegate.Framework.Helper.Utility.FormatDecimal(Convert.ToDecimal(x.DiscountedPrice), 3),
            //            HasStock = x.HasStock,
            //            BrandIID = x.BrandIID,
            //            BrandName = x.BrandName,
            //            Descirption = x.Descirption,
            //            LogoFile = string.Format("{0}\\{1}\\{2}\\{3}", ConfigurationExtensions.GetAppConfigValue("EduegatesHostUrl").ToString(), "WBImages", WBImageTypes.Brands.ToString(), x.LogoFile)
            //        });
            //    });

            ////foreach (var item in lists)
            ////{
            ////    SearchResultDTO dto = new SearchResultDTO
            ////    {
            ////        ProductIID = item.ProductIID,
            ////        ProductCode = item.ProductCode,
            ////        ProductName = item.ProductName,
            ////        Amount = item.Amount,
            ////        SortOrder = item.SortOrder,
            ////        SKU = item.SKU,
            ////        ImageFile = item.ImageFile,
            ////        Sequence = item.Sequence
            ////    };
            ////    list.Add(dto);
            ////}
            //return list;
            return null;
        }

        public List<ProductDTO> GetProductsBySearchCriteria(string searchText, int pageNumber, int pageSize, CallContext context, string sortBy)
        {
            //decimal? ConvertedPrice = UtilityRepository.GetCurrencyPrice(context);
            //List<ProductDTO> products = new List<ProductDTO>();
            //var productList = productCatalogRepository.GetProductsBySearchCriteria(searchText, pageNumber, pageSize, sortBy);
            //foreach (var product in productList)
            //{
            //    var productDTO = new ProductDTO();
            //    productDTO.ProductID = product.ProductIID;
            //    productDTO.SkuID = product.SKUID;
            //    //productDTO.Amount = product.Amount;
            //    productDTO.ImageFile = ImageRootUrl + (product.ImageFile.IsNull() ? string.Empty : product.ImageFile.Replace("\\", "/"));
            //    //productDTO.ProductCode = product.ProductCode;
            //    productDTO.ProductName = product.ProductName;
            //    //productDTO.SKU = product.ProductSKUCode;
            //    //productDTO.SortOrder = product.SortOrder;
            //    //productDTO.Sequence = product.Sequence;
            //    productDTO.BrandName = product.BrandName;
            //    productDTO.ProductPrice = Eduegate.Framework.Helper.Utility.FormatDecimal(Convert.ToDecimal(product.ProductPrice * ConvertedPrice), 3);
            //    productDTO.DiscountedPrice = Eduegate.Framework.Helper.Utility.FormatDecimal(Convert.ToDecimal(product.DiscountedPrice * ConvertedPrice), 3);
            //    productDTO.HasStock = product.HasStock;

            //    products.Add(productDTO);
            //}
            //return products.OrderBy(x => x.HasStock).ToList();
            return null;
        }

        public ProductCategoryDTO GetProductCategory(long categoryID)
        {
            var entity = new ProductCatalogRepository().GetCategory(categoryID, _callContext.IsNotNull() && _callContext.CompanyID.HasValue ? _callContext.CompanyID.Value : 0);
            entity.CategorySettings = new CategoryRepository().GetCategorySettings(categoryID);
            return Mappers.Catalog.CategoryMapper.Mapper(_callContext).ToDTO(entity);
        }

        public ProductCategoryDTO SaveProductCategory(ProductCategoryDTO category)
        {
            var mapper = Mappers.Catalog.CategoryMapper.Mapper(_callContext);
            return mapper.ToDTO(new ProductCatalogRepository().SaveCategory(mapper.ToEntity(category), _callContext.CompanyID.Value));
        }

        public POSProductDTO GetProductAndSKUByID(long productSKUMapID)
        {
            //var product = productCatalogRepository.GetProductAndSKUByID(productSKUMapID);
            //return POSProductMapper.Mapper.ToDTO(product);
            return null;
        }

        public List<RecomendedProductDTO> GetTopSellingProducts(int pageNumber, int pageSize, CallContext context, string searchText, int siteID = 1)
        {
            //// get the converted price 
            //var searchSolrParameter = new SearchParameterDTO()
            //{
            //    FreeSearch = searchText,
            //    PageIndex = 1,
            //    PageSize = pageSize,
            //    Language = _callContext.LanguageCode,
            //    Sort = "",
            //    ConversionRate = UtilityRepository.GetExchangeRate((int)_callContext.CompanyID, _callContext.CurrencyCode),
            //    SiteID = siteID,
            //};

            ////comented to read from solr
            //var result = (new SearchDataBL(context)).GetProductCatalog(searchSolrParameter);
            //var list = new List<RecomendedProductDTO>();

            //foreach (var item in result.Catalogs)
            //{
            //    list.Add(new RecomendedProductDTO
            //    {
            //        ProductID = item.ProductID,
            //        ProductCode = item.ProductCode,
            //        ProductName = item.ProductName,
            //        ProductDiscountPrice = item.ProductDiscountPrice,
            //        ProductPrice = item.ProductPrice,
            //        SkuID = item.SKUID,
            //        ImageFile = item.ProductThumbnail,
            //        ProductAvailableQuantity = item.ProductAvailableQuantity,
            //        ProductListingQuantity = item.ProductAvailableQuantity > 0 ? 1 : 0,
            //        Currency = _callContext.CurrencyCode,
            //    });
            //}

            //return list;
            return null;
        }

        public List<RecomendedProductDTO> GetProductsFloatCategoryWise(int rowNumber, int productsCount, CallContext context, string searchText, int siteID)
        {
            // get the converted price 
            //SearchParameterDTO obj_SearchParameterDTO = new SearchParameterDTO();
            //obj_SearchParameterDTO.FreeSearch = searchText;
            //obj_SearchParameterDTO.PageIndex = 1;
            //obj_SearchParameterDTO.PageSize = rowNumber * productsCount;
            //obj_SearchParameterDTO.Language = _callContext.LanguageCode;
            //obj_SearchParameterDTO.CountryID = 10000;
            //obj_SearchParameterDTO.Sort = "";
            //obj_SearchParameterDTO.SiteID = siteID;
            //obj_SearchParameterDTO.ConversionRate = UtilityRepository.GetExchangeRate((int)_callContext.CompanyID, _callContext.CurrencyCode);

            ////comented to read from solr
            ////var lists = new ProductCatalogRepository().GetTopSellingProducts(pageNumber, pageSize, ConvertedPrice);
            //var result = (new SearchDataBL(context)).GetProductCatalog(obj_SearchParameterDTO);
            //List<RecomendedProductDTO> list = new List<RecomendedProductDTO>();

            //foreach (var item in result.Catalogs)
            //{
            //    RecomendedProductDTO dto = new RecomendedProductDTO
            //    {
            //        ProductID = item.ProductID,
            //        ProductCode = item.ProductCode,
            //        ProductName = item.ProductName,
            //        ProductDiscountPrice = item.ProductDiscountPrice,
            //        ProductPrice = item.ProductPrice,
            //        SkuID = item.SKUID,
            //        ImageFile = item.ProductThumbnail,
            //        ProductAvailableQuantity = item.ProductAvailableQuantity,
            //        ProductListingQuantity = item.ProductAvailableQuantity > 0 ? 1 : 0,
            //        Currency = _callContext.CurrencyCode,
            //    };
            //    list.Add(dto);
            //}
            //return list;
            return null;
        }

        public List<ProductDTO> GetTopProductByCategory(long categoryID, int takeCount = 1, decimal baseConversion = 1, string tag = null)
        {
            var singleProduct = new ProductDetailRepository().GetProductSKUByCategory(categoryID, takeCount, tag);
            var dtos = new List<ProductDTO>();

            foreach (var product in singleProduct)
            {
                var productInventory = new ProductDetailBL(_callContext).GetProductInventoryOnline(product.ProductSKUMapIID.ToString(), Convert.ToInt64(_callContext.UserId)).FirstOrDefault();

                var productImages = new ProductDetailBL().GetProductImages(product.ProductSKUMapIID);
                var thumbNail = productImages.Where(a => a.ProductImageTypeID == 1).FirstOrDefault();
                var listImage = productImages.Where(a => a.ProductImageTypeID == 8).FirstOrDefault();


                dtos.Add(new ProductDTO()
                {
                    ProductID = product.ProductID.Value,
                    ProductCode = product.ProductSKUCode,
                    ProductName = product.SKUName,
                    ProductPrice = (productInventory.ProductPricePrice * baseConversion).ToString(),
                    ProductDiscountPrice = productInventory.ProductDiscountPrice * baseConversion,
                    ProductAvailableQuantity = (int)productInventory.Quantity,
                    ImageFile = listImage.ImageFile,
                    ProductThumbnail = thumbNail.ImageFile,
                    Currency = _callContext.CurrencyCode,
                });
            }

            return dtos;
        }

        public List<ProductDTO> GetProductSKUByTag(int takeCount = 1, decimal baseConversion = 1, string tag = null)
        {
            if(tag != null)
            {
                tag = tag.ToUpper();
            }

            var singleProduct = new ProductDetailRepository().GetProductSKUByTag(tag, takeCount, _callContext != null ? _callContext.LanguageCode : null);
            var dtos = new List<ProductDTO>();

            foreach (var product in singleProduct)
            {
                var productInventory = new ProductDetailBL(_callContext).GetProductInventoryOnline(product.ProductSKUMapIID.ToString(), Convert.ToInt64(_callContext.UserId)).FirstOrDefault();

                var productImages = new ProductDetailBL().GetProductImages(product.ProductSKUMapIID);
                var thumbNail = productImages.Where(a => a.ProductImageTypeID == 1).FirstOrDefault();
                var listImage = productImages.Where(a => a.ProductImageTypeID == 8).FirstOrDefault();


                dtos.Add(new ProductDTO()
                {
                    SKUID = product.ProductSKUMapIID,
                    ProductID = product.ProductID.Value,
                    ProductCode = product.ProductSKUCode,
                    ProductName = product.SKUName,
                    ProductPrice = productInventory == null ? "0" : (productInventory.ProductPricePrice * baseConversion).ToString(),
                    ProductDiscountPrice = productInventory == null ? 0 : productInventory.ProductDiscountPrice * baseConversion,
                    ProductAvailableQuantity = productInventory == null ? 0 : (int)productInventory.Quantity,
                    ImageFile = listImage == null ? "img/no-image.svg" : string.Format("{0}/Products/{1}", ImageRootUrl, listImage.ImageFile),
                    ProductThumbnail = thumbNail == null ? "img/no-image.svg" : string.Format("{0}/Products/{1}", ImageRootUrl, thumbNail.ImageFile),
                    ProductListingImage = listImage == null ? "img/no-image.svg" : string.Format("{0}/Products/{1}", ImageRootUrl, listImage.ImageFile) ,
                    Currency = _callContext.CurrencyCode,
                });
            }

            return dtos;
        }

        public List<ProductDTO> GetProductsBySubCategory(long categoryID)
        {
            //var productDTOList = new List<ProductDTO>();
            //SearchParameterDTO obj_SearchParameterDTO = new SearchParameterDTO();
            //obj_SearchParameterDTO.FreeSearch = "a";
            //obj_SearchParameterDTO.PageIndex = 1;
            //obj_SearchParameterDTO.PageSize = 100;
            //obj_SearchParameterDTO.Language = _callContext.LanguageCode;
            //obj_SearchParameterDTO.CountryID = 10000;
            //obj_SearchParameterDTO.Sort = "";
            //obj_SearchParameterDTO.ConversionRate = UtilityRepository.GetExchangeRate((int)_callContext.CompanyID, _callContext.CurrencyCode);


            //obj_SearchParameterDTO.FreeSearchFilterKey = "cateagoryall";
            //obj_SearchParameterDTO.FreeSearchFilterValue = categoryID.ToString();
            //var result = (new SearchDataBL(_callContext)).GetProductCatalog(obj_SearchParameterDTO);

            //foreach (var product in result.Catalogs)
            //{
            //    var dto = new ProductDTO();
            //    dto.ProductID = product.ProductID;
            //    dto.ProductCode = product.ProductCode;
            //    dto.ProductName = product.ProductName;
            //    dto.ProductPrice = product.ProductPrice.ToString();
            //    dto.ProductDiscountPrice = product.ProductDiscountPrice;
            //    dto.ImageFile = product.ProductListingImage;
            //    dto.ProductAvailableQuantity = product.ProductAvailableQuantity;
            //    dto.ProductActive = product.ProductActive;
            //    dto.Currency = _callContext.CurrencyCode;
            //    productDTOList.Add(dto);
            //}

            ////var lists = new ProductCatalogRepository().GetProductsBySubCategory(categoryID);
            //return productDTOList;
            return null;
        }
        public List<RecomendedProductDTO> GetProductsAlosViewed(long productID, long cultureID, int siteID)
        {
            //var product1 = GetProductBySKU(productID, cultureID);
            //var productDTOList = new List<RecomendedProductDTO>();
            //SearchParameterDTO obj_SearchParameterDTO = new SearchParameterDTO();
            //var productname = product1.ProductName.IsNotNull() ? product1.ProductName.Split(' ') : new string[1] { "" };
            //obj_SearchParameterDTO.FreeSearch = "*:*";
            //obj_SearchParameterDTO.PageIndex = 1;
            //obj_SearchParameterDTO.PageSize = 100;
            //obj_SearchParameterDTO.Language = _callContext.LanguageCode;
            //obj_SearchParameterDTO.CountryID = 10000;
            //obj_SearchParameterDTO.Sort = "";
            //obj_SearchParameterDTO.SiteID = siteID;
            //obj_SearchParameterDTO.ConversionRate = UtilityRepository.GetExchangeRate((int)_callContext.CompanyID, _callContext.CurrencyCode);


            //obj_SearchParameterDTO.FreeSearchFilterKey = "brandid";
            //obj_SearchParameterDTO.FreeSearchFilterValue = product1.BrandID.ToString();
            //var result = (new SearchDataBL(_callContext)).GetProductCatalog(obj_SearchParameterDTO);

            //foreach (var product in result.Catalogs)
            //{
            //    var dto = new RecomendedProductDTO();
            //    dto.ProductID = product.ProductID;
            //    dto.SkuID = product.SKUID;
            //    dto.ProductCode = product.ProductCode;
            //    dto.ProductName = product.ProductName;
            //    dto.ProductPrice = product.ProductPrice;
            //    dto.ProductDiscountPrice = product.ProductDiscountPrice;
            //    dto.ImageFile = product.ProductThumbnail;
            //    dto.ProductAvailableQuantity = product.ProductAvailableQuantity;
            //    dto.ProductActive = product.ProductActive;
            //    dto.ProductListingQuantity = product.ProductAvailableQuantity > 0 ? 1 : 0;
            //    dto.Currency = _callContext.CurrencyCode;
            //    productDTOList.Add(dto);
            //}

            ////var lists = new ProductCatalogRepository().GetProductsBySubCategory(categoryID);
            //return productDTOList;
            return null;
        }

        public List<ProductDTO> GetProductsbyCategoryBlock(long blockID)
        {
            //var lists = new ProductCatalogRepository().GetProductsbyCategoryBlock(blockID);
            //return lists;
            return null;
        }

        public List<ProductImageMapDTO> GetProductDetailImages(long skuID)
        {
            var productImageMapList = productCatalogRepository.GetProductDetailImages(skuID);
            var productImageMapDTOList = new List<ProductImageMapDTO>();

            foreach (var group in productImageMapList.GroupBy(x => x.Sequence))
            {
                var productImageMapDTO = new ProductImageMapDTO();

                foreach (var r in group)
                {
                    productImageMapDTO.ProductSKUMapID = (long)r.ProductSKUMapID;
                    if ((Eduegate.Services.Contracts.Enums.ImageTypes)r.ProductImageTypeID == Eduegate.Services.Contracts.Enums.ImageTypes.Large)
                    {
                        productImageMapDTO.ZoomImage = r.ImageFile;
                    }
                    else if ((Eduegate.Services.Contracts.Enums.ImageTypes)r.ProductImageTypeID == Eduegate.Services.Contracts.Enums.ImageTypes.Listing)
                    {
                        productImageMapDTO.ListingImage = r.ImageFile;
                    }
                    else if ((Eduegate.Services.Contracts.Enums.ImageTypes)r.ProductImageTypeID == Eduegate.Services.Contracts.Enums.ImageTypes.Small)
                    {
                        productImageMapDTO.GalleryImage = r.ImageFile;
                    }
                    else if ((Eduegate.Services.Contracts.Enums.ImageTypes)r.ProductImageTypeID == Eduegate.Services.Contracts.Enums.ImageTypes.Thumbnail)
                    {
                        productImageMapDTO.ThumbnailImage = r.ImageFile;
                    }
                }

                productImageMapDTOList.Add(productImageMapDTO);
            }

            return productImageMapDTOList;
        }

        public List<ProductDetailKeyFeatureDTO> GetProductKeyFeatures(long skuID,int cultureID=1)
        {
            if (_callContext.IsNotNull() && !string.IsNullOrEmpty(_callContext.LanguageCode))
            {
                var languageDTO = new UtilityBL().GetLanguageCultureId(_callContext.LanguageCode);
                cultureID = languageDTO.IsNotNull() ? languageDTO.CultureID : 1;
            }

            var productPropertyMapList = productCatalogRepository.GetProductKeyFeatures(skuID, cultureID);
            var productDetailKeyFeatureDTOList = new List<ProductDetailKeyFeatureDTO>();
            foreach (var productPropertyMap in productPropertyMapList)
            {
                var propertyTypeName = productPropertyMap.PropertyType.PropertyTypeName;
                var propertyValue = productPropertyMap.PropertyID.IsNull() ? productPropertyMap.Value : productPropertyMap.Property.PropertyName;
                var productDetailKeyFeatureDTO = new ProductDetailKeyFeatureDTO();
                productDetailKeyFeatureDTO.SKUID = skuID;
                productDetailKeyFeatureDTO.FeatureName = propertyTypeName;
                productDetailKeyFeatureDTO.FeatureValue = propertyValue;
                productDetailKeyFeatureDTO.PropertyTypeID = productPropertyMap.PropertyTypeID.HasValue ? productPropertyMap.PropertyTypeID.Value : default(byte);
                productDetailKeyFeatureDTO.PropertyIID = productPropertyMap.PropertyID.HasValue ? productPropertyMap.PropertyID.Value : default(long);
                productDetailKeyFeatureDTOList.Add(productDetailKeyFeatureDTO);
            }
            return productDetailKeyFeatureDTOList;
        }


        public List<ProductSKUVariantDTO> GetProductVariants(long skuID, long cultureID)
        {
            //var productSKUVariantList = productCatalogRepository.GetProductVariants(skuID, cultureID, _callContext.LanguageCode);
            //var productSKUVariantDTOList = new List<ProductSKUVariantDTO>();
            //var productPropertyMapFinalList = new List<ProductSKuVariantsCode>();
            //productPropertyMapFinalList.Add(productSKUVariantList.Where(a => a.ProductSKUMapIID == skuID).FirstOrDefault());
            //var firstProperty = productSKUVariantList.Where(a => a.ProductSKUMapIID == skuID).Select(a => a.PropertyTypeName).Distinct().FirstOrDefault();
            //var firstPropertyValue = productSKUVariantList.Where(a => a.ProductSKUMapIID == skuID && a.PropertyTypeName == firstProperty).Select(a => a.PropertyName).FirstOrDefault();
            //var distinctFirstProperty = productSKUVariantList.Where(a => a.PropertyTypeName == firstProperty).Select(b => b.PropertyName).Distinct().Count();
            ////var firstPropertyList = productSKUVariantList.Where(b => b.PropertyTypeName == firstProperty).Select(b => b.PropertyName).GroupBy(a => a.ToLower()).ToList().Take(distinctFirstProperty);
            //var firstPropertyList = from element in productSKUVariantList
            //                        where element.PropertyTypeName == firstProperty && element.PropertyName != firstPropertyValue
            //                        group element by element.PropertyName
            //                            into groups
            //                            select groups.Take(1);
            ////firstPropertyList = firstPropertyList.ToList();
            //var listofSKU = productSKUVariantList.Where(a => a.PropertyName == firstPropertyValue).Select(a => a.ProductSKUMapIID).ToList();
            //var listByPropertyValue = productSKUVariantList.Where(a => listofSKU.Contains(a.ProductSKUMapIID) && a.PropertyTypeName != firstProperty).ToList();

            //foreach (var list1 in firstPropertyList)
            //{
            //    foreach (var list in list1)
            //    {
            //        productPropertyMapFinalList.Add(list);
            //    }
            //}
            //foreach (var list in listByPropertyValue)
            //{
            //    productPropertyMapFinalList.Add(list);
            //}
            //foreach (var productSKUVariant in productPropertyMapFinalList)
            //{
            //    productSKUVariantDTOList.Add(ProductSKUVariantMapperCode.Mapper(_callContext).ToDTO(productSKUVariant));
            //}
            //return productSKUVariantDTOList;
            return null;
        }

        public ProductInventoryConfigDTO GetProductDetailsDescription(long skuID, long cultureID)
        {
            var productInventoryConfig = productCatalogRepository.GetProductDetailsDescription(skuID, cultureID, _callContext.LanguageCode);
            if (productInventoryConfig.IsNotNull())
            {
                if (_callContext.LanguageCode == "ar")
                {
                    cultureID = new UtilityBL().GetLanguageCultureId(_callContext.LanguageCode).CultureID;
                    var cultureConfig = productCatalogRepository.GetProductDescriptionCultureID(skuID, cultureID);
                    productInventoryConfig.Details = cultureConfig.Details;
                    productInventoryConfig.Description = cultureConfig.Description;
                }
                var productInventoryConfigDTO = ProductInventoryConfigMapper.ToDto(productInventoryConfig);
                return productInventoryConfigDTO;
            }
            return new ProductInventoryConfigDTO();
        }

        #region product sku and quantity view
        public Services.Contracts.SearchData.SearchResultDTO ProductSKUSearch(int pageIndex, int pageSize, string searchText, string searchVal,
            string searchBy, string sortBy, string pageType, bool isCategory)
        {
            int totalRecords = 0;
            var productSKU = productCatalogRepository.ProductSKUSearch(pageIndex, pageSize, searchText, searchVal,
            searchBy, sortBy, pageType, isCategory, out totalRecords);

            var searchDTO = new Services.Contracts.SearchData.SearchResultDTO();
            searchDTO.Catalogs = new List<SearchCatalogDTO>();
            searchDTO.CatalogGroups = new List<SearchCatalogGroupDTO>();
            string skuString = productSKU.Rows.Count == 0 ? null : productSKU.AsEnumerable()
                     .Select(row => row["ProductSKUMapIID"].ToString())
                     .Aggregate((s1, s2) => String.Concat(s1, "," + s2));
            var productInventory = new ProductDetailBL(_callContext).GetProductInventoryOnline(skuString, _callContext == null || _callContext.UserId.IsNull() 
                ? (long?)null : Convert.ToInt64(_callContext.UserId));

            foreach (DataRow sku in productSKU.Rows)
            {
                var inventorySKU = productInventory.Where(a => a.ProductSKUMapID == long.Parse(sku["ProductSKUMapIID"].ToString())).FirstOrDefault();
                var productImages = new ProductDetailBL().GetProductImages(long.Parse(sku["ProductSKUMapIID"].ToString()));
                var thumbNail = productImages.Where(a => a.ProductImageTypeID == 1).FirstOrDefault();
                var listImage = productImages.Where(a => a.ProductImageTypeID == 8).FirstOrDefault();

                var catalog = new SearchCatalogDTO()
                {
                    ProductName = sku["SKU"].ToString(),
                    ProductID = long.Parse(sku["ProductIID"].ToString()),
                    SKUID = long.Parse(sku["ProductSKUMapIID"].ToString()),
                    ProductThumbnail = thumbNail == null ? "img/noimage5.png" : string.Format("{0}/Products/{1}", ImageRootUrl, thumbNail.ImageFile),
                    ProductListingImage = listImage == null ? "img/noimage5.png" : string.Format("{0}/Products/{1}", ImageRootUrl, listImage.ImageFile),
                    CurrencyCode = _callContext == null? null : _callContext.CurrencyCode
                };

                if(inventorySKU != null)
                {
                    catalog.ProductPrice = inventorySKU.ProductPricePrice;
                    catalog.ProductDiscountPrice = inventorySKU.ProductDiscountPrice;
                    catalog.ProductAvailableQuantity = int.Parse(inventorySKU.Quantity.ToString());
                }           

                searchDTO.CatalogGroups.Add(new SearchCatalogGroupDTO()
                {
                    Catalogs = new List<SearchCatalogDTO>() {
                       catalog
                    },

                    SelectedCatalog = catalog,
                    CatalogCount = 1,
                });
            }

            searchDTO.TotalProductsCount = totalRecords;
            return searchDTO;
        }

        public List<POSProductDTO> ProductSKUSearch(string searchText, int dataSize, string documentTypeID)
        {
            var products = new List<POSProductDTO>();
            long? productTypeID = null;
            if (documentTypeID != "" && documentTypeID != null)
            {
                MutualRepository mutualRepository = new MutualRepository();
                var settingValue = mutualRepository.GetSettingData("SERVICE_DOCUMENT_TYPE_IDs");

                if (settingValue != null)
                {
                    if (settingValue.SettingValue == documentTypeID)
                    {
                        productTypeID = 4;
                    }
                }
            }
            var productList = productCatalogRepository.ProductSKUSearch(searchText, dataSize, productTypeID);
            var productDetailRepository = new ProductDetailRepository();

            foreach (DataRow product in productList.Rows)
            {
                var skuIID = long.Parse(product["ProductSKUMapIID"].ToString());
                var price = productDetailRepository.GetPriceDetailsSKUID(0, skuIID, 0, "");
                var productIID = long.Parse(product["ProductIID"].ToString());

                products.Add(new POSProductDTO()
                {
                    ProductIID = productIID,
                    ProductSKUMapIID = skuIID,
                    SKU = product["ProductName"].ToString() + "-" + product["SKU"].ToString(),
                    ProductPrice = price.ProductDiscountPrice,
                    CostPrice = price.ProductCostPrice,
                    UnitPrice = price.ProductPricePrice,
                    TaxAmount = string.IsNullOrEmpty(product["TaxAmount"].ToString()) ? (decimal?)null : decimal.Parse(product["TaxAmount"].ToString()),
                    TaxPercentage = string.IsNullOrEmpty(product["TaxPercentage"].ToString()) ? (int?)null : int.Parse(product["TaxPercentage"].ToString()),
                    HasTaxInclusive = string.IsNullOrEmpty(product["HasTaxInclusive"].ToString()) ? false : bool.Parse(product["HasTaxInclusive"].ToString()),
                    TaxTemplateID = string.IsNullOrEmpty(product["TaxTemplateID"].ToString()) ? 0 : int.Parse(product["TaxTemplateID"].ToString()),
                    Barcode = product["BarCode"].ToString(),
                    PartNo = product["PartNo"].ToString(),
                });
            }

            return products;
        }

        public List<POSProductDTO> ProductSKUByCategoryID(long categoryID, int dataSize)
        {
            var products = new List<POSProductDTO>();

            var productList = productCatalogRepository.ProductSKUByCategoryID(categoryID, dataSize);
            var productDetailRepository = new ProductDetailRepository();
            
            foreach (ProductSKUMap sku in productList)
            {
                var price = productDetailRepository.GetPriceDetailsSKUID(20, sku.ProductSKUMapIID, 0, "");

                products.Add(new POSProductDTO()
                {
                    ProductIID = sku.ProductID.Value,
                    ProductSKUMapIID = sku.ProductSKUMapIID,
                    SKU = sku.SKUName,
                    ProductPrice = price.ProductDiscountPrice,
                    CostPrice = price.ProductCostPrice,
                    UnitPrice = price.ProductPricePrice,
                });
            }

            return products;
        }

        public POSProductDTO GetProductSKUInventoryDetail(long skuIID,Services.Contracts.Enums.DocumentReferenceTypes referenceType = 0, long branchID = 0, int companyID = 0)
        {
            var productSkuDTO = new POSProductDTO();

            var productSkuDetail = productCatalogRepository.GetProductSKUInventoryDetail(companyID > 0 ? companyID : _callContext.CompanyID.Value, skuIID, branchID);

            if (productSkuDetail == null)
            {
                productSkuDetail = productCatalogRepository.GetProductSKUDetail(skuIID);
            }

            if (productSkuDetail.IsNotNull())
            {
                productSkuDTO = POSProductMapper.Mapper.ToDTO(productSkuDetail);
                // If caller passes optional parameters then
                if (referenceType != Services.Contracts.Enums.DocumentReferenceTypes.All && branchID != 0)
                {
                    productSkuDTO.CostPrice = productCatalogRepository.GetCostPrice(skuIID, referenceType, branchID);
                    productSkuDTO.UnitPrice = productCatalogRepository.GetUnitPrice(skuIID, referenceType, branchID);
                    var productDetails = productCatalogRepository.GetUnitIDs(skuIID);
                    var sellingPriceDetail = new ProductDetailBL(_callContext).GetProductInventoryOnline(skuIID.ToString()).FirstOrDefault();
                    productSkuDTO.ProductPrice = sellingPriceDetail.IsNotNull() ? (sellingPriceDetail.ProductDiscountPrice.IsNotNull() && sellingPriceDetail.ProductDiscountPrice > 0 ? sellingPriceDetail.ProductDiscountPrice : sellingPriceDetail.ProductPricePrice) : 0;
                    //productSkuDTO.ProductPrice = productCatalogRepository.GetProductSellingPrice(skuIID); // Created new method to get selling price for SKU
                    productSkuDTO.PurchaseUnitID = productDetails.PurchaseUnitID.HasValue ? productDetails.PurchaseUnitID : null;
                    productSkuDTO.SellingUnitID = productDetails.SellingUnitID.HasValue ? productDetails.SellingUnitID : null;
                    productSkuDTO.PurchaseUnitGroupID = productDetails.PurchaseUnitGroupID;
                    productSkuDTO.SellingUnitGroupID = productDetails.SellingUnitGroupID;
                    productSkuDTO.ProductCode = productDetails.ProductCode;
                }
            }

            return productSkuDTO;
            //return null;
        }

        public Eduegate.Services.Contracts.ProductDetail.ProductSKUDetailDTO GetProductBySKU(long skuID, long cultureID)
        {
            var productDetailDTO = new Eduegate.Services.Contracts.ProductDetail.ProductSKUDetailDTO();

            if(_callContext == null)
            {
                _callContext = new CallContext() { CompanyID = 1, LanguageCode = "en" };
            }
            if (_callContext.LanguageCode == "ar")
            {
                cultureID = new UtilityBL().GetLanguageCultureId(_callContext.LanguageCode).CultureID;
            }

            var productSKUDetail = productCatalogRepository.GetProductBySKU(skuID, cultureID, _callContext.LanguageCode = "en", _callContext != null && _callContext.CompanyID.HasValue ? (int)_callContext.CompanyID : 1);

            if (productSKUDetail.IsNotNull())
            {
                var productOnlineInventoryList = new ProductDetailBL(_callContext).GetProductInventoryOnline(skuID.ToString(), null);
                var productOnlineInventory = (from productInventoryDetails in productOnlineInventoryList
                                              where productInventoryDetails.ProductSKUMapID == skuID
                                              select productInventoryDetails).FirstOrDefault();

                productDetailDTO = ProductSKUDetailMapper.Mapper(_callContext).ToDTO(productSKUDetail);
                // commented repo call for below line to get price and discounted price as list page, please undo(uncomment below 1 line and comment next 2) if wrong; Apologies in advance
                //productDetailDTO.ProductDiscountPrice = productCatalogRepository.GetProductPrice(skuID);

                if (productOnlineInventory != null)
                {
                    productDetailDTO.ProductPrice = productOnlineInventory.ProductPricePrice == 0 ? (productSKUDetail.ProductPrice != null ? Convert.ToDecimal(productSKUDetail.ProductPrice) : 0) : productOnlineInventory.ProductPricePrice;
                    productDetailDTO.ProductDiscountPrice = productOnlineInventory.ProductDiscountPrice; // productCatalogRepository.GetProductPrice(skuID);
                }

                productDetailDTO.BrandName = productCatalogRepository.GetBrandbySkuID(skuID, cultureID);

                productDetailDTO.BrandID = productCatalogRepository.GetBrandIDbySKUID(skuID);
                productDetailDTO.ProductAvailableQuantity = productOnlineInventory == null ? 0 : Convert.ToInt64(productOnlineInventory.Quantity);
                productDetailDTO.ProductListingQuantity = productDetailDTO.ProductAvailableQuantity > 0 ? 1 : 0;
                productDetailDTO.BranchID = productOnlineInventory == null ? default(long?) : productOnlineInventory.BranchID;
                //var defaultCurrency = _callContext.CurrencyCode;
                productDetailDTO.Currency = _callContext.CurrencyCode;
                productDetailDTO.IsWishList = GetProductWishList(skuID,Convert.ToInt64(_callContext.UserId));
            }

            return productDetailDTO;
        }

        #endregion
        public List<ProductDetailDeliveryOption> GetProductDeliveryTypeList(long skuID, long cultureID,long branchID=0,int companyID=0)
        {
            var productDetailDeliveryOptionDTO = new List<ProductDetailDeliveryOption>();
            int productType = productCatalogRepository.GetProductType(skuID);
            var deliveryTypes = GetProductDeliveryType(skuID, companyID > 0 ? companyID : _callContext.CompanyID.Value, branchID);
            if (deliveryTypes != null)
            {
                foreach (var item in deliveryTypes.AsEnumerable())
                {
                    var productList = new ProductDetailDeliveryOption();
                    productList.SkuID = Convert.ToInt64(item["ProductSKUMapID"]);
                    productList.ProductID = Convert.ToInt64(item["ProductID"]);
                    productList.DeliveryDays = Convert.ToInt64(item["DeliveryDays"]);
                    productList.DeliveryOption = Convert.ToString(item["DeliveryTypeName"]);
                    productList.DeliveryTypeID = Convert.ToInt64(item["DeliveryTypeID"]);
                    productList.DisplayRange = Convert.ToInt64(item["DisplayRange"]);
                    productList.DeliveryOptionDisplayText = DeliveryTypeText(productList.SkuID, productList.DeliveryTypeID, productType, productList.DeliveryDays, cultureID, productList.DisplayRange);
                    if (!string.IsNullOrEmpty(productList.DeliveryOptionDisplayText.Trim()))
                    {
                        productDetailDeliveryOptionDTO.Add(productList);
                    }
                }
            }

            return productDetailDeliveryOptionDTO;
        }

        public DataTable GetProductDeliveryType(long skuID, int companyID, long branchID = 0)
        {
            return productCatalogRepository.GetProductDeliveryTypeList(skuID,companyID, branchID);
        }

        public string DeliveryTypeText(long skuID, long deliveryTypeID, long productTypeID, long deliveryDays, long cultureID, long displayRange)
        {
            var langCode = _callContext.IsNotNull() && _callContext.LanguageCode.IsNotNull() ? _callContext.LanguageCode : cultureID > 0 ? new MutualBL(_callContext).GetCultureCode(Convert.ToInt32(cultureID)):string.Empty;
            if (productTypeID == 2)
            {
                if (deliveryTypeID == 1 && skuID > 0)
                {
                    return ResourceHelper.GetValue("ExpressDelivery", langCode);
                }
                else if (deliveryTypeID == 1 && skuID == 0)
                {
                    return ResourceHelper.GetValue("NotEligibleForExpressDelivery", langCode);
                }

                if (deliveryTypeID == 2 && skuID > 0)
                {
                    return ResourceHelper.GetValue("SuperExpressDelivery", langCode);
                }
                else if (deliveryTypeID == 2 && skuID == 0)
                {
                    return ResourceHelper.GetValue("NotEligibleForSuperExpressDelivery", langCode);
                }

                if (deliveryTypeID == 5 && skuID > 0)
                {
                    var strDeliveryText = Convert.ToString(displayRange) != "0" ? deliveryDays.ToString() + "-" + (Convert.ToInt32(deliveryDays) + Convert.ToInt32(displayRange)).ToString() : deliveryDays.ToString();
                    return ResourceHelper.GetValue("DeliveryWithinDays1", langCode) + strDeliveryText + ResourceHelper.GetValue("DeliveryWithinDays2", langCode);
                }
                else if (deliveryTypeID == 3 && skuID>0)
                {
                    return ResourceHelper.GetValue("NextDayDelivery", langCode);
                }
                else if (deliveryTypeID == 3 && skuID == 0) { return ResourceHelper.GetValue("NotAvailableForNextDayDelivery", langCode); }
               

                if (deliveryTypeID == 4 && skuID > 0)
                {
                    return ResourceHelper.GetValue("AvailableForStorePickup", langCode);
                }
                else if (deliveryTypeID == 4 && skuID == 0)
                {
                    return ResourceHelper.GetValue("NotAvailableForStorePickup", langCode);
                }

                if (deliveryTypeID == 8 && skuID > 0)
                {
                    if (deliveryDays > 3)
                    {
                        return ResourceHelper.GetValue("SameDayShipping", langCode);
                    }
                    else if (deliveryDays <= 3 && deliveryDays != 0)
                    {
                        return ResourceHelper.GetValue("ShippingWithin1", langCode) + deliveryDays + ResourceHelper.GetValue("ShippingWithin2", langCode);
                    }
                    else if (deliveryDays <= 3 && deliveryDays == 0)
                    {
                        return ResourceHelper.GetValue("SameDayShipping", langCode);
                    }
                }
                else if (deliveryTypeID == 8 && skuID == 0)
                {
                    return ResourceHelper.GetValue("NotEligibleForInternationalShipping", langCode);
                }
            }
            else if (productTypeID == 1)
            {
                if ((deliveryTypeID == 6 || deliveryTypeID == 8) && skuID > 0)
                {
                    return ResourceHelper.GetValue("DeliveryByEmailOnly");
                }
                else if (deliveryTypeID == 8 && skuID == 0)
                {
                    return ResourceHelper.GetValue("NotEligibleForInternationalShipping", langCode);
                }
            }
            return "";
        }

        public byte ProductSkuIDActiveCheck(long skuID)
        {
            return productCatalogRepository.ProductSkuIDActiveCheck(skuID);
            //return default(byte);
        }

        public ProductSKUDetailDTO GetProductDetailsEmarsys(long skuID, long cultureID)
        {
            var productDetailSKU = GetProductBySKU(skuID, cultureID);
            var productImageDTO = GetProductDetailImages(skuID).FirstOrDefault();
            if (productImageDTO.IsNotNull())
            {
                productDetailSKU.ProductListingImage = productImageDTO.ListingImage;
                productDetailSKU.ProductThumbnailImage = productImageDTO.ThumbnailImage;
            }
            return productDetailSKU;
        }

        public List<ProductSKUDetailDTO> GetActiveDealProducts(long branchID, long noOfProducts)
        {
            //var productSKUDetailList = new List<ProductSKUDetailDTO>();
            //var productlist = productCatalogRepository.GetActiveDealProducts(branchID, noOfProducts);
            ////int productType = productCatalogRepository.GetProductType(skuID);
            //var productSKUs = "";
            //foreach (var item in productlist.AsEnumerable())
            //{
            //    if (productSKUs == "")
            //    {
            //        productSKUs = item["ProductSKUID"].ToString();
            //    }
            //    else
            //    {
            //        productSKUs = string.Concat(productSKUs, ",", item["ProductSKUID"]);
            //    }
            //}
            //var productPrice = new ProductDetailRepository().GetInventoryDetails(productSKUs, string.IsNullOrEmpty(_callContext.UserId) ? 0 : long.Parse(_callContext.UserId), "", _callContext);

            //foreach (var product in productlist.AsEnumerable())
            //{

            //    //var productDealDTO = new ProductDealDTO();
            //    //productDealDTO.productPriceList = new ProductPriceDTO();
            //    //productDealDTO.products = new List<ProductSKUDetailDTO>();
            //    //var pricelist = new ProductPriceDTO();
            //    //var rows = from row in productlist.AsEnumerable()
            //    //           where row.Field<long>("ProductPriceListID") == long.Parse(product["ProductPriceListIID"].ToString())
            //    //           select row;
            //    //foreach (var item in rows)
            //    //{
            //    //    var productSKU = GetProductBySKU(long.Parse(item["ProductSKUID"].ToString()), 1);

            //    //    //var price = productPrice.AsEnumerable().Where(r => r.Field<string>("ProductSKUID").Contains(item["ProductSKUID"].ToString()));
            //    //    //var finalprice = price.AsEnumerable().FirstOrDefault()["ProductDiscountPrice"];

            //    //    if (productSKU.IsNotNull())
            //    //    {
            //    //        foreach (var price in productPrice.AsEnumerable())
            //    //        {
            //    //            if (price["ProductSKUMapID"].ToString() == item["ProductSKUID"].ToString())
            //    //            {
            //    //                productSKU.ProductDiscountPrice = decimal.Parse(price["ProductDiscountPrice"].ToString());
            //    //                break;
            //    //            }
            //    //        }

            //    //        productDealDTO.products.Add(productSKU);
            //    //    }
            //    //}
            //    //productDealDTO.productPriceList.EndDate = Convert.ToDateTime(product["EndDate"]);
            //    //productDealDTO.productPriceList.StartDate = Convert.ToDateTime(product["StartDate"]);
            //    //productDealDTO.productPriceList.ProductPriceListIID = long.Parse(product["ProductPriceListIID"].ToString());
            //    //productDealDTO.productPriceList.PriceDescription = product["PriceDescription"].ToString();
            //    //productSKUDetailList.Add(productDealDTO);
            //    var productSKU = GetProductBySKU(long.Parse(product["ProductSKUID"].ToString()), 1);
            //    if (productSKU.IsNotNull())
            //    {
            //        foreach (var price in productPrice.AsEnumerable())
            //        {
            //            if (price["ProductSKUMapID"].ToString() == product["ProductSKUID"].ToString())
            //            {
            //                productSKU.ProductDiscountPrice = decimal.Parse(price["ProductDiscountPrice"].ToString());
            //                productSKU.ProductPrice = decimal.Parse(price["ProductPrice"].ToString());
            //                break;
            //            }
            //        }
            //        var image = "";
            //        try
            //        {
            //            var imageList = GetProductDetailImages(long.Parse(product["ProductSKUID"].ToString()));
            //            if (imageList.IsNotNull())
            //            {
            //                image = imageList.Select(a => a.GalleryImage).FirstOrDefault();
            //            }
            //        }
            //        catch (Exception ex) { }
            //        productSKU.ProductListingImage = image;
            //        productSKU.StartDate = Convert.ToDateTime(product["StartDate"]).ToString();
            //        productSKU.EndDate = Convert.ToDateTime(product["EndDate"]).ToString();
            //        productSKU.ServerCurrentTime = DateTime.Now.ToString("G");
            //        productSKUDetailList.Add(productSKU);
            //    }

            //}

            //return productSKUDetailList;
            return null;
        }

        public List<ProductVideoMapDTO> GetProductVideos(long skuID)
        {
            //var productVideoMapDTOList = new List<ProductVideoMapDTO>();
            //var productVideoList = productCatalogRepository.GetProductVideos(skuID);
            //foreach (var video in productVideoList)
            //{
            //    productVideoMapDTOList.Add(ProductVideoMapMapper.ToDto(video));
            //}
            //return productVideoMapDTOList;
            return null;
        }


        public List<ProductSKUDetailDTO> GetProductByTag(List<string> tagName, string cultureCode, int noOfRecords, long categoryID, int siteID)
        {
            //var pageIndex = 1;
            //var searchText = "*:*";

            //var joinedTagName = string.Join(",", tagName);

            //var searchSolrParameter = new SearchParameterDTO()
            //{
            //    FreeSearch = searchText,
            //    PageIndex = pageIndex,
            //    PageSize = 10000,
            //    Language = _callContext.LanguageCode,
            //    Sort = "",
            //    ConversionRate = 1,
            //    SiteID = siteID,
            //    FreeSearchFilterKey = "skutags",
            //    FreeSearchFilterValue = joinedTagName
            //};
            //var dtoList = new List<ProductSKUDetailDTO>();
            //var dtoSolr = new SearchDataBL(this._callContext).GetProductCatalog(searchSolrParameter);
            //if (dtoSolr.IsNotNull() && dtoSolr.Catalogs.IsNotNull())
            //{
            //    var dtoFinalSolr = dtoSolr.Catalogs.Where(a => a.ProductCategoryAll.Contains(categoryID.ToString())).ToList();
            //    if(dtoFinalSolr.IsNotNull() && dtoFinalSolr.Count > 0)
            //    {
            //        dtoFinalSolr = dtoFinalSolr.OrderBy(a => Guid.NewGuid()).ToList();
            //        if(noOfRecords >= 0)
            //        {
            //            dtoFinalSolr = dtoFinalSolr.Take(noOfRecords).ToList();
            //        }
            //    }
            //    foreach (var sku in dtoFinalSolr)
            //    {
            //        var dto = SolrToProductSKUDetailMapper.Mapper(_callContext).ToDTO(sku);
            //        dto.ProductDiscountPrice = new ProductDetailRepository().GetInventoryDetailsSKUID(sku.SKUID, CustomerID: string.IsNullOrEmpty(_callContext.UserId) ? 0 : long.Parse(_callContext.UserId), type: true,CompanyID: _callContext.CompanyID.HasValue?(int)_callContext.CompanyID.Value:default(int)).ProductDiscountPrice;
            //        dto.Currency = _callContext.CurrencyCode;
            //        dto.ProductListingImage = GetProductDetailImages(sku.SKUID).Select(a => a.ThumbnailImage).FirstOrDefault();
            //        dtoList.Add(dto);
            //    }
            //}
            ////var skus = productCatalogRepository.GetProductByTag(tagName, cultureCode, noOfRecords, categoryID);


            //////var userCart = new ShoppingCartRepository().IsUserCartExist(_callContext.UserId, (int)Eduegate.Framework.Helper.Enums.ShoppingCartStatus.InProcess);
            ////foreach (var sku in skus)
            ////{
            ////    var dto = ProductSKUDetailMapper.Mapper(_callContext).ToDTO(sku);
            ////    dto.ProductDiscountPrice = new ProductDetailRepository().GetInventoryDetailsSKUID(sku.ProductSKUMapIID, CustomerID: string.IsNullOrEmpty(_callContext.UserId) ? 0 : long.Parse(_callContext.UserId), type: true).ProductDiscountPrice;
            ////    dto.Currency = _callContext.CurrencyCode;
            ////    dto.ProductListingImage = GetProductDetailImages(sku.ProductSKUMapIID).Select(a => a.ThumbnailImage).FirstOrDefault();
            ////    dtoList.Add(dto);
            ////}
            //return dtoList;
            return null;
        }

        public List<ProductSKUDetailDTO> GetProductByTagWithoutCategory(List<string> tagName, string cultureCode, int noOfRecords)
        {
            //var skus = productCatalogRepository.GetProductByTagWithoutCategory(tagName, cultureCode, noOfRecords);

            //var dtoList = new List<ProductSKUDetailDTO>();
            ////var userCart = new ShoppingCartRepository().IsUserCartExist(_callContext.UserId, (int)Eduegate.Framework.Helper.Enums.ShoppingCartStatus.InProcess);
            //foreach (var sku in skus)
            //{
            //    var dto = ProductSKUDetailMapper.Mapper(_callContext).ToDTO(sku);
            //    dto.ProductDiscountPrice = new ProductDetailRepository().GetInventoryDetailsSKUID(sku.ProductSKUMapIID, CustomerID: string.IsNullOrEmpty(_callContext.UserId) ? 0 : long.Parse(_callContext.UserId), type: true).ProductDiscountPrice;
            //    dto.Currency = _callContext.CurrencyCode;
            //    dto.ProductListingImage = GetProductDetailImages(sku.ProductSKUMapIID).Select(a => a.ThumbnailImage).FirstOrDefault();
            //    dtoList.Add(dto);
            //}
            //return dtoList;
            return null;
        }

        public List<ProductMapDTO> ProductSearch(string searchText, int dataSize)
        {
            //var products = new List<ProductMapDTO>();

            //var productList = productCatalogRepository.ProductSearch(searchText, dataSize);

            //foreach (DataRow product in productList.Rows)
            //{
            //    products.Add(new ProductMapDTO()
            //    {
            //        ProductID = long.Parse(product["ProductIID"].ToString()),
            //        ProductName = product["ProductName"].ToString(),
            //    });
            //}

            //return products;
            return null;
        }

        public void SetProductSKUSiteMap(long productSKUMapID, int siteID, bool isActive)
        {
            //productCatalogRepository.SetProductSKUSiteMap(productSKUMapID,siteID,isActive);
        }

        public bool GetProductWishList(long skuID,long? customerID)
        {
            //var 
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                //var customer = dbContext.Customers.FirstOrDefault(a => a.LoginID == Convert.ToInt64(_callContext.UserId));

                //var customerID = customer.CustomerIID;

                var wishList = (from w in dbContext.WishLists
                               where w.SKUID == skuID && w.CustomerID == customerID
                                select (w.IsWishList)).FirstOrDefault();
                return wishList;
            }
        
        }


    }
}

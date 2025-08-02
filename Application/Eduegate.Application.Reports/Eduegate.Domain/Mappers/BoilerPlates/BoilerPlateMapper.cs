using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity.Models;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Domain.Entity.Logging.Models;
using Eduegate.Framework.Contracts.Common.BoilerPlates;
using Eduegate.Domain.Repository.ValueObjects;
using Eduegate.Services.Contracts.Mutual;

namespace Eduegate.Domain.Mappers.BoilerPlates
{
    public partial class BoilerPlateMapper
    {
        public static BoilerPlateMapper Mapper { get { return new BoilerPlateMapper(); } }

        public BoilerPlateResultSetDTO GetAllCategoriesHomePageBoilerPlate(Category category)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "ParentCategory";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "CategoryIID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ParentCategoryID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "CategoryName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "CategoryCode" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ImageName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ThumbnailImageName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "IsActive" });
            boilerPlateResultSetDTO.IsArray = false;

            var thumbnail = category.CategoryImageMaps.Where(a => a.ImageTypeID == (byte)ImageTypes.Thumbnail).FirstOrDefault();

            if (category != null)
            {
                boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          category.CategoryIID,
                          category.ParentCategoryID,
                          category.CategoryName,
                          category.CategoryCode,
                          category.ImageName,
                          thumbnail == null ? string.Empty : thumbnail.ImageFile,
                          category.IsActive
                        }
                });
            }

            return boilerPlateResultSetDTO;
        }
        public BoilerPlateResultSetDTO GetAllSubCategoriesHomePageBoilerPlate(List<Category> categoryList)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "SubCategory";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "CategoryIID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ParentCategoryID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "CategoryName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "CategoryCode" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ListingImageName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ListingImageTitle" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ListingLargeImageName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ListingLargeImageTitle" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ThumbnailImageName" });

            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "IsActive" });
            boilerPlateResultSetDTO.IsArray = true;

            if (categoryList != null && categoryList.Count > 0)
            {
                foreach (var category in categoryList)
                {
                    var thumbnail = category.CategoryImageMaps.Where(a => a.ImageTypeID == (byte)ImageTypes.Thumbnail).FirstOrDefault();
                    var categoryListImage = category.CategoryImageMaps.Where(a => a.ImageTypeID == (byte)ImageTypes.Listing).FirstOrDefault();
                    var categoryListLargeImage = category.CategoryImageMaps.Where(a => a.ImageTypeID == (byte)ImageTypes.Listing_Large).FirstOrDefault();

                    boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          category.CategoryIID,
                          category.ParentCategoryID,
                          category.CategoryName,
                          category.CategoryCode,
                          categoryListImage == null ? string.Empty : categoryListImage.ImageFile,
                          categoryListImage == null ? string.Empty : categoryListImage.ImageTitle,
                          categoryListLargeImage == null ? string.Empty : categoryListLargeImage.ImageFile,
                          categoryListLargeImage == null ? string.Empty : categoryListLargeImage.ImageTitle,
                          thumbnail == null ? string.Empty : thumbnail.ImageFile,
                          category.IsActive
                        }
                    });
                }
            }

            return boilerPlateResultSetDTO;
        }
        public BoilerPlateResultSetDTO GetAllCategoriesBannerBoilerPlate(List<CategoryImageMap> categoryImageMapList,string rootUrl)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "ParentCategoryBanner";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "CategoryImageMapIID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "CategoryID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ImageTypeID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ImageFile" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ImageTitle" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ImageLink" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ImageTarget" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ActionLinkTypeID" });
            boilerPlateResultSetDTO.IsArray = true;
            if (categoryImageMapList != null && categoryImageMapList.Count > 0)
            {
                foreach (var categoryImageMap in categoryImageMapList)
                {
                    var url = RootUrlMapping(rootUrl, categoryImageMap.ImageLinkParameters != null ? categoryImageMap.ImageLinkParameters : "");
                    boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                            categoryImageMap.CategoryImageMapIID,
                            categoryImageMap.CategoryID,
                            categoryImageMap.ImageTypeID,categoryImageMap.ImageFile,
                            categoryImageMap.ImageTitle,
                            url + categoryImageMap.ImageLinkParameters,
                            categoryImageMap.ImageTarget,
                            categoryImageMap.ActionLinkTypeID
                        }
                    });
                }
            }

            return boilerPlateResultSetDTO;
        }

        public BoilerPlateResultSetDTO GetTopAllSubCategoriesHomePageBoilerPlate(List<Category> categoryList)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "TopSubCategory";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "CategoryIID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ParentCategoryID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "CategoryName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "CategoryCode" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ListingImageName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ListingImageTitle" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ListingLargeImageName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ListingLargeImageTitle" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ThumbnailImageName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "IsActive" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ListingSmallImageName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ListingSmallImageTitle" });
            boilerPlateResultSetDTO.IsArray = true;
            if (categoryList != null && categoryList.Count > 0)
            {
                foreach (var category in categoryList)
                {
                    var thumbnail = category.CategoryImageMaps.Where(a => a.ImageTypeID == (byte)ImageTypes.Thumbnail).FirstOrDefault();
                    var categoryListImage = category.CategoryImageMaps.Where(a => a.ImageTypeID == (byte)ImageTypes.Listing).FirstOrDefault();
                    var categoryListLargeImage = category.CategoryImageMaps.Where(a => a.ImageTypeID == (byte)ImageTypes.Listing_Large).FirstOrDefault();
                    var categoryListSmallImage = category.CategoryImageMaps.Where(a => a.ImageTypeID == (byte)ImageTypes.Listing_Small).FirstOrDefault();
                    boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          category.CategoryIID,
                          category.ParentCategoryID,
                          category.CategoryName,
                          category.CategoryCode,
                          categoryListImage == null ? string.Empty : categoryListImage.ImageFile,
                          categoryListImage == null ? string.Empty : categoryListImage.ImageTitle,
                          categoryListLargeImage == null ? string.Empty : categoryListLargeImage.ImageFile,
                          categoryListLargeImage == null ? string.Empty : categoryListLargeImage.ImageTitle,
                          thumbnail == null ? string.Empty : thumbnail.ImageFile,
                          category.IsActive,
                          categoryListSmallImage == null ? string.Empty : categoryListSmallImage.ImageFile,
                          categoryListSmallImage == null ? string.Empty : categoryListSmallImage.ImageFile,
                        }
                    });
                }
            }

            return boilerPlateResultSetDTO;
        }

        public BoilerPlateResultSetDTO GetBrandBoilerPlate(List<Brand> brandList)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "HomeCategoryBrand";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "BrandIID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "BrandCode" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "BrandName" });
            //boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "Descirption" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "LogoFile" });
            boilerPlateResultSetDTO.IsArray = true;
            if (brandList != null && brandList.Count > 0)
            {
                foreach (var brand in brandList)
                {
                    boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          brand.BrandIID,
                          brand.BrandCode,
                          brand.BrandName,
                          //brand.Descirption,
                          brand.LogoFile,
                        }
                    });
                }
            }

            return boilerPlateResultSetDTO;
        }

        public BoilerPlateResultSetDTO GetHomeBanner(List<Eduegate.Services.Contracts.BannerMasterDTO> bannerList,string rootUrl)
        {
           
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "HomeBanner";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "BannerIID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "BannerName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "BannerFile" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "BannerTypeID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "Link" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "Target" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "StatusID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "Frequency" });
            boilerPlateResultSetDTO.IsArray = true;
            if (bannerList != null)
            {
                foreach (var banner in bannerList)
                {
                    
                    var url = RootUrlMapping(rootUrl,banner.Link !=null?banner.Link:"");
                    boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          banner.BannerIID,
                          banner.BannerName,
                          banner.BannerFile,
                          banner.BannerTypeID,
                          url + banner.Link,
                          banner.Target,
                          banner.StatusID,
                          banner.Frequency,
                        }
                    });
                }
            }

            return boilerPlateResultSetDTO;
        }

       string RootUrlMapping(string RootUrl,string Link = "")
        {
            if (Link.Contains("http://")) { RootUrl = string.Empty; }
            return RootUrl;
        }

        public BoilerPlateResultSetDTO GetHomeSideBanner(List<Eduegate.Services.Contracts.BannerMasterDTO> bannerList,string rootUrl)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "HomeSideBanner";

            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "BannerIID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "BannerName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "BannerFile" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "BannerTypeID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "Link" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "Target" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "StatusID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "Frequency" });
            boilerPlateResultSetDTO.IsArray = true;
            if (bannerList != null)
            {
                foreach (var banner in bannerList)
                {
                    var url  = RootUrlMapping(rootUrl, banner.Link != null ? banner.Link : "");
                    boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          banner.BannerIID,
                          banner.BannerName,
                          banner.BannerFile,
                          banner.BannerTypeID,
                          url + banner.Link,
                          banner.Target,
                          banner.StatusID,
                          banner.Frequency,
                        }
                    });
                }
            }

            return boilerPlateResultSetDTO;
        }

        public BoilerPlateResultSetDTO GetHomeProductRecommendation(List<Eduegate.Services.Contracts.RecomendedProductDTO> productList , string languageCode, string title = "")
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "HomeProductRecommendation";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductCode" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductPrice" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductDiscountPrice" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ImageFile" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductActive" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "Currency" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "CurrencyDisplayText" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductAvailableQuantity" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductListingQuantity" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "SkuID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "Title" });
            boilerPlateResultSetDTO.IsArray = true;
            if (productList != null)
            {
                foreach (var product in productList)
                {
                    boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          product.ProductID,
                          product.ProductCode,
                          product.ProductName,
                          product.ProductPrice,
                          product.ProductDiscountPrice,
                          product.ImageFile,
                          product.ProductActive,
                          product.Currency,
                          Eduegate.Globalization.ResourceHelper.GetValue(product.Currency, languageCode),
                          product.ProductAvailableQuantity,
                          product.ProductListingQuantity,
                          product.SkuID,
                          title
                        }
                    });
                }

            }

            return boilerPlateResultSetDTO;
        }
        public BoilerPlateResultSetDTO GetTopProductByCategory(List<Eduegate.Services.Contracts.ProductDTO> products)
        {
            var product = products.FirstOrDefault();
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "TopProductCategory";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductCode" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductPrice" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductDiscountPrice" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ImageFile" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductActive" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductAvailableQuantity" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "Currency" });
            boilerPlateResultSetDTO.IsArray = false;
            if (product != null)
            {
                boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          product.ProductID,
                          product.ProductCode,
                          product.ProductName,
                          product.ProductPrice,
                          product.ProductDiscountPrice,
                          product.ImageFile,
                          product.ProductActive,
                          product.ProductAvailableQuantity,
                          product.Currency
                        }
                });


            }

            return boilerPlateResultSetDTO;
        }

        public BoilerPlateResultSetDTO GetProductsBySubCategory(List<Eduegate.Services.Contracts.ProductDTO> productList)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "ProductsBySubCategory";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductCode" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductPrice" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductDiscountPrice" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ImageFile" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductActive" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductAvailableQuantity" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "Currency" });
            boilerPlateResultSetDTO.IsArray = true;
            if (productList != null)
            {
                foreach (var product in productList)
                {
                    boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          product.ProductID,
                          product.ProductCode,
                          product.ProductName,
                          product.ProductPrice,
                          product.ProductDiscountPrice,
                          product.ImageFile,
                          product.ProductActive,
                          product.ProductAvailableQuantity,
                          product.Currency
                        }
                    });
                }

            }

            return boilerPlateResultSetDTO;
        }

        public BoilerPlateResultSetDTO GetCategoryTags(List<Eduegate.Services.Contracts.ProductTagDTO> categoryTagList)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "CategoryTag";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "TagIID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "TagName" });
            boilerPlateResultSetDTO.IsArray = true;
            if (categoryTagList != null)
            {
                foreach (var categoryTag in categoryTagList)
                {
                    boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                         categoryTag.TagIID,
                         categoryTag.TagName
                        }
                    });
                }

            }

            return boilerPlateResultSetDTO;
        }

        public BoilerPlateResultSetDTO GetCategoryBlockDetails(Eduegate.Services.Contracts.Catalog.CategoryDTO category)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "CategoryBlock";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "CategoryIID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ParentCategoryID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "CategoryName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "CategoryCode" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ImageName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ThumbnailImageName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "IsActive" });
            boilerPlateResultSetDTO.IsArray = false;
            if (category != null)
            {
                boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          category.CategoryIID,
                          category.ParentCategoryID,
                          category.CategoryName,
                          category.CategoryCode,
                          category.ImageName,
                          category.ThumbnailImageName,
                          category.IsActive
                        }
                });


            }

            return boilerPlateResultSetDTO;
        }
        public BoilerPlateResultSetDTO GetProductsbyCategoryBlock(List<Eduegate.Services.Contracts.ProductDTO> productList)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "CategoryBlockProducts";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductCode" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductPrice" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductDiscountPrice" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ImageFile" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductActive" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductAvailableQuantity" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "Currency" });
            boilerPlateResultSetDTO.IsArray = true;
            if (productList != null)
            {
                foreach (var product in productList)
                {
                    boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          product.ProductID,
                          product.ProductCode,
                          product.ProductName,
                          product.ProductPrice,
                          product.ProductDiscountPrice,
                          product.ImageFile,
                          product.ProductActive,
                          product.ProductAvailableQuantity,
                          product.Currency
                        }
                    });
                }

            }

            return boilerPlateResultSetDTO;
        }
        public BoilerPlateResultSetDTO GetProductDetails(Eduegate.Services.Contracts.ProductDetail.ProductSKUDetailDTO productDetail, string languageCode)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "ProductDetail";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "BrandName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductPrice" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductDiscountPrice" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductAvailableQuantity" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "Currency" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "CurrencyDisplayText" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "SKUID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductListingQuantity" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "PartNo" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "BrandCode" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "BrandName" });
            boilerPlateResultSetDTO.IsArray = false;
            if (productDetail != null && productDetail.ProductID > 0)
            {
                boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          productDetail.ProductID,
                          productDetail.SKUName,
                          productDetail.BrandName,
                          productDetail.ProductPrice,
                          productDetail.ProductDiscountPrice,
                          productDetail.ProductAvailableQuantity,
                          productDetail.Currency,
                          Globalization.ResourceHelper.GetValue(productDetail.Currency, languageCode),
                          productDetail.SKUID,
                          productDetail.ProductListingQuantity,
                          productDetail.ProductPartNo,
                          productDetail.BrandCode,
                          productDetail.BrandName
                        }
                });
            }
            return boilerPlateResultSetDTO;
        }
        public BoilerPlateResultSetDTO GetProductDetailImages(List<Eduegate.Services.Contracts.ProductImageMapDTO> productDetailImage)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "ProductDetailImages";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductSKUMapID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ZoomImage" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ListingImage" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "GalleryImage" });
            boilerPlateResultSetDTO.IsArray = true;
            if (productDetailImage != null)
            {
                foreach (var image in productDetailImage)
                {
                    boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          image.ProductSKUMapID,
                          image.ZoomImage,
                          image.ListingImage,
                          image.GalleryImage
                        }
                    });
                }
            }
            return boilerPlateResultSetDTO;
        }

        public BoilerPlateResultSetDTO GetProductDetailKeyFeatures(List<Eduegate.Services.Contracts.ProductDetail.ProductDetailKeyFeatureDTO> productFeatures)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "ProductDetailKeyFeatures";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "SKUID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "FeatureName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "FeatureValue" });
            boilerPlateResultSetDTO.IsArray = true;
            if (productFeatures != null)
            {
                foreach (var feature in productFeatures)
                {
                    boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          feature.SKUID,
                          feature.FeatureName,
                          feature.FeatureValue
                        }
                    });
                }
            }
            return boilerPlateResultSetDTO;
        }

        public BoilerPlateResultSetDTO GetProductDetailVariants(List<Eduegate.Services.Contracts.ProductDetail.ProductSKUVariantDTO> productVariants)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "ProductVariant";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductIID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductSKUMapIID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "PropertyName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "PropertyTypeName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductCode" });
            boilerPlateResultSetDTO.IsArray = true;
            if (productVariants != null)
            {
                foreach (var variant in productVariants)
                {
                    if (variant != null)
                    {
                        boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                        {
                            DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          variant.ProductIID,
                          variant.ProductSKUMapIID,
                          variant.PropertyName,
                          variant.PropertyTypeName,
                          variant.ProductCode
                        }
                        });
                    }
                }
            }
            return boilerPlateResultSetDTO;
        }
        public BoilerPlateResultSetDTO GetProductDetailDescription(ProductInventoryConfigDTO productInventoryConfig)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "ProductDetailDescription";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "Description" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "Details" });
            boilerPlateResultSetDTO.IsArray = false;
            if (productInventoryConfig != null)
            {
                boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          productInventoryConfig.Description,
                          productInventoryConfig.Details
                        }
                });

            }
            return boilerPlateResultSetDTO;
        }
        public BoilerPlateResultSetDTO GetProductDetailLocalDeliveryOptions(List<Eduegate.Services.Contracts.ProductDetail.ProductDetailDeliveryOption> productDeliveryOptions)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "ProductDetailLocalDeliveryOptions";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "SkuID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "DeliveryTypeID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "DeliveryOption" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "DeliveryOptionDisplayText" });
            boilerPlateResultSetDTO.IsArray = true;
            if (productDeliveryOptions != null)
            {
                foreach (var deliveryOption in productDeliveryOptions)
                {
                    boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          deliveryOption.ProductID,
                          deliveryOption.SkuID,
                          deliveryOption.DeliveryTypeID,
                          deliveryOption.DeliveryOption,
                          deliveryOption.DeliveryOptionDisplayText,
                        }
                    });
                }
            }
            return boilerPlateResultSetDTO;
        }
        public BoilerPlateResultSetDTO GetProductDetailIntlDeliveryOptions(List<Eduegate.Services.Contracts.ProductDetail.ProductDetailDeliveryOption> productDeliveryOptions)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "ProductDetailIntlDeliveryOptions";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "DeliveryOption" });
            boilerPlateResultSetDTO.IsArray = true;
            if (productDeliveryOptions != null)
            {
                foreach (var deliveryOption in productDeliveryOptions)
                {
                    boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          deliveryOption.ProductID,
                          deliveryOption.DeliveryOption
                        }
                    });
                }
            }
            return boilerPlateResultSetDTO;
        }

        public BoilerPlateResultSetDTO GetProductMultiPriceDetails(List<ProductMultiPriceDTO> multipriceList)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "ProductDetailMultiprice";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "GroupID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "GroupName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "MultipriceValue" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "isSelected" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "Currency" });

            boilerPlateResultSetDTO.IsArray = true;
            if (multipriceList != null)
            {
                foreach (var multiprice in multipriceList)
                {
                    boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          multiprice.GroupID,
                          multiprice.GroupName,
                          multiprice.MultipriceValue,
                          multiprice.isSelected,
                          multiprice.Currency
                        }
                    });
                }
            }
            return boilerPlateResultSetDTO;
        }

        public BoilerPlateResultSetDTO GetProductQtyDiscountDetails(List<ProductQuantityDiscountDTO> qtydiscountList)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "ProductDetailQtyDiscount";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "DiscountPercentage" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "Quantity" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "QtyPrice" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "Currency" });

            boilerPlateResultSetDTO.IsArray = true;
            if (qtydiscountList != null)
            {
                foreach (var qtydiscount in qtydiscountList)
                {
                    boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          qtydiscount.DiscountPercentage,
                          qtydiscount.Quantity,
                          qtydiscount.QtyPrice,
                          ""
                        }
                    });
                }
            }
            return boilerPlateResultSetDTO;
        }

        public BoilerPlateResultSetDTO GetRecentlyViewed(List<Eduegate.Services.Contracts.ProductDetail.ProductSKUDetailDTO> productList, string languageCode)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "HomeProductRecentlyViewed";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductPartNo" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "BrandName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductPrice" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductDiscountPrice" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductAvailableQuantity" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "Currency" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "CurrencyDisplayText" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "SkuID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductListingQuantity" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductListingImage" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductThumbnail" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductCode" });
            boilerPlateResultSetDTO.IsArray = true;
            if (productList != null)
            {
                foreach (var product in productList)
                {
                    boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          product.ProductID,
                          product.ProductPartNo,
                          product.ProductName,
                          product.BrandName,
                          product.ProductPrice,
                          product.ProductDiscountPrice,
                          product.ProductAvailableQuantity,
                          product.Currency,
                          Globalization.ResourceHelper.GetValue(product.Currency, languageCode),
                          product.SKUID,
                          product.ProductListingQuantity,
                          product.ProductListingImage,
                          product.ProductThumbnailImage,
                          product.ProductCode
                        }
                    });
                }

            }

            return boilerPlateResultSetDTO;
        }

        public BoilerPlateResultSetDTO GetCategoryRecommended(List<Eduegate.Services.Contracts.ProductDetail.ProductSKUDetailDTO> productList, string languageCode)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "CategoryProductRecommended";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductPartNo" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "BrandName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductPrice" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductDiscountPrice" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductAvailableQuantity" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "Currency" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "CurrencyDisplayText" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "SkuID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductListingQuantity" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductListingImage" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductThumbnail" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductCode" });
            boilerPlateResultSetDTO.IsArray = true;
            if (productList != null)
            {
                foreach (var product in productList)
                {
                    boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          product.ProductID,
                          product.ProductPartNo,
                          product.ProductName,
                          product.BrandName,
                          product.ProductPrice,
                          product.ProductDiscountPrice,
                          product.ProductAvailableQuantity,
                          product.Currency,
                          Globalization.ResourceHelper.GetValue(product.Currency, languageCode),
                          product.SKUID,
                          product.ProductListingQuantity,
                          product.ProductListingImage,
                          product.ProductThumbnailImage,
                          product.ProductCode
                        }
                    });
                }

            }

            return boilerPlateResultSetDTO;
        }

        public BoilerPlateResultSetDTO GetCategoryDepartment(List<Eduegate.Services.Contracts.ProductDetail.ProductSKUDetailDTO> productList)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "CategoryProductDepartment";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductPartNo" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "BrandName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductPrice" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductDiscountPrice" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductAvailableQuantity" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "Currency" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "SkuID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductListingQuantity" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductListingImage" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductThumbnail" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductCode" });
            boilerPlateResultSetDTO.IsArray = true;
            if (productList != null)
            {
                foreach (var product in productList)
                {
                    boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          product.ProductID,
                          product.ProductPartNo,
                          product.ProductName,
                          product.BrandName,
                          product.ProductPrice,
                          product.ProductDiscountPrice,
                          product.ProductAvailableQuantity,
                          product.Currency,
                          product.SKUID,
                          product.ProductListingQuantity,
                          product.ProductListingImage,
                          product.ProductThumbnailImage,
                          product.ProductCode
                        }
                    });
                }

            }

            return boilerPlateResultSetDTO;
        }
        public BoilerPlateResultSetDTO GetCategoryInterested(List<Eduegate.Services.Contracts.ProductDetail.ProductSKUDetailDTO> productList)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "CategoryProductInterested";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductPartNo" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "BrandName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductPrice" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductDiscountPrice" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductAvailableQuantity" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "Currency" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "SkuID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductListingQuantity" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductListingImage" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductThumbnail" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductCode" });
            boilerPlateResultSetDTO.IsArray = true;
            if (productList != null)
            {
                foreach (var product in productList)
                {
                    boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          product.ProductID,
                          product.ProductPartNo,
                          product.ProductName,
                          product.BrandName,
                          product.ProductPrice,
                          product.ProductDiscountPrice,
                          product.ProductAvailableQuantity,
                          product.Currency,
                          product.SKUID,
                          product.ProductListingQuantity,
                          product.ProductListingImage,
                          product.ProductThumbnailImage,
                          product.ProductCode
                        }
                    });
                }

            }

            return boilerPlateResultSetDTO;
        }

        public BoilerPlateResultSetDTO GetActiveDealProducts(List<Eduegate.Services.Contracts.ProductDetail.ProductSKUDetailDTO> productList)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "ActiveDealProducts";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductPartNo" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "BrandName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductPrice" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductDiscountPrice" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductAvailableQuantity" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "Currency" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "SKUID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductListingQuantity" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductListingImage" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "StartDate" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "EndDate" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductCode" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ServerCurrentTime" });
            boilerPlateResultSetDTO.IsArray = true;
            if (productList != null)
            {
                foreach (var product in productList)
                {
                    boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          product.ProductID,
                          product.ProductPartNo,
                          product.ProductName,
                          product.BrandName,
                          product.ProductPrice,
                          product.ProductDiscountPrice,
                          product.ProductAvailableQuantity,
                          product.Currency,
                          product.SKUID,
                          product.ProductListingQuantity,
                          product.ProductListingImage,
                          product.StartDate,
                          product.EndDate,
                          product.ProductCode,
                          DateTime.Now.ToString("G")
                        }
                    });
                }

            }

            return boilerPlateResultSetDTO;
        }

        public BoilerPlateResultSetDTO GetProductVideos(List<ProductVideoMapDTO> productVideoList)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "ProductVideos";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductVideoMapID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductSKUMapID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "VideoFile" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "Sequence" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "VideoName" });
            boilerPlateResultSetDTO.IsArray = true;
            if (productVideoList != null)
            {
                foreach (var video in productVideoList)
                {
                    boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          video.ProductVideoMapID,
                          video.ProductID,
                          video.ProductSKUMapID,
                          video.VideoFile,
                          video.Sequence,
                          video.VideoName
                        }
                    });
                }
            }
            return boilerPlateResultSetDTO;
        }

        public BoilerPlateResultSetDTO GetTopAllSubCategoriesHomePageFirstColBoilerPlate(Category category)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "TopSubCategoryFirstCol";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "CategoryIID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ParentCategoryID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "CategoryName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "CategoryCode" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ListingImageName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ListingImageTitle" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ListingLargeImageName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ListingLargeImageTitle" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ThumbnailImageName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "IsActive" });
            boilerPlateResultSetDTO.IsArray = false;
            if (category != null)
            {

                var thumbnail = category.CategoryImageMaps.Where(a => a.ImageTypeID == (byte)ImageTypes.Thumbnail).FirstOrDefault();
                var categoryListImage = category.CategoryImageMaps.Where(a => a.ImageTypeID == (byte)ImageTypes.Listing).FirstOrDefault();
                var categoryListLargeImage = category.CategoryImageMaps.Where(a => a.ImageTypeID == (byte)ImageTypes.Listing_Large).FirstOrDefault();

                boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          category.CategoryIID,
                          category.ParentCategoryID,
                          category.CategoryName,
                          category.CategoryCode,
                          categoryListImage == null ? string.Empty : categoryListImage.ImageFile,
                          categoryListImage == null ? string.Empty : categoryListImage.ImageTitle,
                          categoryListLargeImage == null ? string.Empty : categoryListLargeImage.ImageFile,
                          categoryListLargeImage == null ? string.Empty : categoryListLargeImage.ImageTitle,
                          thumbnail == null ? string.Empty : thumbnail.ImageFile,
                          category.IsActive
                        }
                });
            }

            return boilerPlateResultSetDTO;
        }

        public BoilerPlateResultSetDTO GetTopAllSubCategoriesHomePageSecondColBoilerPlate(List<Category> categoryList)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "TopSubCategorySecondCol";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "CategoryIID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ParentCategoryID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "CategoryName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "CategoryCode" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ListingImageName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ListingImageTitle" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ListingLargeImageName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ListingLargeImageTitle" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ThumbnailImageName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "IsActive" });
            boilerPlateResultSetDTO.IsArray = true;
            if (categoryList != null && categoryList.Count > 0)
            {
                foreach (var category in categoryList)
                {
                    var thumbnail = category.CategoryImageMaps.Where(a => a.ImageTypeID == (byte)ImageTypes.Thumbnail).FirstOrDefault();
                    var categoryListImage = category.CategoryImageMaps.Where(a => a.ImageTypeID == (byte)ImageTypes.Listing).FirstOrDefault();
                    var categoryListLargeImage = category.CategoryImageMaps.Where(a => a.ImageTypeID == (byte)ImageTypes.Listing_Large).FirstOrDefault();

                    boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          category.CategoryIID,
                          category.ParentCategoryID,
                          category.CategoryName,
                          category.CategoryCode,
                          categoryListImage == null ? string.Empty : categoryListImage.ImageFile,
                          categoryListImage == null ? string.Empty : categoryListImage.ImageTitle,
                          categoryListLargeImage == null ? string.Empty : categoryListLargeImage.ImageFile,
                          categoryListLargeImage == null ? string.Empty : categoryListLargeImage.ImageTitle,
                          thumbnail == null ? string.Empty : thumbnail.ImageFile,
                          category.IsActive
                        }
                    });
                }
            }

            return boilerPlateResultSetDTO;
        }

        public BoilerPlateResultSetDTO GetProductByTag(List<Eduegate.Services.Contracts.ProductDetail.ProductSKUDetailDTO> productList, string languageCode)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "HomeProductByTag";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductPartNo" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "BrandName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductPrice" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductDiscountPrice" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductAvailableQuantity" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "Currency" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "CurrencyDisplayText" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "SkuID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductListingQuantity" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductListingImage" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductThumbnail" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ProductCode" });
            boilerPlateResultSetDTO.IsArray = true;
            if (productList != null)
            {
                foreach (var product in productList)
                {
                    boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          product.ProductID,
                          product.ProductPartNo,
                          product.ProductName,
                          product.BrandName,
                          product.ProductPrice,
                          product.ProductDiscountPrice,
                          product.ProductAvailableQuantity,
                          product.Currency,
                          Eduegate.Globalization.ResourceHelper.GetValue(product.Currency, languageCode),
                          product.SKUID,
                          product.ProductListingQuantity,
                          product.ProductListingImage,
                          product.ProductThumbnailImage,
                          product.ProductCode
                        }
                    });
                }

            }

            return boilerPlateResultSetDTO;
        }

        public BoilerPlateResultSetDTO GetRecentActivity(Activity recentLoginActiivity, Activity recentFailedActiivity, Activity recentTransactions)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "RecentActivity";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "Description" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "Description2" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ActivityDate" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ReferenceID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ActivityType" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ActivityTypeID" });
            boilerPlateResultSetDTO.IsArray = true;
            if (recentLoginActiivity != null)
            {
                boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          "Last Login",
                          "", //success
                          recentLoginActiivity.Created,
                          recentLoginActiivity.ReferenceID,
                          recentLoginActiivity.ActivityType.ActivityTypeName,
                          recentLoginActiivity.ActivityTypeID,
                    }
                });

            }

            if (recentFailedActiivity != null)
            {
                boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          "Last Login",
                          "Failed attempt",
                          recentFailedActiivity.Created,
                          recentFailedActiivity.ReferenceID,
                          recentFailedActiivity.ActivityType.ActivityTypeName,
                          recentFailedActiivity.ActivityTypeID,
                    }
                });

            }

            if (recentTransactions != null)
            {
                boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          recentTransactions.Description,
                          "Transaction",
                          recentTransactions.Created,
                          recentTransactions.ReferenceID,
                          recentTransactions.ActivityType.ActivityTypeName,
                          recentTransactions.ActivityTypeID,
                    }
                });

            }

            return boilerPlateResultSetDTO;
        }

        public BoilerPlateResultSetDTO GetQuickLanuch(List<MenuLink> menuLinks)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "QuickLaunch";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "MenuLinkIID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "MenuName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "MenuTitle" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "MenuIcon" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ActionLink" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ActionLink1" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ActionLink3" });
            boilerPlateResultSetDTO.IsArray = true;

            if (menuLinks != null)
            {
                foreach (var menu in menuLinks)
                {
                    boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          menu.MenuLinkIID,
                          menu.MenuName,
                          string.IsNullOrEmpty(menu.MenuTitle) ?  menu.MenuName : menu.MenuTitle,
                          menu.MenuIcon,
                          menu.ActionLink,
                          menu.ActionLink1,
                          menu.ActionLink2,
                          menu.ActionLink3,
                    }
                    });
                }

            }
            return boilerPlateResultSetDTO;
        }

        public BoilerPlateResultSetDTO GetChartData(DashBoardChartDTO dto)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "C3Chart";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "JSONData" });
            boilerPlateResultSetDTO.IsArray = true;

            if (dto.ColumnDatas != null)
            {
                foreach (var data in dto.ColumnDatas)
                {
                    boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          data
                        }
                    });
                }

            }
            return boilerPlateResultSetDTO;
        }

        public BoilerPlateResultSetDTO GetChartColumnHeaders(DashBoardChartDTO dto)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "Headers";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "JSONData" });
            boilerPlateResultSetDTO.IsArray = true;

            if (dto.ColumnHeaders != null)
            {
                foreach (var data in dto.ColumnHeaders)
                {
                    boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          data
                        }
                    });
                }

            }

            return boilerPlateResultSetDTO;
        }

        public BoilerPlateResultSetDTO GetCustomer(Customer customer, IDictionary<string, string> customerInfo)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "Customer";
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "CustomerIID" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "FirstName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "CustomerCode" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "CustomerEmail" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "Telephone" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "LastMonthTotalOrders" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "LastMonthTotalSales" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "TotalOrders" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "TotalSales" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "EditEntity" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ViewName" });
            boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = "ViewTitle" });

            if (customer != null)
            {
                boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                    {
                        customer.CustomerIID,
                        customer.FirstName,
                        customer.CustomerCode,
                        customer.CustomerEmail,
                        customer.Telephone,
                        customerInfo["LastMonthTotalOrders"],
                        customerInfo["LastMonthTotalSales"],
                        customerInfo["TotalOrders"],
                        customerInfo["TotalSales"],
                        "Customer",
                        "Customer",
                        "Customer"
                    }
                });
            }

            return boilerPlateResultSetDTO;
        }

        public BoilerPlateResultSetDTO GetListData(ListWidgetDetail jsonData)
        {
            var boilerPlateResultSetDTO = new BoilerPlateResultSetDTO();
            boilerPlateResultSetDTO.BoilerPlateName = "ListData";
            boilerPlateResultSetDTO.IsArray = true;

            if (jsonData != null)
            {
                foreach (var column in jsonData.Columns)
                {
                    boilerPlateResultSetDTO.Columns.Add(new ColumnDTO() { ColumnName = column });
                }

                foreach (var data in jsonData.Datas)
                {
                    var cells = new Services.Contracts.Commons.DataCellListDTO();

                    foreach (var column in data)
                    {
                        cells.Add(column);
                    }

                    boilerPlateResultSetDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = cells
                    });
                }
            }

            return boilerPlateResultSetDTO;
        }

    }
}
using Eduegate.Domain.Charts;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Mappers.BoilerPlates;
using Eduegate.Domain.Repository;
using Eduegate.Domain.Repository.Charts;
using Eduegate.Domain.Repository.Logging;
using Eduegate.Domain.Repository.Payroll;
using Eduegate.Domain.Repository.School;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common.BoilerPlates;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Framework.Contracts.Common.PageRender;
using Eduegate.Framework.Helper;
using Eduegate.Services.Contracts.Enums.Logging;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Services.Contracts.School.Academics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Eduegate.Domain.Entity;
using Eduegate.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Eduegate.Services.Contracts.Commons;
namespace Eduegate.Domain
{
    public class BoilerPlateBL
    {
        private CallContext _callContext;
        public BoilerPlateBL(CallContext context)
        {
            _callContext = context;
        }

        public async Task<BoilerPlateDataSourceDTO> GetBoilerPlates(BoilerPlateInfo boilerPlateInfo)
        {
            var boilerPlateDataSourceDTO = new BoilerPlateDataSourceDTO();
            Services.Contracts.Enums.ImageTypes vCategoryImageTypes = Services.Contracts.Enums.ImageTypes.CategoryBanner_Small;
            var rootUrlHome = boilerPlateInfo.GetParameterValue<string>("RootUrl");
            switch (boilerPlateInfo.boilerPlateID)
            {
                case BoilerPlates.C3Chart:
                    {
                        var chartID = boilerPlateInfo.GetParameterValue<int>("ChartID");
                        var parameter = boilerPlateInfo.GetParameterValue<string>("YEAR");
                        var referenceID = boilerPlateInfo.GetParameterValue<string>("REFERENCEID");
                        if (parameter == null)
                        {
                            parameter = boilerPlateInfo.GetParameterValue<string>("FilterByID");
                        }
                        if (!string.IsNullOrEmpty(referenceID) && referenceID!="0")
                        {
                            parameter = referenceID;
                        }

                        var schoolID = _callContext.SchoolID;
                        var loginID = _callContext.LoginID;
                        var academicYearID = _callContext.AcademicYearID;
                        if (_callContext.SchoolID != null && _callContext.AcademicYearID != null)
                        {
                            if (string.IsNullOrEmpty(parameter))
                            {
                                parameter = boilerPlateInfo.GetParameterValue<string>("MONTHYEAR");
                            }
                            if (chartID != 0)
                            {
                                boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetChartData(new ChartRepository().GetChartData(chartID, parameter, (int?)schoolID, (int?)academicYearID, (int?)loginID)));
                                boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetChartColumnHeaders(new ChartRepository().GetChartData(chartID, parameter, (int?)schoolID, (int?)academicYearID, (int?)loginID)));
                            }
                        }
                    }
                    break;
                case BoilerPlates.HomePageCategoryTwoColumn:
                    {
                        var categoryID = boilerPlateInfo.GetParameterValue<long>("CategoryID");
                        var verticalTagName = boilerPlateInfo.GetParameterValue<string>("VerticalTagList");
                        var subCategoriesVertical = new CategoryRepository().GetSubCategories(categoryID, verticalTagName.Split(',').ToList(), _callContext.LanguageCode, 0);

                        var horizontalFirstColTagName = boilerPlateInfo.GetParameterValue<string>("HorizontalTagFirstColList");
                        var subCategoriesFirst = new CategoryRepository().GetSubCategories(categoryID, horizontalFirstColTagName.Split(',').ToList(), _callContext.LanguageCode, 1);

                        var horizontalSecondColTagName = boilerPlateInfo.GetParameterValue<string>("HorizontalTagSecondColList");
                        var horizontalListSecondColCount = boilerPlateInfo.GetParameterValue<int>("HorizontalListSecondColCount");
                        var subCategoriesSecond = new CategoryRepository().GetSubCategories(categoryID, horizontalSecondColTagName.Split(',').ToList(), _callContext.LanguageCode, horizontalListSecondColCount);

                        var horizontalThirdColTagName = boilerPlateInfo.GetParameterValue<string>("HorizontalTagThridColList");
                        var horizontalListThirdColCount = boilerPlateInfo.GetParameterValue<int>("HorizontalListThirdColCount");
                        var subCategoriesThirdHorizontal = new CategoryRepository().GetSubCategories(categoryID, horizontalThirdColTagName.Split(',').ToList(), _callContext.LanguageCode, horizontalListThirdColCount);

                        boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetAllCategoriesHomePageBoilerPlate(new CategoryRepository().GetCategoryByID(categoryID, _callContext.LanguageCode)));
                        boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetAllSubCategoriesHomePageBoilerPlate(subCategoriesVertical));

                        SetCategoryImageType(boilerPlateInfo, _callContext, ref vCategoryImageTypes);

                        boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetAllCategoriesBannerBoilerPlate(new CategoryBL(_callContext).GetCategoriesBannerbyCategoryID(vCategoryImageTypes, categoryID), rootUrlHome));
                        boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetTopAllSubCategoriesHomePageFirstColBoilerPlate(subCategoriesFirst.FirstOrDefault()));
                        boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetTopAllSubCategoriesHomePageSecondColBoilerPlate(subCategoriesSecond));
                        boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetTopAllSubCategoriesHomePageBoilerPlate(subCategoriesThirdHorizontal));
                    }
                    break;
                case BoilerPlates.HomePageCategoryThreeColumn:
                    {
                        var categoryID = boilerPlateInfo.GetParameterValue<long>("CategoryID");
                        var verticalTagName = boilerPlateInfo.GetParameterValue<string>("VerticalTagList");
                        var subCategoriesVertical = new CategoryRepository().GetSubCategories(categoryID, verticalTagName.Split(',').ToList(), _callContext.LanguageCode, 0);

                        var horizontalTagName = boilerPlateInfo.GetParameterValue<string>("HorizontalTagList");
                        var horizontalListCount = boilerPlateInfo.GetParameterValue<int>("HorizontalListCount");

                        var subCategoriesHorizontal = new CategoryRepository().GetSubCategories(categoryID, horizontalTagName.Split(',').ToList(), _callContext.LanguageCode, horizontalListCount);

                        boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetAllCategoriesHomePageBoilerPlate(
                            new CategoryRepository().GetCategoryByID(categoryID, _callContext.LanguageCode)));
                        boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetAllSubCategoriesHomePageBoilerPlate(subCategoriesVertical));
                        SetCategoryImageType(boilerPlateInfo, _callContext, ref vCategoryImageTypes);
                        boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetAllCategoriesBannerBoilerPlate(
                            new CategoryBL(_callContext).GetCategoriesBannerbyCategoryID(vCategoryImageTypes, categoryID), rootUrlHome));
                        boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetTopAllSubCategoriesHomePageBoilerPlate(subCategoriesHorizontal));
                    }
                    break;
                case BoilerPlates.HomePageCategoryWithBrand:
                    {
                        var categoryID = boilerPlateInfo.GetParameterValue<long>("CategoryID");
                        var verticalTagName = boilerPlateInfo.parameter.Find(a => a.Key == "VerticalTagList").Value;
                        var subCategoriesVertical = new CategoryRepository().GetSubCategories(categoryID, verticalTagName.Split(',').ToList(), _callContext.LanguageCode, 0);

                        var horizontalTagName = boilerPlateInfo.parameter.Find(a => a.Key == "HorizontalTagList").Value;
                        var subCategoriesHorizontal = new CategoryRepository().GetSubCategories(categoryID, horizontalTagName.Split(',').ToList(), _callContext.LanguageCode, 0);

                        var horizontalListCount = boilerPlateInfo.parameter.Find(a => a.Key == "HorizontalListCount").Value;

                        boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetAllCategoriesHomePageBoilerPlate(
                            new CategoryRepository().GetCategoryByID(categoryID, _callContext.LanguageCode)));
                        boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetAllSubCategoriesHomePageBoilerPlate(subCategoriesVertical));
                        SetCategoryImageType(boilerPlateInfo, _callContext, ref vCategoryImageTypes);
                        boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetAllCategoriesBannerBoilerPlate(
                            new CategoryBL(_callContext).GetCategoriesBannerbyCategoryID(vCategoryImageTypes, categoryID), rootUrlHome));
                        boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetBrandBoilerPlate(
                            new CategoryBL(_callContext).GetCategoryBrandsbyTag(horizontalTagName).Take(int.Parse(horizontalListCount)).ToList()));
                    }
                    break;
                case BoilerPlates.HomeBanner:
                    Eduegate.Services.Contracts.Enums.BannerTypes vBannerType;
                    Enum.TryParse<Eduegate.Services.Contracts.Enums.BannerTypes>(boilerPlateInfo.parameter.Find(a => a.Key == "BannerType").Value, true, out vBannerType);
                    Eduegate.Services.Contracts.Enums.BannerStatuses vBannerStatus;
                    Enum.TryParse<Eduegate.Services.Contracts.Enums.BannerStatuses>(boilerPlateInfo.parameter.Find(a => a.Key == "BannerStatus").Value, true, out vBannerStatus);
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetHomeBanner(new BannerBL(_callContext).GetBanners(vBannerType, vBannerStatus), rootUrlHome));
                    //boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetHomeSideBanner(new BannerBL(_callContext).GetHomePageSideBanners(vBannerType, vBannerStatus)));
                    break;
                case BoilerPlates.HomeBannerWithRightBanner:
                    Eduegate.Services.Contracts.Enums.BannerTypes vBannerTypeRight;
                    Eduegate.Services.Contracts.Enums.BannerTypes vSideBannerTypeRight;
                    switch (_callContext.LanguageCode)
                    {
                        case "en":
                        default:
                            Enum.TryParse<Eduegate.Services.Contracts.Enums.BannerTypes>(boilerPlateInfo.parameter.Find(a => a.Key == "BannerType").Value, true, out vBannerTypeRight);
                            Enum.TryParse<Eduegate.Services.Contracts.Enums.BannerTypes>(boilerPlateInfo.parameter.Find(a => a.Key == "SideBannerType").Value, true, out vSideBannerTypeRight);
                            break;
                        case "ar":
                            Enum.TryParse<Eduegate.Services.Contracts.Enums.BannerTypes>(boilerPlateInfo.parameter.Find(a => a.Key == "BannerTypeAr").Value, true, out vBannerTypeRight);
                            Enum.TryParse<Eduegate.Services.Contracts.Enums.BannerTypes>(boilerPlateInfo.parameter.Find(a => a.Key == "SideBannerTypeAr").Value, true, out vSideBannerTypeRight);
                            break;
                    }

                    Eduegate.Services.Contracts.Enums.BannerStatuses vBannerStatusRight;
                    Enum.TryParse<Eduegate.Services.Contracts.Enums.BannerStatuses>(boilerPlateInfo.parameter.Find(a => a.Key == "BannerStatus").Value, true, out vBannerStatusRight);

                    var rootUrl = boilerPlateInfo.GetParameterValue<string>("RootUrl");

                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetHomeBanner(new BannerBL(_callContext).GetBanners(vBannerTypeRight, vBannerStatusRight), rootUrl));



                    Eduegate.Services.Contracts.Enums.BannerStatuses vSideBannerStatusRight;
                    Enum.TryParse<Eduegate.Services.Contracts.Enums.BannerStatuses>(boilerPlateInfo.parameter.Find(a => a.Key == "SideBannerStatus").Value, true, out vSideBannerStatusRight);
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetHomeSideBanner(new BannerBL(_callContext).GetBanners(vSideBannerTypeRight, vSideBannerStatusRight), rootUrl));
                    break;
                case BoilerPlates.CategoryBanner:
                    var categoryIDCategoryBanner = boilerPlateInfo.parameter.Find(a => a.Key == "CategoryID").Value;
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetAllCategoriesHomePageBoilerPlate(new CategoryRepository().GetCategoryByID(long.Parse(categoryIDCategoryBanner), _callContext.LanguageCode)));
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetAllSubCategoriesHomePageBoilerPlate(new CategoryRepository().GetSubCategories(long.Parse(categoryIDCategoryBanner), _callContext.LanguageCode)));
                    SetCategoryImageType(boilerPlateInfo, _callContext, ref vCategoryImageTypes);
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetAllCategoriesBannerBoilerPlate(new CategoryBL(_callContext).GetCategoriesBannerbyCategoryID(vCategoryImageTypes, long.Parse(categoryIDCategoryBanner)), rootUrlHome));
                    break;
                case BoilerPlates.CategoryPageWithProductLeftAlign:
                    {
                        var categoryIDCategoryProductLeft = boilerPlateInfo.parameter.Find(a => a.Key == "CategoryID").Value;
                        var producttag = boilerPlateInfo.GetParameterValue<string>("Tag");
                        boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetAllCategoriesHomePageBoilerPlate(new CategoryRepository().GetCategoryByID(long.Parse(categoryIDCategoryProductLeft), _callContext.LanguageCode)));
                        boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetTopProductByCategory(new ProductBL(_callContext).GetTopProductByCategory(long.Parse(categoryIDCategoryProductLeft), 1)));
                        boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetCategoryTags(new CategoryBL(_callContext).GetCategoryTags(long.Parse(categoryIDCategoryProductLeft))));
                        boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetProductsBySubCategory(new ProductBL(_callContext).GetTopProductByCategory(long.Parse(categoryIDCategoryProductLeft), 8)));
                    }
                    break;
                case BoilerPlates.CategoryPageWithProductRightAlign:
                    {
                        var categoryIDCategoryProductRight = boilerPlateInfo.GetParameterValue<long>("CategoryID");
                        var producttag = boilerPlateInfo.GetParameterValue<string>("Tag");
                        boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetAllCategoriesHomePageBoilerPlate(new CategoryRepository().GetCategoryByID(categoryIDCategoryProductRight, _callContext.LanguageCode)));
                        boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetTopProductByCategory(new ProductBL(_callContext).GetTopProductByCategory(categoryIDCategoryProductRight, 1)));
                        boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetCategoryTags(new CategoryBL(_callContext).GetCategoryTags(categoryIDCategoryProductRight)));
                        boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetProductsBySubCategory(new ProductBL(_callContext).GetTopProductByCategory(categoryIDCategoryProductRight, 8)));
                    }
                    break;
                case BoilerPlates.SubCategoryBanner:
                    var subCategoryID = boilerPlateInfo.parameter.Find(a => a.Key == "CategoryID").Value;
                    SetCategoryImageType(boilerPlateInfo, _callContext, ref vCategoryImageTypes);
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetAllCategoriesBannerBoilerPlate(new CategoryBL(_callContext).GetCategoriesBannerbyCategoryID(vCategoryImageTypes, long.Parse(subCategoryID)), rootUrlHome));
                    break;
                case BoilerPlates.SubCategoryBrand:
                    var subCategoryIDBrand = boilerPlateInfo.parameter.Find(a => a.Key == "CategoryID").Value;
                    var tagName = boilerPlateInfo.parameter.Find(a => a.Key == "Tag").Value;
                    var count = boilerPlateInfo.parameter.Find(a => a.Key == "Count").Value;
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetBrandBoilerPlate(new CategoryBL(_callContext).GetCategoryBrandsbyTag(tagName).Take(int.Parse(count)).ToList()));
                    break;
                case BoilerPlates.SubCategoryProducts:
                    var blockID = boilerPlateInfo.parameter.Find(a => a.Key == "CategoryID").Value;
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetCategoryBlockDetails(new CategoryBL(_callContext).GetCategoryBlockDetails(long.Parse(blockID))));
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetProductsbyCategoryBlock(new ProductBL(_callContext).GetProductsbyCategoryBlock(long.Parse(blockID))));
                    break;
                case BoilerPlates.ProductDetailWithAddToCart:
                    var skuID = boilerPlateInfo.parameter.Find(a => a.Key == "SKUID").Value;
                    var cultureID = boilerPlateInfo.parameter.Find(a => a.Key == "CultureID").Value.Contains("?") ? boilerPlateInfo.parameter.Find(a => a.Key == "CultureID").Value.Split('?')[0] : boilerPlateInfo.parameter.Find(a => a.Key == "CultureID").Value;
                    //var settingDTO = new Domain.Setting.SettingBL().GetSettingDetail("ONLINEBRANCHID");
                    long customerID = 0;
                    try
                    { customerID = new AccountBL(this._callContext).GetUserDetails(this._callContext.EmailID).Customer.CustomerIID; }
                    catch (Exception ex) { customerID = 0; }
                    var productInventory = new ProductDetailBL(this._callContext).GetInventoryDetailsSKUID(long.Parse(skuID), customerID);
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetProductDetailImages(new ProductBL(_callContext).GetProductDetailImages(long.Parse(skuID))));
                    var productSKUDetailDTO = new ProductBL(_callContext).GetProductBySKU(long.Parse(skuID), long.Parse(cultureID));
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetProductDetails(productSKUDetailDTO, _callContext.LanguageCode));
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetProductDetailKeyFeatures(new ProductBL(_callContext).GetProductKeyFeatures(long.Parse(skuID))));
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetProductDetailVariants(new ProductBL(_callContext).GetProductVariants(long.Parse(skuID), long.Parse(cultureID))));
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetProductDetailLocalDeliveryOptions(new ProductBL(_callContext).GetProductDeliveryTypeList(long.Parse(skuID), long.Parse(cultureID), productSKUDetailDTO.BranchID.HasValue ? (long)productSKUDetailDTO.BranchID.Value : default(long))));
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetProductMultiPriceDetails(new ProductDetailBL(_callContext).GetProductMultiPriceDetails(productInventory.BranchID, long.Parse(skuID), customerID)));
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetProductQtyDiscountDetails(new ProductDetailBL(_callContext).GetProductQtyDiscountDetails(productInventory.BranchID, long.Parse(skuID), customerID)));
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetProductDetailDescription(new ProductBL(_callContext).GetProductDetailsDescription(long.Parse(skuID), long.Parse(cultureID))));
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetProductVideos(new ProductBL(_callContext).GetProductVideos(long.Parse(skuID))));
                    break;
                case BoilerPlates.ProductDetailAlsoViewed:
                    {
                        var skuIDViewed = boilerPlateInfo.GetParameterValue<long>("SKUID");
                        var cultureIDViewed = boilerPlateInfo.GetParameterValue<long>("CultureID");
                        //var settingDTO = new Domain.Setting.SettingBL().GetSettingDetail("ONLINEBRANCHID");
                        int siteID = boilerPlateInfo.GetParameterValue<int>("SiteID");
                        boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetHomeProductRecommendation(new ProductBL(_callContext).GetProductsAlosViewed(skuIDViewed, cultureIDViewed, siteID).Take(20).ToList(), _callContext.LanguageCode));
                    }
                    break;
                case BoilerPlates.ProductDetailDescription:
                    var skuIDDesc = boilerPlateInfo.GetParameterValue<long>("SKUID");
                    var cultureIDDesc = boilerPlateInfo.GetParameterValue<long>("CultureID");
                    //var settingDTO = new Domain.Setting.SettingBL().GetSettingDetail("ONLINEBRANCHID");
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetProductDetailDescription(new ProductBL(_callContext).GetProductDetailsDescription(skuIDDesc, cultureIDDesc)));
                    break;
                case BoilerPlates.HomePageRecentlyViewed:
                    string productIDs = boilerPlateInfo.parameter.Find(a => a.Key == "productIDs").Value;
                    long cultureIDEmarsys = long.Parse(boilerPlateInfo.parameter.Find(a => a.Key == "cultureIDEmarsys").Value);
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetRecentlyViewed(new UserServiceBL(_callContext).GetProductsEmarsys(Services.Contracts.Enums.WidgetTypes.RecentlyViewed, productIDs, cultureIDEmarsys), _callContext.LanguageCode));
                    break;
                case BoilerPlates.CategoryPageRecommend:
                    string catproductIDs = boilerPlateInfo.parameter.Find(a => a.Key == "productIDs").Value;
                    long catcultureIDEmarsys = long.Parse(boilerPlateInfo.parameter.Find(a => a.Key == "cultureIDEmarsys").Value);
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetCategoryRecommended(new UserServiceBL(_callContext).GetProductsEmarsys(Services.Contracts.Enums.WidgetTypes.CategoryRecommended, catproductIDs, catcultureIDEmarsys), _callContext.LanguageCode));
                    break;
                case BoilerPlates.CategoryPageDepartment:
                    string deptproductIDs = boilerPlateInfo.parameter.Find(a => a.Key == "productIDs").Value;
                    long deptcultureIDEmarsys = long.Parse(boilerPlateInfo.parameter.Find(a => a.Key == "cultureIDEmarsys").Value);
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetCategoryDepartment(new UserServiceBL(_callContext).GetProductsEmarsys(Services.Contracts.Enums.WidgetTypes.CategoryDepartment, deptproductIDs, deptcultureIDEmarsys)));
                    break;
                case BoilerPlates.CategoryPageInterested:
                    string intproductIDs = boilerPlateInfo.parameter.Find(a => a.Key == "productIDs").Value;
                    long intcultureIDEmarsys = long.Parse(boilerPlateInfo.parameter.Find(a => a.Key == "cultureIDEmarsys").Value);
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetCategoryInterested(new UserServiceBL(_callContext).GetProductsEmarsys(Services.Contracts.Enums.WidgetTypes.CategoryInterested, intproductIDs, intcultureIDEmarsys)));
                    break;
                case BoilerPlates.WebsiteOneDealADay:
                    var settingDetail = new Domain.Setting.SettingBL().GetSettingDetail(Constants.TransactionSettings.ONLINEBRANCHID, _callContext.CompanyID.Value);
                    var noOfProducts = boilerPlateInfo.GetParameterValue<long>("NoOfProducts");
                    var deals = new ProductBL(_callContext).GetActiveDealProducts(long.Parse(settingDetail.SettingValue), noOfProducts);
                    if (deals != null)
                    {
                        if (deals.Count > 0)
                        {
                            var firstDeal = deals.FirstOrDefault();
                            boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetActiveDealProducts(deals));
                            boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetProductDetailImages(new ProductBL(_callContext).GetProductDetailImages(long.Parse(firstDeal.SKUID.ToString()))));
                        }
                    }

                    break;
                case BoilerPlates.HomePageBoilerPlateOnlyProducts:
                    var categoryIDProd = boilerPlateInfo.GetParameterValue<long>("CategoryID");
                    var verticalTagNameProd = boilerPlateInfo.GetParameterValue<string>("VerticalTagList");
                    var subCategoriesVerticalProd = new CategoryRepository().GetSubCategories(categoryIDProd, verticalTagNameProd.Split(',').ToList(), _callContext.LanguageCode, 0);

                    var horizontalTagList = boilerPlateInfo.GetParameterValue<string>("HorizontalTagList");
                    var horizontalTagCountProd = boilerPlateInfo.GetParameterValue<int>("HorizontalTagCount");
                    SetCategoryImageType(boilerPlateInfo, _callContext, ref vCategoryImageTypes);
                    int siteIDProd = boilerPlateInfo.GetParameterValue<int>("SiteID");
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetAllSubCategoriesHomePageBoilerPlate(subCategoriesVerticalProd));
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetAllCategoriesHomePageBoilerPlate(new CategoryRepository().GetCategoryByID(categoryIDProd, _callContext.LanguageCode)));
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetAllCategoriesBannerBoilerPlate(new CategoryBL(_callContext).GetCategoriesBannerbyCategoryID(vCategoryImageTypes, categoryIDProd), rootUrlHome));
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetProductByTag(new ProductBL(_callContext).GetProductByTag(horizontalTagList.Split(',').ToList(), _callContext.LanguageCode, horizontalTagCountProd, categoryIDProd, siteIDProd), _callContext.LanguageCode));

                    break;
                case BoilerPlates.HomePageBoilerPlateProdCatMix:
                    var categoryIDMix = boilerPlateInfo.GetParameterValue<long>("CategoryID");
                    var verticalTagNameMix = boilerPlateInfo.GetParameterValue<string>("VerticalTagList");
                    var subCategoriesVerticalMix = new CategoryRepository().GetSubCategories(categoryIDMix, verticalTagNameMix.Split(',').ToList(), _callContext.LanguageCode, 0);

                    var horizontalTagFirstRowListMix = boilerPlateInfo.GetParameterValue<string>("HorizontalTagFirstRowList");
                    var horizontalTagFirstRowCountMix = boilerPlateInfo.GetParameterValue<int>("HorizontalTagFirstRowCount");

                    var horizontalTagSecondRowListMix = boilerPlateInfo.GetParameterValue<string>("HorizontalTagSecondRowList");
                    var horizontalTagSecondRowCountMix = boilerPlateInfo.GetParameterValue<int>("HorizontalTagSecondRowCount");
                    int siteIDCatProd = boilerPlateInfo.GetParameterValue<int>("SiteID");
                    var subCategoriesFirstProdMix = new CategoryRepository().GetSubCategories(categoryIDMix, horizontalTagFirstRowListMix.Split(',').ToList(), _callContext.LanguageCode, horizontalTagFirstRowCountMix);

                    SetCategoryImageType(boilerPlateInfo, _callContext, ref vCategoryImageTypes);

                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetAllSubCategoriesHomePageBoilerPlate(subCategoriesVerticalMix));
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetAllCategoriesHomePageBoilerPlate(new CategoryRepository().GetCategoryByID(categoryIDMix, _callContext.LanguageCode)));
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetAllCategoriesBannerBoilerPlate(new CategoryBL(_callContext).GetCategoriesBannerbyCategoryID(vCategoryImageTypes, categoryIDMix), rootUrlHome));
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetProductByTag(new ProductBL(_callContext).GetProductByTag(horizontalTagSecondRowListMix.Split(',').ToList(), _callContext.LanguageCode, horizontalTagSecondRowCountMix, categoryIDMix, siteIDCatProd), _callContext.LanguageCode));
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetTopAllSubCategoriesHomePageSecondColBoilerPlate(subCategoriesFirstProdMix));

                    break;
                case BoilerPlates.ProductDetailRecommendedEmarsys:
                    string productIDProductDetails = boilerPlateInfo.parameter.Find(a => a.Key == "productIDs").Value;
                    long cultureIDProductDetailEmarsys = long.Parse(boilerPlateInfo.parameter.Find(a => a.Key == "cultureIDEmarsys").Value);
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(BoilerPlateMapper.Mapper.GetRecentlyViewed(new UserServiceBL(_callContext).GetProductsEmarsys(Services.Contracts.Enums.WidgetTypes.RecentlyViewed, productIDProductDetails, cultureIDProductDetailEmarsys), _callContext.LanguageCode));
                    break;
                case BoilerPlates.RecentActivity:
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(
                        BoilerPlateMapper.Mapper.GetRecentActivity(
                            new ActivityLoggerRepository().GetLastActivity(_callContext.LoginID.Value, (int)ActivityTypes.Login, (int)ActionStatuses.Success),
                            new ActivityLoggerRepository().GetLastActivity(_callContext.LoginID.Value, (int)ActivityTypes.Login, (int)ActionStatuses.Failed),
                            new ActivityLoggerRepository().GetLastActivity(_callContext.LoginID.Value, (int)ActivityTypes.Transactions, (int)ActionStatuses.Success)
                            ));
                    break;
                case BoilerPlates.QuickLaunch:
                    long parentMenuID = boilerPlateInfo.GetParameterValue<long>("ParentMenuID");
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(
                      BoilerPlateMapper.Mapper.GetQuickLanuch(
                          await new MenuRepository().GetMenusByType(Services.Contracts.Enums.MenuLinkTypes.QuickLaunch, parentMenuID, _callContext.LanguageCode)
                          ));
                    break;
                case BoilerPlates.EntitySummaryInfo:
                    var customerId = boilerPlateInfo.GetParameterValue<long>("REFERENCEID");
                    boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(
                      BoilerPlateMapper.Mapper.GetCustomer(
                          new CustomerRepository().GetCustomerV2(customerId),
                          new TransactionRepository().GetCustomerSummary(customerId)
                          ));
                    break;
                case BoilerPlates.ListData:
                    {
                        var entityType = boilerPlateInfo.GetParameterValue<string>("EntityType");
                        var entityId = boilerPlateInfo.GetParameterValue<string>("REFERENCEID");
                        var listType = boilerPlateInfo.GetParameterValue<string>("ListType");

                        if (!string.IsNullOrEmpty(entityType) && !string.IsNullOrEmpty(entityId))
                        {
                            boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(
                              BoilerPlateMapper.Mapper.GetListData(
                                  new ChartRepository().GetListData(entityType, listType, entityId)
                                  ));
                        }
                        break;
                    }
                case BoilerPlates.StudentSupportServices:
                    {
                        var entityType = boilerPlateInfo.GetParameterValue<string>("EntityType");
                        var entityId = boilerPlateInfo.GetParameterValue<string>("REFERENCEID");
                        var listType = boilerPlateInfo.GetParameterValue<string>("ListType");

                        if (!string.IsNullOrEmpty(entityType) && !string.IsNullOrEmpty(entityId))
                        {
                            boilerPlateDataSourceDTO.BoilerPlateResultSetList.Add(
                              BoilerPlateMapper.Mapper.GetListData(
                                  new ChartRepository().GetListData(entityType, listType, entityId)
                                  ));
                        }
                        break;
                    }
                default:
                    {
                        var dataSource = boilerPlateInfo.GetParameterValue<string>("DataSource");
                        if (!string.IsNullOrEmpty(dataSource))
                        {
                            var factory = DataSourceFactory.Create(dataSource);

                            if (factory != null)
                            {
                                factory.SetContext(_callContext);
                                boilerPlateDataSourceDTO = await factory.GetData(boilerPlateInfo);
                            }
                        }
                    }
                    break;
            }

            return boilerPlateDataSourceDTO;
        }

        public void SetCategoryImageType(BoilerPlateInfo boilerPlateInfo, CallContext _callContext, ref Services.Contracts.Enums.ImageTypes vCategoryImageTypes)
        {
            switch (_callContext.LanguageCode)
            {
                case "en":
                default:
                    Enum.TryParse<Services.Contracts.Enums.ImageTypes>(boilerPlateInfo.GetParameterValue<string>("ImageType"), true, out vCategoryImageTypes);
                    break;
                case "ar":
                    Enum.TryParse<Services.Contracts.Enums.ImageTypes>(boilerPlateInfo.GetParameterValue<string>("ImageTypeAr"), true, out vCategoryImageTypes);
                    break;
            }
        }

        public BoilerPlateDTO SaveBoilerPlate(BoilerPlateDTO boilerPlateDTO)
        {
            var mapper = Mappers.PageRender.BoilerPlateMapper.Mapper(_callContext);
            return mapper.ToDTO(new BoilerPlateRepository().SaveBoilerPlate(mapper.ToEntity(boilerPlateDTO, _callContext)));
        }

        public BoilerPlateDTO GetBoilerPlate(long boilerPlateID)
        {
            var mapper = Mappers.PageRender.BoilerPlateMapper.Mapper(_callContext);
            return mapper.ToDTO(new BoilerPlateRepository().GetBoilerPlate(boilerPlateID));
        }

        public List<BoilerPlateParameterDTO> GetBoilerPlateParameters(long boilerPlateID)
        {
            var boilerPlateParameterDTO = new List<BoilerPlateParameterDTO>();
            var boilerPlateParameter = new BoilerPlateRepository().GetBoilerPlateParameters(boilerPlateID);
            if (boilerPlateParameter.Any())
            {
                foreach (var parameter in boilerPlateParameter)
                {
                    boilerPlateParameterDTO.Add(Mappers.PageRender.BoilerPlateParameterMapper.Mapper(_callContext).ToDTO(parameter));
                }
            }
            return boilerPlateParameterDTO;
        }

        public List<PageBoilerplateReportDTO> GetBoilerPlateReports(long? boilerPlateID, long? pageBoilerPlateMapIID)
        {
            var pageBoilerplateReportDTO = new List<PageBoilerplateReportDTO>();
            var pageBoilerplateReports = new BoilerPlateRepository().GetBoilerPlateReports(boilerPlateID, pageBoilerPlateMapIID);
            if (pageBoilerplateReports.Any())
            {
                foreach (var reportParameter in pageBoilerplateReports)
                {
                    pageBoilerplateReportDTO.Add(
                        new PageBoilerplateReportDTO()
                        {
                            BoilerPlateID= reportParameter.BoilerPlateID,
                            PageID= reportParameter.PageID,
                            PageBoilerplateReportID=reportParameter.PageBoilerplateReportID,
                            ParameterName= reportParameter.ParameterName,
                            Remarks= reportParameter.Remarks,
                            ReportHeader= reportParameter.ReportHeader,
                            ReportName  = reportParameter.ReportName                            
                        }
                        );
                }
            }
            return pageBoilerplateReportDTO;
        }
        public DashBoardChartDTO DashBoardMenuCardCounts(int chartID)
        {
            var returnDto = new DashBoardChartDTO();

            var schoolID = _callContext.SchoolID;
            var loginID = _callContext.LoginID;
            var academicYearID = _callContext.AcademicYearID;

            returnDto = new ChartRepository().GetChartData(chartID, "", (int?)schoolID, (int?)academicYearID, (int?)loginID);

            return returnDto;
        }
        public DashBoardChartDTO GetTwinViewData(int chartID,string referenceID)
        {
            var returnDto = new DashBoardChartDTO();

            var schoolID = _callContext.SchoolID;
            var loginID = _callContext.LoginID;
            var academicYearID = _callContext.AcademicYearID;
           

            returnDto = new ChartRepository().GetChartData(chartID, referenceID, (int?)schoolID, (int?)academicYearID, (int?)loginID);

            return returnDto;
        }

        public DirectorsDashBoardDTO GetTeacherRelatedDataForDirectorsDashBoard()
        {
            var returnData = new DirectorsDashBoardDTO();

            var empPermenant = new Domain.Setting.SettingBL(null).GetSettingValue<string>("EMPLOYEE_PERMANANT");
            var empTemporary = new Domain.Setting.SettingBL(null).GetSettingValue<string>("EMPLOYEE_TEMPORARY");
            var adminManDesigID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("EMP_ADMINMANAGER_DESIGNATION_ID");
            var administrationDesigID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("EMP_ADMINISTRATION_DESIGNATION_ID");

            var schools = new SchoolRepository().GetSchools();

            var getAcademic = new SchoolRepository().GetAcademicYearDataByID(_callContext.AcademicYearID);
            var academic = getAcademic.StartDate.Value.Year + " - " + getAcademic.EndDate.Value.Year;

            foreach (var scl in schools)
            {
                long? branchID = Convert.ToInt64(scl.SchoolID);
                var employees = new EmployeeRepository().GetEmployeesByBranch(branchID);

                var empCount = employees.Count();
                var tchrCount = employees.Where(e => e.Designation.DesignationName == "Teacher").Count();
                var admins = employees.Where(d => d.DesignationID == int.Parse(adminManDesigID) || d.DesignationID == int.Parse(administrationDesigID)).Count();

                var A = tchrCount;
                var B = empCount;

                var gcd = GCD(A, B);
                var ratiodat = string.Format("{0}:{1}", A / gcd, B / gcd);

                returnData.Schools.Add(new Services.Contracts.School.Mutual.SchoolsDTO()
                {
                    SchoolName = scl.SchoolName,
                    SchoolID = scl.SchoolID,
                    //StudentCount
                    StudentsCount = scl.Students.Where(s => s.IsActive == true && s.AcademicYear.AcademicYearCode == getAcademic.AcademicYearCode).Count(),
                    TeachersCount = tchrCount,
                    TeachersCountPermanant = employees.Where(p => p.Designation.DesignationName == "Teacher" && p.JobTypeID == int.Parse(empPermenant)).Count(),
                    TeachersCountTemporary = employees.Where(t => t.Designation.DesignationName == "Teacher" && t.JobTypeID == int.Parse(empTemporary)).Count(),
                    TeacherRatio = ratiodat,
                    SectionCount = scl.Sections.Where(r => r.SectionName != "TC-Section").Count(),
                    AdminsCount = admins,
                });

                returnData.TotalStudents = returnData.Schools.Sum(x => x.StudentsCount);
                returnData.TotalTeachers = returnData.Schools.Sum(y => y.TeachersCount);
                returnData.TotalEmployees = empCount;
                returnData.TotalSections = returnData.Schools.Sum(z => z.SectionCount);
                returnData.TotalAdmins = returnData.Schools.Sum(a => a.AdminsCount);

            }

            returnData.Academic = academic;

            return returnData;
        }


        public DirectorsDashBoardDTO GetClassTeachersDashBoard()
        {
            var returnData = new DirectorsDashBoardDTO();

            var getAcademic = new SchoolRepository().GetAcademicYearDataByID(_callContext.AcademicYearID);
            var academic = getAcademic.StartDate.Value.Year + " - " + getAcademic.EndDate.Value.Year;
            var classSec = new SchoolRepository().GetClassClassTeacherMapsByLogin(_callContext.LoginID);

            foreach (var scl in classSec)
            {

            }

            return returnData;
        }


        public EmployeeDTO UserProfileForDashBoard()
        {
            DateTime? TodayDate = DateTime.Now;

            var getUserDetails = new SchoolRepository().GetEmployeeUserDataByLoginID(_callContext.LoginID);

            if (getUserDetails == null)
            {
                return null;
            }

            var attendance = new SchoolRepository().GetStaffAttendancesByID(getUserDetails.EmployeeIID);

            var returnData = new EmployeeDTO()
            {
                EmployeeName = getUserDetails.FirstName + " " + getUserDetails.MiddleName + " " + getUserDetails.LastName,
                EmployeeCode = getUserDetails.EmployeeCode,
                Department = getUserDetails.Departments1?.DepartmentName,
                Designation = getUserDetails.Designation?.DesignationName,
                ProfileImage = getUserDetails?.EmployeePhoto,
                DateOfBirthString = getUserDetails?.DateOfBirth.Value.ToString("dd-MM-yyyy"),
                WorkEmail = getUserDetails?.WorkEmail,
                WorkMobileNo = getUserDetails.WorkMobileNo != null ? getUserDetails.WorkMobileNo : getUserDetails.WorkPhone,
                PresentStatus = attendance?.AttendenceDate.Value.Date == TodayDate.Value.Date ? attendance?.StaffPresentStatus?.StatusDescription : "UnMarked",
            };

            return returnData;
        }

        public Services.Contracts.Notifications.PushNotificationDTO GetNotificationsForDashBoard()
        {
            DateTime? TodayDate = DateTime.Now.Date;
            DateTime? checkDateBy = TodayDate.Value.Date.AddDays(-3);

            var getNotificationDetails = new SchoolRepository().GetNotificationsByLoginAndDate(_callContext.LoginID, checkDateBy.Value);

            if (getNotificationDetails == null)
            {
                return null;
            }

            var myInboxList = new List<Services.Contracts.Notifications.PushNotificationDTO>();

            foreach (var inbx in getNotificationDetails)
            {
                myInboxList.Add(new Services.Contracts.Notifications.PushNotificationDTO()
                {
                    NotificationDateString = inbx.NotificationDate.Value.ToString("dd-MMM-yyyy"),
                    Message = inbx.Message
                });
            }

            var returnData = new Services.Contracts.Notifications.PushNotificationDTO()
            {
                MyInboxCount = getNotificationDetails.Count(),
                MyInboxNotificationList = myInboxList,
            };

            return returnData;
        }
        
        public Services.Contracts.Notifications.PushNotificationDTO GetCareerPortalNotificationsForDashBoard()
        {
            DateTime? TodayDate = DateTime.Now.Date;
            DateTime? checkDateBy = TodayDate.Value.Date.AddDays(-3);

            var getNotificationDetails = new SchoolRepository().GetCareerPortalNotificationsByLoginAndDate(_callContext.LoginID, checkDateBy.Value);

            if (getNotificationDetails == null)
            {
                return null;
            }

            var myInboxList = new List<Services.Contracts.Notifications.PushNotificationDTO>();

            foreach (var inbx in getNotificationDetails)
            {
                myInboxList.Add(new Services.Contracts.Notifications.PushNotificationDTO()
                {
                    NotificationDateString = inbx.NotificationDate.Value.ToString("dd-MMM-yyyy"),
                    Message = inbx.Message
                });
            }

            var returnData = new Services.Contracts.Notifications.PushNotificationDTO()
            {
                MyInboxCount = getNotificationDetails.Count(),
                MyInboxNotificationList = myInboxList,
            };

            return returnData;
        }

        public OperationResultDTO MarkAllasReadNotifications()
        {
            var result = new SchoolRepository().MarkAllasReadNotifications(_callContext.LoginID);
            return result;
        }


        static int GCD(int a, int b)
        {
            return b == 0 ? Math.Abs(a) : GCD(b, a % b);
        }

        public TimeTableDTO GetWeeklyTimeTableForDashBoard(int weekDayID)
        {
            var getTimeTable = new SchoolRepository().GetTimeTableAllocations(_callContext.LoginID, weekDayID);

            if (getTimeTable == null)
            {
                return null;
            }

            var returnDTO = new TimeTableDTO()
            {

            };

            foreach (var dat in getTimeTable)
            {
                returnDTO.TimeTableAllocations.Add(new TimeTableAllocationDTO()
                {
                    TimeTableAllocationIID = dat.TimeTableAllocationIID,
                    ClassSectionName = dat?.Class?.ClassDescription + ' ' + dat?.Section?.SectionName,
                    SubjectName = dat?.Subject?.SubjectName,
                    ClassTime = dat.ClassTiming.StartTime.HasValue ? DateTime.Today.Add(dat.ClassTiming.StartTime.Value).ToString("hh:mm tt") + " to " + DateTime.Today.Add(dat.ClassTiming.EndTime.Value).ToString("hh:mm tt") : null,
                });

                returnDTO.TimeTableID = Convert.ToInt32(dat.TimeTableID);
            }

            return returnDTO;
        }



        public async Task<IActionResult> GetBoilerPlatesForMobileApp(BoilerPlateInfo boilerPlateInfo)
        {
            var dto = await GetBoilerPlates(boilerPlateInfo); ;
            return Json(ToJsonString(dto, true));
        }
        [NonAction]
        public virtual JsonResult Json(object? data)
        {
            return new JsonResult(data);
        }
        private string ToJsonString(BoilerPlateDataSourceDTO dtos, bool isArray = true)
        {
            var jsonString = new StringBuilder();
            if (dtos.BoilerPlateResultSetList != null && dtos.BoilerPlateResultSetList.Count > 0)
            {
                var lastDTO = dtos.BoilerPlateResultSetList.Last();
                //if (isArray)
                //    jsonString.Append("[");
                jsonString.Append("{");
                foreach (var dto in dtos.BoilerPlateResultSetList)
                {
                    if (dto.Rows.Count > 0)
                    {
                        //isArray = true;
                        //if (dto.Rows.Count == 1)
                        //{
                        //    isArray = false;
                        //}
                        isArray = dto.IsArray;

                        jsonString.Append("\"" + dto.BoilerPlateName + "\":");
                        if (isArray)
                            jsonString.Append("[");
                        int i = 0;

                        foreach (var dataRow in dto.Rows)
                        {
                            jsonString.Append("{");

                            for (int j = 0; j < dto.Columns.Count; j++)
                            {
                                if (j < dto.Columns.Count - 1)
                                {
                                    jsonString.Append("\"" + dto.Columns[j].ColumnName.ToString()
                                 + "\":" + "\""
                                 //+ (dataRow.DataCells[j] == null ? string.Empty : HttpUtility.HtmlEncode((dataRow.DataCells[j].ToString().Replace("\t", "")))) + "\",");
                                 + (dataRow.DataCells[j] == null ? string.Empty : System.Web.HttpUtility.HtmlEncode((escapeSpecialCharacters(dataRow.DataCells[j].ToString())))) + "\",");
                                }
                                else if (j == dto.Columns.Count - 1)
                                {
                                    jsonString.Append("\"" + dto.Columns[j].ColumnName.ToString()
                                 + "\":" + "\""
                                 //+ (dataRow.DataCells[j] == null ? string.Empty : HttpUtility.HtmlEncode(dataRow.DataCells[j].ToString().Replace("\t", ""))) + "\"");
                                 + (dataRow.DataCells[j] == null ? string.Empty : System.Web.HttpUtility.HtmlEncode(escapeSpecialCharacters(dataRow.DataCells[j].ToString()))) + "\"");
                                }
                            }

                            if (i == dto.Rows.Count - 1)
                            {
                                jsonString.Append("}");
                            }
                            else
                            {
                                jsonString.Append("},");
                            }

                            i++;

                        }
                        if (isArray)
                            jsonString.Append("]");
                    }
                    if (dto != lastDTO && dto.Rows.Count > 0)
                    {
                        jsonString.Append(",");
                    }
                    else if (dto == lastDTO && dto.Rows.Count == 0)
                    {
                        int index = jsonString.ToString().LastIndexOf(',');
                        if (index != -1)
                        {
                            jsonString = new StringBuilder(jsonString.ToString().Remove(index, 1));//.Insert(index, "");
                        }
                        //jsonString.ToString().TrimEnd(',');
                    }

                }
                //if (isArray)
                //    jsonString.Append("]");
                jsonString.Append("}");
            }
            return jsonString.ToString();
        }
        private string escapeSpecialCharacters(string value)
        {
            string[] stringArray = new string[] { "\t", @"\" };
            string[] stringArrayReplace = new string[] { "", @"\\" };
            for (int i = 0; i <= stringArray.Length - 1; i++)
            {
                if (value.Contains(stringArray[i]))
                {
                    value = value.Replace(stringArray[i], stringArrayReplace[i]);
                }
            }
            return value;
        }

    }
}

using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Managers;
using Eduegate.Domain.Mappers;
using Eduegate.Domain.Mappers.Notification.Helpers;
using Eduegate.Domain.Mappers.School.Inventory;
using Eduegate.Domain.Mappers.School.Students;
using Eduegate.Domain.Mappers.ShoppingCartMapper;
using Eduegate.Domain.Mappers.SolrMapper;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Framework.Contracts.Common.PageRender;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Payment;
using Eduegate.Framework.Security;
using Eduegate.Globalization;
using Eduegate.Logger;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Checkout;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Distributions;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.MobileAppWrapper;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Services.Contracts.PaymentGateway;
using Eduegate.Services.Contracts.ProductDetail;
using Eduegate.Services.Contracts.School.Inventory;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Services.Contracts.SearchData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Runtime.InteropServices.ComTypes;
using Eduegate.Domain.Mappers.HR.Leaves;

namespace Eduegate.Domain.MobileAppWrapper
{
    public class AppDataBL
    {
        private CallContext Context { get; set; }
        private static string FromAddress = ConfigurationExtensions.GetAppConfigValue("FromAddress");
        private static string RootUrl = "";
        private static string ProductImageURL = string.Format("{0}/{1}/", Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("ImageHostUrl"), Eduegate.Framework.Enums.EduegateImageTypes.Products.ToString());
        private static string CategoryURL = string.Format("{0}/{1}/", Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("ImageHostUrl"), Eduegate.Framework.Enums.EduegateImageTypes.Category.ToString());
        private static string BannerURL = string.Format("{0}/{1}/", Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("ImageHostUrl"), Eduegate.Framework.Enums.EduegateImageTypes.Banners.ToString());
        private static int siteID = 1;
        private static int cultureIDContext = 1;
        private OrderManager _orderManager;

        public AppDataBL(CallContext context)
        {
            Context = context;
            _orderManager = OrderManager.Manager(context);
        }

        public bool UpdateUserDefaultBranch(long branchID)
        {
            try
            {
                var userDetail = new AccountBL(this.Context).GetUserDetails(this.Context.EmailID, this.Context.MobileNumber);
                new CustomerBL(this.Context).UpdateUserDefaultBranch(userDetail.Customer.CustomerIID, branchID);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<BannerMasterDTO> GetHomeBanners()
        {
            var bannerType = this.Context.LanguageCode == "ar" ? BannerTypes.HomePageAr : BannerTypes.HomePage;
            var homeBanners = new BannerBL(this.Context).GetBanners(bannerType, BannerStatuses.Active);
            int count = 0;

            //var homeBanners = new List<BannerMasterDTO>();

            foreach (var banner in homeBanners)
            {
                banner.isInternalLink = false;
                ActionLinkTypes actionLinkTypes;
                Enum.TryParse<ActionLinkTypes>(banner.ActionLinkTypeID.ToString(), true, out actionLinkTypes);
                if (banner.Link.IsNotNullOrEmpty())
                {
                    var bannerlink = banner.Link;
                    var isInternalLink = banner.isInternalLink;
                    GetBannerLink(actionLinkTypes, ref bannerlink, ref isInternalLink);
                    //productlists({searchText:'apple',filterBy:'brand',filterValue:'mipow,targus',filterText:'',sortText:'relevance',pageType:'',searchTitle:'" + link + "'})
                    banner.Link = bannerlink;
                    banner.isInternalLink = isInternalLink;

                }
                banner.BannerFile = string.Format("{0}/{1}", BannerURL, banner.BannerFile);
                if (banner.Link.IsNotNullOrEmpty())
                {
                    if (banner.Link.Contains("blink.com.kw"))
                    {
                        if (banner.Link.Contains("productlists"))
                        {
                            banner.isInternalLink = true;
                            var searchText = "";
                        }
                    }

                }
                count++;

            }

            //var homeBanner = new BannerMasterDTO();
            //homeBanner.BannerFile = string.Format("{0}/{1}", BannerURL, "0a0ed6e6-8cee-4ad8-b1b1-8d4c31cb4d0f.jpg");
            //homeBanner.isInternalLink = false;
            //homeBanner.Link = "http://www.blink.com.kw/ExternalPages/MobileRedirect.htm";
            //homeBanners.Add(homeBanner);

            return homeBanners;
        }

        public BannerMasterDTO GetSingleHomePageBanner(BoilerPlateDTO boilerplateDTO)
        {
            int bannerIID = 0;

            if (this.Context.LanguageCode == "ar")
            {
                bannerIID = boilerplateDTO.GetRuntimeParameterValue<int>("BannerIDAr");
            }
            else
            {
                bannerIID = boilerplateDTO.GetRuntimeParameterValue<int>("BannerIDEng");
            }

            var banner = new BannerBL(this.Context).GetBanner(bannerIID);
            var bannerlink = banner.Link;
            ActionLinkTypes actionLinkTypes;
            Enum.TryParse<ActionLinkTypes>(banner.ActionLinkTypeID.ToString(), true, out actionLinkTypes);
            var isInternalLink = banner.isInternalLink;
            GetBannerLink(actionLinkTypes, ref bannerlink, ref isInternalLink);
            banner.Link = bannerlink;
            banner.isInternalLink = isInternalLink;
            banner.BannerFile = string.Format("{0}/{1}", BannerURL, banner.BannerFile);
            return banner;
        }

        private void GetBannerLink(ActionLinkTypes actionLinkTypes, ref string link, ref bool isInternalLink)
        {
            isInternalLink = true;
            switch (actionLinkTypes)
            {
                case ActionLinkTypes.External_Link:
                default:
                    isInternalLink = false;
                    //link = link;
                    break;
                //case ActionLinkTypes.Brand://brand code
                //    banner.Link = "productlists({searchText:'',filterBy:'brand',filterValue:'" + banner.Link + "',filterText:'',sortText:'relevance',pageType:'',searchTitle:'" + banner.Link + "'})";
                //    break;
                case ActionLinkTypes.Category:
                case ActionLinkTypes.Brand:
                case ActionLinkTypes.Product_Detail:
                    var type = new ReferenceDataBL(this.Context).GetUrlTypeByCode(link);
                    switch (type.UrlType)
                    {
                        case Services.Contracts.UrlReWriter.UrlType.Brand:
                            link = "productlists({searchText:'',filterBy:'brand',filterValue:'brand:" + link + "',filterText:'',sortText:'relevance',pageType:'',searchTitle:'" + link + "'})";
                            break;
                        case Services.Contracts.UrlReWriter.UrlType.Category:
                            if (type.LevelNo == 1) //subcategory
                            {
                                link = "productlists({searchText:'',filterBy:'category',filterValue:'category:" + link + "',filterText:'',sortText:'relevance',pageType:'',searchTitle:'" + link + "'})";
                            }
                            else
                            {
                                link = "categoryhome({categoryID:" + type.IID + ",categoryCode:'" + link + "'})";
                            }
                            break;
                        case Services.Contracts.UrlReWriter.UrlType.Product:
                            link = "productdetails({skuID:" + type.IID + "})";
                            break;
                        default:
                            isInternalLink = false;
                            link = "";
                            break;
                    }
                    break;
                //case ActionLinkTypes.Product_Detail:
                //    banner.Link = "productdetails({skuID:product.SKUID})";
                //    break;
                case ActionLinkTypes.Product_Search:
                    //Product/Products?searchText=&sortBy=&searchVal=casecover-stickers&searchBy=category
                    var finalSearchText = string.Empty;
                    var finalSortBy = string.Empty;
                    var finalsearchVal = string.Empty;
                    var finalsearchBy = string.Empty;
                    var separator = link.Split('?');
                    var querystring = string.Empty;
                    if (separator.Length > 1)
                    {
                        querystring = separator[1];
                    }
                    else { querystring = separator[0]; }
                    var finalSeparator = querystring.Split('&');
                    var searchText = Array.FindAll(finalSeparator, s => s.Contains("searchText")).FirstOrDefault();
                    var sortBy = Array.FindAll(finalSeparator, s => s.Contains("sortBy")).FirstOrDefault();
                    var searchVal = Array.FindAll(finalSeparator, s => s.Contains("searchVal")).FirstOrDefault();
                    var searchBy = Array.FindAll(finalSeparator, s => s.Contains("filterBy")).FirstOrDefault();
                    if (searchText.IsNotDefault())
                    {
                        var split = searchText.Split('=');
                        if (split.Length > 1)
                            finalSearchText = split[1];
                    }
                    if (sortBy.IsNotDefault())
                    {
                        var split = sortBy.Split('=');
                        if (split.Length > 1)
                            finalSortBy = split[1];
                    }
                    //if (searchVal.IsNotDefault())
                    //{
                    //    var split = searchVal.Split('=');
                    //    if (split.Length > 1)
                    //        finalsearchVal = split[1];
                    //}
                    if (searchBy.IsNotDefault())
                    {
                        var split = searchBy.Split('=');
                        if (split.Length > 1)
                        {
                            string filters = split[1];
                            var filterSplit = filters.Split(new[] { "||" }, StringSplitOptions.None);
                            if (filterSplit.Length > 1)
                            {
                                finalsearchBy = "catbrand";
                            }
                            else
                            {
                                var filterSplitSplit = filterSplit[0].Split(new[] { ":" }, StringSplitOptions.None);
                                if (filterSplitSplit.Length > 0)
                                {
                                    finalsearchBy = filterSplitSplit[0] == "cat" ? "category" : "brand";
                                }
                            }
                            finalsearchVal = split[1];
                        }
                    }
                    link = "productlists({searchText:'" + finalSearchText + "',filterBy:'" + finalsearchBy + "',filterValue:'" + finalsearchVal + "',filterText:'',sortText:'" + finalSortBy + "',pageType:'',searchTitle:'" + finalSearchText + "'})";
                    break;
                case ActionLinkTypes.Deal:
                    isInternalLink = true;
                    link = "deals";
                    break;
            }

        }

        public List<ProductSKUDetailDTO> GetProductsEmarsys(WidgetTypes widgetType, string productIDs, long cultureID)
        {
            cultureID = cultureIDContext;
            var productSKDDetailListDTO = new List<ProductSKUDetailDTO>();
            var productBLInit = new ProductBL(this.Context);
            var productID = productIDs.Split(',');
            productID = productID.OrderBy(a => Guid.NewGuid()).ToArray();
            for (var i = 0; i <= productID.Length - 1; i++)
            {
                var productskudetailbl = productBLInit.GetProductDetailsEmarsys(long.Parse(productID[i]), cultureID);
                if (productskudetailbl.IsNotNull())
                {
                    if (productskudetailbl.SKUID != 0)
                    {
                        productskudetailbl.Currency = ResourceHelper.GetValue(productskudetailbl.Currency, this.Context.LanguageCode);
                        productskudetailbl.ProductListingImage = string.Format("{0}/{1}", ProductImageURL, productskudetailbl.ProductListingImage);
                        productskudetailbl.ProductThumbnailImage = string.Format("{0}/{1}", ProductImageURL, productskudetailbl.ProductThumbnailImage);

                        productSKDDetailListDTO.Add(productskudetailbl);
                    }
                }

            }
            //for (var i = 0; i <= productID.Length - 1; i++)
            //{
            //    var productBL = productBLInit.GetProductBySKU(Convert.ToInt64(productID[i]), cultureID);
            //    var productImageDTO = productBLInit.GetProductDetailImages(long.Parse(productID[i])).FirstOrDefault();
            //    if (productImageDTO.IsNotNull())
            //    {
            //        productBL.ProductListingImage = string.Format("{0}/{1}", ProductImageURL, productImageDTO.ListingImage);
            //    }
            //    productSKDDetailListDTO.Add(productBL);
            //}
            switch (widgetType)
            {
                case WidgetTypes.Recommended:

                    break;
            }
            return productSKDDetailListDTO;
        }

        public List<ProductSKUDetailDTO> GetProducts(BoilerPlateDTO boilerplateDTO)
        {
            var productSKUDetailDTO = new List<ProductSKUDetailDTO>();

            switch (boilerplateDTO.BoilerPlateID)
            {
                case 49:
                    {
                        var hasSearchServer = ConfigurationExtensions.GetAppConfigValue<bool>("HasSearchServer");

                        if (!hasSearchServer)
                        {
                            var searchText = boilerplateDTO.GetRuntimeParameterValue<string>("SearchText");
                            var tag = boilerplateDTO.GetRuntimeParameterValue<string>("Tag");
                            List<ProductDTO> skus;

                            if (string.IsNullOrEmpty(tag))
                            {
                                var getProducts = new ProductBL(Context).ProductSKUSearch(1, 10, searchText, searchText,
                                            null, null, null, false);
                                skus = new List<ProductDTO>();

                                foreach (var group in getProducts.CatalogGroups)
                                {
                                    foreach (var sku in group.Catalogs)
                                    {
                                        skus.Add(new ProductDTO()
                                        {
                                            //BranchID = sku.,
                                            BrandCode = sku.BrandCode,
                                            BrandID = int.Parse(sku.BrandID.ToString()),
                                            BrandName = sku.BrandName,
                                            Currency = sku.CurrencyCode,
                                            ProductAvailableQuantity = sku.ProductAvailableQuantity,
                                            ProductCode = sku.ProductCode,
                                            ProductDiscountPrice = sku.ProductDiscountPrice,
                                            ProductID = sku.ProductID,
                                            ProductListingImage = sku.ProductListingImage,
                                            //ProductListingQuantity = sku.ProductAvailableQuantity,
                                            ProductThumbnail = sku.ProductThumbnail,
                                            //ProductPartNo = sku.Part,
                                            ProductName = sku.ProductName,
                                            ProductPrice = sku.ProductPrice.ToString(),
                                            SKU = sku.ProductName,
                                            SKUID = sku.SKUID,
                                            //StartDate = sku.StartDate
                                        });
                                    }
                                }
                            }
                            else
                            {
                                if (tag.Equals("ALL"))
                                {
                                    skus = new ProductBL(Context).GetProductSKUByTag(20, 1, null);
                                }
                                else
                                {
                                    skus = new ProductBL(Context).GetProductSKUByTag(20, 1, tag);
                                }
                            }

                            foreach (var sku in skus)
                            {
                                productSKUDetailDTO.Add(new ProductSKUDetailDTO()
                                {
                                    //BranchID = sku.,
                                    BrandCode = sku.BrandCode,
                                    BrandID = sku.BrandIID,
                                    BrandName = sku.BrandName,
                                    Currency = sku.Currency,
                                    ProductAvailableQuantity = sku.ProductAvailableQuantity,
                                    ProductCode = sku.ProductCode,
                                    ProductDiscountPrice = sku.ProductDiscountPrice,
                                    ProductID = sku.ProductID,
                                    ProductListingImage = sku.ProductListingImage,
                                    ProductListingQuantity = sku.ProductAvailableQuantity,
                                    ProductThumbnailImage = sku.ProductThumbnail,
                                    ProductPartNo = sku.ProductPartNo,
                                    ProductName = sku.ProductName,
                                    ProductPrice = decimal.Parse(sku.ProductPrice),
                                    SKUName = sku.SKU,
                                    SKUID = sku.SKUID,
                                    //StartDate = sku.StartDate
                                });
                            }
                        }
                        else
                        {
                            var productTag = boilerplateDTO.GetRuntimeParameterValue<string>("Tag");
                            var dtoSolr = new Services.Contracts.SearchData.SearchResultDTO();
                            dtoSolr = GetProductsfromSolr(1, 100, "", "skutags:" + productTag, "", "relevance", "search");
                            var mapper = new SolrToProductSKUDetailMapper();

                            foreach (var dto in dtoSolr.Catalogs)
                            {
                                var dtoFinal = mapper.ToDTO(dto);

                                dtoFinal.ProductDiscountPrice = new ProductDetailRepository().GetInventoryDetailsSKUID(dtoFinal.SKUID, CustomerID: string.IsNullOrEmpty(this.Context.UserId) ? 0 : long.Parse(this.Context.UserId), type: true, CompanyID: this.Context.CompanyID.Value).ProductDiscountPrice;
                                dtoFinal.Currency = ResourceHelper.GetValue(this.Context.CurrencyCode, this.Context.LanguageCode);
                                dtoFinal.ProductListingImage = string.Format("{0}/{1}", ProductImageURL, new ProductBL(this.Context).GetProductDetailImages(dtoFinal.SKUID).Select(a => a.ThumbnailImage).FirstOrDefault());

                                productSKUDetailDTO.Add(dtoFinal);
                            }
                        }
                    }
                    break;
                default:
                    {
                        var mobileTag = boilerplateDTO.RuntimeParameters.Find(a => a.Key == "MobileHomeTagList").Value;
                        var dtoSolr = new Services.Contracts.SearchData.SearchResultDTO();
                        if (mobileTag.ToUpper() == "MOBILEHOMEPAGEOFFERS")
                        {
                            dtoSolr = GetProductsfromSolr(1, 12, "", "", "", "new-arrivals", "search");
                        }
                        else
                        {
                            dtoSolr = GetProductsfromSolr(1, 100, "", "skutags:" + mobileTag, "", "relevance", "search");
                        }

                        var mapper = new SolrToProductSKUDetailMapper();
                        if (dtoSolr.IsNotNull() && dtoSolr.Catalogs.IsNotNull() && dtoSolr.Catalogs.Count > 0)
                        {
                            dtoSolr.Catalogs = dtoSolr.Catalogs.OrderBy(a => Guid.NewGuid()).ToList();
                        }
                        foreach (var dto in dtoSolr.Catalogs)
                        {
                            var dtoFinal = mapper.ToDTO(dto);

                            dtoFinal.ProductDiscountPrice = new ProductDetailRepository().GetInventoryDetailsSKUID(dtoFinal.SKUID, CustomerID: string.IsNullOrEmpty(this.Context.UserId) ? 0 : long.Parse(this.Context.UserId), type: true, CompanyID: this.Context.CompanyID.Value).ProductDiscountPrice;
                            dtoFinal.Currency = ResourceHelper.GetValue(this.Context.CurrencyCode, this.Context.LanguageCode);
                            dtoFinal.ProductListingImage = string.Format("{0}/{1}", ProductImageURL, new ProductBL(this.Context).GetProductDetailImages(dtoFinal.SKUID).Select(a => a.ThumbnailImage).FirstOrDefault());

                            productSKUDetailDTO.Add(dtoFinal);
                        }
                    }
                    break;
            }

            return productSKUDetailDTO;
        }

        public List<CategoryDTO> GetCategories()
        {
            //var fullCategoryList = new CategoryRepository().GetCategoryList().Where(a => a.ParentCategoryID == null && a.IsActive == true).ToList();
            var fullCategoryList = new CategoryBL(this.Context).GetCategoryByParentIDWithImage(null).ToList();
            var mapper = Mappers.CategoryMapper.Mapper(Context);
            var categories = new List<CategoryDTO>();
            int count = 1;

            foreach (var category in fullCategoryList)
            {
                var dto = category;

                var thumbnail = category.CategoryImageMapList.Where(a => a.ImageTypeID == (byte)ImageTypes.Thumbnail).FirstOrDefault();
                if (thumbnail.IsNotNull())
                {
                    dto.ThumbnailImageName = string.Format("{0}/{1}/{2}", CategoryURL, "Thumbnail/", thumbnail.ImageFile);
                }
                else
                {
                    dto.ThumbnailImageName = "noimage.jpg";
                }

                var listingImage = category.CategoryImageMapList.Where(a => a.ImageTypeID == (byte)ImageTypes.Listing).FirstOrDefault();
                if (listingImage.IsNotNull())
                {
                    dto.ImageName = string.Format("{0}/{1}/{2}", CategoryURL, "Listing/", listingImage.ImageFile);
                }
                else
                {
                    dto.ImageName = "noimage.jpg";
                }
                //}
                //dto.ThumbnailImageName = string.Format("{0}/{1}", CategoryURL, dto.ThumbnailImageName);
                categories.Add(dto);
                count++;
            }

            return categories;
        }

        public List<KnowHowOptionDTO> GetKnowHowOptions()
        {
            return new AccountBL(this.Context).GetKnowHowOptions();
        }

        public List<CountryDTO> GetCountries()
        {
            //siteID change
            return new CountryMasterBL().GetCountriesBySiteMap(siteID);
        }

        public OperationResultDTO Register(UserDTO user)
        {
            //this.Context = new CallContext("","en","1.1.1.1","");
            var operationResultDTO = new OperationResultDTO();
            bool isUserExists = false;

            //if the user is already created just skip the steps
            if (string.IsNullOrEmpty(user.LoginID) || user.LoginID == "0")
            {
                isUserExists = new AccountBL(this.Context).isUserAvailableEmailPhone(user.LoginEmailID, !string.IsNullOrEmpty(user.Customer.TelephoneNumber) ? user.Customer.TelephoneNumber : string.Empty);
            }

            var settingDetail = new SettingBL().GetSettingDetail(Eduegate.Framework.Helper.Constants.TransactionSettings.DOMAINNAME);
            if (!isUserExists)
            {
                if (ConfigurationExtensions.GetAppConfigValue("RegisterUserByEmail").ToLower() == "false")
                    user.StatusID = (byte)Eduegate.Framework.Helper.Enums.UserStatus.Active;
                else
                    user.StatusID = (byte)Eduegate.Framework.Helper.Enums.UserStatus.NeedEmailVerification;
                user.Contacts = new List<ContactDTO>();
                user.Contacts.Add(TranslateCustomertoContactDTO(user, true));
                user.Contacts.Add(TranslateCustomertoContactDTO(user, false));
                user = new AccountBL(this.Context).RegisterUser(user);
                if (user != null)
                {
                    if (user.LoginUserID.IsNotNullOrEmpty())
                    {
                        switch ((Framework.Helper.Enums.UserStatus)user.StatusID)
                        {
                            case Framework.Helper.Enums.UserStatus.Active:
                                {
                                    //ResetCookies(user);
                                    operationResultDTO.Message = "Website is already active for this Email id: " + user.LoginEmailID;
                                    operationResultDTO.operationResult = OperationResult.Error;
                                    break;
                                }
                            case Framework.Helper.Enums.UserStatus.NeedEmailVerification:
                                {

                                    //Add emailn notification
                                    var notificationDTO = new EmailNotificationDTO();
                                    notificationDTO.ToEmailID = user.LoginEmailID;
                                    notificationDTO.EmailNotificationType = Eduegate.Services.Contracts.Enums.EmailNotificationTypes.Registration;
                                    notificationDTO.AdditionalParameters = new List<Eduegate.Services.Contracts.Commons.KeyValueParameterDTO>();

                                    notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.Registration.Keys.RegistrationLink, ParameterValue = string.Concat(RootUrl, "Account/ConfirmEmail?id=", user.LoginUserID) });

                                    //notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.Registration.Keys.Title, ParameterValue = userDTO.Customer.Property.PropertyName });

                                    notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.Registration.Keys.FirstName, ParameterValue = user.Customer.FirstName });

                                    notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.Registration.Keys.LastName, ParameterValue = user.Customer.LastName });
                                    notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.Registration.Keys.DomainName, ParameterValue = Convert.ToString(settingDetail.SettingValue) });

                                    notificationDTO.FromEmailID = FromAddress;
                                    var notificationresult = new NotificationBL(this.Context).SaveEmailData(notificationDTO);
                                    operationResultDTO.Message = ResourceHelper.GetValue("Pleaseactivateyouraccount", this.Context.LanguageCode);
                                    operationResultDTO.operationResult = OperationResult.Success;
                                    break;
                                }

                        }
                    }
                    else
                    {
                        operationResultDTO.Message = ResourceHelper.GetValue("Pleasetrylater", this.Context.LanguageCode);
                        operationResultDTO.operationResult = OperationResult.Error;
                    }
                }
                else
                {
                    operationResultDTO.operationResult = OperationResult.Error;
                    operationResultDTO.Message = ResourceHelper.GetValue("Pleasetrylater", this.Context.LanguageCode);
                }
            }
            else
            {
                operationResultDTO.operationResult = OperationResult.Error;
                operationResultDTO.Message = ResourceHelper.GetValue("EmailIDMobileNoAlreadyExists", this.Context.LanguageCode);
            }
            return operationResultDTO;
        }


        public OperationResultDTO Login(UserDTO user, ApplicationType appType = ApplicationType.ERP)
        {
            var operationResultDTO = new OperationResultDTO();
            bool result = new AccountBL(this.Context).Login(user.LoginEmailID, user.Password, (byte)appType);
            Logger.LogHelper<AppDataBL>.Info("Login Result:" + JsonConvert.SerializeObject(result));
            if (result)
            {
                var userDTO = string.IsNullOrEmpty(user.LoginID) ?
                    new AccountBL(this.Context).GetUserDetails(user.LoginEmailID) : new AccountBL(this.Context).GetUserDetailsByID(long.Parse(user.LoginID));
                bool isConfirmed = true;

                if (userDTO.Customer != null)
                {
                    isConfirmed = new AccountBL(this.Context).isCustomerConfirmed(userDTO.Customer.CustomerIID);
                }
                else
                {
                    userDTO.Customer = new CustomerDTO() { FirstName = user.LoginEmailID };
                }

                Logger.LogHelper<AppDataBL>.Info("User Details:" + JsonConvert.SerializeObject(userDTO));

                if (isConfirmed)
                {
                    operationResultDTO.Message = "";
                    operationResultDTO.operationResult = OperationResult.Success;
                    if (userDTO.IsNotNull())
                    {
                        //ResetCookies(userDTO);
                    }
                    if (this.Context.GUID.IsNotNullOrEmpty())
                    {
                        //merge shopping cart if he has something in cart with guid
                        //var re = new ShoppingCartBL(this.Context).MergeCart(this.Context);
                        //remove guide
                        //this.Context.GUID = null;
                        //ResetCallContext(this.Context);
                    }
                }
                else
                {
                    //operationResultDTO.Message = ResourceHelper.GetValue("Pleaseactivateyouraccount", this.Context.LanguageCode);
                    operationResultDTO.Message = "Please activate your account";
                    operationResultDTO.operationResult = OperationResult.Error;
                }
            }
            else
            {
                //operationResultDTO.Message = ResourceHelper.GetValue("EmailorPasswordisincorrect", this.Context.LanguageCode);
                operationResultDTO.Message = "Email or password is incorrect";
                operationResultDTO.operationResult = OperationResult.Error;
            }

            Logger.LogHelper<AppDataBL>.Info("Opration Result:" + JsonConvert.SerializeObject(operationResultDTO));
            return operationResultDTO;
        }

        public UserDTO GetUserDetails()
        {
            return new AccountBL(this.Context).GetUserDetails(this.Context.EmailID, this.Context.MobileNumber);
        }

        public UserDTO GetEmployeeDetails(string loginEmailID)
        {
            return new AccountBL(this.Context).GetEmployeeDetails(loginEmailID);
        }

        public UserDTO LogOut()
        {
            var userDTO = new UserDTO();
            //ResetCookies(userDTO);
            return userDTO;
        }

        public int GetShoppingCartCount()
        {
            return new ShoppingCartBL(this.Context).ProductCount(this.Context);
        }

        public Services.Contracts.SearchData.SearchResultDTO GetProductSearch
            (int pageIndex, int pageSize, string searchText, string searchVal,
            string searchBy, string sortBy, string pageType, bool isCategory)
        {
            return new ProductBL(Context).ProductSKUSearch(
                    pageIndex, pageSize, searchText, searchVal,
                    searchBy, sortBy, pageType, isCategory);
        }

        public Eduegate.Services.Contracts.SearchData.SearchResultDTO GetProductsfromSolr(int pageIndex, int pageSize, string searchText, string searchVal, string searchBy, string sortBy, string pageType)
        {
            SearchParameterDTO obj_SearchParameterDTO = new SearchParameterDTO();
            searchText = string.IsNullOrEmpty(searchText) ? "*:*" : searchText;
            if (!string.IsNullOrEmpty(searchVal) && (searchVal.ToUpper() == "MOBILEHOMEPAGEOFFERS" || searchVal.ToUpper() == "SKUTAGS:MOBILEHOMEPAGEOFFERS"))
            {
                searchVal = "";
                searchBy = "";
            }
            obj_SearchParameterDTO.FreeSearch = searchText;
            obj_SearchParameterDTO.PageIndex = pageIndex;
            obj_SearchParameterDTO.PageSize = pageSize;
            obj_SearchParameterDTO.Language = this.Context.LanguageCode;
            //obj_SearchParameterDTO.CountryID = 10000;
            obj_SearchParameterDTO.Sort = "";
            obj_SearchParameterDTO.ConversionRate = 1;
            obj_SearchParameterDTO.Sort = sortBy;
            //siteID change
            obj_SearchParameterDTO.SiteID = siteID;
            obj_SearchParameterDTO.Facets = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(searchVal))
            {

                foreach (var filterFacet in searchVal.Split('|'))
                {
                    var facetName = string.Empty;
                    if (filterFacet.IsNotNullOrEmpty())
                    {
                        if (filterFacet.IndexOf(':') > -1)
                        {
                            facetName = filterFacet.Substring(0, filterFacet.IndexOf(':'));
                        }
                        else
                        {
                            facetName = filterFacet;
                        }
                        facetName = facetName == "brands" ? "brandcode" : facetName == "brand" ? "brandcode" : facetName == "cat" ? "categorycode" : facetName == "category" ? "categorycode" : facetName;

                        foreach (var facetCode in filterFacet.Substring(filterFacet.IndexOf(':') + 1).Split(','))
                        {
                            if (facetCode.IsNotNullOrEmpty())
                            {
                                var facet = obj_SearchParameterDTO.Facets.FirstOrDefault(a => a.Key == facetName);

                                if (facet.Equals(default(KeyValuePair<string, string>)))
                                    obj_SearchParameterDTO.Facets.Add(facetName, facetCode);
                                else
                                    obj_SearchParameterDTO.Facets[facet.Key] = facet.Value + "," + facetCode;
                            }
                        }
                    }
                }
                string serverFilter = null;
                var newFacets = new Dictionary<string, string>();

                foreach (var facet in obj_SearchParameterDTO.Facets)
                {
                    if (facet.Key.Equals("categorycode"))
                    {
                        serverFilter = "cat:" + facet.Value;
                    }

                    //brands filter will be using for SEO url
                    if (searchVal.IndexOf("brands") != -1 || searchVal.IndexOf("brand") != -1)
                    {
                        serverFilter = "brandcode:" + facet.Value;
                    }

                    newFacets.Add(facet.Key, facet.Value);
                }

                obj_SearchParameterDTO.Facets = newFacets;
            }

            return null;
        }

        public OperationResultDTO AddToCart(CartProductDTO cartDTO)
        {
            var operationResultDTO = new OperationResultDTO();

            try
            {
                if (string.IsNullOrEmpty(this.Context.EmailID) && string.IsNullOrEmpty(this.Context.GUID))
                {
                    this.Context.GUID = Convert.ToString(Guid.NewGuid());
                    //ResetCallContext(this.Context);
                }

                var result = new ShoppingCartBL(this.Context).AddToCart(cartDTO, this.Context);
                if (result.Status)
                {
                    operationResultDTO.Message = "Product added to your Cart";
                    operationResultDTO.operationResult = OperationResult.Success;
                }
                else
                {
                    operationResultDTO.Message = result.CartMessage;
                    operationResultDTO.operationResult = OperationResult.Error;
                }

            }
            catch (Exception ex)
            {
                operationResultDTO.Message = "Please try later";
                operationResultDTO.operationResult = OperationResult.Error;
            }
            return operationResultDTO;
        }

        public CartDTO GetCartDetails(Eduegate.Framework.Helper.Enums.ShoppingCartStatus cartStatus = ShoppingCartStatus.InProcess)
        {
            var cartDTO = new ShoppingCartBL(this.Context).GetCart(this.Context, 0, isDeliveryCharge: false, cartStatus: cartStatus);

            if (cartDTO.IsNotNull())
            {
                cartDTO.isDigitalCart = false;
                if (cartDTO.Products.IsNotNull())
                {
                    cartDTO.isDigitalCart = !cartDTO.Products.Any(a => a.DeliveryTypeID != (int)DeliveryTypes.Email);
                    cartDTO.Currency = ResourceHelper.GetValue(this.Context.CurrencyCode, this.Context.LanguageCode);
                    foreach (var product in cartDTO.Products)
                    {
                        product.ProductThumbnail = string.Format("{0}/{1}", ProductImageURL, product.ProductThumbnail);
                        product.ProductListingImage = string.Format("{0}/{1}", ProductImageURL, product.ProductListingImage);
                        product.ImageFile = string.Format("{0}/{1}", ProductImageURL, product.ImageFile);
                        product.Currency = ResourceHelper.GetValue(this.Context.CurrencyCode, this.Context.LanguageCode);
                    }
                }
            }
            return cartDTO;
        }

        public OperationResultDTO RemoveCartItem(long SKUID)
        {
            var operationResultDTO = new OperationResultDTO();
            var isDeleted = new ShoppingCartBL(this.Context).RemoveItem(SKUID, this.Context);
            if (isDeleted)
            {
                operationResultDTO.Message = "Product Removed Successfully";
                operationResultDTO.operationResult = OperationResult.Success;
            }
            else
            {
                operationResultDTO.Message = "Please try later";
                operationResultDTO.operationResult = OperationResult.Error;
            }
            return operationResultDTO;
        }

        public OperationResultDTO UpdateCart(CartProductDTO cartDTO)
        {
            var operationResultDTO = new OperationResultDTO();
            var currentInventoryBranchList = new ProductDetailBL(this.Context).GetProductInventoryOnline(cartDTO.SKUID.ToString());
            var currentInventoryBranch = (from productInventoryDetails in currentInventoryBranchList
                                          where productInventoryDetails.ProductSKUMapID == cartDTO.SKUID
                                          select productInventoryDetails).FirstOrDefault();
            if (cartDTO.Quantity > Convert.ToInt64(currentInventoryBranch.Quantity))
            {
                cartDTO.Quantity = currentInventoryBranch.Quantity;
            }
            cartDTO.BranchID = currentInventoryBranch.BranchID;
            var isUpdated = new ShoppingCartBL(this.Context).UpdateCart(cartDTO, this.Context);
            if (isUpdated)
            {
                operationResultDTO.Message = "Cart Updated Successfully";
                operationResultDTO.operationResult = OperationResult.Success;
            }
            else
            {
                operationResultDTO.Message = "Please try later";
                operationResultDTO.operationResult = OperationResult.Error;
            }
            return operationResultDTO;
        }

        public long GetSaveForLaterCount()
        {
            return new UserServiceBL(this.Context).GetSaveForLaterCount(this.Context);
        }

        public List<WishListDTO> GetSaveForLater()
        {
            return new UserServiceBL(this.Context).GetSaveForLater(this.Context);
        }

        public OperationResultDTO AddSaveForLater(long skuID)
        {
            var operationResultDTO = new OperationResultDTO();
            var isAdded = new UserServiceBL(this.Context).AddSaveForLater(skuID, this.Context);
            if (isAdded)
            {
                operationResultDTO.Message = "Item added successfully";
                operationResultDTO.operationResult = OperationResult.Success;
            }
            else
            {
                operationResultDTO.Message = "Item already added in cart";
                operationResultDTO.operationResult = OperationResult.Error;
            }
            return operationResultDTO;
        }

        public OperationResultDTO RemoveSaveForLater(long skuID)
        {
            var operationResultDTO = new OperationResultDTO();
            var isRemoved = new UserServiceBL(this.Context).RemoveSaveForLater(skuID, this.Context);
            if (isRemoved)
            {
                operationResultDTO.Message = "Item removed successfully";
                operationResultDTO.operationResult = OperationResult.Success;
            }
            else
            {
                operationResultDTO.Message = "Please try later";
                operationResultDTO.operationResult = OperationResult.Error;
            }
            return operationResultDTO;
        }

        public OperationResultDTO UpdateCartDelivery(CartProductDTO cartDTO)
        {
            var operationResultDTO = new OperationResultDTO();
            var isShoppingCartUpdated = new ShoppingCartBL(this.Context).UpdateCartDelivery(cartDTO, this.Context);

            if (isShoppingCartUpdated)
            {
                operationResultDTO.Message = "Delivery Updated Successfully";
                operationResultDTO.operationResult = OperationResult.Success;
            }
            else
            {
                operationResultDTO.Message = "Please Try Again";
                operationResultDTO.operationResult = OperationResult.Error;
            }

            return operationResultDTO;
        }

        public ContactDTO GetLastShippingAddress(long addressID)
        {
            return new AccountBL(this.Context).GetLastShippingAddressContacts((long)this.Context.LoginID, addressID,
                string.IsNullOrEmpty(this.Context.SiteID) ? 0 : int.Parse(this.Context.SiteID));
        }

        public ContactDTO GetBillingAddress()
        {
            return new AccountBL(this.Context).GetBillingAddressContacts((long)this.Context.LoginID);
        }

        public ContactDTO GetAddressByContactID(long contactID)
        {
            return new AccountBL(this.Context).GetAddressByContactID(contactID, (long)this.Context.LoginID);
        }

        public OperationResultDTO UpdateContact(ContactDTO contactDTO)
        {
            var operationResultDTO = new OperationResultDTO();
            var isAddressUpdated = false;
            if (contactDTO.ContactID != 0)
            {
                isAddressUpdated = new AccountBL(this.Context).UpdateContact(contactDTO);
            }
            else
            {
                contactDTO.IsBillingAddress = true;
                contactDTO.IsShippingAddress = false;
                isAddressUpdated = new AccountBL(this.Context).AddContact(contactDTO, this.Context);
            }
            if (isAddressUpdated)
            {
                operationResultDTO.Message = ResourceHelper.GetValue("AddressUpdatedSuccessfully", this.Context.LanguageCode);
                operationResultDTO.operationResult = OperationResult.Success;
            }
            else
            {
                operationResultDTO.Message = ResourceHelper.GetValue("Pleasetrylater", this.Context.LanguageCode);
                operationResultDTO.operationResult = OperationResult.Error;
            }
            return operationResultDTO;
        }

        public List<AreaDTO> GetAreaByCountryID(int? countryID)
        {
            return new MutualBL(this.Context).GetAreaByCountryID(countryID);
        }

        public List<CityDTO> GetCityByCountryID(int? countryID)
        {
            var listCity = new List<CityDTO>();
            listCity = new MutualBL(this.Context).GetCityByCountryID(countryID);
            return listCity;
        }

        public List<AreaDTO> GetAreaByCityID(int? cityID)
        {
            return new MutualBL(this.Context).GetAreaByCityID(cityID);
        }

        public long AddContact(ContactDTO contactDTO)
        {
            var isAddressAdded = new AccountBL(this.Context).AddContactContactID(contactDTO, this.Context);
            return isAddressAdded;
        }

        public List<ContactDTO> GetShippingAddressContacts()
        {
            int siteID = string.IsNullOrEmpty(this.Context.SiteID) ? 0 : int.Parse(this.Context.SiteID);

            return new AccountBL(this.Context).GetShippingAddressContacts((long)this.Context.LoginID, siteID);
        }

        public OperationResultDTO RemoveContact(long contactID)
        {
            var operationResultDTO = new OperationResultDTO();
            var isAddressDeleted = new AccountBL(this.Context).RemoveContact(contactID);
            if (isAddressDeleted)
            {
                operationResultDTO.Message = ResourceHelper.GetValue("AddressDeletedSuccessfully", this.Context.LanguageCode);
                operationResultDTO.operationResult = OperationResult.Success;
            }
            else
            {
                operationResultDTO.Message = ResourceHelper.GetValue("Pleasetrylater", this.Context.LanguageCode);
                operationResultDTO.operationResult = OperationResult.Error;
            }
            return operationResultDTO;
        }

        public VoucherDTO ValidateVoucher(string voucherNo)
        {
            return new ShoppingCartBL(this.Context).ApplyVoucher(voucherNo, this.Context);
            //var cartDTO = GetCartDetails();
            //return new VoucherBL(this.Context).GetVoucherDetails(voucherNo, (long)this.Context.LoginID, decimal.Parse(cartDTO.Total));
        }

        public long DefaultPaymentOption()
        {
            if (siteID == 2)
            {
                return 10;
            }
            else
            {
                return 1;
            }
        }

        public bool CheckCartItems(CartDTO cartItems)
        {
            var isError = false;
            //var cartItems = new ShoppingCartBL(this.Context).GetCart(this.Context, withCurrencyConversion: false);
            if (cartItems.Products == null || cartItems.IsCartItemDeleted || cartItems.IsCartItemOutOfStock || cartItems.IsCartItemQuantityAdjusted || cartItems.Products.Count <= 0)
            {
                isError = true;
            }
            return isError;
        }

        public bool CheckVoucherBeforePayment(string voucherNo)
        {
            var isError = false;
            var validateVoucherDTO = ValidateVoucher(voucherNo);
            if (validateVoucherDTO.Status != Status.Valid)
            {
                isError = true;
            }
            return isError;
        }

        public bool CheckCartTotal(CheckoutPaymentDTO checkoutPaymentDTO, CartDTO cartItems)
        {
            bool isError = false;
            var cartTotal = Convert.ToDecimal(cartItems.Total);
            var paymentGateWay = new ShoppingCartBL(this.Context).GetPaymentGatewayType(checkoutPaymentDTO.SelectedPaymentOption);
            if (cartTotal <= 0 && paymentGateWay != Framework.Payment.PaymentGatewayType.VOUCHER)
            {
                isError = true;
            }
            return isError;
        }

        public bool CustomerBlockCheckAmount(CartDTO cartItems)
        {
            bool isError = false;
            long customerID = Convert.ToInt64(this.Context.UserId);
            if (cartItems.IsEmailDeliveryInCart)
            {
                var isDigitalCheck = new TransactionBL(this.Context).DigitalAmountCustomerCheck(customerID, (long)Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.SalesOrder, cartItems.CartDigitalAmount, (int)this.Context.CompanyID.Value);
                if (!isDigitalCheck.IsAllowed)
                {
                    isError = true;
                }
            }
            return isError;
        }

        public bool IsCustomerAddressValidCart(CartDTO cartItems, CheckoutPaymentDTO checkoutPaymentDTO)
        {
            bool isError = false;
            if ((cartItems.Products.Any(a => a.DeliveryTypeID != (int)DeliveryTypes.Email)))
            {
                var contactDTO = GetAddressByContactID(long.Parse(checkoutPaymentDTO.SelectedShippingAddress));
                if (string.IsNullOrEmpty(contactDTO.City) || string.IsNullOrEmpty(contactDTO.MobileNo1))
                {
                    isError = true;
                }
            }
            return isError;
        }

        public bool IsStudentAddressValidCart(CartDTO cartItems, CheckoutPaymentDTO checkoutPaymentDTO)
        {
            bool isError = false;
            //if ((cartItems.Products.Any(a => a.DeliveryTypeID != (int)DeliveryTypes.Email)))
            //{
            //    var contactDTO = GetStudentDetailsByStudentID(long.Parse(checkoutPaymentDTO.SelectedStudentID));

            //}
            return isError;
        }

        public StudentDTO GetStudentDetailsByStudentID(long studentID)
        {
            return StudentMapper
                .Mapper(Context)
                .GetStudentDetailsByStudentID(studentID);
        }
        public OperationResultDTO ValidationBeforePayment(CheckoutPaymentDTO checkoutPaymentDTO)
        {
            var cartItems = new ShoppingCartBL(this.Context).GetCart(this.Context, withCurrencyConversion: false);
            var operationResultDTO = new OperationResultDTO();
            if (CheckCartItems(cartItems))
            {
                operationResultDTO.Message = ResourceHelper.GetValue("OneormoreitemsinyourcartcannotbepurchasedPleasereviewyourcart", this.Context.LanguageCode);
                operationResultDTO.operationResult = OperationResult.Error;
                return operationResultDTO;
            }
            if (checkoutPaymentDTO.VoucherAmount != 0 && !string.IsNullOrEmpty(checkoutPaymentDTO.VoucherNo))
            {
                if (CheckVoucherBeforePayment(checkoutPaymentDTO.VoucherNo))
                {
                    operationResultDTO.Message = ResourceHelper.GetValue("Voucherhasalreadybeenredeemed", this.Context.LanguageCode);
                    operationResultDTO.operationResult = OperationResult.Error;
                    return operationResultDTO;
                }
            }
            if (CheckCartTotal(checkoutPaymentDTO, cartItems))
            {
                operationResultDTO.Message = ResourceHelper.GetValue("CartEmpty", this.Context.LanguageCode);
                operationResultDTO.operationResult = OperationResult.Error;
                return operationResultDTO;
            }

            if (CustomerBlockCheckAmount(cartItems))
            {
                operationResultDTO.Message = ResourceHelper.GetValue("MaxpurchaselimitfordigitalproductsisoverforthedayPleasereviewyourcart", this.Context.LanguageCode);
                operationResultDTO.operationResult = OperationResult.Error;
                return operationResultDTO;
            }

            if (IsStudentAddressValidCart(cartItems, checkoutPaymentDTO))
            {
                operationResultDTO.Message = ResourceHelper.GetValue("UpdateDeliveryAddress", this.Context.LanguageCode);
                operationResultDTO.operationResult = OperationResult.Error;
                return operationResultDTO;
            }

            operationResultDTO.Message = "Validation Completed";
            operationResultDTO.operationResult = OperationResult.Success;

            return operationResultDTO;
        }

        public bool isVoucherPayment(CheckoutPaymentDTO checkoutPaymentDTO)
        {
            var paymentGateWay = new ShoppingCartBL(this.Context).GetPaymentGatewayType(checkoutPaymentDTO.SelectedPaymentOption);
            if (paymentGateWay == Framework.Payment.PaymentGatewayType.VOUCHER || paymentGateWay == Framework.Payment.PaymentGatewayType.COD)
            {
                return true;
            }
            return false;
        }

        public CheckoutPaymentMobileDTO SaveWebsiteOrder(CheckoutPaymentDTO checkoutPaymentDTO)
        {
            var checkpaymentMobileDTO = new CheckoutPaymentMobileDTO();
            checkpaymentMobileDTO.operationResult = new OperationResultDTO();
            checkpaymentMobileDTO.DeviceVersion = checkoutPaymentDTO.DeviceVersion;
            checkpaymentMobileDTO.DevicePlatorm = checkoutPaymentDTO.DevicePlatorm;
            try
            {


                if (!new ShoppingCartBL(this.Context).UpdateCartStatus(long.Parse(checkoutPaymentDTO.ShoppingCartID), Eduegate.Framework.Helper.Enums.ShoppingCartStatus.PaymentInitiated, null, Eduegate.Framework.Helper.Enums.ShoppingCartStatus.InProcess))
                {
                    checkpaymentMobileDTO.operationResult.operationResult = OperationResult.Error;
                    checkpaymentMobileDTO.operationResult.Message = ResourceHelper.GetValue("Pleasetrylater", this.Context.LanguageCode);
                    return checkpaymentMobileDTO;
                }

                var payment = new Eduegate.Services.Contracts.Payments.PaymentDTO();
                var paymentGateWay = new ShoppingCartBL(this.Context).GetPaymentGatewayType(checkoutPaymentDTO.SelectedPaymentOption, cartStatus: ShoppingCartStatus.PaymentInitiated);
                var cartTotal = Convert.ToDecimal(new ShoppingCartBL(this.Context).GetCartTotal(this.Context, false, true, cartStatus: Eduegate.Framework.Helper.Enums.ShoppingCartStatus.PaymentInitiated));

                if (cartTotal <= 0 && paymentGateWay != Framework.Payment.PaymentGatewayType.VOUCHER)
                {
                    checkpaymentMobileDTO.operationResult.operationResult = OperationResult.Error;
                    checkpaymentMobileDTO.operationResult.Message = ResourceHelper.GetValue("CartEmpty", this.Context.LanguageCode);
                }
                else
                {
                    var cartDetails = GetCartDetails(ShoppingCartStatus.PaymentInitiated);
                    var cartID = cartDetails.ShoppingCartID;
                    var branchTransfer = new OrderBL(this.Context).SetTemporaryBranchTransfer(cartID);

                    string transactionID = "";
                    var listOfTransID = new List<string>();
                    string[] transactionIDSplit = listOfTransID.ToArray();
                    //var transactionHead = new TransactionHeadDTO();
                    var DTO = new Services.Contracts.OrderHistory.OrderHistoryDTO();
                    payment.CustomerID = this.Context.UserId;
                    payment.Amount = Convert.ToString(cartTotal);
                    payment.InitiatedFromIP = this.Context.IPAddress;
                    payment.EmailId = this.Context.EmailID;

                    switch (paymentGateWay)
                    {
                        case Framework.Payment.PaymentGatewayType.COD:
                            payment.PaymentGateway = Eduegate.Services.Contracts.Enums.PaymentGatewayType.COD;
                            GenerateOrder(payment.PaymentGateway.ToString(), 0, ref checkpaymentMobileDTO);
                            //transactionID = new OrderBL(this.Context).ConfirmOnlineOrder(payment.PaymentGateway.ToString(), 0, this.Context);
                            //transactionIDSplit = transactionID.Split(',');
                            //for (var i = 0; i <= transactionIDSplit.Length - 1; i++)
                            //{
                            //    if (i == 0)
                            //        checkpaymentMobileDTO.CartID = new ShoppingCartBL(this.Context).GetCartDetailByHeadID(Convert.ToInt64(transactionIDSplit[i]));
                            //    DTO = new UserServiceBL(this.Context).GetOrderHistoryDetails(settingDetail.SettingValue, 0, 1, this.Context.UserId, this.Context, long.Parse(transactionIDSplit[i]), false).FirstOrDefault();
                            //    //transactionHead = new OrderBL(this.Context).GetTransactionDetails(Convert.ToInt64(transactionIDSplit[i]));
                            //    if (string.IsNullOrEmpty(checkpaymentMobileDTO.TransactionNo))
                            //    {
                            //        checkpaymentMobileDTO.TransactionNo = DTO.TransactionNo + " : " + DTO.DeliveryText;
                            //    }
                            //    else
                            //    {
                            //        checkpaymentMobileDTO.TransactionNo = checkpaymentMobileDTO.TransactionNo + "," + DTO.TransactionNo + " : " + DTO.DeliveryText;
                            //    }
                            //    SendNotification(DTO.TransactionOrderIID, emailNotificationTypeDetail, DTO.TransactionNo, RootUrl);
                            //}

                            checkpaymentMobileDTO.operationResult.operationResult = OperationResult.Success;
                            checkpaymentMobileDTO.operationResult.Message = "Order generated successfully";
                            //checkpaymentMobileDTO.TransactionNo = transactionHead.TransactionNo;
                            break;
                        case Framework.Payment.PaymentGatewayType.VOUCHER:
                            payment.PaymentGateway = Eduegate.Services.Contracts.Enums.PaymentGatewayType.VOUCHER;
                            GenerateOrder(payment.PaymentGateway.ToString(), 0, ref checkpaymentMobileDTO);
                            //transactionID = new OrderBL(this.Context).ConfirmOnlineOrder(payment.PaymentGateway.ToString(), 0, this.Context);
                            ////transactionHead = new OrderBL(this.Context).GetTransactionDetails(transactionID);
                            //transactionIDSplit = transactionID.Split(',');
                            //for (var i = 0; i <= transactionIDSplit.Length - 1; i++)
                            //{
                            //    if (i == 0)
                            //        checkpaymentMobileDTO.CartID = new ShoppingCartBL(this.Context).GetCartDetailByHeadID(Convert.ToInt64(transactionIDSplit[i]));
                            //    DTO = new UserServiceBL(this.Context).GetOrderHistoryDetails(settingDetail.SettingValue, 0, 1, this.Context.UserId, this.Context, long.Parse(transactionIDSplit[i]), false).FirstOrDefault();
                            //    //transactionHead = new OrderBL(this.Context).GetTransactionDetails(Convert.ToInt64(transactionIDSplit[i]));
                            //    if (string.IsNullOrEmpty(checkpaymentMobileDTO.TransactionNo))
                            //    {
                            //        checkpaymentMobileDTO.TransactionNo = DTO.TransactionNo + " : " + DTO.DeliveryText;
                            //    }
                            //    else
                            //    {
                            //        checkpaymentMobileDTO.TransactionNo = checkpaymentMobileDTO.TransactionNo + "," + DTO.TransactionNo + " : " + DTO.DeliveryText;
                            //    }
                            //    SendNotification(DTO.TransactionOrderIID, emailNotificationTypeDetail, DTO.TransactionNo, RootUrl);
                            //}
                            checkpaymentMobileDTO.operationResult.operationResult = OperationResult.Success;
                            checkpaymentMobileDTO.operationResult.Message = "Order generated successfully";
                            //checkpaymentMobileDTO.TransactionNo = transactionHead.TransactionNo;
                            break;
                        case Framework.Payment.PaymentGatewayType.THEFORT:
                            payment.PaymentGateway = Eduegate.Services.Contracts.Enums.PaymentGatewayType.THEFORT;
                            SetFortGateway(ref checkpaymentMobileDTO, ref payment, cartDetails);
                            checkpaymentMobileDTO.PostObject = Newtonsoft.Json.JsonConvert.SerializeObject(checkpaymentMobileDTO.PostObject);
                            break;
                        case Framework.Payment.PaymentGatewayType.KNET:
                            payment.PaymentGateway = Eduegate.Services.Contracts.Enums.PaymentGatewayType.KNET;
                            //SetKnetGateway(ref checkpaymentMobileDTO, ref payment, cartDetails);
                            checkpaymentMobileDTO.RedirectUrl = ConfigurationExtensions.GetAppConfigValue("KnetMobilePage");
                            checkpaymentMobileDTO.operationResult.operationResult = OperationResult.Success;
                            checkpaymentMobileDTO.operationResult.Message = "Knet Redirection";
                            checkpaymentMobileDTO.paymentDTO = payment;
                            var postObject = PaymentTracker.GetTrackId("1234567890", 10);
                            checkpaymentMobileDTO.PostObject = Newtonsoft.Json.JsonConvert.SerializeObject(SetKnetParameters(postObject));

                            checkpaymentMobileDTO.RedirectUrl = string.Concat(checkpaymentMobileDTO.RedirectUrl, "?CompanyID=", Context.CompanyID, "&EmailID=",
                                Context.EmailID, "&IPAddress=", Context.IPAddress, "&LoginID=", Context.LoginID, "&GUID=", Context.GUID, "&CurrencyCode=",
                                Context.CurrencyCode, "&UserId=", Context.UserId, "&ApiKey=", Context.ApiKey, "&LanguageCode=", Context.LanguageCode,
                                "&SiteID=", Context.SiteID, "&CustomerID=", payment.CustomerID, "&Amount=", payment.Amount, "&InitiatedFromIP=", payment.InitiatedFromIP,
                                "&PaymentEmailId=", payment.EmailId, "&PaymentGateway=", (int)payment.PaymentGateway, "&udf5=", postObject);
                            break;
                        case Framework.Payment.PaymentGatewayType.MIGS:
                            payment.PaymentGateway = Eduegate.Services.Contracts.Enums.PaymentGatewayType.MIGS;
                            //SetMIGSGateway(ref checkpaymentMobileDTO, ref payment, cartDetails);
                            checkpaymentMobileDTO.PostObject = Newtonsoft.Json.JsonConvert.SerializeObject(checkpaymentMobileDTO.PostObject);
                            break;
                        case Framework.Payment.PaymentGatewayType.PAYPAL:
                            payment.PaymentGateway = Eduegate.Services.Contracts.Enums.PaymentGatewayType.PAYPAL;
                            //SetPayPalGateway(ref checkpaymentMobileDTO, ref payment, cartDetails);
                            checkpaymentMobileDTO.PostObject = Newtonsoft.Json.JsonConvert.SerializeObject(checkpaymentMobileDTO.PostObject);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                checkpaymentMobileDTO.operationResult.operationResult = OperationResult.Error;
                checkpaymentMobileDTO.operationResult.Message = ResourceHelper.GetValue("Pleasetrylater", this.Context.LanguageCode);
                return checkpaymentMobileDTO;
            }
            //return new WebsiteOrderBL(this.Context).SaveWebsiteOrder(checkoutPaymentDTO);
            return checkpaymentMobileDTO;
        }

        public void GenerateOrder(string paymentGateway, long trackKey, ref CheckoutPaymentMobileDTO checkpaymentMobileDTO)
        {
            var deviceInfo = string.Concat(checkpaymentMobileDTO.DevicePlatorm, "|", checkpaymentMobileDTO.DeviceVersion);
            var emailNotificationTypeDetail = new NotificationBL(this.Context).GetEmailNotificationType(Services.Contracts.Enums.EmailNotificationTypes.OrderConfirmation);
            var settingDetail = new SettingBL().GetSettingDetail(Constants.TransactionSettings.ONLINESALESDOCTTYPEID, (long)this.Context.CompanyID);
            var appType = UserRole.Mobile_App_En;
            switch (this.Context.LanguageCode)
            {
                case "ar":
                    appType = UserRole.Mobile_App_Ar;
                    break;
                default:
                    appType = UserRole.Mobile_App_En;
                    break;
            }
            var transactionID = new OrderBL(this.Context).ConfirmOnlineOrder(paymentGateway, trackKey, this.Context, userRole: appType, deviceInfo: deviceInfo);
            //transactionHead = new OrderBL(this.Context).GetTransactionDetails(transactionID);
            var transactionIDSplit = transactionID.Split(',');
            checkpaymentMobileDTO.orderHistory = new List<Services.Contracts.OrderHistory.OrderHistoryDTO>();
            for (var i = 0; i <= transactionIDSplit.Length - 1; i++)
            {
                if (i == 0)
                    checkpaymentMobileDTO.CartID = new ShoppingCartBL(this.Context).GetCartDetailByHeadID(Convert.ToInt64(transactionIDSplit[i]));
                var DTO = new UserServiceBL(this.Context).GetBriefOrderHistory(settingDetail.SettingValue, long.Parse(transactionIDSplit[i])).FirstOrDefault();
                //var DTO = new UserServiceBL(this.Context).GetOrderHistoryDetails(settingDetail.SettingValue, 0, 1, this.Context.UserId, this.Context, long.Parse(transactionIDSplit[i]), false).FirstOrDefault();
                //transactionHead = new OrderBL(this.Context).GetTransactionDetails(Convert.ToInt64(transactionIDSplit[i]));
                checkpaymentMobileDTO.orderHistory.Add(DTO);
                if (string.IsNullOrEmpty(checkpaymentMobileDTO.TransactionNo))
                {
                    checkpaymentMobileDTO.TransactionNo = DTO.TransactionNo + " : " + DTO.DeliveryText;
                }
                else
                {
                    checkpaymentMobileDTO.TransactionNo = checkpaymentMobileDTO.TransactionNo + "," + DTO.TransactionNo + " : " + DTO.DeliveryText;
                }
                SendNotification(DTO.TransactionOrderIID, emailNotificationTypeDetail, DTO.TransactionNo, RootUrl);
            }
        }

        public CheckoutPaymentMobileDTO ConfirmOnlineOrder(CheckoutPaymentDTO checkoutPaymentDTO)
        {
            var settingDetail = new SettingBL().GetSettingDetail(Constants.TransactionSettings.ONLINESALESDOCTTYPEID, (long)this.Context.CompanyID);

            var checkpaymentMobileDTO = new CheckoutPaymentMobileDTO();
            checkpaymentMobileDTO.operationResult = new OperationResultDTO();
            var trackKey = checkoutPaymentDTO.PostObject;
            var paymentGateWay = new ShoppingCartBL(this.Context).GetPaymentGatewayType(checkoutPaymentDTO.SelectedPaymentOption, cartStatus: ShoppingCartStatus.PaymentInitiated);
            //string transactionID = new OrderBL(this.Context).ConfirmOnlineOrder(paymentGateWay.ToString(), long.Parse(trackKey), this.Context);
            //var listOfTransID = new List<string>();
            //string[] transactionIDSplit = listOfTransID.ToArray();
            //var transactionHead = new TransactionHeadDTO();
            //transactionIDSplit = transactionID.Split(',');
            //var emailNotificationTypeDetail = new NotificationBL(this.Context).GetEmailNotificationType(Services.Contracts.Enums.EmailNotificationTypes.OrderConfirmation);
            //for (var i = 0; i <= transactionIDSplit.Length - 1; i++)
            //{
            //    if (i == 0)
            //        checkpaymentMobileDTO.CartID = new ShoppingCartBL(this.Context).GetCartDetailByHeadID(Convert.ToInt64(transactionIDSplit[i]));
            //    transactionHead = new OrderBL(this.Context).GetTransactionDetails(Convert.ToInt64(transactionIDSplit[i]));

            //    if (string.IsNullOrEmpty(checkpaymentMobileDTO.TransactionNo))
            //    {
            //        checkpaymentMobileDTO.TransactionNo = transactionHead.TransactionNo + " : " + new ShoppingCartBL(this.Context).DeliveryTypeText(transactionHead.DeliveryTypeID.HasValue ? transactionHead.DeliveryDays.Value : 0, transactionHead.DeliveryDays.HasValue ? transactionHead.DeliveryDays.Value : 0);
            //    }
            //    else
            //    {
            //        checkpaymentMobileDTO.TransactionNo = checkpaymentMobileDTO.TransactionNo + "," + transactionHead.TransactionNo + " : " + new ShoppingCartBL(this.Context).DeliveryTypeText(transactionHead.DeliveryTypeID.HasValue ? transactionHead.DeliveryDays.Value : 0, transactionHead.DeliveryDays.HasValue ? transactionHead.DeliveryDays.Value : 0);
            //    }
            //    SendNotification(transactionHead.HeadIID, emailNotificationTypeDetail, transactionHead.TransactionNo, RootUrl);
            //}
            //if (paymentGateWay == PaymentGatewayType.KNET)
            //{
            //    trackKey = new Eduegate.Domain.Payment.PaymentBL(this.Context).GetKnetDetails(long.Parse(trackKey)).ToString();
            //}
            checkpaymentMobileDTO.DeviceVersion = checkoutPaymentDTO.DeviceVersion;
            checkpaymentMobileDTO.DevicePlatorm = checkoutPaymentDTO.DevicePlatorm;
            GenerateOrder(paymentGateWay.ToString(), long.Parse(trackKey), ref checkpaymentMobileDTO);

            checkpaymentMobileDTO.operationResult.operationResult = OperationResult.Success;
            checkpaymentMobileDTO.operationResult.Message = "Order generated successfully";

            return checkpaymentMobileDTO;
        }

        //public CheckoutPaymentMobileDTO SetKnetGateway(string emailID, string customerID, string amount, string initiatedIP, string paymentGateway, string companyID, string loginID, string GUID, string currencyCode, string userId, string apiKey, string languageCode, string siteID, string udf5)
        //{
        //    var paymentDTO = new PaymentDTO();
        //    paymentDTO.EmailId = emailID;
        //    paymentDTO.CustomerID = customerID;
        //    paymentDTO.Amount = amount;
        //    paymentDTO.InitiatedFromIP = initiatedIP;
        //    paymentDTO.PaymentGateway = (PaymentGatewayType)(int.Parse(paymentGateway));
        //    var context = new CallContext();

        //    context.CompanyID = int.Parse(companyID);
        //    context.EmailID = emailID;
        //    context.IPAddress = initiatedIP;
        //    context.LoginID = long.Parse(loginID);
        //    context.GUID = GUID;
        //    context.CurrencyCode = currencyCode;
        //    context.UserId = userId;
        //    context.ApiKey = apiKey;
        //    context.LanguageCode = languageCode;
        //    context.SiteID = siteID;

        //    var checkpaymentMobileDTO = new CheckoutPaymentMobileDTO();
        //    checkpaymentMobileDTO.operationResult = new OperationResultDTO();
        //    var knetpaymentDTO = new KNetGateway().InitiatePayment(paymentDTO, context, "1", udf5);
        //    checkpaymentMobileDTO.RedirectUrl = knetpaymentDTO.PaymentGatewayUrl;
        //    checkpaymentMobileDTO.RedirectValues = new List<KeyValueDTO>();
        //    checkpaymentMobileDTO.CartID = "";
        //    var knetparam = SetKnetParameters(knetpaymentDTO.TrackKey);
        //    checkpaymentMobileDTO.PostObject = knetparam;
        //    checkpaymentMobileDTO.operationResult.operationResult = OperationResult.Success;
        //    checkpaymentMobileDTO.operationResult.Message = "Knet Redirection";
        //    return checkpaymentMobileDTO;
        //}

        //public void SetMIGSGateway(ref CheckoutPaymentMobileDTO checkpaymentMobileDTO, ref PaymentDTO paymentDTO, CartDTO cartDetail)
        //{
        //    var migspaymentDTO = new MIGSGateway().InitiatePayment(paymentDTO, this.Context);
        //    var migsparam = SetKnetParameters(migspaymentDTO.TrackID);
        //    checkpaymentMobileDTO.PostObject = migsparam;
        //    checkpaymentMobileDTO.RedirectUrl = string.Concat(ConfigurationExtensions.GetAppConfigValue("MIGSMobilePage"), "?redirect=", migspaymentDTO.PaymentGatewayUrl);
        //    checkpaymentMobileDTO.operationResult.operationResult = OperationResult.Success;
        //    checkpaymentMobileDTO.operationResult.Message = "MIGS Redirection";
        //}

        //public void SetPayPalGateway(ref CheckoutPaymentMobileDTO checkpaymentMobileDTO, ref PaymentDTO paymentDTO, CartDTO cartDetail)
        //{
        //    var paypalDTO = new PaypalPaymentDTO();
        //    var requestParameters = new PaypalGateway().InitiatePaymentMobile(ref paymentDTO, ref paypalDTO, this.Context);

        //    checkpaymentMobileDTO.RedirectUrl = string.Concat(ConfigurationExtensions.GetAppConfigValue("PayPalMobilePage"), "?redirect=", paypalDTO.PaymentGatewayUrl);
        //    for (var i = 0; i <= requestParameters.Count - 1; i++)
        //    {
        //        checkpaymentMobileDTO.RedirectUrl = string.Concat(checkpaymentMobileDTO.RedirectUrl, "&", requestParameters.Keys.ElementAt(i), "=", requestParameters.Values.ElementAt(i));
        //    }
        //    requestParameters.Add("merchant_reference", paymentDTO.TrackID.ToString());
        //    checkpaymentMobileDTO.PostObject = requestParameters;
        //    checkpaymentMobileDTO.operationResult.operationResult = OperationResult.Success;
        //    checkpaymentMobileDTO.operationResult.Message = "Paypal Redirection";
        //}

        public void SetFortGateway(ref CheckoutPaymentMobileDTO checkpaymentMobileDTO, ref Eduegate.Services.Contracts.Payments.PaymentDTO paymentDTO, CartDTO cartDetail)
        {
            paymentDTO.TrackID = paymentDTO.TrackID.IsNullOrEmpty() ? PaymentTracker.GetTrackId("1234567890", 10) : paymentDTO.TrackID;
            paymentDTO.TrackKey = PaymentTracker.GetUniqueKey();
            paymentDTO.PaymentID = PaymentTracker.GetTrackId("1234567890", 10);
            paymentDTO.TransactionStatus = ((int)Eduegate.Framework.Payment.TransactionStatus.Initiated).ToString();
            paymentDTO.InitiatedFromIP = this.Context.IPAddress;
            paymentDTO.InitiatedLocation = Utility.Dot2LongIP(this.Context.IPAddress).ToString();
            if (cartDetail.IsNotNull() && cartDetail.Products.IsNotNull() && cartDetail.Products.Count > 0)
            {
                paymentDTO.Amount = cartDetail.Total;
                paymentDTO.EmailId = this.Context.EmailID;
                string fortLanguage = this.Context.LanguageCode;  // How to get language
                string signatureString = GetRequestSignatureText(paymentDTO);
                string signatureHash = UtilityHelper.CreateSHA256Hash(signatureString);
                var fortDTO = new TheFortPaymentDTO();

                // From fortDTO
                fortDTO.PShaRequestPhrase = ConfigurationExtensions.GetAppConfigValue("TheFortShaRequestPhrase");
                fortDTO.PAccessCode = ConfigurationExtensions.GetAppConfigValue("TheFortAccessCode");
                fortDTO.PMerchantIdentifier = ConfigurationExtensions.GetAppConfigValue("TheFortMerchantIdentifier");
                fortDTO.PCommand = ConfigurationExtensions.GetAppConfigValue("TheFortPCommand");
                fortDTO.PCurrency = ConfigurationExtensions.GetAppConfigValue("TheFortCurrency");
                fortDTO.PCustomerEmail = this.Context.EmailID;
                fortDTO.PLang = fortLanguage;
                fortDTO.PMerchantReference = paymentDTO.TrackKey;
                fortDTO.PSignatureText = signatureString;
                fortDTO.PSignature = signatureHash;
                fortDTO.PAmount = Convert.ToInt32(Convert.ToDecimal(paymentDTO.Amount) * 100); // Why to multiply by 100
                fortDTO.PTransAmount = Convert.ToInt32(Convert.ToDecimal(paymentDTO.Amount) * 100); // Why to multiply by 100
                fortDTO.RefCountryID = Convert.ToInt16(ConfigurationExtensions.GetAppConfigValue("TheFortCountryID"));
                fortDTO.InitIP = this.Context.IPAddress;
                fortDTO.CartID = cartDetail.ShoppingCartID;
                paymentDTO.CustomAttributes = Newtonsoft.Json.JsonConvert.SerializeObject(fortDTO);

                var fortParams = GetTheFortParameters(fortDTO);

                bool isSuccess = new Eduegate.Domain.Payment.PaymentBL(this.Context).CreatePaymentRequest(paymentDTO);
                if (isSuccess)
                {
                    checkpaymentMobileDTO.PostObject = fortParams;
                    if (ConfigurationExtensions.GetAppConfigValue("UseTheFortSandbox").Trim().ToLower() == "true")
                        checkpaymentMobileDTO.RedirectUrl = ConfigurationExtensions.GetAppConfigValue("TheFortPurchaseTestUrl");
                    else
                        checkpaymentMobileDTO.RedirectUrl = ConfigurationExtensions.GetAppConfigValue("TheFortPurchaseLiveUrl");

                    //foreach (var param in fortParams)
                    //{
                    //    checkpaymentMobileDTO.RedirectUrl = string.Concat(checkpaymentMobileDTO.RedirectUrl, "&", param.Key, "=", param.Value);
                    //}
                    for (var i = 0; i <= fortParams.Count - 1; i++)
                    {
                        checkpaymentMobileDTO.RedirectUrl = string.Concat(checkpaymentMobileDTO.RedirectUrl, i == 0 ? "?" : "&", fortParams.Keys.ElementAt(i), "=", fortParams.Values.ElementAt(i));
                    }
                    checkpaymentMobileDTO.RedirectUrl = string.Concat(ConfigurationExtensions.GetAppConfigValue("TheFortMobilePage"), "?redirect=", checkpaymentMobileDTO.RedirectUrl);
                    checkpaymentMobileDTO.operationResult.operationResult = OperationResult.Success;
                    checkpaymentMobileDTO.operationResult.Message = "Fort Redirection";
                }
                else
                {
                    checkpaymentMobileDTO.operationResult.operationResult = OperationResult.Error;
                    checkpaymentMobileDTO.operationResult.Message = ResourceHelper.GetValue("Pleasetrylater", this.Context.LanguageCode);
                }
            }
            else
            {
                checkpaymentMobileDTO.operationResult.operationResult = OperationResult.Error;
                checkpaymentMobileDTO.operationResult.Message = ResourceHelper.GetValue("Pleasetrylater", this.Context.LanguageCode);
            }

        }

        public Eduegate.Services.Contracts.Payments.PaymentDTO PaymentFailure(CheckoutPaymentDTO checkoutPaymentDTO)
        {
            var paymentDTO = new Eduegate.Services.Contracts.Payments.PaymentDTO();
            //var postObjectJSON = checkoutPaymentDTO.PostObject;
            var trackKey = checkoutPaymentDTO.PostObject;
            var cartIID = new ShoppingCartBL(this.Context).GetLoggedInUserCartIID(ShoppingCartStatus.PaymentInitiated);

            if (cartIID > 0)
            {
                new OrderBL(this.Context).SetTemporaryBranchTransfer(cartIID, true);

                new ShoppingCartBL(this.Context).UpdateCartStatus(cartIID, ShoppingCartStatus.InProcess, null, ShoppingCartStatus.PaymentInitiated);
            }
            Framework.Payment.PaymentGatewayType paymentGateway;
            Enum.TryParse(checkoutPaymentDTO.SelectedPaymentOption, out paymentGateway);
            if (trackKey.IsNotNull())
            {
                if (paymentGateway == Framework.Payment.PaymentGatewayType.KNET)
                {
                    trackKey = new Eduegate.Domain.Payment.PaymentBL(this.Context).GetKnetDetails(long.Parse(trackKey)).ToString();
                }
                trackKey = string.Concat(trackKey, "_", (int)(paymentGateway));
                paymentDTO = new Eduegate.Domain.Payment.PaymentBL(this.Context).GetPaymentDetails(0, trackKey);
            }
            return paymentDTO;
        }

        public List<Services.Contracts.OrderHistory.OrderHistoryDTO> GetOrderHistoryDetails(int pageSize, int pageNo)
        {
            //var userDTO = GetUserDetails();
            var settingDetail = new SettingBL().GetSettingDetail(Constants.TransactionSettings.SYSBLOCKEDBRANCH, (long)this.Context.CompanyID);
            var customerID = GetUserDetails().Customer.CustomerIID;
            var orderhistoryDTOList = new UserServiceBL(this.Context).GetOrderHistoryDetailsWithPagination(settingDetail.SettingValue, customerID.ToString(), pageNo, pageSize, Context);
            //var orderhistoryDTO = new UserServiceBL(this.Context).GetOrderHistoryDetails(settingDetail.SettingValue.ToString(), 1, 100, userDTO.Customer.CustomerIID.ToString(), this.Context, 0, true);
            //foreach (var order in orderhistoryDTO)
            //{
            //    foreach (var product in order.OrderDetails)
            //    {
            //        product.ProductImageUrl = ConfigurationExtensions.GetAppConfigValue("ImageHostUrl") + product.ProductImageUrl;
            //    }
            //    //var formatstringdate = string.Format(Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("DateFormat"), order.TransactionDate).ToString();
            //    //order.TransactionDate = DateTime.Parse(formatstringdate);
            //}
            foreach (var order in orderhistoryDTOList)
            {
                order.Currency = ResourceHelper.GetValue(order.Currency, this.Context.LanguageCode);
            }

            return orderhistoryDTOList;
        }


        public List<Services.Contracts.OrderHistory.OrderHistoryDTO> GetOrderHistoryItemDetail(long headID)
        {
            var settingDetail = new SettingBL().GetSettingDetail(Constants.TransactionSettings.SYSBLOCKEDBRANCH, (long)this.Context.CompanyID);
            var customerID = GetUserDetails().Customer.CustomerIID;
            var orderhistoryDTOList = new UserServiceBL(this.Context).GetOrderHistoryDetails(settingDetail.SettingValue, 1, 10, customerID.ToString(), orderID: headID, withCurrencyConversion: false, context: this.Context);
            foreach (var order in orderhistoryDTOList)
            {
                order.Currency = ResourceHelper.GetValue(order.Currency, this.Context.LanguageCode);
                foreach (var product in order.OrderDetails)
                {
                    product.ProductImageUrl = ProductImageURL + product.ProductImageUrl;
                }
                //var formatstringdate = string.Format(Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("DateFormat"), order.TransactionDate).ToString();
                //order.TransactionDate = DateTime.Parse(formatstringdate);
            }
            return orderhistoryDTOList;
        }

        public int GetOrderHistoryCount()
        {
            var userDTO = GetUserDetails();
            var settingDetail = new SettingBL().GetSettingDetail(Constants.TransactionSettings.SYSBLOCKEDBRANCH, (long)this.Context.CompanyID);
            return new UserServiceBL(this.Context).GetOrderHistoryCount(settingDetail.SettingValue, userDTO.Customer.CustomerIID);
            //return 0;
        }

        public UserDTO GetPersonalSettings()
        {
            var userDTO = GetUserDetails();
            var contactDTO = GetBillingAddress();
            //userDTO.Customer.Contacts = new List<ContactDTO>();
            //userDTO.Customer.Contacts.Add(contactDTO);
            userDTO.Customer.TelephoneNumber = contactDTO.MobileNo1;
            return userDTO;
        }

        public OperationResultDTO SavePersonalSettings(UserDTO userDTO)
        {
            var operationResultDTO = new OperationResultDTO();
            //var contactDTO = userDTO.Customer.Contacts.First();
            //var isDuplicate = new AccountBL(this.Context).isDuplicateUserAvailableEmailPhone(userDTO.LoginEmailID, userDTO.Customer.TelephoneNumber, long.Parse(this.Context.LoginID.ToString()));
            //if (!isDuplicate)
            //{
            var isSaved = new UserServiceBL(this.Context).UpdatePersonalProfileDetails(userDTO);
            if (isSaved)
            {
                operationResultDTO.Message = ResourceHelper.GetValue("PersonalProfileUpdated", this.Context.LanguageCode);
                operationResultDTO.operationResult = OperationResult.Success;
            }
            else
            {
                operationResultDTO.Message = ResourceHelper.GetValue("Pleasetrylater", this.Context.LanguageCode);
                operationResultDTO.operationResult = OperationResult.Error;
            }
            //}
            //else
            //{
            //    operationResultDTO.Message = ResourceHelper.GetValue("EmailIDMobileNoAlreadyExists", this.Context.LanguageCode);
            //    operationResultDTO.operationResult = OperationResult.Error;
            //}
            return operationResultDTO;
        }

        public OperationResultDTO UpdatePassword(ChangePasswordDTO changePasswordDTO)
        {
            byte appID = (byte)ApplicationType.ERP;
            var operationResultDTO = new OperationResultDTO();
            var isPasswordValid = new AccountBL(this.Context).Login(this.Context.EmailID, changePasswordDTO.OldPassword, appID);
            if (isPasswordValid)
            {
                KeyValuePair<Common, string> responseLoginIID = new AccountBL(this.Context).ForgotPassword(this.Context.EmailID);
                if (responseLoginIID.Key.ToString().ToUpper() == "SUCCESS")
                {
                    var userDTO = new UserDTO();
                    userDTO.LoginUserID = responseLoginIID.Value;
                    userDTO.LoginEmailID = this.Context.EmailID;
                    userDTO.PasswordSalt = PasswordHash.CreateHash(changePasswordDTO.NewPassword);
                    userDTO.Password = StringCipher.Encrypt(changePasswordDTO.NewPassword, userDTO.PasswordSalt);
                    var result = new AccountBL(this.Context).ResetPassword(userDTO);
                    if (result == Common.Success)
                    {
                        operationResultDTO.Message = ResourceHelper.GetValue("PasswordChanged", this.Context.LanguageCode);
                        operationResultDTO.operationResult = OperationResult.Success;
                    }
                    else
                    {
                        operationResultDTO.Message = ResourceHelper.GetValue("Pleasetrylater", this.Context.LanguageCode);
                        operationResultDTO.operationResult = OperationResult.Error;
                    }
                }
            }
            else
            {
                operationResultDTO.Message = ResourceHelper.GetValue("Currentpassworddoesnotmatch", this.Context.LanguageCode);
                operationResultDTO.operationResult = OperationResult.Error;
            }
            return operationResultDTO;
        }

        public PageDTO GetPageInfo(long pageID, string parameter)
        {
            //var cultureIDContext = new UtilityBL().GetLanguageCultureId(this.Context.LanguageCode).CultureID;
            var pageDTO = new PageRenderBL(this.Context).GetPageInfo(pageID, parameter);
            //pageDTO.BoilerPlates = pageDTO.BoilerPlates.Where(a => a.BoilerPlateID == 20).ToList();
            if (pageDTO.BoilerPlates != null)
            {
                foreach (var page in pageDTO.BoilerPlates)
                {
                    foreach (var runparameter in page.RuntimeParameters)
                    {
                        var cultureText = new PageRenderRepository().GetBoilerPlateCultureData(page.BoilerplateMapIID, runparameter.Key, cultureIDContext);
                        runparameter.Value = cultureText;
                    }
                }
            }

            return pageDTO;
        }

        public List<CategoryImageMapDTO> GetCategoryBanner(BoilerPlateDTO boilerPlateDTO)
        {
            var categoryIDCategoryBanner = boilerPlateDTO.RuntimeParameters.Find(a => a.Key == "CategoryID").Value;
            Eduegate.Services.Contracts.Enums.ImageTypes imageType;
            if (this.Context.LanguageCode == "ar")
            {
                Enum.TryParse<Eduegate.Services.Contracts.Enums.ImageTypes>(boilerPlateDTO.RuntimeParameters.Find(a => a.Key == "ImageTypeAr").Value, true, out imageType);
            }
            else
            {
                Enum.TryParse<Eduegate.Services.Contracts.Enums.ImageTypes>(boilerPlateDTO.RuntimeParameters.Find(a => a.Key == "ImageType").Value, true, out imageType);
            }

            var categoryImageMapList = new CategoryBL(this.Context).GetCategoriesBannerbyCategoryID(imageType, long.Parse(categoryIDCategoryBanner));
            var categoryImageDTOList = new List<CategoryImageMapDTO>();
            foreach (var categoryImageMap in categoryImageMapList)
            {
                ActionLinkTypes actionLinkTypes;
                Enum.TryParse<ActionLinkTypes>(categoryImageMap.ActionLinkTypeID.ToString(), true, out actionLinkTypes);
                categoryImageMap.ImageFile = string.Format("{0}/{1}/{2}", CategoryURL, imageType.ToString(), categoryImageMap.ImageFile);
                var isInternalLink = false;
                var link = categoryImageMap.ImageLinkParameters;
                GetBannerLink(actionLinkTypes, ref link, ref isInternalLink);
                categoryImageMap.ImageLinkParameters = link;
                var dto = CategoryImageMapper.Mapper(this.Context).ToDTO(categoryImageMap);
                dto.isInternalLink = isInternalLink;
                categoryImageDTOList.Add(dto);
            }
            return categoryImageDTOList;
        }

        public Eduegate.Services.Contracts.SearchData.SearchResultDTO CategoryProductLists(string categoryCode, BoilerPlateDTO boilerPlateDTO)
        {
            return GetProductsfromSolr(1, 24, "", categoryCode, "category", "relevance", "category");
        }

        public List<CategoryDTO> GetSubCategories(long categoryID)
        {
            return new CategoryBL(this.Context).GetSubCategoriesDTO(categoryID);
        }

        public CategoryDTO CategoryDetails(long categoryID)
        {
            return CategoryMapper.Mapper(this.Context).ToDTO(new CategoryRepository().GetCategoryByID(categoryID, this.Context.LanguageCode));
        }

        public CategoryDTO GetCategoryByCode(string categoryCode)
        {
            return new CategoryBL(this.Context).GetCategoryByCode(categoryCode);
        }

        public List<ProductImageMapDTO> GetProductDetailImages(long skuID)
        {
            var productImageDTO = new ProductBL(this.Context).GetProductDetailImages(skuID);
            foreach (var imageDTO in productImageDTO)
            {
                imageDTO.GalleryImage = string.Format("{0}/{1}", ProductImageURL, imageDTO.ListingImage);
                imageDTO.ListingImage = string.Format("{0}/{1}", ProductImageURL, imageDTO.ListingImage);
                imageDTO.ZoomImage = string.Format("{0}/{1}", ProductImageURL, imageDTO.ZoomImage);
                //imageDTO.ThumbnailImage = string.Format("{0}/{1}", ProductImageURL, imageDTO.ThumbnailImage);
            }
            return productImageDTO;
        }

        public ProductSKUDetailDTO GetProductDetail(long skuID, long cultureID)
        {
            var dto = new ProductBL(this.Context).GetProductBySKU(skuID, cultureIDContext);
            dto.Currency = ResourceHelper.GetValue(dto.Currency, this.Context != null ? this.Context.LanguageCode : "en");
            return dto;
        }

        public List<ProductDetailKeyFeatureDTO> GetProductFeatures(long skuID)
        {
            return new ProductBL(this.Context).GetProductKeyFeatures(skuID);
        }

        public List<ProductSKUVariantDTO> GetProductVariant(long skuID, long cultureID)
        {

            return new ProductBL(this.Context).GetProductVariants(skuID, cultureIDContext);
        }

        public List<ProductMultiPriceDTO> GetProductMultiPriceDetails(long skuID)
        {
            long customerID = 0;
            try
            { customerID = GetUserDetails().Customer.CustomerIID; }
            catch (Exception ex) { customerID = 0; }
            var productInventory = new ProductDetailBL(this.Context).GetInventoryDetailsSKUID(skuID, customerID,
                companyID: this.Context != null && this.Context.CompanyID.HasValue ? this.Context.CompanyID.Value : 1);
            var dtoList = new ProductDetailBL(this.Context).GetProductMultiPriceDetails(productInventory.BranchID, skuID, customerID);
            foreach (var dto in dtoList)
            {
                dto.Currency = ResourceHelper.GetValue(dto.Currency, this.Context.LanguageCode);
            }
            return dtoList;
        }


        public List<ProductQuantityDiscountDTO> GetProductQtyDiscountDetails(long skuID)
        {
            long customerID = 0;
            try
            { customerID = GetUserDetails().Customer.CustomerIID; }
            catch (Exception ex) { customerID = 1; }
            var productInventory = new ProductDetailBL(this.Context).GetInventoryDetailsSKUID(skuID, customerID, companyID: this.Context != null && this.Context.CompanyID.HasValue ? this.Context.CompanyID.Value : 1);
            var dtoList = new ProductDetailBL(this.Context).GetProductQtyDiscountDetails(productInventory.BranchID, skuID, customerID);
            foreach (var dto in dtoList)
            {
                dto.Currency = ResourceHelper.GetValue(dto.Currency, this.Context.LanguageCode);
            }
            return dtoList;
        }

        public ProductInventoryConfigDTO GetProductDetailsDescription(long skuID, long cultureID)
        {
            return new ProductBL(this.Context).GetProductDetailsDescription(skuID, cultureIDContext);
        }

        public string GenerateApiKey(string uuid, string version)
        {
            return string.Concat(uuid, "_", DateTime.Now.ToString(), "_", version);
        }

        public string GenerateGUID()
        {
            return Convert.ToString(Guid.NewGuid());
        }

        public bool MergeCart()
        {
            var result = false;
            result = new ShoppingCartBL(this.Context).MergeCart(this.Context);
            return result;
        }

        public OperationResultDTO UpdateAddressinShoppingCart(CheckoutPaymentDTO checkoutPaymentDTO)
        {
            var operationResultDTO = new OperationResultDTO();
            var contactDTO = GetBillingAddress();
            var cartDTO = new CartDTO();
            cartDTO.BillingAddressID = contactDTO.ContactID;
            var cartDetails = GetCartDetails();
            //cartDetails.ShoppingCartID
            cartDTO.ShoppingCartID = long.Parse(cartDetails.ShoppingCartID.ToString());
            cartDTO.ShippingAddressID = long.Parse(checkoutPaymentDTO.SelectedShippingAddress);
            var isUpdated = new ShoppingCartBL(this.Context).UpdateCart(cartDTO, this.Context);
            if (isUpdated)
            {
                operationResultDTO.Message = ResourceHelper.GetValue("CartUpdatedSuccessfully", this.Context.LanguageCode);
                operationResultDTO.operationResult = OperationResult.Success;
            }
            else
            {
                operationResultDTO.Message = ResourceHelper.GetValue("Pleasetrylater", this.Context.LanguageCode);
                operationResultDTO.operationResult = OperationResult.Error;
            }
            return operationResultDTO;
        }

        public OperationResultDTO ForgotPassword(string emailID)
        {
            var operationResultDTO = new OperationResultDTO();
            KeyValuePair<Common, string> responseLoginIID = new AccountBL(this.Context).ForgotPassword(emailID);
            if (responseLoginIID.Key.ToString().ToUpper() == "SUCCESS")
            {
                string ResetPasswordLink = RootUrl + "Account/ResetPassword/" + responseLoginIID.Value;
                var notificationDTO = new EmailNotificationDTO();
                notificationDTO.ToEmailID = emailID;
                notificationDTO.EmailNotificationType = Eduegate.Services.Contracts.Enums.EmailNotificationTypes.ForgetPassword;
                notificationDTO.AdditionalParameters = new List<Eduegate.Services.Contracts.Commons.KeyValueParameterDTO>();
                notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.ForgetPassword.Keys.ResetPasswordLink, ParameterValue = ResetPasswordLink });
                notificationDTO = GetAdditionalNotificationParameters(notificationDTO, this.Context, RootUrl);
                notificationDTO.FromEmailID = FromAddress;
                var notificationresult = new NotificationBL(this.Context).SaveEmailData(notificationDTO);
                if (notificationresult.EmailMetaDataIID > 0)
                {
                    operationResultDTO.Message = ResourceHelper.GetValue("Pleasecheckyouremailtoresetyourpassword", this.Context.LanguageCode);
                    operationResultDTO.operationResult = OperationResult.Success;
                }
                else
                {
                    operationResultDTO.Message = ResourceHelper.GetValue("Pleasetrylater", this.Context.LanguageCode);
                    operationResultDTO.operationResult = OperationResult.Error;
                }
            }
            else
            {
                if (responseLoginIID.Key.ToString().ToUpper() == "NOTEXISTS")
                {
                    operationResultDTO.Message = ResourceHelper.GetValue("EmailNotRegistered", this.Context.LanguageCode);
                    operationResultDTO.operationResult = OperationResult.Error;
                }
                else
                {
                    operationResultDTO.Message = ResourceHelper.GetValue("Pleasetrylater", this.Context.LanguageCode);
                    operationResultDTO.operationResult = OperationResult.Error;
                }
            }


            return operationResultDTO;
        }

        public List<ProductDetailDeliveryOption> GetProductDeliveryTypeList(long skuID, long cultureID)
        {
            return new ProductBL(this.Context).GetProductDeliveryTypeList(skuID, cultureIDContext);
        }

        public OperationResultDTO AddNewsletterSubscription(Services.Contracts.NewsletterSubscriptionDTO newsletterSubscriptionDTO)
        {
            var operationResultDTO = new OperationResultDTO();
            var checkSubscribed = new CustomerBL(this.Context).AddNewsletterSubsciption(newsletterSubscriptionDTO.emailID, cultureIDContext, this.Context.IPAddress);
            if (checkSubscribed.result == 3)
            {
                operationResultDTO.Message = checkSubscribed.message;
                operationResultDTO.operationResult = OperationResult.Error;
            }
            else if (checkSubscribed.result == 2)
            {
                operationResultDTO.Message = checkSubscribed.message;
                operationResultDTO.operationResult = OperationResult.Error;
            }
            else if (checkSubscribed.result == 1)
            {
                operationResultDTO.Message = checkSubscribed.message;
                operationResultDTO.operationResult = OperationResult.Success;
            }
            else
            {
                operationResultDTO.Message = checkSubscribed.message;
                operationResultDTO.operationResult = OperationResult.Error;
            }
            return operationResultDTO;
        }

        public OperationResultDTO ResendMail(long headID)
        {
            var operationResultDTO = new OperationResultDTO();

            var orderBL = new OrderBL(this.Context);

            var isOrderofUser = orderBL.isOrderOfUser(headID, long.Parse(this.Context.UserId));
            if (!isOrderofUser)
            {
                operationResultDTO.Message = ResourceHelper.GetValue("Pleasetrylater", this.Context.LanguageCode);
                operationResultDTO.operationResult = OperationResult.Error;
            }
            else
            {
                var notificationDTO = new EmailNotificationDTO();
                notificationDTO.ToEmailID = this.Context.EmailID;

                var deliveryType = orderBL.GetDeliveryType(headID);
                var status = orderBL.GetActualOrderStatus(headID);
                if (deliveryType == DeliveryTypes.Email)
                {
                    notificationDTO.EmailNotificationType = Eduegate.Services.Contracts.Enums.EmailNotificationTypes.OrderDelivery;
                    if (status != (int)ActualOrderStatus.Delivered)
                    {
                        operationResultDTO.Message = ResourceHelper.GetValue("Pleasetrylater", this.Context.LanguageCode);
                        operationResultDTO.operationResult = OperationResult.Error;
                        return operationResultDTO;
                    }
                }
                else
                {
                    notificationDTO.EmailNotificationType = Eduegate.Services.Contracts.Enums.EmailNotificationTypes.OrderConfirmation;
                    if (status == (int)ActualOrderStatus.Failed)
                    {
                        operationResultDTO.Message = ResourceHelper.GetValue("Pleasetrylater", this.Context.LanguageCode);
                        operationResultDTO.operationResult = OperationResult.Error;
                        return operationResultDTO;
                    }
                }

                notificationDTO.AdditionalParameters = new List<Eduegate.Services.Contracts.Commons.KeyValueParameterDTO>();
                notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.OrderConfirmation.Keys.OrderID, ParameterValue = headID.ToString() });
                notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.OrderConfirmation.Keys.OrderHistoryURL, ParameterValue = "" });
                notificationDTO = GetAdditionalNotificationParameters(notificationDTO, this.Context, RootUrl);
                notificationDTO.FromEmailID = FromAddress;
                var notificationresult = new NotificationBL(this.Context).SaveEmailData(notificationDTO);
                if (notificationresult.IsNotNull())
                {
                    operationResultDTO.Message = ResourceHelper.GetValue("Mailsentsuccessfully", this.Context.LanguageCode);
                    operationResultDTO.operationResult = OperationResult.Success;
                }
                else
                {
                    operationResultDTO.Message = ResourceHelper.GetValue("Pleasetrylater", this.Context.LanguageCode);
                    operationResultDTO.operationResult = OperationResult.Error;
                }
            }
            return operationResultDTO;
        }

        public List<SearchKeywordsDictionaryDTO> GetBlinkKeywordsDictionary(string searchtext, string lng)
        {
            //var isFallBack = Convert.ToBoolean(ConfigurationExtensions.GetAppConfigValue("UseFallBackSearchKeywords"));
            //return new SearchDataBL(this.Context).GetBlinkKeywordsDictionary(searchtext, lng, isFallBack);
            return null;
        }

        public List<ProductSKUDetailDTO> GetActiveDealProducts(BoilerPlateDTO boilerPlateDTO)
        {
            var settingDetail = new SettingBL().GetSettingDetail(Constants.TransactionSettings.ONLINEBRANCHID, (long)this.Context.CompanyID);
            var noOfProducts = boilerPlateDTO.RuntimeParameters.Find(a => a.Key == "NoOfProducts").Value;
            var productlist = new ProductBL(this.Context).GetActiveDealProducts(long.Parse(settingDetail.SettingValue), long.Parse(noOfProducts));
            foreach (var product in productlist)
            {
                //product.EndDate = Convert.ToDateTime(product.EndDate).ToString("G", new System.Globalization.CultureInfo("en-GB"));
                product.EndDate = Convert.ToDateTime(product.EndDate).ToString("MMM dd yyyy hh:mm tt");
                product.ServerCurrentTime = DateTime.Now.ToString("MMM dd yyyy hh:mm tt");
                product.ProductListingImage = string.Format("{0}/{1}", ProductImageURL, product.ProductListingImage);
            }
            return productlist;
        }



        public List<PaymentMethodDTO> GetPaymentMethods()
        {
            //siteID change
            var paymentMethodList = new ReferenceDataBL(this.Context).GetPaymentMethods(siteID);
            foreach (var payment in paymentMethodList)
            {
                payment.ImageName = string.Format("{0}/{1}", ConfigurationExtensions.GetAppConfigValue("ImageHostUrl"), payment.ImageName);
            }
            return paymentMethodList;
        }

        #region Private Methods
        private string GetRequestSignatureText(Eduegate.Services.Contracts.Payments.PaymentDTO payment)
        {
            int paymentAmount = Convert.ToInt32(Convert.ToDecimal(payment.Amount) * 100); // Why to multiply this with 100?
            string fortLanguage = this.Context.LanguageCode;  // How to get language

            return ConfigurationExtensions.GetAppConfigValue("TheFortShaRequestPhrase") + "access_code=" + ConfigurationExtensions.GetAppConfigValue("TheFortAccessCode") + "amount=" + paymentAmount + "command=" + ConfigurationExtensions.GetAppConfigValue("TheFortPCommand") + "currency=" + ConfigurationExtensions.GetAppConfigValue("TheFortCurrency") + "customer_email=" + payment.EmailId + "customer_ip=" + payment.InitiatedFromIP + "language=" + fortLanguage + "merchant_identifier=" + ConfigurationExtensions.GetAppConfigValue("TheFortMerchantIdentifier") + "merchant_reference=" + payment.TrackKey + "return_url=" + ConfigurationExtensions.GetAppConfigValue("TheFortRedirectionUrl") + ConfigurationExtensions.GetAppConfigValue("TheFortShaRequestPhrase");
        }

        private Dictionary<string, string> GetTheFortParameters(TheFortPaymentDTO fort)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("signature", fort.PSignature);
            parameters.Add("access_code", fort.PAccessCode);
            parameters.Add("amount", Convert.ToString(fort.PAmount));
            parameters.Add("command", fort.PCommand);
            parameters.Add("currency", fort.PCurrency);
            parameters.Add("customer_email", fort.PCustomerEmail);
            parameters.Add("customer_ip", fort.InitIP);
            //parameters.Add("eci", "ECOMMERCE");
            parameters.Add("language", fort.PLang);
            parameters.Add("merchant_identifier", fort.PMerchantIdentifier);
            parameters.Add("merchant_reference", fort.PMerchantReference);
            parameters.Add("return_url", ConfigurationExtensions.GetAppConfigValue("TheFortRedirectionUrl"));
            //parameters.Add("payment_option", "MASTERCARD");
            return parameters;
        }
        private Dictionary<string, string> SetKnetParameters(string trackKey)
        {
            var parameters = new Dictionary<string, string>();


            parameters.Add("merchant_reference", trackKey);

            //parameters.Add("payment_option", "MASTERCARD");
            return parameters;
        }
        private Dictionary<string, string> SetMIGSParameters(string trackKey)
        {
            var parameters = new Dictionary<string, string>();


            parameters.Add("merchant_reference", trackKey);

            //parameters.Add("payment_option", "MASTERCARD");
            return parameters;
        }
        private ContactDTO TranslateCustomertoContactDTO(UserDTO userDTO, bool isBillingAddressRequired)
        {
            return new ContactDTO()
            {
                FirstName = userDTO.Customer.FirstName,
                MiddleName = userDTO.Customer.MiddleName,
                LastName = userDTO.Customer.LastName,
                IsBillingAddress = isBillingAddressRequired == true ? true : false,
                IsShippingAddress = isBillingAddressRequired == true ? false : true,
                CountryID = userDTO.Customer.CountryID,
                MobileNo1 = userDTO.Customer.TelephoneNumber
            };
        }
        private void ResetCookies(UserDTO user)
        {
            this.Context.EmailID = user.LoginEmailID;
            this.Context.LoginID = user.UserID;
            this.Context.UserRole = string.Join(",", user.Roles);

            if (user.UserClaims.IsNull())
            {
                this.Context.UserClaims = "";
            }
            else
            {
                this.Context.UserClaims = string.Join(",", user.UserClaims);
            }
            this.Context.LoginID = user.UserID;
            this.Context.UserId = user.Customer.IsNotNull() ? Convert.ToString(user.Customer.CustomerIID) : null;
            //ResetCallContext(this.Context);
        }

        private void SendNotification(long headID, EmailNotificationTypeDTO emailNotificationTypeDetail, string transactionNumber, string RootUrl)
        {
            var notificationDTO = new EmailNotificationDTO()
            {
                ToEmailID = this.Context.EmailID,
                EmailNotificationType = Eduegate.Services.Contracts.Enums.EmailNotificationTypes.OrderConfirmation,
                AdditionalParameters = new List<Eduegate.Services.Contracts.Commons.KeyValueParameterDTO>(),
                Subject = emailNotificationTypeDetail.IsNotNull() ? string.Concat(emailNotificationTypeDetail.EmailSubject, " ", transactionNumber) : string.Empty,
            };
            notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO()
            {
                ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.OrderConfirmation.Keys.OrderID,
                ParameterValue = headID.ToString()
            });

            //notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO()
            //{
            //    ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.OrderConfirmation.Keys.OrderHistoryURL,
            //    ParameterValue = ConfigurationExtensions.GetAppConfigValue("RootUrl") + Convert.ToString(ConfigurationExtensions.GetAppConfigValue("OrderHistoryURL"))
            //});
            notificationDTO = GetAdditionalNotificationParameters(notificationDTO, this.Context, RootUrl);
            notificationDTO.FromEmailID = ConfigurationExtensions.GetAppConfigValue("FromAddress");
            var isSent = new NotificationBL(this.Context).SaveEmailData(notificationDTO);
        }
        private EmailNotificationDTO GetAdditionalNotificationParameters(EmailNotificationDTO notificationDTO, CallContext context, string RootUrl)
        {
            notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO()
            {
                //siteID Change
                ParameterName = "SiteID",
                ParameterValue = siteID.ToString()
            });
            notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO()
            {
                //companyId Change
                ParameterName = "CompanyID",
                ParameterValue = context.CompanyID.ToString()
            });

            notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO()
            {
                ParameterName = "Logo",
                ParameterValue = RootUrl + "/Images/" + ConfigurationExtensions.GetAppConfigValue<string>("Logo")
            });

            notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO()
            {
                ParameterName = "LanguageCode",
                ParameterValue = context.LanguageCode
            });

            return notificationDTO;
        }

        #endregion

        public List<FacetsDetail> GetAllCategories(string searchText = "")
        {
            var facets = new List<FacetsDetail>();
            var categories = GetCachedCategories();
            var categoryFacet = new FacetsDetail() { FaceItems = new List<FacetItem>(), Name = "Category" };

            string websiteCategory = new MutualRepository().GetSettingData("DEFAULT_WEBSITE_CATEGORY")?.SettingValue;

            var allCategories = new List<CategoryDetailDTO>();

            if (websiteCategory != null)
            {
                long websiteCategoryID = long.Parse(websiteCategory);

                allCategories = string.IsNullOrEmpty(searchText) ? categories.Where(x => x.CategoryIID == websiteCategoryID).ToList()
                    : categories.Where(x => !x.ParentCategoryID.HasValue && x.CategoryName.Contains(searchText)).ToList();
            }
            else
            {
                allCategories = string.IsNullOrEmpty(searchText) ? categories.Where(x => !x.ParentCategoryID.HasValue).ToList()
                     : categories.Where(x => !x.ParentCategoryID.HasValue && x.CategoryName.Contains(searchText)).ToList();
            }

            foreach (var category in allCategories)
            {
                var categoryImage = category.CategoryImageMapList.Where(x => x.ImageTypeID == 13).FirstOrDefault();
                var categoryCultureData = Context == null || string.IsNullOrEmpty(Context.LanguageCode)
                    || category.CategoryCultureDatas == null ? null : category.CategoryCultureDatas
                    .Where(x => x.CultureCode == Context.LanguageCode)
                    .FirstOrDefault();
                var categoryName = categoryCultureData == null || string.IsNullOrEmpty(categoryCultureData.CategoryName)
                        ? category.CategoryName : categoryCultureData.CategoryName;
                var facet = new FacetItem()
                {
                    code = category.CategoryIID.ToString(),
                    key = categoryName,
                    value = int.Parse(category.CategoryIID.ToString()),
                    ItemImage = categoryImage != null ? string.Format("{0}/Category/Category_sliding/{1}",
                         categoryImage.ImageFile) : null,
                    FaceItems = new List<FacetItem>()
                };

                BuildCategoryTree(facet, string.IsNullOrEmpty(searchText) ?
                    categories.Where(x => x.ParentCategoryID == category.CategoryIID).ToList()
                    : categories.Where(x => x.ParentCategoryID == category.CategoryIID && x.CategoryName.Contains(searchText)).ToList());
                categoryFacet.FaceItems.Add(facet);
            }

            facets.Add(categoryFacet);
            return facets;
        }

        private void BuildCategoryTree(FacetItem facet, List<CategoryDetailDTO> categories)
        {
            foreach (var category in categories)
            {
                var categoryCultureData = Context == null || string.IsNullOrEmpty(Context.LanguageCode)
                   || category.CategoryCultureDatas == null ? null : category.CategoryCultureDatas
                   .Where(x => x.CultureCode == Context.LanguageCode)
                   .FirstOrDefault();
                var categoryName = categoryCultureData == null || string.IsNullOrEmpty(categoryCultureData.CategoryName)
                    ? category.CategoryName : categoryCultureData.CategoryName;

                var facetItem = new FacetItem()
                {
                    code = category.CategoryCode,
                    key = categoryName,
                    value = int.Parse(category.CategoryIID.ToString())
                };

                facet.FaceItems.Add(facetItem);
            }
        }

        private List<CategoryDetailDTO> GetCachedCategories()
        {
            var categories = Framework.CacheManager.MemCacheManager<List<CategoryDetailDTO>>.Get("CATEGORIES");

            if (categories == null)
            {
                var allCat = new ProductDetailRepository().GetCategories();
                categories = allCat.Select(x => new CategoryDetailDTO()
                {
                    CategoryCode = x.CategoryCode,
                    CategoryIID = x.CategoryIID,
                    CategoryName = x.CategoryName,
                    IsActive = x.IsActive,
                    ParentCategoryID = x.ParentCategoryID,
                    SortOrder = x.SortOrder,
                    CategoryImageMapList = x.CategoryImageMaps.Select(y => new CategoryImageMapDTO()
                    {
                        CategoryID = y.CategoryID,
                        CategoryImageMapIID = y.CategoryImageMapIID,
                        ActionLinkTypeID = y.ActionLinkTypeID,
                        ImageFile = y.ImageFile,
                        ImageTitle = y.ImageTitle,
                    }).ToList(),
                    //CategoryCultureDatas = x.CategoryCultureDatas == null ? null :
                    //    x.CategoryCultureDatas.Select(y => new CategoryCultureDataDTO()
                    //    {
                    //        CategoryID = y.CultureID,
                    //        CategoryName = y.CategoryName,
                    //        CultureID = y.CultureID,
                    //        CultureCode = y.Culture.CultureCode,
                    //    }).ToList()
                }).ToList();

                Framework.CacheManager.MemCacheManager<List<CategoryDetailDTO>>.Add(categories, "CATEGORIES");
            }

            return categories;
        }


        public List<CartItemQuantityDTO> GetCartQuantity()
        {
            var loginID = Context.LoginID.HasValue ? Context.LoginID.Value.ToString() : null;


            var cartItems = new ShoppingCartRepository().GetCartDetailWithItems(loginID, (int)ShoppingCartStatus.InProcess, null);
            var cartQuantity = new List<CartItemQuantityDTO>();
            if (cartItems != null)
            {
                cartItems.ForEach(x => cartQuantity
                    .Add(new CartItemQuantityDTO()
                    {
                        SKUID = x.ProductSKUMapID.ToString(),
                        Quantity = x.Quantity.Value,
                        Amount = x.Quantity.Value * (x.ProductDiscountPrice.HasValue ? x.ProductDiscountPrice.Value : x.ProductPrice.Value)
                    }));
            }

            return cartQuantity;
        }

        public List<CategoryDetailDTO> GetCategoriesByBoilerplate(BoilerPlateDTO boilerPlateDTO)
        {
            long? parentCategoryID = null;

            var parentCategoryFilter = boilerPlateDTO.RuntimeParameters.Find(x => x.Key == "ParentCategoryCode");
            var searchFilter = boilerPlateDTO.RuntimeParameters.Find(x => x.Key == "SearchText");
            var searchText = searchFilter == null ? string.Empty : searchFilter.Value;

            if (parentCategoryFilter != null && !string.IsNullOrEmpty(parentCategoryFilter.Key))
            {
                var category = new CategoryBL(this.Context).GetCategoryByCode(parentCategoryFilter.Value);
                if (category != null)
                {
                    parentCategoryID = category.CategoryIID;
                }
            }

            var fullCategoryList = new CategoryBL(this.Context)
                .GetCategoryDetailByParentIDWithImage(parentCategoryID);
            var categories = new List<CategoryDetailDTO>();

            foreach (var category in
                (string.IsNullOrEmpty(searchText) ? fullCategoryList : fullCategoryList
                .Where(x => x.CategoryName.ToLower().Contains(searchText.ToLower()))))
            {
                var dto = category;

                var thumbnail = category.CategoryImageMapList
                    .Where(a => a.ImageTypeID == (byte)ImageTypes.Thumbnail && a.ImageFile != null).FirstOrDefault();

                if (thumbnail == null && category.CategoryImageMapList != null)
                {
                    thumbnail = category.CategoryImageMapList.Where(x => x.ImageFile != null).FirstOrDefault();

                    if (thumbnail != null)
                    {
                        dto.ThumbnailImageName = string.Format("{0}/{1}/{2}/{3}", CategoryURL, category.CategoryIID, "ThumbnailImage", thumbnail.ImageFile);
                    }
                }

                if (string.IsNullOrEmpty(dto.ThumbnailImageName))
                {
                    if (thumbnail.IsNotNull())
                    {
                        dto.ThumbnailImageName = string.Format("{0}/{1}/{2}", CategoryURL, "ThumbnailImage", thumbnail.ImageFile);
                    }
                    else
                    {
                        dto.ThumbnailImageName = "/images/noimage.jpg";
                    }
                }

                var slidingImage = category.CategoryImageMapList
                    .Where(a => a.ImageTypeID == (int)ImageTypes.Category_Sliding).FirstOrDefault();

                if (slidingImage == null && category.CategoryImageMapList != null)
                {
                    slidingImage = category.CategoryImageMapList.Where(x => x.ImageFile != null).FirstOrDefault();

                    if (slidingImage != null)
                    {
                        dto.SlidingImageName = string.Format("{0}/{1}/{2}/{3}", CategoryURL, category.CategoryIID, "ThumbnailImage", slidingImage.ImageFile);
                    }
                }

                if (string.IsNullOrEmpty(dto.SlidingImageName))
                {
                    if (slidingImage.IsNotNull())
                    {
                        dto.SlidingImageName = string.Format("{0}/{1}/{2}", CategoryURL, "Category_sliding", slidingImage.ImageFile);
                    }
                    else
                    {
                        dto.SlidingImageName = "/images/noimage.png";
                    }
                }

                var listingImage = category.CategoryImageMapList.Where(a => a.ImageTypeID == (byte)ImageTypes.Listing).FirstOrDefault();

                if (listingImage == null && category.CategoryImageMapList != null)
                {
                    listingImage = category.CategoryImageMapList.FirstOrDefault();

                    if (listingImage != null)
                    {
                        dto.ImageName = string.Format("{0}/{1}/{2}/{3}", CategoryURL, category.CategoryIID, "ListingImage", listingImage.ImageFile);
                    }
                }

                if (string.IsNullOrEmpty(dto.ImageName))
                {
                    if (listingImage.IsNotNull())
                    {
                        dto.ImageName = string.Format("{0}/{1}/{2}", CategoryURL, "ListingImage", listingImage.ImageFile);
                    }
                    else
                    {
                        dto.ImageName = "/images/noimage.jpg";
                    }
                }

                categories.Add(dto);

            }

            return categories;
        }

        public OperationResultDTO ValidateContinueCheckOut()
        {
            var result = new OperationResultDTO()
            {
                Message = "Success",
                operationResult = OperationResult.Success
            };

            var cartDTO = new ShoppingCartBL(this.Context)
              .GetCart(this.Context, 0, isDeliveryCharge: false,
                cartStatus: ShoppingCartStatus.InProcess);

            ValidateMinimumCap(cartDTO, result);

            //if(result.operationResult == OperationResult.Success)
            //{
            //    ValidateMinimumCartAmount(cartDTO, ref result);
            //}

            return result;
        }

        private void ValidateMinimumCap(CartDTO cartDTO, OperationResultDTO result)
        {
            var minimumValue = new SettingBL(Context)
                .GetSettingDetail("MINIMUMCARTVALUE");

            if (minimumValue != null && !string.IsNullOrEmpty(minimumValue.SettingValue))
            {
                var minimumCap = Convert.ToDecimal(minimumValue.SettingValue);
                var enableAreaCap = new SettingBL(Context)
                       .GetSettingValue<bool>("ENABLEAREAMINIMUMCAP", Context.CompanyID.Value, false);

                if (enableAreaCap && cartDTO.ShippingAddressID.HasValue)
                {
                    long addressId;

                    if (cartDTO.ShippingAddressID.Value == 0)
                    {
                        var address = GetLastShippingAddress(0);
                        addressId = address == null ? 0 : address.ContactID;
                    }
                    else
                    {
                        addressId = cartDTO.ShippingAddressID.Value;
                    }

                    minimumCap = new Eduegate.Domain.Repository.ShoppingCartRepository()
                        .GetMinimumCapForAnArea(addressId);
                }

                decimal? subtotal = cartDTO.SubTotal != null ? decimal.Parse(cartDTO.SubTotal) : 0;

                if (minimumCap > 0 && subtotal < minimumCap)
                {
                    var message = Eduegate.Globalization
                        .ResourceHelper
                        .GetValue("MinimumCartRemaining", this.Context.LanguageCode);
                    var balanceval = string.Format(message, minimumCap - subtotal);
                    result.Message = balanceval;
                    result.operationResult = OperationResult.Error;
                }
                else
                {
                    result.operationResult = OperationResult.Success;
                }
            }
        }

        public long OnlineStoreAddContact(ContactDTO contactDTO)
        {
            return new Eduegate.Domain.AccountBL(this.Context).OnlineStoreAddContactContactID(contactDTO);
        }

        public List<DeliverySettingDTO> GetAllDeliveryTypes(CartDTO cartDTO = null)
        {
            try
            {
                if (cartDTO == null)
                {
                    cartDTO = new ShoppingCartBL(this.Context)
                         .GetCart(this.Context, 0, isDeliveryCharge: false,
                           cartStatus: ShoppingCartStatus.InProcess);
                }

                return DeliveryManager.Manager(Context).GetAllDeliveryTypes();
            }
            catch (Exception ex)
            {
                LogHelper<AppDataBL>.Fatal(ex.Message, ex);
                return null;
            }
        }

        public DeliveryBlockingPeriodDTO GetDeliveryBlockingPeriods()
        {
            var cutOffSlots = new Eduegate.Domain.Repository.ReferenceDataRepository().GetCutOffDeliveryTimeSlots();

            var blockedDates = cutOffSlots
                .Where(x => x.OccurrenceTypeID == 2)
                .Select(x => x.OccuranceDate.Value.ToLongDateString());

            return new DeliveryBlockingPeriodDTO()
            {
                BlockedDates = new List<string>(blockedDates),
                Times = new List<TimeInterval>(),
                MaxLimitDate = DateTime.Now.AddMonths(1).ToLongDateString()
            };
        }

        public List<DeliveryTimeSlotDTO> GetDeliveryTimeSlots(long deliveryTypeId, DateTime dateTime)
        {
            try
            {
                var cartDTO = new ShoppingCartBL(this.Context)
                     .GetCart(this.Context, 0, isDeliveryCharge: false,
                       cartStatus: ShoppingCartStatus.InProcess);
                var branchID = cartDTO.BranchID.HasValue ? cartDTO.BranchID : Context.SchoolID;
                var deliveryTypeManager = DeliveryManager.Manager(Context);
                var slotEntityMap = new List<DeliveryTimeSlotDTO>();
                var defaultSlots = deliveryTypeManager.GetDefaultDeliverySlots();
                var cutOffSlots = deliveryTypeManager.GetCutOffDeliveryTimeSlots();

                var slotCapacityEnabled = new SettingBL(Context)
                             .GetSettingValue<bool>("ENABLESLOTCAPACITY", (long)this.Context.CompanyID, true);
                var slotCapacityByBranchEnabled = new SettingBL(Context).GetSettingValue<bool>("ENABLESLOTCAPACITYBYBRANCH", (long)this.Context.CompanyID, false);

                var occupliedSlots = slotCapacityEnabled ? new Eduegate.Domain.Repository.ReferenceDataRepository()
                               .GetOccupiedTimeSlots(deliveryTypeId == 10 ? DateTime.Now // 10 - same day, 3 - next day
                               : deliveryTypeId == 3 ? DateTime.Now.AddDays(1) : dateTime)
                                : new List<Eduegate.Domain.Entity.Models.DeliveryTypeTimeSlotMap>();

                var cutOffSlotsByDelivery = cutOffSlots.Where(x => !x.DeliveryTypeID.HasValue ||
                    x.DeliveryTypeID == deliveryTypeId);

                foreach (var slot in defaultSlots)
                {
                    if (deliveryTypeId == 10 || dateTime.Date == DateTime.Now.Date)
                    {
                        if (slot.TimeFromSpan.HasValue && slot.TimeFromSpan < DateTime.Now.TimeOfDay)
                        {
                            continue;
                        }
                    }

                    var occupiedSlot = slotCapacityByBranchEnabled && branchID.HasValue ?
                              occupliedSlots
                              .Where(x => x.DeliveryTypeID == deliveryTypeId &&
                               x.DeliveryTypeTimeSlotMapIID == slot.DeliveryTypeTimeSlotMapIID &&
                               x.BranchID == branchID.Value)
                                      .Sum(x => x.NoOfCutOffOrder) :
                              occupliedSlots
                              .Where(x => x.DeliveryTypeID == deliveryTypeId &&
                               x.DeliveryTypeTimeSlotMapIID == slot.DeliveryTypeTimeSlotMapIID)
                                      .Sum(x => x.NoOfCutOffOrder);

                    if (occupiedSlot.HasValue)
                    {
                        if (slot.NoOfCutOffOrder.HasValue
                            && slot.NoOfCutOffOrder.Value <= occupiedSlot.Value)
                        {
                            continue;
                        }
                    }

                    if (slotCapacityByBranchEnabled && slot.BranchID.HasValue &&
                         branchID.HasValue && slot.BranchID != branchID.Value)
                    {
                        continue;
                    }

                    if (cutOffSlotsByDelivery != null && cutOffSlotsByDelivery.Count() > 0)
                    {
                        if (cutOffSlotsByDelivery.Any(x =>
                            x.OccurrenceTypeID == 1 &&
                            x.TimeSlotID == slot.DeliveryTypeTimeSlotMapIID
                             && x.OccuranceDayID == (int)dateTime.DayOfWeek &&
                             (x.DeliveryTypeID == slot.DeliveryTypeID
                             || !slot.DeliveryTypeID.HasValue)))
                        {
                            continue;
                        }

                        if (cutOffSlotsByDelivery.Any(x =>
                            x.OccurrenceTypeID == 2 &&
                            x.TimeSlotID == slot.DeliveryTypeTimeSlotMapIID
                             && x.OccuranceDate.Value.Date == dateTime.Date
                             && (x.DeliveryTypeID == slot.DeliveryTypeID
                             || !slot.DeliveryTypeID.HasValue)))
                        {
                            continue;
                        }
                    }

                    slotEntityMap.Add(slot);
                }

                return slotEntityMap;
            }
            catch (Exception ex)
            {
                LogHelper<AppDataBL>.Fatal(ex.Message, ex);
                return null;
            }
        }

        public List<PaymentMethodDTO> GetPaymentMethodsforOnlineStore()
        {
            var paymentMethodList = new ShoppingCartBL(this.Context).GetPaymentMethods(siteID);

            if (paymentMethodList != null)
            {
                foreach (var payment in paymentMethodList)
                {
                    if (!payment.ImageName.Contains("img"))
                    {
                        payment.ImageName = string.Format("{0}/{1}", "img", payment.ImageName);
                    }
                }

                //TODO include COD if it's a test user
                var testUsers = new SettingBL(Context).GetSettingValue<string>("TestUsers");

                if (Context != null && testUsers != null && testUsers.Split(',').Contains(Context.MobileNumber))
                {
                    if (!paymentMethodList.Any(x => x.PaymentMethodID == 5))
                    {
                        paymentMethodList.Add(new PaymentMethodDTO()
                        {
                            PaymentMethodID = 5,
                            PaymentMethodName = "COD",
                            ImageName = "img/cod.png",
                            IsVirtual = false
                        });
                    }
                }
            }

            return paymentMethodList;
        }

        private void UpdateAddress(CheckoutPaymentDTO checkoutPaymentDTO)
        {
            new Eduegate.Domain.Repository.ShoppingCartRepository().UpdateCartAddress(long.Parse(checkoutPaymentDTO.ShoppingCartID),
                string.IsNullOrEmpty(checkoutPaymentDTO.SelectedShippingAddress) ? (long?)null : long.Parse(checkoutPaymentDTO.SelectedShippingAddress),
                string.IsNullOrEmpty(checkoutPaymentDTO.SelectedBillingAddress) ? (long?)null : long.Parse(checkoutPaymentDTO.SelectedBillingAddress));
        }

        private void UpdateStudentDetails(CheckoutPaymentDTO checkoutPaymentDTO)
        {
            new Eduegate.Domain.Repository.ShoppingCartRepository().UpdateStudentDetails(long.Parse(checkoutPaymentDTO.ShoppingCartID),
                checkoutPaymentDTO.SelectedStudentID, checkoutPaymentDTO.SelectedStudentSchoolID, checkoutPaymentDTO.SelectedStudentAcademicYearID);
        }

        public CheckoutPaymentMobileDTO OnlineStoreGenerateOrder(CheckoutPaymentDTO checkoutPaymentDTO)
        {
            var checkpaymentMobileDTO = new CheckoutPaymentMobileDTO();
            checkpaymentMobileDTO.operationResult = new OperationResultDTO();
            checkpaymentMobileDTO.DeviceVersion = checkoutPaymentDTO.DeviceVersion;
            checkpaymentMobileDTO.DevicePlatorm = checkoutPaymentDTO.DevicePlatorm;
            var shoppingCartBL = new ShoppingCartBL(this.Context);
            var trackKey = checkoutPaymentDTO.PostObject;

            try
            {
                var cartInPrcessTotal = Convert.ToDecimal(shoppingCartBL
                        .GetCartTotal(this.Context, false, true,
                        cartStatus: ShoppingCartStatus.InProcess));

                if (!shoppingCartBL.UpdateCartStatus(long.Parse(checkoutPaymentDTO.ShoppingCartID),
                   ShoppingCartStatus.PaymentInitiated, null,
                    ShoppingCartStatus.InProcess))
                {
                    checkpaymentMobileDTO.operationResult.operationResult = OperationResult.Error;
                    checkpaymentMobileDTO.operationResult.Message =
                        ResourceHelper.GetValue("Pleasetrylater",
                            this.Context.LanguageCode);
                    return checkpaymentMobileDTO;
                }

                var payment = new Eduegate.Services.Contracts.Payments.PaymentDTO();
                var paymentGateWay = shoppingCartBL.GetPaymentGatewayType(checkoutPaymentDTO.SelectedPaymentOption,
                    cartStatus: ShoppingCartStatus.PaymentInitiated);

                decimal? cartTotal = Convert.ToDecimal(shoppingCartBL.GetCartTotal(this.Context, false, true,
                    cartStatus: ShoppingCartStatus.PaymentInitiated));

                if (cartTotal <= 0 && paymentGateWay != Framework.Payment.PaymentGatewayType.VOUCHER)
                {
                    checkpaymentMobileDTO.operationResult.operationResult = OperationResult.Error;
                    checkpaymentMobileDTO.operationResult.Message = ResourceHelper.GetValue("CartEmpty", this.Context.LanguageCode);
                }
                else
                {
                    var cartDetails = GetCartDetails(ShoppingCartStatus.PaymentInitiated);

                    if (cartDetails.ShippingAddressID.HasValue &&
                        cartDetails.ShippingAddressID != checkoutPaymentDTO.SelectedStudentID)
                    {
                        UpdateStudentDetails(checkoutPaymentDTO);
                    }

                    var cartID = cartDetails.ShoppingCartID;
                    //var branchTransfer = new OrderBL(this.Context).SetTemporaryBranchTransfer(cartID);


                    var listOfTransID = new List<string>();
                    string[] transactionIDSplit = listOfTransID.ToArray();
                    //var transactionHead = new TransactionHeadDTO();
                    var DTO = new Services.Contracts.OrderHistory.OrderHistoryDTO();
                    long? customerIDString = new CustomerBL(Context).GetCustomerIDByContext();
                    payment.CustomerID = customerIDString.HasValue ? customerIDString.ToString() : null;
                    payment.Amount = cartTotal.HasValue ? cartTotal.ToString() : null;
                    payment.InitiatedFromIP = this.Context.IPAddress;
                    payment.EmailId = this.Context.EmailID;
                    checkpaymentMobileDTO.PaymentMethodName = paymentGateWay.ToString();

                    switch (paymentGateWay)
                    {
                        case Framework.Payment.PaymentGatewayType.COD:
                            payment.PaymentGateway =
                                    Eduegate.Services.Contracts.Enums.PaymentGatewayType.COD;
                            checkpaymentMobileDTO = OnlineStoreGenerateOrders(payment.PaymentGateway.ToString(), 0, checkpaymentMobileDTO, checkoutPaymentDTO);
                            checkpaymentMobileDTO.operationResult.operationResult = OperationResult.Success;
                            checkpaymentMobileDTO.operationResult.Message = "Order generated successfully";
                            break;

                        case Framework.Payment.PaymentGatewayType.MIGS:
                            payment.PaymentGateway =
                                    Eduegate.Services.Contracts.Enums.PaymentGatewayType.MIGS;
                            checkpaymentMobileDTO = OnlineStoreGenerateOrders(payment.PaymentGateway.ToString(), 0, checkpaymentMobileDTO, checkoutPaymentDTO);
                            checkpaymentMobileDTO.operationResult.operationResult = OperationResult.Success;
                            checkpaymentMobileDTO.operationResult.Message = "Order generated successfully";
                            break;

                        default:
                            break;
                    }
                }

                //update ordernotes and changes
                if (!string.IsNullOrEmpty(checkoutPaymentDTO.OrderNote))
                {
                    foreach (var order in checkpaymentMobileDTO.orderHistory)
                    {
                        var comment = new CommentDTO()
                        {
                            EntityType = Framework.Contracts.Common.Enums.EntityTypes.Transaction,
                            ReferenceID = order.TransactionOrderIID,
                            CommentText = checkoutPaymentDTO.OrderNote,
                            Username = Context.EmailID
                        };

                        new MutualBL(Context).SaveComment(comment);
                        //SendPushNotificationForComments(comment);
                    }
                }

            }
            catch (Exception exception)
            {
                LogHelper<ProductFeatureDTO>.Fatal(exception.Message, exception);
                checkpaymentMobileDTO.operationResult.operationResult = OperationResult.Error;
                checkpaymentMobileDTO.operationResult.Message = "Pleasetrylater";
                return checkpaymentMobileDTO;
            }


            return checkpaymentMobileDTO;
        }

        //public void SendPushNotificationForComments(CommentDTO comment)
        //{
        //    var transactionNumber = new Eduegate.Domain.Repository.ShoppingCartRepository().GetTransactionNumberByHeadID(comment.ReferenceID);
        //    string message = string.Format("You have a message on order#{0}#{1}", comment.ReferenceID, transactionNumber);
        //    var setting = new SettingBL(Context).GetSettingDetail("PUSHNOTIFICATIONTITLE");
        //    var messageTitle = setting == null || string.IsNullOrEmpty(setting.SettingValue) ? "Order Comments" : setting.SettingValue;

        //    var customerLoginID = new Eduegate.Domain.Repository.ShoppingCartRepository().GetOrderCustomerLoginId(comment.ReferenceID);
        //    var employeeLoginID = new Eduegate.Domain.Repository.ReferenceDataRepository().GetOrderEmployeeLoginId(comment.ReferenceID);

        //    if (employeeLoginID.HasValue)
        //    {
        //        if (employeeLoginID.Value != Context.LoginID.Value)
        //        {
        //            var parameters = NotificationSetting.GetEmployeeAppSettings();
        //            parameters.Add("command", "comments");
        //            parameters.Add("subcommand", "orderdetails");
        //            parameters.Add("referenceid", comment.CommentIID.ToString());

        //            SendPushNotification(employeeLoginID.Value, message, messageTitle
        //                         , parameters);
        //            AlertNotification(comment.ReferenceID, message, employeeLoginID.Value);
        //        }
        //    }
        //    else
        //    {
        //        SendPushNotificationToEmployees(message, messageTitle, comment.ReferenceID, "COMMENTS");
        //    }

        //    if (customerLoginID != Context.LoginID.Value)
        //    {
        //        var parameters = NotificationSetting.GetCustomerAppSettings();
        //        parameters.Add("command", "cardgeneration");
        //        parameters.Add("referenceid", comment.CommentIID.ToString());

        //        SendPushNotification(customerLoginID, message, messageTitle,
        //                parameters);
        //        AlertNotification(comment.ReferenceID, message, customerLoginID);
        //    }
        //}

        public CheckoutPaymentMobileDTO OnlineStoreGenerateOrders(string paymentGateway, long trackKey,
            CheckoutPaymentMobileDTO checkpaymentMobileDTO, CheckoutPaymentDTO paymentDTO)
        {
            var deviceInfo = string.Concat(checkpaymentMobileDTO.DevicePlatorm, "|", checkpaymentMobileDTO.DeviceVersion);
            var onlineDocumentType = new SettingBL(Context)
                .GetSettingValue<string>(Constants.TransactionSettings.ONLINESALESDOCTTYPEID.ToString(), this.Context.CompanyID.Value, null);
            UserRole appType;

            switch (this.Context.LanguageCode)
            {
                case "ar":
                    appType = UserRole.Mobile_App_Ar;
                    break;
                default:
                    appType = UserRole.Mobile_App_En;
                    break;
            }

            var setting = new SettingBL(Context)
                .GetSettingValue<string>("CLIENTINSTANCE", Context.CompanyID.Value, null);
            var headIDs = new List<long>();
            var transactionIDs = OnlineStoreConfirmOnlineOrder(paymentGateway, trackKey,
                this.Context, userRole: appType, deviceInfo: deviceInfo, headIDs: headIDs, paymentDTO: paymentDTO);

            checkpaymentMobileDTO.orderHistory = new List<Services.Contracts.OrderHistory.OrderHistoryDTO>();
            bool isFirstItem = true;
            foreach (var transactionId in transactionIDs.Split(','))
            {
                if (isFirstItem == true)
                {
                    checkpaymentMobileDTO.CartID = new ShoppingCartBL(this.Context)
                        .GetCartDetailByHeadID(Convert.ToInt64(transactionId));
                }
                isFirstItem = false;
                var orderDto = (new UserServiceBL(this.Context)
                    .GetBriefOrderHistory(onlineDocumentType, long.Parse(transactionId)))
                    .FirstOrDefault();

                //if (orderDto.PaymentMethod == "CREDIT")
                //{
                //    // share holder info
                //    var shareHolderDetails = IntegratorFactory.GetShareholderFactory(setting)
                //        .GetShareHolderDetailByMobileNumber(Context.MobileNumber);
                //    orderDto.RemainingBalance = shareHolderDetails == null ? null : (shareHolderDetails.Balance_Credit_Limit_Available - orderDto.Total);
                //}

                checkpaymentMobileDTO.orderHistory.Add(orderDto);

                if (string.IsNullOrEmpty(checkpaymentMobileDTO.TransactionNo))
                {
                    checkpaymentMobileDTO.TransactionNo = orderDto.TransactionNo;
                    checkpaymentMobileDTO.DeliveryText = orderDto.DeliveryText + " " + orderDto.TimeSlotText;
                }
                else
                {
                    checkpaymentMobileDTO.TransactionNo = checkpaymentMobileDTO.TransactionNo
                        + "," + orderDto.TransactionNo;
                    checkpaymentMobileDTO.DeliveryText = orderDto.DeliveryText +
                        "," + orderDto.DeliveryText + " " + orderDto.TimeSlotText;
                }

                var transactionNo = orderDto.TransactionNo;
                var transactionHeadID = orderDto.TransactionOrderIID;

                checkpaymentMobileDTO.TransactionHeadIds = headIDs;
                checkpaymentMobileDTO.EntitlementMaps = orderDto.EntitlementMaps;
                checkpaymentMobileDTO.DeliveryTypeID = orderDto.DeliveryTypeID;


                //apply sales promotion
                //try
                //{
                //     new SalesPromotionBL(this.Context).ApplyCartLevelPromotion(paymentDTO.ShoppingCartID, paymentDTO.CustomerID);
                //}
                //catch (Exception ex)
                //{
                //    LogHelper<AppDataBL>.Fatal(ex.Message, ex);
                //}

                //var enableHub = new SettingBL(Context).GetSettingValue<bool>("EnableNotificationHub", false);

                //if (enableHub)
                //{
                //    //Order live signalR conmmunication to the notification hub
                //    try
                //    {
                //        new NotificationBL(this.Context).SendSignalRMessage(JsonConvert.SerializeObject(new SubscriptionDetail()
                //        {
                //            SubScriptionType = SubscriptionTypes.NewOrder,
                //            Data = checkpaymentMobileDTO,
                //            InitiaterLoginID = Context.LoginID.Value,
                //        }));
                //    }
                //    catch (Exception ex)
                //    {
                //        LogHelper<AppDataBL>.Fatal(ex.Message, ex);
                //    }
                //}

                //var enableOrderConfirmation = new SettingBL(Context).GetSettingValue<bool>("EnableOrderConfirmationEmail", false);

                //if (enableOrderConfirmation)
                //{
                //    //Order confirmation email.
                //    try
                //    {
                //        var notificationHandler = NotificationHandler.Handler(Context, _dbContext);
                //        var emailNotificationTypeDetail = new NotificationBL(this.Context)
                //                .GetEmailNotificationType(Services.Contracts.Enums.EmailNotificationTypes.OrderConfirmation);
                //        notificationHandler.SendNotification(transactionHeadID, emailNotificationTypeDetail,
                //            transactionNo, _rootUrl, orderDto, "OrderDelivery.cshtml");
                //    }
                //    catch (Exception ex)
                //    {
                //        LogHelper<AppDataBL>.Fatal(ex.Message, ex);
                //    }
                //}

                //var enableOrderNotification = new SettingBL(Context).GetSettingValue<bool>("EnableOrderNotification", false);

                //if (enableOrderNotification)
                //{
                //    //Order alert for the customer
                //    try
                //    {
                //        var notificationHandler = NotificationHandler.Handler(Context, _dbContext);
                //        var emailNotificationTypeDetail = new NotificationBL(this.Context)
                //        .GetEmailNotificationType(Services.Contracts.Enums.EmailNotificationTypes.OrderConfirmation);
                //        var message = Eduegate.Application.Globalizations.ResourceHelper.GetValue("OrderGenerated", Context.LanguageCode);
                //         notificationHandler.AlertNotification(orderDto.TransactionOrderIID, string.Format(message, transactionHeadID, transactionNo), Context.LoginID.Value);
                //    }
                //    catch (Exception ex)
                //    {
                //        LogHelper<AppDataBL>.Fatal(ex.Message, ex);
                //    }
                //}

                //employee notification when employee generates the order
                //if (!Context.EmployeeID.HasValue)
                //{
                //    try
                //    {
                //        var message = ResourceHelper.GetValue("OrderGenerated", Context.LanguageCode);
                //        var messageTitle = ResourceHelper.GetValue("OrderGeneratedTitle", Context.LanguageCode);
                //        var parameters = NotificationSetting.GetCustomerAppSettings();
                //        parameters.Add("command", "ordergeneration");
                //        parameters.Add("subcommand", "orderdetails");
                //        parameters.Add("referenceid", transactionHeadID.ToString());

                //        var notificationHandler = NotificationHandler.Handler(Context);
                //         notificationHandler.SendPushNotification(Context.LoginID.Value,
                //            string.Format(message, transactionHeadID, transactionNo),
                //            messageTitle, parameters);

                //        // get all employee logins
                //         NotificationHandler.Handler(Context).SendPushNotificationToEmployees(string.Format(message, transactionHeadID, transactionNo),
                //            messageTitle, transactionHeadID, "ordergeneration", Context.BranchID);
                //    }
                //    catch (Exception ex)
                //    {
                //        LogHelper<AppDataBL>.Fatal(ex.Message, ex);
                //    }
                //}

                var orderHistories = checkpaymentMobileDTO.orderHistory;

                ////Invoice email
                //var invoiceEmail = new SettingBL(Context).GetSettingValue<bool>("EnableInvoiceEmailOnOrderGenerates", false);

                //if (invoiceEmail)
                //{
                //    try
                //    {
                //        var transactionNumber = transactionId;
                //        var parameters = new Dictionary<string, string>();
                //        parameters.Add("HeadID", transactionNumber);
                //        parameters.Add("TransactionNo", transactionNo);
                //        var reportFile = ReportBuilder.GenerateReportPDF("SalesOrderPreview.rdl", parameters);

                //        var attachments = new Dictionary<string, string>();
                //        attachments.Add("Invoice", reportFile);
                //        var loginDetail =  new Eduegate.Domain.Repository.AccountRepository().GetLoginDetailByLoginID(Context.LoginID.Value);
                //        SMTPHelper.SendEmail("Hi<br>Please find the invoice<br><br>Regards,<br>Customer Support",
                //            "Invoice Email", loginDetail.LoginEmailID, null, null, null, null,
                //             attachments);
                //    }
                //    catch (Exception ex)
                //    {
                //        LogHelper<AppDataBL>.Fatal(ex.Message, ex);
                //    }
                //}
            }

            return checkpaymentMobileDTO;
        }

        public string OnlineStoreConfirmOnlineOrder(string paymentMethod, long trackID, CallContext callContext,
        long shoppingCartIID = 0, UserRole? userRole = null, string deviceInfo = "",
        long? customerID = (int?)null, decimal deliveryCharge = -1, List<long> headIDs = null,
        CheckoutPaymentDTO paymentDTO = null)
        {
            if (!customerID.HasValue)
            {
                customerID = new ShoppingCartRepository()
                    .GetCustomerID(callContext.EmailID,
                    callContext.MobileNumber, "", callContext.LoginID);
            }

            var cart = new ShoppingCartBL(Context).GetCart(callContext, withCurrencyConversion: false, isDeliveryCharge: false, isValidation: false,
                cartStatus: ShoppingCartStatus.PaymentInitiated, ShoppingCartIID: shoppingCartIID, customerIID: customerID);

            return _orderManager.GenerateOrder(cart, paymentMethod, trackID,
                userRole, deviceInfo, customerID, deliveryCharge, headIDs, paymentDTO);
        }

        public List<Services.Contracts.OrderHistory.OrderHistoryDTO> OnlineStoreGetOrderHistoryDetails(int pageSize, int pageNo)
        {
            //var userDTO = GetUserDetails();
            var settingDetail = new SettingBL(Context).GetSettingValue<string>("ONLINEBRANCHID");

            var customerID = new CustomerBL(Context).GetCustomerIDByContext();
            var orderhistoryDTOList = _orderManager
                .GetOrderHistoryDetailsWithPagination(settingDetail, customerID.ToString(), pageNo, pageSize, Context);

            foreach (var order in orderhistoryDTOList)
            {
                order.Currency = ResourceHelper.GetValue(order.Currency, this.Context.LanguageCode);
            }

            // bring the pending payments always on top
            //if (pageNo == 1)
            //{
            //    var pendingOrders = _orderManager.GetPaymentPendingOrders();
            //    orderhistoryDTOList.InsertRange(0, pendingOrders);
            //}

            return orderhistoryDTOList;
        }

        public Services.Contracts.SearchData.SearchResultDTO OnlineStoreGetProductSearch
        (int pageIndex, int pageSize, string searchText, string searchVal,
        string searchBy, string sortBy, string pageType, bool isCategory, short? statusID = 2)
        {
            var customerID = searchVal != null && searchVal.Contains("FAVOURITE") && Context != null ?
            new CustomerRepository().GetCustomerIDByLoginID(Context.LoginID.Value) : (long?)null;

            return CartMapper.Mapper(Context).OnlineStoreProductSKUSearch(
                    pageIndex, pageSize, searchText, searchVal,
                    searchBy, sortBy, pageType, isCategory, customerID, statusID);
        }

        public OperationResultDTO SaveShoppingCartDetails(CheckoutPaymentDTO checkoutPaymentDTO)
        {

            var operationResultDTO = new OperationResultDTO();

            try
            {

                var data = new ShoppingCartRepository().SaveShoppingCartDetails(checkoutPaymentDTO, Context);

                if (data != null)
                {
                    operationResultDTO.Message = "Updated Successfully";
                    operationResultDTO.operationResult = OperationResult.Success;
                }
                else
                {
                    operationResultDTO.Message = "";
                    operationResultDTO.operationResult = OperationResult.Error;
                }
            }
            catch (Exception ex)
            {
                operationResultDTO.Message = ex.Message;
                operationResultDTO.operationResult = OperationResult.Error;
            }

            return operationResultDTO;
        }

        public OperationResultDTO CheckLoginforCustomer(UserDTO userDTO)
        {
            var operationResultDTO = new OperationResultDTO();

            try
            {

                var data = new ShoppingCartRepository().CheckCustomerExist(userDTO, Context);

                var dataSetting = CheckLoginforCustomerSetting(userDTO);

                if (data == "Data Added")
                {
                    operationResultDTO.Message = "Successfully Regsistered as Customer";
                    operationResultDTO.operationResult = OperationResult.Success;
                }
                else
                {
                    if (data == "Already Exist")
                    {
                        operationResultDTO.Message = "";
                        operationResultDTO.operationResult = OperationResult.Success;
                    }
                    else
                    {
                        operationResultDTO.Message = "";
                        operationResultDTO.operationResult = OperationResult.Error;
                    }
                }
            }
            catch (Exception ex)
            {
                operationResultDTO.Message = ex.Message;
                operationResultDTO.operationResult = OperationResult.Error;
            }

            return operationResultDTO;
        }

        public OperationResultDTO CheckLoginforCustomerSetting(UserDTO userDTO)
        {
            var operationResultDTO = new OperationResultDTO();

            try
            {

                var data = new ShoppingCartRepository().CheckCustomerSettingExist(userDTO, Context);

                if (data == "Data Added")
                {
                    operationResultDTO.Message = "Successfully Regsistered as Customer";
                    operationResultDTO.operationResult = OperationResult.Success;
                }
                else
                {
                    if (data == "Already Exist")
                    {
                        operationResultDTO.Message = "";
                        operationResultDTO.operationResult = OperationResult.Success;
                    }
                    else
                    {
                        operationResultDTO.Message = "";
                        operationResultDTO.operationResult = OperationResult.Error;
                    }
                }
            }
            catch (Exception ex)
            {
                operationResultDTO.Message = ex.Message;
                operationResultDTO.operationResult = OperationResult.Error;
            }

            return operationResultDTO;
        }

        public OperationResultDTO ReOrder(long headID)
        {
            var operationResultDTO = new OperationResultDTO();

            try
            {
                var shoppingCart = new ShoppingCartBL(this.Context);
                var settingDetail = new SettingBL(Context).GetSettingDetail(Constants.TransactionSettings.SYSBLOCKEDBRANCH, (long)this.Context.CompanyID);
                var customerID = new CustomerBL(Context).GetCustomerIDByContext();
                var orderhistoryDTOList = new UserServiceBL(this.Context).GetOrderHistoryDetails(settingDetail.SettingValue, 1, 10, customerID.ToString(), orderID: headID, withCurrencyConversion: false, context: this.Context);

                foreach (var order in orderhistoryDTOList)
                {
                    foreach (var orderDetail in order.OrderDetails)
                    {
                        shoppingCart.AddToCart(new CartProductDTO()
                        {
                            SKUID = orderDetail.ProductSKUMapID.Value,
                            Quantity = orderDetail.Quantity.Value
                        }, this.Context);
                    }
                }

                operationResultDTO.Message = "Product added to your Cart";
                operationResultDTO.operationResult = OperationResult.Success;
            }
            catch (Exception ex)
            {
                LogHelper<AppDataBL>.Fatal("ReOrder_" + ex.Message, ex);
                operationResultDTO.Message = "";
                operationResultDTO.operationResult = OperationResult.Error;
            }

            return operationResultDTO;
        }

        public List<KeyValueDTO> GetAllergies()
        {
            try
            {
                var categories = new List<KeyValueDTO>();

                return AllergyMapper.Mapper(this.Context).GetAllergies();

            }
            catch (Exception ex)
            {
                LogHelper<AppDataBL>.Fatal(ex.Message, ex);
                return null;
            }
        }

        public OperationResultDTO SaveAllergies(long studentID, int allergyID, byte SeverityID)
        {
            var operationResultDTO = new OperationResultDTO();

            var isAdded = AllergyMapper.Mapper(Context).SaveAllergies( studentID,  allergyID, SeverityID);

            if (isAdded)
            {
                operationResultDTO.Message = "Allergy added to student";
                operationResultDTO.operationResult = OperationResult.Success;
            }
            else
            {
                operationResultDTO.Message = "Allergy Existing ";
                operationResultDTO.operationResult = OperationResult.Error;
            }

            return operationResultDTO;
        }


    

        public List<AllergyStudentDTO> GetStudentAllergies()
        {
            try
            {
                var categories = new List<AllergyStudentDTO>();

                return AllergyMapper.Mapper(this.Context).GetStudentAllergies((long)Context.LoginID);

            }
            catch (Exception ex)
            {
                LogHelper<AppDataBL>.Fatal(ex.Message, ex);
                return null;
            }
        }

        public List<AllergyStudentDTO> CheckStudentAllergy(long studentID, long cartID)
        {
            try
            {
                var categories = new List<AllergyStudentDTO>();

                return AllergyMapper.Mapper(this.Context).CheckStudentAllergy(studentID, (long)Context.LoginID, cartID);

            }
            catch (Exception ex)
            {
                LogHelper<AppDataBL>.Fatal(ex.Message, ex);
                return null;
            }
        }

        public OperationResultDTO saveDefaultStudent(long studentID)
        {
            var operationResultDTO = new OperationResultDTO();

            try
            {

                var data = new ShoppingCartRepository().saveDefaultStudent(studentID, Context);

                if (data == "Data Added")
                {
                    operationResultDTO.Message = "Successfully Regsistered as Customer";
                    operationResultDTO.operationResult = OperationResult.Success;
                }
                else
                {
                    if (data == "Already Exist")
                    {
                        operationResultDTO.Message = "";
                        operationResultDTO.operationResult = OperationResult.Success;
                    }
                    else
                    {
                        operationResultDTO.Message = "";
                        operationResultDTO.operationResult = OperationResult.Error;
                    }
                }
            }
            catch (Exception ex)
            {
                operationResultDTO.Message = ex.Message;
                operationResultDTO.operationResult = OperationResult.Error;
            }

            return operationResultDTO;
        }


        public CustomerDTO getDefaultStudent()
        {
            try
            {
                ;
                var data = new ShoppingCartRepository().getDefaultStudent(Context);
                return data;

            }
            catch (Exception ex)
            {
                LogHelper<AppDataBL>.Fatal(ex.Message, ex);
                return null;
            }
        }

        public OperationResultDTO UpdateCartScheduleDate(CartScheduleDateDTO scheduleDate)
        {
            var result = new OperationResultDTO()
            {
                operationResult = Framework.Contracts.Common.Enums.OperationResult.Success,
                Message = "CartUpdatedSuccessfully",
            };

            var cartDTO = new ShoppingCartBL(Context).GetCart(this.Context, 0, isDeliveryCharge: false,
                   cartStatus: ShoppingCartStatus.InProcess);
            //update the changefor 
            var data = new ShoppingCartRepository().UpdateCartScheduleDate(cartDTO.ShoppingCartID, scheduleDate.ScheduleDate != null ? DateTime.Parse(scheduleDate.ScheduleDate) : (DateTime?)null);
            return result;
        }


        public List<SubscriptionTypeDTO> GetSubscriptionTypes(long deliveryTypeId, DateTime dateTime)
        {


            return new List<SubscriptionTypeDTO>() {
              
                    new SubscriptionTypeDTO()
                {
                     SubscriptionTypeID = 1,
                     SubscriptionName = "Weekly",

                },
                         new SubscriptionTypeDTO()
                {
                     SubscriptionTypeID = 2,
                     SubscriptionName = "Daily",

                }
                };

        
        }


        public OperationResultDTO SaveSchedule(ShoppingCartDTO schedule)
        {

            var operationResultDTO = new OperationResultDTO();
            var cartDTO = new ShoppingCartBL(Context).GetCart(this.Context, 0, isDeliveryCharge: false,
                 cartStatus: ShoppingCartStatus.InProcess);

            var isAdded = new ShoppingCartRepository().SaveSchedule(cartDTO.ShoppingCartID, schedule.StartDate != null ? DateTime.Parse(schedule.StartDate) : (DateTime?)null ,
                schedule.EndDate != null ? DateTime.Parse(schedule.EndDate) : (DateTime?)null ,
                schedule.SubscriptionTypeID , schedule.DaysID ,  schedule.DeliveryDaysCount
                );

            if (isAdded)
            {
                operationResultDTO.Message = "Subscription added to student";
                operationResultDTO.operationResult = OperationResult.Success;
            }
            else if (!isAdded)
            {
                operationResultDTO.Message = "Select more than 2 working days";
                operationResultDTO.operationResult = OperationResult.Error;
            }
            else
            {
                operationResultDTO.Message = "Subscription Existing ";
                operationResultDTO.operationResult = OperationResult.Error;
            }

            return operationResultDTO;
        }

        public List<DaysDTO> GetWeekDays()
        {
            try
            {
                var daysDTO = new List<DaysDTO>();
                var days = new ShoppingCartRepository().GetWeekDays();

                return days;
            }
            catch (Exception ex)
            {
                LogHelper<AppDataBL>.Fatal(ex.Message, ex);
                return null;
            }
        }

        public long GetAcademicCalenderByDateRange(DateTime startDate, DateTime endDate , string daysID)
        {
            var isAdded = new ShoppingCartRepository().GetAcademicCalenderByDateRange(startDate, endDate , daysID);
            return isAdded;
            //schedule.StartDate != null ? DateTime.Parse(schedule.StartDate) : (DateTime?)null ,
            //    schedule.EndDate != null ? DateTime.Parse(schedule.EndDate) : (DateTime?)null ,

        }

        public List<Services.Contracts.OrderHistory.OrderHistoryDTO> GetSubscriptionOrderHistoryDetails(int pageSize, int pageNo)
        {
            //var userDTO = GetUserDetails();
            var settingDetail = new SettingBL(Context).GetSettingValue<string>("ONLINEBRANCHID");

            var customerID = new CustomerBL(Context).GetCustomerIDByContext();
            var orderhistoryDTOList = _orderManager.GetScheduledOrderHistoryDetails(settingDetail, customerID.ToString(), pageNo, pageSize, Context);

            return orderhistoryDTOList;
        }

        public List<KeyValueDTO> GetLeaveTypes()
        {
            try
            {
                var LeaveTypes = new List<KeyValueDTO>();

                return LeaveRequestMapper.Mapper(this.Context).GetLeaveTypes();

            }
            catch (Exception ex)
            {
                LogHelper<AppDataBL>.Fatal(ex.Message, ex);
                return null;
            }
        }

        public List<Services.Contracts.OrderHistory.OrderHistoryDTO> GetCanteenNormalOrderHistoryDetails(int pageSize, int pageNo)
        {
            //var userDTO = GetUserDetails();
            var settingDetail = new SettingBL(Context).GetSettingValue<string>("ONLINEBRANCHID");

            var customerID = new CustomerBL(Context).GetCustomerIDByContext();
            var orderhistoryDTOList = _orderManager.GetCanteenNormalOrderHistoryDetails(settingDetail, customerID.ToString(), pageNo, pageSize, Context);

            return orderhistoryDTOList;
        }


        public List<KeyValueDTO> GetSeverity()
        {
            try
            {
                var categories = new List<KeyValueDTO>();

                return AllergyMapper.Mapper(this.Context).GetSeverity();

            }
            catch (Exception ex)
            {
                LogHelper<AppDataBL>.Fatal(ex.Message, ex);
                return null;
            }
        }
    }
}

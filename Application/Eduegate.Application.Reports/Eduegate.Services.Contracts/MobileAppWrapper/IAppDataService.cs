using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Checkout;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.HR;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Services.Contracts.ProductDetail;
using Eduegate.Services.Contracts.Salon;
using Eduegate.Services.Contracts.SearchData;
using Eduegate.Framework.Contracts.Common.PageRender;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Distributions;
using System;
using Eduegate.Services.Contracts.Contents;
using Eduegate.Services.Contracts.School.Inventory;
using Eduegate.Services.Contracts.School.Students;

namespace Eduegate.Services.Contracts.MobileAppWrapper
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAppDataService" in both code and config file together.
    [ServiceContract]
    public interface IAppDataService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetHomeBanners")]
        List<BannerMasterDTO> GetHomeBanners();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSingleHomeBanner")]
        BannerMasterDTO GetSingleHomeBanner(BoilerPlateDTO boilerplateDTO);

        //[OperationContract]
        //[WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProducts?widgetType={widgetType}")]
        //Eduegate.Services.Contracts.SearchData.SearchResultDTO GetProducts(WidgetTypes widgetType);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProducts")]
        List<ProductSKUDetailDTO> GetProducts(BoilerPlateDTO boilerPlateDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductsEmarsys?widgetType={widgetType}&productIDs={productIDs}&cultureID={cultureID}")]
        List<ProductSKUDetailDTO> GetProductsEmarsys(WidgetTypes widgetType, string productIDs, long cultureID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCategories")]
        List<CategoryDTO> GetCategories();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetKnowHowOptions")]
        List<KnowHowOptionDTO> GetKnowHowOptions();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCountries")]
        List<CountryDTO> GetCountries();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Register")]
        OperationResultDTO Register(UserDTO Register);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Login")]
        OperationResultDTO Login(UserDTO user);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetUserDetails")]
        UserDTO GetUserDetails();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "LogOut")]
        UserDTO LogOut();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetShoppingCartCount")]
        int GetShoppingCartCount();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductSearch?pageIndex={pageIndex}&pageSize={pageSize}&searchText={searchText}&searchVal={searchVal}&searchBy={searchBy}&sortBy={sortBy}&pageType={pageType}&isCategory={isCategory}")]
        Eduegate.Services.Contracts.SearchData.SearchResultDTO GetProductSearch
            (int pageIndex, int pageSize, string searchText, string searchVal,
            string searchBy, string sortBy, string pageType, bool isCategory);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductsfromSolr?pageIndex={pageIndex}&pageSize={pageSize}&searchText={searchText}&searchVal={searchVal}&searchBy={searchBy}&sortBy={sortBy}&pageType={pageType}")]
        Eduegate.Services.Contracts.SearchData.SearchResultDTO GetProductsfromSolr(int pageIndex, int pageSize, string searchText, string searchVal, string searchBy, string sortBy, string pageType);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AddToCart")]
        OperationResultDTO AddToCart(CartProductDTO cartDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCartDetails")]
        CartDTO GetCartDetails();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "RemoveCartItem")]
        OperationResultDTO RemoveCartItem(CartProductDTO cartDTO);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdateCart")]
        OperationResultDTO UpdateCart(CartProductDTO cartDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSaveForLaterCount")]
        long GetSaveForLaterCount();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSaveForLater")]
        List<WishListDTO> GetSaveForLater();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "AddSaveForLater?skuID={skuID}")]
        OperationResultDTO AddSaveForLater(long skuID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "RemoveSaveForLater?skuID={skuID}")]
        OperationResultDTO RemoveSaveForLater(long skuID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdateCartDelivery")]
        OperationResultDTO UpdateCartDelivery(CartProductDTO cartDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetLastShippingAddress?addressID={addressID}")]
        ContactDTO GetLastShippingAddress(long addressID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetBillingAddress")]
        ContactDTO GetBillingAddress();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAddressByContactID?addressID={addressID}")]
        ContactDTO GetAddressByContactID(long addressID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdateContact")]
        OperationResultDTO UpdateContact(ContactDTO contactDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAreaByCountryID?countryID={countryID}")]
        List<AreaDTO> GetAreaByCountryID(string countryID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCityByCountryID?countryID={countryID}")]
        List<CityDTO> GetCityByCountryID(string countryID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAreaByCityID?cityID={cityID}")]
        List<AreaDTO> GetAreaByCityID(string cityID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AddContact")]
        long AddContact(ContactDTO contactDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetShippingAddressContacts")]
        List<ContactDTO> GetShippingAddressContacts();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "RemoveContact")]
        OperationResultDTO RemoveContact(long contactID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "ValidateVoucher?voucherNo={voucherNo}")]
        VoucherDTO ValidateVoucher(string voucherNo);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "DefaultPaymentOption")]
        long DefaultPaymentOption();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveWebsiteOrder")]
        CheckoutPaymentMobileDTO SaveWebsiteOrder(CheckoutPaymentDTO checkoutPaymentDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetOrderHistoryDetails?pageSize={pageSize}&pageNo={pageNo}")]
        List<OrderHistory.OrderHistoryDTO> GetOrderHistoryDetails(int pageSize, int pageNo);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetPersonalSettings")]
        UserDTO GetPersonalSettings();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SavePersonalSettings")]
        OperationResultDTO SavePersonalSettings(UserDTO userDTO);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdatePassword")]
        OperationResultDTO UpdatePassword(ChangePasswordDTO changePasswordDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetPageInfo?pageID={pageID}&parameter={parameter}")]
        PageDTO GetPageInfo(long pageID, string parameter);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCategoryBanner")]
        List<CategoryImageMapDTO> GetCategoryBanner(BoilerPlateDTO boilerPlateDTO);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "CategoryProductLists")]
        Eduegate.Services.Contracts.SearchData.SearchResultDTO CategoryProductLists(CategoryProductListDTO categoryProductListDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSubCategories?categoryID={categoryID}")]
        List<CategoryDTO> GetSubCategories(long categoryID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "CategoryDetails?categoryID={categoryID}")]
        CategoryDTO CategoryDetails(long categoryID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductDetailImages?skuID={skuID}")]
        List<ProductImageMapDTO> GetProductDetailImages(long skuID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductDetail?skuID={skuID}&cultureID={cultureID}")]
        ProductSKUDetailDTO GetProductDetail(long skuID, long cultureID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductFeatures?skuID={skuID}")]
        List<ProductDetailKeyFeatureDTO> GetProductFeatures(long skuID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductVariant?skuID={skuID}&cultureID={cultureID}")]
        List<ProductSKUVariantDTO> GetProductVariant(long skuID, long cultureID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GenerateApiKey?uuid={uuid}&version={version}")]
        string GenerateApiKey(string uuid, string version);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GenerateGUID")]
        string GenerateGUID();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "MergeCart")]
        bool MergeCart();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdateAddressinShoppingCart")]
        OperationResultDTO UpdateAddressinShoppingCart(CheckoutPaymentDTO checkoutPaymentDTO);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "ForgotPassword")]
        OperationResultDTO ForgotPassword(string emailID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductDeliveryTypeList?skuID={skuID}&cultureID={cultureID}")]
        List<ProductDetailDeliveryOption> GetProductDeliveryTypeList(long skuID, long cultureID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductDetailsDescription?skuID={skuID}&cultureID={cultureID}")]
        ProductInventoryConfigDTO GetProductDetailsDescription(long skuID, long cultureID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCategoryByCode?categoryCode={categoryCode}")]
        CategoryDTO GetCategoryByCode(string categoryCode);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AddNewsletterSubscription")]
        OperationResultDTO AddNewsletterSubscription(NewsletterSubscriptionDTO newsletterSubscriptionDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetOrderHistoryCount")]
        int GetOrderHistoryCount();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetOrderHistoryItemDetail?headID={headID}")]
        List<OrderHistory.OrderHistoryDTO> GetOrderHistoryItemDetail(long headID);


        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "ResendMail")]
        OperationResultDTO ResendMail(long headID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductMultiPriceDetails?skuID={skuID}")]
        List<ProductMultiPriceDTO> GetProductMultiPriceDetails(long skuID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductQtyDiscountDetails?skuID={skuID}")]
        List<ProductQuantityDiscountDTO> GetProductQtyDiscountDetails(long skuID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetBlinkKeywordsDictionary?searchtext={searchtext}&lng={lng}")]
        List<SearchKeywordsDictionaryDTO> GetBlinkKeywordsDictionary(string searchtext, string lng);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetActiveDealProducts")]
        List<ProductSKUDetailDTO> GetActiveDealProducts(BoilerPlateDTO boilerPlateDTO);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "ValidationBeforePayment")]
        OperationResultDTO ValidationBeforePayment(CheckoutPaymentDTO checkoutPaymentDTO);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "isVoucherPayment")]
        bool isVoucherPayment(CheckoutPaymentDTO checkoutPaymentDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetPaymentMethods")]
        List<PaymentMethodDTO> GetPaymentMethods();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "PaymentFailure")]
        Eduegate.Services.Contracts.Payments.PaymentDTO PaymentFailure(CheckoutPaymentDTO checkoutPaymentDTO);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "ConfirmOnlineOrder")]
        CheckoutPaymentMobileDTO ConfirmOnlineOrder(CheckoutPaymentDTO checkoutPaymentDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SetKnetGateway?emailID={emailID}&customerID={customerID}&amount={amount}&initiatedIP={initiatedIP}&paymentGateway={paymentGateway}&companyID={companyID}&loginID={loginID}&GUID={GUID}&currencyCode={currencyCode}&userId={userId}&apiKey={apiKey}&languageCode={languageCode}&siteID={siteID}&udf5={udf5}")]
        string SetKnetGateway(string emailID, string customerID, string amount, string initiatedIP, string paymentGateway, string companyID, string loginID, string GUID, string currencyCode, string userId, string apiKey, string languageCode, string siteID, string udf5);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                    BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "CheckAppVersion?mobileAppVersion={mobileAppVersion}")]
        bool CheckAppVersion(string mobileAppVersion);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                    BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetMenuDetailsByType")]
        List<Eduegate.Services.Contracts.MenuLinks.MenuDTO> GetMenuDetailsByType();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                    BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetServices")]
        List<ServiceDTO> GetServices();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                   BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSaloons")]
        List<SaloonDTO> GetSaloons();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                   BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetEmployeesByRoles?roleID={roleID}")]
        List<EmployeeDTO> GetEmployeesByRoles(int roleID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetJobOpenings?filter={filter}")]
        List<JobOpeningDTO> GetJobOpenings(string filter);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetJobOpening?jobID={jobID}")]
        JobOpeningDTO GetJobOpening(string jobID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetBranches")]
        List<BranchDTO> GetBranches();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdateUserDefaultBranch?branchID={branchID}")]
        bool UpdateUserDefaultBranch(long branchID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCartQuantity")]
        List<CartItemQuantityDTO> GetCartQuantity();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCategoriesByBoilerplate")]
        List<CategoryDetailDTO> GetCategoriesByBoilerplate(BoilerPlateDTO boilerPlateDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAllCategories?searchText={searchText}")]
        List<FacetsDetail> GetAllCategories(string searchText = "");

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "ValidateContinueCheckOut")]
        OperationResultDTO ValidateContinueCheckOut();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "OnlineStoreAddContact")]
        long OnlineStoreAddContact(ContactDTO contactDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCustomers?searchText={searchText}&dataSize={dataSize}")]
        List<CustomerDTO> GetCustomers(string searchText, int dataSize);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAllDeliveryTypes")]
        List<DeliverySettingDTO> GetAllDeliveryTypes();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetDeliveryBlockingPeriods")]
        DeliveryBlockingPeriodDTO GetDeliveryBlockingPeriods();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetDeliveryTimeSlots?deliveryTypeId={deliveryTypeId}&dateTime={dateTime}")]
        List<DeliveryTimeSlotDTO> GetDeliveryTimeSlots(long deliveryTypeId, DateTime dateTime);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetPaymentMethodsforOnlineStore")]
        List<PaymentMethodDTO> GetPaymentMethodsforOnlineStore();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "OnlineStoreGenerateOrder")]
        CheckoutPaymentMobileDTO OnlineStoreGenerateOrder(CheckoutPaymentDTO checkoutPaymentDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "OnlineStoreGetOrderHistoryDetails?pageSize={pageSize}&pageNo={pageNo}")]
        List<OrderHistory.OrderHistoryDTO> OnlineStoreGetOrderHistoryDetails(int pageSize, int pageNo);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "OnlineStoreGetProductSearch?pageIndex={pageIndex}&pageSize={pageSize}" +
            "&searchText={searchText}&searchVal={searchVal}&searchBy={searchBy}&sortBy={sortBy}" +
            "&pageType={pageType}&isCategory={isCategory}&statusID={statusID}")]
        Eduegate.Services.Contracts.SearchData.SearchResultDTO OnlineStoreGetProductSearch
        (int pageIndex, int pageSize, string searchText, string searchVal,
        string searchBy, string sortBy, string pageType, bool isCategory, short statusID = 2);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveShoppingCartDetails")]
        OperationResultDTO SaveShoppingCartDetails(CheckoutPaymentDTO checkoutPaymentDTO);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "CheckLoginforCustomer")]
        OperationResultDTO CheckLoginforCustomer(UserDTO userDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "ReOrder?headID={headID}")]
        OperationResultDTO ReOrder(long headID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UploadContentAsString")]
        long UploadContentAsString(ContentFileDTO content);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAllergies")]
        List<KeyValueDTO> GetAllergies();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveAllergies?studentID={studentID}&allergyID={allergyID}&severityID={severityID}")]
        OperationResultDTO SaveAllergies(long studentID, int allergyID, byte SeverityID);
        
        
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetStudentAllergies")]
        List<AllergyStudentDTO> GetStudentAllergies();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "CheckStudentAllergy?studnetID={studentID}&cartID={cartID}")]
        List<AllergyStudentDTO> CheckStudentAllergy(long studentID, long cartID);



        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdateCartScheduleDate")]
        OperationResultDTO UpdateCartScheduleDate(CartScheduleDateDTO scheduleDate);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSubscriptionTypes?deliveryTypeId={deliveryTypeId}&dateTime={dateTime}")]
        List<SubscriptionTypeDTO> GetSubscriptionTypes(long deliveryTypeId, DateTime dateTime);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveSchedule")]
        OperationResultDTO SaveSchedule(ShoppingCartDTO Schedule);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetWeekDays")]
        List<DaysDTO> GetWeekDays();


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAcademicCalenderByDateRange?startDateString={startDateString}&endDateString={endDateString}&daysID={daysID}")]
        long GetAcademicCalenderByDateRange(string startDateString, string endDateString, string daysID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSubscriptionOrderHistoryDetails?pageSize={pageSize}&pageNo={pageNo}")]
        List<OrderHistory.OrderHistoryDTO> GetSubscriptionOrderHistoryDetails(int pageSize, int pageNo);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCanteenNormalOrderHistoryDetails?pageSize={pageSize}&pageNo={pageNo}")]
        List<OrderHistory.OrderHistoryDTO> GetCanteenNormalOrderHistoryDetails(int pageSize, int pageNo);
    }
} 
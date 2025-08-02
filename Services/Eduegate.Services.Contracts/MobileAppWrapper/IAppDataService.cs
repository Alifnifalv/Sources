using System.Collections.Generic;
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
    public interface IAppDataService
    {
        List<BannerMasterDTO> GetHomeBanners();

        BannerMasterDTO GetSingleHomeBanner(BoilerPlateDTO boilerplateDTO);

        //Eduegate.Services.Contracts.SearchData.SearchResultDTO GetProducts(WidgetTypes widgetType);

        List<ProductSKUDetailDTO> GetProducts(BoilerPlateDTO boilerPlateDTO);

        List<ProductSKUDetailDTO> GetProductsEmarsys(WidgetTypes widgetType, string productIDs, long cultureID);

        List<CategoryDTO> GetCategories();

        List<KnowHowOptionDTO> GetKnowHowOptions();

        List<CountryDTO> GetCountries();

        OperationResultDTO Register(UserDTO Register);

        OperationResultDTO Login(UserDTO user);

        UserDTO GetUserDetails();

        UserDTO LogOut();

        int GetShoppingCartCount();

        Eduegate.Services.Contracts.SearchData.SearchResultDTO GetProductSearch(int pageIndex, int pageSize, string searchText, string searchVal, string searchBy, string sortBy, string pageType, bool isCategory);

        Eduegate.Services.Contracts.SearchData.SearchResultDTO GetProductsfromSolr(int pageIndex, int pageSize, string searchText, string searchVal, string searchBy, string sortBy, string pageType);

        OperationResultDTO AddToCart(CartProductDTO cartDTO);

        CartDTO GetCartDetails();

        OperationResultDTO RemoveCartItem(CartProductDTO cartDTO);

        OperationResultDTO UpdateCart(CartProductDTO cartDTO);

        long GetSaveForLaterCount();

        List<WishListDTO> GetSaveForLater();

        OperationResultDTO AddSaveForLater(long skuID);

        OperationResultDTO RemoveSaveForLater(long skuID);

        OperationResultDTO UpdateCartDelivery(CartProductDTO cartDTO);

        ContactDTO GetLastShippingAddress(long addressID);

        ContactDTO GetBillingAddress();

        ContactDTO GetAddressByContactID(long addressID);

        OperationResultDTO UpdateContact(ContactDTO contactDTO);

        List<AreaDTO> GetAreaByCountryID(string countryID);

        List<CityDTO> GetCityByCountryID(string countryID);

        List<AreaDTO> GetAreaByCityID(string cityID);

        long AddContact(ContactDTO contactDTO);

        List<ContactDTO> GetShippingAddressContacts();

        OperationResultDTO RemoveContact(long contactID);

        VoucherDTO ValidateVoucher(string voucherNo);

        long DefaultPaymentOption();

        CheckoutPaymentMobileDTO SaveWebsiteOrder(CheckoutPaymentDTO checkoutPaymentDTO);

        List<OrderHistory.OrderHistoryDTO> GetOrderHistoryDetails(int pageSize, int pageNo);

        UserDTO GetPersonalSettings();

        OperationResultDTO SavePersonalSettings(UserDTO userDTO);

        OperationResultDTO UpdatePassword(ChangePasswordDTO changePasswordDTO);

        PageDTO GetPageInfo(long pageID, string parameter);

        List<CategoryImageMapDTO> GetCategoryBanner(BoilerPlateDTO boilerPlateDTO);

        Eduegate.Services.Contracts.SearchData.SearchResultDTO CategoryProductLists(CategoryProductListDTO categoryProductListDTO);

        List<CategoryDTO> GetSubCategories(long categoryID);

        CategoryDTO CategoryDetails(long categoryID);

        List<ProductImageMapDTO> GetProductDetailImages(long skuID);

        ProductSKUDetailDTO GetProductDetail(long skuID, long cultureID);

        List<ProductDetailKeyFeatureDTO> GetProductFeatures(long skuID);

        List<ProductSKUVariantDTO> GetProductVariant(long skuID, long cultureID);

        string GenerateApiKey(string uuid, string version);

        string GenerateGUID();

        bool MergeCart();

        OperationResultDTO UpdateAddressinShoppingCart(CheckoutPaymentDTO checkoutPaymentDTO);

        OperationResultDTO ForgotPassword(string emailID);

        List<ProductDetailDeliveryOption> GetProductDeliveryTypeList(long skuID, long cultureID);

        ProductInventoryConfigDTO GetProductDetailsDescription(long skuID, long cultureID);

        CategoryDTO GetCategoryByCode(string categoryCode);

        OperationResultDTO AddNewsletterSubscription(NewsletterSubscriptionDTO newsletterSubscriptionDTO);

        int GetOrderHistoryCount();

        List<OrderHistory.OrderHistoryDTO> GetOrderHistoryItemDetail(long headID);

        OperationResultDTO ResendMail(long headID);

        List<ProductMultiPriceDTO> GetProductMultiPriceDetails(long skuID);

        List<ProductQuantityDiscountDTO> GetProductQtyDiscountDetails(long skuID);

        List<SearchKeywordsDictionaryDTO> GetBlinkKeywordsDictionary(string searchtext, string lng);

        List<ProductSKUDetailDTO> GetActiveDealProducts(BoilerPlateDTO boilerPlateDTO);

        OperationResultDTO ValidationBeforePayment(CheckoutPaymentDTO checkoutPaymentDTO);

        bool IsVoucherPayment(CheckoutPaymentDTO checkoutPaymentDTO);

        List<PaymentMethodDTO> GetPaymentMethods();

        Eduegate.Services.Contracts.Payments.PaymentDTO PaymentFailure(CheckoutPaymentDTO checkoutPaymentDTO);

        CheckoutPaymentMobileDTO ConfirmOnlineOrder(CheckoutPaymentDTO checkoutPaymentDTO);

        string SetKnetGateway(string emailID, string customerID, string amount, string initiatedIP, string paymentGateway, string companyID, string loginID, string GUID, string currencyCode, string userId, string apiKey, string languageCode, string siteID, string udf5);

        bool CheckAppVersion(string mobileAppVersion);

        List<Eduegate.Services.Contracts.MenuLinks.MenuDTO> GetMenuDetailsByType();

        List<ServiceDTO> GetServices();

        List<SaloonDTO> GetSaloons();

        List<EmployeeDTO> GetEmployeesByRoles(int roleID);

        //List<JobOpeningDTO> GetJobOpenings(string filter);

        //JobOpeningDTO GetJobOpening(string jobID);

        List<BranchDTO> GetBranches();

        bool UpdateUserDefaultBranch(long branchID);

        List<CartItemQuantityDTO> GetCartQuantity();

        List<CategoryDetailDTO> GetCategoriesByBoilerplate(BoilerPlateDTO boilerPlateDTO);

        List<FacetsDetail> GetAllCategories(string searchText = "");

        OperationResultDTO ValidateContinueCheckOut();

        long OnlineStoreAddContact(ContactDTO contactDTO);

        List<CustomerDTO> GetCustomers(string searchText, int dataSize);

        List<DeliverySettingDTO> GetAllDeliveryTypes();

        DeliveryBlockingPeriodDTO GetDeliveryBlockingPeriods();

        List<DeliveryTimeSlotDTO> GetDeliveryTimeSlots(long deliveryTypeId, DateTime dateTime);

        List<PaymentMethodDTO> GetPaymentMethodsforOnlineStore();

        CheckoutPaymentMobileDTO OnlineStoreGenerateOrder(CheckoutPaymentDTO checkoutPaymentDTO);

        List<OrderHistory.OrderHistoryDTO> OnlineStoreGetOrderHistoryDetails(int pageSize, int pageNo);

        Eduegate.Services.Contracts.SearchData.SearchResultDTO OnlineStoreGetProductSearch(int pageIndex, int pageSize, string searchText, string searchVal, string searchBy, string sortBy, string pageType, bool isCategory, short statusID = 2);

        OperationResultDTO SaveShoppingCartDetails(CheckoutPaymentDTO checkoutPaymentDTO);

        OperationResultDTO CheckLoginforCustomer(UserDTO userDTO);

        OperationResultDTO ReOrder(long headID);

        long UploadContentAsString(ContentFileDTO content);

        List<KeyValueDTO> GetAllergies();

        OperationResultDTO SaveAllergies(long studentID, int allergyID, byte SeverityID);

        List<AllergyStudentDTO> GetStudentAllergies();

        List<AllergyStudentDTO> CheckStudentAllergy(long studentID, long cartID);

        OperationResultDTO UpdateCartScheduleDate(CartScheduleDateDTO scheduleDate);

        List<SubscriptionTypeDTO> GetSubscriptionTypes(long deliveryTypeId, DateTime dateTime);

        OperationResultDTO SaveSchedule(ShoppingCartDTO Schedule);

        List<DaysDTO> GetWeekDays();

        long GetAcademicCalenderByDateRange(string startDateString, string endDateString, string daysID);

        List<OrderHistory.OrderHistoryDTO> GetSubscriptionOrderHistoryDetails(int pageSize, int pageNo);

        List<OrderHistory.OrderHistoryDTO> GetCanteenNormalOrderHistoryDetails(int pageSize, int pageNo);
    }
}
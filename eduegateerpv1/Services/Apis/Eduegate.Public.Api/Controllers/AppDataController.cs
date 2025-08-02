using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.PageRender;
using Eduegate.Hub.Client;
using Eduegate.PublicAPI.Common;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Checkout;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Distributions;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Services.Contracts.HR;
using Eduegate.Services.Contracts.MobileAppWrapper;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Services.Contracts.ProductDetail;
using Eduegate.Services.Contracts.Salon;
using Eduegate.Services.Contracts.School.Inventory;
using Eduegate.Services.Contracts.SearchData;
using Eduegate.Services.MobileAppWrapper;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Eduegate.Public.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppDataController : ApiControllerBase
    {
        private readonly ILogger<AppDataController> _logger;
        private readonly dbEduegateSchoolContext _dbContext;
        private readonly IBackgroundJobClient _backgroundJobs;
        private readonly RealtimeClient _realtimeClient;
        private readonly IHttpContextAccessor _accessor;

        public AppDataController(ILogger<AppDataController> logger, IHttpContextAccessor context,
            dbEduegateSchoolContext dbContext, IBackgroundJobClient backgroundJobs,
            IServiceProvider serviceProvider, RealtimeClient realtimeClient,
            IHttpContextAccessor accessor) : base(context)
        {
            _logger = logger;
            _dbContext = dbContext;
            _backgroundJobs = backgroundJobs;
            _realtimeClient = realtimeClient;
            _accessor = accessor;
        }

        [HttpGet]
        [Route("GetHomeBanners")]
        public List<Eduegate.Services.Contracts.BannerMasterDTO> GetHomeBanners()
        {
            return new AppDataService(CallContext).GetHomeBanners();
        }

        [HttpGet]
        [Route("GetSingleHomeBanner")]
        public BannerMasterDTO GetSingleHomeBanner(BoilerPlateDTO boilerplateDTO)
        {
            return new AppDataService(CallContext).GetSingleHomeBanner(boilerplateDTO);
        }

        //[HttpGet]
        //[Route("GetProducts")]
        //public Eduegate.Services.Contracts.SearchData.SearchResultDTO GetProducts(Contracts.Enums.WidgetTypes widgetType)
        //{
        //    return new AppDataService(CallContext).GetProducts(widgetType);
        //}

        [HttpPost]
        [Route("GetProducts")]
        public List<ProductSKUDetailDTO> GetProducts(BoilerPlateDTO boilerPlateDTO)
        {
            return new AppDataService(CallContext).GetProducts(boilerPlateDTO);
        }

        [HttpGet]
        [Route("GetProductsEmarsys")]
        public List<ProductSKUDetailDTO> GetProductsEmarsys(Eduegate.Services.Contracts.Enums.WidgetTypes widgetType, string productIDs, long cultureID)
        {
            return new AppDataService(CallContext).GetProductsEmarsys(widgetType, productIDs, cultureID);
        }

        [HttpGet]
        [Route("GetCategories")]
        public List<Eduegate.Services.Contracts.Catalog.CategoryDTO> GetCategories()
        {
            return new AppDataService(CallContext).GetCategories();
        }

        [HttpGet]
        [Route("GetKnowHowOptions")]
        public List<KnowHowOptionDTO> GetKnowHowOptions()
        {
            return new AppDataService(CallContext).GetKnowHowOptions();
        }

        [HttpGet]
        [Route("GetCountries")]
        public List<CountryDTO> GetCountries()
        {
            return new AppDataService(CallContext).GetCountries();
        }

        [HttpPost]
        [Route("Register")]
        public OperationResultDTO Register(UserDTO Register)
        {
            return new AppDataService(CallContext).Register(Register);
        }

        [HttpPost]
        [Route("Login")]
        public OperationResultDTO Login(UserDTO user)
        {
            return new AppDataService(CallContext).Login(user);
        }

        [HttpGet]
        [Route("GetUserDetails")]
        public UserDTO GetUserDetails()
        {
            return new AppDataService(CallContext).GetUserDetails();
        }

        [HttpPost]
        [Route("LogOut")]
        public UserDTO LogOut()
        {
            return new AppDataService(CallContext).LogOut();
        }

        [HttpGet]
        [Route("GetShoppingCartCount")]
        public int GetShoppingCartCount()
        {
            return new AppDataService(CallContext).GetShoppingCartCount();
        }

        [HttpGet]
        [Route("GetProductsfromSolr")]
        public Eduegate.Services.Contracts.SearchData.SearchResultDTO GetProductsfromSolr(int pageIndex, int pageSize, string searchText, string searchVal, string searchBy, string sortBy, string pageType)
        {
            return new AppDataService(CallContext).GetProductsfromSolr(pageIndex, pageSize, searchText, searchVal, searchBy, sortBy, pageType);
        }

        [HttpGet]
        [Route("GetProductSearch")]
        public Eduegate.Services.Contracts.SearchData.SearchResultDTO GetProductSearch
            (int pageIndex = 1, int pageSize = 50, string searchText = "", string searchVal = "",
            string searchBy = "", string sortBy = "relevance", string pageType = "", bool isCategory = false)
        {
            return new AppDataService(CallContext).GetProductSearch
                (pageIndex, pageSize, searchText, searchVal, searchBy, sortBy, pageType, isCategory);
        }

        [HttpPost]
        [Route("AddToCart")]
        public OperationResultDTO AddToCart(CartProductDTO cartDTO)
        {
            return new AppDataService(CallContext).AddToCart(cartDTO);
        }

        [HttpGet]
        [Route("GetCartDetails")]
        public CartDTO GetCartDetails()
        {
            return new AppDataService(CallContext).GetCartDetails();
        }

        [HttpPost]
        [Route("RemoveCartItem")]
        public OperationResultDTO RemoveCartItem(CartProductDTO cartDTO)
        {
            return new AppDataService(CallContext).RemoveCartItem(cartDTO);
        }

        [HttpPost]
        [Route("UpdateCart")]
        public OperationResultDTO UpdateCart(CartProductDTO cartDTO)
        {
            return new AppDataService(CallContext).UpdateCart(cartDTO);
        }

        [HttpGet]
        [Route("GetSaveForLaterCount")]
        public long GetSaveForLaterCount()
        {
            return new AppDataService(CallContext).GetSaveForLaterCount();
        }

        [HttpGet]
        [Route("GetSaveForLater")]
        public List<WishListDTO> GetSaveForLater()
        {
            return new AppDataService(CallContext).GetSaveForLater();
        }

        [HttpPost]
        [Route("AddSaveForLater")]
        public OperationResultDTO AddSaveForLater(long skuID)
        {
            return new AppDataService(CallContext).AddSaveForLater(skuID);
        }

        [HttpGet]
        [Route("RemoveSaveForLater")]
        public OperationResultDTO RemoveSaveForLater(long skuID)
        {
            return new AppDataService(CallContext).RemoveSaveForLater(skuID);
        }

        [HttpPost]
        [Route("UpdateCartDelivery")]
        public OperationResultDTO UpdateCartDelivery(CartProductDTO cartDTO)
        {
            return new AppDataService(CallContext).UpdateCartDelivery(cartDTO);
        }

        [HttpGet]
        [Route("GetLastShippingAddress")]
        public Eduegate.Services.Contracts.ContactDTO GetLastShippingAddress(long addressID)
        {
            return new AppDataService(CallContext).GetLastShippingAddress(addressID);
        }

        [HttpGet]
        [Route("GetBillingAddress")]
        public Eduegate.Services.Contracts.ContactDTO GetBillingAddress()
        {
            return new AppDataService(CallContext).GetBillingAddress();
        }

        [HttpGet]
        [Route("GetAddressByContactID")]
        public Eduegate.Services.Contracts.ContactDTO GetAddressByContactID(long addressID)
        {
            return new AppDataService(CallContext).GetAddressByContactID(addressID);
        }

        [HttpPost]
        [Route("UpdateContact")]
        public OperationResultDTO UpdateContact(Eduegate.Services.Contracts.ContactDTO contactDTO)
        {
            return new AppDataService(CallContext).UpdateContact(contactDTO);
        }

        [HttpGet]
        [Route("GetAreaByCountryID")]
        public List<AreaDTO> GetAreaByCountryID(string countryID)
        {
            return new AppDataService(CallContext).GetAreaByCountryID(countryID);
        }

        [HttpGet]
        [Route("GetCityByCountryID")]
        public List<CityDTO> GetCityByCountryID(string countryID)
        {
            return new AppDataService(CallContext).GetCityByCountryID(countryID);
        }

        [HttpGet]
        [Route("GetAreaByCityID")]
        public List<AreaDTO> GetAreaByCityID(string cityID)
        {
            return new AppDataService(CallContext).GetAreaByCityID(cityID);
        }

        [HttpPost]
        [Route("AddContact")]
        public long AddContact(Eduegate.Services.Contracts.ContactDTO contactDTO)
        {
            return new AppDataService(CallContext).AddContact(contactDTO);
        }

        [HttpGet]
        [Route("GetShippingAddressContacts")]
        public List<Eduegate.Services.Contracts.ContactDTO> GetShippingAddressContacts()
        {
            return new AppDataService(CallContext).GetShippingAddressContacts();
        }

        [HttpPost]
        [Route("RemoveContact")]
        public OperationResultDTO RemoveContact(long contactID)
        {
            return new AppDataService(CallContext).RemoveContact(contactID);
        }

        [HttpGet]
        [Route("ValidateVoucher")]
        public VoucherDTO ValidateVoucher(string voucherNo)
        {
            return new AppDataService(CallContext).ValidateVoucher(voucherNo);
        }

        [HttpGet]
        [Route("DefaultPaymentOption")]
        public long DefaultPaymentOption()
        {
            return new AppDataService(CallContext).DefaultPaymentOption();
        }

        [HttpPost]
        [Route("SaveWebsiteOrder")]
        public ActionResult SaveWebsiteOrder(CheckoutPaymentDTO checkoutPaymentDTO)
        {
            return Ok(new AppDataService(CallContext).SaveWebsiteOrder(checkoutPaymentDTO));
        }

        [HttpGet]
        [Route("GetOrderHistoryDetails")]
        public ActionResult GetOrderHistoryDetails(int pageSize, int pageNo)
        {
            return Ok(new AppDataService(CallContext).GetOrderHistoryDetails(pageSize, pageNo));
        }

        [HttpGet]
        [Route("GetPersonalSettings")]
        public UserDTO GetPersonalSettings()
        {
            return new AppDataService(CallContext).GetPersonalSettings();
        }

        [HttpPost]
        [Route("SavePersonalSettings")]
        public OperationResultDTO SavePersonalSettings(UserDTO userDTO)
        {
            return new AppDataService(CallContext).SavePersonalSettings(userDTO);
        }

        [HttpPost]
        [Route("UpdatePassword")]
        public OperationResultDTO UpdatePassword(ChangePasswordDTO changePasswordDTO)
        {
            return new AppDataService(CallContext).UpdatePassword(changePasswordDTO);
        }

        [HttpGet]
        [Route("GetPageInfo")]
        public PageDTO GetPageInfo(long pageID, string parameter)
        {
            return new AppDataService(CallContext).GetPageInfo(pageID, parameter);
        }

        [HttpGet]
        [Route("GetCategoryBanner")]
        public List<CategoryImageMapDTO> GetCategoryBanner(BoilerPlateDTO boilerPlateDTO)
        {
            return new AppDataService(CallContext).GetCategoryBanner(boilerPlateDTO);
        }

        [HttpGet]
        [Route("CategoryProductLists")]
        public Eduegate.Services.Contracts.SearchData.SearchResultDTO CategoryProductLists(CategoryProductListDTO categoryProductListDTO)
        {
            return new AppDataService(CallContext).CategoryProductLists(categoryProductListDTO);
        }

        [HttpGet]
        [Route("GetSubCategories")]
        public List<CategoryDTO> GetSubCategories(long categoryID)
        {
            return new AppDataService(CallContext).GetSubCategories(categoryID);
        }

        [HttpGet]
        [Route("CategoryDetails")]
        public CategoryDTO CategoryDetails(long categoryID)
        {
            return new AppDataService(CallContext).CategoryDetails(categoryID);
        }

        [HttpGet]
        [Route("GetProductDetailImages")]
        public List<ProductImageMapDTO> GetProductDetailImages(long skuID)
        {
            return new AppDataService(CallContext).GetProductDetailImages(skuID);
        }

        [HttpGet]
        [Route("GetProductDetail")]
        public ProductSKUDetailDTO GetProductDetail(long skuID, long cultureID)
        {
            return new AppDataService(CallContext).GetProductDetail(skuID, cultureID);
        }

        [HttpGet]
        [Route("GetProductFeatures")]
        public List<ProductDetailKeyFeatureDTO> GetProductFeatures(long skuID)
        {
            return new AppDataService(CallContext).GetProductFeatures(skuID);
        }

        [HttpGet]
        [Route("GetProductVariant")]
        public List<ProductSKUVariantDTO> GetProductVariant(long skuID, long cultureID)
        {
            return new AppDataService(CallContext).GetProductVariant(skuID, cultureID);
        }

        [HttpGet]
        [Route("GenerateApiKey")]
        public string GenerateApiKey(string uuid, string version)
        {
            return new AppDataService(CallContext).GenerateApiKey(uuid, version);
        }

        [HttpGet]
        [Route("GenerateGUID")]
        public string GenerateGUID()
        {
            return new AppDataService(CallContext).GenerateGUID();
        }

        [HttpPost]
        [Route("MergeCart")]
        public bool MergeCart()
        {
            return new AppDataService(CallContext).MergeCart();
        }

        [HttpPost]
        [Route("UpdateAddressinShoppingCart")]
        public OperationResultDTO UpdateAddressinShoppingCart(CheckoutPaymentDTO checkoutPaymentDTO)
        {
            return new AppDataService(CallContext).UpdateAddressinShoppingCart(checkoutPaymentDTO);
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public OperationResultDTO ForgotPassword(string emailID)
        {
            return new AppDataService(CallContext).ForgotPassword(emailID);
        }

        [HttpGet]
        [Route("GetProductDeliveryTypeList")]
        public List<ProductDetailDeliveryOption> GetProductDeliveryTypeList(long skuID, long cultureID)
        {
            return new AppDataService(CallContext).GetProductDeliveryTypeList(skuID, cultureID);
        }

        [HttpGet]
        [Route("GetProductDetailsDescription")]
        public ProductInventoryConfigDTO GetProductDetailsDescription(long skuID, long cultureID)
        {
            return new AppDataService(CallContext).GetProductDetailsDescription(skuID, cultureID);
        }

        [HttpGet]
        [Route("GetCategoryByCode")]
        public CategoryDTO GetCategoryByCode(string categoryCode)
        {
            return new AppDataService(CallContext).GetCategoryByCode(categoryCode);
        }

        [HttpPost]
        [Route("AddNewsletterSubscription")]
        public OperationResultDTO AddNewsletterSubscription(Eduegate.Services.Contracts.NewsletterSubscriptionDTO newsletterSubscriptionDTO)
        {
            return new AppDataService(CallContext).AddNewsletterSubscription(newsletterSubscriptionDTO);
        }

        [HttpGet]
        [Route("GetOrderHistoryCount")]
        public int GetOrderHistoryCount()
        {
            return new AppDataService(CallContext).GetOrderHistoryCount();
        }

        [HttpGet]
        [Route("GetOrderHistoryItemDetail")]
        public ActionResult GetOrderHistoryItemDetail(long headID)
        {
            return Ok(new AppDataService(CallContext).GetOrderHistoryItemDetail(headID));
        }

        [HttpPost]
        [Route("ResendMail")]
        public OperationResultDTO ResendMail(long headID)
        {
            return new AppDataService(CallContext).ResendMail(headID);
        }

        [HttpGet]
        [Route("GetProductMultiPriceDetails")]
        public List<ProductMultiPriceDTO> GetProductMultiPriceDetails(long skuID)
        {
            return new AppDataService(CallContext).GetProductMultiPriceDetails(skuID);
        }

        [HttpGet]
        [Route("GetProductQtyDiscountDetails")]
        public List<ProductQuantityDiscountDTO> GetProductQtyDiscountDetails(long skuID)
        {
            return new AppDataService(CallContext).GetProductQtyDiscountDetails(skuID);
        }

        [HttpGet]
        [Route("GetBlinkKeywordsDictionary")]
        public List<SearchKeywordsDictionaryDTO> GetBlinkKeywordsDictionary(string searchtext, string lng)
        {
            return new AppDataService(CallContext).GetBlinkKeywordsDictionary(searchtext, lng);
        }

        [HttpGet]
        [Route("GetActiveDealProducts")]
        public List<ProductSKUDetailDTO> GetActiveDealProducts(BoilerPlateDTO boilerPlateDTO)
        {
            return new AppDataService(CallContext).GetActiveDealProducts(boilerPlateDTO);
        }

        [HttpPost]
        [Route("ValidationBeforePayment")]
        public OperationResultDTO ValidationBeforePayment(CheckoutPaymentDTO checkoutPaymentDTO)
        {
            return new AppDataService(CallContext).ValidationBeforePayment(checkoutPaymentDTO);
        }

        [HttpGet]
        [Route("IsVoucherPayment")]
        public bool IsVoucherPayment(CheckoutPaymentDTO checkoutPaymentDTO)
        {
            return new AppDataService(CallContext).IsVoucherPayment(checkoutPaymentDTO);
        }

        [HttpGet]
        [Route("GetPaymentMethods")]
        public List<PaymentMethodDTO> GetPaymentMethods()
        {
            return new AppDataService(CallContext).GetPaymentMethods();
        }

        [HttpPost]
        [Route("PaymentFailure")]
        public Eduegate.Services.Contracts.Payments.PaymentDTO PaymentFailure(CheckoutPaymentDTO checkoutPaymentDTO)
        {
            return new AppDataService(CallContext).PaymentFailure(checkoutPaymentDTO);
        }

        [HttpPost]
        [Route("ConfirmOnlineOrder")]
        public ActionResult ConfirmOnlineOrder(CheckoutPaymentDTO checkoutPaymentDTO)
        {
            return Ok(new AppDataService(CallContext).ConfirmOnlineOrder(checkoutPaymentDTO));
        }

        //[HttpPost]
        //[Route("SetKnetGateway")]
        //public string SetKnetGateway(string emailID, string customerID, string amount, string initiatedIP, string paymentGateway, string companyID, string loginID, string GUID, string currencyCode, string userId, string apiKey, string languageCode, string siteID,string udf5)
        //{
        //    return new AppDataService(CallContext).SetKnetGateway(emailID, customerID, amount, initiatedIP, paymentGateway, companyID, loginID, GUID, currencyCode, userId, apiKey, languageCode, siteID, udf5);
        //}

        [HttpPost]
        [Route("SetKnetGateway")]
        public string SetKnetGateway(string emailID, string customerID, string amount, string initiatedIP, string paymentGateway, string companyID, string loginID, string GUID, string currencyCode, string userId, string apiKey, string languageCode, string siteID, string udf5)
        {
            return new AppDataService(CallContext).SetKnetGateway(emailID, customerID, amount, initiatedIP, paymentGateway, companyID, loginID, GUID, currencyCode, userId, apiKey, languageCode, siteID, udf5);
        }

        [HttpPost]
        [Route("CheckAppVersion")]
        public bool CheckAppVersion(string mobileAppVersion)
        {
            return new AppDataService(CallContext).CheckAppVersion(mobileAppVersion);
        }

        [HttpGet]
        [Route("GetMenuDetailsByType")]
        public List<Eduegate.Services.Contracts.MenuLinks.MenuDTO> GetMenuDetailsByType()
        {
            return new AppDataService(CallContext).GetMenuDetailsByType();
        }

        [HttpGet]
        [Route("GetServices")]
        public List<ServiceDTO> GetServices()
        {
            return new AppDataService(CallContext).GetServices();
        }

        [HttpGet]
        [Route("GetSaloons")]
        public List<SaloonDTO> GetSaloons()
        {
            return new AppDataService(CallContext).GetSaloons();
        }

        [HttpGet]
        [Route("GetEmployeesByRoles")]
        public List<EmployeeDTO> GetEmployeesByRoles(int roleID)
        {
            return new AppDataService(CallContext).GetEmployeesByRoles(roleID);
        }

        [HttpGet]
        [Route("GetJobOpenings")]
        public List<JobOpeningDTO> GetJobOpenings(string filter)
        {
            return new AppDataService(CallContext).GetJobOpenings(filter);
        }

        [HttpGet]
        [Route("GetJobOpening")]
        public JobOpeningDTO GetJobOpening(string jobID)
        {
            return new AppDataService(CallContext).GetJobOpening(jobID);
        }

        [HttpGet]
        [Route("GetBranches")]
        public List<BranchDTO> GetBranches()
        {
            return new AppDataService(CallContext).GetBranches();
        }

        [HttpPost]
        [Route("UpdateUserDefaultBranch")]
        public bool UpdateUserDefaultBranch(long branchID)
        {
            return new AppDataService(CallContext).UpdateUserDefaultBranch(branchID);
        }

        [HttpGet]
        [Route("GetCartQuantity")]
        public List<CartItemQuantityDTO> GetCartQuantity()
        {
            return new AppDataService(CallContext).GetCartQuantity();
        }

        [HttpPost]
        [Route("GetCategoriesByBoilerplate")]
        public List<CategoryDetailDTO> GetCategoriesByBoilerplate(BoilerPlateDTO boilerPlateDTO = null)
        {
            return new AppDataService(CallContext).GetCategoriesByBoilerplate(boilerPlateDTO);
        }

        [HttpGet]
        [Route("GetAllCategories")]
        public List<FacetsDetail> GetAllCategories(string searchText = "")
        {
            return new AppDataService(CallContext).GetAllCategories(searchText);
        }

        [HttpPost]
        [Route("ValidateContinueCheckOut")]
        public OperationResultDTO ValidateContinueCheckOut()
        {
            return new AppDataService(CallContext).ValidateContinueCheckOut();
        }

        [HttpPost]
        [Route("OnlineStoreAddContact")]
        public long OnlineStoreAddContact(Eduegate.Services.Contracts.ContactDTO contactDTO)
        {
            return new AppDataService(CallContext).OnlineStoreAddContact(contactDTO);
        }

        [HttpGet]
        [Route("GetCustomers")]
        public List<CustomerDTO> GetCustomers(string searchText, int dataSize)
        {
            return new AppDataService(CallContext).GetCustomers(searchText, dataSize);
        }

        [HttpGet]
        [Route("GetAllDeliveryTypes")]
        public List<DeliverySettingDTO> GetAllDeliveryTypes()
        {
            return new AppDataService(CallContext).GetAllDeliveryTypes();
        }

        [HttpGet]
        [Route("GetDeliveryBlockingPeriods")]
        public DeliveryBlockingPeriodDTO GetDeliveryBlockingPeriods()
        {
            return new AppDataService(CallContext).GetDeliveryBlockingPeriods();
        }

        [HttpGet]
        [Route("GetDeliveryTimeSlots")]
        public List<DeliveryTimeSlotDTO> GetDeliveryTimeSlots(long deliveryTypeId, DateTime dateTime)
        {
            return new AppDataService(CallContext).GetDeliveryTimeSlots(deliveryTypeId, dateTime);
        }

        [HttpGet]
        [Route("GetPaymentMethodsforOnlineStore")]
        public List<PaymentMethodDTO> GetPaymentMethodsforOnlineStore()
        {
            return new AppDataService(CallContext).GetPaymentMethodsforOnlineStore();
        }

        [HttpPost]
        [Route("OnlineStoreGenerateOrder")]
        public ActionResult OnlineStoreGenerateOrder(CheckoutPaymentDTO checkoutPaymentDTO)
        {
            return Ok(new AppDataService(CallContext).OnlineStoreGenerateOrder(checkoutPaymentDTO));
        }

        [HttpGet]
        [Route("OnlineStoreGetOrderHistoryDetails")]
        public ActionResult OnlineStoreGetOrderHistoryDetails(int pageSize, int pageNo)
        {
            return Ok(new AppDataService(CallContext).OnlineStoreGetOrderHistoryDetails(pageSize, pageNo));
        }

        [HttpGet]
        [Route("OnlineStoreGetProductSearch")]
        public Eduegate.Services.Contracts.SearchData.SearchResultDTO OnlineStoreGetProductSearch
        (int pageIndex = 1, int pageSize = 50, string searchText = "", string searchVal = "",
        string searchBy = "", string sortBy = "relevance", string pageType = "", bool isCategory = false, short statusID = 2)
        {
            return new AppDataService(CallContext).OnlineStoreGetProductSearch
            (pageIndex, pageSize * 3, searchText, searchVal, searchBy, sortBy, pageType, isCategory, statusID == 0 ? (short)2 : statusID);
        }

        [HttpPost]
        [Route("SaveShoppingCartDetails")]
        public OperationResultDTO SaveShoppingCartDetails(CheckoutPaymentDTO checkoutPaymentDTO)
        {
            return new AppDataService(CallContext).SaveShoppingCartDetails(checkoutPaymentDTO);
        }

        [HttpPost]
        [Route("CheckLoginforCustomer")]
        public OperationResultDTO CheckLoginforCustomer()
        {
            var userDTO = new UserDTO();
            return new AppDataService(CallContext).CheckLoginforCustomer(userDTO);
        }

        [HttpGet]
        [Route("ReOrder")]
        public OperationResultDTO ReOrder(long headID)
        {
            return new AppDataService(CallContext).ReOrder(headID);
        }

        [HttpPost]
        [Route("UploadContentAsString")]
        public long UploadContentAsString(Services.Contracts.Contents.ContentFileDTO content)
        {
            return new AppDataService(CallContext).UploadContentAsString(content);
        }

        [HttpGet]
        [Route("GetAllergies")]
        public List<KeyValueDTO> GetAllergies()
        {
            return new AppDataService(CallContext).GetAllergies();
        }

        [HttpPost]
        [Route("SaveAllergies")]
        public OperationResultDTO SaveAllergies(long studentID, int allergyID, byte SeverityID)
        {
            return new AppDataService(CallContext).SaveAllergies(studentID, allergyID, SeverityID);
        }

        [HttpGet]
        [Route("GetStudentAllergies")]
        public List<AllergyStudentDTO> GetStudentAllergies()
        {
            return new AppDataService(CallContext).GetStudentAllergies();
        }

        [HttpPost]
        [Route("CheckStudentAllergy")]
        public List<AllergyStudentDTO> CheckStudentAllergy(long studentID, long cartID)
        {
            return new AppDataService(CallContext).CheckStudentAllergy(studentID, cartID);
        }

        [HttpPost]
        [Route("SaveDefaultStudent")]
        public OperationResultDTO SaveDefaultStudent(long studentID)
        {
            return new AppDataService(CallContext).SaveDefaultStudent(studentID);
        }

        [HttpPost]
        [Route("UpdateCartScheduleDate")]
        public OperationResultDTO UpdateCartScheduleDate(CartScheduleDateDTO scheduleDate)
        {
            return new AppDataService(CallContext).UpdateCartScheduleDate(scheduleDate);
        }

        [HttpGet]
        [Route("GetSubscriptionTypes")]
        public List<SubscriptionTypeDTO> GetSubscriptionTypes(long deliveryTypeId, DateTime dateTime)
        {
            return new AppDataService(CallContext).GetSubscriptionTypes(deliveryTypeId, dateTime);
        }

        [HttpPost]
        [Route("SaveSchedule")]
        public OperationResultDTO SaveSchedule(ShoppingCartDTO schedule)
        {
            return new AppDataService(CallContext).SaveSchedule(schedule);
        }

        [HttpGet]
        [Route("GetWeekDays")]
        public List<DaysDTO> GetWeekDays()
        {
            return new AppDataService(CallContext).GetWeekDays();
        }

        [HttpGet]
        [Route("GetAcademicCalenderByDateRange")]
        public long GetAcademicCalenderByDateRange(string startDateString, string endDateString, string daysID)
        {
            return new AppDataService(CallContext).GetAcademicCalenderByDateRange(startDateString, endDateString, daysID);
        }

        [HttpGet]
        [Route("GetSubscriptionOrderHistoryDetails")]
        public ActionResult GetSubscriptionOrderHistoryDetails(int pageSize, int pageNo)
        {
            return Ok(new AppDataService(CallContext).GetSubscriptionOrderHistoryDetails(pageSize, pageNo));
        }

        [HttpGet]
        [Route("GetCanteenNormalOrderHistoryDetails")]
        public ActionResult GetCanteenNormalOrderHistoryDetails(int pageSize, int pageNo)
        {
            return Ok(new AppDataService(CallContext).GetCanteenNormalOrderHistoryDetails(pageSize, pageNo));
        }

        [HttpGet]
        [Route("GetSettingValueByKey")]
        public string GetSettingValueByKey(string settingKey)
        {
            return new AppDataService(CallContext).GetSettingValueByKey(settingKey);
        }

    }
}
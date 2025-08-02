using Eduegate.Domain;
using Eduegate.Domain.MobileAppWrapper;
using Eduegate.Domain.Payroll;
using Eduegate.Domain.Saloon;
using Eduegate.Framework;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Checkout;
using Eduegate.Services.Contracts.HR;
using Eduegate.Services.Contracts.MobileAppWrapper;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Framework.Contracts.Common.PageRender;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Services.Contracts.ProductDetail;
using Eduegate.Services.Contracts.Salon;
using Eduegate.Services.Contracts.SearchData;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Distributions;
using Eduegate.Domain.Mappers.School.Fees;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Domain.Content;
using Eduegate.Services.Contracts.School.Inventory;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Domain.Mappers.ShoppingCartMapper;
using System.Globalization;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Domain.Report;

namespace Eduegate.Services.MobileAppWrapper
{
    public class AppDataService : BaseService, IAppDataService
    {
        public AppDataService(CallContext context)
        {
            CallContext = context;
        }

        public List<Contracts.BannerMasterDTO> GetHomeBanners()
        {
            return new AppDataBL(CallContext).GetHomeBanners();
        }

        public BannerMasterDTO GetSingleHomeBanner(BoilerPlateDTO boilerplateDTO)
        {
            return new AppDataBL(CallContext).GetSingleHomePageBanner(boilerplateDTO);
        }

        //public Eduegate.Services.Contracts.SearchData.SearchResultDTO GetProducts(Contracts.Enums.WidgetTypes widgetType)
        //{
        //    return new AppDataBL(CallContext).GetProducts(widgetType);
        //}

        public List<ProductSKUDetailDTO> GetProducts(BoilerPlateDTO boilerPlateDTO)
        {
            return new AppDataBL(CallContext).GetProducts(boilerPlateDTO);
        }

        public List<ProductSKUDetailDTO> GetProductsEmarsys(Contracts.Enums.WidgetTypes widgetType, string productIDs, long cultureID)
        {
            return new AppDataBL(this.CallContext).GetProductsEmarsys(widgetType, productIDs, cultureID);
        }

        public List<Eduegate.Services.Contracts.Catalog.CategoryDTO> GetCategories()
        {
            return new AppDataBL(CallContext).GetCategories();
        }

        public List<KnowHowOptionDTO> GetKnowHowOptions()
        {
            return new AppDataBL(CallContext).GetKnowHowOptions();
        }

        public List<CountryDTO> GetCountries()
        {
            return new AppDataBL(CallContext).GetCountries();
        }

        public OperationResultDTO Register(UserDTO Register)
        {
            return new AppDataBL(CallContext).Register(Register);
        }

        public OperationResultDTO Login(UserDTO user)
        {
            return new AppDataBL(CallContext).Login(user);
        }

        public UserDTO GetUserDetails()
        {
            return new AppDataBL(CallContext).GetUserDetails();
        }

        public UserDTO LogOut()
        {
            return new AppDataBL(CallContext).LogOut();
        }

        public int GetShoppingCartCount()
        {
            return new AppDataBL(CallContext).GetShoppingCartCount();
        }

        public Eduegate.Services.Contracts.SearchData.SearchResultDTO GetProductsfromSolr(int pageIndex, int pageSize, string searchText, string searchVal, string searchBy, string sortBy, string pageType)
        {
            return new AppDataBL(CallContext).GetProductsfromSolr(pageIndex, pageSize, searchText, searchVal, searchBy, sortBy, pageType);
        }

        public Eduegate.Services.Contracts.SearchData.SearchResultDTO GetProductSearch
            (int pageIndex = 1, int pageSize = 50, string searchText = "", string searchVal = "",
            string searchBy = "", string sortBy = "relevance", string pageType = "", bool isCategory = false)
        {
            return new AppDataBL(CallContext).GetProductSearch
                (pageIndex, pageSize, searchText, searchVal, searchBy, sortBy, pageType, isCategory);
        }

        public OperationResultDTO AddToCart(CartProductDTO cartDTO)
        {
            return new AppDataBL(this.CallContext).AddToCart(cartDTO);
        }

        public CartDTO GetCartDetails()
        {
            return new AppDataBL(this.CallContext).GetCartDetails();
        }

        public OperationResultDTO RemoveCartItem(CartProductDTO cartDTO)
        {
            return new AppDataBL(this.CallContext).RemoveCartItem(cartDTO.SKUID);
        }

        public OperationResultDTO UpdateCart(CartProductDTO cartDTO)
        {
            return new AppDataBL(this.CallContext).UpdateCart(cartDTO);
        }

        public long GetSaveForLaterCount()
        {
            return new AppDataBL(this.CallContext).GetSaveForLaterCount();
        }

        public List<WishListDTO> GetSaveForLater()
        {
            return new AppDataBL(this.CallContext).GetSaveForLater();
        }

        public OperationResultDTO AddSaveForLater(long skuID)
        {
            return new AppDataBL(this.CallContext).AddSaveForLater(skuID);
        }

        public OperationResultDTO RemoveSaveForLater(long skuID)
        {
            return new AppDataBL(this.CallContext).RemoveSaveForLater(skuID);
        }

        public OperationResultDTO UpdateCartDelivery(CartProductDTO cartDTO)
        {
            return new AppDataBL(this.CallContext).UpdateCartDelivery(cartDTO);
        }

        public ContactDTO GetLastShippingAddress(long addressID)
        {
            return new AppDataBL(this.CallContext).GetLastShippingAddress(addressID);
        }

        public ContactDTO GetBillingAddress()
        {
            return new AppDataBL(this.CallContext).GetBillingAddress();
        }

        public ContactDTO GetAddressByContactID(long addressID)
        {
            return new AppDataBL(this.CallContext).GetAddressByContactID(addressID);
        }

        public OperationResultDTO UpdateContact(ContactDTO contactDTO)
        {
            return new AppDataBL(this.CallContext).UpdateContact(contactDTO);
        }

        public List<AreaDTO> GetAreaByCountryID(string countryID)
        {
            return new AppDataBL(this.CallContext).GetAreaByCountryID(string.IsNullOrEmpty(countryID) ? (int?)null : int.Parse(countryID));
        }

        public List<CityDTO> GetCityByCountryID(string countryID)
        {
            return new AppDataBL(this.CallContext).GetCityByCountryID(string.IsNullOrEmpty(countryID) ? (int?)null : int.Parse(countryID));
        }

        public List<AreaDTO> GetAreaByCityID(string cityID)
        {
            return new AppDataBL(this.CallContext).GetAreaByCityID(string.IsNullOrEmpty(cityID) ? (int?)null : int.Parse(cityID));
        }

        public long AddContact(ContactDTO contactDTO)
        {
            return new AppDataBL(this.CallContext).AddContact(contactDTO);
        }

        public List<ContactDTO> GetShippingAddressContacts()
        {
            return new AppDataBL(this.CallContext).GetShippingAddressContacts();
        }

        public OperationResultDTO RemoveContact(long contactID)
        {
            return new AppDataBL(this.CallContext).RemoveContact(contactID);
        }

        public VoucherDTO ValidateVoucher(string voucherNo)
        {
            return new AppDataBL(this.CallContext).ValidateVoucher(voucherNo);
        }

        public long DefaultPaymentOption()
        {
            return new AppDataBL(this.CallContext).DefaultPaymentOption();
        }

        public CheckoutPaymentMobileDTO SaveWebsiteOrder(CheckoutPaymentDTO checkoutPaymentDTO)
        {
            return new AppDataBL(this.CallContext).SaveWebsiteOrder(checkoutPaymentDTO);
        }

        public List<Contracts.OrderHistory.OrderHistoryDTO> GetOrderHistoryDetails(int pageSize, int pageNo)
        {
            return new AppDataBL(this.CallContext).GetOrderHistoryDetails(pageSize, pageNo);
        }

        public UserDTO GetPersonalSettings()
        {
            return new AppDataBL(this.CallContext).GetPersonalSettings();
        }

        public OperationResultDTO SavePersonalSettings(UserDTO userDTO)
        {
            return new AppDataBL(this.CallContext).SavePersonalSettings(userDTO);
        }

        public OperationResultDTO UpdatePassword(ChangePasswordDTO changePasswordDTO)
        {
            return new AppDataBL(this.CallContext).UpdatePassword(changePasswordDTO);
        }

        public PageDTO GetPageInfo(long pageID, string parameter)
        {
            if (this.CallContext == null)
            {
                this.CallContext = new CallContext() { SiteID = "1" };
            }

            switch (this.CallContext.SiteID)
            {
                case "1":
                    switch (pageID)
                    {
                        case 15:
                            pageID = 46;
                            break;
                        case 16:
                            pageID = 37;
                            break;
                        case 20:
                            pageID = 41;
                            break;
                        case 42:
                            pageID = 43;
                            break;
                        default:
                            break;
                    }
                    break;
                case "2":
                    switch (pageID)
                    {
                        case 15:
                            pageID = 46;
                            break;
                        case 16:
                            pageID = 37;
                            break;
                        case 20:
                            pageID = 41;
                            break;
                        case 42:
                            pageID = 43;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            //this.CallContext.CompanyID = 2; // no page defined for company 1
            return new AppDataBL(this.CallContext).GetPageInfo(pageID, parameter);
        }

        public List<CategoryImageMapDTO> GetCategoryBanner(BoilerPlateDTO boilerPlateDTO)
        {
            return new AppDataBL(this.CallContext).GetCategoryBanner(boilerPlateDTO);
        }

        public Eduegate.Services.Contracts.SearchData.SearchResultDTO CategoryProductLists(CategoryProductListDTO categoryProductListDTO)
        {
            return new AppDataBL(this.CallContext).CategoryProductLists(categoryProductListDTO.categoryCode, categoryProductListDTO.boilerPlateDTO);
        }

        public List<CategoryDTO> GetSubCategories(long categoryID)
        {
            return new AppDataBL(this.CallContext).GetSubCategories(categoryID);
        }

        public CategoryDTO CategoryDetails(long categoryID)
        {
            return new AppDataBL(this.CallContext).CategoryDetails(categoryID);
        }

        public List<ProductImageMapDTO> GetProductDetailImages(long skuID)
        {
            return new AppDataBL(this.CallContext).GetProductDetailImages(skuID);
        }

        public ProductSKUDetailDTO GetProductDetail(long skuID, long cultureID)
        {
            return new AppDataBL(this.CallContext).GetProductDetail(skuID, cultureID);
        }

        public List<ProductDetailKeyFeatureDTO> GetProductFeatures(long skuID)
        {
            return new AppDataBL(this.CallContext).GetProductFeatures(skuID);
        }

        public List<ProductSKUVariantDTO> GetProductVariant(long skuID, long cultureID)
        {
            return new AppDataBL(this.CallContext).GetProductVariant(skuID, cultureID);
        }

        public string GenerateApiKey(string uuid, string version)
        {
            return new AppDataBL(this.CallContext).GenerateApiKey(uuid, version);
        }

        public string GenerateGUID()
        {
            return new AppDataBL(this.CallContext).GenerateGUID();
        }

        public bool MergeCart()
        {
            return new AppDataBL(this.CallContext).MergeCart();
        }

        public OperationResultDTO UpdateAddressinShoppingCart(CheckoutPaymentDTO checkoutPaymentDTO)
        {
            return new AppDataBL(this.CallContext).UpdateAddressinShoppingCart(checkoutPaymentDTO);
        }

        public OperationResultDTO ForgotPassword(string emailID)
        {
            return new AppDataBL(this.CallContext).ForgotPassword(emailID);
        }

        public List<ProductDetailDeliveryOption> GetProductDeliveryTypeList(long skuID, long cultureID)
        {
            return new AppDataBL(this.CallContext).GetProductDeliveryTypeList(skuID, cultureID);
        }

        public ProductInventoryConfigDTO GetProductDetailsDescription(long skuID, long cultureID)
        {
            return new AppDataBL(this.CallContext).GetProductDetailsDescription(skuID, cultureID);
        }

        public CategoryDTO GetCategoryByCode(string categoryCode)
        {
            return new AppDataBL(this.CallContext).GetCategoryByCode(categoryCode);
        }

        public OperationResultDTO AddNewsletterSubscription(Contracts.NewsletterSubscriptionDTO newsletterSubscriptionDTO)
        {
            return new AppDataBL(this.CallContext).AddNewsletterSubscription(newsletterSubscriptionDTO);
        }

        public int GetOrderHistoryCount()
        {
            return new AppDataBL(this.CallContext).GetOrderHistoryCount();
        }

        public List<Contracts.OrderHistory.OrderHistoryDTO> GetOrderHistoryItemDetail(long headID)
        {
            return new AppDataBL(this.CallContext).GetOrderHistoryItemDetail(headID);
        }

        public OperationResultDTO ResendMail(long headID)
        {
            return new AppDataBL(this.CallContext).ResendMail(headID);
        }

        public List<ProductMultiPriceDTO> GetProductMultiPriceDetails(long skuID)
        {
            return new AppDataBL(this.CallContext).GetProductMultiPriceDetails(skuID);
        }

        public List<ProductQuantityDiscountDTO> GetProductQtyDiscountDetails(long skuID)
        {
            return new AppDataBL(this.CallContext).GetProductQtyDiscountDetails(skuID);
        }

        public List<SearchKeywordsDictionaryDTO> GetBlinkKeywordsDictionary(string searchtext, string lng)
        {
            return new AppDataBL(this.CallContext).GetBlinkKeywordsDictionary(searchtext, lng);
        }

        public List<ProductSKUDetailDTO> GetActiveDealProducts(BoilerPlateDTO boilerPlateDTO)
        {
            return new AppDataBL(this.CallContext).GetActiveDealProducts(boilerPlateDTO);
        }

        public OperationResultDTO ValidationBeforePayment(CheckoutPaymentDTO checkoutPaymentDTO)
        {
            return new AppDataBL(this.CallContext).ValidationBeforePayment(checkoutPaymentDTO);
        }

        public bool IsVoucherPayment(CheckoutPaymentDTO checkoutPaymentDTO)
        {
            return new AppDataBL(this.CallContext).IsVoucherPayment(checkoutPaymentDTO);
        }

        public List<PaymentMethodDTO> GetPaymentMethods()
        {
            return new AppDataBL(this.CallContext).GetPaymentMethods();
        }

        public Eduegate.Services.Contracts.Payments.PaymentDTO PaymentFailure(CheckoutPaymentDTO checkoutPaymentDTO)
        {
            return new AppDataBL(this.CallContext).PaymentFailure(checkoutPaymentDTO);
        }

        public CheckoutPaymentMobileDTO ConfirmOnlineOrder(CheckoutPaymentDTO checkoutPaymentDTO)
        {
            return new AppDataBL(this.CallContext).ConfirmOnlineOrder(checkoutPaymentDTO);
        }

        //public string SetKnetGateway(string emailID, string customerID, string amount, string initiatedIP, string paymentGateway, string companyID, string loginID, string GUID, string currencyCode, string userId, string apiKey, string languageCode, string siteID,string udf5)
        //{
        //    var object1 = new AppDataBL(this.CallContext).SetKnetGateway(emailID, customerID, amount, initiatedIP, paymentGateway, companyID, loginID, GUID, currencyCode, userId, apiKey, languageCode, siteID, udf5);
        //    return object1.RedirectUrl;
        //}

        public OperationResultDTO AddNewsletterSubscription(Contracts.MobileAppWrapper.NewsletterSubscriptionDTO newsletterSubscriptionDTO)
        {
            throw new NotImplementedException();
        }

        public string SetKnetGateway(string emailID, string customerID, string amount, string initiatedIP, string paymentGateway, string companyID, string loginID, string GUID, string currencyCode, string userId, string apiKey, string languageCode, string siteID, string udf5)
        {
            throw new NotImplementedException();
        }

        public bool CheckAppVersion(string mobileAppVersion)
        {
            return true;
        }

        public List<Eduegate.Services.Contracts.MenuLinks.MenuDTO> GetMenuDetailsByType()
        {
            return new Domain.MenuBL(CallContext).GetMenuDetailsByType(Contracts.Enums.MenuLinkTypes.TopNavigationOnline,
                CallContext != null && !string.IsNullOrEmpty(CallContext.SiteID) ? long.Parse(CallContext.SiteID) : 0);
        }

        public List<ServiceDTO> GetServices()
        {
            return new SaloonBL(CallContext).GetServices();
        }

        public List<SaloonDTO> GetSaloons()
        {
            return new SaloonBL(CallContext).GetSaloons();
        }

        public List<EmployeeDTO> GetEmployeesByRoles(int roleID)
        {
            return new EmployeeBL(CallContext).GetEmployeesByRoles(roleID);
        }

        //public List<JobOpeningDTO> GetJobOpenings(string filter)
        //{
        //    return new EmployeeBL(CallContext).GetJobOpenings(filter);
        //}

        //public JobOpeningDTO GetJobOpening(string jobID)
        //{
        //    return new EmployeeBL(CallContext).GetJobOpening(jobID);
        //}

        public List<BranchDTO> GetBranches()
        {
            return new ReferenceDataBL(CallContext).GetBranches();
        }

        public bool UpdateUserDefaultBranch(long branchID)
        {
            return new AppDataBL(CallContext).UpdateUserDefaultBranch(branchID);
        }

        public List<CartItemQuantityDTO> GetCartQuantity()
        {
            return new AppDataBL(this.CallContext).GetCartQuantity();
        }

        public List<CategoryDetailDTO> GetCategoriesByBoilerplate(BoilerPlateDTO boilerPlateDTO)
        {
            return new AppDataBL(this.CallContext).GetCategoriesByBoilerplate(boilerPlateDTO);
        }

        public List<FacetsDetail> GetAllCategories(string searchText = "")
        {
            return new AppDataBL(this.CallContext).GetAllCategories(searchText);
        }

        public OperationResultDTO ValidateContinueCheckOut()
        {
            return new AppDataBL(this.CallContext).ValidateContinueCheckOut();
        }

        public long OnlineStoreAddContact(ContactDTO contactDTO)
        {
            return new AppDataBL(this.CallContext).OnlineStoreAddContact(contactDTO);
        }

        public List<CustomerDTO> GetCustomers(string searchText, int dataSize)
        {
            return new CustomerBL(this.CallContext).GetCustomers(searchText, dataSize);
        }

        public List<DeliverySettingDTO> GetAllDeliveryTypes()
        {
            return new AppDataBL(CallContext).GetAllDeliveryTypes();
        }

        public DeliveryBlockingPeriodDTO GetDeliveryBlockingPeriods()
        {
            return new AppDataBL(CallContext).GetDeliveryBlockingPeriods();
        }

        public List<DeliveryTimeSlotDTO> GetDeliveryTimeSlots(long deliveryTypeId, DateTime dateTime)
        {
            return new AppDataBL(CallContext).GetDeliveryTimeSlots(deliveryTypeId, dateTime);
        }

        public List<PaymentMethodDTO> GetPaymentMethodsforOnlineStore()
        {
            return new AppDataBL(CallContext).GetPaymentMethodsforOnlineStore();
        }

        public CheckoutPaymentMobileDTO OnlineStoreGenerateOrder(CheckoutPaymentDTO checkoutPaymentDTO)
        {
            var checkoutData = new AppDataBL(CallContext).OnlineStoreGenerateOrder(checkoutPaymentDTO);

            try
            {
                if (checkoutData.TransactionHeadIds != null && checkoutData.TransactionHeadIds.Count > 0)
                {
                    var studentData = new AppDataBL(CallContext).GetStudentDetailsByHeadID(checkoutData.TransactionHeadIds[0].ToString());

                    var canteenSOPrefix = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CANTEEN_SO_PREFIX");

                    if (checkoutData.TransactionNo.Contains(canteenSOPrefix))
                    {
                        //string[] dataList = studentData.Split('#');
                        //var mailID = dataList[1];
                        //var admissionNo = dataList[0];
                        //var schoolID = Convert.ToByte(dataList[2]);

                        var mailSent = CartMapper.Mapper(CallContext).MailSendAfterCanteenOrderGeneration(studentData, checkoutData.TransactionNo);
                    }
                    else
                    {
                        //Auto due & receipt option from online store
                        var feeData = FeeCollectionMapper.Mapper(CallContext).AutoReceiptAccountTransactionSync(0, checkoutData.TransactionHeadIds[0], 0, 1, 0);

                        var feecollectionID = feeData.Select(x => x.FeeCollectionIID).FirstOrDefault();
                        var feereceiptNo = feeData.Select(x => x.FeeReceiptNo).FirstOrDefault();

                        //string[] dataList = studentData.Split('#');
                        //var mailID = dataList[1];
                        //var admissionNo = dataList[0];
                        //var schoolID = Convert.ToByte(dataList[2]);

                        if (feecollectionID != 0 && !string.IsNullOrEmpty(studentData.ParentEmailID))
                        {
                            var feeCollectionDatas = new List<FeeCollectionDTO>()
                            {
                                new FeeCollectionDTO()
                                {
                                    FeeCollectionIID = feecollectionID,
                                    FeeReceiptNo = feereceiptNo,
                                    EmailID = studentData.ParentEmailID,
                                    AdmissionNo = studentData.AdmissionNumber,
                                    SchoolID = studentData.SchoolID,
                                    TransactionHeadID = checkoutData.TransactionHeadIds[0].ToString(),
                                    IsOnlineStore = true,
                                    PaymentTrackID = checkoutData.TrackID,
                                }
                            };

                            //auto mailing receipt
                            new ReportGenerationBL(CallContext).GenerateFeeReceiptAndSendToMail(feeCollectionDatas, EmailTypes.AutoFeeReceipt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
            }

            return checkoutData;
        }

        public List<Contracts.OrderHistory.OrderHistoryDTO> OnlineStoreGetOrderHistoryDetails(int pageSize, int pageNo)
        {
            return new AppDataBL(CallContext).OnlineStoreGetOrderHistoryDetails(pageSize, pageNo);
        }

        public Eduegate.Services.Contracts.SearchData.SearchResultDTO OnlineStoreGetProductSearch
        (int pageIndex = 1, int pageSize = 50, string searchText = "", string searchVal = "",
        string searchBy = "", string sortBy = "relevance", string pageType = "", bool isCategory = false, short statusID = 2)
        {
            return new AppDataBL(CallContext).OnlineStoreGetProductSearch
            (pageIndex, pageSize * 3, searchText, searchVal, searchBy, sortBy, pageType, isCategory, statusID == 0 ? (short)2 : statusID);
        }

        public OperationResultDTO SaveShoppingCartDetails(CheckoutPaymentDTO checkoutPaymentDTO)
        {
            return new AppDataBL(CallContext).SaveShoppingCartDetails(checkoutPaymentDTO);
        }

        public OperationResultDTO CheckLoginforCustomer(UserDTO userDTO)
        {
            return new AppDataBL(this.CallContext).CheckLoginforCustomer(userDTO);
        }

        public OperationResultDTO ReOrder(long headID)
        {
            return new AppDataBL(this.CallContext).ReOrder(headID);
        }

        public long UploadContentAsString(Services.Contracts.Contents.ContentFileDTO content)
        {
            using (var memoryStream = new MemoryStream())
            {
                var savedContent = new ContentBL(CallContext).SaveFile(new Services.Contracts.Contents.ContentFileDTO()
                {
                    ContentFileName = content.ContentFileName,
                    ContentData = Convert.FromBase64String(content.ContentDataString),
                });
                return savedContent.ContentFileIID;
            }
        }

        public List<KeyValueDTO> GetAllergies()
        {
            return new AppDataBL(this.CallContext).GetAllergies();
        }

        public OperationResultDTO SaveAllergies(long studentID, int allergyID,byte SeverityID)
        {
            return new AppDataBL(this.CallContext).SaveAllergies(studentID, allergyID, SeverityID);
        }

        public List<AllergyStudentDTO> GetStudentAllergies()
        {
            return new AppDataBL(this.CallContext).GetStudentAllergies();
        }

        public List<AllergyStudentDTO> CheckStudentAllergy(long studentID, long cartID)
        {
            return new AppDataBL(this.CallContext).CheckStudentAllergy(studentID, cartID);
        }

        public OperationResultDTO SaveDefaultStudent(long studentID)
        {
            return new AppDataBL(this.CallContext).SaveDefaultStudent(studentID);
        }

        public OperationResultDTO UpdateCartScheduleDate(CartScheduleDateDTO scheduleDate)
        {
            return new AppDataBL(this.CallContext).UpdateCartScheduleDate(scheduleDate);
        }

        public List<SubscriptionTypeDTO> GetSubscriptionTypes(long deliveryTypeId, DateTime dateTime)
        {
            return new AppDataBL(CallContext).GetSubscriptionTypes(deliveryTypeId, dateTime);
        }

        public OperationResultDTO SaveSchedule(ShoppingCartDTO schedule)
        {
            return new AppDataBL(this.CallContext).SaveSchedule(schedule);
        }

        public List<DaysDTO> GetWeekDays()
        {
            return new AppDataBL(CallContext).GetWeekDays();
        }

        public long GetAcademicCalenderByDateRange(string startDateString, string endDateString, string daysID)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            DateTime startDate = DateTime.ParseExact(startDateString, dateFormat, CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(endDateString, dateFormat, CultureInfo.InvariantCulture);

            return new AppDataBL(CallContext).GetAcademicCalenderByDateRange(startDate, endDate, daysID);
        }

        public List<Contracts.OrderHistory.OrderHistoryDTO> GetSubscriptionOrderHistoryDetails(int pageSize, int pageNo)
        {
            return new AppDataBL(CallContext).GetSubscriptionOrderHistoryDetails(pageSize, pageNo);
        }

        public List<Contracts.OrderHistory.OrderHistoryDTO> GetCanteenNormalOrderHistoryDetails(int pageSize, int pageNo)
        {
            return new AppDataBL(CallContext).GetCanteenNormalOrderHistoryDetails(pageSize, pageNo);
        }

        public string GetSettingValueByKey(string settingKey)
        {
            return new Domain.Setting.SettingBL(CallContext).GetSettingValue<string>(settingKey);
        }

    }
}
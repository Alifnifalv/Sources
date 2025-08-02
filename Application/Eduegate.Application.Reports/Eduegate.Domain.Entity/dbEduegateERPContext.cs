using Eduegate.Domain.Entity.Communications;
using Eduegate.Domain.Entity.Models.HR;
using Eduegate.Domain.Entity.Models.Inventory;
using Eduegate.Domain.Entity.Models.Mapping;
using Eduegate.Domain.Entity.Models.Mapping.Inventory;
using Eduegate.Domain.Entity.Models.Mapping.Mutual;
using Eduegate.Domain.Entity.Models.Mutual;
using Eduegate.Domain.Entity.Models.Settings;
using Eduegate.Domain.Entity.Models.ValueObjects;
using Eduegate.Domain.Entity.Models.Workflows;
using System.Data.Entity;

namespace Eduegate.Domain.Entity.Models
{
    public partial class dbEduegateERPContext : DbContext
    {
        static dbEduegateERPContext()
        {
            Database.SetInitializer<dbEduegateERPContext>(null);
        }

        public dbEduegateERPContext() : base("Name=dbEduegateERPContext")
        {
        }

        public virtual DbSet<ViewCultureData> ViewCultureDatas { get; set; }
        public virtual DbSet<ScreenMetadataCultureData> ScreenMetadataCultureDatas { get; set; }
        public virtual DbSet<ViewColumnCultureData> ViewColumnCultureDatas { get; set; }
        public virtual DbSet<FilterColumnCultureData> FilterColumnCultureDatas { get; set; }

        public virtual DbSet<LessonPlan> LessonPlan { get; set; }
        public virtual DbSet<LessonPlanStatus> LessonPlanStatus { get; set; }
        public virtual DbSet<MarkRegister> MarkRegister { get; set; }
        public virtual DbSet<MarkEntryStatus> MarkEntryStatus { get; set; }
        public virtual DbSet<Assignment> Assignment { get; set; }
        public virtual DbSet<AssignmentStatus> AssignmentStatus { get; set; }
        public virtual DbSet<StudentAssignmentMap> StudentAssignmentMaps { get; set; }
        public virtual DbSet<ProductSKURackMap> ProductSKURackMaps { get; set; }

        public virtual DbSet<Rack> Racks { get; set; }
        public virtual DbSet<Sequence> Sequences { get; set; }

        public virtual DbSet<CustomerCard> CustomerCards { get; set; }
        public virtual DbSet<CardType> CardTypes { get; set; }

        public virtual DbSet<WorkflowLogMapRuleApproverMap> WorkflowLogMapRuleApproverMaps { get; set; }
        public virtual DbSet<WorkflowLogMapRuleMap> WorkflowLogMapRuleMaps { get; set; }
        public virtual DbSet<WorkflowLogMap> WorkflowLogMaps { get; set; }

        public virtual DbSet<TextTransformType> TextTransformTypes { get; set; }



        public virtual DbSet<ShareHolder> ShareHolders { get; set; }

        public DbSet<BranchCultureData> BranchCultureDatas { get; set; }
        public virtual DbSet<GeoLocationLog> GeoLocationLogs { get; set; }

        public virtual DbSet<UserSetting> UserSettings { get; set; }
        public virtual DbSet<ScreenField> ScreenFields { get; set; }
        public virtual DbSet<ScreenFieldSetting> ScreenFieldSettings { get; set; }
        public virtual DbSet<UserScreenFieldSetting> UserScreenFieldSettings { get; set; }

        public virtual DbSet<Condition> Conditions { get; set; }

        public virtual DbSet<WorkflowCondition> WorkflowConditions { get; set; }
        public virtual DbSet<WorkflowFiled> WorkflowFileds { get; set; }
        public virtual DbSet<WorkflowRuleApprover> WorkflowRuleApprovers { get; set; }
        public virtual DbSet<WorkflowRuleCondition> WorkflowRuleConditions { get; set; }
        public virtual DbSet<WorkflowRule> WorkflowRules { get; set; }
        public virtual DbSet<Workflow> Workflows { get; set; }
        public virtual DbSet<WorkflowStatus> WorkflowStatuses { get; set; }
        public virtual DbSet<WorkflowTransactionHeadMap> WorkflowTransactionHeadMaps { get; set; }
        public virtual DbSet<WorkflowTransactionHeadRuleMap> WorkflowTransactionHeadRuleMaps { get; set; }
        public virtual DbSet<WorkflowTransactionRuleApproverMap> WorkflowTransactionRuleApproverMaps { get; set; }
        public virtual DbSet<WorkflowType> WorkflowTypes { get; set; }

        public virtual DbSet<ChartMetadata> ChartMetadatas { get; set; }

        public DbSet<ViewAction> ViewActions { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<TaxStatus> TaxStatuses { get; set; }
        public DbSet<TaxTemplate> TaxTemplates { get; set; }
        public DbSet<TaxTransaction> TaxTransactions { get; set; }
        public DbSet<AccountTaxTransaction> AccountTaxTransactions { get; set; }
        public DbSet<TaxType> TaxTypes { get; set; }
        public DbSet<TaxTemplateItem> TaxTemplateItems { get; set; }

        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<ScreenLookupMap> ScreenLookupMaps { get; set; }
        public DbSet<ScreenMetadata> ScreenMetadatas { get; set; }
        public DbSet<ScreenShortCut> ScreenShortCuts { get; set; }
        public DbSet<Lookup> Lookups { get; set; }

        public DbSet<SMSNotificationData> SMSNotificationDatas { get; set; }
        public DbSet<SMSNotificationType> SMSNotificationTypes { get; set; }

        public DbSet<PaymentMethodSiteMap> PaymentMethodSiteMaps { get; set; }
        public DbSet<SKUPaymentMethodExceptionMaps> SKUPaymentMethodExceptionMap { get; set; }
        public DbSet<BranchDocumentTypeMap> BranchDocumentTypeMaps { get; set; }
        public DbSet<AccountTransaction> AccountTransactions { get; set; }
        public DbSet<CustomerAccountMap> CustomerAccountMaps { get; set; }
        public DbSet<SupplierAccountMap> SupplierAccountMaps { get; set; }
        public DbSet<TransactionHeadAccountMap> TransactionHeadAccountMaps { get; set; }
        public DbSet<CategoryTagMap> CategoryTagMaps { get; set; }
        public DbSet<BrandTagMap> BrandTagMaps { get; set; }
        public DbSet<BrandTag> BrandTags { get; set; }
        public DbSet<CategoryTag> CategoryTags { get; set; }
        public DbSet<ProductSKUTagMap> ProductSKUTagMaps { get; set; }
        public DbSet<ProductSKUTag> ProductSKUTags { get; set; }
        public DbSet<ProductTagMap> ProductTagMaps { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<BrandImageMap> BrandImageMaps { get; set; }
        public DbSet<AssetCategory> AssetCategories { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetTransactionDetail> AssetTransactionDetails { get; set; }
        public DbSet<AssetTransactionHead> AssetTransactionHeads { get; set; }
        public DbSet<DeliveryTypeAllowedAreaMap> DeliveryTypeAllowedAreaMaps { get; set; }
        public DbSet<DeliveryTypeAllowedCountryMap> DeliveryTypeAllowedCountryMaps { get; set; }
        public DbSet<DeliveryTypes1> DeliveryTypes1 { get; set; }
        public DbSet<DeliveryTypeStatus> DeliveryTypeStatuses { get; set; }
        public DbSet<DeliveryTypeTimeSlotMap> DeliveryTypeTimeSlotMaps { get; set; }
        public DbSet<ProductDeliveryTypeMap> ProductDeliveryTypeMaps { get; set; }
        public DbSet<DeliveryTypeAllowedZoneMap> DeliveryTypeAllowedZoneMaps { get; set; }

        public DbSet<EntityScheduler> EntitySchedulers { get; set; }
        public DbSet<SchedulerEntityType> SchedulerEntityTypes { get; set; }
        public DbSet<SchedulerType> SchedulerTypes { get; set; }
        public DbSet<DeliveryTypeMaster> DeliveryTypeMasters { get; set; }
        public DbSet<TimeSlotMaster> TimeSlotMasters { get; set; }
        public DbSet<TimeSlotOverRider> TimeSlotOverRiders { get; set; }
        public DbSet<AddressMaster> AddressMasters { get; set; }
        public DbSet<AdminTaskLog> AdminTaskLogs { get; set; }
        public DbSet<ArabicReportStatusLog> ArabicReportStatusLogs { get; set; }
        public DbSet<AreaMaster> AreaMasters { get; set; }
        public DbSet<BannerLog> BannerLogs { get; set; }
        public DbSet<BannerMaster> BannerMasters { get; set; }
        public DbSet<BannerMasterEmail> BannerMasterEmails { get; set; }
        public DbSet<BannerMasterPosition> BannerMasterPositions { get; set; }
        public DbSet<BannerPage> BannerPages { get; set; }
        public DbSet<BannerPage1> BannerPages1 { get; set; }
        public DbSet<BannerSession> BannerSessions { get; set; }
        public DbSet<BannerSessionNew> BannerSessionNews { get; set; }
        public DbSet<BannerSideAngle> BannerSideAngles { get; set; }
        public DbSet<BlinkLocationMaster> BlinkLocationMasters { get; set; }
        public DbSet<BlinkPoGrnDetail> BlinkPoGrnDetails { get; set; }
        public DbSet<BlinkPoGrnLog> BlinkPoGrnLogs { get; set; }
        public DbSet<BlinkPoGrnMaster> BlinkPoGrnMasters { get; set; }
        public DbSet<BlinkPoOrderDetail> BlinkPoOrderDetails { get; set; }
        public DbSet<BlinkPoOrderMaster> BlinkPoOrderMasters { get; set; }
        public DbSet<BlinkPoOrderMasterLog> BlinkPoOrderMasterLogs { get; set; }
        public DbSet<BlinkUsaBannerMaster> BlinkUsaBannerMasters { get; set; }
        public DbSet<BlinkVendorMaster> BlinkVendorMasters { get; set; }
        public DbSet<BrandMaster> BrandMasters { get; set; }
        public DbSet<CampaignCustomer> CampaignCustomers { get; set; }
        public DbSet<CampaignMaster> CampaignMasters { get; set; }
        public DbSet<CategoryBlock> CategoryBlocks { get; set; }
        public DbSet<CategoryBlocksBrand> CategoryBlocksBrands { get; set; }
        public DbSet<CategoryBlocksCategory> CategoryBlocksCategories { get; set; }
        public DbSet<CategoryBlocksProduct> CategoryBlocksProducts { get; set; }
        public DbSet<CategoryBrand> CategoryBrands { get; set; }
        public DbSet<CategoryColumn> CategoryColumns { get; set; }
        public DbSet<CategoryFilter> CategoryFilters { get; set; }
        public DbSet<CategoryHomePage> CategoryHomePages { get; set; }
        public DbSet<CategoryMaster> CategoryMasters { get; set; }
        public DbSet<CategoryReportGroup> CategoryReportGroups { get; set; }
        public DbSet<CategoryReportGroupDetail> CategoryReportGroupDetails { get; set; }
        public DbSet<CategorySortDetail> CategorySortDetails { get; set; }
        public DbSet<CategorySortMaster> CategorySortMasters { get; set; }
        public DbSet<CategoryVisitLog> CategoryVisitLogs { get; set; }
        public DbSet<CategoryVisitLogIntl> CategoryVisitLogIntls { get; set; }
        public DbSet<CheckAvailablity> CheckAvailablities { get; set; }
        public DbSet<ColumnGroupCounter> ColumnGroupCounters { get; set; }
        public DbSet<ColumnMaster> ColumnMasters { get; set; }
        public DbSet<ColumnValue> ColumnValues { get; set; }
        public DbSet<CountryGroup> CountryGroups { get; set; }
        public DbSet<CountryMaster> CountryMasters { get; set; }
        //public DbSet<Countries> Countries { get; set; }
        public DbSet<CoutureHomeBannerMaster> CoutureHomeBannerMasters { get; set; }
        public DbSet<CurrencyMaster> CurrencyMasters { get; set; }
        public DbSet<CustomerByUser> CustomerByUsers { get; set; }
        public DbSet<CustomerCategorization> CustomerCategorizations { get; set; }
        public DbSet<CustomerCategorizationProductPrice> CustomerCategorizationProductPrices { get; set; }
        public DbSet<CustomerCategorizationProductPriceIntl> CustomerCategorizationProductPriceIntls { get; set; }
        public DbSet<CustomerLog> CustomerLogs { get; set; }
        public DbSet<CustomerLogVerification> CustomerLogVerifications { get; set; }
        public DbSet<CustomerMaster> CustomerMasters { get; set; }
        public DbSet<CustomerMasterNew> CustomerMasterNews { get; set; }
        public DbSet<CustomerMasterNumberInvalid> CustomerMasterNumberInvalids { get; set; }
        public DbSet<CustomerPageLog> CustomerPageLogs { get; set; }
        public DbSet<CustomerPinSearch> CustomerPinSearches { get; set; }
        public DbSet<CustomerPointsLog> CustomerPointsLogs { get; set; }
        public DbSet<CustomerSellUsed> CustomerSellUseds { get; set; }
        public DbSet<CustomerSession> CustomerSessions { get; set; }
        public DbSet<CustomerSlabMultiprice> CustomerSlabMultiprices { get; set; }
        public DbSet<CustomerSlabMultipriceIntl> CustomerSlabMultipriceIntls { get; set; }
        public DbSet<CustomerSlabMultiPriceProcess> CustomerSlabMultiPriceProcesses { get; set; }
        public DbSet<CustomerSlabMultiPriceProcessIntl> CustomerSlabMultiPriceProcessIntls { get; set; }
        public DbSet<CustomerSupport> CustomerSupports { get; set; }
        public DbSet<CustomerSupportComment> CustomerSupportComments { get; set; }
        public DbSet<DriverMaster> DriverMasters { get; set; }
        public DbSet<DriverViolation> DriverViolations { get; set; }
        public DbSet<EmailTracker> EmailTrackers { get; set; }
        public DbSet<ErrorTrace> ErrorTraces { get; set; }
        public DbSet<ExcelDetail> ExcelDetails { get; set; }
        public DbSet<ExpressDeliverySlot> ExpressDeliverySlots { get; set; }
        public DbSet<ExpressDeliverySlotsHoliday> ExpressDeliverySlotsHolidays { get; set; }
        public DbSet<GiftSlot> GiftSlots { get; set; }
        public DbSet<GiftSlotProduct> GiftSlotProducts { get; set; }
        public DbSet<HomePageLink> HomePageLinks { get; set; }
        public DbSet<HomePageLinksVisit> HomePageLinksVisits { get; set; }
        public DbSet<IntlPoBankAccount> IntlPoBankAccounts { get; set; }
        public DbSet<IntlPoGrnDetail> IntlPoGrnDetails { get; set; }
        public DbSet<IntlPoGrnLog> IntlPoGrnLogs { get; set; }
        public DbSet<IntlPoGrnMaster> IntlPoGrnMasters { get; set; }
        public DbSet<IntlPoOrderDetail> IntlPoOrderDetails { get; set; }
        public DbSet<IntlPoOrderDetailsCancelledLog> IntlPoOrderDetailsCancelledLogs { get; set; }
        public DbSet<IntlPoOrderMaster> IntlPoOrderMasters { get; set; }
        public DbSet<IntlPoOrderMasterLog> IntlPoOrderMasterLogs { get; set; }
        public DbSet<IntlPoOrderMasterPayment> IntlPoOrderMasterPayments { get; set; }
        public DbSet<IntlPoOrderMasterPaymentAdditional> IntlPoOrderMasterPaymentAdditionals { get; set; }
        public DbSet<IntlPoRequest> IntlPoRequests { get; set; }
        public DbSet<IntlPoRequestAction> IntlPoRequestActions { get; set; }
        public DbSet<IntlPoRequestQuantityStatu> IntlPoRequestQuantityStatus { get; set; }
        public DbSet<IntlPoRequestRemark> IntlPoRequestRemarks { get; set; }
        public DbSet<IntlPoRequestStatusLog> IntlPoRequestStatusLogs { get; set; }
        public DbSet<IntlPoShipmentBoxDetail> IntlPoShipmentBoxDetails { get; set; }
        public DbSet<IntlPoShipmentDetail> IntlPoShipmentDetails { get; set; }
        public DbSet<IntlPoShipmentMaster> IntlPoShipmentMasters { get; set; }
        public DbSet<IntlPoShipmentMasterLog> IntlPoShipmentMasterLogs { get; set; }
        public DbSet<IntlPoShipmentPaymentMaster> IntlPoShipmentPaymentMasters { get; set; }
        public DbSet<IntlPoShipmentReceived> IntlPoShipmentReceiveds { get; set; }
        public DbSet<IntlPoShipmentReceivedLog> IntlPoShipmentReceivedLogs { get; set; }
        public DbSet<IntlPoShipperMaster> IntlPoShipperMasters { get; set; }
        public DbSet<IntlPoVendor> IntlPoVendors { get; set; }
        public DbSet<IntlPoWarehouseMaster> IntlPoWarehouseMasters { get; set; }
        public DbSet<IP2Country> IP2Country { get; set; }
        public DbSet<DeliveryCharge> DeliveryCharges { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<JobCardMaster> JobCardMasters { get; set; }
        public DbSet<JobCardMasterLog> JobCardMasterLogs { get; set; }
        public DbSet<JobMaster> JobMasters { get; set; }
        public DbSet<KuwaitNumber> KuwaitNumbers { get; set; }
        public DbSet<KuwaitNumbers2014> KuwaitNumbers2014 { get; set; }
        public DbSet<LoyaltyPointSlabMaster> LoyaltyPointSlabMasters { get; set; }
        public DbSet<MarketingBannerClassification> MarketingBannerClassifications { get; set; }
        public DbSet<MarketingBannerComment> MarketingBannerComments { get; set; }
        public DbSet<MarketingBannerConfig> MarketingBannerConfigs { get; set; }
        public DbSet<MarketingBannerDetail> MarketingBannerDetails { get; set; }
        public DbSet<MarketingBannerLog> MarketingBannerLogs { get; set; }
        public DbSet<MarketingBannerMaster> MarketingBannerMasters { get; set; }
        public DbSet<MarketingEmail> MarketingEmails { get; set; }
        public DbSet<MarketingEmail2015> MarketingEmail2015 { get; set; }
        public DbSet<MarketingEmail2015_v1> MarketingEmail2015_v1 { get; set; }
        public DbSet<MarketingEmail2015Temp> MarketingEmail2015Temp { get; set; }
        public DbSet<MarketingEmaillDetail> MarketingEmaillDetails { get; set; }
        public DbSet<MarketingEmailLog> MarketingEmailLogs { get; set; }
        public DbSet<MarketingEmailMailGun> MarketingEmailMailGuns { get; set; }
        public DbSet<MiniMarketLog> MiniMarketLogs { get; set; }
        public DbSet<ModuleMaster> ModuleMasters { get; set; }
        public DbSet<ModuleMasterErp> ModuleMasterErps { get; set; }
        public DbSet<ModuleMasterErpRight> ModuleMasterErpRights { get; set; }
        public DbSet<ModuleRight> ModuleRights { get; set; }
        public DbSet<Newsletter> Newsletters { get; set; }
        public DbSet<NewsLetterJobEmail> NewsLetterJobEmails { get; set; }
        public DbSet<NewsletterJob> NewsletterJobs { get; set; }
        public DbSet<NewsLetterLoggedMessage> NewsLetterLoggedMessages { get; set; }
        public DbSet<NewsletterSubscriber> NewsletterSubscribers { get; set; }
        public DbSet<NewsLetterTemplate> NewsLetterTemplates { get; set; }
        public DbSet<NewsMaster> NewsMasters { get; set; }
        public DbSet<OfflineCustomerMaster> OfflineCustomerMasters { get; set; }
        public DbSet<OrderAttribute> OrderAttributes { get; set; }
        public DbSet<OrderComment> OrderComments { get; set; }
        public DbSet<OrderDetailsLoyaltyPoint> OrderDetailsLoyaltyPoints { get; set; }
        public DbSet<OrderGiftItem> OrderGiftItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderItemCancel> OrderItemCancels { get; set; }
        public DbSet<OrderItemCancelConfirm> OrderItemCancelConfirms { get; set; }
        public DbSet<OrderItemReturn> OrderItemReturns { get; set; }
        public DbSet<OrderItemReturnConfirm> OrderItemReturnConfirms { get; set; }
        public DbSet<OrderLog> OrderLogs { get; set; }
        public DbSet<OrderLogCod> OrderLogCods { get; set; }
        public DbSet<OrderLogMission> OrderLogMissions { get; set; }
        public DbSet<OrderMaster> OrderMasters { get; set; }
        public DbSet<OrderMasterAddressLog> OrderMasterAddressLogs { get; set; }
        public DbSet<OrderMasterCOD> OrderMasterCODs { get; set; }
        public DbSet<OrderMasterCoutureEmailLog> OrderMasterCoutureEmailLogs { get; set; }
        public DbSet<OrderMasterPurchaseEmailLog> OrderMasterPurchaseEmailLogs { get; set; }
        public DbSet<OrderMissionDetail> OrderMissionDetails { get; set; }
        public DbSet<OrderMissionMaster> OrderMissionMasters { get; set; }
        public DbSet<OrderMissionReturnExchangeLog> OrderMissionReturnExchangeLogs { get; set; }
        public DbSet<OrderMissionReturnExchangeMaster> OrderMissionReturnExchangeMasters { get; set; }
        public DbSet<OrderMissonReturnExchangeDetail> OrderMissonReturnExchangeDetails { get; set; }
        public DbSet<OrderOfflineLog> OrderOfflineLogs { get; set; }
        public DbSet<OrderOfflineMaster> OrderOfflineMasters { get; set; }
        public DbSet<OrderOfflineMissionDetail> OrderOfflineMissionDetails { get; set; }
        public DbSet<OrderOfflineMissionMaster> OrderOfflineMissionMasters { get; set; }
        public DbSet<OrderPayment> OrderPayments { get; set; }
        public DbSet<OrderReturnExchange> OrderReturnExchanges { get; set; }
        public DbSet<OrderSize> OrderSizes { get; set; }
        public DbSet<OrderStatusMaster> OrderStatusMasters { get; set; }
        public DbSet<OrderTransaction> OrderTransactions { get; set; }
        public DbSet<TransactionStatus> TransactionStatuses { get; set; }
        public DbSet<PaymentDetail> PaymentDetails { get; set; }
        public DbSet<PaymentDetailsLog> PaymentDetailsLogs { get; set; }
        public DbSet<PaymentDetailsMasterVisa> PaymentDetailsMasterVisas { get; set; }
        public DbSet<PaymentDetailsPayPal> PaymentDetailsPayPals { get; set; }
        public DbSet<PaymentDetailsTheFort> PaymentDetailsTheForts { get; set; }
        public DbSet<PayPalIPNTran> PayPalIPNTrans { get; set; }
        public DbSet<PoTrackingDetail> PoTrackingDetails { get; set; }
        public DbSet<PoTrackingMaster> PoTrackingMasters { get; set; }
        public DbSet<PoTrackingWorkflow> PoTrackingWorkflows { get; set; }
        public DbSet<ProductBarcodeBatch> ProductBarcodeBatches { get; set; }
        public DbSet<ProductBarcodeBatchLog> ProductBarcodeBatchLogs { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductCoutureNewArrival> ProductCoutureNewArrivals { get; set; }
        public DbSet<ProductDayDeal> ProductDayDeals { get; set; }
        public DbSet<ProductDayDealBlock> ProductDayDealBlocks { get; set; }
        public DbSet<ProductDayDealBlocksProduct> ProductDayDealBlocksProducts { get; set; }
        public DbSet<ProductDayDealLog> ProductDayDealLogs { get; set; }
        public DbSet<ProductDeliveryDaysMaster> ProductDeliveryDaysMasters { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<ProductDigital> ProductDigitals { get; set; }
        public DbSet<ProductDigitalReturn> ProductDigitalReturns { get; set; }
        public DbSet<ProductDigitalRevertLog> ProductDigitalRevertLogs { get; set; }
        public DbSet<ProductDisableQueryBatch> ProductDisableQueryBatches { get; set; }
        public DbSet<ProductDisableQueryBatchLog> ProductDisableQueryBatchLogs { get; set; }
        public DbSet<ProductGallery> ProductGalleries { get; set; }
        public DbSet<ProductHomePage> ProductHomePages { get; set; }
        public DbSet<ProductItunesLog> ProductItunesLogs { get; set; }
        public DbSet<ProductListingVisitLog> ProductListingVisitLogs { get; set; }
        public DbSet<ProductListingVisitLogIntl> ProductListingVisitLogIntls { get; set; }
        public DbSet<ProductLocationBatch> ProductLocationBatches { get; set; }
        public DbSet<ProductLocationBatchLog> ProductLocationBatchLogs { get; set; }
        public DbSet<ProductManager> ProductManagers { get; set; }
        public DbSet<ProductManagerBatch> ProductManagerBatches { get; set; }
        public DbSet<ProductManagerBatchLog> ProductManagerBatchLogs { get; set; }
        public DbSet<ProductManagerEmailLog> ProductManagerEmailLogs { get; set; }
        public DbSet<ProductMaster> ProductMasters { get; set; }
        public DbSet<ProductMasterArabicLog> ProductMasterArabicLogs { get; set; }
        public DbSet<ProductMasterIntl> ProductMasterIntls { get; set; }
        public DbSet<ProductMasterIntlLog> ProductMasterIntlLogs { get; set; }
        public DbSet<ProductMasterLog> ProductMasterLogs { get; set; }
        public DbSet<ProductMasterStockMovement> ProductMasterStockMovements { get; set; }
        public DbSet<ProductMasterSupplierLog> ProductMasterSupplierLogs { get; set; }
        public DbSet<ProductNewArrival> ProductNewArrivals { get; set; }
        public DbSet<ProductNotification> ProductNotifications { get; set; }
        public DbSet<ProductNotificationIntl> ProductNotificationIntls { get; set; }
        public DbSet<ProductOffer> ProductOffers { get; set; }
        public DbSet<ProductPointsMaster> ProductPointsMasters { get; set; }
        public DbSet<ProductPromotion> ProductPromotions { get; set; }
        public DbSet<ProductQuantityDiscount> ProductQuantityDiscounts { get; set; }
        public DbSet<ProductRecommend> ProductRecommends { get; set; }
        public DbSet<ProductSearchKeyword> ProductSearchKeywords { get; set; }
        public DbSet<ProductSearchKeywordsAr> ProductSearchKeywordsArs { get; set; }
        public DbSet<ProductSkusLog> ProductSkusLogs { get; set; }
        public DbSet<ProductStatusBatch> ProductStatusBatches { get; set; }
        public DbSet<ProductStatusBatchLog> ProductStatusBatchLogs { get; set; }
        public DbSet<ProductStockBatch> ProductStockBatches { get; set; }
        public DbSet<ProductStockBatchLog> ProductStockBatchLogs { get; set; }
        public DbSet<ProductStockBatchPartNo> ProductStockBatchPartNoes { get; set; }
        public DbSet<ProductStockBatchPartNoLog> ProductStockBatchPartNoLogs { get; set; }
        public DbSet<ProductUsed> ProductUseds { get; set; }
        public DbSet<ProductVisitLog> ProductVisitLogs { get; set; }
        public DbSet<ProductVisitLogIntl> ProductVisitLogIntls { get; set; }
        public DbSet<ProductVoucherSetting> ProductVoucherSettings { get; set; }
        public DbSet<ProductWeightBatch> ProductWeightBatches { get; set; }
        public DbSet<ProductWeightBatchLog> ProductWeightBatchLogs { get; set; }
        public DbSet<PublicHoliday> PublicHolidays { get; set; }
        public DbSet<SalesReportCoutureLog> SalesReportCoutureLogs { get; set; }
        public DbSet<SearchKeyword> SearchKeywords { get; set; }
        public DbSet<ServiceSupport> ServiceSupports { get; set; }
        public DbSet<ServiceSupportComment> ServiceSupportComments { get; set; }
        public DbSet<ShoppingCartGiftItem> ShoppingCartGiftItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<ShoppingCartItemLocked> ShoppingCartItemLockeds { get; set; }
        public DbSet<ShoppingCartItemLog> ShoppingCartItemLogs { get; set; }
        public DbSet<ShoppingCartPayment> ShoppingCartPayments { get; set; }
        public DbSet<ShoppingCartSaveforLater> ShoppingCartSaveforLaters { get; set; }
        public DbSet<ShoppingCartSummary> ShoppingCartSummaries { get; set; }
        public DbSet<ShoppingCartTransLocked> ShoppingCartTransLockeds { get; set; }
        public DbSet<SpecialOfferBannerMaster> SpecialOfferBannerMasters { get; set; }
        public DbSet<SupplierMaster> SupplierMasters { get; set; }
        public DbSet<SupplierMasterType> SupplierMasterTypes { get; set; }
        public DbSet<SupplierPurchaseOrder> SupplierPurchaseOrders { get; set; }
        public DbSet<SupplierPurchaseOrderDetail> SupplierPurchaseOrderDetails { get; set; }
        public DbSet<SupplierPurchaseOrderLog> SupplierPurchaseOrderLogs { get; set; }
        public DbSet<SupplierPurchaseOrderMaster> SupplierPurchaseOrderMasters { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<UserAccess> UserAccesses { get; set; }
        public DbSet<UserLog> UserLogs { get; set; }
        public DbSet<UserMaster> UserMasters { get; set; }
        public DbSet<UserVisitLog> UserVisitLogs { get; set; }
        public DbSet<VehicleAccidentHistory> VehicleAccidentHistories { get; set; }
        public DbSet<VehicleMaster> VehicleMasters { get; set; }
        public DbSet<VehicleServiceRecord> VehicleServiceRecords { get; set; }
        public DbSet<VoucherClaim> VoucherClaims { get; set; }
        public DbSet<VoucherCustomerTran> VoucherCustomerTrans { get; set; }
        public DbSet<VoucherMaster> VoucherMasters { get; set; }
        public DbSet<VoucherWalletTransaction> VoucherWalletTransactions { get; set; }
        public DbSet<VoucherMasterDetail> VoucherMasterDetails { get; set; }
        public DbSet<VoucherTransaction> VoucherTransactions { get; set; }
        public DbSet<VoucherValidityLog> VoucherValidityLogs { get; set; }
        public DbSet<WebConfig> WebConfigs { get; set; }
        public DbSet<WebConfigApp> WebConfigApps { get; set; }
        public DbSet<WebsiteLog> WebsiteLogs { get; set; }
        public DbSet<WalletCustomerLog> WalletCustomerLogs { get; set; }
        public DbSet<WalletStatusMaster> WalletStatusMasters { get; set; }
        public DbSet<WalletTransactionDetail> WalletTransactionDetails { get; set; }
        public DbSet<WalletTransactionMaster> WalletTransactionMasters { get; set; }
        public DbSet<View_Duplicate> View_Duplicate { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<BrandStatus> BrandStatuses { get; set; }
        public DbSet<BrandCultureData> BrandCultureDatas { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryCultureData> CategoryCultureDatas { get; set; }
        public DbSet<ProductBundle> ProductBundles { get; set; }
        public DbSet<Culture> Cultures { get; set; }
        public DbSet<CultureData> CultureDatas { get; set; }
        public DbSet<Product> Products { get; set; }

        //public DbSet<ProductSKUCultureData> ProductSKUCultureDataa { get; set; }
        public DbSet<ProductSKUCultureData> ProductSKUCultureDatas { get; set; }
        public DbSet<ProductCategoryMap> ProductCategoryMaps { get; set; }
        public DbSet<ProductCultureData> ProductCultureDataa { get; set; }
        public DbSet<ProductCutlureData> ProductCutlureDatas { get; set; }
        public DbSet<ProductFamily> ProductFamilys { get; set; }
        public DbSet<ProductFamilyCultureData> ProductFamilyCultureDatas { get; set; }
        public DbSet<ProductFamilyPropertyMap> ProductFamilyPropertyMaps { get; set; }
        public DbSet<ProductFamilyPropertyTypeMap> ProductFamilyPropertyTypeMaps { get; set; }
        public DbSet<ProductFamilyType> ProductFamilyTypes { get; set; }
        public DbSet<ProductImageMap> ProductImageMaps { get; set; }
        public DbSet<ProductInventoryConfig> ProductInventoryConfigs { get; set; }
        public DbSet<ProductInventory> ProductInventories { get; set; }

        public DbSet<ProductPriceList> ProductPriceLists { get; set; }
        public DbSet<ProductPriceListCultureData> ProductPriceListCultureDatas { get; set; }
        public DbSet<ProductPriceListCustomerMap> ProductPriceListCustomerMaps { get; set; }
        public DbSet<ProductPriceListBrandMap> ProductPriceListBrandMaps { get; set; }
        public DbSet<ProductPriceListBranchMap> ProductPriceListBranchMaps { get; set; }
        public DbSet<ProductPriceListCategoryMap> ProductPriceListCategoryMaps { get; set; }
        public DbSet<ProductPriceListCustomerGroupMap> ProductPriceListCustomerGroupMaps { get; set; }
        public DbSet<ProductPriceListProductMap> ProductPriceListProductMaps { get; set; }
        public DbSet<ProductPriceListProductQuantityMap> ProductPriceListProductQuantityMaps { get; set; }
        public DbSet<ProductPriceListSKUMap> ProductPriceListSKUMaps { get; set; }
        public DbSet<ProductPriceListSKUQuantityMap> ProductPriceListSKUQuantityMaps { get; set; }

        public DbSet<ProductPropertyMap> ProductPropertyMaps { get; set; }
        public DbSet<ProductSKUMap> ProductSKUMaps { get; set; }
        public DbSet<ProductStatu> ProductStatus { get; set; }
        public DbSet<ProductToProductMap> ProductToProductMaps { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductVideoMap> ProductVideoMaps { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyCultureData> PropertyCultureDatas { get; set; }
        public DbSet<SalesRelationshipType> SalesRelationshipTypes { get; set; }
        public DbSet<SeoMetadata> SeoMetadatas { get; set; }
        public DbSet<UIControlType> UIControlTypes { get; set; }
        public DbSet<UIControlValidation> UIControlValidations { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<UnitGroup> UnitGroups { get; set; }
        public DbSet<TransactionDetail> TransactionDetails { get; set; }
        public DbSet<TransactionHead> TransactionHeads { get; set; }
        public DbSet<TransactionHeadShoppingCartMap> TransactionHeadShoppingCartMaps { get; set; }
        public DbSet<DocumentReferenceType> DocumentReferenceTypes { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<DocumentTypeType> DocumentTypeTypes { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Student> Students { get; set; }
        public DbSet<StudentApplication> StudentApplications { get; set; }
        public virtual DbSet<ApplicationStatus> ApplicationStatuses { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<Parent> Parents { get; set; }

        public virtual DbSet<Routes1> Routes1 { get; set; }
        public virtual DbSet<StudentRouteStopMap> StudentRouteStopMaps { get; set; }
        public virtual DbSet<RouteStopMap> RouteStopMaps { get; set; }
        public virtual DbSet<RouteVehicleMap> RouteVehicleMaps { get; set; }
        public virtual DbSet<AssignVehicleMap> AssignVehicleMaps { get; set; }

        public DbSet<LoginRoleMap> LoginRoleMaps { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<PermissionCultureData> PermissionCultureDatas { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RoleCultureData> RoleCultureDatas { get; set; }
        public DbSet<RolePermissionMap> RolePermissionMaps { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ViewColumn> ViewGridColumns { get; set; }
        public DbSet<TestSearchView> TestSearchViews { get; set; }
        public DbSet<View> Views { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<VoucherStatus> VoucherStatuses { get; set; }
        public DbSet<VoucherType> VoucherTypes { get; set; }
        public DbSet<WishList> WishLists { get; set; }


        public DbSet<MenuLink> MenuLinks { get; set; }
        public DbSet<MenuLinkCategoryMap> MenuLinkCategoryMaps { get; set; }
        public DbSet<MenuLinkCultureData> MenuLinkCultureDatas { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<MenuLinkBrandMap> MenuLinkBrandMaps { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsType> NewsTypes { get; set; }
        public DbSet<StaticContentData> StaticContentDatas { get; set; }
        public DbSet<StaticContentType> StaticContentTypes { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartVoucherMap> ShoppingCartVoucherMaps { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<CategoryBannerMaster> CategoryBannerMaster { get; set; }

        public DbSet<UserView> UserViews { get; set; }
        public DbSet<ViewFilter> ViewFilters { get; set; }
        public DbSet<FilterColumn> FilterColumns { get; set; }
        public DbSet<UserViewFilterMap> UserViewFilterMaps { get; set; }
        public DbSet<ViewType> ViewTypes { get; set; }
        public DbSet<FilterColumnConditionMap> FilterColumnConditionMaps { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<FilterColumnUserValue> FilterColumnUserValues { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<BannerStatus> BannerStatuses { get; set; }
        public DbSet<BannerType> BannerTypes { get; set; }
        public DbSet<CustomerGroup> CustomerGroups { get; set; }

        public DbSet<PaymentDetailsKnet> PaymentDetailsKnets { get; set; }
        public DbSet<PaymentMasterVisa> PaymentMasterVisas { get; set; }
        public DbSet<InvetoryTransaction> InvetoryTransactions { get; set; }
        public virtual DbSet<PaymentLog> PaymentLogs { get; set; }

        public DbSet<NotificationQueueParentMap> NotificationQueueParentMaps { get; set; }
        public DbSet<NotificationStatus> NotificationStatuses { get; set; }
        public DbSet<NotificationLog> NotificationLogs { get; set; }
        public DbSet<NotificationsProcessing> NotificationsProcessings { get; set; }
        public DbSet<NotificationsQueue> NotificationsQueues { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }
        public DbSet<AlertStatus> AlertStatuses { get; set; }
        public DbSet<AlertType> AlertTypes { get; set; }

        public DbSet<GetSKUByProperty> GetSKUByProperties { get; set; }
        public DbSet<EmailNotificationData> EmailNotificationDatas { get; set; }
        public DbSet<EmailNotificationType> EmailNotificationTypes { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<StatusesCultureData> StatusesCultureDatas { get; set; }
        public DbSet<StockNotification> StockNotifications { get; set; }
        public DbSet<StockNotificationStatus> StockNotificationStatuses { get; set; }
        public DbSet<EntityChangeTracker> EntityChangeTrackers { get; set; }
        public DbSet<EntityChangeTrackersInProcess> EntityChangeTrackersInProcesses { get; set; }
        public DbSet<EntityChangeTrackersQueue> EntityChangeTrackersQueues { get; set; }
        public DbSet<OperationType> OperationTypes { get; set; }
        public DbSet<SyncEntity> SyncEntities { get; set; }
        public DbSet<TrackerStatus> TrackerStatuses { get; set; }
        public DbSet<OrderContactMap> OrderContactMaps { get; set; }
        public DbSet<OrderTracking> OrderTrackings { get; set; }
        public DbSet<SyncFieldMap> SyncFieldMaps { get; set; }
        public DbSet<SyncFieldMapType> SyncFieldMapTypes { get; set; }
        public DbSet<LoginStatus> LoginStatuses { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<BranchStatus> BranchStatuses { get; set; }
        public DbSet<CompanyStatus> CompanyStatuses { get; set; }
        public DbSet<WarehouseStatus> WarehouseStatuses { get; set; }
        public DbSet<BranchGroupStatus> BranchGroupStatuses { get; set; }
        public DbSet<DepartmentStatus> DepartmentStatuses { get; set; }
        public DbSet<BranchGroup> BranchGroups { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyCurrencyMap> CompanyCurrencyMaps { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<vwProductNewArrival> vwProductNewArrivals { get; set; }
        public DbSet<vwProductOrderByDate> vwProductOrderByDates { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<MaritalStatus> MaritalStatuses { get; set; }

        public virtual DbSet<Relegion> Relegions { get; set; }
        public virtual DbSet<Cast> Casts { get; set; }
        public virtual DbSet<Community> Communitys { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<EmployeeRole> EmployeeRoles { get; set; }
        public DbSet<JobType> JobTypes { get; set; }

        public DbSet<ProductInventoryProductConfigMap> ProductInventoryProductConfigMaps { get; set; }
        public DbSet<ProductInventorySKUConfigMap> ProductInventorySKUConfigMaps { get; set; }
        public DbSet<SeoMetadataCultureData> SeoMetadataCultureDatas { get; set; }
        public DbSet<DeliveryType> DeliveryTypes { get; set; }
        public DbSet<UserViewColumnMap> UserViewColumnMaps { get; set; }
        public DbSet<RelationType> RelationTypes { get; set; }
        public DbSet<EmployeeCatalogRelation> EmployeeCatalogRelations { get; set; }
        public DbSet<EntitlementMap> EntitlementMaps { get; set; }
        public DbSet<EntityProperty> EntityProperties { get; set; }
        public DbSet<EntityPropertyMap> EntityPropertyMaps { get; set; }
        public DbSet<EntityPropertyType> EntityPropertyTypes { get; set; }
        public DbSet<EntityTypeEntitlement> EntityTypeEntitlements { get; set; }
        public DbSet<EntityTypePaymentMethodMap> EntityTypePaymentMethodMaps { get; set; }
        public DbSet<EntityTypeRelationMap> EntityTypeRelationMaps { get; set; }
        public DbSet<EntityType> EntityTypes { get; set; }
        public DbSet<CustomerStatus> CustomerStatuses { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<PackingType> PackingTypes { get; set; }
        public DbSet<CustomerSupplierMap> CustomerSupplierMaps { get; set; }
        //public DbSet<SeoMetadataCultureData> SeoMetadataCultureDatas { get; set; }
        public DbSet<DataFeedLog> DataFeedLogs { get; set; }
        public DbSet<DataFeedOperation> DataFeedOperations { get; set; }
        public DbSet<DataFeedStatus> DataFeedStatuses { get; set; }
        public DbSet<DataFeedType> DataFeedTypes { get; set; }
        public DbSet<DataFeedTableColumn> DataFeedTableColumns { get; set; }
        public DbSet<DataFeedTable> DataFeedTables { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<DocumentFile> DocumentFiles { get; set; }
        public DbSet<DocumentFileStatus> DocumentFileStatuses { get; set; }

        public DbSet<EntityChangeTrackerLog> EntityChangeTrackerLogs { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<SupplierStatus> SupplierStatuses { get; set; }
        public DbSet<ProductPriceListType> ProductPriceListTypes { get; set; }
        public DbSet<ProductPriceListLevel> ProductPriceListLevels { get; set; }
        public DbSet<CustomerProductReference> CustomerProductReferences { get; set; }
        public DbSet<TransactionShipment> TransactionShipments { get; set; }
        public DbSet<TransactionAllocation> TransactionAllocations { get; set; }
        public DbSet<ProductPriceListSupplierMap> ProductPriceListSupplierMaps { get; set; }

        public DbSet<ClaimLoginMap> ClaimLoginMaps { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<ClaimSetClaimMap> ClaimSetClaimMaps { get; set; }
        public DbSet<ClaimSetClaimSetMap> ClaimSetClaimSetMaps { get; set; }
        public DbSet<ClaimSetLoginMap> ClaimSetLoginMaps { get; set; }
        public DbSet<ClaimSet> ClaimSets { get; set; }
        public DbSet<ClaimType> ClaimTypes { get; set; }
        public DbSet<ProductSerialMap> ProductSerialMaps { get; set; }
        public DbSet<JobEntryDetail> JobEntryDetails { get; set; }
        public DbSet<JobEntryHead> JobEntryHeads { get; set; }
        public DbSet<JobStatus> JobStatuses { get; set; }
        public DbSet<JobActivity> JobActivities { get; set; }
        public DbSet<JobOperationStatus> JobOperationStatuses { get; set; }

        public DbSet<Priority> Priorities { get; set; }
        public DbSet<vwProductOrderByDateIntl> vwProductOrderByDateIntls { get; set; }
        public DbSet<vwProductListSearchListingIntl> vwProductListSearchListingIntls { get; set; }
        public DbSet<vwProductNewArrivalsIntl> vwProductNewArrivalsIntls { get; set; }
        public DbSet<ProductSearchKeywordsArIntl> ProductSearchKeywordsArIntls { get; set; }
        public DbSet<ProductSearchKeywordsIntl> ProductSearchKeywordsIntls { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationType> LocationTypes { get; set; }
        public DbSet<ProductLocationMap> ProductLocationMaps { get; set; }

        public DbSet<VehicleOwnershipType> VehicleOwnershipTypes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }

        public DbSet<Basket> Basket { get; set; }

        public DbSet<CategoryImageMap> CategoryImageMaps { get; set; }

        public DbSet<ImageType> ImageTypes { get; set; }

        public DbSet<Area> Areas { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<Route> Routes { get; set; }

        public DbSet<Page> Pages { get; set; }
        public DbSet<PageType> PageTypes { get; set; }
        public DbSet<BoilerPlate> BoilerPlates { get; set; }
        public DbSet<PageBoilerplateMap> PageBoilerplateMaps { get; set; }
        public DbSet<PageBoilerplateMapParameter> PageBoilerplateMapParameters { get; set; }
        public DbSet<InventoryVerification> InventoryVerifications { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<ServiceProvider> ServiceProviders { get; set; }
        public DbSet<ServiceProviderLog> ServiceProviderLogs { get; set; }
        public DbSet<ServiceProviderSetting> ServiceProviderSettings { get; set; }

        public DbSet<ProductSKUVariant> ProductSKUVariants { get; set; }

        public DbSet<PropertyTypeCultureData> PropertyTypeCultureDatas { get; set; }
        public DbSet<DataFormat> DataFormats { get; set; }
        public DbSet<DataFormatType> DataFormatTypes { get; set; }
        public virtual DbSet<NotificationAlert> NotificationAlerts { get; set; }
        public virtual DbSet<MailBox> MailBoxes { get; set; }
        public DbSet<UserDataFormatMap> UserDataFormatMaps { get; set; }

        public DbSet<CustomerSetting> CustomerSettings { get; set; }

        public DbSet<KnowHowOption> KnowHowOptions { get; set; }

        public virtual DbSet<Accounts_SubLedger> Accounts_SubLedger { get; set; }
        public DbSet<AccountBehavoir> AccountBehavoirs { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<ChartOfAccountMap> ChartOfAccountMaps { get; set; }
        public DbSet<ChartOfAccount> ChartOfAccounts { get; set; }
        public DbSet<ChartRowType> ChartRowTypes { get; set; }

        public DbSet<SupportAction> SupportActions { get; set; }
        public DbSet<TicketPriority> TicketPriorities { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketStatus> TicketStatuses { get; set; }
        public DbSet<TicketTag> TicketTags { get; set; }
        public DbSet<TicketActionDetailDetailMap> TicketActionDetailDetailMaps { get; set; }
        public DbSet<TicketActionDetailMap> TicketActionDetailMaps { get; set; }

        public DbSet<ProductDeliveryCountrySetting> ProductDeliveryCountrySettings { get; set; }
        public DbSet<BoilerPlateParameter> BoilerPlateParameters { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<EmployeeRoleMap> EmployeeRoleMaps { get; set; }

        public DbSet<TicketReason> TicketReasons { get; set; }
        public DbSet<TicketProductMap> TicketProductMaps { get; set; }
        public DbSet<JobSize> JobSizes { get; set; }

        public DbSet<ProductInventorySerialMap> ProductInventorySerialMaps { get; set; }
        public DbSet<PropertyTypePropertyMap> PropertyTypePropertyMaps { get; set; }
        public DbSet<BrandPriceListMap> BrandPriceListMaps { get; set; }
        public DbSet<CustomerGroupDeliveryTypeMap> CustomerGroupDeliveryTypeMaps { get; set; }
        public DbSet<ProductTypeDeliveryTypeMap> ProductTypeDeliveryTypeMaps { get; set; }

        public DbSet<TransactionHeadEntitlementMap> TransactionHeadEntitlementMaps { get; set; }

        public DbSet<TransactionHeadPointsMap> TransactionHeadPointsMaps { get; set; }

        public DbSet<NewsletterSubscription> NewsletterSubscriptions { get; set; }
        public DbSet<CustomerSupportTicket> CustomerSupportTickets { get; set; }
        public DbSet<CustomerJustAsk> CustomerJustAsks { get; set; }
        public DbSet<UserJobApplication> UserJobApplications { get; set; }
        public DbSet<ProductInventoryConfigCultureData> ProductInventoryConfigCultureDatas { get; set; }
        public DbSet<PageBoilerplateMapParameterCultureData> PageBoilerplateMapParameterCultureDatas { get; set; }
        public DbSet<SiteCountryMap> SiteCountryMaps { get; set; }
        public DbSet<DocumentReferenceStatusMap> DocumentReferenceStatusMaps { get; set; }
        public DbSet<DocumentStatus> DocumentStatuses { get; set; }

        public DbSet<ActionLinkType> ActionLinkTypes { get; set; }
        public DbSet<OrderRoleTracking> OrderRoleTrackings { get; set; }
        public DbSet<JobStatusOperationMap> JobStatusOperationMaps { get; set; }
        public DbSet<DataHistoryEntity> DataHistoryEntities { get; set; }

        public DbSet<Payable> Payables { get; set; }
        public DbSet<Receivable> Receivables { get; set; }
        public DbSet<JobsEntryHeadPayableMap> JobsEntryHeadPayableMaps { get; set; }
        public DbSet<JobsEntryHeadReceivableMap> JobsEntryHeadReceivableMaps { get; set; }
        public DbSet<TransactionHeadReceivablesMap> TransactionHeadReceivablesMaps { get; set; }
        public DbSet<TransactionHeadPayablesMap> TransactionHeadPayablesMaps { get; set; }
        public DbSet<DocumentTypeTransactionNumber> DocumentTypeTransactionNumbers { get; set; }
        public DbSet<AccountTransactionHead> AccountTransactionHeads { get; set; }
        public DbSet<AccountTransactionDetail> AccountTransactionDetails { get; set; }
        public DbSet<AssetTransactionHeadAccountMap> AssetTransactionHeadAccountMaps { get; set; }
        public DbSet<AccountTransactionHeadAccountMap> AccountTransactionHeadAccountMaps { get; set; }
        public DbSet<AccountTransactionInventoryHeadMap> AccountTransactionInventoryHeadMaps { get; set; }
        public DbSet<PaymentGroup> PaymentGroups { get; set; }
        public DbSet<Notify> Notifies { get; set; }
        public DbSet<ProductSKUSiteMap> ProductSKUSiteMaps { get; set; }
        public DbSet<DeliveryTypeTimeSlotMapsCulture> DeliveryTypeTimeSlotMapsCultures { get; set; }
        public DbSet<OrderDeliveryDisplayHeadMap> OrderDeliveryDisplayHeadMaps { get; set; }

        public DbSet<ReceivingMethod> ReceivingMethods { get; set; }
        public DbSet<ReturnMethod> ReturnMethods { get; set; }
        public DbSet<CategorySetting> CategorySettings { get; set; }
        public DbSet<PaymentMode> PaymentModes { get; set; }
        public DbSet<EmployeeAccountMap> EmployeeAccountMaps { get; set; }
        public DbSet<CostCenter> CostCenters { get; set; }
        public virtual DbSet<EmployeeAdditionalInfo> EmployeeAdditionalInfos { get; set; }
        public virtual DbSet<Nationality> Nationalities { get; set; }

        public virtual DbSet<PassportVisaDetail> PassportVisaDetails { get; set; }

        public virtual DbSet<EmployeeBankDetail> EmployeeBankDetails { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<Catogory> Catogories { get; set; }
        public virtual DbSet<LicenseType> LicenseTypes { get; set; }

        public virtual DbSet<AreaTreeSearch> AreaTreeSearches { get; set; }

        public virtual DbSet<Communication> Communications { get; set; }
        public virtual DbSet<CommunicationType> CommunicationTypes { get; set; }
        public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }
        public virtual DbSet<Lead> Leads { get; set; }
        public DbSet<Schools> Schools { get; set; }

        public virtual DbSet<CertificateLog> CertificateLogs { get; set; }
        public virtual DbSet<CertificateTemplate> CertificateTemplates { get; set; }

        public virtual DbSet<UserDeviceMap> UserDeviceMaps { get; set; }

        public virtual DbSet<AcademicYear> AcademicYears { get; set; }
        public virtual DbSet<AcademicYearStatu> AcademicYearStatus { get; set; }

        public virtual DbSet<AcademicYearCalendarEventType> AcademicYearCalendarEventTypes { get; set; }
        public virtual DbSet<AcademicYearCalendarEvent> AcademicYearCalendarEvents { get; set; }

        public virtual DbSet<AcademicYearCalendarStatu> AcademicYearCalendarStatus { get; set; }
        public virtual DbSet<AcadamicCalendar> AcadamicCalendars { get; set; }
        public virtual DbSet<CalendarType> CalendarTypes { get; set; }

        public virtual DbSet<SchoolGeoMap> SchoolGeoMaps { get; set; }

        public virtual DbSet<AdditionalExpenseProvisionalAccountMap> AdditionalExpenseProvisionalAccountMaps { get; set; }
        public virtual DbSet<AdditionalExpens> AdditionalExpenses { get; set; }
        public virtual DbSet<AdditionalExpensesTransactionsMap> AdditionalExpensesTransactionsMaps { get; set; }
        public virtual DbSet<DeliveryTypeCultureData> DeliveryTypeCultureDatas { get; set; }
        public virtual DbSet<DeliveryTypeCutOffSlot> DeliveryTypeCutOffSlots { get; set; }
        public virtual DbSet<StockCompareDetail> StockCompareDetails { get; set; }
        public virtual DbSet<StockCompareHead> StockCompareHeads { get; set; }
        public virtual DbSet<CostCenterAccountMap> CostCenterAccountMaps { get; set; }

        public virtual DbSet<FeeDueInventoryMap> FeeDueInventoryMaps { get; set; }
        public virtual DbSet<Sponsor> Sponsors { get; set; }
        public virtual DbSet<AccomodationType> AccomodationTypes { get; set; }
        public virtual DbSet<PassageType> PassageTypes { get; set; }
        public virtual DbSet<LeaveGroup> LeaveGroups { get; set; }
        //public virtual DbSet<LeaveGroupTypeMap> LeaveGroupTypeMaps { get; set; }

        public virtual DbSet<Allergy> Allergies { get; set; }
        public virtual DbSet<AllergyStudentMap> AllergyStudentMaps { get; set; }
        public virtual DbSet<ProductAllergyMap> ProductAllergyMaps { get; set; }

        public virtual DbSet<StudentTransferRequest> StudentTransferRequests { get; set; }

        public virtual DbSet<PeriodClosingTranHead> PeriodClosingTranHeads { get; set; }
        public virtual DbSet<PeriodClosingTranMaster> PeriodClosingTranMasters { get; set; }
        public virtual DbSet<PeriodClosingTranStatus> PeriodClosingTranStatuses { get; set; }
        public virtual DbSet<PeriodClosingTranTail> PeriodClosingTranTails { get; set; }

        public virtual DbSet<Day> Days { get; set; }
        public virtual DbSet<SubscriptionType> SubscriptionTypes { get; set; }

        public virtual DbSet<ShoppingCartWeekDayMap> ShoppingCartWeekDayMaps { get; set; }

        public virtual DbSet<VisitorAttachmentMap> VisitorAttachmentMaps { get; set; }
        public virtual DbSet<Visitor> Visitors { get; set; }

        public virtual DbSet<Severity> Severities { get; set; }

        public virtual DbSet<DepartmentCostCenterMap> DepartmentCostCenterMaps { get; set; }

        public virtual DbSet<EmployeeRelationsDetail> EmployeeRelationsDetails { get; set; }

        public virtual DbSet<EmployeeRelationType> EmployeeRelationTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BranchCultureDataMap());
            modelBuilder.Configurations.Add(new SiteCountryMapMap());
            modelBuilder.Configurations.Add(new AccountTransactionMap());
            modelBuilder.Configurations.Add(new TransactionHeadAccountMapMap());
            modelBuilder.Configurations.Add(new CustomerAccountMapMap());
            modelBuilder.Configurations.Add(new SupplierAccountMapMap());
            modelBuilder.Configurations.Add(new LoginRoleMapMap());
            modelBuilder.Configurations.Add(new LoginMap());
            modelBuilder.Configurations.Add(new PermissionCultureDataMap());
            modelBuilder.Configurations.Add(new PermissionMap());
            modelBuilder.Configurations.Add(new RoleCultureDataMap());
            modelBuilder.Configurations.Add(new RolePermissionMapMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new AssetCategoryMap());
            modelBuilder.Configurations.Add(new AssetMap());
            modelBuilder.Configurations.Add(new AssetTransactionDetailMap());
            modelBuilder.Configurations.Add(new AssetTransactionHeadMap());
            modelBuilder.Configurations.Add(new DeliveryTypeMasterMap());
            modelBuilder.Configurations.Add(new TimeSlotMasterMap());
            modelBuilder.Configurations.Add(new TimeSlotOverRiderMap());
            modelBuilder.Configurations.Add(new AddressMasterMap());
            modelBuilder.Configurations.Add(new AdminTaskLogMap());
            modelBuilder.Configurations.Add(new ArabicReportStatusLogMap());
            modelBuilder.Configurations.Add(new AreaMasterMap());
            modelBuilder.Configurations.Add(new BannerLogMap());
            modelBuilder.Configurations.Add(new BannerMasterMap());
            modelBuilder.Configurations.Add(new BannerMasterEmailMap());
            modelBuilder.Configurations.Add(new BannerMasterPositionMap());
            modelBuilder.Configurations.Add(new BannerPageMap());
            modelBuilder.Configurations.Add(new BannerPage1Map());
            modelBuilder.Configurations.Add(new BannerSessionMap());
            modelBuilder.Configurations.Add(new BannerSessionNewMap());
            modelBuilder.Configurations.Add(new BannerSideAngleMap());
            modelBuilder.Configurations.Add(new BlinkLocationMasterMap());
            modelBuilder.Configurations.Add(new BlinkPoGrnDetailMap());
            modelBuilder.Configurations.Add(new BlinkPoGrnLogMap());
            modelBuilder.Configurations.Add(new BlinkPoGrnMasterMap());
            modelBuilder.Configurations.Add(new BlinkPoOrderDetailMap());
            modelBuilder.Configurations.Add(new BlinkPoOrderMasterMap());
            modelBuilder.Configurations.Add(new BlinkPoOrderMasterLogMap());
            modelBuilder.Configurations.Add(new BlinkUsaBannerMasterMap());
            modelBuilder.Configurations.Add(new BlinkVendorMasterMap());
            modelBuilder.Configurations.Add(new BrandMasterMap());
            modelBuilder.Configurations.Add(new CampaignCustomerMap());
            modelBuilder.Configurations.Add(new CampaignMasterMap());
            modelBuilder.Configurations.Add(new CategoryBlockMap());
            modelBuilder.Configurations.Add(new CategoryBlocksBrandMap());
            modelBuilder.Configurations.Add(new CategoryBlocksCategoryMap());
            modelBuilder.Configurations.Add(new CategoryBlocksProductMap());
            modelBuilder.Configurations.Add(new CategoryBrandMap());
            modelBuilder.Configurations.Add(new CategoryColumnMap());
            modelBuilder.Configurations.Add(new CategoryFilterMap());
            modelBuilder.Configurations.Add(new CategoryHomePageMap());
            modelBuilder.Configurations.Add(new CategoryMasterMap());
            modelBuilder.Configurations.Add(new CategoryReportGroupMap());
            modelBuilder.Configurations.Add(new CategoryReportGroupDetailMap());
            modelBuilder.Configurations.Add(new CategorySortDetailMap());
            modelBuilder.Configurations.Add(new CategorySortMasterMap());
            modelBuilder.Configurations.Add(new CategoryVisitLogMap());
            modelBuilder.Configurations.Add(new CategoryVisitLogIntlMap());
            modelBuilder.Configurations.Add(new CheckAvailablityMap());
            modelBuilder.Configurations.Add(new ColumnGroupCounterMap());
            modelBuilder.Configurations.Add(new ColumnMasterMap());
            modelBuilder.Configurations.Add(new ColumnValueMap());
            modelBuilder.Configurations.Add(new CountryGroupMap());
            modelBuilder.Configurations.Add(new CountryMasterMap());
            modelBuilder.Configurations.Add(new CoutureHomeBannerMasterMap());
            modelBuilder.Configurations.Add(new CurrencyMasterMap());
            modelBuilder.Configurations.Add(new CustomerByUserMap());
            modelBuilder.Configurations.Add(new CustomerCategorizationMap());
            modelBuilder.Configurations.Add(new CustomerCategorizationProductPriceMap());
            modelBuilder.Configurations.Add(new CustomerCategorizationProductPriceIntlMap());
            modelBuilder.Configurations.Add(new CustomerJustAskMap());
            modelBuilder.Configurations.Add(new CustomerLogMap());
            modelBuilder.Configurations.Add(new CustomerLogVerificationMap());
            modelBuilder.Configurations.Add(new CustomerMasterMap());
            modelBuilder.Configurations.Add(new CustomerMasterNewMap());
            modelBuilder.Configurations.Add(new CustomerMasterNumberInvalidMap());
            modelBuilder.Configurations.Add(new CustomerPageLogMap());
            modelBuilder.Configurations.Add(new CustomerPinSearchMap());
            modelBuilder.Configurations.Add(new CustomerPointsLogMap());
            modelBuilder.Configurations.Add(new CustomerSellUsedMap());
            modelBuilder.Configurations.Add(new CustomerSessionMap());
            modelBuilder.Configurations.Add(new CustomerSlabMultipriceMap());
            modelBuilder.Configurations.Add(new CustomerSlabMultipriceIntlMap());
            modelBuilder.Configurations.Add(new CustomerSlabMultiPriceProcessMap());
            modelBuilder.Configurations.Add(new CustomerSlabMultiPriceProcessIntlMap());
            modelBuilder.Configurations.Add(new CustomerSupportMap());
            modelBuilder.Configurations.Add(new CustomerSupportCommentMap());
            modelBuilder.Configurations.Add(new DataFeedLogMap());
            modelBuilder.Configurations.Add(new DataFeedOperationMap());
            modelBuilder.Configurations.Add(new DataFeedStatusMap());
            modelBuilder.Configurations.Add(new DataFeedTypeMap());
            modelBuilder.Configurations.Add(new DataFeedTableColumnMap());
            modelBuilder.Configurations.Add(new DataFeedTableMap());
            modelBuilder.Configurations.Add(new ContactMap());
            modelBuilder.Configurations.Add(new CountryMap());
            modelBuilder.Configurations.Add(new DriverMasterMap());
            modelBuilder.Configurations.Add(new DriverViolationMap());
            modelBuilder.Configurations.Add(new EmailTrackerMap());
            modelBuilder.Configurations.Add(new ErrorTraceMap());
            modelBuilder.Configurations.Add(new ExcelDetailMap());
            modelBuilder.Configurations.Add(new ExpressDeliverySlotMap());
            modelBuilder.Configurations.Add(new ExpressDeliverySlotsHolidayMap());
            modelBuilder.Configurations.Add(new GiftSlotMap());
            modelBuilder.Configurations.Add(new GiftSlotProductMap());
            modelBuilder.Configurations.Add(new HomePageLinkMap());
            modelBuilder.Configurations.Add(new HomePageLinksVisitMap());
            modelBuilder.Configurations.Add(new IntlPoBankAccountMap());
            modelBuilder.Configurations.Add(new IntlPoGrnDetailMap());
            modelBuilder.Configurations.Add(new IntlPoGrnLogMap());
            modelBuilder.Configurations.Add(new IntlPoGrnMasterMap());
            modelBuilder.Configurations.Add(new IntlPoOrderDetailMap());
            modelBuilder.Configurations.Add(new IntlPoOrderDetailsCancelledLogMap());
            modelBuilder.Configurations.Add(new IntlPoOrderMasterMap());
            modelBuilder.Configurations.Add(new IntlPoOrderMasterLogMap());
            modelBuilder.Configurations.Add(new IntlPoOrderMasterPaymentMap());
            modelBuilder.Configurations.Add(new IntlPoOrderMasterPaymentAdditionalMap());
            modelBuilder.Configurations.Add(new IntlPoRequestMap());
            modelBuilder.Configurations.Add(new IntlPoRequestActionMap());
            modelBuilder.Configurations.Add(new IntlPoRequestQuantityStatuMap());
            modelBuilder.Configurations.Add(new IntlPoRequestRemarkMap());
            modelBuilder.Configurations.Add(new IntlPoRequestStatusLogMap());
            modelBuilder.Configurations.Add(new IntlPoShipmentBoxDetailMap());
            modelBuilder.Configurations.Add(new IntlPoShipmentDetailMap());
            modelBuilder.Configurations.Add(new IntlPoShipmentMasterMap());
            modelBuilder.Configurations.Add(new IntlPoShipmentMasterLogMap());
            modelBuilder.Configurations.Add(new IntlPoShipmentPaymentMasterMap());
            modelBuilder.Configurations.Add(new IntlPoShipmentReceivedMap());
            modelBuilder.Configurations.Add(new IntlPoShipmentReceivedLogMap());
            modelBuilder.Configurations.Add(new IntlPoShipperMasterMap());
            modelBuilder.Configurations.Add(new IntlPoVendorMap());
            modelBuilder.Configurations.Add(new IntlPoWarehouseMasterMap());
            modelBuilder.Configurations.Add(new IP2CountryMap());
            modelBuilder.Configurations.Add(new DeliveryChargeMap());
            modelBuilder.Configurations.Add(new JobApplicationMap());
            modelBuilder.Configurations.Add(new JobCardMasterMap());
            modelBuilder.Configurations.Add(new JobCardMasterLogMap());
            modelBuilder.Configurations.Add(new JobMasterMap());
            modelBuilder.Configurations.Add(new KuwaitNumberMap());
            modelBuilder.Configurations.Add(new KuwaitNumbers2014Map());
            modelBuilder.Configurations.Add(new LoyaltyPointSlabMasterMap());
            modelBuilder.Configurations.Add(new MarketingBannerClassificationMap());
            modelBuilder.Configurations.Add(new MarketingBannerCommentMap());
            modelBuilder.Configurations.Add(new MarketingBannerConfigMap());
            modelBuilder.Configurations.Add(new MarketingBannerDetailMap());
            modelBuilder.Configurations.Add(new MarketingBannerLogMap());
            modelBuilder.Configurations.Add(new MarketingBannerMasterMap());
            modelBuilder.Configurations.Add(new MarketingEmailMap());
            modelBuilder.Configurations.Add(new MarketingEmail2015Map());
            modelBuilder.Configurations.Add(new MarketingEmail2015_v1Map());
            modelBuilder.Configurations.Add(new MarketingEmail2015TempMap());
            modelBuilder.Configurations.Add(new MarketingEmaillDetailMap());
            modelBuilder.Configurations.Add(new MarketingEmailLogMap());
            modelBuilder.Configurations.Add(new MarketingEmailMailGunMap());
            modelBuilder.Configurations.Add(new MiniMarketLogMap());
            modelBuilder.Configurations.Add(new ModuleMasterMap());
            modelBuilder.Configurations.Add(new ModuleMasterErpMap());
            modelBuilder.Configurations.Add(new ModuleMasterErpRightMap());
            modelBuilder.Configurations.Add(new ModuleRightMap());
            modelBuilder.Configurations.Add(new NewsletterMap());
            modelBuilder.Configurations.Add(new NewsLetterJobEmailMap());
            modelBuilder.Configurations.Add(new NewsletterJobMap());
            modelBuilder.Configurations.Add(new NewsLetterLoggedMessageMap());
            modelBuilder.Configurations.Add(new NewsletterSubscriberMap());
            modelBuilder.Configurations.Add(new NewsLetterTemplateMap());
            modelBuilder.Configurations.Add(new NewsMasterMap());
            modelBuilder.Configurations.Add(new OfflineCustomerMasterMap());
            modelBuilder.Configurations.Add(new OrderAttributeMap());
            modelBuilder.Configurations.Add(new OrderCommentMap());
            modelBuilder.Configurations.Add(new OrderDetailsLoyaltyPointMap());
            modelBuilder.Configurations.Add(new OrderGiftItemMap());
            modelBuilder.Configurations.Add(new OrderItemMap());
            modelBuilder.Configurations.Add(new OrderItemCancelMap());
            modelBuilder.Configurations.Add(new OrderItemCancelConfirmMap());
            modelBuilder.Configurations.Add(new OrderItemReturnMap());
            modelBuilder.Configurations.Add(new OrderItemReturnConfirmMap());
            modelBuilder.Configurations.Add(new OrderLogMap());
            modelBuilder.Configurations.Add(new OrderLogCodMap());
            modelBuilder.Configurations.Add(new OrderLogMissionMap());
            modelBuilder.Configurations.Add(new OrderMasterMap());
            modelBuilder.Configurations.Add(new OrderMasterAddressLogMap());
            modelBuilder.Configurations.Add(new OrderMasterCODMap());
            modelBuilder.Configurations.Add(new OrderMasterCoutureEmailLogMap());
            modelBuilder.Configurations.Add(new OrderMasterPurchaseEmailLogMap());
            modelBuilder.Configurations.Add(new OrderMissionDetailMap());
            modelBuilder.Configurations.Add(new OrderMissionMasterMap());
            modelBuilder.Configurations.Add(new OrderMissionReturnExchangeLogMap());
            modelBuilder.Configurations.Add(new OrderMissionReturnExchangeMasterMap());
            modelBuilder.Configurations.Add(new OrderMissonReturnExchangeDetailMap());
            modelBuilder.Configurations.Add(new OrderOfflineLogMap());
            modelBuilder.Configurations.Add(new OrderOfflineMasterMap());
            modelBuilder.Configurations.Add(new OrderOfflineMissionDetailMap());
            modelBuilder.Configurations.Add(new OrderOfflineMissionMasterMap());
            modelBuilder.Configurations.Add(new OrderPaymentMap());
            modelBuilder.Configurations.Add(new OrderReturnExchangeMap());
            modelBuilder.Configurations.Add(new OrderSizeMap());
            modelBuilder.Configurations.Add(new OrderStatusMasterMap());
            modelBuilder.Configurations.Add(new OrderTransactionMap());
            modelBuilder.Configurations.Add(new TransactionStatusMap());
            modelBuilder.Configurations.Add(new PaymentDetailMap());
            modelBuilder.Configurations.Add(new PaymentDetailsLogMap());
            modelBuilder.Configurations.Add(new PaymentDetailsMasterVisaMap());
            modelBuilder.Configurations.Add(new PaymentDetailsPayPalMap());
            modelBuilder.Configurations.Add(new PaymentDetailsTheFortMap());
            modelBuilder.Configurations.Add(new PayPalIPNTranMap());
            modelBuilder.Configurations.Add(new PoTrackingDetailMap());
            modelBuilder.Configurations.Add(new PoTrackingMasterMap());
            modelBuilder.Configurations.Add(new PoTrackingWorkflowMap());
            modelBuilder.Configurations.Add(new ProductBarcodeBatchMap());
            modelBuilder.Configurations.Add(new ProductBarcodeBatchLogMap());
            modelBuilder.Configurations.Add(new ProductCategoryMapMap());
            modelBuilder.Configurations.Add(new ProductSKUCultureDataMap());
            modelBuilder.Configurations.Add(new ProductCoutureNewArrivalMap());
            modelBuilder.Configurations.Add(new ProductDayDealMap());
            modelBuilder.Configurations.Add(new ProductDayDealBlockMap());
            modelBuilder.Configurations.Add(new ProductDayDealBlocksProductMap());
            modelBuilder.Configurations.Add(new ProductDayDealLogMap());
            modelBuilder.Configurations.Add(new ProductDeliveryDaysMasterMap());
            modelBuilder.Configurations.Add(new ProductDetailMap());
            modelBuilder.Configurations.Add(new ProductDigitalMap());
            modelBuilder.Configurations.Add(new ProductDigitalReturnMap());
            modelBuilder.Configurations.Add(new ProductDigitalRevertLogMap());
            modelBuilder.Configurations.Add(new ProductDisableQueryBatchMap());
            modelBuilder.Configurations.Add(new ProductDisableQueryBatchLogMap());
            modelBuilder.Configurations.Add(new ProductGalleryMap());
            modelBuilder.Configurations.Add(new ProductHomePageMap());
            modelBuilder.Configurations.Add(new ProductItunesLogMap());
            modelBuilder.Configurations.Add(new ProductListingVisitLogMap());
            modelBuilder.Configurations.Add(new ProductListingVisitLogIntlMap());
            modelBuilder.Configurations.Add(new ProductLocationBatchMap());
            modelBuilder.Configurations.Add(new ProductLocationBatchLogMap());
            modelBuilder.Configurations.Add(new ProductManagerMap());
            modelBuilder.Configurations.Add(new ProductManagerBatchMap());
            modelBuilder.Configurations.Add(new ProductManagerBatchLogMap());
            modelBuilder.Configurations.Add(new ProductManagerEmailLogMap());
            modelBuilder.Configurations.Add(new ProductMasterMap());
            modelBuilder.Configurations.Add(new ProductMasterArabicLogMap());
            modelBuilder.Configurations.Add(new ProductMasterIntlMap());
            modelBuilder.Configurations.Add(new ProductMasterIntlLogMap());
            modelBuilder.Configurations.Add(new ProductMasterLogMap());
            modelBuilder.Configurations.Add(new ProductMasterStockMovementMap());
            modelBuilder.Configurations.Add(new ProductMasterSupplierLogMap());
            modelBuilder.Configurations.Add(new ProductNewArrivalMap());
            modelBuilder.Configurations.Add(new ProductNotificationMap());
            modelBuilder.Configurations.Add(new ProductNotificationIntlMap());
            modelBuilder.Configurations.Add(new ProductOfferMap());
            modelBuilder.Configurations.Add(new ProductPointsMasterMap());
            modelBuilder.Configurations.Add(new ProductPromotionMap());
            modelBuilder.Configurations.Add(new ProductQuantityDiscountMap());
            modelBuilder.Configurations.Add(new ProductRecommendMap());
            modelBuilder.Configurations.Add(new ProductSearchKeywordMap());
            modelBuilder.Configurations.Add(new ProductSearchKeywordsArMap());
            modelBuilder.Configurations.Add(new ProductSkusLogMap());
            modelBuilder.Configurations.Add(new ProductStatusBatchMap());
            modelBuilder.Configurations.Add(new ProductStatusBatchLogMap());
            modelBuilder.Configurations.Add(new ProductStockBatchMap());
            modelBuilder.Configurations.Add(new ProductStockBatchLogMap());
            modelBuilder.Configurations.Add(new ProductStockBatchPartNoMap());
            modelBuilder.Configurations.Add(new ProductStockBatchPartNoLogMap());
            modelBuilder.Configurations.Add(new ProductUsedMap());
            modelBuilder.Configurations.Add(new ProductVisitLogMap());
            modelBuilder.Configurations.Add(new ProductVisitLogIntlMap());
            modelBuilder.Configurations.Add(new ProductVoucherSettingMap());
            modelBuilder.Configurations.Add(new ProductWeightBatchMap());
            modelBuilder.Configurations.Add(new ProductWeightBatchLogMap());
            modelBuilder.Configurations.Add(new PublicHolidayMap());
            modelBuilder.Configurations.Add(new SalesReportCoutureLogMap());
            modelBuilder.Configurations.Add(new SearchKeywordMap());
            modelBuilder.Configurations.Add(new ServiceSupportMap());
            modelBuilder.Configurations.Add(new ServiceSupportCommentMap());
            modelBuilder.Configurations.Add(new ShoppingCartGiftItemMap());
            modelBuilder.Configurations.Add(new ShoppingCartItemMap());
            modelBuilder.Configurations.Add(new ShoppingCartItemLockedMap());
            modelBuilder.Configurations.Add(new ShoppingCartItemLogMap());
            modelBuilder.Configurations.Add(new ShoppingCartPaymentMap());
            modelBuilder.Configurations.Add(new ShoppingCartSaveforLaterMap());
            modelBuilder.Configurations.Add(new ShoppingCartSummaryMap());
            modelBuilder.Configurations.Add(new ShoppingCartTransLockedMap());
            modelBuilder.Configurations.Add(new SpecialOfferBannerMasterMap());
            modelBuilder.Configurations.Add(new SupplierMasterMap());
            modelBuilder.Configurations.Add(new SupplierMasterTypeMap());
            modelBuilder.Configurations.Add(new SupplierPurchaseOrderMap());
            modelBuilder.Configurations.Add(new SupplierPurchaseOrderDetailMap());
            modelBuilder.Configurations.Add(new SupplierPurchaseOrderLogMap());
            modelBuilder.Configurations.Add(new SupplierPurchaseOrderMasterMap());
            modelBuilder.Configurations.Add(new SupplierMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new UserAccessMap());
            modelBuilder.Configurations.Add(new UserLogMap());
            modelBuilder.Configurations.Add(new UserMasterMap());
            modelBuilder.Configurations.Add(new UserVisitLogMap());
            modelBuilder.Configurations.Add(new VehicleAccidentHistoryMap());
            modelBuilder.Configurations.Add(new VehicleMasterMap());
            modelBuilder.Configurations.Add(new VehicleServiceRecordMap());
            modelBuilder.Configurations.Add(new VoucherClaimMap());
            modelBuilder.Configurations.Add(new VoucherCustomerTranMap());
            modelBuilder.Configurations.Add(new VoucherMasterMap());
            modelBuilder.Configurations.Add(new VoucherMasterDetailMap());
            modelBuilder.Configurations.Add(new VoucherTransactionMap());
            modelBuilder.Configurations.Add(new VoucherWalletTransactionMap());
            modelBuilder.Configurations.Add(new VoucherValidityLogMap());
            modelBuilder.Configurations.Add(new WebConfigMap());
            modelBuilder.Configurations.Add(new WebConfigAppMap());
            modelBuilder.Configurations.Add(new WebsiteLogMap());
            modelBuilder.Configurations.Add(new WalletCustomerLogMap());
            modelBuilder.Configurations.Add(new WalletStatusMasterMap());
            modelBuilder.Configurations.Add(new WalletTransactionDetailMap());
            modelBuilder.Configurations.Add(new WalletTransactionMasterMap());
            modelBuilder.Configurations.Add(new View_DuplicateMap());
            modelBuilder.Configurations.Add(new LanguageMap());
            modelBuilder.Configurations.Add(new BrandCultureDataMap());
            modelBuilder.Configurations.Add(new BrandMap());
            modelBuilder.Configurations.Add(new BrandStatusMap());
            modelBuilder.Configurations.Add(new CategoryCultureDataMap());
            modelBuilder.Configurations.Add(new ProductBundleMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new CultureDataMap());
            modelBuilder.Configurations.Add(new CultureMap());
            modelBuilder.Configurations.Add(new ProductCultureDataMap());
            modelBuilder.Configurations.Add(new ProductCutlureDataMap());
            modelBuilder.Configurations.Add(new ProductFamilyCultureDataMap());
            modelBuilder.Configurations.Add(new ProductFamilyMap());
            modelBuilder.Configurations.Add(new ProductFamilyPropertyMapMap());
            modelBuilder.Configurations.Add(new ProductFamilyPropertyTypeMapMap());
            modelBuilder.Configurations.Add(new ProductFamilyTypeMap());
            modelBuilder.Configurations.Add(new ProductInventoryConfigMap());
            modelBuilder.Configurations.Add(new ProductInventoryMap());
            modelBuilder.Configurations.Add(new ProductMap());

            modelBuilder.Configurations.Add(new ProductPriceListCultureDataMap());
            modelBuilder.Configurations.Add(new ProductPriceListCustomerMapMap());
            modelBuilder.Configurations.Add(new ProductPriceListMap());
            modelBuilder.Configurations.Add(new ProductPriceListBrandMapMap());
            modelBuilder.Configurations.Add(new ProductPriceListCategoryMapMap());
            modelBuilder.Configurations.Add(new ProductPriceListCustomerGroupMapMap());
            modelBuilder.Configurations.Add(new ProductPriceListProductMapMap());
            modelBuilder.Configurations.Add(new ProductPriceListProductQuantityMapMap());
            modelBuilder.Configurations.Add(new ProductPriceListSKUMapMap());
            modelBuilder.Configurations.Add(new ProductPriceListSKUQuantityMapMap());

            modelBuilder.Configurations.Add(new ProductPropertyMapMap());
            modelBuilder.Configurations.Add(new ProductTypeMap());
            modelBuilder.Configurations.Add(new ProductVideoMapMap());
            modelBuilder.Configurations.Add(new PropertyCultureDataMap());
            modelBuilder.Configurations.Add(new PropertyMap());
            modelBuilder.Configurations.Add(new SalesRelationshipTypeMap());
            modelBuilder.Configurations.Add(new SeoMetadataMap());
            modelBuilder.Configurations.Add(new UIControlTypeMap());
            modelBuilder.Configurations.Add(new UIControlValidationMap());
            modelBuilder.Configurations.Add(new UnitMap());
            modelBuilder.Configurations.Add(new UnitGroupMap());
            modelBuilder.Configurations.Add(new TransactionDetailMap());
            modelBuilder.Configurations.Add(new TransactionHeadMap());
            modelBuilder.Configurations.Add(new DocumentReferenceTypeMap());
            modelBuilder.Configurations.Add(new DocumentTypeMap());
            modelBuilder.Configurations.Add(new Eduegate.Domain.Entity.Models.Mapping.DocumentTypeTypeMap());
            modelBuilder.Configurations.Add(new SettingMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new TestSearchViewMap());
            modelBuilder.Configurations.Add(new ViewColumnMap());
            modelBuilder.Configurations.Add(new ViewMap());
            modelBuilder.Configurations.Add(new MenuLinkMap());
            modelBuilder.Configurations.Add(new MenuLinkCategoryMapMap());
            modelBuilder.Configurations.Add(new MenuLinkCultureDataMap());
            modelBuilder.Configurations.Add(new Eduegate.Domain.Entity.Models.Mapping.CategoryBannerMasterMap());
            modelBuilder.Configurations.Add(new ProductImageMapMap());
            modelBuilder.Configurations.Add(new ProductSKUMapMap());
            modelBuilder.Configurations.Add(new PropertyTypeMap());
            modelBuilder.Configurations.Add(new ProductStatuMap());
            modelBuilder.Configurations.Add(new WishListMap());
            modelBuilder.Configurations.Add(new MenuLinkBrandMapMap());
            modelBuilder.Configurations.Add(new NewsMap());
            modelBuilder.Configurations.Add(new NewsTypeMap());
            modelBuilder.Configurations.Add(new StaticContentDataMap());
            modelBuilder.Configurations.Add(new StaticContentTypeMap());
            modelBuilder.Configurations.Add(new ShoppingCartMap());
            modelBuilder.Configurations.Add(new ShoppingCartVoucherMapMap());
            modelBuilder.Configurations.Add(new UserViewMap());
            modelBuilder.Configurations.Add(new ViewFilterMap());
            modelBuilder.Configurations.Add(new FilterColumnMap());
            modelBuilder.Configurations.Add(new UserViewFilterMapMap());
            modelBuilder.Configurations.Add(new ViewTypeMap());
            modelBuilder.Configurations.Add(new FilterColumnConditionMapMap());
            modelBuilder.Configurations.Add(new SubscriptionMap());
            modelBuilder.Configurations.Add(new VoucherMap());
            modelBuilder.Configurations.Add(new VoucherStatusMap());
            modelBuilder.Configurations.Add(new VoucherTypeMap());

            modelBuilder.Configurations.Add(new FilterColumnUserValueMap());
            modelBuilder.Configurations.Add(new BannerMap());
            modelBuilder.Configurations.Add(new BannerStatusMap());
            modelBuilder.Configurations.Add(new BannerTypeMap());
            modelBuilder.Configurations.Add(new CustomerGroupMap());

            modelBuilder.Configurations.Add(new TransactionHeadShoppingCartMapMap());
            modelBuilder.Configurations.Add(new PaymentDetailsKnetMap());
            modelBuilder.Configurations.Add(new PaymentMasterVisaMap());

            modelBuilder.Configurations.Add(new NotificationQueueParentMapMap());
            modelBuilder.Configurations.Add(new NotificationStatusMap());
            modelBuilder.Configurations.Add(new NotificationLogMap());
            modelBuilder.Configurations.Add(new NotificationsProcessingMap());
            modelBuilder.Configurations.Add(new NotificationsQueueMap());
            modelBuilder.Configurations.Add(new NotificationTypeMap());
            modelBuilder.Configurations.Add(new AlertStatusMap());

            modelBuilder.Configurations.Add(new GetSKUByPropertyMap());

            modelBuilder.Configurations.Add(new InvetoryTransactionMap());
            modelBuilder.Configurations.Add(new EmailNotificationDataMap());
            modelBuilder.Configurations.Add(new EmailNotificationTypeMap());
            modelBuilder.Configurations.Add(new StatusMap());
            modelBuilder.Configurations.Add(new StatusesCultureDataMap());
            modelBuilder.Configurations.Add(new StockNotificationMap());
            modelBuilder.Configurations.Add(new StockNotificationStatusMap());
            modelBuilder.Configurations.Add(new OrderContactMapMap());
            modelBuilder.Configurations.Add(new OrderTrackingMap());

            modelBuilder.Configurations.Add(new ProductToProductMapMap());

            modelBuilder.Configurations.Add(new EntityChangeTrackerMap());
            modelBuilder.Configurations.Add(new EntityChangeTrackersInProcessMap());
            modelBuilder.Configurations.Add(new EntityChangeTrackersQueueMap());
            modelBuilder.Configurations.Add(new OperationTypeMap());
            modelBuilder.Configurations.Add(new SyncEntityMap());
            modelBuilder.Configurations.Add(new TrackerStatusMap());
            modelBuilder.Configurations.Add(new SyncFieldMapMap());
            modelBuilder.Configurations.Add(new SyncFieldMapTypeMap());

            modelBuilder.Configurations.Add(new LoginStatusMap());
            modelBuilder.Configurations.Add(new WarehouseMap());
            modelBuilder.Configurations.Add(new BranchMap());
            modelBuilder.Configurations.Add(new BranchGroupMap());
            modelBuilder.Configurations.Add(new CompanyMap());
            modelBuilder.Configurations.Add(new CompanyCurrencyMapMap());
            modelBuilder.Configurations.Add(new DepartmentMap());
            modelBuilder.Configurations.Add(new EmployeeMap());
            modelBuilder.Configurations.Add(new vwProductNewArrivalMap());
            modelBuilder.Configurations.Add(new vwProductOrderByDateMap());
            modelBuilder.Configurations.Add(new Eduegate.Domain.Entity.Models.Mapping.ProductCategoryMap());

            modelBuilder.Configurations.Add(new ProductInventoryProductConfigMapMap());
            modelBuilder.Configurations.Add(new ProductInventorySKUConfigMapMap());
            modelBuilder.Configurations.Add(new ProductPriceListBranchMapMap());
            modelBuilder.Configurations.Add(new GenderMap());
            modelBuilder.Configurations.Add(new MaritalStatusMap());
            modelBuilder.Configurations.Add(new DesignationMap());
            modelBuilder.Configurations.Add(new Eduegate.Domain.Entity.Models.Mapping.EmployeeRoleMap());
            modelBuilder.Configurations.Add(new JobTypeMap());
            modelBuilder.Configurations.Add(new SeoMetadataCultureDataMap());
            modelBuilder.Configurations.Add(new DeliveryTypeMap());

            modelBuilder.Configurations.Add(new UserViewColumnMapMap());
            modelBuilder.Configurations.Add(new RelationTypeMap());
            modelBuilder.Configurations.Add(new EmployeeCatalogRelationMap());
            modelBuilder.Configurations.Add(new EntitlementMapMap());
            modelBuilder.Configurations.Add(new EntityPropertiesMap());
            modelBuilder.Configurations.Add(new EntityPropertyMapMap());
            modelBuilder.Configurations.Add(new EntityPropertyTypeMap());
            modelBuilder.Configurations.Add(new EntityTypeEntitlementMap());
            modelBuilder.Configurations.Add(new EntityTypePaymentMethodMapMap());
            modelBuilder.Configurations.Add(new EntityTypeRelationMapMap());
            modelBuilder.Configurations.Add(new EntityTypeMap());
            modelBuilder.Configurations.Add(new BranchStatusMap());
            modelBuilder.Configurations.Add(new CompanyStatusMap());
            modelBuilder.Configurations.Add(new WarehouseStatusMap());
            modelBuilder.Configurations.Add(new BranchGroupStatusMap());
            modelBuilder.Configurations.Add(new DepartmentStatusMap());
            modelBuilder.Configurations.Add(new CustomerStatusMap());
            modelBuilder.Configurations.Add(new PaymentMethodMap());
            modelBuilder.Configurations.Add(new PackingTypeMap());
            modelBuilder.Configurations.Add(new CustomerSupplierMapMap());

            modelBuilder.Configurations.Add(new CommentMap());
            modelBuilder.Configurations.Add(new DocumentFileMap());
            modelBuilder.Configurations.Add(new DocumentFileStatusMap());

            modelBuilder.Configurations.Add(new EntityChangeTrackerLogMap());

            //modelBuilder.Configurations.Add(new SeoMetadataCultureDataMap());
            modelBuilder.Configurations.Add(new CurrencyMap());
            modelBuilder.Configurations.Add(new SupplierStatusMap());
            modelBuilder.Configurations.Add(new ProductPriceListTypeMap());
            modelBuilder.Configurations.Add(new ProductPriceListLevelMap());
            modelBuilder.Configurations.Add(new CustomerProductReferenceMap());

            modelBuilder.Configurations.Add(new TransactionShipmentMap());
            modelBuilder.Configurations.Add(new TransactionAllocationMap());
            modelBuilder.Configurations.Add(new ProductPriceListSupplierMapMap());

            modelBuilder.Configurations.Add(new ClaimLoginMapMap());
            modelBuilder.Configurations.Add(new ClaimMap());
            modelBuilder.Configurations.Add(new ClaimSetClaimMapMap());
            modelBuilder.Configurations.Add(new ClaimSetClaimSetMapMap());
            modelBuilder.Configurations.Add(new ClaimSetLoginMapMap());
            modelBuilder.Configurations.Add(new ClaimSetMap());
            modelBuilder.Configurations.Add(new ClaimTypeMap());
            modelBuilder.Configurations.Add(new ProductSerialMapMap());

            modelBuilder.Configurations.Add(new JobEntryDetailMap());
            modelBuilder.Configurations.Add(new JobEntryHeadMap());
            modelBuilder.Configurations.Add(new JobStatusMap());
            modelBuilder.Configurations.Add(new JobActivityMap());
            modelBuilder.Configurations.Add(new PriorityMap());
            modelBuilder.Configurations.Add(new JobOperationStatusMap());

            modelBuilder.Configurations.Add(new vwProductNewArrivalsIntlMap());
            modelBuilder.Configurations.Add(new vwProductListSearchListingIntlMap());
            modelBuilder.Configurations.Add(new vwProductOrderByDateIntlMap());
            modelBuilder.Configurations.Add(new ProductSearchKeywordsArIntlMap());
            modelBuilder.Configurations.Add(new ProductSearchKeywordsIntlMap());
            modelBuilder.Configurations.Add(new LocationMap());
            modelBuilder.Configurations.Add(new LocationTypeMap());
            modelBuilder.Configurations.Add(new ProductLocationMapMap());
            modelBuilder.Configurations.Add(new BasketMap());
            modelBuilder.Configurations.Add(new VehicleOwnershipTypeMap());
            modelBuilder.Configurations.Add(new VehicleMap());
            modelBuilder.Configurations.Add(new VehicleTypeMap());
            modelBuilder.Configurations.Add(new CityMap());
            modelBuilder.Configurations.Add(new ZoneMap());
            modelBuilder.Configurations.Add(new AreaMap());
            modelBuilder.Configurations.Add(new RouteMap());

            modelBuilder.Configurations.Add(new CategoryImageMapMap());
            modelBuilder.Configurations.Add(new ImageTypeMap());

            modelBuilder.Configurations.Add(new PageMap());
            modelBuilder.Configurations.Add(new PageTypeMap());
            modelBuilder.Configurations.Add(new BoilerPlateMap());
            modelBuilder.Configurations.Add(new PageBoilerplateMapMap());
            modelBuilder.Configurations.Add(new PageBoilerplateMapParameterMap());

            modelBuilder.Configurations.Add(new InventoryVerificationMap());
            modelBuilder.Configurations.Add(new SiteMap());
            modelBuilder.Configurations.Add(new ServiceProviderMap());
            modelBuilder.Configurations.Add(new ServiceProviderLogMap());
            modelBuilder.Configurations.Add(new ServiceProviderSettingMap());
            modelBuilder.Configurations.Add(new ProductSKUVariantMap());
            modelBuilder.Configurations.Add(new PropertyTypeCultureDataMap());

            modelBuilder.Configurations.Add(new DataFormatMap());
            modelBuilder.Configurations.Add(new DataFormatTypeMap());
            modelBuilder.Configurations.Add(new NotificationAlertMap());
            modelBuilder.Configurations.Add(new UserDataFormatMapMap());
            modelBuilder.Configurations.Add(new CustomerSettingMap());
            modelBuilder.Configurations.Add(new KnowHowOptionMap());

            modelBuilder.Configurations.Add(new AccountBehavoirMap());
            modelBuilder.Configurations.Add(new AccountMap());
            modelBuilder.Configurations.Add(new GroupMap());

            modelBuilder.Configurations.Add(new Eduegate.Domain.Entity.Models.Mapping.ChartOfAccountMap());
            modelBuilder.Configurations.Add(new ChartOfAccountMapMap());
            modelBuilder.Configurations.Add(new ChartRowTypeMap());

            modelBuilder.Configurations.Add(new SupportActionMap());
            modelBuilder.Configurations.Add(new TicketPriorityMap());
            modelBuilder.Configurations.Add(new TicketMap());
            modelBuilder.Configurations.Add(new TicketStatusMap());
            modelBuilder.Configurations.Add(new TicketTagMap());
            modelBuilder.Configurations.Add(new TicketReasonMap());
            modelBuilder.Configurations.Add(new TicketProductMapMap());
            modelBuilder.Configurations.Add(new TicketActionDetailDetailMapMap());
            modelBuilder.Configurations.Add(new TicketActionDetailMapMap());

            modelBuilder.Configurations.Add(new ProductDeliveryCountrySettingMap());
            modelBuilder.Configurations.Add(new BoilerPlateParameterMap());
            modelBuilder.Configurations.Add(new TitleMap());
            modelBuilder.Configurations.Add(new EmployeeRoleMapMap());
            modelBuilder.Configurations.Add(new JobSizeMap());
            modelBuilder.Configurations.Add(new ProductInventorySerialMapMap());

            modelBuilder.Configurations.Add(new EntitySchedulerMap());
            modelBuilder.Configurations.Add(new SchedulerEntityTypeMap());
            modelBuilder.Configurations.Add(new SchedulerTypeMap());
            modelBuilder.Configurations.Add(new PropertyTypePropertyMapMap());


            modelBuilder.Configurations.Add(new DeliveryTypeAllowedAreaMapMap());
            modelBuilder.Configurations.Add(new DeliveryTypeAllowedCountryMapMap());
            modelBuilder.Configurations.Add(new DeliveryTypes1Map());
            modelBuilder.Configurations.Add(new DeliveryTypeStatusMap());
            modelBuilder.Configurations.Add(new DeliveryTypeAllowedZoneMapMap());
            modelBuilder.Configurations.Add(new ProductDeliveryTypeMapMap());
            modelBuilder.Configurations.Add(new BrandPriceListMapMap());
            modelBuilder.Configurations.Add(new CustomerGroupDeliveryTypeMapMap());
            modelBuilder.Configurations.Add(new CategoryPriceListMapMap());
            modelBuilder.Configurations.Add(new DeliveryTypeTimeSlotMapMap());
            modelBuilder.Configurations.Add(new ProductTypeDeliveryTypeMapMap());
            modelBuilder.Configurations.Add(new TransactionHeadEntitlementMapMap());
            modelBuilder.Configurations.Add(new TransactionHeadPointsMapMap());
            modelBuilder.Configurations.Add(new NewsletterSubscriptionMap());

            modelBuilder.Configurations.Add(new Mapping.BrandTagMapMap());
            modelBuilder.Configurations.Add(new Mapping.BrandTagMap());
            modelBuilder.Configurations.Add(new Mapping.CategoryTagMapMap());
            modelBuilder.Configurations.Add(new Mapping.CategoryTagMap());
            modelBuilder.Configurations.Add(new Mapping.ProductSKUTagMapMap());
            modelBuilder.Configurations.Add(new Mapping.ProductSKUTagMap());
            modelBuilder.Configurations.Add(new Mapping.ProductTagMapMap());
            modelBuilder.Configurations.Add(new Mapping.ProductTagMap());
            modelBuilder.Configurations.Add(new Mapping.BrandImageMapMap());
            modelBuilder.Configurations.Add(new BranchDocumentTypeMapMap());
            modelBuilder.Configurations.Add(new CustomerSupportTicketMap());
            modelBuilder.Configurations.Add(new UserJobApplicationMap());
            modelBuilder.Configurations.Add(new PaymentMethodSiteMapMap());
            modelBuilder.Configurations.Add(new SKUPaymentMethodExceptionMapMap());
            modelBuilder.Configurations.Add(new ProductInventoryConfigCultureDataMap());
            modelBuilder.Configurations.Add(new PageBoilerplateMapParameterCultureDataMap());
            modelBuilder.Configurations.Add(new DocumentReferenceStatusMapMap());
            modelBuilder.Configurations.Add(new DocumentStatusMap());

            modelBuilder.Configurations.Add(new AlertTypeMap());
            modelBuilder.Configurations.Add(new ActionLinkTypeMap());
            modelBuilder.Configurations.Add(new OrderRoleTrackingMap());

            modelBuilder.Configurations.Add(new JobStatusOperationMapMap());
            modelBuilder.Configurations.Add(new DataHistoryEntityMap());
            modelBuilder.Configurations.Add(new SMSNotificationDataMap());
            modelBuilder.Configurations.Add(new SMSNotificationTypeMap());

            modelBuilder.Configurations.Add(new PayableMap());
            modelBuilder.Configurations.Add(new ReceivableMap());
            modelBuilder.Configurations.Add(new JobsEntryHeadPayableMapMap());
            modelBuilder.Configurations.Add(new JobsEntryHeadReceivableMapMap());
            modelBuilder.Configurations.Add(new TransactionHeadPayablesMapMap());
            modelBuilder.Configurations.Add(new TransactionHeadReceivablesMapMap());
            modelBuilder.Configurations.Add(new DocumentTypeTransactionNumberMap());
            modelBuilder.Configurations.Add(new AccountTransactionDetailMap());
            modelBuilder.Configurations.Add(new AccountTransactionHeadMap());
            modelBuilder.Configurations.Add(new AssetTransactionHeadAccountMapMap());
            modelBuilder.Configurations.Add(new AccountTransactionHeadAccountMapMap());
            modelBuilder.Configurations.Add(new AccountTransactionInventoryHeadMapMap());
            modelBuilder.Configurations.Add(new PaymentGroupMap());
            modelBuilder.Configurations.Add(new NotifyMap());
            modelBuilder.Configurations.Add(new ProductSKUSiteMapMap());
            modelBuilder.Configurations.Add(new DeliveryTypeTimeSlotMapsCultureMap());
            modelBuilder.Configurations.Add(new OrderDeliveryDisplayHeadMapMap());
            modelBuilder.Configurations.Add(new ReceivingMethodMap());
            modelBuilder.Configurations.Add(new ReturnMethodMap());
            modelBuilder.Configurations.Add(new CategorySettingMap());

            modelBuilder.Configurations.Add(new ScreenMetadataMap());
            modelBuilder.Configurations.Add(new Mapping.Settings.ScreenShortCutMap());
            modelBuilder.Configurations.Add(new ScreenLookupMapMap());
            modelBuilder.Configurations.Add(new LookupMap());

            modelBuilder.Configurations.Add(new PaymentModeMap());
            modelBuilder.Configurations.Add(new EmployeeAccountMapMap());

            modelBuilder.Configurations.Add(new AttachmentMap());

            modelBuilder.Configurations.Add(new TaxMap());
            modelBuilder.Configurations.Add(new TaxStatusMap());
            modelBuilder.Configurations.Add(new TaxTypeMap());
            modelBuilder.Configurations.Add(new TaxTemplateMap());
            modelBuilder.Configurations.Add(new TaxTemplateItemMap());
            modelBuilder.Configurations.Add(new TaxTransactionMap());
            modelBuilder.Configurations.Add(new AccountTaxTransactionMap());

            modelBuilder.Configurations.Add(new ViewActionMap());
            modelBuilder.Configurations.Add(new CostCenterMap());

            modelBuilder.Configurations.Add(new GeoLocationLogMap());

            modelBuilder.Entity<FilterColumn>()
              .HasMany(e => e.FilterColumnCultureDatas)
              .WithRequired(e => e.FilterColumn)
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<Culture>()
                .HasMany(e => e.FilterColumnCultureDatas)
                .WithRequired(e => e.Culture)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ViewColumn>()
              .HasMany(e => e.ViewColumnCultureDatas)
              .WithRequired(e => e.ViewColumn)
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<Culture>()
              .HasMany(e => e.ViewColumnCultureDatas)
              .WithRequired(e => e.Culture)
              .WillCascadeOnDelete(false);

            //adding amount as to save values to 3 decimal places
            modelBuilder.Entity<WalletTransactionDetail>().Property(WalletTransactionDetail => WalletTransactionDetail.Amount).HasPrecision(18, 3);
            modelBuilder.Entity<AssetTransactionDetail>().Property(assetTransactionDetail => assetTransactionDetail.Amount).HasPrecision(18, 3);


            modelBuilder.Entity<TransactionHead>()
             .HasMany(e => e.WorkflowTransactionHeadMaps)
             .WithOptional(e => e.TransactionHead)
             .HasForeignKey(e => e.TransactionHeadID);

            modelBuilder.Entity<EntityType>()
             .HasMany(e => e.Workflows)
             .WithOptional(e => e.EntityType)
             .HasForeignKey(e => e.LinkedEntityTypeID);

            modelBuilder.Entity<WorkflowCondition>()
                .Property(e => e.ConditionType)
                .IsUnicode(false);

            modelBuilder.Entity<WorkflowCondition>()
                .HasMany(e => e.WorkflowRuleConditions)
                .WithOptional(e => e.WorkflowCondition)
                .HasForeignKey(e => e.ConditionID);

            modelBuilder.Entity<WorkflowCondition>()
                .HasMany(e => e.WorkflowRules)
                .WithOptional(e => e.WorkflowCondition)
                .HasForeignKey(e => e.ConditionID);

            modelBuilder.Entity<WorkflowFiled>()
                .Property(e => e.PhysicalColumnName)
                .IsUnicode(false);

            modelBuilder.Entity<WorkflowFiled>()
                .HasMany(e => e.Workflows)
                .WithOptional(e => e.WorkflowFiled)
                .HasForeignKey(e => e.WorkflowApplyFieldID);

            modelBuilder.Entity<WorkflowRuleCondition>()
                .HasMany(e => e.WorkflowRuleApprovers)
                .WithRequired(e => e.WorkflowRuleCondition)
                .HasForeignKey(e => e.WorkflowRuleConditionID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WorkflowRuleCondition>()
                .HasMany(e => e.WorkflowTransactionRuleApproverMaps)
                .WithOptional(e => e.WorkflowRuleCondition)
                .HasForeignKey(e => e.WorkflowRuleConditionID);

            modelBuilder.Entity<WorkflowRule>()
                .HasMany(e => e.WorkflowRuleConditions)
                .WithOptional(e => e.WorkflowRule)
                .HasForeignKey(e => e.WorkflowRuleID);

            modelBuilder.Entity<WorkflowRule>()
                .HasMany(e => e.WorkflowTransactionHeadRuleMaps)
                .WithOptional(e => e.WorkflowRule)
                .HasForeignKey(e => e.WorkflowRuleID);

            modelBuilder.Entity<Workflow>()
                .HasMany(e => e.WorkflowRules)
                .WithOptional(e => e.Workflow)
                .HasForeignKey(e => e.WorkflowID);

            modelBuilder.Entity<Workflow>()
                .HasMany(e => e.WorkflowTransactionHeadMaps)
                .WithOptional(e => e.Workflow)
                .HasForeignKey(e => e.WorkflowID);

            modelBuilder.Entity<WorkflowTransactionHeadMap>()
                .HasMany(e => e.WorkflowTransactionHeadRuleMaps)
                .WithOptional(e => e.WorkflowTransactionHeadMap)
                .HasForeignKey(e => e.WorkflowTransactionHeadMapID);

            modelBuilder.Entity<WorkflowTransactionHeadRuleMap>()
                .HasMany(e => e.WorkflowTransactionRuleApproverMaps)
                .WithOptional(e => e.WorkflowTransactionHeadRuleMap)
                .HasForeignKey(e => e.WorkflowTransactionHeadRuleMapID);

            //modelBuilder.Entity<EntityType>()
            //  .HasMany(e => e.Workflows)
            //  .WithOptional(e => e.EntityType)
            //  .HasForeignKey(e => e.LinkedEntityTypeID);

            modelBuilder.Entity<Login>()
               .HasMany(e => e.UserScreenFieldSettings)
               .WithOptional(e => e.Login)
               .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<ScreenField>()
               .Property(e => e.ModelName)
               .IsUnicode(false);

            modelBuilder.Entity<ScreenField>()
                .Property(e => e.PhysicalFieldName)
                .IsUnicode(false);

            modelBuilder.Entity<ScreenField>()
                .Property(e => e.LookupName)
                .IsUnicode(false);

            modelBuilder.Entity<ScreenField>()
                .Property(e => e.DateType)
                .IsUnicode(false);

            modelBuilder.Entity<Login>()
            .HasMany(e => e.UserSettings)
            .WithRequired(e => e.Login)
            .HasForeignKey(e => e.LoginID)
            .WillCascadeOnDelete(false);

            // modelBuilder.Entity<Sequence>()
            //.HasMany(e => e.ScreenFieldSettings)
            //.WithOptional(e => e.Sequence)
            //.HasForeignKey(e => e.SequenceID);

            modelBuilder.Entity<WorkflowLogMap>()
              .HasMany(e => e.WorkflowLogMapRuleMaps)
              .WithOptional(e => e.WorkflowLogMap)
              .HasForeignKey(e => e.WorkflowLogMapID);

            modelBuilder.Entity<WorkflowRule>()
               .HasMany(e => e.WorkflowLogMapRuleMaps)
               .WithOptional(e => e.WorkflowRule)
               .HasForeignKey(e => e.WorkflowRuleID);

            modelBuilder.Entity<Workflow>()
                .HasMany(e => e.WorkflowLogMaps)
                .WithOptional(e => e.Workflow)
                .HasForeignKey(e => e.WorkflowID);

            modelBuilder.Entity<Workflow>()
            .HasMany(e => e.WorkflowLogMaps)
            .WithOptional(e => e.Workflow)
            .HasForeignKey(e => e.WorkflowID);

            modelBuilder.Entity<WorkflowRule>()
                .HasMany(e => e.WorkflowLogMapRuleMaps)
                .WithOptional(e => e.WorkflowRule)
                .HasForeignKey(e => e.WorkflowRuleID);

            modelBuilder.Entity<WorkflowLogMapRuleMap>()
                .HasMany(e => e.WorkflowLogMapRuleApproverMaps)
                .WithOptional(e => e.WorkflowLogMapRuleMap)
                .HasForeignKey(e => e.WorkflowLogMapRuleMapID);

            modelBuilder.Entity<WorkflowLogMap>()
                .HasMany(e => e.WorkflowLogMapRuleMaps)
                .WithOptional(e => e.WorkflowLogMap)
                .HasForeignKey(e => e.WorkflowLogMapID);

            modelBuilder.Entity<WorkflowRuleCondition>()
                .HasMany(e => e.WorkflowLogMapRuleApproverMaps)
                .WithOptional(e => e.WorkflowRuleCondition)
                .HasForeignKey(e => e.WorkflowRuleConditionID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.WorkflowRuleApprovers)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<WorkflowRuleCondition>()
                .HasMany(e => e.WorkflowTransactionRuleApproverMaps)
                .WithOptional(e => e.WorkflowRuleCondition)
                .HasForeignKey(e => e.WorkflowRuleConditionID);

            modelBuilder.Entity<WorkflowCondition>()
                .HasMany(e => e.WorkflowRuleConditions)
                .WithOptional(e => e.WorkflowCondition)
                .HasForeignKey(e => e.ConditionID);

            modelBuilder.Entity<WorkflowCondition>()
                .HasMany(e => e.WorkflowRules)
                .WithOptional(e => e.WorkflowCondition)
                .HasForeignKey(e => e.ConditionID);


            modelBuilder.Entity<WorkflowRule>()
                .HasMany(e => e.WorkflowLogMapRuleMaps)
                .WithOptional(e => e.WorkflowRule)
                .HasForeignKey(e => e.WorkflowRuleID);

            modelBuilder.Entity<WorkflowRule>()
                .HasMany(e => e.WorkflowRuleConditions)
                .WithOptional(e => e.WorkflowRule)
                .HasForeignKey(e => e.WorkflowRuleID);

            modelBuilder.Entity<Product>()
              .HasMany(e => e.TransactionDetails)
              .WithOptional(e => e.Product)
              .HasForeignKey(e => e.ProductID);

            modelBuilder.Entity<ProductSKUMap>()
             .HasMany(e => e.TransactionDetails)
             .WithOptional(e => e.ProductSKUMap)
             .HasForeignKey(e => e.ProductSKUMapID);

            modelBuilder.Entity<TransactionDetail>()
                .HasMany(e => e.TransactionAllocations)
                .WithOptional(e => e.TransactionDetail)
                .HasForeignKey(e => e.TrasactionDetailID);

            modelBuilder.Entity<TransactionDetail>()
                .HasMany(e => e.TransactionDetails1)
                .WithOptional(e => e.TransactionDetail1)
                .HasForeignKey(e => e.ParentDetailID);



            modelBuilder.Entity<Login>()
             .HasMany(e => e.CustomerCards)
             .WithOptional(e => e.Login)
             .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<Employee>()
             .HasMany(e => e.EmployeeAdditionalInfos)
             .WithOptional(e => e.Employee)
             .HasForeignKey(e => e.EmployeeID);


            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Product>()
            //  .HasMany(e => e.ProductSKUMaps)
            //  .WithOptional(e => e.Product)
            //  .HasForeignKey(e => e.ProductID);

            modelBuilder.Entity<ProductSKUMap>()
               .HasMany(e => e.ProductSKURackMaps)
               .WithRequired(e => e.ProductSKUMap)
               .HasForeignKey(e => e.ProductSKUMapID)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rack>()
               .HasMany(e => e.ProductSKURackMaps)
               .WithRequired(e => e.Rack)
               .HasForeignKey(e => e.RackID)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.AccountTransactionHeads)
                .WithOptional(e => e.Branch)
                .HasForeignKey(e => e.BranchID);

            modelBuilder.Entity<PaymentMode>()
                .HasMany(e => e.AccountTransactionHeads)
                .WithOptional(e => e.PaymentMode)
                .HasForeignKey(e => e.PaymentModeID);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.ProductCategoryMaps)
                .WithOptional(e => e.Category)
                .HasForeignKey(e => e.CategoryID);

            modelBuilder.Entity<Product>()
               .HasMany(e => e.ProductCategoryMaps)
               .WithOptional(e => e.Product)
               .HasForeignKey(e => e.ProductID);

            modelBuilder.Entity<Product>()
                 .HasMany(e => e.ProductSKURackMaps)
                 .WithRequired(e => e.Product)
                 .HasForeignKey(e => e.ProductID)
                 .WillCascadeOnDelete(false);


            modelBuilder.Entity<ProductSKURackMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<TransactionHead>()
              .HasMany(e => e.TransactionHeadReceivablesMaps)
              .WithRequired(e => e.TransactionHead)
              .HasForeignKey(e => e.HeadID)
              .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Country>()
            //  .HasMany(e => e.Employees)
            //  .WithOptional(e => e.Country)
            //  .HasForeignKey(e => e.NationalityID);

            //modelBuilder.Entity<TransactionHead>()
            //   .HasMany(e => e.TransactionHeadAccountMaps)
            //   .WithOptional(e => e.TransactionHead)
            //   .HasForeignKey(e => e.TransactionHeadID);

            modelBuilder.Entity<Nationality>()
                .HasMany(e => e.Employees)
                .WithOptional(e => e.Nationality)
                .HasForeignKey(e => e.NationalityID);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.PassportVisaDetails)
                .WithOptional(e => e.Country)
                .HasForeignKey(e => e.CountryofIssueID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.PassportVisaDetails)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.ReferenceID);



            //modelBuilder.Entity<EmployeeBankDetail>()
            //    .Property(e => e.TimeStamps)
            //    .IsFixedLength();

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeBankDetails)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<Bank>()
                .HasMany(e => e.EmployeeBankDetails)
                .WithOptional(e => e.Bank)
                .HasForeignKey(e => e.BankID);

            modelBuilder.Entity<Lead>()
                .HasMany(e => e.Communications)
                .WithOptional(e => e.Lead)
                .HasForeignKey(e => e.LeadID);

            modelBuilder.Entity<Parent>()
                .HasMany(e => e.Students)
                .WithOptional(e => e.Parent)
                .HasForeignKey(e => e.ParentID);

            modelBuilder.Entity<Routes1>()
                .HasMany(e => e.StudentRouteStopMaps1)
                .WithOptional(e => e.Routes11)
                .HasForeignKey(e => e.PickupRouteID);

            modelBuilder.Entity<Routes1>()
                .HasMany(e => e.StudentRouteStopMaps)
                .WithOptional(e => e.Routes1)
                .HasForeignKey(e => e.DropStopRouteID);

            modelBuilder.Entity<Nationality>()
                .HasMany(e => e.Parents1)
                .WithOptional(e => e.Nationality1)
                .HasForeignKey(e => e.MotherCountryID);

            modelBuilder.Entity<Nationality>()
                .HasMany(e => e.Parents)
                .WithOptional(e => e.Nationality)
                .HasForeignKey(e => e.FatherCountryID);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Parents2)
                .WithOptional(e => e.Country2)
                .HasForeignKey(e => e.MotherPassportCountryofIssueID);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Parents1)
                .WithOptional(e => e.Country1)
                .HasForeignKey(e => e.FatherPassportCountryofIssueID);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Parents)
                .WithOptional(e => e.Country)
                .HasForeignKey(e => e.CountryID);

            modelBuilder.Entity<Login>()
               .HasMany(e => e.Parents)
               .WithOptional(e => e.Login)
               .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<Vehicle>()
                .HasMany(e => e.AssignVehicleMaps)
                .WithOptional(e => e.Vehicle)
                .HasForeignKey(e => e.VehicleID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.AssignVehicleMaps)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<Vehicle>()
                .HasMany(e => e.RouteVehicleMaps)
                .WithOptional(e => e.Vehicle)
                .HasForeignKey(e => e.VehicleID);

            modelBuilder.Entity<Student>()
                 .HasMany(e => e.StudentRouteStopMaps)
                 .WithOptional(e => e.Student)
                 .HasForeignKey(e => e.StudentID);

            modelBuilder.Entity<RouteStopMap>()
                .HasMany(e => e.StudentRouteStopMaps)
                .WithOptional(e => e.RouteStopMap)
                .HasForeignKey(e => e.RouteStopMapID);

            modelBuilder.Entity<RouteStopMap>()
                .HasMany(e => e.StudentRouteStopMaps1)
                .WithOptional(e => e.RouteStopMap1)
                .HasForeignKey(e => e.PickupStopMapID);

            modelBuilder.Entity<RouteStopMap>()
                .HasMany(e => e.StudentRouteStopMaps2)
                .WithOptional(e => e.RouteStopMap2)
                .HasForeignKey(e => e.DropStopMapID);

            modelBuilder.Entity<CertificateLog>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<CertificateTemplate>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Login>()
                .HasMany(e => e.UserDeviceMaps)
                .WithOptional(e => e.Login)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<UserDeviceMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<AcadamicCalendar>()
                .HasMany(e => e.AcademicYearCalendarEvents)
                .WithRequired(e => e.AcadamicCalendar)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AcademicYearCalendarEvent>()
                .Property(e => e.NoofHours)
                .HasPrecision(5, 2);

            modelBuilder.Entity<AcademicYearCalendarStatu>()
                .HasMany(e => e.AcadamicCalendars)
                .WithOptional(e => e.AcademicYearCalendarStatu)
                .HasForeignKey(e => e.AcademicCalendarStatusID);

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.Students)
                .WithOptional(e => e.AcademicYear)
                .HasForeignKey(e => e.AcademicYearID);

            modelBuilder.Entity<DeliveryTypes1>()
                .HasMany(e => e.DeliveryTypeCultureDatas)
                .WithRequired(e => e.DeliveryTypes1)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Culture>()
                .HasMany(e => e.DeliveryTypeCultureDatas)
                .WithRequired(e => e.Culture)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DeliveryTypeTimeSlotMap>()
                .HasMany(e => e.DeliveryTypeCutOffSlots)
                .WithOptional(e => e.DeliveryTypeTimeSlotMap)
                .HasForeignKey(e => e.TimeSlotID);
            modelBuilder.Entity<ProductCategoryMap>()
                .HasMany(e => e.FeeDueInventoryMaps)
                .WithOptional(e => e.ProductCategoryMap)
                .HasForeignKey(e => e.ProductCategoryMapID);

            modelBuilder.Entity<TransactionHead>()
                .HasMany(e => e.FeeDueInventoryMaps)
                .WithOptional(e => e.TransactionHead)
                .HasForeignKey(e => e.TransactionHeadID);

            modelBuilder.Entity<TransactionHead>()
                .HasMany(e => e.ShoppingCarts)
                .WithOptional(e => e.TransactionHead)
                .HasForeignKey(e => e.BlockedHeadID);

            modelBuilder.Entity<ShoppingCartItem>()
                .Property(e => e.Quantity)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ShoppingCartItem>()
                .Property(e => e.NetWeight)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ShoppingCartItem>()
                .Property(e => e.CostPrice)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ShoppingCartItem>()
                .Property(e => e.ProductPrice)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ShoppingCartItem>()
                .Property(e => e.ProductDiscountPrice)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ShoppingCart>()
                .Property(e => e.DeliveryCharge)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ShoppingCart>()
                .Property(e => e.ActualDeliveryCharge)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ShoppingCart>()
                .Property(e => e.LoyaltyAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ShoppingCart>()
                .Property(e => e.RadeemPoint)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ShoppingCart>()
                .Property(e => e.VoucherAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ShoppingCart>()
                .Property(e => e.WalletAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ShoppingCart>()
                .HasMany(e => e.ShoppingCartItems)
                .WithOptional(e => e.ShoppingCart)
                .HasForeignKey(e => e.ShoppingCartID);

            modelBuilder.Entity<ShoppingCart>()
                .HasMany(e => e.ShoppingCartVoucherMaps)
                .WithOptional(e => e.ShoppingCart)
                .HasForeignKey(e => e.ShoppingCartID);

            modelBuilder.Entity<ShoppingCart>()
                .HasMany(e => e.TransactionHeadShoppingCartMaps)
                .WithOptional(e => e.ShoppingCart)
                .HasForeignKey(e => e.ShoppingCartID);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.ShoppingCarts)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.StudentID);


            modelBuilder.Entity<StockCompareDetail>()
                .Property(e => e.Fraction)
                .HasPrecision(18, 3);

            modelBuilder.Entity<StockCompareDetail>()
                .Property(e => e.PhysicalUnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<StockCompareDetail>()
                .Property(e => e.PhysicalQuantity)
                .HasPrecision(19, 4);

            modelBuilder.Entity<StockCompareDetail>()
                .Property(e => e.PhysicalAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<StockCompareDetail>()
                .Property(e => e.ActualUnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<StockCompareDetail>()
                .Property(e => e.ActualQuantity)
                .HasPrecision(19, 4);

            modelBuilder.Entity<StockCompareDetail>()
                .Property(e => e.ActualAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<StockCompareDetail>()
                .Property(e => e.DifferUnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<StockCompareDetail>()
                .Property(e => e.DifferQuantity)
                .HasPrecision(19, 4);

            modelBuilder.Entity<StockCompareDetail>()
                .Property(e => e.DifferAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<StockCompareDetail>()
                .Property(e => e.ExchangeRate)
                .HasPrecision(18, 3);

            modelBuilder.Entity<StockCompareDetail>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<StockCompareDetail>()
                .Property(e => e.SerialNumber)
                .IsUnicode(false);

            modelBuilder.Entity<StockCompareDetail>()
                .Property(e => e.LastCostPrice)
                .HasPrecision(18, 3);

            modelBuilder.Entity<StockCompareHead>()
                .Property(e => e.TransactionNo)
                .IsUnicode(false);

            modelBuilder.Entity<StockCompareHead>()
                .Property(e => e.ExchangeRate)
                .HasPrecision(18, 3);

            modelBuilder.Entity<StockCompareHead>()
                .Property(e => e.DocumentStatusID)
                .IsFixedLength();

            modelBuilder.Entity<StockCompareHead>()
                .Property(e => e.ExternalReference1)
                .IsUnicode(false);

            modelBuilder.Entity<StockCompareHead>()
                .Property(e => e.ExternalReference2)
                .IsUnicode(false);

            modelBuilder.Entity<StockCompareHead>()
                .Property(e => e.PhysicalTotalAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<StockCompareHead>()
                .Property(e => e.BookTotalAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<StockCompareHead>()
                .Property(e => e.DifferTotalAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<TransactionHead>()
                .Property(e => e.PaidAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.Account)
                .HasForeignKey(e => e.GLAccountID);

            modelBuilder.Entity<Culture>()
                .HasMany(e => e.ViewCultureDatas)
                .WithRequired(e => e.Culture)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<View>()
                .HasMany(e => e.ViewCultureDatas)
                .WithRequired(e => e.View)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Culture>()
                .HasMany(e => e.ScreenMetadataCultureDatas)
                .WithRequired(e => e.Culture)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ScreenMetadata>()
                .HasMany(e => e.ScreenMetadataCultureDatas)
                .WithRequired(e => e.ScreenMetadata)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sponsor>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Sponsor>()
                .HasMany(e => e.PassportVisaDetails)
                .WithOptional(e => e.Sponsor)
                .HasForeignKey(e => e.SponsorID);

            modelBuilder.Entity<Employee>()
               .HasMany(e => e.EmployeeLeaveAllocations)
               .WithOptional(e => e.Employee)
               .HasForeignKey(e => e.EmployeeID);


            //modelBuilder.Entity<LeaveGroupTypeMap>()
            //    .Property(e => e.TimeStamps)
            //    .IsFixedLength();

            modelBuilder.Entity<Product>()
                .Property(e => e.Calorie)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Allergy>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<AllergyStudentMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Student>()
                .HasMany(e => e.AllergyStudentMaps)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.StudentID);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductAllergyMaps)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.ProductID);

            modelBuilder.Entity<ProductAllergyMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Student>()
                .HasMany(e => e.StudentTransferRequests)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.StudentID);

            modelBuilder.Entity<PeriodClosingTranStatus>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<PeriodClosingTranHead>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<PeriodClosingTranHead>()
                .HasMany(e => e.PeriodClosingTranTails)
                .WithOptional(e => e.PeriodClosingTranHead)
                .HasForeignKey(e => e.PeriodClosingTranHeadID);

            modelBuilder.Entity<PeriodClosingTranTail>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<PeriodClosingTranMaster>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ShoppingCart>()
                .HasMany(e => e.ShoppingCartWeekDayMaps)
                .WithOptional(e => e.ShoppingCart)
                .HasForeignKey(e => e.ShoppingCartID);

            modelBuilder.Entity<Day>()
                .HasMany(e => e.ShoppingCartWeekDayMaps)
                .WithOptional(e => e.Day)
                .HasForeignKey(e => e.WeekDayID);

            modelBuilder.Entity<Login>()
                .HasMany(e => e.Visitors)
                .WithOptional(e => e.Login)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<VisitorAttachmentMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Visitor>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Visitor>()
                .HasMany(e => e.VisitorAttachmentMaps)
                .WithOptional(e => e.Visitor)
                .HasForeignKey(e => e.VisitorID);

            modelBuilder.Entity<Severity>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeRelationsDetails)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<EmployeeRelationsDetail>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();


        }
    }
}
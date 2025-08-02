using Eduegate.Domain.Entity.Communications;
using Eduegate.Domain.Entity.CustomEntity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity.Models.HR;
using Eduegate.Domain.Entity.Models.Inventory;
using Eduegate.Domain.Entity.Models.Mutual;
using Eduegate.Domain.Entity.Models.Notification;
using Eduegate.Domain.Entity.Models.Settings;
using Eduegate.Domain.Entity.Models.ValueObjects;
using Eduegate.Domain.Entity.Models.Workflows;
using EntityGenerator.Core.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity
{
    public partial class dbEduegateERPContext : DbContext
    {
        public dbEduegateERPContext()
        {
        }

        public dbEduegateERPContext(DbContextOptions<dbEduegateERPContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var data = Infrastructure.ConfigHelper.GetDefaultConnectionString();
                optionsBuilder.UseSqlServer(Infrastructure.ConfigHelper.GetDefaultConnectionString());
            }
        }

        public virtual DbSet<AccountTransactionAmountDetail> AccountTransactionAmountDetails { get; set; }
        public virtual DbSet<Accounts_Chart> AccountCharts { get; set; }
        public virtual DbSet<ProductSKUDetail> ProductSKUDetails { get; set; }
        public virtual DbSet<SearchResult> SearchResults { get; set; }
        public virtual DbSet<ProductPriceSKU> ProductPriceSKUs { get; set; }
        public virtual DbSet<ProductListBySKU> ProductListBySKUs { get; set; }

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
        //public DbSet<JobApplication> JobApplications { get; set; }
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
        public DbSet<Eduegate.Domain.Entity.Models.ProductQuantityDiscount> ProductQuantityDiscounts { get; set; }
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
        public DbSet<SupplierContentIDs> SupplierContentIDs { get; set; } 
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Sysdiagram> sysdiagrams { get; set; }
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
        public DbSet<ProductCultureData> ProductCultureDatas { get; set; }
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
        public virtual DbSet<FiscalYear> FiscalYears { get; set; }
        public DbSet<TransactionHeadShoppingCartMap> TransactionHeadShoppingCartMaps { get; set; }
        public DbSet<DocumentReferenceType> DocumentReferenceTypes { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<DocumentTypeSetting> DocumentTypeSettings { get; set; }
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
        //public DbSet<TestSearchView> TestSearchViews { get; set; }
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
        public DbSet<PaymentQPay> PaymentQPays { get; set; }
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
        public DbSet<Department1> Departments1 { get; set; }
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
        public virtual DbSet<PageBoilerplateReport> PageBoilerplateReports { get; set; }
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
        public virtual DbSet<CareerNotificationAlert> CareerNotificationAlerts { get; set; }
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
        public  DbSet<AccountTransactionReceivablesMap> AccountTransactionReceivablesMaps { get; set; }
        
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
        public virtual DbSet<EmployeeQualificationMap> EmployeeQualificationMaps { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<Catogory> Catogories { get; set; }
        public virtual DbSet<LicenseType> LicenseTypes { get; set; }

        public virtual DbSet<AreaTreeSearch> AreaTreeSearches { get; set; }

        public virtual DbSet<Communication> Communications { get; set; }
        public virtual DbSet<CommunicationType> CommunicationTypes { get; set; }

        public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }
        public virtual DbSet<EmailTemplate2> EmailTemplates2 { get; set; }
        public virtual DbSet<EmailTemplateParameterMap> EmailTemplateParameterMaps { get; set; }

        public virtual DbSet<Lead> Leads { get; set; }
        public virtual DbSet<Schools> Schools { get; set; }

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

        public virtual DbSet<DeliveryOrderType> DeliveryOrderTypes { get; set; }

        public virtual DbSet<VWS_Fee_Outstanding_Current> VWS_Fee_Outstanding_Currents { get; set; }

        public virtual DbSet<ProductClassMap> ProductClassMaps { get; set; }

        public virtual DbSet<ProductClassType> ProductClassTypes { get; set; }

        public virtual DbSet<Dashboard> Dashboards { get; set; }

        public virtual DbSet<RFQSupplierRequestMap> RFQSupplierRequestMaps { get; set; }

        public virtual DbSet<ArchiveTable> ArchiveTables { get; set; }

        public virtual DbSet<BusinessTypes> BusinessTypes { get; set; }

        public virtual DbSet<Qualification> Qualifications { get; set; }

        public virtual DbSet<QualificationCoreSubjectMap> QualificationCoreSubjectMaps { get; set; }

        public virtual DbSet<EmployeeExperienceDetail> EmployeeExperienceDetails { get; set; }

        public virtual DbSet<Tender> Tenders { get; set; }
        public virtual DbSet<TenderAuthentication> TenderAuthentications { get; set; }

        public virtual DbSet<TenderAuthenticationLog> TenderAuthenticationLogs { get; set; }
        public virtual DbSet<TenderStatus> TenderStatuses { get; set; }
        public virtual DbSet<TenderType1> TenderTypes1 { get; set; }

        public virtual DbSet<BankReconciliationDetail> BankReconciliationDetails { get; set; }
        public virtual DbSet<BankReconciliationHead> BankReconciliationHeads { get; set; }
        public virtual DbSet<BankReconciliationMatchingEntry> BankReconciliationMatchingEntries { get; set; }
        public virtual DbSet<BankReconciliationMatchedStatus> BankReconciliationMatchedStatuses { get; set; }       
        public virtual DbSet<BankReconciliationStatus> BankReconciliationStatuses { get; set; }
        public virtual DbSet<BankStatement> BankStatements { get; set; }

        public virtual DbSet<BidApprovalDetail> BidApprovalDetails { get; set; }
        public virtual DbSet<BidApprovalHead> BidApprovalHeads { get; set; }

        public virtual DbSet<BankStatementEntry> BankStatementEntries { get; set; }

        public virtual DbSet<BankAccount> BankAccounts { get; set; }
        public virtual DbSet<BankAccountStatus> BankAccountStatuses { get; set; }
        public virtual DbSet<BankAccountType> BankAccountTypes { get; set; }

        public virtual DbSet<BroadCast> BroadCasts { get; set; }
        public virtual DbSet<BroadcastRecipient> BroadcastRecipients { get; set; }

        public virtual DbSet<AvailableJob> AvailableJobs { get; set; }
        public virtual DbSet<AvailableJobTag> AvailableJobTags { get; set; }

        public virtual DbSet<JobSeeker> JobSeekers { get; set; }
        public virtual DbSet<JobSeekerSkillMap> JobSeekerSkillMaps { get; set; }
        public virtual DbSet<RecruitmentLogin> RecruitmentLogins { get; set; }

        public virtual DbSet<JobApplication> JobApplications { get; set; }

        public virtual DbSet<EmployeeRelationsDetail> EmployeeRelationsDetails { get; set; }

        public virtual DbSet<AvailableJobSkillMap> AvailableJobSkills { get; set; }
        public virtual DbSet<AvailableJobCriteriaMap> AvailableJobCriterias { get; set; }
        public virtual DbSet<JobCriteriaType> JobCriteriaTypes { get; set; }
        public virtual DbSet<JobInterview> JobInterviews { get; set; }
        public virtual DbSet<JobInterviewMap> JobInterviewMaps { get; set; }
        public virtual DbSet<JobInterviewRound> JobInterviewRounds { get; set; }
        public virtual DbSet<JobInterviewRoundMap> JobInterviewRoundMaps { get; set; }
        public virtual DbSet<JobInterviewMarkMap> JobInterviewMarkMaps { get; set; }
        public virtual DbSet<EmployeeJobDescription> EmployeeJobDescriptions { get; set; }
        public virtual DbSet<EmployeeJobDescriptionDetail> EmployeeJobDescriptionDetails { get; set; }
        public virtual DbSet<JobDescription> JobDescriptions { get; set; }
        public virtual DbSet<JobDescriptionDetail> JobDescriptionDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccount>(entity =>
            {
                entity.Property(e => e.Balance).HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrencyID).HasDefaultValueSql("((0))");

                entity.Property(e => e.InterestRate).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsJointAccount).HasDefaultValueSql("((0))");

                entity.Property(e => e.OverdraftLimit).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamps)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.BankAccounts)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_BankAccounts_Accounts");

                entity.HasOne(d => d.AccountType)
                    .WithMany(p => p.BankAccounts)
                    .HasForeignKey(d => d.AccountTypeID)
                    .HasConstraintName("FK_BankAccounts_BankAccountTypes");

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.BankAccounts)
                    .HasForeignKey(d => d.BankID)
                    .HasConstraintName("FK_BankAccounts_Banks");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.BankAccounts)
                    .HasForeignKey(d => d.CurrencyID)
                    .HasConstraintName("FK_BankAccounts_Currencies");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.BankAccounts)
                    .HasForeignKey(d => d.StatusID)
                    .HasConstraintName("FK_BankAccounts_BankAccountStatuses");
            });

            modelBuilder.Entity<BankAccountStatus>(entity =>
            {
                entity.HasKey(e => e.StatusID)
                    .HasName("PK__BankAcco__C8EE20431C8BF0A7");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TimeStamps)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<BankAccountType>(entity =>
            {
                entity.HasKey(e => e.AccountTypeID)
                    .HasName("PK__BankAcco__8F95854FDC775D39");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TimeStamps)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");
            });
            modelBuilder.Entity<AcadamicCalendar>(entity =>
            {
                entity.HasKey(e => e.AcademicCalendarID)
                    .HasName("PK_AcadamicCalendar");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYearCalendarStatu)
                    .WithMany(p => p.AcadamicCalendars)
                    .HasForeignKey(d => d.AcademicCalendarStatusID)
                    .HasConstraintName("FK_AcadamicCalenders_AcademicYearCalenderStatus");

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.AcadamicCalendars)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_AcadamicCalenders_AcademicYears");

                entity.HasOne(d => d.CalendarType)
                    .WithMany(p => p.AcadamicCalendars)
                    .HasForeignKey(d => d.CalendarTypeID)
                    .HasConstraintName("FK_Calendar_type");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.AcadamicCalendars)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_AcadamicCalendars_School");
            });

            //modelBuilder.Entity<AcademicClassMap>(entity =>
            //{
            //    entity.HasKey(e => e.AcademicClassMapIID)
            //        .HasName("PK_AcademicClasses");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.AcademicClassMaps)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_AcademicClassMaps_AcademicYear");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.AcademicClassMaps)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_AcademicClassMaps_Classes");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.AcademicClassMaps)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_AcademicClassMaps_Schools");
            //});

            //modelBuilder.Entity<AcademicNote>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.AcademicNotes)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_AcademicNotes_Classes");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.AcademicNotes)
            //        .HasForeignKey(d => d.SectionID)
            //        .HasConstraintName("FK_AcademicNotes_Sections");

            //    entity.HasOne(d => d.Subject)
            //        .WithMany(p => p.AcademicNotes)
            //        .HasForeignKey(d => d.SubjectID)
            //        .HasConstraintName("FK_AcademicNotes_Subjects");
            //});

            //modelBuilder.Entity<AcademicSchoolMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.AcademicSchoolMaps)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_AcademicSchoolMaps_AcademicYear");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.AcademicSchoolMaps)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_AcademicSchoolMaps_Schools");
            //});            

            modelBuilder.Entity<AcademicYear>(entity =>
            {
                entity.Property(e => e.AcademicYearID).ValueGeneratedNever();

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.ORDERNO).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AcademicYearStatu)
                    .WithMany(p => p.AcademicYears)
                    .HasForeignKey(d => d.AcademicYearStatusID)
                    .HasConstraintName("FK_AcademicYears_AcademicYearStatus");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.AcademicYears)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_AcademicYears_Schools");
            });

            modelBuilder.Entity<AcademicYearCalendarEvent>(entity =>
            {
                entity.HasKey(e => e.AcademicYearCalendarEventIID)
                    .HasName("PK_Events");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcadamicCalendar)
                    .WithMany(p => p.AcademicYearCalendarEvents)
                    .HasForeignKey(d => d.AcademicCalendarID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AcademicYearCalendarEvents_AcadamicCalenders");

                entity.HasOne(d => d.AcademicYearCalendarEventType)
                    .WithMany(p => p.AcademicYearCalendarEvents)
                    .HasForeignKey(d => d.AcademicYearCalendarEventTypeID)
                    .HasConstraintName("FK_AcademicYearCalendarEvents_AcademicYearCalendarEventType");
            });

            modelBuilder.Entity<AcademicYearCalendarEventType>(entity =>
            {
                entity.Property(e => e.AcademicYearCalendarEventTypeID).ValueGeneratedNever();
            });


            modelBuilder.Entity<AcademicYearCalendarStatu>(entity =>
            {
                entity.HasKey(e => e.AcademicYearCalendarStatusID)
                    .HasName("PK_AcademicYearStatus");
            });

            modelBuilder.Entity<AcademicYearStatu>(entity =>
            {
                entity.HasKey(e => e.AcademicYearStatusID)
                    .HasName("PK_AcademYearStatus");
            });

            modelBuilder.Entity<AccomodationType>(entity =>
            {
                entity.Property(e => e.AccomodationTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<Account>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AccountBehavoir)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.AccountBehavoirID)
                    .HasConstraintName("FK_Accounts_AccountBehavoirs");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.GroupID)
                    .HasConstraintName("FK_Accounts_Groups");

                //entity.HasOne(d => d.ParentAccount)
                //    .WithMany(p => p.InverseParentAccount)
                //    .HasForeignKey(d => d.ParentAccountID)
                //    .HasConstraintName("FK_Accounts_Accounts");
            });


            modelBuilder.Entity<AccountTaxTransaction>(entity =>
            {
                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountTaxTransactions)
                    .HasForeignKey(d => d.AccoundID)
                    .HasConstraintName("FK_AccountTaxTransactions_Accounts");

                entity.HasOne(d => d.AccountTransactionHead)
                    .WithMany(p => p.AccountTaxTransactions)
                    .HasForeignKey(d => d.HeadID)
                    .HasConstraintName("FK_AccountTaxTransactions_TransactionHead");

                entity.HasOne(d => d.TaxTemplate)
                    .WithMany(p => p.AccountTaxTransactions)
                    .HasForeignKey(d => d.TaxTemplateID)
                    .HasConstraintName("FK_AccountTaxTransactions_TaxTemplates");

                entity.HasOne(d => d.TaxTemplateItem)
                    .WithMany(p => p.AccountTaxTransactions)
                    .HasForeignKey(d => d.TaxTemplateItemID)
                    .HasConstraintName("FK_AccountTaxTransactions_TaxTemplateItems");

                entity.HasOne(d => d.TaxType)
                    .WithMany(p => p.AccountTaxTransactions)
                    .HasForeignKey(d => d.TaxTypeID)
                    .HasConstraintName("FK_AccountTaxTransactions_TaxTypes");
            });

            modelBuilder.Entity<AccountTransaction>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountTransactions)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_AccountTransactions_Accounts");

                entity.HasOne(d => d.Budget)
                    .WithMany(p => p.AccountTransactions)
                    .HasForeignKey(d => d.BudgetID)
                    .HasConstraintName("FK_AccountTran_Budgets");

                entity.HasOne(d => d.CostCenter)
                    .WithMany(p => p.AccountTransactions)
                    .HasForeignKey(d => d.CostCenterID)
                    .HasConstraintName("FK_AccountTransactions_CostCenters");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.AccountTransactions)
                    .HasForeignKey(d => d.DocumentTypeID)
                    .HasConstraintName("FK_AccountTransactions_DocumentTypes");
            });

            modelBuilder.Entity<AccountTransactionDetail>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountTransactionDetails)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_AccountTransactionDetails_Accounts");

                entity.HasOne(d => d.AccountTransactionHead)
                    .WithMany(p => p.AccountTransactionDetails)
                    .HasForeignKey(d => d.AccountTransactionHeadID)
                    .HasConstraintName("FK_AccountTransactionDetails_AccountTransactionHeads");

                entity.HasOne(d => d.Budget)
                    .WithMany(p => p.AccountTransactionDetails)
                    .HasForeignKey(d => d.BudgetID)
                    .HasConstraintName("FK_AccountTransac_Budgets");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.AccountTransactionDetails)
                    .HasForeignKey(d => d.DepartmentID)
                    .HasConstraintName("FK_AccountTransactionDet_Departments");

                entity.HasOne(d => d.ProductSKUMap)
                    .WithMany(p => p.AccountTransactionDetails)
                    .HasForeignKey(d => d.ProductSKUId)
                    .HasConstraintName("FK_AccountTransactionDetails_ProductSKUMaps");

                entity.HasOne(d => d.Accounts_SubLedger)
                    .WithMany(p => p.AccountTransactionDetails)
                    .HasForeignKey(d => d.SubLedgerID)
                    .HasConstraintName("FK_AccountTransactionDetails_Accounts_SubLedger");
            });

            modelBuilder.Entity<AccountTransactionHead>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountTransactionHeads)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_AccountTransactionHeads_Accounts");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.AccountTransactionHeads)
                    .HasForeignKey(d => d.BranchID)
                    .HasConstraintName("FK_AccountTransactionHeads_Branches");

                entity.HasOne(d => d.DocumentStatus)
                    .WithMany(p => p.AccountTransactionHeads)
                    .HasForeignKey(d => d.DocumentStatusID)
                    .HasConstraintName("FK_AccountTransactionHeads_DocumentStatuses");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.AccountTransactionHeads)
                    .HasForeignKey(d => d.DocumentTypeID)
                    .HasConstraintName("FK_AccountTransactionHeads_DocumentTypes");

                entity.HasOne(d => d.TransactionStatus)
                    .WithMany(p => p.AccountTransactionHeads)
                    .HasForeignKey(d => d.TransactionStatusID)
                    .HasConstraintName("FK_AccountTransactionHeads_TransactionStatuses");
            });

            modelBuilder.Entity<AccountTransactionHeadAccountMap>(entity =>
            {
                entity.HasOne(d => d.AccountTransactionHead)
                    .WithMany(p => p.AccountTransactionHeadAccountMaps)
                    .HasForeignKey(d => d.AccountTransactionHeadID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountTransactionHeadAccountMaps_AccountTransactionHeads");

                entity.HasOne(d => d.AccountTransaction)
                    .WithMany(p => p.AccountTransactionHeadAccountMaps)
                    .HasForeignKey(d => d.AccountTransactionID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountTransactionHeadAccountMaps_AccountTransactions");
            });


            modelBuilder.Entity<Accounts_SubLedger>(entity =>
            {
                entity.Property(e => e.AllowUserDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.AllowUserEdit).HasDefaultValueSql("((0))");

                entity.Property(e => e.AllowUserRename).HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsHidden).HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedBy).HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");
            });


            modelBuilder.Entity<Accounts_SubLedger_Relation>(entity =>
            {
                //entity.HasOne(d => d.SL_Account)
                //    .WithMany(p => p.Accounts_SubLedger_Relation)
                //    .HasForeignKey(d => d.SL_AccountID)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Accounts_SubLedger_Relation_Accounts_SubLedger");
            });

            modelBuilder.Entity<ActionLinkType>(entity =>
            {
                entity.Property(e => e.ActionLinkTypeID).ValueGeneratedNever();
            });

            //modelBuilder.Entity<ActionStatus>(entity =>
            //{
            //    entity.Property(e => e.ActionStatusID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<ActionType>(entity =>
            //{
            //    entity.Property(e => e.ActionTypeID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<Activity>(entity =>
            //{
            //    entity.HasOne(d => d.ActionStatus)
            //        .WithMany(p => p.Activities)
            //        .HasForeignKey(d => d.ActionStatusID)
            //        .HasConstraintName("FK_Activities_ActionStatuses");

            //    entity.HasOne(d => d.ActionType)
            //        .WithMany(p => p.Activities)
            //        .HasForeignKey(d => d.ActionTypeID)
            //        .HasConstraintName("FK_Activities_ActionTypes");

            //    entity.HasOne(d => d.ActivityType)
            //        .WithMany(p => p.Activities)
            //        .HasForeignKey(d => d.ActivityTypeID)
            //        .HasConstraintName("FK_Activities_ActivityTypes");

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.Activities)
            //        .HasForeignKey(d => d.UserID)
            //        .HasConstraintName("FK_Activities_UserReferences");
            //});

            //modelBuilder.Entity<ActivityType>(entity =>
            //{
            //    entity.Property(e => e.ActivityTypeID).ValueGeneratedNever();
            //});

            modelBuilder.Entity<AdditionalExpens>(entity =>
            {
                entity.Property(e => e.AdditionalExpenseID).ValueGeneratedNever();
            });

            modelBuilder.Entity<AdditionalExpenseProvisionalAccountMap>(entity =>
            {
                entity.HasKey(e => e.AdditionalExpProvAccountMapIID)
                    .HasName("PK__Addition__92AA8BBD7AB4F805");

                //entity.Property(e => e.Isdefault).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<AdditionalExpensesTransactionsMap>(entity =>
            {
                entity.HasKey(e => e.AdditionalExpensesTransactionsMapIID)
                    .HasName("PK__Addition__E3CF711C3A8C47D5");

                entity.Property(e => e.ISAffectSupplier).HasDefaultValueSql("((0))");
            });


            modelBuilder.Entity<BroadcastRecipient>(entity =>
            {
                entity.HasOne(d => d.Broadcast)
                    .WithMany(p => p.BroadcastRecipients)
                    .HasForeignKey(d => d.BroadcastID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BroadcastRecipients_BroadCast");
            });


            modelBuilder.Entity<BankReconciliationDetail>(entity =>
            {
                entity.HasKey(e => e.ReconciliationDetailIID)
                    .HasName("PK__BankReco__FD411CEDADCE2CB0");

                entity.HasOne(d => d.BankReconciliationHead)
                    .WithMany(p => p.BankReconciliationDetails)
                    .HasForeignKey(d => d.BankReconciliationHeadID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BankRecon__BankR__0C408994");

                entity.HasOne(d => d.BankReconciliationMatchedStatus)
                    .WithMany(p => p.BankReconciliationDetails)
                    .HasForeignKey(d => d.BankReconciliationMatchedStatusID)
                    .HasConstraintName("FK__BankRecon__BankR__16BE1807");

                entity.HasOne(d => d.BankStatementEntry)
                   .WithMany(p => p.BankReconciliationDetails)
                   .HasForeignKey(d => d.BankStatementEntryID)
                   .HasConstraintName("FK__BankRecon__BankS__11C458C0");

                entity.HasOne(d => d.TranHead)
                    .WithMany(p => p.BankReconciliationDetails)
                    .HasForeignKey(d => d.TranHeadID)
                    .HasConstraintName("FK__BankRecon__Statu__0B4C655B");
            });

            modelBuilder.Entity<BankReconciliationHead>(entity =>
            {
                entity.HasKey(e => e.BankReconciliationHeadIID)
                    .HasName("PK__BankReco__F4FF0F348C6D44B0");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.BankReconciliationStatus)
                    .WithMany(p => p.BankReconciliationHeads)
                    .HasForeignKey(d => d.BankReconciliationStatusID)
                    .HasConstraintName("FK__BankRecon__BankR__15C9F3CE");

                entity.HasOne(d => d.BankStatement)
                    .WithMany(p => p.BankReconciliationHeads)
                    .HasForeignKey(d => d.BankStatementID)
                    .HasConstraintName("FK__BankRecon__BankS__086FF8B0");
            });
            modelBuilder.Entity<BankStatementEntry>(entity =>
            {
                entity.HasKey(e => e.BankStatementEntryIID)
                    .HasName("PK__BankStat__76E4BB7358C52448");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.BankStatement)
                    .WithMany(p => p.BankStatementEntries)
                    .HasForeignKey(d => d.BankStatementID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BankState__BankS__10D03487");
            });

            modelBuilder.Entity<BankReconciliationMatchedStatus>(entity =>
            {
                entity.HasKey(e => e.StatusID)
                    .HasName("PK__BankReco__C8EE20433C8E8DA6");

                entity.Property(e => e.StatusID).ValueGeneratedNever();
            });

            modelBuilder.Entity<BankReconciliationMatchingEntry>(entity =>
            {
                entity.HasKey(e => e.BankReconciliationMatchingEntryIID)
                    .HasName("PK__BankReco__05ADE04F974E415E");

                entity.Property(e => e.IsReconciled).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.BankReconciliationDetail)
                    .WithMany(p => p.BankReconciliationMatchingEntries)
                    .HasForeignKey(d => d.BankReconciliationDetailID)
                    .HasConstraintName("FK__BankRecon__BankR__39332B9C");              

                entity.HasOne(d => d.BankStatementEntry)
                    .WithMany(p => p.BankReconciliationMatchingEntries)
                    .HasForeignKey(d => d.BankStatementEntryID)
                    .HasConstraintName("FK__BankRecon__BankS__277E8FB5");

                entity.HasOne(d => d.TranHead)
                    .WithMany(p => p.BankReconciliationMatchingEntries)
                    .HasForeignKey(d => d.TranHeadID)
                    .HasConstraintName("FK__BankRecon__TranH__2872B3EE");
            });


            modelBuilder.Entity<BankReconciliationStatus>(entity =>
            {
                entity.HasKey(e => e.StatusID)
                    .HasName("PK__BankReco__C8EE2043AFDE265E");

                entity.Property(e => e.StatusID).ValueGeneratedNever();
            });

            modelBuilder.Entity<BankStatement>(entity =>
            {
                entity.HasKey(e => e.BankStatementIID)
                    .HasName("PK__BankStat__E3778C433EBD9B52");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            //modelBuilder.Entity<AdmissionEnquiry>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.AdmissionEnquiries)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_AdmissionEnquiries_Schools");
            //});


            //modelBuilder.Entity<AgeCriteria>(entity =>
            //{
            //    entity.HasKey(e => e.AgeCriteriaIID)
            //        .HasName("PK_AgeCriterias");

            //    entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.AgeCriterias)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_AgeCriteria_AcademicYear");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.AgeCriterias)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_AgeCriteria_classes");

            //    entity.HasOne(d => d.Curriculum)
            //        .WithMany(p => p.AgeCriterias)
            //        .HasForeignKey(d => d.CurriculumID)
            //        .HasConstraintName("FK_AgeCriteria_Syllabus");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.AgeCriterias)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_AgeCriteria_School");
            //});


            //modelBuilder.Entity<Agenda>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.Agenda)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_Agendas_AcademicYear");

            //    entity.HasOne(d => d.AgendaStatus)
            //        .WithMany(p => p.Agenda)
            //        .HasForeignKey(d => d.AgendaStatusID)
            //        .HasConstraintName("FK_Agendas_AgendaStatuses");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.Agenda)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_Agendas_Classes");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.Agenda)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_Agendas_School");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.Agenda)
            //        .HasForeignKey(d => d.SectionID)
            //        .HasConstraintName("FK_Agendas_Sections");

            //    entity.HasOne(d => d.Subject)
            //        .WithMany(p => p.Agenda)
            //        .HasForeignKey(d => d.SubjectID)
            //        .HasConstraintName("FK_Agendas_Subjects");

            //    entity.HasOne(d => d.TeachingAid)
            //        .WithMany(p => p.Agenda)
            //        .HasForeignKey(d => d.TeachingAidID)
            //        .HasConstraintName("FK_Agendas_TeachingAid");
            //});

            //modelBuilder.Entity<AgendaAttachmentMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Agenda)
            //        .WithMany(p => p.AgendaAttachmentMaps)
            //        .HasForeignKey(d => d.AgendaID)
            //        .HasConstraintName("FK_AgendaAttachmentMaps_Agenda");
            //});

            //modelBuilder.Entity<AgendaSectionMap>(entity =>
            //{
            //    entity.HasKey(e => e.AgendaSectionMapIID)
            //        .HasName("PK_AgendaSectionMap");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Agenda)
            //        .WithMany(p => p.AgendaSectionMaps)
            //        .HasForeignKey(d => d.AgendaID)
            //        .HasConstraintName("FK_AgendaSectionMap_Agenda");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.AgendaSectionMaps)
            //        .HasForeignKey(d => d.SectionID)
            //        .HasConstraintName("FK_AgendaSectionMap_Section");
            //});

            //modelBuilder.Entity<AgendaTaskAttachmentMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AgendaTaskMap)
            //        .WithMany(p => p.AgendaTaskAttachmentMaps)
            //        .HasForeignKey(d => d.AgendaTaskMapID)
            //        .HasConstraintName("FK_AgendaAttachment_AgendaTask");
            //});

            //modelBuilder.Entity<AgendaTaskMap>(entity =>
            //{
            //    entity.HasKey(e => e.AgendaTaskMapIID)
            //        .HasName("PK_AgendaTopicTaskMaps");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Agenda)
            //        .WithMany(p => p.AgendaTaskMaps)
            //        .HasForeignKey(d => d.AgendaID)
            //        .HasConstraintName("FK_Agendas");

            //    entity.HasOne(d => d.AgendaTopicMap)
            //        .WithMany(p => p.AgendaTaskMaps)
            //        .HasForeignKey(d => d.AgendaTopicMapID)
            //        .HasConstraintName("FK_AgendaTaskTopicMaps");

            //    entity.HasOne(d => d.TaskType)
            //        .WithMany(p => p.AgendaTaskMaps)
            //        .HasForeignKey(d => d.TaskTypeID)
            //        .HasConstraintName("FK_AgendaTaskTaskTypes");
            //});

            //modelBuilder.Entity<AgendaTopicAttachmentMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AgendaTopicMap)
            //        .WithMany(p => p.AgendaTopicAttachmentMaps)
            //        .HasForeignKey(d => d.AgendaTopicMapID)
            //        .HasConstraintName("FK_AgendaAttachmentMaps_AgendaTopic");
            //});

            //modelBuilder.Entity<AgendaTopicMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Agenda)
            //        .WithMany(p => p.AgendaTopicMaps)
            //        .HasForeignKey(d => d.AgendaID)
            //        .HasConstraintName("FK_AgendaTopicMaps_Agendas");
            //});

            //modelBuilder.Entity<Album>(entity =>
            //{
            //    entity.Property(e => e.AlbumID).ValueGeneratedNever();

            //    entity.HasOne(d => d.AlbumType)
            //        .WithMany(p => p.Albums)
            //        .HasForeignKey(d => d.AlbumTypeID)
            //        .HasConstraintName("FK_Albums_AlbumTypes");
            //});

            //modelBuilder.Entity<AlbumImageMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Album)
            //        .WithMany(p => p.AlbumImageMaps)
            //        .HasForeignKey(d => d.AlbumID)
            //        .HasConstraintName("FK_AlbumImageMaps_Albums");
            //});


            modelBuilder.Entity<AlertStatus>(entity =>
            {
                entity.Property(e => e.AlertStatusID).ValueGeneratedNever();
            });

            modelBuilder.Entity<AlertType>(entity =>
            {
                entity.Property(e => e.AlertTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<Allergy>(entity =>
            {
                entity.Property(e => e.AllergyID).ValueGeneratedNever();

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<AllergyStudentMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.AllergyStudentMaps)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_AllergyStudentMaps_AcademicYear");

                entity.HasOne(d => d.Allergy)
                    .WithMany(p => p.AllergyStudentMaps)
                    .HasForeignKey(d => d.AllergyID)
                    .HasConstraintName("FK_AllergyStudentMaps_Allergy");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.AllergyStudentMaps)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_AllergyStudentMaps_School");

                //entity.HasOne(d => d.Severity)
                //    .WithMany(p => p.AllergyStudentMaps)
                //    .HasForeignKey(d => d.SeverityID)
                //    .HasConstraintName("FK_AllergyStudentMaps_Severities");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.AllergyStudentMaps)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_AllergyStudentMaps_Students");
            });

            //modelBuilder.Entity<ApplicationForm>(entity =>
            //{
            //    entity.HasKey(e => e.ApplicationFormIID)
            //        .HasName("PK_ApplicationForm");
            //});


            //modelBuilder.Entity<ApplicationSubmitType>(entity =>
            //{
            //    entity.HasKey(e => e.SubmitTypeID)
            //        .HasName("PK_SubmitTypeName");

            //    entity.Property(e => e.SubmitTypeID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<Appointment>(entity =>
            //{
            //    entity.HasOne(d => d.Customer)
            //        .WithMany(p => p.Appointments)
            //        .HasForeignKey(d => d.CustomerID)
            //        .HasConstraintName("FK_Appointments_Customers");

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.Appointments)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_Appointments_Employees");

            //    entity.HasOne(d => d.FrequencyType)
            //        .WithMany(p => p.Appointments)
            //        .HasForeignKey(d => d.FrequencyTypeID)
            //        .HasConstraintName("FK_Appointments_Appointments");

            //    entity.HasOne(d => d.Service)
            //        .WithMany(p => p.Appointments)
            //        .HasForeignKey(d => d.ServiceID)
            //        .HasConstraintName("FK_Appointments_Services");
            //});


            modelBuilder.Entity<Area>(entity =>
            {
                entity.Property(e => e.AreaID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.ParentArea)
                //    .WithMany(p => p.InverseParentArea)
                //    .HasForeignKey(d => d.ParentAreaID)
                //    .HasConstraintName("FK_Areas_Areas");
            });

            //modelBuilder.Entity<AreaCultureData>(entity =>
            //{
            //    entity.HasKey(e => new { e.CultureID, e.AreaID });

            //    entity.HasOne(d => d.Area)
            //        .WithMany(p => p.AreaCultureDatas)
            //        .HasForeignKey(d => d.AreaID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_AreaCultureDatas_Areas");

            //    entity.HasOne(d => d.Culture)
            //        .WithMany(p => p.AreaCultureDatas)
            //        .HasForeignKey(d => d.CultureID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_AreaCultureDatas_Cultures");
            //});


            modelBuilder.Entity<Asset>(entity =>
            {
                entity.HasOne(d => d.AccumulatedDepGLAcc)
                    .WithMany(p => p.AssetAccumulatedDepGLAccs)
                    .HasForeignKey(d => d.AccumulatedDepGLAccID)
                    .HasConstraintName("FK_Assets_Accounts1");

                entity.HasOne(d => d.AssetCategory)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.AssetCategoryID)
                    .HasConstraintName("FK_Assets_AssetCategories");

                entity.HasOne(d => d.AssetGlAcc)
                    .WithMany(p => p.AssetAssetGlAccs)
                    .HasForeignKey(d => d.AssetGlAccID)
                    .HasConstraintName("FK_Assets_Accounts");

                entity.HasOne(d => d.DepreciationExpGLAcc)
                    .WithMany(p => p.AssetDepreciationExpGLAccs)
                    .HasForeignKey(d => d.DepreciationExpGLAccId)
                    .HasConstraintName("FK_Assets_Accounts2");
            });

            modelBuilder.Entity<AssetTransactionDetail>(entity =>
            {
                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AssetTransactionDetails)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_AssetTransactionDetails_Account");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.AssetTransactionDetails)
                    .HasForeignKey(d => d.AssetID)
                    .HasConstraintName("FK_AssetTransactionDetails_Assets");

                entity.HasOne(d => d.AssetTransactionHead)
                    .WithMany(p => p.AssetTransactionDetails)
                    .HasForeignKey(d => d.HeadID)
                    .HasConstraintName("FK_AssetTransactionDetails_AssetTransactionHead");
            });
        


            modelBuilder.Entity<AssetTransactionHead>(entity =>
            {
                entity.HasOne(d => d.DocumentReferenceStatusMap)
                    .WithMany(p => p.AssetTransactionHeads)
                    .HasForeignKey(d => d.DocumentStatusID)
                    .HasConstraintName("FK_AssetTransactionHead_DocumentReferenceStatusMap");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.AssetTransactionHeads)
                    .HasForeignKey(d => d.DocumentTypeID)
                    .HasConstraintName("FK_AssetTransactionHead_DocumentTypes");

                entity.HasOne(d => d.TransactionStatus)
                    .WithMany(p => p.AssetTransactionHeads)
                    .HasForeignKey(d => d.ProcessingStatusID)
                    .HasConstraintName("FK_AssetTransactionHead_TransactionStatuses");
            });

            modelBuilder.Entity<AssetTransactionHeadAccountMap>(entity =>
            {
                entity.HasOne(d => d.AccountTransaction)
                    .WithMany(p => p.AssetTransactionHeadAccountMaps)
                    .HasForeignKey(d => d.AccountTransactionID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetTransactionHeadAccountMaps_AccountTransactions");

                entity.HasOne(d => d.AssetTransactionHead)
                    .WithMany(p => p.AssetTransactionHeadAccountMaps)
                    .HasForeignKey(d => d.AssetTransactionHeadID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetTransactionHeadAccountMaps_AssetTransactionHead");
            });


            //modelBuilder.Entity<AssignVehicleAttendantMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AssignVehicleMap)
            //        .WithMany(p => p.AssignVehicleAttendantMaps)
            //        .HasForeignKey(d => d.AssignVehicleMapID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_AssignVehicleAttendantMaps_AssignVehicleMap");

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.AssignVehicleAttendantMaps)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_AssignVehicleAttendantMaps_Employees");
            //});

            modelBuilder.Entity<AssignVehicleMap>(entity =>
            {
                entity.HasKey(e => e.AssignVehicleMapIID)
                    .HasName("PK_AssignVehicleMaps");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.AssignVehicleMaps)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_AssignVehicleMap_AcademicYear");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.AssignVehicleMaps)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_AssignVehicleMaps_Employees");

                entity.HasOne(d => d.Routes1)
                    .WithMany(p => p.AssignVehicleMaps)
                    .HasForeignKey(d => d.RouteID)
                    .HasConstraintName("FK_AssignVehicleMap_Route");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.AssignVehicleMaps)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_AssignVehicleMap_School");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.AssignVehicleMaps)
                    .HasForeignKey(d => d.VehicleID)
                    .HasConstraintName("FK_AssignVehicleMaps_Vehicles");
            });


            modelBuilder.Entity<Assignment>(entity =>
            {
                entity.Property(e => e.TimeStamaps)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.Assignments)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_Assignments_AcademicYears");

                entity.HasOne(d => d.AssignmentStatus)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(d => d.AssignmentStatusID)
                    .HasConstraintName("FK_Assignments_AssignmentStatuses");

                //entity.HasOne(d => d.AssignmentType)
                //    .WithMany(p => p.Assignments)
                //    .HasForeignKey(d => d.AssignmentTypeID)
                //    .HasConstraintName("FK_Assignments_AssignmentTypes");

                //entity.HasOne(d => d.Class)
                //    .WithMany(p => p.Assignments)
                //    .HasForeignKey(d => d.ClassID)
                //    .HasConstraintName("FK_Assignments_Classes");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.Assignments)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_Assignments_School");

                //entity.HasOne(d => d.Section)
                //    .WithMany(p => p.Assignments)
                //    .HasForeignKey(d => d.SectionID)
                //    .HasConstraintName("FK_Assignments_Sections");

                //entity.HasOne(d => d.Subject)
                //    .WithMany(p => p.Assignments)
                //    .HasForeignKey(d => d.SubjectID)
                //    .HasConstraintName("FK_Assignments_Subjects");
            });

            modelBuilder.Entity<AssignmentAttachmentMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Assignment)
                    .WithMany(p => p.AssignmentAttachmentMaps)
                    .HasForeignKey(d => d.AssignmentID)
                    .HasConstraintName("FK_AssignmentAttachmentMaps_Assignments");
            });

            //modelBuilder.Entity<AssignmentDocument>(entity =>
            //{
            //    entity.HasOne(d => d.Assignment)
            //        .WithMany(p => p.AssignmentDocuments)
            //        .HasForeignKey(d => d.AssignmentID)
            //        .HasConstraintName("FK_AssignmentDocument_Assignments");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.AssignmentDocuments)
            //        .HasForeignKey(d => d.StudentID)
            //        .HasConstraintName("FK_AssignmentDocument_Students");
            //});

            //modelBuilder.Entity<AssignmentSectionMap>(entity =>
            //{
            //    entity.HasKey(e => e.AssignmentSectionMapIID)
            //        .HasName("PK_AssignmentSectionMap");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Assignment)
            //        .WithMany(p => p.AssignmentSectionMaps)
            //        .HasForeignKey(d => d.AssignmentID)
            //        .HasConstraintName("FK_AssignmentSectionMap_Assignment");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.AssignmentSectionMaps)
            //        .HasForeignKey(d => d.SectionID)
            //        .HasConstraintName("FK_AssignmentSectionMap_Section");
            //});

            modelBuilder.Entity<Attachment>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.EntityType)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.EntityTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Attachments_EntityType");
            });

            //modelBuilder.Entity<Attendence>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AttendenceStatus)
            //        .WithMany(p => p.Attendences)
            //        .HasForeignKey(d => d.AttendenceStatusID)
            //        .HasConstraintName("FK_Attendences_AttendenceStatuses");

            //    entity.HasOne(d => d.Company)
            //        .WithMany(p => p.Attendences)
            //        .HasForeignKey(d => d.CompanyID)
            //        .HasConstraintName("FK_Attendences_Companies");

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.Attendences)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_Attendences_Employees");
            //});

            //modelBuilder.Entity<AttendenceReason>(entity =>
            //{
            //    entity.Property(e => e.AttendenceReasonID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<AvailableJobCultureData>(entity =>
            //{
            //    entity.HasKey(e => new { e.CultureID, e.JobIID });

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            modelBuilder.Entity<Bank>(entity =>
            {
                entity.HasKey(e => e.BankIID)
                    .HasName("PK_BanksNames");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<Banner>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.BannerType)
                    .WithMany(p => p.Banners)
                    .HasForeignKey(d => d.BannerTypeID)
                    .HasConstraintName("FK_Banners_Banners");

                entity.HasOne(d => d.BannerStatus)
                    .WithMany(p => p.Banners)
                    .HasForeignKey(d => d.StatusID)
                    .HasConstraintName("FK_Banners_BannerStatuses");
            });

            modelBuilder.Entity<BannerType>(entity =>
            {
                entity.Property(e => e.BannerTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<Basket>(entity =>
            {
                entity.Property(e => e.BasketID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<BloodGroup>(entity =>
            {
                entity.Property(e => e.BloodGroupID).ValueGeneratedNever();
            });

            modelBuilder.Entity<BoilerPlate>(entity =>
            {
                entity.Property(e => e.BoilerPlateID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<BoilerPlateParameter>(entity =>
            {
                entity.Property(e => e.BoilerPlateParameterID).ValueGeneratedNever();

                entity.HasOne(d => d.BoilerPlate)
                    .WithMany(p => p.BoilerPlateParameters)
                    .HasForeignKey(d => d.BoilerPlateID)
                    .HasConstraintName("FK_BoilerPlateParameters_BoilerPlates");
            });


            modelBuilder.Entity<Branch>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.BranchGroup)
                    .WithMany(p => p.Branches)
                    .HasForeignKey(d => d.BranchGroupID)
                    .HasConstraintName("FK_Branches_BranchGroups");

                entity.HasOne(d => d.BranchStatuses)
                    .WithMany(p => p.Branches)
                    .HasForeignKey(d => d.StatusID)
                    .HasConstraintName("FK_Branches_BranchStatuses");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.Branches)
                    .HasForeignKey(d => d.WarehouseID)
                    .HasConstraintName("FK_Branches_Warehouses");
            });

            modelBuilder.Entity<BranchCultureData>(entity =>
            {
                entity.HasKey(e => new { e.CultureID, e.BranchID });

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.BranchCultureDatas)
                    .HasForeignKey(d => d.BranchID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BranchCultureDatas_Branches");

                entity.HasOne(d => d.Culture)
                    .WithMany(p => p.BranchCultureDatas)
                    .HasForeignKey(d => d.CultureID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BranchCultureDatas_Cultures");
            });

            modelBuilder.Entity<BranchDocumentTypeMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.BranchDocumentTypeMaps)
                    .HasForeignKey(d => d.BranchID)
                    .HasConstraintName("FK_BranchDocumentMaps_Branches");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.BranchDocumentTypeMaps)
                    .HasForeignKey(d => d.DocumentTypeID)
                    .HasConstraintName("FK_BranchDocumentMaps_DocumentTypes");
            });

            modelBuilder.Entity<BranchGroup>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.BranchGroupStatuses)
                    .WithMany(p => p.BranchGroups)
                    .HasForeignKey(d => d.StatusID)
                    .HasConstraintName("FK_BranchGroups_BranchGroupStatuses");
            });



            modelBuilder.Entity<Brand>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.SeoMetadata)
                    .WithMany(p => p.Brands)
                    .HasForeignKey(d => d.SEOMetadataID)
                    .HasConstraintName("FK_Brands_SEOMetaDataID");

                entity.HasOne(d => d.BrandStatus)
                    .WithMany(p => p.Brands)
                    .HasForeignKey(d => d.StatusID)
                    .HasConstraintName("FK_Brands_BrandStatuses");
            });

            modelBuilder.Entity<BrandCultureData>(entity =>
            {
                entity.HasKey(e => new { e.CultureID, e.BrandID });

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.BrandCultureDatas)
                    .HasForeignKey(d => d.BrandID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BrandCultureDatas_Brands");

                entity.HasOne(d => d.Culture)
                    .WithMany(p => p.BrandCultureDatas)
                    .HasForeignKey(d => d.CultureID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BrandCultureDatas_Cultures");
            });

            modelBuilder.Entity<BrandImageMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.BrandImageMaps)
                    .HasForeignKey(d => d.BrandID)
                    .HasConstraintName("FK_BrandImageMap_Brands");
            });



            modelBuilder.Entity<BrandTag>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.Brand)
                //    .WithMany(p => p.BrandTags)
                //    .HasForeignKey(d => d.BrandID)
                //    .HasConstraintName("FK_BrandTags_Brands");
            });

            modelBuilder.Entity<BrandTagMap>(entity =>
            {
                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.BrandTagMaps)
                    .HasForeignKey(d => d.BrandID)
                    .HasConstraintName("FK_BrandTagMaps_Brands");

                entity.HasOne(d => d.BrandTag)
                    .WithMany(p => p.BrandTagMaps)
                    .HasForeignKey(d => d.BrandTagID)
                    .HasConstraintName("FK_BrandTagMaps_BrandTags");
            });



            modelBuilder.Entity<Budget>(entity =>
            {
                entity.Property(e => e.BudgetID).ValueGeneratedNever();
            });

            //modelBuilder.Entity<Building>(entity =>
            //{
            //    entity.Property(e => e.BuildingID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<BuildingClassRoomMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Building)
            //        .WithMany(p => p.BuildingClassRoomMaps)
            //        .HasForeignKey(d => d.BuildingID)
            //        .HasConstraintName("FK_BuildingClassRoomMaps_Buildings");
            //});







            //modelBuilder.Entity<CCR_NOTE_DATE_WRONG_20230910>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<CIRCULAR_DATA_MOGRASYS>(entity =>
            //{
            //    entity.Property(e => e.sims_circular_status).IsFixedLength();
            //});



            //modelBuilder.Entity<CRMCompany>(entity =>
            //{
            //    entity.Property(e => e.CompanyID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<CRN_TRANHEAD_DATE_CHANGED_20230808>(entity =>
            //{
            //    entity.Property(e => e.TH_ID).ValueGeneratedOnAdd();
            //});

            //modelBuilder.Entity<CalendarEntry>(entity =>
            //{
            //    entity.Property(e => e.CalendarEntryID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicCalendar)
            //        .WithMany(p => p.CalendarEntries)
            //        .HasForeignKey(d => d.AcademicCalendarID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_CalendarEntries_AcadamicCalenders");
            //});

            //modelBuilder.Entity<Campaign>(entity =>
            //{
            //    entity.HasOne(d => d.CampaignType)
            //        .WithMany(p => p.Campaigns)
            //        .HasForeignKey(d => d.CampaignTypeID)
            //        .HasConstraintName("FK_Campaigns_CampaignTypes");
            //});

            //modelBuilder.Entity<CampaignEmployeeMap>(entity =>
            //{
            //    entity.HasKey(e => e.CompaignEmployeeMapIID)
            //        .HasName("PK_CompaignEmployeeMaps");

            //    entity.HasOne(d => d.Compaign)
            //        .WithMany(p => p.CampaignEmployeeMaps)
            //        .HasForeignKey(d => d.CompaignID)
            //        .HasConstraintName("FK_CompaignEmployeeMaps_SocailMediaCampaigns");

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.CampaignEmployeeMaps)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_CompaignEmployeeMaps_Employees");
            //});

            //modelBuilder.Entity<CampaignStatSummaryMap>(entity =>
            //{
            //    entity.HasOne(d => d.Campaign)
            //        .WithMany(p => p.CampaignStatSummaryMaps)
            //        .HasForeignKey(d => d.CampaignID)
            //        .HasConstraintName("FK_CampaignStatSummaryMaps_SocailMediaCampaigns");

            //    entity.HasOne(d => d.CampaignStatType)
            //        .WithMany(p => p.CampaignStatSummaryMaps)
            //        .HasForeignKey(d => d.CampaignStatTypeID)
            //        .HasConstraintName("FK_CampaignStatSummaryMaps_CampaignStatTypes");
            //});

            //modelBuilder.Entity<CampaignStatType>(entity =>
            //{
            //    entity.Property(e => e.CampaignStatTypeID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<CampaignTag>(entity =>
            //{
            //    entity.Property(e => e.CampaignTagIID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<CampaignType>(entity =>
            //{
            //    entity.Property(e => e.CampaignTypeID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<CampusTransfer>(entity =>
            //{
            //    entity.HasOne(d => d.FromAcademicYear)
            //        .WithMany(p => p.CampusTransferFromAcademicYears)
            //        .HasForeignKey(d => d.FromAcademicYearID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_CampusTransfers_FrmAcademicYears");

            //    entity.HasOne(d => d.FromClass)
            //        .WithMany(p => p.CampusTransferFromClasses)
            //        .HasForeignKey(d => d.FromClassID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_CampusTransfers_FromClass");

            //    entity.HasOne(d => d.FromSchool)
            //        .WithMany(p => p.CampusTransfers)
            //        .HasForeignKey(d => d.FromSchoolID)
            //        .HasConstraintName("FK_CampusTransfers_FromSchoolID");

            //    entity.HasOne(d => d.FromSection)
            //        .WithMany(p => p.CampusTransferFromSections)
            //        .HasForeignKey(d => d.FromSectionID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_CampusTransfers_FromSection");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.CampusTransfers)
            //        .HasForeignKey(d => d.StudentID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_CampusTransfers_Students");

            //    entity.HasOne(d => d.ToAcademicYear)
            //        .WithMany(p => p.CampusTransferToAcademicYears)
            //        .HasForeignKey(d => d.ToAcademicYearID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_CampusTransfers_ToAcademicYears");

            //    entity.HasOne(d => d.ToClass)
            //        .WithMany(p => p.CampusTransferToClasses)
            //        .HasForeignKey(d => d.ToClassID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_CampusTransfers_ToClass");

            //    entity.HasOne(d => d.ToSection)
            //        .WithMany(p => p.CampusTransferToSections)
            //        .HasForeignKey(d => d.ToSectionID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_CampusTransfers_ToSection");
            //});


            //modelBuilder.Entity<Candidate>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.CandidateStatus)
            //        .WithMany(p => p.Candidates)
            //        .HasForeignKey(d => d.CandidateStatusID)
            //        .HasConstraintName("FK_Candidate_CandidateStatus");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.Candidates)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_Candidate_Class");

            //    entity.HasOne(d => d.StudentApplication)
            //        .WithMany(p => p.Candidates)
            //        .HasForeignKey(d => d.StudentApplicationID)
            //        .HasConstraintName("FK_Candidate_StudentApplication");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.Candidates)
            //        .HasForeignKey(d => d.StudentID)
            //        .HasConstraintName("FK_Candidates_Students");
            //});

            //modelBuilder.Entity<CandidateAnswer>(entity =>
            //{
            //    entity.HasOne(d => d.Candidate)
            //        .WithMany(p => p.CandidateAnswers)
            //        .HasForeignKey(d => d.CandidateID)
            //        .HasConstraintName("FK_CandidateAnswers_Candidate");

            //    entity.HasOne(d => d.CandidateOnlineExamMap)
            //        .WithMany(p => p.CandidateAnswers)
            //        .HasForeignKey(d => d.CandidateOnlineExamMapID)
            //        .HasConstraintName("FK_CandidateAnswers_CandidateOnlineExamMap");

            //    entity.HasOne(d => d.Question)
            //        .WithMany(p => p.CandidateAnswers)
            //        .HasForeignKey(d => d.QuestionID)
            //        .HasConstraintName("FK_CandidateAnswers_Question");

            //    entity.HasOne(d => d.QuestionOptionMap)
            //        .WithMany(p => p.CandidateAnswers)
            //        .HasForeignKey(d => d.QuestionOptionMapID)
            //        .HasConstraintName("FK_CandidateAnswers_QuestionOptionMap");
            //});



            //modelBuilder.Entity<CandidateAssesment>(entity =>
            //{
            //    entity.HasOne(d => d.AnswerQuestionOptionMap)
            //        .WithMany(p => p.CandidateAssesmentAnswerQuestionOptionMaps)
            //        .HasForeignKey(d => d.AnswerQuestionOptionMapID)
            //        .HasConstraintName("FK_CandidateAssesments_QuestionOptionMaps1");

            //    entity.HasOne(d => d.CandidateOnlinExamMap)
            //        .WithMany(p => p.CandidateAssesments)
            //        .HasForeignKey(d => d.CandidateOnlinExamMapID)
            //        .HasConstraintName("FK_CandidateAssesments_CandidateOnlineExamMaps");

            //    entity.HasOne(d => d.SelectedQuestionOptionMap)
            //        .WithMany(p => p.CandidateAssesmentSelectedQuestionOptionMaps)
            //        .HasForeignKey(d => d.SelectedQuestionOptionMapID)
            //        .HasConstraintName("FK_CandidateAssesments_QuestionOptionMaps");
            //});

            //modelBuilder.Entity<CandidateOnlineExamMap>(entity =>
            //{
            //    entity.HasKey(e => e.CandidateOnlinExamMapIID)
            //        .HasName("PK_CandidateOnlinExamMaps");

            //    entity.HasOne(d => d.Candidate)
            //        .WithMany(p => p.CandidateOnlineExamMaps)
            //        .HasForeignKey(d => d.CandidateID)
            //        .HasConstraintName("FK_CandidateOnlinExamMaps_Candidates");

            //    entity.HasOne(d => d.OnlineExam)
            //        .WithMany(p => p.CandidateOnlineExamMaps)
            //        .HasForeignKey(d => d.OnlineExamID)
            //        .HasConstraintName("FK_CandidateOnlinExamMaps_OnlineExams");

            //    entity.HasOne(d => d.OnlineExamOperationStatus)
            //        .WithMany(p => p.CandidateOnlineExamMaps)
            //        .HasForeignKey(d => d.OnlineExamOperationStatusID)
            //        .HasConstraintName("FK_CandidateOnlineExamMaps_OnlineExamOperationStatuses");

            //    entity.HasOne(d => d.OnlineExamStatus)
            //        .WithMany(p => p.CandidateOnlineExamMaps)
            //        .HasForeignKey(d => d.OnlineExamStatusID)
            //        .HasConstraintName("FK_CandidateOnlineExamMaps_OnlineExamStatuses");
            //});



            modelBuilder.Entity<CardType>(entity =>
            {
                entity.Property(e => e.CardTypeID).ValueGeneratedNever();
            });

            //modelBuilder.Entity<CartCharge>(entity =>
            //{
            //    entity.Property(e => e.CartChargeID).ValueGeneratedNever();

            //    entity.HasOne(d => d.CartChargeType)
            //        .WithMany(p => p.CartCharges)
            //        .HasForeignKey(d => d.CartChargeTypeID)
            //        .HasConstraintName("FK_CartCharges_CartChargeTypes");
            //});

            //modelBuilder.Entity<CartChargeCultureData>(entity =>
            //{
            //    entity.HasKey(e => new { e.CartChargeID, e.CultureID });
            //});

            //modelBuilder.Entity<CashChangeCultureData>(entity =>
            //{
            //    entity.HasKey(e => new { e.CashChangeID, e.CultureID });

            //    entity.HasOne(d => d.CashChange)
            //        .WithMany(p => p.CashChangeCultureDatas)
            //        .HasForeignKey(d => d.CashChangeID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_CashChangeCultureDatas_CashChanges");

            //    entity.HasOne(d => d.Culture)
            //        .WithMany(p => p.CashChangeCultureDatas)
            //        .HasForeignKey(d => d.CultureID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_CashChangeCultureDatas_Cultures");
            //});

            //modelBuilder.Entity<CashChanx>(entity =>
            //{
            //    entity.Property(e => e.CashChangeID).ValueGeneratedNever();
            //});

            modelBuilder.Entity<Cast>(entity =>
            {
                entity.HasOne(d => d.Relegion)
                    .WithMany(p => p.Casts)
                    .HasForeignKey(d => d.RelegionID)
                    .HasConstraintName("FK_Casts_Relegions");
            });



            modelBuilder.Entity<Category>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.Class)
                //    .WithMany(p => p.Categories)
                //    .HasForeignKey(d => d.ClassID)
                //    .HasConstraintName("FK_Category_Class");

                entity.HasOne(d => d.SeoMetadata)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.SeoMetadataID)
                    .HasConstraintName("FK_Categories_SeoMetadatas");
            });



            modelBuilder.Entity<CategoryCultureData>(entity =>
            {
                entity.HasKey(e => new { e.CultureID, e.CategoryID });

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategoryCultureDatas)
                    .HasForeignKey(d => d.CategoryID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoryCultureDatas_Categories");

                entity.HasOne(d => d.Culture)
                    .WithMany(p => p.CategoryCultureDatas)
                    .HasForeignKey(d => d.CultureID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoryCultureDatas_Cultures");
            });

            //modelBuilder.Entity<CategoryFeeMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});



            modelBuilder.Entity<CategoryImageMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategoryImageMaps)
                    .HasForeignKey(d => d.CategoryID)
                    .HasConstraintName("FK_CategoryImageMaps_Categories");

                entity.HasOne(d => d.ImageType)
                    .WithMany(p => p.CategoryImageMaps)
                    .HasForeignKey(d => d.ImageTypeID)
                    .HasConstraintName("FK_CategoryImageMaps_ImageTypes");
            });

            //modelBuilder.Entity<CategoryPageBoilerPlatMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.PageBoilerplateMap)
            //        .WithMany(p => p.CategoryPageBoilerPlatMaps)
            //        .HasForeignKey(d => d.PageBoilerplateMapID)
            //        .HasConstraintName("FK_CategoryPageBoilerPlatMaps_PageBoilerplateMaps");
            //});





            modelBuilder.Entity<CategorySetting>(entity =>
            {
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategorySettings)
                    .HasForeignKey(d => d.CategoryID)
                    .HasConstraintName("FK_CategorySettings_Categories");

                entity.HasOne(d => d.UIControlType)
                    .WithMany(p => p.CategorySettings)
                    .HasForeignKey(d => d.UIControlTypeID)
                    .HasConstraintName("FK_CategorySettings_UIControlTypes");
            });

            modelBuilder.Entity<CategoryTag>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.Category)
                //    .WithMany(p => p.CategoryTags)
                //    .HasForeignKey(d => d.CategoryID)
                //    .HasConstraintName("FK_CategoryTags_Categories");
            });

            modelBuilder.Entity<CategoryTagMap>(entity =>
            {
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategoryTagMaps)
                    .HasForeignKey(d => d.CategoryID)
                    .HasConstraintName("FK_CategoryTagMaps_Categories");

                entity.HasOne(d => d.CategoryTag)
                    .WithMany(p => p.CategoryTagMaps)
                    .HasForeignKey(d => d.CategoryTagID)
                    .HasConstraintName("FK_CategoryTagMaps_CategoryTags");
            });

            //modelBuilder.Entity<CategoryTree1>(entity =>
            //{
            //    entity.HasKey(e => e.CategoryIID)
            //        .HasName("PK__Category__68320745B6A3AF09");

            //    entity.Property(e => e.CategoryIID).ValueGeneratedNever();
            //});

            modelBuilder.Entity<Catogory>(entity =>
            {
                entity.HasKey(e => e.CategoryID)
                    .HasName("PK_Category");
            });

            modelBuilder.Entity<CertificateLog>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.CertificateTemplateI)
                //    .WithMany(p => p.CertificateLogs)
                //    .HasForeignKey(d => d.CertificateTemplateIID)
                //    .HasConstraintName("FK_CertificateTemplates_CertificateTemplateIID");
            });

            modelBuilder.Entity<CertificateTemplate>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            //modelBuilder.Entity<CertificateTemplateParameter>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<Channel>(entity =>
            //{
            //    entity.Property(e => e.ChannelIID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<ChannelType>(entity =>
            //{
            //    entity.Property(e => e.ChannelTypeID).ValueGeneratedNever();
            //});


            modelBuilder.Entity<ChartMetadata>(entity =>
            {
                entity.Property(e => e.ChartMetadataID).ValueGeneratedNever();
            });



            modelBuilder.Entity<ChartOfAccount>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<ChartOfAccountMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.ChartOfAccountMaps)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_ChartOfAccountMaps_Accounts");

                entity.HasOne(d => d.ChartOfAccount)
                    .WithMany(p => p.ChartOfAccountMaps)
                    .HasForeignKey(d => d.ChartOfAccountID)
                    .HasConstraintName("FK_ChartOfAccountMaps_ChartOfAccounts");

                entity.HasOne(d => d.ChartRowType)
                    .WithMany(p => p.ChartOfAccountMaps)
                    .HasForeignKey(d => d.ChartRowTypeID)
                    .HasConstraintName("FK_ChartOfAccountMaps_ChartRowTypes");
            });



            modelBuilder.Entity<ChartRowType>(entity =>
            {
                entity.Property(e => e.ChartRowTypeID).ValueGeneratedNever();
            });

            //modelBuilder.Entity<Circular>(entity =>
            //{
            //    entity.HasOne(d => d.AcadamicYear)
            //        .WithMany(p => p.Circulars)
            //        .HasForeignKey(d => d.AcadamicYearID)
            //        .HasConstraintName("FK_Circulars_AcademicYears");

            //    entity.HasOne(d => d.CircularPriority)
            //        .WithMany(p => p.Circulars)
            //        .HasForeignKey(d => d.CircularPriorityID)
            //        .HasConstraintName("FK_Circulars_CircularPriorities");

            //    entity.HasOne(d => d.CircularStatus)
            //        .WithMany(p => p.Circulars)
            //        .HasForeignKey(d => d.CircularStatusID)
            //        .HasConstraintName("FK_Circulars_CircularStatuses");

            //    entity.HasOne(d => d.CircularType)
            //        .WithMany(p => p.Circulars)
            //        .HasForeignKey(d => d.CircularTypeID)
            //        .HasConstraintName("FK_Circulars_CirculateTypes");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.Circulars)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_Circulars_Circulars");
            //});

            //modelBuilder.Entity<CircularAttachmentMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Circular)
            //        .WithMany(p => p.CircularAttachmentMaps)
            //        .HasForeignKey(d => d.CircularID)
            //        .HasConstraintName("FK_CircularAttachmentMaps_Circular");
            //});



            //modelBuilder.Entity<CircularMap>(entity =>
            //{
            //    entity.HasOne(d => d.Circular)
            //        .WithMany(p => p.CircularMaps)
            //        .HasForeignKey(d => d.CircularID)
            //        .HasConstraintName("FK_CircularMaps_Circulars");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.CircularMaps)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_CircularMaps_Classes");

            //    entity.HasOne(d => d.Department)
            //        .WithMany(p => p.CircularMaps)
            //        .HasForeignKey(d => d.DepartmentID)
            //        .HasConstraintName("FK_CircularMaps_Departments");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.CircularMaps)
            //        .HasForeignKey(d => d.SectionID)
            //        .HasConstraintName("FK_CircularMaps_Sections");
            //});



            //modelBuilder.Entity<CircularType>(entity =>
            //{
            //    entity.HasKey(e => e.CirculateTypeID)
            //        .HasName("PK_CirculateTypes");
            //});

            //modelBuilder.Entity<CircularUserTypeMap>(entity =>
            //{
            //    entity.HasOne(d => d.Circular)
            //        .WithMany(p => p.CircularUserTypeMaps)
            //        .HasForeignKey(d => d.CircularID)
            //        .HasConstraintName("FK_CircularUserTypeMaps_Circulars");

            //    entity.HasOne(d => d.CircularUserType)
            //        .WithMany(p => p.CircularUserTypeMaps)
            //        .HasForeignKey(d => d.CircularUserTypeID)
            //        .HasConstraintName("FK_CircularUserTypeMaps_CircularUserTypes");
            //});

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.CityID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.Country)
                //    .WithMany(p => p.Cities)
                //    .HasForeignKey(d => d.CountryID)
                //    .HasConstraintName("FK_Cities_Countries");
            });


            modelBuilder.Entity<Claim>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.ClaimType)
                    .WithMany(p => p.Claims)
                    .HasForeignKey(d => d.ClaimTypeID)
                    .HasConstraintName("FK_Claims_ClaimTypes");
            });

            modelBuilder.Entity<ClaimLoginMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Claim)
                    .WithMany(p => p.ClaimLoginMaps)
                    .HasForeignKey(d => d.ClaimID)
                    .HasConstraintName("FK_ClaimLoginMaps_ClaimLoginMaps");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.ClaimLoginMaps)
                    .HasForeignKey(d => d.CompanyID)
                    .HasConstraintName("FK_ClaimLoginMaps_Companies");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.ClaimLoginMaps)
                    .HasForeignKey(d => d.LoginID)
                    .HasConstraintName("FK_ClaimLoginMaps_Logins");
            });



            modelBuilder.Entity<ClaimSet>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<ClaimSetClaimMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Claim)
                    .WithMany(p => p.ClaimSetClaimMaps)
                    .HasForeignKey(d => d.ClaimID)
                    .HasConstraintName("FK_ClaimSetClaimMaps_Claims");

                entity.HasOne(d => d.ClaimSet)
                    .WithMany(p => p.ClaimSetClaimMaps)
                    .HasForeignKey(d => d.ClaimSetID)
                    .HasConstraintName("FK_ClaimSetClaimMaps_ClaimSets");
            });

            modelBuilder.Entity<ClaimSetClaimSetMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.ClaimSet)
                    .WithMany(p => p.ClaimSetClaimSetMapClaimSets)
                    .HasForeignKey(d => d.ClaimSetID)
                    .HasConstraintName("FK_ClaimSetClaimSetMaps_ClaimSets");

                entity.HasOne(d => d.LinkedClaimSet)
                    .WithMany(p => p.ClaimSetClaimSetMapLinkedClaimSets)
                    .HasForeignKey(d => d.LinkedClaimSetID)
                    .HasConstraintName("FK_ClaimSetClaimSetMaps_ClaimSets1");
            });

            modelBuilder.Entity<ClaimSetLoginMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.ClaimSet)
                    .WithMany(p => p.ClaimSetLoginMaps)
                    .HasForeignKey(d => d.ClaimSetID)
                    .HasConstraintName("FK_ClaimSetLoginMaps_ClaimSets");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.ClaimSetLoginMaps)
                    .HasForeignKey(d => d.CompanyID)
                    .HasConstraintName("FK_ClaimSetLoginMaps_Companies");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.ClaimSetLoginMaps)
                    .HasForeignKey(d => d.LoginID)
                    .HasConstraintName("FK_ClaimSetLoginMaps_Logins");
            });



            modelBuilder.Entity<ClaimType>(entity =>
            {
                entity.Property(e => e.ClaimTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.Property(e => e.ClassID).ValueGeneratedNever();

                entity.Property(e => e.IsVisible).HasDefaultValueSql("((1))");

                entity.Property(e => e.ORDERNO).HasDefaultValueSql("((1))");

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.Classes)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_Classes_AcademicYear");

                //entity.HasOne(d => d.CostCenter)
                //    .WithMany(p => p.Classes)
                //    .HasForeignKey(d => d.CostCenterID)
                //    .HasConstraintName("FK_Classes_CostCenters");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.Classes)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_Classes_Schools");
            });

            //modelBuilder.Entity<ClassAssociateTeacherMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.ClassClassTeacherMap)
            //        .WithMany(p => p.ClassAssociateTeacherMaps)
            //        .HasForeignKey(d => d.ClassClassTeacherMapID)
            //        .HasConstraintName("FK_ClassssociateClassTeacherMap");

            //    entity.HasOne(d => d.Teacher)
            //        .WithMany(p => p.ClassAssociateTeacherMaps)
            //        .HasForeignKey(d => d.TeacherID)
            //        .HasConstraintName("FK_ClassAssociateTeacherMaps_Employees1");
            //});

            //modelBuilder.Entity<ClassClassGroupMap>(entity =>
            //{
            //    entity.HasKey(e => e.ClassClassGroupMapIID)
            //        .HasName("PK_[ClassClassGroupMaps");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.ClassClassGroupMaps)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_ClassGrpMaps_AcademicYearID");

            //    entity.HasOne(d => d.ClassGroup)
            //        .WithMany(p => p.ClassClassGroupMaps)
            //        .HasForeignKey(d => d.ClassGroupID)
            //        .HasConstraintName("FK_ClassClassGroupMaps_ClassGrps");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.ClassClassGroupMaps)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_ClassClassGroupMaps_class");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.ClassClassGroupMaps)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_ClassClassGroupMaps_School");
            //});

            //modelBuilder.Entity<ClassClassTeacherMap>(entity =>
            //{
            //    entity.HasKey(e => e.ClassClassTeacherMapIID)
            //        .HasName("PK_ClassClassTeacherMapss");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.ClassClassTeacherMaps)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_ClassClassTeacherMaps_AcademicYear");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.ClassClassTeacherMaps)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_ClassClassTeacherMaps_Class");

            //    entity.HasOne(d => d.Coordinator)
            //        .WithMany(p => p.ClassClassTeacherMapCoordinators)
            //        .HasForeignKey(d => d.CoordinatorID)
            //        .HasConstraintName("FK_ClassClassTeacherMaps_Coordinator");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.ClassClassTeacherMaps)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_ClassClassTeacherMaps_School");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.ClassClassTeacherMaps)
            //        .HasForeignKey(d => d.SectionID)
            //        .HasConstraintName("FK_ClassClassTeacherMaps_Section");

            //    entity.HasOne(d => d.Subject)
            //        .WithMany(p => p.ClassClassTeacherMaps)
            //        .HasForeignKey(d => d.SubjectID)
            //        .HasConstraintName("FK_ClassClassTeacherMaps_Parent");

            //    entity.HasOne(d => d.Teacher)
            //        .WithMany(p => p.ClassClassTeacherMapTeachers)
            //        .HasForeignKey(d => d.TeacherID)
            //        .HasConstraintName("FK_ClassClassTeacherMaps_Employees");
            //});

            //modelBuilder.Entity<ClassCoordinator>(entity =>
            //{
            //    entity.Property(e => e.ISACTIVE).HasDefaultValueSql("((1))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.ClassCoordinators)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_ClassCoordinator_AcademicYear");

            //    entity.HasOne(d => d.Coordinator)
            //        .WithMany(p => p.ClassCoordinators)
            //        .HasForeignKey(d => d.CoordinatorID)
            //        .HasConstraintName("FK_ClassCoordinator_Coordinator");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.ClassCoordinators)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_ClassCoordinator_School");
            //});

            //modelBuilder.Entity<ClassCoordinatorClassMap>(entity =>
            //{
            //    entity.HasOne(d => d.ClassCoordinator)
            //        .WithMany(p => p.ClassCoordinatorClassMaps)
            //        .HasForeignKey(d => d.ClassCoordinatorID)
            //        .HasConstraintName("FK_ClassCoordinatorClassMaps_Coordinators");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.ClassCoordinatorClassMaps)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_ClassCoordinatorClassMaps_Classes");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.ClassCoordinatorClassMaps)
            //        .HasForeignKey(d => d.SectionID)
            //        .HasConstraintName("FK_ClassCoordinatorClassMaps_Sections");
            //});

            //modelBuilder.Entity<ClassCoordinatorMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.ClassCoordinatorMaps)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_ClassCoordinatorMap_AcademicYear");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.ClassCoordinatorMaps)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_ClassCoordinatorMap_Class");

            //    entity.HasOne(d => d.Coordinator)
            //        .WithMany(p => p.ClassCoordinatorMaps)
            //        .HasForeignKey(d => d.CoordinatorID)
            //        .HasConstraintName("FK_ClassCoordinatorMap_Coordinator");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.ClassCoordinatorMaps)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_ClassCoordinatorMap_School");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.ClassCoordinatorMaps)
            //        .HasForeignKey(d => d.SectionID)
            //        .HasConstraintName("FK_ClassCoordinatorMap_Section");
            //});

            //modelBuilder.Entity<ClassFeeMaster>(entity =>
            //{
            //    entity.HasKey(e => e.ClassFeeMasterIID)
            //        .HasName("PK_FeeAssigns");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcadamicYear)
            //        .WithMany(p => p.ClassFeeMasters)
            //        .HasForeignKey(d => d.AcadamicYearID)
            //        .HasConstraintName("FK_FeeAssigns_AcademicYears");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.ClassFeeMasters)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_FeeAssigns_Classes");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.ClassFeeMasters)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_ClassFeeMasters_School");
            //});

            //modelBuilder.Entity<ClassFeeStructureMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcadamicYear)
            //        .WithMany(p => p.ClassFeeStructureMaps)
            //        .HasForeignKey(d => d.AcadamicYearID)
            //        .HasConstraintName("FK_ClassFeeStructureMaps_AcademicYears");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.ClassFeeStructureMaps)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_ClassFeeStructureMaps_Classes");

            //    entity.HasOne(d => d.FeeStructure)
            //        .WithMany(p => p.ClassFeeStructureMaps)
            //        .HasForeignKey(d => d.FeeStructureID)
            //        .HasConstraintName("FK_ClassFeeStructureMaps_FeeStructures");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.ClassFeeStructureMaps)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_ClassFeeStructureMaps_School");
            //});



            //modelBuilder.Entity<ClassGroup>(entity =>
            //{
            //    entity.HasKey(e => e.ClassGroupID)
            //        .IsClustered(false);

            //    entity.Property(e => e.ClassGroupID).ValueGeneratedNever();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.ClassGroups)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_ClassGroups_AcademicYear");

            //    entity.HasOne(d => d.HeadTeacher)
            //        .WithMany(p => p.ClassGroups)
            //        .HasForeignKey(d => d.HeadTeacherID)
            //        .HasConstraintName("FK_ClassGroups_HeadTeacher");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.ClassGroups)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_ClassGroups_School");

            //    entity.HasOne(d => d.Subject)
            //        .WithMany(p => p.ClassGroups)
            //        .HasForeignKey(d => d.SubjectID)
            //        .HasConstraintName("FK_ClassGroups_SubjectMap");
            //});

            //modelBuilder.Entity<ClassGroupTeacherMap>(entity =>
            //{
            //    entity.Property(e => e.IsHeadTeacher).HasDefaultValueSql("((0))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.ClassGroup)
            //        .WithMany(p => p.ClassGroupTeacherMaps)
            //        .HasForeignKey(d => d.ClassGroupID)
            //        .HasConstraintName("FK_ClassGrpTeacherMaps_ClassGrps");

            //    entity.HasOne(d => d.Teacher)
            //        .WithMany(p => p.ClassGroupTeacherMaps)
            //        .HasForeignKey(d => d.TeacherID)
            //        .HasConstraintName("FK_ClassGroupTeacherMaps_Teacher");
            //});



            //modelBuilder.Entity<ClassSectionMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.ClassSectionMaps)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_ClassSectionMaps_AcademicYear");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.ClassSectionMaps)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_ClassSectionMaps_Classes");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.ClassSectionMaps)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_ClassSectionMaps_Schools");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.ClassSectionMaps)
            //        .HasForeignKey(d => d.SectionID)
            //        .HasConstraintName("FK_ClassSectionMaps_Sections");
            //});


            //modelBuilder.Entity<ClassSubjectMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.ClassSubjectMaps)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_ClassSubjectMaps_AcademicYear");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.ClassSubjectMaps)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_ClassSubjectMaps_Classes");

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.ClassSubjectMaps)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_ClassSubjectMaps_Employees");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.ClassSubjectMaps)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_ClassSubjectMaps_School");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.ClassSubjectMaps)
            //        .HasForeignKey(d => d.SectionID)
            //        .HasConstraintName("FK_ClassSubjectMaps_Sections");

            //    entity.HasOne(d => d.Subject)
            //        .WithMany(p => p.ClassSubjectMaps)
            //        .HasForeignKey(d => d.SubjectID)
            //        .HasConstraintName("FK_ClassSubjectMaps_Subjects");
            //});

            //modelBuilder.Entity<ClassSubjectSkillGroupMap>(entity =>
            //{
            //    entity.Property(e => e.ClassSubjectSkillGroupMapID).ValueGeneratedNever();

            //    entity.Property(e => e.ISScholastic).HasDefaultValueSql("((0))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.ClassSubjectSkillGroupMaps)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_ClassSubjectSkillGroupMaps_AcademicYear");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.ClassSubjectSkillGroupMaps)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_ClassSubjectSkillGroupMaps_Classes");

            //    entity.HasOne(d => d.Exam)
            //        .WithMany(p => p.ClassSubjectSkillGroupMaps)
            //        .HasForeignKey(d => d.ExamID)
            //        .HasConstraintName("FK_ClassSubjectSkillGroupMaps_Exams");

            //    entity.HasOne(d => d.MarkGrade)
            //        .WithMany(p => p.ClassSubjectSkillGroupMaps)
            //        .HasForeignKey(d => d.MarkGradeID)
            //        .HasConstraintName("FK_ClassSubjectSkillGroupMaps_MarkGrades");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.ClassSubjectSkillGroupMaps)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_ClassSubjectSkillGroupMaps_School");

            //    entity.HasOne(d => d.Subject)
            //        .WithMany(p => p.ClassSubjectSkillGroupMaps)
            //        .HasForeignKey(d => d.SubjectID)
            //        .HasConstraintName("FK_ClassSubjectSkillGroupMaps_Subject");
            //});



            //modelBuilder.Entity<ClassSubjectSkillGroupSkillMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.ClassSubjectSkillGroupMap)
            //        .WithMany(p => p.ClassSubjectSkillGroupSkillMaps)
            //        .HasForeignKey(d => d.ClassSubjectSkillGroupMapID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_ClassSubjectSkillGroupSkillMaps_ClassSubjectSkillGroupMaps");

            //    entity.HasOne(d => d.MarkGrade)
            //        .WithMany(p => p.ClassSubjectSkillGroupSkillMaps)
            //        .HasForeignKey(d => d.MarkGradeID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_ClassSubjectSkillGroupSkillMaps_MarkGrades");

            //    entity.HasOne(d => d.SkillGroupMaster)
            //        .WithMany(p => p.ClassSubjectSkillGroupSkillMaps)
            //        .HasForeignKey(d => d.SkillGroupMasterID)
            //        .HasConstraintName("FK_ClassSubjectSkillGroupSkillMaps_SkillGroups");

            //    entity.HasOne(d => d.SkillMaster)
            //        .WithMany(p => p.ClassSubjectSkillGroupSkillMaps)
            //        .HasForeignKey(d => d.SkillMasterID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_ClassSubjectSkillGroupSkillMaps_SkillMaster");
            //});

            //modelBuilder.Entity<ClassSubjectWorkflowEntityMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.ClassSubjectMap)
            //        .WithMany(p => p.ClassSubjectWorkflowEntityMaps)
            //        .HasForeignKey(d => d.ClassSubjectMapID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_ClassSubjectMaps_ClassSubjectWorkflowEntity");

            //    entity.HasOne(d => d.Subject)
            //        .WithMany(p => p.ClassSubjectWorkflowEntityMaps)
            //        .HasForeignKey(d => d.SubjectID)
            //        .HasConstraintName("FK_ClassSubjectWorkflowEntityMaps_Subject");

            //    entity.HasOne(d => d.WorkflowEntity)
            //        .WithMany(p => p.ClassSubjectWorkflowEntityMaps)
            //        .HasForeignKey(d => d.WorkflowEntityID)
            //        .HasConstraintName("FK_ClassSubjectWorkflowEntity_WorkflowEntity");

            //    entity.HasOne(d => d.workflow)
            //        .WithMany(p => p.ClassSubjectWorkflowEntityMaps)
            //        .HasForeignKey(d => d.workflowID)
            //        .HasConstraintName("FK_ClassSubjectWorkflowEntity_Workflow");
            //});

            //modelBuilder.Entity<ClassTeacherMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.ClassTeacherMaps)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_ClassTeacherMaps_AcademicYear");

            //    entity.HasOne(d => d.ClassClassTeacherMap)
            //        .WithMany(p => p.ClassTeacherMaps)
            //        .HasForeignKey(d => d.ClassClassTeacherMapID)
            //        .HasConstraintName("FK_ClassGroupTeacherMaps_ClassTeacherMap");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.ClassTeacherMaps)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_ClassTeacherMaps_Classes");

            //    entity.HasOne(d => d.ClassTeacher)
            //        .WithMany(p => p.ClassTeacherMapClassTeachers)
            //        .HasForeignKey(d => d.ClassTeacherID)
            //        .HasConstraintName("FK_ClassTeacherMaps_ClassTeacher");

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.ClassTeacherMapEmployees)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_ClassTeacherMaps_Employees");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.ClassTeacherMaps)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_ClassTeacherMaps_School");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.ClassTeacherMaps)
            //        .HasForeignKey(d => d.SectionID)
            //        .HasConstraintName("FK_ClassTeacherMaps_Sections");

            //    entity.HasOne(d => d.Subject)
            //        .WithMany(p => p.ClassTeacherMaps)
            //        .HasForeignKey(d => d.SubjectID)
            //        .HasConstraintName("FK_OtherTeacherMaps_Subject");

            //    entity.HasOne(d => d.Teacher)
            //        .WithMany(p => p.ClassTeacherMapTeachers)
            //        .HasForeignKey(d => d.TeacherID)
            //        .HasConstraintName("FK_ClassTeacherMaps_Employees1");
            //});



            //modelBuilder.Entity<ClassTiming>(entity =>
            //{
            //    entity.Property(e => e.ClassTimingID).ValueGeneratedNever();

            //    entity.Property(e => e.IsBreakTime).HasDefaultValueSql("((0))");

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.ClassTimings)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_ClassTimings_AcademicYear");

            //    entity.HasOne(d => d.BreakType)
            //        .WithMany(p => p.ClassTimings)
            //        .HasForeignKey(d => d.BreakTypeID)
            //        .HasConstraintName("FK_ClassTimings_BreakTypes");

            //    entity.HasOne(d => d.ClassTimingSet)
            //        .WithMany(p => p.ClassTimings)
            //        .HasForeignKey(d => d.ClassTimingSetID)
            //        .HasConstraintName("FK_ClassTimings_ClassTimingSets");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.ClassTimings)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_ClassTimings_School");
            //});



            //modelBuilder.Entity<ClassWorkFlowMap>(entity =>
            //{
            //    entity.HasKey(e => e.ClassWorkFlowIID)
            //        .HasName("PK_ClassWorkFlowMap");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.ClassWorkFlowMaps)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_ClassWorkFlowMaps_Class");

            //    entity.HasOne(d => d.WorkflowEntity)
            //        .WithMany(p => p.ClassWorkFlowMaps)
            //        .HasForeignKey(d => d.WorkflowEntityID)
            //        .HasConstraintName("FK_ClassWorkFlowMaps_WorkflowEntity");

            //    entity.HasOne(d => d.Workflow)
            //        .WithMany(p => p.ClassWorkFlowMaps)
            //        .HasForeignKey(d => d.WorkflowID)
            //        .HasConstraintName("FK_ClassWorkFlowMaps_Workflow");
            //});

            modelBuilder.Entity<Comment>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.EntityType)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.EntityTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comments_EntityType");

                entity.HasOne(d => d.ParentComment)
                    .WithMany(p => p.Comments1)
                    .HasForeignKey(d => d.ParentCommentID)
                    .HasConstraintName("FK_Comments_Comments");
                entity.HasOne(c => c.FromEmployee)
                    .WithMany()
                    .HasForeignKey(c => c.FromLoginID)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Comments_Employees_FromLoginID"); // Ensure foreign key constraint name is correct
            });

            modelBuilder.Entity<Communication>(entity =>
            {
                entity.HasKey(e => e.CommunicationIID)
                    .HasName("PK_LeadCommunications");

                entity.HasOne(d => d.CommunicationType)
                    .WithMany(p => p.Communications)
                    .HasForeignKey(d => d.CommunicationTypeID)
                    .HasConstraintName("FK_LeadCommunications_CommunicationTypes");

                entity.HasOne(d => d.EmailTemplate)
                    .WithMany(p => p.Communications)
                    .HasForeignKey(d => d.EmailTemplateID)
                    .HasConstraintName("FK_LeadCommunications_EmailTemplates");

                entity.HasOne(d => d.Lead)
                    .WithMany(p => p.Communications)
                    .HasForeignKey(d => d.LeadID)
                    .HasConstraintName("FK_Communications_Leads");
            });

            //modelBuilder.Entity<CommunicationLog>(entity =>
            //{
            //    entity.HasOne(d => d.CommunicationStatus)
            //        .WithMany(p => p.CommunicationLogs)
            //        .HasForeignKey(d => d.CommunicationStatusID)
            //        .HasConstraintName("FK_CommunicationLogs_CommunicationStatuses");

            //    entity.HasOne(d => d.CommunicationType)
            //        .WithMany(p => p.CommunicationLogs)
            //        .HasForeignKey(d => d.CommunicationTypeID)
            //        .HasConstraintName("FK_CommunicationLogs_CommunicationTypes");

            //    entity.HasOne(d => d.Login)
            //        .WithMany(p => p.CommunicationLogs)
            //        .HasForeignKey(d => d.LoginID)
            //        .HasConstraintName("FK_CommunicationLogs_Logins");
            //});

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.CompanyID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.BaseCurrencyID)
                    .HasConstraintName("FK_Companies_Currencies");

                //entity.HasOne(d => d.CompanyGroup)
                //    .WithMany(p => p.Companies)
                //    .HasForeignKey(d => d.CompanyGroupID)
                //    .HasConstraintName("FK_Companies_CompanyGroups");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.CountryID)
                    .HasConstraintName("FK_Companies_Companies");

                entity.HasOne(d => d.CompanyStatuses)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.StatusID)
                    .HasConstraintName("FK_Companies_CompanyStatuses");
            });

            modelBuilder.Entity<CompanyCurrencyMap>(entity =>
            {
                entity.HasKey(e => new { e.CompanyID, e.CurrencyID });

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyCurrencyMaps)
                    .HasForeignKey(d => d.CompanyID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyCurrencyMaps_Companies");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.CompanyCurrencyMaps)
                    .HasForeignKey(d => d.CurrencyID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyCurrencyMaps_Currencies");
            });

            //modelBuilder.Entity<CompanyGroup>(entity =>
            //{
            //    entity.Property(e => e.CompanyGroupID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<Company_FiscalYear_Close>(entity =>
            //{
            //    entity.HasKey(e => e.CFC_ID)
            //        .IsClustered(false);
            //});

            //modelBuilder.Entity<Complain>(entity =>
            //{
            //    entity.Property(e => e.ComplainIID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<Contact>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<ContentFile>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.ContentType)
            //        .WithMany(p => p.ContentFiles)
            //        .HasForeignKey(d => d.ContentTypeID)
            //        .HasConstraintName("FK_ContentFiles_ContentTypes");
            //});

            //modelBuilder.Entity<ContentType>(entity =>
            //{
            //    entity.Property(e => e.ContentTypeID).ValueGeneratedNever();
            //});


            modelBuilder.Entity<CostCenter>(entity =>
            {
                entity.Property(e => e.CostCenterID).ValueGeneratedNever();

                //entity.Property(e => e.IsAffect_A).HasDefaultValueSql("((0))");

                //entity.Property(e => e.IsAffect_C).HasDefaultValueSql("((0))");

                //entity.Property(e => e.IsAffect_E).HasDefaultValueSql("((1))");

                //entity.Property(e => e.IsAffect_I).HasDefaultValueSql("((1))");

                //entity.Property(e => e.IsAffect_L).HasDefaultValueSql("((0))");

                //entity.Property(e => e.IsFixed).HasDefaultValueSql("((0))");

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.CostCenters)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_CostCenters_AcademicYear");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.CostCenters)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_CostCenters_School");
            });

            modelBuilder.Entity<CostCenterAccountMap>(entity =>
            {
                entity.Property(e => e.CostCenterAccountMapIID).ValueGeneratedOnAdd();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.CostCenter)
                    .WithMany()
                    .HasForeignKey(d => d.CostCenterID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CostCentersAccountMaps_CostCenter");
            });



            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.CountryID).ValueGeneratedNever();

                //entity.HasOne(d => d.Currency)
                //    .WithMany(p => p.Countries)
                //    .HasForeignKey(d => d.CurrencyID)
                //    .HasConstraintName("FK_Countries_Currencies");
            });

            //modelBuilder.Entity<CreditNoteFeeTypeMap>(entity =>
            //{
            //    entity.HasKey(e => e.CreditNoteFeeTypeMapIID)
            //        .HasName("PK_CreditNoteFeeTypeMapIID");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.CreditNoteFeeTypeMaps)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_CreditNoteFeeTypeMaps_AcademicYear");

            //    entity.HasOne(d => d.FeeDueFeeTypeMaps)
            //        .WithMany(p => p.CreditNoteFeeTypeMaps)
            //        .HasForeignKey(d => d.FeeDueFeeTypeMapsID)
            //        .HasConstraintName("FK_CreditNoteFeeTypeMaps_FeeDueFeeTypeMaps");

            //    entity.HasOne(d => d.FeeDueMonthlySplit)
            //        .WithMany(p => p.CreditNoteFeeTypeMaps)
            //        .HasForeignKey(d => d.FeeDueMonthlySplitID)
            //        .HasConstraintName("FK_CreditNoteFeeTypeMaps_FeeDueMonthlySplits");

            //    entity.HasOne(d => d.FeeMaster)
            //        .WithMany(p => p.CreditNoteFeeTypeMaps)
            //        .HasForeignKey(d => d.FeeMasterID)
            //        .HasConstraintName("FK_CreditNoteFeeTypeMaps_FeeMasters");

            //    entity.HasOne(d => d.Period)
            //        .WithMany(p => p.CreditNoteFeeTypeMaps)
            //        .HasForeignKey(d => d.PeriodID)
            //        .HasConstraintName("FK_CreditNoteFeeTypeMaps_FeePeriods");

            //    entity.HasOne(d => d.SchoolCreditNote)
            //        .WithMany(p => p.CreditNoteFeeTypeMaps)
            //        .HasForeignKey(d => d.SchoolCreditNoteID)
            //        .HasConstraintName("FK_CreditNoteFeeTypeMaps_SchoolCreditNote");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.CreditNoteFeeTypeMaps)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_CreditNoteFeeTypeMaps_School");
            //});

            modelBuilder.Entity<CultureData>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Culture)
                    .WithMany(p => p.CultureDatas)
                    .HasForeignKey(d => d.CultureID)
                    .HasConstraintName("FK_CultureDatas_Cultures");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.Property(e => e.CurrencyID).ValueGeneratedNever();

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Currencies)
                    .HasForeignKey(d => d.CompanyID)
                    .HasConstraintName("FK_Currencies_Companies");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.DefaultStudent)
                //    .WithMany(p => p.Customers)
                //    .HasForeignKey(d => d.DefaultStudentID)
                //    .HasConstraintName("FK_customers_Students");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.LoginID)
                    .HasConstraintName("FK_Customers_Logins");
            });

            modelBuilder.Entity<CustomerAccountMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.CustomerAccountMaps)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_CustomerAccountMaps_Accounts");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerAccountMaps)
                    .HasForeignKey(d => d.CustomerID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerAccountMaps_Customers");

                entity.HasOne(d => d.EntityTypeEntitlement)
                    .WithMany(p => p.CustomerAccountMaps)
                    .HasForeignKey(d => d.EntitlementID)
                    .HasConstraintName("FK_CustomerAccountMaps_EntityTypeEntitlements");
            });

            modelBuilder.Entity<CustomerCard>(entity =>
            {
                entity.HasOne(d => d.CardType)
                    .WithMany(p => p.CustomerCards)
                    .HasForeignKey(d => d.CardTypeID)
                    .HasConstraintName("FK_CustomerCards_CardTypes");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerCards)
                    .HasForeignKey(d => d.CustomerID)
                    .HasConstraintName("FK_CustomerCards_Customers");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.CustomerCards)
                    .HasForeignKey(d => d.LoginID)
                    .HasConstraintName("FK_CustomerCards_Logins");
            });



            //modelBuilder.Entity<CustomerFeedBack>(entity =>
            //{
            //    entity.HasOne(d => d.CustomerFeedbackType)
            //        .WithMany(p => p.CustomerFeedBacks)
            //        .HasForeignKey(d => d.CustomerFeedbackTypeID)
            //        .HasConstraintName("FK_CustomerFeedBacks_CustomerFeedbackTypes");
            //});

            //modelBuilder.Entity<CustomerGroup>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});



            modelBuilder.Entity<CustomerGroupDeliveryTypeMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.CustomerGroup)
                    .WithMany(p => p.CustomerGroupDeliveryTypeMaps)
                    .HasForeignKey(d => d.CustomerGroupID)
                    .HasConstraintName("FK_CustomerGroupDeliveryTypeMaps_CustomerGroups");

                entity.HasOne(d => d.DeliveryTypes1)
                    .WithMany(p => p.CustomerGroupDeliveryTypeMaps)
                    .HasForeignKey(d => d.DeliveryTypeID)
                    .HasConstraintName("FK_CustomerGroupDeliveryTypeMaps_DeliveryTypes");
            });

            //modelBuilder.Entity<CustomerGroupLoginMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});



            modelBuilder.Entity<CustomerJustAsk>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Culture)
                    .WithMany(p => p.CustomerJustAsks)
                    .HasForeignKey(d => d.CultureID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerJustAsk_Cultures");
            });

            modelBuilder.Entity<CustomerProductReference>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerProductReferences)
                    .HasForeignKey(d => d.CustomerID)
                    .HasConstraintName("FK_CustomerProductReferences_Customers");

                entity.HasOne(d => d.ProductSKUMap)
                    .WithMany(p => p.CustomerProductReferences)
                    .HasForeignKey(d => d.ProductSKUMapID)
                    .HasConstraintName("FK_CustomerProductReferences_ProductSKUMaps");
            });



            modelBuilder.Entity<CustomerSetting>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerSettings)
                    .HasForeignKey(d => d.CustomerID)
                    .HasConstraintName("FK_CustomerSettings_Customers");
            });



            modelBuilder.Entity<CustomerSupplierMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerSupplierMaps)
                    .HasForeignKey(d => d.CustomerID)
                    .HasConstraintName("FK_CustomerSupplierMaps_Customers");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.CustomerSupplierMaps)
                    .HasForeignKey(d => d.SupplierID)
                    .HasConstraintName("FK_CustomerSupplierMap_Suppliers");
            });

            modelBuilder.Entity<CustomerSupportTicket>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Culture)
                    .WithMany(p => p.CustomerSupportTickets)
                    .HasForeignKey(d => d.CultureID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerSupportTicket_Cultures");
            });




            modelBuilder.Entity<DataFeedLog>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.DataFeedStatus)
                    .WithMany(p => p.DataFeedLogs)
                    .HasForeignKey(d => d.DataFeedStatusID)
                    .HasConstraintName("FK_DataFeedLogs_DataFeedStatuses");

                entity.HasOne(d => d.DataFeedType)
                    .WithMany(p => p.DataFeedLogs)
                    .HasForeignKey(d => d.DataFeedTypeID)
                    .HasConstraintName("FK_DataFeedLogs_DataFeedTypes");
            });



            modelBuilder.Entity<DataFeedStatus>(entity =>
            {
                entity.Property(e => e.DataFeedStatusID).ValueGeneratedNever();
            });

            modelBuilder.Entity<DataFeedTable>(entity =>
            {
                entity.Property(e => e.DataFeedTableID).ValueGeneratedNever();

                entity.HasOne(d => d.DataFeedType)
                    .WithMany(p => p.DataFeedTables)
                    .HasForeignKey(d => d.DataFeedTypeID)
                    .HasConstraintName("FK_DataFeedTables_DataFeedTypes");
            });

            modelBuilder.Entity<DataFeedTableColumn>(entity =>
            {
                entity.Property(e => e.DataFeedTableColumnID).ValueGeneratedNever();

                entity.HasOne(d => d.DataFeedTable)
                    .WithMany(p => p.DataFeedTableColumns)
                    .HasForeignKey(d => d.DataFeedTableID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DataFeedTableColumns_DataFeedTables");
            });

            modelBuilder.Entity<DataFeedType>(entity =>
            {
                entity.Property(e => e.DataFeedTypeID).ValueGeneratedNever();

                entity.HasOne(d => d.DataFeedOperation)
                    .WithMany(p => p.DataFeedTypes)
                    .HasForeignKey(d => d.OperationID)
                    .HasConstraintName("FK_DataFeedTypes_DataFeedOperations");
            });



            modelBuilder.Entity<DataFormat>(entity =>
            {
                entity.Property(e => e.DataFormatID).ValueGeneratedNever();

                entity.HasOne(d => d.DataFormatType)
                    .WithMany(p => p.DataFormats)
                    .HasForeignKey(d => d.DataFormatTypeID)
                    .HasConstraintName("FK_DataFormats_DataFormatTypes");
            });

            modelBuilder.Entity<DataFormatType>(entity =>
            {
                entity.Property(e => e.DataFormatTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<DataHistoryEntity>(entity =>
            {
                entity.Property(e => e.DataHistoryEntityID).ValueGeneratedNever();
            });

            modelBuilder.Entity<DeliveryCharge>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.CountryGroup)
                //    .WithMany(p => p.DeliveryCharges)
                //    .HasForeignKey(d => d.CountryGroupID)
                //    .HasConstraintName("FK_DeliveryCharges_ServiceProviderCountryGroups");

                entity.HasOne(d => d.DeliveryType)
                    .WithMany(p => p.DeliveryCharges)
                    .HasForeignKey(d => d.DeliveryTypeID)
                    .HasConstraintName("FK_DeliveryCharges_DeliveryTypes");

                entity.HasOne(d => d.FromCountry)
                    .WithMany(p => p.DeliveryChargeFromCountries)
                    .HasForeignKey(d => d.FromCountryID)
                    .HasConstraintName("FK_DeliveryCharges_Countries1");

                entity.HasOne(d => d.ServiceProvider)
                    .WithMany(p => p.DeliveryCharges)
                    .HasForeignKey(d => d.ServiceProviderID)
                    .HasConstraintName("FK_DeliveryCharges_ServiceProviders");

                entity.HasOne(d => d.ToCountry)
                    .WithMany(p => p.DeliveryChargeToCountries)
                    .HasForeignKey(d => d.ToCountryID)
                    .HasConstraintName("FK_DeliveryCharges_Countries11");
            });

            //modelBuilder.Entity<DeliveryTimeSlot>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<DeliveryTimeSlotBranchMap>(entity =>
            //{
            //    entity.Property(e => e.DeliveryTimeSlotBranchMapID).ValueGeneratedNever();

            //    entity.HasOne(d => d.Branch)
            //        .WithMany(p => p.DeliveryTimeSlotBranchMaps)
            //        .HasForeignKey(d => d.BranchID)
            //        .HasConstraintName("FK_DeliveryTimeSlotBranchMaps_DeliveryTimeSlotBranchMaps");

            //    entity.HasOne(d => d.DeliveryTimeSlot)
            //        .WithMany(p => p.DeliveryTimeSlotBranchMaps)
            //        .HasForeignKey(d => d.DeliveryTimeSlotID)
            //        .HasConstraintName("FK_DeliveryTimeSlotBranchMaps_DeliveryTimeSlots");
            //});

            modelBuilder.Entity<DeliveryType>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<DeliveryTypes1>(entity =>
            {
                entity.HasKey(e => e.DeliveryTypeID)
                    .HasName("PK_DeliveryTypes_1");

                entity.Property(e => e.DeliveryTypeID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.DeliveryTypeStatus)
                    .WithMany(p => p.DeliveryTypes1)
                    .HasForeignKey(d => d.StatusID)
                    .HasConstraintName("FK_DeliveryTypes_DeliveryTypeStatuses");
            });

            modelBuilder.Entity<DeliveryTypeAllowedAreaMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.DeliveryTypeAllowedAreaMaps)
                    .HasForeignKey(d => d.AreaID)
                    .HasConstraintName("FK_DeliveryTypeAllowedAreaMaps_Areas");

                entity.HasOne(d => d.DeliveryTypes1)
                    .WithMany(p => p.DeliveryTypeAllowedAreaMaps)
                    .HasForeignKey(d => d.DeliveryTypeID)
                    .HasConstraintName("FK_DeliveryTypeAllowedAreaMaps_DeliveryTypes");
            });

            modelBuilder.Entity<DeliveryTypeAllowedCountryMap>(entity =>
            {
                entity.HasKey(e => new { e.DeliveryTypeID, e.FromCountryID, e.ToCountryID })
                    .HasName("PK_DeliveryTypeAllowedCountryMaps_1");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.DeliveryTypes1)
                    .WithMany(p => p.DeliveryTypeAllowedCountryMaps)
                    .HasForeignKey(d => d.DeliveryTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DeliveryTypeAllowedCountryMaps_DeliveryTypes");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.DeliveryTypeAllowedCountryMaps)
                    .HasForeignKey(d => d.FromCountryID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DeliveryTypeAllowedCountryMaps_Countries1");

                entity.HasOne(d => d.Country1)
                    .WithMany(p => p.DeliveryTypeAllowedCountryMaps1)
                    .HasForeignKey(d => d.ToCountryID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DeliveryTypeAllowedCountryMaps_Countries11");
            });

            modelBuilder.Entity<DeliveryTypeAllowedZoneMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.DeliveryTypes1)
                    .WithMany(p => p.DeliveryTypeAllowedZoneMaps)
                    .HasForeignKey(d => d.DeliveryTypeID)
                    .HasConstraintName("FK_DelivertyTypeAllowedZoneMaps_DeliveryTypes");

                entity.HasOne(d => d.Zone)
                    .WithMany(p => p.DeliveryTypeAllowedZoneMaps)
                    .HasForeignKey(d => d.ZoneID)
                    .HasConstraintName("FK_DelivertyTypeAllowedZoneMaps_Zones");
            });

            //modelBuilder.Entity<DeliveryTypeCategoryMaster>(entity =>
            //{
            //    entity.HasOne(d => d.RefDeliveryType)
            //        .WithMany()
            //        .HasForeignKey(d => d.RefDeliveryTypeID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_DeliveryTypeCategoryMaster_DeliveryTypeMaster");
            //});

            modelBuilder.Entity<DeliveryTypeCultureData>(entity =>
            {
                entity.HasKey(e => new { e.DeliveryTypeID, e.CultureID });

                entity.HasOne(d => d.Culture)
                    .WithMany(p => p.DeliveryTypeCultureDatas)
                    .HasForeignKey(d => d.CultureID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DeliveryTypeCultureDatas_Cultures");

                entity.HasOne(d => d.DeliveryTypes1)
                    .WithMany(p => p.DeliveryTypeCultureDatas)
                    .HasForeignKey(d => d.DeliveryTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DeliveryTypeCultureDatas_DeliveryTypes");
            });

            modelBuilder.Entity<DeliveryTypeCutOffSlot>(entity =>
            {
                entity.Property(e => e.DeliveryTypeCutOffSlotID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.DeliveryTypes1)
                    .WithMany(p => p.DeliveryTypeCutOffSlots)
                    .HasForeignKey(d => d.DeliveryTypeID)
                    .HasConstraintName("FK_DeliveryTypeCutOffSlots_DeliveryTypes");

                entity.HasOne(d => d.DeliveryTypeTimeSlotMap)
                    .WithMany(p => p.DeliveryTypeCutOffSlots)
                    .HasForeignKey(d => d.TimeSlotID)
                    .HasConstraintName("FK_DeliveryTypeCutOffSlots_DeliveryTimeSlots");

            });

            modelBuilder.Entity<DeliveryTypeCutOffSlotCultureData>(entity =>
            {
                entity.HasKey(e => new { e.DeliveryTypeCutOffSlotID, e.CultureID });

                //entity.HasOne(d => d.Culture)
                //    .WithMany(p => p.DeliveryTypeCutOffSlotCultureDatas)
                //    .HasForeignKey(d => d.CultureID)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_DeliveryTypeCutOffSlotCultureDatas_Cultures");

                entity.HasOne(d => d.DeliveryTypeCutOffSlot)
                    .WithMany(p => p.DeliveryTypeCutOffSlotCultureDatas)
                    .HasForeignKey(d => d.DeliveryTypeCutOffSlotID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DeliveryTypeCutOffSlotCultureDatas_DeliveryTypeCutOffSlots");
            });

            //modelBuilder.Entity<DeliveryTypeGeoMap>(entity =>
            //{
            //    entity.HasOne(d => d.Branch)
            //        .WithMany(p => p.DeliveryTypeGeoMaps)
            //        .HasForeignKey(d => d.BranchID)
            //        .HasConstraintName("FK_DeliveryTypeGeoMaps_Branches");

            //    entity.HasOne(d => d.DeliveryType)
            //        .WithMany(p => p.DeliveryTypeGeoMaps)
            //        .HasForeignKey(d => d.DeliveryTypeID)
            //        .HasConstraintName("FK_DeliveryTypeGeoMaps_DeliveryTypeGeoMaps");
            //});

            modelBuilder.Entity<DeliveryTypeTimeSlotMap>(entity =>
            {
                //entity.HasKey(e => e.DeliveryTypeTimeSlotMapIID)
                //    .HasName("PK_DeliveryTypeTimeSlotMaps_1");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.DeliveryTypes1)
                    .WithMany(p => p.DeliveryTypeTimeSlotMaps)
                    .HasForeignKey(d => d.DeliveryTypeID)
                    .HasConstraintName("FK_DeliveryTypeTimeSlotMaps_DeliveryTypes");
            });

            modelBuilder.Entity<DeliveryTypeTimeSlotMapsCulture>(entity =>
            {
                entity.HasKey(e => new { e.CultureID, e.DeliveryTypeTimeSlotMapID });

                entity.HasOne(d => d.Culture)
                    .WithMany(p => p.DeliveryTypeTimeSlotMapsCultures)
                    .HasForeignKey(d => d.CultureID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DeliveryTypeTimeSlotMapsCulture_Cultures");

                entity.HasOne(d => d.DeliveryTypeTimeSlotMap)
                    .WithMany(p => p.DeliveryTypeTimeSlotMapsCultures)
                    .HasForeignKey(d => d.DeliveryTypeTimeSlotMapID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DeliveryTypeTimeSlotMapsCulture_DeliveryTypeTimeSlotMaps");
            });



            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.DepartmentID).ValueGeneratedNever();
            });

            //modelBuilder.Entity<Department1>(entity =>
            //{
            //    entity.Property(e => e.DepartmentID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.Department1)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_Departments_AcademicYear");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.Department1)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_Departments_School");
            //});

            //modelBuilder.Entity<DepartmentCostCenterMap>(entity =>
            //{
            //    entity.Property(e => e.DepartmentCostCenterMapIID).ValueGeneratedOnAdd();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.CostCenter)
            //        .WithMany()
            //        .HasForeignKey(d => d.CostCenterID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_DepartmentCostCenterMap_CostCenter");

            //    entity.HasOne(d => d.Department)
            //        .WithMany()
            //        .HasForeignKey(d => d.DepartmentID)
            //        .HasConstraintName("FK_DepartmentCostCenterMap_Department");
            //});

            //modelBuilder.Entity<DepartmentTag>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Department)
            //        .WithMany(p => p.DepartmentTags)
            //        .HasForeignKey(d => d.DepartmentID)
            //        .HasConstraintName("FK_DepartmentTags_Departments");
            //});

            modelBuilder.Entity<Designation>(entity =>
            {
                entity.Property(e => e.DesignationID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            //modelBuilder.Entity<Despatch>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});



            //modelBuilder.Entity<DocFileType>(entity =>
            //{
            //    entity.Property(e => e.DocFileTypeID).ValueGeneratedNever();
            //});

            modelBuilder.Entity<DocumentFile>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.DocFileType)
                //    .WithMany(p => p.DocumentFiles)
                //    .HasForeignKey(d => d.DocFileTypeID)
                //    .HasConstraintName("FK_DocumentFiles_DocFileTypes");

                //entity.HasOne(d => d.DocumentStatus)
                //    .WithMany(p => p.DocumentFiles)
                //    .HasForeignKey(d => d.DocumentStatusID)
                //    .HasConstraintName("FK_DocumentFiles_DocumentFileStatuses");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.DocumentFiles)
                    .HasForeignKey(d => d.OwnerEmployeeID)
                    .HasConstraintName("FK_DocumentFiles_Employees");
            });

            modelBuilder.Entity<DocumentFileStatus>(entity =>
            {
                entity.Property(e => e.DocumentStatusID).ValueGeneratedNever();

                entity.Property(e => e.StatusName).IsFixedLength();
            });

            modelBuilder.Entity<DocumentReferenceStatusMap>(entity =>
            {
                entity.Property(e => e.DocumentReferenceStatusMapID).ValueGeneratedNever();

                entity.HasOne(d => d.DocumentStatus)
                    .WithMany(p => p.DocumentReferenceStatusMaps)
                    .HasForeignKey(d => d.DocumentStatusID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocumentReferenceStatusMap_DocumentStatuses");

                entity.HasOne(d => d.DocumentReferenceType)
                    .WithMany(p => p.DocumentReferenceStatusMaps)
                    .HasForeignKey(d => d.ReferenceTypeID)
                    .HasConstraintName("FK_DocumentReferenceStatusMap_DocumentReferenceTypes");
            });

            //modelBuilder.Entity<DocumentReferenceTicketStatusMap>(entity =>
            //{
            //    entity.HasOne(d => d.ReferenceType)
            //        .WithMany(p => p.DocumentReferenceTicketStatusMaps)
            //        .HasForeignKey(d => d.ReferenceTypeID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_DocumentReferenceTicketStatusMap_DocumentReferenceTypes");

            //    entity.HasOne(d => d.TicketStatus)
            //        .WithMany(p => p.DocumentReferenceTicketStatusMaps)
            //        .HasForeignKey(d => d.TicketStatusID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_DocumentReferenceTicketStatusMap_TicketStatuses");
            //});

            modelBuilder.Entity<DocumentReferenceType>(entity =>
            {
                entity.HasKey(e => e.ReferenceTypeID)
                    .HasName("PK_InventoryTypes");

                entity.Property(e => e.ReferenceTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<DocumentStatus>(entity =>
            {
                entity.Property(e => e.DocumentStatusID).ValueGeneratedNever();
            });

            modelBuilder.Entity<DocumentType>(entity =>
            {
                entity.Property(e => e.DocumentTypeID).ValueGeneratedNever();

                //entity.Property(e => e.IsExternal).HasDefaultValueSql("((0))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.DocumentReferenceType)
                    .WithMany(p => p.DocumentTypes)
                    .HasForeignKey(d => d.ReferenceTypeID)
                    .HasConstraintName("FK_DocumentTypes_DocumentReferenceTypes");

                //entity.HasOne(d => d.TaxTemplate)
                //    .WithMany(p => p.DocumentTypes)
                //    .HasForeignKey(d => d.TaxTemplateID)
                //    .HasConstraintName("FK_DocumentTypes_TaxTemplates");

                entity.HasOne(d => d.Workflow)
                    .WithMany(p => p.DocumentTypes)
                    .HasForeignKey(d => d.WorkflowID)
                    .HasConstraintName("FK_DocumentTypes_Workflows");
            });

            modelBuilder.Entity<DocumentTypeSetting>(entity =>
            {
                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.DocumentTypeSettings)
                    .HasForeignKey(d => d.DocumentTypeID)
                    .HasConstraintName("FK_DocumentTypeSettings_DocumentType");
            });

            modelBuilder.Entity<DocumentTypeType>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.DocumentTypeType)
                    .HasForeignKey(d => d.DocumentTypeID)
                    .HasConstraintName("FK_DocumentTypeTypeMaps_DocumentTypes");

                entity.HasOne(d => d.DocumentTypeType2)
                    .WithMany(p => p.DocumentTypeType2)
                    .HasForeignKey(d => d.DocumentTypeMapID)
                    .HasConstraintName("FK_DocumentTypeTypeMaps_DocumentTypes1");
            });

            modelBuilder.Entity<DocumentTypeTransactionNumber>(entity =>
            {
                entity.HasKey(e => new { e.DocumentTypeID, e.Year, e.Month });

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.DocumentTypeTransactionNumbers)
                    .HasForeignKey(d => d.DocumentTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocumentTypeTransactionNumbers_DocumentTypes");
            });

            //modelBuilder.Entity<DocumentTypeTypeMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.DocumentType)
            //        .WithMany(p => p.DocumentTypeTypeMapDocumentTypes)
            //        .HasForeignKey(d => d.DocumentTypeID)
            //        .HasConstraintName("FK_DocumentTypeTypeMaps_DocumentTypes");

            //    entity.HasOne(d => d.DocumentTypeMap)
            //        .WithMany(p => p.DocumentTypeTypeMapDocumentTypeMaps)
            //        .HasForeignKey(d => d.DocumentTypeMapID)
            //        .HasConstraintName("FK_DocumentTypeTypeMaps_DocumentTypes1");
            //});

            //modelBuilder.Entity<DriverScheduleLog>(entity =>
            //{
            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.DriverScheduleLogs)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_DriverScheduleLogs_Employee");

            //    entity.HasOne(d => d.Route)
            //        .WithMany(p => p.DriverScheduleLogs)
            //        .HasForeignKey(d => d.RouteID)
            //        .HasConstraintName("FK_DriverScheduleLogs_Route");

            //    entity.HasOne(d => d.RouteStopMap)
            //        .WithMany(p => p.DriverScheduleLogs)
            //        .HasForeignKey(d => d.RouteStopMapID)
            //        .HasConstraintName("FK_DriverScheduleLogs_RouteStopMap");

            //    entity.HasOne(d => d.SheduleLogStatus)
            //        .WithMany(p => p.DriverScheduleLogs)
            //        .HasForeignKey(d => d.SheduleLogStatusID)
            //        .HasConstraintName("FK_DriverScheduleLogs_ScheduleLogStatus");

            //    entity.HasOne(d => d.StopEntryStatus)
            //        .WithMany(p => p.DriverScheduleLogs)
            //        .HasForeignKey(d => d.StopEntryStatusID)
            //        .HasConstraintName("FK_DriverScheduleLogs_StopEntryStatus");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.DriverScheduleLogs)
            //        .HasForeignKey(d => d.StudentID)
            //        .HasConstraintName("FK_DriverScheduleLogs_Student");

            //    entity.HasOne(d => d.Vehicle)
            //        .WithMany(p => p.DriverScheduleLogs)
            //        .HasForeignKey(d => d.VehicleID)
            //        .HasConstraintName("FK_DriverScheduleLogs_Vehicle");
            //});



            //modelBuilder.Entity<Dup_FeeCollection_Receipt_No_20230107>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<DuplicateTuitionFee>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<EducationDetail>(entity =>
            //{
            //    entity.Property(e => e.EducationDetailIID).ValueGeneratedNever();

            //    entity.HasOne(d => d.EducationType)
            //        .WithMany(p => p.EducationDetails)
            //        .HasForeignKey(d => d.EducationTypeID)
            //        .HasConstraintName("FK_EducationDetails_EducationDetails");

            //    entity.HasOne(d => d.Member)
            //        .WithMany(p => p.EducationDetails)
            //        .HasForeignKey(d => d.MemberID)
            //        .HasConstraintName("FK_EducationDetails_Members");
            //});

            //modelBuilder.Entity<EmailCampaign>(entity =>
            //{
            //    entity.HasOne(d => d.Campaign)
            //        .WithMany(p => p.EmailCampaigns)
            //        .HasForeignKey(d => d.CampaignID)
            //        .HasConstraintName("FK_EmailCampaigns_Campaigns");

            //    entity.HasOne(d => d.EmailTemplate)
            //        .WithMany(p => p.EmailCampaigns)
            //        .HasForeignKey(d => d.EmailTemplateID)
            //        .HasConstraintName("FK_EmailCampaigns_EmailTemplates");

            //    entity.HasOne(d => d.Segment)
            //        .WithMany(p => p.EmailCampaigns)
            //        .HasForeignKey(d => d.SegmentID)
            //        .HasConstraintName("FK_EmailCampaigns_Segments");
            //});

            modelBuilder.Entity<EmailNotificationData>(entity =>
            {
                entity.HasKey(e => e.EmailMetaDataIID)
                    .HasName("PK_EmailMetaData");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.EmailNotificationType)
                    .WithMany(p => p.EmailNotificationDatas)
                    .HasForeignKey(d => d.EmailNotificationTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmailNotificationData_EmailNotificationTypes");
            });

            modelBuilder.Entity<EmailNotificationType>(entity =>
            {
                entity.Property(e => e.EmailNotificationTypeID).ValueGeneratedNever();

                entity.Property(e => e.TimeStamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });
            modelBuilder.Entity<TicketEntitilement>(entity =>
            {
                entity.Property(e => e.TimeStamps)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.CountryAirport)
                   .WithMany(p => p.TicketEntitilements)
                   .HasForeignKey(d => d.CountryAirportID)
                   .HasConstraintName("FK_TicketEntitilements_Airport");
            });

            modelBuilder.Entity<EmailTemplate>(entity =>
            {
                entity.Property(e => e.EmailTemplateID).ValueGeneratedNever();
            });

            modelBuilder.Entity<EmailTemplate2>(entity =>
            {
                entity.Property(e => e.EmailTemplateID).ValueGeneratedNever();

            });

            modelBuilder.Entity<EmailTemplateParameterMap>(entity =>
            {
                entity.Property(e => e.EmailTemplateParameterMapID).ValueGeneratedNever();

                entity.HasOne(d => d.EmailTemplate)
                    .WithMany(p => p.EmailTemplateParameterMaps)
                    .HasForeignKey(d => d.EmailTemplateID)
                    .HasConstraintName("FK_EmailTemplateParameterMaps_EmailTemplates");
            });


            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.IsOTEligible).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsOverrideLeaveGroup).HasDefaultValueSql("((0))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcadamicCalendar)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.AcademicCalendarID)
                    .HasConstraintName("FK_Employees_AcadamicCalendar");

                entity.HasOne(d => d.AccomodationType)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.AccomodationTypeID)
                    .HasConstraintName("FK_Employee_AccomodationType");

                entity.HasOne(d => d.BloodGroup)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.BloodGroupID)
                    .HasConstraintName("FK_Employee_BloodGroup");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.BranchID)
                    .HasConstraintName("FK_Employees_Branches");

                //entity.HasOne(d => d.CalendarType)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.CalendarTypeID)
                //    .HasConstraintName("FK_Emp_Calendar_type");

                entity.HasOne(d => d.Cast)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.CastID)
                    .HasConstraintName("FK_Employees_Casts");

                entity.HasOne(d => d.Catogory)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.CategoryID)
                    .HasConstraintName("FK_Employee_Category");

                entity.HasOne(d => d.Community)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.CommunityID)
                    .HasConstraintName("FK_Employees_Communitys");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentID)
                    .HasConstraintName("FK_Employees_Departments");

                entity.HasOne(d => d.Designation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DesignationID)
                    .HasConstraintName("FK_Employees_Designations");

                entity.HasOne(d => d.EmployeeRole)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.EmployeeRoleID)
                    .HasConstraintName("FK_Employees_EmployeeRoles");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.GenderID)
                    .HasConstraintName("FK_Employees_Genders");

                entity.HasOne(d => d.JobType)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.JobTypeID)
                    .HasConstraintName("FK_Employees_JobTypes");

                entity.HasOne(d => d.LeaveGroup)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.LeaveGroupID)
                    .HasConstraintName("FK_emp_LeaveGroup");

                //entity.HasOne(d => d.LeavingType)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.LeavingTypeID)
                //    .HasConstraintName("FK_employees_LeavingType");

                entity.HasOne(d => d.LicenseType)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.LicenseTypeID)
                    .HasConstraintName("FK_Employee_LicenseType");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.LoginID)
                    .HasConstraintName("FK_Employees_Logins");

                //entity.HasOne(d => d.MaritalStatus)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.MaritalStatusID)
                //    .HasConstraintName("FK_Employees_MaritalStatuses");

                entity.HasOne(d => d.Nationality)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.NationalityID)
                    .HasConstraintName("FK_Employees_Nationality");

                entity.HasOne(d => d.PassageType)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.PassageTypeID)
                    .HasConstraintName("FK_Employee_PassageType");

                entity.HasOne(d => d.Relegion)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.RelegionID)
                    .HasConstraintName("FK_Employees_Relegions");

                entity.HasOne(d => d.Employee1)
                    .WithMany(p => p.Employees1)
                    .HasForeignKey(d => d.ReportingEmployeeID)
                    .HasConstraintName("FK_Employees_Employees");

                //entity.HasOne(d => d.ResidencyCompany)
                //    .WithMany(p => p.Employees)
                //    .HasForeignKey(d => d.ResidencyCompanyId)
                //    .HasConstraintName("FK_Employees_Companies");

                entity.HasOne(d => d.SalaryMethod)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.SalaryMethodID)
                    .HasConstraintName("FK_Employees_SalaryMethod");

                entity.HasOne(d => d.EmployeeCountryAirport)
                     .WithMany(p => p.EmployeeEmployeeCountryAirports)
                     .HasForeignKey(d => d.EmployeeCountryAirportID)
                     .HasConstraintName("FK_EmployeeCountry_Airport");

                entity.HasOne(d => d.EmployeeNearestAirport)
                    .WithMany(p => p.EmployeeEmployeeNearestAirports)
                    .HasForeignKey(d => d.EmployeeNearestAirportID)
                    .HasConstraintName("FK_EmployeeNearestAirport");              

                entity.HasOne(d => d.TicketEntitilement)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.TicketEntitilementID)
                    .HasConstraintName("FK_EmployeeTicketEntitlement");
            });

            modelBuilder.Entity<EmployeeAdditionalInfo>(entity =>
            {
                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeAdditionalInfos)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_EmployeeAdditionalInfos_Employees");
            });
            modelBuilder.Entity<EmployeeQualificationMap>(entity =>
            {
                entity.HasKey(e => e.EmployeeQualificationMapIID)
                    .HasName("PK_EmployeeQualificationMap");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeQualificationMaps)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_EmployeeQualificationMap_Qualification");

                entity.HasOne(d => d.Qualification)
                    .WithMany(p => p.EmployeeQualificationMaps)
                    .HasForeignKey(d => d.QualificationID)
                    .HasConstraintName("FK_Employee_Qualification");
            });

            modelBuilder.Entity<EmployeeBankDetail>(entity =>
            {
                entity.HasKey(e => e.EmployeeBankIID)
                    .HasName("PK_EmployeeBankDetail");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.EmployeeBankDetails)
                    .HasForeignKey(d => d.BankID)
                    .HasConstraintName("FK_Employee_BankName");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeBankDetails)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_Employee_BankDetail");
            });

            modelBuilder.Entity<EmployeeCatalogRelation>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.RelationType)
                    .WithMany(p => p.EmployeeCatalogRelations)
                    .HasForeignKey(d => d.RelationTypeID)
                    .HasConstraintName("FK_EmployeeCatalogRelations_RelationTypes");
            });

            //modelBuilder.Entity<EmployeeGrade>(entity =>
            //{
            //    entity.Property(e => e.EmployeeGradeID).ValueGeneratedNever();
            //});



            modelBuilder.Entity<EmployeeLeaveAllocation>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeLeaveAllocations)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_EmpLeaveAllocations_Employee");

                entity.HasOne(d => d.LeaveType)
                    .WithMany(p => p.EmployeeLeaveAllocations)
                    .HasForeignKey(d => d.LeaveTypeID)
                    .HasConstraintName("FK_EmployeeLeaveAllocations_LeaveTypes");
            });

            //modelBuilder.Entity<EmployeeLeavingType>(entity =>
            //{
            //    entity.HasKey(e => e.LeavingTypeID)
            //        .HasName("PK_LeadTypes");
            //});

            //modelBuilder.Entity<EmployeeLevel>(entity =>
            //{
            //    entity.Property(e => e.EmployeeLevelID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<EmployeePromotion>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Account)
            //        .WithMany(p => p.EmployeePromotions)
            //        .HasForeignKey(d => d.AccountID)
            //        .HasConstraintName("FK_EmployeePromotions_Accounts");

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.EmployeePromotions)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_EmployeePromotions_Employees");

            //    entity.HasOne(d => d.NewBranch)
            //        .WithMany(p => p.EmployeePromotionNewBranches)
            //        .HasForeignKey(d => d.NewBranchID)
            //        .HasConstraintName("FK_EmployeePromotions_NewBranches");

            //    entity.HasOne(d => d.NewDesignation)
            //        .WithMany(p => p.EmployeePromotionNewDesignations)
            //        .HasForeignKey(d => d.NewDesignationID)
            //        .HasConstraintName("FK_EmployeePromotions_NewDesignations");

            //    entity.HasOne(d => d.NewLeaveGroup)
            //        .WithMany(p => p.EmployeePromotionNewLeaveGroups)
            //        .HasForeignKey(d => d.NewLeaveGroupID)
            //        .HasConstraintName("FK_EmployeePromotions_LeaveGroups");

            //    entity.HasOne(d => d.NewRole)
            //        .WithMany(p => p.EmployeePromotionNewRoles)
            //        .HasForeignKey(d => d.NewRoleID)
            //        .HasConstraintName("FK_EmployeePromotions_NewRoles");

            //    entity.HasOne(d => d.NewSalaryStructure)
            //        .WithMany(p => p.EmployeePromotionNewSalaryStructures)
            //        .HasForeignKey(d => d.NewSalaryStructureID)
            //        .HasConstraintName("FK_EmployeePromotions_NewSalaryStructure");

            //    entity.HasOne(d => d.OldBranch)
            //        .WithMany(p => p.EmployeePromotionOldBranches)
            //        .HasForeignKey(d => d.OldBranchID)
            //        .HasConstraintName("FK_EmployeePromotions_OldBranches");

            //    entity.HasOne(d => d.OldDesignation)
            //        .WithMany(p => p.EmployeePromotionOldDesignations)
            //        .HasForeignKey(d => d.OldDesignationID)
            //        .HasConstraintName("FK_EmployeePromotions_OldDesignations");

            //    entity.HasOne(d => d.OldLeaveGroup)
            //        .WithMany(p => p.EmployeePromotionOldLeaveGroups)
            //        .HasForeignKey(d => d.OldLeaveGroupID)
            //        .HasConstraintName("FK_EmployeePromotions_OldLeaveGroups");

            //    entity.HasOne(d => d.OldRole)
            //        .WithMany(p => p.EmployeePromotionOldRoles)
            //        .HasForeignKey(d => d.OldRoleID)
            //        .HasConstraintName("FK_EmployeePromotions_OldRoles");

            //    entity.HasOne(d => d.OldSalaryStructure)
            //        .WithMany(p => p.EmployeePromotionOldSalaryStructures)
            //        .HasForeignKey(d => d.OldSalaryStructureID)
            //        .HasConstraintName("FK_EmployeePromotions_OldSalaryStructure");

            //    entity.HasOne(d => d.PaymentMode)
            //        .WithMany(p => p.EmployeePromotions)
            //        .HasForeignKey(d => d.PaymentModeID)
            //        .HasConstraintName("FK_EmployeePromotions_SalaryPaymentModes");

            //    entity.HasOne(d => d.PayrollFrequency)
            //        .WithMany(p => p.EmployeePromotions)
            //        .HasForeignKey(d => d.PayrollFrequencyID)
            //        .HasConstraintName("FK_EmployeePromotions_PayrollFrequencies");

            //    entity.HasOne(d => d.SalaryStructure)
            //        .WithMany(p => p.EmployeePromotionSalaryStructures)
            //        .HasForeignKey(d => d.SalaryStructureID)
            //        .HasConstraintName("FK_EmployeePromotions_SalaryStructure");

            //    entity.HasOne(d => d.TimeSheetSalaryComponent)
            //        .WithMany(p => p.EmployeePromotions)
            //        .HasForeignKey(d => d.TimeSheetSalaryComponentID)
            //        .HasConstraintName("FK_EmployeePromotions_SalaryComponents");
            //});

            //modelBuilder.Entity<EmployeePromotionLeaveAllocation>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.EmployeePromotion)
            //        .WithMany(p => p.EmployeePromotionLeaveAllocations)
            //        .HasForeignKey(d => d.EmployeePromotionID)
            //        .HasConstraintName("FK_EmpLeavePromoLeavAllocations_LeaveTypes");

            //    entity.HasOne(d => d.LeaveType)
            //        .WithMany(p => p.EmployeePromotionLeaveAllocations)
            //        .HasForeignKey(d => d.LeaveTypeID)
            //        .HasConstraintName("FK_EmployeePromoLeaveAllocons_LeaveTypes");
            //});

            //modelBuilder.Entity<EmployeePromotionSalaryComponentMap>(entity =>
            //{
            //    entity.HasOne(d => d.EmployeePromotion)
            //        .WithMany(p => p.EmployeePromotionSalaryComponentMaps)
            //        .HasForeignKey(d => d.EmployeePromotionID)
            //        .HasConstraintName("FK_EmployeePromotionSalaryComp_Promotion");

            //    entity.HasOne(d => d.EmployeeSalaryStructureComponentMap)
            //        .WithMany(p => p.EmployeePromotionSalaryComponentMaps)
            //        .HasForeignKey(d => d.EmployeeSalaryStructureComponentMapID)
            //        .HasConstraintName("FK_EmployeePromotion_SalaryComponentMaps");

            //    entity.HasOne(d => d.SalaryComponent)
            //        .WithMany(p => p.EmployeePromotionSalaryComponentMaps)
            //        .HasForeignKey(d => d.SalaryComponentID)
            //        .HasConstraintName("FK_EmployeePromotionComp_SalaryComponents");
            //});

            //modelBuilder.Entity<EmployeeRelationsDetail>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.CountryofIssue)
            //        .WithMany(p => p.EmployeeRelationsDetails)
            //        .HasForeignKey(d => d.CountryofIssueID)
            //        .HasConstraintName("FK_EmployeeRelations_CountryOfIssue");

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.EmployeeRelationsDetails)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_EmployeeRelations_Employee");

            //    entity.HasOne(d => d.EmployeeRelationType)
            //        .WithMany(p => p.EmployeeRelationsDetails)
            //        .HasForeignKey(d => d.EmployeeRelationTypeID)
            //        .HasConstraintName("FK_EmployeeRelations_Ralation");

            //    entity.HasOne(d => d.Sponsor)
            //        .WithMany(p => p.EmployeeRelationsDetails)
            //        .HasForeignKey(d => d.SponsorID)
            //        .HasConstraintName("FK_EmployeeRelations_Sponsor");
            //});

            modelBuilder.Entity<EmployeeRole>(entity =>
            {
                entity.Property(e => e.EmployeeRoleID).ValueGeneratedNever();
            });

            modelBuilder.Entity<EmployeeRoleMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeRoleMaps)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_EmployeeRoleMaps_Employees");

                entity.HasOne(d => d.EmployeeRole)
                    .WithMany(p => p.EmployeeRoleMaps)
                    .HasForeignKey(d => d.EmployeeRoleID)
                    .HasConstraintName("FK_EmployeeRoleMaps_EmployeeRoles");
            });

            modelBuilder.Entity<EmployeeSalary>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.Company)
                //    .WithMany(p => p.EmployeeSalaries)
                //    .HasForeignKey(d => d.CompanyID)
                //    .HasConstraintName("FK_EmployeeSalaries_Companies");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeSalaries)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_EmployeeSalaries_Employees");

                //entity.HasOne(d => d.SalaryComponent)
                //    .WithMany(p => p.EmployeeSalaries)
                //    .HasForeignKey(d => d.SalaryComponentID)
                //    .HasConstraintName("FK_EmployeeSalaries_SalaryComponents");
            });

            //modelBuilder.Entity<EmployeeSalaryStructure>(entity =>
            //{
            //    entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Account)
            //        .WithMany(p => p.EmployeeSalaryStructures)
            //        .HasForeignKey(d => d.AccountID)
            //        .HasConstraintName("FK_EmployeeSalaryStructures_Accounts");

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.EmployeeSalaryStructures)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_EmployeeSalaryStructures_Employees");

            //    entity.HasOne(d => d.PaymentMode)
            //        .WithMany(p => p.EmployeeSalaryStructures)
            //        .HasForeignKey(d => d.PaymentModeID)
            //        .HasConstraintName("FK_EmployeeSalaryStructures_SalaryPaymentModes");

            //    entity.HasOne(d => d.PayrollFrequency)
            //        .WithMany(p => p.EmployeeSalaryStructures)
            //        .HasForeignKey(d => d.PayrollFrequencyID)
            //        .HasConstraintName("FK_EmployeeSalaryStructures_PayrollFrequencies");

            //    entity.HasOne(d => d.SalaryStructure)
            //        .WithMany(p => p.EmployeeSalaryStructures)
            //        .HasForeignKey(d => d.SalaryStructureID)
            //        .HasConstraintName("FK_EmployeeSalaryStructures_SalaryStructure");

            //    entity.HasOne(d => d.TimeSheetSalaryComponent)
            //        .WithMany(p => p.EmployeeSalaryStructures)
            //        .HasForeignKey(d => d.TimeSheetSalaryComponentID)
            //        .HasConstraintName("FK_EmployeeSalaryStructures_SalaryComponents");
            //});

            //modelBuilder.Entity<EmployeeSalaryStructureComponentMap>(entity =>
            //{
            //    entity.HasOne(d => d.EmployeeSalaryStructure)
            //        .WithMany(p => p.EmployeeSalaryStructureComponentMaps)
            //        .HasForeignKey(d => d.EmployeeSalaryStructureID)
            //        .HasConstraintName("FK_EmployeeSalaryStructureComponentMaps_EmployeeSalaryStructures");

            //    entity.HasOne(d => d.SalaryComponent)
            //        .WithMany(p => p.EmployeeSalaryStructureComponentMaps)
            //        .HasForeignKey(d => d.SalaryComponentID)
            //        .HasConstraintName("FK_EmployeeSalaryStructureComponentMaps_SalaryComponents");
            //});



            //modelBuilder.Entity<EmployeeTimeSheet>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.Property(e => e.TimesheetEntryStatusID).HasDefaultValueSql("((1))");

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.EmployeeTimeSheets)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_EmployeeTimeSheets_Employees");

            //    entity.HasOne(d => d.Task)
            //        .WithMany(p => p.EmployeeTimeSheets)
            //        .HasForeignKey(d => d.TaskID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_EmployeeTimeSheets_EmployeeTimeSheets");

            //    entity.HasOne(d => d.TimesheetEntryStatus)
            //        .WithMany(p => p.EmployeeTimeSheets)
            //        .HasForeignKey(d => d.TimesheetEntryStatusID)
            //        .HasConstraintName("FK_EmployeeTimeSheet_Satus");

            //    entity.HasOne(d => d.TimesheetTimeType)
            //        .WithMany(p => p.EmployeeTimeSheets)
            //        .HasForeignKey(d => d.TimesheetTimeTypeID)
            //        .HasConstraintName("FK_EmployeeTimeSheet_TimesheetTimeType");
            //});

            //modelBuilder.Entity<EmployeeTimeSheetApproval>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.EmployeeTimeSheetApprovals)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_EmployeeTimeSheetAs_Employees");

            //    entity.HasOne(d => d.TimesheetApprovalStatus)
            //        .WithMany(p => p.EmployeeTimeSheetApprovals)
            //        .HasForeignKey(d => d.TimesheetApprovalStatusID)
            //        .HasConstraintName("FK_EmployeeTimesheetApproval_ApprovalStatuses");

            //    entity.HasOne(d => d.TimesheetTimeType)
            //        .WithMany(p => p.EmployeeTimeSheetApprovals)
            //        .HasForeignKey(d => d.TimesheetTimeTypeID)
            //        .HasConstraintName("FK_EmployeeTimeSheets_TimesheetTimeType");
            //});



            //modelBuilder.Entity<EmployeeTimesheetApprovalMap>(entity =>
            //{
            //    entity.Property(e => e.EmployeeTimesheetApprovalMapIID).ValueGeneratedOnAdd();

            //    entity.HasOne(d => d.EmployeeTimeSheet)
            //        .WithMany()
            //        .HasForeignKey(d => d.EmployeeTimeSheetID)
            //        .HasConstraintName("FK_EmployeeTimesheetApprovalMaps_EmployeeTimeSheets");

            //    entity.HasOne(d => d.EmployeeTimesheetApproval)
            //        .WithMany()
            //        .HasForeignKey(d => d.EmployeeTimesheetApprovalID)
            //        .HasConstraintName("FK_EmployeeTimesheetApprovalMaps_EmployeeTimeSheetApprovals");
            //});


            modelBuilder.Entity<EntitlementMap>(entity =>
            {
                entity.HasKey(e => e.EntitlementMapIID)
                    .HasName("PK_EntityTypeEntitlementMaps");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.EntityTypeEntitlement)
                    .WithMany(p => p.EntitlementMaps)
                    .HasForeignKey(d => d.EntitlementID)
                    .HasConstraintName("FK_EntitlementMaps_EntityTypeEntitlements");
            });

            modelBuilder.Entity<EntityChangeTracker>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.SyncEntity)
                    .WithMany(p => p.EntityChangeTrackers)
                    .HasForeignKey(d => d.EntityID)
                    .HasConstraintName("FK_EntityChangeTracker_Entities");

                entity.HasOne(d => d.OperationType)
                    .WithMany(p => p.EntityChangeTrackers)
                    .HasForeignKey(d => d.OperationTypeID)
                    .HasConstraintName("FK_EntityChangeTracker_OperationTypes");

                entity.HasOne(d => d.TrackerStatus)
                    .WithMany(p => p.EntityChangeTrackers)
                    .HasForeignKey(d => d.TrackerStatusID)
                    .HasConstraintName("FK_EntityChangeTracker_TrackerStatuses");
            });

            modelBuilder.Entity<EntityChangeTrackerLog>(entity =>
            {
                entity.Property(e => e.EntityChangeTrackerType).HasComment("0 - Category\r\n1 - Brand\r\n2 - Supplier");
            });

            modelBuilder.Entity<EntityChangeTrackersInProcess>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.EntityChangeTracker)
                    .WithMany(p => p.EntityChangeTrackersInProcesses)
                    .HasForeignKey(d => d.EntityChangeTrackerID)
                    .HasConstraintName("FK_EntityChangeTrackersInProcess_EntityChangeTracker");
            });

            modelBuilder.Entity<EntityChangeTrackersQueue>(entity =>
            {
                entity.HasKey(e => e.EntityChangeTrackerQueueIID)
                    .HasName("PK_EntityChangeTrackerQueues");

                entity.HasOne(d => d.EntityChangeTracker)
                    .WithMany(p => p.EntityChangeTrackersQueues)
                    .HasForeignKey(d => d.EntityChangeTrackeID)
                    .HasConstraintName("FK_EntityChangeTrackerQueues_EntityChangeTracker");
            });

            modelBuilder.Entity<EntityProperty>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.EntityPropertyType)
                    .WithMany(p => p.EntityProperties)
                    .HasForeignKey(d => d.EntityPropertyTypeID)
                    .HasConstraintName("FK_EntityProperties_EntityPropertyTypes");
            });

            modelBuilder.Entity<EntityPropertyMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<EntityPropertyType>(entity =>
            {
                entity.Property(e => e.EntityPropertyTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<EntityScheduler>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.SchedulerEntityType)
                    .WithMany(p => p.EntitySchedulers)
                    .HasForeignKey(d => d.SchedulerEntityTypeID)
                    .HasConstraintName("FK_EntitySchedulers_SchedulerEntityTypes");

                entity.HasOne(d => d.SchedulerType)
                    .WithMany(p => p.EntitySchedulers)
                    .HasForeignKey(d => d.SchedulerTypeID)
                    .HasConstraintName("FK_EntitySchedulers_SchedulerTypes");
            });

            modelBuilder.Entity<EntityType>(entity =>
            {
                entity.Property(e => e.EntityTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<EntityTypeEntitlement>(entity =>
            {
                entity.HasKey(e => e.EntitlementID)
                    .HasName("PK_Entitlements");

                entity.HasOne(d => d.EntityType)
                    .WithMany(p => p.EntityTypeEntitlements)
                    .HasForeignKey(d => d.EntityTypeID)
                    .HasConstraintName("FK_Entitlements_EntityTypes");
            });

            modelBuilder.Entity<EntityTypePaymentMethodMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.EntityPropertyType)
                    .WithMany(p => p.EntityTypePaymentMethodMaps)
                    .HasForeignKey(d => d.EntityPropertyTypeID)
                    .HasConstraintName("FK_EntityTypePaymentMethodMaps_EntityPropertyTypes");

                entity.HasOne(d => d.EntityType)
                    .WithMany(p => p.EntityTypePaymentMethodMaps)
                    .HasForeignKey(d => d.EntityTypeID)
                    .HasConstraintName("FK_EntityTypePaymentMethodMaps_EntityTypes");

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.EntityTypePaymentMethodMaps)
                    .HasForeignKey(d => d.PaymentMethodID)
                    .HasConstraintName("FK_EntityTypePaymentMethodMaps_PaymentMethods");
            });

            modelBuilder.Entity<EntityTypeRelationMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.EntityType)
                    .WithMany(p => p.EntityTypeRelationMaps)
                    .HasForeignKey(d => d.FromEntityTypeID)
                    .HasConstraintName("FK_EntityTypeRelationMaps_EntityTypes");

                entity.HasOne(d => d.EntityType1)
                    .WithMany(p => p.EntityTypeRelationMaps1)
                    .HasForeignKey(d => d.ToEntityTypeID)
                    .HasConstraintName("FK_EntityTypeRelationMaps_EntityTypes1");
            });

            //modelBuilder.Entity<Event>(entity =>
            //{
            //    entity.Property(e => e.EventIID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<EventAudienceMap>(entity =>
            //{
            //    entity.Property(e => e.EventAudienceMapIID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.EventAudienceMaps)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_EventAudienceMaps_Classes");

            //    entity.HasOne(d => d.EventAudienceType)
            //        .WithMany(p => p.EventAudienceMaps)
            //        .HasForeignKey(d => d.EventAudienceTypeID)
            //        .HasConstraintName("FK_EventAudienceMaps_EventAudienceTypes");

            //    entity.HasOne(d => d.Event)
            //        .WithMany(p => p.EventAudienceMaps)
            //        .HasForeignKey(d => d.EventID)
            //        .HasConstraintName("FK_EventAudienceMaps_EventAudienceMaps");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.EventAudienceMaps)
            //        .HasForeignKey(d => d.SectionID)
            //        .HasConstraintName("FK_EventAudienceMaps_Sections");

            //    entity.HasOne(d => d.StudentCategory)
            //        .WithMany(p => p.EventAudienceMaps)
            //        .HasForeignKey(d => d.StudentCategoryID)
            //        .HasConstraintName("FK_EventAudienceMaps_StudentCategories");
            //});

            //modelBuilder.Entity<EventTransportAllocation>(entity =>
            //{
            //    entity.HasKey(e => e.EventTransportAllocationIID)
            //        .HasName("PK__EventTra__006FE7BC1DA92FB0");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Attendar)
            //        .WithMany(p => p.EventTransportAllocationAttendars)
            //        .HasForeignKey(d => d.AttendarID)
            //        .HasConstraintName("FK_EventTransportAllocation_Attendar");

            //    entity.HasOne(d => d.Driver)
            //        .WithMany(p => p.EventTransportAllocationDrivers)
            //        .HasForeignKey(d => d.DriverID)
            //        .HasConstraintName("FK_EventTransportAllocation_Driver");

            //    entity.HasOne(d => d.Event)
            //        .WithMany(p => p.EventTransportAllocations)
            //        .HasForeignKey(d => d.EventID)
            //        .HasConstraintName("FK_EventTransportAllocation_Event");

            //    entity.HasOne(d => d.Route)
            //        .WithMany(p => p.EventTransportAllocations)
            //        .HasForeignKey(d => d.RouteID)
            //        .HasConstraintName("FK_EventTransportAllocation_Route");

            //    entity.HasOne(d => d.Vehicle)
            //        .WithMany(p => p.EventTransportAllocations)
            //        .HasForeignKey(d => d.VehicleID)
            //        .HasConstraintName("FK_EventTransportAllocation_Vehicle");
            //});

            //modelBuilder.Entity<EventTransportAllocationMap>(entity =>
            //{
            //    entity.HasKey(e => e.EventTransportAllocationMapIID)
            //        .HasName("PK__EventTra__11DA3B17439210AF");

            //    entity.HasOne(d => d.EventTransportAllocation)
            //        .WithMany(p => p.EventTransportAllocationMaps)
            //        .HasForeignKey(d => d.EventTransportAllocationID)
            //        .HasConstraintName("FK_EventTransportAllocation_Master");

            //    entity.HasOne(d => d.StaffRouteStopMap)
            //        .WithMany(p => p.EventTransportAllocationMaps)
            //        .HasForeignKey(d => d.StaffRouteStopMapID)
            //        .HasConstraintName("FK_EventTransportAllocation_Staff");

            //    entity.HasOne(d => d.StudentRouteStopMap)
            //        .WithMany(p => p.EventTransportAllocationMaps)
            //        .HasForeignKey(d => d.StudentRouteStopMapID)
            //        .HasConstraintName("FK_EventTransportAllocation_Student");
            //});



            //modelBuilder.Entity<Exam>(entity =>
            //{
            //    entity.Property(e => e.IsAnnualExam).HasDefaultValueSql("((0))");

            //    entity.Property(e => e.IsProgressCard).HasDefaultValueSql("((0))");

            //    entity.Property(e => e.ProgressCardHeader).HasDefaultValueSql("('')");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.Exams)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_Exams_AcademicYear");

            //    entity.HasOne(d => d.ExamGroup)
            //        .WithMany(p => p.Exams)
            //        .HasForeignKey(d => d.ExamGroupID)
            //        .HasConstraintName("FK_Exams_ExamGroup");

            //    entity.HasOne(d => d.ExamType)
            //        .WithMany(p => p.Exams)
            //        .HasForeignKey(d => d.ExamTypeID)
            //        .HasConstraintName("FK_Exams_ExamTypes");

            //    entity.HasOne(d => d.MarkGrade)
            //        .WithMany(p => p.Exams)
            //        .HasForeignKey(d => d.MarkGradeID)
            //        .HasConstraintName("FK_Exams_MarkGrades");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.Exams)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_Exams_School");
            //});

            //modelBuilder.Entity<ExamClassMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.ExamClassMaps)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_ExamClassMaps_Classes");

            //    entity.HasOne(d => d.Exam)
            //        .WithMany(p => p.ExamClassMaps)
            //        .HasForeignKey(d => d.ExamID)
            //        .HasConstraintName("FK_ExamClassMaps_Exams");

            //    entity.HasOne(d => d.ExamSchedule)
            //        .WithMany(p => p.ExamClassMaps)
            //        .HasForeignKey(d => d.ExamScheduleID)
            //        .HasConstraintName("FK_ExamClassMaps_ExamSchedules");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.ExamClassMaps)
            //        .HasForeignKey(d => d.SectionID)
            //        .HasConstraintName("FK_ExamClassMaps_Sections");
            //});

            //modelBuilder.Entity<ExamClassMaps_20220304>(entity =>
            //{
            //    entity.Property(e => e.ExamClassMapIID).ValueGeneratedOnAdd();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<ExamGroup>(entity =>
            //{
            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.ExamGroups)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_examgroups_AcademicYear");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.ExamGroups)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_examgroups_School");
            //});

            //modelBuilder.Entity<ExamQuestionGroupMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.OnlineExam)
            //        .WithMany(p => p.ExamQuestionGroupMaps)
            //        .HasForeignKey(d => d.OnlineExamID)
            //        .HasConstraintName("FK_ExamQuestionGroupMap_Exam");

            //    entity.HasOne(d => d.QuestionGroup)
            //        .WithMany(p => p.ExamQuestionGroupMaps)
            //        .HasForeignKey(d => d.QuestionGroupID)
            //        .HasConstraintName("FK_ExamQuestionGroupMap_QuestionGroup");
            //});

            //modelBuilder.Entity<ExamSchedule>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Exam)
            //        .WithMany(p => p.ExamSchedules)
            //        .HasForeignKey(d => d.ExamID)
            //        .HasConstraintName("FK_ExamSchedules_Exams");
            //});

            //modelBuilder.Entity<ExamSkillMap>(entity =>
            //{
            //    entity.HasOne(d => d.ClassSubjectSkillGroupMap)
            //        .WithMany(p => p.ExamSkillMaps)
            //        .HasForeignKey(d => d.ClassSubjectSkillGroupMapID)
            //        .HasConstraintName("FK_ExamSkillMaps_ClassSubjectSkillGroupMaps");

            //    entity.HasOne(d => d.Exam)
            //        .WithMany(p => p.ExamSkillMaps)
            //        .HasForeignKey(d => d.ExamID)
            //        .HasConstraintName("FK_ExamSkillMaps_Exams");

            //    entity.HasOne(d => d.SkillGroupMaster)
            //        .WithMany(p => p.ExamSkillMaps)
            //        .HasForeignKey(d => d.SkillGroupMasterID)
            //        .HasConstraintName("FK_ExamSkillMaps_SkillGroupMasters");
            //});

            //modelBuilder.Entity<ExamSubjectMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Exam)
            //        .WithMany(p => p.ExamSubjectMaps)
            //        .HasForeignKey(d => d.ExamID)
            //        .HasConstraintName("FK_ExamSubjectMaps_Exams");

            //    entity.HasOne(d => d.MarkGrade)
            //        .WithMany(p => p.ExamSubjectMaps)
            //        .HasForeignKey(d => d.MarkGradeID)
            //        .HasConstraintName("FK_ExamSubjectMaps_ExamSubjectMaps");

            //    entity.HasOne(d => d.Subject)
            //        .WithMany(p => p.ExamSubjectMaps)
            //        .HasForeignKey(d => d.SubjectID)
            //        .HasConstraintName("FK_ExamSubjectMaps_Subjects");
            //});

            //modelBuilder.Entity<ExamSubjectMaps_20220304>(entity =>
            //{
            //    entity.Property(e => e.ExamSubjectMapIID).ValueGeneratedOnAdd();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<ExamType>(entity =>
            //{
            //    entity.Property(e => e.ExamTypeID).ValueGeneratedOnAdd();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.ExamTypes)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_ExamTypes_AcademicYear");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.ExamTypes)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_ExamTypes_School");
            //});






            //modelBuilder.Entity<ExtraTimeType>(entity =>
            //{
            //    entity.Property(e => e.ExtraTimeTypeID).ValueGeneratedNever();
            //});




            //modelBuilder.Entity<Family>(entity =>
            //{
            //    entity.Property(e => e.HouseOwnerShipType).IsFixedLength();

            //    entity.Property(e => e.HouseType).IsFixedLength();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<FeeCollection>(entity =>
            //{
            //    entity.Property(e => e.IsCancelled).HasDefaultValueSql("((0))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcadamicYear)
            //        .WithMany(p => p.FeeCollections)
            //        .HasForeignKey(d => d.AcadamicYearID)
            //        .HasConstraintName("FK_FeeCollections_AcademicYears");

            //    entity.HasOne(d => d.AccountTransactionHead)
            //        .WithMany(p => p.FeeCollections)
            //        .HasForeignKey(d => d.AccountTransactionHeadID)
            //        .HasConstraintName("FK_FeeCollections_AccountTransactionHeads");

            //    entity.HasOne(d => d.Cashier)
            //        .WithMany(p => p.FeeCollections)
            //        .HasForeignKey(d => d.CashierID)
            //        .HasConstraintName("FK_FeeCollections_Employees");

            //    entity.HasOne(d => d.ClassFeeMaster)
            //        .WithMany(p => p.FeeCollections)
            //        .HasForeignKey(d => d.ClassFeeMasterId)
            //        .HasConstraintName("FK_classfeemaster");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.FeeCollections)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_FeeCollections_Classes");

            //    entity.HasOne(d => d.FeeCollectionStatus)
            //        .WithMany(p => p.FeeCollections)
            //        .HasForeignKey(d => d.FeeCollectionStatusID)
            //        .HasConstraintName("FK_FeeCollections_FeeCollectionStatus");

            //    entity.HasOne(d => d.FeeMaster)
            //        .WithMany(p => p.FeeCollections)
            //        .HasForeignKey(d => d.FeeMasterID)
            //        .HasConstraintName("FK_FeeMasters_PeriodId");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.FeeCollections)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_FeeCollections_School");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.FeeCollections)
            //        .HasForeignKey(d => d.SectionID)
            //        .HasConstraintName("FK_FeeCollections_Sections");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.FeeCollections)
            //        .HasForeignKey(d => d.StudentID)
            //        .HasConstraintName("FK_FeeCollections_Students");
            //});

            //modelBuilder.Entity<FeeCollectionFeeTypeMap>(entity =>
            //{
            //    entity.HasKey(e => e.FeeCollectionFeeTypeMapsIID)
            //        .HasName("PK_FeeClassMapIID");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AccountTransactionHead)
            //        .WithMany(p => p.FeeCollectionFeeTypeMaps)
            //        .HasForeignKey(d => d.AccountTransactionHeadID)
            //        .HasConstraintName("FK_FeeCollectionFeeTypeMaps_AccountTransactionHeads");

            //    entity.HasOne(d => d.FeeCollection)
            //        .WithMany(p => p.FeeCollectionFeeTypeMaps)
            //        .HasForeignKey(d => d.FeeCollectionID)
            //        .HasConstraintName("FK_FeecollectionClassMaps_Collection");

            //    entity.HasOne(d => d.FeeDueFeeTypeMaps)
            //        .WithMany(p => p.FeeCollectionFeeTypeMaps)
            //        .HasForeignKey(d => d.FeeDueFeeTypeMapsID)
            //        .HasConstraintName("FK_FeeCollectionFeeTypeMaps_FeeDueFeeTypeMaps");

            //    entity.HasOne(d => d.FeeMaster)
            //        .WithMany(p => p.FeeCollectionFeeTypeMaps)
            //        .HasForeignKey(d => d.FeeMasterID)
            //        .HasConstraintName("FK_FeeCollectionFeeTypeMaps_FeeMasters");

            //    entity.HasOne(d => d.FeePeriod)
            //        .WithMany(p => p.FeeCollectionFeeTypeMaps)
            //        .HasForeignKey(d => d.FeePeriodID)
            //        .HasConstraintName("FK_FeecollectionClassMaps_FeePeriods");

            //    entity.HasOne(d => d.FineMaster)
            //        .WithMany(p => p.FeeCollectionFeeTypeMaps)
            //        .HasForeignKey(d => d.FineMasterID)
            //        .HasConstraintName("FK_FeeCollectionFeeTypeMaps_FineMasters");

            //    entity.HasOne(d => d.FineMasterStudentMap)
            //        .WithMany(p => p.FeeCollectionFeeTypeMaps)
            //        .HasForeignKey(d => d.FineMasterStudentMapID)
            //        .HasConstraintName("FK_FeeCollectionFeeTypeMaps_FineMasterStudentMaps");
            //});

            //modelBuilder.Entity<FeeCollectionMonthlySplit>(entity =>
            //{
            //    entity.HasKey(e => e.FeeCollectionMonthlySplitIID)
            //        .HasName("PK_FeeAssignMonthlySplitId");

            //    entity.HasOne(d => d.AccountTransactionHead)
            //        .WithMany(p => p.FeeCollectionMonthlySplits)
            //        .HasForeignKey(d => d.AccountTransactionHeadID)
            //        .HasConstraintName("FK_FeeCollectionMonthlySplit_AccountTransactionHead");

            //    entity.HasOne(d => d.FeeCollectionFeeTypeMap)
            //        .WithMany(p => p.FeeCollectionMonthlySplits)
            //        .HasForeignKey(d => d.FeeCollectionFeeTypeMapId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_FeeCollectionMonthlySplit_FeeCollectionClassMaps");

            //    entity.HasOne(d => d.FeeDueMonthlySplit)
            //        .WithMany(p => p.FeeCollectionMonthlySplits)
            //        .HasForeignKey(d => d.FeeDueMonthlySplitID)
            //        .HasConstraintName("FK_FeeCollectionMonthlySplit_FeeDueMonthlySplit");
            //});

            //modelBuilder.Entity<FeeCollectionPaymentModeMap>(entity =>
            //{
            //    entity.HasKey(e => e.FeeCollectionPaymentModeMapIID)
            //        .HasName("PK_FeeCollectionPaymentModeMapIID");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AccountTransactionDetail)
            //        .WithMany(p => p.FeeCollectionPaymentModeMaps)
            //        .HasForeignKey(d => d.AccountTransactionDetailID)
            //        .HasConstraintName("FK_FeeCollectionPaymentModeMaps_AccountTransactionDetails");

            //    entity.HasOne(d => d.Bank)
            //        .WithMany(p => p.FeeCollectionPaymentModeMaps)
            //        .HasForeignKey(d => d.BankID)
            //        .HasConstraintName("FK_FeeCollections_Banks");

            //    entity.HasOne(d => d.FeeCollection)
            //        .WithMany(p => p.FeeCollectionPaymentModeMaps)
            //        .HasForeignKey(d => d.FeeCollectionID)
            //        .HasConstraintName("FK_FeeCollectionPaymentModeMap_FeeCollections");

            //    entity.HasOne(d => d.PaymentMode)
            //        .WithMany(p => p.FeeCollectionPaymentModeMaps)
            //        .HasForeignKey(d => d.PaymentModeID)
            //        .HasConstraintName("FK_FeeCollectionPaymentModeMaps_PaymentModes");
            //});

            //modelBuilder.Entity<FeeCollectionStatus>(entity =>
            //{
            //    entity.Property(e => e.FeeCollectionStatusID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<FeeConcessionApprovalType>(entity =>
            //{
            //    entity.HasKey(e => e.ConcessionApprovalTypeID)
            //        .HasName("PK__FeeConce__9A15E8131C1E4131");

            //    entity.Property(e => e.ConcessionApprovalTypeID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<FeeConcessionType>(entity =>
            //{
            //    entity.HasKey(e => e.ConcessionTypeID)
            //        .HasName("PK__Concessi__D97C9C76A6E139BB");

            //    entity.Property(e => e.ConcessionTypeID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<FeeCycle>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<FeeDiscount>(entity =>
            //{
            //    entity.Property(e => e.FeeDiscountID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<FeeDueCancellation>(entity =>
            //{
            //    entity.HasKey(e => e.FeeDueCancellationIID)
            //        .HasName("PK_FeeDuecancelations");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.FeeDueCancellations)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_FeeDuecancelations_AcademicYear");

            //    entity.HasOne(d => d.FeeDueFeeTypeMaps)
            //        .WithMany(p => p.FeeDueCancellations)
            //        .HasForeignKey(d => d.FeeDueFeeTypeMapsID)
            //        .HasConstraintName("FK_FeeDuecancelation_DueFeeType");

            //    entity.HasOne(d => d.StudentFeeDue)
            //        .WithMany(p => p.FeeDueCancellations)
            //        .HasForeignKey(d => d.StudentFeeDueID)
            //        .HasConstraintName("FK_FeeDuecancelation_Due");
            //});

            //modelBuilder.Entity<FeeDueFeeTypeMap>(entity =>
            //{
            //    entity.HasKey(e => e.FeeDueFeeTypeMapsIID)
            //        .HasName("PK_FeeDueFeeTypeMapsIID");

            //    entity.Property(e => e.CollectedAmount).HasDefaultValueSql("((0))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AccountTransactionHead)
            //        .WithMany(p => p.FeeDueFeeTypeMaps)
            //        .HasForeignKey(d => d.AccountTransactionHeadID)
            //        .HasConstraintName("FK_FeeDueFeeTypeMaps_AccountTransactionHeads");

            //    entity.HasOne(d => d.ClassFeeMaster)
            //        .WithMany(p => p.FeeDueFeeTypeMaps)
            //        .HasForeignKey(d => d.ClassFeeMasterID)
            //        .HasConstraintName("FK_FeeDueFeeTypeMaps_ClassFeeMasters");

            //    entity.HasOne(d => d.FeeMasterClassMap)
            //        .WithMany(p => p.FeeDueFeeTypeMaps)
            //        .HasForeignKey(d => d.FeeMasterClassMapID)
            //        .HasConstraintName("FK_feemasterClassMap_ClassFeeMasters");

            //    entity.HasOne(d => d.FeeMaster)
            //        .WithMany(p => p.FeeDueFeeTypeMaps)
            //        .HasForeignKey(d => d.FeeMasterID)
            //        .HasConstraintName("FK_Feemaster_FeeDueFeeTypeMaps");

            //    entity.HasOne(d => d.FeePeriod)
            //        .WithMany(p => p.FeeDueFeeTypeMaps)
            //        .HasForeignKey(d => d.FeePeriodID)
            //        .HasConstraintName("FK_FeeDueFeeTypeMaps_FeePeriods");

            //    entity.HasOne(d => d.FeeStructureFeeMap)
            //        .WithMany(p => p.FeeDueFeeTypeMaps)
            //        .HasForeignKey(d => d.FeeStructureFeeMapID)
            //        .HasConstraintName("FK_FeeDueFeeTypeMaps_FeeStructureFeeMaps");

            //    entity.HasOne(d => d.FineMaster)
            //        .WithMany(p => p.FeeDueFeeTypeMaps)
            //        .HasForeignKey(d => d.FineMasterID)
            //        .HasConstraintName("FK_FeeDueFeeTypeMaps_FineMasters");

            //    entity.HasOne(d => d.FineMasterStudentMap)
            //        .WithMany(p => p.FeeDueFeeTypeMaps)
            //        .HasForeignKey(d => d.FineMasterStudentMapID)
            //        .HasConstraintName("FK_FeeDueFeeTypeMaps_FineMasterStudentMaps");

            //    entity.HasOne(d => d.StudentFeeDue)
            //        .WithMany(p => p.FeeDueFeeTypeMaps)
            //        .HasForeignKey(d => d.StudentFeeDueID)
            //        .HasConstraintName("FK_StudentFeeDue_FeeDueFeeTypeMaps1");
            //});

            modelBuilder.Entity<FeeDueInventoryMap>(entity =>
            {
                entity.HasKey(e => e.FeeDueInventoryMapIID)
                    .HasName("PK_FeeDueInventoryMapIID");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.FeeDueFeeTypeMaps)
                //    .WithMany(p => p.FeeDueInventoryMaps)
                //    .HasForeignKey(d => d.FeeDueFeeTypeMapsID)
                //    .HasConstraintName("FK_FeeDueInventory_FeeDueFeeTypeMaps");

                //entity.HasOne(d => d.FeeMaster)
                //    .WithMany(p => p.FeeDueInventoryMaps)
                //    .HasForeignKey(d => d.FeeMasterID)
                //    .HasConstraintName("FK_Feemaster_FeeDueInventoryMaps");

                entity.HasOne(d => d.ProductCategoryMap)
                    .WithMany(p => p.FeeDueInventoryMaps)
                    .HasForeignKey(d => d.ProductCategoryMapID)
                    .HasConstraintName("FK_ProductCategory_FeeDueInventory");

                //entity.HasOne(d => d.StudentFeeDue)
                //    .WithMany(p => p.FeeDueInventoryMaps)
                //    .HasForeignKey(d => d.StudentFeeDueID)
                //    .HasConstraintName("FK_StudentFeeDue_FeeDueInventory");

                entity.HasOne(d => d.TransactionHead)
                    .WithMany(p => p.FeeDueInventoryMaps)
                    .HasForeignKey(d => d.TransactionHeadID)
                    .HasConstraintName("FK_FeeDueInventoryMap_TransactionHeads");
            });

            //modelBuilder.Entity<FeeDueMonthlySplit>(entity =>
            //{
            //    entity.HasKey(e => e.FeeDueMonthlySplitIID)
            //        .HasName("PK_FeeDueMonthlySplitIID");

            //    entity.HasOne(d => d.AccountTransactionHead)
            //        .WithMany(p => p.FeeDueMonthlySplits)
            //        .HasForeignKey(d => d.AccountTransactionHeadID)
            //        .HasConstraintName("FK_FeeDueMonthlySplit_AccountTransactionHeads");

            //    entity.HasOne(d => d.FeeDueFeeTypeMaps)
            //        .WithMany(p => p.FeeDueMonthlySplits)
            //        .HasForeignKey(d => d.FeeDueFeeTypeMapsID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_FeeDueMonthlySplit_FeeDueFeeTypeMaps");

            //    entity.HasOne(d => d.FeePeriod)
            //        .WithMany(p => p.FeeDueMonthlySplits)
            //        .HasForeignKey(d => d.FeePeriodID)
            //        .HasConstraintName("FK_FeeDueMonthlySplit_FeePeriods");

            //    entity.HasOne(d => d.FeeStructureMontlySplitMap)
            //        .WithMany(p => p.FeeDueMonthlySplits)
            //        .HasForeignKey(d => d.FeeStructureMontlySplitMapID)
            //        .HasConstraintName("FK_FeeDueMonthlySplit_FeeStructureMontlySplitMaps");
            //});

            //modelBuilder.Entity<FeeFineType>(entity =>
            //{
            //    entity.Property(e => e.FeeFineTypeID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.FeeType)
            //        .WithMany(p => p.FeeFineTypes)
            //        .HasForeignKey(d => d.FeeTypeID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_FeeFineTypes_FeeTypes");
            //});

            //modelBuilder.Entity<FeeGroup>(entity =>
            //{
            //    entity.Property(e => e.FeeGroupID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<FeeMaster>(entity =>
            //{
            //    entity.Property(e => e.FeeMasterID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.FeeMasters)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_FeeMasters_AcademicYear");

            //    entity.HasOne(d => d.AdvanceAccount)
            //        .WithMany(p => p.FeeMasterAdvanceAccounts)
            //        .HasForeignKey(d => d.AdvanceAccountID)
            //        .HasConstraintName("FK_FeeMasters_Accounts3");

            //    entity.HasOne(d => d.AdvanceTaxAccount)
            //        .WithMany(p => p.FeeMasterAdvanceTaxAccounts)
            //        .HasForeignKey(d => d.AdvanceTaxAccountID)
            //        .HasConstraintName("FK_FeeMasters_Accounts5");

            //    entity.HasOne(d => d.FeeCycle)
            //        .WithMany(p => p.FeeMasters)
            //        .HasForeignKey(d => d.FeeCycleID)
            //        .HasConstraintName("FK__FeeMaster__FeeCy__1A9FBED1");

            //    entity.HasOne(d => d.FeeType)
            //        .WithMany(p => p.FeeMasters)
            //        .HasForeignKey(d => d.FeeTypeID)
            //        .HasConstraintName("FK_FeeMasters_FeeTypes");

            //    entity.HasOne(d => d.LedgerAccount)
            //        .WithMany(p => p.FeeMasterLedgerAccounts)
            //        .HasForeignKey(d => d.LedgerAccountID)
            //        .HasConstraintName("FK_FeeMasters_Accounts");

            //    entity.HasOne(d => d.OSTaxAccount)
            //        .WithMany(p => p.FeeMasterOSTaxAccounts)
            //        .HasForeignKey(d => d.OSTaxAccountID)
            //        .HasConstraintName("FK_FeeMasters_Accounts4");

            //    entity.HasOne(d => d.OutstandingAccount)
            //        .WithMany(p => p.FeeMasterOutstandingAccounts)
            //        .HasForeignKey(d => d.OutstandingAccountID)
            //        .HasConstraintName("FK_FeeMasters_Accounts2");

            //    entity.HasOne(d => d.ProvisionforAdvanceAccount)
            //        .WithMany(p => p.FeeMasterProvisionforAdvanceAccounts)
            //        .HasForeignKey(d => d.ProvisionforAdvanceAccountID)
            //        .HasConstraintName("FK_FeeMasters_ProvisforAdvanAcc");

            //    entity.HasOne(d => d.ProvisionforOutstandingAccount)
            //        .WithMany(p => p.FeeMasterProvisionforOutstandingAccounts)
            //        .HasForeignKey(d => d.ProvisionforOutstandingAccountID)
            //        .HasConstraintName("FK_FeeMasters_ProvisforOutSAcc");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.FeeMasters)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_FeeMasters_School");

            //    entity.HasOne(d => d.TaxLedgerAccount)
            //        .WithMany(p => p.FeeMasterTaxLedgerAccounts)
            //        .HasForeignKey(d => d.TaxLedgerAccountID)
            //        .HasConstraintName("FK_FeeMasters_Accounts1");
            //});

            //modelBuilder.Entity<FeeMasterClassMap>(entity =>
            //{
            //    entity.HasKey(e => e.FeeMasterClassMapIID)
            //        .HasName("PK_FeeMasterClassMapIID");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.ClassFeeMaster)
            //        .WithMany(p => p.FeeMasterClassMaps)
            //        .HasForeignKey(d => d.ClassFeeMasterID)
            //        .HasConstraintName("FK_FeeMasterClassMaps_ClassFeeMasters");

            //    entity.HasOne(d => d.FeeMaster)
            //        .WithMany(p => p.FeeMasterClassMaps)
            //        .HasForeignKey(d => d.FeeMasterID)
            //        .HasConstraintName("FK_FeeMasterClassMaps_FeeMasters");

            //    entity.HasOne(d => d.FeePeriod)
            //        .WithMany(p => p.FeeMasterClassMaps)
            //        .HasForeignKey(d => d.FeePeriodID)
            //        .HasConstraintName("FK_FeeMasterClassMaps_FeePeriods");
            //});

            //modelBuilder.Entity<FeeMasterClassMontlySplitMap>(entity =>
            //{
            //    entity.HasOne(d => d.FeeMasterClassMap)
            //        .WithMany(p => p.FeeMasterClassMontlySplitMaps)
            //        .HasForeignKey(d => d.FeeMasterClassMapID)
            //        .HasConstraintName("FK_FeeMasterClassMontlySplitMaps_FeeMasterClassMaps");

            //    entity.HasOne(d => d.FeePeriod)
            //        .WithMany(p => p.FeeMasterClassMontlySplitMaps)
            //        .HasForeignKey(d => d.FeePeriodID)
            //        .HasConstraintName("FK_FeeMasterClassMontlySplitMaps_FeePeriods");
            //});

            //modelBuilder.Entity<FeePaymentMode>(entity =>
            //{
            //    entity.Property(e => e.PaymentModeID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<FeePeriod>(entity =>
            //{
            //    entity.Property(e => e.FeePeriodID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.FeePeriods)
            //        .HasForeignKey(d => d.AcademicYearId)
            //        .HasConstraintName("FK_FeePeriods_AcademicYears");

            //    entity.HasOne(d => d.FeePeriodType)
            //        .WithMany(p => p.FeePeriods)
            //        .HasForeignKey(d => d.FeePeriodTypeID)
            //        .HasConstraintName("FK_FeeTypes_FeePeriodType");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.FeePeriods)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_FeePeriods_School");
            //});

            //modelBuilder.Entity<FeePeriodType>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<FeeStructure>(entity =>
            //{
            //    entity.HasKey(e => e.FeeStructureIID)
            //        .HasName("PK_ClassPackageMasters");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcadamicYear)
            //        .WithMany(p => p.FeeStructures)
            //        .HasForeignKey(d => d.AcadamicYearID)
            //        .HasConstraintName("FK_FeeStructures_AcademicYears");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.FeeStructures)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_FeeStructures_School");
            //});

            //modelBuilder.Entity<FeeStructureFeeMap>(entity =>
            //{
            //    entity.HasKey(e => e.FeeStructureFeeMapIID)
            //        .HasName("PK_[FeeStructureFeeMaps");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.FeeMaster)
            //        .WithMany(p => p.FeeStructureFeeMaps)
            //        .HasForeignKey(d => d.FeeMasterID)
            //        .HasConstraintName("FK_FeeStructureFeeMaps_FeeMasters");

            //    entity.HasOne(d => d.FeePeriod)
            //        .WithMany(p => p.FeeStructureFeeMaps)
            //        .HasForeignKey(d => d.FeePeriodID)
            //        .HasConstraintName("FK_FeeStructureFeeMaps_FeePeriods");

            //    entity.HasOne(d => d.FeeStructure)
            //        .WithMany(p => p.FeeStructureFeeMaps)
            //        .HasForeignKey(d => d.FeeStructureID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_FeeStructureFeeMaps_FeeStructures");
            //});

            //modelBuilder.Entity<FeeStructureMontlySplitMap>(entity =>
            //{
            //    entity.HasKey(e => e.FeeStructureMontlySplitMapIID)
            //        .HasName("PK_FeeStructureMontlySplitMapMaps");

            //    entity.HasOne(d => d.FeeStructureFeeMap)
            //        .WithMany(p => p.FeeStructureMontlySplitMaps)
            //        .HasForeignKey(d => d.FeeStructureFeeMapID)
            //        .HasConstraintName("FK_FeeStructureMontlySplitMaps_FeeStructureMontlySplitMaps");
            //});

            //modelBuilder.Entity<FeeType>(entity =>
            //{
            //    entity.Property(e => e.FeeTypeID).ValueGeneratedNever();

            //    entity.Property(e => e.IsRefundable).HasDefaultValueSql("((0))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.FeeTypes)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_FeeTypes_AcademicYear");

            //    entity.HasOne(d => d.FeeCycle)
            //        .WithMany(p => p.FeeTypes)
            //        .HasForeignKey(d => d.FeeCycleId)
            //        .HasConstraintName("FK_FeeTypes_FeeCycles");

            //    entity.HasOne(d => d.FeeGroup)
            //        .WithMany(p => p.FeeTypes)
            //        .HasForeignKey(d => d.FeeGroupId)
            //        .HasConstraintName("FK_FeeTypes_FeeGroups");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.FeeTypes)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_FeeTypes_School");
            //});

            modelBuilder.Entity<FilterColumn>(entity =>
            {
                entity.Property(e => e.FilterColumnID).ValueGeneratedNever();

                entity.Property(e => e.IsQuickFilter).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.DataType)
                    .WithMany(p => p.FilterColumns)
                    .HasForeignKey(d => d.DataTypeID)
                    .HasConstraintName("FK_FilterColumns_DataTypes");

                entity.HasOne(d => d.UIControlType)
                    .WithMany(p => p.FilterColumns)
                    .HasForeignKey(d => d.UIControlTypeID)
                    .HasConstraintName("FK_FilterColumns_UIControlTypes");

                entity.HasOne(d => d.View)
                    .WithMany(p => p.FilterColumns)
                    .HasForeignKey(d => d.ViewID)
                    .HasConstraintName("FK_FilterColumns_Views");
            });

            modelBuilder.Entity<FilterColumnConditionMap>(entity =>
            {
                entity.Property(e => e.FilterColumnConditionMapID).ValueGeneratedNever();

                entity.HasOne(d => d.Condition)
                    .WithMany(p => p.FilterColumnConditionMaps)
                    .HasForeignKey(d => d.ConidtionID)
                    .HasConstraintName("FK_FilterColumnConditionMaps_Conditions");

                entity.HasOne(d => d.DataType)
                    .WithMany(p => p.FilterColumnConditionMaps)
                    .HasForeignKey(d => d.DataTypeID)
                    .HasConstraintName("FK_FilterColumnConditionMaps_DataTypes");

                entity.HasOne(d => d.FilterColumn)
                    .WithMany(p => p.FilterColumnConditionMaps)
                    .HasForeignKey(d => d.FilterColumnID)
                    .HasConstraintName("FK_FilterColumnConditionMaps_FilterColumns");
            });

            modelBuilder.Entity<FilterColumnCultureData>(entity =>
            {
                entity.HasKey(e => new { e.FilterColumnID, e.CultureID });

                entity.HasOne(d => d.Culture)
                    .WithMany(p => p.FilterColumnCultureDatas)
                    .HasForeignKey(d => d.CultureID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FilterColumnCultureDatas_Cultures");

                entity.HasOne(d => d.FilterColumn)
                    .WithMany(p => p.FilterColumnCultureDatas)
                    .HasForeignKey(d => d.FilterColumnID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FilterColumnCultureDatas_FilterColumns");
            });

            modelBuilder.Entity<FilterColumnUserValue>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Condition)
                    .WithMany(p => p.FilterColumnUserValues)
                    .HasForeignKey(d => d.ConditionID)
                    .HasConstraintName("FK_FilterColumnUserValues_Conditions");

                entity.HasOne(d => d.FilterColumn)
                    .WithMany(p => p.FilterColumnUserValues)
                    .HasForeignKey(d => d.FilterColumnID)
                    .HasConstraintName("FK_FilterColumnUserValues_FilterColumns");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.FilterColumnUserValues)
                    .HasForeignKey(d => d.LoginID)
                    .HasConstraintName("FK_FilterColumnUserValues_Logins");

                entity.HasOne(d => d.View)
                    .WithMany(p => p.FilterColumnUserValues)
                    .HasForeignKey(d => d.ViewID)
                    .HasConstraintName("FK_FilterColumnUserValues_Views");
            });

            //modelBuilder.Entity<FinalSettlement>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcadamicYear)
            //        .WithMany(p => p.FinalSettlements)
            //        .HasForeignKey(d => d.AcadamicYearID)
            //        .HasConstraintName("FK_FinalSettlement_AcademicYears");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.FinalSettlements)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_FinalSettlement_Classes");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.FinalSettlements)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_FinalSettlement_School");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.FinalSettlements)
            //        .HasForeignKey(d => d.SectionID)
            //        .HasConstraintName("FK_FinalSettlement_Sections");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.FinalSettlements)
            //        .HasForeignKey(d => d.StudentID)
            //        .HasConstraintName("FK_FinalSettlement_Students");
            //});

            //modelBuilder.Entity<FinalSettlementFeeTypeMap>(entity =>
            //{
            //    entity.HasKey(e => e.FinalSettlementFeeTypeMapsIID)
            //        .HasName("PK_FinalSettlementFeeTypeMapIID");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AccountTransactionHead)
            //        .WithMany(p => p.FinalSettlementFeeTypeMaps)
            //        .HasForeignKey(d => d.AccountTransactionHeadID)
            //        .HasConstraintName("FK_FinalSettlementFeeTypeMaps_AccountTransactionHeads");

            //    entity.HasOne(d => d.FeeCollectionFeeTypeMaps)
            //        .WithMany(p => p.FinalSettlementFeeTypeMaps)
            //        .HasForeignKey(d => d.FeeCollectionFeeTypeMapsID)
            //        .HasConstraintName("FK_FinalSettlementFeeTypeMaps_FeeCollectionFeeTypeMaps");

            //    entity.HasOne(d => d.FeeDueFeeTypeMaps)
            //        .WithMany(p => p.FinalSettlementFeeTypeMaps)
            //        .HasForeignKey(d => d.FeeDueFeeTypeMapsID)
            //        .HasConstraintName("FK_FinalSettlementFeeTypeMaps_FeeDueFeeTypeMaps");

            //    entity.HasOne(d => d.FeeMaster)
            //        .WithMany(p => p.FinalSettlementFeeTypeMaps)
            //        .HasForeignKey(d => d.FeeMasterID)
            //        .HasConstraintName("FK_FinalSettlementFeeTypeMaps_FeeMasters");

            //    entity.HasOne(d => d.FeePeriod)
            //        .WithMany(p => p.FinalSettlementFeeTypeMaps)
            //        .HasForeignKey(d => d.FeePeriodID)
            //        .HasConstraintName("FK_FinalSettlementFeeTypeMaps_FeePeriods");

            //    entity.HasOne(d => d.FinalSettlement)
            //        .WithMany(p => p.FinalSettlementFeeTypeMaps)
            //        .HasForeignKey(d => d.FinalSettlementID)
            //        .HasConstraintName("FK_FinalSettlementFeeTypeMaps_FinalSettlement");
            //});

            //modelBuilder.Entity<FinalSettlementMonthlySplit>(entity =>
            //{
            //    entity.HasOne(d => d.AccountTransactionHead)
            //        .WithMany(p => p.FinalSettlementMonthlySplits)
            //        .HasForeignKey(d => d.AccountTransactionHeadID)
            //        .HasConstraintName("FK_FinalSettlementMonthlySplit_AccountTransactionHead");

            //    entity.HasOne(d => d.FeeDueMonthlySplit)
            //        .WithMany(p => p.FinalSettlementMonthlySplits)
            //        .HasForeignKey(d => d.FeeDueMonthlySplitID)
            //        .HasConstraintName("FK_FinalSettlementMonthlySplit_FeeDueMonthlySplit");

            //    entity.HasOne(d => d.FinalSettlementFeeTypeMap)
            //        .WithMany(p => p.FinalSettlementMonthlySplits)
            //        .HasForeignKey(d => d.FinalSettlementFeeTypeMapId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_FinalSettlementMonthlySplit_FinalSettlementFeeTypeMaps");
            //});

            //modelBuilder.Entity<FinalSettlementPaymentModeMap>(entity =>
            //{
            //    entity.HasKey(e => e.FinalSettlementPaymentModeMapIID)
            //        .HasName("PK_FinalSettlementPaymentModeMapIID");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.FinalSettlement)
            //        .WithMany(p => p.FinalSettlementPaymentModeMaps)
            //        .HasForeignKey(d => d.FinalSettlementID)
            //        .HasConstraintName("FK_FinalSettlementPaymentModeMap_FinalSettlement");

            //    entity.HasOne(d => d.PaymentMode)
            //        .WithMany(p => p.FinalSettlementPaymentModeMaps)
            //        .HasForeignKey(d => d.PaymentModeID)
            //        .HasConstraintName("FK_FinalSettlementPaymentModeMaps_PaymentModes");
            //});

            //modelBuilder.Entity<FineMaster>(entity =>
            //{
            //    entity.Property(e => e.FineMasterID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.FineMasters)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_FineMasters_AcademicYear");

            //    entity.HasOne(d => d.FeeFineType)
            //        .WithMany(p => p.FineMasters)
            //        .HasForeignKey(d => d.FeeFineTypeID)
            //        .HasConstraintName("FK_FineMasters_FeeFineTypes");

            //    entity.HasOne(d => d.LedgerAccount)
            //        .WithMany(p => p.FineMasters)
            //        .HasForeignKey(d => d.LedgerAccountID)
            //        .HasConstraintName("FK_FineMasters_Accounts");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.FineMasters)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_FineMasters_School");
            //});

            //modelBuilder.Entity<FineMasterStudentMap>(entity =>
            //{
            //    entity.HasKey(e => e.FineMasterStudentMapIID)
            //        .HasName("PK_FineMasterStudentMapIID");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.FineMasterStudentMaps)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_FineMasterStudentMaps_AcademicYear");

            //    entity.HasOne(d => d.FineMaster)
            //        .WithMany(p => p.FineMasterStudentMaps)
            //        .HasForeignKey(d => d.FineMasterID)
            //        .HasConstraintName("FK_FineMasterStudentMaps_FineMasters");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.FineMasterStudentMaps)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_FineMasterStudentMaps_School");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.FineMasterStudentMaps)
            //        .HasForeignKey(d => d.StudentId)
            //        .HasConstraintName("FK_FineMasterStudentMaps_Students");
            //});

            //modelBuilder.Entity<FiscalYear>(entity =>
            //{
            //    entity.HasKey(e => e.FiscalYear_ID)
            //        .HasName("PK_FISCYEAR")
            //        .IsClustered(false);
            //});

            //modelBuilder.Entity<Form>(entity =>
            //{
            //    entity.Property(e => e.FormID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<FormField>(entity =>
            //{
            //    entity.Property(e => e.FormFieldID).ValueGeneratedNever();

            //    entity.HasOne(d => d.Form)
            //        .WithMany(p => p.FormFields)
            //        .HasForeignKey(d => d.FormID)
            //        .HasConstraintName("FK_FormFields_Form");
            //});

            //modelBuilder.Entity<FormValue>(entity =>
            //{
            //    entity.HasOne(d => d.FormField)
            //        .WithMany(p => p.FormValues)
            //        .HasForeignKey(d => d.FormFieldID)
            //        .HasConstraintName("FK_FormValues_FormField");

            //    entity.HasOne(d => d.Form)
            //        .WithMany(p => p.FormValues)
            //        .HasForeignKey(d => d.FormID)
            //        .HasConstraintName("FK_FormValues_Form");
            //});

            //modelBuilder.Entity<FrequencyType>(entity =>
            //{
            //    entity.Property(e => e.FrequencyTypeID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<FunctionalPeriod>(entity =>
            //{
            //    entity.Property(e => e.FunctionalPeriodID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.FunctionalPeriods)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_FunctionalPeriods_AcademicYear");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.FunctionalPeriods)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_FunctionalPeriods_School");
            //});

            //modelBuilder.Entity<Gallery>(entity =>
            //{
            //    entity.Property(e => e.ISActive).HasDefaultValueSql("((1))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.Galleries)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_Galleries_AcademicYears");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.Galleries)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_Galleries_Schools");
            //});

            //modelBuilder.Entity<GalleryAttachmentMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.ContentFile)
            //        .WithMany(p => p.GalleryAttachmentMaps)
            //        .HasForeignKey(d => d.AttachmentContentID)
            //        .HasConstraintName("FK_GalleryAttachmentMap_Content");

            //    entity.HasOne(d => d.Gallery)
            //        .WithMany(p => p.GalleryAttachmentMaps)
            //        .HasForeignKey(d => d.GalleryID)
            //        .HasConstraintName("FK_GalleryAttachmentMap_Gallery");
            //});

            modelBuilder.Entity<GeoLocationLog>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            //modelBuilder.Entity<GlobalSetting>(entity =>
            //{
            //    entity.HasKey(e => e.GlobalSettingIID)
            //        .HasName("PK_setting.GlobalSettings");
            //});

            //modelBuilder.Entity<GradeMapsForReport>(entity =>
            //{
            //    entity.HasKey(e => e.ReportGradeMapIID)
            //        .HasName("PK_GradeMapsReport");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            modelBuilder.Entity<Group>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            //modelBuilder.Entity<HealthEntry>(entity =>
            //{
            //    entity.HasKey(e => e.HealthEntryIID)
            //        .HasName("PK_HealthEntry");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.HealthEntries)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_HealthEntries_AcademicYear");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.HealthEntries)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_HealthEntries_Classes");

            //    entity.HasOne(d => d.ExamGroup)
            //        .WithMany(p => p.HealthEntries)
            //        .HasForeignKey(d => d.ExamGroupID)
            //        .HasConstraintName("FK_HealthEntries_ExamGroup");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.HealthEntries)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_HealthEntries_School");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.HealthEntries)
            //        .HasForeignKey(d => d.SectionID)
            //        .HasConstraintName("FK_HealthEntries_Sections");

            //    entity.HasOne(d => d.Teacher)
            //        .WithMany(p => p.HealthEntries)
            //        .HasForeignKey(d => d.TeacherID)
            //        .HasConstraintName("FK_HealthEntries_Teacher");
            //});

            //modelBuilder.Entity<HealthEntryStudentMap>(entity =>
            //{
            //    entity.HasOne(d => d.HealthEntry)
            //        .WithMany(p => p.HealthEntryStudentMaps)
            //        .HasForeignKey(d => d.HealthEntryID)
            //        .HasConstraintName("FK_HealthEntryStudentMaps_HealthEntry");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.HealthEntryStudentMaps)
            //        .HasForeignKey(d => d.StudentID)
            //        .HasConstraintName("FK_HealthEntryStudentMaps_Students");
            //});

            //modelBuilder.Entity<Holiday>(entity =>
            //{
            //    entity.Property(e => e.HolidayIID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.HolidayList)
            //        .WithMany(p => p.Holidays)
            //        .HasForeignKey(d => d.HolidayListID)
            //        .HasConstraintName("FK_Holidays_Holidays");

            //    entity.HasOne(d => d.HolidayType)
            //        .WithMany(p => p.Holidays)
            //        .HasForeignKey(d => d.HolidayTypeID)
            //        .HasConstraintName("FK_Holidays_HolidayTypes");
            //});

            //modelBuilder.Entity<Hostel>(entity =>
            //{
            //    entity.Property(e => e.HostelID).ValueGeneratedNever();

            //    entity.HasOne(d => d.HostelType)
            //        .WithMany(p => p.Hostels)
            //        .HasForeignKey(d => d.HostelTypeID)
            //        .HasConstraintName("FK_Hostels_HostelTypes");
            //});

            //modelBuilder.Entity<HostelRoom>(entity =>
            //{
            //    entity.Property(e => e.HostelRoomIID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Hostel)
            //        .WithMany(p => p.HostelRooms)
            //        .HasForeignKey(d => d.HostelID)
            //        .HasConstraintName("FK_HostelRooms_Hostels");

            //    entity.HasOne(d => d.RoomType)
            //        .WithMany(p => p.HostelRooms)
            //        .HasForeignKey(d => d.RoomTypeID)
            //        .HasConstraintName("FK_HostelRooms_RoomTypes");
            //});

            //modelBuilder.Entity<IndustryType>(entity =>
            //{
            //    entity.Property(e => e.IndustryTypeID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<IntegrationParameter>(entity =>
            //{
            //    entity.Property(e => e.IntegrationParameterId).ValueGeneratedNever();
            //});

            modelBuilder.Entity<InventoryVerification>(entity =>
            {
                entity.Property(e => e.InventoryVerificationIID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.InventoryVerifications)
                    .HasForeignKey(d => d.BranchID)
                    .HasConstraintName("FK_InventoryVerifications_Branches");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.InventoryVerifications)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_InventoryVerifications_Employees");

                entity.HasOne(d => d.ProductSKUMap)
                    .WithMany(p => p.InventoryVerifications)
                    .HasForeignKey(d => d.ProductSKUMapID)
                    .HasConstraintName("FK_InventoryVerifications_ProductSKUMaps");
            });

            modelBuilder.Entity<InvetoryTransaction>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.InvetoryTransactions)
                    .HasForeignKey(d => d.BranchID)
                    .HasConstraintName("FK_InvetoryTransactions_Branches");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.InvetoryTransactions)
                    .HasForeignKey(d => d.CompanyID)
                    .HasConstraintName("FK_InvetoryTransactions_Companies");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.InvetoryTransactions)
                    .HasForeignKey(d => d.CurrencyID)
                    .HasConstraintName("FK_InvetoryTransactions_Currencies");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.InvetoryTransactions)
                    .HasForeignKey(d => d.DocumentTypeID)
                    .HasConstraintName("FK_InvetoryTransactions_DocumentTypes");

                entity.HasOne(d => d.TransactionHead)
                    .WithMany(p => p.InvetoryTransactions)
                    .HasForeignKey(d => d.HeadID)
                    .HasConstraintName("FK_InvetoryTransactions_TransactionHead");

                entity.HasOne(d => d.InvetoryTransaction1)
                    .WithMany(p => p.InvetoryTransactions1)
                    .HasForeignKey(d => d.LinkDocumentID)
                    .HasConstraintName("FK_InvetoryTransactions_InvetoryTransactions");

                entity.HasOne(d => d.ProductSKUMap)
                    .WithMany(p => p.InvetoryTransactions)
                    .HasForeignKey(d => d.ProductSKUMapID)
                    .HasConstraintName("FK_InvetoryTransactions_ProductSKUMaps");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.InvetoryTransactions)
                    .HasForeignKey(d => d.UnitID)
                    .HasConstraintName("FK_InvetoryTransactions_Units");
            });

            modelBuilder.Entity<JobActivity>(entity =>
            {
                entity.Property(e => e.JobActivityID).ValueGeneratedNever();
            });

            modelBuilder.Entity<JobEntryDetail>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.JobEntryHead)
                    .WithMany(p => p.JobEntryDetails)
                    .HasForeignKey(d => d.JobEntryHeadID)
                    .HasConstraintName("FK_JobEntryDetails_JobEntryHeads");

                entity.HasOne(d => d.JobStatus)
                    .WithMany(p => p.JobEntryDetails)
                    .HasForeignKey(d => d.JobStatusID)
                    .HasConstraintName("FK_JobEntryDetails_JobStatuses");

                entity.HasOne(d => d.JobEntryHead1)
                    .WithMany(p => p.JobEntryDetails1)
                    .HasForeignKey(d => d.ParentJobEntryHeadID)
                    .HasConstraintName("FK_JobEntryDetails_JobEntryHeads1");

                entity.HasOne(d => d.ProductSKUMap)
                    .WithMany(p => p.JobEntryDetails)
                    .HasForeignKey(d => d.ProductSKUID)
                    .HasConstraintName("FK_JobEntryDetails_ProductSKUMaps");
            });

            modelBuilder.Entity<JobEntryHead>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Basket)
                    .WithMany(p => p.JobEntryHeads)
                    .HasForeignKey(d => d.BasketID)
                    .HasConstraintName("FK_JobEntryHeads_Baskets");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.JobEntryHeads)
                    .HasForeignKey(d => d.BranchID)
                    .HasConstraintName("FK_JobEntryHeads_Branches");

                entity.HasOne(d => d.DocumentType1)
                    .WithMany(p => p.JobEntryHeads1)
                    .HasForeignKey(d => d.DocumentTypeID)
                    .HasConstraintName("FK_JobEntryHeads_DocumentTypes1");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.JobEntryHeads)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_JobEntryHeads_Employees");

                entity.HasOne(d => d.JobActivity)
                    .WithMany(p => p.JobEntryHeads)
                    .HasForeignKey(d => d.JobActivityID)
                    .HasConstraintName("FK_JobEntryHeads_JobActivities");


                entity.HasOne(d => d.JobOperationStatus)
                    .WithMany(p => p.JobEntryHeads)
                    .HasForeignKey(d => d.JobOperationStatusID)
                    .HasConstraintName("FK_JobEntryHeads_JobOperationStatuses");

                entity.HasOne(d => d.JobSize)
                    .WithMany(p => p.JobEntryHeads)
                    .HasForeignKey(d => d.JobSizeID)
                    .HasConstraintName("FK_JobEntryHeads_JobSizes");

                entity.HasOne(d => d.JobStatus)
                    .WithMany(p => p.JobEntryHeads)
                    .HasForeignKey(d => d.JobStatusID)
                    .HasConstraintName("FK_JobEntryHeads_JobStatuses");

                entity.HasOne(d => d.OrderContactMap)
                    .WithMany(p => p.JobEntryHeads)
                    .HasForeignKey(d => d.OrderContactMapID)
                    .HasConstraintName("FK_JobEntryHeads_OrderContactMaps");

                //entity.HasOne(d => d.ParentJobEntryHead)
                //    .WithMany(p => p.InverseParentJobEntryHead)
                //    .HasForeignKey(d => d.ParentJobEntryHeadId)
                //    .HasConstraintName("FK_JobEntryHeads_JobEntryHeads");

                entity.HasOne(d => d.Priority)
                    .WithMany(p => p.JobEntryHeads)
                    .HasForeignKey(d => d.PriorityID)
                    .HasConstraintName("FK_JobEntryHeads_Priorities");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.JobEntryHeads)
                    .HasForeignKey(d => d.ReferenceDocumentTypeID)
                    .HasConstraintName("FK_JobEntryHeads_DocumentTypes");

                entity.HasOne(d => d.TransactionHead)
                    .WithMany(p => p.JobEntryHeads)
                    .HasForeignKey(d => d.TransactionHeadID)
                    .HasConstraintName("FK_JobEntryHeads_TransactionHead");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.JobEntryHeads)
                    .HasForeignKey(d => d.VehicleID)
                    .HasConstraintName("FK_JobEntryHeads_Vehicles");
            });

            modelBuilder.Entity<JobSize>(entity =>
            {
                entity.Property(e => e.JobSizeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<JobStatus>(entity =>
            {
                entity.Property(e => e.JobStatusID).ValueGeneratedNever();

                entity.HasOne(d => d.JobType)
                    .WithMany(p => p.JobStatus)
                    .HasForeignKey(d => d.JobTypeID)
                    .HasConstraintName("FK_JobStatuses_JobTypes");
            });

            modelBuilder.Entity<JobType>(entity =>
            {
                entity.Property(e => e.JobTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<JobsEntryHeadPayableMap>(entity =>
            {
                entity.HasOne(d => d.JobEntryHead)
                    .WithMany(p => p.JobsEntryHeadPayableMaps)
                    .HasForeignKey(d => d.JobEntryHeadID)
                    .HasConstraintName("FK_JobsEntryHeadPayableMaps_JobEntryHeads");

                entity.HasOne(d => d.Payable)
                    .WithMany(p => p.JobsEntryHeadPayableMaps)
                    .HasForeignKey(d => d.PayableID)
                    .HasConstraintName("FK_JobsEntryHeadPayableMaps_Payables");
            });

            modelBuilder.Entity<JobsEntryHeadReceivableMap>(entity =>
            {
                entity.HasOne(d => d.JobEntryHead)
                    .WithMany(p => p.JobsEntryHeadReceivableMaps)
                    .HasForeignKey(d => d.JobEntryHeadID)
                    .HasConstraintName("FK_JobsEntryHeadReceivableMaps_JobEntryHeads");

                entity.HasOne(d => d.Receivable)
                    .WithMany(p => p.JobsEntryHeadReceivableMaps)
                    .HasForeignKey(d => d.ReceivableID)
                    .HasConstraintName("FK_JobsEntryHeadReceivableMaps_Receivables");
            });

            //modelBuilder.Entity<Landmark>(entity =>
            //{
            //    entity.Property(e => e.LandmarkID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Location)
            //        .WithMany(p => p.Landmarks)
            //        .HasForeignKey(d => d.LocationID)
            //        .HasConstraintName("FK_Landmarks_Locations");
            //});

            modelBuilder.Entity<Language>(entity =>
            {
                //entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Lead>(entity =>
            {
                //entity.HasOne(d => d.AcademicYearNavigation)
                //    .WithMany(p => p.Leads)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_Leads_AcademicYears");

                //entity.HasOne(d => d.Class)
                //    .WithMany(p => p.Leads)
                //    .HasForeignKey(d => d.ClassID)
                //    .HasConstraintName("FK_Leads_Classes");

                //entity.HasOne(d => d.Company)
                //    .WithMany(p => p.Leads)
                //    .HasForeignKey(d => d.CompanyID)
                //    .HasConstraintName("FK_Leads_CRMCompanies");

                //entity.HasOne(d => d.Contact)
                //    .WithMany(p => p.Leads)
                //    .HasForeignKey(d => d.ContactID)
                //    .HasConstraintName("FK_Leads_Contacts");

                //entity.HasOne(d => d.Curriculam)
                //    .WithMany(p => p.Leads)
                //    .HasForeignKey(d => d.CurriculamID)
                //    .HasConstraintName("FK_Leads_Syllabus");

                //entity.HasOne(d => d.Gender)
                //    .WithMany(p => p.Leads)
                //    .HasForeignKey(d => d.GenderID)
                //    .HasConstraintName("FK_Leads_Genders");

                //entity.HasOne(d => d.IndustryType)
                //    .WithMany(p => p.Leads)
                //    .HasForeignKey(d => d.IndustryTypeID)
                //    .HasConstraintName("FK_Leads_IndustryTypes");

                //entity.HasOne(d => d.LeadSource)
                //    .WithMany(p => p.Leads)
                //    .HasForeignKey(d => d.LeadSourceID)
                //    .HasConstraintName("FK_Leads_LeadSources");

                //entity.HasOne(d => d.LeadStatus)
                //    .WithMany(p => p.Leads)
                //    .HasForeignKey(d => d.LeadStatusID)
                //    .HasConstraintName("FK_Leads_LeadStatus");

                //entity.HasOne(d => d.LeadType)
                //    .WithMany(p => p.Leads)
                //    .HasForeignKey(d => d.LeadTypeID)
                //    .HasConstraintName("FK_Leads_LeadTypes");

                //entity.HasOne(d => d.MarketSegment)
                //    .WithMany(p => p.Leads)
                //    .HasForeignKey(d => d.MarketSegmentID)
                //    .HasConstraintName("FK_Leads_MarketSegments");

                //entity.HasOne(d => d.Nationality)
                //    .WithMany(p => p.Leads)
                //    .HasForeignKey(d => d.NationalityID)
                //    .HasConstraintName("FK_Lead_Nationality");

                //entity.HasOne(d => d.RequestType)
                //    .WithMany(p => p.Leads)
                //    .HasForeignKey(d => d.RequestTypeID)
                //    .HasConstraintName("FK_Leads_RequestTypes");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.Leads)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_leads_School");
            });

            //modelBuilder.Entity<LeadEmailMap>(entity =>
            //{
            //    entity.HasOne(d => d.EmailTemplate)
            //        .WithMany(p => p.LeadEmailMaps)
            //        .HasForeignKey(d => d.EmailTemplateID)
            //        .HasConstraintName("FK_LeadEmailMaps_EmailTemplates");

            //    entity.HasOne(d => d.Lead)
            //        .WithMany(p => p.LeadEmailMaps)
            //        .HasForeignKey(d => d.LeadID)
            //        .HasConstraintName("FK_LeadEmailMaps_Leads");
            //});

            //modelBuilder.Entity<LeaveAllocation>(entity =>
            //{
            //    entity.Property(e => e.LeaveAllocationIID).ValueGeneratedOnAdd();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.LeaveGroup)
            //        .WithMany()
            //        .HasForeignKey(d => d.LeaveGroupID)
            //        .HasConstraintName("FK_LeaveAllocations_LeaveGroups");

            //    entity.HasOne(d => d.LeaveType)
            //        .WithMany()
            //        .HasForeignKey(d => d.LeaveTypeID)
            //        .HasConstraintName("FK_LeaveAllocations_LeaveTypes");
            //});

            //modelBuilder.Entity<LeaveApplication>(entity =>
            //{
            //    entity.Property(e => e.IsLeaveWithoutPay).HasDefaultValueSql("((0))");

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.LeaveApplications)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_LeaveApplications_Employees");

            //    entity.HasOne(d => d.FromSession)
            //        .WithMany(p => p.LeaveApplicationFromSessions)
            //        .HasForeignKey(d => d.FromSessionID)
            //        .HasConstraintName("FK_LeaveApplications_LeaveApplications");

            //    entity.HasOne(d => d.LeaveStatus)
            //        .WithMany(p => p.LeaveApplications)
            //        .HasForeignKey(d => d.LeaveStatusID)
            //        .HasConstraintName("FK_LeaveApplications_LeaveStatuses");

            //    entity.HasOne(d => d.LeaveType)
            //        .WithMany(p => p.LeaveApplications)
            //        .HasForeignKey(d => d.LeaveTypeID)
            //        .HasConstraintName("FK_LeaveApplications_LeaveTypes");

            //    entity.HasOne(d => d.StaffLeaveReason)
            //        .WithMany(p => p.LeaveApplications)
            //        .HasForeignKey(d => d.StaffLeaveReasonID)
            //        .HasConstraintName("FK_LeaveApplications_LeaveApplications1");

            //    entity.HasOne(d => d.ToSession)
            //        .WithMany(p => p.LeaveApplicationToSessions)
            //        .HasForeignKey(d => d.ToSessionID)
            //        .HasConstraintName("FK_LeaveApplications_LeaveSessions");
            //});

            //modelBuilder.Entity<LeaveApplicationApprover>(entity =>
            //{
            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.LeaveApplicationApprovers)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_LeaveApplicationApprovers_Employees");

            //    entity.HasOne(d => d.LeaveApplication)
            //        .WithMany(p => p.LeaveApplicationApprovers)
            //        .HasForeignKey(d => d.LeaveApplicationID)
            //        .HasConstraintName("FK_LeaveApplicationApprovers_LeaveApplications");
            //});

            //modelBuilder.Entity<LeaveBlockListApprover>(entity =>
            //{
            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.LeaveBlockListApprovers)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_LeaveBlockListApprovers_Employees");

            //    entity.HasOne(d => d.LeaveBlockList)
            //        .WithMany(p => p.LeaveBlockListApprovers)
            //        .HasForeignKey(d => d.LeaveBlockListID)
            //        .HasConstraintName("FK_LeaveBlockListApprovers_LeaveBlockLists");
            //});

            //modelBuilder.Entity<LeaveBlockListEntry>(entity =>
            //{
            //    entity.HasOne(d => d.LeaveBlockList)
            //        .WithMany(p => p.LeaveBlockListEntries)
            //        .HasForeignKey(d => d.LeaveBlockListID)
            //        .HasConstraintName("FK_LeaveBlockListEntries_LeaveBlockLists");
            //});

            modelBuilder.Entity<LeaveGroup>(entity =>
            {
                entity.Property(e => e.LeaveGroupID).ValueGeneratedNever();
            });

            modelBuilder.Entity<LeaveType>(entity =>
            {
                entity.Property(e => e.LeaveTypeID).ValueGeneratedNever();
            });

            //modelBuilder.Entity<LessonClassRoomComment>(entity =>
            //{
            //    entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<LessonLearningOutcome>(entity =>
            //{
            //    entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            modelBuilder.Entity<LessonPlan>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.LessonPlans)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_LessonPlans_AcademicYear");

                //entity.HasOne(d => d.Class)
                //    .WithMany(p => p.LessonPlans)
                //    .HasForeignKey(d => d.ClassID)
                //    .HasConstraintName("FK_LessonPlans_Classes");

                //entity.HasOne(d => d.ExpectedLearningOutcome)
                //    .WithMany(p => p.LessonPlans)
                //    .HasForeignKey(d => d.ExpectedLearningOutcomeID)
                //    .HasConstraintName("FK_LessonPlans_Scholastic");

                //entity.HasOne(d => d.LessonPlanStatus)
                //    .WithMany(p => p.LessonPlans)
                //    .HasForeignKey(d => d.LessonPlanStatusID)
                //    .HasConstraintName("FK_LessonPlans_LessonPlanStatuses");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.LessonPlans)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_LessonPlans_School");

                //entity.HasOne(d => d.Section)
                //    .WithMany(p => p.LessonPlans)
                //    .HasForeignKey(d => d.SectionID)
                //    .HasConstraintName("FK_LessonPlans_Sections");

                //entity.HasOne(d => d.Subject)
                //    .WithMany(p => p.LessonPlans)
                //    .HasForeignKey(d => d.SubjectID)
                //    .HasConstraintName("FK_LessonPlans_Subjects");

                //entity.HasOne(d => d.TeachingAid)
                //    .WithMany(p => p.LessonPlans)
                //    .HasForeignKey(d => d.TeachingAidID)
                //    .HasConstraintName("FK_LessonPlans_TeachingAid");
            });

            //modelBuilder.Entity<LessonPlanAttachmentMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.LessonPlan)
            //        .WithMany(p => p.LessonPlanAttachmentMaps)
            //        .HasForeignKey(d => d.LessonPlanID)
            //        .HasConstraintName("FK_LessonPlanAttachmentMaps_LessonPlan");
            //});

            //modelBuilder.Entity<LessonPlanClassSectionMap>(entity =>
            //{
            //    entity.HasKey(e => e.LessonPlanClassSectionMapIID)
            //        .HasName("PK_LessonPlanClassSectionMap");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.LessonPlanClassSectionMaps)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_LessonPlanClassSectionMap_Class");

            //    entity.HasOne(d => d.LessonPlan)
            //        .WithMany(p => p.LessonPlanClassSectionMaps)
            //        .HasForeignKey(d => d.LessonPlanID)
            //        .HasConstraintName("FK_LessonPlanClassSectionMaps_Maping");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.LessonPlanClassSectionMaps)
            //        .HasForeignKey(d => d.SectionID)
            //        .HasConstraintName("FK_LessonPlanClassSectionMap_Section");
            //});

            //modelBuilder.Entity<LessonPlanTaskAttachmentMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.LessonPlanTaskMap)
            //        .WithMany(p => p.LessonPlanTaskAttachmentMaps)
            //        .HasForeignKey(d => d.LessonPlanTaskMapID)
            //        .HasConstraintName("FK_LessonPlanAttachment_LessonPlanTask");
            //});

            //modelBuilder.Entity<LessonPlanTaskMap>(entity =>
            //{
            //    entity.HasKey(e => e.LessonPlanTaskMapIID)
            //        .HasName("PK_LessonPlanTopicTaskMaps");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.LessonPlan)
            //        .WithMany(p => p.LessonPlanTaskMaps)
            //        .HasForeignKey(d => d.LessonPlanID)
            //        .HasConstraintName("FK_LessonPlans");

            //    entity.HasOne(d => d.LessonPlanTopicMap)
            //        .WithMany(p => p.LessonPlanTaskMaps)
            //        .HasForeignKey(d => d.LessonPlanTopicMapID)
            //        .HasConstraintName("FK_LessonPlanTaskTopicMaps");

            //    entity.HasOne(d => d.TaskType)
            //        .WithMany(p => p.LessonPlanTaskMaps)
            //        .HasForeignKey(d => d.TaskTypeID)
            //        .HasConstraintName("FK_LessonPlanTaskTaskTypes");
            //});

            //modelBuilder.Entity<LessonPlanTopicAttachmentMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.LessonPlanTopicMap)
            //        .WithMany(p => p.LessonPlanTopicAttachmentMaps)
            //        .HasForeignKey(d => d.LessonPlanTopicMapID)
            //        .HasConstraintName("FK_LessonPlanAttachmentMaps_LessonPlanTopic");
            //});

            //modelBuilder.Entity<LessonPlanTopicMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.LessonPlan)
            //        .WithMany(p => p.LessonPlanTopicMaps)
            //        .HasForeignKey(d => d.LessonPlanID)
            //        .HasConstraintName("FK_LessonPlanTopicMaps_LessonPlans");
            //});

            //modelBuilder.Entity<LessonStudentEngagement>(entity =>
            //{
            //    entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<LessonSubjectKnowledge>(entity =>
            //{
            //    entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<LibraryBook>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.LibraryBooks)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_LibraryBooks_AcademicYear");

            //    entity.HasOne(d => d.BookCategoryCode)
            //        .WithMany(p => p.LibraryBooks)
            //        .HasForeignKey(d => d.BookCategoryCodeID)
            //        .HasConstraintName("FK_LibraryBooks_BookCategoryCode");

            //    entity.HasOne(d => d.BookCondition)
            //        .WithMany(p => p.LibraryBooks)
            //        .HasForeignKey(d => d.BookConditionID)
            //        .HasConstraintName("FK_LibraryBooks_LibraryBookConditions");

            //    entity.HasOne(d => d.BookStatus)
            //        .WithMany(p => p.LibraryBooks)
            //        .HasForeignKey(d => d.BookStatusID)
            //        .HasConstraintName("FK_LibraryBooks_LibraryBookStatuses");

            //    entity.HasOne(d => d.LibraryBookType)
            //        .WithMany(p => p.LibraryBooks)
            //        .HasForeignKey(d => d.LibraryBookTypeID)
            //        .HasConstraintName("FK_LibraryBooks_LibraryBookTypes");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.LibraryBooks)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_LibraryBooks_School");
            //});

            //modelBuilder.Entity<LibraryBookCategory>(entity =>
            //{
            //    entity.Property(e => e.LibraryBookCategoryID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<LibraryBookCategoryMap>(entity =>
            //{
            //    entity.HasOne(d => d.BookCategory)
            //        .WithMany(p => p.LibraryBookCategoryMaps)
            //        .HasForeignKey(d => d.BookCategoryID)
            //        .HasConstraintName("FK_LibraryBookCategoryMaps_LibraryBookCategories");

            //    entity.HasOne(d => d.LibraryBook)
            //        .WithMany(p => p.LibraryBookCategoryMaps)
            //        .HasForeignKey(d => d.LibraryBookID)
            //        .HasConstraintName("FK_LibraryBookCategoryMaps_LibraryBooks");
            //});

            //modelBuilder.Entity<LibraryBookCondition>(entity =>
            //{
            //    entity.HasKey(e => e.BookConditionID)
            //        .HasName("PK_BookConditions");
            //});

            //modelBuilder.Entity<LibraryBookMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.LibraryBook)
            //        .WithMany(p => p.LibraryBookMaps)
            //        .HasForeignKey(d => d.LibraryBookID)
            //        .HasConstraintName("FK_LibraryBookMap_LibraryBook");
            //});

            //modelBuilder.Entity<LibraryStaffRegister>(entity =>
            //{
            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.LibraryStaffRegisters)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_LibraryStaffRegisters_AcademicYear");

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.LibraryStaffRegisters)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_LibraryStaffRegisters_LibraryStaffRegisters");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.LibraryStaffRegisters)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_LibraryStaffRegisters_School");
            //});

            //modelBuilder.Entity<LibraryStudentRegister>(entity =>
            //{
            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.LibraryStudentRegisters)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_LibraryStudentRegisters_AcademicYear");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.LibraryStudentRegisters)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_LibraryStudentRegisters_School");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.LibraryStudentRegisters)
            //        .HasForeignKey(d => d.StudentID)
            //        .HasConstraintName("FK_LibraryStudentRegisters_Students");
            //});

            //modelBuilder.Entity<LibraryTransaction>(entity =>
            //{
            //    entity.Property(e => e.IsCollected).HasDefaultValueSql("((0))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.LibraryTransactions)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_LibraryTransactions_AcademicYear");

            //    entity.HasOne(d => d.BookCondion)
            //        .WithMany(p => p.LibraryTransactions)
            //        .HasForeignKey(d => d.BookCondionID)
            //        .HasConstraintName("FK_LibraryTransactions_LibraryBookConditions");

            //    entity.HasOne(d => d.Book)
            //        .WithMany(p => p.LibraryTransactions)
            //        .HasForeignKey(d => d.BookID)
            //        .HasConstraintName("FK_LibraryTransactions_LibraryBooks");

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.LibraryTransactions)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_LibraryTransactions_Employees");

            //    entity.HasOne(d => d.LibraryBookMap)
            //        .WithMany(p => p.LibraryTransactions)
            //        .HasForeignKey(d => d.LibraryBookMapID)
            //        .HasConstraintName("FK_LibraryTransaction_LibraryBookMap");

            //    entity.HasOne(d => d.LibraryTransactionType)
            //        .WithMany(p => p.LibraryTransactions)
            //        .HasForeignKey(d => d.LibraryTransactionTypeID)
            //        .HasConstraintName("FK_LibraryTransactions_LibraryTransactionTypes");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.LibraryTransactions)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_LibraryTransactions_School");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.LibraryTransactions)
            //        .HasForeignKey(d => d.StudentID)
            //        .HasConstraintName("FK_LibraryTransactions_Students");
            //});

            modelBuilder.Entity<Location>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.BranchID)
                    .HasConstraintName("FK_Locations_Branches");

                entity.HasOne(d => d.LocationType)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.LocationTypeID)
                    .HasConstraintName("FK_Locations_LocationTypes");
            });

            //modelBuilder.Entity<Location1>(entity =>
            //{
            //    entity.HasKey(e => e.LocationID)
            //        .HasName("PK_Locations_1");

            //    entity.Property(e => e.LocationID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Area)
            //        .WithMany(p => p.Location1)
            //        .HasForeignKey(d => d.AreaID)
            //        .HasConstraintName("FK_Locations_Areas");

            //    entity.HasOne(d => d.ParentLocation)
            //        .WithMany(p => p.InverseParentLocation)
            //        .HasForeignKey(d => d.ParentLocationID)
            //        .HasConstraintName("FK_Locations_Locations");
            //});

            modelBuilder.Entity<Login>(entity =>
            {
                entity.Property(e => e.RequirePasswordReset).HasDefaultValueSql("((0))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            //modelBuilder.Entity<LoginImageMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Login)
            //        .WithMany(p => p.LoginImageMaps)
            //        .HasForeignKey(d => d.LoginID)
            //        .HasConstraintName("FK_LoginImageMaps_Logins");
            //});

            //modelBuilder.Entity<LoginRoleMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Login)
            //        .WithMany(p => p.LoginRoleMaps)
            //        .HasForeignKey(d => d.LoginID)
            //        .HasConstraintName("FK_LoginRoleMaps_Logins");

            //    entity.HasOne(d => d.Role)
            //        .WithMany(p => p.LoginRoleMaps)
            //        .HasForeignKey(d => d.RoleID)
            //        .HasConstraintName("FK_LoginRoleMaps_Roles");
            //});

            //modelBuilder.Entity<LoginUserGroup>(entity =>
            //{
            //    entity.Property(e => e.LoginUserGroupID).ValueGeneratedNever();
            //});

            modelBuilder.Entity<Lookup>(entity =>
            {
                entity.Property(e => e.LookupID).ValueGeneratedNever();

                entity.Property(e => e.LookupType).IsFixedLength();
            });

            //modelBuilder.Entity<LookupColumnConditionMap>(entity =>
            //{
            //    entity.Property(e => e.LookupColumnConditionMapID).ValueGeneratedNever();

            //    entity.HasOne(d => d.Designation)
            //        .WithMany(p => p.LookupColumnConditionMaps)
            //        .HasForeignKey(d => d.DesignationID)
            //        .HasConstraintName("FK_LookupColumnConditionMaps_Designations");

            //    entity.HasOne(d => d.ScreenLookupMap)
            //        .WithMany(p => p.LookupColumnConditionMaps)
            //        .HasForeignKey(d => d.ScreenLookupMapID)
            //        .HasConstraintName("FK_LookupColumnConditionMaps_ScreenLookupMaps");
            //});

            //modelBuilder.Entity<MaritalStatus1>(entity =>
            //{
            //    entity.Property(e => e.MaritalStatusID).ValueGeneratedNever();
            //});

            modelBuilder.Entity<MarkEntryStatus>(entity =>
            {
                entity.Property(e => e.MarkEntryStatusID).ValueGeneratedOnAdd();

                //entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            //modelBuilder.Entity<MarkGrade>(entity =>
            //{
            //    entity.HasKey(e => e.MarkGradeIID)
            //        .HasName("PK_MarkGradeGroups");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.MarkGrades)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_MarkGrades_AcademicYear");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.MarkGrades)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_MarkGrades_School");
            //});

            //modelBuilder.Entity<MarkGradeMap>(entity =>
            //{
            //    entity.HasKey(e => e.MarksGradeMapIID)
            //        .HasName("PK_MarkGrades");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.MarkGradeMaps)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_MarkGradeMaps_AcademicYear");

            //    entity.HasOne(d => d.MarkGrade)
            //        .WithMany(p => p.MarkGradeMaps)
            //        .HasForeignKey(d => d.MarkGradeID)
            //        .HasConstraintName("FK_MarkGrades_MarkGradeGroups");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.MarkGradeMaps)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_MarkGradeMaps_School");
            //});

            modelBuilder.Entity<MarkRegister>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.MarkRegisters)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_MarkRegisters_AcademicYear");

                //entity.HasOne(d => d.Class)
                //    .WithMany(p => p.MarkRegisters)
                //    .HasForeignKey(d => d.ClassID)
                //    .HasConstraintName("FK_MarkRegisters_Classes");

                //entity.HasOne(d => d.ExamGroup)
                //    .WithMany(p => p.MarkRegisters)
                //    .HasForeignKey(d => d.ExamGroupID)
                //    .HasConstraintName("FK_MarkRegister_ExamGroup");

                //entity.HasOne(d => d.Exam)
                //    .WithMany(p => p.MarkRegisters)
                //    .HasForeignKey(d => d.ExamID)
                //    .HasConstraintName("FK_MarkRegisters_Exams");

                entity.HasOne(d => d.MarkEntryStatus)
                    .WithMany(p => p.MarkRegisters)
                    .HasForeignKey(d => d.MarkEntryStatusID)
                    .HasConstraintName("FK_MarkRegisters_Status");

                //entity.HasOne(d => d.PresentStatus)
                //    .WithMany(p => p.MarkRegisters)
                //    .HasForeignKey(d => d.PresentStatusID)
                //    .HasConstraintName("FK_MarkRegister_PresentStatusID");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.MarkRegisters)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_MarkRegisters_School");

                //entity.HasOne(d => d.Section)
                //    .WithMany(p => p.MarkRegisters)
                //    .HasForeignKey(d => d.SectionID)
                //    .HasConstraintName("FK_MarkRegisters_Sections");

                //entity.HasOne(d => d.Student)
                //    .WithMany(p => p.MarkRegisters)
                //    .HasForeignKey(d => d.StudentId)
                //    .HasConstraintName("FK_MarkRegisters_Students");
            });

            //modelBuilder.Entity<MarkRegisterSkill>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.MarkEntryStatus)
            //        .WithMany(p => p.MarkRegisterSkills)
            //        .HasForeignKey(d => d.MarkEntryStatusID)
            //        .HasConstraintName("FK_MarkRegisterSkills_ MarkEntryStatusID");

            //    entity.HasOne(d => d.MarkRegisterSkillGroup)
            //        .WithMany(p => p.MarkRegisterSkills)
            //        .HasForeignKey(d => d.MarkRegisterSkillGroupID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_MarkRegisterSkills_MarkRegisterSkillGroups");

            //    entity.HasOne(d => d.MarksGradeMap)
            //        .WithMany(p => p.MarkRegisterSkills)
            //        .HasForeignKey(d => d.MarksGradeMapID)
            //        .HasConstraintName("FK_MarkRegisterSkills_MarkGradeMaps");

            //    entity.HasOne(d => d.SkillGroupMaster)
            //        .WithMany(p => p.MarkRegisterSkills)
            //        .HasForeignKey(d => d.SkillGroupMasterID)
            //        .HasConstraintName("FK_MarkRegisterSkills_SkillGroupMasters");

            //    entity.HasOne(d => d.SkillMaster)
            //        .WithMany(p => p.MarkRegisterSkills)
            //        .HasForeignKey(d => d.SkillMasterID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_MarkRegisterSkills_SkillMasters");
            //});

            //modelBuilder.Entity<MarkRegisterSkillGroup>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.ClassSubjectSkillGroupMap)
            //        .WithMany(p => p.MarkRegisterSkillGroups)
            //        .HasForeignKey(d => d.ClassSubjectSkillGroupMapID)
            //        .HasConstraintName("FK_MarkRegisterSkillGroups_SkillSet");

            //    entity.HasOne(d => d.MarkRegister)
            //        .WithMany(p => p.MarkRegisterSkillGroups)
            //        .HasForeignKey(d => d.MarkRegisterID)
            //        .HasConstraintName("FK_MarkRegisterSkillGroups_MarkRegister");

            //    entity.HasOne(d => d.MarkRegisterSubjectMap)
            //        .WithMany(p => p.MarkRegisterSkillGroups)
            //        .HasForeignKey(d => d.MarkRegisterSubjectMapID)
            //        .HasConstraintName("FK_MarkRegisterSkillGroups_MarkRegisterSubjectMaps");

            //    entity.HasOne(d => d.MarksGradeMap)
            //        .WithMany(p => p.MarkRegisterSkillGroups)
            //        .HasForeignKey(d => d.MarksGradeMapID)
            //        .HasConstraintName("FK_MarkRegisterSkillGroups_MarkGradeMaps");

            //    entity.HasOne(d => d.SkillGroupMaster)
            //        .WithMany(p => p.MarkRegisterSkillGroups)
            //        .HasForeignKey(d => d.SkillGroupMasterID)
            //        .HasConstraintName("FK_MarkRegisterSkillGroups_SkillGroupMasters");

            //    entity.HasOne(d => d.Subject)
            //        .WithMany(p => p.MarkRegisterSkillGroups)
            //        .HasForeignKey(d => d.SubjectID)
            //        .HasConstraintName("FK_MarkRegisterSkillMaps_Subjects");
            //});

            //modelBuilder.Entity<MarkRegisterSubjectMap>(entity =>
            //{
            //    entity.HasKey(e => e.MarkRegisterSubjectMapIID)
            //        .HasName("PK_MarkRegisterSubjectMap");

            //    entity.Property(e => e.IsAbsent).HasDefaultValueSql("((1))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.MarkEntryStatus)
            //        .WithMany(p => p.MarkRegisterSubjectMaps)
            //        .HasForeignKey(d => d.MarkEntryStatusID)
            //        .HasConstraintName("FK_MarkRegisterSubjectMaps_ MarkEntryStatusID");

            //    entity.HasOne(d => d.MarkRegister)
            //        .WithMany(p => p.MarkRegisterSubjectMaps)
            //        .HasForeignKey(d => d.MarkRegisterID)
            //        .HasConstraintName("FK_MarkRegisterSubjectMaps_MarkRegisters");

            //    entity.HasOne(d => d.MarksGradeMap)
            //        .WithMany(p => p.MarkRegisterSubjectMaps)
            //        .HasForeignKey(d => d.MarksGradeMapID)
            //        .HasConstraintName("FK_MarkRegisterSubjectMaps_MarkRegisterGrade");

            //    entity.HasOne(d => d.Subject)
            //        .WithMany(p => p.MarkRegisterSubjectMaps)
            //        .HasForeignKey(d => d.SubjectID)
            //        .HasConstraintName("FK_MarkRegisterSubjectMaps_Subjects");
            //});

            //modelBuilder.Entity<MarkStatus>(entity =>
            //{
            //    entity.Property(e => e.MarkStatusID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<Medium>(entity =>
            //{
            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.Mediums)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_Mediums_AcademicYear");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.Mediums)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_Mediums_School");
            //});

            //modelBuilder.Entity<Member>(entity =>
            //{
            //    entity.HasOne(d => d.CreatedByNavigation)
            //        .WithMany(p => p.Members)
            //        .HasForeignKey(d => d.CreatedBy)
            //        .HasConstraintName("FK_Members_CreatedByTypes");

            //    entity.HasOne(d => d.Family)
            //        .WithMany(p => p.Members)
            //        .HasForeignKey(d => d.FamilyID)
            //        .HasConstraintName("FK_Members_Families");

            //    entity.HasOne(d => d.Gender)
            //        .WithMany(p => p.Members)
            //        .HasForeignKey(d => d.GenderID)
            //        .HasConstraintName("FK_Members_Genders");

            //    entity.HasOne(d => d.RelationWithHeadOfFamily)
            //        .WithMany(p => p.Members)
            //        .HasForeignKey(d => d.RelationWithHeadOfFamilyID)
            //        .HasConstraintName("FK_Members_RelationWithHeadOfFamilies");
            //});

            //modelBuilder.Entity<MemberHealth>(entity =>
            //{
            //    entity.HasOne(d => d.BloodGroup)
            //        .WithMany(p => p.MemberHealths)
            //        .HasForeignKey(d => d.BloodGroupID)
            //        .HasConstraintName("FK_MemberHealths_MemberHealths");

            //    entity.HasOne(d => d.Member)
            //        .WithMany(p => p.MemberHealths)
            //        .HasForeignKey(d => d.MemberID)
            //        .HasConstraintName("FK_MemberHealths_Members");
            //});

            //modelBuilder.Entity<MemberPartner>(entity =>
            //{
            //    entity.HasOne(d => d.MaritalStatus)
            //        .WithMany(p => p.MemberPartners)
            //        .HasForeignKey(d => d.MaritalStatusID)
            //        .HasConstraintName("FK_MemberPartners_MaritalStatuses");

            //    entity.HasOne(d => d.SpouseMember)
            //        .WithMany(p => p.MemberPartners)
            //        .HasForeignKey(d => d.SpouseMemberID)
            //        .HasConstraintName("FK_MemberPartners_Members");
            //});

            //modelBuilder.Entity<MemberQuestionnaireAnswerMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Member)
            //        .WithMany(p => p.MemberQuestionnaireAnswerMaps)
            //        .HasForeignKey(d => d.MemberID)
            //        .HasConstraintName("FK_MemberQuestionnaireAnswerMaps_MemberQuestionnaireAnswerMaps");

            //    entity.HasOne(d => d.QuestionnaireAnswer)
            //        .WithMany(p => p.MemberQuestionnaireAnswerMaps)
            //        .HasForeignKey(d => d.QuestionnaireAnswerID)
            //        .HasConstraintName("FK_MemberQuestionnaireAnswerMaps_QuestionnaireAnswers");

            //    entity.HasOne(d => d.Questionnaire)
            //        .WithMany(p => p.MemberQuestionnaireAnswerMaps)
            //        .HasForeignKey(d => d.QuestionnaireID)
            //        .HasConstraintName("FK_MemberQuestionnaireAnswerMaps_Questionnaires");
            //});

            modelBuilder.Entity<MenuLink>(entity =>
            {
                entity.Property(e => e.MenuLinkIID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.MenuLinkType)
                //    .WithMany(p => p.MenuLinks)
                //    .HasForeignKey(d => d.MenuLinkTypeID)
                //    .HasConstraintName("FK_MenuLinks_MenuLinkTypes");
            });

            modelBuilder.Entity<MenuLinkBrandMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.MenuLinkBrandMaps)
                    .HasForeignKey(d => d.BrandID)
                    .HasConstraintName("FK_MenuLinkBrandMaps_Brands");

                entity.HasOne(d => d.MenuLink)
                    .WithMany(p => p.MenuLinkBrandMaps)
                    .HasForeignKey(d => d.MenuLinkID)
                    .HasConstraintName("FK_MenuLinkBrandMaps_MenuLinks");
            });

            modelBuilder.Entity<MenuLinkCategoryMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.MenuLinkCategoryMaps)
                    .HasForeignKey(d => d.CategoryID)
                    .HasConstraintName("FK_MenuLinkCategoryMaps_Categories");

                entity.HasOne(d => d.MenuLink)
                    .WithMany(p => p.MenuLinkCategoryMaps)
                    .HasForeignKey(d => d.MenuLinkID)
                    .HasConstraintName("FK_MenuLinkCategoryMaps_MenuLinks");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.MenuLinkCategoryMaps)
                    .HasForeignKey(d => d.SiteID)
                    .HasConstraintName("FK_MenuLinkCategoryMaps_Sites");
            });

            modelBuilder.Entity<MenuLinkCultureData>(entity =>
            {
                entity.HasKey(e => new { e.CultureID, e.MenuLinkID });

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Culture)
                    .WithMany(p => p.MenuLinkCultureDatas)
                    .HasForeignKey(d => d.CultureID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuLinkCultureDatas_MenuLinkCultureDatas");

                entity.HasOne(d => d.MenuLink)
                    .WithMany(p => p.MenuLinkCultureDatas)
                    .HasForeignKey(d => d.MenuLinkID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuLinkCultureDatas_MenuLinks");
            });

            modelBuilder.Entity<Nationality>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<News>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.NewsType)
                    .WithMany(p => p.News)
                    .HasForeignKey(d => d.NewsTypeID)
                    .HasConstraintName("FK_News_NewsTypes");
            });

            modelBuilder.Entity<NewsType>(entity =>
            {
                entity.Property(e => e.NewsTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<NewsletterSubscription>(entity =>
            {
                entity.HasOne(d => d.Culture)
                    .WithMany(p => p.NewsletterSubscriptions)
                    .HasForeignKey(d => d.CultureID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NewsletterSubscription_Cultures");
            });

            modelBuilder.Entity<NotificationAlert>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AlertStatus)
                    .WithMany(p => p.NotificationAlerts)
                    .HasForeignKey(d => d.AlertStatusID)
                    .HasConstraintName("FK_NotificationAlerts_AlertStatuses");

                //entity.HasOne(d => d.AlertType)
                //    .WithMany(p => p.NotificationAlerts)
                //    .HasForeignKey(d => d.AlertTypeID)
                //    .HasConstraintName("FK_NotificationAlerts_AlertTypes");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.NotificationAlerts)
                    .HasForeignKey(d => d.FromLoginID)
                    .HasConstraintName("FK_NotificationAlerts_Logins");

                entity.HasOne(d => d.Login1)
                    .WithMany(p => p.NotificationAlerts1)
                    .HasForeignKey(d => d.ToLoginID)
                    .HasConstraintName("FK_NotificationAlerts_Logins1");
            });

            modelBuilder.Entity<NotificationLog>(entity =>
            {
                entity.HasKey(e => e.NotificationLogsIID)
                    .HasName("PK_EmailNotificationLogs");

                //entity.HasOne(d => d.NotificationStatus)
                //    .WithMany(p => p.NotificationLogs)
                //    .HasForeignKey(d => d.NotificationStatusID)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_NotificationLogs_NotificationStatuses");

                entity.HasOne(d => d.NotificationType)
                    .WithMany(p => p.NotificationLogs)
                    .HasForeignKey(d => d.NotificationTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NotificationLogs_NotificationTypes");
            });

            modelBuilder.Entity<NotificationQueueParentMap>(entity =>
            {
                entity.HasKey(e => e.NotificationQueueParentMapIID)
                    .HasName("PK_NotificationQueueParent");
            });

            modelBuilder.Entity<NotificationStatus>(entity =>
            {
                entity.Property(e => e.NotificationStatusID).ValueGeneratedNever();
            });

            modelBuilder.Entity<NotificationType>(entity =>
            {
                entity.Property(e => e.NotificationTypeID).ValueGeneratedNever();
            });

            //modelBuilder.Entity<NotificationUser>(entity =>
            //{
            //    entity.Property(e => e.NotificationUserID).ValueGeneratedNever();
            //});

            modelBuilder.Entity<NotificationsProcessing>(entity =>
            {
                entity.HasKey(e => e.NotificationProcessingIID)
                    .HasName("PK_EmailNotificationsProcessing");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.NotificationType)
                    .WithMany(p => p.NotificationsProcessings)
                    .HasForeignKey(d => d.NotificationTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NotificationsProcessing_NotificationTypes");
            });

            modelBuilder.Entity<NotificationsQueue>(entity =>
            {
                entity.HasKey(e => e.NotificationQueueIID)
                    .HasName("PK_EmailNotificationsQueue");

                entity.HasOne(d => d.NotificationType)
                    .WithMany(p => p.NotificationsQueues)
                    .HasForeignKey(d => d.NotificationTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NotificationsQueue_NotificationTypes");
            });

            modelBuilder.Entity<Notify>(entity =>
            {
                entity.HasKey(e => e.NotifyIID)
                    .HasName("PK_Notifiy");

                //entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ProductSKUMap)
                    .WithMany(p => p.Notifies)
                    .HasForeignKey(d => d.ProductSKUMapID)
                    .HasConstraintName("FK_Notifiy_ProductSKUMaps");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.Notifies)
                    .HasForeignKey(d => d.SiteID)
                    .HasConstraintName("FK_Notifiy_Sites");
            });

            //modelBuilder.Entity<OnlineExam>(entity =>
            //{
            //    entity.HasKey(e => e.OnlineExamIID)
            //        .HasName("PK_Exams");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.OnlineExams)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_OnlineExamResults_AcademicYear");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.OnlineExams)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_OnlineExams_Classes");

            //    entity.HasOne(d => d.OnlineExamType)
            //        .WithMany(p => p.OnlineExams)
            //        .HasForeignKey(d => d.OnlineExamTypeID)
            //        .HasConstraintName("FK_Exams_OnlineExamTypes");

            //    entity.HasOne(d => d.QuestionSelection)
            //        .WithMany(p => p.OnlineExams)
            //        .HasForeignKey(d => d.QuestionSelectionID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_Exams_QuestionSelections");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.OnlineExams)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_OnlineExamResults_school");
            //});

            //modelBuilder.Entity<OnlineExamQuestion>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.OnlineExamQuestions)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_OnlineExamQuestions_AcademicYear");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.OnlineExamQuestions)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_OnlineExamQuestions_school");
            //});

            //modelBuilder.Entity<OnlineExamQuestionMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.OnlineExamQuestionMaps)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_OnlineExamQuestionMaps_AcademicYear");

            //    entity.HasOne(d => d.OnlineExam)
            //        .WithMany(p => p.OnlineExamQuestionMaps)
            //        .HasForeignKey(d => d.OnlineExamID)
            //        .HasConstraintName("FK_OnlineExamQuestionMaps_OnlineExam");

            //    entity.HasOne(d => d.QuestionGroup)
            //        .WithMany(p => p.OnlineExamQuestionMaps)
            //        .HasForeignKey(d => d.QuestionGroupID)
            //        .HasConstraintName("FK_OnlineExamQuestionMaps_QuestionGroup");

            //    entity.HasOne(d => d.Question)
            //        .WithMany(p => p.OnlineExamQuestionMaps)
            //        .HasForeignKey(d => d.QuestionID)
            //        .HasConstraintName("FK_OnlineExamQuestionMaps_Question");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.OnlineExamQuestionMaps)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_OnlineExamQuestionMaps_School");
            //});

            //modelBuilder.Entity<OnlineExamResult>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.OnlineExamResults)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_OnlineExamResultss_AcademicYear");

            //    entity.HasOne(d => d.Candidate)
            //        .WithMany(p => p.OnlineExamResults)
            //        .HasForeignKey(d => d.CandidateID)
            //        .HasConstraintName("FK_OnlineExamResult_Candidates");

            //    entity.HasOne(d => d.OnlineExam)
            //        .WithMany(p => p.OnlineExamResults)
            //        .HasForeignKey(d => d.OnlineExamID)
            //        .HasConstraintName("FK_OnlineExam_Exams");

            //    entity.HasOne(d => d.ResultStatus)
            //        .WithMany(p => p.OnlineExamResults)
            //        .HasForeignKey(d => d.ResultStatusID)
            //        .HasConstraintName("FK_OnlineExamResults_ResultStatus");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.OnlineExamResults)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_OnlineExamResultss_school");
            //});

            //modelBuilder.Entity<OnlineExamResultQuestionMap>(entity =>
            //{
            //    entity.HasKey(e => e.OnlineExamResultQuestionMapIID)
            //        .HasName("PK_OnlineExamResultQuestionMap");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.OnlineExamResult)
            //        .WithMany(p => p.OnlineExamResultQuestionMaps)
            //        .HasForeignKey(d => d.OnlineExamResultID)
            //        .HasConstraintName("FK_OnlineExamResultQuestionMaps_OnlineExamResult");

            //    entity.HasOne(d => d.Question)
            //        .WithMany(p => p.OnlineExamResultQuestionMaps)
            //        .HasForeignKey(d => d.QuestionID)
            //        .HasConstraintName("FK_OnlineExamResultQuestionMaps_Question");
            //});

            //modelBuilder.Entity<OnlineExamResultSubjectMap>(entity =>
            //{
            //    entity.HasKey(e => e.OnlineExamResultSubjectMapIID)
            //        .HasName("PK_OnlineExamResultSubjectMap");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.OnlineExamResults)
            //        .WithMany(p => p.OnlineExamResultSubjectMaps)
            //        .HasForeignKey(d => d.OnlineExamResultsID)
            //        .HasConstraintName("FK_OnlineExamResusSubjeMaps_OnlineExamRes");

            //    entity.HasOne(d => d.Subject)
            //        .WithMany(p => p.OnlineExamResultSubjectMaps)
            //        .HasForeignKey(d => d.SubjectID)
            //        .HasConstraintName("FK_OnlineExamResultSubjectMaps_Subjects");
            //});

            //modelBuilder.Entity<OnlineExamStatus>(entity =>
            //{
            //    entity.HasKey(e => e.ExamStatusID)
            //        .HasName("PK_ExamStatuses");
            //});

            //modelBuilder.Entity<OnlineExamSubjectMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.OnlineExam)
            //        .WithMany(p => p.OnlineExamSubjectMaps)
            //        .HasForeignKey(d => d.OnlineExamID)
            //        .HasConstraintName("FK_OnlineExamSubjectMap_Exams");

            //    entity.HasOne(d => d.Subject)
            //        .WithMany(p => p.OnlineExamSubjectMaps)
            //        .HasForeignKey(d => d.SubjectID)
            //        .HasConstraintName("FK_OnlineExamSubjectMaps_Subjects");
            //});

            modelBuilder.Entity<OperationType>(entity =>
            {
                entity.Property(e => e.OperationTypeID).ValueGeneratedNever();
            });

            //modelBuilder.Entity<Operator>(entity =>
            //{
            //    entity.Property(e => e.OperatorID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<Opportunity>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Company)
            //        .WithMany(p => p.Opportunities)
            //        .HasForeignKey(d => d.CompanyID)
            //        .HasConstraintName("FK_Opportunities_CRMCompanies");

            //    entity.HasOne(d => d.Lead)
            //        .WithMany(p => p.Opportunities)
            //        .HasForeignKey(d => d.LeadID)
            //        .HasConstraintName("FK_Opportunities_Leads");

            //    entity.HasOne(d => d.OpportunityFrom)
            //        .WithMany(p => p.Opportunities)
            //        .HasForeignKey(d => d.OpportunityFromID)
            //        .HasConstraintName("FK_Opportunities_OpportunityFroms");

            //    entity.HasOne(d => d.OpportunityStatus)
            //        .WithMany(p => p.Opportunities)
            //        .HasForeignKey(d => d.OpportunityStatusID)
            //        .HasConstraintName("FK_Opportunities_OpportunityStatuses");

            //    entity.HasOne(d => d.OpportunityType)
            //        .WithMany(p => p.Opportunities)
            //        .HasForeignKey(d => d.OpportunityTypeID)
            //        .HasConstraintName("FK_Opportunities_OpportunityTypes");

            //    entity.HasOne(d => d.Sources)
            //        .WithMany(p => p.Opportunities)
            //        .HasForeignKey(d => d.SourcesID)
            //        .HasConstraintName("FK_Opportunities_Sources");
            //});

            //modelBuilder.Entity<OpportunityType>(entity =>
            //{
            //    entity.Property(e => e.OpportunityTypeID).ValueGeneratedNever();
            //});

            modelBuilder.Entity<OrderContactMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.OrderContactMaps)
                    .HasForeignKey(d => d.AreaID)
                    .HasConstraintName("FK_OrderContactMaps_Areas");

                //entity.HasOne(d => d.Contact)
                //    .WithMany(p => p.OrderContactMaps)
                //    .HasForeignKey(d => d.ContactID)
                //    .HasConstraintName("FK_OrderContactsMaps_Contacts");

                entity.HasOne(d => d.TransactionHead)
                    .WithMany(p => p.OrderContactMaps)
                    .HasForeignKey(d => d.OrderID)
                    .HasConstraintName("FK_OrderContactMaps_TransactionHead");

                entity.HasOne(d => d.Title)
                    .WithMany(p => p.OrderContactMaps)
                    .HasForeignKey(d => d.TitleID)
                    .HasConstraintName("FK_OrderContactMaps_Titles");
            });

            modelBuilder.Entity<OrderDeliveryDisplayHeadMap>(entity =>
            {
                entity.HasKey(e => new { e.HeadID, e.CultureID });

                //entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Culture)
                    .WithMany(p => p.OrderDeliveryDisplayHeadMaps)
                    .HasForeignKey(d => d.CultureID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDeliveryDisplayHeadMap_Cultures");

                entity.HasOne(d => d.TransactionHead)
                    .WithMany(p => p.OrderDeliveryDisplayHeadMaps)
                    .HasForeignKey(d => d.HeadID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDeliveryDisplayHeadMap_TransactionHead");
            });

            //modelBuilder.Entity<OrderDeliveryHolidayDetail>(entity =>
            //{
            //    entity.HasOne(d => d.OrderDeliveryHolidayHead)
            //        .WithMany(p => p.OrderDeliveryHolidayDetails)
            //        .HasForeignKey(d => d.OrderDeliveryHolidayHeadID)
            //        .HasConstraintName("FK_OrderDeliveryHolidayDetails_OrderDeliveryHolidayHead");
            //});

            //modelBuilder.Entity<OrderDeliveryHolidayHead>(entity =>
            //{
            //    entity.HasOne(d => d.Company)
            //        .WithMany(p => p.OrderDeliveryHolidayHeads)
            //        .HasForeignKey(d => d.CompanyID)
            //        .HasConstraintName("FK_OrderDeliveryHolidayHead_Companies");

            //    entity.HasOne(d => d.DeliveryType)
            //        .WithMany(p => p.OrderDeliveryHolidayHeads)
            //        .HasForeignKey(d => d.DeliveryTypeID)
            //        .HasConstraintName("FK_OrderDeliveryHolidayHead_DeliveryTypes");

            //    entity.HasOne(d => d.Site)
            //        .WithMany(p => p.OrderDeliveryHolidayHeads)
            //        .HasForeignKey(d => d.SiteID)
            //        .HasConstraintName("FK_OrderDeliveryHolidayHead_Sites");
            //});

            modelBuilder.Entity<OrderRoleTracking>(entity =>
            {
                entity.HasOne(d => d.TransactionHead)
                    .WithMany(p => p.OrderRoleTrackings)
                    .HasForeignKey(d => d.TransactionHeadID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderRoleTracking_TransactionHead");
            });

            //modelBuilder.Entity<Organization>(entity =>
            //{
            //    entity.Property(e => e.OrganizationID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.ParentOrganization)
            //        .WithMany(p => p.InverseParentOrganization)
            //        .HasForeignKey(d => d.ParentOrganizationID)
            //        .HasConstraintName("FK_Organizations_Organizations");
            //});

            //modelBuilder.Entity<PURCHASEINVOICEDETAIL>(entity =>
            //{
            //    entity.Property(e => e.DetailIID).ValueGeneratedOnAdd();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<PURCHASEINVOICEDETAILNEW>(entity =>
            //{
            //    entity.Property(e => e.DetailIID).ValueGeneratedOnAdd();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<PURCHASEINVOICEHEAD>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<PackageConfig>(entity =>
            //{
            //    entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcadamicYear)
            //        .WithMany(p => p.PackageConfigs)
            //        .HasForeignKey(d => d.AcadamicYearID)
            //        .HasConstraintName("FK_PackageConfig_AcademicYears");

            //    entity.HasOne(d => d.CreditNoteAccount)
            //        .WithMany(p => p.PackageConfigs)
            //        .HasForeignKey(d => d.CreditNoteAccountID)
            //        .HasConstraintName("FK_PackageConfig_Account");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.PackageConfigs)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_PackageConfig_School");
            //});

            //modelBuilder.Entity<PackageConfigClassMap>(entity =>
            //{
            //    entity.HasKey(e => e.PackageConfigClassMapIID)
            //        .HasName("PK_PackageConfigClassMap");

            //    entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.PackageConfigClassMaps)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_PackageConfigClassMaps_Classes");

            //    entity.HasOne(d => d.PackageConfig)
            //        .WithMany(p => p.PackageConfigClassMaps)
            //        .HasForeignKey(d => d.PackageConfigID)
            //        .HasConstraintName("FK_PackageConfigClassMaps_PackageConfig");
            //});

            //modelBuilder.Entity<PackageConfigFeeStructureMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.FeeStructure)
            //        .WithMany(p => p.PackageConfigFeeStructureMaps)
            //        .HasForeignKey(d => d.FeeStructureID)
            //        .HasConstraintName("FK_PackageConfigFeeStructureMaps_FeeStructures");

            //    entity.HasOne(d => d.PackageConfig)
            //        .WithMany(p => p.PackageConfigFeeStructureMaps)
            //        .HasForeignKey(d => d.PackageConfigID)
            //        .HasConstraintName("FK_PackageConfigFeeStructureMaps_PackageConfig");
            //});

            //modelBuilder.Entity<PackageConfigStudentGroupMap>(entity =>
            //{
            //    entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.PackageConfig)
            //        .WithMany(p => p.PackageConfigStudentGroupMaps)
            //        .HasForeignKey(d => d.PackageConfigID)
            //        .HasConstraintName("FK_PackageConfigStudentGroupMaps_PackageConfig");

            //    entity.HasOne(d => d.StudentGroup)
            //        .WithMany(p => p.PackageConfigStudentGroupMaps)
            //        .HasForeignKey(d => d.StudentGroupID)
            //        .HasConstraintName("FK_PackageConfigStudentGroupMaps_StudentGroups");
            //});

            //modelBuilder.Entity<PackageConfigStudentMap>(entity =>
            //{
            //    entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.PackageConfig)
            //        .WithMany(p => p.PackageConfigStudentMaps)
            //        .HasForeignKey(d => d.PackageConfigID)
            //        .HasConstraintName("FK_PackageConfigStudentMaps_PackageConfig");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.PackageConfigStudentMaps)
            //        .HasForeignKey(d => d.StudentID)
            //        .HasConstraintName("FK_PackageConfigStudentMaps_Students");
            //});

            modelBuilder.Entity<PackingType>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<Page>(entity =>
            {
                entity.Property(e => e.PageID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.PageType)
                    .WithMany(p => p.Pages)
                    .HasForeignKey(d => d.PageTypeID)
                    .HasConstraintName("FK_Pages_PageTypes");

                entity.HasOne(d => d.Page1)
                    .WithMany(p => p.Pages1)
                    .HasForeignKey(d => d.ParentPageID)
                    .HasConstraintName("FK_Pages_Pages1");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.Pages)
                    .HasForeignKey(d => d.SiteID)
                    .HasConstraintName("FK_Pages_Sites");
            });

            modelBuilder.Entity<PageBoilerplateMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.BoilerPlate)
                    .WithMany(p => p.PageBoilerplateMaps)
                    .HasForeignKey(d => d.BoilerplateID)
                    .HasConstraintName("FK_PageBoilerplateMaps_BoilerPlates");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.PageBoilerplateMaps)
                    .HasForeignKey(d => d.PageID)
                    .HasConstraintName("FK_PageBoilerplateMaps_Pages");
            });

            modelBuilder.Entity<PageBoilerplateMapParameter>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.PageBoilerplateMap)
                    .WithMany(p => p.PageBoilerplateMapParameters)
                    .HasForeignKey(d => d.PageBoilerplateMapID)
                    .HasConstraintName("FK_PageBoilerplateMapParameters_PageBoilerplateMaps");
            });
            modelBuilder.Entity<PageBoilerplateReport>(entity =>
            {
                entity.Property(e => e.PageBoilerplateReportID).ValueGeneratedNever();

                entity.HasOne(d => d.BoilerPlate)
                    .WithMany(p => p.PageBoilerplateReports)
                    .HasForeignKey(d => d.BoilerPlateID)
                    .HasConstraintName("FK_PageBoilerplateReport_BoilerPlates");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.PageBoilerplateReports)
                    .HasForeignKey(d => d.PageID)
                    .HasConstraintName("FK_PageBoilerplateReport_Pages");
            });
            modelBuilder.Entity<PageBoilerplateMapParameterCultureData>(entity =>
            {
                entity.HasKey(e => new { e.CultureID, e.PageBoilerplateMapParameterID });

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Culture)
                    .WithMany(p => p.PageBoilerplateMapParameterCultureDatas)
                    .HasForeignKey(d => d.CultureID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PageBoilerplateMapParameterCultureDatas_Cultures");

                entity.HasOne(d => d.PageBoilerplateMapParameter)
                    .WithMany(p => p.PageBoilerplateMapParameterCultureDatas)
                    .HasForeignKey(d => d.PageBoilerplateMapParameterID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PageBoilerplateMapParameterCultureDatas_PageBoilerplateMapParameters");
            });

            modelBuilder.Entity<Parent>(entity =>
            {
                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.Parents)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_Parents_AcademicYear");

                //entity.HasOne(d => d.CanYouVolunteerToHelpOne)
                //    .WithMany(p => p.ParentCanYouVolunteerToHelpOnes)
                //    .HasForeignKey(d => d.CanYouVolunteerToHelpOneID)
                //    .HasConstraintName("FK_Parents_VolunteerType");

                //entity.HasOne(d => d.CanYouVolunteerToHelpTwo)
                //    .WithMany(p => p.ParentCanYouVolunteerToHelpTwoes)
                //    .HasForeignKey(d => d.CanYouVolunteerToHelpTwoID)
                //    .HasConstraintName("FK_Parents_VolunteerType1");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Parents)
                    .HasForeignKey(d => d.CountryID)
                    .HasConstraintName("FK_Parents_Countries");

                entity.HasOne(d => d.Nationality)
                    .WithMany(p => p.Parents)
                    .HasForeignKey(d => d.FatherCountryID)
                    .HasConstraintName("FK_Parents_FatherNat");

                entity.HasOne(d => d.Country1)
                    .WithMany(p => p.Parents1)
                    .HasForeignKey(d => d.FatherPassportCountryofIssueID)
                    .HasConstraintName("FK_Parents_Countries2");

                //entity.HasOne(d => d.GuardianCountryofIssue)
                //    .WithMany(p => p.ParentGuardianCountryofIssues)
                //    .HasForeignKey(d => d.GuardianCountryofIssueID)
                //    .HasConstraintName("FK_Parents_Guradian_PassportCountryofIssueID");

                //entity.HasOne(d => d.GuardianNationality)
                //    .WithMany(p => p.ParentGuardianNationalities)
                //    .HasForeignKey(d => d.GuardianNationalityID)
                //    .HasConstraintName("FK_Parents_Guradian_Nationality");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.Parents)
                    .HasForeignKey(d => d.LoginID)
                    .HasConstraintName("FK_Parents_Logins");

                entity.HasOne(d => d.Nationality1)
                    .WithMany(p => p.Parents1)
                    .HasForeignKey(d => d.MotherCountryID)
                    .HasConstraintName("FK_Parents_MotherNat");

                entity.HasOne(d => d.Country2)
                    .WithMany(p => p.Parents2)
                    .HasForeignKey(d => d.MotherPassportCountryofIssueID)
                    .HasConstraintName("FK_Parents_Countries4");

                //entity.HasOne(d => d.MotherStudentRelationShip)
                //    .WithMany(p => p.Parents)
                //    .HasForeignKey(d => d.MotherStudentRelationShipID)
                //    .HasConstraintName("FK_Parents_GuardianTypes");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.Parents)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_Parents_School");
            });

            //modelBuilder.Entity<ParentStudentApplicationMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Parent)
            //        .WithMany(p => p.ParentStudentApplicationMaps)
            //        .HasForeignKey(d => d.ParentID)
            //        .HasConstraintName("FK_ParentStudentApplicationMaps_Parents");
            //});

            //modelBuilder.Entity<ParentStudentMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.ParentStudentMaps)
            //        .HasForeignKey(d => d.StudentID)
            //        .HasConstraintName("FK_ParentStudentMaps_Students");
            //});

            //modelBuilder.Entity<ParentUploadDocumentMap>(entity =>
            //{
            //    entity.HasKey(e => e.ParentUploadDocumentMapIID)
            //        .HasName("PK_ContentFiles");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Parent)
            //        .WithMany(p => p.ParentUploadDocumentMaps)
            //        .HasForeignKey(d => d.ParentID)
            //        .HasConstraintName("FK_ParentUploadDocument_Parent");

            //    entity.HasOne(d => d.UploadDocument)
            //        .WithMany(p => p.ParentUploadDocumentMaps)
            //        .HasForeignKey(d => d.UploadDocumentID)
            //        .HasConstraintName("FK_ParentUploadDocumentMaps_UploadDocument");

            //    entity.HasOne(d => d.UploadDocumentType)
            //        .WithMany(p => p.ParentUploadDocumentMaps)
            //        .HasForeignKey(d => d.UploadDocumentTypeID)
            //        .HasConstraintName("FK_ParentUploadDocumentMaps_UploadDocumentTypes");
            //});

            //modelBuilder.Entity<PassportDetailMap>(entity =>
            //{
            //    entity.HasKey(e => e.PassportDetailsIID)
            //        .HasName("PK_PassportDetails");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.CountryofIssue)
            //        .WithMany(p => p.PassportDetailMaps)
            //        .HasForeignKey(d => d.CountryofIssueID)
            //        .HasConstraintName("FK_PassportDetailMaps_Countries");
            //});

            modelBuilder.Entity<PassportVisaDetail>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.PassportVisaDetails)
                    .HasForeignKey(d => d.CountryofIssueID)
                    .HasConstraintName("FK_PassportVisa_CountryOfIssue");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.PassportVisaDetails)
                    .HasForeignKey(d => d.ReferenceID)
                    .HasConstraintName("FK_PassportVisa_Employee");

                entity.HasOne(d => d.Sponsor)
                    .WithMany(p => p.PassportVisaDetails)
                    .HasForeignKey(d => d.SponsorID)
                    .HasConstraintName("FK_PassportVisa_Sponsor");
            });

            modelBuilder.Entity<Payable>(entity =>
            {
                entity.HasKey(e => e.PayableIID)
                    .HasName("PK_Receivable1");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Payables)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_Payables_Accounts");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Payables)
                    .HasForeignKey(d => d.CurrencyID)
                    .HasConstraintName("FK_Payables_Currencies");

                entity.HasOne(d => d.DocumentStatus)
                    .WithMany(p => p.Payables)
                    .HasForeignKey(d => d.DocumentStatusID)
                    .HasConstraintName("FK_Payables_DocumentStatuses");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.Payables)
                    .HasForeignKey(d => d.DocumentTypeID)
                    .HasConstraintName("FK_Payables_Payables1");

                entity.HasOne(d => d.Payable1)
                    .WithMany(p => p.Payables1)
                    .HasForeignKey(d => d.ReferencePayablesID)
                    .HasConstraintName("FK_Payables_Payables");

                entity.HasOne(d => d.TransactionStatus)
                    .WithMany(p => p.Payables)
                    .HasForeignKey(d => d.TransactionStatusID)
                    .HasConstraintName("FK_Payables_TransactionStatuses");
            });

            modelBuilder.Entity<PaymentDetailsKnet>(entity =>
            {
                entity.HasKey(e => new { e.TrackID, e.TrackKey })
                    .HasName("PK_PaymentDetails");

                entity.Property(e => e.InitOn).HasDefaultValueSql("(getdate())");
            });

            //modelBuilder.Entity<PaymentDetailsMyFatoorah>(entity =>
            //{
            //    entity.HasKey(e => new { e.TrackID, e.TrackKey })
            //        .HasName("PK_MyFatoorahPaymentDetails");

            //    entity.Property(e => e.InitOn).HasDefaultValueSql("(getdate())");
            //});

            modelBuilder.Entity<PaymentDetailsPayPal>(entity =>
            {
                entity.HasKey(e => new { e.TrackID, e.TrackKey })
                    .HasName("PK_PayPalTrans");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.PaymentDetailsPayPals)
                    .HasForeignKey(d => d.RefCustomerID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PaymentDetailsPayPal_Customers");
            });

            modelBuilder.Entity<PaymentDetailsTheFort>(entity =>
            {
                entity.HasKey(e => new { e.TrackID, e.TrackKey });

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.PaymentDetailsTheForts)
                    .HasForeignKey(d => d.CustomerID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PaymentDetailsTheFort_Customers");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasOne(d => d.OrderMaster)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.RefOrderID);

                entity.HasOne(d => d.ProductMaster)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.RefOrderProductID);

                entity.HasOne(d => d.ProductMaster1)
                    .WithMany(p => p.OrderItems1)
                    .HasForeignKey(d => d.RefOrderProductID);
            });

            //modelBuilder.Entity<PaymentEntry>(entity =>
            //{
            //    entity.Property(e => e.PayrollGenerationIID).ValueGeneratedOnAdd();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<PaymentExceptionByZoneDelivery>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.DeliveryType)
            //        .WithMany(p => p.PaymentExceptionByZoneDeliveries)
            //        .HasForeignKey(d => d.DeliveryTypeID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_PaymentExceptionByZoneDelivery_DeliveryTypes");

            //    entity.HasOne(d => d.PaymentMethod)
            //        .WithMany(p => p.PaymentExceptionByZoneDeliveries)
            //        .HasForeignKey(d => d.PaymentMethodID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_PaymentExceptionByZoneDelivery_PaymentMethods");

            //    entity.HasOne(d => d.Zone)
            //        .WithMany(p => p.PaymentExceptionByZoneDeliveries)
            //        .HasForeignKey(d => d.ZoneID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_PaymentExceptionByZoneDelivery_Zones");
            //});

            modelBuilder.Entity<PaymentGroup>(entity =>
            {
                entity.Property(e => e.PaymentGroupID).ValueGeneratedNever();

                entity.HasMany(d => d.PaymentMethods)
                   .WithMany(p => p.PaymentGroups)
                   .UsingEntity<Dictionary<string, object>>(
                       "PaymentGroupPaymentTypeMap",
                       l => l.HasOne<PaymentMethod>().WithMany().HasForeignKey("PaymentMethodID").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_PaymentGroupPaymentTypeMaps_PaymentMethods"),
                       r => r.HasOne<PaymentGroup>().WithMany().HasForeignKey("PaymentGroupID").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_PaymentGroupPaymentTypeMaps_PaymentGroupPaymentTypeMaps"),
                       j =>
                       {
                           j.HasKey("PaymentGroupID", "PaymentMethodID");

                           j.ToTable("PaymentGroupPaymentTypeMaps", "mutual");
                       });
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.Property(e => e.PaymentMethodID).ValueGeneratedNever();
            });

            //modelBuilder.Entity<PaymentMethodCultureData>(entity =>
            //{
            //    entity.HasKey(e => new { e.PaymentMethodID, e.CultureID });

            //    entity.HasOne(d => d.Culture)
            //        .WithMany(p => p.PaymentMethodCultureDatas)
            //        .HasForeignKey(d => d.CultureID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_PaymentMethodCultureDatas_Cultures");

            //    entity.HasOne(d => d.PaymentMethodNavigation)
            //        .WithMany(p => p.PaymentMethodCultureDatas)
            //        .HasForeignKey(d => d.PaymentMethodID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_PaymentMethodCultureDatas_PaymentMethods");
            //});

            modelBuilder.Entity<PaymentMethodSiteMap>(entity =>
            {
                entity.HasKey(e => new { e.SiteID, e.PaymentMethodID });

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.PaymentMethodSiteMaps)
                    .HasForeignKey(d => d.PaymentMethodID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PaymentMethodSiteMaps_PaymentMethods");
            });

            modelBuilder.Entity<PaymentMode>(entity =>
            {
                entity.Property(e => e.PaymentModeID).ValueGeneratedNever();

                //entity.HasOne(d => d.Account)
                //    .WithMany(p => p.PaymentModes)
                //    .HasForeignKey(d => d.AccountId)
                //    .HasConstraintName("FK_PaymentModes_Accounts");

                //entity.HasOne(d => d.TenderType)
                //    .WithMany(p => p.PaymentModes)
                //    .HasForeignKey(d => d.TenderTypeID)
                //    .HasConstraintName("FK_PaymentModes_TenderTypes");
            });

            modelBuilder.Entity<PeriodClosingTranHead>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
                entity.HasOne(d => d.Company)
                   .WithMany(p => p.PeriodClosingTranHeads)
                   .HasForeignKey(d => d.CompanyID)
                   .HasConstraintName("FK_PeriodClosingTranHead_Comapny");

                entity.HasOne(d => d.PeriodClosingTranStatus)
                    .WithMany(p => p.PeriodClosingTranHeads)
                    .HasForeignKey(d => d.PeriodClosingTranStatusID)
                    .HasConstraintName("FK_[PeriodClosingTranHead_TranStatus");
            });

            modelBuilder.Entity<PeriodClosingTranMaster>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<PeriodClosingTranStatus>(entity =>
            {
                entity.Property(e => e.PeriodClosingTranStatusID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<PeriodClosingTranTail>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.PeriodClosingTranHead)
                    .WithMany(p => p.PeriodClosingTranTails)
                    .HasForeignKey(d => d.PeriodClosingTranHeadID)
                    .HasConstraintName("FK_PeriodClosingTails_TranHeads");

                entity.HasOne(d => d.PeriodClosingTranMaster)
                    .WithMany(p => p.PeriodClosingTranTails)
                    .HasForeignKey(d => d.PeriodClosingTranMasterID)
                    .HasConstraintName("FK_PeriodClosingTails_TranMaster");
            });

            //modelBuilder.Entity<PeriodType>(entity =>
            //{
            //    entity.Property(e => e.PeriodTypeID).ValueGeneratedNever();

            //    entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            //});

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.Property(e => e.PermissionID).ValueGeneratedNever();
            });

            modelBuilder.Entity<PermissionCultureData>(entity =>
            {
                entity.HasKey(e => new { e.PermissionID, e.CultureID });

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Culture)
                    .WithMany(p => p.PermissionCultureDatas)
                    .HasForeignKey(d => d.CultureID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PermissionCultureDatas_Cultures");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.PermissionCultureDatas)
                    .HasForeignKey(d => d.PermissionID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PermissionCultureDatas_PermissionCultureDatas");
            });

            //modelBuilder.Entity<PhoneCallLog>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<Poll>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<PollAnswerMap>(entity =>
            //{
            //    entity.Property(e => e.PollAnswerMapIID).ValueGeneratedNever();

            //    entity.HasOne(d => d.Poll)
            //        .WithMany(p => p.PollAnswerMaps)
            //        .HasForeignKey(d => d.PollID)
            //        .HasConstraintName("FK_PollAnswerMaps_Polls");
            //});

            //modelBuilder.Entity<PostalDespatch>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<PostalReceive>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<PricingType>(entity =>
            //{
            //    entity.Property(e => e.PricingTypeID).ValueGeneratedNever();
            //});

            modelBuilder.Entity<Product>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BrandID)
                    .HasConstraintName("FK_Products_Brands");

                entity.HasOne(d => d.GLAccount)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.GLAccountID)
                    .HasConstraintName("FK_Products_GLAccount");

                entity.HasOne(d => d.ProductFamily)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductFamilyID)
                    .HasConstraintName("FK_Products_ProductFamilies");

                entity.HasOne(d => d.UnitGroup)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.PurchaseUnitGroupID)
                    .HasConstraintName("FK_Products_PurchaseUnitGroup");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.PurchaseUnitID)
                    .HasConstraintName("FK_Products_PurchaseUnit");

                entity.HasOne(d => d.UnitGroup1)
                    .WithMany(p => p.Products1)
                    .HasForeignKey(d => d.SellingUnitGroupID)
                    .HasConstraintName("FK_Products_SellingUnitGroup");

                entity.HasOne(d => d.Unit1)
                    .WithMany(p => p.Products1)
                    .HasForeignKey(d => d.SellingUnitID)
                    .HasConstraintName("FK_Products_SellingUnit");

                entity.HasOne(d => d.SeoMetadata)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SeoMetadataIID)
                    .HasConstraintName("FK_Products_SeoMetadatas");

                entity.HasOne(d => d.ProductStatu)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.StatusID)
                    .HasConstraintName("FK_Products_ProductStatus");

                //entity.HasOne(d => d.TaxTemplate)
                //    .WithMany(p => p.Products)
                //    .HasForeignKey(d => d.TaxTemplateID)
                //    .HasConstraintName("FK_Products_TaxTemplates");

                entity.HasOne(d => d.UnitGroup2)
                    .WithMany(p => p.Products2)
                    .HasForeignKey(d => d.UnitGroupID)
                    .HasConstraintName("FK_Products_UnitGroups");

                entity.HasOne(d => d.Unit2)
                    .WithMany(p => p.Products2)
                    .HasForeignKey(d => d.UnitID)
                    .HasConstraintName("FK_Products_Units");
            });

            modelBuilder.Entity<ProductRecommend>(entity =>
            {
                entity.HasOne(d => d.ProductMaster)
                    .WithMany(p => p.ProductRecommends)
                    .HasForeignKey(d => d.Ref1ProductID);

                entity.HasOne(d => d.ProductMaster1)
                    .WithMany(p => p.ProductRecommends1)
                    .HasForeignKey(d => d.Ref2ProductID);
            });

            modelBuilder.Entity<ProductAllergyMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Allergy)
                    .WithMany(p => p.ProductAllergyMaps)
                    .HasForeignKey(d => d.AllergyID)
                    .HasConstraintName("FK_ProductAllergyMaps_Allergy");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductAllergyMaps)
                    .HasForeignKey(d => d.ProductID)
                    .HasConstraintName("FK_ProductAllergyMaps_Products");
            });

            modelBuilder.Entity<ProductBundle>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.FromProduct)
                    .WithMany(p => p.ProductBundleFromProducts)
                    .HasForeignKey(d => d.FromProductID)
                    .HasConstraintName("FK_ProductBundles_Products");

                entity.HasOne(d => d.FromProductSKUMap)
                    .WithMany(p => p.ProductBundleFromProductSKUMaps)
                    .HasForeignKey(d => d.FromProductSKUMapID)
                    .HasConstraintName("FK_ProductBundles_ProductSKUMaps1");

                entity.HasOne(d => d.ToProduct)
                    .WithMany(p => p.ProductBundleToProducts)
                    .HasForeignKey(d => d.ToProductID)
                    .HasConstraintName("FK_ProductBundles_Products1");

                entity.HasOne(d => d.ToProductSKUMap)
                    .WithMany(p => p.ProductBundleToProductSKUMaps)
                    .HasForeignKey(d => d.ToProductSKUMapID)
                    .HasConstraintName("FK_ProductBundles_ProductSKUMaps");
            });

            modelBuilder.Entity<Category>(entity =>
            {  

                entity.HasOne(d => d.SeoMetadata)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.SeoMetadataID)
                    .HasConstraintName("FK_Categories_SeoMetadatas");

            });

            modelBuilder.Entity<ProductCategoryMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ProductCategoryMaps)
                    .HasForeignKey(d => d.CategoryID)
                    .HasConstraintName("FK_ProductCategoryMaps_Categories");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductCategoryMaps)
                    .HasForeignKey(d => d.ProductID)
                    .HasConstraintName("FK_ProductCategoryMaps_Products");
            });

            //modelBuilder.Entity<ProductClassMap>(entity =>
            //{
            //    entity.HasKey(e => e.ProductClassMapIID)
            //        .HasName("PK__ProductC__104170018E27D0AF");

            //    entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.ProductClassMaps)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_ProductClassMap_Class");
            //});

            modelBuilder.Entity<ProductCultureData>(entity =>
            {
                entity.HasKey(e => new { e.CultureID, e.ProductID });

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Culture)
                    .WithMany(p => p.ProductCultureDatas)
                    .HasForeignKey(d => d.CultureID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductCultureDatas_Cultures");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductCultureDatas)
                    .HasForeignKey(d => d.ProductID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductCultureDatas_Products");
            });

            modelBuilder.Entity<ProductDeliveryCountrySetting>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductDeliveryCountrySettings)
                    .HasForeignKey(d => d.ProductID)
                    .HasConstraintName("FK_ProductDeliveryCountrySettings_Products");

                entity.HasOne(d => d.ProductSKUMap)
                    .WithMany(p => p.ProductDeliveryCountrySettings)
                    .HasForeignKey(d => d.ProductSKUMapID)
                    .HasConstraintName("FK_ProductDeliveryCountrySettings_ProductSKUMaps");
            });

            modelBuilder.Entity<ProductDeliveryTypeMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.ProductDeliveryTypeMaps)
                    .HasForeignKey(d => d.BranchID)
                    .HasConstraintName("FK_ProductDeliveryTypeMaps_Branches");

                //entity.HasOne(d => d.Company)
                //    .WithMany(p => p.ProductDeliveryTypeMaps)
                //    .HasForeignKey(d => d.CompanyID)
                //    .HasConstraintName("FK_ProductDeliveryTypeMaps_CompanyID");

                entity.HasOne(d => d.DeliveryTypes1)
                    .WithMany(p => p.ProductDeliveryTypeMaps)
                    .HasForeignKey(d => d.DeliveryTypeID)
                    .HasConstraintName("FK_ProductDeliveryTypeMaps_DeliveryTypes");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductDeliveryTypeMaps)
                    .HasForeignKey(d => d.ProductID)
                    .HasConstraintName("FK_ProductDeliveryTypeMaps_Products");

                entity.HasOne(d => d.ProductSKUMap)
                    .WithMany(p => p.ProductDeliveryTypeMaps)
                    .HasForeignKey(d => d.ProductSKUMapID)
                    .HasConstraintName("FK_ProductDeliveryTypeMaps_ProductSKUMaps");
            });

            modelBuilder.Entity<ProductFamily>(entity =>
            {
                entity.HasKey(e => e.ProductFamilyIID)
                    .HasName("PK_AttributeTypes");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<ProductFamilyCultureData>(entity =>
            {
                entity.HasKey(e => new { e.CultureID, e.ProductFamilyID })
                    .HasName("PK_ProductFamiliesCultureDatas");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Culture)
                    .WithMany(p => p.ProductFamilyCultureDatas)
                    .HasForeignKey(d => d.CultureID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductFamiliesCultureDatas_Cultures");

                entity.HasOne(d => d.ProductFamily)
                    .WithMany(p => p.ProductFamilyCultureDatas)
                    .HasForeignKey(d => d.ProductFamilyID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductFamiliesCultureDatas_ProductFamilies");
            });

            modelBuilder.Entity<ProductFamilyPropertyMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.ProductFamily)
                    .WithMany(p => p.ProductFamilyPropertyMaps)
                    .HasForeignKey(d => d.ProductFamilyID)
                    .HasConstraintName("FK_ProductFamilyPropertyMaps_ProductFamilyPropertyMaps");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.ProductFamilyPropertyMaps)
                    .HasForeignKey(d => d.ProductPropertyID)
                    .HasConstraintName("FK_ProductFamilyPropertyMaps_ProductProperties");
            });

            modelBuilder.Entity<ProductFamilyPropertyTypeMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.ProductFamily)
                    .WithMany(p => p.ProductFamilyPropertyTypeMaps)
                    .HasForeignKey(d => d.ProductFamilyID)
                    .HasConstraintName("FK_ProductFamilyPropertyTypeMaps_ProductFamilies");
            });

            modelBuilder.Entity<ProductFamilyType>(entity =>
            {
                entity.HasKey(e => new { e.FamilyTypeID, e.CultureID })
                    .HasName("PK_catalog].[FamilyTypes");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Culture)
                    .WithMany(p => p.ProductFamilyTypes)
                    .HasForeignKey(d => d.CultureID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductFamilyTypes_Cultures");
            });
            modelBuilder.Entity<Qualification>(entity =>
            {
                entity.Property(e => e.TimeStamps)
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<QualificationCoreSubjectMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.EmployeeQualificationMap)
                    .WithMany(p => p.QualificationCoreSubjectMaps)
                    .HasForeignKey(d => d.EmployeeQualificationMapID)
                    .HasConstraintName("FK_QualificationSubMap_Qualification");
            });

            //modelBuilder.Entity<ProductFeeMap>(entity =>
            //{
            //    entity.HasKey(e => e.ProductFeeMapIID)
            //        .HasName("PK__ProductF__F4AB93CA4852A8FE");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            modelBuilder.Entity<ProductImageMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductImageMaps)
                    .HasForeignKey(d => d.ProductID)
                    .HasConstraintName("FK_ProductImageMaps_Products");

                entity.HasOne(d => d.ProductSKUMap)
                    .WithMany(p => p.ProductImageMaps)
                    .HasForeignKey(d => d.ProductSKUMapID)
                    .HasConstraintName("FK_ProductImageMaps_ProductSKUMaps");
            });

            modelBuilder.Entity<ProductInventory>(entity =>
            {
                entity.HasKey(e => new { e.ProductSKUMapID, e.Batch, e.BranchID });

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.ProductInventories)
                    .HasForeignKey(d => d.BranchID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductInventories_Branches");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.ProductInventories)
                    .HasForeignKey(d => d.CompanyID)
                    .HasConstraintName("FK_ProductInventories_Companies");

                entity.HasOne(d => d.ProductSKUMap)
                    .WithMany(p => p.ProductInventories)
                    .HasForeignKey(d => d.ProductSKUMapID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductInventories_ProductSKUMaps");
            });

            modelBuilder.Entity<ProductInventoryConfig>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.ProductInventoryConfigs)
                    .HasForeignKey(d => d.CompanyID)
                    .HasConstraintName("FK_ProductInventoryConfig_Companies");
            });

            modelBuilder.Entity<ProductInventoryConfigCultureData>(entity =>
            {
                entity.HasKey(e => new { e.CultureID, e.ProductInventoryConfigID })
                    .HasName("PK_[catalog].ProductInventoryConfigCultureDatas");

                entity.HasOne(d => d.Culture)
                    .WithMany(p => p.ProductInventoryConfigCultureDatas)
                    .HasForeignKey(d => d.CultureID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductInventoryConfigCultureDatas_CultureID");

                entity.HasOne(d => d.ProductInventoryConfig)
                    .WithMany(p => p.ProductInventoryConfigCultureDatas)
                    .HasForeignKey(d => d.ProductInventoryConfigID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductInventoryConfigCultureDatas_ProductInventoryConfigID");
            });

            modelBuilder.Entity<ProductInventoryProductConfigMap>(entity =>
            {
                entity.HasKey(e => new { e.ProductInventoryConfigID, e.ProductID });

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductInventoryProductConfigMaps)
                    .HasForeignKey(d => d.ProductID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductInventoryProductConfigMap_Products");

                entity.HasOne(d => d.ProductInventoryConfig)
                    .WithMany(p => p.ProductInventoryProductConfigMaps)
                    .HasForeignKey(d => d.ProductInventoryConfigID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductInventoryProductConfigMaps_ProductInventoryConfig");
            });

            modelBuilder.Entity<ProductInventorySKUConfigMap>(entity =>
            {
                entity.HasKey(e => new { e.ProductInventoryConfigID, e.ProductSKUMapID })
                    .HasName("PK_ProductInventorySKUConfigMaps_1");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.ProductInventoryConfig)
                    .WithMany(p => p.ProductInventorySKUConfigMaps)
                    .HasForeignKey(d => d.ProductInventoryConfigID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductInventorySKUConfigMaps_ProductInventoryConfig");

                entity.HasOne(d => d.ProductSKUMap)
                    .WithMany(p => p.ProductInventorySKUConfigMaps)
                    .HasForeignKey(d => d.ProductSKUMapID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductInventorySKUConfigMaps_ProductSKUMaps");
            });

            modelBuilder.Entity<ProductInventorySerialMap>(entity =>
            {
                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.ProductInventorySerialMaps)
                    .HasForeignKey(d => d.BranchID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductInventorySerialMaps_Branches");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.ProductInventorySerialMaps)
                    .HasForeignKey(d => d.CompanyID)
                    .HasConstraintName("FK_ProductInventorySerialMaps_Companies");

                entity.HasOne(d => d.ProductSKUMap)
                    .WithMany(p => p.ProductInventorySerialMaps)
                    .HasForeignKey(d => d.ProductSKUMapID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductInventorySerialMaps_ProductSKUMaps");
            });

            modelBuilder.Entity<ProductLocationMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.ProductLocationMaps)
                    .HasForeignKey(d => d.LocationID)
                    .HasConstraintName("FK_ProductLocationMaps_Locations");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductLocationMaps)
                    .HasForeignKey(d => d.ProductID)
                    .HasConstraintName("FK_ProductLocationMaps_Products");

                entity.HasOne(d => d.ProductSKUMap)
                    .WithMany(p => p.ProductLocationMaps)
                    .HasForeignKey(d => d.ProductSKUMapID)
                    .HasConstraintName("FK_ProductLocationMaps_ProductSKUMaps");
            });

            //modelBuilder.Entity<ProductOption>(entity =>
            //{
            //    entity.Property(e => e.ProductOptionID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<ProductOptionCultureData>(entity =>
            //{
            //    entity.HasKey(e => new { e.ProductOptionID, e.CultureID });

            //    entity.HasOne(d => d.Culture)
            //        .WithMany(p => p.ProductOptionCultureDatas)
            //        .HasForeignKey(d => d.CultureID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_ProductOptionCultureDatas_Cultures");

            //    entity.HasOne(d => d.ProductOption)
            //        .WithMany(p => p.ProductOptionCultureDatas)
            //        .HasForeignKey(d => d.ProductOptionID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_ProductOptionCultureDatas_ProductOptions");
            //});

            modelBuilder.Entity<ProductPriceList>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.ProductPriceListLevel)
                    .WithMany(p => p.ProductPriceLists)
                    .HasForeignKey(d => d.ProductPriceListLevelID)
                    .HasConstraintName("FK_ProductPriceLists_ProductPriceListLevels");

                entity.HasOne(d => d.ProductPriceListType)
                    .WithMany(p => p.ProductPriceLists)
                    .HasForeignKey(d => d.ProductPriceListTypeID)
                    .HasConstraintName("FK_ProductPriceLists_ProductPriceListTypes");
            });

            modelBuilder.Entity<ProductPriceListBranchMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.ProductPriceListBranchMaps)
                    .HasForeignKey(d => d.BranchID)
                    .HasConstraintName("FK_ProductPriceListBranchMaps_Branches");

                entity.HasOne(d => d.ProductPriceList)
                    .WithMany(p => p.ProductPriceListBranchMaps)
                    .HasForeignKey(d => d.ProductPriceListID)
                    .HasConstraintName("FK_ProductPriceListBranchMaps_ProductPriceLists");
            });

            modelBuilder.Entity<ProductPriceListBrandMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.ProductPriceListBrandMaps)
                    .HasForeignKey(d => d.BrandID)
                    .HasConstraintName("FK_ProductPriceListBrandMaps_Brands");

                entity.HasOne(d => d.ProductPriceList)
                    .WithMany(p => p.ProductPriceListBrandMaps)
                    .HasForeignKey(d => d.ProductPriceListID)
                    .HasConstraintName("FK_ProductPriceListBrandMaps_ProductPriceLists");
            });

            modelBuilder.Entity<ProductPriceListCategoryMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ProductPriceListCategoryMaps)
                    .HasForeignKey(d => d.CategoryID)
                    .HasConstraintName("FK_ProductPriceListCategoryMaps_Categories");

                entity.HasOne(d => d.ProductPriceList)
                    .WithMany(p => p.ProductPriceListCategoryMaps)
                    .HasForeignKey(d => d.ProductPriceListID)
                    .HasConstraintName("FK_ProductPriceListCategoryMaps_ProductPriceLists");
            });

            modelBuilder.Entity<ProductPriceListCustomerGroupMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.ProductPriceListCustomerGroupMaps)
                    .HasForeignKey(d => d.BrandID)
                    .HasConstraintName("FK_ProductPriceListCustomerGroupMaps_Brands");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ProductPriceListCustomerGroupMaps)
                    .HasForeignKey(d => d.CategoryID)
                    .HasConstraintName("FK_ProductPriceListCustomerGroupMaps_Categories");

                entity.HasOne(d => d.CustomerGroup)
                    .WithMany(p => p.ProductPriceListCustomerGroupMaps)
                    .HasForeignKey(d => d.CustomerGroupID)
                    .HasConstraintName("FK_ProductPriceListCustomerGroupMaps_CustomerGroups");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductPriceListCustomerGroupMaps)
                    .HasForeignKey(d => d.ProductID)
                    .HasConstraintName("FK_ProductPriceListCustomerGroupMaps_Products");

                entity.HasOne(d => d.ProductPriceList)
                    .WithMany(p => p.ProductPriceListCustomerGroupMaps)
                    .HasForeignKey(d => d.ProductPriceListID)
                    .HasConstraintName("FK_ProductPriceListCustomerGroupMaps_ProductPriceLists");

                entity.HasOne(d => d.ProductSKUMap)
                    .WithMany(p => p.ProductPriceListCustomerGroupMaps)
                    .HasForeignKey(d => d.ProductSKUMapID)
                    .HasConstraintName("FK_ProductPriceListCustomerGroupMaps_ProductSKUMaps");
            });

            modelBuilder.Entity<ProductPriceListCustomerMap>(entity =>
            {
                entity.HasKey(e => e.ProductPriceListCustomerMapIID)
                    .HasName("PK_ProductPriceListCustomerMap");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ProductPriceListCustomerMaps)
                    .HasForeignKey(d => d.CustomerID)
                    .HasConstraintName("FK_ProductPriceListCustomerMap_Customers");

                entity.HasOne(d => d.EntityTypeEntitlement)
                    .WithMany(p => p.ProductPriceListCustomerMaps)
                    .HasForeignKey(d => d.EntitlementID)
                    .HasConstraintName("FK_ProductPriceListCustomerMaps_EntityTypeEntitlements");

                entity.HasOne(d => d.ProductPriceList)
                    .WithMany(p => p.ProductPriceListCustomerMaps)
                    .HasForeignKey(d => d.ProductPriceListID)
                    .HasConstraintName("FK_ProductPriceListCustomerMap_ProductPriceLists");
            });

            modelBuilder.Entity<ProductPriceListLevel>(entity =>
            {
                entity.Property(e => e.ProductPriceListLevelID).ValueGeneratedNever();
            });

            modelBuilder.Entity<ProductPriceListProductMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.Company)
                //    .WithMany(p => p.ProductPriceListProductMaps)
                //    .HasForeignKey(d => d.CompanyID)
                //    .HasConstraintName("FK_ProductPriceListProductMaps_CompanyID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductPriceListProductMaps)
                    .HasForeignKey(d => d.ProductID)
                    .HasConstraintName("FK_ProductPriceListProductMaps_Products");

                entity.HasOne(d => d.ProductPriceList)
                    .WithMany(p => p.ProductPriceListProductMaps)
                    .HasForeignKey(d => d.ProductPriceListID)
                    .HasConstraintName("FK_ProductPriceListProductMaps_ProductPriceLists");
            });

            modelBuilder.Entity<ProductPriceListProductQuantityMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductPriceListProductQuantityMaps)
                    .HasForeignKey(d => d.ProductID)
                    .HasConstraintName("FK_ProductPriceListProductQuantityMaps_Products");

                entity.HasOne(d => d.ProductPriceListProductMap)
                    .WithMany(p => p.ProductPriceListProductQuantityMaps)
                    .HasForeignKey(d => d.ProductPriceListProductMapID)
                    .HasConstraintName("FK_ProductPriceListProductQuantityMaps_ProductPriceListProductMaps");
            });

            modelBuilder.Entity<ProductPriceListSKUMap>(entity =>
            {
                entity.HasKey(e => e.ProductPriceListItemMapIID)
                    .HasName("PK_ProductPriceListItemMaps");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.Company)
                //    .WithMany(p => p.ProductPriceListSKUMaps)
                //    .HasForeignKey(d => d.CompanyID)
                //    .HasConstraintName("FK_ProductPriceListSKUMaps_CompanyID");

                entity.HasOne(d => d.CustomerGroup)
                    .WithMany(p => p.ProductPriceListSKUMaps)
                    .HasForeignKey(d => d.CustomerGroupID)
                    .HasConstraintName("FK_ProductPriceListSKUMaps_CustomerGroups");

                entity.HasOne(d => d.ProductPriceList)
                    .WithMany(p => p.ProductPriceListSKUMaps)
                    .HasForeignKey(d => d.ProductPriceListID)
                    .HasConstraintName("FK_ProductPriceListItemMaps_ProductPriceLists");

                entity.HasOne(d => d.ProductSKUMap)
                    .WithMany(p => p.ProductPriceListSKUMaps)
                    .HasForeignKey(d => d.ProductSKUID)
                    .HasConstraintName("FK_ProductPriceListItemMaps_ProductSKUMaps");

                entity.HasOne(d => d.UnitGroup)
                    .WithMany(p => p.ProductPriceListSKUMaps)
                    .HasForeignKey(d => d.UnitGroundID)
                    .HasConstraintName("FK_ProductPriceListItemMaps_UnitGroups");
            });

            modelBuilder.Entity<ProductPriceListSKUQuantityMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.ProductPriceListSKUMap)
                    .WithMany(p => p.ProductPriceListSKUQuantityMaps)
                    .HasForeignKey(d => d.ProductPriceListSKUMapID)
                    .HasConstraintName("FK_ProductPriceListSKUQuantityMaps_ProductPriceListSKUMaps");

                entity.HasOne(d => d.ProductSKUMap)
                    .WithMany(p => p.ProductPriceListSKUQuantityMaps)
                    .HasForeignKey(d => d.ProductSKUMapID)
                    .HasConstraintName("FK_ProductPriceListSKUQuantityMaps_ProductSKUMaps");
            });

            modelBuilder.Entity<ProductPriceListType>(entity =>
            {
                entity.Property(e => e.ProductPriceListTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<ProductPropertyMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductPropertyMaps)
                    .HasForeignKey(d => d.ProductID)
                    .HasConstraintName("FK_ProductPropertyMaps_Products");

                entity.HasOne(d => d.ProductSKUMap)
                    .WithMany(p => p.ProductPropertyMaps)
                    .HasForeignKey(d => d.ProductSKUMapID)
                    .HasConstraintName("FK_ProductPropertyMaps_ProductSKUMaps");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.ProductPropertyMaps)
                    .HasForeignKey(d => d.PropertyID)
                    .HasConstraintName("FK_ProductPropertyMaps_ProductProperties");

                entity.HasOne(d => d.PropertyType)
                    .WithMany(p => p.ProductPropertyMaps)
                    .HasForeignKey(d => d.PropertyTypeID)
                    .HasConstraintName("FK_ProductPropertyMaps_PropertyTypes");
            });

            //modelBuilder.Entity<ProductPropertyMapCultureData>(entity =>
            //{
            //    entity.HasKey(e => new { e.CultureID, e.ProductPropertyMapID });

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<ProductSKUBranchMap>(entity =>
            //{
            //    entity.HasOne(d => d.Branch)
            //        .WithMany(p => p.ProductSKUBranchMaps)
            //        .HasForeignKey(d => d.BranchID)
            //        .HasConstraintName("FK_ProductSKUBranchMaps_Branches");

            //    entity.HasOne(d => d.ProductSKU)
            //        .WithMany(p => p.ProductSKUBranchMaps)
            //        .HasForeignKey(d => d.ProductSKUID)
            //        .HasConstraintName("FK_ProductSKUBranchMaps_ProductSKUMaps");
            //});

            modelBuilder.Entity<ProductSKUCultureData>(entity =>
            {
                entity.HasKey(e => new { e.CultureID, e.ProductSKUMapID })
                    .HasName("PK_SKUCultureDatas");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Culture)
                    .WithMany(p => p.ProductSKUCultureDatas)
                    .HasForeignKey(d => d.CultureID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SKUCultureDatas_CultureID");

                entity.HasOne(d => d.ProductSKUMap)
                    .WithMany(p => p.ProductSKUCultureDatas)
                    .HasForeignKey(d => d.ProductSKUMapID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SKUCultureDatas_ProductSKUMapID");
            });

            modelBuilder.Entity<ProductSKUMap>(entity =>
            {
                entity.Property(e => e.ProductSKUMapIIDTEXT).HasComputedColumnSql("(CONVERT([varchar](20),[ProductSKUMapIID],(0)))", true);

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductSKUMaps)
                    .HasForeignKey(d => d.ProductID)
                    .HasConstraintName("FK_ProductSKUMaps_ProductSKUMaps");

                entity.HasOne(d => d.SeoMetadata)
                    .WithMany(p => p.ProductSKUMaps)
                    .HasForeignKey(d => d.SeoMetadataID)
                    .HasConstraintName("FK_ProductSKUMaps_SEOMetaDataID");

                entity.HasOne(d => d.ProductStatu)
                    .WithMany(p => p.ProductSKUMaps)
                    .HasForeignKey(d => d.StatusID)
                    .HasConstraintName("FK_ProductSKUMaps_ProductStatus");
            });

            modelBuilder.Entity<ProductSKURackMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductSKURackMaps)
                    .HasForeignKey(d => d.ProductID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductSKURackMaps_Products");

                entity.HasOne(d => d.ProductSKUMap)
                    .WithMany(p => p.ProductSKURackMaps)
                    .HasForeignKey(d => d.ProductSKUMapID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductSKURackMaps_ProductSKUMaps");

                entity.HasOne(d => d.Rack)
                    .WithMany(p => p.ProductSKURackMaps)
                    .HasForeignKey(d => d.RackID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductSKURackMaps_Racks");
            });

            modelBuilder.Entity<ProductSKUSiteMap>(entity =>
            {
                entity.HasOne(d => d.ProductSKUMap)
                    .WithMany(p => p.ProductSKUSiteMaps)
                    .HasForeignKey(d => d.ProductSKUMapID)
                    .HasConstraintName("FK_ProductSKUSiteMap_ProductSKUMaps");
            });

            modelBuilder.Entity<ProductSKUTag>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<ProductSKUTagMap>(entity =>
            {
                entity.HasOne(d => d.ProductSKUTag)
                    .WithMany(p => p.ProductSKUTagMaps)
                    .HasForeignKey(d => d.ProductSKUTagID)
                    .HasConstraintName("FK_ProductSKUTagMaps_ProductSKUTags");

                entity.HasOne(d => d.ProductSKUMap)
                    .WithMany(p => p.ProductSKUTagMaps)
                    .HasForeignKey(d => d.ProductSKuMapID)
                    .HasConstraintName("FK_ProductSKUTagMaps_ProductSKUMaps");
            });

            modelBuilder.Entity<ProductSerialMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.TransactionDetail)
                    .WithMany(p => p.ProductSerialMaps)
                    .HasForeignKey(d => d.DetailID)
                    .HasConstraintName("FK_ProductSerialMaps_TransactionDetails");

                entity.HasOne(d => d.ProductSKUMap)
                    .WithMany(p => p.ProductSerialMaps)
                    .HasForeignKey(d => d.ProductSKUMapID)
                    .HasConstraintName("FK_ProductSerialMaps_ProductSKUMaps");
            });

            //modelBuilder.Entity<ProductStatusCultureData>(entity =>
            //{
            //    entity.HasKey(e => new { e.CoultureID, e.ProductStatusID });
            //});

            //modelBuilder.Entity<ProductStudentMap>(entity =>
            //{
            //    entity.HasKey(e => e.ProductStudentMapIID)
            //        .HasName("PK__ProductS__E5A7AA18AA6E7C67");

            //    entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.ProductStudentMaps)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_ProductStudentMaps_AcademicYears");

            //    entity.HasOne(d => d.FeeMaster)
            //        .WithMany(p => p.ProductStudentMaps)
            //        .HasForeignKey(d => d.FeeMasterID)
            //        .HasConstraintName("FK_ProductStudentMaps_FeeMasters");

            //    entity.HasOne(d => d.Product)
            //        .WithMany(p => p.ProductStudentMaps)
            //        .HasForeignKey(d => d.ProductID)
            //        .HasConstraintName("FK_ProductStudentMaps_Products");

            //    entity.HasOne(d => d.ProductSKUMap)
            //        .WithMany(p => p.ProductStudentMaps)
            //        .HasForeignKey(d => d.ProductSKUMapID)
            //        .HasConstraintName("FK_ProductStudentMaps_ProductSKUMaps");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.ProductStudentMaps)
            //        .HasForeignKey(d => d.StudentID)
            //        .HasConstraintName("FK_ProductStudentMaps_Students");
            //});

            modelBuilder.Entity<ProductTag>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.Product)
                //    .WithMany(p => p.ProductTags)
                //    .HasForeignKey(d => d.ProductID)
                //    .HasConstraintName("FK_ProductTags_Products");
            });

            modelBuilder.Entity<ProductTagMap>(entity =>
            {
                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductTagMaps)
                    .HasForeignKey(d => d.ProductID)
                    .HasConstraintName("FK_ProductTagMaps_Products");

                entity.HasOne(d => d.ProductTag)
                    .WithMany(p => p.ProductTagMaps)
                    .HasForeignKey(d => d.ProductTagID)
                    .HasConstraintName("FK_ProductTagMaps_ProductTags");
            });

            modelBuilder.Entity<ProductToProductMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductToProductMaps)
                    .HasForeignKey(d => d.ProductID)
                    .HasConstraintName("FK_ProductToProductMaps_Products2");

                entity.HasOne(d => d.Product1)
                    .WithMany(p => p.ProductToProductMaps1)
                    .HasForeignKey(d => d.ProductIDTo)
                    .HasConstraintName("FK_ProductToProductMaps_Products3");
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.Property(e => e.ProductTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<ProductTypeDeliveryTypeMap>(entity =>
            {
                entity.Property(e => e.ProductTypeDeliveryTypeMapIID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.ProductTypeDeliveryTypeMaps)
                    .HasForeignKey(d => d.CompanyID)
                    .HasConstraintName("FK_ProductTypeDeliveryTypeMaps_Companies");

                entity.HasOne(d => d.DeliveryTypes1)
                    .WithMany(p => p.ProductTypeDeliveryTypeMaps)
                    .HasForeignKey(d => d.DeliveryTypeID)
                    .HasConstraintName("FK_ProductTypeDeliveryTypeMaps_DeliveryTypes");

                entity.HasOne(d => d.ProductType)
                    .WithMany(p => p.ProductTypeDeliveryTypeMaps)
                    .HasForeignKey(d => d.ProductTypeID)
                    .HasConstraintName("FK_ProductTypeDeliveryTypeMaps_ProductTypes");
            });

            modelBuilder.Entity<ProductVideoMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductVideoMaps)
                    .HasForeignKey(d => d.ProductID)
                    .HasConstraintName("FK_ProductVideoMaps_Products");

                entity.HasOne(d => d.ProductSKUMap)
                    .WithMany(p => p.ProductVideoMaps)
                    .HasForeignKey(d => d.ProductSKUMapID)
                    .HasConstraintName("FK_ProductVideoMaps_ProductSKUMaps");
            });

            //modelBuilder.Entity<ProgressReport>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.ProgressReports)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_ProgressReports_AcademicYear");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.ProgressReports)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_ProgressReports_Classes");

            //    entity.HasOne(d => d.ExamGroup)
            //        .WithMany(p => p.ProgressReports)
            //        .HasForeignKey(d => d.ExamGroupID)
            //        .HasConstraintName("FK_ProgressReport_ExamGroup");

            //    entity.HasOne(d => d.Exam)
            //        .WithMany(p => p.ProgressReports)
            //        .HasForeignKey(d => d.ExamID)
            //        .HasConstraintName("FK_ProgressReport_Exam");

            //    entity.HasOne(d => d.PublishStatus)
            //        .WithMany(p => p.ProgressReports)
            //        .HasForeignKey(d => d.PublishStatusID)
            //        .HasConstraintName("FK_ProgressReport_PublishStatusID");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.ProgressReports)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_ProgressReports_School");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.ProgressReports)
            //        .HasForeignKey(d => d.SectionID)
            //        .HasConstraintName("FK_ProgressReports_Sections");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.ProgressReports)
            //        .HasForeignKey(d => d.StudentId)
            //        .HasConstraintName("FK_ProgressReports_Students");
            //});

            //modelBuilder.Entity<ProgressReportPublishStatus>(entity =>
            //{
            //    entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            //});

            //modelBuilder.Entity<Project>(entity =>
            //{
            //    entity.Property(e => e.ProjectIID).ValueGeneratedOnAdd();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<Promotion>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.PromotionType)
            //        .WithMany(p => p.Promotions)
            //        .HasForeignKey(d => d.PromotionTypeID)
            //        .HasConstraintName("FK_Promotions_PromotionTypes");
            //});

            //modelBuilder.Entity<PromotionLog>(entity =>
            //{
            //    entity.HasOne(d => d.Login)
            //        .WithMany(p => p.PromotionLogs)
            //        .HasForeignKey(d => d.LoginID)
            //        .HasConstraintName("FK_PromotionLogs_Logins");

            //    entity.HasOne(d => d.Promotion)
            //        .WithMany(p => p.PromotionLogs)
            //        .HasForeignKey(d => d.PromotionID)
            //        .HasConstraintName("FK_PromotionLogs_Promotions");
            //});

            modelBuilder.Entity<Property>(entity =>
            {
                entity.HasKey(e => e.PropertyIID)
                    .HasName("PK_ProductProperties");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.PropertyType)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.PropertyTypeID)
                    .HasConstraintName("FK_Properties_PropertyTypes");

                entity.HasOne(d => d.UIControlType)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.UIControlTypeID)
                    .HasConstraintName("FK_ProductProperties_UIControlTypes");

                entity.HasOne(d => d.UIControlValidation)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.UIControlValidationID)
                    .HasConstraintName("FK_ProductProperties_UIControlValidations");
            });

            modelBuilder.Entity<PropertyCultureData>(entity =>
            {
                entity.HasKey(e => new { e.CultureID, e.PropertyID });

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Culture)
                    .WithMany(p => p.PropertyCultureDatas)
                    .HasForeignKey(d => d.CultureID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PropertyCultureDatas_Cultures");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.PropertyCultureDatas)
                    .HasForeignKey(d => d.PropertyID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PropertyCultureDatas_Properties");
            });

            modelBuilder.Entity<PropertyType>(entity =>
            {
                //entity.HasMany(d => d.PropertiesNavigation)
                //    .WithMany(p => p.PropertyTypes)
                //    .UsingEntity<Dictionary<string, object>>(
                //        "PropertyTypePropertyMap",
                //        l => l.HasOne<Property>().WithMany().HasForeignKey("PropertyID").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_PropertyTypePropertyMaps_Properties"),
                //        r => r.HasOne<PropertyType>().WithMany().HasForeignKey("PropertyTypeID").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_PropertyTypePropertyMaps_PropertyTypes"),
                //        j =>
                //        {
                //            j.HasKey("PropertyTypeID", "PropertyID");

                //            j.ToTable("PropertyTypePropertyMaps", "catalog");
                //        });
            });

            modelBuilder.Entity<PropertyTypeCultureData>(entity =>
            {
                entity.HasKey(e => new { e.CultureID, e.PropertyTypeID });

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            //modelBuilder.Entity<Question>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AnswerType)
            //        .WithMany(p => p.Questions)
            //        .HasForeignKey(d => d.AnswerTypeID)
            //        .HasConstraintName("FK_Questions_AnswerTypes");

            //    entity.HasOne(d => d.QuestionGroup)
            //        .WithMany(p => p.Questions)
            //        .HasForeignKey(d => d.QuestionGroupID)
            //        .HasConstraintName("FK_Questions_QuestionGroups");

            //    entity.HasOne(d => d.Subject)
            //        .WithMany(p => p.Questions)
            //        .HasForeignKey(d => d.SubjectID)
            //        .HasConstraintName("FK_Questions_Subjects");
            //});

            //modelBuilder.Entity<QuestionAnswerMap>(entity =>
            //{
            //    entity.HasOne(d => d.Question)
            //        .WithMany(p => p.QuestionAnswerMaps)
            //        .HasForeignKey(d => d.QuestionID)
            //        .HasConstraintName("FK_QuestionAnswerMaps_Questions");

            //    entity.HasOne(d => d.QuestionOptionMap)
            //        .WithMany(p => p.QuestionAnswerMaps)
            //        .HasForeignKey(d => d.QuestionOptionMapID)
            //        .HasConstraintName("FK_QuestionAnswerMaps_QuestionOptionMaps");
            //});

            //modelBuilder.Entity<QuestionGroup>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Subject)
            //        .WithMany(p => p.QuestionGroups)
            //        .HasForeignKey(d => d.SubjectID)
            //        .HasConstraintName("FK_QuestionGroups_Subject");
            //});

            //modelBuilder.Entity<QuestionOptionMap>(entity =>
            //{
            //    entity.HasOne(d => d.Question)
            //        .WithMany(p => p.QuestionOptionMaps)
            //        .HasForeignKey(d => d.QuestionID)
            //        .HasConstraintName("FK_QuestionOptionMaps_Questions");
            //});

            //modelBuilder.Entity<Questionnaire>(entity =>
            //{
            //    entity.HasOne(d => d.QuestionnaireAnswerType)
            //        .WithMany(p => p.Questionnaires)
            //        .HasForeignKey(d => d.QuestionnaireAnswerTypeID)
            //        .HasConstraintName("FK_Questionnaires_QuestionnaireAnswerTypes");
            //});

            //modelBuilder.Entity<QuestionnaireAnswer>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.QuestionnaireAnswerType)
            //        .WithMany(p => p.QuestionnaireAnswers)
            //        .HasForeignKey(d => d.QuestionnaireAnswerTypeID)
            //        .HasConstraintName("FK_QuestionnaireAnswers_QuestionnaireAnswerTypes");

            //    entity.HasOne(d => d.Questionnaire)
            //        .WithMany(p => p.QuestionnaireAnswers)
            //        .HasForeignKey(d => d.QuestionnaireID)
            //        .HasConstraintName("FK_QuestionnaireAnswers_Questionnaires");
            //});

            //modelBuilder.Entity<QuestionnaireAnswerType>(entity =>
            //{
            //    entity.Property(e => e.QuestionnaireAnswerTypeID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<QuestionnaireSet>(entity =>
            //{
            //    entity.Property(e => e.QuestionnaireSetID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<QuestionnaireType>(entity =>
            //{
            //    entity.Property(e => e.QuestionnaireTypeID).ValueGeneratedNever();
            //});

            modelBuilder.Entity<Rack>(entity =>
            {
                entity.Property(e => e.RackIID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            //modelBuilder.Entity<RatingScale>(entity =>
            //{
            //    entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            modelBuilder.Entity<Receivable>(entity =>
            {
                entity.HasKey(e => e.ReceivableIID)
                    .HasName("PK_Receivable");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Receivables)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_Receivables_Accounts");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Receivables)
                    .HasForeignKey(d => d.CurrencyID)
                    .HasConstraintName("FK_Receivables_Currencies");

                entity.HasOne(d => d.DocumentStatus)
                    .WithMany(p => p.Receivables)
                    .HasForeignKey(d => d.DocumentStatusID)
                    .HasConstraintName("FK_Receivables_DocumentStatuses");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.Receivables)
                    .HasForeignKey(d => d.DocumentTypeID)
                    .HasConstraintName("FK_Receivables_DocumentTypes");

                entity.HasOne(d => d.Receivable1)
                    .WithMany(p => p.Receivables1)
                    .HasForeignKey(d => d.ReferenceReceivablesID)
                    .HasConstraintName("FK_Receivables_Receivables");

                entity.HasOne(d => d.TransactionStatus)
                    .WithMany(p => p.Receivables)
                    .HasForeignKey(d => d.TransactionStatusID)
                    .HasConstraintName("FK_Receivables_TransactionStatuses");
            });

            modelBuilder.Entity<ReceivingMethod>(entity =>
            {
                entity.Property(e => e.ReceivingMethodID).ValueGeneratedNever();
            });
            modelBuilder.Entity<AccountTransactionReceivablesMap>(entity =>
            {
                entity.HasOne(d => d.AccountTransactionHead)
                    .WithMany(p => p.AccountTransactionReceivablesMaps)
                    .HasForeignKey(d => d.AccountTransactionHeadID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountTransactionReceivablesMap_TransactionHead");

                entity.HasOne(d => d.Receivable)
                    .WithMany(p => p.AccountTransactionReceivablesMaps)
                    .HasForeignKey(d => d.ReceivableID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountTransactionReceivablesMap_Receivables");
            });
            //modelBuilder.Entity<ReferFriendToken>(entity =>
            //{
            //    entity.HasOne(d => d.Customer)
            //        .WithMany(p => p.ReferFriendTokens)
            //        .HasForeignKey(d => d.CustomerID)
            //        .HasConstraintName("FK_ReferFriendTokens_Customers");
            //});

            //modelBuilder.Entity<Referral>(entity =>
            //{
            //    entity.HasOne(d => d.ReferredCustomer)
            //        .WithMany(p => p.ReferralReferredCustomers)
            //        .HasForeignKey(d => d.ReferredCustomerID)
            //        .HasConstraintName("FK_Referrals_Customers");

            //    entity.HasOne(d => d.ReferrerCustomer)
            //        .WithMany(p => p.ReferralReferrerCustomers)
            //        .HasForeignKey(d => d.ReferrerCustomerID)
            //        .HasConstraintName("FK_Referrals_Customers1");
            //});

            //modelBuilder.Entity<Refund>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcadamicYear)
            //        .WithMany(p => p.Refunds)
            //        .HasForeignKey(d => d.AcadamicYearID)
            //        .HasConstraintName("FK_Refund_AcademicYears");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.Refunds)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_Refund_Classes");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.Refunds)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_Refund_School");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.Refunds)
            //        .HasForeignKey(d => d.SectionID)
            //        .HasConstraintName("FK_Refund_Sections");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.Refunds)
            //        .HasForeignKey(d => d.StudentID)
            //        .HasConstraintName("FK_Refund_Students");
            //});

            //modelBuilder.Entity<RefundFeeTypeMap>(entity =>
            //{
            //    entity.HasKey(e => e.RefundFeeTypeMapsIID)
            //        .HasName("PK_RefundFeeTypeMapIID");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AccountTransactionHead)
            //        .WithMany(p => p.RefundFeeTypeMaps)
            //        .HasForeignKey(d => d.AccountTransactionHeadID)
            //        .HasConstraintName("FK_RefundFeeTypeMaps_AccountTransactionHeads");

            //    entity.HasOne(d => d.FeeCollectionFeeTypeMaps)
            //        .WithMany(p => p.RefundFeeTypeMaps)
            //        .HasForeignKey(d => d.FeeCollectionFeeTypeMapsID)
            //        .HasConstraintName("FK_RefundFeeTypeMaps_FeeCollectionFeeTypeMaps");

            //    entity.HasOne(d => d.FeeDueFeeTypeMaps)
            //        .WithMany(p => p.RefundFeeTypeMaps)
            //        .HasForeignKey(d => d.FeeDueFeeTypeMapsID)
            //        .HasConstraintName("FK_RefundFeeTypeMaps_FeeDueFeeTypeMaps");

            //    entity.HasOne(d => d.FeeMaster)
            //        .WithMany(p => p.RefundFeeTypeMaps)
            //        .HasForeignKey(d => d.FeeMasterID)
            //        .HasConstraintName("FK_RefundFeeTypeMaps_FeeMasters");

            //    entity.HasOne(d => d.FeePeriod)
            //        .WithMany(p => p.RefundFeeTypeMaps)
            //        .HasForeignKey(d => d.FeePeriodID)
            //        .HasConstraintName("FK_RefundFeeTypeMaps_FeePeriods");

            //    entity.HasOne(d => d.Refund)
            //        .WithMany(p => p.RefundFeeTypeMaps)
            //        .HasForeignKey(d => d.RefundID)
            //        .HasConstraintName("FK_RefundFeeTypeMaps_Refund");
            //});

            //modelBuilder.Entity<RefundMonthlySplit>(entity =>
            //{
            //    entity.HasOne(d => d.AccountTransactionHead)
            //        .WithMany(p => p.RefundMonthlySplits)
            //        .HasForeignKey(d => d.AccountTransactionHeadID)
            //        .HasConstraintName("FK_RefundMonthlySplit_AccountTransactionHead");

            //    entity.HasOne(d => d.FeeDueMonthlySplit)
            //        .WithMany(p => p.RefundMonthlySplits)
            //        .HasForeignKey(d => d.FeeDueMonthlySplitID)
            //        .HasConstraintName("FK_RefundMonthlySplit_FeeDueMonthlySplit");

            //    entity.HasOne(d => d.RefundFeeTypeMap)
            //        .WithMany(p => p.RefundMonthlySplits)
            //        .HasForeignKey(d => d.RefundFeeTypeMapId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_RefundMonthlySplit_RefundFeeTypeMaps");
            //});

            //modelBuilder.Entity<RefundPaymentModeMap>(entity =>
            //{
            //    entity.HasKey(e => e.RefundPaymentModeMapIID)
            //        .HasName("PK_RefundPaymentModeMapIID");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.PaymentMode)
            //        .WithMany(p => p.RefundPaymentModeMaps)
            //        .HasForeignKey(d => d.PaymentModeID)
            //        .HasConstraintName("FK_RefundPaymentModeMaps_PaymentModes");

            //    entity.HasOne(d => d.Refund)
            //        .WithMany(p => p.RefundPaymentModeMaps)
            //        .HasForeignKey(d => d.RefundID)
            //        .HasConstraintName("FK_RefundPaymentModeMap_Refund");
            //});

            //modelBuilder.Entity<RefundStatus>(entity =>
            //{
            //    entity.Property(e => e.RefundStatusID).ValueGeneratedNever();
            //});

            modelBuilder.Entity<RelationType>(entity =>
            {
                entity.Property(e => e.RelationTypeID).ValueGeneratedNever();
            });

            //modelBuilder.Entity<Relegion1>(entity =>
            //{
            //    entity.HasKey(e => e.RelegionID)
            //        .HasName("PK_Reglegions");
            //});

            //modelBuilder.Entity<RemarksEntry>(entity =>
            //{
            //    entity.HasKey(e => e.RemarksEntryIID)
            //        .HasName("PK_RemarksEntry");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.RemarksEntries)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_RemarksEntries_AcademicYear");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.RemarksEntries)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_RemarksEntries_Classes");

            //    entity.HasOne(d => d.ExamGroup)
            //        .WithMany(p => p.RemarksEntries)
            //        .HasForeignKey(d => d.ExamGroupID)
            //        .HasConstraintName("FK_RemarksEntries_ExamGroup");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.RemarksEntries)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_RemarksEntries_School");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.RemarksEntries)
            //        .HasForeignKey(d => d.SectionID)
            //        .HasConstraintName("FK_RemarksEntries_Sections");

            //    entity.HasOne(d => d.Teacher)
            //        .WithMany(p => p.RemarksEntries)
            //        .HasForeignKey(d => d.TeacherID)
            //        .HasConstraintName("FK_RemarksEntries_Teacher");
            //});

            //modelBuilder.Entity<RemarksEntryExamMap>(entity =>
            //{
            //    entity.HasKey(e => e.RemarksEntryExamMapIID)
            //        .HasName("PK_RemarksEntryExam");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Exam)
            //        .WithMany(p => p.RemarksEntryExamMaps)
            //        .HasForeignKey(d => d.ExamID)
            //        .HasConstraintName("FK_RemarksEntries_Exams");

            //    entity.HasOne(d => d.RemarksEntryStudentMap)
            //        .WithMany(p => p.RemarksEntryExamMaps)
            //        .HasForeignKey(d => d.RemarksEntryStudentMapID)
            //        .HasConstraintName("FK_RemarksEntryExamMap_RemarksEntryStudentMaps");

            //    entity.HasOne(d => d.subject)
            //        .WithMany(p => p.RemarksEntryExamMaps)
            //        .HasForeignKey(d => d.subjectID)
            //        .HasConstraintName("FK_RemarksEntries_Subjects");
            //});

            //modelBuilder.Entity<RemarksEntryStudentMap>(entity =>
            //{
            //    entity.HasOne(d => d.RemarksEntry)
            //        .WithMany(p => p.RemarksEntryStudentMaps)
            //        .HasForeignKey(d => d.RemarksEntryID)
            //        .HasConstraintName("FK_RemarksEntryStudentMaps_RemarksEntry");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.RemarksEntryStudentMaps)
            //        .HasForeignKey(d => d.StudentID)
            //        .HasConstraintName("FK_RemarksEntryStudentMaps_Students");
            //});

            //modelBuilder.Entity<ReportHeadGroup>(entity =>
            //{
            //    entity.Property(e => e.ReportHeadGroupID).ValueGeneratedNever();
            //});

            modelBuilder.Entity<ReturnMethod>(entity =>
            {
                entity.Property(e => e.ReturnMethodID).ValueGeneratedNever();
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleID).ValueGeneratedNever();
            });

            modelBuilder.Entity<RoleCultureData>(entity =>
            {
                entity.HasKey(e => new { e.RoleID, e.CultureID });

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Culture)
                    .WithMany(p => p.RoleCultureDatas)
                    .HasForeignKey(d => d.CultureID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleCultureDatas_Cultures");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleCultureDatas)
                    .HasForeignKey(d => d.RoleID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleCultureDatas_Roles");
            });

            modelBuilder.Entity<RolePermissionMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.RolePermissionMaps)
                    .HasForeignKey(d => d.PermissionID)
                    .HasConstraintName("FK_RolePermissionMaps_RolePermissionMaps");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolePermissionMaps)
                    .HasForeignKey(d => d.RoleID)
                    .HasConstraintName("FK_RolePermissionMaps_Roles");
            });

            //modelBuilder.Entity<RoomType>(entity =>
            //{
            //    entity.Property(e => e.RoomTypeID).ValueGeneratedNever();
            //});

            modelBuilder.Entity<Route>(entity =>
            {
                entity.Property(e => e.RouteID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Route)
                    .HasForeignKey(d => d.CountryID)
                    .HasConstraintName("FK_Routes_Countries");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.Routes)
                    .HasForeignKey(d => d.WarehouseID)
                    .HasConstraintName("FK_Routes_Warehouses");
            });



            //modelBuilder.Entity<Route1>(entity =>
            //{
            //    entity.HasKey(e => e.RouteID)
            //        .HasName("PK_Routes_1");

            //    entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.Route1)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_Routes_AcademicYear");

            //    entity.HasOne(d => d.CostCenter)
            //        .WithMany(p => p.Route1)
            //        .HasForeignKey(d => d.CostCenterID)
            //        .HasConstraintName("FK_Routes_CostCenters");

            //    entity.HasOne(d => d.RouteGroup)
            //        .WithMany(p => p.Route1)
            //        .HasForeignKey(d => d.RouteGroupID)
            //        .HasConstraintName("FK_Routes_RouteGroup");

            //    entity.HasOne(d => d.RouteType)
            //        .WithMany(p => p.Route1)
            //        .HasForeignKey(d => d.RouteTypeID)
            //        .HasConstraintName("FK_Routes_RouteTypes");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.Route1)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_Routes_School");
            //});

            //modelBuilder.Entity<RouteGroup>(entity =>
            //{
            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.RouteGroups)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_RouteGroups_AcademicYear");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.RouteGroups)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_RouteGroups_School");
            //});

            modelBuilder.Entity<RouteStopMap>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.RouteStopMaps)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_RouteStopMaps_AcademicYear");

                //entity.HasOne(d => d.Route)
                //    .WithMany(p => p.RouteStopMaps)
                //    .HasForeignKey(d => d.RouteID)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_RouteStopMaps_Routes");
            });

            //modelBuilder.Entity<RouteType>(entity =>
            //{
            //    entity.Property(e => e.IsVisible).HasDefaultValueSql("((1))");

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.RouteTypes)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_RouteTypes_AcademicYear");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.RouteTypes)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_RouteTypes_School");
            //});

            modelBuilder.Entity<RouteVehicleMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.RouteVehicleMaps)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_RouteVehicleMaps_AcademicYear");

                //entity.HasOne(d => d.Route)
                //    .WithMany(p => p.RouteVehicleMaps)
                //    .HasForeignKey(d => d.RouteID)
                //    .HasConstraintName("FK_RouteVehicleMaps_Routes");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.RouteVehicleMaps)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_RouteVehicleMaps_School");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.RouteVehicleMaps)
                    .HasForeignKey(d => d.VehicleID)
                    .HasConstraintName("FK_RouteVehicleMaps_Vehicles");
            });

            //modelBuilder.Entity<SKUMapBarCodeMap>(entity =>
            //{
            //    entity.HasOne(d => d.SKUMap)
            //        .WithMany(p => p.SKUMapBarCodeMaps)
            //        .HasForeignKey(d => d.SKUMapID)
            //        .HasConstraintName("FK_SKUMapBarCodeMaps_ProductSKUMaps");
            //});

            //modelBuilder.Entity<SKUPaymentMethodExceptionMap>(entity =>
            //{
            //    entity.HasKey(e => e.SKUPaymentMethodExceptionMapIID)
            //        .HasName("[PK_SKUPaymentMethodExceptionMaps");

            //    entity.HasOne(d => d.PaymentMethod)
            //        .WithMany(p => p.SKUPaymentMethodExceptionMaps)
            //        .HasForeignKey(d => d.PaymentMethodID)
            //        .HasConstraintName("FK_SKUPaymentMethodExceptionMaps_PaymentMethods");

            //    entity.HasOne(d => d.SKU)
            //        .WithMany(p => p.SKUPaymentMethodExceptionMaps)
            //        .HasForeignKey(d => d.SKUID)
            //        .HasConstraintName("FK_SKUPaymentMethodExceptionMaps_ProductSKUMaps");
            //});

            modelBuilder.Entity<SMSNotificationData>(entity =>
            {
                entity.Property(e => e.SMSNotificationDataIID).ValueGeneratedOnAdd();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.SMSNotificationType)
                    .WithMany()
                    .HasForeignKey(d => d.SMSNotificationTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SMSNotificationDatas_SMSNotificationTypes");
            });

            modelBuilder.Entity<SMSNotificationType>(entity =>
            {
                entity.Property(e => e.SMSNotificationTypeID).ValueGeneratedNever();

                entity.Property(e => e.TimeStamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            //modelBuilder.Entity<SalaryComponent>(entity =>
            //{
            //    entity.Property(e => e.SalaryComponentID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.ComponentType)
            //        .WithMany(p => p.SalaryComponents)
            //        .HasForeignKey(d => d.ComponentTypeID)
            //        .HasConstraintName("FK_SalaryComponents_SalaryComponentTypes");

            //    entity.HasOne(d => d.ReportHeadGroup)
            //        .WithMany(p => p.SalaryComponents)
            //        .HasForeignKey(d => d.ReportHeadGroupID)
            //        .HasConstraintName("FK_SalaryComponents_[ReportHeadGroups");

            //    entity.HasOne(d => d.SalaryComponentGroup)
            //        .WithMany(p => p.SalaryComponents)
            //        .HasForeignKey(d => d.SalaryComponentGroupID)
            //        .HasConstraintName("FK_SalaryComponent_Group");
            //});

            //modelBuilder.Entity<SalaryComponentRelationMap>(entity =>
            //{
            //    entity.HasOne(d => d.RelatedComponent)
            //        .WithMany(p => p.SalaryComponentRelationMapRelatedComponents)
            //        .HasForeignKey(d => d.RelatedComponentID)
            //        .HasConstraintName("FK_SalaryComponentMaps_RelatedComponents");

            //    entity.HasOne(d => d.RelationType)
            //        .WithMany(p => p.SalaryComponentRelationMaps)
            //        .HasForeignKey(d => d.RelationTypeID)
            //        .HasConstraintName("FK_SalaryStructureComponentMaps_Relations");

            //    entity.HasOne(d => d.SalaryComponent)
            //        .WithMany(p => p.SalaryComponentRelationMapSalaryComponents)
            //        .HasForeignKey(d => d.SalaryComponentID)
            //        .HasConstraintName("FK_SalaryComponentRelationMap_SalaryComponents");
            //});

            //modelBuilder.Entity<SalaryComponentRelationType>(entity =>
            //{
            //    entity.Property(e => e.RelationTypeID).ValueGeneratedNever();
            //});

            modelBuilder.Entity<SalaryMethod>(entity =>
            {
                entity.Property(e => e.SalaryMethodID).ValueGeneratedNever();
            });

            //modelBuilder.Entity<SalaryPaymentMode>(entity =>
            //{
            //    entity.Property(e => e.SalaryPaymentModeID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<SalarySlip>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.SalarySlips)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_SalarySlips_Employees");

            //    entity.HasOne(d => d.SalaryComponent)
            //        .WithMany(p => p.SalarySlips)
            //        .HasForeignKey(d => d.SalaryComponentID)
            //        .HasConstraintName("FK_SalarySlips_SalaryComponents");

            //    entity.HasOne(d => d.SalarySlipStatus)
            //        .WithMany(p => p.SalarySlips)
            //        .HasForeignKey(d => d.SalarySlipStatusID)
            //        .HasConstraintName("FK_SalarySlips_SalarySlipStatuses");
            //});

            //modelBuilder.Entity<SalaryStructure>(entity =>
            //{
            //    entity.HasOne(d => d.Account)
            //        .WithMany(p => p.SalaryStructures)
            //        .HasForeignKey(d => d.AccountID)
            //        .HasConstraintName("FK_SalaryStructure_Accounts");

            //    entity.HasOne(d => d.PaymentMode)
            //        .WithMany(p => p.SalaryStructures)
            //        .HasForeignKey(d => d.PaymentModeID)
            //        .HasConstraintName("FK_SalaryStructure_SalaryPaymentModes");

            //    entity.HasOne(d => d.PayrollFrequency)
            //        .WithMany(p => p.SalaryStructures)
            //        .HasForeignKey(d => d.PayrollFrequencyID)
            //        .HasConstraintName("FK_SalaryStructure_PayrollFrequencies");

            //    entity.HasOne(d => d.TimeSheetSalaryComponent)
            //        .WithMany(p => p.SalaryStructures)
            //        .HasForeignKey(d => d.TimeSheetSalaryComponentID)
            //        .HasConstraintName("FK_SalaryStructure_SalaryComponents");
            //});

            //modelBuilder.Entity<SalaryStructureComponentMap>(entity =>
            //{
            //    entity.HasOne(d => d.SalaryComponent)
            //        .WithMany(p => p.SalaryStructureComponentMaps)
            //        .HasForeignKey(d => d.SalaryComponentID)
            //        .HasConstraintName("FK_SalaryStructureComponentMaps_SalaryComponents");

            //    entity.HasOne(d => d.SalaryStructure)
            //        .WithMany(p => p.SalaryStructureComponentMaps)
            //        .HasForeignKey(d => d.SalaryStructureID)
            //        .HasConstraintName("FK_SalaryStructureComponentMaps_SalaryStructure");
            //});

            //modelBuilder.Entity<SalesPromotion>(entity =>
            //{
            //    entity.HasOne(d => d.Customer)
            //        .WithMany(p => p.SalesPromotions)
            //        .HasForeignKey(d => d.CustomerID)
            //        .HasConstraintName("FK_SalesPromotions_Customers");

            //    entity.HasOne(d => d.SalesPromotionType)
            //        .WithMany(p => p.SalesPromotions)
            //        .HasForeignKey(d => d.SalesPromotionTypeID)
            //        .HasConstraintName("FK_SalesPromotions_SalesPromotionTypes");
            //});

            //modelBuilder.Entity<SalesPromotionLog>(entity =>
            //{
            //    entity.HasOne(d => d.Customer)
            //        .WithMany(p => p.SalesPromotionLogs)
            //        .HasForeignKey(d => d.CustomerID)
            //        .HasConstraintName("FK_SalesPromotionLogs_Customers");

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.SalesPromotionLogs)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_SalesPromotionLogs_Employees");

            //    entity.HasOne(d => d.Login)
            //        .WithMany(p => p.SalesPromotionLogs)
            //        .HasForeignKey(d => d.LoginID)
            //        .HasConstraintName("FK_SalesPromotionLogs_Logins");

            //    entity.HasOne(d => d.SalesPromotion)
            //        .WithMany(p => p.SalesPromotionLogs)
            //        .HasForeignKey(d => d.SalesPromotionID)
            //        .HasConstraintName("FK_SalesPromotionLogs_SalesPromotions");
            //});

            //modelBuilder.Entity<SalesPromotionType>(entity =>
            //{
            //    entity.Property(e => e.SalesPromotionTypeID).ValueGeneratedNever();
            //});

            modelBuilder.Entity<SalesRelationshipType>(entity =>
            {
                entity.HasKey(e => new { e.CultureID, e.SalesRelationTypeID });

                entity.HasOne(d => d.Culture)
                    .WithMany(p => p.SalesRelationshipTypes)
                    .HasForeignKey(d => d.CultureID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SalesRelationshipType_Cultures");
            });

            //modelBuilder.Entity<Saloon>(entity =>
            //{
            //    entity.HasOne(d => d.Branch)
            //        .WithMany(p => p.Saloons)
            //        .HasForeignKey(d => d.BranchID)
            //        .HasConstraintName("FK_Saloons_Branches");
            //});

            //modelBuilder.Entity<ScheduleLogStatus>(entity =>
            //{
            //    entity.Property(e => e.ScheduleLogStatusID).ValueGeneratedNever();
            //});

            modelBuilder.Entity<SchedulerEntityType>(entity =>
            {
                entity.Property(e => e.SchedulerEntityTypID).ValueGeneratedNever();
            });

            modelBuilder.Entity<SchedulerType>(entity =>
            {
                entity.Property(e => e.SchedulerTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<Schools>(entity =>
            {
                //entity.HasOne(d => d.Company)
                //    .WithMany(p => p.Schools)
                //    .HasForeignKey(d => d.CompanyID)
                //    .HasConstraintName("FK_Schools_Companies");
            });

            //modelBuilder.Entity<SchoolCalender>(entity =>
            //{
            //    entity.Property(e => e.SchoolCalenderID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.SchoolCalenders)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_SchoolCalenders_AcademicYear");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.SchoolCalenders)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_SchoolCalenders_School");
            //});

            //modelBuilder.Entity<SchoolCalenderHolidayMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Holiday)
            //        .WithMany(p => p.SchoolCalenderHolidayMaps)
            //        .HasForeignKey(d => d.HolidayID)
            //        .HasConstraintName("FK_SchoolCalenderHolidayMaps_SchoolCalenderHolidayMaps");
            //});

            //modelBuilder.Entity<SchoolCreditNote>(entity =>
            //{
            //    entity.HasKey(e => e.SchoolCreditNoteIID)
            //        .HasName("PK_SchoolCreditNoteIID");

            //    entity.Property(e => e.IsDebitNote).HasDefaultValueSql("((0))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.SchoolCreditNotes)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_SchoolCreditNote_AcademicYear");

            //    entity.HasOne(d => d.AccountTransactionHead)
            //        .WithMany(p => p.SchoolCreditNotes)
            //        .HasForeignKey(d => d.AccountTransactionHeadID)
            //        .HasConstraintName("FK_SchoolCreditNote_AccountTransactionHeads");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.SchoolCreditNotes)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_SchoolCreditNote_Classes");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.SchoolCreditNotes)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_SchoolCreditNote_School");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.SchoolCreditNotes)
            //        .HasForeignKey(d => d.SectionID)
            //        .HasConstraintName("FK_SchoolCreditNote_Section");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.SchoolCreditNotes)
            //        .HasForeignKey(d => d.StudentID)
            //        .HasConstraintName("FK_SchoolCreditNote_Students");
            //});

            //modelBuilder.Entity<SchoolDateSetting>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.SchoolDateSettings)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_SchoolDateSettings_AcademicYear");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.SchoolDateSettings)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_SchoolDateSettings_Schools");
            //});

            //modelBuilder.Entity<SchoolDateSettingMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.SchoolDateSetting)
            //        .WithMany(p => p.SchoolDateSettingMaps)
            //        .HasForeignKey(d => d.SchoolDateSettingID)
            //        .HasConstraintName("FK_SchoolDateSettingMaps_DateSetting");
            //});

            //modelBuilder.Entity<SchoolEvent>(entity =>
            //{
            //    entity.HasKey(e => e.SchoolEventIID)
            //        .HasName("PK__SchoolEv__BAFAED4B2AD60482");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<SchoolPollAnswerLog>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.PollAnswerMap)
            //        .WithMany(p => p.SchoolPollAnswerLogs)
            //        .HasForeignKey(d => d.PollAnswerMapID)
            //        .HasConstraintName("FK_SchoolPollAnswerLogs_PollAnswerMaps");

            //    entity.HasOne(d => d.Poll)
            //        .WithMany(p => p.SchoolPollAnswerLogs)
            //        .HasForeignKey(d => d.PollID)
            //        .HasConstraintName("FK_SchoolPollAnswerLogs_Polls");

            //    entity.HasOne(d => d.Staff)
            //        .WithMany(p => p.SchoolPollAnswerLogs)
            //        .HasForeignKey(d => d.StaffID)
            //        .HasConstraintName("FK_SchoolPollAnswerLogs_Employees");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.SchoolPollAnswerLogs)
            //        .HasForeignKey(d => d.StudentID)
            //        .HasConstraintName("FK_SchoolPollAnswerLogs_Students");
            //});

            modelBuilder.Entity<ScreenField>(entity =>
            {
                entity.Property(e => e.ScreenFieldID).ValueGeneratedNever();
            });

            modelBuilder.Entity<ScreenFieldSetting>(entity =>
            {
                entity.Property(e => e.ScreenFieldSettingID).ValueGeneratedNever();

                entity.HasOne(d => d.ScreenField)
                    .WithMany(p => p.ScreenFieldSettings)
                    .HasForeignKey(d => d.ScreenFieldID)
                    .HasConstraintName("FK_ScreenFieldSettings_ScreenFields");

                entity.HasOne(d => d.ScreenMetadata)
                    .WithMany(p => p.ScreenFieldSettings)
                    .HasForeignKey(d => d.ScreenID)
                    .HasConstraintName("FK_ScreenFieldSettings_ScreenMetadatas");

                entity.HasOne(d => d.Sequence)
                    .WithMany(p => p.ScreenFieldSettings)
                    .HasForeignKey(d => d.SequenceID)
                    .HasConstraintName("FK_ScreenFieldSettings_Squences");

                entity.HasOne(d => d.TextTransformType)
                    .WithMany(p => p.ScreenFieldSettings)
                    .HasForeignKey(d => d.TextTransformTypeId)
                    .HasConstraintName("FK_ScreenFieldSettings_TextTransformTypes");
            });

            modelBuilder.Entity<ScreenLookupMap>(entity =>
            {
                entity.Property(e => e.ScreenLookupMapID).ValueGeneratedNever();

                entity.HasOne(d => d.ScreenMetadata)
                    .WithMany(p => p.ScreenLookupMaps)
                    .HasForeignKey(d => d.ScreenID)
                    .HasConstraintName("FK_ScreenLookupMaps_ScreenMetadatas");
            });

            modelBuilder.Entity<ScreenMetadata>(entity =>
            {
                entity.HasKey(e => e.ScreenID)
                    .HasName("PK_CRUDMetadatas");

                entity.Property(e => e.ScreenID).ValueGeneratedNever();

                //entity.HasOne(d => d.View)
                //    .WithMany(p => p.ScreenMetadatas)
                //    .HasForeignKey(d => d.ViewID)
                //    .HasConstraintName("FK_CRUDMetadatas_Views");
            });

            modelBuilder.Entity<ScreenMetadataCultureData>(entity =>
            {
                entity.HasKey(e => new { e.ScreenID, e.CultureID });

                entity.HasOne(d => d.Culture)
                    .WithMany(p => p.ScreenMetadataCultureDatas)
                    .HasForeignKey(d => d.CultureID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScreenMetadataCultureDatas_Cultures");

                entity.HasOne(d => d.ScreenMetadata)
                    .WithMany(p => p.ScreenMetadataCultureDatas)
                    .HasForeignKey(d => d.ScreenID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScreenMetadataCultureDatas_ScreenMetadatas");
            });

            modelBuilder.Entity<ScreenShortCut>(entity =>
            {
                entity.Property(e => e.ScreenShortCutID).ValueGeneratedNever();

                //entity.HasOne(d => d.ScreenMetadata)
                //    .WithMany(p => p.ScreenShortCuts)
                //    .HasForeignKey(d => d.ScreenID)
                //    .HasConstraintName("FK_ScreenShortCuts_ScreenMetadatas");
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.Property(e => e.SectionID).ValueGeneratedNever();

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.Sections)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_Sections_AcademicYear");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.Sections)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_Sections_School");
            });

            //modelBuilder.Entity<SegmentCustomerMap>(entity =>
            //{
            //    entity.HasOne(d => d.Customer)
            //        .WithMany(p => p.SegmentCustomerMaps)
            //        .HasForeignKey(d => d.CustomerID)
            //        .HasConstraintName("FK_SegmentCustomerMaps_Customers");

            //    entity.HasOne(d => d.Segment)
            //        .WithMany(p => p.SegmentCustomerMaps)
            //        .HasForeignKey(d => d.SegmentID)
            //        .HasConstraintName("FK_SegmentCustomerMaps_Segments");
            //});

            modelBuilder.Entity<SeoMetadata>(entity =>
            {
                entity.HasKey(e => e.SEOMetadataIID)
                    .HasName("PK_SEOMetadatas");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<SeoMetadataCultureData>(entity =>
            {
                entity.HasKey(e => new { e.CultureID, e.SEOMetadataID });

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Culture)
                    .WithMany(p => p.SeoMetadataCultureDatas)
                    .HasForeignKey(d => d.CultureID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SeoMetadataCultureDatas_Cultures");

                entity.HasOne(d => d.SeoMetadata)
                    .WithMany(p => p.SeoMetadataCultureDatas)
                    .HasForeignKey(d => d.SEOMetadataID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SeoMetadataCultureDatas_SeoMetadatas");
            });

            modelBuilder.Entity<Sequence>(entity =>
            {
                entity.Property(e => e.SequenceID).ValueGeneratedNever();
            });

            //modelBuilder.Entity<Service>(entity =>
            //{
            //    entity.Property(e => e.ServiceIID).ValueGeneratedNever();

            //    entity.HasOne(d => d.ExtraTimeType)
            //        .WithMany(p => p.Services)
            //        .HasForeignKey(d => d.ExtraTimeTypeID)
            //        .HasConstraintName("FK_Services_ExtraTimeTypes");

            //    entity.HasOne(d => d.ParentService)
            //        .WithMany(p => p.InverseParentService)
            //        .HasForeignKey(d => d.ParentServiceID)
            //        .HasConstraintName("FK_Services_Services");

            //    entity.HasOne(d => d.PricingType)
            //        .WithMany(p => p.Services)
            //        .HasForeignKey(d => d.PricingTypeID)
            //        .HasConstraintName("FK_Services_PricingTypes");

            //    entity.HasOne(d => d.ServiceAvailable)
            //        .WithMany(p => p.Services)
            //        .HasForeignKey(d => d.ServiceAvailableID)
            //        .HasConstraintName("FK_Services_ServiceAvailables");

            //    entity.HasOne(d => d.ServiceGroup)
            //        .WithMany(p => p.Services)
            //        .HasForeignKey(d => d.ServiceGroupID)
            //        .HasConstraintName("FK_Services_ServiceGroups");

            //    entity.HasOne(d => d.TreatmentType)
            //        .WithMany(p => p.Services)
            //        .HasForeignKey(d => d.TreatmentTypeID)
            //        .HasConstraintName("FK_Services_TreatmentTypes");
            //});

            //modelBuilder.Entity<ServiceAvailable>(entity =>
            //{
            //    entity.Property(e => e.ServiceAvailableID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<ServiceEmployeeMap>(entity =>
            //{
            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.ServiceEmployeeMaps)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_ServiceEmployeeMaps_Employees");

            //    entity.HasOne(d => d.Service)
            //        .WithMany(p => p.ServiceEmployeeMaps)
            //        .HasForeignKey(d => d.ServiceID)
            //        .HasConstraintName("FK_ServiceEmployeeMaps_Services");
            //});

            //modelBuilder.Entity<ServiceGroup>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<ServicePricing>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Service)
            //        .WithMany(p => p.ServicePricings)
            //        .HasForeignKey(d => d.ServiceID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_ServicePricings_Services");
            //});

            modelBuilder.Entity<ServiceProvider>(entity =>
            {
                entity.Property(e => e.ServiceProviderID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.ServiceProviders)
                    .HasForeignKey(d => d.CountryID)
                    .HasConstraintName("FK_ServiceProders_Countries1");
            });

            //modelBuilder.Entity<ServiceProviderCountryGroup>(entity =>
            //{
            //    entity.Property(e => e.CountryGroupID).ValueGeneratedNever();

            //    entity.HasOne(d => d.ServiceProvider)
            //        .WithMany(p => p.ServiceProviderCountryGroups)
            //        .HasForeignKey(d => d.ServiceProviderID)
            //        .HasConstraintName("FK_ServiceProviderCountryGroups_ServiceProviders");

            //    entity.HasMany(d => d.Countries)
            //        .WithMany(p => p.CountryGroups)
            //        .UsingEntity<Dictionary<string, object>>(
            //            "ServiceProviderCountryGroupMap",
            //            l => l.HasOne<Country>().WithMany().HasForeignKey("CountryID").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_ServiceProviderCountryGroupMaps_Countries"),
            //            r => r.HasOne<ServiceProviderCountryGroup>().WithMany().HasForeignKey("CountryGroupID").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_ServiceProviderCountryGroupMaps_ServiceProviderCountryGroupMaps"),
            //            j =>
            //            {
            //                j.HasKey("CountryGroupID", "CountryID");

            //                j.ToTable("ServiceProviderCountryGroupMaps", "distribution");
            //            });
            //});

            modelBuilder.Entity<ServiceProviderLog>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.ServiceProvider)
                //    .WithMany(p => p.ServiceProviderLogs)
                //    .HasForeignKey(d => d.ServiceProviderID)
                //    .HasConstraintName("FK_ServiceProviderLogs_ServiceProviders");
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.HasKey(e => new { e.SettingCode, e.CompanyID })
                    .HasName("PK_TransactionHistoryArchive_TransactionID");

                //entity.HasOne(d => d.Company)
                //    .WithMany(p => p.Settings)
                //    .HasForeignKey(d => d.CompanyID)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Settings_Companies");
            });

            //modelBuilder.Entity<Severity>(entity =>
            //{
            //    entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            modelBuilder.Entity<ShareHolder>(entity =>
            {
                //entity.HasOne(d => d.Customer)
                //    .WithMany(p => p.ShareHolders)
                //    .HasForeignKey(d => d.CustomerID)
                //    .HasConstraintName("FK_ShareHolders_Customers");

                //entity.HasOne(d => d.Login)
                //    .WithMany(p => p.ShareHolders)
                //    .HasForeignKey(d => d.LoginID)
                //    .HasConstraintName("FK_ShareHolders_Logins");
            });

            //modelBuilder.Entity<Shift>(entity =>
            //{
            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.Shifts)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_Shifts_AcademicYear");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.Shifts)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_Shifts_School");
            //});

            //modelBuilder.Entity<ShoppingCart>(entity =>
            //{
            //    //entity.HasKey(e => e.ShoppingCartItemID)
            //    //    .HasName("PK_Catalog.ShoppingCart");

            //    //entity.Property(e => e.Price).IsFixedLength();


            //    entity.HasOne(d => d.TransactionHead)
            //        .WithMany(p => p.ShoppingCarts)
            //        .HasForeignKey(d => d.BlockedHeadID)
            //        .HasConstraintName("FK_ShoppingCarts_TransactionHead");


            //});

            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.ShoppingCart1)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_ShoppingCart_AcademicYear");


                entity.HasOne(d => d.TransactionHead)
                    .WithMany(p => p.ShoppingCarts)
                    .HasForeignKey(d => d.BlockedHeadID)
                    .HasConstraintName("FK_ShoppingCarts_TransactionHead");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.ShoppingCarts)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_ShoppingCart_School");

                //entity.HasOne(d => d.Student)
                //    .WithMany(p => p.ShoppingCart1)
                //    .HasForeignKey(d => d.StudentID)
                //    .HasConstraintName("FK_ShoppingCart_Student");

                //entity.HasOne(d => d.SubscriptionType)
                //    .WithMany(p => p.ShoppingCart1)
                //    .HasForeignKey(d => d.SubscriptionTypeID)
                //    .HasConstraintName("FK_ShoppingCarts_SubscriptionType");
            });

            //modelBuilder.Entity<ShoppingCartActivityLog>(entity =>
            //{
            //    entity.HasOne(d => d.CartActivityStatus)
            //        .WithMany(p => p.ShoppingCartActivityLogs)
            //        .HasForeignKey(d => d.CartActivityStatusID)
            //        .HasConstraintName("FK_ShoppingCartActivityLogs_CartActivityStatuses");

            //    entity.HasOne(d => d.CartActivityType)
            //        .WithMany(p => p.ShoppingCartActivityLogs)
            //        .HasForeignKey(d => d.CartActivityTypeID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_ShoppingCartActivityLogs_CartActivityTypes");

            //    entity.HasOne(d => d.ShoppingCart)
            //        .WithMany(p => p.ShoppingCartActivityLogs)
            //        .HasForeignKey(d => d.ShoppingCartID)
            //        .HasConstraintName("FK_ShoppingCartActivityLogs_ShoppingCarts");

            //    entity.HasOne(d => d.ShoppingCartItem)
            //        .WithMany(p => p.ShoppingCartActivityLogs)
            //        .HasForeignKey(d => d.ShoppingCartItemID)
            //        .HasConstraintName("FK_ShoppingCartActivityLogs_ShoppingCartItems");
            //});

            //modelBuilder.Entity<ShoppingCartActivityLogCultreData>(entity =>
            //{
            //    entity.HasKey(e => new { e.ShoppingCartActivityLogID, e.CultreID });

            //    entity.HasOne(d => d.Cultre)
            //        .WithMany(p => p.ShoppingCartActivityLogCultreDatas)
            //        .HasForeignKey(d => d.CultreID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_ShoppingCartActivityLogCultreDatas_Cultures");

            //    entity.HasOne(d => d.ShoppingCartActivityLog)
            //        .WithMany(p => p.ShoppingCartActivityLogCultreDatas)
            //        .HasForeignKey(d => d.ShoppingCartActivityLogID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_ShoppingCartActivityLogCultreDatas_ShoppingCartActivityLogs");
            //});

            //modelBuilder.Entity<ShoppingCartActivitySKUMap>(entity =>
            //{
            //    entity.HasKey(e => new { e.ShoppingCartActivityLogID, e.ProductSKUMapID });
            //});

            //modelBuilder.Entity<ShoppingCartChargeMap>(entity =>
            //{
            //    entity.HasOne(d => d.CartCharge)
            //        .WithMany(p => p.ShoppingCartChargeMaps)
            //        .HasForeignKey(d => d.CartChargeID)
            //        .HasConstraintName("FK_ShoppingCartChargeMaps_ShoppingCarts1");

            //    entity.HasOne(d => d.CartChargeType)
            //        .WithMany(p => p.ShoppingCartChargeMaps)
            //        .HasForeignKey(d => d.CartChargeTypeID)
            //        .HasConstraintName("FK_ShoppingCartChargeMaps_CartChargeTypes");

            //    entity.HasOne(d => d.ShoppingCart)
            //        .WithMany(p => p.ShoppingCartChargeMaps)
            //        .HasForeignKey(d => d.ShoppingCartID)
            //        .HasConstraintName("FK_ShoppingCartChargeMaps_ShoppingCarts");
            //});

            modelBuilder.Entity<ShoppingCartItem>(entity =>
            {
                entity.HasOne(d => d.ShoppingCart)
                    .WithMany(p => p.ShoppingCartItems)
                    .HasForeignKey(d => d.ShoppingCartID)
                    .HasConstraintName("FK_ShoppingCartItems_ShoppingCarts");
            });

            modelBuilder.Entity<ShoppingCartVoucherMap>(entity =>
            {
                entity.Property(e => e.ShoppingCartVoucherMapIID).ValueGeneratedOnAdd();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.ShoppingCart)
                    .WithMany(p => p.ShoppingCartVoucherMaps)
                    .HasForeignKey(d => d.ShoppingCartID)
                    .HasConstraintName("FK_ShoppingCartVoucherMaps_ShoppingCarts");

                //entity.HasOne(d => d.Status)
                //    .WithMany(p => p.ShoppingCartVoucherMaps)
                //    .HasForeignKey(d => d.StatusID)
                //    .HasConstraintName("FK_ShoppingCartVoucherMaps_Statuses");

                entity.HasOne(d => d.Voucher)
                    .WithMany(p => p.ShoppingCartVoucherMaps)
                    .HasForeignKey(d => d.VoucherID)
                    .HasConstraintName("FK_ShoppingCartVoucherMaps_Vouchers");
            });

            modelBuilder.Entity<ShoppingCartWeekDayMap>(entity =>
            {
                entity.HasOne(d => d.ShoppingCart)
                    .WithMany(p => p.ShoppingCartWeekDayMaps)
                    .HasForeignKey(d => d.ShoppingCartID)
                    .HasConstraintName("FK_ShoppingCartWeekDayMaps_ShoppingCarts");

                entity.HasOne(d => d.Day)
                    .WithMany(p => p.ShoppingCartWeekDayMaps)
                    .HasForeignKey(d => d.WeekDayID)
                    .HasConstraintName("FK_ShoppingCartWeekDayMaps_Days");
            });

            //modelBuilder.Entity<Signup>(entity =>
            //{
            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.Signups)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_Signups_AcademicYear");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.Signups)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_Signups_Class");

            //    entity.HasOne(d => d.OrganizerEmployee)
            //        .WithMany(p => p.Signups)
            //        .HasForeignKey(d => d.OrganizerEmployeeID)
            //        .HasConstraintName("FK_MeetingSlotMaps_Employee");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.Signups)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_Signups_School");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.Signups)
            //        .HasForeignKey(d => d.SectionID)
            //        .HasConstraintName("FK_Signups_Section");

            //    entity.HasOne(d => d.SignupCategory)
            //        .WithMany(p => p.Signups)
            //        .HasForeignKey(d => d.SignupCategoryID)
            //        .HasConstraintName("FK_Signups_Category");

            //    entity.HasOne(d => d.SignupType)
            //        .WithMany(p => p.Signups)
            //        .HasForeignKey(d => d.SignupTypeID)
            //        .HasConstraintName("FK_Signups_SignupType");
            //});

            //modelBuilder.Entity<SignupAudienceMap>(entity =>
            //{
            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.SignupAudienceMaps)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_TeacherSignupAudience_AcademicYear");

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.SignupAudienceMaps)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_SignupAudience_EmployeeIID");

            //    entity.HasOne(d => d.Parent)
            //        .WithMany(p => p.SignupAudienceMaps)
            //        .HasForeignKey(d => d.ParentID)
            //        .HasConstraintName("FK_SignupAudience_Parent");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.SignupAudienceMaps)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_TeacherSignupAudience_School");

            //    entity.HasOne(d => d.Signup)
            //        .WithMany(p => p.SignupAudienceMaps)
            //        .HasForeignKey(d => d.SignupID)
            //        .HasConstraintName("FK_SignupAudience_Signup");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.SignupAudienceMaps)
            //        .HasForeignKey(d => d.StudentID)
            //        .HasConstraintName("FK_SignupAudience_Student");
            //});

            //modelBuilder.Entity<SignupCategory>(entity =>
            //{
            //    entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            //});

            //modelBuilder.Entity<SignupSlotAllocationMap>(entity =>
            //{
            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.SignupSlotAllocationMaps)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_TeacherSignupSlotAllocation_AcademicYear");

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.SignupSlotAllocationMaps)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_SignupSlotAllocation_EmployeeIID");

            //    entity.HasOne(d => d.Parent)
            //        .WithMany(p => p.SignupSlotAllocationMaps)
            //        .HasForeignKey(d => d.ParentID)
            //        .HasConstraintName("FK_SignupSlotAllocation_Parent");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.SignupSlotAllocationMaps)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_TeacherSignupSlotAllocation_School");

            //    entity.HasOne(d => d.SignupSlotMap)
            //        .WithMany(p => p.SignupSlotAllocationMaps)
            //        .HasForeignKey(d => d.SignupSlotMapID)
            //        .HasConstraintName("FK_SignupSlotAllocation_Signup");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.SignupSlotAllocationMaps)
            //        .HasForeignKey(d => d.StudentID)
            //        .HasConstraintName("FK_SignupSlotAllocation_Student");
            //});

            //modelBuilder.Entity<SignupSlotMap>(entity =>
            //{
            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.SignupSlotMaps)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_SignupSlotMaps_AcademicYear");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.SignupSlotMaps)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_SignupSlotMap_School");

            //    entity.HasOne(d => d.Signup)
            //        .WithMany(p => p.SignupSlotMaps)
            //        .HasForeignKey(d => d.SignupID)
            //        .HasConstraintName("FK_SignupSlotMaps_Signups");

            //    entity.HasOne(d => d.SignupSlotType)
            //        .WithMany(p => p.SignupSlotMaps)
            //        .HasForeignKey(d => d.SignupSlotTypeID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_SignupSlotMap_SlotType");
            //});

            //modelBuilder.Entity<SignupSlotType>(entity =>
            //{
            //    entity.Property(e => e.IsActive).HasDefaultValueSql("((0))");
            //});

            //modelBuilder.Entity<SignupType>(entity =>
            //{
            //    entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            //});

            modelBuilder.Entity<Site>(entity =>
            {
                entity.Property(e => e.SiteID).ValueGeneratedNever();

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Sites)
                    .HasForeignKey(d => d.CompanyID)
                    .HasConstraintName("FK_Sites_Companies");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.Sites)
                    .HasForeignKey(d => d.HomePageID)
                    .HasConstraintName("FK_Sites_Pages");

                entity.HasOne(d => d.Page1)
                    .WithMany(p => p.Sites1)
                    .HasForeignKey(d => d.MasterPageID)
                    .HasConstraintName("FK_Sites_Pages1");
            });

            //modelBuilder.Entity<Site1>(entity =>
            //{
            //    entity.Property(e => e.SiteID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            modelBuilder.Entity<SiteCountryMap>(entity =>
            {
                entity.HasOne(d => d.Country)
                    .WithMany(p => p.SiteCountryMaps)
                    .HasForeignKey(d => d.CountryID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SiteCountryMaps_Countries");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.SiteCountryMaps)
                    .HasForeignKey(d => d.SiteID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SiteCountryMaps_Sites");
            });

            //modelBuilder.Entity<SkillGroupMaster>(entity =>
            //{
            //    entity.Property(e => e.SkillGroupMasterID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<SkillGroupSubjectMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.ClassSubjectSkillGroupMap)
            //        .WithMany(p => p.SkillGroupSubjectMaps)
            //        .HasForeignKey(d => d.ClassSubjectSkillGroupMapID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_SkillGroupSkillMaps_SubjectSkillGroupMaps");

            //    entity.HasOne(d => d.MarkGrade)
            //        .WithMany(p => p.SkillGroupSubjectMaps)
            //        .HasForeignKey(d => d.MarkGradeID)
            //        .HasConstraintName("FK_SkillGroupSkillMaps_MarkGrades");

            //    entity.HasOne(d => d.Subject)
            //        .WithMany(p => p.SkillGroupSubjectMaps)
            //        .HasForeignKey(d => d.SubjectID)
            //        .HasConstraintName("FK_SkillGroupSkillMaps_Subjects");
            //});

            //modelBuilder.Entity<SkillMaster>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.SkillGroupMaster)
            //        .WithMany(p => p.SkillMasters)
            //        .HasForeignKey(d => d.SkillGroupMasterID)
            //        .HasConstraintName("FK_SkillMasters_SkillGroups");
            //});


            //modelBuilder.Entity<SocailMediaCampaign>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Campaign)
            //        .WithMany(p => p.SocailMediaCampaigns)
            //        .HasForeignKey(d => d.CampaignID)
            //        .HasConstraintName("FK_SocailMediaCampaigns_Campaigns");
            //});

            //modelBuilder.Entity<SocailMediaCampaignTag>(entity =>
            //{
            //    entity.HasOne(d => d.CampaignTag)
            //        .WithMany(p => p.InverseCampaignTag)
            //        .HasForeignKey(d => d.CampaignTagID)
            //        .HasConstraintName("FK_SocailMediaCampaignTags_SocailMediaCampaignTags1");

            //    entity.HasOne(d => d.SocailMediaCampaign)
            //        .WithMany(p => p.SocailMediaCampaignTags)
            //        .HasForeignKey(d => d.SocailMediaCampaignID)
            //        .HasConstraintName("FK_SocailMediaCampaignTags_SocailMediaCampaigns");
            //});

            //modelBuilder.Entity<SocialMediaPosting>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<SocialMediaPostingMap>(entity =>
            //{
            //    entity.HasOne(d => d.SocialMediaPosting)
            //        .WithMany(p => p.SocialMediaPostingMaps)
            //        .HasForeignKey(d => d.SocialMediaPostingID)
            //        .HasConstraintName("FK_SocialMediaPostingMaps_SocialMediaPostings");
            //});

            //modelBuilder.Entity<SocialService>(entity =>
            //{
            //    entity.HasOne(d => d.Member)
            //        .WithMany(p => p.SocialServices)
            //        .HasForeignKey(d => d.MemberID)
            //        .HasConstraintName("FK_SocialServices_Members");

            //    entity.HasOne(d => d.OccupationType)
            //        .WithMany(p => p.SocialServices)
            //        .HasForeignKey(d => d.OccupationTypeID)
            //        .HasConstraintName("FK_SocialServices_OccupationTypes");
            //});

            //modelBuilder.Entity<Source>(entity =>
            //{
            //    entity.Property(e => e.SourceID).ValueGeneratedNever();
            //});

            modelBuilder.Entity<Sponsor>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            //modelBuilder.Entity<StaffAttendence>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.StaffAttendences)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_StaffAttendences_AcademicYear");

            //    entity.HasOne(d => d.AttendenceReason)
            //        .WithMany(p => p.StaffAttendences)
            //        .HasForeignKey(d => d.AttendenceReasonID)
            //        .HasConstraintName("FK_StaffAttendences_AttendenceReasons");

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.StaffAttendences)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_StaffAttendences_Employees");

            //    entity.HasOne(d => d.PresentStatus)
            //        .WithMany(p => p.StaffAttendences)
            //        .HasForeignKey(d => d.PresentStatusID)
            //        .HasConstraintName("FK_StaffAttendences_StaffPresentStatuses");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.StaffAttendences)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_StaffAttendences_School");
            //});

            //modelBuilder.Entity<StaffLeaveReason>(entity =>
            //{
            //    entity.Property(e => e.StaffLeaveReasonIID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<StaffRouteMonthlySplit>(entity =>
            //{
            //    entity.HasOne(d => d.StaffRouteStopMap)
            //        .WithMany(p => p.StaffRouteMonthlySplits)
            //        .HasForeignKey(d => d.StaffRouteStopMapID)
            //        .HasConstraintName("FK_StaffRouteMonthlySplit_StaffRouteStopMaps");
            //});

            //modelBuilder.Entity<StaffRouteShiftMapLog>(entity =>
            //{
            //    entity.HasKey(e => e.StaffRouteStopMapLogIID)
            //        .HasName("PK_StaffRouteStopMapLogs");

            //    entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.StaffRouteShiftMapLogs)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_StaffRouteStopMapLogs_AcademicYear");

            //    entity.HasOne(d => d.DropStopMap)
            //        .WithMany(p => p.StaffRouteShiftMapLogDropStopMaps)
            //        .HasForeignKey(d => d.DropStopMapID)
            //        .HasConstraintName("FK_StaffRouteStopMapLogs_DropStop");

            //    entity.HasOne(d => d.PickupStopMap)
            //        .WithMany(p => p.StaffRouteShiftMapLogPickupStopMaps)
            //        .HasForeignKey(d => d.PickupStopMapID)
            //        .HasConstraintName("FK_StaffRouteStopMapLogs_PickUpStop");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.StaffRouteShiftMapLogs)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_StaffRouteStopMapLogs_School");

            //    entity.HasOne(d => d.ShiftFromRoute)
            //        .WithMany(p => p.StaffRouteShiftMapLogs)
            //        .HasForeignKey(d => d.ShiftFromRouteID)
            //        .HasConstraintName("FK_StaffRouteStopMapLogs_Main_Route");

            //    entity.HasOne(d => d.Staff)
            //        .WithMany(p => p.StaffRouteShiftMapLogs)
            //        .HasForeignKey(d => d.StaffID)
            //        .HasConstraintName("FK_StaffRouteStopMapLogs_Staff");

            //    entity.HasOne(d => d.StaffRouteStopMap)
            //        .WithMany(p => p.StaffRouteShiftMapLogs)
            //        .HasForeignKey(d => d.StaffRouteStopMapID)
            //        .HasConstraintName("FK_StaffRouteStopMapLogs_StaffRouteStopmaps");

            //    entity.HasOne(d => d.TransportStatus)
            //        .WithMany(p => p.StaffRouteShiftMapLogs)
            //        .HasForeignKey(d => d.TransportStatusID)
            //        .HasConstraintName("FK_StaffRouteStopMapLogs_TransportStatus");
            //});

            //modelBuilder.Entity<StaffRouteStopMap>(entity =>
            //{
            //    entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.StaffRouteStopMaps)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_StaffRouteStopMaps_AcademicYear");

            //    entity.HasOne(d => d.DropStopMap)
            //        .WithMany(p => p.StaffRouteStopMapDropStopMaps)
            //        .HasForeignKey(d => d.DropStopMapID)
            //        .HasConstraintName("FK_StaffRouteStopMaps_RouteStopMaps2");

            //    entity.HasOne(d => d.DropStopRoute)
            //        .WithMany(p => p.StaffRouteStopMapDropStopRoutes)
            //        .HasForeignKey(d => d.DropStopRouteID)
            //        .HasConstraintName("FK_StaffRouteStopMaps_Routes1");

            //    entity.HasOne(d => d.PickupRoute)
            //        .WithMany(p => p.StaffRouteStopMapPickupRoutes)
            //        .HasForeignKey(d => d.PickupRouteID)
            //        .HasConstraintName("FK_StaffRouteStopMaps_Routes");

            //    entity.HasOne(d => d.PickupStopMap)
            //        .WithMany(p => p.StaffRouteStopMapPickupStopMaps)
            //        .HasForeignKey(d => d.PickupStopMapID)
            //        .HasConstraintName("FK_StaffRouteStopMaps_RouteStopMaps1");

            //    entity.HasOne(d => d.RouteStopMap)
            //        .WithMany(p => p.StaffRouteStopMapRouteStopMaps)
            //        .HasForeignKey(d => d.RouteStopMapID)
            //        .HasConstraintName("FK_StaffRouteStopMaps_RouteStopMaps");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.StaffRouteStopMaps)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_StaffRouteStopMaps_School");

            //    entity.HasOne(d => d.Staff)
            //        .WithMany(p => p.StaffRouteStopMaps)
            //        .HasForeignKey(d => d.StaffID)
            //        .HasConstraintName("FK_StaffRouteStopMaps_Employees");

            //    entity.HasOne(d => d.TransportStatus)
            //        .WithMany(p => p.StaffRouteStopMaps)
            //        .HasForeignKey(d => d.TransportStatusID)
            //        .HasConstraintName("FK_StaffRouteStopMaps_TransportStatus");
            //});


            modelBuilder.Entity<StaticContentData>(entity =>
            {
                entity.Property(e => e.ContentDataIID).ValueGeneratedOnAdd();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.StaticContentType)
                    .WithMany()
                    .HasForeignKey(d => d.ContentTypeID)
                    .HasConstraintName("FK_StaticContentDatas_StaticContentTypes");
            });

            modelBuilder.Entity<StaticContentType>(entity =>
            {
                entity.Property(e => e.ContentTypeID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });


            modelBuilder.Entity<StatusesCultureData>(entity =>
            {
                entity.HasKey(e => new { e.CultureID, e.StatusID });

                entity.HasOne(d => d.Culture)
                    .WithMany(p => p.StatusesCultureDatas)
                    .HasForeignKey(d => d.CultureID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StatusesCultureDatas_Cultures");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.StatusesCultureDatas)
                    .HasForeignKey(d => d.CultureID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StatusesCultureDatas_Statuses");
            });

            //modelBuilder.Entity<StockAccountMap>(entity =>
            //{
            //    entity.HasKey(e => e.SAMID)
            //        .HasName("PK_account.StockAccountMaps");
            //});

            modelBuilder.Entity<StockCompareDetail>(entity =>
            {
                entity.Property(e => e.DetailIID).ValueGeneratedOnAdd();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<StockCompareHead>(entity =>
            {
                //entity.Property(e => e.DocumentStatusID)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.Property(e => e.HeadIID).ValueGeneratedOnAdd();
            });

            //modelBuilder.Entity<Stop>(entity =>
            //{
            //    entity.Property(e => e.StopID).ValueGeneratedNever();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.Stops)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_Stops_AcademicYear");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.Stops)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_Stops_School");
            //});

            //modelBuilder.Entity<StopEntryStatus>(entity =>
            //{
            //    entity.Property(e => e.StopEntryStatusID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<Stream>(entity =>
            //{
            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.Streams)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_Streams_AcademicYear");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.Streams)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_Streams_Schools");

            //    entity.HasOne(d => d.StreamGroup)
            //        .WithMany(p => p.Streams)
            //        .HasForeignKey(d => d.StreamGroupID)
            //        .HasConstraintName("FK_Streams_StreamGroups");
            //});

            //modelBuilder.Entity<StreamGroup>(entity =>
            //{
            //    entity.HasKey(e => e.StreamGroupID)
            //        .IsClustered(false);

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.StreamGroups)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_StreamGroups_AcademicYear");
            //});

            //modelBuilder.Entity<StreamOptionalSubjectMap>(entity =>
            //{
            //    entity.HasKey(e => e.StreamOptionalSubjectIID)
            //        .HasName("PK_StreamOptionalSubjectMap");

            //    entity.HasOne(d => d.Stream)
            //        .WithMany(p => p.StreamOptionalSubjectMaps)
            //        .HasForeignKey(d => d.StreamID)
            //        .HasConstraintName("FK_OptionalSubjectMap_Stream");

            //    entity.HasOne(d => d.Subject)
            //        .WithMany(p => p.StreamOptionalSubjectMaps)
            //        .HasForeignKey(d => d.SubjectID)
            //        .HasConstraintName("FK_OptionalSubjectMap_Subject");
            //});

            //modelBuilder.Entity<StreamSubjectMap>(entity =>
            //{
            //    entity.Property(e => e.IsOptionalSubject).HasDefaultValueSql("((0))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.StreamSubjectMaps)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_StreamSubjectMaps_AcademicYear");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.StreamSubjectMaps)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_StreamSubjectMaps_School");

            //    entity.HasOne(d => d.Stream)
            //        .WithMany(p => p.StreamSubjectMaps)
            //        .HasForeignKey(d => d.StreamID)
            //        .HasConstraintName("FK_StreamSubjectMaps_Streams");

            //    entity.HasOne(d => d.Subject)
            //        .WithMany(p => p.StreamSubjectMaps)
            //        .HasForeignKey(d => d.SubjectID)
            //        .HasConstraintName("FK_StreamSubjectMaps_Subjects");
            //});


            //modelBuilder.Entity<Street>(entity =>
            //{
            //    entity.Property(e => e.StreetID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Country)
            //        .WithMany(p => p.Streets)
            //        .HasForeignKey(d => d.CountryID)
            //        .HasConstraintName("FK_Streets_Countries");
            //});

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Status)
                    .HasDefaultValueSql("((1))")
                    .HasComment("1-Active\r\n2-Transferred\r\n3-Discontinue\r\n");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.StudentAcademicYears)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_Students_AcademicYear");

                //entity.HasOne(d => d.Application)
                //    .WithMany(p => p.Students)
                //    .HasForeignKey(d => d.ApplicationID)
                //    .HasConstraintName("FK_Students_StudentApplications");

                //entity.HasOne(d => d.BloodGroup)
                //    .WithMany(p => p.Students)
                //    .HasForeignKey(d => d.BloodGroupID)
                //    .HasConstraintName("FK_Students_BloodGroups");

                //entity.HasOne(d => d.Cast)
                //    .WithMany(p => p.Students)
                //    .HasForeignKey(d => d.CastID)
                //    .HasConstraintName("FK_Students_Casts");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.ClassID)
                    .HasConstraintName("FK_Students_Classes");

                //entity.HasOne(d => d.Community)
                //    .WithMany(p => p.Students)
                //    .HasForeignKey(d => d.CommunityID)
                //    .HasConstraintName("FK_Students_Communitys");

                //entity.HasOne(d => d.CurrentCountry)
                //    .WithMany(p => p.StudentCurrentCountries)
                //    .HasForeignKey(d => d.CurrentCountryID)
                //    .HasConstraintName("FK_Students_Countries");

                //entity.HasOne(d => d.Gender)
                //    .WithMany(p => p.Students)
                //    .HasForeignKey(d => d.GenderID)
                //    .HasConstraintName("FK_Students_Genders");

                //entity.HasOne(d => d.Grade)
                //    .WithMany(p => p.Students)
                //    .HasForeignKey(d => d.GradeID)
                //    .HasConstraintName("FK_Students_Grades");

                //entity.HasOne(d => d.Hostel)
                //    .WithMany(p => p.Students)
                //    .HasForeignKey(d => d.HostelID)
                //    .HasConstraintName("FK_Students_Hostels");

                //entity.HasOne(d => d.HostelRoom)
                //    .WithMany(p => p.Students)
                //    .HasForeignKey(d => d.HostelRoomID)
                //    .HasConstraintName("FK_Students_HostelRooms");

                //entity.HasOne(d => d.Login)
                //    .WithMany(p => p.Students)
                //    .HasForeignKey(d => d.LoginID)
                //    .HasConstraintName("FK_Students_Logins");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.ParentID)
                    .HasConstraintName("FK_Students_Parents");

                //entity.HasOne(d => d.PermenentCountry)
                //    .WithMany(p => p.StudentPermenentCountries)
                //    .HasForeignKey(d => d.PermenentCountryID)
                //    .HasConstraintName("FK_Students_Countries1");

                //entity.HasOne(d => d.PreviousSchoolClassCompleted)
                //    .WithMany(p => p.StudentPreviousSchoolClassCompleteds)
                //    .HasForeignKey(d => d.PreviousSchoolClassCompletedID)
                //    .HasConstraintName("FK_Students_Classes1");

                //entity.HasOne(d => d.PreviousSchoolSyllabus)
                //    .WithMany(p => p.Students)
                //    .HasForeignKey(d => d.PreviousSchoolSyllabusID)
                //    .HasConstraintName("FK_Students_Syllabus");

                //entity.HasOne(d => d.PrimaryContact)
                //    .WithMany(p => p.Students)
                //    .HasForeignKey(d => d.PrimaryContactID)
                //    .HasConstraintName("FK_Students_GuardianTypes");

                //entity.HasOne(d => d.Relegion)
                //    .WithMany(p => p.Students)
                //    .HasForeignKey(d => d.RelegionID)
                //    .HasConstraintName("FK_Students_Relegions");

                //entity.HasOne(d => d.SchoolAcademicyear)
                //    .WithMany(p => p.StudentSchoolAcademicyears)
                //    .HasForeignKey(d => d.SchoolAcademicyearID)
                //    .HasConstraintName("FK_Students_SchoolAcademicYear");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.Students)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_Students_School");

                //entity.HasOne(d => d.SecondLang)
                //    .WithMany(p => p.StudentSecondLangs)
                //    .HasForeignKey(d => d.SecondLangID)
                //    .HasConstraintName("FK_Students_N_SecondLanguage");

                //entity.HasOne(d => d.SecoundLanguage)
                //    .WithMany(p => p.StudentSecoundLanguages)
                //    .HasForeignKey(d => d.SecoundLanguageID)
                //    .HasConstraintName("FK_Students_SecondLang");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.SectionID)
                    .HasConstraintName("FK_Students_Sections");

                //entity.HasOne(d => d.Stream)
                //    .WithMany(p => p.Students)
                //    .HasForeignKey(d => d.StreamID)
                //    .HasConstraintName("FK_Students_Streams");

                //entity.HasOne(d => d.StudentCategory)
                //    .WithMany(p => p.Students)
                //    .HasForeignKey(d => d.StudentCategoryID)
                //    .HasConstraintName("FK_Students_StudentCategories");

                //entity.HasOne(d => d.StudentHouse)
                //    .WithMany(p => p.Students)
                //    .HasForeignKey(d => d.StudentHouseID)
                //    .HasConstraintName("FK_Students_StudentHouses");

                //entity.HasOne(d => d.SubjectMap)
                //    .WithMany(p => p.StudentSubjectMaps)
                //    .HasForeignKey(d => d.SubjectMapID)
                //    .HasConstraintName("FK_Students_SubjectMap");

                //entity.HasOne(d => d.ThirdLang)
                //    .WithMany(p => p.StudentThirdLangs)
                //    .HasForeignKey(d => d.ThirdLangID)
                //    .HasConstraintName("FK_Students_N_ThirdLanguage");

                //entity.HasOne(d => d.ThridLanguage)
                //    .WithMany(p => p.StudentThridLanguages)
                //    .HasForeignKey(d => d.ThridLanguageID)
                //    .HasConstraintName("FK_Students_ThirdLang");
            });

            //modelBuilder.Entity<StudentAchievement>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.StudentAchievements)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_StudentAchievement_AcademicYear");

            //    entity.HasOne(d => d.Category)
            //        .WithMany(p => p.StudentAchievements)
            //        .HasForeignKey(d => d.CategoryID)
            //        .HasConstraintName("FK_StudentAchievement_Category");

            //    entity.HasOne(d => d.Ranking)
            //        .WithMany(p => p.StudentAchievements)
            //        .HasForeignKey(d => d.RankingID)
            //        .HasConstraintName("FK_StudentAchievement_Ranking");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.StudentAchievements)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_StudentAchievement_School");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.StudentAchievements)
            //        .HasForeignKey(d => d.StudentID)
            //        .HasConstraintName("FK_StudentAchievement_Student");

            //    entity.HasOne(d => d.Type)
            //        .WithMany(p => p.StudentAchievements)
            //        .HasForeignKey(d => d.TypeID)
            //        .HasConstraintName("FK_StudentAchievement_Type");
            //});

            modelBuilder.Entity<StudentApplication>(entity =>
            {
                entity.HasKey(e => e.ApplicationIID)
                    .HasName("PK_ApplicationIID");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.ApplicationStatus)
                //    .WithMany(p => p.StudentApplications)
                //    .HasForeignKey(d => d.ApplicationStatusID)
                //    .HasConstraintName("FK_StudentApplications_ApplicationStatuses");

                //entity.HasOne(d => d.ApplicationType)
                //    .WithMany(p => p.StudentApplications)
                //    .HasForeignKey(d => d.ApplicationTypeID)
                //    .HasConstraintName("FK_StudentApplications_SubmitType");

                //entity.HasOne(d => d.CanYouVolunteerToHelpOne)
                //    .WithMany(p => p.StudentApplicationCanYouVolunteerToHelpOnes)
                //    .HasForeignKey(d => d.CanYouVolunteerToHelpOneID)
                //    .HasConstraintName("FK_StudentApplications_VolunteerType");

                //entity.HasOne(d => d.CanYouVolunteerToHelpTwo)
                //    .WithMany(p => p.StudentApplicationCanYouVolunteerToHelpTwoes)
                //    .HasForeignKey(d => d.CanYouVolunteerToHelpTwoID)
                //    .HasConstraintName("FK_StudentApplications_VolunteerType1");

                //entity.HasOne(d => d.Cast)
                //    .WithMany(p => p.StudentApplications)
                //    .HasForeignKey(d => d.CastID)
                //    .HasConstraintName("FK_StudentApplications_Casts");

                //entity.HasOne(d => d.Class)
                //    .WithMany(p => p.StudentApplicationClasses)
                //    .HasForeignKey(d => d.ClassID)
                //    .HasConstraintName("FK_StudentApplications_Classes");

                //entity.HasOne(d => d.Country)
                //    .WithMany(p => p.StudentApplicationCountries)
                //    .HasForeignKey(d => d.CountryID)
                //    .HasConstraintName("FK_StudentApplications_Countries");

                //entity.HasOne(d => d.Curriculam)
                //    .WithMany(p => p.StudentApplicationCurriculams)
                //    .HasForeignKey(d => d.CurriculamID)
                //    .HasConstraintName("FK_StudentApplications_Syllabus");

                //entity.HasOne(d => d.FatherCountry)
                //    .WithMany(p => p.StudentApplicationFatherCountries)
                //    .HasForeignKey(d => d.FatherCountryID)
                //    .HasConstraintName("FK_StudentApplications_FatherNat");

                //entity.HasOne(d => d.FatherPassportDetailNo)
                //    .WithMany(p => p.StudentApplicationFatherPassportDetailNoes)
                //    .HasForeignKey(d => d.FatherPassportDetailNoID)
                //    .HasConstraintName("FK_StudentApplications_PassportDetailMaps");

                //entity.HasOne(d => d.FatherStudentRelationShip)
                //    .WithMany(p => p.StudentApplicationFatherStudentRelationShips)
                //    .HasForeignKey(d => d.FatherStudentRelationShipID)
                //    .HasConstraintName("FK_StudentApplications_GuardianTypes");

                //entity.HasOne(d => d.FatherVisaDetailNo)
                //    .WithMany(p => p.StudentApplicationFatherVisaDetailNoes)
                //    .HasForeignKey(d => d.FatherVisaDetailNoID)
                //    .HasConstraintName("FK_StudentApplications_VisaDetailMaps1");

                //entity.HasOne(d => d.Gender)
                //    .WithMany(p => p.StudentApplications)
                //    .HasForeignKey(d => d.GenderID)
                //    .HasConstraintName("FK_StudentApplications_Genders");

                //entity.HasOne(d => d.GuardianNationality)
                //    .WithMany(p => p.StudentApplicationGuardianNationalities)
                //    .HasForeignKey(d => d.GuardianNationalityID)
                //    .HasConstraintName("FK_Studentapplications_Guradian_Nationality");

                //entity.HasOne(d => d.GuardianPassportDetailNo)
                //    .WithMany(p => p.StudentApplicationGuardianPassportDetailNoes)
                //    .HasForeignKey(d => d.GuardianPassportDetailNoID)
                //    .HasConstraintName("FK_Studentapplications_Guradian_PassportDetail");

                //entity.HasOne(d => d.GuardianStudentRelationShip)
                //    .WithMany(p => p.StudentApplicationGuardianStudentRelationShips)
                //    .HasForeignKey(d => d.GuardianStudentRelationShipID)
                //    .HasConstraintName("FK_Studentapplications_Guradian_RelationShip");

                //entity.HasOne(d => d.GuardianVisaDetailNo)
                //    .WithMany(p => p.StudentApplicationGuardianVisaDetailNoes)
                //    .HasForeignKey(d => d.GuardianVisaDetailNoID)
                //    .HasConstraintName("FK_Studentapplications_Guradian_VisaDetail");

                //entity.HasOne(d => d.Login)
                //    .WithMany(p => p.StudentApplications)
                //    .HasForeignKey(d => d.LoginID)
                //    .HasConstraintName("FK_StudentApplications_Logins");

                //entity.HasOne(d => d.MotherCountry)
                //    .WithMany(p => p.StudentApplicationMotherCountries)
                //    .HasForeignKey(d => d.MotherCountryID)
                //    .HasConstraintName("FK_StudentApplications_MotherNat");

                //entity.HasOne(d => d.MotherPassportDetailNo)
                //    .WithMany(p => p.StudentApplicationMotherPassportDetailNoes)
                //    .HasForeignKey(d => d.MotherPassportDetailNoID)
                //    .HasConstraintName("FK_StudentApplications_PassportDetailMaps1");

                //entity.HasOne(d => d.MotherStudentRelationShip)
                //    .WithMany(p => p.StudentApplicationMotherStudentRelationShips)
                //    .HasForeignKey(d => d.MotherStudentRelationShipID)
                //    .HasConstraintName("FK_StudentApplications_GuardianTypes1");

                //entity.HasOne(d => d.MotherVisaDetailNo)
                //    .WithMany(p => p.StudentApplicationMotherVisaDetailNoes)
                //    .HasForeignKey(d => d.MotherVisaDetailNoID)
                //    .HasConstraintName("FK_StudentApplications_VisaDetailMaps2");

                //entity.HasOne(d => d.Nationality)
                //    .WithMany(p => p.StudentApplicationNationalities)
                //    .HasForeignKey(d => d.NationalityID)
                //    .HasConstraintName("FK_StudentApplications_Nationalities");

                //entity.HasOne(d => d.PreviousSchoolClassCompleted)
                //    .WithMany(p => p.StudentApplicationPreviousSchoolClassCompleteds)
                //    .HasForeignKey(d => d.PreviousSchoolClassCompletedID)
                //    .HasConstraintName("FK_StudentApplications_Classes1");

                //entity.HasOne(d => d.PreviousSchoolSyllabus)
                //    .WithMany(p => p.StudentApplicationPreviousSchoolSyllabus)
                //    .HasForeignKey(d => d.PreviousSchoolSyllabusID)
                //    .HasConstraintName("FK_StudentApplications_StudentApplications");

                //entity.HasOne(d => d.PrimaryContact)
                //    .WithMany(p => p.StudentApplicationPrimaryContacts)
                //    .HasForeignKey(d => d.PrimaryContactID)
                //    .HasConstraintName("FK_StudentApplications_GuardianTypes2");

                //entity.HasOne(d => d.Relegion)
                //    .WithMany(p => p.StudentApplications)
                //    .HasForeignKey(d => d.RelegionID)
                //    .HasConstraintName("FK_StudentApplications_Relegions");

                //entity.HasOne(d => d.SchoolAcademicyear)
                //    .WithMany(p => p.StudentApplications)
                //    .HasForeignKey(d => d.SchoolAcademicyearID)
                //    .HasConstraintName("FK_StudentApplications_AcademicYears1");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.StudentApplications)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_StudentApplications_School");

                //entity.HasOne(d => d.SecondLang)
                //    .WithMany(p => p.StudentApplicationSecondLangs)
                //    .HasForeignKey(d => d.SecondLangID)
                //    .HasConstraintName("FK_StudentApplications_N_SecondLanguage");

                //entity.HasOne(d => d.SecoundLanguage)
                //    .WithMany(p => p.StudentApplicationSecoundLanguages)
                //    .HasForeignKey(d => d.SecoundLanguageID)
                //    .HasConstraintName("FK_StudentApplications_SecondLang");

                //entity.HasOne(d => d.StreamGroup)
                //    .WithMany(p => p.StudentApplications)
                //    .HasForeignKey(d => d.StreamGroupID)
                //    .HasConstraintName("FK_Studentapplications_StreamGroup");

                //entity.HasOne(d => d.Stream)
                //    .WithMany(p => p.StudentApplications)
                //    .HasForeignKey(d => d.StreamID)
                //    .HasConstraintName("FK_Studentapplications_Stream");

                //entity.HasOne(d => d.StudentCategory)
                //    .WithMany(p => p.StudentApplications)
                //    .HasForeignKey(d => d.StudentCategoryID)
                //    .HasConstraintName("FK_StudentApplications_StudentCategories");

                //entity.HasOne(d => d.StudentCoutryOfBrith)
                //    .WithMany(p => p.StudentApplicationStudentCoutryOfBriths)
                //    .HasForeignKey(d => d.StudentCoutryOfBrithID)
                //    .HasConstraintName("FK_StudentApplications_Countries4");

                //entity.HasOne(d => d.StudentPassportDetailNo)
                //    .WithMany(p => p.StudentApplicationStudentPassportDetailNoes)
                //    .HasForeignKey(d => d.StudentPassportDetailNoID)
                //    .HasConstraintName("FK_StudentApplications_PassportDetailMaps2");

                //entity.HasOne(d => d.StudentVisaDetailNo)
                //    .WithMany(p => p.StudentApplicationStudentVisaDetailNoes)
                //    .HasForeignKey(d => d.StudentVisaDetailNoID)
                //    .HasConstraintName("FK_StudentApplications_VisaDetailMaps");

                //entity.HasOne(d => d.ThirdLang)
                //    .WithMany(p => p.StudentApplicationThirdLangs)
                //    .HasForeignKey(d => d.ThirdLangID)
                //    .HasConstraintName("FK_StudentApplications_N_ThirdLanguage");

                //entity.HasOne(d => d.ThridLanguage)
                //    .WithMany(p => p.StudentApplicationThridLanguages)
                //    .HasForeignKey(d => d.ThridLanguageID)
                //    .HasConstraintName("FK_StudentApplications_ThirdLang");
            });

            //modelBuilder.Entity<StudentApplicationDocumentMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Application)
            //        .WithMany(p => p.StudentApplicationDocumentMaps)
            //        .HasForeignKey(d => d.ApplicationID)
            //        .HasConstraintName("FK_StudentApplicationDocumentMaps_DocAttach");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.StudentApplicationDocumentMaps)
            //        .HasForeignKey(d => d.StudentID)
            //        .HasConstraintName("FK_StudentApplicationDocumentMaps_Student");
            //});

            //modelBuilder.Entity<StudentApplicationOptionalSubjectMap>(entity =>
            //{
            //    entity.HasOne(d => d.Application)
            //        .WithMany(p => p.StudentApplicationOptionalSubjectMaps)
            //        .HasForeignKey(d => d.ApplicationID)
            //        .HasConstraintName("FK_StudentApplicationOptionalSubjectMaps_ApplicationID");

            //    entity.HasOne(d => d.Stream)
            //        .WithMany(p => p.StudentApplicationOptionalSubjectMaps)
            //        .HasForeignKey(d => d.StreamID)
            //        .HasConstraintName("FK_StudentApplicationOptionalSubjectMaps_Stream");

            //    entity.HasOne(d => d.Subject)
            //        .WithMany(p => p.StudentApplicationOptionalSubjectMaps)
            //        .HasForeignKey(d => d.SubjectID)
            //        .HasConstraintName("FK_StudentApplicationOptionalSubjectMaps_Subject");
            //});


            //modelBuilder.Entity<StudentApplicationSiblingMap>(entity =>
            //{
            //    entity.HasKey(e => e.StudentApplicationSiblingMapIID)
            //        .HasName("PK_StudentSiblingAppMaps");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Application)
            //        .WithMany(p => p.StudentApplicationSiblingMaps)
            //        .HasForeignKey(d => d.ApplicationID)
            //        .HasConstraintName("FK_StudentApplicationSiblingMaps_StudentApplications");

            //    entity.HasOne(d => d.Sibling)
            //        .WithMany(p => p.StudentApplicationSiblingMaps)
            //        .HasForeignKey(d => d.SiblingID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_StudentApplicationSiblingMaps_Students");
            //});

            modelBuilder.Entity<StudentAssignmentMap>(entity =>
            {
                entity.Property(e => e.StudentAssignmentMapIID).ValueGeneratedNever();

                entity.Property(e => e.TimeStamaps)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.Assignment)
                    .WithMany(p => p.StudentAssignmentMaps)
                    .HasForeignKey(d => d.AssignmentID)
                    .HasConstraintName("FK_StudentAssignmentMaps_Assignments1");

                //entity.HasOne(d => d.AssignmentStatus)
                //    .WithMany(p => p.StudentAssignmentMaps)
                //    .HasForeignKey(d => d.AssignmentStatusID)
                //    .HasConstraintName("FK_StudentAssignmentMaps_AssignmentStatuses1");

                //entity.HasOne(d => d.Student)
                //    .WithMany(p => p.StudentAssignmentMaps)
                //    .HasForeignKey(d => d.StudentId)
                //    .HasConstraintName("FK_StudentAssignmentMaps_Students");
            });


            // modelBuilder.Entity<StudentAttendence>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.StudentAttendences)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_StudentAttendences_AcademicYear");

            //    entity.HasOne(d => d.AttendenceReason)
            //        .WithMany(p => p.StudentAttendences)
            //        .HasForeignKey(d => d.AttendenceReasonID)
            //        .HasConstraintName("FK_StudentAttendences_AttendenceReasons");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.StudentAttendences)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_StudentAttendences_Classes");

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.StudentAttendences)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_StudentAttendence_Employees");

            //    entity.HasOne(d => d.PresentStatus)
            //        .WithMany(p => p.StudentAttendences)
            //        .HasForeignKey(d => d.PresentStatusID)
            //        .HasConstraintName("FK_StudentAttendences_PresentStatuses");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.StudentAttendences)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_StudentAttendences_School");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.StudentAttendences)
            //        .HasForeignKey(d => d.SectionID)
            //        .HasConstraintName("FK_StudentAttendences_Sections");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.StudentAttendences)
            //        .HasForeignKey(d => d.StudentID)
            //        .HasConstraintName("FK_StudentAttendences_Students");
            //});

            //modelBuilder.Entity<StudentCategory>(entity =>
            //{
            //    entity.Property(e => e.StudentCategoryID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<StudentClassHistoryMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.StudentClassHistoryMaps)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_StudentClassHistoryMaps_Classes");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.StudentClassHistoryMaps)
            //        .HasForeignKey(d => d.SectionID)
            //        .HasConstraintName("FK_StudentClassHistoryMaps_Sections");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.StudentClassHistoryMaps)
            //        .HasForeignKey(d => d.StudentID)
            //        .HasConstraintName("FK_StudentClassHistoryMaps_StudentClassHistoryMaps");
            //});        

            //modelBuilder.Entity<StudentFeeConcession>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.StudentFeeConcessions)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_StudentFeeConcessions_AcademicYear");

            //    entity.HasOne(d => d.ConcessionApprovalType)
            //        .WithMany(p => p.StudentFeeConcessions)
            //        .HasForeignKey(d => d.ConcessionApprovalTypeID)
            //        .HasConstraintName("FK_StudentFeeConcessions_ApprovalType");

            //    entity.HasOne(d => d.CreditNoteFeeTypeMap)
            //        .WithMany(p => p.StudentFeeConcessions)
            //        .HasForeignKey(d => d.CreditNoteFeeTypeMapID)
            //        .HasConstraintName("FK_StudFeeCon_CreditNoteFee");

            //    entity.HasOne(d => d.FeeDueFeeTypeMaps)
            //        .WithMany(p => p.StudentFeeConcessions)
            //        .HasForeignKey(d => d.FeeDueFeeTypeMapsID)
            //        .HasConstraintName("FK_StudentFeeConcession_DueFeeType");

            //    entity.HasOne(d => d.FeeMaster)
            //        .WithMany(p => p.StudentFeeConcessions)
            //        .HasForeignKey(d => d.FeeMasterID)
            //        .HasConstraintName("FK_StudentFeeConcessions_FeeMasters");

            //    entity.HasOne(d => d.FeePeriod)
            //        .WithMany(p => p.StudentFeeConcessions)
            //        .HasForeignKey(d => d.FeePeriodID)
            //        .HasConstraintName("FK_StudentFeeConcessions_FeePeriods");

            //    entity.HasOne(d => d.Parent)
            //        .WithMany(p => p.StudentFeeConcessions)
            //        .HasForeignKey(d => d.ParentID)
            //        .HasConstraintName("FK_StudentFeeConcession_Parent");

            //    entity.HasOne(d => d.Staff)
            //        .WithMany(p => p.StudentFeeConcessions)
            //        .HasForeignKey(d => d.StaffID)
            //        .HasConstraintName("FK_StudentFeeConcession_Staff");

            //    entity.HasOne(d => d.StudentFeeDue)
            //        .WithMany(p => p.StudentFeeConcessions)
            //        .HasForeignKey(d => d.StudentFeeDueID)
            //        .HasConstraintName("FK_StudentFeeConcession_Due");

            //    entity.HasOne(d => d.StudentGroup)
            //        .WithMany(p => p.StudentFeeConcessions)
            //        .HasForeignKey(d => d.StudentGroupID)
            //        .HasConstraintName("FK_StudentFeeConcessions_StudentGroup");
            //});

            //modelBuilder.Entity<StudentFeeDue>(entity =>
            //{
            //    entity.HasKey(e => e.StudentFeeDueIID)
            //        .HasName("PK_FeeDueStudentMapIID");

            //    entity.Property(e => e.IsAccountPost).HasDefaultValueSql("((0))");

            //    entity.Property(e => e.IsCancelled).HasDefaultValueSql("((0))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcadamicYear)
            //        .WithMany(p => p.StudentFeeDues)
            //        .HasForeignKey(d => d.AcadamicYearID)
            //        .HasConstraintName("FK_StudentFeeDues_AcademicYears");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.StudentFeeDues)
            //        .HasForeignKey(d => d.ClassId)
            //        .HasConstraintName("FK_StudentFeeDues_Classes");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.StudentFeeDues)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_StudentFeeDues_School");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.StudentFeeDues)
            //        .HasForeignKey(d => d.SectionID)
            //        .HasConstraintName("FK_StudentFeeDues_Sections");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.StudentFeeDues)
            //        .HasForeignKey(d => d.StudentId)
            //        .HasConstraintName("FK_StudentFeeDues_Students");
            //});                    

            //modelBuilder.Entity<StudentGroup>(entity =>
            //{
            //    entity.Property(e => e.StudentGroupID).ValueGeneratedNever();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.StudentGroups)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_StudentGroups_AcademicYear");

            //    entity.HasOne(d => d.GroupType)
            //        .WithMany(p => p.StudentGroups)
            //        .HasForeignKey(d => d.GroupTypeID)
            //        .HasConstraintName("FK_StudentGroups_StudentGroupType");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.StudentGroups)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_StudentGroups_School");
            //});

            //modelBuilder.Entity<StudentGroupFeeMaster>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcadamicYear)
            //        .WithMany(p => p.StudentGroupFeeMasters)
            //        .HasForeignKey(d => d.AcadamicYearID)
            //        .HasConstraintName("FK_StudentGroupFeeMasters_AcademicYears");
            //});

            //modelBuilder.Entity<StudentGroupFeeTypeMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.FeeMaster)
            //        .WithMany(p => p.StudentGroupFeeTypeMaps)
            //        .HasForeignKey(d => d.FeeMasterID)
            //        .HasConstraintName("FK_StudentGroupFeeTypeMaps_FeeMasters");

            //    entity.HasOne(d => d.FeePeriod)
            //        .WithMany(p => p.StudentGroupFeeTypeMaps)
            //        .HasForeignKey(d => d.FeePeriodID)
            //        .HasConstraintName("FK_StudentGroupFeeTypeMaps_FeePeriods");

            //    entity.HasOne(d => d.StudentGroupFeeMaster)
            //        .WithMany(p => p.StudentGroupFeeTypeMaps)
            //        .HasForeignKey(d => d.StudentGroupFeeMasterID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_StudentGroupFeeTypeMaps_StudentGroupFeeMasters");
            //});

            //modelBuilder.Entity<StudentGroupMap>(entity =>
            //{
            //    entity.Property(e => e.IsActive)
            //        .HasDefaultValueSql("((0))")
            //        .HasComment("");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.StudentGroupMaps)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_StudentGroupMaps_AcademicYear");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.StudentGroupMaps)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_StudentGroupMaps_School");

            //    entity.HasOne(d => d.StudentGroup)
            //        .WithMany(p => p.StudentGroupMaps)
            //        .HasForeignKey(d => d.StudentGroupID)
            //        .HasConstraintName("FK_StudentGroupMaps_StudentGroups");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.StudentGroupMaps)
            //        .HasForeignKey(d => d.StudentID)
            //        .HasConstraintName("FK_StudentGroupMaps_Students");
            //});

            //modelBuilder.Entity<StudentGroupType>(entity =>
            //{
            //    entity.Property(e => e.GroupTypeIID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<StudentHous>(entity =>
            //{
            //    entity.Property(e => e.StudentHouseID).ValueGeneratedNever();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.StudentHous)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_StudentHouses_AcademicYear");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.StudentHous)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_StudentHouses_School");
            //});

            //modelBuilder.Entity<StudentLeaveApplication>(entity =>
            //{
            //    entity.Property(e => e.LeaveStatusID).HasDefaultValueSql("((1))");

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.StudentLeaveApplications)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_StudentLeaveApplications_AcademicYear");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.StudentLeaveApplications)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_StudentLeaveApplications_Classes");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.StudentLeaveApplications)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_StudentLeaveApplications_School");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.StudentLeaveApplications)
            //        .HasForeignKey(d => d.StudentID)
            //        .HasConstraintName("FK_StudentLeaveApplications_Students");
            //});


            //modelBuilder.Entity<StudentMiscDetail>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Staff)
            //        .WithMany(p => p.StudentMiscDetails)
            //        .HasForeignKey(d => d.StaffID)
            //        .HasConstraintName("FK_StudentMiscDetails_Employees");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.StudentMiscDetails)
            //        .HasForeignKey(d => d.StudentID)
            //        .HasConstraintName("FK_StudentMiscDetails_Students");
            //});

            //modelBuilder.Entity<StudentPassportDetail>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.CountryofBirth)
            //        .WithMany(p => p.StudentPassportDetailCountryofBirths)
            //        .HasForeignKey(d => d.CountryofBirthID)
            //        .HasConstraintName("FK_StudentPassportDetails_Countries2");

            //    entity.HasOne(d => d.CountryofIssue)
            //        .WithMany(p => p.StudentPassportDetailCountryofIssues)
            //        .HasForeignKey(d => d.CountryofIssueID)
            //        .HasConstraintName("FK_StudentPassportDetails_Countries1");

            //    entity.HasOne(d => d.Nationality)
            //        .WithMany(p => p.StudentPassportDetails)
            //        .HasForeignKey(d => d.NationalityID)
            //        .HasConstraintName("FK_StudentPassportDetails_StudentNat");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.StudentPassportDetails)
            //        .HasForeignKey(d => d.StudentID)
            //        .HasConstraintName("FK_StudentPassportDetails_Students");
            //});

            //modelBuilder.Entity<StudentPickLog>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.StudentPickLogs)
            //        .HasForeignKey(d => d.StudentID)
            //        .HasConstraintName("FK_StudentPickLogs_Student");

            //    entity.HasOne(d => d.StudentPicker)
            //        .WithMany(p => p.StudentPickLogs)
            //        .HasForeignKey(d => d.StudentPickerID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_StudentPickLogs_picker");
            //});

            //modelBuilder.Entity<StudentPicker>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.StudentPickers)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_StudentPickers_AcademicYear");

            //    entity.HasOne(d => d.Parent)
            //        .WithMany(p => p.StudentPickers)
            //        .HasForeignKey(d => d.ParentID)
            //        .HasConstraintName("FK_StudentPickers_Parent");

            //    entity.HasOne(d => d.PickedBy)
            //        .WithMany(p => p.StudentPickers)
            //        .HasForeignKey(d => d.PickedByID)
            //        .HasConstraintName("FK_StudentPickers_PickedBy");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.StudentPickers)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_StudentPickers_School");
            //});

            //modelBuilder.Entity<StudentPickerStudentMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.StudentPickerStudentMaps)
            //        .HasForeignKey(d => d.StudentID)
            //        .HasConstraintName("FK_StudentPickers_Student");

            //    entity.HasOne(d => d.StudentPicker)
            //        .WithMany(p => p.StudentPickerStudentMaps)
            //        .HasForeignKey(d => d.StudentPickerID)
            //        .HasConstraintName("FK_StudentPickers_picker");
            //});

            //modelBuilder.Entity<StudentPickupRequest>(entity =>
            //{
            //    entity.Property(e => e.StudentPickupRequestIID).ValueGeneratedOnAdd();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany()
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_StudentPickupRequests_AcademicYear");

            //    entity.HasOne(d => d.PickedBy)
            //        .WithMany()
            //        .HasForeignKey(d => d.PickedByID)
            //        .HasConstraintName("FK_StudentPickupRequests_PickedBy");

            //    entity.HasOne(d => d.RequestStatus)
            //        .WithMany()
            //        .HasForeignKey(d => d.RequestStatusID)
            //        .HasConstraintName("FK_StudentPickupRequests_StudentPickupRequestStatus");

            //    entity.HasOne(d => d.School)
            //        .WithMany()
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_StudentPickupRequests_School");

            //    entity.HasOne(d => d.Student)
            //        .WithMany()
            //        .HasForeignKey(d => d.StudentID)
            //        .HasConstraintName("FK_StudentPickupRequests_Student");

            //    entity.HasOne(d => d.StudentPicker)
            //        .WithMany()
            //        .HasForeignKey(d => d.StudentPickerID)
            //        .HasConstraintName("FK_StudentPickersreq_picker");
            //});         

            //modelBuilder.Entity<StudentPromotionLog>(entity =>
            //{
            //    entity.Property(e => e.IsPromoted).HasDefaultValueSql("((1))");

            //    entity.Property(e => e.Status).HasDefaultValueSql("((1))");

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.StudentPromotionLogAcademicYears)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_StudentPromotionLogs_AcademicYears1");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.StudentPromotionLogClasses)
            //        .HasForeignKey(d => d.ClassID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_StudentPromotionLogs_Classes");

            //    entity.HasOne(d => d.PromotionStatus)
            //        .WithMany(p => p.StudentPromotionLogs)
            //        .HasForeignKey(d => d.PromotionStatusID)
            //        .HasConstraintName("FK_Promotion_PromotionStatus");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.StudentPromotionLogSchools)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_StudentPromotionLogs_Schools");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.StudentPromotionLogSections)
            //        .HasForeignKey(d => d.SectionID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_StudentPromotionLogs_Sections1");

            //    entity.HasOne(d => d.ShiftFromAcademicYear)
            //        .WithMany(p => p.StudentPromotionLogShiftFromAcademicYears)
            //        .HasForeignKey(d => d.ShiftFromAcademicYearID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_StudentPromotionLogs_AcademicYears");

            //    entity.HasOne(d => d.ShiftFromClass)
            //        .WithMany(p => p.StudentPromotionLogShiftFromClasses)
            //        .HasForeignKey(d => d.ShiftFromClassID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_StudentPromotionLogs_Classes1");

            //    entity.HasOne(d => d.ShiftFromSchool)
            //        .WithMany(p => p.StudentPromotionLogShiftFromSchools)
            //        .HasForeignKey(d => d.ShiftFromSchoolID)
            //        .HasConstraintName("FK_StudentPromotionLogs_FromSchools");

            //    entity.HasOne(d => d.ShiftFromSection)
            //        .WithMany(p => p.StudentPromotionLogShiftFromSections)
            //        .HasForeignKey(d => d.ShiftFromSectionID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_StudentPromotionLogs_Sections");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.StudentPromotionLogs)
            //        .HasForeignKey(d => d.StudentID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_StudentPromotionLogs_Students");
            //});


            //modelBuilder.Entity<StudentRouteMonthlySplit>(entity =>
            //{
            //    entity.HasOne(d => d.FeePeriod)
            //        .WithMany(p => p.StudentRouteMonthlySplits)
            //        .HasForeignKey(d => d.FeePeriodID)
            //        .HasConstraintName("FK_StudentRouteMonthlySplit_FeePeriods");

            //    entity.HasOne(d => d.StudentRouteStopMap)
            //        .WithMany(p => p.StudentRouteMonthlySplits)
            //        .HasForeignKey(d => d.StudentRouteStopMapID)
            //        .HasConstraintName("FK_StudentRouteMonthlySplit_StudentRouteStopMaps");
            //});

            //modelBuilder.Entity<StudentRoutePeriodMap>(entity =>
            //{
            //    entity.HasKey(e => e.StudentRoutePeriodMapIID)
            //        .HasName("PK_StudentRoutePeriodMap");

            //    entity.HasOne(d => d.FeePeriod)
            //        .WithMany(p => p.StudentRoutePeriodMaps)
            //        .HasForeignKey(d => d.FeePeriodID)
            //        .HasConstraintName("FK_StudentRoutePeriodMap_FeePeriods");

            //    entity.HasOne(d => d.StudentRouteStopMap)
            //        .WithMany(p => p.StudentRoutePeriodMaps)
            //        .HasForeignKey(d => d.StudentRouteStopMapID)
            //        .HasConstraintName("FK_StudeRouteStoPeriodpMaps_RouteStopMap");
            //});

            modelBuilder.Entity<StudentRouteStopMap>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                //entity.Property(e => e.IsFromPromotion).HasDefaultValueSql("((0))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.StudentRouteStopMaps)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_StudentRouteStopMaps_AcademicYear");

                //entity.HasOne(d => d.Class)
                //    .WithMany(p => p.StudentRouteStopMaps)
                //    .HasForeignKey(d => d.ClassID)
                //    .HasConstraintName("FK_StudentRouteStopMaps_Class");

                entity.HasOne(d => d.RouteStopMap2)
                    .WithMany(p => p.StudentRouteStopMaps2)
                    .HasForeignKey(d => d.DropStopMapID)
                    .HasConstraintName("FK_StudentRouteStopMaps_RouteStopMaps2");

                entity.HasOne(d => d.Routes1)
                    .WithMany(p => p.StudentRouteStopMaps)
                    .HasForeignKey(d => d.DropStopRouteID)
                    .HasConstraintName("FK_StudentRouteStopMaps_DropRoutes");

                //entity.HasOne(d => d.FeePeriod)
                //    .WithMany(p => p.StudentRouteStopMaps)
                //    .HasForeignKey(d => d.FeePeriodID)
                //    .HasConstraintName("FK_StudentRouteStopMap_FeePeriods");

                entity.HasOne(d => d.Routes11)
                    .WithMany(p => p.StudentRouteStopMaps1)
                    .HasForeignKey(d => d.PickupRouteID)
                    .HasConstraintName("FK_StudentRouteStopMaps_PickUpRoutes");

                entity.HasOne(d => d.RouteStopMap1)
                    .WithMany(p => p.StudentRouteStopMaps1)
                    .HasForeignKey(d => d.PickupStopMapID)
                    .HasConstraintName("FK_StudentRouteStopMaps_RouteStopMaps1");

                entity.HasOne(d => d.RouteStopMap)
                    .WithMany(p => p.StudentRouteStopMaps)
                    .HasForeignKey(d => d.RouteStopMapID)
                    .HasConstraintName("FK_StudentRouteStopMaps_RouteStopMaps");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.StudentRouteStopMaps)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_StudentRouteStopMaps_School");

                //entity.HasOne(d => d.Section)
                //    .WithMany(p => p.StudentRouteStopMaps)
                //    .HasForeignKey(d => d.SectionID)
                //    .HasConstraintName("FK_StudentRouteStopMaps_Sections");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentRouteStopMaps)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_StudentRouteStopMaps_Students");

                //entity.HasOne(d => d.TransportStatus)
                //    .WithMany(p => p.StudentRouteStopMaps)
                //    .HasForeignKey(d => d.TransportStatusID)
                //    .HasConstraintName("FK_StudentRouteStopMaps_TransportStatus");
            });

            //modelBuilder.Entity<StudentRouteStopMapLog>(entity =>
            //{
            //    entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.StudentRouteStopMapLogs)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_StudentRouteStopMapLogs_AcademicYear");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.StudentRouteStopMapLogs)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_StudentRouteStopMapLogs_Class");

            //    entity.HasOne(d => d.DropStopMap)
            //        .WithMany(p => p.StudentRouteStopMapLogDropStopMaps)
            //        .HasForeignKey(d => d.DropStopMapID)
            //        .HasConstraintName("FK_StudentRouteStopMapLogs_RouteStopMaps2");

            //    entity.HasOne(d => d.DropStopRoute)
            //        .WithMany(p => p.StudentRouteStopMapLogDropStopRoutes)
            //        .HasForeignKey(d => d.DropStopRouteID)
            //        .HasConstraintName("FK_StudentRouteStopMapLogs_DropRoutes");

            //    entity.HasOne(d => d.PickupRoute)
            //        .WithMany(p => p.StudentRouteStopMapLogPickupRoutes)
            //        .HasForeignKey(d => d.PickupRouteID)
            //        .HasConstraintName("FK_StudentRouteStopMapLogs_PickUpRoutes");

            //    entity.HasOne(d => d.PickupStopMap)
            //        .WithMany(p => p.StudentRouteStopMapLogPickupStopMaps)
            //        .HasForeignKey(d => d.PickupStopMapID)
            //        .HasConstraintName("FK_StudentRouteStopMapLogs_RouteStopMaps1");

            //    entity.HasOne(d => d.Route)
            //        .WithMany(p => p.StudentRouteStopMapLogRoutes)
            //        .HasForeignKey(d => d.RouteID)
            //        .HasConstraintName("FK_StudentRouteStopMapLogs_Main_Route");

            //    entity.HasOne(d => d.RouteStopMap)
            //        .WithMany(p => p.StudentRouteStopMapLogRouteStopMaps)
            //        .HasForeignKey(d => d.RouteStopMapID)
            //        .HasConstraintName("FK_StudentRouteStopMapLogs_RouteStopMaps");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.StudentRouteStopMapLogs)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_StudentRouteStopMapLogs_School");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.StudentRouteStopMapLogs)
            //        .HasForeignKey(d => d.SectionID)
            //        .HasConstraintName("FK_StudentRouteStopMapLogs_Sections");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.StudentRouteStopMapLogs)
            //        .HasForeignKey(d => d.StudentID)
            //        .HasConstraintName("FK_StudentRouteStopMapLogs_Students");

            //    entity.HasOne(d => d.StudentRouteStopMap)
            //        .WithMany(p => p.StudentRouteStopMapLogs)
            //        .HasForeignKey(d => d.StudentRouteStopMapID)
            //        .HasConstraintName("FK_StudentRouteStopMapLogs_StudRouteStopmaps");

            //    entity.HasOne(d => d.TransportStatus)
            //        .WithMany(p => p.StudentRouteStopMapLogs)
            //        .HasForeignKey(d => d.TransportStatusID)
            //        .HasConstraintName("FK_StudentRouteStopMapLogs_TransportStatus");
            //});          

            //modelBuilder.Entity<StudentSiblingMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Parent)
            //        .WithMany(p => p.StudentSiblingMaps)
            //        .HasForeignKey(d => d.ParentID)
            //        .HasConstraintName("FK_StudentSiblingMap_Parent");

            //    entity.HasOne(d => d.Sibling)
            //        .WithMany(p => p.StudentSiblingMapSiblings)
            //        .HasForeignKey(d => d.SiblingID)
            //        .HasConstraintName("FK_StudentSiblingMaps_Students1");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.StudentSiblingMapStudents)
            //        .HasForeignKey(d => d.StudentID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_StudentSiblingMaps_Students");
            //});           

            //modelBuilder.Entity<StudentSkillGroupMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.MarksGradeMap)
            //        .WithMany(p => p.StudentSkillGroupMaps)
            //        .HasForeignKey(d => d.MarksGradeMapID)
            //        .HasConstraintName("FK_StudentSkillGroupMaps_MarkGradeMaps");

            //    entity.HasOne(d => d.SkillGroupMaster)
            //        .WithMany(p => p.StudentSkillGroupMaps)
            //        .HasForeignKey(d => d.SkillGroupMasterID)
            //        .HasConstraintName("FK_StudentSkillGroupMaps_SkillGroupMasters");

            //    entity.HasOne(d => d.StudentSkillRegister)
            //        .WithMany(p => p.StudentSkillGroupMaps)
            //        .HasForeignKey(d => d.StudentSkillRegisterID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_StudentSkillGroupMaps_StudentSkillRegisters");
            //});

            //modelBuilder.Entity<StudentSkillMasterMap>(entity =>
            //{
            //    entity.HasKey(e => e.StudentSkillMasterMapIID)
            //        .HasName("PK_StudentSkillMasterMap");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.MarksGradeMap)
            //        .WithMany(p => p.StudentSkillMasterMaps)
            //        .HasForeignKey(d => d.MarksGradeMapID)
            //        .HasConstraintName("FK_StudentSkillMasterMaps_MarkGradeMaps");

            //    entity.HasOne(d => d.SkillGroupMaster)
            //        .WithMany(p => p.StudentSkillMasterMaps)
            //        .HasForeignKey(d => d.SkillGroupMasterID)
            //        .HasConstraintName("FK_StudentSkillMasterMaps_SkillGroupMasters");

            //    entity.HasOne(d => d.SkillMaster)
            //        .WithMany(p => p.StudentSkillMasterMaps)
            //        .HasForeignKey(d => d.SkillMasterID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_StudentSkillMasterMaps_SkillMasters");

            //    entity.HasOne(d => d.StudentSkillGroupMaps)
            //        .WithMany(p => p.StudentSkillMasterMaps)
            //        .HasForeignKey(d => d.StudentSkillGroupMapsID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_StudentSkillMasterMaps_StudentSkillGroupMaps");
            //});

            //modelBuilder.Entity<StudentSkillRegister>(entity =>
            //{
            //    entity.HasKey(e => e.StudentSkillRegisterIID)
            //        .HasName("PK_StudentSkillRegister");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.StudentSkillRegisters)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_StudentSkillRegisters_Classes");

            //    entity.HasOne(d => d.Exam)
            //        .WithMany(p => p.StudentSkillRegisters)
            //        .HasForeignKey(d => d.ExamID)
            //        .HasConstraintName("FK_StudentSkillRegisters_Exams");

            //    //entity.HasOne(d => d.MarksGradeMap)
            //    //    .WithMany(p => p.StudentSkillRegisters)
            //    //    .HasForeignKey(d => d.MarksGradeMapID)
            //    //    .HasConstraintName("FK_StudentSkillRegisters_MarkGradeMaps");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.StudentSkillRegisters)
            //        .HasForeignKey(d => d.SectionID)
            //        .HasConstraintName("FK_StudentSkillRegisters_Sections");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.StudentSkillRegisters)
            //        .HasForeignKey(d => d.StudentId)
            //        .HasConstraintName("FK_StudentSkillRegisters_Students");

            //    entity.HasOne(d => d.Subject)
            //        .WithMany(p => p.StudentSkillRegisters)
            //        .HasForeignKey(d => d.SubjectID)
            //        .HasConstraintName("FK_StudentSkillRegisters_Subjects");
            //});

            //modelBuilder.Entity<StudentStaffMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Staff)
            //        .WithMany(p => p.StudentStaffMaps)
            //        .HasForeignKey(d => d.StaffID)
            //        .HasConstraintName("FK_StudentStaffMap_Staff");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.StudentStaffMaps)
            //        .HasForeignKey(d => d.StudentID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_StudentStaffMap_Students");
            //});

            //modelBuilder.Entity<StudentStatusHistory>(entity =>
            //{
            //    entity.HasKey(e => e.SSH_ID)
            //        .HasName("PK__StudentS__13B8461C05AAC944");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<StudentStreamOptionalSubjectMap>(entity =>
            //{
            //    entity.HasKey(e => e.StudentStreamOptionalSubjectMapIID)
            //        .HasName("PK_StreamOptionalSubjectMaps");

            //    entity.HasOne(d => d.Stream)
            //        .WithMany(p => p.StudentStreamOptionalSubjectMaps)
            //        .HasForeignKey(d => d.StreamID)
            //        .HasConstraintName("FK_StudentOptionalSubjectMap_Stream");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.StudentStreamOptionalSubjectMaps)
            //        .HasForeignKey(d => d.StudentID)
            //        .HasConstraintName("FK_StudentOptionalSubjectMap_StudentID");

            //    entity.HasOne(d => d.Subject)
            //        .WithMany(p => p.StudentStreamOptionalSubjectMaps)
            //        .HasForeignKey(d => d.SubjectID)
            //        .HasConstraintName("FK_StudentOptionalSubjectMap_Subject");
            //});

            modelBuilder.Entity<StudentTransferRequest>(entity =>
            {
                entity.Property(e => e.StudentTransferRequestIID).ValueGeneratedNever();

                entity.Property(e => e.IsMailSent).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsTransferRequested).HasDefaultValueSql("((1))");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.StudentTransferRequests)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_studenttransferRequests_AcademicYear");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.StudentTransferRequests)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_studenttransferRequests_School");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentTransferRequests)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_StudentTransferRequest_Students");

                //entity.HasOne(d => d.TransferRequestReason)
                //    .WithMany(p => p.StudentTransferRequests)
                //    .HasForeignKey(d => d.TransferRequestReasonID)
                //    .HasConstraintName("FK_StudentTransferRequests_StudentTransferRequests");

                //entity.HasOne(d => d.TransferRequestStatus)
                //    .WithMany(p => p.StudentTransferRequests)
                //    .HasForeignKey(d => d.TransferRequestStatusID)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_StudentTransferRequests_TransferRequestStatuses");
            });

            //modelBuilder.Entity<StudentTransferRequestReason>(entity =>
            //{
            //    entity.HasKey(e => e.TransferRequestReasonIID)
            //        .HasName("PK_TCReasonss");

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.StudentTransferRequestReasons)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_StudentTransferRequestReasons_AcademicYear");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.StudentTransferRequestReasons)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_StudentTransferRequestReasons_School");
            //});


            //modelBuilder.Entity<StudentVehicleAssign>(entity =>
            //{
            //    entity.Property(e => e.StudentAssignId).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Route)
            //        .WithMany(p => p.StudentVehicleAssigns)
            //        .HasForeignKey(d => d.RouteId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_StudentVehicleAssign_Routes");

            //    entity.HasOne(d => d.Vehicle)
            //        .WithMany(p => p.StudentVehicleAssigns)
            //        .HasForeignKey(d => d.VehicleId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_StudentVehicleAssign_Vehicles");
            //});            

            //modelBuilder.Entity<Subject>(entity =>
            //{
            //    entity.Property(e => e.SubjectID).ValueGeneratedNever();

            //    entity.Property(e => e.IsLanguage).HasDefaultValueSql("((0))");

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.Subjects)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_Subjects_AcademicYear");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.Subjects)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_Subjects_School");

            //    entity.HasOne(d => d.SubjectType)
            //        .WithMany(p => p.Subjects)
            //        .HasForeignKey(d => d.SubjectTypeID)
            //        .HasConstraintName("FK_Subjects_SubjectTypes");
            //});

            //modelBuilder.Entity<SubjectGroupSubjectMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.SubjectGroupSubjectMaps)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_SubjectGroupSubjectMaps_AcademicYear");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.SubjectGroupSubjectMaps)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_SubjectGroupSubjectMaps_Classes");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.SubjectGroupSubjectMaps)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_SubjectGroupSubjectMaps_School");

            //    entity.HasOne(d => d.SubjectGroup)
            //        .WithMany(p => p.SubjectGroupSubjectMaps)
            //        .HasForeignKey(d => d.SubjectGroupID)
            //        .HasConstraintName("FK_SubjectGroupSubjectMaps_SubjectGroups");

            //    entity.HasOne(d => d.Subject)
            //        .WithMany(p => p.SubjectGroupSubjectMaps)
            //        .HasForeignKey(d => d.SubjectID)
            //        .HasConstraintName("FK_SubjectGroupSubjectMaps_Subjects");
            //});

            //modelBuilder.Entity<SubjectInchargerClassMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.SubjectInchargerClassMaps)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_SubjectInchargerClassMaps_AcademicYear");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.SubjectInchargerClassMaps)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_SubjectInchargerClassMaps_Class");

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.SubjectInchargerClassMaps)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_SubjectInchargerClassMaps_Employees");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.SubjectInchargerClassMaps)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_SubjectInchargerClassMaps_School");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.SubjectInchargerClassMaps)
            //        .HasForeignKey(d => d.SectionID)
            //        .HasConstraintName("FK_SubjectInchargerClassMaps_Sections");

            //    entity.HasOne(d => d.Subject)
            //        .WithMany(p => p.SubjectInchargerClassMaps)
            //        .HasForeignKey(d => d.SubjectID)
            //        .HasConstraintName("FK_SubjectInchargerClassMaps_Subjects");
            //});


            //modelBuilder.Entity<SubjectTeacherMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.SubjectTeacherMaps)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_SubjectTeacherMaps_AcademicYear");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.SubjectTeacherMaps)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_SubjectTeacherMaps_Classes");

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.SubjectTeacherMaps)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_SubjectTeacherMaps_Employees");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.SubjectTeacherMaps)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_SubjectTeacherMaps_School");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.SubjectTeacherMaps)
            //        .HasForeignKey(d => d.SectionID)
            //        .HasConstraintName("FK_SubjectTeacherMaps_Sections");

            //    entity.HasOne(d => d.Subject)
            //        .WithMany(p => p.SubjectTeacherMaps)
            //        .HasForeignKey(d => d.SubjectID)
            //        .HasConstraintName("FK_SubjectTeacherMaps_Subjects");
            //});


            //modelBuilder.Entity<SubjectTopic>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.SubjectTopics)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_SubjectTopics_Classes");

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.SubjectTopics)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_SubjectTopics_Employees");

            //    entity.HasOne(d => d.ParentTopic)
            //        .WithMany(p => p.InverseParentTopic)
            //        .HasForeignKey(d => d.ParentTopicID)
            //        .HasConstraintName("FK_SubjectTopics_SubjectTopics");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.SubjectTopics)
            //        .HasForeignKey(d => d.SectionID)
            //        .HasConstraintName("FK_SubjectTopics_Sections");

            //    entity.HasOne(d => d.Subject)
            //        .WithMany(p => p.SubjectTopics)
            //        .HasForeignKey(d => d.SubjectID)
            //        .HasConstraintName("FK_SubjectTopics_Subjects");
            //});


            modelBuilder.Entity<Subscription>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<SubscriptionType>(entity =>
            {
                entity.Property(e => e.SubscriptionTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.BlockedBranchID)
                    .HasConstraintName("FK_Suppliers_Branches");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.CompanyID)
                    .HasConstraintName("FK_Suppliers_CompanyID");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_Suppliers_Employees");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.LoginID)
                    .HasConstraintName("FK_Suppliers_Logins");

                entity.HasOne(d => d.ReceivingMethod)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.ReceivingMethodID)
                    .HasConstraintName("FK_Suppliers_ReceivingMethods");

                entity.HasOne(d => d.ReturnMethod)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.ReturnMethodID)
                    .HasConstraintName("FK_Suppliers_ReturnMethods");

                entity.HasOne(d => d.SupplierStatus)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.StatusID)
                    .HasConstraintName("FK_Suppliers_SupplierStatuses");

                entity.HasOne(d => d.BusinessType)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.BusinessTypeID)
                    .HasConstraintName("FK_Supplier_BusinessType");

                entity.HasOne(d => d.TaxJurisdictionCountry)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.TaxJurisdictionCountryID)
                    .HasConstraintName("FK_Supplier_TaxJurisdictionCountry");
            });

            modelBuilder.Entity<SupplierAccountMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.SupplierAccountMaps)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_SupplierAccountMaps_Accounts");

                entity.HasOne(d => d.EntityTypeEntitlement)
                    .WithMany(p => p.SupplierAccountMaps)
                    .HasForeignKey(d => d.EntitlementID)
                    .HasConstraintName("FK_SupplierAccountMaps_EntityTypeEntitlements");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.SupplierAccountMaps)
                    .HasForeignKey(d => d.SupplierID)
                    .HasConstraintName("FK_SupplierAccountMaps_Suppliers");
            });


            //modelBuilder.Entity<Syllabu>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.Syllabus)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_Syllabus_AcademicYear");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.Syllabus)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_Syllabus_School");
            //});

            modelBuilder.Entity<SyncEntity>(entity =>
            {
                entity.HasKey(e => e.EntityID)
                    .HasName("PK_Entities");

                entity.Property(e => e.EntityID).ValueGeneratedNever();
            });

            modelBuilder.Entity<SyncFieldMap>(entity =>
            {
                entity.Property(e => e.SyncFieldMapID).ValueGeneratedNever();

                entity.HasOne(d => d.SyncFieldMapType)
                    .WithMany(p => p.SyncFieldMaps)
                    .HasForeignKey(d => d.SynchFieldMapTypeID)
                    .HasConstraintName("FK_SyncFieldMaps_SyncFieldMapTypes");
            });

            modelBuilder.Entity<SyncFieldMapType>(entity =>
            {
                entity.HasKey(e => e.SynchFieldMapTypeID)
                    .HasName("PK_SynchFieldMapTypes");

                entity.Property(e => e.SynchFieldMapTypeID).ValueGeneratedNever();
            });


            //modelBuilder.Entity<Task>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.TaskPrioity)
            //        .WithMany(p => p.Tasks)
            //        .HasForeignKey(d => d.TaskPrioityID)
            //        .HasConstraintName("FK_Tasks_TaskPrioities");

            //    entity.HasOne(d => d.TaskStatus)
            //        .WithMany(p => p.Tasks)
            //        .HasForeignKey(d => d.TaskStatusID)
            //        .HasConstraintName("FK_Tasks_TaskStatuses");

            //    entity.HasOne(d => d.TaskType)
            //        .WithMany(p => p.Tasks)
            //        .HasForeignKey(d => d.TaskTypeID)
            //        .HasConstraintName("FK_Tasks_TaskTypes");
            //});

            //modelBuilder.Entity<TaskAssingner>(entity =>
            //{
            //    entity.HasOne(d => d.AssingedToLogin)
            //        .WithMany(p => p.TaskAssingners)
            //        .HasForeignKey(d => d.AssingedToLoginID)
            //        .HasConstraintName("FK_TaskAssingners_Logins");

            //    entity.HasOne(d => d.Task)
            //        .WithMany(p => p.TaskAssingners)
            //        .HasForeignKey(d => d.TaskID)
            //        .HasConstraintName("FK_TaskAssingners_TaskAssingners");
            //});

            modelBuilder.Entity<Tax>(entity =>
            {
                entity.Property(e => e.TaxID).ValueGeneratedNever();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Taxes)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_Taxes_Accounts");

                entity.HasOne(d => d.TaxStatus)
                    .WithMany(p => p.Taxes)
                    .HasForeignKey(d => d.TaxStatusID)
                    .HasConstraintName("FK_Taxes_TaxStatuses");

                entity.HasOne(d => d.TaxType)
                    .WithMany(p => p.Taxes)
                    .HasForeignKey(d => d.TaxTypeID)
                    .HasConstraintName("FK_Taxes_TaxTypes");
            });

            modelBuilder.Entity<TaxStatus>(entity =>
            {
                entity.Property(e => e.TaxStatusID).ValueGeneratedNever();
            });

            modelBuilder.Entity<TaxTemplate>(entity =>
            {
                entity.Property(e => e.TaxTemplateID).ValueGeneratedNever();
            });

            modelBuilder.Entity<TaxTemplateItem>(entity =>
            {
                entity.Property(e => e.TaxTemplateItemID).ValueGeneratedNever();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.TaxTemplateItems)
                    .HasForeignKey(d => d.AccountID)
                    .HasConstraintName("FK_TaxTemplateItems_Accounts");

                entity.HasOne(d => d.TaxTemplate)
                    .WithMany(p => p.TaxTemplateItems)
                    .HasForeignKey(d => d.TaxTemplateID)
                    .HasConstraintName("FK_TaxTemplateItems_TaxTemplates");

                entity.HasOne(d => d.TaxType)
                    .WithMany(p => p.TaxTemplateItems)
                    .HasForeignKey(d => d.TaxTypeID)
                    .HasConstraintName("FK_TaxTemplateItems_TaxTypes");
            });

            modelBuilder.Entity<TaxTransaction>(entity =>
            {
                entity.HasOne(d => d.Account)
                    .WithMany(p => p.TaxTransactions)
                    .HasForeignKey(d => d.AccoundID)
                    .HasConstraintName("FK_TaxTransactions_Accounts");

                entity.HasOne(d => d.TransactionHead)
                    .WithMany(p => p.TaxTransactions)
                    .HasForeignKey(d => d.HeadID)
                    .HasConstraintName("FK_TaxTransactions_TransactionHead");

                entity.HasOne(d => d.TaxTemplate)
                    .WithMany(p => p.TaxTransactions)
                    .HasForeignKey(d => d.TaxTemplateID)
                    .HasConstraintName("FK_TaxTransactions_TaxTemplates");

                entity.HasOne(d => d.TaxTemplateItem)
                    .WithMany(p => p.TaxTransactions)
                    .HasForeignKey(d => d.TaxTemplateItemID)
                    .HasConstraintName("FK_TaxTransactions_TaxTemplateItems");

                entity.HasOne(d => d.TaxType)
                    .WithMany(p => p.TaxTransactions)
                    .HasForeignKey(d => d.TaxTypeID)
                    .HasConstraintName("FK_TaxTransactions_TaxTypes");
            });

            modelBuilder.Entity<TaxType>(entity =>
            {
                entity.Property(e => e.TaxTypeID).ValueGeneratedNever();
            });

            //modelBuilder.Entity<TeacherActivity>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.TeacherActivities)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_TeacherActivities_Classes");

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.TeacherActivities)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_TeacherActivities_Employees");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.TeacherActivities)
            //        .HasForeignKey(d => d.SectionID)
            //        .HasConstraintName("FK_TeacherActivities_Sections");

            //    entity.HasOne(d => d.Shift)
            //        .WithMany(p => p.TeacherActivities)
            //        .HasForeignKey(d => d.ShiftID)
            //        .HasConstraintName("FK_TeacherActivities_Shifts");

            //    entity.HasOne(d => d.SubTopic)
            //        .WithMany(p => p.TeacherActivitySubTopics)
            //        .HasForeignKey(d => d.SubTopicID)
            //        .HasConstraintName("FK_TeacherActivities_SubjectTopics1");

            //    entity.HasOne(d => d.Subject)
            //        .WithMany(p => p.TeacherActivities)
            //        .HasForeignKey(d => d.SubjectID)
            //        .HasConstraintName("FK_TeacherActivities_Subjects");

            //    entity.HasOne(d => d.Topic)
            //        .WithMany(p => p.TeacherActivityTopics)
            //        .HasForeignKey(d => d.TopicID)
            //        .HasConstraintName("FK_TeacherActivities_SubjectTopics");
            //});

            //modelBuilder.Entity<TeachingAid>(entity =>
            //{
            //    entity.Property(e => e.TeachingAidID).ValueGeneratedOnAdd();
            //});

            //modelBuilder.Entity<TempClaimSetClaimMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            //modelBuilder.Entity<TenderType>(entity =>
            //{
            //    entity.Property(e => e.TenderTypeID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            modelBuilder.Entity<Ticket>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.SupportAction)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.ActionID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tickets_SupportActions");

                entity.HasOne(d => d.Employee2)
                    .WithMany(p => p.Tickets2)
                    .HasForeignKey(d => d.AssingedEmployeeID)
                    .HasConstraintName("FK_Tickets_Employees2");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.CustomerID)
                    .HasConstraintName("FK_Tickets_Customers");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.DocumentTypeID)
                    .HasConstraintName("FK_Tickets_DocumentTypes");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_Tickets_Employees");

                entity.HasOne(d => d.Employee1)
                    .WithMany(p => p.Tickets1)
                    .HasForeignKey(d => d.ManagerEmployeeID)
                    .HasConstraintName("FK_Tickets_Employees1");

                //entity.HasOne(d => d.ReferenceTicket)
                //    .WithMany(p => p.InverseReferenceTicket)
                //    .HasForeignKey(d => d.ReferenceTicketID)
                //    .HasConstraintName("FK_Tickets_Tickets");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.SupplierID)
                    .HasConstraintName("FK_Tickets_Suppliers");

                //entity.HasOne(d => d.TicketProcessingStatus)
                //    .WithMany(p => p.Tickets)
                //    .HasForeignKey(d => d.TicketProcessingStatusID)
                //    .HasConstraintName("FK_Tickets_TicketProcessingStatuses");

                entity.HasOne(d => d.TicketStatus)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.TicketStatusID)
                    .HasConstraintName("FK_Tickets_TicketStatuses");
            });

            modelBuilder.Entity<TicketActionDetailDetailMap>(entity =>
            {
                entity.Property(e => e.TicketActionDetailDetailMapIID).ValueGeneratedNever();

                entity.Property(e => e.Timestamps)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.TicketActionDetailMap)
                    .WithMany(p => p.TicketActionDetailDetailMaps)
                    .HasForeignKey(d => d.TicketActionDetailMapID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketActionDetailDetailMaps_TicketActionDetailMaps");
            });

            modelBuilder.Entity<TicketActionDetailMap>(entity =>
            {
                entity.Property(e => e.Timestamps)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.TicketActionDetailMaps)
                    .HasForeignKey(d => d.TicketID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketActionDetailMaps_Tickets");
            });

            //modelBuilder.Entity<TicketProcessingStatus>(entity =>
            //{
            //    entity.HasKey(e => e.TicketProcessingStatusIID)
            //        .HasName("PK_TicketClaimStatuses");

            //    entity.Property(e => e.TicketProcessingStatusIID).ValueGeneratedOnAdd();
            //});

            modelBuilder.Entity<TicketProductMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TicketProductMaps)
                    .HasForeignKey(d => d.ProductID)
                    .HasConstraintName("FK_TicketProductMaps_Products");

                entity.HasOne(d => d.ProductSKUMap)
                    .WithMany(p => p.TicketProductMaps)
                    .HasForeignKey(d => d.ProductSKUMapID)
                    .HasConstraintName("FK_TicketProductMaps_ProductSKUMaps");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.TicketProductMaps)
                    .HasForeignKey(d => d.TicketID)
                    .HasConstraintName("FK_TicketProductMaps_Tickets");
            });

            modelBuilder.Entity<TicketReason>(entity =>
            {
                entity.Property(e => e.TicketReasonID).ValueGeneratedNever();
            });


            modelBuilder.Entity<TicketTag>(entity =>
            {
                entity.Property(e => e.TicketTagsID).ValueGeneratedNever();
            });

            modelBuilder.Entity<TimeSlotMaster>(entity =>
            {
                entity.HasKey(e => e.TimeSlotID)
                    .HasName("PK_cms.TimeSlotMaster");
            });

            modelBuilder.Entity<TimeSlotOverRider>(entity =>
            {
                entity.HasKey(e => e.OverrideID)
                    .HasName("PK_cms.TimeSlotOverRider");
            });

            //modelBuilder.Entity<TimeTable>(entity =>
            //{
            //    entity.Property(e => e.TimeTableID).ValueGeneratedNever();

            //    entity.Property(e => e.IsActive).HasComment("1-Active, 2-Inactive");

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.TimeTables)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_TimeTables_TimeTables");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.TimeTables)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_TimeTables_School");
            //});

            //modelBuilder.Entity<TimeTableAllocation>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.TimeTableAllocations)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_TimeTableAllocations_AcademicYear");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.TimeTableAllocations)
            //        .HasForeignKey(d => d.ClassId)
            //        .HasConstraintName("FK_TimeTableAllocations_Classes");

            //    entity.HasOne(d => d.ClassTiming)
            //        .WithMany(p => p.TimeTableAllocations)
            //        .HasForeignKey(d => d.ClassTimingID)
            //        .HasConstraintName("FK_TimeTableAllocations_ClassTimings");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.TimeTableAllocations)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_TimeTableAllocations_School");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.TimeTableAllocations)
            //        .HasForeignKey(d => d.SectionID)
            //        .HasConstraintName("FK_TimeTableAllocations_Sections");

            //    entity.HasOne(d => d.Staff)
            //        .WithMany(p => p.TimeTableAllocations)
            //        .HasForeignKey(d => d.StaffID)
            //        .HasConstraintName("FK_TimeTableAllocations_Employees");

            //    entity.HasOne(d => d.Subject)
            //        .WithMany(p => p.TimeTableAllocations)
            //        .HasForeignKey(d => d.SubjectID)
            //        .HasConstraintName("FK_TimeTableAllocations_Subjects");

            //    entity.HasOne(d => d.TimeTable)
            //        .WithMany(p => p.TimeTableAllocations)
            //        .HasForeignKey(d => d.TimeTableID)
            //        .HasConstraintName("FK_TimeTableAllocations_TimeTables");

            //    entity.HasOne(d => d.WeekDay)
            //        .WithMany(p => p.TimeTableAllocations)
            //        .HasForeignKey(d => d.WeekDayID)
            //        .HasConstraintName("FK_TimeTableAllocations_WeekDays");
            //});

            //modelBuilder.Entity<TimeTableLog>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.TimeTableLogs)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_TimeTableLogs_AcademicYear");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.TimeTableLogs)
            //        .HasForeignKey(d => d.ClassId)
            //        .HasConstraintName("FK_TimeTableLogs_Classes");

            //    entity.HasOne(d => d.ClassTiming)
            //        .WithMany(p => p.TimeTableLogs)
            //        .HasForeignKey(d => d.ClassTimingID)
            //        .HasConstraintName("FK_TimeTableLogs_ClassTimings");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.TimeTableLogs)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_TimeTableLogs_School");

            //    entity.HasOne(d => d.Section)
            //        .WithMany(p => p.TimeTableLogs)
            //        .HasForeignKey(d => d.SectionID)
            //        .HasConstraintName("FK_TimeTableLogs_Sections");

            //    entity.HasOne(d => d.Staff)
            //        .WithMany(p => p.TimeTableLogs)
            //        .HasForeignKey(d => d.StaffID)
            //        .HasConstraintName("FK_TimeTableLogs_Employees");

            //    entity.HasOne(d => d.Subject)
            //        .WithMany(p => p.TimeTableLogs)
            //        .HasForeignKey(d => d.SubjectID)
            //        .HasConstraintName("FK_TimeTableLogs_Subjects");

            //    entity.HasOne(d => d.TimeTable)
            //        .WithMany(p => p.TimeTableLogs)
            //        .HasForeignKey(d => d.TimeTableID)
            //        .HasConstraintName("FK_TimeTableLogs_TimeTables");

            //    entity.HasOne(d => d.WeekDay)
            //        .WithMany(p => p.TimeTableLogs)
            //        .HasForeignKey(d => d.WeekDayID)
            //        .HasConstraintName("FK_TimeTableLogs_WeekDays");
            //});


            //modelBuilder.Entity<TimesheetEntryLog>(entity =>
            //{
            //    entity.Property(e => e.TimesheetEntryLogID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.Employee)
            //        .WithMany(p => p.TimesheetEntryLogs)
            //        .HasForeignKey(d => d.EmployeeID)
            //        .HasConstraintName("FK_TimesheetEntryLogs_Employees");

            //    entity.HasOne(d => d.EntryType)
            //        .WithMany(p => p.TimesheetEntryLogs)
            //        .HasForeignKey(d => d.EntryTypeID)
            //        .HasConstraintName("FK_TimesheetEntryLogs_EntryType");

            //    entity.HasOne(d => d.LogInType)
            //        .WithMany(p => p.TimesheetEntryLogs)
            //        .HasForeignKey(d => d.LogInTypeID)
            //        .HasConstraintName("FK_TimesheetEntryLogs_LogInTypes");
            //});

            //modelBuilder.Entity<TimesheetEntryType>(entity =>
            //{
            //    entity.Property(e => e.EntryTypeID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<TimesheetLogInType>(entity =>
            //{
            //    entity.HasKey(e => e.LogInTypeID)
            //        .HasName("PK_TimesheetLogInType");

            //    entity.Property(e => e.LogInTypeID).ValueGeneratedNever();
            //});

            modelBuilder.Entity<Title>(entity =>
            {
                entity.Property(e => e.TitleID).ValueGeneratedNever();
            });

            modelBuilder.Entity<TrackerStatus>(entity =>
            {
                entity.Property(e => e.TrackerStatusID).ValueGeneratedNever();
            });


            modelBuilder.Entity<TransactionAllocation>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.TransactionDetail)
                    .WithMany(p => p.TransactionAllocations)
                    .HasForeignKey(d => d.TrasactionDetailID)
                    .HasConstraintName("FK_TransactionAllocations_TransactionDetails");
            });

            modelBuilder.Entity<TransactionDetail>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.CostCenter)
                //    .WithMany(p => p.TransactionDetails)
                //    .HasForeignKey(d => d.CostCenterID)
                //    .HasConstraintName("FK_TransactionDetails_CostCenters");

                entity.HasOne(d => d.TransactionHead)
                    .WithMany(p => p.TransactionDetails)
                    .HasForeignKey(d => d.HeadID)
                    .HasConstraintName("FK_TransactionDetails_TransactionHead");

                entity.HasOne(d => d.TransactionDetail1)
                    .WithMany(p => p.TransactionDetails1)
                    .HasForeignKey(d => d.ParentDetailID)
                    .HasConstraintName("FK_TransactionDetails_TransactionDetails");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TransactionDetails)
                    .HasForeignKey(d => d.ProductID)
                    .HasConstraintName("FK_TransactionDetails_Products");

                entity.HasOne(d => d.ProductSKUMap)
                    .WithMany(p => p.TransactionDetails)
                    .HasForeignKey(d => d.ProductSKUMapID)
                    .HasConstraintName("FK_TransactionDetails_ProductSKUMaps");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.TransactionDetails)
                    .HasForeignKey(d => d.UnitID)
                    .HasConstraintName("FK_TransactionDetails_Units");
            });


            modelBuilder.Entity<TransactionHead>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.TransactionHeads)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_TransactionHead_AcademicYear");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.TransactionHeads)
                    .HasForeignKey(d => d.BranchID)
                    .HasConstraintName("FK_TransactionHead_Branches");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.TransactionHeads)
                    .HasForeignKey(d => d.CompanyID)
                    .HasConstraintName("FK_TransactionHead_Companies");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.TransactionHeads)
                    .HasForeignKey(d => d.CurrencyID)
                    .HasConstraintName("FK_TransactionHead_Currencies");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TransactionHeads)
                    .HasForeignKey(d => d.CustomerID)
                    .HasConstraintName("FK_TransactionHead_Customers");

                entity.HasOne(d => d.DeliveryType)
                    .WithMany(p => p.TransactionHeads)
                    .HasForeignKey(d => d.DeliveryMethodID)
                    .HasConstraintName("FK_TransactionHead_DeliveryTypes");

                entity.HasOne(d => d.DeliveryTypes1)
                    .WithMany(p => p.TransactionHeads)
                    .HasForeignKey(d => d.DeliveryTypeID)
                    .HasConstraintName("FK_TransactionHead_DeliveryTypeID");

                entity.HasOne(d => d.DocumentReferenceStatusMap)
                    .WithMany(p => p.TransactionHeads)
                    .HasForeignKey(d => d.DocumentStatusID)
                    .HasConstraintName("FK_TransactionHead_DocumentReferenceStatusMap");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.TransactionHeads)
                    .HasForeignKey(d => d.DocumentTypeID)
                    .HasConstraintName("FK_TransactionHead_DocumentTypes");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TransactionHeads)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_TransactionHead_Employees");

                entity.HasOne(d => d.EntityTypeEntitlement)
                    .WithMany(p => p.TransactionHeads)
                    .HasForeignKey(d => d.EntitlementID)
                    .HasConstraintName("FK_TransactionHead_EntityTypeEntitlements");

                entity.HasOne(d => d.ReceivingMethod)
                    .WithMany(p => p.TransactionHeads)
                    .HasForeignKey(d => d.ReceivingMethodID)
                    .HasConstraintName("FK_TransactionHead_ReceivingMethods");

                entity.HasOne(d => d.TransactionHead2)
                    .WithMany(p => p.TransactionHead1)
                    .HasForeignKey(d => d.ReferenceHeadID)
                    .HasConstraintName("FK_TransactionHead_TransactionHead");

                entity.HasOne(d => d.ReturnMethod)
                    .WithMany(p => p.TransactionHeads)
                    .HasForeignKey(d => d.ReturnMethodID)
                    .HasConstraintName("FK_TransactionHead_ReturnMethods");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.TransactionHeads)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_TransactionHead_School");

                entity.HasOne(d => d.Employee1)
                    .WithMany(p => p.TransactionHeads1)
                    .HasForeignKey(d => d.StaffID)
                    .HasConstraintName("FK_TransactionHead_Staff");

                //entity.HasOne(d => d.Student)
                //    .WithMany(p => p.TransactionHeads)
                //    .HasForeignKey(d => d.StudentID)
                //    .HasConstraintName("FK_TransactionHead_Students");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.TransactionHeads)
                    .HasForeignKey(d => d.SupplierID)
                    .HasConstraintName("FK_TransactionHead_Suppliers");

                entity.HasOne(d => d.Branch1)
                    .WithMany(p => p.TransactionHeads1)
                    .HasForeignKey(d => d.ToBranchID)
                    .HasConstraintName("FK_TransactionHead_Branches1");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TransactionHeads)
                    .HasForeignKey(d => d.TransactionRole)
                    .HasConstraintName("FK_TransactionHead_Roles");

                entity.HasOne(d => d.TransactionStatus)
                    .WithMany(p => p.TransactionHeads)
                    .HasForeignKey(d => d.TransactionStatusID)
                    .HasConstraintName("FK_TransactionHead_TransactionStatuses");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.TransactionHeads)
                    .HasForeignKey(d => d.DepartmentID)
                    .HasConstraintName("FK_TransactionHead_Department");

                entity.HasOne(d => d.ApprovedByNavigation)
                    .WithMany(p => p.TransactionHeadApprovedByNavigations)
                    .HasForeignKey(d => d.ApprovedBy)
                    .HasConstraintName("FK_TransactionHead_ApprovedBy");

                entity.HasOne(d => d.Bid)
                    .WithMany(p => p.TransactionHeads)
                    .HasForeignKey(d => d.BidID)
                    .HasConstraintName("FK_TransactionHead_Bid");

            });


            modelBuilder.Entity<TransactionHeadAccountMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.AccountTransaction)
                    .WithMany(p => p.TransactionHeadAccountMaps)
                    .HasForeignKey(d => d.AccountTransactionID)
                    .HasConstraintName("FK_TransactionHeadAccountMaps_AccountTransactions");

                entity.HasOne(d => d.TransactionHead)
                    .WithMany(p => p.TransactionHeadAccountMaps)
                    .HasForeignKey(d => d.TransactionHeadID)
                    .HasConstraintName("FK_TransactionHeadAccountMaps_TransactionHeadAccountMaps");
            });

            //modelBuilder.Entity<TransactionHeadChargeMap>(entity =>
            //{
            //    entity.HasOne(d => d.CartCharge)
            //        .WithMany(p => p.TransactionHeadChargeMaps)
            //        .HasForeignKey(d => d.CartChargeID)
            //        .HasConstraintName("FK_TransactionHeadChargeMaps_CartChargeTypes1");

            //    entity.HasOne(d => d.CartChargeType)
            //        .WithMany(p => p.TransactionHeadChargeMaps)
            //        .HasForeignKey(d => d.CartChargeTypeID)
            //        .HasConstraintName("FK_TransactionHeadChargeMaps_CartChargeTypes");

            //    entity.HasOne(d => d.Head)
            //        .WithMany(p => p.TransactionHeadChargeMaps)
            //        .HasForeignKey(d => d.HeadID)
            //        .HasConstraintName("FK_TransactionHeadChargeMaps_TransactionHead");
            //});

            modelBuilder.Entity<TransactionHeadEntitlementMap>(entity =>
            {
                entity.HasOne(d => d.EntityTypeEntitlement)
                    .WithMany(p => p.TransactionHeadEntitlementMaps)
                    .HasForeignKey(d => d.EntitlementID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransactionHeadEntitlementMap_EntityTypeEntitlements");

                entity.HasOne(d => d.TransactionHead)
                    .WithMany(p => p.TransactionHeadEntitlementMaps)
                    .HasForeignKey(d => d.TransactionHeadID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransactionHeadEntitlementMap_TransactionHead");
            });


            modelBuilder.Entity<TransactionHeadPayablesMap>(entity =>
            {
                entity.HasOne(d => d.TransactionHead)
                    .WithMany(p => p.TransactionHeadPayablesMaps)
                    .HasForeignKey(d => d.HeadID)
                    .HasConstraintName("FK_TransactionHeadPayablesMaps_TransactionHead");
            });


            modelBuilder.Entity<TransactionHeadPointsMap>(entity =>
            {
                entity.HasOne(d => d.TransactionHead)
                    .WithMany(p => p.TransactionHeadPointsMaps)
                    .HasForeignKey(d => d.TransactionHeadID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransactionHeadPointsMap_TransactionHead");
            });

            modelBuilder.Entity<TransactionHeadReceivablesMap>(entity =>
            {
                entity.HasOne(d => d.TransactionHead)
                    .WithMany(p => p.TransactionHeadReceivablesMaps)
                    .HasForeignKey(d => d.HeadID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransactionHeadReceivablesMaps_TransactionHead");

                entity.HasOne(d => d.Receivable)
                    .WithMany(p => p.TransactionHeadReceivablesMaps)
                    .HasForeignKey(d => d.ReceivableID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransactionHeadReceivablesMaps_Receivables");
            });

            modelBuilder.Entity<TransactionHeadShoppingCartMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.ShoppingCart)
                    .WithMany(p => p.TransactionHeadShoppingCartMaps)
                    .HasForeignKey(d => d.ShoppingCartID)
                    .HasConstraintName("FK_TransactionHeadShoppingCartMaps_ShoppingCarts");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TransactionHeadShoppingCartMaps)
                    .HasForeignKey(d => d.StatusID)
                    .HasConstraintName("FK_TransactionHeadShoppingCartMaps_Statuses");

                entity.HasOne(d => d.TransactionHead)
                    .WithMany(p => p.TransactionHeadShoppingCartMaps)
                    .HasForeignKey(d => d.TransactionHeadID)
                    .HasConstraintName("FK_TransactionHeadShoppingCartMaps_TransactionHead");
            });

            modelBuilder.Entity<TransactionShipment>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.TransactionShipments)
                    .HasForeignKey(d => d.SupplierIDFrom)
                    .HasConstraintName("FK_TransactionShipments_Suppliers");

                entity.HasOne(d => d.Supplier1)
                    .WithMany(p => p.TransactionShipments1)
                    .HasForeignKey(d => d.SupplierIDTo)
                    .HasConstraintName("FK_TransactionShipments_Suppliers1");

                entity.HasOne(d => d.TransactionHead)
                    .WithMany(p => p.TransactionShipments)
                    .HasForeignKey(d => d.TransactionHeadID)
                    .HasConstraintName("FK_TransactionShipments_TransactionHead");
            });

            //modelBuilder.Entity<TransportApplctnStudentMap>(entity =>
            //{
            //    entity.HasKey(e => e.TransportApplctnStudentMapIID)
            //        .HasName("PK_TransportApplctnStudentMapIID");

            //    entity.Property(e => e.IsActive).HasDefaultValueSql("((0))");

            //    entity.Property(e => e.IsNewRider).HasDefaultValueSql("((1))");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.TransportApplctnStudentMaps)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_TransportApplctnStudentMaps_AcademicYears");

            //    entity.HasOne(d => d.Class)
            //        .WithMany(p => p.TransportApplctnStudentMaps)
            //        .HasForeignKey(d => d.ClassID)
            //        .HasConstraintName("FK_TransportApplicationstud_Classes1");

            //    entity.HasOne(d => d.Gender)
            //        .WithMany(p => p.TransportApplctnStudentMaps)
            //        .HasForeignKey(d => d.GenderID)
            //        .HasConstraintName("FK_TransportApplicationstud_Genders");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.TransportApplctnStudentMaps)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_TransportApplctnStudentMaps_Schools");

            //    entity.HasOne(d => d.Student)
            //        .WithMany(p => p.TransportApplctnStudentMaps)
            //        .HasForeignKey(d => d.StudentID)
            //        .HasConstraintName("FK_TransportApplicationstud_Students");

            //    entity.HasOne(d => d.TransportApplcnStatus)
            //        .WithMany(p => p.TransportApplctnStudentMaps)
            //        .HasForeignKey(d => d.TransportApplcnStatusID)
            //        .HasConstraintName("FK_StudentApplicStud_TransportApplcnStatus");

            //    entity.HasOne(d => d.TransportApplication)
            //        .WithMany(p => p.TransportApplctnStudentMaps)
            //        .HasForeignKey(d => d.TransportApplicationID)
            //        .HasConstraintName("FK_TransportApplctnStudentMaps_TransportApplications");
            //});

            //modelBuilder.Entity<TransportApplication>(entity =>
            //{
            //    entity.HasKey(e => e.TransportApplicationIID)
            //        .HasName("PK_TransportApplicationIID");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.TransportApplications)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_TransportApplications_AcademicYears1");

            //    entity.HasOne(d => d.DropStopMap)
            //        .WithMany(p => p.TransportApplicationDropStopMaps)
            //        .HasForeignKey(d => d.DropStopMapID)
            //        .HasConstraintName("FK_TransportApplications_DropStopMap");

            //    entity.HasOne(d => d.Parent)
            //        .WithMany(p => p.TransportApplications)
            //        .HasForeignKey(d => d.ParentID)
            //        .HasConstraintName("FK_TransportApplications_Parents");

            //    entity.HasOne(d => d.PickupStopMap)
            //        .WithMany(p => p.TransportApplicationPickupStopMaps)
            //        .HasForeignKey(d => d.PickupStopMapID)
            //        .HasConstraintName("FK_TransportApplications_PickUpStop");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.TransportApplications)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_TransportApplications_Schools");

            //    entity.HasOne(d => d.Street)
            //        .WithMany(p => p.TransportApplications)
            //        .HasForeignKey(d => d.StreetID)
            //        .HasConstraintName("FK_StudentAppli_Street");
            //});


            //modelBuilder.Entity<TreatmentGroup>(entity =>
            //{
            //    entity.Property(e => e.TreatmentGroupID).ValueGeneratedNever();
            //});

            //modelBuilder.Entity<TreatmentType>(entity =>
            //{
            //    entity.Property(e => e.TreatmentTypeID).ValueGeneratedNever();

            //    entity.HasOne(d => d.TreatmentGroup)
            //        .WithMany(p => p.TreatmentTypes)
            //        .HasForeignKey(d => d.TreatmentGroupID)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_TreatmentTypes_TreatmentGroups");
            //});

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.Property(e => e.UnitID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.UnitGroup)
                //    .WithMany(p => p.Units)
                //    .HasForeignKey(d => d.UnitGroupID)
                //    .HasConstraintName("FK_Units_UnitGroup");
            });

            modelBuilder.Entity<UnitGroup>(entity =>
            {
                entity.Property(e => e.UnitGroupID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });


            //modelBuilder.Entity<UploadDocument>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.ContentType)
            //        .WithMany(p => p.UploadDocuments)
            //        .HasForeignKey(d => d.ContentTypeID)
            //        .HasConstraintName("FK_UploadDocument_ContentTypes");

            //    entity.HasOne(d => d.UploadDocumentType)
            //        .WithMany(p => p.UploadDocuments)
            //        .HasForeignKey(d => d.UploadDocumentTypeID)
            //        .HasConstraintName("FK_UploadDocuments_UploadDocumentTypes");
            //});

            modelBuilder.Entity<UserDataFormatMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.DataFormat)
                    .WithMany(p => p.UserDataFormatMaps)
                    .HasForeignKey(d => d.DataFormatID)
                    .HasConstraintName("FK_UserDataFormatMaps_DataFormats");

                entity.HasOne(d => d.DataFormatType)
                    .WithMany(p => p.UserDataFormatMaps)
                    .HasForeignKey(d => d.DataFormatTypeID)
                    .HasConstraintName("FK_UserDataFormatMaps_DataFormatTypes");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.UserDataFormatMaps)
                    .HasForeignKey(d => d.LoginID)
                    .HasConstraintName("FK_UserDataFormatMaps_Logins");
            });

            modelBuilder.Entity<UserDeviceMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.UserDeviceMaps)
                    .HasForeignKey(d => d.LoginID)
                    .HasConstraintName("FK_UserDeviceMaps_Logins");
            });


            modelBuilder.Entity<UserJobApplication>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            //modelBuilder.Entity<UserReference>(entity =>
            //{
            //    entity.Property(e => e.UserID).ValueGeneratedNever();

            //    entity.HasOne(d => d.App)
            //        .WithMany(p => p.UserReferences)
            //        .HasForeignKey(d => d.AppID)
            //        .HasConstraintName("FK_UserReferences_Applications");
            //});

            modelBuilder.Entity<UserScreenFieldSetting>(entity =>
            {
                entity.Property(e => e.UserScreenFieldSettingID).ValueGeneratedNever();

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.UserScreenFieldSettings)
                    .HasForeignKey(d => d.LoginID)
                    .HasConstraintName("FK_UserScreenFieldSettings_Logins");

                entity.HasOne(d => d.ScreenField)
                    .WithMany(p => p.UserScreenFieldSettings)
                    .HasForeignKey(d => d.ScreenFieldID)
                    .HasConstraintName("FK_UserScreenFieldSettings_ScreenFields");

                entity.HasOne(d => d.ScreenMetadata)
                    .WithMany(p => p.UserScreenFieldSettings)
                    .HasForeignKey(d => d.ScreenID)
                    .HasConstraintName("FK_UserScreenFieldSettings_ScreenMetadatas");
            });

            modelBuilder.Entity<UserSetting>(entity =>
            {
                entity.HasKey(e => new { e.LoginID, e.SettingCode, e.CompanyID });

                //entity.HasOne(d => d.Company)
                //    .WithMany(p => p.UserSettings)
                //    .HasForeignKey(d => d.CompanyID)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_UserSettings_Companies");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.UserSettings)
                    .HasForeignKey(d => d.LoginID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserSettings_Logins");
            });

            modelBuilder.Entity<UserView>(entity =>
            {
                entity.HasKey(e => e.UserViewIID)
                    .HasName("PK_SearchUserViews");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.UserViews)
                    .HasForeignKey(d => d.LoginID)
                    .HasConstraintName("FK_UserViews_UserViews");

                entity.HasOne(d => d.View)
                    .WithMany(p => p.UserViews)
                    .HasForeignKey(d => d.ViewID)
                    .HasConstraintName("FK_SearchUserViews_SearchViews");
            });

            modelBuilder.Entity<UserViewColumnMap>(entity =>
            {
                entity.HasKey(e => e.UserViewColumnMapIID)
                    .HasName("PK_UserViewColumnMap");

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.UserView)
                    .WithMany(p => p.UserViewColumnMaps)
                    .HasForeignKey(d => d.UserViewID)
                    .HasConstraintName("FK_UserViewColumnMap_UserViews");
            });


            //modelBuilder.Entity<VariableSetting>(entity =>
            //{
            //    entity.Property(e => e.VariableSettingID).ValueGeneratedNever();

            //    entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            //});

            modelBuilder.Entity<Vehicle>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.AcademicYear)
                //    .WithMany(p => p.Vehicles)
                //    .HasForeignKey(d => d.AcademicYearID)
                //    .HasConstraintName("FK_Vehicles_AcademicYear");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.RigistrationCityID)
                    .HasConstraintName("FK_Vehicles_Cities");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Vehicle)
                    .HasForeignKey(d => d.RigistrationCountryID)
                    .HasConstraintName("FK_Vehicles_Countries");

                //entity.HasOne(d => d.School)
                //    .WithMany(p => p.Vehicles)
                //    .HasForeignKey(d => d.SchoolID)
                //    .HasConstraintName("FK_Vehicles_School");

                //entity.HasOne(d => d.Transmission)
                //    .WithMany(p => p.Vehicles)
                //    .HasForeignKey(d => d.TransmissionID)
                //    .HasConstraintName("FK_Vehicles_VehicleTransmissions");

                entity.HasOne(d => d.VehicleOwnershipType)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.VehicleOwnershipTypeID)
                    .HasConstraintName("FK_Vehicles_VehicleOwnershipTypes");

                entity.HasOne(d => d.VehicleType)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.VehicleTypeID)
                    .HasConstraintName("FK_Vehicles_VehicleTypes");
            });

            //modelBuilder.Entity<VehicleDetailMap>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.AcademicYear)
            //        .WithMany(p => p.VehicleDetailMaps)
            //        .HasForeignKey(d => d.AcademicYearID)
            //        .HasConstraintName("FK_VehicleDetailMaps_AcademicYear");

            //    entity.HasOne(d => d.School)
            //        .WithMany(p => p.VehicleDetailMaps)
            //        .HasForeignKey(d => d.SchoolID)
            //        .HasConstraintName("FK_VehicleDetailMaps_School");

            //    entity.HasOne(d => d.Vehicle)
            //        .WithMany(p => p.VehicleDetailMaps)
            //        .HasForeignKey(d => d.VehicleID)
            //        .HasConstraintName("FK_VehicleDetailMaps_Vehicles");
            //});            

            modelBuilder.Entity<VehicleOwnershipType>(entity =>
            {
                entity.Property(e => e.VehicleOwnershipTypeID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });


            modelBuilder.Entity<VehicleType>(entity =>
            {
                entity.Property(e => e.VehicleTypeID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();
            });

            modelBuilder.Entity<View>(entity =>
            {
                entity.Property(e => e.ViewID).ValueGeneratedNever();

                //entity.HasOne(d => d.ChildView)
                //    .WithMany(p => p.InverseChildView)
                //    .HasForeignKey(d => d.ChildViewID)
                //    .HasConstraintName("FK_Views_Views1");

                //entity.HasOne(d => d.FilterQueries)
                //    .WithMany(p => p.Views)
                //    .HasForeignKey(d => d.FilterQueriesID)
                //    .HasConstraintName("FK_Views_FilterQueries");

                entity.HasOne(d => d.ViewType)
                    .WithMany(p => p.Views)
                    .HasForeignKey(d => d.ViewTypeID)
                    .HasConstraintName("FK_Views_ViewTypes");
            });

            modelBuilder.Entity<ViewAction>(entity =>
            {
                entity.Property(e => e.ViewActionID).ValueGeneratedNever();

                entity.HasOne(d => d.View)
                    .WithMany(p => p.ViewActions)
                    .HasForeignKey(d => d.ViewID)
                    .HasConstraintName("FK_ViewActions_Views");
            });

            modelBuilder.Entity<ViewColumn>(entity =>
            {
                entity.Property(e => e.ViewColumnID).ValueGeneratedNever();

                entity.Property(e => e.IsExcludeForExport).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsExpression).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.View)
                    .WithMany(p => p.ViewColumns)
                    .HasForeignKey(d => d.ViewID)
                    .HasConstraintName("FK_ViewGridColumns_Views");
            });

            modelBuilder.Entity<ViewColumnCultureData>(entity =>
            {
                entity.HasKey(e => new { e.ViewColumnID, e.CultureID });

                entity.HasOne(d => d.Culture)
                    .WithMany(p => p.ViewColumnCultureDatas)
                    .HasForeignKey(d => d.CultureID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ViewColumnCultureDatas_Cultures");

                entity.HasOne(d => d.ViewColumn)
                    .WithMany(p => p.ViewColumnCultureDatas)
                    .HasForeignKey(d => d.ViewColumnID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ViewColumnCultureDatas_ViewColumns");
            });

            modelBuilder.Entity<ViewCultureData>(entity =>
            {
                entity.HasKey(e => new { e.ViewID, e.CultureID });

                entity.HasOne(d => d.Culture)
                    .WithMany(p => p.ViewCultureDatas)
                    .HasForeignKey(d => d.CultureID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ViewCultureDatas_Cultures");

                entity.HasOne(d => d.View)
                    .WithMany(p => p.ViewCultureDatas)
                    .HasForeignKey(d => d.ViewID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ViewCultureDatas_Views");
            });

            //modelBuilder.Entity<VisaDetailMap>(entity =>
            //{
            //    entity.HasKey(e => e.VisaDetailsIID)
            //        .HasName("PK_VisaDetails");

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            modelBuilder.Entity<Visitor>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.Visitors)
                    .HasForeignKey(d => d.LoginID)
                    .HasConstraintName("FK_Visitor_Login");
            });

            modelBuilder.Entity<VisitorAttachmentMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Visitor)
                    .WithMany(p => p.VisitorAttachmentMaps)
                    .HasForeignKey(d => d.VisitorID)
                    .HasConstraintName("FK_VisitorAttachmentMap_Visitor");
            });

            //modelBuilder.Entity<VisitorBook>(entity =>
            //{
            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.HasOne(d => d.VisitingPurpose)
            //        .WithMany(p => p.VisitorBooks)
            //        .HasForeignKey(d => d.VisitingPurposeID)
            //        .HasConstraintName("FK_VisitorBooks_VisitingPurposes");
            //});

            //modelBuilder.Entity<VolunteerType>(entity =>
            //{
            //    entity.Property(e => e.VolunteerTypeID).ValueGeneratedNever();

            //    entity.Property(e => e.TimeStamps)
            //        .IsRowVersion()
            //        .IsConcurrencyToken();
            //});

            modelBuilder.Entity<Voucher>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Vouchers)
                    .HasForeignKey(d => d.CustomerID)
                    .HasConstraintName("FK_Vouchers_Customers");

                entity.HasOne(d => d.VoucherStatus)
                    .WithMany(p => p.Vouchers)
                    .HasForeignKey(d => d.StatusID)
                    .HasConstraintName("FK_Vouchers_Vouchers");

                entity.HasOne(d => d.VoucherType)
                    .WithMany(p => p.Vouchers)
                    .HasForeignKey(d => d.VoucherTypeID)
                    .HasConstraintName("FK_Vouchers_VoucherTypes");
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.Property(e => e.WarehouseID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.WarehouseStatuses)
                    .WithMany(p => p.Warehouses)
                    .HasForeignKey(d => d.StatusID)
                    .HasConstraintName("FK_Warehouses_WarehouseStatuses");
            });


            //modelBuilder.Entity<WeekDay>(entity =>
            //{
            //    entity.Property(e => e.WeekDayID).ValueGeneratedNever();

            //    entity.HasOne(d => d.ClassTimingSet)
            //        .WithMany(p => p.WeekDays)
            //        .HasForeignKey(d => d.ClassTimingSetID)
            //        .HasConstraintName("FK_WeekDays_WeekDays");

            //    entity.HasOne(d => d.Day)
            //        .WithMany(p => p.WeekDays)
            //        .HasForeignKey(d => d.DayID)
            //        .HasConstraintName("FK_WeekDays_Days");
            //});

            modelBuilder.Entity<WishList>(entity =>
            {
                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.WishLists)
                    .HasForeignKey(d => d.CustomerID)
                    .HasConstraintName("FK_WishList_Customers");
            });

            modelBuilder.Entity<Workflow>(entity =>
            {
                entity.HasOne(d => d.EntityType)
                    .WithMany(p => p.Workflows)
                    .HasForeignKey(d => d.LinkedEntityTypeID)
                    .HasConstraintName("FK_Workflows_EntityTypes");

                entity.HasOne(d => d.WorkflowFiled)
                    .WithMany(p => p.Workflows)
                    .HasForeignKey(d => d.WorkflowApplyFieldID)
                    .HasConstraintName("FK_Workflows_WorkflowFileds");

                entity.HasOne(d => d.WorkflowType)
                    .WithMany(p => p.Workflows)
                    .HasForeignKey(d => d.WorkflowTypeID)
                    .HasConstraintName("FK_Workflows_WorkflowTypes");
            });

            modelBuilder.Entity<WorkflowCondition>(entity =>
            {
                entity.Property(e => e.WorkflowConditionID).ValueGeneratedNever();
            });

            //modelBuilder.Entity<WorkflowEntity>(entity =>
            //{
            //    entity.Property(e => e.WorkflowEntityID).ValueGeneratedNever();
            //});

            modelBuilder.Entity<WorkflowFiled>(entity =>
            {
                entity.Property(e => e.WorkflowFieldID).ValueGeneratedNever();

                entity.HasOne(d => d.WorkflowType)
                    .WithMany(p => p.WorkflowFileds)
                    .HasForeignKey(d => d.WorkflowTypeID)
                    .HasConstraintName("FK_WorkflowFileds_WorkflowTypes");
            });

            modelBuilder.Entity<WorkflowLogMap>(entity =>
            {
                entity.HasOne(d => d.Workflow)
                    .WithMany(p => p.WorkflowLogMaps)
                    .HasForeignKey(d => d.WorkflowID)
                    .HasConstraintName("w");

                //entity.HasOne(d => d.WorkflowStatus)
                //    .WithMany(p => p.WorkflowLogMaps)
                //    .HasForeignKey(d => d.WorkflowStatusID)
                //    .HasConstraintName("FK_WorkflowLogMaps_WorkflowStatuses");
            });

            modelBuilder.Entity<WorkflowLogMapRuleApproverMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //                    .IsRowVersion()
                //                    .IsConcurrencyToken();

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.WorkflowLogMapRuleApproverMaps)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_WorkflowLogMapRuleApproverMaps_Employees");

                entity.HasOne(d => d.WorkflowCondition)
                    .WithMany(p => p.WorkflowLogMapRuleApproverMaps)
                    .HasForeignKey(d => d.WorkflowConditionID)
                    .HasConstraintName("FK_WorkflowLogMapRuleApproverMaps_WorkflowConditions");

                entity.HasOne(d => d.WorkflowLogMapRuleMap)
                    .WithMany(p => p.WorkflowLogMapRuleApproverMaps)
                    .HasForeignKey(d => d.WorkflowLogMapRuleMapID)
                    .HasConstraintName("FK_WorkflowLogMapRuleApproverMaps_WorkflowLogMapRuleMaps");

                entity.HasOne(d => d.WorkflowRuleCondition)
                    .WithMany(p => p.WorkflowLogMapRuleApproverMaps)
                    .HasForeignKey(d => d.WorkflowRuleConditionID)
                    .HasConstraintName("FK_WorkflowLogMapRuleApproverMaps_WorkflowRuleConditions");

                entity.HasOne(d => d.WorkflowStatus)
                    .WithMany(p => p.WorkflowLogMapRuleApproverMaps)
                    .HasForeignKey(d => d.WorkflowStatusID)
                    .HasConstraintName("FK_WorkflowLogMapRuleApproverMaps_WorkflowStatuses");
            });

            modelBuilder.Entity<WorkflowLogMapRuleMap>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.WorkflowCondition)
                    .WithMany(p => p.WorkflowLogMapRuleMaps)
                    .HasForeignKey(d => d.WorkflowConditionID)
                    .HasConstraintName("FK_WorkflowLogMapRuleMaps_WorkflowConditions");

                entity.HasOne(d => d.WorkflowLogMap)
                    .WithMany(p => p.WorkflowLogMapRuleMaps)
                    .HasForeignKey(d => d.WorkflowLogMapID)
                    .HasConstraintName("FK_WorkflowLogMapRuleMaps_WorkflowLogMaps");

                entity.HasOne(d => d.WorkflowRule)
                    .WithMany(p => p.WorkflowLogMapRuleMaps)
                    .HasForeignKey(d => d.WorkflowRuleID)
                    .HasConstraintName("FK_WorkflowLogMapRuleMaps_WorkflowRules");

                entity.HasOne(d => d.WorkflowStatus)
                    .WithMany(p => p.WorkflowLogMapRuleMaps)
                    .HasForeignKey(d => d.WorkflowStatusID)
                    .HasConstraintName("FK_WorkflowLogMapRuleMaps_WorkflowStatuses");
            });

            modelBuilder.Entity<WorkflowRule>(entity =>
            {
                entity.HasOne(d => d.Condition)
                    .WithMany(p => p.WorkflowRules)
                    .HasForeignKey(d => d.ConditionID)
                    .HasConstraintName("FK_WorkflowRules_WorkflowConditions");

                entity.HasOne(d => d.Workflow)
                    .WithMany(p => p.WorkflowRules)
                    .HasForeignKey(d => d.WorkflowID)
                    .HasConstraintName("FK_WorkflowRules_Workflows");
            });

            modelBuilder.Entity<WorkflowRuleApprover>(entity =>
            {
                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.WorkflowRuleApprovers)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_WorkflowRuleApprovers_Employees");

                entity.HasOne(d => d.WorkflowRuleCondition)
                    .WithMany(p => p.WorkflowRuleApprovers)
                    .HasForeignKey(d => d.WorkflowRuleConditionID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkflowRuleApprovers_WorkflowRuleConditions");
            });

            modelBuilder.Entity<WorkflowRuleCondition>(entity =>
            {
                entity.HasOne(d => d.Condition)
                    .WithMany(p => p.WorkflowRuleConditions)
                    .HasForeignKey(d => d.ConditionID)
                    .HasConstraintName("FK_WorkflowRuleConditions_WorkflowConditions");

                entity.HasOne(d => d.WorkflowRule)
                    .WithMany(p => p.WorkflowRuleConditions)
                    .HasForeignKey(d => d.WorkflowRuleID)
                    .HasConstraintName("FK_WorkflowRuleConditions_WorkflowRules");
            });

            //modelBuilder.Entity<WorkflowStatus>(entity =>
            //{
            //    entity.HasOne(d => d.WorkflowType)
            //        .WithMany(p => p.WorkflowStatus)
            //        .HasForeignKey(d => d.WorkflowTypeID)
            //        .HasConstraintName("FK_WorkflowStatuses_WorkflowTypes");
            //});

            modelBuilder.Entity<WorkflowTransactionHeadMap>(entity =>
            {
                entity.HasOne(d => d.TransactionHead)
                    .WithMany(p => p.WorkflowTransactionHeadMaps)
                    .HasForeignKey(d => d.TransactionHeadID)
                    .HasConstraintName("FK_WorkflowTransactionHeadMaps_TransactionHead");

                entity.HasOne(d => d.Workflow)
                    .WithMany(p => p.WorkflowTransactionHeadMaps)
                    .HasForeignKey(d => d.WorkflowID)
                    .HasConstraintName("FK_WorkflowTransactionHeadMaps_Workflows");

                entity.HasOne(d => d.WorkflowStatus)
                    .WithMany(p => p.WorkflowTransactionHeadMaps)
                    .HasForeignKey(d => d.WorkflowStatusID)
                    .HasConstraintName("FK_WorkflowTransactionHeadMaps_WorkflowStatuses");
            });

            modelBuilder.Entity<WorkflowTransactionHeadRuleMap>(entity =>
            {
                entity.HasOne(d => d.WorkflowCondition)
                    .WithMany(p => p.WorkflowTransactionHeadRuleMaps)
                    .HasForeignKey(d => d.WorkflowConditionID)
                    .HasConstraintName("FK_WorkflowTransactionHeadRuleMaps_WorkflowConditions");

                entity.HasOne(d => d.WorkflowRule)
                    .WithMany(p => p.WorkflowTransactionHeadRuleMaps)
                    .HasForeignKey(d => d.WorkflowRuleID)
                    .HasConstraintName("FK_WorkflowTransactionHeadRuleMaps_WorkflowRules");

                entity.HasOne(d => d.WorkflowStatus)
                    .WithMany(p => p.WorkflowTransactionHeadRuleMaps)
                    .HasForeignKey(d => d.WorkflowStatusID)
                    .HasConstraintName("FK_WorkflowTransactionHeadRuleMaps_WorkflowStatuses");

                entity.HasOne(d => d.WorkflowTransactionHeadMap)
                    .WithMany(p => p.WorkflowTransactionHeadRuleMaps)
                    .HasForeignKey(d => d.WorkflowTransactionHeadMapID)
                    .HasConstraintName("FK_WorkflowTransactionHeadRuleMaps_WorkflowTransactionHeadMaps");
            });

            modelBuilder.Entity<WorkflowTransactionRuleApproverMap>(entity =>
            {
                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.WorkflowTransactionRuleApproverMaps)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_WorkflowTransactionRuleApproverMaps_Employees");

                entity.HasOne(d => d.WorkflowCondition)
                    .WithMany(p => p.WorkflowTransactionRuleApproverMaps)
                    .HasForeignKey(d => d.WorkflowConditionID)
                    .HasConstraintName("FK_WorkflowTransactionRuleApproverMaps_WorkflowConditions");

                entity.HasOne(d => d.WorkflowRuleCondition)
                    .WithMany(p => p.WorkflowTransactionRuleApproverMaps)
                    .HasForeignKey(d => d.WorkflowRuleConditionID)
                    .HasConstraintName("FK_WorkflowTransactionRuleApproverMaps_WorkflowRuleConditions");

                entity.HasOne(d => d.WorkflowStatus)
                    .WithMany(p => p.WorkflowTransactionRuleApproverMaps)
                    .HasForeignKey(d => d.WorkflowStatusID)
                    .HasConstraintName("FK_WorkflowTransactionRuleApproverMaps_WorkflowStatuses");

                entity.HasOne(d => d.WorkflowTransactionHeadRuleMap)
                    .WithMany(p => p.WorkflowTransactionRuleApproverMaps)
                    .HasForeignKey(d => d.WorkflowTransactionHeadRuleMapID)
                    .HasConstraintName("FK_WorkflowTransactionRuleApproverMaps_WorkflowTransactionHeadRuleMaps");
            });

            modelBuilder.Entity<WorkflowType>(entity =>
            {
                entity.Property(e => e.WorkflowTypeID).ValueGeneratedNever();
            });

            modelBuilder.Entity<Zone>(entity =>
            {
                entity.Property(e => e.ZoneID).ValueGeneratedNever();

                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Zones)
                    .HasForeignKey(d => d.CountryID)
                    .HasConstraintName("FK_Zones_Countries");
            });

            modelBuilder.Entity<Department1>(entity =>
            {
                entity.Property(e => e.DepartmentID).ValueGeneratedNever();

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.Department1)
                    .HasForeignKey(d => d.AcademicYearID)
                    .HasConstraintName("FK_Departments_AcademicYear");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Department1)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_Departments_School");
            });

            modelBuilder.Entity<Dashboard>(entity =>
            {
                entity.HasKey(e => e.Dsh_ID)
                    .HasName("PK__Dashboar__413E2F850CF3693F");
            });

            modelBuilder.Entity<RFQSupplierRequestMap>(entity =>
            {
                entity.HasKey(e => e.RFQMapID)
                    .HasName("PK_RFQMapID");

                entity.HasOne(d => d.Head)
                    .WithMany(p => p.RFQSupplierRequestMapHeads)
                    .HasForeignKey(d => d.HeadID)
                    .HasConstraintName("FK_RFQSupplierRequestMap_TransHead");

                entity.HasOne(d => d.PurchaseRequest)
                    .WithMany(p => p.RFQSupplierRequestMapPurchaseRequests)
                    .HasForeignKey(d => d.PurchaseRequestID)
                    .HasConstraintName("FK_RFQSupplierRequestMap_PurchaseRequest");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.RFQSupplierRequestMaps)
                    .HasForeignKey(d => d.SupplierID)
                    .HasConstraintName("FK_RFQSupplierRequestMap_Supplier");

                entity.HasOne(d => d.Tender)
                    .WithMany(p => p.RFQSupplierRequestMaps)
                    .HasForeignKey(d => d.TenderID)
                    .HasConstraintName("FK_RFQSupplierRequestMap_Tender");

            });

            modelBuilder.Entity<SupplierContentIDs>(entity =>
            {
                entity.HasKey(e => e.SupplierContentID)
                    .HasName("PK_SupplierContentID");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.SupplierContentIDs)
                    .HasForeignKey(d => d.SupplierID)
                    .HasConstraintName("FK_SupplierContent_Supplier");
            });
            modelBuilder.Entity<EmployeeExperienceDetail>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeExperienceDetails)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_EmployeeExperienceDetails_Employees");
            });


            modelBuilder.Entity<ArchiveTable>(entity =>
            {
                entity.Property(e => e.ArchiveTableID).ValueGeneratedNever();
            });

            modelBuilder.Entity<Tender>(entity =>
            {
                entity.HasKey(e => e.TenderIID)
                    .HasName("PK_TenderIID");

                entity.HasOne(d => d.TenderStatus)
                    .WithMany(p => p.Tenders)
                    .HasForeignKey(d => d.TenderStatusID)
                    .HasConstraintName("FK_Tender_Status");

                entity.HasOne(d => d.TenderType)
                    .WithMany(p => p.Tenders)
                    .HasForeignKey(d => d.TenderTypeID)
                    .HasConstraintName("FK_Tender_Types");
            });

            modelBuilder.Entity<TenderAuthentication>(entity =>
            {
                entity.HasKey(e => e.AuthenticationID)
                    .HasName("PK_AuthenticationID");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TenderAuthentications)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_TenderAuthentication_Employee");

            });

            modelBuilder.Entity<TenderAuthenticationLog>(entity =>
            {
                entity.HasKey(e => e.TenderAuthMapIID)
                    .HasName("PK_TenderAuthMapIID");

                entity.HasOne(d => d.Authentication)
                    .WithMany(p => p.TenderAuthenticationLogs)
                    .HasForeignKey(d => d.AuthenticationID)
                    .HasConstraintName("FK_TenderAuthenticationLog_Authendication");

                entity.HasOne(d => d.Tender)
                    .WithMany(p => p.TenderAuthenticationLogs)
                    .HasForeignKey(d => d.TenderID)
                    .HasConstraintName("FK_TenderAuthenticationLog_Tender");
            });

            modelBuilder.Entity<TenderType1>(entity =>
            {
                entity.HasKey(e => e.TenderTypeID)
                    .HasName("PK_TenderTypeID");
            });

            modelBuilder.Entity<BidApprovalDetail>(entity =>
            {
                entity.HasKey(e => e.DetailIID)
                    .HasName("PK_DetailIID");

                entity.HasOne(d => d.BidApproval)
                    .WithMany(p => p.BidApprovalDetails)
                    .HasForeignKey(d => d.BidApprovalID)
                    .HasConstraintName("FK_BidApprovalDetail_Head");
            });

            modelBuilder.Entity<BidApprovalHead>(entity =>
            {
                entity.HasKey(e => e.BidApprovalIID)
                    .HasName("PK_BidApprovalIID");

                entity.HasOne(d => d.DocumentStatus)
                    .WithMany(p => p.BidApprovalHeads)
                    .HasForeignKey(d => d.DocumentStatusID)
                    .HasConstraintName("FK_BidApprovalHead_DocStatus");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.BidApprovalHeads)
                    .HasForeignKey(d => d.DocumentTypeID)
                    .HasConstraintName("FK_BidApprovalHead_DocType");

                entity.HasOne(d => d.Tender)
                    .WithMany(p => p.BidApprovalHeads)
                    .HasForeignKey(d => d.TenderID)
                    .HasConstraintName("FK_BidApprovalHead_Tender");
            });

            modelBuilder.Entity<JobSeeker>(entity =>
            {
                entity.HasKey(e => e.SeekerID)
                    .HasName("PK_SeekerID");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.JobSeekers)
                    .HasForeignKey(d => d.GenderID)
                    .HasConstraintName("FK_JobSeeker_Gender");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.JobSeekers)
                    .HasForeignKey(d => d.LoginID)
                    .HasConstraintName("FK_JobSeeker_Login");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.JobSeekers)
                    .HasForeignKey(d => d.CountryID)
                    .HasConstraintName("FK_JobSeeker_Country");

                entity.HasOne(d => d.Qualification)
                    .WithMany(p => p.JobSeekers)
                    .HasForeignKey(d => d.QualificationID)
                    .HasConstraintName("FK_JobSeeker_Qualification");

                entity.HasOne(d => d.BloodGroup)
                    .WithMany(p => p.JobSeekers)
                    .HasForeignKey(d => d.BloodGroupID)
                    .HasConstraintName("FK_JobSeeker_BloodGroup");

                entity.HasOne(d => d.PassportIssueCountry)
                    .WithMany(p => p.JobSeekerPassportIssueCountries)
                    .HasForeignKey(d => d.PassportIssueCountryID)
                    .HasConstraintName("FK_JobSeeker_PassPortIssueCountry");

                    entity.HasOne(d => d.Nationality)
                    .WithMany(p => p.JobSeekers)
                    .HasForeignKey(d => d.NationalityID)
                    .HasConstraintName("FK_JobSeeker_Nationality");
            });

            modelBuilder.Entity<JobSeekerSkillMap>(entity =>
            {
                entity.HasKey(e => e.SkillMapID)
                    .HasName("PK_SkillMapID");

                entity.HasOne(d => d.Seeker)
                    .WithMany(p => p.JobSeekerSkillMaps)
                    .HasForeignKey(d => d.SeekerID)
                    .HasConstraintName("FK_JobSeekerSkillMaps_JobSeeker");
            });

            modelBuilder.Entity<RecruitmentLogin>(entity =>
            {
                entity.HasKey(e => e.LoginID)
                    .HasName("PK_LoginID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RecruitmentLogins)
                    .HasForeignKey(d => d.RoleID)
                    .HasConstraintName("FK_RecruitmentLogin_Role");
            });

            modelBuilder.Entity<AvailableJob>(entity =>
            {
                entity.HasOne(d => d.JobType)
                    .WithMany(p => p.AvailableJobs)
                    .HasForeignKey(d => d.JobTypeID)
                    .HasConstraintName("FK_AvailJob_JobType");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.AvailableJobs)
                    .HasForeignKey(d => d.SchoolID)
                    .HasConstraintName("FK_AvailJob_School");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.AvailableJobs)
                    .HasForeignKey(d => d.StatusID)
                    .HasConstraintName("FK_AvailJob_Status");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.AvailableJobs)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_AvailableJob_Department");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.AvailableJobs)
                    .HasForeignKey(d => d.CountryID)
                    .HasConstraintName("FK_AvailableJob_Country");

                entity.HasOne(d => d.Designation)
                    .WithMany(p => p.AvailableJobs)
                    .HasForeignKey(d => d.DesignationID)
                    .HasConstraintName("FK_AvailableJob_Designation");
            });

            modelBuilder.Entity<AvailableJobSkillMap>(entity =>
            {
                entity.HasKey(e => e.SkillID)
                    .HasName("PK_SkillID");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.AvailableJobSkillMaps)
                    .HasForeignKey(d => d.JobID)
                    .HasConstraintName("FK_JobSkill_Job");
            });

            modelBuilder.Entity<AvailableJobCriteriaMap>(entity =>
            {
                entity.HasKey(e => e.CriteriaID)
                    .HasName("PK_CriteriaID");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.AvailableJobCriteriaMaps)
                    .HasForeignKey(d => d.JobID)
                    .HasConstraintName("FK_AvailableJobCriteria_Job");

                entity.HasOne(d => d.Qualification)
                    .WithMany(p => p.AvailableJobCriteriaMaps)
                    .HasForeignKey(d => d.QualificationID)
                    .HasConstraintName("FK_AvailableJobCriteria_Qualification");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.AvailableJobCriteriaMaps)
                    .HasForeignKey(d => d.TypeID)
                    .HasConstraintName("FK_AvailableJobCriteria_Type");
            });

            modelBuilder.Entity<AvailableJobTag>(entity =>
            {
                entity.HasOne(d => d.Job)
                    .WithMany(p => p.AvailableJobTags)
                    .HasForeignKey(d => d.JobID)
                    .HasConstraintName("FK_AvailableJobTags_AvailableJobs");
            });

            modelBuilder.Entity<JobApplication>(entity =>
            {
                entity.HasKey(e => e.ApplicationIID)
                    .HasName("PK_ApplicationIID");

                entity.HasOne(d => d.Applicant)
                    .WithMany(p => p.JobApplications)
                    .HasForeignKey(d => d.ApplicantID)
                    .HasConstraintName("FK_JobApplication_Seeker");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobApplications)
                    .HasForeignKey(d => d.JobID)
                    .HasConstraintName("FK_JobApplication_Job");

                entity.HasOne(d => d.CountryOfResidence)
                    .WithMany(p => p.JobApplications)
                    .HasForeignKey(d => d.CountryOfResidenceID)
                    .HasConstraintName("FK_JobApplications_Country");
            });

            modelBuilder.Entity<EmployeeRelationsDetail>(entity =>
            {
                //entity.Property(e => e.TimeStamps)
                //    .IsRowVersion()
                //    .IsConcurrencyToken();

                //entity.HasOne(d => d.CountryofIssue)
                //    .WithMany(p => p.EmployeeRelationsDetails)
                //    .HasForeignKey(d => d.CountryofIssueID)
                //    .HasConstraintName("FK_EmployeeRelations_CountryOfIssue");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeRelationsDetails)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_EmployeeRelations_Employee");

                entity.HasOne(d => d.EmployeeRelationType)
                    .WithMany(p => p.EmployeeRelationsDetails)
                    .HasForeignKey(d => d.EmployeeRelationTypeID)
                    .HasConstraintName("FK_EmployeeRelations_Ralation");

                //entity.HasOne(d => d.Sponsor)
                //    .WithMany(p => p.EmployeeRelationsDetails)
                //    .HasForeignKey(d => d.SponsorID)
                //    .HasConstraintName("FK_EmployeeRelations_Sponsor");
            });

            modelBuilder.Entity<JobInterview>(entity =>
            {
                entity.HasKey(e => e.InterviewID)
                    .HasName("PK_InterviewID");

                entity.HasOne(d => d.Interviewer)
                    .WithMany(p => p.JobInterviews)
                    .HasForeignKey(d => d.InterviewerID)
                    .HasConstraintName("FK_JobInterview_Interviewer");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobInterviews)
                    .HasForeignKey(d => d.JobID)
                    .HasConstraintName("FK_JobInterview_Job");
            });

            modelBuilder.Entity<JobInterviewMap>(entity =>
            {
                entity.HasKey(e => e.MapID)
                    .HasName("PK_MapID");

                entity.HasOne(d => d.CompletedRound)
                    .WithMany(p => p.JobInterviewMaps)
                    .HasForeignKey(d => d.CompletedRoundID)
                    .HasConstraintName("FK_JobInterviewMap_Round");

                entity.HasOne(d => d.Interview)
                    .WithMany(p => p.JobInterviewMaps)
                    .HasForeignKey(d => d.InterviewID)
                    .HasConstraintName("FK_JobInterviewMap_Interview");

                entity.HasOne(d => d.Applicant)
                    .WithMany(p => p.JobInterviewMaps)
                    .HasForeignKey(d => d.ApplicantID)
                    .HasConstraintName("FK_JobInterviewMap_Applicant");
            });

            modelBuilder.Entity<JobInterviewRound>(entity =>
            {
                entity.HasKey(e => e.RoundID)
                    .HasName("PK_RoundID");
            });

            modelBuilder.Entity<JobInterviewRoundMap>(entity =>
            {
                entity.HasKey(e => e.RoundMapID)
                    .HasName("PK_RoundMapID");

                entity.HasOne(d => d.Interview)
                    .WithMany(p => p.JobInterviewRoundMaps)
                    .HasForeignKey(d => d.InterviewID)
                    .HasConstraintName("FK_JobInterviewRoundMap_Interview");

                entity.HasOne(d => d.Round)
                    .WithMany(p => p.JobInterviewRoundMaps)
                    .HasForeignKey(d => d.RoundID)
                    .HasConstraintName("FK_JobInterviewRoundMap_Round");
            });

            modelBuilder.Entity<EmployeeJobDescription>(entity =>
            {
                entity.HasKey(e => e.JobDescriptionIID)
                    .HasName("PK_JobDescriptionIID");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeJobDescriptionEmployees)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_EmployeeJobDescription_Employee");

                entity.HasOne(d => d.JobApplication)
                    .WithMany(p => p.EmployeeJobDescriptions)
                    .HasForeignKey(d => d.JobApplicationID)
                    .HasConstraintName("FK_EmployeeJobDescription_JobApplication");

                entity.HasOne(d => d.ReportingToEmployee)
                    .WithMany(p => p.EmployeeJobDescriptionReportingToEmployees)
                    .HasForeignKey(d => d.ReportingToEmployeeID)
                    .HasConstraintName("FK_EmployeeJobDescription_ReportingToEmployee");
            });

            modelBuilder.Entity<EmployeeJobDescriptionDetail>(entity =>
            {
                entity.HasKey(e => e.JobDescriptionMapID)
                    .HasName("PK_JobDescriptionMapID");

                entity.HasOne(d => d.JobDescription)
                    .WithMany(p => p.EmployeeJobDescriptionDetails)
                    .HasForeignKey(d => d.JobDescriptionID)
                    .HasConstraintName("FK_EmployeeJobDescriptionDetail_JobDescription");
            });

            modelBuilder.Entity<JobDescription>(entity =>
            {
                entity.HasKey(e => e.JDMasterIID)
                    .HasName("PK_JDMasterIID");

                entity.HasOne(d => d.ReportingToEmployee)
                    .WithMany(p => p.JobDescriptions)
                    .HasForeignKey(d => d.ReportingToEmployeeID)
                    .HasConstraintName("FK_JobDescription_ReportingToEmployee");
            });

            modelBuilder.Entity<JobDescriptionDetail>(entity =>
            {
                entity.HasKey(e => e.JDMapID)
                    .HasName("PK_JDMapID");

                entity.HasOne(d => d.JDMaster)
                    .WithMany(p => p.JobDescriptionDetails)
                    .HasForeignKey(d => d.JDMasterID)
                    .HasConstraintName("FK_JobDescriptionDetail_JobDescription");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
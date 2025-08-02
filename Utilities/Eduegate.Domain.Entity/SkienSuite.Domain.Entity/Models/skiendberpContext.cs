using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Eduegate.Domain.Entity.Models.Mapping;

namespace Eduegate.Domain.Entity.Models
{
    public partial class skiendberpContext : DbContext
    {
        static skiendberpContext()
        {
            Database.SetInitializer<skiendberpContext>(null);
        }

        public skiendberpContext()
            : base("Name=skiendberpContext")
        {
        }

        public DbSet<AccountBehavoir> AccountBehavoirs { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountTransactionDetail> AccountTransactionDetails { get; set; }
        public DbSet<AccountTransactionHeadAccountMap> AccountTransactionHeadAccountMaps { get; set; }
        public DbSet<AccountTransactionHead> AccountTransactionHeads { get; set; }
        public DbSet<AccountTransaction> AccountTransactions { get; set; }
        public DbSet<ChartOfAccountMap> ChartOfAccountMaps { get; set; }
        public DbSet<ChartOfAccount> ChartOfAccounts { get; set; }
        public DbSet<ChartRowType> ChartRowTypes { get; set; }
        public DbSet<CostCenter> CostCenters { get; set; }
        public DbSet<CustomerAccountMap> CustomerAccountMaps { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Payable> Payables { get; set; }
        public DbSet<Receivable> Receivables { get; set; }
        public DbSet<SupplierAccountMap> SupplierAccountMaps { get; set; }
        public DbSet<ClaimLoginMap> ClaimLoginMaps { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<ClaimSetClaimMap> ClaimSetClaimMaps { get; set; }
        public DbSet<ClaimSetClaimSetMap> ClaimSetClaimSetMaps { get; set; }
        public DbSet<ClaimSetLoginMap> ClaimSetLoginMaps { get; set; }
        public DbSet<ClaimSet> ClaimSets { get; set; }
        public DbSet<ClaimType> ClaimTypes { get; set; }
        public DbSet<LoginRoleMap> LoginRoleMaps { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<PermissionCultureData> PermissionCultureDatas { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RoleCultureData> RoleCultureDatas { get; set; }
        public DbSet<RolePermissionMap> RolePermissionMaps { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<AssetCategory> AssetCategories { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetTransactionDetail> AssetTransactionDetails { get; set; }
        public DbSet<AssetTransactionHead> AssetTransactionHeads { get; set; }
        public DbSet<AssetTransactionHeadAccountMap> AssetTransactionHeadAccountMaps { get; set; }
        public DbSet<BrandCultureData> BrandCultureDatas { get; set; }
        public DbSet<BrandImageMap> BrandImageMaps { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<BrandStatus> BrandStatuses { get; set; }
        public DbSet<BrandTagMap> BrandTagMaps { get; set; }
        public DbSet<BrandTag> BrandTags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryCultureData> CategoryCultureDatas { get; set; }
        public DbSet<CategoryImageMap> CategoryImageMaps { get; set; }
        public DbSet<CategorySetting> CategorySettings { get; set; }
        public DbSet<CategoryTagMap> CategoryTagMaps { get; set; }
        public DbSet<CategoryTag> CategoryTags { get; set; }
        public DbSet<CustomerProductReference> CustomerProductReferences { get; set; }
        public DbSet<DeliveryType> DeliveryTypes { get; set; }
        public DbSet<EmployeeCatalogRelation> EmployeeCatalogRelations { get; set; }
        public DbSet<PackingType> PackingTypes { get; set; }
        public DbSet<ProductBundle> ProductBundles { get; set; }
        public DbSet<ProductCategoryMap> ProductCategoryMaps { get; set; }
        public DbSet<ProductCultureData> ProductCultureDatas { get; set; }
        public DbSet<ProductFamily> ProductFamilies { get; set; }
        public DbSet<ProductFamilyCultureData> ProductFamilyCultureDatas { get; set; }
        public DbSet<ProductFamilyPropertyMap> ProductFamilyPropertyMaps { get; set; }
        public DbSet<ProductFamilyPropertyTypeMap> ProductFamilyPropertyTypeMaps { get; set; }
        public DbSet<ProductFamilyType> ProductFamilyTypes { get; set; }
        public DbSet<ProductImageMap> ProductImageMaps { get; set; }
        public DbSet<ProductInventoryConfig> ProductInventoryConfigs { get; set; }
        public DbSet<ProductInventoryConfigCultureData> ProductInventoryConfigCultureDatas { get; set; }
        public DbSet<ProductInventoryConfigTemp> ProductInventoryConfigTemps { get; set; }
        public DbSet<ProductInventoryProductConfigMap> ProductInventoryProductConfigMaps { get; set; }
        public DbSet<ProductInventorySKUConfigMap> ProductInventorySKUConfigMaps { get; set; }
        public DbSet<ProductPriceListBranchMap> ProductPriceListBranchMaps { get; set; }
        public DbSet<ProductPriceListBrandMap> ProductPriceListBrandMaps { get; set; }
        public DbSet<ProductPriceListCategoryMap> ProductPriceListCategoryMaps { get; set; }
        public DbSet<ProductPriceListCustomerGroupMap> ProductPriceListCustomerGroupMaps { get; set; }
        public DbSet<ProductPriceListCustomerMap> ProductPriceListCustomerMaps { get; set; }
        public DbSet<ProductPriceListLevel> ProductPriceListLevels { get; set; }
        public DbSet<ProductPriceListProductMap> ProductPriceListProductMaps { get; set; }
        public DbSet<ProductPriceListProductQuantityMap> ProductPriceListProductQuantityMaps { get; set; }
        public DbSet<ProductPriceList> ProductPriceLists { get; set; }
        public DbSet<ProductPriceListSKUMap> ProductPriceListSKUMaps { get; set; }
        public DbSet<ProductPriceListSKUQuantityMap> ProductPriceListSKUQuantityMaps { get; set; }
        public DbSet<ProductPriceListType> ProductPriceListTypes { get; set; }
        public DbSet<ProductPropertyMapCultureData> ProductPropertyMapCultureDatas { get; set; }
        public DbSet<ProductPropertyMap> ProductPropertyMaps { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSKUCultureData> ProductSKUCultureDatas { get; set; }
        public DbSet<ProductSKUMap> ProductSKUMaps { get; set; }
        public DbSet<ProductSKUSiteMap> ProductSKUSiteMaps { get; set; }
        public DbSet<ProductSKUTagMap> ProductSKUTagMaps { get; set; }
        public DbSet<ProductSKUTag> ProductSKUTags { get; set; }
        public DbSet<ProductStatu> ProductStatus { get; set; }
        public DbSet<ProductStatusCultureData> ProductStatusCultureDatas { get; set; }
        public DbSet<ProductTagMap> ProductTagMaps { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<ProductToProductMap> ProductToProductMaps { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductVideoMap> ProductVideoMaps { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyCultureData> PropertyCultureDatas { get; set; }
        public DbSet<PropertyTypeCultureData> PropertyTypeCultureDatas { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<RelationType> RelationTypes { get; set; }
        public DbSet<SalesRelationshipType> SalesRelationshipTypes { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<UnitGroup> UnitGroups { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<BannerStatus> BannerStatuses { get; set; }
        public DbSet<BannerType> BannerTypes { get; set; }
        public DbSet<BoilerPlateParameter> BoilerPlateParameters { get; set; }
        public DbSet<BoilerPlate> BoilerPlates { get; set; }
        public DbSet<CategoryPageBoilerPlatMap> CategoryPageBoilerPlatMaps { get; set; }
        public DbSet<CustomerJustAsk> CustomerJustAsks { get; set; }
        public DbSet<CustomerSupportTicket> CustomerSupportTickets { get; set; }
        public DbSet<DeliveryTypeCategoryMaster> DeliveryTypeCategoryMasters { get; set; }
        public DbSet<DeliveryTypeMaster> DeliveryTypeMasters { get; set; }
        public DbSet<KnowHowOption> KnowHowOptions { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsletterSubscription> NewsletterSubscriptions { get; set; }
        public DbSet<NewsType> NewsTypes { get; set; }
        public DbSet<PageBoilerplateMapParameterCultureData> PageBoilerplateMapParameterCultureDatas { get; set; }
        public DbSet<PageBoilerplateMapParameter> PageBoilerplateMapParameters { get; set; }
        public DbSet<PageBoilerplateMap> PageBoilerplateMaps { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PageType> PageTypes { get; set; }
        public DbSet<SiteCountryMap> SiteCountryMaps { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<StaticContentData> StaticContentDatas { get; set; }
        public DbSet<StaticContentType> StaticContentTypes { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<TimeSlotMaster> TimeSlotMasters { get; set; }
        public DbSet<TimeSlotOverRider> TimeSlotOverRiders { get; set; }
        public DbSet<UserJobApplication> UserJobApplications { get; set; }
        public DbSet<OrderDeliveryDisplayHeadMap> OrderDeliveryDisplayHeadMaps { get; set; }
        public DbSet<OrderDeliveryHolidayDetail> OrderDeliveryHolidayDetails { get; set; }
        public DbSet<OrderDeliveryHolidayHead> OrderDeliveryHolidayHeads { get; set; }
        public DbSet<ReceivingMethod> ReceivingMethods { get; set; }
        public DbSet<ReturnMethod> ReturnMethods { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<ServiceProviderCountryGroup> ServiceProviderCountryGroups { get; set; }
        public DbSet<ServiceProviderLog> ServiceProviderLogs { get; set; }
        public DbSet<ServiceProvider> ServiceProviders { get; set; }
        public DbSet<DocumentFile> DocumentFiles { get; set; }
        public DbSet<DocumentFileStatus> DocumentFileStatuses { get; set; }
        public DbSet<DataFeedLogDetail> DataFeedLogDetails { get; set; }
        public DbSet<DataFeedLog> DataFeedLogs { get; set; }
        public DbSet<DataFeedOperation> DataFeedOperations { get; set; }
        public DbSet<DataFeedStatus> DataFeedStatuses { get; set; }
        public DbSet<DataFeedTableColumn> DataFeedTableColumns { get; set; }
        public DbSet<DataFeedTable> DataFeedTables { get; set; }
        public DbSet<DataFeedType> DataFeedTypes { get; set; }
        public DbSet<BranchDocumentTypeMap> BranchDocumentTypeMaps { get; set; }
        public DbSet<CustomerGroupDeliveryTypeMap> CustomerGroupDeliveryTypeMaps { get; set; }
        public DbSet<DeliveryCharge> DeliveryCharges { get; set; }
        public DbSet<DeliveryDuration> DeliveryDurations { get; set; }
        public DbSet<DeliveryTypeAllowedAreaMap> DeliveryTypeAllowedAreaMaps { get; set; }
        public DbSet<DeliveryTypeAllowedCountryMap> DeliveryTypeAllowedCountryMaps { get; set; }
        public DbSet<DeliveryTypeAllowedZoneMap> DeliveryTypeAllowedZoneMaps { get; set; }
        public DbSet<DeliveryTypes1> DeliveryTypes1 { get; set; }
        public DbSet<DeliveryTypeStatus> DeliveryTypeStatuses { get; set; }
        public DbSet<DeliveryTypeTimeSlotMap> DeliveryTypeTimeSlotMaps { get; set; }
        public DbSet<DeliveryTypeTimeSlotMapsCulture> DeliveryTypeTimeSlotMapsCultures { get; set; }
        public DbSet<InventoryVerification> InventoryVerifications { get; set; }
        public DbSet<InvetoryTransaction> InvetoryTransactions { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationType> LocationTypes { get; set; }
        public DbSet<Notify> Notifies { get; set; }
        public DbSet<ProductDeliveryCountrySetting> ProductDeliveryCountrySettings { get; set; }
        public DbSet<ProductDeliveryTypeMap> ProductDeliveryTypeMaps { get; set; }
        public DbSet<ProductInventory> ProductInventories { get; set; }
        public DbSet<ProductInventories_Bak> ProductInventories_Bak { get; set; }
        public DbSet<ProductInventorySerialMap> ProductInventorySerialMaps { get; set; }
        public DbSet<ProductLocationMap> ProductLocationMaps { get; set; }
        public DbSet<ProductSerialMap> ProductSerialMaps { get; set; }
        public DbSet<ProductTypeDeliveryTypeMap> ProductTypeDeliveryTypeMaps { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<ShoppingCart1> ShoppingCarts1 { get; set; }
        public DbSet<ShoppingCartVoucherMap> ShoppingCartVoucherMaps { get; set; }
        public DbSet<SKUPaymentMethodExceptionMap> SKUPaymentMethodExceptionMaps { get; set; }
        public DbSet<TransactionAllocation> TransactionAllocations { get; set; }
        public DbSet<TransactionDetail> TransactionDetails { get; set; }
        public DbSet<TransactionHead> TransactionHeads { get; set; }
        public DbSet<TransactionHeadAccountMap> TransactionHeadAccountMaps { get; set; }
        public DbSet<TransactionHeadEntitlementMap> TransactionHeadEntitlementMaps { get; set; }
        public DbSet<TransactionHeadPayablesMap> TransactionHeadPayablesMaps { get; set; }
        public DbSet<TransactionHeadPointsMap> TransactionHeadPointsMaps { get; set; }
        public DbSet<TransactionHeadReceivablesMap> TransactionHeadReceivablesMaps { get; set; }
        public DbSet<TransactionHeadShoppingCartMap> TransactionHeadShoppingCartMaps { get; set; }
        public DbSet<TransactionShipment> TransactionShipments { get; set; }
        public DbSet<TransactionStatus> TransactionStatuses { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<VoucherStatus> VoucherStatuses { get; set; }
        public DbSet<VoucherType> VoucherTypes { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<JobActivity> JobActivities { get; set; }
        public DbSet<JobEntryDetail> JobEntryDetails { get; set; }
        public DbSet<JobEntryHead> JobEntryHeads { get; set; }
        public DbSet<JobOperationStatus> JobOperationStatuses { get; set; }
        public DbSet<JobsEntryHeadPayableMap> JobsEntryHeadPayableMaps { get; set; }
        public DbSet<JobsEntryHeadReceivableMap> JobsEntryHeadReceivableMaps { get; set; }
        public DbSet<JobSize> JobSizes { get; set; }
        public DbSet<JobStatus> JobStatuses { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<ActionLinkType> ActionLinkTypes { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<BranchGroup> BranchGroups { get; set; }
        public DbSet<BranchGroupStatus> BranchGroupStatuses { get; set; }
        public DbSet<BranchStatus> BranchStatuses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyCurrencyMap> CompanyCurrencyMaps { get; set; }
        public DbSet<CompanyStatus> CompanyStatuses { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Culture> Cultures { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CustomerGroup> CustomerGroups { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerSetting> CustomerSettings { get; set; }
        public DbSet<CustomerStatus> CustomerStatuses { get; set; }
        public DbSet<CustomerSupplierMap> CustomerSupplierMaps { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentStatus> DepartmentStatuses { get; set; }
        public DbSet<DocumentReferenceStatusMap> DocumentReferenceStatusMaps { get; set; }
        public DbSet<DocumentReferenceType> DocumentReferenceTypes { get; set; }
        public DbSet<DocumentStatus> DocumentStatuses { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<DocumentTypeTransactionNumber> DocumentTypeTransactionNumbers { get; set; }
        public DbSet<DocumentTypeTypeMap> DocumentTypeTypeMaps { get; set; }
        public DbSet<EntitlementMap> EntitlementMaps { get; set; }
        public DbSet<EntityProperty> EntityProperties { get; set; }
        public DbSet<EntityPropertyMap> EntityPropertyMaps { get; set; }
        public DbSet<EntityPropertyType> EntityPropertyTypes { get; set; }
        public DbSet<EntityTypeEntitlement> EntityTypeEntitlements { get; set; }
        public DbSet<EntityTypePaymentMethodMap> EntityTypePaymentMethodMaps { get; set; }
        public DbSet<EntityTypeRelationMap> EntityTypeRelationMaps { get; set; }
        public DbSet<EntityType> EntityTypes { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<ImageType> ImageTypes { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public DbSet<PaymentGroup> PaymentGroups { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<PaymentMethodSiteMap> PaymentMethodSiteMaps { get; set; }
        public DbSet<SeoMetadataCultureData> SeoMetadataCultureDatas { get; set; }
        public DbSet<SeoMetadata> SeoMetadatas { get; set; }
        public DbSet<SiteCountryMaps1> SiteCountryMaps1 { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<StatusesCultureData> StatusesCultureDatas { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierStatus> SupplierStatuses { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<VehicleOwnershipType> VehicleOwnershipTypes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<WarehouseStatus> WarehouseStatuses { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<AlertStatus> AlertStatuses { get; set; }
        public DbSet<AlertType> AlertTypes { get; set; }
        public DbSet<EmailNotificationData> EmailNotificationDatas { get; set; }
        public DbSet<EmailNotificationType> EmailNotificationTypes { get; set; }
        public DbSet<NotificationAlert> NotificationAlerts { get; set; }
        public DbSet<NotificationLog> NotificationLogs { get; set; }
        public DbSet<NotificationQueueParentMap> NotificationQueueParentMaps { get; set; }
        public DbSet<NotificationsProcessing> NotificationsProcessings { get; set; }
        public DbSet<NotificationsQueue> NotificationsQueues { get; set; }
        public DbSet<NotificationStatus> NotificationStatuses { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }
        public DbSet<SMSNotificationData> SMSNotificationDatas { get; set; }
        public DbSet<SMSNotificationType> SMSNotificationTypes { get; set; }
        public DbSet<OrderContactMap> OrderContactMaps { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<EmployeeBankDetail> EmployeeBankDetails { get; set; }
        public DbSet<EmployeeRoleMap> EmployeeRoleMaps { get; set; }
        public DbSet<EmployeeRole> EmployeeRoles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<JobType> JobTypes { get; set; }
        public DbSet<SalaryMethod> SalaryMethods { get; set; }
        public DbSet<ExtraTimeType> ExtraTimeTypes { get; set; }
        public DbSet<PricingType> PricingTypes { get; set; }
        public DbSet<ServiceAvailable> ServiceAvailables { get; set; }
        public DbSet<ServiceEmployeeMap> ServiceEmployeeMaps { get; set; }
        public DbSet<ServiceGroup> ServiceGroups { get; set; }
        public DbSet<ServicePricing> ServicePricings { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<TreatmentGroup> TreatmentGroups { get; set; }
        public DbSet<TreatmentType> TreatmentTypes { get; set; }
        public DbSet<EntityScheduler> EntitySchedulers { get; set; }
        public DbSet<SchedulerEntityType> SchedulerEntityTypes { get; set; }
        public DbSet<SchedulerType> SchedulerTypes { get; set; }
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<CultureData> CultureDatas { get; set; }
        public DbSet<DataFormat> DataFormats { get; set; }
        public DbSet<DataFormatType> DataFormatTypes { get; set; }
        public DbSet<DataHistoryEntity> DataHistoryEntities { get; set; }
        public DbSet<DataType> DataTypes { get; set; }
        public DbSet<FilterColumnConditionMap> FilterColumnConditionMaps { get; set; }
        public DbSet<FilterColumn> FilterColumns { get; set; }
        public DbSet<FilterColumnUserValue> FilterColumnUserValues { get; set; }
        public DbSet<GlobalSetting> GlobalSettings { get; set; }
        public DbSet<Lookup> Lookups { get; set; }
        public DbSet<MenuLink> MenuLinks { get; set; }
        public DbSet<MenuLinkType> MenuLinkTypes { get; set; }
        public DbSet<ScreenLookupMap> ScreenLookupMaps { get; set; }
        public DbSet<ScreenMetadata> ScreenMetadatas { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Sites1> Sites1 { get; set; }
        public DbSet<UIControlType> UIControlTypes { get; set; }
        public DbSet<UIControlValidation> UIControlValidations { get; set; }
        public DbSet<UserDataFormatMap> UserDataFormatMaps { get; set; }
        public DbSet<UserViewColumnMap> UserViewColumnMaps { get; set; }
        public DbSet<UserView> UserViews { get; set; }
        public DbSet<ViewColumn> ViewColumns { get; set; }
        public DbSet<View> Views { get; set; }
        public DbSet<ViewType> ViewTypes { get; set; }
        public DbSet<EntityChangeTracker> EntityChangeTrackers { get; set; }
        public DbSet<EntityChangeTrackerBatch> EntityChangeTrackerBatches { get; set; }
        public DbSet<EntityChangeTrackerLog> EntityChangeTrackerLogs { get; set; }
        public DbSet<EntityChangeTrackersInProcess> EntityChangeTrackersInProcesses { get; set; }
        public DbSet<EntityChangeTrackersQueue> EntityChangeTrackersQueues { get; set; }
        public DbSet<OperationType> OperationTypes { get; set; }
        public DbSet<SyncEntity> SyncEntities { get; set; }
        public DbSet<SyncFieldMap> SyncFieldMaps { get; set; }
        public DbSet<SyncFieldMapType> SyncFieldMapTypes { get; set; }
        public DbSet<TrackerStatus> TrackerStatuses { get; set; }
        public DbSet<ClaimSearchView> ClaimSearchViews { get; set; }
        public DbSet<ClaimSetSearchView> ClaimSetSearchViews { get; set; }
        public DbSet<CategoryCulture> CategoryCultures { get; set; }
        public DbSet<CategoryPriceSettingSearchView> CategoryPriceSettingSearchViews { get; set; }
        public DbSet<PriceSearchView> PriceSearchViews { get; set; }
        public DbSet<PriceSummaryView> PriceSummaryViews { get; set; }
        public DbSet<ProductCategorySearchView> ProductCategorySearchViews { get; set; }
        public DbSet<ProductDeliverySettingSearchView> ProductDeliverySettingSearchViews { get; set; }
        public DbSet<ProductDeliverySummaryView> ProductDeliverySummaryViews { get; set; }
        public DbSet<ProductPriceSettingSearchView> ProductPriceSettingSearchViews { get; set; }
        public DbSet<ProductQuickSearch> ProductQuickSearches { get; set; }
        public DbSet<ProductSearchSummaryView> ProductSearchSummaryViews { get; set; }
        public DbSet<ProductSearchView> ProductSearchViews { get; set; }
        public DbSet<ProductSettingSummaryView> ProductSettingSummaryViews { get; set; }
        public DbSet<ProductSKUMapSKUNamesView> ProductSKUMapSKUNamesViews { get; set; }
        public DbSet<ProductSkuSearchView> ProductSkuSearchViews { get; set; }
        public DbSet<ProductSKUVariant> ProductSKUVariants { get; set; }
        public DbSet<ProductSKUVariantsCulture> ProductSKUVariantsCultures { get; set; }
        public DbSet<ProductSummaryView> ProductSummaryViews { get; set; }
        public DbSet<PropertySearchView> PropertySearchViews { get; set; }
        public DbSet<PropertyTypeSearchView> PropertyTypeSearchViews { get; set; }
        public DbSet<SKUSearchView> SKUSearchViews { get; set; }
        public DbSet<SKUTagSearchView> SKUTagSearchViews { get; set; }
        public DbSet<SupplierProductPriceSettingSearchView> SupplierProductPriceSettingSearchViews { get; set; }
        public DbSet<SupplierProductSearchView> SupplierProductSearchViews { get; set; }
        public DbSet<SupplierProductSKUSearchView> SupplierProductSKUSearchViews { get; set; }
        public DbSet<SupplierProductSKUView> SupplierProductSKUViews { get; set; }
        public DbSet<SupplierProductSummaryView> SupplierProductSummaryViews { get; set; }
        public DbSet<SupplierProductView> SupplierProductViews { get; set; }
        public DbSet<BannerSearchView> BannerSearchViews { get; set; }
        public DbSet<BolierPlateSearchView> BolierPlateSearchViews { get; set; }
        public DbSet<BrandPriceSettingSearchView> BrandPriceSettingSearchViews { get; set; }
        public DbSet<BrandView> BrandViews { get; set; }
        public DbSet<NewsSearchView> NewsSearchViews { get; set; }
        public DbSet<PageView> PageViews { get; set; }
        public DbSet<StaticContentView> StaticContentViews { get; set; }
        public DbSet<ServiceSearch> ServiceSearches { get; set; }
        public DbSet<DataFeedDetailView> DataFeedDetailViews { get; set; }
        public DbSet<DataFeedSearchView> DataFeedSearchViews { get; set; }
        public DbSet<DataFeedTypesView> DataFeedTypesViews { get; set; }
        public DbSet<CustomerGroupDeliverySetting> CustomerGroupDeliverySettings { get; set; }
        public DbSet<PurchaseInvoiceSearchView> PurchaseInvoiceSearchViews { get; set; }
        public DbSet<PurchaseOrderSearchView> PurchaseOrderSearchViews { get; set; }
        public DbSet<SKUDeliverySetting> SKUDeliverySettings { get; set; }
        public DbSet<AccountDocumentTypesView> AccountDocumentTypesViews { get; set; }
        public DbSet<BranchView> BranchViews { get; set; }
        public DbSet<CitySearchView> CitySearchViews { get; set; }
        public DbSet<CustomerContactsSearchView> CustomerContactsSearchViews { get; set; }
        public DbSet<CustomerGroupSearchView> CustomerGroupSearchViews { get; set; }
        public DbSet<LoginSearchView> LoginSearchViews { get; set; }
        public DbSet<StockDocumentTypesView> StockDocumentTypesViews { get; set; }
        public DbSet<SupplierSearchView> SupplierSearchViews { get; set; }
        public DbSet<VehicleSearchView> VehicleSearchViews { get; set; }
        public DbSet<WarehouseDocumentTypesView> WarehouseDocumentTypesViews { get; set; }
        public DbSet<ZoneSearchView> ZoneSearchViews { get; set; }
        public DbSet<EmailTemplateView> EmailTemplateViews { get; set; }
        public DbSet<NotificationAlertView> NotificationAlertViews { get; set; }
        public DbSet<NotificationView> NotificationViews { get; set; }
        public DbSet<ServiceSearch1> ServiceSearch1 { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AccountBehavoirMap());
            modelBuilder.Configurations.Add(new AccountMap());
            modelBuilder.Configurations.Add(new AccountTransactionDetailMap());
            modelBuilder.Configurations.Add(new AccountTransactionHeadAccountMapMap());
            modelBuilder.Configurations.Add(new AccountTransactionHeadMap());
            modelBuilder.Configurations.Add(new AccountTransactionMap());
            modelBuilder.Configurations.Add(new ChartOfAccountMapMap());
            modelBuilder.Configurations.Add(new ChartOfAccountMap());
            modelBuilder.Configurations.Add(new ChartRowTypeMap());
            modelBuilder.Configurations.Add(new CostCenterMap());
            modelBuilder.Configurations.Add(new CustomerAccountMapMap());
            modelBuilder.Configurations.Add(new GroupMap());
            modelBuilder.Configurations.Add(new PayableMap());
            modelBuilder.Configurations.Add(new ReceivableMap());
            modelBuilder.Configurations.Add(new SupplierAccountMapMap());
            modelBuilder.Configurations.Add(new ClaimLoginMapMap());
            modelBuilder.Configurations.Add(new ClaimMap());
            modelBuilder.Configurations.Add(new ClaimSetClaimMapMap());
            modelBuilder.Configurations.Add(new ClaimSetClaimSetMapMap());
            modelBuilder.Configurations.Add(new ClaimSetLoginMapMap());
            modelBuilder.Configurations.Add(new ClaimSetMap());
            modelBuilder.Configurations.Add(new ClaimTypeMap());
            modelBuilder.Configurations.Add(new LoginRoleMapMap());
            modelBuilder.Configurations.Add(new LoginMap());
            modelBuilder.Configurations.Add(new PermissionCultureDataMap());
            modelBuilder.Configurations.Add(new PermissionMap());
            modelBuilder.Configurations.Add(new RoleCultureDataMap());
            modelBuilder.Configurations.Add(new RolePermissionMapMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new ActivityMap());
            modelBuilder.Configurations.Add(new ActivityTypeMap());
            modelBuilder.Configurations.Add(new AssetCategoryMap());
            modelBuilder.Configurations.Add(new AssetMap());
            modelBuilder.Configurations.Add(new AssetTransactionDetailMap());
            modelBuilder.Configurations.Add(new AssetTransactionHeadMap());
            modelBuilder.Configurations.Add(new AssetTransactionHeadAccountMapMap());
            modelBuilder.Configurations.Add(new BrandCultureDataMap());
            modelBuilder.Configurations.Add(new BrandImageMapMap());
            modelBuilder.Configurations.Add(new BrandMap());
            modelBuilder.Configurations.Add(new BrandStatusMap());
            modelBuilder.Configurations.Add(new BrandTagMapMap());
            modelBuilder.Configurations.Add(new BrandTagMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new CategoryCultureDataMap());
            modelBuilder.Configurations.Add(new CategoryImageMapMap());
            modelBuilder.Configurations.Add(new CategorySettingMap());
            modelBuilder.Configurations.Add(new CategoryTagMapMap());
            modelBuilder.Configurations.Add(new CategoryTagMap());
            modelBuilder.Configurations.Add(new CustomerProductReferenceMap());
            modelBuilder.Configurations.Add(new DeliveryTypeMap());
            modelBuilder.Configurations.Add(new EmployeeCatalogRelationMap());
            modelBuilder.Configurations.Add(new PackingTypeMap());
            modelBuilder.Configurations.Add(new ProductBundleMap());
            modelBuilder.Configurations.Add(new ProductCategoryMapMap());
            modelBuilder.Configurations.Add(new ProductCultureDataMap());
            modelBuilder.Configurations.Add(new ProductFamilyMap());
            modelBuilder.Configurations.Add(new ProductFamilyCultureDataMap());
            modelBuilder.Configurations.Add(new ProductFamilyPropertyMapMap());
            modelBuilder.Configurations.Add(new ProductFamilyPropertyTypeMapMap());
            modelBuilder.Configurations.Add(new ProductFamilyTypeMap());
            modelBuilder.Configurations.Add(new ProductImageMapMap());
            modelBuilder.Configurations.Add(new ProductInventoryConfigMap());
            modelBuilder.Configurations.Add(new ProductInventoryConfigCultureDataMap());
            modelBuilder.Configurations.Add(new ProductInventoryConfigTempMap());
            modelBuilder.Configurations.Add(new ProductInventoryProductConfigMapMap());
            modelBuilder.Configurations.Add(new ProductInventorySKUConfigMapMap());
            modelBuilder.Configurations.Add(new ProductPriceListBranchMapMap());
            modelBuilder.Configurations.Add(new ProductPriceListBrandMapMap());
            modelBuilder.Configurations.Add(new ProductPriceListCategoryMapMap());
            modelBuilder.Configurations.Add(new ProductPriceListCustomerGroupMapMap());
            modelBuilder.Configurations.Add(new ProductPriceListCustomerMapMap());
            modelBuilder.Configurations.Add(new ProductPriceListLevelMap());
            modelBuilder.Configurations.Add(new ProductPriceListProductMapMap());
            modelBuilder.Configurations.Add(new ProductPriceListProductQuantityMapMap());
            modelBuilder.Configurations.Add(new ProductPriceListMap());
            modelBuilder.Configurations.Add(new ProductPriceListSKUMapMap());
            modelBuilder.Configurations.Add(new ProductPriceListSKUQuantityMapMap());
            modelBuilder.Configurations.Add(new ProductPriceListTypeMap());
            modelBuilder.Configurations.Add(new ProductPropertyMapCultureDataMap());
            modelBuilder.Configurations.Add(new ProductPropertyMapMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new ProductSKUCultureDataMap());
            modelBuilder.Configurations.Add(new ProductSKUMapMap());
            modelBuilder.Configurations.Add(new ProductSKUSiteMapMap());
            modelBuilder.Configurations.Add(new ProductSKUTagMapMap());
            modelBuilder.Configurations.Add(new ProductSKUTagMap());
            modelBuilder.Configurations.Add(new ProductStatuMap());
            modelBuilder.Configurations.Add(new ProductStatusCultureDataMap());
            modelBuilder.Configurations.Add(new ProductTagMapMap());
            modelBuilder.Configurations.Add(new ProductTagMap());
            modelBuilder.Configurations.Add(new ProductToProductMapMap());
            modelBuilder.Configurations.Add(new ProductTypeMap());
            modelBuilder.Configurations.Add(new ProductVideoMapMap());
            modelBuilder.Configurations.Add(new PropertyMap());
            modelBuilder.Configurations.Add(new PropertyCultureDataMap());
            modelBuilder.Configurations.Add(new PropertyTypeCultureDataMap());
            modelBuilder.Configurations.Add(new PropertyTypeMap());
            modelBuilder.Configurations.Add(new RelationTypeMap());
            modelBuilder.Configurations.Add(new SalesRelationshipTypeMap());
            modelBuilder.Configurations.Add(new ShoppingCartMap());
            modelBuilder.Configurations.Add(new UnitGroupMap());
            modelBuilder.Configurations.Add(new UnitMap());
            modelBuilder.Configurations.Add(new BannerMap());
            modelBuilder.Configurations.Add(new BannerStatusMap());
            modelBuilder.Configurations.Add(new BannerTypeMap());
            modelBuilder.Configurations.Add(new BoilerPlateParameterMap());
            modelBuilder.Configurations.Add(new BoilerPlateMap());
            modelBuilder.Configurations.Add(new CategoryPageBoilerPlatMapMap());
            modelBuilder.Configurations.Add(new CustomerJustAskMap());
            modelBuilder.Configurations.Add(new CustomerSupportTicketMap());
            modelBuilder.Configurations.Add(new DeliveryTypeCategoryMasterMap());
            modelBuilder.Configurations.Add(new DeliveryTypeMasterMap());
            modelBuilder.Configurations.Add(new KnowHowOptionMap());
            modelBuilder.Configurations.Add(new NewsMap());
            modelBuilder.Configurations.Add(new NewsletterSubscriptionMap());
            modelBuilder.Configurations.Add(new NewsTypeMap());
            modelBuilder.Configurations.Add(new PageBoilerplateMapParameterCultureDataMap());
            modelBuilder.Configurations.Add(new PageBoilerplateMapParameterMap());
            modelBuilder.Configurations.Add(new PageBoilerplateMapMap());
            modelBuilder.Configurations.Add(new PageMap());
            modelBuilder.Configurations.Add(new PageTypeMap());
            modelBuilder.Configurations.Add(new SiteCountryMapMap());
            modelBuilder.Configurations.Add(new SiteMap());
            modelBuilder.Configurations.Add(new StaticContentDataMap());
            modelBuilder.Configurations.Add(new StaticContentTypeMap());
            modelBuilder.Configurations.Add(new SubscriptionMap());
            modelBuilder.Configurations.Add(new TimeSlotMasterMap());
            modelBuilder.Configurations.Add(new TimeSlotOverRiderMap());
            modelBuilder.Configurations.Add(new UserJobApplicationMap());
            modelBuilder.Configurations.Add(new OrderDeliveryDisplayHeadMapMap());
            modelBuilder.Configurations.Add(new OrderDeliveryHolidayDetailMap());
            modelBuilder.Configurations.Add(new OrderDeliveryHolidayHeadMap());
            modelBuilder.Configurations.Add(new ReceivingMethodMap());
            modelBuilder.Configurations.Add(new ReturnMethodMap());
            modelBuilder.Configurations.Add(new RouteMap());
            modelBuilder.Configurations.Add(new ServiceProviderCountryGroupMap());
            modelBuilder.Configurations.Add(new ServiceProviderLogMap());
            modelBuilder.Configurations.Add(new ServiceProviderMap());
            modelBuilder.Configurations.Add(new DocumentFileMap());
            modelBuilder.Configurations.Add(new DocumentFileStatusMap());
            modelBuilder.Configurations.Add(new DataFeedLogDetailMap());
            modelBuilder.Configurations.Add(new DataFeedLogMap());
            modelBuilder.Configurations.Add(new DataFeedOperationMap());
            modelBuilder.Configurations.Add(new DataFeedStatusMap());
            modelBuilder.Configurations.Add(new DataFeedTableColumnMap());
            modelBuilder.Configurations.Add(new DataFeedTableMap());
            modelBuilder.Configurations.Add(new DataFeedTypeMap());
            modelBuilder.Configurations.Add(new BranchDocumentTypeMapMap());
            modelBuilder.Configurations.Add(new CustomerGroupDeliveryTypeMapMap());
            modelBuilder.Configurations.Add(new DeliveryChargeMap());
            modelBuilder.Configurations.Add(new DeliveryDurationMap());
            modelBuilder.Configurations.Add(new DeliveryTypeAllowedAreaMapMap());
            modelBuilder.Configurations.Add(new DeliveryTypeAllowedCountryMapMap());
            modelBuilder.Configurations.Add(new DeliveryTypeAllowedZoneMapMap());
            modelBuilder.Configurations.Add(new DeliveryTypes1Map());
            modelBuilder.Configurations.Add(new DeliveryTypeStatusMap());
            modelBuilder.Configurations.Add(new DeliveryTypeTimeSlotMapMap());
            modelBuilder.Configurations.Add(new DeliveryTypeTimeSlotMapsCultureMap());
            modelBuilder.Configurations.Add(new InventoryVerificationMap());
            modelBuilder.Configurations.Add(new InvetoryTransactionMap());
            modelBuilder.Configurations.Add(new LocationMap());
            modelBuilder.Configurations.Add(new LocationTypeMap());
            modelBuilder.Configurations.Add(new NotifyMap());
            modelBuilder.Configurations.Add(new ProductDeliveryCountrySettingMap());
            modelBuilder.Configurations.Add(new ProductDeliveryTypeMapMap());
            modelBuilder.Configurations.Add(new ProductInventoryMap());
            modelBuilder.Configurations.Add(new ProductInventories_BakMap());
            modelBuilder.Configurations.Add(new ProductInventorySerialMapMap());
            modelBuilder.Configurations.Add(new ProductLocationMapMap());
            modelBuilder.Configurations.Add(new ProductSerialMapMap());
            modelBuilder.Configurations.Add(new ProductTypeDeliveryTypeMapMap());
            modelBuilder.Configurations.Add(new ShoppingCartItemMap());
            modelBuilder.Configurations.Add(new ShoppingCart1Map());
            modelBuilder.Configurations.Add(new ShoppingCartVoucherMapMap());
            modelBuilder.Configurations.Add(new SKUPaymentMethodExceptionMapMap());
            modelBuilder.Configurations.Add(new TransactionAllocationMap());
            modelBuilder.Configurations.Add(new TransactionDetailMap());
            modelBuilder.Configurations.Add(new TransactionHeadMap());
            modelBuilder.Configurations.Add(new TransactionHeadAccountMapMap());
            modelBuilder.Configurations.Add(new TransactionHeadEntitlementMapMap());
            modelBuilder.Configurations.Add(new TransactionHeadPayablesMapMap());
            modelBuilder.Configurations.Add(new TransactionHeadPointsMapMap());
            modelBuilder.Configurations.Add(new TransactionHeadReceivablesMapMap());
            modelBuilder.Configurations.Add(new TransactionHeadShoppingCartMapMap());
            modelBuilder.Configurations.Add(new TransactionShipmentMap());
            modelBuilder.Configurations.Add(new TransactionStatusMap());
            modelBuilder.Configurations.Add(new VoucherMap());
            modelBuilder.Configurations.Add(new VoucherStatusMap());
            modelBuilder.Configurations.Add(new VoucherTypeMap());
            modelBuilder.Configurations.Add(new WishListMap());
            modelBuilder.Configurations.Add(new BasketMap());
            modelBuilder.Configurations.Add(new JobActivityMap());
            modelBuilder.Configurations.Add(new JobEntryDetailMap());
            modelBuilder.Configurations.Add(new JobEntryHeadMap());
            modelBuilder.Configurations.Add(new JobOperationStatusMap());
            modelBuilder.Configurations.Add(new JobsEntryHeadPayableMapMap());
            modelBuilder.Configurations.Add(new JobsEntryHeadReceivableMapMap());
            modelBuilder.Configurations.Add(new JobSizeMap());
            modelBuilder.Configurations.Add(new JobStatusMap());
            modelBuilder.Configurations.Add(new PriorityMap());
            modelBuilder.Configurations.Add(new ActionLinkTypeMap());
            modelBuilder.Configurations.Add(new AreaMap());
            modelBuilder.Configurations.Add(new BranchMap());
            modelBuilder.Configurations.Add(new BranchGroupMap());
            modelBuilder.Configurations.Add(new BranchGroupStatusMap());
            modelBuilder.Configurations.Add(new BranchStatusMap());
            modelBuilder.Configurations.Add(new CityMap());
            modelBuilder.Configurations.Add(new CommentMap());
            modelBuilder.Configurations.Add(new CompanyMap());
            modelBuilder.Configurations.Add(new CompanyCurrencyMapMap());
            modelBuilder.Configurations.Add(new CompanyStatusMap());
            modelBuilder.Configurations.Add(new ContactMap());
            modelBuilder.Configurations.Add(new CountryMap());
            modelBuilder.Configurations.Add(new CultureMap());
            modelBuilder.Configurations.Add(new CurrencyMap());
            modelBuilder.Configurations.Add(new CustomerGroupMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new CustomerSettingMap());
            modelBuilder.Configurations.Add(new CustomerStatusMap());
            modelBuilder.Configurations.Add(new CustomerSupplierMapMap());
            modelBuilder.Configurations.Add(new DepartmentMap());
            modelBuilder.Configurations.Add(new DepartmentStatusMap());
            modelBuilder.Configurations.Add(new DocumentReferenceStatusMapMap());
            modelBuilder.Configurations.Add(new DocumentReferenceTypeMap());
            modelBuilder.Configurations.Add(new DocumentStatusMap());
            modelBuilder.Configurations.Add(new DocumentTypeMap());
            modelBuilder.Configurations.Add(new DocumentTypeTransactionNumberMap());
            modelBuilder.Configurations.Add(new DocumentTypeTypeMapMap());
            modelBuilder.Configurations.Add(new EntitlementMapMap());
            modelBuilder.Configurations.Add(new EntityPropertyMap());
            modelBuilder.Configurations.Add(new EntityPropertyMapMap());
            modelBuilder.Configurations.Add(new EntityPropertyTypeMap());
            modelBuilder.Configurations.Add(new EntityTypeEntitlementMap());
            modelBuilder.Configurations.Add(new EntityTypePaymentMethodMapMap());
            modelBuilder.Configurations.Add(new EntityTypeRelationMapMap());
            modelBuilder.Configurations.Add(new EntityTypeMap());
            modelBuilder.Configurations.Add(new GenderMap());
            modelBuilder.Configurations.Add(new ImageTypeMap());
            modelBuilder.Configurations.Add(new LanguageMap());
            modelBuilder.Configurations.Add(new MaritalStatusMap());
            modelBuilder.Configurations.Add(new PaymentGroupMap());
            modelBuilder.Configurations.Add(new PaymentMethodMap());
            modelBuilder.Configurations.Add(new PaymentMethodSiteMapMap());
            modelBuilder.Configurations.Add(new SeoMetadataCultureDataMap());
            modelBuilder.Configurations.Add(new SeoMetadataMap());
            modelBuilder.Configurations.Add(new SiteCountryMaps1Map());
            modelBuilder.Configurations.Add(new StatusMap());
            modelBuilder.Configurations.Add(new StatusesCultureDataMap());
            modelBuilder.Configurations.Add(new SupplierMap());
            modelBuilder.Configurations.Add(new SupplierStatusMap());
            modelBuilder.Configurations.Add(new TitleMap());
            modelBuilder.Configurations.Add(new VehicleOwnershipTypeMap());
            modelBuilder.Configurations.Add(new VehicleMap());
            modelBuilder.Configurations.Add(new VehicleTypeMap());
            modelBuilder.Configurations.Add(new WarehouseMap());
            modelBuilder.Configurations.Add(new WarehouseStatusMap());
            modelBuilder.Configurations.Add(new ZoneMap());
            modelBuilder.Configurations.Add(new AlertStatusMap());
            modelBuilder.Configurations.Add(new AlertTypeMap());
            modelBuilder.Configurations.Add(new EmailNotificationDataMap());
            modelBuilder.Configurations.Add(new EmailNotificationTypeMap());
            modelBuilder.Configurations.Add(new NotificationAlertMap());
            modelBuilder.Configurations.Add(new NotificationLogMap());
            modelBuilder.Configurations.Add(new NotificationQueueParentMapMap());
            modelBuilder.Configurations.Add(new NotificationsProcessingMap());
            modelBuilder.Configurations.Add(new NotificationsQueueMap());
            modelBuilder.Configurations.Add(new NotificationStatusMap());
            modelBuilder.Configurations.Add(new NotificationTypeMap());
            modelBuilder.Configurations.Add(new SMSNotificationDataMap());
            modelBuilder.Configurations.Add(new SMSNotificationTypeMap());
            modelBuilder.Configurations.Add(new OrderContactMapMap());
            modelBuilder.Configurations.Add(new DesignationMap());
            modelBuilder.Configurations.Add(new EmployeeBankDetailMap());
            modelBuilder.Configurations.Add(new EmployeeRoleMapMap());
            modelBuilder.Configurations.Add(new EmployeeRoleMap());
            modelBuilder.Configurations.Add(new EmployeeMap());
            modelBuilder.Configurations.Add(new JobTypeMap());
            modelBuilder.Configurations.Add(new SalaryMethodMap());
            modelBuilder.Configurations.Add(new ExtraTimeTypeMap());
            modelBuilder.Configurations.Add(new PricingTypeMap());
            modelBuilder.Configurations.Add(new ServiceAvailableMap());
            modelBuilder.Configurations.Add(new ServiceEmployeeMapMap());
            modelBuilder.Configurations.Add(new ServiceGroupMap());
            modelBuilder.Configurations.Add(new ServicePricingMap());
            modelBuilder.Configurations.Add(new ServiceMap());
            modelBuilder.Configurations.Add(new TreatmentGroupMap());
            modelBuilder.Configurations.Add(new TreatmentTypeMap());
            modelBuilder.Configurations.Add(new EntitySchedulerMap());
            modelBuilder.Configurations.Add(new SchedulerEntityTypeMap());
            modelBuilder.Configurations.Add(new SchedulerTypeMap());
            modelBuilder.Configurations.Add(new ConditionMap());
            modelBuilder.Configurations.Add(new CultureDataMap());
            modelBuilder.Configurations.Add(new DataFormatMap());
            modelBuilder.Configurations.Add(new DataFormatTypeMap());
            modelBuilder.Configurations.Add(new DataHistoryEntityMap());
            modelBuilder.Configurations.Add(new DataTypeMap());
            modelBuilder.Configurations.Add(new FilterColumnConditionMapMap());
            modelBuilder.Configurations.Add(new FilterColumnMap());
            modelBuilder.Configurations.Add(new FilterColumnUserValueMap());
            modelBuilder.Configurations.Add(new GlobalSettingMap());
            modelBuilder.Configurations.Add(new LookupMap());
            modelBuilder.Configurations.Add(new MenuLinkMap());
            modelBuilder.Configurations.Add(new MenuLinkTypeMap());
            modelBuilder.Configurations.Add(new ScreenLookupMapMap());
            modelBuilder.Configurations.Add(new ScreenMetadataMap());
            modelBuilder.Configurations.Add(new SettingMap());
            modelBuilder.Configurations.Add(new Sites1Map());
            modelBuilder.Configurations.Add(new UIControlTypeMap());
            modelBuilder.Configurations.Add(new UIControlValidationMap());
            modelBuilder.Configurations.Add(new UserDataFormatMapMap());
            modelBuilder.Configurations.Add(new UserViewColumnMapMap());
            modelBuilder.Configurations.Add(new UserViewMap());
            modelBuilder.Configurations.Add(new ViewColumnMap());
            modelBuilder.Configurations.Add(new ViewMap());
            modelBuilder.Configurations.Add(new ViewTypeMap());
            modelBuilder.Configurations.Add(new EntityChangeTrackerMap());
            modelBuilder.Configurations.Add(new EntityChangeTrackerBatchMap());
            modelBuilder.Configurations.Add(new EntityChangeTrackerLogMap());
            modelBuilder.Configurations.Add(new EntityChangeTrackersInProcessMap());
            modelBuilder.Configurations.Add(new EntityChangeTrackersQueueMap());
            modelBuilder.Configurations.Add(new OperationTypeMap());
            modelBuilder.Configurations.Add(new SyncEntityMap());
            modelBuilder.Configurations.Add(new SyncFieldMapMap());
            modelBuilder.Configurations.Add(new SyncFieldMapTypeMap());
            modelBuilder.Configurations.Add(new TrackerStatusMap());
            modelBuilder.Configurations.Add(new ClaimSearchViewMap());
            modelBuilder.Configurations.Add(new ClaimSetSearchViewMap());
            modelBuilder.Configurations.Add(new CategoryCultureMap());
            modelBuilder.Configurations.Add(new CategoryPriceSettingSearchViewMap());
            modelBuilder.Configurations.Add(new PriceSearchViewMap());
            modelBuilder.Configurations.Add(new PriceSummaryViewMap());
            modelBuilder.Configurations.Add(new ProductCategorySearchViewMap());
            modelBuilder.Configurations.Add(new ProductDeliverySettingSearchViewMap());
            modelBuilder.Configurations.Add(new ProductDeliverySummaryViewMap());
            modelBuilder.Configurations.Add(new ProductPriceSettingSearchViewMap());
            modelBuilder.Configurations.Add(new ProductQuickSearchMap());
            modelBuilder.Configurations.Add(new ProductSearchSummaryViewMap());
            modelBuilder.Configurations.Add(new ProductSearchViewMap());
            modelBuilder.Configurations.Add(new ProductSettingSummaryViewMap());
            modelBuilder.Configurations.Add(new ProductSKUMapSKUNamesViewMap());
            modelBuilder.Configurations.Add(new ProductSkuSearchViewMap());
            modelBuilder.Configurations.Add(new ProductSKUVariantMap());
            modelBuilder.Configurations.Add(new ProductSKUVariantsCultureMap());
            modelBuilder.Configurations.Add(new ProductSummaryViewMap());
            modelBuilder.Configurations.Add(new PropertySearchViewMap());
            modelBuilder.Configurations.Add(new PropertyTypeSearchViewMap());
            modelBuilder.Configurations.Add(new SKUSearchViewMap());
            modelBuilder.Configurations.Add(new SKUTagSearchViewMap());
            modelBuilder.Configurations.Add(new SupplierProductPriceSettingSearchViewMap());
            modelBuilder.Configurations.Add(new SupplierProductSearchViewMap());
            modelBuilder.Configurations.Add(new SupplierProductSKUSearchViewMap());
            modelBuilder.Configurations.Add(new SupplierProductSKUViewMap());
            modelBuilder.Configurations.Add(new SupplierProductSummaryViewMap());
            modelBuilder.Configurations.Add(new SupplierProductViewMap());
            modelBuilder.Configurations.Add(new BannerSearchViewMap());
            modelBuilder.Configurations.Add(new BolierPlateSearchViewMap());
            modelBuilder.Configurations.Add(new BrandPriceSettingSearchViewMap());
            modelBuilder.Configurations.Add(new BrandViewMap());
            modelBuilder.Configurations.Add(new NewsSearchViewMap());
            modelBuilder.Configurations.Add(new PageViewMap());
            modelBuilder.Configurations.Add(new StaticContentViewMap());
            modelBuilder.Configurations.Add(new ServiceSearchMap());
            modelBuilder.Configurations.Add(new DataFeedDetailViewMap());
            modelBuilder.Configurations.Add(new DataFeedSearchViewMap());
            modelBuilder.Configurations.Add(new DataFeedTypesViewMap());
            modelBuilder.Configurations.Add(new CustomerGroupDeliverySettingMap());
            modelBuilder.Configurations.Add(new PurchaseInvoiceSearchViewMap());
            modelBuilder.Configurations.Add(new PurchaseOrderSearchViewMap());
            modelBuilder.Configurations.Add(new SKUDeliverySettingMap());
            modelBuilder.Configurations.Add(new AccountDocumentTypesViewMap());
            modelBuilder.Configurations.Add(new BranchViewMap());
            modelBuilder.Configurations.Add(new CitySearchViewMap());
            modelBuilder.Configurations.Add(new CustomerContactsSearchViewMap());
            modelBuilder.Configurations.Add(new CustomerGroupSearchViewMap());
            modelBuilder.Configurations.Add(new LoginSearchViewMap());
            modelBuilder.Configurations.Add(new StockDocumentTypesViewMap());
            modelBuilder.Configurations.Add(new SupplierSearchViewMap());
            modelBuilder.Configurations.Add(new VehicleSearchViewMap());
            modelBuilder.Configurations.Add(new WarehouseDocumentTypesViewMap());
            modelBuilder.Configurations.Add(new ZoneSearchViewMap());
            modelBuilder.Configurations.Add(new EmailTemplateViewMap());
            modelBuilder.Configurations.Add(new NotificationAlertViewMap());
            modelBuilder.Configurations.Add(new NotificationViewMap());
            modelBuilder.Configurations.Add(new ServiceSearch1Map());
        }
    }
}

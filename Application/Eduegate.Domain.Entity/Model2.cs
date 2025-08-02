using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Eduegate.Domain.Entity
{
    public partial class Model2 : DbContext
    {
        public Model2()
            : base("name=Model2")
        {
        }

        public virtual DbSet<AccountBehavoir> AccountBehavoirs { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Accounts_Reference> Accounts_Reference { get; set; }
        public virtual DbSet<Accounts_SubLedger> Accounts_SubLedger { get; set; }
        public virtual DbSet<Accounts_SubLedger_Relation> Accounts_SubLedger_Relation { get; set; }
        public virtual DbSet<AccountTaxTransaction> AccountTaxTransactions { get; set; }
        public virtual DbSet<AccountTransactionDetail> AccountTransactionDetails { get; set; }
        public virtual DbSet<AccountTransactionHeadAccountMap> AccountTransactionHeadAccountMaps { get; set; }
        public virtual DbSet<AccountTransactionHead> AccountTransactionHeads { get; set; }
        public virtual DbSet<AccountTransaction> AccountTransactions { get; set; }
        public virtual DbSet<ChartOfAccountMap> ChartOfAccountMaps { get; set; }
        public virtual DbSet<ChartOfAccount> ChartOfAccounts { get; set; }
        public virtual DbSet<ChartRowType> ChartRowTypes { get; set; }
        public virtual DbSet<Company_FiscalYear_Close> Company_FiscalYear_Close { get; set; }
        public virtual DbSet<CostCenter> CostCenters { get; set; }
        public virtual DbSet<CustomerAccountMap> CustomerAccountMaps { get; set; }
        public virtual DbSet<FiscalYear> FiscalYears { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Payable> Payables { get; set; }
        public virtual DbSet<PaymentMode> PaymentModes { get; set; }
        public virtual DbSet<Receivable> Receivables { get; set; }
        public virtual DbSet<SupplierAccountMap> SupplierAccountMaps { get; set; }
        public virtual DbSet<TenderType> TenderTypes { get; set; }
        public virtual DbSet<Tranhead> Tranheads { get; set; }
        public virtual DbSet<Trantail> Trantails { get; set; }
        public virtual DbSet<Trantail_CostCenter> Trantail_CostCenter { get; set; }
        public virtual DbSet<Trantail_Narration> Trantail_Narration { get; set; }
        public virtual DbSet<Trantail_Payment> Trantail_Payment { get; set; }
        public virtual DbSet<Trantail_SubLedger> Trantail_SubLedger { get; set; }
        public virtual DbSet<ClaimLoginMap> ClaimLoginMaps { get; set; }
        public virtual DbSet<Claim> Claims { get; set; }
        public virtual DbSet<ClaimSetClaimMap> ClaimSetClaimMaps { get; set; }
        public virtual DbSet<ClaimSetClaimSetMap> ClaimSetClaimSetMaps { get; set; }
        public virtual DbSet<ClaimSetLoginMap> ClaimSetLoginMaps { get; set; }
        public virtual DbSet<ClaimSet> ClaimSets { get; set; }
        public virtual DbSet<ClaimType> ClaimTypes { get; set; }
        public virtual DbSet<LoginRoleMap> LoginRoleMaps { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<PermissionCultureData> PermissionCultureDatas { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<RoleCultureData> RoleCultureDatas { get; set; }
        public virtual DbSet<RolePermissionMap> RolePermissionMaps { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<AssetCategory> AssetCategories { get; set; }
        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<AssetTransactionDetail> AssetTransactionDetails { get; set; }
        public virtual DbSet<AssetTransactionHead> AssetTransactionHeads { get; set; }
        public virtual DbSet<AssetTransactionHeadAccountMap> AssetTransactionHeadAccountMaps { get; set; }
        public virtual DbSet<BrandCultureData> BrandCultureDatas { get; set; }
        public virtual DbSet<BrandImageMap> BrandImageMaps { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<BrandStatus> BrandStatuses { get; set; }
        public virtual DbSet<BrandTagMap> BrandTagMaps { get; set; }
        public virtual DbSet<BrandTag> BrandTags { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryCultureData> CategoryCultureDatas { get; set; }
        public virtual DbSet<CategoryImageMap> CategoryImageMaps { get; set; }
        public virtual DbSet<CategorySetting> CategorySettings { get; set; }
        public virtual DbSet<CategoryTagMap> CategoryTagMaps { get; set; }
        public virtual DbSet<CategoryTag> CategoryTags { get; set; }
        public virtual DbSet<CustomerProductReference> CustomerProductReferences { get; set; }
        public virtual DbSet<DeliveryType> DeliveryTypes { get; set; }
        public virtual DbSet<EmployeeCatalogRelation> EmployeeCatalogRelations { get; set; }
        public virtual DbSet<MenuLinkBrandMap> MenuLinkBrandMaps { get; set; }
        public virtual DbSet<MenuLinkCategoryMap> MenuLinkCategoryMaps { get; set; }
        public virtual DbSet<PackingType> PackingTypes { get; set; }
        public virtual DbSet<ProductBundle> ProductBundles { get; set; }
        public virtual DbSet<ProductCategoryMap> ProductCategoryMaps { get; set; }
        public virtual DbSet<ProductCultureData> ProductCultureDatas { get; set; }
        public virtual DbSet<ProductFamily> ProductFamilies { get; set; }
        public virtual DbSet<ProductFamilyCultureData> ProductFamilyCultureDatas { get; set; }
        public virtual DbSet<ProductFamilyPropertyMap> ProductFamilyPropertyMaps { get; set; }
        public virtual DbSet<ProductFamilyPropertyTypeMap> ProductFamilyPropertyTypeMaps { get; set; }
        public virtual DbSet<ProductFamilyType> ProductFamilyTypes { get; set; }
        public virtual DbSet<ProductImageMap> ProductImageMaps { get; set; }
        public virtual DbSet<ProductInventoryConfig> ProductInventoryConfigs { get; set; }
        public virtual DbSet<ProductInventoryConfigCultureData> ProductInventoryConfigCultureDatas { get; set; }
        public virtual DbSet<ProductInventoryConfigTemp> ProductInventoryConfigTemps { get; set; }
        public virtual DbSet<ProductInventoryProductConfigMap> ProductInventoryProductConfigMaps { get; set; }
        public virtual DbSet<ProductInventorySKUConfigMap> ProductInventorySKUConfigMaps { get; set; }
        public virtual DbSet<ProductPriceListBranchMap> ProductPriceListBranchMaps { get; set; }
        public virtual DbSet<ProductPriceListBrandMap> ProductPriceListBrandMaps { get; set; }
        public virtual DbSet<ProductPriceListCategoryMap> ProductPriceListCategoryMaps { get; set; }
        public virtual DbSet<ProductPriceListCustomerGroupMap> ProductPriceListCustomerGroupMaps { get; set; }
        public virtual DbSet<ProductPriceListCustomerMap> ProductPriceListCustomerMaps { get; set; }
        public virtual DbSet<ProductPriceListLevel> ProductPriceListLevels { get; set; }
        public virtual DbSet<ProductPriceListProductMap> ProductPriceListProductMaps { get; set; }
        public virtual DbSet<ProductPriceListProductQuantityMap> ProductPriceListProductQuantityMaps { get; set; }
        public virtual DbSet<ProductPriceList> ProductPriceLists { get; set; }
        public virtual DbSet<ProductPriceListSKUMap> ProductPriceListSKUMaps { get; set; }
        public virtual DbSet<ProductPriceListSKUQuantityMap> ProductPriceListSKUQuantityMaps { get; set; }
        public virtual DbSet<ProductPriceListType> ProductPriceListTypes { get; set; }
        public virtual DbSet<ProductPropertyMapCultureData> ProductPropertyMapCultureDatas { get; set; }
        public virtual DbSet<ProductPropertyMap> ProductPropertyMaps { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductSKUCultureData> ProductSKUCultureDatas { get; set; }
        public virtual DbSet<ProductSKUMap> ProductSKUMaps { get; set; }
        public virtual DbSet<ProductSKURackMap> ProductSKURackMaps { get; set; }
        public virtual DbSet<ProductSKUSiteMap> ProductSKUSiteMaps { get; set; }
        public virtual DbSet<ProductSKUTagMap> ProductSKUTagMaps { get; set; }
        public virtual DbSet<ProductSKUTag> ProductSKUTags { get; set; }
        public virtual DbSet<ProductStatu> ProductStatus { get; set; }
        public virtual DbSet<ProductStatusCultureData> ProductStatusCultureDatas { get; set; }
        public virtual DbSet<ProductTagMap> ProductTagMaps { get; set; }
        public virtual DbSet<ProductTag> ProductTags { get; set; }
        public virtual DbSet<ProductToProductMap> ProductToProductMaps { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<ProductVideoMap> ProductVideoMaps { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<PropertyCultureData> PropertyCultureDatas { get; set; }
        public virtual DbSet<PropertyTypeCultureData> PropertyTypeCultureDatas { get; set; }
        public virtual DbSet<PropertyType> PropertyTypes { get; set; }
        public virtual DbSet<Rack> Racks { get; set; }
        public virtual DbSet<RelationType> RelationTypes { get; set; }
        public virtual DbSet<SalesRelationshipType> SalesRelationshipTypes { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<UnitGroup> UnitGroups { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<Banner> Banners { get; set; }
        public virtual DbSet<BannerStatus> BannerStatuses { get; set; }
        public virtual DbSet<BannerType> BannerTypes { get; set; }
        public virtual DbSet<BoilerPlateParameter> BoilerPlateParameters { get; set; }
        public virtual DbSet<BoilerPlate> BoilerPlates { get; set; }
        public virtual DbSet<CategoryPageBoilerPlatMap> CategoryPageBoilerPlatMaps { get; set; }
        public virtual DbSet<CustomerJustAsk> CustomerJustAsks { get; set; }
        public virtual DbSet<CustomerSupportTicket> CustomerSupportTickets { get; set; }
        public virtual DbSet<DeliveryTypeMaster> DeliveryTypeMasters { get; set; }
        public virtual DbSet<KnowHowOption> KnowHowOptions { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NewsletterSubscription> NewsletterSubscriptions { get; set; }
        public virtual DbSet<NewsType> NewsTypes { get; set; }
        public virtual DbSet<PageBoilerplateMapParameterCultureData> PageBoilerplateMapParameterCultureDatas { get; set; }
        public virtual DbSet<PageBoilerplateMapParameter> PageBoilerplateMapParameters { get; set; }
        public virtual DbSet<PageBoilerplateMap> PageBoilerplateMaps { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<PageType> PageTypes { get; set; }
        public virtual DbSet<SiteCountryMap> SiteCountryMaps { get; set; }
        public virtual DbSet<Site> Sites { get; set; }
        public virtual DbSet<StaticContentType> StaticContentTypes { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<TimeSlotMaster> TimeSlotMasters { get; set; }
        public virtual DbSet<TimeSlotOverRider> TimeSlotOverRiders { get; set; }
        public virtual DbSet<UserJobApplication> UserJobApplications { get; set; }
        public virtual DbSet<AlbumImageMap> AlbumImageMaps { get; set; }
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<AlbumType> AlbumTypes { get; set; }
        public virtual DbSet<EventAudienceMap> EventAudienceMaps { get; set; }
        public virtual DbSet<EventAudienceType> EventAudienceTypes { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<PollAnswerMap> PollAnswerMaps { get; set; }
        public virtual DbSet<Poll> Polls { get; set; }
        public virtual DbSet<QuestionnaireAnswer> QuestionnaireAnswers { get; set; }
        public virtual DbSet<QuestionnaireAnswerType> QuestionnaireAnswerTypes { get; set; }
        public virtual DbSet<Questionnaire> Questionnaires { get; set; }
        public virtual DbSet<QuestionnaireSet> QuestionnaireSets { get; set; }
        public virtual DbSet<QuestionnaireType> QuestionnaireTypes { get; set; }
        public virtual DbSet<Channel> Channels { get; set; }
        public virtual DbSet<ChannelType> ChannelTypes { get; set; }
        public virtual DbSet<CreatedByType> CreatedByTypes { get; set; }
        public virtual DbSet<EducationDetail> EducationDetails { get; set; }
        public virtual DbSet<EducationType> EducationTypes { get; set; }
        public virtual DbSet<Family> Families { get; set; }
        public virtual DbSet<Mahallu> Mahallus { get; set; }
        public virtual DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public virtual DbSet<MemberHealth> MemberHealths { get; set; }
        public virtual DbSet<MemberPartner> MemberPartners { get; set; }
        public virtual DbSet<MemberQuestionnaireAnswerMap> MemberQuestionnaireAnswerMaps { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<OccupationType> OccupationTypes { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<RelationWithHeadOfFamily> RelationWithHeadOfFamilies { get; set; }
        public virtual DbSet<SocialService> SocialServices { get; set; }
        public virtual DbSet<ContentFile> ContentFiles { get; set; }
        public virtual DbSet<ContentType> ContentTypes { get; set; }
        public virtual DbSet<Communication> Communications { get; set; }
        public virtual DbSet<CommunicationType> CommunicationTypes { get; set; }
        public virtual DbSet<CRMCompany> CRMCompanies { get; set; }
        public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }
        public virtual DbSet<IndustryType> IndustryTypes { get; set; }
        public virtual DbSet<Lead> Leads { get; set; }
        public virtual DbSet<LeadStatus> LeadStatuses { get; set; }
        public virtual DbSet<LeadType> LeadTypes { get; set; }
        public virtual DbSet<MarketSegment> MarketSegments { get; set; }
        public virtual DbSet<Opportunity> Opportunities { get; set; }
        public virtual DbSet<OpportunityFrom> OpportunityFroms { get; set; }
        public virtual DbSet<OpportunityStatus> OpportunityStatuses { get; set; }
        public virtual DbSet<OpportunityType> OpportunityTypes { get; set; }
        public virtual DbSet<RequestType> RequestTypes { get; set; }
        public virtual DbSet<Source> Sources { get; set; }
        public virtual DbSet<DocumentReferenceTicketStatusMap> DocumentReferenceTicketStatusMaps { get; set; }
        public virtual DbSet<RefundStatus> RefundStatuses { get; set; }
        public virtual DbSet<RefundType> RefundTypes { get; set; }
        public virtual DbSet<SupportAction> SupportActions { get; set; }
        public virtual DbSet<TicketActionDetailDetailMap> TicketActionDetailDetailMaps { get; set; }
        public virtual DbSet<TicketActionDetailMap> TicketActionDetailMaps { get; set; }
        public virtual DbSet<TicketBankDetail> TicketBankDetails { get; set; }
        public virtual DbSet<TicketCashDetail> TicketCashDetails { get; set; }
        public virtual DbSet<TicketPriority> TicketPriorities { get; set; }
        public virtual DbSet<TicketProcessingStatus> TicketProcessingStatuses { get; set; }
        public virtual DbSet<TicketProductMap> TicketProductMaps { get; set; }
        public virtual DbSet<TicketReason> TicketReasons { get; set; }
        public virtual DbSet<TicketRefundDetail> TicketRefundDetails { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TicketStatus> TicketStatuses { get; set; }
        public virtual DbSet<TicketTag> TicketTags { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<OrderDeliveryDisplayHeadMap> OrderDeliveryDisplayHeadMaps { get; set; }
        public virtual DbSet<OrderDeliveryHolidayDetail> OrderDeliveryHolidayDetails { get; set; }
        public virtual DbSet<OrderDeliveryHolidayHead> OrderDeliveryHolidayHeads { get; set; }
        public virtual DbSet<ReceivingMethod> ReceivingMethods { get; set; }
        public virtual DbSet<ReturnMethod> ReturnMethods { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<ServiceProviderCountryGroup> ServiceProviderCountryGroups { get; set; }
        public virtual DbSet<ServiceProviderLog> ServiceProviderLogs { get; set; }
        public virtual DbSet<ServiceProvider> ServiceProviders { get; set; }
        public virtual DbSet<DocFileType> DocFileTypes { get; set; }
        public virtual DbSet<DocumentFile> DocumentFiles { get; set; }
        public virtual DbSet<DocumentFileStatus> DocumentFileStatuses { get; set; }
        public virtual DbSet<AnswerType> AnswerTypes { get; set; }
        public virtual DbSet<CandidateAnswer> CandidateAnswers { get; set; }
        public virtual DbSet<CandidateAssesment> CandidateAssesments { get; set; }
        public virtual DbSet<CandidateOnlineExamMap> CandidateOnlineExamMaps { get; set; }
        public virtual DbSet<Candidate> Candidates { get; set; }
        public virtual DbSet<OnlineExamOperationStatus> OnlineExamOperationStatuses { get; set; }
        public virtual DbSet<OnlineExam> OnlineExams { get; set; }
        public virtual DbSet<OnlineExamStatus> OnlineExamStatuses { get; set; }
        public virtual DbSet<QuestionAnswerMap> QuestionAnswerMaps { get; set; }
        public virtual DbSet<QuestionGroup> QuestionGroups { get; set; }
        public virtual DbSet<QuestionOptionMap> QuestionOptionMaps { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionSelection> QuestionSelections { get; set; }
        public virtual DbSet<DataFeedLogDetail> DataFeedLogDetails { get; set; }
        public virtual DbSet<DataFeedLog> DataFeedLogs { get; set; }
        public virtual DbSet<DataFeedOperation> DataFeedOperations { get; set; }
        public virtual DbSet<DataFeedStatus> DataFeedStatuses { get; set; }
        public virtual DbSet<DataFeedTableColumn> DataFeedTableColumns { get; set; }
        public virtual DbSet<DataFeedTable> DataFeedTables { get; set; }
        public virtual DbSet<DataFeedType> DataFeedTypes { get; set; }
        public virtual DbSet<ApplicationForm> ApplicationForms { get; set; }
        public virtual DbSet<AvailableJobCultureData> AvailableJobCultureDatas { get; set; }
        public virtual DbSet<AvailableJob> AvailableJobs { get; set; }
        public virtual DbSet<AvailableJobTag> AvailableJobTags { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<DepartmentTag> DepartmentTags { get; set; }
        public virtual DbSet<BranchDocumentTypeMap> BranchDocumentTypeMaps { get; set; }
        public virtual DbSet<CustomerGroupDeliveryTypeMap> CustomerGroupDeliveryTypeMaps { get; set; }
        public virtual DbSet<DeliveryCharge> DeliveryCharges { get; set; }
        public virtual DbSet<DeliveryDuration> DeliveryDurations { get; set; }
        public virtual DbSet<DeliveryTimeSlot> DeliveryTimeSlots { get; set; }
        public virtual DbSet<DeliveryTypeAllowedAreaMap> DeliveryTypeAllowedAreaMaps { get; set; }
        public virtual DbSet<DeliveryTypeAllowedCountryMap> DeliveryTypeAllowedCountryMaps { get; set; }
        public virtual DbSet<DeliveryTypeAllowedZoneMap> DeliveryTypeAllowedZoneMaps { get; set; }
        public virtual DbSet<DeliveryTypes1> DeliveryTypes1 { get; set; }
        public virtual DbSet<DeliveryTypeStatus> DeliveryTypeStatuses { get; set; }
        public virtual DbSet<DeliveryTypeTimeSlotMap> DeliveryTypeTimeSlotMaps { get; set; }
        public virtual DbSet<DeliveryTypeTimeSlotMapsCulture> DeliveryTypeTimeSlotMapsCultures { get; set; }
        public virtual DbSet<InventoryVerification> InventoryVerifications { get; set; }
        public virtual DbSet<InvetoryTransaction> InvetoryTransactions { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<LocationType> LocationTypes { get; set; }
        public virtual DbSet<Notify> Notifies { get; set; }
        public virtual DbSet<ProductDeliveryCountrySetting> ProductDeliveryCountrySettings { get; set; }
        public virtual DbSet<ProductDeliveryTypeMap> ProductDeliveryTypeMaps { get; set; }
        public virtual DbSet<ProductInventory> ProductInventories { get; set; }
        public virtual DbSet<ProductInventorySerialMap> ProductInventorySerialMaps { get; set; }
        public virtual DbSet<ProductLocationMap> ProductLocationMaps { get; set; }
        public virtual DbSet<ProductSerialMap> ProductSerialMaps { get; set; }
        public virtual DbSet<ProductTypeDeliveryTypeMap> ProductTypeDeliveryTypeMaps { get; set; }
        public virtual DbSet<ShareHolder> ShareHolders { get; set; }
        public virtual DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public virtual DbSet<ShoppingCart1> ShoppingCarts1 { get; set; }
        public virtual DbSet<SKUPaymentMethodExceptionMap> SKUPaymentMethodExceptionMaps { get; set; }
        public virtual DbSet<Tax> Taxes { get; set; }
        public virtual DbSet<TaxStatus> TaxStatuses { get; set; }
        public virtual DbSet<TaxTemplateItem> TaxTemplateItems { get; set; }
        public virtual DbSet<TaxTemplate> TaxTemplates { get; set; }
        public virtual DbSet<TaxTransaction> TaxTransactions { get; set; }
        public virtual DbSet<TaxType> TaxTypes { get; set; }
        public virtual DbSet<TransactionAllocation> TransactionAllocations { get; set; }
        public virtual DbSet<TransactionDetail> TransactionDetails { get; set; }
        public virtual DbSet<TransactionHead> TransactionHeads { get; set; }
        public virtual DbSet<TransactionHeadAccountMap> TransactionHeadAccountMaps { get; set; }
        public virtual DbSet<TransactionHeadEntitlementMap> TransactionHeadEntitlementMaps { get; set; }
        public virtual DbSet<TransactionHeadPayablesMap> TransactionHeadPayablesMaps { get; set; }
        public virtual DbSet<TransactionHeadPointsMap> TransactionHeadPointsMaps { get; set; }
        public virtual DbSet<TransactionHeadReceivablesMap> TransactionHeadReceivablesMaps { get; set; }
        public virtual DbSet<TransactionHeadShoppingCartMap> TransactionHeadShoppingCartMaps { get; set; }
        public virtual DbSet<TransactionShipment> TransactionShipments { get; set; }
        public virtual DbSet<TransactionStatus> TransactionStatuses { get; set; }
        public virtual DbSet<Voucher> Vouchers { get; set; }
        public virtual DbSet<VoucherStatus> VoucherStatuses { get; set; }
        public virtual DbSet<VoucherType> VoucherTypes { get; set; }
        public virtual DbSet<WishList> WishLists { get; set; }
        public virtual DbSet<Basket> Baskets { get; set; }
        public virtual DbSet<JobActivity> JobActivities { get; set; }
        public virtual DbSet<JobEntryDetail> JobEntryDetails { get; set; }
        public virtual DbSet<JobEntryHead> JobEntryHeads { get; set; }
        public virtual DbSet<JobOperationStatus> JobOperationStatuses { get; set; }
        public virtual DbSet<JobsEntryHeadPayableMap> JobsEntryHeadPayableMaps { get; set; }
        public virtual DbSet<JobsEntryHeadReceivableMap> JobsEntryHeadReceivableMaps { get; set; }
        public virtual DbSet<JobSize> JobSizes { get; set; }
        public virtual DbSet<JobStatus> JobStatuses { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<ActionLinkType> ActionLinkTypes { get; set; }
        public virtual DbSet<ApplicationSubmitType> ApplicationSubmitTypes { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<AssignVehicleAttendantMap> AssignVehicleAttendantMaps { get; set; }
        public virtual DbSet<AssignVehicleMap> AssignVehicleMaps { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<BloodGroup> BloodGroups { get; set; }
        public virtual DbSet<BranchCultureData> BranchCultureDatas { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<BranchGroup> BranchGroups { get; set; }
        public virtual DbSet<BranchGroupStatus> BranchGroupStatuses { get; set; }
        public virtual DbSet<BranchStatus> BranchStatuses { get; set; }
        public virtual DbSet<CardType> CardTypes { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CompanyCurrencyMap> CompanyCurrencyMaps { get; set; }
        public virtual DbSet<CompanyGroup> CompanyGroups { get; set; }
        public virtual DbSet<CompanyStatus> CompanyStatuses { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Culture> Cultures { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<CustomerCard> CustomerCards { get; set; }
        public virtual DbSet<CustomerGroup> CustomerGroups { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerSetting> CustomerSettings { get; set; }
        public virtual DbSet<CustomerStatus> CustomerStatuses { get; set; }
        public virtual DbSet<CustomerSupplierMap> CustomerSupplierMaps { get; set; }
        public virtual DbSet<Day> Days { get; set; }
        public virtual DbSet<Departments1> Departments1 { get; set; }
        public virtual DbSet<DepartmentStatus> DepartmentStatuses { get; set; }
        public virtual DbSet<DocumentReferenceStatusMap> DocumentReferenceStatusMaps { get; set; }
        public virtual DbSet<DocumentReferenceType> DocumentReferenceTypes { get; set; }
        public virtual DbSet<DocumentStatus> DocumentStatuses { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<DocumentTypeTransactionNumber> DocumentTypeTransactionNumbers { get; set; }
        public virtual DbSet<DocumentTypeTypeMap> DocumentTypeTypeMaps { get; set; }
        public virtual DbSet<EntitlementMap> EntitlementMaps { get; set; }
        public virtual DbSet<EntityProperty> EntityProperties { get; set; }
        public virtual DbSet<EntityPropertyMap> EntityPropertyMaps { get; set; }
        public virtual DbSet<EntityPropertyType> EntityPropertyTypes { get; set; }
        public virtual DbSet<EntityTypeEntitlement> EntityTypeEntitlements { get; set; }
        public virtual DbSet<EntityTypePaymentMethodMap> EntityTypePaymentMethodMaps { get; set; }
        public virtual DbSet<EntityTypeRelationMap> EntityTypeRelationMaps { get; set; }
        public virtual DbSet<EntityType> EntityTypes { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<GeoLocationLog> GeoLocationLogs { get; set; }
        public virtual DbSet<ImageType> ImageTypes { get; set; }
        public virtual DbSet<Landmark> Landmarks { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Locations1> Locations1 { get; set; }
        public virtual DbSet<MaritalStatuses1> MaritalStatuses1 { get; set; }
        public virtual DbSet<Nationality> Nationalities { get; set; }
        public virtual DbSet<PaymentGroup> PaymentGroups { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<PaymentMethodSiteMap> PaymentMethodSiteMaps { get; set; }
        public virtual DbSet<Relegion> Relegions { get; set; }
        public virtual DbSet<SeoMetadataCultureData> SeoMetadataCultureDatas { get; set; }
        public virtual DbSet<SeoMetadata> SeoMetadatas { get; set; }
        public virtual DbSet<SiteCountryMaps1> SiteCountryMaps1 { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<StatusesCultureData> StatusesCultureDatas { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<SupplierStatus> SupplierStatuses { get; set; }
        public virtual DbSet<Title> Titles { get; set; }
        public virtual DbSet<VehicleDetailMap> VehicleDetailMaps { get; set; }
        public virtual DbSet<VehicleOwnershipType> VehicleOwnershipTypes { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<VehicleTransmission> VehicleTransmissions { get; set; }
        public virtual DbSet<VehicleType> VehicleTypes { get; set; }
        public virtual DbSet<Warehouse> Warehouses { get; set; }
        public virtual DbSet<WarehouseStatus> WarehouseStatuses { get; set; }
        public virtual DbSet<Zone> Zones { get; set; }
        public virtual DbSet<AlertStatus> AlertStatuses { get; set; }
        public virtual DbSet<AlertType> AlertTypes { get; set; }
        public virtual DbSet<EmailNotificationData> EmailNotificationDatas { get; set; }
        public virtual DbSet<EmailNotificationType> EmailNotificationTypes { get; set; }
        public virtual DbSet<EmailTemplateParameterMap> EmailTemplateParameterMaps { get; set; }
        public virtual DbSet<EmailTemplates1> EmailTemplates1 { get; set; }
        public virtual DbSet<NotificationAlert> NotificationAlerts { get; set; }
        public virtual DbSet<NotificationLog> NotificationLogs { get; set; }
        public virtual DbSet<NotificationQueueParentMap> NotificationQueueParentMaps { get; set; }
        public virtual DbSet<NotificationsProcessing> NotificationsProcessings { get; set; }
        public virtual DbSet<NotificationsQueue> NotificationsQueues { get; set; }
        public virtual DbSet<NotificationStatus> NotificationStatuses { get; set; }
        public virtual DbSet<NotificationType> NotificationTypes { get; set; }
        public virtual DbSet<SMSNotificationType> SMSNotificationTypes { get; set; }
        public virtual DbSet<OrderContactMap> OrderContactMaps { get; set; }
        public virtual DbSet<OrderRoleTracking> OrderRoleTrackings { get; set; }
        public virtual DbSet<PaymentDetailsKnet> PaymentDetailsKnets { get; set; }
        public virtual DbSet<PaymentDetailsLogKnet> PaymentDetailsLogKnets { get; set; }
        public virtual DbSet<PaymentDetailsMyFatoorah> PaymentDetailsMyFatoorahs { get; set; }
        public virtual DbSet<PaymentDetailsPayPal> PaymentDetailsPayPals { get; set; }
        public virtual DbSet<PaymentDetailsTheFort> PaymentDetailsTheForts { get; set; }
        public virtual DbSet<PaymentExceptionByZoneDelivery> PaymentExceptionByZoneDeliveries { get; set; }
        public virtual DbSet<PaymentLog> PaymentLogs { get; set; }
        public virtual DbSet<PaymentMasterVisa> PaymentMasterVisas { get; set; }
        public virtual DbSet<Attendence> Attendences { get; set; }
        public virtual DbSet<AttendenceStatus> AttendenceStatuses { get; set; }
        public virtual DbSet<Designation> Designations { get; set; }
        public virtual DbSet<EmployeeAccountMap> EmployeeAccountMaps { get; set; }
        public virtual DbSet<EmployeeAdditionalInfo> EmployeeAdditionalInfos { get; set; }
        public virtual DbSet<EmployeeBankDetail> EmployeeBankDetails { get; set; }
        public virtual DbSet<EmployeeGrade> EmployeeGrades { get; set; }
        public virtual DbSet<EmployeeLevel> EmployeeLevels { get; set; }
        public virtual DbSet<EmployeeRoleMap> EmployeeRoleMaps { get; set; }
        public virtual DbSet<EmployeeRole> EmployeeRoles { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeSalary> EmployeeSalaries { get; set; }
        public virtual DbSet<EmployeeSalaryStructureComponentMap> EmployeeSalaryStructureComponentMaps { get; set; }
        public virtual DbSet<EmployeeSalaryStructure> EmployeeSalaryStructures { get; set; }
        public virtual DbSet<EmployeeTimeSheet> EmployeeTimeSheets { get; set; }
        public virtual DbSet<HolidayList> HolidayLists { get; set; }
        public virtual DbSet<Holiday> Holidays { get; set; }
        public virtual DbSet<HolidayType> HolidayTypes { get; set; }
        public virtual DbSet<JobType> JobTypes { get; set; }
        public virtual DbSet<LeaveApplicationApprover> LeaveApplicationApprovers { get; set; }
        public virtual DbSet<LeaveApplication> LeaveApplications { get; set; }
        public virtual DbSet<LeaveBlockListApprover> LeaveBlockListApprovers { get; set; }
        public virtual DbSet<LeaveBlockListEntry> LeaveBlockListEntries { get; set; }
        public virtual DbSet<LeaveBlockList> LeaveBlockLists { get; set; }
        public virtual DbSet<LeaveSession> LeaveSessions { get; set; }
        public virtual DbSet<LeaveStatus> LeaveStatuses { get; set; }
        public virtual DbSet<LeaveType> LeaveTypes { get; set; }
        public virtual DbSet<PayrollFrequency> PayrollFrequencies { get; set; }
        public virtual DbSet<SalaryComponent> SalaryComponents { get; set; }
        public virtual DbSet<SalaryComponentType> SalaryComponentTypes { get; set; }
        public virtual DbSet<SalaryMethod> SalaryMethods { get; set; }
        public virtual DbSet<SalaryPaymentMode> SalaryPaymentModes { get; set; }
        public virtual DbSet<SalarySlip> SalarySlips { get; set; }
        public virtual DbSet<SalarySlipStatus> SalarySlipStatuses { get; set; }
        public virtual DbSet<SalaryStructure> SalaryStructures { get; set; }
        public virtual DbSet<SalaryStructureComponentMap> SalaryStructureComponentMaps { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<ExtraTimeType> ExtraTimeTypes { get; set; }
        public virtual DbSet<FrequencyType> FrequencyTypes { get; set; }
        public virtual DbSet<PricingType> PricingTypes { get; set; }
        public virtual DbSet<Saloon> Saloons { get; set; }
        public virtual DbSet<ServiceAvailable> ServiceAvailables { get; set; }
        public virtual DbSet<ServiceEmployeeMap> ServiceEmployeeMaps { get; set; }
        public virtual DbSet<ServiceGroup> ServiceGroups { get; set; }
        public virtual DbSet<ServicePricing> ServicePricings { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<TreatmentGroup> TreatmentGroups { get; set; }
        public virtual DbSet<TreatmentType> TreatmentTypes { get; set; }
        public virtual DbSet<Despatch> Despatches { get; set; }
        public virtual DbSet<DriverSchedule> DriverSchedules { get; set; }
        public virtual DbSet<EntityScheduler> EntitySchedulers { get; set; }
        public virtual DbSet<SchedulerEntityType> SchedulerEntityTypes { get; set; }
        public virtual DbSet<SchedulerType> SchedulerTypes { get; set; }
        public virtual DbSet<AcadamicCalendar> AcadamicCalendars { get; set; }
        public virtual DbSet<AcademicNote> AcademicNotes { get; set; }
        public virtual DbSet<AcademicYearCalendarEvent> AcademicYearCalendarEvents { get; set; }
        public virtual DbSet<AcademicYearCalendarStatu> AcademicYearCalendarStatus { get; set; }
        public virtual DbSet<AcademicYear> AcademicYears { get; set; }
        public virtual DbSet<AcademicYearStatu> AcademicYearStatus { get; set; }
        public virtual DbSet<AdmissionEnquiry> AdmissionEnquiries { get; set; }
        public virtual DbSet<ApplicationStatus> ApplicationStatuses { get; set; }
        public virtual DbSet<AssignmentAttachmentMap> AssignmentAttachmentMaps { get; set; }
        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<AssignmentStatus> AssignmentStatuses { get; set; }
        public virtual DbSet<AssignmentType> AssignmentTypes { get; set; }
        public virtual DbSet<AttendenceReason> AttendenceReasons { get; set; }
        public virtual DbSet<BuildingClassRoomMap> BuildingClassRoomMaps { get; set; }
        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<Cast> Casts { get; set; }
        public virtual DbSet<ClassAgeLimit> ClassAgeLimits { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<ClassFeeMaster> ClassFeeMasters { get; set; }
        public virtual DbSet<ClassFeeStructureMap> ClassFeeStructureMaps { get; set; }
        public virtual DbSet<ClassGroup> ClassGroups { get; set; }
        public virtual DbSet<ClassRoomType> ClassRoomTypes { get; set; }
        public virtual DbSet<ClassSectionMap> ClassSectionMaps { get; set; }
        public virtual DbSet<ClassSubjectMap> ClassSubjectMaps { get; set; }
        public virtual DbSet<ClassSubjectSkillGroupMap> ClassSubjectSkillGroupMaps { get; set; }
        public virtual DbSet<ClassSubjectSkillGroupSkillMap> ClassSubjectSkillGroupSkillMaps { get; set; }
        public virtual DbSet<ClassTeacherMap> ClassTeacherMaps { get; set; }
        public virtual DbSet<ClassTiming> ClassTimings { get; set; }
        public virtual DbSet<ClassTimingSet> ClassTimingSets { get; set; }
        public virtual DbSet<Community> Communitys { get; set; }
        public virtual DbSet<Complain> Complains { get; set; }
        public virtual DbSet<ComplaintSourceType> ComplaintSourceTypes { get; set; }
        public virtual DbSet<ComplainType> ComplainTypes { get; set; }
        public virtual DbSet<CreditNoteFeeTypeMap> CreditNoteFeeTypeMaps { get; set; }
        public virtual DbSet<EnquiryReferenceType> EnquiryReferenceTypes { get; set; }
        public virtual DbSet<EnquirySource> EnquirySources { get; set; }
        public virtual DbSet<ExamClassMap> ExamClassMaps { get; set; }
        public virtual DbSet<ExamGroup> ExamGroups { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<ExamSchedule> ExamSchedules { get; set; }
        public virtual DbSet<ExamSubjectMap> ExamSubjectMaps { get; set; }
        public virtual DbSet<ExamType> ExamTypes { get; set; }
        public virtual DbSet<FeeCollectionFeeTypeMap> FeeCollectionFeeTypeMaps { get; set; }
        public virtual DbSet<FeeCollectionMonthlySplit> FeeCollectionMonthlySplits { get; set; }
        public virtual DbSet<FeeCollectionPaymentModeMap> FeeCollectionPaymentModeMaps { get; set; }
        public virtual DbSet<FeeCollection> FeeCollections { get; set; }
        public virtual DbSet<FeeConcessionType> FeeConcessionTypes { get; set; }
        public virtual DbSet<FeeCycle> FeeCycles { get; set; }
        public virtual DbSet<FeeDiscount> FeeDiscounts { get; set; }
        public virtual DbSet<FeeDueFeeTypeMap> FeeDueFeeTypeMaps { get; set; }
        public virtual DbSet<FeeDueMonthlySplit> FeeDueMonthlySplits { get; set; }
        public virtual DbSet<FeeFineType> FeeFineTypes { get; set; }
        public virtual DbSet<FeeGroup> FeeGroups { get; set; }
        public virtual DbSet<FeeMasterClassMap> FeeMasterClassMaps { get; set; }
        public virtual DbSet<FeeMasterClassMontlySplitMap> FeeMasterClassMontlySplitMaps { get; set; }
        public virtual DbSet<FeeMaster> FeeMasters { get; set; }
        public virtual DbSet<FeePaymentMode> FeePaymentModes { get; set; }
        public virtual DbSet<FeePeriod> FeePeriods { get; set; }
        public virtual DbSet<FeeStructureFeeMap> FeeStructureFeeMaps { get; set; }
        public virtual DbSet<FeeStructureMontlySplitMap> FeeStructureMontlySplitMaps { get; set; }
        public virtual DbSet<FeeStructure> FeeStructures { get; set; }
        public virtual DbSet<FeeType> FeeTypes { get; set; }
        public virtual DbSet<FinalSettlement> FinalSettlements { get; set; }
        public virtual DbSet<FinalSettlementFeeTypeMap> FinalSettlementFeeTypeMaps { get; set; }
        public virtual DbSet<FinalSettlementPaymentModeMap> FinalSettlementPaymentModeMaps { get; set; }
        public virtual DbSet<FineMaster> FineMasters { get; set; }
        public virtual DbSet<FineMasterStudentMap> FineMasterStudentMaps { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<GuardianType> GuardianTypes { get; set; }
        public virtual DbSet<HostelRoom> HostelRooms { get; set; }
        public virtual DbSet<Hostel> Hostels { get; set; }
        public virtual DbSet<HostelType> HostelTypes { get; set; }
        public virtual DbSet<LessonPlan> LessonPlans { get; set; }
        public virtual DbSet<LessonPlanStatus> LessonPlanStatuses { get; set; }
        public virtual DbSet<LessonPlanTopicMap> LessonPlanTopicMaps { get; set; }
        public virtual DbSet<LibraryBookCategory> LibraryBookCategories { get; set; }
        public virtual DbSet<LibraryBookCategoryMap> LibraryBookCategoryMaps { get; set; }
        public virtual DbSet<LibraryBookCondition> LibraryBookConditions { get; set; }
        public virtual DbSet<LibraryBook> LibraryBooks { get; set; }
        public virtual DbSet<LibraryBookStatus> LibraryBookStatuses { get; set; }
        public virtual DbSet<LibraryBookTypes1> LibraryBookTypes1 { get; set; }
        public virtual DbSet<LibraryStaffRegister> LibraryStaffRegisters { get; set; }
        public virtual DbSet<LibraryStudentRegister> LibraryStudentRegisters { get; set; }
        public virtual DbSet<LibraryTransaction> LibraryTransactions { get; set; }
        public virtual DbSet<LibraryTransactionType> LibraryTransactionTypes { get; set; }
        public virtual DbSet<MarkGradeMap> MarkGradeMaps { get; set; }
        public virtual DbSet<MarkGrade> MarkGrades { get; set; }
        public virtual DbSet<MarkRegister> MarkRegisters { get; set; }
        public virtual DbSet<MarkRegisterSkillGroup> MarkRegisterSkillGroups { get; set; }
        public virtual DbSet<MarkRegisterSkill> MarkRegisterSkills { get; set; }
        public virtual DbSet<MarkRegisterSubjectMap> MarkRegisterSubjectMaps { get; set; }
        public virtual DbSet<Medium> Mediums { get; set; }
        public virtual DbSet<PackageConfig> PackageConfigs { get; set; }
        public virtual DbSet<PackageConfigClassMap> PackageConfigClassMaps { get; set; }
        public virtual DbSet<PackageConfigFeeStructureMap> PackageConfigFeeStructureMaps { get; set; }
        public virtual DbSet<PackageConfigStudentGroupMap> PackageConfigStudentGroupMaps { get; set; }
        public virtual DbSet<PackageConfigStudentMap> PackageConfigStudentMaps { get; set; }
        public virtual DbSet<Parent> Parents { get; set; }
        public virtual DbSet<ParentStudentApplicationMap> ParentStudentApplicationMaps { get; set; }
        public virtual DbSet<ParentStudentMap> ParentStudentMaps { get; set; }
        public virtual DbSet<PassportDetailMap> PassportDetailMaps { get; set; }
        public virtual DbSet<PassportVisaDetail> PassportVisaDetails { get; set; }
        public virtual DbSet<PhoneCallLog> PhoneCallLogs { get; set; }
        public virtual DbSet<PhoneCallType> PhoneCallTypes { get; set; }
        public virtual DbSet<PostalDespatch> PostalDespatches { get; set; }
        public virtual DbSet<PostalReceive> PostalReceives { get; set; }
        public virtual DbSet<PresentStatus> PresentStatuses { get; set; }
        public virtual DbSet<Relegions1> Relegions1 { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }
        public virtual DbSet<Routes1> Routes1 { get; set; }
        public virtual DbSet<RouteStopMap> RouteStopMaps { get; set; }
        public virtual DbSet<RouteType> RouteTypes { get; set; }
        public virtual DbSet<RouteVehicleMap> RouteVehicleMaps { get; set; }
        public virtual DbSet<SchoolCalenderHolidayMap> SchoolCalenderHolidayMaps { get; set; }
        public virtual DbSet<SchoolCalender> SchoolCalenders { get; set; }
        public virtual DbSet<SchoolCreditNote> SchoolCreditNotes { get; set; }
        public virtual DbSet<SchoolPollAnswerLog> SchoolPollAnswerLogs { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<SkillGroupMaster> SkillGroupMasters { get; set; }
        public virtual DbSet<SkillMaster> SkillMasters { get; set; }
        public virtual DbSet<StaffAttendence> StaffAttendences { get; set; }
        public virtual DbSet<StaffPresentStatus> StaffPresentStatuses { get; set; }
        public virtual DbSet<StaffRouteMonthlySplit> StaffRouteMonthlySplits { get; set; }
        public virtual DbSet<StaffRouteStopMap> StaffRouteStopMaps { get; set; }
        public virtual DbSet<StudentApplication> StudentApplications { get; set; }
        public virtual DbSet<StudentApplicationSiblingMap> StudentApplicationSiblingMaps { get; set; }
        public virtual DbSet<StudentAssignmentMap> StudentAssignmentMaps { get; set; }
        public virtual DbSet<StudentAttendence> StudentAttendences { get; set; }
        public virtual DbSet<StudentCategory> StudentCategories { get; set; }
        public virtual DbSet<StudentClassHistoryMap> StudentClassHistoryMaps { get; set; }
        public virtual DbSet<StudentFeeDue> StudentFeeDues { get; set; }
        public virtual DbSet<StudentGroupFeeMaster> StudentGroupFeeMasters { get; set; }
        public virtual DbSet<StudentGroupFeeTypeMap> StudentGroupFeeTypeMaps { get; set; }
        public virtual DbSet<StudentGroupMap> StudentGroupMaps { get; set; }
        public virtual DbSet<StudentGroup> StudentGroups { get; set; }
        public virtual DbSet<StudentGroupType> StudentGroupTypes { get; set; }
        public virtual DbSet<StudentHous> StudentHouses { get; set; }
        public virtual DbSet<StudentLeaveApplication> StudentLeaveApplications { get; set; }
        public virtual DbSet<StudentMiscDetail> StudentMiscDetails { get; set; }
        public virtual DbSet<StudentPassportDetail> StudentPassportDetails { get; set; }
        public virtual DbSet<StudentPromotionLog> StudentPromotionLogs { get; set; }
        public virtual DbSet<StudentRouteMonthlySplit> StudentRouteMonthlySplits { get; set; }
        public virtual DbSet<StudentRouteStopMap> StudentRouteStopMaps { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentSiblingMap> StudentSiblingMaps { get; set; }
        public virtual DbSet<StudentSkillGroupMap> StudentSkillGroupMaps { get; set; }
        public virtual DbSet<StudentSkillMasterMap> StudentSkillMasterMaps { get; set; }
        public virtual DbSet<StudentSkillRegister> StudentSkillRegisters { get; set; }
        public virtual DbSet<StudentTransferRequestReason> StudentTransferRequestReasons { get; set; }
        public virtual DbSet<StudentTransferRequest> StudentTransferRequests { get; set; }
        public virtual DbSet<StudentVehicleAssign> StudentVehicleAssigns { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<SubjectTeacherMap> SubjectTeacherMaps { get; set; }
        public virtual DbSet<SubjectTopic> SubjectTopics { get; set; }
        public virtual DbSet<SubjectType> SubjectTypes { get; set; }
        public virtual DbSet<Syllabu> Syllabus { get; set; }
        public virtual DbSet<TeacherActivity> TeacherActivities { get; set; }
        public virtual DbSet<TimeTableAllocation> TimeTableAllocations { get; set; }
        public virtual DbSet<TimeTable> TimeTables { get; set; }
        public virtual DbSet<TransferRequestStatus> TransferRequestStatuses { get; set; }
        public virtual DbSet<VisaDetailMap> VisaDetailMaps { get; set; }
        public virtual DbSet<VisitingPurpos> VisitingPurposes { get; set; }
        public virtual DbSet<VisitorBook> VisitorBooks { get; set; }
        public virtual DbSet<VolunteerType> VolunteerTypes { get; set; }
        public virtual DbSet<WeekDay> WeekDays { get; set; }
        public virtual DbSet<ChartMetadata> ChartMetadatas { get; set; }
        public virtual DbSet<Condition> Conditions { get; set; }
        public virtual DbSet<CultureData> CultureDatas { get; set; }
        public virtual DbSet<DataFormat> DataFormats { get; set; }
        public virtual DbSet<DataFormatType> DataFormatTypes { get; set; }
        public virtual DbSet<DataHistoryEntity> DataHistoryEntities { get; set; }
        public virtual DbSet<DataType> DataTypes { get; set; }
        public virtual DbSet<FilterColumnConditionMap> FilterColumnConditionMaps { get; set; }
        public virtual DbSet<FilterColumn> FilterColumns { get; set; }
        public virtual DbSet<FilterColumnUserValue> FilterColumnUserValues { get; set; }
        public virtual DbSet<FilterQuery> FilterQueries { get; set; }
        public virtual DbSet<GlobalSetting> GlobalSettings { get; set; }
        public virtual DbSet<Lookup> Lookups { get; set; }
        public virtual DbSet<MenuLinkCultureData> MenuLinkCultureDatas { get; set; }
        public virtual DbSet<MenuLink> MenuLinks { get; set; }
        public virtual DbSet<MenuLinkType> MenuLinkTypes { get; set; }
        public virtual DbSet<ScreenField> ScreenFields { get; set; }
        public virtual DbSet<ScreenFieldSetting> ScreenFieldSettings { get; set; }
        public virtual DbSet<ScreenLookupMap> ScreenLookupMaps { get; set; }
        public virtual DbSet<ScreenMetadata> ScreenMetadatas { get; set; }
        public virtual DbSet<ScreenShortCut> ScreenShortCuts { get; set; }
        public virtual DbSet<Sequence> Sequences { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<Sites1> Sites1 { get; set; }
        public virtual DbSet<TextTransformType> TextTransformTypes { get; set; }
        public virtual DbSet<UIControlType> UIControlTypes { get; set; }
        public virtual DbSet<UIControlValidation> UIControlValidations { get; set; }
        public virtual DbSet<UserDataFormatMap> UserDataFormatMaps { get; set; }
        public virtual DbSet<UserScreenFieldSetting> UserScreenFieldSettings { get; set; }
        public virtual DbSet<UserSetting> UserSettings { get; set; }
        public virtual DbSet<UserViewColumnMap> UserViewColumnMaps { get; set; }
        public virtual DbSet<UserView> UserViews { get; set; }
        public virtual DbSet<ViewAction> ViewActions { get; set; }
        public virtual DbSet<ViewColumn> ViewColumns { get; set; }
        public virtual DbSet<View> Views { get; set; }
        public virtual DbSet<ViewType> ViewTypes { get; set; }
        public virtual DbSet<EntityChangeTracker> EntityChangeTrackers { get; set; }
        public virtual DbSet<EntityChangeTrackerBatch> EntityChangeTrackerBatches { get; set; }
        public virtual DbSet<EntityChangeTrackerLog> EntityChangeTrackerLogs { get; set; }
        public virtual DbSet<EntityChangeTrackersInProcess> EntityChangeTrackersInProcesses { get; set; }
        public virtual DbSet<EntityChangeTrackersQueue> EntityChangeTrackersQueues { get; set; }
        public virtual DbSet<OperationType> OperationTypes { get; set; }
        public virtual DbSet<SyncEntity> SyncEntities { get; set; }
        public virtual DbSet<SyncFieldMap> SyncFieldMaps { get; set; }
        public virtual DbSet<SyncFieldMapType> SyncFieldMapTypes { get; set; }
        public virtual DbSet<TrackerStatus> TrackerStatuses { get; set; }
        public virtual DbSet<TaskAssingner> TaskAssingners { get; set; }
        public virtual DbSet<TaskPrioity> TaskPrioities { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TaskStatus> TaskStatuses { get; set; }
        public virtual DbSet<TaskType> TaskTypes { get; set; }
        public virtual DbSet<WorkflowCondition> WorkflowConditions { get; set; }
        public virtual DbSet<WorkflowFiled> WorkflowFileds { get; set; }
        public virtual DbSet<WorkflowLogMapRuleApproverMap> WorkflowLogMapRuleApproverMaps { get; set; }
        public virtual DbSet<WorkflowLogMapRuleMap> WorkflowLogMapRuleMaps { get; set; }
        public virtual DbSet<WorkflowLogMap> WorkflowLogMaps { get; set; }
        public virtual DbSet<WorkflowRuleApprover> WorkflowRuleApprovers { get; set; }
        public virtual DbSet<WorkflowRuleCondition> WorkflowRuleConditions { get; set; }
        public virtual DbSet<WorkflowRule> WorkflowRules { get; set; }
        public virtual DbSet<Workflow> Workflows { get; set; }
        public virtual DbSet<WorkflowStatus> WorkflowStatuses { get; set; }
        public virtual DbSet<WorkflowTransactionHeadMap> WorkflowTransactionHeadMaps { get; set; }
        public virtual DbSet<WorkflowTransactionHeadRuleMap> WorkflowTransactionHeadRuleMaps { get; set; }
        public virtual DbSet<WorkflowTransactionRuleApproverMap> WorkflowTransactionRuleApproverMaps { get; set; }
        public virtual DbSet<WorkflowType> WorkflowTypes { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<DeliveryTypeCategoryMaster> DeliveryTypeCategoryMasters { get; set; }
        public virtual DbSet<StaticContentData> StaticContentDatas { get; set; }
        public virtual DbSet<Month> Months { get; set; }
        public virtual DbSet<QuestionImageMap> QuestionImageMaps { get; set; }
        public virtual DbSet<ProductInventories_Bak> ProductInventories_Bak { get; set; }
        public virtual DbSet<ShoppingCartVoucherMap> ShoppingCartVoucherMaps { get; set; }
        public virtual DbSet<SMSNotificationData> SMSNotificationDatas { get; set; }
        public virtual DbSet<LeaveAllocation> LeaveAllocations { get; set; }
        public virtual DbSet<PaymentEntry> PaymentEntries { get; set; }
        public virtual DbSet<VIEWS_20210112> VIEWS_20210112 { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.Alias)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Account>()
                .Property(e => e.ExternalReferenceID)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.AccountCode)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.AccountAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.TaxRegistrationNum)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Accounts1)
                .WithOptional(e => e.Account1)
                .HasForeignKey(e => e.ParentAccountID);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.AccountTaxTransactions)
                .WithOptional(e => e.Account)
                .HasForeignKey(e => e.AccoundID);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Assets)
                .WithOptional(e => e.Account)
                .HasForeignKey(e => e.AssetGlAccID);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Assets1)
                .WithOptional(e => e.Account1)
                .HasForeignKey(e => e.AccumulatedDepGLAccID);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Assets2)
                .WithOptional(e => e.Account2)
                .HasForeignKey(e => e.DepreciationExpGLAccId);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.FeeMasters)
                .WithOptional(e => e.Account)
                .HasForeignKey(e => e.LedgerAccountID);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.FeeMasters1)
                .WithOptional(e => e.Account1)
                .HasForeignKey(e => e.TaxLedgerAccountID);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.FineMasters)
                .WithOptional(e => e.Account)
                .HasForeignKey(e => e.LedgerAccountID);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.TaxTransactions)
                .WithOptional(e => e.Account)
                .HasForeignKey(e => e.AccoundID);

            modelBuilder.Entity<Accounts_Reference>()
                .Property(e => e.Ref_No)
                .IsUnicode(false);

            modelBuilder.Entity<Accounts_Reference>()
                .Property(e => e.Ref_No1)
                .IsUnicode(false);

            modelBuilder.Entity<Accounts_Reference>()
                .Property(e => e.Ref_No2)
                .IsUnicode(false);

            modelBuilder.Entity<Accounts_SubLedger>()
                .HasMany(e => e.Accounts_SubLedger_Relation)
                .WithRequired(e => e.Accounts_SubLedger)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Accounts_SubLedger>()
                .HasMany(e => e.AccountTransactionDetails)
                .WithOptional(e => e.Accounts_SubLedger)
                .HasForeignKey(e => e.SubLedgerID);

            modelBuilder.Entity<Accounts_SubLedger>()
                .HasMany(e => e.Payables)
                .WithOptional(e => e.Accounts_SubLedger)
                .HasForeignKey(e => e.SubLedgerID);

            modelBuilder.Entity<Accounts_SubLedger>()
                .HasMany(e => e.Receivables)
                .WithOptional(e => e.Accounts_SubLedger)
                .HasForeignKey(e => e.SubLedgerID);

            modelBuilder.Entity<AccountTaxTransaction>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<AccountTransactionDetail>()
                .Property(e => e.ReferenceNumber)
                .IsUnicode(false);

            modelBuilder.Entity<AccountTransactionDetail>()
                .Property(e => e.ReferenceRate)
                .HasPrecision(18, 3);

            modelBuilder.Entity<AccountTransactionDetail>()
                .Property(e => e.ReferenceQuantity)
                .HasPrecision(18, 3);

            modelBuilder.Entity<AccountTransactionDetail>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<AccountTransactionDetail>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<AccountTransactionDetail>()
                .Property(e => e.InvoiceAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<AccountTransactionDetail>()
                .Property(e => e.PaidAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<AccountTransactionDetail>()
                .Property(e => e.ReturnAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<AccountTransactionDetail>()
                .Property(e => e.ExchangeRate)
                .HasPrecision(18, 3);

            modelBuilder.Entity<AccountTransactionDetail>()
                .Property(e => e.AvailableQuantity)
                .HasPrecision(18, 3);

            modelBuilder.Entity<AccountTransactionDetail>()
                .Property(e => e.CurrentAvgCost)
                .HasPrecision(18, 3);

            modelBuilder.Entity<AccountTransactionDetail>()
                .Property(e => e.NewAvgCost)
                .HasPrecision(18, 3);

            modelBuilder.Entity<AccountTransactionDetail>()
                .Property(e => e.UnPaidAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<AccountTransactionDetail>()
                .Property(e => e.TaxPercentage)
                .HasPrecision(8, 3);

            modelBuilder.Entity<AccountTransactionDetail>()
                .Property(e => e.ExternalReference1)
                .IsUnicode(false);

            modelBuilder.Entity<AccountTransactionDetail>()
                .Property(e => e.ExternalReference2)
                .IsUnicode(false);

            modelBuilder.Entity<AccountTransactionDetail>()
                .Property(e => e.TaxAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<AccountTransactionDetail>()
                .Property(e => e.ExternalReference3)
                .IsUnicode(false);

            modelBuilder.Entity<AccountTransactionDetail>()
                .Property(e => e.DiscountAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<AccountTransactionDetail>()
                .HasMany(e => e.FeeCollectionPaymentModeMaps)
                .WithOptional(e => e.AccountTransactionDetail)
                .HasForeignKey(e => e.AccountTransactionDetailID);

            modelBuilder.Entity<AccountTransactionHead>()
                .Property(e => e.TransactionNumber)
                .IsUnicode(false);

            modelBuilder.Entity<AccountTransactionHead>()
                .Property(e => e.ExchangeRate)
                .HasPrecision(18, 3);

            modelBuilder.Entity<AccountTransactionHead>()
                .Property(e => e.AdvanceAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<AccountTransactionHead>()
                .Property(e => e.AmountPaid)
                .HasPrecision(18, 3);

            modelBuilder.Entity<AccountTransactionHead>()
                .Property(e => e.ExternalReference1)
                .IsUnicode(false);

            modelBuilder.Entity<AccountTransactionHead>()
                .Property(e => e.ExternalReference2)
                .IsUnicode(false);

            modelBuilder.Entity<AccountTransactionHead>()
                .Property(e => e.ExternalReference3)
                .IsUnicode(false);

            modelBuilder.Entity<AccountTransactionHead>()
                .Property(e => e.DiscountAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<AccountTransactionHead>()
                .Property(e => e.DiscountPercentage)
                .HasPrecision(6, 3);

            modelBuilder.Entity<AccountTransactionHead>()
                .Property(e => e.ChequeNumber)
                .IsUnicode(false);

            modelBuilder.Entity<AccountTransactionHead>()
                .Property(e => e.TaxAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<AccountTransactionHead>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<AccountTransactionHead>()
                .HasMany(e => e.AccountTaxTransactions)
                .WithOptional(e => e.AccountTransactionHead)
                .HasForeignKey(e => e.HeadID);

            modelBuilder.Entity<AccountTransactionHead>()
                .HasMany(e => e.AccountTransactionDetails)
                .WithOptional(e => e.AccountTransactionHead)
                .HasForeignKey(e => e.AccountTransactionHeadID);

            modelBuilder.Entity<AccountTransactionHead>()
                .HasMany(e => e.AccountTransactionHeadAccountMaps)
                .WithRequired(e => e.AccountTransactionHead)
                .HasForeignKey(e => e.AccountTransactionHeadID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AccountTransactionHead>()
                .HasMany(e => e.FeeCollectionFeeTypeMaps)
                .WithOptional(e => e.AccountTransactionHead)
                .HasForeignKey(e => e.AccountTransactionHeadID);

            modelBuilder.Entity<AccountTransactionHead>()
                .HasMany(e => e.FeeCollectionMonthlySplits)
                .WithOptional(e => e.AccountTransactionHead)
                .HasForeignKey(e => e.AccountTransactionHeadID);

            modelBuilder.Entity<AccountTransactionHead>()
                .HasMany(e => e.FeeCollections)
                .WithOptional(e => e.AccountTransactionHead)
                .HasForeignKey(e => e.AccountTransactionHeadID);

            modelBuilder.Entity<AccountTransactionHead>()
                .HasMany(e => e.FeeDueFeeTypeMaps)
                .WithOptional(e => e.AccountTransactionHead)
                .HasForeignKey(e => e.AccountTransactionHeadID);

            modelBuilder.Entity<AccountTransactionHead>()
                .HasMany(e => e.FeeDueMonthlySplits)
                .WithOptional(e => e.AccountTransactionHead)
                .HasForeignKey(e => e.AccountTransactionHeadID);

            modelBuilder.Entity<AccountTransactionHead>()
                .HasMany(e => e.FinalSettlementFeeTypeMaps)
                .WithOptional(e => e.AccountTransactionHead)
                .HasForeignKey(e => e.AccountTransactionHeadID);

            modelBuilder.Entity<AccountTransaction>()
                .Property(e => e.TransactionNumber)
                .IsUnicode(false);

            modelBuilder.Entity<AccountTransaction>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<AccountTransaction>()
                .Property(e => e.InclusiveTaxAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<AccountTransaction>()
                .Property(e => e.ExclusiveTaxAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<AccountTransaction>()
                .Property(e => e.DiscountAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<AccountTransaction>()
                .Property(e => e.DiscountPercentage)
                .HasPrecision(6, 3);

            modelBuilder.Entity<AccountTransaction>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<AccountTransaction>()
                .HasMany(e => e.AccountTransactionHeadAccountMaps)
                .WithRequired(e => e.AccountTransaction)
                .HasForeignKey(e => e.AccountTransactionID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AccountTransaction>()
                .HasMany(e => e.AssetTransactionHeadAccountMaps)
                .WithRequired(e => e.AccountTransaction)
                .HasForeignKey(e => e.AccountTransactionID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AccountTransaction>()
                .HasMany(e => e.TransactionHeadAccountMaps)
                .WithOptional(e => e.AccountTransaction)
                .HasForeignKey(e => e.AccountTransactionID);

            modelBuilder.Entity<ChartOfAccountMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ChartOfAccount>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ChartOfAccount>()
                .HasMany(e => e.ChartOfAccountMaps)
                .WithOptional(e => e.ChartOfAccount)
                .HasForeignKey(e => e.ChartOfAccountID);

            modelBuilder.Entity<Company_FiscalYear_Close>()
                .Property(e => e.CFC_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Company_FiscalYear_Close>()
                .Property(e => e.OpeningStock)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Company_FiscalYear_Close>()
                .Property(e => e.ClosingStock)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Company_FiscalYear_Close>()
                .Property(e => e.GrossProfit)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Company_FiscalYear_Close>()
                .Property(e => e.NetProfit)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Company_FiscalYear_Close>()
                .Property(e => e.Diff_In_Opening)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CustomerAccountMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<FiscalYear>()
                .Property(e => e.FiscalYear_Name)
                .IsUnicode(false);

            modelBuilder.Entity<FiscalYear>()
                .Property(e => e.CurrentYear)
                .IsUnicode(false);

            modelBuilder.Entity<Group>()
                .Property(e => e.Default_Side)
                .IsUnicode(false);

            modelBuilder.Entity<Group>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Payable>()
                .Property(e => e.TransactionNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Payable>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Payable>()
                .Property(e => e.PaidAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Payable>()
                .Property(e => e.TaxAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Payable>()
                .Property(e => e.DiscountAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Payable>()
                .Property(e => e.ExchangeRate)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Payable>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Payable>()
                .HasMany(e => e.JobsEntryHeadPayableMaps)
                .WithOptional(e => e.Payable)
                .HasForeignKey(e => e.PayableID);

            modelBuilder.Entity<Payable>()
                .HasMany(e => e.Payables1)
                .WithOptional(e => e.Payable1)
                .HasForeignKey(e => e.ReferencePayablesID);

            modelBuilder.Entity<Receivable>()
                .Property(e => e.TransactionNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Receivable>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Receivable>()
                .Property(e => e.PaidAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Receivable>()
                .Property(e => e.TaxAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Receivable>()
                .Property(e => e.DiscountAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Receivable>()
                .Property(e => e.ExchangeRate)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Receivable>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Receivable>()
                .HasMany(e => e.JobsEntryHeadReceivableMaps)
                .WithOptional(e => e.Receivable)
                .HasForeignKey(e => e.ReceivableID);

            modelBuilder.Entity<Receivable>()
                .HasMany(e => e.Receivables1)
                .WithOptional(e => e.Receivable1)
                .HasForeignKey(e => e.ReferenceReceivablesID);

            modelBuilder.Entity<Receivable>()
                .HasMany(e => e.TransactionHeadReceivablesMaps)
                .WithRequired(e => e.Receivable)
                .HasForeignKey(e => e.ReceivableID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SupplierAccountMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<TenderType>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Tranhead>()
                .Property(e => e.VoucherNo)
                .IsUnicode(false);

            modelBuilder.Entity<Tranhead>()
                .Property(e => e.InvoiceNo)
                .IsUnicode(false);

            modelBuilder.Entity<Tranhead>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Trantail>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Trantail>()
                .Property(e => e.CRate)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Trantail>()
                .Property(e => e.CRate1)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Trantail>()
                .Property(e => e.CRate2)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Trantail>()
                .Property(e => e.FAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Trantail>()
                .Property(e => e.FAmount1)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Trantail>()
                .Property(e => e.FAmount2)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Trantail_CostCenter>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Trantail_Narration>()
                .Property(e => e.Narration)
                .IsUnicode(false);

            modelBuilder.Entity<Trantail_Narration>()
                .Property(e => e.Narration1)
                .IsUnicode(false);

            modelBuilder.Entity<Trantail_Payment>()
                .Property(e => e.Cheque_No)
                .IsUnicode(false);

            modelBuilder.Entity<Trantail_Payment>()
                .Property(e => e.Cheque_No_Altered)
                .IsUnicode(false);

            modelBuilder.Entity<Trantail_Payment>()
                .Property(e => e.Card_No)
                .IsUnicode(false);

            modelBuilder.Entity<Trantail_Payment>()
                .Property(e => e.Card_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Trantail_Payment>()
                .Property(e => e.Reference_No)
                .IsUnicode(false);

            modelBuilder.Entity<Trantail_SubLedger>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ClaimLoginMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Claim>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Claim>()
                .HasMany(e => e.ClaimLoginMaps)
                .WithOptional(e => e.Claim)
                .HasForeignKey(e => e.ClaimID);

            modelBuilder.Entity<Claim>()
                .HasMany(e => e.ClaimSetClaimMaps)
                .WithOptional(e => e.Claim)
                .HasForeignKey(e => e.ClaimID);

            modelBuilder.Entity<ClaimSetClaimMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ClaimSetClaimSetMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ClaimSetLoginMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ClaimSet>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ClaimSet>()
                .HasMany(e => e.ClaimSetClaimMaps)
                .WithOptional(e => e.ClaimSet)
                .HasForeignKey(e => e.ClaimSetID);

            modelBuilder.Entity<ClaimSet>()
                .HasMany(e => e.ClaimSetClaimSetMaps)
                .WithOptional(e => e.ClaimSet)
                .HasForeignKey(e => e.ClaimSetID);

            modelBuilder.Entity<ClaimSet>()
                .HasMany(e => e.ClaimSetClaimSetMaps1)
                .WithOptional(e => e.ClaimSet1)
                .HasForeignKey(e => e.LinkedClaimSetID);

            modelBuilder.Entity<ClaimSet>()
                .HasMany(e => e.ClaimSetLoginMaps)
                .WithOptional(e => e.ClaimSet)
                .HasForeignKey(e => e.ClaimSetID);

            modelBuilder.Entity<LoginRoleMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Login>()
                .Property(e => e.PasswordSalt)
                .IsUnicode(false);

            modelBuilder.Entity<Login>()
                .Property(e => e.RegisteredIP)
                .IsUnicode(false);

            modelBuilder.Entity<Login>()
                .Property(e => e.RegisteredIPCountry)
                .IsUnicode(false);

            modelBuilder.Entity<Login>()
                .Property(e => e.LastOTP)
                .IsUnicode(false);

            modelBuilder.Entity<Login>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Login>()
                .HasMany(e => e.ClaimLoginMaps)
                .WithOptional(e => e.Login)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<Login>()
                .HasMany(e => e.ClaimSetLoginMaps)
                .WithOptional(e => e.Login)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<Login>()
                .HasMany(e => e.LoginRoleMaps)
                .WithOptional(e => e.Login)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<Login>()
                .HasMany(e => e.CustomerCards)
                .WithOptional(e => e.Login)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<Login>()
                .HasMany(e => e.Customers)
                .WithOptional(e => e.Login)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<Login>()
                .HasMany(e => e.Employees)
                .WithOptional(e => e.Login)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<Login>()
                .HasMany(e => e.FilterColumnUserValues)
                .WithOptional(e => e.Login)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<Login>()
                .HasMany(e => e.NotificationAlerts)
                .WithOptional(e => e.Login)
                .HasForeignKey(e => e.FromLoginID);

            modelBuilder.Entity<Login>()
                .HasMany(e => e.NotificationAlerts1)
                .WithOptional(e => e.Login1)
                .HasForeignKey(e => e.ToLoginID);

            modelBuilder.Entity<Login>()
                .HasMany(e => e.Parents)
                .WithOptional(e => e.Login)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<Login>()
                .HasMany(e => e.ShareHolders)
                .WithOptional(e => e.Login)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<Login>()
                .HasMany(e => e.StudentApplications)
                .WithOptional(e => e.Login)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<Login>()
                .HasMany(e => e.Students)
                .WithOptional(e => e.Login)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<Login>()
                .HasMany(e => e.Suppliers)
                .WithOptional(e => e.Login)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<Login>()
                .HasMany(e => e.TaskAssingners)
                .WithOptional(e => e.Login)
                .HasForeignKey(e => e.AssingedToLoginID);

            modelBuilder.Entity<Login>()
                .HasMany(e => e.UserDataFormatMaps)
                .WithOptional(e => e.Login)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<Login>()
                .HasMany(e => e.UserScreenFieldSettings)
                .WithOptional(e => e.Login)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<Login>()
                .HasMany(e => e.UserSettings)
                .WithRequired(e => e.Login)
                .HasForeignKey(e => e.LoginID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Login>()
                .HasMany(e => e.UserViews)
                .WithOptional(e => e.Login)
                .HasForeignKey(e => e.LoginID);

            modelBuilder.Entity<PermissionCultureData>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Permission>()
                .HasMany(e => e.PermissionCultureDatas)
                .WithRequired(e => e.Permission)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RoleCultureData>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<RolePermissionMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Role>()
                .HasMany(e => e.RoleCultureDatas)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.TransactionHeads)
                .WithOptional(e => e.Role)
                .HasForeignKey(e => e.TransactionRole);

            modelBuilder.Entity<AssetCategory>()
                .Property(e => e.CategoryName)
                .IsUnicode(false);

            modelBuilder.Entity<Asset>()
                .HasMany(e => e.AssetTransactionDetails)
                .WithOptional(e => e.Asset)
                .HasForeignKey(e => e.AssetID);

            modelBuilder.Entity<AssetTransactionDetail>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<AssetTransactionHead>()
                .HasMany(e => e.AssetTransactionDetails)
                .WithOptional(e => e.AssetTransactionHead)
                .HasForeignKey(e => e.HeadID);

            modelBuilder.Entity<AssetTransactionHead>()
                .HasMany(e => e.AssetTransactionHeadAccountMaps)
                .WithRequired(e => e.AssetTransactionHead)
                .HasForeignKey(e => e.AssetTransactionHeadID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BrandCultureData>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<BrandImageMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Brand>()
                .Property(e => e.BrandCode)
                .IsUnicode(false);

            modelBuilder.Entity<Brand>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Brand>()
                .HasMany(e => e.BrandCultureDatas)
                .WithRequired(e => e.Brand)
                .HasForeignKey(e => e.BrandID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Brand>()
                .HasMany(e => e.BrandImageMaps)
                .WithOptional(e => e.Brand)
                .HasForeignKey(e => e.BrandID);

            modelBuilder.Entity<Brand>()
                .HasMany(e => e.BrandTagMaps)
                .WithOptional(e => e.Brand)
                .HasForeignKey(e => e.BrandID);

            modelBuilder.Entity<Brand>()
                .HasMany(e => e.BrandTags)
                .WithOptional(e => e.Brand)
                .HasForeignKey(e => e.BrandID);

            modelBuilder.Entity<Brand>()
                .HasMany(e => e.MenuLinkBrandMaps)
                .WithOptional(e => e.Brand)
                .HasForeignKey(e => e.BrandID);

            modelBuilder.Entity<Brand>()
                .HasMany(e => e.ProductPriceListBrandMaps)
                .WithOptional(e => e.Brand)
                .HasForeignKey(e => e.BrandID);

            modelBuilder.Entity<Brand>()
                .HasMany(e => e.ProductPriceListCustomerGroupMaps)
                .WithOptional(e => e.Brand)
                .HasForeignKey(e => e.BrandID);

            modelBuilder.Entity<Brand>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.Brand)
                .HasForeignKey(e => e.BrandID);

            modelBuilder.Entity<BrandStatus>()
                .HasMany(e => e.Brands)
                .WithOptional(e => e.BrandStatus)
                .HasForeignKey(e => e.StatusID);

            modelBuilder.Entity<BrandTag>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<BrandTag>()
                .HasMany(e => e.BrandTagMaps)
                .WithOptional(e => e.BrandTag)
                .HasForeignKey(e => e.BrandTagID);

            modelBuilder.Entity<Category>()
                .Property(e => e.CategoryCode)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Category>()
                .Property(e => e.Profit)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.CategoryCultureDatas)
                .WithRequired(e => e.Category)
                .HasForeignKey(e => e.CategoryID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.CategoryImageMaps)
                .WithOptional(e => e.Category)
                .HasForeignKey(e => e.CategoryID);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.CategorySettings)
                .WithOptional(e => e.Category)
                .HasForeignKey(e => e.CategoryID);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.CategoryTagMaps)
                .WithOptional(e => e.Category)
                .HasForeignKey(e => e.CategoryID);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.CategoryTags)
                .WithOptional(e => e.Category)
                .HasForeignKey(e => e.CategoryID);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.MenuLinkCategoryMaps)
                .WithOptional(e => e.Category)
                .HasForeignKey(e => e.CategoryID);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.ProductCategoryMaps)
                .WithOptional(e => e.Category)
                .HasForeignKey(e => e.CategoryID);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.ProductPriceListCategoryMaps)
                .WithOptional(e => e.Category)
                .HasForeignKey(e => e.CategoryID);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.ProductPriceListCustomerGroupMaps)
                .WithOptional(e => e.Category)
                .HasForeignKey(e => e.CategoryID);

            modelBuilder.Entity<CategoryCultureData>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<CategoryImageMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<CategoryImageMap>()
                .Property(e => e.ImageTitle)
                .IsUnicode(false);

            modelBuilder.Entity<CategoryTag>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<CategoryTag>()
                .HasMany(e => e.CategoryTagMaps)
                .WithOptional(e => e.CategoryTag)
                .HasForeignKey(e => e.CategoryTagID);

            modelBuilder.Entity<CustomerProductReference>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<DeliveryType>()
                .Property(e => e.DeliveryCost)
                .HasPrecision(3, 3);

            modelBuilder.Entity<DeliveryType>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<DeliveryType>()
                .HasMany(e => e.TransactionHeads)
                .WithOptional(e => e.DeliveryType)
                .HasForeignKey(e => e.DeliveryMethodID);

            modelBuilder.Entity<EmployeeCatalogRelation>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<MenuLinkBrandMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<MenuLinkCategoryMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<PackingType>()
                .Property(e => e.PackingCost)
                .HasPrecision(3, 3);

            modelBuilder.Entity<PackingType>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductBundle>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductBundle>()
                .Property(e => e.Quantity)
                .HasPrecision(18, 4);

            modelBuilder.Entity<ProductBundle>()
                .Property(e => e.SellingPrice)
                .HasPrecision(18, 4);

            modelBuilder.Entity<ProductBundle>()
                .Property(e => e.CostPrice)
                .HasPrecision(18, 4);

            modelBuilder.Entity<ProductCategoryMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductCultureData>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductFamily>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductFamily>()
                .HasMany(e => e.ProductFamilyCultureDatas)
                .WithRequired(e => e.ProductFamily)
                .HasForeignKey(e => e.ProductFamilyID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductFamily>()
                .HasMany(e => e.ProductFamilyPropertyMaps)
                .WithOptional(e => e.ProductFamily)
                .HasForeignKey(e => e.ProductFamilyID);

            modelBuilder.Entity<ProductFamily>()
                .HasMany(e => e.ProductFamilyPropertyTypeMaps)
                .WithOptional(e => e.ProductFamily)
                .HasForeignKey(e => e.ProductFamilyID);

            modelBuilder.Entity<ProductFamily>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.ProductFamily)
                .HasForeignKey(e => e.ProductFamilyID);

            modelBuilder.Entity<ProductFamilyCultureData>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductFamilyPropertyMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductFamilyPropertyTypeMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductFamilyType>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductImageMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductInventoryConfig>()
                .Property(e => e.NotifyQuantity)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductInventoryConfig>()
                .Property(e => e.MinimumQuantity)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductInventoryConfig>()
                .Property(e => e.MaximumQuantity)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductInventoryConfig>()
                .Property(e => e.MinimumQuanityInCart)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductInventoryConfig>()
                .Property(e => e.MaximumQuantityInCart)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductInventoryConfig>()
                .Property(e => e.ProductWeight)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductInventoryConfig>()
                .Property(e => e.ProductLength)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductInventoryConfig>()
                .Property(e => e.ProductWidth)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductInventoryConfig>()
                .Property(e => e.ProductHeight)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductInventoryConfig>()
                .Property(e => e.DimensionalWeight)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductInventoryConfig>()
                .Property(e => e.ProductCost)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductInventoryConfig>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductInventoryConfig>()
                .HasMany(e => e.ProductInventoryConfigCultureDatas)
                .WithRequired(e => e.ProductInventoryConfig)
                .HasForeignKey(e => e.ProductInventoryConfigID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductInventoryConfig>()
                .HasMany(e => e.ProductInventoryProductConfigMaps)
                .WithRequired(e => e.ProductInventoryConfig)
                .HasForeignKey(e => e.ProductInventoryConfigID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductInventoryConfig>()
                .HasMany(e => e.ProductInventorySKUConfigMaps)
                .WithRequired(e => e.ProductInventoryConfig)
                .HasForeignKey(e => e.ProductInventoryConfigID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductInventoryConfigTemp>()
                .Property(e => e.NotifyQuantity)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductInventoryConfigTemp>()
                .Property(e => e.MinimumQuantity)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductInventoryConfigTemp>()
                .Property(e => e.MaximumQuantity)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductInventoryConfigTemp>()
                .Property(e => e.MinimumQuanityInCart)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductInventoryConfigTemp>()
                .Property(e => e.MaximumQuantityInCart)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductInventoryConfigTemp>()
                .Property(e => e.ProductWeight)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductInventoryConfigTemp>()
                .Property(e => e.ProductLength)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductInventoryConfigTemp>()
                .Property(e => e.ProductWidth)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductInventoryConfigTemp>()
                .Property(e => e.ProductHeight)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductInventoryConfigTemp>()
                .Property(e => e.DimensionalWeight)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductInventoryConfigTemp>()
                .Property(e => e.HSCode)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ProductInventoryConfigTemp>()
                .Property(e => e.ProductCost)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductInventoryConfigTemp>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductInventoryProductConfigMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductInventorySKUConfigMap>()
                .Property(e => e.Price)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductInventorySKUConfigMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductInventorySKUConfigMap>()
                .HasOptional(e => e.ProductInventorySKUConfigMaps1)
                .WithRequired(e => e.ProductInventorySKUConfigMap1);

            modelBuilder.Entity<ProductPriceListBranchMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductPriceListBrandMap>()
                .Property(e => e.DiscountPrice)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductPriceListBrandMap>()
                .Property(e => e.DiscountPercentage)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductPriceListBrandMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductPriceListBrandMap>()
                .Property(e => e.Price)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductPriceListCategoryMap>()
                .Property(e => e.DiscountPrice)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductPriceListCategoryMap>()
                .Property(e => e.DiscountPercentage)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductPriceListCategoryMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductPriceListCategoryMap>()
                .Property(e => e.Price)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductPriceListCustomerGroupMap>()
                .Property(e => e.Quantity)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductPriceListCustomerGroupMap>()
                .Property(e => e.DiscountPrice)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductPriceListCustomerGroupMap>()
                .Property(e => e.DiscountPercentage)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductPriceListCustomerGroupMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductPriceListCustomerGroupMap>()
                .Property(e => e.Price)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductPriceListCustomerMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductPriceListProductMap>()
                .Property(e => e.Price)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductPriceListProductMap>()
                .Property(e => e.DiscountPrice)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductPriceListProductMap>()
                .Property(e => e.DiscountPercentage)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductPriceListProductMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductPriceListProductMap>()
                .Property(e => e.Cost)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductPriceListProductMap>()
                .HasMany(e => e.ProductPriceListProductQuantityMaps)
                .WithOptional(e => e.ProductPriceListProductMap)
                .HasForeignKey(e => e.ProductPriceListProductMapID);

            modelBuilder.Entity<ProductPriceListProductQuantityMap>()
                .Property(e => e.Quantity)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductPriceListProductQuantityMap>()
                .Property(e => e.DiscountPrice)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductPriceListProductQuantityMap>()
                .Property(e => e.DiscountPercentage)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductPriceListProductQuantityMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductPriceList>()
                .Property(e => e.PriceDescription)
                .IsUnicode(false);

            modelBuilder.Entity<ProductPriceList>()
                .Property(e => e.Price)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductPriceList>()
                .Property(e => e.PricePercentage)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductPriceList>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductPriceList>()
                .HasMany(e => e.ProductPriceListBranchMaps)
                .WithOptional(e => e.ProductPriceList)
                .HasForeignKey(e => e.ProductPriceListID);

            modelBuilder.Entity<ProductPriceList>()
                .HasMany(e => e.ProductPriceListBrandMaps)
                .WithOptional(e => e.ProductPriceList)
                .HasForeignKey(e => e.ProductPriceListID);

            modelBuilder.Entity<ProductPriceList>()
                .HasMany(e => e.ProductPriceListCategoryMaps)
                .WithOptional(e => e.ProductPriceList)
                .HasForeignKey(e => e.ProductPriceListID);

            modelBuilder.Entity<ProductPriceList>()
                .HasMany(e => e.ProductPriceListCustomerGroupMaps)
                .WithOptional(e => e.ProductPriceList)
                .HasForeignKey(e => e.ProductPriceListID);

            modelBuilder.Entity<ProductPriceList>()
                .HasMany(e => e.ProductPriceListCustomerMaps)
                .WithOptional(e => e.ProductPriceList)
                .HasForeignKey(e => e.ProductPriceListID);

            modelBuilder.Entity<ProductPriceList>()
                .HasMany(e => e.ProductPriceListProductMaps)
                .WithOptional(e => e.ProductPriceList)
                .HasForeignKey(e => e.ProductPriceListID);

            modelBuilder.Entity<ProductPriceList>()
                .HasMany(e => e.ProductPriceListSKUMaps)
                .WithOptional(e => e.ProductPriceList)
                .HasForeignKey(e => e.ProductPriceListID);

            modelBuilder.Entity<ProductPriceListSKUMap>()
                .Property(e => e.SellingQuantityLimit)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductPriceListSKUMap>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductPriceListSKUMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductPriceListSKUMap>()
                .Property(e => e.PricePercentage)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductPriceListSKUMap>()
                .Property(e => e.Price)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductPriceListSKUMap>()
                .Property(e => e.Discount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductPriceListSKUMap>()
                .Property(e => e.DiscountPercentage)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductPriceListSKUMap>()
                .Property(e => e.Cost)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductPriceListSKUMap>()
                .HasMany(e => e.ProductPriceListSKUQuantityMaps)
                .WithOptional(e => e.ProductPriceListSKUMap)
                .HasForeignKey(e => e.ProductPriceListSKUMapID);

            modelBuilder.Entity<ProductPriceListSKUQuantityMap>()
                .Property(e => e.Quantity)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductPriceListSKUQuantityMap>()
                .Property(e => e.DiscountPrice)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductPriceListSKUQuantityMap>()
                .Property(e => e.DiscountPercentage)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductPriceListSKUQuantityMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductPropertyMapCultureData>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductPropertyMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Product>()
                .Property(e => e.Weight)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Product>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductBundles)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.FromProductID);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductBundles1)
                .WithOptional(e => e.Product1)
                .HasForeignKey(e => e.ToProductID);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductCategoryMaps)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.ProductID);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductCultureDatas)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.ProductID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductImageMaps)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.ProductID);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductInventoryProductConfigMaps)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.ProductID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductPriceListCustomerGroupMaps)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.ProductID);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductPriceListProductMaps)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.ProductID);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductPriceListProductQuantityMaps)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.ProductID);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductPropertyMaps)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.ProductID);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductDeliveryCountrySettings)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.ProductID);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductDeliveryTypeMaps)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.ProductID);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductLocationMaps)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.ProductID);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductSKUMaps)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.ProductID);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductSKURackMaps)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.ProductID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductTagMaps)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.ProductID);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductTags)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.ProductID);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductToProductMaps)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.ProductID);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductToProductMaps1)
                .WithOptional(e => e.Product1)
                .HasForeignKey(e => e.ProductIDTo);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductVideoMaps)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.ProductID);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.TicketProductMaps)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.ProductID);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.TransactionDetails)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.ProductID);

            modelBuilder.Entity<ProductSKUCultureData>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductSKUMap>()
                .Property(e => e.ProductPrice)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductSKUMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductSKUMap>()
                .Property(e => e.ProductSKUMapIIDTEXT)
                .IsUnicode(false);

            modelBuilder.Entity<ProductSKUMap>()
                .HasMany(e => e.AccountTransactionDetails)
                .WithOptional(e => e.ProductSKUMap)
                .HasForeignKey(e => e.ProductSKUId);

            modelBuilder.Entity<ProductSKUMap>()
                .HasMany(e => e.CustomerProductReferences)
                .WithOptional(e => e.ProductSKUMap)
                .HasForeignKey(e => e.ProductSKUMapID);

            modelBuilder.Entity<ProductSKUMap>()
                .HasMany(e => e.ProductBundles)
                .WithOptional(e => e.ProductSKUMap)
                .HasForeignKey(e => e.ToProductSKUMapID);

            modelBuilder.Entity<ProductSKUMap>()
                .HasMany(e => e.ProductBundles1)
                .WithOptional(e => e.ProductSKUMap1)
                .HasForeignKey(e => e.FromProductSKUMapID);

            modelBuilder.Entity<ProductSKUMap>()
                .HasMany(e => e.ProductImageMaps)
                .WithOptional(e => e.ProductSKUMap)
                .HasForeignKey(e => e.ProductSKUMapID);

            modelBuilder.Entity<ProductSKUMap>()
                .HasMany(e => e.ProductInventorySKUConfigMaps)
                .WithRequired(e => e.ProductSKUMap)
                .HasForeignKey(e => e.ProductSKUMapID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductSKUMap>()
                .HasMany(e => e.ProductPriceListCustomerGroupMaps)
                .WithOptional(e => e.ProductSKUMap)
                .HasForeignKey(e => e.ProductSKUMapID);

            modelBuilder.Entity<ProductSKUMap>()
                .HasMany(e => e.ProductPriceListSKUMaps)
                .WithOptional(e => e.ProductSKUMap)
                .HasForeignKey(e => e.ProductSKUID);

            modelBuilder.Entity<ProductSKUMap>()
                .HasMany(e => e.ProductPriceListSKUQuantityMaps)
                .WithOptional(e => e.ProductSKUMap)
                .HasForeignKey(e => e.ProductSKUMapID);

            modelBuilder.Entity<ProductSKUMap>()
                .HasMany(e => e.ProductPropertyMaps)
                .WithOptional(e => e.ProductSKUMap)
                .HasForeignKey(e => e.ProductSKUMapID);

            modelBuilder.Entity<ProductSKUMap>()
                .HasMany(e => e.ProductSKUCultureDatas)
                .WithRequired(e => e.ProductSKUMap)
                .HasForeignKey(e => e.ProductSKUMapID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductSKUMap>()
                .HasMany(e => e.InventoryVerifications)
                .WithOptional(e => e.ProductSKUMap)
                .HasForeignKey(e => e.ProductSKUMapID);

            modelBuilder.Entity<ProductSKUMap>()
                .HasMany(e => e.InvetoryTransactions)
                .WithOptional(e => e.ProductSKUMap)
                .HasForeignKey(e => e.ProductSKUMapID);

            modelBuilder.Entity<ProductSKUMap>()
                .HasMany(e => e.JobEntryDetails)
                .WithOptional(e => e.ProductSKUMap)
                .HasForeignKey(e => e.ProductSKUID);

            modelBuilder.Entity<ProductSKUMap>()
                .HasMany(e => e.Notifies)
                .WithOptional(e => e.ProductSKUMap)
                .HasForeignKey(e => e.ProductSKUMapID);

            modelBuilder.Entity<ProductSKUMap>()
                .HasMany(e => e.ProductDeliveryCountrySettings)
                .WithOptional(e => e.ProductSKUMap)
                .HasForeignKey(e => e.ProductSKUMapID);

            modelBuilder.Entity<ProductSKUMap>()
                .HasMany(e => e.ProductDeliveryTypeMaps)
                .WithOptional(e => e.ProductSKUMap)
                .HasForeignKey(e => e.ProductSKUMapID);

            modelBuilder.Entity<ProductSKUMap>()
                .HasMany(e => e.ProductInventories)
                .WithRequired(e => e.ProductSKUMap)
                .HasForeignKey(e => e.ProductSKUMapID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductSKUMap>()
                .HasMany(e => e.ProductInventorySerialMaps)
                .WithRequired(e => e.ProductSKUMap)
                .HasForeignKey(e => e.ProductSKUMapID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductSKUMap>()
                .HasMany(e => e.ProductLocationMaps)
                .WithOptional(e => e.ProductSKUMap)
                .HasForeignKey(e => e.ProductSKUMapID);

            modelBuilder.Entity<ProductSKUMap>()
                .HasMany(e => e.ProductSerialMaps)
                .WithOptional(e => e.ProductSKUMap)
                .HasForeignKey(e => e.ProductSKUMapID);

            modelBuilder.Entity<ProductSKUMap>()
                .HasMany(e => e.ProductSKURackMaps)
                .WithRequired(e => e.ProductSKUMap)
                .HasForeignKey(e => e.ProductSKUMapID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductSKUMap>()
                .HasMany(e => e.ProductSKUSiteMaps)
                .WithOptional(e => e.ProductSKUMap)
                .HasForeignKey(e => e.ProductSKUMapID);

            modelBuilder.Entity<ProductSKUMap>()
                .HasMany(e => e.ProductSKUTagMaps)
                .WithOptional(e => e.ProductSKUMap)
                .HasForeignKey(e => e.ProductSKuMapID);

            modelBuilder.Entity<ProductSKUMap>()
                .HasMany(e => e.ProductVideoMaps)
                .WithOptional(e => e.ProductSKUMap)
                .HasForeignKey(e => e.ProductSKUMapID);

            modelBuilder.Entity<ProductSKUMap>()
                .HasMany(e => e.SKUPaymentMethodExceptionMaps)
                .WithOptional(e => e.ProductSKUMap)
                .HasForeignKey(e => e.SKUID);

            modelBuilder.Entity<ProductSKUMap>()
                .HasMany(e => e.TicketProductMaps)
                .WithOptional(e => e.ProductSKUMap)
                .HasForeignKey(e => e.ProductSKUMapID);

            modelBuilder.Entity<ProductSKUMap>()
                .HasMany(e => e.TransactionDetails)
                .WithOptional(e => e.ProductSKUMap)
                .HasForeignKey(e => e.ProductSKUMapID);

            modelBuilder.Entity<ProductSKURackMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductSKUTag>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductSKUTag>()
                .HasMany(e => e.ProductSKUTagMaps)
                .WithOptional(e => e.ProductSKUTag)
                .HasForeignKey(e => e.ProductSKUTagID);

            modelBuilder.Entity<ProductStatu>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.ProductStatu)
                .HasForeignKey(e => e.StatusID);

            modelBuilder.Entity<ProductStatu>()
                .HasMany(e => e.ProductSKUMaps)
                .WithOptional(e => e.ProductStatu)
                .HasForeignKey(e => e.StatusID);

            modelBuilder.Entity<ProductTag>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductTag>()
                .HasMany(e => e.ProductTagMaps)
                .WithOptional(e => e.ProductTag)
                .HasForeignKey(e => e.ProductTagID);

            modelBuilder.Entity<ProductToProductMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductVideoMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Property>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Property>()
                .HasMany(e => e.ProductFamilyPropertyMaps)
                .WithOptional(e => e.Property)
                .HasForeignKey(e => e.ProductPropertyID);

            modelBuilder.Entity<Property>()
                .HasMany(e => e.ProductPropertyMaps)
                .WithOptional(e => e.Property)
                .HasForeignKey(e => e.PropertyID);

            modelBuilder.Entity<Property>()
                .HasMany(e => e.PropertyCultureDatas)
                .WithRequired(e => e.Property)
                .HasForeignKey(e => e.PropertyID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Property>()
                .HasMany(e => e.PropertyTypes)
                .WithMany(e => e.Properties1)
                .Map(m => m.ToTable("PropertyTypePropertyMaps", "catalog").MapLeftKey("PropertyID").MapRightKey("PropertyTypeID"));

            modelBuilder.Entity<PropertyCultureData>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<PropertyTypeCultureData>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<PropertyType>()
                .HasMany(e => e.Properties)
                .WithOptional(e => e.PropertyType)
                .HasForeignKey(e => e.PropertyTypeID);

            modelBuilder.Entity<Rack>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Rack>()
                .HasMany(e => e.ProductSKURackMaps)
                .WithRequired(e => e.Rack)
                .HasForeignKey(e => e.RackID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ShoppingCart>()
                .Property(e => e.Price)
                .IsFixedLength();

            modelBuilder.Entity<UnitGroup>()
                .Property(e => e.UnitGroupCode)
                .IsUnicode(false);

            modelBuilder.Entity<UnitGroup>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<UnitGroup>()
                .HasMany(e => e.ProductPriceListSKUMaps)
                .WithOptional(e => e.UnitGroup)
                .HasForeignKey(e => e.UnitGroundID);

            modelBuilder.Entity<Unit>()
                .Property(e => e.UnitCode)
                .IsUnicode(false);

            modelBuilder.Entity<Unit>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Banner>()
                .Property(e => e.ReferenceID)
                .IsUnicode(false);

            modelBuilder.Entity<Banner>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<BannerStatus>()
                .HasMany(e => e.Banners)
                .WithOptional(e => e.BannerStatus)
                .HasForeignKey(e => e.StatusID);

            modelBuilder.Entity<BoilerPlate>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<BoilerPlate>()
                .Property(e => e.Template)
                .IsUnicode(false);

            modelBuilder.Entity<BoilerPlate>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<BoilerPlate>()
                .HasOptional(e => e.BoilerPlates1)
                .WithRequired(e => e.BoilerPlate1);

            modelBuilder.Entity<CategoryPageBoilerPlatMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<CustomerJustAsk>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<CustomerSupportTicket>()
                .Property(e => e.TransactionNo)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerSupportTicket>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<DeliveryTypeMaster>()
                .HasMany(e => e.DeliveryTypeCategoryMasters)
                .WithRequired(e => e.DeliveryTypeMaster)
                .HasForeignKey(e => e.RefDeliveryTypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<News>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<PageBoilerplateMapParameterCultureData>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<PageBoilerplateMapParameter>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<PageBoilerplateMapParameter>()
                .HasMany(e => e.PageBoilerplateMapParameterCultureDatas)
                .WithRequired(e => e.PageBoilerplateMapParameter)
                .HasForeignKey(e => e.PageBoilerplateMapParameterID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PageBoilerplateMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<PageBoilerplateMap>()
                .HasMany(e => e.CategoryPageBoilerPlatMaps)
                .WithOptional(e => e.PageBoilerplateMap)
                .HasForeignKey(e => e.PageBoilerplateMapID);

            modelBuilder.Entity<PageBoilerplateMap>()
                .HasMany(e => e.PageBoilerplateMapParameters)
                .WithOptional(e => e.PageBoilerplateMap)
                .HasForeignKey(e => e.PageBoilerplateMapID);

            modelBuilder.Entity<Page>()
                .Property(e => e.TemplateName)
                .IsUnicode(false);

            modelBuilder.Entity<Page>()
                .Property(e => e.PlaceHolder)
                .IsUnicode(false);

            modelBuilder.Entity<Page>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Page>()
                .HasMany(e => e.Pages1)
                .WithOptional(e => e.Page1)
                .HasForeignKey(e => e.ParentPageID);

            modelBuilder.Entity<Page>()
                .HasMany(e => e.Sites)
                .WithOptional(e => e.Page)
                .HasForeignKey(e => e.HomePageID);

            modelBuilder.Entity<Page>()
                .HasMany(e => e.Sites1)
                .WithOptional(e => e.Page1)
                .HasForeignKey(e => e.MasterPageID);

            modelBuilder.Entity<Site>()
                .HasMany(e => e.Pages)
                .WithOptional(e => e.Site)
                .HasForeignKey(e => e.SiteID);

            modelBuilder.Entity<Site>()
                .HasMany(e => e.SiteCountryMaps)
                .WithRequired(e => e.Site)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StaticContentType>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Subscription>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<UserJobApplication>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<AlbumImageMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<EventAudienceMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Event>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Event>()
                .HasMany(e => e.EventAudienceMaps)
                .WithOptional(e => e.Event)
                .HasForeignKey(e => e.EventID);

            modelBuilder.Entity<PollAnswerMap>()
                .HasMany(e => e.SchoolPollAnswerLogs)
                .WithOptional(e => e.PollAnswerMap)
                .HasForeignKey(e => e.PollAnswerMapID);

            modelBuilder.Entity<Poll>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Poll>()
                .HasMany(e => e.PollAnswerMaps)
                .WithOptional(e => e.Poll)
                .HasForeignKey(e => e.PollID);

            modelBuilder.Entity<Poll>()
                .HasMany(e => e.SchoolPollAnswerLogs)
                .WithOptional(e => e.Poll)
                .HasForeignKey(e => e.PollID);

            modelBuilder.Entity<QuestionnaireAnswer>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<QuestionnaireAnswer>()
                .HasMany(e => e.MemberQuestionnaireAnswerMaps)
                .WithOptional(e => e.QuestionnaireAnswer)
                .HasForeignKey(e => e.QuestionnaireAnswerID);

            modelBuilder.Entity<Questionnaire>()
                .HasMany(e => e.QuestionnaireAnswers)
                .WithOptional(e => e.Questionnaire)
                .HasForeignKey(e => e.QuestionnaireID);

            modelBuilder.Entity<Questionnaire>()
                .HasMany(e => e.MemberQuestionnaireAnswerMaps)
                .WithOptional(e => e.Questionnaire)
                .HasForeignKey(e => e.QuestionnaireID);

            modelBuilder.Entity<Channel>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<CreatedByType>()
                .HasMany(e => e.Members)
                .WithOptional(e => e.CreatedByType)
                .HasForeignKey(e => e.CreatedBy);

            modelBuilder.Entity<Family>()
                .Property(e => e.BlockID)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Family>()
                .Property(e => e.LandPhone)
                .IsUnicode(false);

            modelBuilder.Entity<Family>()
                .Property(e => e.HouseOwnerShipType)
                .IsFixedLength();

            modelBuilder.Entity<Family>()
                .Property(e => e.HouseType)
                .IsFixedLength();

            modelBuilder.Entity<Family>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Family>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Family>()
                .HasMany(e => e.Members)
                .WithOptional(e => e.Family)
                .HasForeignKey(e => e.FamilyID);

            modelBuilder.Entity<MemberQuestionnaireAnswerMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Member>()
                .Property(e => e.Sex)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.MaritalStatus)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.EducationDetails)
                .WithOptional(e => e.Member)
                .HasForeignKey(e => e.MemberID);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.MemberHealths)
                .WithOptional(e => e.Member)
                .HasForeignKey(e => e.MemberID);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.MemberPartners)
                .WithOptional(e => e.Member)
                .HasForeignKey(e => e.SpouseMemberID);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.MemberQuestionnaireAnswerMaps)
                .WithOptional(e => e.Member)
                .HasForeignKey(e => e.MemberID);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.SocialServices)
                .WithOptional(e => e.Member)
                .HasForeignKey(e => e.MemberID);

            modelBuilder.Entity<Organization>()
                .Property(e => e.RegistrationID)
                .IsUnicode(false);

            modelBuilder.Entity<Organization>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Organization>()
                .HasMany(e => e.Organizations1)
                .WithOptional(e => e.Organization1)
                .HasForeignKey(e => e.ParentOrganizationID);

            modelBuilder.Entity<SocialService>()
                .Property(e => e.IncomePerMonth)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ContentFile>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Lead>()
                .HasMany(e => e.Communications)
                .WithOptional(e => e.Lead)
                .HasForeignKey(e => e.LeadID);

            modelBuilder.Entity<Lead>()
                .HasMany(e => e.Opportunities)
                .WithOptional(e => e.Lead)
                .HasForeignKey(e => e.LeadID);

            modelBuilder.Entity<Opportunity>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Source>()
                .HasMany(e => e.Leads)
                .WithOptional(e => e.Source)
                .HasForeignKey(e => e.LeadSourceID);

            modelBuilder.Entity<Source>()
                .HasMany(e => e.Opportunities)
                .WithOptional(e => e.Source)
                .HasForeignKey(e => e.SourcesID);

            modelBuilder.Entity<RefundStatus>()
                .Property(e => e.RefundStatusName)
                .IsUnicode(false);

            modelBuilder.Entity<RefundType>()
                .Property(e => e.RefundTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<SupportAction>()
                .HasMany(e => e.Tickets)
                .WithRequired(e => e.SupportAction)
                .HasForeignKey(e => e.ActionID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TicketActionDetailDetailMap>()
                .Property(e => e.Timestamps)
                .IsFixedLength();

            modelBuilder.Entity<TicketActionDetailMap>()
                .Property(e => e.Timestamps)
                .IsFixedLength();

            modelBuilder.Entity<TicketActionDetailMap>()
                .HasMany(e => e.TicketActionDetailDetailMaps)
                .WithRequired(e => e.TicketActionDetailMap)
                .HasForeignKey(e => e.TicketActionDetailMapID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TicketBankDetail>()
                .Property(e => e.BankName)
                .IsUnicode(false);

            modelBuilder.Entity<TicketBankDetail>()
                .Property(e => e.RefundAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<TicketCashDetail>()
                .Property(e => e.RefundAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<TicketProcessingStatus>()
                .HasMany(e => e.Tickets)
                .WithOptional(e => e.TicketProcessingStatus)
                .HasForeignKey(e => e.TicketProcessingStatusID);

            modelBuilder.Entity<TicketProductMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<TicketProductMap>()
                .Property(e => e.Quantity)
                .HasPrecision(18, 0);

            modelBuilder.Entity<TicketProductMap>()
                .Property(e => e.SerialNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Ticket>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Ticket>()
                .HasMany(e => e.TicketActionDetailMaps)
                .WithRequired(e => e.Ticket)
                .HasForeignKey(e => e.TicketID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ticket>()
                .HasMany(e => e.TicketProductMaps)
                .WithOptional(e => e.Ticket)
                .HasForeignKey(e => e.TicketID);

            modelBuilder.Entity<Ticket>()
                .HasMany(e => e.Tickets1)
                .WithOptional(e => e.Ticket1)
                .HasForeignKey(e => e.ReferenceTicketID);

            modelBuilder.Entity<TicketStatus>()
                .HasMany(e => e.DocumentReferenceTicketStatusMaps)
                .WithRequired(e => e.TicketStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderDeliveryHolidayHead>()
                .HasMany(e => e.OrderDeliveryHolidayDetails)
                .WithOptional(e => e.OrderDeliveryHolidayHead)
                .HasForeignKey(e => e.OrderDeliveryHolidayHeadID);

            modelBuilder.Entity<Route>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ServiceProviderCountryGroup>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ServiceProviderLog>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ServiceProvider>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<DocFileType>()
                .Property(e => e.FileExtension)
                .IsUnicode(false);

            modelBuilder.Entity<DocumentFile>()
                .Property(e => e.FileName)
                .IsUnicode(false);

            modelBuilder.Entity<DocumentFile>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<DocumentFile>()
                .Property(e => e.Tags)
                .IsUnicode(false);

            modelBuilder.Entity<DocumentFile>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<DocumentFile>()
                .Property(e => e.Version)
                .IsUnicode(false);

            modelBuilder.Entity<DocumentFile>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<DocumentFileStatus>()
                .Property(e => e.StatusName)
                .IsFixedLength();

            modelBuilder.Entity<CandidateOnlineExamMap>()
                .HasMany(e => e.CandidateAnswers)
                .WithOptional(e => e.CandidateOnlineExamMap)
                .HasForeignKey(e => e.CandidateOnlineExamMapID);

            modelBuilder.Entity<CandidateOnlineExamMap>()
                .HasMany(e => e.CandidateAssesments)
                .WithOptional(e => e.CandidateOnlineExamMap)
                .HasForeignKey(e => e.CandidateOnlinExamMapID);

            modelBuilder.Entity<Candidate>()
                .HasMany(e => e.CandidateOnlineExamMaps)
                .WithOptional(e => e.Candidate)
                .HasForeignKey(e => e.CandidateID);

            modelBuilder.Entity<OnlineExamStatus>()
                .HasMany(e => e.CandidateOnlineExamMaps)
                .WithOptional(e => e.OnlineExamStatus)
                .HasForeignKey(e => e.OnlineExamStatusID);

            modelBuilder.Entity<QuestionOptionMap>()
                .HasMany(e => e.CandidateAnswers)
                .WithOptional(e => e.QuestionOptionMap)
                .HasForeignKey(e => e.CandidateOnlineExamMapID);

            modelBuilder.Entity<QuestionOptionMap>()
                .HasMany(e => e.CandidateAnswers1)
                .WithOptional(e => e.QuestionOptionMap1)
                .HasForeignKey(e => e.QuestionOptionMapID);

            modelBuilder.Entity<QuestionOptionMap>()
                .HasMany(e => e.CandidateAssesments)
                .WithOptional(e => e.QuestionOptionMap)
                .HasForeignKey(e => e.SelectedQuestionOptionMapID);

            modelBuilder.Entity<QuestionOptionMap>()
                .HasMany(e => e.CandidateAssesments1)
                .WithOptional(e => e.QuestionOptionMap1)
                .HasForeignKey(e => e.AnswerQuestionOptionMapID);

            modelBuilder.Entity<QuestionOptionMap>()
                .HasMany(e => e.QuestionAnswerMaps)
                .WithOptional(e => e.QuestionOptionMap)
                .HasForeignKey(e => e.QuestionOptionMapID);

            modelBuilder.Entity<Question>()
                .Property(e => e.Points)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.QuestionAnswerMaps)
                .WithOptional(e => e.Question)
                .HasForeignKey(e => e.QuestionID);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.QuestionOptionMaps)
                .WithOptional(e => e.Question)
                .HasForeignKey(e => e.QuestionID);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.QuestionImageMaps)
                .WithOptional(e => e.Question)
                .HasForeignKey(e => e.QuestionID);

            modelBuilder.Entity<QuestionSelection>()
                .HasMany(e => e.OnlineExams)
                .WithRequired(e => e.QuestionSelection)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DataFeedLog>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<DataFeedOperation>()
                .Property(e => e.OperationName)
                .IsUnicode(false);

            modelBuilder.Entity<DataFeedTableColumn>()
                .Property(e => e.PhysicalColumnName)
                .IsUnicode(false);

            modelBuilder.Entity<DataFeedTableColumn>()
                .Property(e => e.DisplayColumnName)
                .IsUnicode(false);

            modelBuilder.Entity<DataFeedTableColumn>()
                .Property(e => e.DataType)
                .IsUnicode(false);

            modelBuilder.Entity<DataFeedTable>()
                .Property(e => e.TableName)
                .IsUnicode(false);

            modelBuilder.Entity<DataFeedTable>()
                .HasMany(e => e.DataFeedTableColumns)
                .WithRequired(e => e.DataFeedTable)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DataFeedType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<DataFeedType>()
                .Property(e => e.TemplateName)
                .IsUnicode(false);

            modelBuilder.Entity<DataFeedType>()
                .Property(e => e.ProcessingSPName)
                .IsUnicode(false);

            modelBuilder.Entity<ApplicationForm>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<ApplicationForm>()
                .Property(e => e.ContactNo)
                .IsUnicode(false);

            modelBuilder.Entity<ApplicationForm>()
                .Property(e => e.CountryOfResidence)
                .IsUnicode(false);

            modelBuilder.Entity<ApplicationForm>()
                .Property(e => e.YearsOfExperience)
                .IsUnicode(false);

            modelBuilder.Entity<ApplicationForm>()
                .Property(e => e.PositionAppliedFor)
                .IsUnicode(false);

            modelBuilder.Entity<ApplicationForm>()
                .Property(e => e.CV)
                .IsUnicode(false);

            modelBuilder.Entity<ApplicationForm>()
                .Property(e => e.Nationality)
                .IsUnicode(false);

            modelBuilder.Entity<ApplicationForm>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<ApplicationForm>()
                .Property(e => e.Qualification)
                .IsUnicode(false);

            modelBuilder.Entity<ApplicationForm>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<AvailableJobCultureData>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<AvailableJob>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<AvailableJob>()
                .HasMany(e => e.AvailableJobTags)
                .WithOptional(e => e.AvailableJob)
                .HasForeignKey(e => e.JobID);

            modelBuilder.Entity<AvailableJobTag>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<DepartmentTag>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<BranchDocumentTypeMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<CustomerGroupDeliveryTypeMap>()
                .Property(e => e.CartTotalFrom)
                .HasPrecision(18, 3);

            modelBuilder.Entity<CustomerGroupDeliveryTypeMap>()
                .Property(e => e.CartTotalTo)
                .HasPrecision(18, 3);

            modelBuilder.Entity<CustomerGroupDeliveryTypeMap>()
                .Property(e => e.DeliveryCharge)
                .HasPrecision(18, 3);

            modelBuilder.Entity<CustomerGroupDeliveryTypeMap>()
                .Property(e => e.DeliveryChargePercentage)
                .HasPrecision(18, 3);

            modelBuilder.Entity<CustomerGroupDeliveryTypeMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<DeliveryCharge>()
                .Property(e => e.CartStartRange)
                .HasPrecision(18, 3);

            modelBuilder.Entity<DeliveryCharge>()
                .Property(e => e.CartEndRange)
                .HasPrecision(18, 3);

            modelBuilder.Entity<DeliveryCharge>()
                .Property(e => e.WeightStartRange)
                .HasPrecision(18, 3);

            modelBuilder.Entity<DeliveryCharge>()
                .Property(e => e.WeightEndRange)
                .HasPrecision(18, 3);

            modelBuilder.Entity<DeliveryCharge>()
                .Property(e => e.DeliveryCharge1)
                .HasPrecision(18, 3);

            modelBuilder.Entity<DeliveryCharge>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<DeliveryCharge>()
                .Property(e => e.WeightRangeDivisor)
                .HasPrecision(18, 3);

            modelBuilder.Entity<DeliveryCharge>()
                .Property(e => e.WeightChargeDivisor)
                .HasPrecision(18, 3);

            modelBuilder.Entity<DeliveryCharge>()
                .HasOptional(e => e.DeliveryCharges1)
                .WithRequired(e => e.DeliveryCharge2);

            modelBuilder.Entity<DeliveryTimeSlot>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<DeliveryTypeAllowedAreaMap>()
                .Property(e => e.CartTotalFrom)
                .HasPrecision(18, 3);

            modelBuilder.Entity<DeliveryTypeAllowedAreaMap>()
                .Property(e => e.CartTotalTo)
                .HasPrecision(18, 3);

            modelBuilder.Entity<DeliveryTypeAllowedAreaMap>()
                .Property(e => e.DeliveryCharge)
                .HasPrecision(18, 3);

            modelBuilder.Entity<DeliveryTypeAllowedAreaMap>()
                .Property(e => e.DeliveryChargePercentage)
                .HasPrecision(18, 3);

            modelBuilder.Entity<DeliveryTypeAllowedAreaMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<DeliveryTypeAllowedCountryMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<DeliveryTypeAllowedZoneMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<DeliveryTypeAllowedZoneMap>()
                .Property(e => e.DeliveryCharge)
                .HasPrecision(18, 3);

            modelBuilder.Entity<DeliveryTypeAllowedZoneMap>()
                .Property(e => e.DeliveryChargePercentage)
                .HasPrecision(18, 3);

            modelBuilder.Entity<DeliveryTypeAllowedZoneMap>()
                .Property(e => e.CartTotalFrom)
                .HasPrecision(18, 3);

            modelBuilder.Entity<DeliveryTypeAllowedZoneMap>()
                .Property(e => e.CartTotalTo)
                .HasPrecision(18, 3);

            modelBuilder.Entity<DeliveryTypes1>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<DeliveryTypes1>()
                .HasMany(e => e.DeliveryTypeAllowedCountryMaps)
                .WithRequired(e => e.DeliveryTypes1)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DeliveryTypes1>()
                .HasMany(e => e.PaymentExceptionByZoneDeliveries)
                .WithRequired(e => e.DeliveryTypes1)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DeliveryTypeStatus>()
                .HasMany(e => e.DeliveryTypes1)
                .WithOptional(e => e.DeliveryTypeStatus)
                .HasForeignKey(e => e.StatusID);

            modelBuilder.Entity<DeliveryTypeTimeSlotMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<DeliveryTypeTimeSlotMap>()
                .HasMany(e => e.DeliveryTypeTimeSlotMapsCultures)
                .WithRequired(e => e.DeliveryTypeTimeSlotMap)
                .HasForeignKey(e => e.DeliveryTypeTimeSlotMapID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<InventoryVerification>()
                .Property(e => e.StockQuantity)
                .HasPrecision(18, 3);

            modelBuilder.Entity<InventoryVerification>()
                .Property(e => e.VerifiedQuantity)
                .HasPrecision(18, 3);

            modelBuilder.Entity<InventoryVerification>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<InvetoryTransaction>()
                .Property(e => e.Cost)
                .HasPrecision(18, 3);

            modelBuilder.Entity<InvetoryTransaction>()
                .Property(e => e.Quantity)
                .HasPrecision(18, 3);

            modelBuilder.Entity<InvetoryTransaction>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<InvetoryTransaction>()
                .Property(e => e.Rate)
                .HasPrecision(18, 3);

            modelBuilder.Entity<InvetoryTransaction>()
                .Property(e => e.Discount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<InvetoryTransaction>()
                .Property(e => e.ExchangeRate)
                .HasPrecision(18, 3);

            modelBuilder.Entity<InvetoryTransaction>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<InvetoryTransaction>()
                .HasMany(e => e.InvetoryTransactions1)
                .WithOptional(e => e.InvetoryTransaction1)
                .HasForeignKey(e => e.LinkDocumentID);

            modelBuilder.Entity<Location>()
                .Property(e => e.Barcode)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Location>()
                .HasMany(e => e.ProductLocationMaps)
                .WithOptional(e => e.Location)
                .HasForeignKey(e => e.LocationID);

            modelBuilder.Entity<ProductDeliveryCountrySetting>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductDeliveryTypeMap>()
                .Property(e => e.DeliveryCharge)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductDeliveryTypeMap>()
                .Property(e => e.DeliveryChargePercentage)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductDeliveryTypeMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductDeliveryTypeMap>()
                .Property(e => e.CartTotalFrom)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductDeliveryTypeMap>()
                .Property(e => e.CartTotalTo)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductInventory>()
                .Property(e => e.Quantity)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductInventory>()
                .Property(e => e.CostPrice)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductLocationMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductSerialMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductTypeDeliveryTypeMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ShareHolder>()
                .Property(e => e.ShareHolderID)
                .IsUnicode(false);

            modelBuilder.Entity<ShareHolder>()
                .Property(e => e.AmountOfInvestment)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ShareHolder>()
                .Property(e => e.MemberShipCard)
                .IsUnicode(false);

            modelBuilder.Entity<ShareHolder>()
                .Property(e => e.CreditLimit)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ShareHolder>()
                .Property(e => e.Balance)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ShareHolder>()
                .Property(e => e.MobileNumber)
                .IsUnicode(false);

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

            modelBuilder.Entity<ShoppingCart1>()
                .HasMany(e => e.ShoppingCartItems)
                .WithOptional(e => e.ShoppingCart)
                .HasForeignKey(e => e.ShoppingCartID);

            modelBuilder.Entity<ShoppingCart1>()
                .HasMany(e => e.ShoppingCartVoucherMaps)
                .WithOptional(e => e.ShoppingCart)
                .HasForeignKey(e => e.ShoppingCartID);

            modelBuilder.Entity<ShoppingCart1>()
                .HasMany(e => e.TransactionHeadShoppingCartMaps)
                .WithOptional(e => e.ShoppingCart)
                .HasForeignKey(e => e.ShoppingCartID);

            modelBuilder.Entity<Tax>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<TaxTemplateItem>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<TaxTransaction>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<TaxTransaction>()
                .Property(e => e.InclusiveAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<TaxTransaction>()
                .Property(e => e.ExclusiveAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<TransactionAllocation>()
                .Property(e => e.Quantity)
                .HasPrecision(18, 5);

            modelBuilder.Entity<TransactionAllocation>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<TransactionAllocation>()
                .HasOptional(e => e.TransactionAllocations1)
                .WithRequired(e => e.TransactionAllocation1);

            modelBuilder.Entity<TransactionDetail>()
                .Property(e => e.Quantity)
                .HasPrecision(18, 3);

            modelBuilder.Entity<TransactionDetail>()
                .Property(e => e.DiscountPercentage)
                .HasPrecision(18, 3);

            modelBuilder.Entity<TransactionDetail>()
                .Property(e => e.UnitPrice)
                .HasPrecision(18, 3);

            modelBuilder.Entity<TransactionDetail>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<TransactionDetail>()
                .Property(e => e.ExchangeRate)
                .HasPrecision(18, 3);

            modelBuilder.Entity<TransactionDetail>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<TransactionDetail>()
                .Property(e => e.SerialNumber)
                .IsUnicode(false);

            modelBuilder.Entity<TransactionDetail>()
                .Property(e => e.TaxAmount1)
                .HasPrecision(18, 3);

            modelBuilder.Entity<TransactionDetail>()
                .Property(e => e.TaxAmount2)
                .HasPrecision(18, 3);

            modelBuilder.Entity<TransactionDetail>()
                .Property(e => e.TaxPercentage)
                .HasPrecision(8, 3);

            modelBuilder.Entity<TransactionDetail>()
                .Property(e => e.InclusiveTaxAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<TransactionDetail>()
                .Property(e => e.ExclusiveTaxAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<TransactionDetail>()
                .HasMany(e => e.ProductSerialMaps)
                .WithOptional(e => e.TransactionDetail)
                .HasForeignKey(e => e.DetailID);

            modelBuilder.Entity<TransactionDetail>()
                .HasMany(e => e.TransactionAllocations)
                .WithOptional(e => e.TransactionDetail)
                .HasForeignKey(e => e.TrasactionDetailID);

            modelBuilder.Entity<TransactionDetail>()
                .HasMany(e => e.TransactionDetails1)
                .WithOptional(e => e.TransactionDetail1)
                .HasForeignKey(e => e.ParentDetailID);

            modelBuilder.Entity<TransactionHead>()
                .Property(e => e.TransactionNo)
                .IsUnicode(false);

            modelBuilder.Entity<TransactionHead>()
                .Property(e => e.DiscountAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<TransactionHead>()
                .Property(e => e.DiscountPercentage)
                .HasPrecision(6, 3);

            modelBuilder.Entity<TransactionHead>()
                .Property(e => e.ExchangeRate)
                .HasPrecision(18, 3);

            modelBuilder.Entity<TransactionHead>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<TransactionHead>()
                .Property(e => e.DeliveryCharge)
                .HasPrecision(18, 3);

            modelBuilder.Entity<TransactionHead>()
                .Property(e => e.ToalAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<TransactionHead>()
                .Property(e => e.ExternalReference1)
                .IsUnicode(false);

            modelBuilder.Entity<TransactionHead>()
                .Property(e => e.ExternalReference2)
                .IsUnicode(false);

            modelBuilder.Entity<TransactionHead>()
                .HasMany(e => e.OrderDeliveryDisplayHeadMaps)
                .WithRequired(e => e.TransactionHead)
                .HasForeignKey(e => e.HeadID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TransactionHead>()
                .HasMany(e => e.InvetoryTransactions)
                .WithOptional(e => e.TransactionHead)
                .HasForeignKey(e => e.HeadID);

            modelBuilder.Entity<TransactionHead>()
                .HasMany(e => e.ShoppingCarts)
                .WithOptional(e => e.TransactionHead)
                .HasForeignKey(e => e.BlockedHeadID);

            modelBuilder.Entity<TransactionHead>()
                .HasMany(e => e.TaxTransactions)
                .WithOptional(e => e.TransactionHead)
                .HasForeignKey(e => e.HeadID);

            modelBuilder.Entity<TransactionHead>()
                .HasMany(e => e.TransactionDetails)
                .WithOptional(e => e.TransactionHead)
                .HasForeignKey(e => e.HeadID);

            modelBuilder.Entity<TransactionHead>()
                .HasMany(e => e.JobEntryHeads)
                .WithOptional(e => e.TransactionHead)
                .HasForeignKey(e => e.TransactionHeadID);

            modelBuilder.Entity<TransactionHead>()
                .HasMany(e => e.OrderContactMaps)
                .WithOptional(e => e.TransactionHead)
                .HasForeignKey(e => e.OrderID);

            modelBuilder.Entity<TransactionHead>()
                .HasMany(e => e.OrderRoleTrackings)
                .WithRequired(e => e.TransactionHead)
                .HasForeignKey(e => e.TransactionHeadID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TransactionHead>()
                .HasMany(e => e.TransactionHead1)
                .WithOptional(e => e.TransactionHead2)
                .HasForeignKey(e => e.ReferenceHeadID);

            modelBuilder.Entity<TransactionHead>()
                .HasMany(e => e.TransactionHeadAccountMaps)
                .WithOptional(e => e.TransactionHead)
                .HasForeignKey(e => e.TransactionHeadID);

            modelBuilder.Entity<TransactionHead>()
                .HasMany(e => e.TransactionHeadEntitlementMaps)
                .WithRequired(e => e.TransactionHead)
                .HasForeignKey(e => e.TransactionHeadID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TransactionHead>()
                .HasMany(e => e.TransactionHeadPayablesMaps)
                .WithOptional(e => e.TransactionHead)
                .HasForeignKey(e => e.HeadID);

            modelBuilder.Entity<TransactionHead>()
                .HasMany(e => e.TransactionHeadPointsMaps)
                .WithRequired(e => e.TransactionHead)
                .HasForeignKey(e => e.TransactionHeadID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TransactionHead>()
                .HasMany(e => e.TransactionHeadReceivablesMaps)
                .WithRequired(e => e.TransactionHead)
                .HasForeignKey(e => e.HeadID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TransactionHead>()
                .HasMany(e => e.TransactionHeadShoppingCartMaps)
                .WithOptional(e => e.TransactionHead)
                .HasForeignKey(e => e.TransactionHeadID);

            modelBuilder.Entity<TransactionHead>()
                .HasMany(e => e.TransactionShipments)
                .WithOptional(e => e.TransactionHead)
                .HasForeignKey(e => e.TransactionHeadID);

            modelBuilder.Entity<TransactionHead>()
                .HasMany(e => e.WorkflowTransactionHeadMaps)
                .WithOptional(e => e.TransactionHead)
                .HasForeignKey(e => e.TransactionHeadID);

            modelBuilder.Entity<TransactionHeadAccountMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<TransactionHeadEntitlementMap>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<TransactionHeadShoppingCartMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<TransactionHeadShoppingCartMap>()
                .Property(e => e.DeliveryCharge)
                .HasPrecision(18, 3);

            modelBuilder.Entity<TransactionShipment>()
                .Property(e => e.FreightCharges)
                .HasPrecision(18, 3);

            modelBuilder.Entity<TransactionShipment>()
                .Property(e => e.BrokerCharges)
                .HasPrecision(18, 3);

            modelBuilder.Entity<TransactionShipment>()
                .Property(e => e.AdditionalCharges)
                .HasPrecision(18, 3);

            modelBuilder.Entity<TransactionShipment>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<TransactionStatus>()
                .HasMany(e => e.AssetTransactionHeads)
                .WithOptional(e => e.TransactionStatus)
                .HasForeignKey(e => e.ProcessingStatusID);

            modelBuilder.Entity<Voucher>()
                .Property(e => e.VoucherPin)
                .IsUnicode(false);

            modelBuilder.Entity<Voucher>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Voucher>()
                .Property(e => e.MinimumAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Voucher>()
                .Property(e => e.CurrentBalance)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Voucher>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Voucher>()
                .HasMany(e => e.ShoppingCartVoucherMaps)
                .WithOptional(e => e.Voucher)
                .HasForeignKey(e => e.VoucherID);

            modelBuilder.Entity<VoucherStatus>()
                .HasMany(e => e.Vouchers)
                .WithOptional(e => e.VoucherStatus)
                .HasForeignKey(e => e.StatusID);

            modelBuilder.Entity<Basket>()
                .Property(e => e.Barcode)
                .IsUnicode(false);

            modelBuilder.Entity<Basket>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<JobActivity>()
                .HasMany(e => e.JobStatuses)
                .WithOptional(e => e.JobActivity)
                .HasForeignKey(e => e.JobTypeID);

            modelBuilder.Entity<JobEntryDetail>()
                .Property(e => e.UnitPrice)
                .HasPrecision(18, 3);

            modelBuilder.Entity<JobEntryDetail>()
                .Property(e => e.Quantity)
                .HasPrecision(18, 3);

            modelBuilder.Entity<JobEntryDetail>()
                .Property(e => e.ValidatedQuantity)
                .HasPrecision(18, 3);

            modelBuilder.Entity<JobEntryDetail>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<JobEntryDetail>()
                .Property(e => e.AWBNo)
                .IsUnicode(false);

            modelBuilder.Entity<JobEntryHead>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<JobEntryHead>()
                .Property(e => e.AirWaybillNo)
                .IsUnicode(false);

            modelBuilder.Entity<JobEntryHead>()
                .HasMany(e => e.JobEntryDetails)
                .WithOptional(e => e.JobEntryHead)
                .HasForeignKey(e => e.JobEntryHeadID);

            modelBuilder.Entity<JobEntryHead>()
                .HasMany(e => e.JobEntryDetails1)
                .WithOptional(e => e.JobEntryHead1)
                .HasForeignKey(e => e.ParentJobEntryHeadID);

            modelBuilder.Entity<JobEntryHead>()
                .HasMany(e => e.JobEntryHeads1)
                .WithOptional(e => e.JobEntryHead1)
                .HasForeignKey(e => e.ParentJobEntryHeadId);

            modelBuilder.Entity<JobEntryHead>()
                .HasMany(e => e.JobsEntryHeadPayableMaps)
                .WithOptional(e => e.JobEntryHead)
                .HasForeignKey(e => e.JobEntryHeadID);

            modelBuilder.Entity<JobEntryHead>()
                .HasMany(e => e.JobsEntryHeadReceivableMaps)
                .WithOptional(e => e.JobEntryHead)
                .HasForeignKey(e => e.JobEntryHeadID);

            modelBuilder.Entity<ActionLinkType>()
                .Property(e => e.ActionLinkTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<ApplicationSubmitType>()
                .HasMany(e => e.StudentApplications)
                .WithOptional(e => e.ApplicationSubmitType)
                .HasForeignKey(e => e.ApplicationTypeID);

            modelBuilder.Entity<Area>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Area>()
                .HasMany(e => e.Areas1)
                .WithOptional(e => e.Area1)
                .HasForeignKey(e => e.ParentAreaID);

            modelBuilder.Entity<AssignVehicleAttendantMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<AssignVehicleMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<AssignVehicleMap>()
                .HasMany(e => e.AssignVehicleAttendantMaps)
                .WithRequired(e => e.AssignVehicleMap)
                .HasForeignKey(e => e.AssignVehicleMapID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Attachment>()
                .Property(e => e.AttachmentName)
                .IsUnicode(false);

            modelBuilder.Entity<Attachment>()
                .Property(e => e.FileName)
                .IsUnicode(false);

            modelBuilder.Entity<Attachment>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Bank>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Bank>()
                .HasMany(e => e.EmployeeBankDetails)
                .WithOptional(e => e.Bank)
                .HasForeignKey(e => e.BankID);

            modelBuilder.Entity<BranchCultureData>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Branch>()
                .Property(e => e.BranchName)
                .IsUnicode(false);

            modelBuilder.Entity<Branch>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Branch>()
                .Property(e => e.Longitude)
                .IsUnicode(false);

            modelBuilder.Entity<Branch>()
                .Property(e => e.Latitude)
                .IsUnicode(false);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.AccountTransactionHeads)
                .WithOptional(e => e.Branch)
                .HasForeignKey(e => e.BranchID);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.ProductPriceListBranchMaps)
                .WithOptional(e => e.Branch)
                .HasForeignKey(e => e.BranchID);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.BranchDocumentTypeMaps)
                .WithOptional(e => e.Branch)
                .HasForeignKey(e => e.BranchID);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.InventoryVerifications)
                .WithOptional(e => e.Branch)
                .HasForeignKey(e => e.BranchID);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.InvetoryTransactions)
                .WithOptional(e => e.Branch)
                .HasForeignKey(e => e.BranchID);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.Locations)
                .WithOptional(e => e.Branch)
                .HasForeignKey(e => e.BranchID);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.ProductDeliveryTypeMaps)
                .WithOptional(e => e.Branch)
                .HasForeignKey(e => e.BranchID);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.ProductInventories)
                .WithRequired(e => e.Branch)
                .HasForeignKey(e => e.BranchID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.ProductInventorySerialMaps)
                .WithRequired(e => e.Branch)
                .HasForeignKey(e => e.BranchID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.TransactionHeads)
                .WithOptional(e => e.Branch)
                .HasForeignKey(e => e.BranchID);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.TransactionHeads1)
                .WithOptional(e => e.Branch1)
                .HasForeignKey(e => e.ToBranchID);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.JobEntryHeads)
                .WithOptional(e => e.Branch)
                .HasForeignKey(e => e.BranchID);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.BranchCultureDatas)
                .WithRequired(e => e.Branch)
                .HasForeignKey(e => e.BranchID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.Employees)
                .WithOptional(e => e.Branch)
                .HasForeignKey(e => e.BranchID);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.Saloons)
                .WithOptional(e => e.Branch)
                .HasForeignKey(e => e.BranchID);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.Suppliers)
                .WithOptional(e => e.Branch)
                .HasForeignKey(e => e.BlockedBranchID);

            modelBuilder.Entity<BranchGroup>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<BranchGroup>()
                .HasMany(e => e.Branches)
                .WithOptional(e => e.BranchGroup)
                .HasForeignKey(e => e.BranchGroupID);

            modelBuilder.Entity<BranchGroupStatus>()
                .HasMany(e => e.BranchGroups)
                .WithOptional(e => e.BranchGroupStatus)
                .HasForeignKey(e => e.StatusID);

            modelBuilder.Entity<BranchStatus>()
                .HasMany(e => e.Branches)
                .WithOptional(e => e.BranchStatus)
                .HasForeignKey(e => e.StatusID);

            modelBuilder.Entity<City>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<City>()
                .HasMany(e => e.Vehicles)
                .WithOptional(e => e.City)
                .HasForeignKey(e => e.RigistrationCityID);

            modelBuilder.Entity<Comment>()
                .Property(e => e.Comment1)
                .IsUnicode(false);

            modelBuilder.Entity<Comment>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Comment>()
                .HasMany(e => e.Comments1)
                .WithOptional(e => e.Comment2)
                .HasForeignKey(e => e.ParentCommentID);

            modelBuilder.Entity<Company>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Company>()
                .HasMany(e => e.CompanyCurrencyMaps)
                .WithRequired(e => e.Company)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.Currencies)
                .WithOptional(e => e.Company)
                .HasForeignKey(e => e.CompanyID);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.Employees)
                .WithOptional(e => e.Company)
                .HasForeignKey(e => e.ResidencyCompanyId);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.Settings)
                .WithRequired(e => e.Company)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.UserSettings)
                .WithRequired(e => e.Company)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CompanyCurrencyMap>()
                .Property(e => e.ExchangeRate)
                .HasPrecision(18, 3);

            modelBuilder.Entity<CompanyStatus>()
                .HasMany(e => e.Companies)
                .WithOptional(e => e.CompanyStatus)
                .HasForeignKey(e => e.StatusID);

            modelBuilder.Entity<Contact>()
                .Property(e => e.Latitude)
                .HasPrecision(12, 9);

            modelBuilder.Entity<Contact>()
                .Property(e => e.Longitude)
                .HasPrecision(12, 9);

            modelBuilder.Entity<Contact>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Contact>()
                .HasMany(e => e.Leads)
                .WithOptional(e => e.Contact)
                .HasForeignKey(e => e.ContactID);

            modelBuilder.Entity<Contact>()
                .HasMany(e => e.OrderContactMaps)
                .WithOptional(e => e.Contact)
                .HasForeignKey(e => e.ContactID);

            modelBuilder.Entity<Country>()
                .Property(e => e.TwoLetterCode)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.ThreeLetterCode)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Logins)
                .WithOptional(e => e.Country)
                .HasForeignKey(e => e.RegisteredCountryID);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.SiteCountryMaps)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.DeliveryCharges)
                .WithOptional(e => e.Country)
                .HasForeignKey(e => e.FromCountryID);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.DeliveryCharges1)
                .WithOptional(e => e.Country1)
                .HasForeignKey(e => e.ToCountryID);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.DeliveryTypeAllowedCountryMaps)
                .WithRequired(e => e.Country)
                .HasForeignKey(e => e.FromCountryID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.DeliveryTypeAllowedCountryMaps1)
                .WithRequired(e => e.Country1)
                .HasForeignKey(e => e.ToCountryID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Parents)
                .WithOptional(e => e.Country)
                .HasForeignKey(e => e.CountryID);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Parents1)
                .WithOptional(e => e.Country1)
                .HasForeignKey(e => e.FatherPassportCountryofIssueID);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Parents2)
                .WithOptional(e => e.Country2)
                .HasForeignKey(e => e.MotherPassportCountryofIssueID);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.PassportDetailMaps)
                .WithOptional(e => e.Country)
                .HasForeignKey(e => e.CountryofIssueID);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.PassportVisaDetails)
                .WithOptional(e => e.Country)
                .HasForeignKey(e => e.CountryofIssueID);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.StudentApplications)
                .WithOptional(e => e.Country)
                .HasForeignKey(e => e.CountryID);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.StudentApplications1)
                .WithOptional(e => e.Country1)
                .HasForeignKey(e => e.StudentCoutryOfBrithID);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.StudentPassportDetails)
                .WithOptional(e => e.Country)
                .HasForeignKey(e => e.CountryofIssueID);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.StudentPassportDetails1)
                .WithOptional(e => e.Country1)
                .HasForeignKey(e => e.CountryofBirthID);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Students)
                .WithOptional(e => e.Country)
                .HasForeignKey(e => e.CurrentCountryID);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Students1)
                .WithOptional(e => e.Country1)
                .HasForeignKey(e => e.PermenentCountryID);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Vehicles)
                .WithOptional(e => e.Country)
                .HasForeignKey(e => e.RigistrationCountryID);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.ServiceProviderCountryGroups)
                .WithMany(e => e.Countries)
                .Map(m => m.ToTable("ServiceProviderCountryGroupMaps", "distribution").MapLeftKey("CountryID").MapRightKey("CountryGroupID"));

            modelBuilder.Entity<Culture>()
                .Property(e => e.CultureCode)
                .IsUnicode(false);

            modelBuilder.Entity<Culture>()
                .HasMany(e => e.PermissionCultureDatas)
                .WithRequired(e => e.Culture)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Culture>()
                .HasMany(e => e.RoleCultureDatas)
                .WithRequired(e => e.Culture)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Culture>()
                .HasMany(e => e.BrandCultureDatas)
                .WithRequired(e => e.Culture)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Culture>()
                .HasMany(e => e.CategoryCultureDatas)
                .WithRequired(e => e.Culture)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Culture>()
                .HasMany(e => e.ProductCultureDatas)
                .WithRequired(e => e.Culture)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Culture>()
                .HasMany(e => e.ProductFamilyCultureDatas)
                .WithRequired(e => e.Culture)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Culture>()
                .HasMany(e => e.ProductFamilyTypes)
                .WithRequired(e => e.Culture)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Culture>()
                .HasMany(e => e.ProductInventoryConfigCultureDatas)
                .WithRequired(e => e.Culture)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Culture>()
                .HasMany(e => e.ProductSKUCultureDatas)
                .WithRequired(e => e.Culture)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Culture>()
                .HasMany(e => e.PropertyCultureDatas)
                .WithRequired(e => e.Culture)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Culture>()
                .HasMany(e => e.SalesRelationshipTypes)
                .WithRequired(e => e.Culture)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Culture>()
                .HasMany(e => e.CustomerJustAsks)
                .WithRequired(e => e.Culture)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Culture>()
                .HasMany(e => e.CustomerSupportTickets)
                .WithRequired(e => e.Culture)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Culture>()
                .HasMany(e => e.NewsletterSubscriptions)
                .WithRequired(e => e.Culture)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Culture>()
                .HasMany(e => e.PageBoilerplateMapParameterCultureDatas)
                .WithRequired(e => e.Culture)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Culture>()
                .HasMany(e => e.OrderDeliveryDisplayHeadMaps)
                .WithRequired(e => e.Culture)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Culture>()
                .HasMany(e => e.DeliveryTypeTimeSlotMapsCultures)
                .WithRequired(e => e.Culture)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Culture>()
                .HasMany(e => e.BranchCultureDatas)
                .WithRequired(e => e.Culture)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Culture>()
                .HasMany(e => e.MenuLinkCultureDatas)
                .WithRequired(e => e.Culture)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Culture>()
                .HasMany(e => e.SeoMetadataCultureDatas)
                .WithRequired(e => e.Culture)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Culture>()
                .HasMany(e => e.StatusesCultureDatas)
                .WithRequired(e => e.Culture)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Currency>()
                .Property(e => e.ExchangeRate)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.Companies)
                .WithOptional(e => e.Currency)
                .HasForeignKey(e => e.BaseCurrencyID);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.CompanyCurrencyMaps)
                .WithRequired(e => e.Currency)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CustomerGroup>()
                .Property(e => e.PointLimit)
                .HasPrecision(18, 3);

            modelBuilder.Entity<CustomerGroup>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<CustomerGroup>()
                .HasMany(e => e.ProductPriceListCustomerGroupMaps)
                .WithOptional(e => e.CustomerGroup)
                .HasForeignKey(e => e.CustomerGroupID);

            modelBuilder.Entity<CustomerGroup>()
                .HasMany(e => e.ProductPriceListSKUMaps)
                .WithOptional(e => e.CustomerGroup)
                .HasForeignKey(e => e.CustomerGroupID);

            modelBuilder.Entity<CustomerGroup>()
                .HasMany(e => e.CustomerGroupDeliveryTypeMaps)
                .WithOptional(e => e.CustomerGroup)
                .HasForeignKey(e => e.CustomerGroupID);

            modelBuilder.Entity<Customer>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Customer>()
                .Property(e => e.Telephone)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.ShareHolderID)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.AddressLatitude)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.AddressLongitude)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.CustomerAccountMaps)
                .WithRequired(e => e.Customer)
                .HasForeignKey(e => e.CustomerID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.CustomerProductReferences)
                .WithOptional(e => e.Customer)
                .HasForeignKey(e => e.CustomerID);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.ProductPriceListCustomerMaps)
                .WithOptional(e => e.Customer)
                .HasForeignKey(e => e.CustomerID);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Tickets)
                .WithOptional(e => e.Customer)
                .HasForeignKey(e => e.CustomerID);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.ShareHolders)
                .WithOptional(e => e.Customer)
                .HasForeignKey(e => e.CustomerID);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.TransactionHeads)
                .WithOptional(e => e.Customer)
                .HasForeignKey(e => e.CustomerID);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Vouchers)
                .WithOptional(e => e.Customer)
                .HasForeignKey(e => e.CustomerID);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.WishLists)
                .WithOptional(e => e.Customer)
                .HasForeignKey(e => e.CustomerID);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.CustomerCards)
                .WithOptional(e => e.Customer)
                .HasForeignKey(e => e.CustomerID);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Appointments)
                .WithOptional(e => e.Customer)
                .HasForeignKey(e => e.CustomerID);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.CustomerSettings)
                .WithOptional(e => e.Customer)
                .HasForeignKey(e => e.CustomerID);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.CustomerSupplierMaps)
                .WithOptional(e => e.Customer)
                .HasForeignKey(e => e.CustomerID);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.PaymentDetailsPayPals)
                .WithRequired(e => e.Customer)
                .HasForeignKey(e => e.RefCustomerID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.PaymentDetailsTheForts)
                .WithRequired(e => e.Customer)
                .HasForeignKey(e => e.CustomerID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CustomerSetting>()
                .Property(e => e.CurrentLoyaltyPoints)
                .HasPrecision(18, 3);

            modelBuilder.Entity<CustomerSetting>()
                .Property(e => e.TotalLoyaltyPoints)
                .HasPrecision(18, 3);

            modelBuilder.Entity<CustomerSetting>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<CustomerSupplierMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Departments1>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<DepartmentStatus>()
                .HasMany(e => e.Departments1)
                .WithOptional(e => e.DepartmentStatus)
                .HasForeignKey(e => e.StatusID);

            modelBuilder.Entity<DocumentReferenceStatusMap>()
                .HasMany(e => e.AssetTransactionHeads)
                .WithOptional(e => e.DocumentReferenceStatusMap)
                .HasForeignKey(e => e.DocumentStatusID);

            modelBuilder.Entity<DocumentReferenceStatusMap>()
                .HasMany(e => e.TransactionHeads)
                .WithOptional(e => e.DocumentReferenceStatusMap)
                .HasForeignKey(e => e.DocumentStatusID);

            modelBuilder.Entity<DocumentReferenceType>()
                .Property(e => e.InventoryTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<DocumentReferenceType>()
                .Property(e => e.System)
                .IsUnicode(false);

            modelBuilder.Entity<DocumentReferenceType>()
                .HasMany(e => e.DocumentReferenceTicketStatusMaps)
                .WithRequired(e => e.DocumentReferenceType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DocumentStatus>()
                .HasMany(e => e.DocumentReferenceStatusMaps)
                .WithRequired(e => e.DocumentStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DocumentType>()
                .Property(e => e.System)
                .IsUnicode(false);

            modelBuilder.Entity<DocumentType>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<DocumentType>()
                .HasMany(e => e.JobEntryHeads)
                .WithOptional(e => e.DocumentType)
                .HasForeignKey(e => e.ReferenceDocumentTypeID);

            modelBuilder.Entity<DocumentType>()
                .HasMany(e => e.JobEntryHeads1)
                .WithOptional(e => e.DocumentType1)
                .HasForeignKey(e => e.DocumentTypeID);

            modelBuilder.Entity<DocumentType>()
                .HasMany(e => e.DocumentTypeTransactionNumbers)
                .WithRequired(e => e.DocumentType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DocumentType>()
                .HasMany(e => e.DocumentTypeTypeMaps)
                .WithOptional(e => e.DocumentType)
                .HasForeignKey(e => e.DocumentTypeID);

            modelBuilder.Entity<DocumentType>()
                .HasMany(e => e.DocumentTypeTypeMaps1)
                .WithOptional(e => e.DocumentType1)
                .HasForeignKey(e => e.DocumentTypeMapID);

            modelBuilder.Entity<DocumentTypeTypeMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<EntitlementMap>()
                .Property(e => e.EntitlementAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<EntitlementMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<EntityProperty>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<EntityPropertyMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<EntityTypeEntitlement>()
                .HasMany(e => e.TransactionHeadEntitlementMaps)
                .WithRequired(e => e.EntityTypeEntitlement)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EntityTypePaymentMethodMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<EntityTypeRelationMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<EntityType>()
                .HasMany(e => e.Attachments)
                .WithRequired(e => e.EntityType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EntityType>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.EntityType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EntityType>()
                .HasMany(e => e.EntityTypeRelationMaps)
                .WithOptional(e => e.EntityType)
                .HasForeignKey(e => e.FromEntityTypeID);

            modelBuilder.Entity<EntityType>()
                .HasMany(e => e.EntityTypeRelationMaps1)
                .WithOptional(e => e.EntityType1)
                .HasForeignKey(e => e.ToEntityTypeID);

            modelBuilder.Entity<EntityType>()
                .HasMany(e => e.Workflows)
                .WithOptional(e => e.EntityType)
                .HasForeignKey(e => e.LinkedEntityTypeID);

            modelBuilder.Entity<GeoLocationLog>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<GeoLocationLog>()
                .Property(e => e.Latitude)
                .IsUnicode(false);

            modelBuilder.Entity<GeoLocationLog>()
                .Property(e => e.Longitude)
                .IsUnicode(false);

            modelBuilder.Entity<GeoLocationLog>()
                .Property(e => e.ReferenceID1)
                .IsUnicode(false);

            modelBuilder.Entity<GeoLocationLog>()
                .Property(e => e.ReferenceID2)
                .IsUnicode(false);

            modelBuilder.Entity<GeoLocationLog>()
                .Property(e => e.ReferenceID3)
                .IsUnicode(false);

            modelBuilder.Entity<GeoLocationLog>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Landmark>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Locations1>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Locations1>()
                .HasMany(e => e.Locations11)
                .WithOptional(e => e.Locations12)
                .HasForeignKey(e => e.ParentLocationID);

            modelBuilder.Entity<Nationality>()
                .HasMany(e => e.Employees)
                .WithOptional(e => e.Nationality)
                .HasForeignKey(e => e.NationalityID);

            modelBuilder.Entity<Nationality>()
                .HasMany(e => e.Parents)
                .WithOptional(e => e.Nationality)
                .HasForeignKey(e => e.FatherCountryID);

            modelBuilder.Entity<Nationality>()
                .HasMany(e => e.Parents1)
                .WithOptional(e => e.Nationality1)
                .HasForeignKey(e => e.MotherCountryID);

            modelBuilder.Entity<Nationality>()
                .HasMany(e => e.StudentApplications)
                .WithOptional(e => e.Nationality)
                .HasForeignKey(e => e.FatherCountryID);

            modelBuilder.Entity<Nationality>()
                .HasMany(e => e.StudentApplications1)
                .WithOptional(e => e.Nationality1)
                .HasForeignKey(e => e.MotherCountryID);

            modelBuilder.Entity<Nationality>()
                .HasMany(e => e.StudentApplications2)
                .WithOptional(e => e.Nationality2)
                .HasForeignKey(e => e.NationalityID);

            modelBuilder.Entity<Nationality>()
                .HasMany(e => e.StudentPassportDetails)
                .WithOptional(e => e.Nationality)
                .HasForeignKey(e => e.NationalityID);

            modelBuilder.Entity<PaymentGroup>()
                .HasOptional(e => e.PaymentGroups1)
                .WithRequired(e => e.PaymentGroup1);

            modelBuilder.Entity<PaymentGroup>()
                .HasMany(e => e.PaymentMethods)
                .WithMany(e => e.PaymentGroups)
                .Map(m => m.ToTable("PaymentGroupPaymentTypeMaps", "mutual").MapLeftKey("PaymentGroupID").MapRightKey("PaymentMethodID"));

            modelBuilder.Entity<PaymentMethod>()
                .HasMany(e => e.PaymentExceptionByZoneDeliveries)
                .WithRequired(e => e.PaymentMethod)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PaymentMethod>()
                .HasMany(e => e.PaymentMethodSiteMaps)
                .WithRequired(e => e.PaymentMethod)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PaymentMethodSiteMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<SeoMetadataCultureData>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<SeoMetadata>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<SeoMetadata>()
                .HasMany(e => e.Brands)
                .WithOptional(e => e.SeoMetadata)
                .HasForeignKey(e => e.SEOMetadataID);

            modelBuilder.Entity<SeoMetadata>()
                .HasMany(e => e.Categories)
                .WithOptional(e => e.SeoMetadata)
                .HasForeignKey(e => e.SeoMetadataID);

            modelBuilder.Entity<SeoMetadata>()
                .HasMany(e => e.ProductSKUMaps)
                .WithOptional(e => e.SeoMetadata)
                .HasForeignKey(e => e.SeoMetadataID);

            modelBuilder.Entity<SeoMetadata>()
                .HasMany(e => e.SeoMetadataCultureDatas)
                .WithRequired(e => e.SeoMetadata)
                .HasForeignKey(e => e.SEOMetadataID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.StatusesCultureDatas)
                .WithRequired(e => e.Status)
                .HasForeignKey(e => e.CultureID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Profit)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.SupplierAccountMaps)
                .WithOptional(e => e.Supplier)
                .HasForeignKey(e => e.SupplierID);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.Tickets)
                .WithOptional(e => e.Supplier)
                .HasForeignKey(e => e.SupplierID);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.TransactionHeads)
                .WithOptional(e => e.Supplier)
                .HasForeignKey(e => e.SupplierID);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.TransactionShipments)
                .WithOptional(e => e.Supplier)
                .HasForeignKey(e => e.SupplierIDFrom);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.TransactionShipments1)
                .WithOptional(e => e.Supplier1)
                .HasForeignKey(e => e.SupplierIDTo);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.CustomerSupplierMaps)
                .WithOptional(e => e.Supplier)
                .HasForeignKey(e => e.SupplierID);

            modelBuilder.Entity<SupplierStatus>()
                .HasMany(e => e.Suppliers)
                .WithOptional(e => e.SupplierStatus)
                .HasForeignKey(e => e.StatusID);

            modelBuilder.Entity<VehicleDetailMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<VehicleOwnershipType>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Vehicle>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Vehicle>()
                .HasMany(e => e.JobEntryHeads)
                .WithOptional(e => e.Vehicle)
                .HasForeignKey(e => e.VehicleID);

            modelBuilder.Entity<Vehicle>()
                .HasMany(e => e.AssignVehicleMaps)
                .WithOptional(e => e.Vehicle)
                .HasForeignKey(e => e.VehicleID);

            modelBuilder.Entity<Vehicle>()
                .HasMany(e => e.VehicleDetailMaps)
                .WithOptional(e => e.Vehicle)
                .HasForeignKey(e => e.VehicleID);

            modelBuilder.Entity<Vehicle>()
                .HasMany(e => e.RouteVehicleMaps)
                .WithOptional(e => e.Vehicle)
                .HasForeignKey(e => e.VehicleID);

            modelBuilder.Entity<Vehicle>()
                .HasMany(e => e.StudentVehicleAssigns)
                .WithRequired(e => e.Vehicle)
                .HasForeignKey(e => e.VehicleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VehicleTransmission>()
                .HasMany(e => e.Vehicles)
                .WithOptional(e => e.VehicleTransmission)
                .HasForeignKey(e => e.TransmissionID);

            modelBuilder.Entity<VehicleType>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Warehouse>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<WarehouseStatus>()
                .HasMany(e => e.Warehouses)
                .WithOptional(e => e.WarehouseStatus)
                .HasForeignKey(e => e.StatusID);

            modelBuilder.Entity<Zone>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Zone>()
                .HasMany(e => e.PaymentExceptionByZoneDeliveries)
                .WithRequired(e => e.Zone)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EmailNotificationData>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<EmailNotificationType>()
                .Property(e => e.TimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<EmailNotificationType>()
                .HasMany(e => e.EmailNotificationDatas)
                .WithRequired(e => e.EmailNotificationType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EmailTemplates1>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<NotificationAlert>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<NotificationsProcessing>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<NotificationStatus>()
                .HasMany(e => e.NotificationLogs)
                .WithRequired(e => e.NotificationStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NotificationType>()
                .HasMany(e => e.NotificationLogs)
                .WithRequired(e => e.NotificationType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NotificationType>()
                .HasMany(e => e.NotificationsProcessings)
                .WithRequired(e => e.NotificationType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NotificationType>()
                .HasMany(e => e.NotificationsQueues)
                .WithRequired(e => e.NotificationType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SMSNotificationType>()
                .Property(e => e.TimeStamp)
                .IsFixedLength();

            modelBuilder.Entity<SMSNotificationType>()
                .HasMany(e => e.SMSNotificationDatas)
                .WithRequired(e => e.SMSNotificationType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderContactMap>()
                .Property(e => e.Latitude)
                .HasPrecision(12, 9);

            modelBuilder.Entity<OrderContactMap>()
                .Property(e => e.Longitude)
                .HasPrecision(12, 9);

            modelBuilder.Entity<OrderContactMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<OrderContactMap>()
                .HasMany(e => e.JobEntryHeads)
                .WithOptional(e => e.OrderContactMap)
                .HasForeignKey(e => e.OrderContactMapID);

            modelBuilder.Entity<PaymentDetailsKnet>()
                .Property(e => e.InitStatus)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsKnet>()
                .Property(e => e.InitIP)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsKnet>()
                .Property(e => e.InitLocation)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsKnet>()
                .Property(e => e.PaymentAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<PaymentDetailsKnet>()
                .Property(e => e.PaymentAction)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsKnet>()
                .Property(e => e.PaymentCurrency)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsKnet>()
                .Property(e => e.PaymentLang)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsKnet>()
                .Property(e => e.InitErrorMsg)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsKnet>()
                .Property(e => e.TransResult)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsKnet>()
                .Property(e => e.TransPostDate)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsKnet>()
                .Property(e => e.TransAuth)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsKnet>()
                .Property(e => e.TransRef)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsKnet>()
                .Property(e => e.TransErrorMsg)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsKnet>()
                .Property(e => e.TransIP)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsKnet>()
                .Property(e => e.TransLocation)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsLogKnet>()
                .Property(e => e.TransResult)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsLogKnet>()
                .Property(e => e.TransPostDate)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsLogKnet>()
                .Property(e => e.TransAuth)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsLogKnet>()
                .Property(e => e.TransRef)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsMyFatoorah>()
                .Property(e => e.InitStatus)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsMyFatoorah>()
                .Property(e => e.InitIP)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsMyFatoorah>()
                .Property(e => e.InitLocation)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsMyFatoorah>()
                .Property(e => e.PaymentAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<PaymentDetailsMyFatoorah>()
                .Property(e => e.PaymentCurrency)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsMyFatoorah>()
                .Property(e => e.InitErrorMsg)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsMyFatoorah>()
                .Property(e => e.ReferenceID)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsMyFatoorah>()
                .Property(e => e.TransResult)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsMyFatoorah>()
                .Property(e => e.TransRef)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsMyFatoorah>()
                .Property(e => e.TransErrorMsg)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsMyFatoorah>()
                .Property(e => e.TransIP)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsMyFatoorah>()
                .Property(e => e.TransLocation)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsMyFatoorah>()
                .Property(e => e.PaymentMode)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsMyFatoorah>()
                .Property(e => e.PaymentMethod)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsMyFatoorah>()
                .Property(e => e.PayRequestResponseCode)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsMyFatoorah>()
                .Property(e => e.PaymentURL)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsMyFatoorah>()
                .Property(e => e.OrderStatusResponse_AuthID)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsMyFatoorah>()
                .Property(e => e.OrderStatusResponse_OrderID)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsMyFatoorah>()
                .Property(e => e.OrderStatusResponse_PayTxnID)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsMyFatoorah>()
                .Property(e => e.OrderStatusResponse_Paymode)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsMyFatoorah>()
                .Property(e => e.OrderStatusResponse_PostDate)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsMyFatoorah>()
                .Property(e => e.OrderStatusResponse_RefID)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsMyFatoorah>()
                .Property(e => e.OrderStatusResponse_ResponseCode)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsMyFatoorah>()
                .Property(e => e.OrderStatusResponse_ResponseMessage)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsMyFatoorah>()
                .Property(e => e.OrderStatusResponse_TransID)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsMyFatoorah>()
                .Property(e => e.OrderStatusResponse_GrossAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<PaymentDetailsMyFatoorah>()
                .Property(e => e.OrderStatusResponse_NetAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<PaymentDetailsMyFatoorah>()
                .Property(e => e.OrderStatusResponse_Result)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsMyFatoorah>()
                .Property(e => e.udf1)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsMyFatoorah>()
                .Property(e => e.udf2)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsMyFatoorah>()
                .Property(e => e.udf3)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsMyFatoorah>()
                .Property(e => e.udf4)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsMyFatoorah>()
                .Property(e => e.udf5)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsPayPal>()
                .Property(e => e.InitStatus)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsPayPal>()
                .Property(e => e.InitIP)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsPayPal>()
                .Property(e => e.InitLocation)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsPayPal>()
                .Property(e => e.InitAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<PaymentDetailsPayPal>()
                .Property(e => e.ExRateUSD)
                .HasPrecision(18, 3);

            modelBuilder.Entity<PaymentDetailsPayPal>()
                .Property(e => e.TransAmountActualKWD)
                .HasPrecision(18, 3);

            modelBuilder.Entity<PaymentDetailsPayPal>()
                .Property(e => e.PaypalDataTransferData)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsTheFort>()
                .Property(e => e.InitStatus)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsTheFort>()
                .Property(e => e.InitIP)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsTheFort>()
                .Property(e => e.InitLocation)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsTheFort>()
                .Property(e => e.InitAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<PaymentDetailsTheFort>()
                .Property(e => e.PLang)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsTheFort>()
                .Property(e => e.PTransCurrency)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsTheFort>()
                .Property(e => e.PTransEci)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsTheFort>()
                .Property(e => e.PTransAuthorizationCode)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsTheFort>()
                .Property(e => e.PTransCustomerIP)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsTheFort>()
                .Property(e => e.PTransCardNumber)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsTheFort>()
                .Property(e => e.PTransActualAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<PaymentDetailsTheFort>()
                .Property(e => e.TLanguage)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentDetailsTheFort>()
                .Property(e => e.CardHolderName)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentExceptionByZoneDelivery>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<PaymentMasterVisa>()
                .Property(e => e.InitStatus)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentMasterVisa>()
                .Property(e => e.InitIP)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentMasterVisa>()
                .Property(e => e.InitLocation)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentMasterVisa>()
                .Property(e => e.VpcVersion)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentMasterVisa>()
                .Property(e => e.VpcCommand)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentMasterVisa>()
                .Property(e => e.AccessCode)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentMasterVisa>()
                .Property(e => e.MerchantID)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentMasterVisa>()
                .Property(e => e.VpcLocale)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentMasterVisa>()
                .Property(e => e.PaymentAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<PaymentMasterVisa>()
                .Property(e => e.PaymentCurrency)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentMasterVisa>()
                .Property(e => e.ResponseCode)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentMasterVisa>()
                .Property(e => e.CodeDescription)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentMasterVisa>()
                .Property(e => e.Message)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentMasterVisa>()
                .Property(e => e.ReceiptNumber)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentMasterVisa>()
                .Property(e => e.TransID)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentMasterVisa>()
                .Property(e => e.AcquireResponseCode)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentMasterVisa>()
                .Property(e => e.BankAuthorizationID)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentMasterVisa>()
                .Property(e => e.BatchNo)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentMasterVisa>()
                .Property(e => e.CardType)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentMasterVisa>()
                .Property(e => e.ResponseIP)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentMasterVisa>()
                .Property(e => e.ResponseLocation)
                .IsUnicode(false);

            modelBuilder.Entity<Attendence>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<EmployeeBankDetail>()
                .Property(e => e.BankName)
                .IsUnicode(false);

            modelBuilder.Entity<EmployeeBankDetail>()
                .Property(e => e.BankDetails)
                .IsUnicode(false);

            modelBuilder.Entity<EmployeeBankDetail>()
                .Property(e => e.AccountHolderName)
                .IsUnicode(false);

            modelBuilder.Entity<EmployeeBankDetail>()
                .Property(e => e.AccountNo)
                .IsUnicode(false);

            modelBuilder.Entity<EmployeeBankDetail>()
                .Property(e => e.IBAN)
                .IsUnicode(false);

            modelBuilder.Entity<EmployeeBankDetail>()
                .Property(e => e.SwiftCode)
                .IsUnicode(false);

            modelBuilder.Entity<EmployeeBankDetail>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<EmployeeBankDetail>()
                .HasMany(e => e.Employees)
                .WithOptional(e => e.EmployeeBankDetail1)
                .HasForeignKey(e => e.EmployeeBankID);

            modelBuilder.Entity<EmployeeGrade>()
                .Property(e => e.GradeName)
                .IsUnicode(false);

            modelBuilder.Entity<EmployeeRoleMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Employee>()
                .Property(e => e.WorkEmail)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.WorkPhone)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.WorkMobileNo)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Employee>()
                .Property(e => e.PersonalMobileNo)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.CivilIDNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.SponsorDetails)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.AdhaarCardNo)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Tickets)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Tickets1)
                .WithOptional(e => e.Employee1)
                .HasForeignKey(e => e.ManagerEmployeeID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Tickets2)
                .WithOptional(e => e.Employee2)
                .HasForeignKey(e => e.AssingedEmployeeID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.DocumentFiles)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.OwnerEmployeeID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.InventoryVerifications)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.TransactionHeads)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.JobEntryHeads)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.AssignVehicleAttendantMaps)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.AssignVehicleMaps)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Suppliers)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Attendences)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeAdditionalInfos)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<Employee>()
                .HasOptional(e => e.EmployeeBankDetail)
                .WithRequired(e => e.Employee);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeRoleMaps)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Appointments)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.ClassSubjectMaps)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.ClassTeacherMaps)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Employees1)
                .WithOptional(e => e.Employee1)
                .HasForeignKey(e => e.ReportingEmployeeID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeSalaries)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeSalaryStructures)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeTimeSheets)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.LeaveApplicationApprovers)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.LeaveApplications)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.LeaveBlockListApprovers)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.LibraryStaffRegisters)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.LibraryTransactions)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.PassportVisaDetails)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.ReferenceID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.SalarySlips)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.SchoolPollAnswerLogs)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.StaffID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.ServiceEmployeeMaps)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.StaffAttendences)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.StaffRouteStopMaps)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.StaffID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.StudentMiscDetails)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.StaffID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.SubjectTeacherMaps)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.SubjectTopics)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.TeacherActivities)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.TimeTableAllocations)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.StaffID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.WorkflowLogMapRuleApproverMaps)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.WorkflowRuleApprovers)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.WorkflowTransactionRuleApproverMaps)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<EmployeeSalary>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<EmployeeSalary>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<EmployeeSalaryStructureComponentMap>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<EmployeeSalaryStructure>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<EmployeeSalaryStructure>()
                .Property(e => e.TimeSheetHourRate)
                .HasPrecision(18, 3);

            modelBuilder.Entity<EmployeeSalaryStructure>()
                .Property(e => e.TimeSheetLeaveEncashmentPerDay)
                .HasPrecision(18, 3);

            modelBuilder.Entity<EmployeeSalaryStructure>()
                .Property(e => e.TimeSheetMaximumBenefits)
                .HasPrecision(18, 3);

            modelBuilder.Entity<EmployeeSalaryStructure>()
                .HasMany(e => e.EmployeeSalaryStructureComponentMaps)
                .WithOptional(e => e.EmployeeSalaryStructure)
                .HasForeignKey(e => e.EmployeeSalaryStructureID);

            modelBuilder.Entity<EmployeeTimeSheet>()
                .Property(e => e.OTHours)
                .HasPrecision(5, 2);

            modelBuilder.Entity<EmployeeTimeSheet>()
                .Property(e => e.NormalHours)
                .HasPrecision(5, 2);

            modelBuilder.Entity<EmployeeTimeSheet>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<HolidayList>()
                .HasMany(e => e.Holidays)
                .WithOptional(e => e.HolidayList)
                .HasForeignKey(e => e.HolidayListID);

            modelBuilder.Entity<Holiday>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Holiday>()
                .HasMany(e => e.SchoolCalenderHolidayMaps)
                .WithOptional(e => e.Holiday)
                .HasForeignKey(e => e.HolidayID);

            modelBuilder.Entity<LeaveApplication>()
                .HasMany(e => e.LeaveApplicationApprovers)
                .WithOptional(e => e.LeaveApplication)
                .HasForeignKey(e => e.LeaveApplicationID);

            modelBuilder.Entity<LeaveBlockList>()
                .HasMany(e => e.LeaveBlockListApprovers)
                .WithOptional(e => e.LeaveBlockList)
                .HasForeignKey(e => e.LeaveBlockListID);

            modelBuilder.Entity<LeaveBlockList>()
                .HasMany(e => e.LeaveBlockListEntries)
                .WithOptional(e => e.LeaveBlockList)
                .HasForeignKey(e => e.LeaveBlockListID);

            modelBuilder.Entity<LeaveSession>()
                .HasMany(e => e.LeaveApplications)
                .WithOptional(e => e.LeaveSession)
                .HasForeignKey(e => e.FromSessionID);

            modelBuilder.Entity<LeaveSession>()
                .HasMany(e => e.LeaveApplications1)
                .WithOptional(e => e.LeaveSession1)
                .HasForeignKey(e => e.ToSessionID);

            modelBuilder.Entity<SalaryComponent>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<SalaryComponent>()
                .HasMany(e => e.EmployeeSalaryStructures)
                .WithOptional(e => e.SalaryComponent)
                .HasForeignKey(e => e.TimeSheetSalaryComponentID);

            modelBuilder.Entity<SalaryComponent>()
                .HasMany(e => e.SalaryStructures)
                .WithOptional(e => e.SalaryComponent)
                .HasForeignKey(e => e.TimeSheetSalaryComponentID);

            modelBuilder.Entity<SalaryComponentType>()
                .HasMany(e => e.SalaryComponents)
                .WithOptional(e => e.SalaryComponentType)
                .HasForeignKey(e => e.ComponentTypeID);

            modelBuilder.Entity<SalaryMethod>()
                .Property(e => e.SalaryMethodName)
                .IsUnicode(false);

            modelBuilder.Entity<SalaryPaymentMode>()
                .HasMany(e => e.EmployeeSalaryStructures)
                .WithOptional(e => e.SalaryPaymentMode)
                .HasForeignKey(e => e.PaymentModeID);

            modelBuilder.Entity<SalaryPaymentMode>()
                .HasMany(e => e.SalaryStructures)
                .WithOptional(e => e.SalaryPaymentMode)
                .HasForeignKey(e => e.PaymentModeID);

            modelBuilder.Entity<SalarySlip>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<SalarySlip>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<SalaryStructure>()
                .Property(e => e.TimeSheetHourRate)
                .HasPrecision(18, 3);

            modelBuilder.Entity<SalaryStructure>()
                .Property(e => e.TimeSheetLeaveEncashmentPerDay)
                .HasPrecision(18, 3);

            modelBuilder.Entity<SalaryStructure>()
                .Property(e => e.TimeSheetMaximumBenefits)
                .HasPrecision(18, 3);

            modelBuilder.Entity<SalaryStructureComponentMap>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<SalaryStructureComponentMap>()
                .Property(e => e.MinAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<SalaryStructureComponentMap>()
                .Property(e => e.MaxAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ServiceGroup>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ServiceGroup>()
                .HasMany(e => e.Services)
                .WithOptional(e => e.ServiceGroup)
                .HasForeignKey(e => e.ServiceGroupID);

            modelBuilder.Entity<ServicePricing>()
                .Property(e => e.Price)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ServicePricing>()
                .Property(e => e.DiscountPrice)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ServicePricing>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Service>()
                .HasMany(e => e.Appointments)
                .WithOptional(e => e.Service)
                .HasForeignKey(e => e.ServiceID);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.ServiceEmployeeMaps)
                .WithOptional(e => e.Service)
                .HasForeignKey(e => e.ServiceID);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.ServicePricings)
                .WithRequired(e => e.Service)
                .HasForeignKey(e => e.ServiceID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.Services1)
                .WithOptional(e => e.Service1)
                .HasForeignKey(e => e.ParentServiceID);

            modelBuilder.Entity<TreatmentGroup>()
                .HasMany(e => e.TreatmentTypes)
                .WithRequired(e => e.TreatmentGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Despatch>()
                .Property(e => e.TimeFrom)
                .IsUnicode(false);

            modelBuilder.Entity<Despatch>()
                .Property(e => e.TimeTo)
                .IsUnicode(false);

            modelBuilder.Entity<Despatch>()
                .Property(e => e.Rate)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Despatch>()
                .Property(e => e.ReceivedAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Despatch>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Despatch>()
                .Property(e => e.TaxAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Despatch>()
                .Property(e => e.TaxPercentage)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Despatch>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<EntityScheduler>()
                .Property(e => e.EntityValue)
                .IsUnicode(false);

            modelBuilder.Entity<EntityScheduler>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<EntityScheduler>()
                .Property(e => e.EntityID)
                .IsUnicode(false);

            modelBuilder.Entity<SchedulerEntityType>()
                .HasMany(e => e.EntitySchedulers)
                .WithOptional(e => e.SchedulerEntityType)
                .HasForeignKey(e => e.SchedulerEntityTypeID);

            modelBuilder.Entity<AcadamicCalendar>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<AcadamicCalendar>()
                .HasMany(e => e.AcademicYearCalendarEvents)
                .WithRequired(e => e.AcadamicCalendar)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AcademicNote>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<AcademicYearCalendarEvent>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<AcademicYearCalendarStatu>()
                .HasMany(e => e.AcadamicCalendars)
                .WithOptional(e => e.AcademicYearCalendarStatu)
                .HasForeignKey(e => e.AcademicCalendarStatusID);

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.ClassFeeStructureMaps)
                .WithOptional(e => e.AcademicYear)
                .HasForeignKey(e => e.AcadamicYearID);

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.ClassFeeMasters)
                .WithOptional(e => e.AcademicYear)
                .HasForeignKey(e => e.AcadamicYearID);

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.FeeCollections)
                .WithOptional(e => e.AcademicYear)
                .HasForeignKey(e => e.AcadamicYearID);

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.FeeStructures)
                .WithOptional(e => e.AcademicYear)
                .HasForeignKey(e => e.AcadamicYearID);

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.FinalSettlements)
                .WithOptional(e => e.AcademicYear)
                .HasForeignKey(e => e.AcadamicYearID);

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.PackageConfigs)
                .WithOptional(e => e.AcademicYear)
                .HasForeignKey(e => e.AcadamicYearID);

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.StudentApplications)
                .WithOptional(e => e.AcademicYear)
                .HasForeignKey(e => e.SchoolAcademicyearID);

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.StudentFeeDues)
                .WithOptional(e => e.AcademicYear)
                .HasForeignKey(e => e.AcadamicYearID);

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.StudentGroupFeeMasters)
                .WithOptional(e => e.AcademicYear)
                .HasForeignKey(e => e.AcadamicYearID);

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.StudentPromotionLogs)
                .WithRequired(e => e.AcademicYear)
                .HasForeignKey(e => e.ShiftFromAcademicYearID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.StudentPromotionLogs1)
                .WithRequired(e => e.AcademicYear1)
                .HasForeignKey(e => e.AcademicYearID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AdmissionEnquiry>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<AssignmentAttachmentMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Assignment>()
                .Property(e => e.TimeStamaps)
                .IsFixedLength();

            modelBuilder.Entity<Assignment>()
                .HasMany(e => e.AssignmentAttachmentMaps)
                .WithOptional(e => e.Assignment)
                .HasForeignKey(e => e.AssignmentID);

            modelBuilder.Entity<Assignment>()
                .HasMany(e => e.StudentAssignmentMaps)
                .WithOptional(e => e.Assignment)
                .HasForeignKey(e => e.AssignmentID);

            modelBuilder.Entity<BuildingClassRoomMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Class>()
                .HasMany(e => e.ClassSubjectSkillGroupMaps)
                .WithRequired(e => e.Class)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Class>()
                .HasMany(e => e.StudentApplications)
                .WithOptional(e => e.Class)
                .HasForeignKey(e => e.ClassID);

            modelBuilder.Entity<Class>()
                .HasMany(e => e.StudentApplications1)
                .WithOptional(e => e.Class1)
                .HasForeignKey(e => e.PreviousSchoolClassCompletedID);

            modelBuilder.Entity<Class>()
                .HasMany(e => e.StudentPromotionLogs)
                .WithRequired(e => e.Class)
                .HasForeignKey(e => e.ClassID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Class>()
                .HasMany(e => e.StudentPromotionLogs1)
                .WithRequired(e => e.Class1)
                .HasForeignKey(e => e.ShiftFromClassID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Class>()
                .HasMany(e => e.Students)
                .WithOptional(e => e.Class)
                .HasForeignKey(e => e.ClassID);

            modelBuilder.Entity<Class>()
                .HasMany(e => e.Students1)
                .WithOptional(e => e.Class1)
                .HasForeignKey(e => e.PreviousSchoolClassCompletedID);

            modelBuilder.Entity<ClassFeeMaster>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ClassFeeMaster>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ClassFeeMaster>()
                .HasMany(e => e.FeeCollections)
                .WithOptional(e => e.ClassFeeMaster)
                .HasForeignKey(e => e.ClassFeeMasterId);

            modelBuilder.Entity<ClassFeeMaster>()
                .HasMany(e => e.FeeDueFeeTypeMaps)
                .WithOptional(e => e.ClassFeeMaster)
                .HasForeignKey(e => e.ClassFeeMasterID);

            modelBuilder.Entity<ClassFeeMaster>()
                .HasMany(e => e.FeeMasterClassMaps)
                .WithOptional(e => e.ClassFeeMaster)
                .HasForeignKey(e => e.ClassFeeMasterID);

            modelBuilder.Entity<ClassFeeStructureMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ClassSectionMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ClassSubjectMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ClassSubjectSkillGroupMap>()
                .Property(e => e.TotalMarks)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ClassSubjectSkillGroupMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ClassSubjectSkillGroupMap>()
                .Property(e => e.MinimumMarks)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ClassSubjectSkillGroupMap>()
                .Property(e => e.MaximumMarks)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ClassSubjectSkillGroupMap>()
                .HasMany(e => e.ClassSubjectSkillGroupSkillMaps)
                .WithRequired(e => e.ClassSubjectSkillGroupMap)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClassSubjectSkillGroupSkillMap>()
                .Property(e => e.MinimumMarks)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ClassSubjectSkillGroupSkillMap>()
                .Property(e => e.MaximumMarks)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ClassSubjectSkillGroupSkillMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ClassTeacherMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Complain>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<CreditNoteFeeTypeMap>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<CreditNoteFeeTypeMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ExamClassMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ExamGroup>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Exam>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Exam>()
                .HasMany(e => e.ClassSubjectSkillGroupMaps)
                .WithRequired(e => e.Exam)
                .HasForeignKey(e => e.ExamID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Exam>()
                .HasMany(e => e.ExamClassMaps)
                .WithOptional(e => e.Exam)
                .HasForeignKey(e => e.ExamID);

            modelBuilder.Entity<Exam>()
                .HasMany(e => e.ExamSchedules)
                .WithOptional(e => e.Exam)
                .HasForeignKey(e => e.ExamID);

            modelBuilder.Entity<Exam>()
                .HasMany(e => e.ExamSubjectMaps)
                .WithOptional(e => e.Exam)
                .HasForeignKey(e => e.ExamID);

            modelBuilder.Entity<Exam>()
                .HasMany(e => e.MarkRegisters)
                .WithOptional(e => e.Exam)
                .HasForeignKey(e => e.ExamID);

            modelBuilder.Entity<Exam>()
                .HasMany(e => e.StudentSkillRegisters)
                .WithOptional(e => e.Exam)
                .HasForeignKey(e => e.ExamID);

            modelBuilder.Entity<ExamSchedule>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ExamSchedule>()
                .HasMany(e => e.ExamClassMaps)
                .WithOptional(e => e.ExamSchedule)
                .HasForeignKey(e => e.ExamScheduleID);

            modelBuilder.Entity<ExamSubjectMap>()
                .Property(e => e.MinimumMarks)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ExamSubjectMap>()
                .Property(e => e.MaximumMarks)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ExamSubjectMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<FeeCollectionFeeTypeMap>()
                .Property(e => e.Amount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<FeeCollectionFeeTypeMap>()
                .Property(e => e.TaxPercentage)
                .HasPrecision(3, 0);

            modelBuilder.Entity<FeeCollectionFeeTypeMap>()
                .Property(e => e.TaxAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<FeeCollectionFeeTypeMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<FeeCollectionFeeTypeMap>()
                .Property(e => e.RefundAmount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<FeeCollectionFeeTypeMap>()
                .Property(e => e.CreditNoteAmount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<FeeCollectionFeeTypeMap>()
                .Property(e => e.Balance)
                .HasPrecision(18, 4);

            modelBuilder.Entity<FeeCollectionFeeTypeMap>()
                .HasMany(e => e.FeeCollectionMonthlySplits)
                .WithRequired(e => e.FeeCollectionFeeTypeMap)
                .HasForeignKey(e => e.FeeCollectionFeeTypeMapId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FeeCollectionFeeTypeMap>()
                .HasMany(e => e.FinalSettlementFeeTypeMaps)
                .WithOptional(e => e.FeeCollectionFeeTypeMap)
                .HasForeignKey(e => e.FeeCollectionFeeTypeMapsID);

            modelBuilder.Entity<FeeCollectionMonthlySplit>()
                .Property(e => e.Amount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<FeeCollectionMonthlySplit>()
                .Property(e => e.TaxPercentage)
                .HasPrecision(3, 0);

            modelBuilder.Entity<FeeCollectionMonthlySplit>()
                .Property(e => e.TaxAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<FeeCollectionMonthlySplit>()
                .Property(e => e.CreditNoteAmount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<FeeCollectionMonthlySplit>()
                .Property(e => e.Balance)
                .HasPrecision(18, 4);

            modelBuilder.Entity<FeeCollectionMonthlySplit>()
                .Property(e => e.RefundAmount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<FeeCollectionPaymentModeMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<FeeCollectionPaymentModeMap>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<FeeCollectionPaymentModeMap>()
                .Property(e => e.RefundAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<FeeCollectionPaymentModeMap>()
                .Property(e => e.Balance)
                .HasPrecision(18, 3);

            modelBuilder.Entity<FeeCollection>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<FeeCollection>()
                .Property(e => e.DiscountAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<FeeCollection>()
                .Property(e => e.FineAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<FeeCollection>()
                .Property(e => e.PaidAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<FeeCollection>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<FeeCollection>()
                .HasMany(e => e.FeeCollectionFeeTypeMaps)
                .WithOptional(e => e.FeeCollection)
                .HasForeignKey(e => e.FeeCollectionID);

            modelBuilder.Entity<FeeCollection>()
                .HasMany(e => e.FeeCollectionPaymentModeMaps)
                .WithOptional(e => e.FeeCollection)
                .HasForeignKey(e => e.FeeCollectionID);

            modelBuilder.Entity<FeeConcessionType>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<FeeCycle>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<FeeDiscount>()
                .Property(e => e.DiscountPercentage)
                .HasPrecision(3, 2);

            modelBuilder.Entity<FeeDiscount>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<FeeDueFeeTypeMap>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<FeeDueFeeTypeMap>()
                .Property(e => e.TaxPercentage)
                .HasPrecision(3, 0);

            modelBuilder.Entity<FeeDueFeeTypeMap>()
                .Property(e => e.TaxAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<FeeDueFeeTypeMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<FeeDueFeeTypeMap>()
                .Property(e => e.CollectedAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<FeeDueFeeTypeMap>()
                .HasMany(e => e.FeeCollectionFeeTypeMaps)
                .WithOptional(e => e.FeeDueFeeTypeMap)
                .HasForeignKey(e => e.FeeDueFeeTypeMapsID);

            modelBuilder.Entity<FeeDueFeeTypeMap>()
                .HasMany(e => e.FeeDueMonthlySplits)
                .WithRequired(e => e.FeeDueFeeTypeMap)
                .HasForeignKey(e => e.FeeDueFeeTypeMapsID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FeeDueFeeTypeMap>()
                .HasMany(e => e.FinalSettlementFeeTypeMaps)
                .WithOptional(e => e.FeeDueFeeTypeMap)
                .HasForeignKey(e => e.FeeDueFeeTypeMapsID);

            modelBuilder.Entity<FeeDueMonthlySplit>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<FeeDueMonthlySplit>()
                .Property(e => e.TaxPercentage)
                .HasPrecision(3, 0);

            modelBuilder.Entity<FeeDueMonthlySplit>()
                .Property(e => e.TaxAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<FeeDueMonthlySplit>()
                .HasMany(e => e.FeeCollectionMonthlySplits)
                .WithOptional(e => e.FeeDueMonthlySplit)
                .HasForeignKey(e => e.FeeDueMonthlySplitID);

            modelBuilder.Entity<FeeFineType>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<FeeMasterClassMap>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<FeeMasterClassMap>()
                .Property(e => e.TaxPercentage)
                .HasPrecision(3, 0);

            modelBuilder.Entity<FeeMasterClassMap>()
                .Property(e => e.TaxAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<FeeMasterClassMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<FeeMasterClassMap>()
                .HasMany(e => e.FeeDueFeeTypeMaps)
                .WithOptional(e => e.FeeMasterClassMap)
                .HasForeignKey(e => e.FeeMasterClassMapID);

            modelBuilder.Entity<FeeMasterClassMap>()
                .HasMany(e => e.FeeMasterClassMontlySplitMaps)
                .WithOptional(e => e.FeeMasterClassMap)
                .HasForeignKey(e => e.FeeMasterClassMapID);

            modelBuilder.Entity<FeeMasterClassMontlySplitMap>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<FeeMasterClassMontlySplitMap>()
                .Property(e => e.TaxPercentage)
                .HasPrecision(3, 0);

            modelBuilder.Entity<FeeMasterClassMontlySplitMap>()
                .Property(e => e.TaxAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<FeeMaster>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<FeeMaster>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<FeeMaster>()
                .Property(e => e.TaxPercentage)
                .HasPrecision(4, 2);

            modelBuilder.Entity<FeePaymentMode>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<FeePeriod>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<FeeStructureFeeMap>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<FeeStructureFeeMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<FeeStructureFeeMap>()
                .HasMany(e => e.FeeDueFeeTypeMaps)
                .WithOptional(e => e.FeeStructureFeeMap)
                .HasForeignKey(e => e.FeeStructureFeeMapID);

            modelBuilder.Entity<FeeStructureFeeMap>()
                .HasMany(e => e.FeeStructureMontlySplitMaps)
                .WithOptional(e => e.FeeStructureFeeMap)
                .HasForeignKey(e => e.FeeStructureFeeMapID);

            modelBuilder.Entity<FeeStructureMontlySplitMap>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<FeeStructureMontlySplitMap>()
                .Property(e => e.TaxPercentage)
                .HasPrecision(3, 0);

            modelBuilder.Entity<FeeStructureMontlySplitMap>()
                .Property(e => e.TaxAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<FeeStructureMontlySplitMap>()
                .HasMany(e => e.FeeDueMonthlySplits)
                .WithOptional(e => e.FeeStructureMontlySplitMap)
                .HasForeignKey(e => e.FeeStructureMontlySplitMapID);

            modelBuilder.Entity<FeeStructure>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<FeeStructure>()
                .HasMany(e => e.ClassFeeStructureMaps)
                .WithOptional(e => e.FeeStructure)
                .HasForeignKey(e => e.FeeStructureID);

            modelBuilder.Entity<FeeStructure>()
                .HasMany(e => e.FeeStructureFeeMaps)
                .WithRequired(e => e.FeeStructure)
                .HasForeignKey(e => e.FeeStructureID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FeeStructure>()
                .HasMany(e => e.PackageConfigFeeStructureMaps)
                .WithOptional(e => e.FeeStructure)
                .HasForeignKey(e => e.FeeStructureID);

            modelBuilder.Entity<FeeType>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<FeeType>()
                .HasMany(e => e.FeeFineTypes)
                .WithRequired(e => e.FeeType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FeeType>()
                .HasOptional(e => e.FeeTypes1)
                .WithRequired(e => e.FeeType1);

            modelBuilder.Entity<FinalSettlement>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<FinalSettlement>()
                .Property(e => e.PaidAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<FinalSettlement>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<FinalSettlement>()
                .HasMany(e => e.FinalSettlementFeeTypeMaps)
                .WithOptional(e => e.FinalSettlement)
                .HasForeignKey(e => e.FinalSettlementID);

            modelBuilder.Entity<FinalSettlement>()
                .HasMany(e => e.FinalSettlementPaymentModeMaps)
                .WithOptional(e => e.FinalSettlement)
                .HasForeignKey(e => e.FinalSettlementID);

            modelBuilder.Entity<FinalSettlementFeeTypeMap>()
                .Property(e => e.Amount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<FinalSettlementFeeTypeMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<FinalSettlementFeeTypeMap>()
                .Property(e => e.RefundAmount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<FinalSettlementFeeTypeMap>()
                .Property(e => e.Balance)
                .HasPrecision(18, 4);

            modelBuilder.Entity<FinalSettlementPaymentModeMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<FinalSettlementPaymentModeMap>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<FinalSettlementPaymentModeMap>()
                .Property(e => e.RefundAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<FinalSettlementPaymentModeMap>()
                .Property(e => e.Balance)
                .HasPrecision(18, 3);

            modelBuilder.Entity<FineMaster>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<FineMaster>()
                .Property(e => e.Amount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<FineMasterStudentMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<FineMasterStudentMap>()
                .HasMany(e => e.FeeCollectionFeeTypeMaps)
                .WithOptional(e => e.FineMasterStudentMap)
                .HasForeignKey(e => e.FineMasterStudentMapID);

            modelBuilder.Entity<FineMasterStudentMap>()
                .HasMany(e => e.FeeDueFeeTypeMaps)
                .WithOptional(e => e.FineMasterStudentMap)
                .HasForeignKey(e => e.FineMasterStudentMapID);

            modelBuilder.Entity<GuardianType>()
                .HasMany(e => e.Parents)
                .WithOptional(e => e.GuardianType)
                .HasForeignKey(e => e.MotherStudentRelationShipID);

            modelBuilder.Entity<GuardianType>()
                .HasMany(e => e.StudentApplications)
                .WithOptional(e => e.GuardianType)
                .HasForeignKey(e => e.FatherStudentRelationShipID);

            modelBuilder.Entity<GuardianType>()
                .HasMany(e => e.StudentApplications1)
                .WithOptional(e => e.GuardianType1)
                .HasForeignKey(e => e.MotherStudentRelationShipID);

            modelBuilder.Entity<GuardianType>()
                .HasMany(e => e.StudentApplications2)
                .WithOptional(e => e.GuardianType2)
                .HasForeignKey(e => e.PrimaryContactID);

            modelBuilder.Entity<GuardianType>()
                .HasMany(e => e.Students)
                .WithOptional(e => e.GuardianType)
                .HasForeignKey(e => e.PrimaryContactID);

            modelBuilder.Entity<HostelRoom>()
                .Property(e => e.CostPerBed)
                .HasPrecision(18, 3);

            modelBuilder.Entity<HostelRoom>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<HostelRoom>()
                .HasMany(e => e.Students)
                .WithOptional(e => e.HostelRoom)
                .HasForeignKey(e => e.HostelRoomID);

            modelBuilder.Entity<Hostel>()
                .Property(e => e.HostelName)
                .IsUnicode(false);

            modelBuilder.Entity<LessonPlan>()
                .Property(e => e.TotalHours)
                .HasPrecision(18, 3);

            modelBuilder.Entity<LessonPlan>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<LessonPlan>()
                .HasMany(e => e.LessonPlanTopicMaps)
                .WithOptional(e => e.LessonPlan)
                .HasForeignKey(e => e.LessonPlanID);

            modelBuilder.Entity<LessonPlanTopicMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<LibraryBookCategory>()
                .HasMany(e => e.LibraryBookCategoryMaps)
                .WithOptional(e => e.LibraryBookCategory)
                .HasForeignKey(e => e.BookCategoryID);

            modelBuilder.Entity<LibraryBookCondition>()
                .HasMany(e => e.LibraryTransactions)
                .WithOptional(e => e.LibraryBookCondition)
                .HasForeignKey(e => e.BookCondionID);

            modelBuilder.Entity<LibraryBook>()
                .Property(e => e.BookPrice)
                .HasPrecision(18, 3);

            modelBuilder.Entity<LibraryBook>()
                .Property(e => e.PostPrice)
                .HasPrecision(18, 3);

            modelBuilder.Entity<LibraryBook>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<LibraryBook>()
                .HasMany(e => e.LibraryBookCategoryMaps)
                .WithOptional(e => e.LibraryBook)
                .HasForeignKey(e => e.LibraryBookID);

            modelBuilder.Entity<LibraryBook>()
                .HasMany(e => e.LibraryTransactions)
                .WithOptional(e => e.LibraryBook)
                .HasForeignKey(e => e.BookID);

            modelBuilder.Entity<LibraryBookStatus>()
                .HasMany(e => e.LibraryBooks)
                .WithOptional(e => e.LibraryBookStatus)
                .HasForeignKey(e => e.BookStatusID);

            modelBuilder.Entity<LibraryTransaction>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<MarkGradeMap>()
                .Property(e => e.GradeFrom)
                .HasPrecision(18, 3);

            modelBuilder.Entity<MarkGradeMap>()
                .Property(e => e.GradeTo)
                .HasPrecision(18, 3);

            modelBuilder.Entity<MarkGradeMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<MarkGradeMap>()
                .HasMany(e => e.MarkRegisterSkillGroups)
                .WithOptional(e => e.MarkGradeMap)
                .HasForeignKey(e => e.MarksGradeMapID);

            modelBuilder.Entity<MarkGradeMap>()
                .HasMany(e => e.MarkRegisterSkills)
                .WithOptional(e => e.MarkGradeMap)
                .HasForeignKey(e => e.MarksGradeMapID);

            modelBuilder.Entity<MarkGradeMap>()
                .HasMany(e => e.MarkRegisterSubjectMaps)
                .WithOptional(e => e.MarkGradeMap)
                .HasForeignKey(e => e.MarksGradeMapID);

            modelBuilder.Entity<MarkGradeMap>()
                .HasMany(e => e.StudentSkillGroupMaps)
                .WithOptional(e => e.MarkGradeMap)
                .HasForeignKey(e => e.MarksGradeMapID);

            modelBuilder.Entity<MarkGradeMap>()
                .HasMany(e => e.StudentSkillMasterMaps)
                .WithOptional(e => e.MarkGradeMap)
                .HasForeignKey(e => e.MarksGradeMapID);

            modelBuilder.Entity<MarkGradeMap>()
                .HasMany(e => e.StudentSkillRegisters)
                .WithOptional(e => e.MarkGradeMap)
                .HasForeignKey(e => e.MarksGradeMapID);

            modelBuilder.Entity<MarkGrade>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<MarkGrade>()
                .HasMany(e => e.ClassSubjectSkillGroupMaps)
                .WithRequired(e => e.MarkGrade)
                .HasForeignKey(e => e.MarkGradeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MarkGrade>()
                .HasMany(e => e.ClassSubjectSkillGroupSkillMaps)
                .WithRequired(e => e.MarkGrade)
                .HasForeignKey(e => e.MarkGradeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MarkGrade>()
                .HasMany(e => e.Exams)
                .WithOptional(e => e.MarkGrade)
                .HasForeignKey(e => e.MarkGradeID);

            modelBuilder.Entity<MarkGrade>()
                .HasMany(e => e.ExamSubjectMaps)
                .WithOptional(e => e.MarkGrade)
                .HasForeignKey(e => e.MarkGradeID);

            modelBuilder.Entity<MarkGrade>()
                .HasMany(e => e.MarkGradeMaps)
                .WithOptional(e => e.MarkGrade)
                .HasForeignKey(e => e.MarkGradeID);

            modelBuilder.Entity<MarkRegister>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<MarkRegister>()
                .HasMany(e => e.MarkRegisterSubjectMaps)
                .WithOptional(e => e.MarkRegister)
                .HasForeignKey(e => e.MarkRegisterID);

            modelBuilder.Entity<MarkRegisterSkillGroup>()
                .Property(e => e.MinimumMark)
                .HasPrecision(10, 4);

            modelBuilder.Entity<MarkRegisterSkillGroup>()
                .Property(e => e.MaximumMark)
                .HasPrecision(10, 4);

            modelBuilder.Entity<MarkRegisterSkillGroup>()
                .Property(e => e.MarkObtained)
                .HasPrecision(10, 4);

            modelBuilder.Entity<MarkRegisterSkillGroup>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<MarkRegisterSkillGroup>()
                .HasMany(e => e.MarkRegisterSkills)
                .WithRequired(e => e.MarkRegisterSkillGroup)
                .HasForeignKey(e => e.MarkRegisterSkillGroupID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MarkRegisterSkill>()
                .Property(e => e.MarksObtained)
                .HasPrecision(10, 4);

            modelBuilder.Entity<MarkRegisterSkill>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<MarkRegisterSkill>()
                .Property(e => e.MinimumMark)
                .HasPrecision(10, 4);

            modelBuilder.Entity<MarkRegisterSkill>()
                .Property(e => e.MaximumMark)
                .HasPrecision(10, 4);

            modelBuilder.Entity<MarkRegisterSubjectMap>()
                .Property(e => e.Mark)
                .HasPrecision(10, 3);

            modelBuilder.Entity<MarkRegisterSubjectMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<MarkRegisterSubjectMap>()
                .HasMany(e => e.MarkRegisterSkillGroups)
                .WithRequired(e => e.MarkRegisterSubjectMap)
                .HasForeignKey(e => e.MarkRegisterSubjectMapID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PackageConfig>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<PackageConfig>()
                .HasMany(e => e.PackageConfigClassMaps)
                .WithOptional(e => e.PackageConfig)
                .HasForeignKey(e => e.PackageConfigID);

            modelBuilder.Entity<PackageConfig>()
                .HasMany(e => e.PackageConfigFeeStructureMaps)
                .WithOptional(e => e.PackageConfig)
                .HasForeignKey(e => e.PackageConfigID);

            modelBuilder.Entity<PackageConfig>()
                .HasMany(e => e.PackageConfigStudentGroupMaps)
                .WithOptional(e => e.PackageConfig)
                .HasForeignKey(e => e.PackageConfigID);

            modelBuilder.Entity<PackageConfig>()
                .HasMany(e => e.PackageConfigStudentMaps)
                .WithOptional(e => e.PackageConfig)
                .HasForeignKey(e => e.PackageConfigID);

            modelBuilder.Entity<PackageConfigClassMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<PackageConfigFeeStructureMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<PackageConfigStudentGroupMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<PackageConfigStudentMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Parent>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Parent>()
                .Property(e => e.MotherPhone)
                .IsUnicode(false);

            modelBuilder.Entity<Parent>()
                .HasMany(e => e.ParentStudentApplicationMaps)
                .WithOptional(e => e.Parent)
                .HasForeignKey(e => e.ParentID);

            modelBuilder.Entity<Parent>()
                .HasMany(e => e.Students)
                .WithOptional(e => e.Parent)
                .HasForeignKey(e => e.ParentID);

            modelBuilder.Entity<ParentStudentApplicationMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ParentStudentMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<PassportDetailMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<PassportDetailMap>()
                .HasMany(e => e.StudentApplications)
                .WithOptional(e => e.PassportDetailMap)
                .HasForeignKey(e => e.FatherPassportDetailNoID);

            modelBuilder.Entity<PassportDetailMap>()
                .HasMany(e => e.StudentApplications1)
                .WithOptional(e => e.PassportDetailMap1)
                .HasForeignKey(e => e.MotherPassportDetailNoID);

            modelBuilder.Entity<PassportDetailMap>()
                .HasMany(e => e.StudentApplications2)
                .WithOptional(e => e.PassportDetailMap2)
                .HasForeignKey(e => e.StudentPassportDetailNoID);

            modelBuilder.Entity<PassportVisaDetail>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<PhoneCallLog>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<PostalDespatch>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<PostalReceive>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Routes1>()
                .Property(e => e.RouteFareOneWay)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Routes1>()
                .Property(e => e.RouteFareTwoWay)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Routes1>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Routes1>()
                .HasMany(e => e.RouteStopMaps)
                .WithRequired(e => e.Routes1)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Routes1>()
                .HasMany(e => e.StaffRouteStopMaps)
                .WithOptional(e => e.Routes1)
                .HasForeignKey(e => e.PickupRouteID);

            modelBuilder.Entity<Routes1>()
                .HasMany(e => e.StaffRouteStopMaps1)
                .WithOptional(e => e.Routes11)
                .HasForeignKey(e => e.DropStopRouteID);

            modelBuilder.Entity<Routes1>()
                .HasMany(e => e.StudentRouteStopMaps)
                .WithOptional(e => e.Routes1)
                .HasForeignKey(e => e.DropStopRouteID);

            modelBuilder.Entity<Routes1>()
                .HasMany(e => e.StudentRouteStopMaps1)
                .WithOptional(e => e.Routes11)
                .HasForeignKey(e => e.PickupRouteID);

            modelBuilder.Entity<Routes1>()
                .HasMany(e => e.StudentVehicleAssigns)
                .WithRequired(e => e.Routes1)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RouteStopMap>()
                .Property(e => e.OneWayFee)
                .HasPrecision(18, 3);

            modelBuilder.Entity<RouteStopMap>()
                .Property(e => e.TwoWayFee)
                .HasPrecision(18, 3);

            modelBuilder.Entity<RouteStopMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<RouteStopMap>()
                .HasMany(e => e.StaffRouteStopMaps)
                .WithOptional(e => e.RouteStopMap)
                .HasForeignKey(e => e.RouteStopMapID);

            modelBuilder.Entity<RouteStopMap>()
                .HasMany(e => e.StaffRouteStopMaps1)
                .WithOptional(e => e.RouteStopMap1)
                .HasForeignKey(e => e.PickupStopMapID);

            modelBuilder.Entity<RouteStopMap>()
                .HasMany(e => e.StaffRouteStopMaps2)
                .WithOptional(e => e.RouteStopMap2)
                .HasForeignKey(e => e.DropStopMapID);

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

            modelBuilder.Entity<RouteVehicleMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<SchoolCalenderHolidayMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<SchoolCalender>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<SchoolCreditNote>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<SchoolCreditNote>()
                .Property(e => e.Amount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<SchoolCreditNote>()
                .HasMany(e => e.CreditNoteFeeTypeMaps)
                .WithOptional(e => e.SchoolCreditNote)
                .HasForeignKey(e => e.SchoolCreditNoteID);

            modelBuilder.Entity<SchoolPollAnswerLog>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<School>()
                .HasMany(e => e.Classes)
                .WithOptional(e => e.School)
                .HasForeignKey(e => e.SchoolID);

            modelBuilder.Entity<School>()
                .HasMany(e => e.Classes1)
                .WithOptional(e => e.School1)
                .HasForeignKey(e => e.SchoolID);

            modelBuilder.Entity<Section>()
                .HasMany(e => e.StudentPromotionLogs)
                .WithRequired(e => e.Section)
                .HasForeignKey(e => e.ShiftFromSectionID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Section>()
                .HasMany(e => e.StudentPromotionLogs1)
                .WithRequired(e => e.Section1)
                .HasForeignKey(e => e.SectionID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shift>()
                .HasMany(e => e.TeacherActivities)
                .WithOptional(e => e.Shift)
                .HasForeignKey(e => e.ShiftID);

            modelBuilder.Entity<SkillGroupMaster>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<SkillGroupMaster>()
                .HasMany(e => e.ClassSubjectSkillGroupMaps)
                .WithRequired(e => e.SkillGroupMaster)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SkillMaster>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<SkillMaster>()
                .HasMany(e => e.ClassSubjectSkillGroupSkillMaps)
                .WithRequired(e => e.SkillMaster)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SkillMaster>()
                .HasMany(e => e.MarkRegisterSkills)
                .WithRequired(e => e.SkillMaster)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SkillMaster>()
                .HasMany(e => e.StudentSkillMasterMaps)
                .WithRequired(e => e.SkillMaster)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StaffAttendence>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<StaffRouteStopMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<StaffRouteStopMap>()
                .HasMany(e => e.StaffRouteMonthlySplits)
                .WithOptional(e => e.StaffRouteStopMap)
                .HasForeignKey(e => e.StaffRouteStopMapID);

            modelBuilder.Entity<StudentApplication>()
                .Property(e => e.MobileNumber)
                .IsUnicode(false);

            modelBuilder.Entity<StudentApplication>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<StudentApplication>()
                .HasMany(e => e.StudentApplicationSiblingMaps)
                .WithOptional(e => e.StudentApplication)
                .HasForeignKey(e => e.ApplicationID);

            modelBuilder.Entity<StudentApplication>()
                .HasMany(e => e.Students)
                .WithOptional(e => e.StudentApplication)
                .HasForeignKey(e => e.ApplicationID);

            modelBuilder.Entity<StudentApplicationSiblingMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<StudentAssignmentMap>()
                .Property(e => e.TimeStamaps)
                .IsFixedLength();

            modelBuilder.Entity<StudentAttendence>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<StudentClassHistoryMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<StudentFeeDue>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<StudentFeeDue>()
                .HasMany(e => e.FeeDueFeeTypeMaps)
                .WithOptional(e => e.StudentFeeDue)
                .HasForeignKey(e => e.StudentFeeDueID);

            modelBuilder.Entity<StudentGroupFeeMaster>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<StudentGroupFeeMaster>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<StudentGroupFeeMaster>()
                .HasMany(e => e.StudentGroupFeeTypeMaps)
                .WithRequired(e => e.StudentGroupFeeMaster)
                .HasForeignKey(e => e.StudentGroupFeeMasterID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StudentGroupFeeTypeMap>()
                .Property(e => e.PercentageAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<StudentGroupFeeTypeMap>()
                .Property(e => e.TaxPercentage)
                .HasPrecision(3, 0);

            modelBuilder.Entity<StudentGroupFeeTypeMap>()
                .Property(e => e.TaxAmount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<StudentGroupFeeTypeMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<StudentGroupMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<StudentGroupType>()
                .HasMany(e => e.StudentGroups)
                .WithOptional(e => e.StudentGroupType)
                .HasForeignKey(e => e.GroupTypeID);

            modelBuilder.Entity<StudentMiscDetail>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<StudentPassportDetail>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<StudentRouteStopMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<StudentRouteStopMap>()
                .HasMany(e => e.StudentRouteMonthlySplits)
                .WithOptional(e => e.StudentRouteStopMap)
                .HasForeignKey(e => e.StudentRouteStopMapID);

            modelBuilder.Entity<Student>()
                .Property(e => e.MobileNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Height)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Weight)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Candidates)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.StudentID);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.TransactionHeads)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.StudentID);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.FeeCollections)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.StudentID);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.FinalSettlements)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.StudentID);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.FineMasterStudentMaps)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.LibraryStudentRegisters)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.StudentID);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.LibraryTransactions)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.StudentID);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.MarkRegisters)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.PackageConfigStudentMaps)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.StudentID);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.ParentStudentMaps)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.StudentID);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.SchoolCreditNotes)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.StudentID);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.SchoolPollAnswerLogs)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.StudentID);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.StudentApplicationSiblingMaps)
                .WithRequired(e => e.Student)
                .HasForeignKey(e => e.SiblingID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.StudentAssignmentMaps)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.StudentAttendences)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.StudentID);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.StudentClassHistoryMaps)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.StudentID);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.StudentFeeDues)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.StudentGroupMaps)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.StudentID);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.StudentLeaveApplications)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.StudentID);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.StudentMiscDetails)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.StudentID);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.StudentPassportDetails)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.StudentID);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.StudentPromotionLogs)
                .WithRequired(e => e.Student)
                .HasForeignKey(e => e.StudentID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.StudentRouteStopMaps)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.StudentID);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.StudentSiblingMaps)
                .WithRequired(e => e.Student)
                .HasForeignKey(e => e.StudentID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.StudentSiblingMaps1)
                .WithOptional(e => e.Student1)
                .HasForeignKey(e => e.SiblingID);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.StudentSkillRegisters)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<StudentSiblingMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<StudentSkillGroupMap>()
                .Property(e => e.MinimumMark)
                .HasPrecision(10, 4);

            modelBuilder.Entity<StudentSkillGroupMap>()
                .Property(e => e.MaximumMark)
                .HasPrecision(10, 4);

            modelBuilder.Entity<StudentSkillGroupMap>()
                .Property(e => e.MarkObtained)
                .HasPrecision(10, 4);

            modelBuilder.Entity<StudentSkillGroupMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<StudentSkillGroupMap>()
                .HasMany(e => e.StudentSkillMasterMaps)
                .WithRequired(e => e.StudentSkillGroupMap)
                .HasForeignKey(e => e.StudentSkillGroupMapsID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StudentSkillMasterMap>()
                .Property(e => e.MarksObtained)
                .HasPrecision(10, 4);

            modelBuilder.Entity<StudentSkillMasterMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<StudentSkillMasterMap>()
                .Property(e => e.MinimumMark)
                .HasPrecision(10, 4);

            modelBuilder.Entity<StudentSkillMasterMap>()
                .Property(e => e.MaximumMark)
                .HasPrecision(10, 4);

            modelBuilder.Entity<StudentSkillRegister>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<StudentSkillRegister>()
                .HasMany(e => e.StudentSkillGroupMaps)
                .WithRequired(e => e.StudentSkillRegister)
                .HasForeignKey(e => e.StudentSkillRegisterID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StudentTransferRequestReason>()
                .HasMany(e => e.StudentTransferRequests)
                .WithOptional(e => e.StudentTransferRequestReason)
                .HasForeignKey(e => e.TransferRequestReasonID);

            modelBuilder.Entity<StudentTransferRequest>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<StudentVehicleAssign>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.ClassSubjectSkillGroupMaps)
                .WithRequired(e => e.Subject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SubjectTeacherMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<SubjectTopic>()
                .Property(e => e.Duration)
                .HasPrecision(12, 3);

            modelBuilder.Entity<SubjectTopic>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<SubjectTopic>()
                .HasMany(e => e.SubjectTopics1)
                .WithOptional(e => e.SubjectTopic1)
                .HasForeignKey(e => e.ParentTopicID);

            modelBuilder.Entity<SubjectTopic>()
                .HasMany(e => e.TeacherActivities)
                .WithOptional(e => e.SubjectTopic)
                .HasForeignKey(e => e.TopicID);

            modelBuilder.Entity<SubjectTopic>()
                .HasMany(e => e.TeacherActivities1)
                .WithOptional(e => e.SubjectTopic1)
                .HasForeignKey(e => e.SubTopicID);

            modelBuilder.Entity<Syllabu>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Syllabu>()
                .HasMany(e => e.StudentApplications)
                .WithOptional(e => e.Syllabu)
                .HasForeignKey(e => e.PreviousSchoolSyllabusID);

            modelBuilder.Entity<Syllabu>()
                .HasMany(e => e.StudentApplications1)
                .WithOptional(e => e.Syllabu1)
                .HasForeignKey(e => e.CurriculamID);

            modelBuilder.Entity<Syllabu>()
                .HasMany(e => e.Students)
                .WithOptional(e => e.Syllabu)
                .HasForeignKey(e => e.PreviousSchoolSyllabusID);

            modelBuilder.Entity<TeacherActivity>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<TimeTableAllocation>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<VisaDetailMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<VisaDetailMap>()
                .HasMany(e => e.StudentApplications)
                .WithOptional(e => e.VisaDetailMap)
                .HasForeignKey(e => e.StudentVisaDetailNoID);

            modelBuilder.Entity<VisaDetailMap>()
                .HasMany(e => e.StudentApplications1)
                .WithOptional(e => e.VisaDetailMap1)
                .HasForeignKey(e => e.FatherVisaDetailNoID);

            modelBuilder.Entity<VisaDetailMap>()
                .HasMany(e => e.StudentApplications2)
                .WithOptional(e => e.VisaDetailMap2)
                .HasForeignKey(e => e.MotherVisaDetailNoID);

            modelBuilder.Entity<VisitorBook>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<VolunteerType>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<VolunteerType>()
                .HasMany(e => e.Parents)
                .WithOptional(e => e.VolunteerType)
                .HasForeignKey(e => e.CanYouVolunteerToHelpOneID);

            modelBuilder.Entity<VolunteerType>()
                .HasMany(e => e.Parents1)
                .WithOptional(e => e.VolunteerType1)
                .HasForeignKey(e => e.CanYouVolunteerToHelpTwoID);

            modelBuilder.Entity<VolunteerType>()
                .HasMany(e => e.StudentApplications)
                .WithOptional(e => e.VolunteerType)
                .HasForeignKey(e => e.CanYouVolunteerToHelpOneID);

            modelBuilder.Entity<VolunteerType>()
                .HasMany(e => e.StudentApplications1)
                .WithOptional(e => e.VolunteerType1)
                .HasForeignKey(e => e.CanYouVolunteerToHelpTwoID);

            modelBuilder.Entity<ChartMetadata>()
                .Property(e => e.ChartType)
                .IsUnicode(false);

            modelBuilder.Entity<ChartMetadata>()
                .Property(e => e.ChartPhysicalEntiy)
                .IsUnicode(false);

            modelBuilder.Entity<Condition>()
                .HasMany(e => e.FilterColumnConditionMaps)
                .WithOptional(e => e.Condition)
                .HasForeignKey(e => e.ConidtionID);

            modelBuilder.Entity<CultureData>()
                .Property(e => e.TableName)
                .IsUnicode(false);

            modelBuilder.Entity<CultureData>()
                .Property(e => e.ColumnName)
                .IsUnicode(false);

            modelBuilder.Entity<CultureData>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<FilterColumnUserValue>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<GlobalSetting>()
                .Property(e => e.GlobalSettingKey)
                .IsUnicode(false);

            modelBuilder.Entity<Lookup>()
                .Property(e => e.LookupType)
                .IsFixedLength();

            modelBuilder.Entity<Lookup>()
                .Property(e => e.Query)
                .IsUnicode(false);

            modelBuilder.Entity<MenuLinkCultureData>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<MenuLink>()
                .Property(e => e.ActionType)
                .IsUnicode(false);

            modelBuilder.Entity<MenuLink>()
                .Property(e => e.ActionLink)
                .IsUnicode(false);

            modelBuilder.Entity<MenuLink>()
                .Property(e => e.ActionLink1)
                .IsUnicode(false);

            modelBuilder.Entity<MenuLink>()
                .Property(e => e.ActionLink2)
                .IsUnicode(false);

            modelBuilder.Entity<MenuLink>()
                .Property(e => e.ActionLink3)
                .IsUnicode(false);

            modelBuilder.Entity<MenuLink>()
                .Property(e => e.Parameters)
                .IsUnicode(false);

            modelBuilder.Entity<MenuLink>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<MenuLink>()
                .Property(e => e.MenuGroup)
                .IsUnicode(false);

            modelBuilder.Entity<MenuLink>()
                .HasMany(e => e.MenuLinkBrandMaps)
                .WithOptional(e => e.MenuLink)
                .HasForeignKey(e => e.MenuLinkID);

            modelBuilder.Entity<MenuLink>()
                .HasMany(e => e.MenuLinkCategoryMaps)
                .WithOptional(e => e.MenuLink)
                .HasForeignKey(e => e.MenuLinkID);

            modelBuilder.Entity<MenuLink>()
                .HasMany(e => e.MenuLinkCultureDatas)
                .WithRequired(e => e.MenuLink)
                .HasForeignKey(e => e.MenuLinkID)
                .WillCascadeOnDelete(false);

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

            modelBuilder.Entity<ScreenLookupMap>()
                .Property(e => e.LookUpName)
                .IsUnicode(false);

            modelBuilder.Entity<ScreenLookupMap>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<ScreenLookupMap>()
                .Property(e => e.CallBack)
                .IsUnicode(false);

            modelBuilder.Entity<ScreenMetadata>()
                .Property(e => e.ModelAssembly)
                .IsUnicode(false);

            modelBuilder.Entity<ScreenMetadata>()
                .Property(e => e.MasterViewModel)
                .IsUnicode(false);

            modelBuilder.Entity<ScreenMetadata>()
                .Property(e => e.JsControllerName)
                .IsUnicode(false);

            modelBuilder.Entity<ScreenMetadata>()
                .Property(e => e.EntityMapperAssembly)
                .IsUnicode(false);

            modelBuilder.Entity<ScreenMetadata>()
                .Property(e => e.EntityMapperViewModel)
                .IsUnicode(false);

            modelBuilder.Entity<ScreenMetadata>()
                .Property(e => e.SaveCRUDMethod)
                .IsUnicode(false);

            modelBuilder.Entity<ScreenShortCut>()
                .Property(e => e.KeyCode)
                .IsUnicode(false);

            modelBuilder.Entity<ScreenShortCut>()
                .Property(e => e.Action)
                .IsUnicode(false);

            modelBuilder.Entity<Setting>()
                .Property(e => e.SettingCode)
                .IsUnicode(false);

            modelBuilder.Entity<Setting>()
                .Property(e => e.ValueType)
                .IsUnicode(false);

            modelBuilder.Entity<Sites1>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<UserDataFormatMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<UserSetting>()
                .Property(e => e.SettingCode)
                .IsUnicode(false);

            modelBuilder.Entity<UserSetting>()
                .Property(e => e.SettingValue)
                .IsUnicode(false);

            modelBuilder.Entity<UserSetting>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<UserViewColumnMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<UserView>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<UserView>()
                .HasMany(e => e.UserViewColumnMaps)
                .WithOptional(e => e.UserView)
                .HasForeignKey(e => e.UserViewID);

            modelBuilder.Entity<ViewColumn>()
                .Property(e => e.DataType)
                .IsUnicode(false);

            modelBuilder.Entity<ViewColumn>()
                .Property(e => e.PhysicalColumnName)
                .IsUnicode(false);

            modelBuilder.Entity<View>()
                .Property(e => e.ViewFullPath)
                .IsUnicode(false);

            modelBuilder.Entity<View>()
                .Property(e => e.PhysicalSchemaName)
                .IsUnicode(false);

            modelBuilder.Entity<View>()
                .Property(e => e.ChildFilterField)
                .IsUnicode(false);

            modelBuilder.Entity<View>()
                .Property(e => e.ControllerName)
                .IsUnicode(false);

            modelBuilder.Entity<View>()
                .Property(e => e.JsControllerName)
                .IsUnicode(false);

            modelBuilder.Entity<View>()
                .HasMany(e => e.Views1)
                .WithOptional(e => e.View1)
                .HasForeignKey(e => e.ChildViewID);

            modelBuilder.Entity<EntityChangeTracker>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<EntityChangeTracker>()
                .Property(e => e.BatchNo)
                .IsUnicode(false);

            modelBuilder.Entity<EntityChangeTracker>()
                .HasMany(e => e.EntityChangeTrackersQueues)
                .WithOptional(e => e.EntityChangeTracker)
                .HasForeignKey(e => e.EntityChangeTrackeID);

            modelBuilder.Entity<EntityChangeTracker>()
                .HasMany(e => e.EntityChangeTrackersInProcesses)
                .WithOptional(e => e.EntityChangeTracker)
                .HasForeignKey(e => e.EntityChangeTrackerID);

            modelBuilder.Entity<EntityChangeTrackersInProcess>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<OperationType>()
                .Property(e => e.OperationName)
                .IsUnicode(false);

            modelBuilder.Entity<SyncEntity>()
                .Property(e => e.EntityName)
                .IsUnicode(false);

            modelBuilder.Entity<SyncEntity>()
                .Property(e => e.EntityDataSource)
                .IsUnicode(false);

            modelBuilder.Entity<SyncFieldMap>()
                .Property(e => e.SourceField)
                .IsUnicode(false);

            modelBuilder.Entity<SyncFieldMap>()
                .Property(e => e.DestinationField)
                .IsUnicode(false);

            modelBuilder.Entity<SyncFieldMapType>()
                .Property(e => e.MapName)
                .IsUnicode(false);

            modelBuilder.Entity<TaskPrioity>()
                .HasMany(e => e.Tasks)
                .WithOptional(e => e.TaskPrioity)
                .HasForeignKey(e => e.TaskPrioityID);

            modelBuilder.Entity<Task>()
                .Property(e => e.ColorCode)
                .IsUnicode(false);

            modelBuilder.Entity<Task>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<Task>()
                .HasMany(e => e.EmployeeTimeSheets)
                .WithRequired(e => e.Task)
                .HasForeignKey(e => e.TaskID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Task>()
                .HasMany(e => e.TaskAssingners)
                .WithOptional(e => e.Task)
                .HasForeignKey(e => e.TaskID);

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
                .Property(e => e.WorkflowFieldType)
                .IsUnicode(false);

            modelBuilder.Entity<WorkflowFiled>()
                .HasMany(e => e.Workflows)
                .WithOptional(e => e.WorkflowFiled)
                .HasForeignKey(e => e.WorkflowApplyFieldID);

            modelBuilder.Entity<WorkflowLogMapRuleApproverMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<WorkflowLogMapRuleMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

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
                .HasMany(e => e.WorkflowLogMapRuleMaps)
                .WithOptional(e => e.WorkflowRule)
                .HasForeignKey(e => e.WorkflowRuleID);

            modelBuilder.Entity<WorkflowRule>()
                .HasMany(e => e.WorkflowRuleConditions)
                .WithOptional(e => e.WorkflowRule)
                .HasForeignKey(e => e.WorkflowRuleID);

            modelBuilder.Entity<WorkflowRule>()
                .HasMany(e => e.WorkflowTransactionHeadRuleMaps)
                .WithOptional(e => e.WorkflowRule)
                .HasForeignKey(e => e.WorkflowRuleID);

            modelBuilder.Entity<Workflow>()
                .HasMany(e => e.DocumentTypes)
                .WithOptional(e => e.Workflow)
                .HasForeignKey(e => e.WorkflowID);

            modelBuilder.Entity<Workflow>()
                .HasMany(e => e.WorkflowLogMaps)
                .WithOptional(e => e.Workflow)
                .HasForeignKey(e => e.WorkflowID);

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

            modelBuilder.Entity<Project>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<StaticContentData>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<ProductInventories_Bak>()
                .Property(e => e.Quantity)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ProductInventories_Bak>()
                .Property(e => e.CostPrice)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ShoppingCartVoucherMap>()
                .Property(e => e.Amount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<ShoppingCartVoucherMap>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<SMSNotificationData>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<LeaveAllocation>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<PaymentEntry>()
                .Property(e => e.TimeStamps)
                .IsFixedLength();

            modelBuilder.Entity<VIEWS_20210112>()
                .Property(e => e.ViewFullPath)
                .IsUnicode(false);

            modelBuilder.Entity<VIEWS_20210112>()
                .Property(e => e.PhysicalSchemaName)
                .IsUnicode(false);

            modelBuilder.Entity<VIEWS_20210112>()
                .Property(e => e.ChildFilterField)
                .IsUnicode(false);

            modelBuilder.Entity<VIEWS_20210112>()
                .Property(e => e.ControllerName)
                .IsUnicode(false);

            modelBuilder.Entity<VIEWS_20210112>()
                .Property(e => e.JsControllerName)
                .IsUnicode(false);
        }
    }
}

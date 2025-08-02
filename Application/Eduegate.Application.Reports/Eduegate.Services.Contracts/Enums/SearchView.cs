using System.Runtime.Serialization;
namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "SearchView")]
    public enum SearchView
    {
        [EnumMember]
        Product = 1,
        [EnumMember]
        ProductSummary = 2,
        [EnumMember]
        Customer = 3,
        [EnumMember]
        CustomerSummary = 4,
        [EnumMember]
        Supplier = 5,
        [EnumMember]
        SupplierSummary = 6,
        [EnumMember]
        Price = 7,
        [EnumMember]
        PriceSummary = 8,
        [EnumMember]
        Sales = 9,
        [EnumMember]
        SalesSummary = 10,
        [EnumMember]
        Voucher = 11,
        [EnumMember]
        VoucherSummary = 12,
        [EnumMember]
        Banner = 13,
        [EnumMember]
        BannerSummary = 14,
        [EnumMember]
        CustomerGroup = 15,
        [EnumMember]
        CustomerGroupSummary = 16,
        [EnumMember]
        News = 17,
        [EnumMember]
        NewsSummary = 18,
        [EnumMember]
        Transaction = 19,
        [EnumMember]
        TransactionSummary = 20,
        [EnumMember]
        Category = 21,
        [EnumMember]
        CategorySummary = 22,
        [EnumMember]
        Stockin = 23,
        [EnumMember]
        StockinSummary = 24,
        [EnumMember]
        Login = 25,
        [EnumMember]
        LoginSummary = 26,
        [EnumMember]
        Order = 27,
        [EnumMember]
        OrderSummary = 28,
        [EnumMember]
        StaticContent = 29,
        [EnumMember]
        StaticContentSummary = 30,
        [EnumMember]
        Notification = 31,
        [EnumMember]
        NotificationSummary = 32,
        [EnumMember]
        Brand = 33,
        [EnumMember]
        BrandSummary = 34,
        [EnumMember]
        UserManagement = 35,
        [EnumMember]
        UserManagementSummary = 36,
        [EnumMember]
        ProductFamily = 37,
        [EnumMember]
        ProductFamilySummary = 38,
        [EnumMember]
        ProductProperty = 39,
        [EnumMember]
        ProductPropertySummary = 40,
        [EnumMember]
        StockDocumentType = 41,
        [EnumMember]
        StockDocumentTypeSummary = 42,
        [EnumMember]
        AccountDocumentType = 43,
        [EnumMember]
        AccountDocumentTypeSummary = 44,
        [EnumMember]
        Warehouse = 45,
        [EnumMember]
        WarehouseSummary = 46,
        [EnumMember]
        Branch = 47,
        [EnumMember]
        BranchSummary = 48,
        [EnumMember]
        BranchGroup = 49,
        [EnumMember]
        BranchGroupSummary = 50,
        [EnumMember]
        Company = 51,
        [EnumMember]
        CompanySummary = 52,
        [EnumMember]
        Department = 53,
        [EnumMember]
        DepartmentSummary = 54,
        [EnumMember]
        Employee = 55,
        [EnumMember]
        EmployeeSummary = 56,
        [EnumMember]
        ProductSKU = 57,
        [EnumMember]
        ProductSKUSummary = 58,
        [EnumMember]
        PurchaseOrder = 59,
        [EnumMember]
        PurchaseOrderSummary = 60,
        [EnumMember]
        PurchaseInvoice = 61,
        [EnumMember]
        PurchaseInvoiceSummary = 62,
        [EnumMember]
        PurchaseReturn = 63,
        [EnumMember]
        PurchaseReturnSummary = 64,
        [EnumMember]
        SalesOrder = 65,
        [EnumMember]
        SalesOrderSummary = 66,
        [EnumMember]
        SalesInvoice = 67,
        [EnumMember]
        SalesInvoiceSummary = 68,
        [EnumMember]
        SalesReturn = 69,
        [EnumMember]
        SalesReturnSummary = 70,
        [EnumMember]
        BranchTransfer = 71,
        [EnumMember]
        BranchTransferSummary = 72,
        [EnumMember]
        PurchaseReturnRequest = 73,
        [EnumMember]
        PurchaseReturnRequestSummary = 74,
        [EnumMember]
        CustomerContacts = 75,
        [EnumMember]
        SalesReturnRequest = 76,
        [EnumMember]
        SalesReturnRequestSummary = 77,
        [EnumMember]
        BranchTransferRequest = 78,
        [EnumMember]
        BranchTransferRequestSummary = 79,
        [EnumMember]
        Claim = 80,
        [EnumMember]
        ClaimSummary = 81,
        [EnumMember]
        ClaimSet = 82,
        [EnumMember]
        ClaimSetSummary = 83,
        [EnumMember]
        WarehouseDocumentType = 84,
        [EnumMember]
        WarehouseDocumentTypeSummary = 85,
        [EnumMember]
        JobEntry = 86,
        [EnumMember]
        JobEntrySummary = 87,
        [EnumMember]
        JobOperation = 88,
        [EnumMember]
        JobOperationSummary = 89,
        [EnumMember]
        InventoryDetails = 90,
        [EnumMember]
        InventoryDetailsSummary = 91,
        [EnumMember]
        Driver = 92,
        [EnumMember]
        DriverSummary = 93,
        [EnumMember]
        Route = 94,
        [EnumMember]
        RouteSummary = 95,
        [EnumMember]
        Vehicle = 96,
        [EnumMember]
        VehicleSummary = 97,
        [EnumMember]
        ServiceProvider = 98,
        [EnumMember]
        ServiceProviderSummary = 99,
        [EnumMember]
        City = 100,
        [EnumMember]
        CitySummary = 101,
        [EnumMember]
        Area = 102,
        [EnumMember]
        AreaSummary = 103,
        [EnumMember]
        Location = 104,
        [EnumMember]
        LocationSummary = 105,
        [EnumMember]
        ReadyForShipping = 106,
        [EnumMember]
        ReadyForShippingSummary = 107,
        [EnumMember]
        Mission = 108,
        [EnumMember]
        MissionSummary = 109,
        [EnumMember]
        Basket = 110,
        [EnumMember]
        BasketSummary = 111,
        [EnumMember]
        JobEntryDetail = 112,
        [EnumMember]
        JobEntryDetailSummary = 113,
        [EnumMember]
        Zone = 114,
        [EnumMember]
        ZoneSummary = 115,
        [EnumMember]
        Ticket = 116,
        [EnumMember]
        TicketSummary = 117,
        [EnumMember]
        BoilerPlate = 118,
        [EnumMember]
        BoilerPlateSummary = 119,
        [EnumMember]
        StockVerification = 120,
        [EnumMember]
        StockVerificationSummary = 121,
        [EnumMember]
        SupportDocumentType = 122,
        [EnumMember]
        SupportDocumentTypeSummary = 123,
        [EnumMember]
        ChartOfAccount = 124,
        [EnumMember]
        ChartOfAccountSummary = 125,
        [EnumMember]
        AccountEntry = 126,
        [EnumMember]
        AccountEntrySummary = 127,
        [EnumMember]
        AccountGroup = 128,
        [EnumMember]
        AccountGroupSummary = 129,
        [EnumMember]
        Journal = 130,
        [EnumMember]
        JournalSummary = 131,
        [EnumMember]
        Page = 132,
        [EnumMember]
        PageSummary = 133,
        [EnumMember]
        DeliverySetting = 134,
        [EnumMember]
        DeliverySettingSummary = 135,
        [EnumMember]
        PropertyType = 136,
        [EnumMember]
        PropertyTypeSummary = 137,
        [EnumMember]
        OnlinePayment = 138,
        [EnumMember]
        PurchaseSKU = 139,
        [EnumMember]
        PaymentSummary = 140,
        [EnumMember]
        ProductDelivery = 141,
        [EnumMember]
        ProductDeliverySummary = 142,
        [EnumMember]
        AreaDeliverySetting = 143,
        [EnumMember]
        AreaDeliverySettingSummary = 144,
        [EnumMember]
        CustomerGroupDeliverySetting = 145,
        [EnumMember]
        CustomerGroupDeliverySettingSummary = 146,
        [EnumMember]
        ProductPriceSetting = 151,
        [EnumMember]
        ProductSKUDeliverySetting = 153,
        [EnumMember]
        ProductSKUDeliverySettingSummary = 154,
        [EnumMember]
        ZoneDeliverySetting = 155,
        [EnumMember]
        ZoneDeliverySettingSummary = 156,
        [EnumMember]
        ProductSKUPriceSetting = 157,
        [EnumMember]
        DataFeedLog = 158,
        [EnumMember]
        DataFeedLogSummary = 159,
        [EnumMember]
        SupplierProduct = 160,
        [EnumMember]
        SupplierProductSummary = 161,
        [EnumMember]
        SupplierSKU = 162,
        [EnumMember]
        SupplierSKUSummary = 163,
        [EnumMember]
        ProductDeliverySetting = 164,
        [EnumMember]
        ProductDeliverySettingSummary = 165,
        [EnumMember]
        SupplierProductPriceSetting = 166,
        [EnumMember]
        SupplierProductPriceSettingSummary = 167,
        [EnumMember]
        CategoryPriceSetting = 170,
        [EnumMember]
        ProductPriceSettingSummary = 171,
        [EnumMember]
        BrandPriceSetting = 172,
        [EnumMember]
        BrandPriceSettingSummary = 173,
        [EnumMember]
        InventoryBranch = 174,
        [EnumMember]
        DistributionDocumentType = 175,
        [EnumMember]
        DistributionDocumentTypeSummary = 176,
        [EnumMember]
        ShoppingCart = 177,
        [EnumMember]
        ShoppingCartSummary = 178,
        [EnumMember]
        RepairOrder = 179,
        [EnumMember]
        RepairOrderSummary = 180,
        [EnumMember]
        MyMission = 181,
        [EnumMember]
        EmailTemplate = 183,
        [EnumMember]
        SKU = 185,
        [EnumMember]
        RepairOrderCustomer = 186,
        [EnumMember]
        RepairOrderCustomerSummary = 187,
        [EnumMember]
        Emarsys = 190,
        [EnumMember]
        MarketPlaceInventoryDetails = 192,
        [EnumMember]
        ShoppingCartItems = 194,
        [EnumMember]
        Alerts = 196,
        [EnumMember]
        NotifyMeRequest = 198,
        [EnumMember]
        PayfortLogs = 200,
        [EnumMember]
        PayfortLogsSummary = 201,
        [EnumMember]
        SupplierTransaction = 202,
        [EnumMember]
        SupplierTransactionSummary = 203,
        [EnumMember]
        SalesOrderAdvanceSearch = 204,
        [EnumMember]
        Daybook = 205,
        [EnumMember]
        DaybookSummary = 206,
        [EnumMember]
        PaymentKnet = 207,
        [EnumMember]
        PaymentKnetSummary = 208,
        [EnumMember]
        MasterVisa = 209,
        [EnumMember]
        MasterVisaSummary = 210,
        [EnumMember]
        PayPal = 211,
        [EnumMember]
        PayPalSummary = 212,
        [EnumMember]
        JobOpening = 213,
        [EnumMember]
        JobOpeningSummary = 214,
        [EnumMember]
        JobProfile = 215,
        [EnumMember]
        JobProfileSummary = 216,
        [EnumMember]
        AdvancedTicketSearch = 217,
        [EnumMember]
        CODOrders = 218,
        [EnumMember]
        CODOrdersSummary = 219,
        [EnumMember]
        ArchivedJobs = 220,
        [EnumMember]
        Replacement = 222,
        [EnumMember]
        ReplacementSummary = 223,
        [EnumMember]
        InventoryBranchMarketPlace = 225,
        [EnumMember]
        DataFeedDetail = 229,
        [EnumMember]
        JobDepartment = 230,
        [EnumMember]
        JobDepartmentSummary = 231,
        [EnumMember]
        DoctorSearch = 233,
        [EnumMember]
        DoctorSummary = 234,
        [EnumMember]
        DepartmentSearch = 235,
        [EnumMember]
        DepartmentsSummary = 236,
        [EnumMember]
        OrderChangeRequest = 237,
        [EnumMember]
        OrderChangeRequestSummary = 238,
        [EnumMember]
        SuppTransaction = 241,
        [EnumMember]
        SuppTransactionSummary = 242,
        [EnumMember]
        AssetList = 243,
        [EnumMember]
        ClinicSearch = 244,
        [EnumMember]
        ClinicSummary = 245,
        [EnumMember]
        AssetMaster = 300,
        [EnumMember]
        AssetMasterSummary = 301,
        [EnumMember]
        AssetEntry = 302,
        [EnumMember]
        AssetEntrySummary = 303,
        [EnumMember]
        AssetEntryTransactionDetails = 304,
        [EnumMember]
        AssetJV = 305,
        [EnumMember]
        AssetJVSummary = 306,
        [EnumMember]
        AssetRemoval = 307,
        [EnumMember]
        AssetRemovalSummary = 308,
        [EnumMember]
        AssetRemovalBranch = 309,
        [EnumMember]
        AssetJVBranch = 310,
        [EnumMember]
        PurchaseInvoiceAdvanceSearch = 312,
        [EnumMember]
        PurchaseReturnRequestAdvanceSearch = 313,
        [EnumMember]
        SalesReturnRequestAdvanceSearch = 314,
        [EnumMember]
        SalesInvoiceAdvanceSearch = 315,
        [EnumMember]
        PurchaseOrderAdvanceSearch = 316,
        [EnumMember]
        GRNAdvanceSearch = 359,
        [EnumMember]
        ReplacementAdvanced = 317,
        [EnumMember]
        ReceiptVoucher = 318,
        [EnumMember]
        AgainstMission = 319,
        [EnumMember]
        AgainstInvoice = 320,
        [EnumMember]
        PaymentVoucher = 321,
        [EnumMember]
        CreditNote = 322,
        [EnumMember]
        DebitNote = 323,
        [EnumMember]
        DataFeed = 324,
        [EnumMember]
        DataFeedSummary = 325,
        [EnumMember]
        ServiceJob = 326,
        [EnumMember]
        ServiceJobSummary = 327,
        [EnumMember]
        RVMission = 328,
        [EnumMember]
        RVMissionSummary = 329,
        [EnumMember]
        RVInvoice = 330,
        [EnumMember]
        RVInvoiceSummary = 331,
        [EnumMember]
        RVRegularReceipt = 332,
        [EnumMember]
        RVRegularReceiptSummary = 333,
        [EnumMember]
        RVRegularReceiptDetail = 334,
        [EnumMember]
        PVMission = 335,
        [EnumMember]
        PVMissionSummary = 336,
        [EnumMember]
        PVInvoice = 337,
        [EnumMember]
        PVInvoiceSummary = 338,
        [EnumMember]
        PVRegularPayment = 339,
        [EnumMember]
        PVRegularPaymentSummary = 340,
        [EnumMember]
        PVMissionPayment = 341,
        [EnumMember]
        PVMissionPaymentSummary = 342,
        [EnumMember]
        ProductSKUTag = 345,
        [EnumMember]
        DebitProduct = 347,
        [EnumMember]
        DebitProductSummary = 348,
        [EnumMember]
        RegularDebit = 349,
        [EnumMember]
        RegularDebitSummary = 350,
        [EnumMember]
        RegularCredit = 351,
        [EnumMember]
        RegularCreditSummary = 352,
        [EnumMember]
        PointOfSale = 356,
        [EnumMember]
        PointOfSaleSummary = 357,
        [EnumMember]
        Member = 500,
        [EnumMember]
        MemberSummary = 501,
        [EnumMember]
        Committee = 502,
        [EnumMember]
        CommitteeSummary = 503,
        [EnumMember]
        Block = 504,
        [EnumMember]
        BlockSummary = 505,
        [EnumMember]
        Grade = 506,
        [EnumMember]
        GradeSummary = 507,
        [EnumMember]
        Marriage = 508,
        [EnumMember]
        MarriageSummary = 509,
        [EnumMember]
        Divorce = 510,
        [EnumMember]
        DivorceSummary = 511,
        [EnumMember]
        Death = 512,
        [EnumMember]
        DeathSummary = 513,
        [EnumMember]
        Complaint = 514,
        [EnumMember]
        ComplaintSummary = 515,
        [EnumMember]
        Project = 516,
        [EnumMember]
        ProjectSummary = 517,
        [EnumMember]
        Bans = 518,
        [EnumMember]
        BansSummary = 519,
        [EnumMember]
        Masjid = 520,
        [EnumMember]
        MasjidSummmary = 521,
        [EnumMember]
        JV = 553,
        [EnumMember]
        JVDetail = 554,
        [EnumMember]
        JVSummary = 555,
        [EnumMember]
        Services = 600,
        [EnumMember]
        ServicesSummary = 601,
        [EnumMember]
        Appointments = 602,
        [EnumMember]
        AppointmentsSummary = 603,
        [EnumMember]
        TreatmentGroups = 604,
        [EnumMember]
        TreatmentGroupsSummary = 605,
        [EnumMember]
        TreatmentTypes = 606,
        [EnumMember]
        TreatmentTypesSummary = 607,
        [EnumMember]
        ServiceGroups = 608,
        [EnumMember]
        ServiceGroupsSummary = 609,
        [EnumMember]
        ServiceAvailables = 610,
        [EnumMember]
        ServiceAvailablesSummary = 611,
        [EnumMember]
        PricingTypes = 612,
        [EnumMember]
        PricingTypesSummary = 613,
        [EnumMember]
        ExtraTimeTypes = 614,
        [EnumMember]
        ExtraTimeTypesSummary = 615,
        [EnumMember]
        PurchaseTender = 700,
        [EnumMember]
        PurchaseTenderSummary = 701,
        [EnumMember]
        PurchaseQuotation = 702,
        [EnumMember]
        PurchaseQuotationSummary = 703,
        [EnumMember]
        SupplierInventory = 800,
        [EnumMember]
        EmarsysProductList = 900,
        [EnumMember]
        JobProfileArchived = 1000,
        [EnumMember]
        JobProfileArchivedSummary = 1001,
        [EnumMember]
        ArchivedJob = 1002,
        [EnumMember]
        ArchivedJobSummary = 1003,
        [EnumMember]
        DocManagement = 1100,
        [EnumMember]
        Workflow = 1200,
        [EnumMember]
        WorkflowSummary = 1201,
        [EnumMember]
        ExpenseEntry = 1300,
        [EnumMember]
        ExpenseEntrySummary = 1301,
        [EnumMember]
        ProductMaster = 1400,
        [EnumMember]
        QuickCustomer = 1402,
        [EnumMember]
        TaxSetting = 1500,
        [EnumMember]
        TaxSettingSummary = 1501,
        [EnumMember]
        QuickSupplier = 1600,
        [EnumMember]
        QuickSupplierSummary = 1601,
        [EnumMember]
        SalesVoucher = 1700,
        [EnumMember]
        SalesVoucherSummary = 1701,
        [EnumMember]
        RVInvoiceAllocation = 1800,
        [EnumMember]
        RVInvoiceAllocationSummary = 1801,
        [EnumMember]
        PVInvoiceAllocation = 1802,
        [EnumMember]
        PVInvoiceAllocationSummary = 1803,
        [EnumMember]
        GeoLogs = 1804,
        [EnumMember]
        LeaveRequest = 1900,
        [EnumMember]
        LeaveRequestSummary = 1901,
        [EnumMember]
        LeaveAllocation = 1902,
        [EnumMember]
        LeaveAllocationSummary = 1903,
        [EnumMember]
        LeaveBlockList = 1904,
        [EnumMember]
        LeaveBlockListSummary = 1905,
        [EnumMember]
        LeaveType = 1908,
        [EnumMember]
        LeaveTypeSummary = 1909,
        [EnumMember]
        LeaveStatus = 1910,
        [EnumMember]
        LeaveStatusSummary = 1911,
        [EnumMember]
        LeaveSession = 1912,
        [EnumMember]
        LeaveSessionSummary = 1913,
        [EnumMember]
        SalaryMethod = 1914,
        [EnumMember]
        SalaryMethodSummary = 1915,
        [EnumMember]
        SalaryComponentType = 1916,
        [EnumMember]
        SalaryComponentTypeSummary = 1917,
        [EnumMember]
        SalaryComponent = 1918,
        [EnumMember]
        SalaryComponentSummary = 1919,
        [EnumMember]
        SalarySlip = 1920,
        [EnumMember]
        SalarySlipSummary = 1921,
        [EnumMember]
        PayrollFrequency = 1922,
        [EnumMember]
        PayrollFrequencySummary = 1923,
        [EnumMember]
        PaymentEntry = 1924,
        [EnumMember]
        PaymentEntrySummary = 1925,
        [EnumMember]
        JobType = 1926,
        [EnumMember]
        JobTypeSummary = 1927,
        [EnumMember]
        EmployeeRole = 1928,
        [EnumMember]
        EmployeeRoleSummary = 1929,
        [EnumMember]
        Designation = 1930,
        [EnumMember]
        DesignationSummary = 1931,
        [EnumMember]
        AttendenceStatus = 1932,
        [EnumMember]
        AttendenceStatusSummary = 1933,
        [EnumMember]
        Attendence = 1934,
        [EnumMember]
        AttendenceSummary = 1935,
        [EnumMember]
        EmploymentRequest = 2000,
        [EnumMember]
        TicketDescription = 2002,
        [EnumMember]
        TicketDescriptionSummary = 2003,
        [EnumMember]
        EmarsysKuwait = 2005,
        [EnumMember]
        EmarsysKuwaitSummary = 2006,
        [EnumMember]
        Holiday = 2007,
        [EnumMember]
        HolidaySummary = 2008,
        [EnumMember]
        Class = 2009,
        [EnumMember]
        ClassSummary = 2010,
        [EnumMember]
        Student = 2011,
        [EnumMember]
        StudentSummary = 2012,
        [EnumMember]
        StudentCategory = 2013,
        [EnumMember]
        StudentCategorySummary = 2014,
        [EnumMember]
        StudentHouse = 2015,
        [EnumMember]
        StudentHouseSummary = 2016,
        [EnumMember]
        Section = 2017,
        [EnumMember]
        SectionSummary = 2018,
        [EnumMember]
        Classes = 2019,
        [EnumMember]
        ClassesSummary = 2020,
        [EnumMember]
        Subject = 2021,
        [EnumMember]
        SubjectSummary = 2022,
        [EnumMember]
        SubjectType = 2023,
        [EnumMember]
        SubjectTypeSummary = 2024,
        [EnumMember]
        FeeDiscount = 2025,
        [EnumMember]
        FeeDiscountSummary = 2026,
        [EnumMember]
        FeeType = 2027,
        [EnumMember]
        FeeTypeSummary = 2028,
        [EnumMember]
        FeeGroup = 2029,
        [EnumMember]
        FeeGroupSummary = 2030,
        [EnumMember]
        FeeMaster = 2031,
        [EnumMember]
        FeeMasterSummary = 2032,
        [EnumMember]
        MarkGrade = 2033,
        [EnumMember]
        MarkGradeSummary = 2034,
        [EnumMember]
        Exam = 2035,
        [EnumMember]
        ExamSummary = 2036,
        [EnumMember]
        ExamSchedule = 2037,
        [EnumMember]
        ExamScheduleSummary = 2038,
        [EnumMember]
        HostelType = 2039,
        [EnumMember]
        HostelTypeSummary = 2040,
        [EnumMember]
        HostelRoom = 2041,
        [EnumMember]
        HostelRoomSummary = 2042,
        [EnumMember]
        Hostel = 2043,
        [EnumMember]
        HostelSummary = 2044,
        [EnumMember]
        LibraryBook = 2045,
        [EnumMember]
        LibraryBookSummary = 2046,
        [EnumMember]
        LibraryTransactionType = 2047,
        [EnumMember]
        LibraryTransactionTypeSummary = 2048,
        [EnumMember]
        LibraryStaffRegister = 2049,
        [EnumMember]
        LibraryStaffRegisterSummary = 2050,
        [EnumMember]
        LibraryStudentRegister = 2051,
        [EnumMember]
        LibraryStudentRegisterSummary = 2052,
        [EnumMember]
        LibraryTransaction = 2053,
        [EnumMember]
        LibraryTransactionSummary = 2054,
        [EnumMember]
        RoomType = 2055,
        [EnumMember]
        RoomTypeSummary = 2056,
        [EnumMember]
        FeeCollection = 2057,
        [EnumMember]
        FeeCollectionSummary = 2058,
        [EnumMember]
        MarkRegister = 2059,
        [EnumMember]
        MarkRegisterSummary = 2060,
        [EnumMember]
        ClassTeacherMap = 2061,
        [EnumMember]
        ClassTeacherMapSummary = 2062,
        [EnumMember]
        ClassSubjectMap = 2063,
        [EnumMember]
        ClassSubjectMapSummary = 2064,
        [EnumMember]
        StaffAttendence = 2065,
        [EnumMember]
        StaffAttendenceSummary = 2066,
        [EnumMember]
        StudentAttendence = 2067,
        [EnumMember]
        StudentAttendenceSummary = 2068,
        [EnumMember]
        ClassGroup = 2069,
        [EnumMember]
        ClassGroupSummary = 2070,
        [EnumMember]
        ClassRoomType = 2071,
        [EnumMember]
        ClassRoomTypeSummary = 2072,
        [EnumMember]
        Complains = 2073,
        [EnumMember]
        ComplainsSummary = 2074,
        [EnumMember]
        ComplaintSourceType = 2075,
        [EnumMember]
        ComplaintSourceTypeSummary = 2076,
        [EnumMember]
        ComplainType = 2077,
        [EnumMember]
        ComplainTypeSummary = 2078,
        [EnumMember]
        EnquiryReferenceType = 2079,
        [EnumMember]
        EnquiryReferenceTypeSummary = 2080,
        [EnumMember]
        EnquirySource = 2081,
        [EnumMember]
        EnquirySourceSummary = 2082,
        [EnumMember]
        Medium = 2083,
        [EnumMember]
        MediumSummary = 2084,
        [EnumMember]
        Shift = 2085,
        [EnumMember]
        ShiftSummary = 2086,
        [EnumMember]
        SubjectTopic = 2089,
        [EnumMember]
        SubjectTopicSummary = 2090,
        [EnumMember]
        TeacherActivity = 2091,
        [EnumMember]
        TeacherActivitySummary = 2092,
        [EnumMember]
        VisitorBook = 2093,
        [EnumMember]
        VisitorBookSummary = 2094,
        [EnumMember]
        SubjectTeacherMap = 2095,
        [EnumMember]
        SubjectTeacherMapSummary = 2096,
        [EnumMember]
        Company1 = 2097,
        [EnumMember]
        CompanySummary1 = 2098,
        [EnumMember]
        CompanyGroup = 2099,
        [EnumMember]
        CompanyGroupSummary = 2100,
        [EnumMember]
        AdmissionEnquiry = 2101,
        [EnumMember]
        AdmissionEnquirySummary = 2102,
        [EnumMember]
        AcademicYear = 2103,
        [EnumMember]
        AcademicYearSummary = 2104,
        [EnumMember]
        Schools = 2105,
        [EnumMember]
        SchoolsSummary = 2106,
        [EnumMember]
        LibraryBookConditions = 2107,
        [EnumMember]
        LibraryBookConditionsSummary = 2108,
        [EnumMember]
        LibraryBookType = 2109,
        [EnumMember]
        LibraryBookTypeSummary = 2110,
        [EnumMember]
        VehicleType = 2111,
        [EnumMember]
        VehicleTypeSummary = 2112,
        [EnumMember]
        VehicleOwnershipType = 2113,
        [EnumMember]
        VehicleOwnershipTypeSummary = 2114,
        [EnumMember]
        LibraryBookStatus = 2115,
        [EnumMember]
        LibraryBookStatusSummary = 2116,
        [EnumMember]
        LibraryBookCategory = 2117,
        [EnumMember]
        LibraryBookCategorySummary = 2118,
        [EnumMember]
        Family = 2119,
        [EnumMember]
        FamilySummary = 2120,
        [EnumMember]
        Mahallu = 2121,
        [EnumMember]
        MahalluSummary = 2122,
        [EnumMember]
        StudentApplication = 2123,
        [EnumMember]
        StudentApplicationSummary = 2124,
        [EnumMember]
        AlbumType = 2125,
        [EnumMember]
        AlbumTypeSummary = 2126,
        [EnumMember]
        Album = 2127,
        [EnumMember]
        AlbumSummary = 2128,
        [EnumMember]
        Collaboration = 2129,
        [EnumMember]
        CollaborationSummary = 2130,
        [EnumMember]
        Event = 2131,
        [EnumMember]
        EventSummary = 2132,
        [EnumMember]
        EventAudienceType = 2133,
        [EnumMember]
        EventAudienceTypeSummary = 2134,
        [EnumMember]
        Poll = 2135,
        [EnumMember]
        PollSummary = 2136,
        [EnumMember]
        Questionnaire = 2137,
        [EnumMember]
        QuestionnaireSummary = 2138,
        [EnumMember]
        QuestionnaireSet = 2139,
        [EnumMember]
        QuestionnaireSetSummary = 2140,
        [EnumMember]
        QuestionnaireType = 2141,
        [EnumMember]
        QuestionnaireTypeSummary = 2142,
        [EnumMember]
        QuestionnaireAnswer = 2143,
        [EnumMember]
        QuestionnaireAnswerSummary = 2144,
        [EnumMember]
        QuestionnaireAnswerType = 2145,
        [EnumMember]
        QuestionnaireAnswerTypeSummary = 2146,
        [EnumMember]
        PayrollAttendence = 2147,
        [EnumMember]
        PayrollAttendenceSummary = 2148,
        [EnumMember]
        PayrollAttendenceStatus = 2149,
        [EnumMember]
        PayrollAttendenceStatusSummary = 2150,
        [EnumMember]
        Building = 2151,
        [EnumMember]
        BuildingSummary = 2152,
        [EnumMember]
        AttendenceReason = 2153,
        [EnumMember]
        AttendenceReasonSummary = 2154,
        [EnumMember]
        Sequence = 2155,
        [EnumMember]
        SequenceSummary = 2156,
        [EnumMember]
        Cast = 2157,
        [EnumMember]
        CastSummary = 2158,
        [EnumMember]
        Certificates = 2159,
        [EnumMember]
        CertificatesSummary = 2160,
        [EnumMember]
        StudentTransferRequest = 2163,
        [EnumMember]
        StudentTransferRequestSummary = 2164,
        [EnumMember]
        payslip = 2165,
        [EnumMember]
        payslipSummary = 2166,
        [EnumMember]
        SalaryPaymentModes = 2167,
        [EnumMember]
        SalaryPaymentModesSummary = 2168,
        [EnumMember]
        FeePaymentModes = 2169,
        [EnumMember]
        FeePaymentModesSummary = 2170,
        [EnumMember]
        FeeCycles = 2170,
        [EnumMember]
        FeeCyclesSummary = 2171,
        [EnumMember]
        SalaryStructure = 2173,
        [EnumMember]
        SalaryStructureSummary = 2174,
        [EnumMember]
        FeePeriods = 2175,
        [EnumMember]
        FeePeriodsSummary = 2176,
        [EnumMember]
        FeeFineTypes = 2177,
        [EnumMember]
        FeeFineTypesSummary = 2178,
        [EnumMember]
        FeeConcessionTypes = 2179,
        [EnumMember]
        FeeConcessionTypesSummary = 2180,
        [EnumMember]
        FeeMasterClassMaps = 2181,
        [EnumMember]
        FeeMasterClassMapsSummary = 2182,
        [EnumMember]
        SalaryStructureComponentMap = 2183,
        [EnumMember]
        SalaryStructureComponentMapSummary = 2184,
        [EnumMember]
        StudentApplicationAdvancedSearch = 2185,
        [EnumMember]
        EmployeeSalaryStructure = 2186,
        [EnumMember]
        EmployeeSalaryStructureSummary = 2187,
        [EnumMember]
        TimeTable = 2189,
        [EnumMember]
        TimeTableSummary = 2190,
        [EnumMember]
        TimeTableMaster = 2191,
        [EnumMember]
        TimeTableMasterSummary = 2192,
        [EnumMember]
        FeeDueGeneration = 2193,
        [EnumMember]
        FeeDueGenerationSummary = 2194,
        [EnumMember]
        ExamType = 2195,
        [EnumMember]
        ExamTypeSummary = 2196,
        [EnumMember]
        AssignVehicle = 2197,
        [EnumMember]
        AssignVehicleSummary = 2198,
        [EnumMember]
        VehicleDetailMap = 2199,
        [EnumMember]
        VehicleDetailMapSummary = 2200,
        [EnumMember]
        LessonPlan = 2211,
        [EnumMember]
        LessonPlanSummary = 2212,
        [EnumMember]
        Assignment = 2213,
        [EnumMember]
        AssignmentSummary = 2214,
        [EnumMember]
        StudentRouteStopMap = 2215,
        [EnumMember]
        StudentRouteStopMapSummary = 2216,
        [EnumMember]
        EditSalarySlip = 2217,
        [EnumMember]
        EditSalarySlipSummary = 2218,
        [EnumMember]
        RouteVehicleMap = 2221,
        [EnumMember]
        RouteVehicleMapSummary = 2222,
        [EnumMember]
        StaffRouteStopMap = 2223,
        [EnumMember]
        StaffRouteStopMapSummary = 2224,
        [EnumMember]
        StudentGroup = 2225,
        [EnumMember]
        StudenGroupSummary = 2226,
        [EnumMember]
        StudentGroupMap = 2227,
        [EnumMember]
        StudentGroupMapSummary = 2228,
        [EnumMember]
        StudentGroupFeeMaster = 2229,
        [EnumMember]
        StudentGroupFeeMasterSummary = 2230,
        [EnumMember]
        StudentAssignmentMap = 2231,
        [EnumMember]
        StudentAssignmentMapSummary = 2232,
        [EnumMember]
        FeeDueEdit = 2233,
        [EnumMember]
        EditFeeDueSummary = 2234,
        [EnumMember]
        CollectFeeAccountPosting = 2235,
        [EnumMember]
        StudenAdvancedSearch = 2237,
        [EnumMember]
        StudenAdvancedSearchSummary = 2238,
        [EnumMember]
        EmployeeAdvancedSearch = 2239,
        [EnumMember]
        EmployeeAdvancedSearchSummary = 2240,
        [EnumMember]
        StudentLeaveApplication = 2241,
        [EnumMember]
        StudentLeaveApplicationSummary = 2242,
        [EnumMember]
        CostCenter = 2243,
        [EnumMember]
        CostCenterSummary = 2244,
        [EnumMember]
        PaymentMode = 2245,
        [EnumMember]
        PaymentModeSummary = 2246,
        [EnumMember]
        Accounts_SubLedger = 2247,
        [EnumMember]
        Accounts_SubLedgerSummary = 2248,
        [EnumMember]
        ParentLogin = 2249,
        [EnumMember]
        ParentLoginSummary = 2250,
        [EnumMember]
        ClassSectionMap = 2251,
        [EnumMember]
        ClassSectionMapSummary = 2252,
        [EnumMember]
        Rack = 2253,
        [EnumMember]
        RackSummary = 2254,
        [EnumMember]
        Unit = 2255,
        [EnumMember]
        UnitSummary = 2256,
        [EnumMember]
        GoodsReceivedNote = 2257,
        [EnumMember]
        GoodsReceivedNoteSummary = 2258,
        [EnumMember]
        SalesQuotation = 2259,
        [EnumMember]
        SalesQuotationSummary = 2260,
        [EnumMember]
        DeliveryNote = 2261,
        [EnumMember]
        DeliveryNoteSummary = 2262,
        [EnumMember]
        ClassSubjectSkillMap = 2263,
        [EnumMember]
        ClassSubjectSkillMapSummary = 2264,
        [EnumMember]
        FineMaster = 2265,
        [EnumMember]
        FineMasterSummary = 2266,
        [EnumMember]
        FineMasterStudentMap = 2267,
        [EnumMember]
        FineMasterStudentMapSummary = 2268,
        [EnumMember]
        StudentSkillRegister = 2269,
        [EnumMember]
        StudentSkillRegisterSummary = 2270,
        [EnumMember]
        BundleWrap = 2271,
        [EnumMember]
        BundleWrapSummary = 2272,
        [EnumMember]
        BundleUnWrap = 2273,
        [EnumMember]
        BundleUnWrapSummary = 2274,
        [EnumMember]
        BundleWrapAdvanceSearch = 2275,
        [EnumMember]
        SkillMaster = 2276,
        [EnumMember]
        SkillMasterSummary = 2277,
        [EnumMember]
        SkillGroupMaster = 2278,
        [EnumMember]
        SkillGroupMasterSummary = 2279,
        [EnumMember]
        SalesQuotationAdvanceSearch = 2280,
        [EnumMember]
        TimeSheets = 2281,
        [EnumMember]
        TimeSheetsSummary = 2282,
        [EnumMember]
        TimeSheetsSelf = 2283,
        [EnumMember]
        TimeSheetsSelfSummary = 2284,
        [EnumMember]
        FinalSettlement = 2285,
        [EnumMember]
        FinalSettlementSummary = 2286,
        [EnumMember]
        PackageConfig = 2287,
        [EnumMember]
        PackageConfigSummary = 2288,
        [EnumMember]
        FeePayment = 2289,
        [EnumMember]
        FeePaymentSummary = 2290,
        [EnumMember]
        FeeStructure = 2291,
        [EnumMember]
        FeeStructureSummary = 2292,
        [EnumMember]
        FeeStructureClassMap = 2293,
        [EnumMember]
        FeeStructureClassMapSummary = 2294,
        [EnumMember]
        ApplicationStatusReport = 2295,
        [EnumMember]
        SchoolCreditNote = 2297,
        [EnumMember]
        SchoolCreditNoteSummary = 2298,
        [EnumMember]
        GRNAdvanceSearchView = 2301,
        [EnumMember]
        GRNAdvanceSearchViewSummary = 2302,
        [EnumMember]
        AcademicCalendarMaster = 2303,
        [EnumMember]
        AcademicCalendarMasterSummary = 2304,
        [EnumMember]
        TimeTableList = 2305,
        [EnumMember]
        TimeTableListSummary = 2306,
        [EnumMember]
        ServiceEntry = 2307,
        [EnumMember]
        ServiceEntrySummary = 2308,
        [EnumMember]
        ServiceEntryAdvanceSearch = 2309,
        [EnumMember]
        LibraryStudentsAdvanceSearchView = 2310,
        [EnumMember]
        LibraryStaffssAdvanceSearch = 2312,
        [EnumMember]
        SubjectMarkEntry = 2313,
        [EnumMember]
        SubjectMarkEntrySummary = 2314,
        [EnumMember]
        ClassTiming = 2315,
        [EnumMember]
        ClassTimingSummary = 2316,
        [EnumMember]
        StudentPromotion = 2317,
        [EnumMember]
        StudentPromotionSummary = 2318,
        [EnumMember]
        Lead = 2319,
        [EnumMember]
        LeadSummary = 2320,
        [EnumMember]
        Candidate = 2325,
        [EnumMember]
        CandidateSummary = 2326,
        [EnumMember]
        OnlineQuestions = 2327,
        [EnumMember]
        OnlineQuestionsSummary = 2328,
        [EnumMember]
        OnlineQuestionGroups = 2329,
        [EnumMember]
        OnlineQuestionGroupsSummary = 2330,
        [EnumMember]
        OnlineExams = 2331,
        [EnumMember]
        OnlineExamsSummary = 2332,
        [EnumMember]
        LeadAdvancedSearchView = 2333,
        [EnumMember]
        ExamGroup = 2334,
        [EnumMember]
        ExamGroupSummary = 2335,
        [EnumMember]
        RemarksEntry = 2336,
        [EnumMember]
        RemarksEntrySummary = 2337,
        [EnumMember]
        MarkEntry = 2338,
        [EnumMember]
        MarkEntrySummary = 2339,
        [EnumMember]
        Circular = 2340,
        [EnumMember]
        CircularSummary = 2341,
        [EnumMember]
        CircularType = 2342,
        [EnumMember]
        CircularTypeSummary = 2343,
        [EnumMember]
        MarkPublish = 2345,
        [EnumMember]
        MarkPublishSummary = 2346,
        [EnumMember]
        Agenda = 2347,
        [EnumMember]
        AgendaSummary = 2348,
        [EnumMember]
        AgeCriteria = 2349,
        [EnumMember]
        AgeCriteriaSummary = 2350,
        [EnumMember]
        ProductAdvancedSearchView = 2351,
        [EnumMember]
        HealthEntry = 2353,
        [EnumMember]
        HealthEntrySummary = 2354,
        [EnumMember]
        TransportApplication = 2355,
        [EnumMember]
        TransportApplicationSummary = 2356,
        [EnumMember]
        StudentsAdvanceSearchFull = 2357,
        [EnumMember]
        StudentsAdvanceSearchFullSummary = 2358,
        [EnumMember]
        StreamSubjectMap = 2359,
        [EnumMember]
        StreamSubjectMapSummary = 2360,
        [EnumMember]
        CampusTransfer = 2361,
        [EnumMember]
        CampusTransfersummary = 2362,
        Streams = 2363,
        [EnumMember]
        Streamssummary = 2364,
        [EnumMember]
        RouteShifting = 2365,
        [EnumMember]
        RouteShiftingSummary = 2366,
        [EnumMember]
        StudentTC = 2367,
        [EnumMember]
        StudentTCSummary = 2368,
        [EnumMember]
        SeatAvailabilityList = 2369,
        [EnumMember]
        SeatAvailabilityListSummary = 2370,
        [EnumMember]
        OnlineExamResult = 2371,
        [EnumMember]
        OnlineExamResultSummary = 2372,
        [EnumMember]
        CertificateIssue = 2373,
        [EnumMember]
        CertificateIssueSummary = 2374,
        [EnumMember]
        CertificateHistory = 2375,
        [EnumMember]
        CertificateHistorySummary = 2376,
        [EnumMember]
        TransportApplicationStudentRouteStopMap = 2377,
        [EnumMember]
        TransportApplicationStudentRouteStopMapSummary = 2378,
        [EnumMember]
        AcademicClassMap = 2379,
        [EnumMember]
        AcademicClassMapSummary = 2380,
        [EnumMember]
        EmployeeSettlement = 2381,
        [EnumMember]
        EmployeeSettlementSummary = 2382,
        [EnumMember]
        FeeCategoryMap = 2383,
        [EnumMember]
        FeeCategoryMapSummary = 2384,
        [EnumMember]
        AcademicSchoolMap = 2385,
        [EnumMember]
        AcademicSchoolMapSummary = 2386,
        [EnumMember]
        FunctionalPeriods = 2387,
        [EnumMember]
        FunctionalPeriodsSummary = 2388,
        [EnumMember]
        SalarySlipPublish = 2389,
        [EnumMember]
        SalarySlipPublishSummary = 2390,
        [EnumMember]
        PushNotification = 2391,
        [EnumMember]
        PushNotificationSummary = 2392,
        [EnumMember]
        ParentAdvancedSearch = 2395,
        [EnumMember]
        ParentAdvancedSearchSummary = 2396,
        [EnumMember]
        UserDeviceMap = 2397,
        [EnumMember]
        UserDeviceMapSummary = 2398,
        [EnumMember]
        ClassCoordinatorMap = 2399,
        [EnumMember]
        ClassCoordinatorMapSummary = 2400,
        [EnumMember]
        Gallery = 2401,
        [EnumMember]
        GallerySummary = 2402,
        [EnumMember]
        UnitGroup = 2403,
        [EnumMember]
        UnitGroupSummary = 2404,
        [EnumMember]
        EventTransportAllocation = 2405,
        [EnumMember]
        EventTransportAllocationSummary = 2406,
        [EnumMember]
        SchoolEvents = 2407,
        [EnumMember]
        SchoolEventSummary = 2408,
        [EnumMember]
        OnlineExamQuestionMap = 2409,
        [EnumMember]
        OnlineExamQuestionMapSummary = 2410,
        [EnumMember]
        AdditionalExpense = 2411,
        [EnumMember]
        AdditionalExpenseSummary = 2412,
        [EnumMember]
        StockUpdation = 2413,
        [EnumMember]
        StockUpdationSummary = 2414,
        [EnumMember]
        PaymentLog = 2417,
        [EnumMember]
        PaymentLogSummary = 2418,
        [EnumMember]
        RouteGroup = 2419,
        [EnumMember]
        RouteGroupSummary = 2420,
        [EnumMember]
        StudentConcession = 2421,
        [EnumMember]
        StudentConcessionSummary = 2422,
        [EnumMember]
        BudgetMaster = 2425,
        [EnumMember]
        BudgetMasterSummary = 2426,
        [EnumMember]
        ApplicationForm = 2427,
        [EnumMember]
        ApplicationFormSummary = 2428,
        [EnumMember]
        DriverSchedule = 2429,
        [EnumMember]
        DriverScheduleSummary = 2430,
        [EnumMember]
        FeeDueCancellation = 2431,
        [EnumMember]
        FeeDueCancellationSummary = 2432,
        [EnumMember]
        MailFeeDueStatementReport = 2433,
        [EnumMember]
        MailFeeDueStatementReportSummary = 2434,
        [EnumMember]
        StudentLeaveRequestForDashBoard = 2500,
        [EnumMember]
        StudentTCRequestForDashBoard = 2501,
        [EnumMember]
        CircularForDashBoard = 2502,
        [EnumMember]
        TransportApplicationListForDashBoard = 2503,
        [EnumMember]
        FOCSales = 2505,
        [EnumMember]
        FOCSalesSummary = 2506,
        [EnumMember]
        EmployeeTimeSheetApproval = 2507,
        [EnumMember]
        EmployeeTimeSheetApprovalSummary = 2508,
        [EnumMember]
        EmployeePromotion = 2509,
        [EnumMember]
        EmployeePromotionSummary = 2510,
        [EnumMember]
        CandidateAnswer = 2511,
        [EnumMember]
        CandidateAnswerSummary = 2512,
        [EnumMember]
        TCApproval = 2513,
        [EnumMember]
        TCApprovalSummary = 2514,
        [EnumMember]
        Allergy = 2515,
        [EnumMember]
        AllergySummary = 2516,
        [EnumMember]
        GrantAccess = 2519,
        [EnumMember]
        GrantAccessSummary = 2520,
        [EnumMember]
        StudentDailyPickLogView = 2521,
        [EnumMember]
        StudentDailyPickLogViewSummary = 2522,
        [EnumMember]
        CandidateStudentApplicationAdvancedSearch = 2523,
        [EnumMember]
        SalesInvoiceLite = 2524,
        [EnumMember]
        SalesInvoiceLiteSummary = 2525,
        [EnumMember]
        SalesOrderLite = 2526,
        [EnumMember]
        SalesOrderLiteSummary = 2527,
        [EnumMember]
        SubscriptionSalesOrder = 2528,
        [EnumMember]
        SubscriptionSalesOrderSummary = 2529,
        [EnumMember]
        CanteenSalesOrderAdvanceSearch = 2532,
        [EnumMember]
        LessonObservationForm = 2533,
        [EnumMember]
        LessonObservationFormSummary = 2534,
        [EnumMember]
        StudentAchievement = 2535,
        [EnumMember]
        StudentAchievementSummary = 2536,
        [EnumMember]
        SubjectIncharger = 2537,
        [EnumMember]
        SubjectInchargerSummary = 2538,
        [EnumMember]
        WPS = 2539,
        [EnumMember]
        WPSSummary = 2540,
    }
}
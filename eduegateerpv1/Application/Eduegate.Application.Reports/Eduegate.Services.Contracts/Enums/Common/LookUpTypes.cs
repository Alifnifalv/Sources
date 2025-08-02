using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "LookUpTypes")]
    public enum LookUpTypes
    {
        [EnumMember]
        Title = 1,
        [EnumMember]
        BannerType = 2,
        [EnumMember]
        BannerStatus = 3,
        [EnumMember]
        VoucherType = 4,
        [EnumMember]
        VoucherStatus = 5,
        [EnumMember]
        CustomerGroup = 6,
        [EnumMember]
        NewsType = 7,
        [EnumMember]
        LoginUserStatus = 8,
        [EnumMember]
        Roles = 9,
        [EnumMember]
        Customer = 10,
        [EnumMember]
        StaticContentType = 11,
        [EnumMember]
        Brands = 12,
        [EnumMember]
        NotificationStatus = 13,
        [EnumMember]
        BrandStatus = 14,
        [EnumMember]
        TransactionStatus = 15,
        [EnumMember]
        ProductFamilyType = 16,
        [EnumMember]
        PropertyType = 17,
        [EnumMember]
        StockDocumentReferenceTypes = 18,
        [EnumMember]
        AccountDocumentReferenceTypes = 19,
        [EnumMember]
        BranchGroup = 20,
        [EnumMember]
        Warehouse = 21,
        [EnumMember]
        Gender = 22,
        [EnumMember]
        MaritalStatus = 23,
        [EnumMember]
        Designation = 24,
        [EnumMember]
        EmployeeRole = 25,
        [EnumMember]
        JobType = 26,
        [EnumMember]
        Branch = 27,
        [EnumMember]
        Department = 28,
        [EnumMember]
        Property = 29,
        [EnumMember]
        DocumentFileStatus = 30,
        [EnumMember]
        Currency = 31,
        [EnumMember]
        Language = 32,
        [EnumMember]
        Country = 33,
        [EnumMember]
        Employee = 34,
        [EnumMember]
        WarehouseDocumentReferenceTypes = 35,
        [EnumMember]
        JobStatus = 36,
        [EnumMember]
        JobActivity = 37,
        [EnumMember]
        JobPriority = 38,
        [EnumMember]
        LocationType = 39,
        [EnumMember]
        Driver = 40,
        [EnumMember]
        JobOperationStatus = 41,
        [EnumMember]
        Vehicle = 42,
        [EnumMember]
        MissionJobStatus = 43,
        [EnumMember]
        City = 44,
        [EnumMember]
        Zone = 45,
        [EnumMember]
        Area = 46,
        [EnumMember]
        VehicleType = 47,
        [EnumMember]
        VehicleOwnershipType = 48,
        [EnumMember]
        Basket = 49,
        [EnumMember]
        Site = 50,
        [EnumMember]
        PageType = 51,
        [EnumMember]
        Claims = 52,
        [EnumMember]
        ClaimSets = 53,
        [EnumMember]
        Country1 = 54,
        [EnumMember]
        ClaimType = 55,
        [EnumMember]
        JobSize = 56,
        [EnumMember]
        SupportDocumentType = 57,
        [EnumMember]
        Route = 58,
        [EnumMember]
        TicketPriority = 59,
        [EnumMember]
        TicketAction = 60,
        [EnumMember]
        TicketStatus = 61,
        [EnumMember]
        TicketReason = 62,
        [EnumMember]
        JobOperationReason = 63,
        [EnumMember]
        AccountGroup = 64,
        [EnumMember]
        AccountBehavior = 66,
        [EnumMember]
        Account = 67,
        [EnumMember]
        Pages = 68,
        [EnumMember]
        Boilerplates = 69,
        [EnumMember]
        IncomeOrBalance = 70,
        [EnumMember]
        ChartRowType = 71,
        [EnumMember]
        SchedulerType = 72,
        [EnumMember]
        DeliveryTypeStatus = 73,
        [EnumMember]
        BranchMarketPlace = 74,
        [EnumMember]
        PriceList = 75,
        [EnumMember]
        ImageTypes = 76,
        [EnumMember]
        BranchStatus = 77,
        [EnumMember]
        CompanyStatus = 78,
        [EnumMember]
        WarehouseStatus = 79,
        [EnumMember]
        BranchGroupStatus = 80,
        [EnumMember]
        DepartmentStatus = 81,
        [EnumMember]
        Supplier = 82,
        [EnumMember]
        AllocationBranch = 83,
        [EnumMember]
        DeliveryType = 84,
        [EnumMember]
        ArraySize = 85,
        [EnumMember]
        DistributionDocumentReferenceTypes = 86,
        [EnumMember]
        ServiceProviders = 87,
        [EnumMember]
        JobStatuses = 88,
        [EnumMember]
        AllBranch = 89,
        [EnumMember]
        CityByCompany = 90,
        [EnumMember]
        Manager = 91,
        [EnumMember]
        SHOP = 92,
        [EnumMember]
        REPAIRCUSTOMER = 93,
        [EnumMember]
        SYMPTOMCODE = 94,
        [EnumMember]
        OPERATION = 95,
        [EnumMember]
        OPERATIONGROUP = 96,
        [EnumMember]
        SALESORDERDOCUMENTREFERENCESTATUSES = 97,
        [EnumMember]
        SALESINVOICEDOCUMENTREFERENCESTATUSES = 98,
        [EnumMember]
        PURCHASEORDERDOCUMENTREFERENCESTATUSES = 99,
        [EnumMember]
        PURCHASEINVOICEDOCUMENTREFERENCESTATUSES = 100,
        [EnumMember]
        REPAIRORDERTYPE = 101,
        [EnumMember]
        DefectCode = 102,
        [EnumMember]
        DefectSide = 103,
        [EnumMember]
        Nationality = 104,
        [EnumMember]
        PayComp = 105,
        [EnumMember]
        HRDepartment = 106,
        [EnumMember]
        GroupDesignation = 107,
        [EnumMember]
        MainDesignation = 108,
        [EnumMember]
        HRDesignation = 109,
        [EnumMember]
        ProductiveType = 110,
        [EnumMember]
        EmploymentType = 111,
        [EnumMember]
        RecruitmentType = 112,
        [EnumMember]
        Allowance = 113,
        [EnumMember]
        PeriodSalary = 114,
        [EnumMember]
        LocationMaster = 115,
        [EnumMember]
        DesigMaster = 116,
        [EnumMember]
        AllowanceMaster = 117,
        [EnumMember]
        PayComMatser = 118,
        [EnumMember]
        HRGender = 119,
        [EnumMember]
        EmployeeList = 120,
        [EnumMember]
        HRMaritalStatus = 121,
        [EnumMember]
        DocType = 122,
        [EnumMember]
        HRStatus = 123,
        [EnumMember]
        ProcessStatus = 124,
        [EnumMember]
        Agents = 125,
        [EnumMember]
        ContractType = 126,
        [EnumMember]
        QuotaType = 127,
        [EnumMember]
        VisaCompany = 128,
        [EnumMember]
        EmploymentProcessStatus,
        [EnumMember]
        ActionLinkType = 130,
        [EnumMember]
        PaymentMethod = 131,
        [EnumMember]
        DBDepartment = 132,

        [EnumMember]
        AssetCode = 429,
        [EnumMember]
        AssetCategory = 430,
        [EnumMember]
        AssetEntryDocumentStatuses = 431,
        [EnumMember]
        AssetDepreciationDocumentStatuses = 432,
        [EnumMember]
        AssetRemovalDocumentStatuses = 433,

        [EnumMember]
        TypeOfJob = 134,
        [EnumMember]
        JobOpeningStatus = 135,
        [EnumMember]
        BRANCHTRANSFERDOCUMENTREFERENCESTATUSES = 136,

        [EnumMember]
        ProductTypesName = 138,
        [EnumMember]
        BranchTransferRequestDocumentStatus = 137,
        [EnumMember]
        ReplacementActions = 139,
        [EnumMember]
        BooleanType = 140,
        [EnumMember]
        OREDERCHANGEREQUESTDOCUMENTREFERENCESTATUSES = 141,
        [EnumMember]
        MissionJobOperationStatus = 142,
        [EnumMember]
        Category = 143,
        [EnumMember]
        DocumentStatuses = 144,
        [EnumMember]
        ContactStatus = 145,
        [EnumMember]
        PriceListStatuses = 146,
        [EnumMember]
        ReceivingMethod = 147,
        [EnumMember]
        ReturnMethod = 148,
        [EnumMember]
        MarketPlaceOrderStatuses = 150,
        [EnumMember]
        CategorySettingsSortBy = 149,
        [EnumMember]
        SelectOrNotSelect = 151,
        [EnumMember]
        TreatmentGroup = 152,
        [EnumMember]
        TreatmentType = 153,
        [EnumMember]
        AvailableFor = 154,
        [EnumMember]
        PricingType = 155,
        [EnumMember]
        ExtraTimeType = 156,
        [EnumMember]
        Duration = 157,
        [EnumMember]
        CostCenter = 200,
        [EnumMember]
        PaymentModes = 500,

        [EnumMember]
        PopupPageType,
        [EnumMember]
        PopupBanner,
        [EnumMember]
        BasicStatus = 228,
        [EnumMember]
        MemberType = 229,
        [EnumMember]
        ListName = 230,
        [EnumMember]
        DataList,
        [EnumMember]
        BasicStatuses = 232,
        [EnumMember]
        WorkflowType = 233,
        [EnumMember]
        WorkflowField = 234,
        [EnumMember]
        Conditions = 235,
        [EnumMember]
        ApprovalWorkflow = 236,
        [EnumMember]
        WorkflowCondition = 237,
        [EnumMember]
        ApprovalCondition = 238,
        [EnumMember]
        Months = 239,
        [EnumMember]
        SequenceTypes = 240,

        [EnumMember]
        JournalDocumentStatuses = 600,
        [EnumMember]
        UserSettings = 241,

        [EnumMember]
        TaxTemplates = 242,
        [EnumMember]
        TaxTypes = 243,

        [EnumMember]
        ReceivableGLSubAccounts = 700,
        [EnumMember]
        PayableGLSubAccounts = 701,

        [EnumMember]
        Maids = 800,
        [EnumMember]
        LeadMaids = 801,

        [EnumMember]
        SubjectTypes = 900,
        [EnumMember]
        StudentHouses = 901,
        [EnumMember]
        StudentCategories = 100,
        [EnumMember]
        Section = 223,
        [EnumMember]
        Relegion = 222,

        [EnumMember]
        MarkGrades = 104,
        [EnumMember]
        GuardianTypes = 105,
        [EnumMember]
        FeeTypes = 106,
        [EnumMember]
        ExamSchedules = 107,
        [EnumMember]
        Classes = 908,
        [EnumMember]
        Casts = 109,
        [EnumMember]
        FeeMaster = 110,
        [EnumMember]
        FeeType = 102,
        [EnumMember]
        SupplierStatuses = 113,
        [EnumMember]
        PURCHASERETURNDOCUMENTREFERENCESTATUSES = 550,
        [EnumMember]
        SALESQUOTATIONDOCUMENTREFERENCESTATUSES = 551,

        [EnumMember]
        OccuranceType = 1000,
        [EnumMember]
        OccuranceDay = 1001,
        [EnumMember]
        DeliverTimeSlot = 1002,
        [EnumMember]
        AcademicYearCalendarStatus = 1005,

        [EnumMember]
        ChartMonthAndYearPeriod = 1200,
        [EnumMember]
        ChartYearPeriod = 1201,

        [EnumMember]
        ActiveStatus = 1300,

        [EnumMember]
        LeaveStatus = 1350,

        [EnumMember]
        ApplicationStatus = 1400,

        [EnumMember]
        StudentTransferRequestStatus = 1450,

        [EnumMember]
        IsRefundable = 1500,

        [EnumMember]
        PresentStatus = 1550,

        [EnumMember]
        LessonPlanStatus = 1600,

        [EnumMember]
        IsCollected = 1650,

        [EnumMember]
        LibraryTransactionType = 1700,

        [EnumMember]
        BookStatus = 1750,


        [EnumMember]
        IsShared = 1800,

        [EnumMember]
        BloodGroup = 1850,

        [EnumMember]
        RouteType = 1900,
        [EnumMember]
        LibraryStudents = 1950,
        [EnumMember]
        LibraryStaffs = 2000,
        [EnumMember]
        AcademicYearCode = 2050,
        [EnumMember]
        ClassTimingSets = 3000,
        [EnumMember]
        AcademicYearStatus = 3050,
        [EnumMember]
        StudentTransferRequestReasons = 3100,
        [EnumMember]
        LeaveSession = 3150,
        [EnumMember]
        LeaveType = 3200,
        [EnumMember]
        AcademicYear = 3250,
        [EnumMember]
        SubGroup = 3251,
        [EnumMember]
        LedgerAccount = 3260,
        [EnumMember]
        MainGroup = 3270,
        [EnumMember]
        Languages = 3285,
        [EnumMember]
        ApplicationSubmitType = 3290,
        [EnumMember]
        FilterClasses = 3300,
        [EnumMember]
        TeachingAids = 3350,
        [EnumMember]
        MonthNames = 3360,
        [EnumMember]
        LessonPlanTaskTypes = 3370,
        [EnumMember]
        CircularTypes = 3380,
        [EnumMember]
        CircularPriorities = 3381,
        [EnumMember]
        ClassGroup = 3400,
        [EnumMember]
        SkillSets = 3450,
        [EnumMember]
        TransportStatus = 3500,
        [EnumMember]
        TransportApplicationStatus = 3501,
        [EnumMember]
        Cashier = 3550,
        [EnumMember]
        SecondLanguage = 3600,
        [EnumMember]
        ThirdLanguage = 3650,
        [EnumMember]
        School = 3700,
        [EnumMember]
        LeadStatus = 3750,
        [EnumMember]
        LicenseType = 3755,
        [EnumMember]
        Catogories = 3760,
        [EnumMember]
        AgendaStatus = 3765,
        [EnumMember]
        MarkStatuses = 3770,
        [EnumMember]
        AllSectionsFilter = 3775,
        [EnumMember]
        PickupStopALL = 3780,
        [EnumMember]
        DropStopALL = 3785,
        [EnumMember]
        WorkFlow = 3790,
        [EnumMember]
        WorkflowEntitys = 3795,
        [EnumMember]
        OnlineExamStatus = 3798,
        [EnumMember]
        OnlineExamOperationStatus = 3799,
        [EnumMember]
        OnlineExams = 3800,       
        [EnumMember]
        Years = 3802,
        [EnumMember]
        Variables = 3803,
        [EnumMember]
        Operators = 3804,
        [EnumMember]
        AcademicYearWithLastYear=3805,
        [EnumMember]
        Currencies = 3810,
        [EnumMember]
        AdditionalExpense =3811,
        [EnumMember]
        FullAcademicYears = 3812,
        [EnumMember]
        DocumentType =3813,
        [EnumMember]
        StudentStatus = 3814,
        [EnumMember]
        StudentPickedBy = 3815,
        [EnumMember]
        StudentPickupRequestStatus = 3816,
        [EnumMember]
        ConcessionApprovalType=3817,
        [EnumMember]
        ConcessionType=3818,
        [EnumMember]
        PromotionStatus=3819,
        [EnumMember]
        ExcludePromotionStatus = 3820,
        [EnumMember]
        Budget= 3821,
        [EnumMember]
        StockGLAccount = 3822,
        [EnumMember]
        Forms = 3823,
        [EnumMember]
        Parent = 3824,
        [EnumMember]
        TimesheetEntryStatus = 3825,
        [EnumMember]
        LeaveGroupList=3826,
        [EnumMember]
        DepartmentsTeacher = 3827
    }
}

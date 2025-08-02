using Eduegate.Domain.Entity;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.Enums;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Contracts.Appointments;
using Eduegate.Services.Contracts.Contents.Interfaces;
using Eduegate.Services.Contracts.CRM;
using Eduegate.Services.Contracts.Framework;
using Eduegate.Services.Contracts.Inventory;
using Eduegate.Services.Contracts.Logging;
using Eduegate.Services.Contracts.MobileAppWrapper;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Services.Contracts.PriceSettings;
using Eduegate.Services.Contracts.Security;
using Eduegate.Services.Contracts.Services;
using Eduegate.Services.Contracts.Signups;
using Eduegate.Services.Contracts.SmartView;
using Eduegate.Services.Contracts.Supports;
using Eduegate.Services.Contracts.Warehouses;
using Eduegate.Services.Contracts.Workflows;
using Hangfire;

namespace Eduegate.Services.Client.Factory
{
    public class ClientFactory
    {
        static Tiers tiers = new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers");

        public static ICustomer CustomerServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.Customer();
            service.CallContext = context;
            return service;
        }

        public static IPaymentGatewayService PaymentGatewayServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.PaymentGatewayService();
            service.CallContext = context;
            return service;
        }

        public static ISmartViewService SmartViewServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.SmartViewService();
            service.CallContext = context;
            return service;
        }

        public static IStaticContent StaticContentServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.StaticContent();
            service.CallContext = context;
            return service;
        }

        public static IUtilitiy UtilitiyServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.Utilitiy();
            service.CallContext = context;
            return service;
        }

        public static IVoucher VoucherServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.Voucher();
            service.CallContext = context;
            return service;
        }

        public static IPriceSettings PriceSettingServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.PriceSettings.PriceSettings();
            service.CallContext = context;
            return service;
        }

        public static IPageRenderService PageRenderServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.PageRenderService();
            service.CallContext = context;
            return service;
        }

        public static IShoppingCart ShoppingCartServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.ShoppingCart();
            service.CallContext = context;
            return service;
        }

        public static IOrder OrderServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.Order();
            service.CallContext = context;
            return service;
        }

        public static IOnlineExamService OnlineExamServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.OnlineExamService();
            service.CallContext = context;
            return service;
        }

        public static INews NewsDataServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.News();
            service.CallContext = context;
            return service;
        }

        public static IMenu MenuServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.Menu();
            service.CallContext = context;
            return service;
        }

        public static ILoggingService LoggingServicesClient(CallContext context)
        {
            var service = new Eduegate.Services.Logging.LoggingServices();
            service.CallContext = context;
            return service;
        }

        public static IInventoryService InventoryServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.Inventory.InventoryService();
            service.CallContext = context;
            return service;
        }

        public static IDocumentService DocumentServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.DocumentService();
            service.CallContext = context;
            return service;
        }

        public static IDataFeedService DataFeedServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.DataFeedService();
            service.CallContext = context;
            return service;
        }

        public static ICategoryService CategoryServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.CategoryService();
            service.CallContext = context;
            return service;
        }

        public static IBrandService BrandServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.BrandService();
            service.CallContext = context;
            return service;
        }

        public static IWorkflowService WorkflowServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.Workflow.WorkflowService();
            service.CallContext = context;
            return service;
        }

        public static IWarehouseServices WarehouseServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.Warehouse.WarehouseServices();
            service.CallContext = context;
            return service;
        }

        public static IUserService UserServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.UserService();
            service.CallContext = context;
            return service;
        }

        public static Eduegate.Services.Contracts.Schools.ISchoolService SchoolServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.Schools.SchoolService();
            service.CallContext = context;
            return service;
        }

        public static IProductDetail ProductDetailServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.ProductDetail();
            service.CallContext = context;
            return service;
        }

        public static IFrameworkService FrameworkServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.Frameworks.FrameworkService();
            service.CallContext = context;
            return service;
        }

        public static IEmploymentService EmployementServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.Payroll.EmploymentService();
            service.CallContext = context;
            return service;
        }

        public static IReportGenerationService ReportGenerationServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.ReportGenerationService();
            service.CallContext = context;
            return service;
        }

        public static IContentService ContentServicesClient(CallContext context)
        {
            var service = new Eduegate.Services.Contents.ContentService();
            service.CallContext = context;
            return service;
        }

        public static IEmployeeService EmployeeServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.Payroll.EmployeeService();
            service.CallContext = context;
            return service;
        }

        public static ICountryMaster CountryMasterServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.CountryMaster();
            service.CallContext = context;
            return service;
        }

        public static IBoilerPlateService BoilerPlateServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.BoilerPlateService();
            service.CallContext = context;
            return service;
        }

        public static IBannerService BannerServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.BannerService();
            service.CallContext = context;
            return service;
        }

        public static IAppointmentService AppointmentServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.Appointments.AppointmentService();
            service.CallContext = context;
            return service;
        }

        public static IFixedAssetsService FixedAssetServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.FixedAssetsService();
            service.CallContext = context;
            return service;
        }

        public static IMutualService MutualServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.MutualService();
            service.CallContext = context;
            return service;
        }

        public static ISupplierService SupplierServiceClient(CallContext context, dbEduegateERPContext dbContext)
        {
            var service = new Eduegate.Services.SupplierService();
            service.CallContext = context;
            return service;
        }

        public static ICustomer CustomerServiceClient(CallContext context, dbEduegateERPContext dbContext)
        {
            var service = new Eduegate.Services.Customer();
            service.CallContext = context;
            return service;
        }

        public static IAccountingService AccountingServiceClient(CallContext context, dbEduegateERPContext dbContext)
        {
            var service = new Eduegate.Services.Accounting.AccountingService();
            service.CallContext = context;
            return service;
        }

        public static IProduct ProductCatalogServiceClient(CallContext context, dbEduegateERPContext dbContext)
        {
            var service = new Eduegate.Services.Product();
            service.CallContext = context;
            return service;
        }

        public static IAppDataService AppDataServiceClient(CallContext context, dbEduegateERPContext dbContext)
        {
            var service = new Eduegate.Services.MobileAppWrapper.AppDataService(context);
            service.CallContext = context;
            return service;
        }

        public static IECommerceService ECommerceServiceClient(CallContext context, dbEduegateERPContext dbContext, IBackgroundJobClient backgroundJobs)
        {
            var service = new Eduegate.Services.ECommerceService();
            service.CallContext = context;
            return service;
        }

        public static IShoppingCart ShoppingCartServiceClient(CallContext context, dbEduegateERPContext dbContext, IBackgroundJobClient backgroundJobs)
        {
            var service = new Eduegate.Services.ShoppingCart();
            service.CallContext = context;
            return service;
        }

        public static IMetadataService MetadataServiceClient(CallContext callContext)
        {
            var service = new Eduegate.Services.MetadataService();
            service.CallContext = callContext;
            return service;
        }

        public static IReferenceData ReferenceDataServiceClient(CallContext callContext)
        {
            var service = new Eduegate.Services.ReferenceData();
            service.CallContext = callContext;
            return service;
        }

        public static ISearchService SearchServiceClient(CallContext callContext)
        {
            var service = new Eduegate.Services.SearchService();
            service.CallContext = callContext;
            return service;
        }

        public static ISecurityService SecurityServiceClient(CallContext callContext)
        {
            var service = new Eduegate.Services.Security.SecurityService();
            service.CallContext = callContext;
            return service;
        }

        public static ISettingService SettingServiceClient(CallContext callContext)
        {
            var service = new Eduegate.Services.SettingService();
            service.CallContext = callContext;
            return service;
        }

        public static ITransaction TransactionServiceClient(CallContext callContext)
        {
            var service = new Eduegate.Services.Transaction();
            service.CallContext = callContext;
            return service;
        }

        public static IAccount AccountServiceClient(CallContext callContext)
        {
            var service = new Eduegate.Services.Account();
            service.CallContext = callContext;
            return service;
        }

        public static INotifications NotificationServiceClient(CallContext callContext)
        {
            var service = new Eduegate.Services.Notifications();
            service.CallContext = callContext;
            return service;
        }

        public static IAccountingTransaction AccountingTransactionServiceClient(CallContext callContext)
        {
            var service = new Eduegate.Services.AccountingTransaction();
            service.CallContext = callContext;
            return service;
        }

        public static IAccountingService AccountingServiceClient(CallContext callContext)
        {
            var service = new Eduegate.Services.Accounting.AccountingService();
            service.CallContext = callContext;
            return service;
        }

        public static IFormBuilderService FormBuilderServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.FormBuilderService();
            service.CallContext = context;
            return service;
        }

        public static ICRMService CRMServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.CRM.CRMService();
            service.CallContext = context;
            return service;
        }

        public static ISupportService SupportServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.SupportService();
            service.CallContext = context;
            return service;
        }

        public static ISignupService SignupServiceClient(CallContext context)
        {
            var service = new Eduegate.Services.Signups.SignupService();
            service.CallContext = context;
            return service;
        }

    }
}
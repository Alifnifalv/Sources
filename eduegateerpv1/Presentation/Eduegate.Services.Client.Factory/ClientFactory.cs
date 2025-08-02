using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.Enums;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Contracts.Appointments;
using Eduegate.Services.Contracts.Contents.Interfaces;
using Eduegate.Services.Contracts.CustomerService;
using Eduegate.Services.Contracts.Distributions;
using Eduegate.Services.Contracts.Framework;
using Eduegate.Services.Contracts.Inventory;
using Eduegate.Services.Contracts.Logging;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Services.Contracts.PriceSettings;
using Eduegate.Services.Contracts.Schools;
using Eduegate.Services.Contracts.Security;
using Eduegate.Services.Contracts.Services;
using Eduegate.Services.Contracts.SmartView;
using Eduegate.Services.Contracts.Supports;
using Eduegate.Services.Contracts.Warehouses;
using Eduegate.Services.Contracts.Workflows;
using Eduegate.Services.Contracts.CRM;

namespace Eduegate.Services.Client.Factory
{
    public class ClientFactory
    {
        static Tiers tiers = ConfigurationExtensions.GetAppConfigValue<Tiers>("Tiers");

        public static IContentService ContentServicesClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Services.Client.Contents.ContentServiceClient(context);
            else
                return new Eduegate.Services.Client.Contents.Direct.ContentServiceClient(context);
        }

        public static ISchoolService SchoolServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Services.Client.Shools.SchoolServiceClient(context);
            else
                return new Eduegate.Services.Client.Direct.Schools.SchoolServiceClient(context);
        }

        public static IStaticContent StaticContentServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Services.Client.StaticContentServiceClient(context);
            else
                return new Eduegate.Services.Client.Direct.StaticContentServiceClient(context);
        }

        public static INews NewsDataServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Services.Client.NewsDataServiceClient(context);
            else
                return new Eduegate.Services.Client.Direct.NewsDataServiceClient(context);
        }

        public static IVoucher VoucherServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.VoucherServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.VoucherServiceClient(context);
        }

        public static ISmartViewService SmartViewServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Services.Client.Frameworks.SmartViewServiceClient(context);
            else
                return new Eduegate.Services.Client.Direct.Frameworks.SmartViewServiceClient(context);
        }

        public static IBannerService BannerServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.BannerServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.BannerServiceClient(context);
        }

        public static IDataFeedService DataFeedServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.DataFeedServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.DataFeedServiceClient(context);
        }

        public static IBrandService BrandServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.BrandServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.BrandServiceClient(context);
        }

        public static IRepairOrderService RepairOrderServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.CustomerService.RepairOrderServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.CustomerService.RepairOrderServiceClient(context);
        }

        public static ICountryMaster CountryMasterServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.CountryMasterServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.CountryMasterServiceClient(context);
        }

        public static IBoilerPlateService BoilerPlateServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.BoilerPlateServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.BoilerPlateServiceClient(context);
        }


        public static ICategoryService CategoryServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.CategoryServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.CategoryServiceClient(context);
        }

        public static IInventoryService InventoryServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.Inventory.InventoryServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.Inventory.InventoryServiceClient(context);
        }

        public static ICustomer CustomerServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.CustomerServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.CustomerServiceClient(context);
        }

        public static IDistributionService DistributionServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.DistributionServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.DistributionServiceClient(context);
        }

        public static IPriceSettings PriceSettingServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.PriceSettingServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.PriceSettingServiceClient(context);
        }

        public static INotifications NotificationServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.NotificationServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.NotificationServiceClient(context);
        }

        public static IOrder OrderServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.OrderServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.OrderServiceClient(context);
        }

        public static IShoppingCart ShoppingCartServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.ShoppingCartServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.ShoppingCartServiceClient(context);
        }

        public static IAccountingTransaction AccountingTransactionServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.AccountingTransactionServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.AccountingTransactionServiceClient(context);
        }

        public static IProduct ProductCatalogServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.ProductCatalogServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.ProductCatalogServiceClient(context);
        }

        public static IMenu MenuServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.MenuServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.MenuServiceClient(context);
        }

        public static IFrameworkService FrameworkServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Services.Client.Frameworks.FrameworkServiceClient(context);
            else
                return new Eduegate.Services.Client.Direct.Frameworks.FrameworkServiceClient(context);
        }

        public static IAppointmentService AppointmentServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Services.Client.Appointments.AppointmentServiceClient(context);
            else
                return new Eduegate.Services.Client.Direct.Appointments.AppointmentServiceClient(context);
        }

        public static IEmployeeService EmployeeServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.Payroll.EmployeeServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.Payroll.EmployeeServiceClient(context);
        }

        public static IProductDetail ProductDetailServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.ProductDetailServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.ProductDetailServiceClient(context);
        }

        public static ISearchService SearchServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.SearchServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.SearchServiceClient(context);
        }

        public static IMetadataService MetadataServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.MetadataServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.MetadataServiceClient(context);
        }

        public static IReferenceData ReferenceDataServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.ReferenceDataServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.ReferenceDataServiceClient(context);
        }

        public static IAccountingService AccountingServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.Accounts.AccountingServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.Accounts.AccountingServiceClient(context);
        }

        public static IFixedAssetsService FixedAssetServiceClient()
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.FixedAssetServiceClient(null);
            else
                return new Eduegate.Service.Client.Direct.FixedAssetServiceClient(null);
        }

        public static IFixedAssetsService FixedAssetServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.FixedAssetServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.FixedAssetServiceClient(context);
        }

        public static IAccount AccountServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.AccountServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.AccountServiceClient(context);
        }

        public static ISecurityService SecurityServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.SecurityServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.SecurityServiceClient(context);
        }

        public static IMutualService MutualServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.MutualServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.MutualServiceClient(context);
        }

        public static IDocumentService DocumentServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.DocumentServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.DocumentServiceClient(context);
        }

        public static IEmploymentService EmployementServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.Payroll.EmployementServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.Payroll.EmployementServiceClient(context);
        }

        public static ISupplierService SupplierServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.SupplierServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.SupplierServiceClient(context);
        }

        public static ITransaction TransactionServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.TransactionServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.TransactionServiceClient(context);
        }

        public static IUserService UserServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.UserServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.UserServiceClient(context);
        }

        public static ISettingService SettingServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.SettingServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.SettingServiceClient(context);
        }

        public static IPageRenderService PageRenderServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.PageRenderServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.PageRenderServiceClient(context);
        }

        public static ISupportService SupportServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.SupportServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.SupportServiceClient(context);
        }

        public static IWorkflowService WorkflowServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Services.Client.Workflow.WorkflowServiceClient(context);
            else
                return new Eduegate.Services.Client.Direct.Workflow.WorkflowServiceClient(context);
        }

        public static IWarehouseServices WarehouseServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.WarehouseServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.WarehouseServiceClient(context);
        }

        public static IUtilitiy UtilitiyServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.UtilityServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.UtilityServiceClient(context);
        }

        public static ILoggingService LoggingServicesClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Service.Client.Logging.LoggingServiceClient(context);
            else
                return new Eduegate.Service.Client.Direct.Logging.LoggingServiceClient(context);
        }

        public static ICRMService CRMServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Services.Client.CRM.CRMServiceClient(context);
            else
                return new Eduegate.Services.Client.Direct.CRMServiceClient(context);
        }

        public static ICertificateService CertificateServiceClient(CallContext context)
        {
            //if (tiers == Tiers.Multi)
            //    return new Eduegate.Service.Client.CertificateServiceClient(context);
            //else
            return new Eduegate.Service.Client.Direct.CertificateServiceClient(context);
        }

        public static IReportGenerationService ReportGenerationServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Services.Client.ReportGenerationServiceClient(context);
            else
                return new Eduegate.Services.Client.Direct.ReportGenerationServiceClient(context);
        }

        public static IPaymentGatewayService PaymentGatewayServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Services.Client.PaymentGatewayServiceClient(context);
            else
                return new Eduegate.Services.Client.Direct.PaymentGatewayServiceClient(context);
        }

        public static IFormBuilderService FormBuilderServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Services.Client.FormBuilderServiceClient(context);
            else
                return new Eduegate.Services.Client.Direct.FormBuilderServiceClient(context);
        }

        public static IOnlineExamService OnlineExamServiceClient(CallContext context)
        {
            if (tiers == Tiers.Multi)
                return new Eduegate.Services.Client.OnlineExamServiceClient(context);
            else
                return new Eduegate.Services.Client.Direct.OnlineExamServiceClient(context);
        }

    }
}
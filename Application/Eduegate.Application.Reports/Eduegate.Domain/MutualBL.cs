using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Framework;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Domain.Mappers.DynamicViews;
using Eduegate.Domain.Mappers;
using Eduegate.Services.Contracts.Distributions;
using Eduegate.Domain.Mappers.Distributions;
using Eduegate.Domain.Repository.Payroll;
using Eduegate.Domain.Repository.Inventory;
//using Eduegate.Domain.Repository.Oracle;
using Eduegate.Framework.Security.Secured;
using Eduegate.Services.Contracts.HR;
using Eduegate.Domain.Repository.HR;
using Eduegate.Domain.Repository.Security;
using Eduegate.Framework.Services;
using Eduegate.Domain.Mappers.Common;
using Eduegate.Services.Contracts.Metadata;
using Eduegate.Domain.Mappers.Mutual;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Services.Contracts.Accounting;

namespace Eduegate.Domain
{
    public class MutualBL
    {
        private CallContext _callContext;
        MutualRepository repository = new MutualRepository();

        public MutualBL(CallContext context)
        {
            _callContext = context;
        }

        public EntityTypeRelationMapDTO SaveEntityTypeRelationMaps(EntityTypeRelationDTO dto)
        {
            bool isSuccess = false;
            EntityTypeRelationMap entity = new EntityTypeRelationMap(); ;
            entity.FromEntityTypeID = (short)dto.FromEntityTypes;
            entity.ToEntityTypeID = (short)dto.ToEntityTypes;
            entity.FromRelationID = dto.FromRelaionID;
            // Remove existing record 
            isSuccess = new MutualRepository().RemoveEntityTypeRelationMaps(entity);

            if (isSuccess)
            {
                foreach (var item in dto.ToRelaionIDs)
                {
                    entity = new EntityTypeRelationMap();
                    entity.FromEntityTypeID = (short)dto.FromEntityTypes;
                    entity.ToEntityTypeID = (short)dto.ToEntityTypes;
                    entity.FromRelationID = dto.FromRelaionID;
                    entity.UpdatedBy = (int)_callContext.LoginID;
                    entity.UpdatedDate = DateTime.Now;

                    entity.CreatedBy = (int)_callContext.LoginID;
                    entity.CreatedDate = DateTime.Now;
                    entity.ToRelationID = item;
                    entity = new MutualRepository().SaveEntityTypeRelationMaps(entity);
                }
                // return last entity only
                return EntityTypeRelationMapMapper.ToDto(entity);
            }
            else return null;
        }

        public List<KeyValueDTO> GetEmployeeIdNameEntityTypeRelation(EntityTypeRelationDTO dto)
        {
            EntityTypeRelationMap entity = new EntityTypeRelationMap();
            entity.FromEntityTypeID = (short)dto.FromEntityTypes;
            entity.ToEntityTypeID = (short)dto.ToEntityTypes;
            entity.FromRelationID = dto.FromRelaionID;

            List<Employee> lists = new MutualRepository().GetEmployeeIdNameEntityTypeRelation(entity);

            if (lists == null || lists.Count == 0)
                return new List<KeyValueDTO>();

            List<KeyValueDTO> dtos = new List<KeyValueDTO>();
            lists.ForEach(x =>
            {
                dtos.Add(new KeyValueDTO
                {
                    Key = Convert.ToString(x.EmployeeIID),
                    Value = x.EmployeeName
                });
            });
            return dtos;
        }

        public List<KeyValueDTO> GetEntityPropertiesByType(int entityType)
        {
            List<KeyValueDTO> dtoList = new List<KeyValueDTO>();
            KeyValueDTO dto = null;

            List<EntityProperty> entityList = new MutualRepository().GetEntityPropertiesByType(entityType);

            if (entityList != null && entityList.Count > 0)
            {
                foreach (var ep in entityList)
                {
                    dto = new KeyValueDTO();

                    dto.Key = ep.EntityPropertyIID.ToString();
                    dto.Value = ep.PropertyName;

                    dtoList.Add(dto);
                }
            }

            return dtoList;
        }

        public List<ContactDTO> CreateEntityProperties(List<ContactDTO> contactDTOList)
        {
            CustomerRepository repo = new CustomerRepository();
            List<EntityPropertyMap> entityPropertyMaps;

            //EntityPropertyMap entityPropertyMap = null;

            if (contactDTOList != null && contactDTOList.Count > 0)
            {
                foreach (var contact in contactDTOList)
                {
                    // Phone
                    if (contact.Phones.IsNotNull() && contact.Phones.Count > 0)
                    {
                        entityPropertyMaps = new List<EntityPropertyMap>();
                        //contact.Phones = new List<EntityPropertyMapDTO>();
                        // add list into list
                        entityPropertyMaps.AddRange(contact.Phones.Select(x => EntityPropertyMapMapper.ToEntity(x)).ToList());
                        // Save Phone
                        entityPropertyMaps = new MutualRepository().CreateEntityProperties(entityPropertyMaps);
                        contact.Phones = entityPropertyMaps.Select(x => EntityPropertyMapMapper.ToDto(x, repo.GetEntityPropertyName((long)x.EntityPropertyID))).ToList();
                    }

                    // Email
                    if (contact.Emails.IsNotNull() && contact.Emails.Count > 0)
                    {
                        entityPropertyMaps = new List<EntityPropertyMap>();
                        //contact.Emails = new List<EntityPropertyMapDTO>();
                        // add list into list
                        entityPropertyMaps.AddRange(contact.Emails.Select(x => EntityPropertyMapMapper.ToEntity(x)).ToList());
                        // Save Email
                        entityPropertyMaps = new MutualRepository().CreateEntityProperties(entityPropertyMaps);
                        contact.Emails = entityPropertyMaps.Select(x => EntityPropertyMapMapper.ToDto(x, repo.GetEntityPropertyName((long)x.EntityPropertyID))).ToList();
                    }

                    // Fax
                    if (contact.Faxs.IsNotNull() && contact.Faxs.Count > 0)
                    {
                        entityPropertyMaps = new List<EntityPropertyMap>();
                        //contact.Faxs = new List<EntityPropertyMapDTO>();
                        // add list into list
                        entityPropertyMaps.AddRange(contact.Faxs.Select(x => EntityPropertyMapMapper.ToEntity(x)).ToList());
                        // Save Fax
                        entityPropertyMaps = new MutualRepository().CreateEntityProperties(entityPropertyMaps);
                        contact.Faxs = entityPropertyMaps.Select(x => EntityPropertyMapMapper.ToDto(x, repo.GetEntityPropertyName((long)x.EntityPropertyID))).ToList();
                    }
                }

            }

            return contactDTOList;
        }

        public List<KeyValueDTO> GetEntityTypeEntitlementByEntityType(EntityTypes entityType)
        {
            List<KeyValueDTO> dtoList = new List<KeyValueDTO>();
            KeyValueDTO dto = null;

            List<EntityTypeEntitlement> entityList = new MutualRepository().GetEntityTypeEntitlementByEntityType((short)entityType);

            if (entityList != null && entityList.Count > 0)
            {
                foreach (var ep in entityList)
                {
                    dto = new KeyValueDTO();

                    dto.Key = ep.EntitlementID.ToString();
                    dto.Value = ep.EntitlementName;

                    dtoList.Add(dto);
                }
            }

            return dtoList;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO ExecuteView(DynamicViews view, string IID1 = "", string IID2 = "")
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            switch (view)
            {
                case DynamicViews.ProductDetails:
                    searchDTO = DynamicViewMapper.Mapper.GetProductDetails(new ProductDetailRepository().GetProduct(long.Parse(IID1)));
                    break;
                case DynamicViews.BundleWrap:
                    searchDTO = DynamicViewMapper.Mapper.GetBundleDetails(new ProductDetailRepository().GetBundleDetails(long.Parse(IID1)));
                    break;
                case DynamicViews.ProductSettings:
                    searchDTO = DynamicViewMapper.Mapper.GetProductSettings(new ProductDetailRepository().GetProduct(long.Parse(IID1)));
                    break;
                case DynamicViews.ProductSKUSettings:
                    var skuConfigDetails = new ProductDetailRepository().GetProductInventorySKUConfig(long.Parse(IID1));

                    // if ProductInventorySKUConfigMaps is  not null then get SKU details
                    if (skuConfigDetails.IsNotNull())
                    {
                        searchDTO = DynamicViewMapper.Mapper.GetProductSKUSettings(skuConfigDetails);
                    }
                    else
                    {
                        var tempProduct = new ProductDetailBL(null).GetProductBySKUID(long.Parse(IID1));
                        var productConfigDetails = new ProductDetailRepository().GetProduct(tempProduct.ProductIID);
                        searchDTO = DynamicViewMapper.Mapper.GetProductSettings(productConfigDetails);
                    }
                    break;
                case DynamicViews.ProductSKUMaps:
                    searchDTO = DynamicViewMapper.Mapper.GetProductSKUMaps(new ProductDetailRepository().GetProduct(long.Parse(IID1)));
                    break;
                case DynamicViews.ProductSales:
                    break;
                case DynamicViews.ProductAccounts:
                    break;
                case DynamicViews.ProductInventory:
                    var secured = new SecuredData(new Eduegate.Domain.Repository.Security.SecurityRepository().GetUserClaimKey(_callContext.LoginID.HasValue ? _callContext.LoginID.Value : 0, (int)Eduegate.Services.Contracts.Enums.ClaimType.Branches));
                    searchDTO = DynamicViewMapper.Mapper.GetProductInventory(new ProductDetailRepository().GetProductInventory(long.Parse(IID1)), secured);
                    break;
                case DynamicViews.ProductOperations:
                    break;
                case DynamicViews.ProductPurchase:
                    break;
                case DynamicViews.Brand:
                    searchDTO = DynamicViewMapper.Mapper.GetBrand(new BrandRepository().GetBrand(long.Parse(IID1)));
                    break;
                case DynamicViews.Category:
                    var category = new ProductCatalogRepository().GetCategory(long.Parse(IID1));
                    Category parentCategory = null;
                    if (category.ParentCategoryID.IsNotNull() && category.ParentCategoryID > 0)
                    {
                        parentCategory = new ProductCatalogRepository().GetCategory((long)category.ParentCategoryID);
                    }
                    searchDTO = DynamicViewMapper.Mapper.GetCategory(category, parentCategory);
                    break;
                case DynamicViews.ProductFamily:
                    var productfamilies = new ProductDetailRepository().GetProductFamily((long.Parse(IID1)));
                    var property = new ProductDetailRepository().GetProperitesByProductFamilyID(productfamilies.ProductFamilyIID);
                    searchDTO = DynamicViewMapper.Mapper.ProductFamilies(productfamilies, property);
                    break;
                case DynamicViews.ProductProperty:
                    var properties = new ProductDetailRepository().GetProperty((long.Parse(IID1)));
                    PropertyType propertyType = null;
                    if (properties.PropertyTypeID.IsNotNull())
                    {
                        propertyType = new ProductDetailRepository().GetPropertyTypeByPropertyTypeID((long)properties.PropertyTypeID);
                    }
                    searchDTO = DynamicViewMapper.Mapper.ProductProperties(properties, propertyType);
                    break;
                case DynamicViews.PropertyType:
                    var propertytypes = new ProductDetailRepository().GetPropertyType(byte.Parse(IID1));
                    var Property = new List<Property>();
                    if (propertytypes.PropertyTypeID.IsNotNull())
                    {
                        Property = new ProductDetailRepository().GetPropertiesByPropertyTypeID(propertytypes.PropertyTypeID);
                    }
                    searchDTO = DynamicViewMapper.Mapper.GetPropertyType(propertytypes, Property);
                    break;
                case DynamicViews.Price:
                    searchDTO = DynamicViewMapper.Mapper.GetProductPriceListDetail(new ProductDetailRepository().GetProductPriceListDetail(long.Parse(IID1), _callContext.CompanyID.HasValue ? (int)_callContext.CompanyID.Value : default(int)));
                    break;
                case DynamicViews.CustomerGroup:
                    searchDTO = DynamicViewMapper.Mapper.GetCustomerGroup(new CustomerRepository().GetCustomerGroup(long.Parse(IID1)));
                    break;
                //case DynamicViews.Voucher:
                //    searchDTO = DynamicViewMapper.Mapper.GetVoucher(new VoucherRepository().GetVoucher(long.Parse(IID1)));
                //    break;
                //case DynamicViews.CategorySKUMaps:
                //    var pr = new ProductCatalogRepository();
                //    var Products = pr.GetProductsByCategory((long.Parse(IID1)));
                //    var ProductSKUMap = pr.GetProductSKUDetailsByCategory((long.Parse(IID1)));
                //    searchDTO = DynamicViewMapper.Mapper.GetCategoryProductSKUMaps(Products, ProductSKUMap);
                //    break;
                case DynamicViews.CategoryImageMaps:
                    searchDTO = DynamicViewMapper.Mapper.GetCategoryImageMaps(new CategoryRepository().GetCategoryImage(null, long.Parse(IID1)));
                    break;
                //case DynamicViews.BrandSKUMaps:
                //    var catalogRepository = new ProductCatalogRepository();
                //    var Products1 = catalogRepository.GetProductsByBrand(long.Parse(IID1));
                //    var Productsku = catalogRepository.GetProductSKUDetailsByBrand((long.Parse(IID1)));
                //    searchDTO = DynamicViewMapper.Mapper.GetProductByBrand(Products1, Productsku);
                //    break;
                case DynamicViews.PriceSKUMaps:
                    searchDTO = DynamicViewMapper.Mapper.GetProductPriceSKU(new ProductDetailRepository().GetProductPriceSKU(long.Parse(IID1)));
                    break;
                case DynamicViews.PurchaseOrder:
                    searchDTO = DynamicViewMapper.Mapper.GetPurchaseTransactionDetail(new TransactionRepository().GetTransaction(long.Parse(IID1)));
                    break;
                case DynamicViews.PurchaseTender:
                    searchDTO = DynamicViewMapper.Mapper.GetPurchaseTransactionDetail(new TransactionRepository().GetTransaction(long.Parse(IID1)));
                    break;
                case DynamicViews.PurchaseQuotation:
                    searchDTO = DynamicViewMapper.Mapper.GetPurchaseTransactionDetail(new TransactionRepository().GetTransaction(long.Parse(IID1)));
                    break;
                case DynamicViews.PurchaseInvoice:
                    searchDTO = DynamicViewMapper.Mapper.GetPurchaseTransactionDetail(new TransactionRepository().GetTransaction(long.Parse(IID1)));
                    break;
                case DynamicViews.PurchaseReturn:
                    searchDTO = DynamicViewMapper.Mapper.GetPurchaseTransactionDetail(new TransactionRepository().GetTransaction(long.Parse(IID1)));
                    break;
                case DynamicViews.PurchaseReturnRequest:
                    searchDTO = DynamicViewMapper.Mapper.GetPurchaseTransactionDetail(new TransactionRepository().GetTransaction(long.Parse(IID1)));
                    break;
                case DynamicViews.ProductSKUDetails:
                    var sku = new ProductDetailRepository().GetProductSKUMap(long.Parse(IID1));
                    var product = (sku.IsNotNull() && sku.ProductID.HasValue) ? new ProductDetailRepository().GetProduct(sku.ProductID.Value) : null;
                    searchDTO = DynamicViewMapper.Mapper.GetProductSKUDetails(sku, product);
                    break;
                case DynamicViews.PurchaseSKUMaps:
                    searchDTO = DynamicViewMapper.Mapper.GetTransaction(new TransactionBL(_callContext).GetTransaction(long.Parse(IID1)));
                    break;
                case DynamicViews.SalesOrder:
                    searchDTO = DynamicViewMapper.Mapper.GetPurchaseTransactionDetail(new TransactionRepository().GetTransaction(long.Parse(IID1)));
                    break;
                case DynamicViews.SalesInvoice:
                    searchDTO = DynamicViewMapper.Mapper.GetPurchaseTransactionDetail(new TransactionRepository().GetTransaction(long.Parse(IID1)));
                    break;
                case DynamicViews.SalesReturn:
                    searchDTO = DynamicViewMapper.Mapper.GetPurchaseTransactionDetail(new TransactionRepository().GetTransaction(long.Parse(IID1)));
                    break;
                case DynamicViews.SalesReturnRequest:
                    searchDTO = DynamicViewMapper.Mapper.GetPurchaseTransactionDetail(new TransactionRepository().GetTransaction(long.Parse(IID1)));
                    break;
                case DynamicViews.ShoppingCart:
                    searchDTO = DynamicViewMapper.Mapper.GetShoppingCartDetails(new ShoppingCartRepository().GetCartDetailbyIID(long.Parse(IID1)));
                    break;
                case DynamicViews.JobEntrySKUDetails:
                    var head = new TransactionRepository().GetOrderByJobId(long.Parse(IID1));
                    searchDTO = DynamicViewMapper.Mapper.GetTransaction(head);
                    break;
                case DynamicViews.JobEntrySODetails:
                    var SODetails = new TransactionRepository().GetTransactionDetailsByJob(long.Parse(IID1), DocumentReferenceTypes.SalesOrder);
                    searchDTO = DynamicViewMapper.Mapper.GetJobTransactionDetail(SODetails);
                    break;
                case DynamicViews.JobEntrySIDetails:
                    var SIDetails = new TransactionRepository().GetTransactionDetailsByJob(long.Parse(IID1), DocumentReferenceTypes.SalesInvoice);
                    searchDTO = DynamicViewMapper.Mapper.GetJobTransactionDetail(SIDetails);
                    break;
                case DynamicViews.JobEntreyPODetails:
                    var PODetails = new TransactionRepository().GetTransactionDetailsByJob(long.Parse(IID1), DocumentReferenceTypes.PurchaseOrder);
                    searchDTO = DynamicViewMapper.Mapper.GetJobTransactionDetail(PODetails);
                    break;
                case DynamicViews.JobEntryPIDetails:
                    var PIDetails = new TransactionRepository().GetTransactionDetailsByJob(long.Parse(IID1), DocumentReferenceTypes.PurchaseInvoice);
                    searchDTO = DynamicViewMapper.Mapper.GetJobTransactionDetail(PIDetails);
                    break;
                case DynamicViews.ShoppingCartItems:
                    searchDTO = DynamicViewMapper.Mapper.GetShoppingCartItems(new ShoppingCartRepository().GetCartItemsBycartID(long.Parse(IID1)));
                    break;
                case DynamicViews.Customer:
                    if (long.Parse(IID1) > 0)
                    {
                        var customer = new CustomerRepository().GetCustomerV2(long.Parse(IID1));
                        Customer parentCustomer = null;
                        if (customer.ParentCustomerID.IsNotNull() && customer.ParentCustomerID > 0)
                        {
                            parentCustomer = new CustomerRepository().GetCustomerV2((long)customer.ParentCustomerID);
                        }
                        searchDTO = DynamicViewMapper.Mapper.GetCustomer(customer, parentCustomer);
                    }

                    break;
                case DynamicViews.Supplier:
                    searchDTO = DynamicViewMapper.Mapper.GetSupplier(new SupplierRepository().GetSupplier(long.Parse(IID1)));
                    break;
                case DynamicViews.BranchTransfer:
                    searchDTO = DynamicViewMapper.Mapper.GetBranchTransfer(new TransactionBL(_callContext).GetTransaction(long.Parse(IID1)));
                    break;
                case DynamicViews.Transaction:
                    searchDTO = DynamicViewMapper.Mapper.GetPurchaseTransactionDetail(new TransactionRepository().GetTransaction(long.Parse(IID1)));
                    break;
                case DynamicViews.DeliveryDetails:
                    searchDTO = DynamicViewMapper.Mapper.GetDeliveryTransactionDetail(new TransactionRepository().GetTransaction(long.Parse(IID1))); //This BL method should be replaced to get delivery details
                    break;
                case DynamicViews.InvoiceDeliveryDetails:
                    var salesorder = new TransactionRepository().GetOrderByinvoiceId(long.Parse(IID1));
                    var transaction = new TransactionRepository().GetTransaction(salesorder.HeadIID);
                    searchDTO = DynamicViewMapper.Mapper.GetInvoiceDeliveryDetail(salesorder, transaction);
                    break;
                case DynamicViews.Location:
                    searchDTO = DynamicViewMapper.Mapper.GetLocation(new MetadataBL(_callContext).GetLocation(long.Parse(IID1)));
                    break;
                case DynamicViews.Basket:
                    searchDTO = DynamicViewMapper.Mapper.GetBasket(new WarehouseBL(_callContext).GetBasket(long.Parse(IID1)));
                    break;
                case DynamicViews.Vehicle:
                    searchDTO = DynamicViewMapper.Mapper.GetVehicle(new MutualBL(_callContext).GetVehicle(long.Parse(IID1)));
                    break;
                case DynamicViews.Route:
                    searchDTO = DynamicViewMapper.Mapper.GetRoute(new DistributionRepository().GetRoute(long.Parse(IID1)));
                    break;
                case DynamicViews.City:
                    searchDTO = DynamicViewMapper.Mapper.GetCity(new MutualRepository().GetCity(long.Parse(IID1)));
                    break;
                case DynamicViews.Zone:
                    searchDTO = DynamicViewMapper.Mapper.GetZone(new MutualRepository().GetZone(long.Parse(IID1)));
                    break;
                case DynamicViews.Area:
                    searchDTO = DynamicViewMapper.Mapper.GetArea(new MutualRepository().GetArea(long.Parse(IID1)));
                    break;
                case DynamicViews.DeliveryTypes:
                    searchDTO = DynamicViewMapper.Mapper.GetDeliveryTypes(new DistributionRepository().GetDeliveryTypes());
                    break;
                case DynamicViews.ProductTypeDeliveryTypes:
                    searchDTO = DynamicViewMapper.Mapper.GetProductTypeDeliveryTypes(new DistributionRepository().GetProductTypeDeliveryTypes(long.Parse(IID1)), true, long.Parse(IID1));
                    break;
                case DynamicViews.ProductSKUTypeDeliveryTypes:
                    searchDTO = DynamicViewMapper.Mapper.GetProductTypeDeliveryTypes(new DistributionRepository().GetProductSKUTypeDeliveryTypes(long.Parse(IID1)), false, long.Parse(IID1));
                    break;
                case DynamicViews.CustomerDeliveryTypes:
                    searchDTO = DynamicViewMapper.Mapper.GetCustomerGroupDeliveryTypes(new DistributionRepository().GetDeliveryTypes(), long.Parse(IID1));
                    break;
                case DynamicViews.ZoneDeliveryTypes:
                    searchDTO = DynamicViewMapper.Mapper.GetZoneDeliveryTypes(new DistributionRepository().GetDeliveryTypes(), short.Parse(IID1));
                    break;
                case DynamicViews.AreaDeliveryTypes:
                    searchDTO = DynamicViewMapper.Mapper.GetAreaDeliveryTypes(new DistributionRepository().GetDeliveryTypes(), int.Parse(IID1));
                    break;
                case DynamicViews.DataFeedLog:
                    {
                        var dataFeed = new DataFeedRepository().GetDataFeedLogByID(long.Parse(IID1));
                        var customer = new DataFeedRepository().GetCustomerByDataFeedID(long.Parse(IID1));
                        searchDTO = DynamicViewMapper.Mapper.GetDataFeedLog(dataFeed, customer);
                    }
                    break;
                case DynamicViews.DataFeed:
                    {
                        var dataFeed = new DataFeedRepository().GetDataFeedType(int.Parse(IID1));
                        searchDTO = DynamicViewMapper.Mapper.GetDataFeedLog(dataFeed);
                    }
                    break;
                case DynamicViews.BannerDetails:
                    searchDTO = DynamicViewMapper.Mapper.GetBanner(new BannerRepository().GetBanner(long.Parse(IID1), _callContext.CompanyID.HasValue ? (int)_callContext.CompanyID : default(int)));
                    break;
                case DynamicViews.PageDetails:
                    searchDTO = DynamicViewMapper.Mapper.GetPageDetails(new PageRenderRepository().GetPageDetails(long.Parse(IID1)));
                    break;
                case DynamicViews.PageBoilerplateDetails:
                    searchDTO = DynamicViewMapper.Mapper.GetPageBoilerplateDetails(new PageRenderRepository().GetBoilerPlate(long.Parse(IID1)));
                    break;
                case DynamicViews.News:
                    searchDTO = DynamicViewMapper.Mapper.GetNewsDetails(new NewsRepository().GetNews(long.Parse(IID1)));
                    break;
                case DynamicViews.Company:
                    searchDTO = DynamicViewMapper.Mapper.GetCompanyDetails(new ReferenceDataRepository().GetCompany(long.Parse(IID1)));
                    break;
                case DynamicViews.Department:
                    searchDTO = DynamicViewMapper.Mapper.GetDepartmentDetails(new ReferenceDataRepository().GetDepartment(long.Parse(IID1)));
                    break;
                case DynamicViews.Warehouse:
                    searchDTO = DynamicViewMapper.Mapper.GetWarehouseDetails(new ReferenceDataRepository().GetWarehouse(long.Parse(IID1)));
                    break;
                case DynamicViews.BranchGroup:
                    searchDTO = DynamicViewMapper.Mapper.GetBranchGroupDetails(new ReferenceDataRepository().GetBranchGroup(long.Parse(IID1)));
                    break;
                case DynamicViews.Branch:
                    searchDTO = DynamicViewMapper.Mapper.GetBranchDetails(new ReferenceDataRepository().GetBranch(long.Parse(IID1)));
                    break;
                case DynamicViews.DocumentType:
                    var DocumentType = new MetadataRepository().GetDocumentType(long.Parse(IID1));
                    var DocumentReferenceType = new MetadataRepository().GetDocumentReferenceTypesByDocumentType(int.Parse(IID1));
                    searchDTO = DynamicViewMapper.Mapper.GetWarehouseDocumentTypeDetails(DocumentType, DocumentReferenceType);
                    break;
                case DynamicViews.InvoiceDetails:
                    searchDTO = DynamicViewMapper.Mapper.GetMissionInvoiceDetails(new TransactionRepository().GetTransactionByJobID(long.Parse(IID1)));
                    break;
                case DynamicViews.JobEntry:
                    searchDTO = DynamicViewMapper.Mapper.GetJobEntryDetails(new WarehouseRepository().GetJobEntryDetails(long.Parse(IID1)));
                    break;
                case DynamicViews.PayfortLogs:
                    searchDTO = DynamicViewMapper.Mapper.GetPayfortDetails(new WarehouseRepository().GetPayfortDetails(long.Parse(IID1)));
                    break;
                case DynamicViews.PaymentKnet:
                    var details = new Eduegate.Domain.Repository.Payment.PaymentRepository().GetPaymentKnetDetails(0, long.Parse(IID1));
                    var cur = new WarehouseRepository().GetCurrencyDetails(int.Parse(details.PaymentCurrency));
                    var stat = new WarehouseRepository().GetStatusDetails(int.Parse(details.InitStatus));
                    searchDTO = DynamicViewMapper.Mapper.GetPaymentKnetDetails(details, cur, stat);
                    break;
                case DynamicViews.PayPal:
                    var PayPals = new Eduegate.Domain.Repository.Payment.PaymentRepository().GetPaymentDetailPaypal(0, long.Parse(IID1));
                    var stats = new WarehouseRepository().GetStatusDetails(int.Parse(PayPals.InitStatus));
                    searchDTO = DynamicViewMapper.Mapper.GetPayPalDetails(PayPals, stats);
                    break;
                case DynamicViews.MasterVisa:
                    var Master = new Eduegate.Domain.Repository.Payment.PaymentRepository().GetMasterVisaDetails(0, long.Parse(IID1));
                    var status = new WarehouseRepository().GetStatusDetails(int.Parse(Master.InitStatus));
                    searchDTO = DynamicViewMapper.Mapper.GetMasterVisaDetails(Master, status);
                    break;
                case DynamicViews.Notification:
                    searchDTO = DynamicViewMapper.Mapper.GetNotificationDetails(new NotificationRepository().GetNotificationDetail(long.Parse(IID1)));
                    break;
                case DynamicViews.EmailData:
                    searchDTO = DynamicViewMapper.Mapper.GetEmaildataDetails(new NotificationRepository().GetEmailData(long.Parse(IID1)));
                    break;
                case DynamicViews.DayBook:
                    var det1 = new AccountRepository().GetAccountDetails(long.Parse(IID1));
                    var det2 = new AccountTransactionRepository().GetDayBookData(long.Parse(IID1));
                    var det3 = new TransactionRepository().GetTransactionDetail(long.Parse(IID1));
                    searchDTO = DynamicViewMapper.Mapper.GetDayBookDetails(det1, det2, det3);
                    break;

                case DynamicViews.JobEntryDetails:
                    var jobs = new WarehouseRepository().GetJobOperationDetailsByJobId(long.Parse(IID1));
                    searchDTO = DynamicViewMapper.Mapper.GetJobEntryDetail(jobs);
                    break;
                case DynamicViews.Driver:
                    searchDTO = DynamicViewMapper.Mapper.GetDriverDetails(new EmployeeRepository().GetEmployee(long.Parse(IID1)));
                    break;
                case DynamicViews.LastTransactions:
                    searchDTO = DynamicViewMapper.Mapper.GetTransactions(new TransactionRepository().GetTransactions(long.Parse(IID1), 20));
                    break;
                case DynamicViews.BranchWiseInventoryQuantity:
                    searchDTO = DynamicViewMapper.Mapper.GetBranchwiseInventory(new InventoryRepository().GetBranchWiseInventory(long.Parse(IID1)));
                    break;
                case DynamicViews.CartDetails:
                    var OrderDetails = new WarehouseRepository().GetCartByJobId(long.Parse(IID1));
                    // Get TransactionHeadIds from OrderDetails
                    var tHeadIds = OrderDetails.Select(x => x.TransactionHeadID).ToList();

                    var CartDetails = new TransactionRepository().GetTransactionDetails(tHeadIds);
                    searchDTO = DynamicViewMapper.Mapper.GetOrdersByCartDetails(OrderDetails, CartDetails);
                    break;
                case DynamicViews.JobComments:
                    searchDTO = DynamicViewMapper.Mapper.GetComments(new MutualRepository().GetCommentsByEntityType((long)EntityTypes.Job, long.Parse(IID1)));
                    break;
                case DynamicViews.TransactionComments:
                    var Job = new MutualRepository().GetJobByHeadID(long.Parse(IID1));
                    var JobID = Job.JobEntryHeadIID;
                    var GetCommentsByHeadID = new MutualRepository().GetCommentsByEntityType((long)EntityTypes.Job, JobID);
                    searchDTO = DynamicViewMapper.Mapper.GetTransactionComments(Job, GetCommentsByHeadID);
                    break;
                case DynamicViews.TransactionCartDetails:
                    var orderDetails = new WarehouseRepository().GetCartDetailsByHeadID(long.Parse(IID1));
                    // Get HeadIds from OrderDetails 
                    var HeadIds = orderDetails.Select(x => x.TransactionHeadID).ToList();

                    var cartDetails = new TransactionRepository().GetTransactionDetails(HeadIds);
                    searchDTO = DynamicViewMapper.Mapper.GetOrdersByCartDetails(orderDetails, cartDetails);
                    break;
                case DynamicViews.TransactionInvoiceDetails:
                    var orders = new TransactionRepository().GetinvoiceByOrderId(long.Parse(IID1));
                    searchDTO = DynamicViewMapper.Mapper.GetInvoiceDetails(orders);
                    break;
                //case DynamicViews.RepairOrder:
                //    searchDTO = DynamicViewMapper.Mapper.GetRepairOrder(new Eduegate.Domain.Repository.Oracle.RepairOrderRepository().GetRepairOrders(int.Parse(IID1)));
                //    break;
                //case DynamicViews.RepairOrderCustomer:
                //    searchDTO = DynamicViewMapper.Mapper.GetCustomer(new Eduegate.Domain.Repository.Oracle.RepairOrderRepository().GetCustomer(int.Parse(IID1)));
                //    break;
                //case DynamicViews.RepairOrderDetails:
                //    searchDTO = DynamicViewMapper.Mapper.RepairOrderDetails(new Eduegate.Domain.Repository.Oracle.RepairOrderRepository().RepairOrderDetails(int.Parse(IID1)));
                //    break;
                //case DynamicViews.VehcileDetails:
                //    searchDTO = DynamicViewMapper.Mapper.VehicleDetails(new Eduegate.Domain.Repository.Oracle.RepairOrderRepository().GetVehcileInfoByChasisNo(IID1));
                //    break;
                case DynamicViews.EmailTemplate:
                    searchDTO = DynamicViewMapper.Mapper.GetEmailDetails(new NotificationRepository().GetEmailNotificationType(int.Parse(IID1)));
                    break;
                case DynamicViews.CartDeliveryDetails:
                    searchDTO = DynamicViewMapper.Mapper.GetDeliveryTransactionDetail(new WarehouseRepository().GetTransactionByCartID(long.Parse(IID1)));
                    break;
                case DynamicViews.EmploymentRequest:
                    var dto = new Eduegate.Domain.Payroll.EmployeeBL(_callContext).GetEmploymentRequest(long.Parse(IID1));
                    searchDTO = DynamicViewMapper.Mapper.GetEmploymentRequest(dto);
                    break;
                case DynamicViews.EmploymentAllowances:
                    var dto1 = new Eduegate.Domain.Payroll.EmployeeBL(_callContext).GetEmploymentAllowance(long.Parse(IID1));
                    searchDTO = DynamicViewMapper.Mapper.GetEmploymentAllowance(dto1);
                    break;
                case DynamicViews.PaymentFort:
                    var transactionOrders = new PaymentRepository().GetOrderByCartId(long.Parse(IID1));
                    var headIds = transactionOrders.Select(x => x.TransactionHeadID).ToList();
                    //var entitlements = new PaymentRepository().GetEntitlementbyHeadID(headIds);
                    searchDTO = DynamicViewMapper.Mapper.GetPaymentDetails(transactionOrders);
                    break;
                //case DynamicViews.PaymentVoucher:
                //    searchDTO = DynamicViewMapper.Mapper.GetVoucherDetails(new VoucherRepository().GetVoucherByCartID(long.Parse(IID1)));
                //    break;
                case DynamicViews.TicketDetails:
                    var ticket = new SupportRepository().GetTicket(long.Parse(IID1));
                    searchDTO = DynamicViewMapper.Mapper.GetTicketDetails(ticket);
                    break;
                case DynamicViews.JobProfile:
                    searchDTO = DynamicViewMapper.Mapper.GetApplicationDetails(new EmploymentServiceRepository().GetApplicationForm(Guid.Parse(IID1)));
                    break;
                case DynamicViews.OrderProcessing:
                    searchDTO = DynamicViewMapper.Mapper.GetOrderProcessingData(new ReferenceDataRepository().GetPaymentMethods());
                    break;
                case DynamicViews.ModifyShoppingCarts:
                    searchDTO = DynamicViewMapper.Mapper.GetModifyShoppingCarts(new ShoppingCartRepository().GetCartDetailbyIID(long.Parse(IID1)));
                    break;
                case DynamicViews.ClaimDetails:
                    searchDTO = DynamicViewMapper.Mapper.GetClaimDetails(new SecurityRepository().GetClaim(long.Parse(IID1)));
                    break;
                case DynamicViews.ClaimSetDetails:
                    searchDTO = DynamicViewMapper.Mapper.GetClaimDetails(new SecurityRepository().GetClaimSet(long.Parse(IID1)));
                    break;
                case DynamicViews.LoginDetails:
                    searchDTO = DynamicViewMapper.Mapper.LoginDetails(new SecurityRepository().GetLogin(long.Parse(IID1)));
                    break;
                case DynamicViews.TransactionByJob:
                    var OrderDetail = new WarehouseRepository().GetCartByJobId(long.Parse(IID1));
                    var trans = new TransactionRepository().GetOrderByJobID(long.Parse(IID1));
                    searchDTO = DynamicViewMapper.Mapper.GetDeliveryTransactionHead(OrderDetail, trans);
                    break;
                case DynamicViews.ProductSKUTag:
                    searchDTO = DynamicViewMapper.Mapper.GetSKUTagDetail(new ProductDetailRepository().GetSKUTag(long.Parse(IID1)));
                    break;
                case DynamicViews.ProductSKUTagMaps:
                    searchDTO = DynamicViewMapper.Mapper.GetSKUTagMapDetails(new ProductDetailRepository().GetSKUTagMaps(long.Parse(IID1)));
                    break;
            }

            return searchDTO;
        }

        public string GetNextTransactionNumber(long documentTypeID, List<KeyValueParameterDTO> parameters = null)
        {
            string nextTransactionNumber = string.Empty;
            DocumentType entity = new MetadataRepository().GetDocumentType(documentTypeID);
            if (entity.IsNotNull())
            {
                var transactionNo = default(string);
                nextTransactionNumber = entity.TransactionNoPrefix;
                if (parameters != null && parameters.Any())
                {
                    foreach (var item in parameters)
                    {
                        transactionNo += entity.TransactionNoPrefix.Replace("{" + item.ParameterName.Trim().ToUpper() + "}", item.ParameterValue);
                    }
                    nextTransactionNumber = transactionNo.Trim() == string.Empty ? nextTransactionNumber : transactionNo.Trim();
                }
                nextTransactionNumber += entity.LastTransactionNo.IsNull() ? "1" : Convert.ToString(entity.LastTransactionNo + 1);
                return nextTransactionNumber;
            }
            else return nextTransactionNumber;
        }

        public string GetAndSaveNextTransactionNumber(long documentTypeID, List<KeyValueParameterDTO> parameters = null)
        {
            string nextTransactionNumber = string.Empty;
            DocumentType entity = new MetadataRepository().GetDocumentType(documentTypeID);
            if (entity.IsNotNull())
            {
                var transactionNo = default(string);
                nextTransactionNumber = entity.TransactionNoPrefix;
                if (parameters != null && parameters.Any())
                {
                    foreach (var item in parameters)
                    {
                        transactionNo += entity.TransactionNoPrefix.Replace("{" + item.ParameterName.Trim().ToUpper() + "}", item.ParameterValue);
                    }
                    nextTransactionNumber = transactionNo.Trim() == string.Empty ? nextTransactionNumber : transactionNo.Trim();
                }
                entity.LastTransactionNo = entity.LastTransactionNo.HasValue ? entity.LastTransactionNo.Value + 1 : 1;
                entity = new MetadataRepository().SaveDocumentType(entity);
                nextTransactionNumber += entity.LastTransactionNo.IsNull() ? "1" : Convert.ToString(entity.LastTransactionNo);
                return nextTransactionNumber;
            }
            else return nextTransactionNumber;
        }

        public DocumentType UpdateLastTransactionNo(long documentTypeID, string transactionNo)
        {
            // get the DocumentType based on DocumentTypeID
            DocumentType entity = new MetadataRepository().GetDocumentType(documentTypeID);
            // split the transactionHead.TransactionNo using entity.TransactionNoPrefix 
            // compare both Transaction Number by add +1 in entity.LastTransactionNo

            if (GetPrefixedNumber(transactionNo) == (entity.LastTransactionNo.IsNull() ? 1 : entity.LastTransactionNo + 1))
            {
                entity.LastTransactionNo = entity.LastTransactionNo.IsNull() ? 1 : entity.LastTransactionNo + 1;
                // Update DocumentType
                entity = new MetadataRepository().SaveDocumentType(entity);
            }
            return entity;
        }

        private long GetPrefixedNumber(string transactionNo)
        {
            long prefixedNumber;

            string lastNumber = new string(transactionNo.Reverse()
                 .SkipWhile(x => !char.IsDigit(x))
                 .TakeWhile(x => char.IsDigit(x))
                 .ToArray());
            long.TryParse(string.Join("", lastNumber.Reverse()), out prefixedNumber);
            return prefixedNumber;
        }

        #region City

        public CityDTO SaveCity(CityDTO dtoCity)
        {
            // Check param variable availability
            if (dtoCity == null)
                return new CityDTO();

            // Convert City DTO to City entity
            City city = CityMapper.Mapper(_callContext).ToEntity(dtoCity);

            // Call Repository
            city = new MutualRepository().SaveCity(city);

            // Convert City entity to City DTO and return
            return CityMapper.Mapper(_callContext).ToDTO(city);
        }

        public CityDTO GetCity(long cityID)
        {
            // Check param variable availability
            if (cityID <= 0)
                return new CityDTO();

            // Call Repository
            City city = new MutualRepository().GetCity(cityID);

            // Convert City entity to City DTO and return
            return CityMapper.Mapper(_callContext).ToDTO(city);
        }

        #endregion


        #region Zone

        public ZoneDTO SaveZone(ZoneDTO dtoZone)
        {
            // Check param variable availability
            if (dtoZone == null)
                return new ZoneDTO();

            // Convert Zone DTO to Zone entity
            Zone zone = ZoneMapper.Mapper(_callContext).ToEntity(dtoZone);

            // Call Repository
            zone = new MutualRepository().SaveZone(zone);

            // Convert Zone entity to Zone DTO and return
            return ZoneMapper.Mapper(_callContext).ToDTO(zone);
        }

        public ZoneDTO GetZone(short zoneID)
        {
            // Check param variable availability
            if (zoneID <= 0)
                return new ZoneDTO();

            // Call Repository
            Zone zone = new MutualRepository().GetZone(zoneID);

            // Convert Zone entity to Zone DTO and return
            return ZoneMapper.Mapper(_callContext).ToDTO(zone);
        }

        #endregion


        #region Area

        public AreaDTO SaveArea(AreaDTO dtoArea)
        {
            // Check param variable availability
            if (dtoArea == null)
                return new AreaDTO();

            // Convert Area DTO to Area entity
            Area area = AreaMapper.Mapper(_callContext).ToEntity(dtoArea);

            // Call Repository
            area = new MutualRepository().SaveArea(area);

            // Convert Area entity to Area DTO and return
            return AreaMapper.Mapper(_callContext).ToDTO(area);
        }

        public AreaDTO GetArea(int areaID)
        {
            // Check param variable availability
            if (areaID <= 0)
                return new AreaDTO();

            // Call Repository
            Area area = new MutualRepository().GetArea(areaID);

            // Convert Area entity to Area DTO and return
            return AreaMapper.Mapper(_callContext).ToDTO(area);
        }

        public List<AreaDTO> GetAreaByCountryID(int? countryID)
        {
            // Check param variable availability
            if (countryID <= 0)
                return new List<AreaDTO>();

            // Call Repository
            List<Area> area = new MutualRepository().GetAreaByCountryID(countryID);

            // Convert Area entity to Area DTO and return
            return AreaMapper.Mapper(_callContext).ToDTO(area);
        }

        public List<CityDTO> GetCityByCountryID(int? countryID)
        {
            if (countryID <= 0)
                return new List<CityDTO>();

            List<City> city = new MutualRepository().GetCityByCountryID(countryID);
            return CityMapper.Mapper(_callContext).ToDTOList(city);
        }

        public List<AreaDTO> GetAreaByCityID(int? cityID = 0, int siteID = 0, int countryID = 0)
        {
            List<Area> area = new List<Area>();
            if (cityID <= 0)
                return new List<AreaDTO>();

            area = new MutualRepository().GetAreaByCityID(cityID);
            return AreaMapper.Mapper(_callContext).ToDTO(area);
        }

        #endregion

        #region Vehicle

        public VehicleDTO SaveVehicle(VehicleDTO dtoVehicle)
        {
            // Check param variable availability
            if (dtoVehicle == null)
                return new VehicleDTO();

            // Convert Vehicle DTO to Vehicle entity
            Vehicle vehicle = VehicleMapper.Mapper(_callContext).ToEntity(dtoVehicle);

            // Call Repository
            vehicle = new MutualRepository().SaveVehicle(vehicle);

            // Convert Vehicle entity to Vehicle DTO and return
            return VehicleMapper.Mapper(_callContext).ToDTO(vehicle);
        }

        public VehicleDTO GetVehicle(long vehicleID)
        {
            // Check param variable availability
            if (vehicleID <= 0)
                return new VehicleDTO();

            // Call Repository
            Vehicle vehicle = new MutualRepository().GetVehicle(vehicleID);

            // Convert Vehicle entity to Vehicle DTO and return
            return VehicleMapper.Mapper(_callContext).ToDTO(vehicle);
        }

        #endregion

        #region Comments
        public CommentDTO SaveComment(CommentDTO dto)
        {
            if (dto == null)
                return new CommentDTO();

            Comment comment = CommentMapper.Mapper(_callContext).ToEntity(dto);
            comment = new MutualRepository().SaveComment(comment);
            return CommentMapper.Mapper(_callContext).ToDTO(comment);
        }

        public List<CommentDTO> GetComments(Eduegate.Infrastructure.Enums.EntityTypes entityType, long referenceID, long departmentID = 0)
        {
            var commentsDTO = new List<CommentDTO>();
            var comments = new MutualRepository().GetComments((int)entityType, referenceID, departmentID);

            // Get username from id
            commentsDTO.AddRange(comments.Select(c => CommentMapper.Mapper(_callContext).ToDTO(c)).ToList());

            return commentsDTO;
        }


        public CommentDTO GetComment(long commentID)
        {
            var comment = new MutualRepository().GetComment(commentID);
            return CommentMapper.Mapper(_callContext).ToDTO(comment);
        }

        public bool DeleteComment(long commentID)
        {
            return new MutualRepository().DeleteComment(commentID);
        }
        #endregion

        #region Attachments
        public AttachmentDTO SaveAttachment(AttachmentDTO dto)
        {
            if (dto == null)
                return new AttachmentDTO();

            return new AttachmentDTO();

            //Attachment comment = AttachmentMapper.Mapper(_callContext).ToEntity(dto);
            //comment = new MutualRepository().SaveAttachment(comment);
            //return AttachmentMapper.Mapper(_callContext).ToDTO(comment);
        }

        public List<AttachmentDTO> GetAttachments(EntityTypes entityType, long referenceID, long departmentID = 0)
        {
            var commentsDTO = new List<AttachmentDTO>();
            var attachments = new MutualRepository().GetAttachments((int)entityType, referenceID, departmentID);

            // Get username from id
            //commentsDTO.AddRange(attachments.Select(c => AttachmentsMapper.Mapper().ToDTO(c)).ToList());

            return commentsDTO;
        }


        public AttachmentDTO GetAttachment(long commentID)
        {
            //var comment = new MutualRepository().GetComment(commentID);
            //return CommentMapper.Mapper(_callContext).ToDTO(comment);
            throw new NotImplementedException();
        }

        public bool DeleteAttachment(long commentID)
        {
            //return new MutualRepository().DeleteComment(commentID);
            throw new NotImplementedException();
        }
        #endregion

        #region Delivery Charge

        public bool SaveDeliveryCharges(List<ProductDeliveryTypeDTO> dtos, long IID, bool isProduct)
        {
            bool result = false;
            var pdtMaps = new List<ProductDeliveryTypeMap>();

            foreach (var dto in dtos)
            {
                pdtMaps.Add(ProductDeliveryTypeMapMapper.Mapper(_callContext).ToEntity(dto));
            }

            result = new MutualRepository().SaveDeliveryCharges(pdtMaps, IID, isProduct);

            return result;
        }

        public bool SaveCustomerDeliveryCharges(List<CustomerGroupDeliveryChargeDTO> dtos, int customerGroupID)
        {
            bool result = false;
            var cgdtMaps = new List<CustomerGroupDeliveryTypeMap>();

            foreach (var dto in dtos)
            {
                cgdtMaps.Add(CustomerGroupDeliveryTypeMapper.Mapper(_callContext).ToEntity(dto));
            }

            result = new MutualRepository().SaveCustomerDeliveryCharges(cgdtMaps, customerGroupID);

            return result;
        }

        public bool SaveZoneDeliveryCharges(List<ZoneDeliveryChargeDTO> dtos, short zoneID)
        {
            bool result = false;
            var dtzMaps = new List<DeliveryTypeAllowedZoneMap>();

            foreach (var dto in dtos)
            {
                dtzMaps.Add(DeliveryTypeAllowedZoneMapper.Mapper(_callContext).ToEntity(dto));
            }

            result = new MutualRepository().SaveZoneDeliveryCharges(dtzMaps, zoneID);

            return result;
        }


        public bool SaveAreaDeliveryCharges(List<AreaDeliveryChargeDTO> dtos, int areaID)
        {
            bool result = false;
            var dtaMaps = new List<DeliveryTypeAllowedAreaMap>();

            foreach (var dto in dtos)
            {
                dtaMaps.Add(DeliveryTypeAllowedAreaMapper.Mapper(_callContext).ToEntity(dto));
            }

            result = new MutualRepository().SaveAreaDeliveryCharges(dtaMaps, areaID);

            return result;
        }

        #endregion

        public SupplierAccountMapDTO GetAccountBySupplierID(long? supplierID)
        {
            SupplierAccountMapDTO supplierAccountMapDTO = new SupplierAccountMapDTO();
            var supplierAccountMap = new MutualRepository().GetAccountBySupplierID(supplierID);

            if (supplierAccountMap.IsNotNull())
            {
                supplierAccountMapDTO.AccountID = supplierAccountMap.AccountID;
                supplierAccountMapDTO.AccountName = supplierAccountMap.AccountName;
            }
            return supplierAccountMapDTO;
        }

        public List<AdditionalExpenseProvisionalAccountMapDTO> GetProvisionalAccountByAdditionalExpense(int? additionalExpenseID)
        {
            var additionalExpenseProvisionalAccountMapDTO = new List<AdditionalExpenseProvisionalAccountMapDTO>();
            var additionalExpenseProvisionalAccountMap = new MutualRepository().GetProvisionalAccountByAdditionalExpense(additionalExpenseID);
            var AccntList = additionalExpenseProvisionalAccountMap.Select(x => x.ProvisionalAccountID).Distinct().ToList();
            var accntData = new MutualRepository().GetAccountDetByAccountIDs(AccntList);
            if (additionalExpenseProvisionalAccountMap.Count() > 0)
            {
                foreach (var det in additionalExpenseProvisionalAccountMap)
                {
                    additionalExpenseProvisionalAccountMapDTO.Add(new AdditionalExpenseProvisionalAccountMapDTO()
                    {
                        AdditionalExpenseID = det.AdditionalExpenseID,
                        ProvisionalAccountID = det.ProvisionalAccountID,
                        AccountName = accntData.Where(x => x.AccountID == det.ProvisionalAccountID).Select(y => y.AccountName).FirstOrDefault(),
                        IsDefault = det.IsDefault
                    });

                }
            }
            return additionalExpenseProvisionalAccountMapDTO;
        }

        public KeyValueDTO GetEntitlementById(short id)
        {
            KeyValueDTO keyValueDto = new KeyValueDTO();
            var entityTypeEntitlement = new MutualRepository().GetEntitlementById(id);

            if (entityTypeEntitlement.IsNotNull())
            {
                keyValueDto.Key = entityTypeEntitlement.EntitlementID.ToString();
                keyValueDto.Value = entityTypeEntitlement.EntitlementName;
            }
            return keyValueDto;
        }

        public List<KeyValueDTO> GetCustomerEntitlements(long customerID)
        {
            List<KeyValueDTO> dtoList = new List<KeyValueDTO>();
            List<EntityTypeEntitlement> customerEntitlement = null;
            var customer = new CustomerRepository().GetCustomerV2(customerID);
            if (customer.IsOfflineCustomer == true)
            {
                customerEntitlement = new MutualRepository().GetEntityTypeEntitlementByEntityType(10);
            }
            else
            {
                customerEntitlement = new MutualRepository().GetEntityTypeEntitlementByEntityType(2);
            }
            if (customerEntitlement != null && customerEntitlement.Count > 0)
            {
                foreach (var ep in customerEntitlement)
                {
                    var dto = new KeyValueDTO();

                    dto.Key = ep.EntitlementID.ToString();
                    dto.Value = ep.EntitlementName;

                    dtoList.Add(dto);
                }
            }

            return dtoList;

        }

        public GeoLocationDTO GetGeoLocation()
        {
            var result = ServiceHelper.HttpGetRequest<GeoLocationDTO>(ConfigurationExtensions.GetAppConfigValue("GeoLocationUrl"), _callContext);
            return result;
        }
        public KeyValueDTO GetDocumentStatus(short statusID)
        {
            KeyValueDTO keyValueDto = new KeyValueDTO();
            var status = new MutualRepository().GetDocumentStatus(statusID);

            if (status.IsNotNull())
            {
                keyValueDto.Key = status.DocumentStatusID.ToString();
                keyValueDto.Value = status.StatusName;
            }
            return keyValueDto;
        }

        public List<DocumentStatus> GetDocumentStatus()
        {
            KeyValueDTO keyValueDto = new KeyValueDTO();
            var status = new MutualRepository().GetDocumentStatus();

            if (status.IsNotNull())
            {
                foreach (var stat in status)
                {
                    keyValueDto.Key = stat.DocumentStatusID.ToString();
                    keyValueDto.Value = stat.StatusName;
                }
            }
            return status;
        }

        public EntitlementMapDTO GetEntitlementMaps(long supplierID, short supplier)
        {
            EntitlementMapDTO dto = new EntitlementMapDTO();
            var entitlement = new MutualRepository().GetEntitlementMap(supplierID, supplier);
            if (entitlement.IsNotNull())
            {
                dto.EntitlementID = entitlement.EntitlementID;
            }
            return dto;
        }


        public string GetNextTransactionNumberByMonthYear(TransactionNumberDTO dto, bool isSave = false)
        {
            int documentTypeID = dto.DocumentTypeID;
            int month = dto.Month;
            int year = dto.Year;
            var parameters = new List<KeyValueParameterDTO>();

            if (!string.IsNullOrEmpty(dto.PaymentMode))
            {
                parameters.Add(new KeyValueParameterDTO { ParameterName = "PaymentMode", ParameterValue = dto.PaymentMode });
            }

            parameters.Add(new KeyValueParameterDTO { ParameterName = "Month", ParameterValue = dto.Month.ToString() });
            parameters.Add(new KeyValueParameterDTO { ParameterName = "Year", ParameterValue = dto.Year.ToString() });

            string nextTransactionNumber = string.Empty;
            var metadataRepository = new MetadataRepository();
            var DocumentTypeEntity = isSave ? metadataRepository.SaveNextTransactionNumberByMonthYear(documentTypeID, month, year) : metadataRepository.GetNextTransactionNumberByMonthYear(documentTypeID, month, year);

            if (DocumentTypeEntity.IsNotNull())
            {
                string monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);

                var documentTypeTransactionNumber = DocumentTypeEntity.DocumentTypeTransactionNumbers.Where(x => x.Month == month && x.Year == year).FirstOrDefault();

                //var transactionNo = default(string);
                nextTransactionNumber = DocumentTypeEntity.TransactionNoPrefix;
                //e.g.RV-{PaymentMode}-{Year}/{Month}
                if (parameters != null && parameters.Any())
                {
                    foreach (var item in parameters)
                    {
                        nextTransactionNumber = nextTransactionNumber.Replace("{" + item.ParameterName.Trim() + "}", item.ParameterValue);
                    }

                    //nextTransactionNumber = transactionNo.Trim() == string.Empty ? nextTransactionNumber : transactionNo.Trim();
                }

                nextTransactionNumber += documentTypeTransactionNumber.LastTransactionNo.IsNull() ? "1" : Convert.ToString(documentTypeTransactionNumber.LastTransactionNo);
                return nextTransactionNumber;
            }
            else return nextTransactionNumber;
        }

        public DateTime GetHoliday(int deliveryTypeID, int siteID, int addedDays)
        {
            return repository.GetHoliday(deliveryTypeID, siteID, addedDays);
        }

        public string GetCultureCode(int cultureID)
        {
            return repository.GetCultureCode(cultureID);
        }

        public List<ScreenShortCutDTO> GetShortCuts(long screenID)
        {
            return ScreenShortCutMapper.Mapper(_callContext).ToDTO(repository.GetShortCuts(screenID));
        }

        public string GetNextSequence(string sequenceType)
        {
            var sequence = repository.GetNextSequence(sequenceType);
            var nextSequence = sequence.LastSequence.HasValue ? sequence.LastSequence.Value : 1;
            var sequenceValue = sequence.Prefix + nextSequence.ToString();
            sequenceValue = sequenceValue.Replace("{YEAR}", DateTime.Now.Year.ToString());
            return sequenceValue;
        }
    }
}

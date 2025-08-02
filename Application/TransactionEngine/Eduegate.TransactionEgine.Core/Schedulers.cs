using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Enums.Schedulers;
using Eduegate.Services.Contracts.Enums.Warehouses;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Services.Contracts.Schedulers;

namespace Eduegate.TransactionEngineCore
{
    public class Schedulers
    {
        public static void Process(ViewModels.TransactionHeadViewModel transaction)
        {
            //Generate a JOB if the scheduler exists
            var schedulers = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().GetSchedulerByType(SchedulerTypes.StockDocumentType, transaction.DocumentTypeID.ToString());

            if (schedulers != null)
            {
                foreach (var scheduler in schedulers)
                {
                    switch (scheduler.SchedulerEntityType)
                    {
                        case SchedulerEntityTypes.Job:
                            CreateJob(transaction, scheduler);
                            break;
                        case SchedulerEntityTypes.PurchaseOrder:
                            CreatePurchaseOrder(transaction, scheduler);
                            break;
                        case SchedulerEntityTypes.SalesInvoice:
                            CreateSalesInvoice(transaction);
                            break;
                        case SchedulerEntityTypes.EmailNotification:
                            // Add Email Notificartion
                            AddOrderNotification(transaction);
                            break;
                    }
                }
            }
        }

        private static void CreateJob(ViewModels.TransactionHeadViewModel transaction, SchedulerDTO scheduler)
        {
            var documentType = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().GetDocumentType(int.Parse(scheduler.EntityValue));

            //TODO : fill all information
            var jobHead = new Services.Contracts.Warehouses.JobEntryHeadDTO()
            {
                BranchID = transaction.BranchID,
                JobEntryDetails = new List<Services.Contracts.Warehouses.JobEntryDetailDTO>(),
                TransactionHeadID = transaction.HeadIID,
                JobStartDate = transaction.TransactionDate,
                JobEndDate = transaction.DeliveryDate, 
                //JobEndDate = transaction.DueDate, // we are not populating this field in TransactionHead, its always null!!!
                DocumentTypeID = int.Parse(scheduler.EntityValue),
                JobStatusID = documentType.ReferenceTypeID == (int?)DocumentReferenceTypes.InboundOperations ? (int?)JobOperationTypes.Receiving :
                    documentType.ReferenceTypeID == (int?)DocumentReferenceTypes.OutboundOperations ? (int?)JobOperationTypes.Picking : documentType.ReferenceTypeID == (int?)DocumentReferenceTypes.MarketplaceOperations ? (int?)JobOperationTypes.Created : null,
                CompanyID = transaction.CompanyID,
            };

            foreach (var detail in transaction.TransactionDetails)
            {
                jobHead.JobEntryDetails.Add(new Services.Contracts.Warehouses.JobEntryDetailDTO()
                {
                    ProductSKUID = detail.ProductSKUMapID,
                    Quantity = detail.Quantity,
                    UnitPrice = detail.UnitPrice
                });
            }

            var newJob = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().CreateUpdateJobEntry(jobHead);
        }

        private static void CreatePurchaseOrder(ViewModels.TransactionHeadViewModel transaction, SchedulerDTO scheduler)
        {
            var documentType = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().GetDocumentType(int.Parse(scheduler.EntityValue));

            var supplierDetail = new Services.Contracts.SupplierDTO();
            if (transaction.SupplierID.IsNotNull() && transaction.SupplierID > 0)
            {
                supplierDetail = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().GetSupplier(transaction.SupplierID.ToString());
            }

            var head = new TransactionDTO()
            {
                TransactionHead = new TransactionHeadDTO()
                {
                    BranchID = transaction.BranchID,
                     //supplierDetail.IsNotNull() && supplierDetail.IsMarketPlace == true ? Convert.ToInt64(supplierDetail.BranchID) : transaction.BranchID,
                    ToBranchID = transaction.ToBranchID,
                    DocumentReferenceTypeID =  Services.Contracts.Enums.DocumentReferenceTypes.PurchaseOrder,
                    CompanyID = transaction.CompanyID,
                    DocumentTypeID = int.Parse(scheduler.EntityValue),
                    CurrencyID = transaction.CurrencyID,
                    CustomerID = transaction.CustomerID,
                    SupplierID = transaction.SupplierID,
                    DeliveryDate = transaction.DeliveryDate,
                    DeliveryTypeID = transaction.DeliveryTypeID,
                    DeliveryMethodID = transaction.DeliveryMethodID,
                    Description = transaction.Description,
                    DocumentStatusID = (short)DocumentStatuses.Approved,
                    TransactionStatusID = (byte)TransactionStatus.New,
                    TransactionDate = DateTime.Now,
                    DueDate = transaction.DueDate,
                    ReferenceHeadID = transaction.HeadIID,
                }
            };

            head.TransactionDetails = new List<TransactionDetailDTO>();

            foreach (var detail in transaction.TransactionDetails)
            {
                // Check if supplier is marketplace supplier
                if (supplierDetail.IsNotNull() && supplierDetail.IsMarketPlace == true)
                {
                    // Get cost price for sku from supplier's branch-pricelist
                    //var skuPriceDetail = new SupplierServiceClient().GetSKUPriceDetailByBranch(Convert.ToInt64(supplierDetail.BranchID), Convert.ToInt64(detail.ProductSKUMapID));
                    //if (skuPriceDetail.IsNotNull() && skuPriceDetail.Cost > 0)
                    //{
                    //    detail.UnitPrice = skuPriceDetail.Cost;
                    //    detail.Amount = detail.Quantity * detail.UnitPrice;
                    //}
                    var supplierEntitlements = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().GetEntitlementMaps(supplierDetail.SupplierIID, (short)EntityTypes.Supplier);
                    byte entitlementId = 4;

                    head.TransactionHead.TransactionHeadEntitlementMaps.Add(new TransactionHeadEntitlementMapDTO()
                    {
                        EntitlementID = supplierEntitlements.EntitlementID.IsNotNull() && supplierEntitlements.EntitlementID > 0 ? (byte)supplierEntitlements.EntitlementID : entitlementId,
                        Amount = detail.Amount,
                    });
                }

                head.TransactionDetails.Add(new TransactionDetailDTO()
                {
                    UnitPrice =  detail.UnitPrice,
                    UnitID = detail.UnitID,
                    Quantity = detail.Quantity,
                    Amount = detail.Amount,
                    ProductID = detail.ProductID,
                    ProductSKUMapID = detail.ProductSKUMapID,
                    ExchangeRate = detail.ExchangeRate,
                    DiscountPercentage = detail.DiscountPercentage,
                });
            }

            new Eduegate.TransactionEgine.ClientFactory.ClientFactory(null).SaveTransactions(head);
        }

        public static void CreateSalesInvoice(ViewModels.TransactionHeadViewModel transaction)
        {
            // Create Sales invoice
            var trans = new TransactionDTO();
            trans.TransactionHead = new TransactionHeadDTO();
            var tt = new TransactionDetailDTO();
            trans.TransactionDetails = new List<TransactionDetailDTO>();

            //var docTypes = new ReferenceDataServiceClient(null).GetDocumentTypesByID(Services.Contracts.Enums.DocumentReferenceTypes.SalesInvoice);

            // Get Doctype for Digital SalesInvoice (I assume it creates SalesInvoice for digital SO only while doing this)
            var digitalDocTypeSetting = new Eduegate.TransactionEgine.ClientFactory.ClientFactory(null).GetSettingDetail("ONLINEDIGITALSALESINVOICEDOCTYPEID");

            // Fill head
            //trans.TransactionHead.HeadIID = transaction.HeadIID;
            trans.TransactionHead.CompanyID = transaction.CompanyID;
            trans.TransactionHead.BranchID = transaction.BranchID;
            trans.TransactionHead.ToBranchID = transaction.ToBranchID;
            trans.TransactionHead.DocumentTypeID = Convert.ToInt32(digitalDocTypeSetting.SettingValue);// sales invoice
            trans.TransactionHead.DocumentReferenceTypeID = Services.Contracts.Enums.DocumentReferenceTypes.SalesInvoice;// sales invoice
            //trans.TransactionHead.TransactionNo = transaction.TransactionNo;
            trans.TransactionHead.Description = transaction.Description;
            trans.TransactionHead.CustomerID = transaction.CustomerID;
            trans.TransactionHead.SupplierID = transaction.SupplierID;
            //trans.TransactionHead.DiscountAmount = transaction.DiscountAmount;
            //trans.TransactionHead.DiscountPercentage = transaction.DiscountPercentage;
            trans.TransactionHead.TransactionDate = DateTime.Now;
            trans.TransactionHead.DueDate = transaction.DueDate;
            trans.TransactionHead.DeliveryDate = transaction.DeliveryDate;
            trans.TransactionHead.DocumentStatusID = (short)DocumentStatuses.Approved;
            trans.TransactionHead.TransactionStatusID = (byte)TransactionStatus.New;
            //trans.TransactionHead.EntitlementID = transaction.EntitlementID;
            trans.TransactionHead.ReferenceHeadID = transaction.HeadIID;
            trans.TransactionHead.DeliveryTypeID = transaction.DeliveryTypeID;
            trans.TransactionHead.CurrencyID = transaction.CurrencyID;
            trans.TransactionHead.DeliveryMethodID = transaction.DeliveryMethodID;
            //trans.TransactionHead.IsShipment = transaction.IsShipment;
            //trans.TransactionHead.EmployeeID = transaction.EmployeeID;
            //trans.TransactionHead.CreatedBy = transaction.CreatedBy;

            // Fill detail
            foreach (var item in transaction.TransactionDetails)
            {
                trans.TransactionDetails.Add(new TransactionDetailDTO()
                {
                    ProductID = item.ProductID,
                    ProductSKUMapID = item.ProductSKUMapID,
                    Quantity = item.Quantity,
                    UnitID = item.UnitID,
                    DiscountPercentage = item.DiscountPercentage,
                    UnitPrice = item.UnitPrice,
                    Amount = item.Amount,
                    ExchangeRate = item.ExchangeRate,
                    //WarrantyDate = item.WarrantyDate,
                    CreatedBy = item.CreatedBy,
                });
            }

            var result = new Eduegate.TransactionEgine.ClientFactory.ClientFactory(null).SaveTransactions(trans);
        }

        public static void AddOrderNotification(ViewModels.TransactionHeadViewModel transaction)
        {
            if (transaction.SupplierID.IsNotNull() && transaction.SupplierID > 0)
            {
                // set notification type to supplier
                var emailNotificationTypeDetail = new Eduegate.TransactionEgine.ClientFactory.ClientFactory(null).GetEmailNotificationType(Services.Contracts.Enums.EmailNotificationTypes.OrderNotificationForSupplier);

                var notificationDTO = new EmailNotificationDTO();
                notificationDTO.EmailNotificationType = Eduegate.Services.Contracts.Enums.EmailNotificationTypes.OrderNotificationForSupplier; 
                notificationDTO.AdditionalParameters = new List<Eduegate.Services.Contracts.Commons.KeyValueParameterDTO>();
                notificationDTO.FromEmailID = new Domain.Setting.SettingBL().GetSettingValue<string>("EmailFrom").ToString();

                // Get supplier's emailid
                var supplierDetail = new Eduegate.TransactionEgine.ClientFactory.ClientFactory(null).GetSupplier(transaction.SupplierID.ToString());

                string websiteUrl = string.Empty;
                var domainsetting = new Eduegate.TransactionEgine.ClientFactory.ClientFactory(null).GetSettingDetail("DOMAINNAME");

                if (domainsetting.IsNotNull())
                {
                    websiteUrl = domainsetting.SettingValue + ": ";
                }


                notificationDTO.Subject = string.Concat(websiteUrl, transaction.TransactionNo, " | ", supplierDetail.FirstName, " ", supplierDetail.LastName, " ");

                // Add OrderID, ReportName, Attachment flag
                notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.OrderNotificationForSupplier.Keys.HeadID, ParameterValue = transaction.HeadIID.ToString() });
                notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.OrderNotificationForSupplier.Keys.ReportName, ParameterValue = "NoReport" });
                notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.OrderNotificationForSupplier.Keys.Attachment, ParameterValue = "fales" });
                notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.OrderNotificationForSupplier.Keys.ReturnFile, ParameterValue = "false" });

                if (supplierDetail.Login.LoginEmailID.IsNotNull())
                {
                    notificationDTO.ToEmailID = supplierDetail.Login.LoginEmailID;
                    notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.OrderNotificationForSupplier.Keys.ErrorMessage, ParameterValue = "" });
                }
                else
                {
                    notificationDTO.ToEmailID = new Domain.Setting.SettingBL().GetSettingValue<string>("EmailFrom").ToString();
                    notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.OrderNotificationForSupplier.Keys.ErrorMessage, ParameterValue = string.Concat("Could not Find Email Address for Supplier: ", supplierDetail.FirstName, " (supplierID: ", transaction.SupplierID, ")") });
                }
                var notificationReponse = Task<EmailNotificationDTO>.Factory.StartNew(() => new Eduegate.TransactionEgine.ClientFactory.ClientFactory(null).SaveEmailData(notificationDTO));
            }
        }
    }
}

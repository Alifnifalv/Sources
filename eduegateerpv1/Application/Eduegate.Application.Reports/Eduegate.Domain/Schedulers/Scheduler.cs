using Eduegate.Domain.Helpers;
using Eduegate.Framework;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Enums.Schedulers;
using System;
using System.Collections.Generic;
using Eduegate.Domain.Entity.Models;

namespace Eduegate.Domain.Schedulers
{
    public class Scheduler
    {
        CallContext _callContext;

        public static Scheduler OrderScheduler(CallContext callContext)
        {
            var manager = new Scheduler();
            manager._callContext = callContext;
            return manager;
        }

        public void Process(TransactionHeadDTO transaction)
        {
            //Generate a JOB if the scheduler exists
            var schedulers = new Eduegate.Domain.Repository.ReferenceDataRepository().GetEntityScheduler(
                    (int)SchedulerTypes.StockDocumentType, transaction.DocumentTypeID.ToString());

            if (schedulers != null)
            {
                foreach (var scheduler in schedulers)
                {
                    switch (scheduler.SchedulerEntityTypeID)
                    {
                        //case (int)SchedulerEntityTypes.Job:
                        //    new Eduegate.Domain.Distributions
                        //        .WarehouseBL(_callContext).CreateJob(transaction, scheduler.EntityValue);
                        //    break;
                        case (int)SchedulerEntityTypes.PurchaseOrder:
                            CreatePurchaseOrder(transaction, scheduler);
                            break;
                        case (int)SchedulerEntityTypes.SalesInvoice:
                            CreateSalesInvoice(transaction);
                            break;
                        //case (int)SchedulerEntityTypes.EmailNotification:
                        //    // Add Email Notificartion
                        //    new Eduegate.Domain.Notifications
                        //        .NotificationHelperBL(_callContext)
                        //        .AddOrderNotification(transaction);
                        //    break;
                    }
                }
            }
        }
       
        private void CreatePurchaseOrder(TransactionHeadDTO transaction, EntityScheduler scheduler)
        {
            var documentType = new Eduegate.Domain.Repository.ReferenceDataRepository().GetDocumentType(int.Parse(scheduler.EntityValue));

            var supplierDetail = new Supplier();
            if (transaction.SupplierID.IsNotNull() && transaction.SupplierID > 0)
            {
                supplierDetail = new Eduegate.Domain.Repository.ReferenceDataRepository().GetSupplier(transaction.SupplierID.Value);
            }

            var head = new TransactionHead()
            {
                BranchID = transaction.BranchID,
                //supplierDetail.IsNotNull() && supplierDetail.IsMarketPlace == true ? Convert.ToInt64(supplierDetail.BranchID) : transaction.BranchID,
                ToBranchID = transaction.ToBranchID,
                //DocumentReferenceTypeID = Services.Contracts.Enums.DocumentReferenceTypes.PurchaseOrder,
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
                TransactionStatusID = (byte)Framework.Enums.TransactionStatus.New,
                TransactionDate = DateTime.Now,
                DueDate = transaction.DueDate,
                ReferenceHeadID = transaction.HeadIID,
            };

            head.TransactionDetails = new List<TransactionDetail>();

            foreach (var detail in transaction.TransactionDetails)
            {
                // Check if supplier is marketplace supplier
                if (supplierDetail.IsNotNull() && supplierDetail.IsMarketPlace == true)
                {
                    var supplierEntitlements = new Eduegate.Domain.Repository.MutualRepository().GetEntitlementMap(supplierDetail.SupplierIID, (short)EntityTypes.Supplier);
                    byte entitlementId = 4;

                    head.TransactionHeadEntitlementMaps.Add(new TransactionHeadEntitlementMap()
                    {
                        EntitlementID = supplierEntitlements.EntitlementID.IsNotNull() && supplierEntitlements.EntitlementID > 0 ? (byte)supplierEntitlements.EntitlementID : entitlementId,
                        Amount = detail.Amount,
                    });
                }

                head.TransactionDetails.Add(new TransactionDetail()
                {
                    UnitPrice = detail.UnitPrice,
                    UnitID = detail.UnitID,
                    Quantity = detail.Quantity,
                    Amount = detail.Amount,
                    ProductID = detail.ProductID,
                    ProductSKUMapID = detail.ProductSKUMapID,
                    ExchangeRate = detail.ExchangeRate,
                    DiscountPercentage = detail.DiscountPercentage,
                });
            }

            new Eduegate.Domain.Repository.TransactionRepository().SaveTransactions(head);
        }

        public void CreateSalesInvoice(TransactionHeadDTO transaction)
        {
            // Create Sales invoice
            var trans = new TransactionHead();
            var tt = new TransactionDetailDTO();
            trans.TransactionDetails = new List<TransactionDetail>();

            //var docTypes = new ReferenceDataServiceClient(null).GetDocumentTypesByID(Services.Contracts.Enums.DocumentReferenceTypes.SalesInvoice);

            // Get Doctype for Digital SalesInvoice (I assume it creates SalesInvoice for digital SO only while doing this)
            var digitalDocTypeSetting = ShoppingCartHelper.Helper(_callContext).GetSettingValue<int>("ONLINEDIGITALSALESINVOICEDOCTYPEID", _callContext.CompanyID.Value, 0);

            // Fill head
            //trans.TransactionHead.HeadIID = transaction.HeadIID;
            trans.CompanyID = transaction.CompanyID;
            trans.BranchID = transaction.BranchID;
            trans.ToBranchID = transaction.ToBranchID;
            trans.DocumentTypeID = digitalDocTypeSetting;// sales invoice
            //trans.TransactionNo = transaction.TransactionNo;
            trans.Description = transaction.Description;
            trans.CustomerID = transaction.CustomerID;
            trans.SupplierID = transaction.SupplierID;
            //trans.TransactionHead.DiscountAmount = transaction.DiscountAmount;
            //trans.TransactionHead.DiscountPercentage = transaction.DiscountPercentage;
            trans.TransactionDate = DateTime.Now;
            trans.DueDate = transaction.DueDate;
            trans.DeliveryDate = transaction.DeliveryDate;
            trans.DocumentStatusID = (short)DocumentStatuses.Approved;
            trans.TransactionStatusID = (byte)Framework.Enums.TransactionStatus.New;
            //trans.EntitlementID = transaction.EntitlementID;
            trans.ReferenceHeadID = transaction.HeadIID;
            trans.DeliveryTypeID = transaction.DeliveryTypeID;
            trans.CurrencyID = transaction.CurrencyID;
            trans.DeliveryMethodID = transaction.DeliveryMethodID;
            //trans.IsShipment = transaction.IsShipment;
            //trans.EmployeeID = transaction.EmployeeID;
            //trans.CreatedBy = transaction.CreatedBy;

            // Fill detail
            foreach (var item in transaction.TransactionDetails)
            {
                trans.TransactionDetails.Add(new TransactionDetail()
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

            var result = new Eduegate.Domain.Repository.TransactionRepository().SaveTransactions(trans);
        }        
    }
}

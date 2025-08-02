using Eduegate.Domain;
using Eduegate.Framework;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.Enums;
using Eduegate.Service.Client;
using Eduegate.Service.Client.Logging;
using Eduegate.Service.Client.Schedulers;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Enums.Schedulers;
using Eduegate.Services.Contracts.Logging;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Services.Contracts.Schedulers;
using Eduegate.Services.Contracts.Settings;
using Eduegate.Services.Contracts.Warehouses;
using System;
using System.Collections.Generic;
using Eduegate.Domain.Logging;

namespace Eduegate.TransactionEgine.ClientFactory
{
    public class ClientFactory
    {
        public CallContext _context;

        public ClientFactory(CallContext context = null)
        {
            _context = context;
        }

        public List<AssetTransactionHeadDTO> GetAssetTransactionHeads(DocumentReferenceTypes referenceTypes, Eduegate.Framework.Enums.TransactionStatus transactionStatus)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new FixedAssetServiceClient(_context).GetAssetTransactionHeads(referenceTypes, transactionStatus);
            else
                return new FixedAssetBL(_context).GetAssetTransactionHeads(referenceTypes, transactionStatus);
        }

        public AssetTransactionHeadDTO GetAssetTransactionHeadById(long headID)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new FixedAssetServiceClient(_context).GetAssetTransactionHeadById(headID);
            else
                return new FixedAssetBL(_context).GetAssetTransactionHeadById(headID);
        }

        public bool UpdateAssetTransactionHead(AssetTransactionHeadDTO transactionStatus)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new FixedAssetServiceClient(_context).UpdateAssetTransactionHead(transactionStatus);
            else
                return new FixedAssetBL(_context).UpdateAssetTransactionHead(transactionStatus);
        }

        public Services.Contracts.Catalog.AddProductDTO GetProduct(long productID)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new ProductDetailServiceClient(_context).GetProduct(productID);
            else
                return new ProductDetailBL(_context).GetProduct(productID);
        }

        public bool IsChangeRequestDetailProcessed(long detailID)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new TransactionServiceClient(_context).IsChangeRequestDetailProcessed(detailID);
            else
                return new TransactionBL(_context).IsChangeRequestDetailProcessed(detailID);
        }

        public JobEntryHeadDTO GetMissionByJobID(long headID)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new WarehouseServiceClient(_context).GetMissionByJobID(headID);
            else
                return new WarehouseBL(_context).GetMissionByJobID(headID);
        }

        public JobEntryHeadDTO GetJobByHeadID(long headID)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new WarehouseServiceClient(_context).GetJobByHeadID(headID);
            else
                return new WarehouseBL(_context).GetJobByHeadID(headID);
        }

        public List<JobEntryHeadAccountingDTO> GetAllMissionJobEntryHeads(DocumentReferenceTypes referenceTypes, Eduegate.Framework.Enums.TransactionStatus transactionStatus)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new AccountingTransactionServiceClient(_context).GetAllMissionJobEntryHeads(referenceTypes, transactionStatus);
            else
                return new AccountingTransactionBL(_context).GetAllMissionJobEntryHeads(referenceTypes, transactionStatus);
        }

        public List<AccountTransactionHeadDTO> GetAccountTransactionHeads(DocumentReferenceTypes referenceTypes, TransactionStatus transactionStatus)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new AccountingTransactionServiceClient(_context).GetAccountTransactionHeads(referenceTypes, transactionStatus);
            else
                return new AccountingTransactionBL(_context).GetAccountTransactionHeads(referenceTypes, transactionStatus);
        }

        public CustomerAccountMapsDTO GetCustomerAccountMap(long customerID, int entitlementID)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new AccountingTransactionServiceClient(_context).GetCustomerAccountMap(customerID, entitlementID);
            else
                return new AccountingTransactionBL(_context).GetCustomerAccountMap(customerID, entitlementID);
        }

        public SupplierAccountEntitlmentMapsDTO GetSupplierAccountMap(long supplierID, int entitlementID)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new AccountingTransactionServiceClient(_context).GetSupplierAccountMap(supplierID, entitlementID);
            else
                return new AccountingTransactionBL(_context).GetSupplierAccountMap(supplierID, entitlementID);
        }

        public AccountTransactionHeadDTO GetAccountTransactionHeadById(long HeadID)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new AccountingTransactionServiceClient(_context).GetAccountTransactionHeadById(HeadID);
            else
                return new AccountingTransactionBL(_context).GetAccountTransactionHeadById(HeadID);
        }

        public List<TransactionHeadDTO> GetAllTransaction(DocumentReferenceTypes referenceTypes, TransactionStatus transactionStatus)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new TransactionServiceClient(_context).GetAllTransaction(referenceTypes, transactionStatus);
            else
                return new TransactionBL(_context).GetAllTransaction(referenceTypes, transactionStatus);
        }

        public TransactionHeadDTO GetTransactionDetail(long headID)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new TransactionServiceClient(_context).GetTransactionDetail(headID.ToString());
            else
                return new TransactionBL(_context).GetTransactionDetail(headID);
        }

        public bool CancelTransaction(long headID)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new TransactionServiceClient(_context).CancelTransaction(headID);
            else
                return new TransactionBL(_context).CancelTransaction(headID);
        }

        public bool SaveInvetoryTransactions(List<InvetoryTransactionDTO> dto)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new TransactionServiceClient(_context).SaveInvetoryTransactions(dto);
            else
                return new TransactionBL(_context).SaveInvetoryTransactions(dto);
        }

        public TransactionDTO SaveTransactions(TransactionDTO transaction)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new TransactionServiceClient(_context).SaveTransactions(transaction);
            else
                return new TransactionBL(_context).SaveTransactions(transaction);
        }

        public bool UpdateTransactionHead(TransactionHeadDTO dto)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new TransactionServiceClient(_context).UpdateTransactionHead(dto);
            else
                return new TransactionBL(_context).UpdateTransactionHead(dto);
        }

        public decimal CheckAvailibility(long branchID, long productSKUMapID)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new TransactionServiceClient(_context).CheckAvailibility(branchID, productSKUMapID);
            else
                return new TransactionBL(_context).CheckAvailibility(branchID, productSKUMapID);
        }

        //public List<ProductBundleDTO> GetProductBundleItemDetail(long productSKUMapID)
        //{
        //    if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
        //        return new ProductDetailServiceClient(_context).GetProductBundleItemDetail(productSKUMapID);
        //    else
        //        return new ProductDetailBL(_context).GetProductBundleItemDetail(productSKUMapID);
        //}

        public List<ProductBundleDTO> GetProductBundleItemDetail(long productSKUMapID)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new TransactionServiceClient(_context).GetProductBundleItemDetail(productSKUMapID);
            else
                return new TransactionBL(_context).GetProductBundleItemDetail(productSKUMapID);
        }

        public DocumentTypeDTO GetDocumentTypesByHeadId(long headID)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new ReferenceDataServiceClient(_context).GetDocumentTypesByHeadId(headID);
            else
                return new ReferenceDataBL(_context).GetDocumentTypesByHeadId(headID);
        }

        public List<ProductInventoryDTO> ProcessProductInventory(List<ProductInventoryDTO> dto)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new TransactionServiceClient(_context).ProcessProductInventory(dto);
            else
                return new TransactionBL(_context).ProcessProductInventory(dto);
        }

        public bool HasDigitalProduct(long headID)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new TransactionServiceClient().HasDigitalProduct(headID);
            else
                return new TransactionBL(_context).HasDigitalProduct(headID);
        }

        public UserDTO GetCustomerDetails(long customerID, bool securityInfo)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new AccountServiceClient().GetCustomerDetails(customerID, securityInfo);
            else
                return new AccountBL(_context).GetUserDetailsByCustomerID(customerID, securityInfo);
        }

        public Eduegate.Services.Contracts.Catalog.TransactionDTO GetTransaction(long headIID, bool partialCalulation = false, bool checkClaims = false)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new TransactionServiceClient(_context).GetTransaction(headIID, partialCalulation, checkClaims);
            else
                return new TransactionBL(_context).GetTransaction(headIID, partialCalulation, checkClaims);
        }

        public EmailNotificationDTO SaveEmailData(EmailNotificationDTO emailData)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new NotificationServiceClient(_context).SaveEmailData(emailData);
            else
                return new NotificationBL(_context).SaveEmailData(emailData);
        }

        public EmailNotificationTypeDTO GetEmailNotificationType(Eduegate.Services.Contracts.Enums.EmailNotificationTypes notificationType)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new NotificationServiceClient(_context).GetEmailNotificationType(notificationType);
            else
                return new NotificationBL(_context).GetEmailNotificationType(notificationType);
        }

        public List<SchedulerDTO> GetSchedulerByType(Services.Contracts.Enums.Schedulers.SchedulerTypes schedulerType, string entityID)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new SchedulerServiceClient(_context).GetSchedulerByType(schedulerType, entityID);
            else
                return new SchedulerBL(_context).GetSchedulerByType(schedulerType, entityID);
        }

        public Services.Contracts.DocumentTypeDTO GetDocumentType(long documentTypeID, string type = null)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new MetadataServiceClient(_context).GetDocumentType(documentTypeID, type);
            else
                return new MetadataBL(_context).GetDocumentType(documentTypeID, (SchedulerTypes)Enum.Parse(typeof(SchedulerTypes), type));
        }

        public JobEntryHeadDTO CreateUpdateJobEntry(JobEntryHeadDTO jobEntry)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new WarehouseServiceClient(_context).CreateUpdateJobEntry(jobEntry);
            else
                return new WarehouseBL(_context).CreateUpdateJobEntry(jobEntry);
        }

        public SupplierDTO GetSupplier(string supplierID)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new SupplierServiceClient(_context).GetSupplier(supplierID);
            else
                return new SupplierBL(_context).GetSupplier(long.Parse(supplierID));
        }

        public EntitlementMapDTO GetEntitlementMaps(long supplierID, short supplier)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new MutualServiceClient(_context).GetEntitlementMaps(supplierID, supplier);
            else
                return new MutualBL(_context).GetEntitlementMaps(supplierID, supplier);
        }

        public SettingDTO GetSettingDetail(string settingKey)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new SettingServiceClient(_context).GetSettingDetail(settingKey);
            else
                return new Domain.Setting.SettingBL(_context).GetSettingDetail(settingKey);
        }

        public void SaveActivitiesAsynch(List<ActivityDTO> activities)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                new LoggingServiceClient(_context).SaveActivitiesAsynch(activities);
            else
                new LoggingBL(_context).SaveActivitiesAsynch(activities);
        }

        public List<InvetoryTransactionDTO> GetInvetoryTransactionsByTransactionHeadID(long transactionHeadIID)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new AccountingTransactionServiceClient(_context).GetInvetoryTransactionsByTransactionHeadID(transactionHeadIID);
            else
                return new AccountingTransactionBL(_context).GetInvetoryTransactionsByTransactionHeadID(transactionHeadIID);
        }

        public List<TransactionHeadEntitlementMapDTO> GetTransactionEntitlementByHeadId(long transactionHeadIID)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new AccountingTransactionServiceClient(_context).GetTransactionEntitlementByHeadId(transactionHeadIID);
            else
                return new AccountingTransactionBL(_context).GetTransactionEntitlementByHeadId(transactionHeadIID);
        }

        public AccountDTO GetGLAccountByCode(string code)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new AccountingTransactionServiceClient(_context).GetGLAccountByCode(code);
            else
                return new AccountingTransactionBL(_context).GetGLAccountByCode(code);
        }

        public bool AddPayables(List<PayableDTO> paybles)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new AccountingTransactionServiceClient(_context).AddPayables(paybles);
            else
                return new AccountingTransactionBL(_context).AddPayables(paybles);
        }

        public bool AddReceivables(List<ReceivableDTO> receivables)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new AccountingTransactionServiceClient(_context).AddReceivables(receivables);
            else
                return new AccountingTransactionBL(_context).AddReceivables(receivables);
        }

        public bool UpdateAccountTransactionProcessStatus(TransactionHeadDTO dto)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new AccountingTransactionServiceClient(_context).UpdateAccountTransactionProcessStatus(dto);
            else
                return new AccountingTransactionBL(_context).UpdateAccountTransactionProcessStatus(dto);
        }

        public bool AccountTransactionSync(long accountTransactionHeadIID, long referenceID, int type)
        {
            return new AccountingTransactionBL(_context).AccountTransactionSync(accountTransactionHeadIID, referenceID, type);
        }
        public bool AccountTransMerge(long accountTransactionHeadIID, long referenceID, DateTime transDate, int type)
        {
            return new AccountingTransactionBL(_context).AccountTransMerge(accountTransactionHeadIID, referenceID, transDate, type);
        }
        public long? AutoReceiptAccountTransactionSync(long accountTransactionHeadIID, long referenceID,int type)
        {
            return new AccountingTransactionBL(_context).AutoReceiptAccountTransactionSync(accountTransactionHeadIID, referenceID, type);
        }
        public string AdditionalExpensesTransactionsMap(List<AdditionalExpensesTransactionsMapDTO> additionalExpenseData, long accountTransactionHeadIID, long referenceID, short documentStatus)
        {
            return new AccountingTransactionBL(_context).AdditionalExpensesTransactionsMap(additionalExpenseData, accountTransactionHeadIID, referenceID, documentStatus);
        }
        public List<AdditionalExpensesTransactionsMapDTO> GetAdditionalExpensesTransactions(List<AdditionalExpensesTransactionsMapDTO> additionalExpenseData, long accountTransactionHeadIID, long referenceID)
        {
            return new AccountingTransactionBL(_context).GetAdditionalExpensesTransactions(additionalExpenseData, accountTransactionHeadIID, referenceID);
        }
        public string GetCartDetailByHeadID(long cartID)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new ShoppingCartServiceClient(_context).GetCartDetailByHeadID(cartID);
            else
                return new ShoppingCartBL(_context).GetCartDetailByHeadID(cartID);
        }

        public Services.Contracts.Eduegates.CartDTO GetCartDetailbyIID(long cartID)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new ShoppingCartServiceClient(_context).GetCartDetailbyIID(cartID);
            else
                return new ShoppingCartBL(_context).GetCartDetailbyIID(cartID);
        }

        public VoucherMasterDTO GetVoucher(long cartID)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new AccountingTransactionServiceClient(_context).GetVoucher(cartID);
            else
                return new AccountingTransactionBL(_context).GetVoucher(cartID);
        }

        public bool AddAccountTransactions(List<AccountTransactionsDTO> dto)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new AccountingTransactionServiceClient(_context).AddAccountTransactions(dto);
            else
                return new AccountingTransactionBL(_context).AddAccountTransactions(dto);
        }

        public bool HasClaimAccess(long claimID, long userID)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new UserServiceClient(_context).HasClaimAccess(claimID, userID);
            else
                return new UserServiceBL(_context).HasClaimAccess(claimID, userID);
        }

        public AssetDTO GetAssetById(long assetId)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new FixedAssetServiceClient(_context).GetAssetById(assetId);
            else
                return new FixedAssetBL(_context).GetAssetById(assetId);
        }

        public decimal GetAccumulatedDepreciation(long assetId, int approciation)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new FixedAssetServiceClient(_context).GetAccumulatedDepreciation(assetId, approciation);
            else
                return new FixedAssetBL(_context).GetAccumulatedDepreciation(assetId, approciation);
        }

        public List<ReceivableDTO> GetReceivables(List<long> receivableIds)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new AccountingTransactionServiceClient(_context).GetReceivables(receivableIds);
            else
                return new AccountingTransactionBL(_context).GetReceivables(receivableIds);
        }

        public List<ReceivableDTO> SaveReceivables(List<ReceivableDTO> receivables)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new AccountingTransactionServiceClient(_context).SaveReceivables(receivables);
            else
                return new AccountingTransactionBL(_context).SaveReceivables(receivables);
        }

        public List<PayableDTO> SavePayables(List<PayableDTO> payables)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new AccountingTransactionServiceClient(_context).SavePayables(payables);
            else
                return new AccountingTransactionBL(_context).SavePayables(payables);
        }

        public bool ClearPostedData(long accountingHeadID)
        {
            if (new Domain.Setting.SettingBL().GetSettingValue<Tiers>("Tiers") == Tiers.Multi)
                return new AccountingTransactionServiceClient(_context).ClearPostedData(accountingHeadID);
            else
                return new AccountingTransactionBL(_context).ClearPostedData(accountingHeadID);
        }
    }
}

using System;
using System.Collections.Generic;
using Eduegate.Framework.Enums;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Checkout;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Distributions;
using Eduegate.Services.Contracts.Inventory;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITransactionEngine" in both code and config file together.   
    public interface ITransaction
    {
        List<TransactionHeadDTO> GetAllTransaction(DocumentReferenceTypes referenceTypes, Eduegate.Framework.Enums.TransactionStatus transactionStatus);

        bool SaveTransaction(List<TransactionHeadDTO> dtoList);

        List<ProductInventoryDTO> ProcessProductInventory(List<ProductInventoryDTO> dto);

        List<ProductInventoryDTO> UpdateProductInventory(List<ProductInventoryDTO> dto);

        bool SaveInvetoryTransactions(List<InvetoryTransactionDTO> dto);

        bool UpdateTransactionHead(TransactionHeadDTO dto);

        ProductInventoryDTO GetProductInventoryDetail(ProductInventoryDTO dto);
              
        decimal CheckAvailibility(long branchID, long productSKUMapID);

        bool CheckAvailibility(long? branchID, List<TransactionDetailDTO> transactions);

        bool CheckAvailibilityBundleItem(long branchID, List<TransactionDetailDTO> transactions);

        TransactionHeadDTO GetTransactionDetail(string headId);

        TransactionDTO SaveTransactions(TransactionDTO transaction);

        TransactionDTO GetTransaction(long headIID, bool partialCalulation, bool checkClaims);

        List<TransactionAllocationDTO> GetTransactionAllocations(long transactionDetailID);

        List<KeyValueDTO> GetProductInventorySerialMaps(long productSKUMapID, string searchText, bool serialKeyUsed, int pageSize = default(int), bool checkClaims = false);

        List<KeyValueDTO> GetEntitlementsByHeadId(long HeadID); 

        TransactionSummaryDetailDTO GetTransactionDetails(TransactionSummaryParameterDTO parameter);

        TransactionSummaryDetailDTO GetSupplierTransactionDetails(TransactionSummaryParameterDTO parameter);

        TransactionDTO GetTransactionByJobEntryHeadId(long jobEntryHeadId, bool partialCalulation = false);

        DigitalLimitDTO GetDigitalAmountCustomerCheck(Int64 customerID, long referenceType, decimal cartDigitalAmount, int companyID);

        bool HasDigitalProduct(long headID);

        List<TransactionHeadDTO> GetAllTransactionsBySerialKey(string serialKey, bool IsDigital); 

        bool CancelTransaction(long headID);

        bool ProductSerialCountCheck(string hash, long ProductSKUMapID, string SerialNo, long ProductSerialID);

        OrderDetailDTO GetDeliveryDetails(long Id);

        OrderDetailDTO SaveDeliveryDetails(OrderDetailDTO order);

        List<DeliverySettingDTO> GetDeliveryOptions();

        bool IsChangeRequestDetailProcessed(long detailId);

        List<KeyValueDTO> GetKeysEncryptDecrypt(string keys, bool isEncrypted);

        List<TransactionDetailDTO> GetProductsByPurchaseRequestID(List<long> request_IDs);

        string SaveRFQMappingData(TransactionDTO dto);

        long UpdateQuotation(TransactionDTO transaction);

        List<TransactionDetailDTO> FillQuotationItemList(List<long> quotation_IDs);

        List<KeyValueDTO> FillQuotationsByRFQ(string rfqHeadIID);
        List<KeyValueDTO> FillBidLookUpByRFQ(string rfqHeadIID);

        List<TransactionDetailDTO> FillBidItemList(string bidApprovalIID);

    }
}
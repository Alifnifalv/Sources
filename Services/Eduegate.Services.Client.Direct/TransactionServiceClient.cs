using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Checkout;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Distributions;
using Eduegate.Services.Contracts.Inventory;
using Eduegate.Services;

namespace Eduegate.Service.Client.Direct
{
    public class TransactionServiceClient : ITransaction
    {

        Transaction service = new Transaction();

        public TransactionServiceClient(CallContext callContext = null, Action<string> logger = null)
        {
            service.CallContext = callContext;
        }

        public List<TransactionHeadDTO> GetAllTransaction(DocumentReferenceTypes referenceTypes, Eduegate.Framework.Enums.TransactionStatus transactionStatus)
        {
            return service.GetAllTransaction(referenceTypes, transactionStatus);
        }

        public bool SaveTransaction(List<TransactionHeadDTO> dtoList)
        {
            return service.SaveTransaction(dtoList);
        }

        public List<ProductInventoryDTO> ProcessProductInventory(List<ProductInventoryDTO> dto)
        {
            return service.ProcessProductInventory(dto);
        }

        public List<ProductInventoryDTO> UpdateProductInventory(List<ProductInventoryDTO> dto)
        {
            return service.UpdateProductInventory(dto);
        }

        public bool SaveInvetoryTransactions(List<InvetoryTransactionDTO> dto)
        {
            return service.SaveInvetoryTransactions(dto);
        }

        public bool UpdateTransactionHead(TransactionHeadDTO dto)
        {
            return service.UpdateTransactionHead(dto);
        }

        public ProductInventoryDTO GetProductInventoryDetail(ProductInventoryDTO dto)
        {
            return service.GetProductInventoryDetail(dto);
        }

        public decimal CheckAvailibility(long branchID, long productSKUMapID)
        {
            return service.CheckAvailibility(branchID, productSKUMapID);
        }

        public bool CheckAvailibility(long? branchID, List<TransactionDetailDTO> transactions)
        {
            return service.CheckAvailibility(branchID, transactions);
        }
        public TransactionDetailDTO CheckAvailibilityBundleItem(long branchID, List<TransactionDetailDTO> transactions)
        {
            return service.CheckAvailibilityBundleItem(branchID, transactions);
        }


        public TransactionHeadDTO GetTransactionDetail(string headId)
        {
            return service.GetTransactionDetail(headId);
        }

        public TransactionDTO SaveTransactions(TransactionDTO transaction)
        {
            return service.SaveTransactions(transaction);
        }

        public TransactionDTO GetTransaction(long headIID, bool partialCalulation = false, bool checkClaims = false)
        {
            return service.GetTransaction(headIID, partialCalulation, checkClaims);
        }

        public List<TransactionAllocationDTO> GetTransactionAllocations(long transactionDetailID)
        {
            return service.GetTransactionAllocations(transactionDetailID);
        }
        public List<KeyValueDTO> GetProductInventorySerialMaps(long productSKUMapID, string searchText, bool serialKeyUsed, int pageSize = default(int), bool checkClaims = false)
        {
            return service.GetProductInventorySerialMaps(productSKUMapID, searchText, serialKeyUsed, pageSize , checkClaims);
        }

        public List<KeyValueDTO> GetEntitlementsByHeadId(long HeadID)
        {
            return service.GetEntitlementsByHeadId(HeadID);
        }

        public TransactionSummaryDetailDTO GetTransactionDetails(TransactionSummaryParameterDTO parameter)
        {
            return service.GetTransactionDetails(parameter);
        }

        public TransactionDTO GetTransactionByJobEntryHeadId(long jobEntryHeadId, bool partialCalulation = false)
        {
            return service.GetTransactionByJobEntryHeadId(jobEntryHeadId, partialCalulation);
        }
        public DigitalLimitDTO GetDigitalAmountCustomerCheck(Int64 customerID, long referenceType, decimal cartDigitalAmount, int companyID)
        {
            return service.GetDigitalAmountCustomerCheck(customerID, referenceType, cartDigitalAmount, companyID);
        }

        public bool HasDigitalProduct(long headID)
        {
            return service.HasDigitalProduct(headID);
        }

        public List<TransactionHeadDTO> GetAllTransactionsBySerialKey (string serialKey,bool IsDigital)
        {
            return service.GetAllTransactionsBySerialKey(serialKey, IsDigital);
        }         

        public bool CancelTransaction(long headID)
        {
            return service.CancelTransaction(headID);
        }

        public bool ProductSerialCountCheck(string hash, long ProductSKUMapID, string SerialNo, long ProductSerialID)
        {
            return service.ProductSerialCountCheck(hash, ProductSKUMapID, SerialNo, ProductSerialID);
        }

        public OrderDetailDTO GetDeliveryDetails(long Id)
        {
            return service.GetDeliveryDetails(Id);
        }

        public OrderDetailDTO SaveDeliveryDetails(OrderDetailDTO order)  
        {
            return service.SaveDeliveryDetails(order);
        }

        public List<DeliverySettingDTO> GetDeliveryOptions() 
        {
            return service.GetDeliveryOptions();
        }

        public bool IsChangeRequestDetailProcessed(long detailId)
        {
            return service.IsChangeRequestDetailProcessed(detailId);
        }

        public List<KeyValueDTO> GetKeysEncryptDecrypt(string keys, bool isEncrypted)
        {
            return service.GetKeysEncryptDecrypt(keys,isEncrypted);
        }

        public TransactionSummaryDetailDTO GetSupplierTransactionDetails(TransactionSummaryParameterDTO parameter)
        {
            return service.GetSupplierTransactionDetails(parameter);
        }
        public List<ProductBundleDTO> GetProductBundleItemDetail(long productSKUMapID)
        {
            return service.GetProductBundleItemDetail(productSKUMapID);
        }

        public List<TransactionDetailDTO> GetProductsByPurchaseRequestID(List<long> request_IDs)
        {
            return service.GetProductsByPurchaseRequestID(request_IDs);
        }

        public string SaveRFQMappingData(TransactionDTO dto)
        {
            return service.SaveRFQMappingData(dto);
        } 
        
        public long UpdateQuotation(TransactionDTO dto)
        {
            return service.UpdateQuotation(dto);
        }

        public List<TransactionDetailDTO> FillQuotationItemList(List<long> quotation_IDs)
        {
            return service.FillQuotationItemList(quotation_IDs);
        }  
        
        public List<TransactionDetailDTO> FillBidItemList(string bidApprovalIID)
        {
            return service.FillBidItemList(bidApprovalIID);
        }
        
        public List<KeyValueDTO> FillQuotationsByRFQ(string rfqHeadIID)
        {
            return service.FillQuotationsByRFQ(rfqHeadIID);
        }      
        
        public List<KeyValueDTO> FillBidLookUpByRFQ(string rfqHeadIID)
        {
            return service.FillBidLookUpByRFQ(rfqHeadIID);
        }
    }
}

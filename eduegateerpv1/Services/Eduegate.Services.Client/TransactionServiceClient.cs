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
namespace Eduegate.Service.Client
{
    public class TransactionServiceClient : BaseClient, ITransaction
    {

        private static string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string Service = string.Concat(ServiceHost, Eduegate.Framework.Helper.Constants.TRANSACTION_SERVICE_NAME);

        public TransactionServiceClient(CallContext callContext = null, Action<string> logger = null)
            : base(callContext, logger)
        {

        }

        public List<TransactionHeadDTO> GetAllTransaction(DocumentReferenceTypes referenceTypes, Eduegate.Framework.Enums.TransactionStatus transactionStatus)
        {
            string uri = string.Concat(Service + "\\GetAllTransaction?referenceTypes=" + referenceTypes + "&transactionStatus=" + transactionStatus);
            return ServiceHelper.HttpGetRequest<List<TransactionHeadDTO>>(uri);
        }

        public bool SaveTransaction(List<TransactionHeadDTO> dtoList)
        {
            throw new NotImplementedException();
        }

        public List<ProductInventoryDTO> ProcessProductInventory(List<ProductInventoryDTO> dto)
        {
            string uri = string.Concat(Service + "\\ProcessProductInventory");
            return ServiceHelper.HttpPostGetRequest<List<ProductInventoryDTO>>(uri, dto);
        }

        public List<ProductInventoryDTO> UpdateProductInventory(List<ProductInventoryDTO> dto)
        {
            string uri = string.Concat(Service + "\\UpdateProductInventory");
            return ServiceHelper.HttpPostGetRequest<List<ProductInventoryDTO>>(uri, dto);
        }

        public bool SaveInvetoryTransactions(List<InvetoryTransactionDTO> dto)
        {
            string uri = string.Concat(Service + "\\SaveInvetoryTransactions");
            return ServiceHelper.HttpPostRequest(uri, dto) == "true" ? true : false;
        }

        public bool UpdateTransactionHead(TransactionHeadDTO dto)
        {
            string uri = string.Concat(Service + "\\UpdateTransactionHead");
            return ServiceHelper.HttpPostRequest(uri, dto) == "true" ? true : false;
        }

        public ProductInventoryDTO GetProductInventoryDetail(ProductInventoryDTO dto)
        {
            throw new NotImplementedException();
        }

        public decimal CheckAvailibility(long branchID, long productSKUMapID)
        {
            string uri = string.Concat(Service + "\\CheckAvailibility?branchID=" + branchID + "&productSKUMapID=" + productSKUMapID);
            return ServiceHelper.HttpGetRequest<decimal>(uri);
        }
        public bool CheckAvailibility(long? branchID, List<TransactionDetailDTO> transactions)
        {
            string uri = string.Concat(Service + "\\CheckAvailibility?branchID=" + branchID + "&transactions=" + transactions);
            return ServiceHelper.HttpGetRequest<bool>(uri);
        }

        public bool CheckAvailibilityBundleItem(long branchID, List<TransactionDetailDTO> transactions)
        {
            string uri = string.Concat(Service + "\\CheckAvailibilityBundleItem?branchID=" + branchID + "&transactions=" + transactions);
            return ServiceHelper.HttpGetRequest<bool>(uri);
        }

        public TransactionHeadDTO GetTransactionDetail(string headId)
        {
            string uri = string.Concat(Service + "\\GetTransactionDetail?headId=" + headId);
            return ServiceHelper.HttpGetRequest<TransactionHeadDTO>(uri);
        }

        public TransactionDTO SaveTransactions(TransactionDTO transaction)
        {
            var uri = string.Format("{0}/SaveTransactions", Service);
            return ServiceHelper.HttpPostGetRequest<TransactionDTO>(uri, transaction, _callContext, _logger);
        }

        public TransactionDTO GetTransaction(long headIID, bool partialCalulation = false, bool checkClaims = false)
        {
            var uri = string.Format("{0}/GetTransaction?headIID={1}&partialCalulation={2}&checkClaims={3}", Service, headIID, partialCalulation, checkClaims);
            return ServiceHelper.HttpGetRequest<TransactionDTO>(uri, _callContext, _logger);
        }

        public List<TransactionAllocationDTO> GetTransactionAllocations(long transactionDetailID)
        {
            var uri = string.Format("{0}/GetTransactionAllocations?transactionDetailID={1}", Service, transactionDetailID);
            return ServiceHelper.HttpGetRequest<List<TransactionAllocationDTO>>(uri);
        }
        public List<KeyValueDTO> GetProductInventorySerialMaps(long productSKUMapID, string searchText, bool serialKeyUsed, int pageSize = default(int), bool checkClaims = false)
        {
            var uri = string.Format("{0}/GetProductInventorySerialMaps?productSKUMapID={1}&searchText={2}&serialKeyUsed={3}&pageSize={4}&checkClaims={5}", Service, productSKUMapID, searchText, serialKeyUsed, pageSize, checkClaims);
            return ServiceHelper.HttpGetRequest<List<KeyValueDTO>>(uri, _callContext);
        }

        public List<KeyValueDTO> GetEntitlementsByHeadId(long HeadID)
        {
            var uri = string.Format("{0}/GetEntitlementsByHeadId?HeadId={1}", Service, HeadID);
            return ServiceHelper.HttpGetRequest<List<KeyValueDTO>>(uri);
        }

        public TransactionSummaryDetailDTO GetTransactionDetails(TransactionSummaryParameterDTO parameter)
        {
            var uri = string.Format("{0}/GetTransactionDetails", Service);
            var result = ServiceHelper.HttpPostRequest<TransactionSummaryParameterDTO>(uri, parameter, _callContext);
            return JsonConvert.DeserializeObject<TransactionSummaryDetailDTO>(result);
        }

        public TransactionDTO GetTransactionByJobEntryHeadId(long jobEntryHeadId, bool partialCalulation = false)
        {
            var uri = string.Format("{0}/GetTransactionByJobEntryHeadId?jobEntryHeadId={1}&partialCalulation={2}", Service, jobEntryHeadId, partialCalulation);
            return ServiceHelper.HttpGetRequest<TransactionDTO>(uri, _callContext, _logger);
        }
        public DigitalLimitDTO GetDigitalAmountCustomerCheck(Int64 customerID, long referenceType, decimal cartDigitalAmount, int companyID)
        {
            var uri = string.Format("{0}/GetDigitalAmountCustomerCheck?customerID={1}&referenceType={2}&cartDigitalAmount={3}&companyID={4}", Service, customerID, referenceType, cartDigitalAmount, companyID);
            return ServiceHelper.HttpGetRequest<DigitalLimitDTO>(uri);
        }

        public bool HasDigitalProduct(long headID)
        {
            var uri = string.Format("{0}/HasDigitalProduct?headID={1}", Service, headID);
            return ServiceHelper.HttpGetRequest<bool>(uri);
        }

        public List<TransactionHeadDTO> GetAllTransactionsBySerialKey(string serialKey, bool IsDigital)
        {
            string uri = string.Format("{0}/GetAllTransactionsBySerialKey?serialKey={1}&IsDigital={2}", Service, serialKey, IsDigital);
            return ServiceHelper.HttpGetRequest<List<TransactionHeadDTO>>(uri, _callContext, _logger);
        }

        public bool CancelTransaction(long headID)
        {
            var uri = string.Format("{0}/CancelTransaction?headID={1}", Service, headID);
            return ServiceHelper.HttpGetRequest<bool>(uri);
        }

        public bool ProductSerialCountCheck(string hash, long ProductSKUMapID, string SerialNo, long ProductSerialID)
        {
            var uri = string.Format("{0}/ProductSerialCountCheck?hash={1}&ProductSKUMapID={2}&SerialNo={3}&ProductSerialID={4}", Service, hash, ProductSKUMapID, SerialNo, ProductSerialID);
            return ServiceHelper.HttpGetRequest<bool>(uri);
        }

        public OrderDetailDTO GetDeliveryDetails(long Id)
        {
            var uri = string.Format("{0}/GetDeliveryDetails?Id={1}", Service, Id);
            return ServiceHelper.HttpGetRequest<OrderDetailDTO>(uri);
        }

        public OrderDetailDTO SaveDeliveryDetails(OrderDetailDTO order)
        {
            var uri = string.Format("{0}/SaveDeliveryDetails", Service);
            return ServiceHelper.HttpPostGetRequest<OrderDetailDTO>(uri, order, _callContext, _logger);
        }

        public List<DeliverySettingDTO> GetDeliveryOptions()
        {
            string uri = string.Format("{0}/GetDeliveryOptions", Service);
            return ServiceHelper.HttpGetRequest<List<DeliverySettingDTO>>(uri, _callContext, _logger);
        }

        public bool IsChangeRequestDetailProcessed(long detailId)
        {
            string uri = string.Format("{0}/IsChangeRequestDetailProcessed?detailId={1}", Service, detailId);
            return ServiceHelper.HttpGetRequest<bool>(uri, _callContext, _logger);
        }

        public List<KeyValueDTO> GetKeysEncryptDecrypt(string keys, bool isEncrypted)
        {
            var uri = string.Format("{0}/GetKeysEncryptDecrypt?keys={1}&isEncrypted={2}", Service, keys, isEncrypted);
            return ServiceHelper.HttpGetRequest<List<KeyValueDTO>>(uri);
        }

        public TransactionSummaryDetailDTO GetSupplierTransactionDetails(TransactionSummaryParameterDTO parameter)
        {
            var uri = string.Format("{0}/GetSupplierTransactionDetails", Service);
            var result = ServiceHelper.HttpPostRequest<TransactionSummaryParameterDTO>(uri, parameter, _callContext);
            return JsonConvert.DeserializeObject<TransactionSummaryDetailDTO>(result);
        }

        List<TransactionHeadDTO> ITransaction.GetAllTransaction(DocumentReferenceTypes referenceTypes, TransactionStatus transactionStatus)
        {
            throw new NotImplementedException();
        }

        bool ITransaction.SaveTransaction(List<TransactionHeadDTO> dtoList)
        {
            throw new NotImplementedException();
        }

        List<ProductInventoryDTO> ITransaction.ProcessProductInventory(List<ProductInventoryDTO> dto)
        {
            throw new NotImplementedException();
        }

        List<ProductInventoryDTO> ITransaction.UpdateProductInventory(List<ProductInventoryDTO> dto)
        {
            throw new NotImplementedException();
        }

        bool ITransaction.SaveInvetoryTransactions(List<InvetoryTransactionDTO> dto)
        {
            throw new NotImplementedException();
        }

        bool ITransaction.UpdateTransactionHead(TransactionHeadDTO dto)
        {
            throw new NotImplementedException();
        }

        ProductInventoryDTO ITransaction.GetProductInventoryDetail(ProductInventoryDTO dto)
        {
            throw new NotImplementedException();
        }
        //bool ITransaction.CheckAvailibilityBundleItem(long branchID, List<TransactionDetailDTO> transactions)
        //{
        //    throw new NotImplementedException();
        //}

        decimal ITransaction.CheckAvailibility(long branchID, long productSKUMapID)
        {
            throw new NotImplementedException();
        }


        bool ITransaction.CheckAvailibility(long? branchID, List<TransactionDetailDTO> transactions)
        {
            throw new NotImplementedException();
        }

         bool ITransaction.CheckAvailibilityBundleItem(long branchID, List<TransactionDetailDTO> transactions)
        {
            throw new NotImplementedException();
        }

        TransactionHeadDTO ITransaction.GetTransactionDetail(string headId)
        {
            throw new NotImplementedException();
        }

        TransactionDTO ITransaction.SaveTransactions(TransactionDTO transaction)
        {
            throw new NotImplementedException();
        }

        TransactionDTO ITransaction.GetTransaction(long headIID, bool partialCalulation, bool checkClaims)
        {
            throw new NotImplementedException();
        }

        List<TransactionAllocationDTO> ITransaction.GetTransactionAllocations(long transactionDetailID)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ITransaction.GetProductInventorySerialMaps(long productSKUMapID, string searchText, bool serialKeyUsed, int pageSize, bool checkClaims)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ITransaction.GetEntitlementsByHeadId(long HeadID)
        {
            throw new NotImplementedException();
        }

        TransactionSummaryDetailDTO ITransaction.GetTransactionDetails(TransactionSummaryParameterDTO parameter)
        {
            throw new NotImplementedException();
        }

        TransactionSummaryDetailDTO ITransaction.GetSupplierTransactionDetails(TransactionSummaryParameterDTO parameter)
        {
            throw new NotImplementedException();
        }

        TransactionDTO ITransaction.GetTransactionByJobEntryHeadId(long jobEntryHeadId, bool partialCalulation)
        {
            throw new NotImplementedException();
        }

        DigitalLimitDTO ITransaction.GetDigitalAmountCustomerCheck(long customerID, long referenceType, decimal cartDigitalAmount, int companyID)
        {
            throw new NotImplementedException();
        }

        bool ITransaction.HasDigitalProduct(long headID)
        {
            throw new NotImplementedException();
        }

        List<TransactionHeadDTO> ITransaction.GetAllTransactionsBySerialKey(string serialKey, bool IsDigital)
        {
            throw new NotImplementedException();
        }

        bool ITransaction.CancelTransaction(long headID)
        {
            throw new NotImplementedException();
        }

        bool ITransaction.ProductSerialCountCheck(string hash, long ProductSKUMapID, string SerialNo, long ProductSerialID)
        {
            throw new NotImplementedException();
        }

        OrderDetailDTO ITransaction.GetDeliveryDetails(long Id)
        {
            throw new NotImplementedException();
        }

        OrderDetailDTO ITransaction.SaveDeliveryDetails(OrderDetailDTO order)
        {
            throw new NotImplementedException();
        }

        List<DeliverySettingDTO> ITransaction.GetDeliveryOptions()
        {
            throw new NotImplementedException();
        }

        bool ITransaction.IsChangeRequestDetailProcessed(long detailId)
        {
            throw new NotImplementedException();
        }

        List<KeyValueDTO> ITransaction.GetKeysEncryptDecrypt(string keys, bool isEncrypted)
        {
            throw new NotImplementedException();
        }
        public List<ProductBundleDTO> GetProductBundleItemDetail(long productSKUMapID)
        {
            var uri = string.Concat(Service, "GetProductBundleItemDetail?productSKUMapID=" + productSKUMapID);
            return ServiceHelper.HttpGetRequest<List<ProductBundleDTO>>(uri, _callContext, _logger);
        }

        public List<TransactionDetailDTO> GetProductsByPurchaseRequestID(List<long> request_IDs) 
        {
            throw new NotImplementedException();
        }

        public string SaveRFQMappingData(TransactionDTO dto)
        {
            throw new NotImplementedException();
        }

        public long UpdateQuotation(TransactionDTO dto)
        {
            throw new NotImplementedException();
        }

        public List<TransactionDetailDTO> FillQuotationItemList(List<long> quotation_IDs)
        {
            throw new NotImplementedException();
        }    
        
        public List<TransactionDetailDTO> FillBidItemList(string bidApprovalIID) 
        {
            throw new NotImplementedException();
        }  
        
        public List<KeyValueDTO> FillQuotationsByRFQ(string rfqHeadIID)
        {
            throw new NotImplementedException();
        }  
        
        public List<KeyValueDTO> FillBidLookUpByRFQ(string rfqHeadIID)
        {
            throw new NotImplementedException();
        }
    }
}

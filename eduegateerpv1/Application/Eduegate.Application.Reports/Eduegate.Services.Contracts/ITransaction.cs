using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using Eduegate.Framework.Enums;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Checkout;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Distributions;
using Eduegate.Services.Contracts.Inventory;
using Eduegate.Domain.Entity.Models;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITransactionEngine" in both code and config file together.
    [ServiceContract]
    public interface ITransaction
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAllTransaction?referenceTypes={referenceTypes}&transactionStatus={transactionStatus}")]
        List<TransactionHeadDTO> GetAllTransaction(DocumentReferenceTypes referenceTypes, Eduegate.Framework.Enums.TransactionStatus transactionStatus);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveTransaction")]
        bool SaveTransaction(List<TransactionHeadDTO> dtoList);


        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "ProcessProductInventory")]
        List<ProductInventoryDTO> ProcessProductInventory(List<ProductInventoryDTO> dto);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdateProductInventory")]
        List<ProductInventoryDTO> UpdateProductInventory(List<ProductInventoryDTO> dto);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveInvetoryTransactions")]
        bool SaveInvetoryTransactions(List<InvetoryTransactionDTO> dto);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdateTransactionHead")]
        bool UpdateTransactionHead(TransactionHeadDTO dto);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductInventoryDetail")]
        ProductInventoryDTO GetProductInventoryDetail(ProductInventoryDTO dto);
              
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "CheckAvailibility?branchID={branchID}&productSKUMapID={productSKUMapID}")]
        decimal CheckAvailibility(long branchID, long productSKUMapID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "CheckAvailibility?branchID={branchID}&transactions={transactions}")]
        bool CheckAvailibility(long? branchID, List<TransactionDetailDTO> transactions);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "CheckAvailibilityBundleItem?branchID={branchID}&transactions={transactions}")]
        bool CheckAvailibilityBundleItem(long branchID, List<TransactionDetailDTO> transactions);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetTransactionDetail?headId={headId}")]
        TransactionHeadDTO GetTransactionDetail(string headId);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveTransactions")]
        TransactionDTO SaveTransactions(TransactionDTO transaction);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetTransaction?headIID={headIID}&partialCalulation={partialCalulation}&checkClaims={checkClaims}")]
        TransactionDTO GetTransaction(long headIID, bool partialCalulation, bool checkClaims);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetTransactionAllocations?transactionDetailID={transactionDetailID}")]
        List<TransactionAllocationDTO> GetTransactionAllocations(long transactionDetailID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetProductInventorySerialMaps?productSKUMapID={productSKUMapID}&searchText={searchText}&serialKeyUsed={serialKeyUsed}&pageSize={pageSize}&checkClaims={checkClaims}")]
        List<KeyValueDTO> GetProductInventorySerialMaps(long productSKUMapID, string searchText, bool serialKeyUsed, int pageSize = default(int), bool checkClaims = false);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetEntitlementsByHeadId?HeadID={HeadID}")]
        List<KeyValueDTO> GetEntitlementsByHeadId(long HeadID); 

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetTransactionDetails")]
        TransactionSummaryDetailDTO GetTransactionDetails(TransactionSummaryParameterDTO parameter);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetSupplierTransactionDetails")]
        TransactionSummaryDetailDTO GetSupplierTransactionDetails(TransactionSummaryParameterDTO parameter);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetTransactionByJobEntryHeadId?jobEntryHeadId={jobEntryHeadId}&partialCalulation={partialCalulation}")]
        TransactionDTO GetTransactionByJobEntryHeadId(long jobEntryHeadId, bool partialCalulation = false);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetDigitalAmountCustomerCheck?customerID={customerID}&referenceType={referenceType}&cartDigitalAmount={cartDigitalAmount}&companyID={companyID}")]
        DigitalLimitDTO GetDigitalAmountCustomerCheck(Int64 customerID, long referenceType, decimal cartDigitalAmount, int companyID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "HasDigitalProduct?headID={headID}")]
        bool HasDigitalProduct(long headID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAllTransactionsBySerialKey?serialKey={serialKey}&IsDigital={IsDigital}")]
        List<TransactionHeadDTO> GetAllTransactionsBySerialKey(string serialKey,bool IsDigital); 

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "CancelTransaction?headID={headID}")]
        bool CancelTransaction(long headID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "ProductSerialCountCheck?hash={hash}&ProductSKUMapID={ProductSKUMapID}&SerialNo={SerialNo}&ProductSerialID={ProductSerialID}")]
        bool ProductSerialCountCheck(string hash, long ProductSKUMapID, string SerialNo, long ProductSerialID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetDeliveryDetails?Id={Id}")]
        OrderDetailDTO GetDeliveryDetails(long Id);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveDeliveryDetails")]
        OrderDetailDTO SaveDeliveryDetails(OrderDetailDTO order);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetDeliveryOptions")]
        List<DeliverySettingDTO> GetDeliveryOptions();



        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "IsChangeRequestDetailProcessed?detailId={detailId}")]
        bool IsChangeRequestDetailProcessed(long detailId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetKeysEncryptDecrypt?keys={keys}&isEncrypted={isEncrypted}")]
        List<KeyValueDTO> GetKeysEncryptDecrypt(string keys, bool isEncrypted);

    }
}

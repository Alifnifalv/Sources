using System.ServiceModel;
using Eduegate.Domain;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Checkout;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Distributions;
using Eduegate.Services.Contracts.Inventory;
using Eduegate.Domain.Setting;

namespace Eduegate.Services
{
    public class Transaction : BaseService, ITransaction
    {
        public List<TransactionHeadDTO> GetAllTransaction(DocumentReferenceTypes referenceTypes, Eduegate.Framework.Enums.TransactionStatus transactionStatus)
        {
            return new TransactionBL(CallContext).GetAllTransaction(referenceTypes, transactionStatus);
        }

        public bool SaveTransaction(List<TransactionHeadDTO> dtoList)
        {
            return new TransactionBL(CallContext).SaveTransaction(dtoList);
        }

        public List<ProductInventoryDTO> ProcessProductInventory(List<ProductInventoryDTO> dto)
        {
            return new TransactionBL(CallContext).ProcessProductInventory(dto);
        }

        public List<ProductInventoryDTO> UpdateProductInventory(List<ProductInventoryDTO> dto)
        {
            return new TransactionBL(CallContext).UpdateProductInventory(dto);
        }

        public bool SaveInvetoryTransactions(List<InvetoryTransactionDTO> dto)
        {
            return new TransactionBL(CallContext).SaveInvetoryTransactions(dto);
        }

        public bool UpdateTransactionHead(TransactionHeadDTO dto)
        {
            return new TransactionBL(CallContext).UpdateTransactionHead(dto);
        }

        public ProductInventoryDTO GetProductInventoryDetail(ProductInventoryDTO dto)
        {
            return new TransactionBL(CallContext).GetProductInventoryDetail(dto);
        }

    
        public decimal CheckAvailibility(long branchID, long productSKUMapID)
        {
            return new TransactionBL(CallContext).CheckAvailibility(branchID, productSKUMapID);
        }

        public bool CheckAvailibility(long? branchID, List<TransactionDetailDTO> transactions)
        {
            return new TransactionBL(CallContext).CheckAvailibility(branchID, transactions);
        }
        public TransactionDetailDTO CheckAvailibilityBundleItem(long branchID, List<TransactionDetailDTO> transactions)
        {
            return new TransactionBL(CallContext).CheckAvailibilityBundleItem(branchID, transactions);
        }
        public TransactionHeadDTO GetTransactionDetail(string headId)
        {
            return new TransactionBL(CallContext).GetTransactionDetail(Convert.ToInt32(headId));
        }

        public TransactionDTO SaveTransactions(TransactionDTO transaction)
        {
            try
            {
                var dto = new TransactionBL(CallContext).SaveTransactions(transaction);
                var processing = new SettingBL().GetSettingValue("TransactionProcessing", TransactionProcessing.Immediate);

                if (!dto.TransactionHead.IsError && processing == TransactionProcessing.Immediate)
                {
                    new TransactionEngineCore.Transaction(null).StartProcess(0, 0, dto.TransactionHead.HeadIID);
                }

                return dto;
            }

            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<Transaction>.Fatal(ex.Message.ToString(), ex);
                throw new FaultException(ex.Message);
            }
        }

        public TransactionDTO GetTransaction(long headIID, bool partialCalulation, bool checkClaims)
        {
            try
            {
                return new TransactionBL(CallContext).GetTransaction(headIID, partialCalulation, checkClaims);
            }

            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<Transaction>.Fatal(ex.Message.ToString(), ex);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<TransactionAllocationDTO> GetTransactionAllocations(long transactionDetailID)
        {
            return new TransactionBL(CallContext).GetTransactionAllocations(transactionDetailID);
        }

        public List<KeyValueDTO> GetProductInventorySerialMaps(long productSKUMapID, string searchText, bool serialKeyUsed, int pageSize = default(int),bool checkClaims = false)
        {
            try
            {
                if (pageSize == default(int))
                {
                    pageSize = Convert.ToInt32(new Domain.Setting.SettingBL(null).GetSettingValue<string>(Constants.MAX_FETCH_COUNT));
                }
                return new TransactionBL(CallContext).GetProductInventorySerialMaps(productSKUMapID, searchText, Convert.ToInt32(pageSize), serialKeyUsed, checkClaims);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<Transaction>.Fatal(ex.Message.ToString(), ex);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public TransactionSummaryDetailDTO GetTransactionDetails(TransactionSummaryParameterDTO parameter)
        {
            return new TransactionBL(CallContext).GetTransactionDetails(parameter.DocuementTypeID, parameter.DateFrom, parameter.DateTo);
        }

        public TransactionSummaryDetailDTO GetSupplierTransactionDetails(TransactionSummaryParameterDTO parameter)
        {
            return new TransactionBL(CallContext).GetSupplierTransactionDetails(parameter.LoginID, parameter.DocuementTypeID, parameter.DateFrom, parameter.DateTo);
        }

        public List<KeyValueDTO> GetEntitlementsByHeadId(long HeadID)
        {
            return new TransactionBL(CallContext).GetEntitlementsByHeadId(HeadID);
        }

        public TransactionDTO GetTransactionByJobEntryHeadId(long jobEntryHeadId, bool partialCalulation = false)
        {
            try
            {
                return new TransactionBL(CallContext).GetTransactionByJobEntryHeadId(jobEntryHeadId, partialCalulation);
            }

            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<Transaction>.Fatal(ex.Message.ToString(), ex);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }


        public DigitalLimitDTO GetDigitalAmountCustomerCheck(Int64 customerID, long referenceType, decimal cartDigitalAmount, int companyID)
        {
            try
            {
                return new TransactionBL(CallContext).DigitalAmountCustomerCheck(customerID, referenceType, cartDigitalAmount, companyID);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<Transaction>.Fatal(ex.Message.ToString(), ex);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool HasDigitalProduct(long headID)
        {
            try
            {
                return new TransactionBL(CallContext).HasDigitalProduct(headID);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<Transaction>.Fatal(ex.Message.ToString(), ex);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
         
        public List<TransactionHeadDTO> GetAllTransactionsBySerialKey(string serialKey,bool IsDigital)
        {
            try
            {
                return new TransactionBL(CallContext).GetAllTransactionsBySerialKey(serialKey, IsDigital);
            }

            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<Transaction>.Fatal(ex.Message.ToString(), ex);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool CancelTransaction(long headID)
        {
            try
            {
                return new TransactionBL(CallContext).CancelTransaction(headID);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<Transaction>.Fatal(ex.Message.ToString(), ex);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool ProductSerialCountCheck(string hash, long ProductSKUMapID, string SerialNo, long ProductSerialID)
        {
            try
            {
                return new TransactionBL(CallContext).ProductSerialCountCheck(hash,ProductSKUMapID, SerialNo, ProductSerialID);
            }

            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<Transaction>.Fatal(ex.Message.ToString(), ex);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
    
        public OrderDetailDTO GetDeliveryDetails(long Id)
        {
            try
            {
                return new TransactionBL(CallContext).GetDeliveryDetails(Id);              
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<OrderDetailDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }

        }

        public OrderDetailDTO SaveDeliveryDetails(OrderDetailDTO order)
        { 
            try
            {
                OrderDetailDTO orderDetail = new TransactionBL(CallContext).SaveDeliveryDetails(order);
                Eduegate.Logger.LogHelper<OrderDetailDTO>.Info("Service Result: " + orderDetail);
                return orderDetail;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<OrderDetailDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<DeliverySettingDTO> GetDeliveryOptions() 
        { 
            return new TransactionBL(CallContext).GetDeliveryOptions();
        }

        public bool IsChangeRequestDetailProcessed(long detailId)
        {
            try
            {
                return new TransactionBL(CallContext).IsChangeRequestDetailProcessed(detailId);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Transaction>.Fatal(exception.Message.ToString(), exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<KeyValueDTO> GetKeysEncryptDecrypt(string keys, bool isEncrypted)
        {
            try
            {
                return new TransactionBL(CallContext).GetKeysEncryptDecrypt(keys, isEncrypted);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<TransactionBL>.Fatal(ex.Message.ToString(), ex);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<ProductBundleDTO> GetProductBundleItemDetail(long productSKUMapID)
        {
            try
            {

                var transactionBL = new TransactionBL(CallContext);
                var bundleItems = transactionBL.GetProductBundleItemDetail(productSKUMapID);
                Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + bundleItems.Count.ToString());
                return bundleItems;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductBundleDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }


        #region RFQ -- Start
        public List<TransactionDetailDTO> GetProductsByPurchaseRequestID(List<long> request_IDs)
        {
            return new TransactionBL(CallContext).GetProductsByPurchaseRequestID(request_IDs);
        }

        public string SaveRFQMappingData(TransactionDTO dto)
        {
            return new TransactionBL(CallContext).SaveRFQMappingData(dto);
        }  
        
        public long UpdateQuotation(TransactionDTO dto)
        {
            return new TransactionBL(CallContext).UpdateQuotation(dto);
        }

        //Comapare screen
        public List<TransactionDetailDTO> FillQuotationItemList(List<long> quotation_IDs)
        {
            return new TransactionBL(CallContext).FillQuotationItemList(quotation_IDs);
        } 
        
        public List<TransactionDetailDTO> FillBidItemList(string bidApprovalIID) 
        {
            return new TransactionBL(CallContext).FillBidItemList(bidApprovalIID);
        }       
        
        public List<KeyValueDTO> FillQuotationsByRFQ(string rfqHeadIID)
        {
            return new TransactionBL(CallContext).FillQuotationsByRFQ(rfqHeadIID);
        }
        public List<KeyValueDTO> FillBidLookUpByRFQ(string rfqHeadIID)
        {
            return new TransactionBL(CallContext).FillBidLookUpByRFQ(rfqHeadIID);
        }
        #endregion

    }
}

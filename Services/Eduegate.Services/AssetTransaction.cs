using System.ServiceModel;
using Eduegate.Domain;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Services;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Domain.Setting;
using Eduegate.Domain.Accounts.Assets;
using Eduegate.Services.Contracts.Accounts.Assets;
using Eduegate.Domain.Mappers.Accounts.Assets;

namespace Eduegate.Services
{
    public class AssetTransaction : BaseService
    {
        //public List<AssetCategoryDTO> GetAssetCategories()
        //{
        //    return new FixedAssetBL(CallContext).GetAssetCategories();
        //}

        //public List<KeyValueDTO> GetAssetCodes()
        //{
        //    return new FixedAssetBL(CallContext).GetAssetCodes();
        //}

        //public List<AssetDTO> SaveAssets(List<AssetDTO> dtos)
        //{
        //    return new FixedAssetBL(CallContext).SaveAssets(dtos);
        //}

        //public AssetDTO GetAssetById(long AssetId)
        //{
        //    return new FixedAssetBL(CallContext).GetAssetById(AssetId);
        //}

        //public bool DeleteAsset(long AssetId)
        //{
        //    return new FixedAssetBL(CallContext).DeleteAsset(AssetId);
        //}

        //public AssetTransactionHeadDTO SaveAssetTransaction(AssetTransactionHeadDTO headDto)
        //{
        //    return new FixedAssetBL(CallContext).SaveAssetTransaction(headDto);
        //}

        //public AssetTransactionHeadDTO GetAssetTransactionHeadById(long HeadID)
        //{
        //    return new FixedAssetBL(CallContext).GetAssetTransactionHeadById(HeadID);
        //}

        //public List<AssetTransactionHeadDTO> GetAssetTransactionHeads(DocumentReferenceTypes referenceTypes, Eduegate.Framework.Enums.TransactionStatus transactionStatus)
        //{
        //    return new FixedAssetBL(CallContext).GetAssetTransactionHeads(referenceTypes, transactionStatus);
        //}

        //public List<KeyValueDTO> GetAssetCodesSearch(string SearchText)
        //{
        //    return new FixedAssetBL(CallContext).GetAssetCodesSearch(SearchText);
        //}

        //public List<KeyValueDTO> GetAccountCodesSearch(string SearchText)
        //{
        //    return new FixedAssetBL(CallContext).GetAccountCodesSearch(SearchText);
        //}

        //public AssetDTO GetAssetByAssetCode(string AssetCode)
        //{
        //    return new FixedAssetBL(CallContext).GetAssetByAssetCode(AssetCode);
        //}

        //public decimal GetAccumulatedDepreciation(long assetId, int documentTypeID)
        //{
        //    return new FixedAssetBL(CallContext).GetAccumulatedDepreciation(assetId, documentTypeID);
        //}

        ////public AssetTransactionHeadDTO GetAssetTransaction(AssetTransactionHeadDTO headDto)
        ////{
        ////    return new AssetTransactionBL(CallContext).SaveAssetTransaction(headDto);
        ////}

        //public AssetTransactionDTO SaveAssetTransactions(AssetTransactionDTO assetTransaction)
        //{
        //    try
        //    {
        //        var dto = new AssetTransactionBL(CallContext).SaveAssetTransactions(assetTransaction);
        //        var processing = new SettingBL().GetSettingValue("TransactionProcessing", TransactionProcessing.Immediate);

        //        if (!dto.AssetTransactionHead.IsError && processing == TransactionProcessing.Immediate)
        //        {
        //            new TransactionEngineCore.AssetTransaction(null, CallContext).StartProcess(0, 0, dto.AssetTransactionHead.HeadIID);
        //        }

        //        return dto;
        //    }

        //    catch (Exception ex)
        //    {
        //        Eduegate.Logger.LogHelper<Transaction>.Fatal(ex.Message.ToString(), ex);
        //        throw new FaultException(ex.Message);
        //    }
        //}

        //public AssetDTO GetAssetDetailsByID(long assetID)
        //{
        //    return AssetMapper.Mapper(CallContext).GetAssetDetailsByID(assetID);
        //}

        //public List<AssetSerialMapDTO> GetAssetSerialMapsByAssetID(long assetID)
        //{
        //    return AssetSerialMapMapper.Mapper(CallContext).GetAssetSerialMapsByAssetID(assetID);
        //}

        //public long? SaveAssetOpeningEntryDetails(long feedLogID)
        //{
        //    return AssetManualEntryMapper.Mapper(CallContext).SaveAssetOpeningEntryDetails(feedLogID);
        //}

        //public List<KeyValueDTO> GetFullActiveAssets()
        //{
        //    return AssetMapper.Mapper(CallContext).GetFullActiveAssets();
        //}

        //public List<KeyValueDTO> GetAssetsByCategoryID(long categoryID)
        //{
        //    return AssetCategoryMapper.Mapper(CallContext).GetAssetsByCategoryID(categoryID);
        //}

        //public List<AssetSerialMapDTO> GetAssetSerialMapsDetailsByAssetID(long assetID, DateTime? fiscalYearStartDate, DateTime? fiscalYearEndDate, long? branchID)
        //{
        //    return AssetSerialMapMapper.Mapper(CallContext).GetAssetSerialMapsDetailsByAssetID(assetID, fiscalYearStartDate, fiscalYearEndDate, branchID);
        //}

        //public decimal GetPreviousAcculatedDepreciationAmount(long assetSerialMapID, DateTime? fiscalYearStartDate)
        //{
        //    return AssetTransactionSerialMapMapper.Mapper(CallContext).GetPreviousAcculatedDepreciationAmount(assetSerialMapID, fiscalYearStartDate);
        //}

        //public List<KeyValueDTO> GetAssetSequencesByAssetID(long assetID)
        //{
        //    return AssetSerialMapMapper.Mapper(CallContext).GetAssetSequencesByAssetID(assetID);
        //}

        //public AssetInventoryDTO GetAssetInventoryDetail(long assetID, long branchID)
        //{
        //    try
        //    {
        //        var assetInventoryDTO = new AssetBL(CallContext).GetAssetInventoryDetail(assetID, branchID);
        //        return assetInventoryDTO;
        //    }
        //    catch (Exception exception)
        //    {
        //        Eduegate.Logger.LogHelper<Product>.Fatal(exception.Message, exception);
        //        throw new FaultException("Internal server, please check with your administrator");
        //    }
        //}

        //public AssetTransactionHeadDTO GetAssetTransactionDetail(long headID)
        //{
        //    try
        //    {
        //        return new AssetTransactionBL(CallContext).GetAssetTransactionDetail(headID);
        //    }
        //    catch (Exception ex)
        //    {
        //        Eduegate.Logger.LogHelper<Transaction>.Fatal(ex.Message.ToString(), ex);
        //        throw new FaultException("Internal server, please check with your administrator");
        //    }
        //}
        //public List<AssetTransactionHeadDTO> GetAllAssetTransaction(DocumentReferenceTypes referenceTypes, Eduegate.Framework.Enums.TransactionStatus transactionStatus)
        //{
        //    return new AssetTransactionBL(CallContext).GetAllAssetTransaction(referenceTypes, transactionStatus);
        //}

        //public bool SaveAssetTransaction(List<AssetTransactionHeadDTO> dtoList)
        //{
        //    return new AssetTransactionBL(CallContext).SaveAssetTransaction(dtoList);
        //}

        //public List<AssetInventoryDTO> ProcessAssetInventory(List<AssetInventoryDTO> dto)
        //{
        //    return new AssetTransactionBL(CallContext).ProcessAssetInventory(dto);
        //}

        //public List<AssetInventoryDTO> UpdateAssetInventory(List<AssetInventoryDTO> dto)
        //{
        //    return new AssetTransactionBL(CallContext).UpdateAssetInventory(dto);
        //}

        //public bool SaveAssetInventoryTransactions(List<AssetInventoryTransactionDTO> dto)
        //{
        //    return new AssetTransactionBL(CallContext).SaveAssetInventoryTransactions(dto);
        //}

        //public bool UpdateAssetTransactionHead(AssetTransactionHeadDTO dto)
        //{
        //    return new AssetTransactionBL(CallContext).UpdateAssetTransactionHead(dto);
        //}

        //public AssetInventoryDTO GetAssetInventoryDetail(AssetInventoryDTO dto)
        //{
        //    return new AssetTransactionBL(CallContext).GetAssetInventoryDetail(dto);
        //}

    
        //public decimal CheckAssetAvailability(long branchID, long productSKUMapID)
        //{
        //    return new AssetTransactionBL(CallContext).CheckAssetAvailability(branchID, productSKUMapID);
        //}

        //public bool CheckAssetAvailability(long? branchID, List<AssetTransactionDetailsDTO> assetTransactions)
        //{
        //    return new AssetTransactionBL(CallContext).CheckAssetAvailability(branchID, assetTransactions);
        //}

        ////public AssetTransactionDetailsDTO CheckAvailibilityBundleItem(long branchID, List<AssetTransactionDetailsDTO> assetTransactions)
        ////{
        ////    return new AssetTransactionBL(CallContext).CheckAssetAvailabilityBundleItem(branchID, assetTransactions);
        ////}

        //public AssetTransactionHeadDTO GetAssetTransactionDetail(string headId)
        //{
        //    return new AssetTransactionBL(CallContext).GetAssetTransactionDetail(Convert.ToInt32(headId));
        //}

        //public AssetTransactionDTO SaveTransactions(AssetTransactionDTO transaction)
        //{
        //    try
        //    {
        //        var dto = new AssetTransactionBL(CallContext).SaveAssetTransactions(transaction);
        //        var processing = new SettingBL().GetSettingValue("TransactionProcessing", TransactionProcessing.Immediate);

        //        if (!dto.AssetTransactionHead.IsError && processing == TransactionProcessing.Immediate)
        //        {
        //            new TransactionEngineCore.Transaction(null).StartProcess(0, 0, dto.AssetTransactionHead.HeadIID);
        //        }

        //        return dto;
        //    }

        //    catch (Exception ex)
        //    {
        //        Eduegate.Logger.LogHelper<Transaction>.Fatal(ex.Message.ToString(), ex);
        //        throw new FaultException(ex.Message);
        //    }
        //}

        //public AssetTransactionDTO GetAssetTransaction(long headIID, bool partialCalulation, bool checkClaims)
        //{
        //    try
        //    {
        //        return new AssetTransactionBL(CallContext).GetAssetTransaction(headIID, partialCalulation, checkClaims);
        //    }

        //    catch (Exception ex)
        //    {
        //        Eduegate.Logger.LogHelper<Transaction>.Fatal(ex.Message.ToString(), ex);
        //        throw new FaultException("Internal server, please check with your administrator");
        //    }
        //}

        ////public List<AssetTransactionAllocationDTO> GetAssetTransactionAllocations(long transactionDetailID)
        ////{
        ////    return new AssetTransactionBL(CallContext).GetAssetTransactionAllocations(transactionDetailID);
        ////}

        ////public List<KeyValueDTO> GetAssetInventorySerialMaps(long productSKUMapID, string searchText, bool serialKeyUsed, int pageSize = default(int),bool checkClaims = false)
        ////{
        ////    try
        ////    {
        ////        if (pageSize == default(int))
        ////        {
        ////            pageSize = Convert.ToInt32(new Domain.Setting.SettingBL(null).GetSettingValue<string>(Constants.MAX_FETCH_COUNT));
        ////        }
        ////        return new AssetTransactionBL(CallContext).GetAssetInventorySerialMaps(productSKUMapID, searchText, Convert.ToInt32(pageSize), serialKeyUsed, checkClaims);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        Eduegate.Logger.LogHelper<Transaction>.Fatal(ex.Message.ToString(), ex);
        ////        throw new FaultException("Internal server, please check with your administrator");
        ////    }
        ////}

        //public AssetTransactionSummaryDetailDTO GetAssetTransactionDetails(AssetTransactionSummaryParameterDTO parameter)
        //{
        //    return new AssetTransactionBL(CallContext).GetAssetTransactionDetails(parameter.DocuementTypeID, parameter.DateFrom, parameter.DateTo);
        //}

        //public AssetTransactionSummaryDetailDTO GetSupplierTransactionDetails(AssetTransactionSummaryParameterDTO parameter)
        //{
        //    return new AssetTransactionBL(CallContext).GetSupplierTransactionDetails(parameter.LoginID, parameter.DocuementTypeID, parameter.DateFrom, parameter.DateTo);
        //}

        //public List<KeyValueDTO> GetEntitlementsByHeadId(long HeadID)
        //{
        //    return new AssetTransactionBL(CallContext).GetEntitlementsByHeadId(HeadID);
        //}

        ////public TransactionDTO GetAssetTransactionByJobEntryHeadID(long jobEntryHeadId, bool partialCalulation = false)
        ////{
        ////    try
        ////    {
        ////        return new AssetTransactionBL(CallContext).GetAssetTransactionByJobEntryHeadID(jobEntryHeadId, partialCalulation);
        ////    }

        ////    catch (Exception ex)
        ////    {
        ////        Eduegate.Logger.LogHelper<Transaction>.Fatal(ex.Message.ToString(), ex);
        ////        throw new FaultException("Internal server, please check with your administrator");
        ////    }
        ////}

        ////public DigitalLimitDTO GetDigitalAmountCustomerCheck(Int64 customerID, long referenceType, decimal cartDigitalAmount, int companyID)
        ////{
        ////    try
        ////    {
        ////        return new AssetTransactionBL(CallContext).DigitalAmountCustomerCheck(customerID, referenceType, cartDigitalAmount, companyID);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        Eduegate.Logger.LogHelper<Transaction>.Fatal(ex.Message.ToString(), ex);
        ////        throw new FaultException("Internal server, please check with your administrator");
        ////    }
        ////}

        ////public bool HasDigitalProduct(long headID)
        ////{
        ////    try
        ////    {
        ////        return new AssetTransactionBL(CallContext).HasDigitalProduct(headID);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        Eduegate.Logger.LogHelper<Transaction>.Fatal(ex.Message.ToString(), ex);
        ////        throw new FaultException("Internal server, please check with your administrator");
        ////    }
        ////}
         
        //public List<AssetTransactionHeadDTO> GetAllTransactionsBySerialKey(string serialKey,bool IsDigital)
        //{
        //    try
        //    {
        //        return new AssetTransactionBL(CallContext).GetAllTransactionsBySerialKey(serialKey, IsDigital);
        //    }

        //    catch (Exception ex)
        //    {
        //        Eduegate.Logger.LogHelper<Transaction>.Fatal(ex.Message.ToString(), ex);
        //        throw new FaultException("Internal server, please check with your administrator");
        //    }
        //}

        //public bool CancelAssetTransaction(long headID)
        //{
        //    try
        //    {
        //        return new AssetTransactionBL(CallContext).CancelAssetTransaction(headID);
        //    }
        //    catch (Exception ex)
        //    {
        //        Eduegate.Logger.LogHelper<Transaction>.Fatal(ex.Message.ToString(), ex);
        //        throw new FaultException("Internal server, please check with your administrator");
        //    }
        //}

        //public bool ProductSerialCountCheck(string hash, long ProductSKUMapID, string SerialNo, long ProductSerialID)
        //{
        //    try
        //    {
        //        return new AssetTransactionBL(CallContext).ProductSerialCountCheck(hash,ProductSKUMapID, SerialNo, ProductSerialID);
        //    }

        //    catch (Exception ex)
        //    {
        //        Eduegate.Logger.LogHelper<Transaction>.Fatal(ex.Message.ToString(), ex);
        //        throw new FaultException("Internal server, please check with your administrator");
        //    }
        //}
    
        ////public OrderDetailDTO GetDeliveryDetails(long Id)
        ////{
        ////    try
        ////    {
        ////        return new AssetTransactionBL(CallContext).GetDeliveryDetails(Id);              
        ////    }
        ////    catch (Exception exception)
        ////    {
        ////        Eduegate.Logger.LogHelper<OrderDetailDTO>.Fatal(exception.Message, exception);
        ////        throw new FaultException("Internal server, please check with your administrator");
        ////    }

        ////}

        ////public OrderDetailDTO SaveDeliveryDetails(OrderDetailDTO order)
        ////{ 
        ////    try
        ////    {
        ////        OrderDetailDTO orderDetail = new AssetTransactionBL(CallContext).SaveDeliveryDetails(order);
        ////        Eduegate.Logger.LogHelper<OrderDetailDTO>.Info("Service Result: " + orderDetail);
        ////        return orderDetail;
        ////    }
        ////    catch (Exception exception)
        ////    {
        ////        Eduegate.Logger.LogHelper<OrderDetailDTO>.Fatal(exception.Message, exception);
        ////        throw new FaultException("Internal server, please check with your administrator");
        ////    }
        ////}

        ////public List<DeliverySettingDTO> GetDeliveryOptions() 
        ////{ 
        ////    return new AssetTransactionBL(CallContext).GetDeliveryOptions();
        ////}

        ////public bool IsChangeRequestDetailProcessed(long detailId)
        ////{
        ////    try
        ////    {
        ////        return new AssetTransactionBL(CallContext).IsChangeRequestDetailProcessed(detailId);
        ////    }
        ////    catch (Exception exception)
        ////    {
        ////        Eduegate.Logger.LogHelper<Transaction>.Fatal(exception.Message.ToString(), exception);
        ////        throw new FaultException("Internal server, please check with your administrator");
        ////    }
        ////}

        //public List<KeyValueDTO> GetKeysEncryptDecrypt(string keys, bool isEncrypted)
        //{
        //    try
        //    {
        //        return new AssetTransactionBL(CallContext).GetKeysEncryptDecrypt(keys, isEncrypted);
        //    }
        //    catch (Exception ex)
        //    {
        //        Eduegate.Logger.LogHelper<TransactionBL>.Fatal(ex.Message.ToString(), ex);
        //        throw new FaultException("Internal server, please check with your administrator");
        //    }
        //}

        ////public List<ProductBundleDTO> GetProductBundleItemDetail(long productSKUMapID)
        ////{
        ////    try
        ////    {
        ////        var transactionBL = new AssetTransactionBL(CallContext);
        ////        var bundleItems = transactionBL.GetAssetBundleItemDetail(productSKUMapID);
        ////        Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + bundleItems.Count.ToString());
        ////        return bundleItems;
        ////    }
        ////    catch (Exception exception)
        ////    {
        ////        Eduegate.Logger.LogHelper<ProductBundleDTO>.Fatal(exception.Message, exception);
        ////        throw new FaultException("Internal server, please check with your administrator");
        ////    }
        ////}

        //public bool UpdateAssetSerialMapsInventoryID(List<long> AssetSerialMapIDs, AssetInventoryDTO assetInventory)
        //{
        //    try
        //    {
        //        return new AssetTransactionBL(CallContext).UpdateAssetSerialMapsInventoryID(AssetSerialMapIDs, assetInventory);
        //    }
        //    catch (Exception ex)
        //    {
        //        Eduegate.Logger.LogHelper<Transaction>.Fatal(ex.Message.ToString(), ex);
        //        throw new FaultException("Internal server, please check with your administrator");
        //    }
        //}

        //public bool UpdateAssetSerialMapsStatus(List<long> AssetSerialMapIDs, AssetInventoryDTO assetInventory)
        //{
        //    try
        //    {
        //        return new AssetTransactionBL(CallContext).UpdateAssetSerialMapsStatus(AssetSerialMapIDs, assetInventory);
        //    }
        //    catch (Exception ex)
        //    {
        //        Eduegate.Logger.LogHelper<Transaction>.Fatal(ex.Message.ToString(), ex);
        //        throw new FaultException("Internal server, please check with your administrator");
        //    }
        //}

    }
}

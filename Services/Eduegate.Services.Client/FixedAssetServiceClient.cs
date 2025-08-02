using System;
using System.Collections.Generic;
using Eduegate.Framework;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Accounts.Assets;

namespace Eduegate.Service.Client
{
    public class FixedAssetServiceClient : BaseClient, IFixedAssetsService
    {

        private static string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string Service = string.Concat(ServiceHost, "FixedAssetsService.svc/");

        public FixedAssetServiceClient(CallContext callContext = null, Action<string> logger = null)
            : base(callContext, logger)
        {

        }
        public List<AssetCategoryDTO> GetAssetCategories()
        {
            string uri = string.Concat(Service + "\\GetAssetCategories");
            return ServiceHelper.HttpGetRequest<List<AssetCategoryDTO>>(uri, _callContext, _logger);
        }
        public List<AssetDTO> SaveAssets(List<AssetDTO> dtos)
        {
            string uri = string.Concat(Service + "\\SaveAssets");
            return ServiceHelper.HttpPostGetRequest(uri, dtos) ;
        }
        public List<KeyValueDTO> GetAssetCodes()
        {
            string uri = string.Concat(Service + "\\GetAssetCodes");
            return ServiceHelper.HttpGetRequest<List<KeyValueDTO>>(uri);
        }

        public AssetDTO GetAssetById(long AssetId)
        {
            string uri = string.Concat(Service + "\\GetAssetById?AssetId=" + AssetId);
            return ServiceHelper.HttpGetRequest<AssetDTO>(uri, _callContext, _logger);            
        }
        public bool DeleteAsset(long AssetId)
        {
            string uri = string.Concat(Service + "\\DeleteAsset");
            return ServiceHelper.HttpPostRequest(uri, AssetId) == "true" ? true : false;
        }

        public AssetTransactionHeadDTO SaveAssetTransaction(AssetTransactionHeadDTO headDto)
        {
            string uri = string.Concat(Service + "\\SaveAssetTransaction");
            return ServiceHelper.HttpPostGetRequest<AssetTransactionHeadDTO>(uri, headDto, _callContext, _logger) ;           
        }

        public AssetTransactionHeadDTO GetAssetTransactionHeadById(long HeadID)
        {
            string uri = string.Concat(Service + "\\GetAssetTransactionHeadById?HeadID=" + HeadID);
            return ServiceHelper.HttpGetRequest<AssetTransactionHeadDTO>(uri, _callContext, _logger);
        }

        public List<AssetTransactionHeadDTO> GetAssetTransactionHeads(DocumentReferenceTypes referenceTypes, Eduegate.Framework.Enums.TransactionStatus transactionStatus)
        {
            string uri = string.Concat(Service + "\\GetAssetTransactionHeads?referenceTypes=" + referenceTypes + "&transactionStatus=" + transactionStatus);
            return ServiceHelper.HttpGetRequest<List<AssetTransactionHeadDTO>>(uri);
        }

        public bool UpdateAssetTransactionHead(AssetTransactionHeadDTO dto)
        {
            string uri = string.Concat(Service + "\\UpdateAssetTransactionHead");
            return ServiceHelper.HttpPostRequest(uri, dto) == "true" ? true : false;
        }
        public List<KeyValueDTO> GetAssetCodesSearch(string SearchText)
        {
            string uri = string.Concat(Service + "\\GetAssetCodesSearch?SearchText="+ SearchText);
            return ServiceHelper.HttpGetRequest<List<KeyValueDTO>>(uri);
        }
        public List<KeyValueDTO> GetAccountCodesSearch(string SearchText)
        {
            string uri = string.Concat(Service + "\\GetAccountCodesSearch?SearchText=" + SearchText);
            return ServiceHelper.HttpGetRequest<List<KeyValueDTO>>(uri);
        }
        public AssetDTO GetAssetByAssetCode(string AssetCode)
        {
            string uri = string.Concat(Service + "\\GetAssetByAssetCode?AssetCode=" + AssetCode);
            return ServiceHelper.HttpGetRequest<AssetDTO>(uri, _callContext, _logger);
        }

        public decimal GetAccumulatedDepreciation(long assetId, int documentTypeID)
        {
            string uri = string.Concat(Service + "\\GetAccumulatedDepreciation?assetId=" + assetId + "&documentTypeID=" + documentTypeID);
            return ServiceHelper.HttpGetRequest<decimal>(uri, _callContext, _logger);
        }

        AssetTransactionDTO IFixedAssetsService.SaveAssetTransactions(AssetTransactionDTO assetTransaction)
        {
            throw new NotImplementedException();
        }

        AssetTransactionDTO IFixedAssetsService.GetAssetTransaction(long headIID, bool partialCalulation, bool checkClaims)
        {
            throw new NotImplementedException();
        }

        public AssetDTO GetAssetDetailsByID(long assetID)
        {
            throw new NotImplementedException();
        }

        public decimal CheckAssetAvailability(long branchID, long assetID)
        {
            throw new NotImplementedException();
        }

        public List<AssetInventoryDTO> ProcessAssetInventory(List<AssetInventoryDTO> dtos)
        {
            throw new NotImplementedException();
        }

        public List<AssetSerialMapDTO> GetAssetSerialMapsByAssetID(long assetID)
        {
            throw new NotImplementedException();
        }

        public long? SaveAssetOpeningEntryDetails(long feedLogID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetFullActiveAssets()
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetAssetsByCategoryID(long categoryID)
        {
            throw new NotImplementedException();
        }

        public List<AssetSerialMapDTO> GetAssetSerialMapsDetailsByAssetID(long assetID, DateTime? fiscalYearStartDate, DateTime? fiscalYearEndDate, long? branchID)
        {
            throw new NotImplementedException();
        }

        public decimal GetPreviousAcculatedDepreciationAmount(long assetSerialMapID, DateTime? fiscalYearStartDate)
        {
            throw new NotImplementedException();
        }

        public bool SaveAssetInventoryTransactions(List<AssetInventoryTransactionDTO> dto)
        {
            throw new NotImplementedException();
        }

        public bool CancelAssetTransaction(long headID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetAssetSequencesByAssetID(long assetID)
        {
            throw new NotImplementedException();
        }

        public AssetInventoryDTO GetAssetInventoryDetail(long assetID, long branchID)
        {
            throw new NotImplementedException();
        }

        public AssetTransactionHeadDTO GetAssetTransactionDetail(long headID)
        {
            throw new NotImplementedException();
        }

        public List<AssetTransactionHeadDTO> GetAllAssetTransaction(DocumentReferenceTypes referenceTypes, Eduegate.Framework.Enums.TransactionStatus transactionStatus)
        {
            throw new NotImplementedException();
        }

        public bool UpdateAssetSerialMapsInventoryID(List<long> AssetSerialMapIDs, AssetInventoryDTO assetInventory)
        {
            throw new NotImplementedException();
        }

        public bool UpdateAssetSerialMapsStatus(List<long> AssetSerialMapIDs, AssetInventoryDTO assetInventory)
        {
            throw new NotImplementedException();
        }

        public AssetCategoryDTO GetAssetCategoryDetailsByID(long assetCategoryID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetAssetsByProductSKUID(long productSKUID)
        {
            throw new NotImplementedException();
        }

        public List<AssetSerialMapDTO> GetAssetSerialMapsByAssetAndBranchID(long assetID, long branchID)
        {
            throw new NotImplementedException();
        }

        public bool InsertAssetSerialMapList(List<AssetSerialMapDTO> assetSerialMaps)
        {
            throw new NotImplementedException();
        }

    }
}
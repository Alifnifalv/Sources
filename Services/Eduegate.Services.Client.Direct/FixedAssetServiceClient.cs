using System;
using System.Collections.Generic;
using Eduegate.Framework;
using Eduegate.Framework.Enums;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services;
using Eduegate.Services.Contracts.Accounts.Assets;

namespace Eduegate.Service.Client.Direct
{
    public class FixedAssetServiceClient : BaseClient, IFixedAssetsService
    {
        FixedAssetsService assetService = new FixedAssetsService();

        public FixedAssetServiceClient(CallContext callContext = null, Action<string> logger = null)
            : base(callContext, logger)
        {
            assetService.CallContext = callContext;
        }

        public List<AssetCategoryDTO> GetAssetCategories()
        {
            return assetService.GetAssetCategories();
        }
        public List<AssetDTO> SaveAssets(List<AssetDTO> dtos)
        {
            return assetService.SaveAssets(dtos);
        }
        public List<KeyValueDTO> GetAssetCodes()
        {
            return assetService.GetAssetCodes();
        }

        public AssetDTO GetAssetById(long AssetId)
        {
            return assetService.GetAssetById(AssetId);
        }
        public bool DeleteAsset(long AssetId)
        {
            return assetService.DeleteAsset(AssetId);
        }

        public AssetTransactionHeadDTO SaveAssetTransaction(AssetTransactionHeadDTO headDto)
        {
            return assetService.SaveAssetTransaction(headDto);
        }

        public AssetTransactionHeadDTO GetAssetTransactionHeadById(long HeadID)
        {
            return assetService.GetAssetTransactionHeadById(HeadID);
        }
        public List<AssetTransactionHeadDTO> GetAssetTransactionHeads(DocumentReferenceTypes referenceTypes, Eduegate.Framework.Enums.TransactionStatus transactionStatus)
        {
            return assetService.GetAssetTransactionHeads(referenceTypes, transactionStatus);
        }

        public bool UpdateAssetTransactionHead(AssetTransactionHeadDTO dto)
        {
            return assetService.UpdateAssetTransactionHead(dto);
        }
        public List<KeyValueDTO> GetAssetCodesSearch(string searchText)
        {
            return assetService.GetAssetCodesSearch(searchText);
        }
        public List<KeyValueDTO> GetAccountCodesSearch(string searchText)
        {
            return assetService.GetAccountCodesSearch(searchText);
        }
        public AssetDTO GetAssetByAssetCode(string assetCode)
        {
            return assetService.GetAssetByAssetCode(assetCode);
        }

        public decimal GetAccumulatedDepreciation(long assetId, int documentTypeID)
        {
            return assetService.GetAccumulatedDepreciation(assetId, documentTypeID);
        }

        public AssetTransactionDTO SaveAssetTransactions(AssetTransactionDTO headDto)
        {
            return assetService.SaveAssetTransactions(headDto);
        }

        public AssetTransactionDTO GetAssetTransaction(long headIID, bool partialCalulation, bool checkClaims)
        {
            return assetService.GetAssetTransaction(headIID, partialCalulation, checkClaims);
        }

        public AssetDTO GetAssetDetailsByID(long assetID)
        {
            return assetService.GetAssetDetailsByID(assetID);
        }

        public decimal CheckAssetAvailability(long branchID, long assetID)
        {
            return assetService.CheckAssetAvailability(branchID, assetID);
        }

        public List<AssetInventoryDTO> ProcessAssetInventory(List<AssetInventoryDTO> dtos)
        {
            return assetService.ProcessAssetInventory(dtos);
        }

        public List<AssetSerialMapDTO> GetAssetSerialMapsByAssetID(long assetID)
        {
            return assetService.GetAssetSerialMapsByAssetID(assetID);
        }

        public long? SaveAssetOpeningEntryDetails(long feedLogID)
        {
            return assetService.SaveAssetOpeningEntryDetails(feedLogID);
        }

        public List<KeyValueDTO> GetFullActiveAssets()
        {
            return assetService.GetFullActiveAssets();
        }

        public List<KeyValueDTO> GetAssetsByCategoryID(long categoryID)
        {
            return assetService.GetAssetsByCategoryID(categoryID);
        }

        public List<AssetSerialMapDTO> GetAssetSerialMapsDetailsByAssetID(long assetID, DateTime? fiscalYearStartDate, DateTime? fiscalYearEndDate, long? branchID)
        {
            return assetService.GetAssetSerialMapsDetailsByAssetID(assetID, fiscalYearStartDate, fiscalYearEndDate, branchID);
        }

        public decimal GetPreviousAcculatedDepreciationAmount(long assetSerialMapID, DateTime? fiscalYearStartDate)
        {
            return assetService.GetPreviousAcculatedDepreciationAmount(assetSerialMapID, fiscalYearStartDate);
        }

        public bool SaveAssetInventoryTransactions(List<AssetInventoryTransactionDTO> dto)
        {
            return assetService.SaveAssetInventoryTransactions(dto);
        }

        public bool CancelAssetTransaction(long headID)
        {
            return assetService.CancelAssetTransaction(headID);
        }

        public List<KeyValueDTO> GetAssetSequencesByAssetID(long assetID)
        {
            return assetService.GetAssetSequencesByAssetID(assetID);
        }

        public AssetInventoryDTO GetAssetInventoryDetail(long assetID, long branchID)
        {
            return assetService.GetAssetInventoryDetail(assetID, branchID);
        }

        public AssetTransactionHeadDTO GetAssetTransactionDetail(long headID)
        {
            return assetService.GetAssetTransactionDetail(headID);
        }

        public List<AssetTransactionHeadDTO> GetAllAssetTransaction(DocumentReferenceTypes referenceTypes, Eduegate.Framework.Enums.TransactionStatus transactionStatus)
        {
            return assetService.GetAllAssetTransaction(referenceTypes, transactionStatus);
        }

        public bool UpdateAssetSerialMapsInventoryID(List<long> AssetSerialMapIDs, AssetInventoryDTO assetInventory)
        {
            return assetService.UpdateAssetSerialMapsInventoryID(AssetSerialMapIDs, assetInventory);
        }

        public bool UpdateAssetSerialMapsStatus(List<long> AssetSerialMapIDs, AssetInventoryDTO assetInventory)
        {
            return assetService.UpdateAssetSerialMapsInventoryID(AssetSerialMapIDs, assetInventory);
        }

        public AssetCategoryDTO GetAssetCategoryDetailsByID(long assetCategoryID)
        {
            return assetService.GetAssetCategoryDetailsByID(assetCategoryID);
        }

        public List<KeyValueDTO> GetAssetsByProductSKUID(long productSKUID)
        {
            return assetService.GetAssetsByProductSKUID(productSKUID);
        }

        public List<AssetSerialMapDTO> GetAssetSerialMapsByAssetAndBranchID(long assetID, long branchID)
        {
            return assetService.GetAssetSerialMapsByAssetID(assetID);
        }

        public bool InsertAssetSerialMapList(List<AssetSerialMapDTO> assetSerialMaps)
        {
            return assetService.InsertAssetSerialMapList(assetSerialMaps);
        }

    }
}
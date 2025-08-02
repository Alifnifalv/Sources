using System.Collections.Generic;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Accounts.Assets;
using System;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITransactionEngine" in both code and config file together.
    public interface IFixedAssetsService
    {
        List<AssetCategoryDTO> GetAssetCategories();

        List<KeyValueDTO> GetAssetCodes();

        List<AssetDTO> SaveAssets(List<AssetDTO> dtos);
 
        AssetDTO GetAssetById(long AssetId);

        bool DeleteAsset(long AssetId);

        AssetTransactionHeadDTO SaveAssetTransaction(AssetTransactionHeadDTO headDto);

        AssetTransactionHeadDTO GetAssetTransactionHeadById(long HeadID);

        List<AssetTransactionHeadDTO> GetAssetTransactionHeads(DocumentReferenceTypes referenceTypes, Eduegate.Framework.Enums.TransactionStatus transactionStatus);

        bool UpdateAssetTransactionHead(AssetTransactionHeadDTO dto);

        List<KeyValueDTO> GetAssetCodesSearch(string searchText);

        List<KeyValueDTO> GetAccountCodesSearch(string searchText);

        AssetDTO GetAssetByAssetCode(string assetCode);

        decimal GetAccumulatedDepreciation(long assetId, int documentTypeID);

        AssetTransactionDTO SaveAssetTransactions(AssetTransactionDTO assetTransaction);

        AssetTransactionDTO GetAssetTransaction(long headIID, bool partialCalulation, bool checkClaims);

        public AssetDTO GetAssetDetailsByID(long assetID);

        public decimal CheckAssetAvailability(long branchID, long assetID);

        public List<AssetInventoryDTO> ProcessAssetInventory(List<AssetInventoryDTO> dtos);

        public List<AssetSerialMapDTO> GetAssetSerialMapsByAssetID(long assetID);

        public long? SaveAssetOpeningEntryDetails(long feedLogID);

        List<KeyValueDTO> GetFullActiveAssets();

        List<KeyValueDTO> GetAssetsByCategoryID(long categoryID);

        public List<AssetSerialMapDTO> GetAssetSerialMapsDetailsByAssetID(long assetID, DateTime? fiscalYearStartDate, DateTime? fiscalYearEndDate, long? branchID);

        public decimal GetPreviousAcculatedDepreciationAmount(long assetSerialMapID, DateTime? fiscalYearStartDate);

        public bool SaveAssetInventoryTransactions(List<AssetInventoryTransactionDTO> dto);

        public bool CancelAssetTransaction(long headID);

        public List<KeyValueDTO> GetAssetSequencesByAssetID(long assetID);

        public AssetInventoryDTO GetAssetInventoryDetail(long assetID, long branchID);

        public AssetTransactionHeadDTO GetAssetTransactionDetail(long headID);

        public List<AssetTransactionHeadDTO> GetAllAssetTransaction(DocumentReferenceTypes referenceTypes, Eduegate.Framework.Enums.TransactionStatus transactionStatus);

        public bool UpdateAssetSerialMapsInventoryID(List<long> AssetSerialMapIDs, AssetInventoryDTO assetInventory);

        public bool UpdateAssetSerialMapsStatus(List<long> AssetSerialMapIDs, AssetInventoryDTO assetInventory);

        public AssetCategoryDTO GetAssetCategoryDetailsByID(long assetCategoryID);

        public List<KeyValueDTO> GetAssetsByProductSKUID(long productSKUID);

        public List<AssetSerialMapDTO> GetAssetSerialMapsByAssetAndBranchID(long assetID, long branchID);

        public bool InsertAssetSerialMapList(List<AssetSerialMapDTO> assetSerialMaps);

    }
}
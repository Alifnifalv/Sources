using Eduegate.Framework.Enums;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Accounts.Assets;
using Eduegate.Domain.Accounts.Assets;
using System.ServiceModel;
using Eduegate.Domain.Mappers.Accounts.Assets;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Domain.Setting;

namespace Eduegate.Services
{
    public class FixedAssetsService : BaseService, IFixedAssetsService
    {
        FixedAssetBL fixedAssetBL;
        public FixedAssetsService()
        {
            fixedAssetBL = new FixedAssetBL(CallContext);
        }

        public List<AssetCategoryDTO> GetAssetCategories()
        {
            return fixedAssetBL.GetAssetCategories();
        }

        public List<KeyValueDTO> GetAssetCodes()
        {
            return fixedAssetBL.GetAssetCodes();
        }

        public List<AssetDTO> SaveAssets(List<AssetDTO> dtos)
        {
            return fixedAssetBL.SaveAssets(dtos);
        }

        public AssetDTO GetAssetById(long AssetId)
        {
            return fixedAssetBL.GetAssetById(AssetId);
        }

        public bool DeleteAsset(long AssetId)
        {
            return fixedAssetBL.DeleteAsset(AssetId);
        }

        public AssetTransactionHeadDTO SaveAssetTransaction(AssetTransactionHeadDTO headDto)
        {
            return fixedAssetBL.SaveAssetTransaction(headDto);
        }

        public AssetTransactionHeadDTO GetAssetTransactionHeadById(long HeadID)
        {
            return fixedAssetBL.GetAssetTransactionHeadById(HeadID);
        }

        public List<AssetTransactionHeadDTO> GetAssetTransactionHeads(DocumentReferenceTypes referenceTypes, Eduegate.Framework.Enums.TransactionStatus transactionStatus)
        {
            return fixedAssetBL.GetAssetTransactionHeads(referenceTypes, transactionStatus);
        }

        public bool UpdateAssetTransactionHead(AssetTransactionHeadDTO dto)
        {
            return fixedAssetBL.UpdateAssetTransactionHead(dto);
        }

        public List<KeyValueDTO> GetAssetCodesSearch(string SearchText)
        {
            return fixedAssetBL.GetAssetCodesSearch(SearchText);
        }

        public List<KeyValueDTO> GetAccountCodesSearch(string SearchText)
        {
            return fixedAssetBL.GetAccountCodesSearch(SearchText);
        }

        public AssetDTO GetAssetByAssetCode(string AssetCode)
        {
            return fixedAssetBL.GetAssetByAssetCode(AssetCode);
        }

        public decimal GetAccumulatedDepreciation(long assetId, int documentTypeID)
        {
            return fixedAssetBL.GetAccumulatedDepreciation(assetId, documentTypeID);
        }

        public AssetTransactionHeadDTO GetAssetTransaction(AssetTransactionHeadDTO headDto)
        {
            return fixedAssetBL.SaveAssetTransaction(headDto);
        }

        public AssetTransactionDTO SaveAssetTransactions(AssetTransactionDTO assetTransaction)
        {
            try
            {
                var dto = new AssetTransactionBL(CallContext).SaveAssetTransactions(assetTransaction);
                var processing = new SettingBL().GetSettingValue("TransactionProcessing", TransactionProcessing.Immediate);

                if (!dto.AssetTransactionHead.IsError && processing == TransactionProcessing.Immediate)
                {
                    new TransactionEngineCore.AssetTransaction(null, CallContext).StartProcess(0, 0, dto.AssetTransactionHead.HeadIID, CallContext?.LoginID);
                }

                return dto;
            }

            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<Transaction>.Fatal(ex.Message.ToString(), ex);
                throw new FaultException(ex.Message);
            }
        }

        public AssetTransactionDTO GetAssetTransaction(long headIID, bool partialCalulation, bool checkClaims)
        {
            try
            {
                return new AssetTransactionBL(CallContext).GetAssetTransaction(headIID, partialCalulation, checkClaims);
            }

            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<Transaction>.Fatal(ex.Message.ToString(), ex);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public AssetDTO GetAssetDetailsByID(long assetID)
        {
            return AssetMapper.Mapper(CallContext).GetAssetDetailsByID(assetID);
        }

        public decimal CheckAssetAvailability(long branchID, long assetID)
        {
            return new AssetTransactionBL(CallContext).CheckAssetAvailability(branchID, assetID);
        }

        public List<AssetInventoryDTO> ProcessAssetInventory(List<AssetInventoryDTO> dtos)
        {
            return new AssetTransactionBL(CallContext).ProcessAssetInventory(dtos);
        }

        public bool SaveAssetInventoryTransactions(List<AssetInventoryTransactionDTO> dtos)
        {
            return new AssetTransactionBL(CallContext).SaveAssetInventoryTransactions(dtos);
        }

        public List<AssetSerialMapDTO> GetAssetSerialMapsByAssetID(long assetID)
        {
            return AssetSerialMapMapper.Mapper(CallContext).GetAssetSerialMapsByAssetID(assetID);
        }

        public long? SaveAssetOpeningEntryDetails(long feedLogID)
        {
            return AssetManualEntryMapper.Mapper(CallContext).SaveAssetOpeningEntryDetails(feedLogID);
        }

        public List<KeyValueDTO> GetFullActiveAssets()
        {
            return AssetMapper.Mapper(CallContext).GetFullActiveAssets();
        }

        public List<KeyValueDTO> GetAssetsByCategoryID(long categoryID)
        {
            return AssetCategoryMapper.Mapper(CallContext).GetAssetsByCategoryID(categoryID);
        }

        public List<AssetSerialMapDTO> GetAssetSerialMapsDetailsByAssetID(long assetID, DateTime? fiscalYearStartDate, DateTime? fiscalYearEndDate, long? branchID)
        {
            return AssetSerialMapMapper.Mapper(CallContext).GetAssetSerialMapsDetailsByAssetID(assetID, fiscalYearStartDate, fiscalYearEndDate, branchID);
        }

        public decimal GetPreviousAcculatedDepreciationAmount(long assetSerialMapID, DateTime? fiscalYearStartDate)
        {
            return AssetTransactionSerialMapMapper.Mapper(CallContext).GetPreviousAcculatedDepreciationAmount(assetSerialMapID, fiscalYearStartDate);
        }

        public bool CancelAssetTransaction(long headID)
        {
            try
            {
                return new AssetTransactionBL(CallContext).CancelAssetTransaction(headID);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<Transaction>.Fatal(ex.Message.ToString(), ex);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<KeyValueDTO> GetAssetSequencesByAssetID(long assetID)
        {
            return AssetSerialMapMapper.Mapper(CallContext).GetAssetSequencesByAssetID(assetID);
        }

        public AssetInventoryDTO GetAssetInventoryDetail(long assetID, long branchID)
        {
            try
            {
                var assetInventoryDTO = new AssetBL(CallContext).GetAssetInventoryDetail(assetID, branchID);
                return assetInventoryDTO;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Product>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public AssetTransactionHeadDTO GetAssetTransactionDetail(long headID)
        {
            try
            {
                return new AssetTransactionBL(CallContext).GetAssetTransactionDetail(headID);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<Transaction>.Fatal(ex.Message.ToString(), ex);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<AssetTransactionHeadDTO> GetAllAssetTransaction(DocumentReferenceTypes referenceTypes, Eduegate.Framework.Enums.TransactionStatus transactionStatus)
        {
            try
            {
                return new AssetTransactionBL(CallContext).GetAllAssetTransaction(referenceTypes, transactionStatus);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<Transaction>.Fatal(ex.Message.ToString(), ex);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool UpdateAssetSerialMapsInventoryID(List<long> AssetSerialMapIDs, AssetInventoryDTO assetInventory)
        {
            try
            {
                return new AssetTransactionBL(CallContext).UpdateAssetSerialMapsInventoryID(AssetSerialMapIDs, assetInventory);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<Transaction>.Fatal(ex.Message.ToString(), ex);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool UpdateAssetSerialMapsStatus(List<long> AssetSerialMapIDs, AssetInventoryDTO assetInventory)
        {
            try
            {
                return new AssetTransactionBL(CallContext).UpdateAssetSerialMapsStatus(AssetSerialMapIDs, assetInventory);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<Transaction>.Fatal(ex.Message.ToString(), ex);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public AssetCategoryDTO GetAssetCategoryDetailsByID(long assetCategoryID)
        {
            return AssetCategoryMapper.Mapper(CallContext).GetAssetCategoryDetailsByID(assetCategoryID);
        }

        public List<KeyValueDTO> GetAssetsByProductSKUID(long productSKUID)
        {
            return AssetMapper.Mapper(CallContext).GetAssetsByProductSKUID(productSKUID);
        }

        public List<AssetSerialMapDTO> GetAssetSerialMapsByAssetAndBranchID(long assetID, long branchID)
        {
            return AssetSerialMapMapper.Mapper(CallContext).GetAssetSerialMapsByAssetAndBranchID(assetID, branchID);
        }

        public bool InsertAssetSerialMapList(List<AssetSerialMapDTO> assetSerialMaps)
        {
            try
            {
                return new AssetTransactionBL(CallContext).InsertAssetSerialMapList(assetSerialMaps);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<Transaction>.Fatal(ex.Message.ToString(), ex);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

    }
}
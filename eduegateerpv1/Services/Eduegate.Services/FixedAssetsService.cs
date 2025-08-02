using Eduegate.Domain;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Accounting;

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
            return  fixedAssetBL.UpdateAssetTransactionHead(dto);
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
    }
}

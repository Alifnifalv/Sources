using System.Collections.Generic;
using Eduegate.Framework.Enums;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Accounting;

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
    }        
}
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
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services;

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
    }
}

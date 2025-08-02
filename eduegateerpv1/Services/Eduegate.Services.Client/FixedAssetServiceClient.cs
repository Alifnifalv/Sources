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
    }
}

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using Eduegate.Framework.Enums;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Accounting;


namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITransactionEngine" in both code and config file together.
    [ServiceContract]
    public interface IFixedAssetsService
    {

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAssetCategories")]
        List<AssetCategoryDTO> GetAssetCategories();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAssetCodes")]
        List<KeyValueDTO> GetAssetCodes();


        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveAssets")]
        List<AssetDTO> SaveAssets(List<AssetDTO> dtos);

       
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAssetById?AssetId={AssetId}")]
        AssetDTO GetAssetById(long AssetId);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "DeleteAsset")]
        bool DeleteAsset(long AssetId);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveAssetTransaction")]
        AssetTransactionHeadDTO SaveAssetTransaction(AssetTransactionHeadDTO headDto);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAssetTransactionHeadById?HeadID={HeadID}")]
        AssetTransactionHeadDTO GetAssetTransactionHeadById(long HeadID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAssetTransactionHeads?referenceTypes={referenceTypes}&transactionStatus={transactionStatus}")]
        List<AssetTransactionHeadDTO> GetAssetTransactionHeads(DocumentReferenceTypes referenceTypes, Eduegate.Framework.Enums.TransactionStatus transactionStatus);


        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdateAssetTransactionHead")]
        bool UpdateAssetTransactionHead(AssetTransactionHeadDTO dto);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAssetCodesSearch?SearchText={SearchText}")]
        List<KeyValueDTO> GetAssetCodesSearch(string searchText);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAccountCodesSearch?SearchText={SearchText}")]
        List<KeyValueDTO> GetAccountCodesSearch(string searchText);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAssetByAssetCode?AssetCode={AssetCode}")]
        AssetDTO GetAssetByAssetCode(string assetCode);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAccumulatedDepreciation?assetId={assetId}&documentTypeID={documentTypeID}")]
        decimal GetAccumulatedDepreciation(long assetId, int documentTypeID);
    }        
}

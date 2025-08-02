using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.IO;

namespace Eduegate.Services.Contracts
{
    [ServiceContract]
    public interface IDataFeedService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "FeedInventory")]
        bool FeedInventory(List<ProductInventoryDTO> inventoryDTOCollection);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetDataFeedTypes")]
        List<DataFeedTypeDTO> GetDataFeedTypes();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetDataFeedType?templateID={templateID}")]
        DataFeedTypeDTO GetDataFeedType(int templateID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveDataFeedLog")]
        DataFeedLogDTO SaveDataFeedLog(DataFeedLogDTO dataFeedLogDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetDataFeedLogByID?ID={ID}")]
        DataFeedLogDTO GetDataFeedLogByID(long ID);

        [OperationContract]
        [WebGet(UriTemplate = "DownloadProcessedFeedFile?feedLogID={feedLogID}")]
        Stream DownloadProcessedFeedFile(long feedLogID);

        [OperationContract]
        [WebGet(UriTemplate = "DownloadDataFeedTemplate?templateTypeID={templateTypeID}")]
        Stream DownloadDataFeedTemplate(int templateTypeID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UploadFeedFile?fileName={fileName}&feedLogID={feedLogID}&filePath={filePath}")]
        DataFeedLogDTO UploadFeedFile(string fileName, long feedLogID, string filePath, Stream stream);
    }
}

using Eduegate.Services.Contracts.Contents.Enums;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Eduegate.Services.Contracts.Contents.Interfaces
{
    [ServiceContract]
    public interface IContentService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveFile")]
        ContentFileDTO SaveFile(ContentFileDTO data);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveFiles")]
        List<ContentFileDTO> SaveFiles(List<ContentFileDTO> datas);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetFile?contentType={contentType}&referenceID={referenceID}")]
        ContentFileDTO GetFile(ContentType contentType, long referenceID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
          RequestFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetFile?contentID={contentID}")]
        ContentFileDTO ReadContentsById(long contentID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "DeleteEntity?contentID={contentID}")]
        long DeleteEntity(long contentID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveBulkContentFiles?fileDTOs={fileDTOs}")]
        List<ContentFileDTO> SaveBulkContentFiles(List<ContentFileDTO> fileDTOs);


        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
          RequestFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetContentFileList?fileDTOs={fileDTOs}")]
        List<ContentFileDTO> GetContentFileList(List<ContentFileDTO> fileDTOs);
    }
}

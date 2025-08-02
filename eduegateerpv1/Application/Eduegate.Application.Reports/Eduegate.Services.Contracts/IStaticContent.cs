using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts
{
    [ServiceContract]
    public interface IStaticContent
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveStaticContent")]
        StaticContentDTO SaveStaticContent(StaticContentDTO contentDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetStaticContent?contentID={contentID}")]
        StaticContentDTO GetStaticContent(long contentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetStaticContentTypes?staticContentTypes={staticContentTypes}")]
        StaticContentTypeDTO GetStaticContentTypes(Eduegate.Services.Contracts.Enums.StaticContentTypes staticContentTypes);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetStaticContentData?staticContentTypes={staticContentTypes}&pageSize={pageSize}&pageNumber={pageNumber}")]
        List<StaticContentDataDTO> GetStaticContentData(Eduegate.Services.Contracts.Enums.StaticContentTypes staticContentTypes, int pageSize, int pageNumber);
    }
}

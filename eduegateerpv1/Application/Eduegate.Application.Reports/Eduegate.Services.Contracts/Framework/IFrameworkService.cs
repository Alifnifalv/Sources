using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Eduegate.Services.Contracts.Framework
{
    [ServiceContract]
    public interface IFrameworkService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetScreenMetadata?screenID={screenID}")]
        ScreenMetadataDTO GetScreenMetadata(long screenID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetScreenData?screenID={screenID}&IID={IID}")]
        string GetScreenData(long screenID, long IID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveScreenData")]
        ScreenDataDTO SaveScreenData(ScreenDataDTO data);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveCRUDData")]
        CRUDDataDTO SaveCRUDData(CRUDDataDTO data);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "ValidateField?field={field}")]
        KeyValueDTO ValidateField(CRUDDataDTO data, string field);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "DeleteCRUDData?screenID={screenID}&IID={{IID}}")]
        bool DeleteCRUDData(long screenID, long IID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "CloneCRUDData?screenID={screenID}&IID={{IID}}")]
        long CloneCRUDData(long screenID, long IID);
    }   
}

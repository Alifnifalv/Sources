using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDocumentService" in both code and config file together.
    [ServiceContract]
    public interface IDocumentService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetDocuments?referenceID={referenceID}&entityType={entityType}")]
        List<DocumentFileDTO> GetDocuments(long referenceID, EntityTypes entityType);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveDocuments")]
        List<DocumentFileDTO> SaveDocuments(List<DocumentFileDTO> documents);
    }
}

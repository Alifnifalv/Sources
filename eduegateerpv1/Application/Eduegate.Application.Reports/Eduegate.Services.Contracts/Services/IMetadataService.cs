using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Enums.Schedulers;
using Eduegate.Services.Contracts.Metadata;
using Eduegate.Services.Contracts.Search;
using Eduegate.Services.Contracts.Warehouses;

namespace Eduegate.Services.Contracts.Services
{
    [ServiceContract]
    public interface IMetadataService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "FilterMetadata?view={view}")]
        List<FilterColumnDTO> GetFilterMetadata(SearchView view);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveUserFilterMetadata")]
        bool SaveUserFilterMetadata(UserFilterValueDTO value);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetUserFilterMetadata?view={view}")]
        List<Contracts.Metadata.FilterUserValueDTO> GetUserFilterMetadata(Contracts.Enums.SearchView view);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetDocumentType?documentTypeID={documentTypeID}&type={type}")]
        DocumentTypeDTO GetDocumentType(long documentTypeID, string type = null);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveDocumentType")]
        DocumentTypeDTO SaveDocumentType(DocumentTypeDTO familyDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AvailableViewColumns?view={view}")]
        List<ColumnDTO> AvailableViewColumns(SearchView view);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetLocation?locationID={locationID}")]
        LocationDTO GetLocation(long locationID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveLocation")]
        LocationDTO SaveLocation(LocationDTO location);
    }
}

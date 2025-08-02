using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.SearchData;
using search = Eduegate.Services.Contracts.Search;

namespace Eduegate.Services.Contracts
{
    [ServiceContract]
    public interface ISearchService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SearchList?view={view}")]
        search.SearchGridMetadata SearchList(Eduegate.Services.Contracts.Enums.SearchView view);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SearchData?view={view}&currentPage={currentPage}&pageSize={pageSize}&orderBy={orderBy}&runtimeFilter={runtimeFilter}&viewType={viewType}&schoolID={schoolID}&academicYearID={academicYearID}")]
        search.SearchResultDTO SearchData(Eduegate.Services.Contracts.Enums.SearchView view, int currentPage, int pageSize, string orderBy, string runtimeFilter, char viewType = '\0', byte? schoolID=null, int? academicYearID=null);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SearchDataPOST")]
        search.SearchResultDTO SearchDataPOST(search.SearchParameterDTO parameter);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetBlinkKeywordsDictionary?searchtext={searchtext}&lng={lng}&isFallBack={isFallBack}")]
        List<SearchKeywordsDictionaryDTO> GetBlinkKeywordsDictionary(string searchtext, string lng, bool isFallBack);
    }
}

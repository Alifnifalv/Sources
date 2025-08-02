using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Eduegate.Services.Contracts.SearchData
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISearchDataService" in both code and config file together.
    [ServiceContract]
    public interface ISearchDataService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetProductCatalog?searchText={searchText}")]
        SearchResultDTO GetProductCatalog(string searchText);


        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductCatalogPost")]
        SearchResultDTO GetProductCatalogPost(SearchParameterDTO parameter);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetBlinkKeywords?keyword={keyword}&lng={lng}&countryID={countryID}")]
        List<KeywordsDTO> GetBlinkKeywords(string keyword, string lng,int countryID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetBlinkKeywordsAutoSearch?keyword={keyword}")]
        List<KeywordsDTO> GetBlinkKeywordsAutoSearch(string keyword);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetBlinkKeywordsAutoSearchSuggest?keywordtext={keywordtext}")]
        List<KeywordsDTO> GetBlinkKeywordsAutoSearchSuggest(string keywordtext);
    }
}

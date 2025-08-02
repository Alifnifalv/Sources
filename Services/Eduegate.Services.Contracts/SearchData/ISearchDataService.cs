using System.Collections.Generic;

namespace Eduegate.Services.Contracts.SearchData
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISearchDataService" in both code and config file together.
    public interface ISearchDataService
    {
        SearchResultDTO GetProductCatalog(string searchText);

        SearchResultDTO GetProductCatalogPost(SearchParameterDTO parameter);

        List<KeywordsDTO> GetBlinkKeywords(string keyword, string lng,int countryID);

        List<KeywordsDTO> GetBlinkKeywordsAutoSearch(string keyword);

        List<KeywordsDTO> GetBlinkKeywordsAutoSearchSuggest(string keywordtext);
    }
}
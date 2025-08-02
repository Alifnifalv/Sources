using System.Collections.Generic;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.SearchData;

namespace Eduegate.Services.Contracts
{
    public interface ISearchService
    {
        Task<Search.SearchGridMetadata> SearchList(Enums.SearchView view);

        Task<Search.SearchResultDTO> SearchData(Enums.SearchView view, int currentPage, int pageSize, string orderBy, string runtimeFilter, char viewType = '\0', byte? schoolID = null, int? academicYearID = null);

        Task<Search.SearchResultDTO> SearchDataPOST(Search.SearchParameterDTO parameter);

        List<SearchKeywordsDictionaryDTO> GetBlinkKeywordsDictionary(string searchtext, string lng, bool isFallBack);
    }
}
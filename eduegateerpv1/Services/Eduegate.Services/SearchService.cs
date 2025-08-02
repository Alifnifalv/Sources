using Eduegate.Services.Contracts;
using Search = Eduegate.Services.Contracts.Search;
using Eduegate.Domain;
using Eduegate.Services.Contracts.Search;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.SearchData;

namespace Eduegate.Services
{
    public class SearchService : BaseService, ISearchService
    {
        public async Task<SearchGridMetadata> SearchList(Eduegate.Services.Contracts.Enums.SearchView view)
        {
            return await new SearchBL(this.CallContext).SearchList(view);
        }

        public async Task<Search.SearchResultDTO> SearchData(Eduegate.Services.Contracts.Enums.SearchView view, int currentPage, int pageSize, string orderBy, string runtimeFilter, char viewType = '\0',byte? schoolID=null, int? academicYearID=null)
        {
            return await new SearchBL(this.CallContext).SearchData(view, currentPage, pageSize, orderBy, runtimeFilter, viewType, schoolID, academicYearID);
        }

        public List<SearchKeywordsDictionaryDTO> GetBlinkKeywordsDictionary(string searchtext, string lng,bool isFallBack)
        {
            var data = new List<SearchKeywordsDictionaryDTO>();

            //data = await new SearchDataBL(this.CallContext).GetBlinkKeywordsDictionary(searchtext, lng, isFallBack);

            return data;
        }

        public async Task<Search.SearchResultDTO> SearchDataPOST(Eduegate.Services.Contracts.Search.SearchParameterDTO parameter)
        {
            return await new SearchBL(this.CallContext).SearchData(parameter.SearchView, parameter.CurrentPage, parameter.PageSize, parameter.OrderBy, parameter.RuntimeFilter, parameter.ViewType,(byte?) parameter.SchoolID, parameter.AcademicYearID);
        }

    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.SearchData;
using Eduegate.Services;

namespace Eduegate.Service.Client.Direct
{
    public class SearchServiceClient : ISearchService
    {
        SearchService service = new SearchService();

        public SearchServiceClient(CallContext context = null, Action<string> logger = null)
        {
            service.CallContext = context;
        }

        public Task<Services.Contracts.Search.SearchGridMetadata> SearchList(Services.Contracts.Enums.SearchView view)
        {
            return service.SearchList(view);
        }

        public Task<Services.Contracts.Search.SearchResultDTO> SearchData(Services.Contracts.Enums.SearchView view, int currentPage, int pageSize, string orderBy, string runtimeFilter, char viewType, byte? schoolID , int? academicYearID )
        {
            return service.SearchData(view, currentPage, pageSize, orderBy, runtimeFilter, viewType, schoolID, academicYearID);
        }

        public List<SearchKeywordsDictionaryDTO> GetBlinkKeywordsDictionary(string searchtext, string lng, bool isFallBack)
        {
            return service.GetBlinkKeywordsDictionary(searchtext, lng, isFallBack);
        }

        public Task<Services.Contracts.Search.SearchResultDTO> SearchDataPOST(Eduegate.Services.Contracts.Search.SearchParameterDTO parameter)
        {
            return service.SearchDataPOST(parameter);
        }
    }
}

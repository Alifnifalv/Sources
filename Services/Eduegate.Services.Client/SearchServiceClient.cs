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

namespace Eduegate.Service.Client
{
    public class SearchServiceClient : BaseClient, ISearchService
    {
        private static string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string Service = string.Concat(ServiceHost, Eduegate.Framework.Helper.Constants.SEARCH_SERVICE_NAME);

        public SearchServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
        }

        public async Task<Services.Contracts.Search.SearchGridMetadata> SearchList(Services.Contracts.Enums.SearchView view)
        {
            return await Task.FromResult(ServiceHelper.HttpGetRequest<Services.Contracts.Search.SearchGridMetadata>(Service + "SearchList?view=" + view, _callContext));
        }

        public async Task<Services.Contracts.Search.SearchResultDTO> SearchData(Services.Contracts.Enums.SearchView view, int currentPage, int pageSize, string orderBy, string runtimeFilter, char viewType='\0', byte? schoolID=null, int? academicYearID=null)
        {
            return await Task.FromResult(ServiceHelper.HttpGetRequest<Services.Contracts.Search.SearchResultDTO>(Service + "SearchData?view=" + view
                    + "&currentPage=" + currentPage.ToString() + "&pageSize=" + pageSize.ToString() + "&orderBy=" + orderBy + "&runtimeFilter=" + runtimeFilter+ "&viewType="+viewType + "&schoolID=" + schoolID + "&academicYearID=" + academicYearID, _callContext));
        }

        public List<SearchKeywordsDictionaryDTO> GetBlinkKeywordsDictionary(string searchtext, string lng, bool isFallBack)
        {
            return ServiceHelper.HttpGetRequest<List<SearchKeywordsDictionaryDTO>>(Service + "GetBlinkKeywordsDictionary?searchtext=" + searchtext
                    + "&lng=" + lng + "&isFallBack=" + isFallBack, _callContext);
        }

        public async Task<Services.Contracts.Search.SearchResultDTO> SearchDataPOST(Eduegate.Services.Contracts.Search.SearchParameterDTO parameter)
        {
            return await Task.FromResult(JsonConvert.DeserializeObject<Services.Contracts.Search.SearchResultDTO>(ServiceHelper.HttpPostRequest(Service + "SearchDataPOST", parameter, _callContext)));
        }
    }
}

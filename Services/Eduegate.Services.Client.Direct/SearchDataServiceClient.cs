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
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.PageRender;
using Newtonsoft.Json;
namespace Eduegate.Service.Client
{
    public class SearchDataServiceClient : BaseClient, ISearchDataService
    {

        private static string _serviceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string _searchDataService = string.Concat(_serviceHost, Eduegate.Framework.Helper.Constants.SEARCHDATA_SERVICE_NAME);

        public SearchDataServiceClient(CallContext callContext = null, Action<string> logger = null)
            : base(callContext, logger)
        {
        }

        public SearchResultDTO GetProductCatalogPost(SearchParameterDTO parameter)
        {
            var result = ServiceHelper.HttpPostRequest(_searchDataService + "GetProductCatalogPost", parameter, _callContext);
            return JsonConvert.DeserializeObject<SearchResultDTO>(result);
        }

        public SearchResultDTO GetProductCatalog(string searchText)
        {
            return null;
        }

        public List<KeywordsDTO> GetBlinkKeywords(string keyword, string lng, int countryID)
        {
            return null;
        }


        public List<KeywordsDTO> GetBlinkKeywordsAutoSearch(string keyword)
        {
            return null;
        }

        public List<KeywordsDTO> GetBlinkKeywordsAutoSearchSuggest(string keywordtext)
        {
            return null;
        }

    }
}

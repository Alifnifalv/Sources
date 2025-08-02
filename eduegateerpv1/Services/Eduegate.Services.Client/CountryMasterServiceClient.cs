using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Eduegates;

namespace Eduegate.Service.Client
{
    public class CountryMasterServiceClient : BaseClient, ICountryMaster
    {
        private static string serviceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string service = string.Concat(serviceHost, Eduegate.Framework.Helper.Constants.COUNTRY_SERVICE_NAME);
        public CountryMasterServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
        }

        public List<CountryDTO> GetCountries()
        {
            var bannerUri = string.Format("{0}/{1}", service, "GetCountries");
            return ServiceHelper.HttpGetRequest<List<Eduegate.Services.Contracts.CountryDTO>>(bannerUri, _callContext, _logger);
        }

        public List<CountryMasterDTO> GetCountryMasters()
        {
            var bannerUri = string.Format("{0}/{1}", service, "GetCountryMasters");
            return ServiceHelper.HttpGetRequest<List<Eduegate.Services.Contracts.Eduegates.CountryMasterDTO>>(bannerUri, _callContext, _logger);
        }

        public CountryMasterDTO GetCountryDetail(long id)
        {
            var bannerUri = string.Format("{0}/{1}?id={2}", service, "GetCountryDetail", id);
            return ServiceHelper.HttpGetRequest<CountryMasterDTO>(bannerUri, _callContext, _logger);
        }
        public List<CountryDTO> GetCountriesBySiteMap(int siteID)
        {
            var uri = string.Format("{0}/{1}?siteID={2}", service, "GetCountriesBySiteMap", siteID);
            return ServiceHelper.HttpGetRequest<List<Eduegate.Services.Contracts.CountryDTO>>(uri, _callContext, _logger);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Eduegates;

namespace Eduegate.Service.Client.Direct
{
    public class CountryMasterServiceClient : BaseClient, ICountryMaster
    {
        CountryMaster countryService = new CountryMaster();

        public CountryMasterServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
            countryService.CallContext = context;
        }

        public List<CountryDTO> GetCountries()
        {
            return countryService.GetCountries();
        }

        public List<CountryMasterDTO> GetCountryMasters()
        {
            return countryService.GetCountryMasters();
        }

        public CountryMasterDTO GetCountryDetail(long id)
        {
            return countryService.GetCountryDetail(id);
        }
        public List<CountryDTO> GetCountriesBySiteMap(int siteID)
        {
            return countryService.GetCountriesBySiteMap(siteID);
        }
    }
}

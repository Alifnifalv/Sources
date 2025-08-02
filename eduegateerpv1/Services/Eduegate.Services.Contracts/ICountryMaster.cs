using System.Collections.Generic;
using Eduegate.Services.Contracts.Eduegates;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICountryMaster" in both code and config file together.
    public interface ICountryMaster
    {
        List<CountryMasterDTO> GetCountryMasters();

        CountryMasterDTO GetCountryDetail(long id);

        List<CountryDTO> GetCountries();

        List<CountryDTO> GetCountriesBySiteMap(int siteID);
    }
}
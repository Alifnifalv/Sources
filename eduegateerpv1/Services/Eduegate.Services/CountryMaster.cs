using System.ServiceModel;
using Eduegate.Domain;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Eduegates;

namespace Eduegate.Services
{
    public class CountryMaster : BaseService, ICountryMaster
    {
        //Get country masters
        public List<CountryMasterDTO> GetCountryMasters()
        {
            try
            {
                List<CountryMasterDTO> countryMasterDetails = new CountryMasterBL().GetCountryMasters();
                return countryMasterDetails;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Product>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public CountryMasterDTO GetCountryDetail(long id)
        {
            CountryMasterDTO dto = new CountryMasterBL().GetCountryDetail(id);
            return dto;
        }

        public List<CountryDTO> GetCountries()
        {
            return new CountryMasterBL().GetCountries();
        }
        public List<CountryDTO> GetCountriesBySiteMap(int siteID)
        {
            return new CountryMasterBL().GetCountriesBySiteMap(siteID);
        }

    }
}

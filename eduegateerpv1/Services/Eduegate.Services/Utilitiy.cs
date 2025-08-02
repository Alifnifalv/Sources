using System.ServiceModel;
using Eduegate.Domain;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Services;

namespace Eduegate.Services
{
    public class Utilitiy : BaseService, IUtilitiy
    {
        public string GetCurrencyConfiguration(long ipAddress)
        {
            return UtilityBL.GetCurrencyConfiguration(ipAddress);
        }

        public List<LanguageDTO> GetLanguages()
        {
            return UtilityBL.GetLanguages();
        }

        public List<KeyValuePair<string, string>> GetCurrencies(int companyID)
        {
            return UtilityBL.GetCurrencies(companyID);
        }

        //public List<Language1DTO> GetLanguages1()
        //{
        //    return UtilityBL.GetLanguages1();
        //}

        //Get title masters
        public List<PropertyDTO> GetProperties(string propertyTypeIDs)
        {
            try
            {
                List<PropertyDTO> propertyDTOList = new UtilityBL().GetProperties(propertyTypeIDs);
                return propertyDTOList;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Product>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<PropertyDTO> GetPropertyTypes(string propertyTypeIDs)
        {
            try
            {
                List<PropertyDTO> propertyTypes = new UtilityBL().GetPropertyTypes(propertyTypeIDs);
                return propertyTypes;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Product>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public LanguageDTO GetLanguageCultureId(string languageCode)
        {
            return new UtilityBL().GetLanguageCultureId(languageCode);
        }

        public decimal GetExchangeRate(int companyID, string forCurrencyCode)
        {
            return new UtilityBL().GetExchangeRate(companyID, forCurrencyCode);
        }

        public CurrencyDTO GetCurrencyByID(long currencyID)
        {
            return new UtilityBL().GetCurrencyByID(currencyID);
        }
    }
}

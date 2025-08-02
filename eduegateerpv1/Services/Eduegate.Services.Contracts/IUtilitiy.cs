using System.Collections.Generic;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUtilitiy" in both code and config file together.   
    public interface IUtilitiy
    {
        string GetCurrencyConfiguration(long ipAddress);

        List<LanguageDTO> GetLanguages();

        List<KeyValuePair<string, string>> GetCurrencies(int companyID);

        decimal GetExchangeRate(int companyID, string forCurrencyCode);

        List<PropertyDTO> GetProperties(string ids);

        List<PropertyDTO> GetPropertyTypes(string ids);

        LanguageDTO GetLanguageCultureId(string languageCode);

        CurrencyDTO GetCurrencyByID(long currencyID);
    }
}
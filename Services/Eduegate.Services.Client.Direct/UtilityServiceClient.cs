using System;
using System.Collections.Generic;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Services;

namespace Eduegate.Service.Client.Direct
{
    public class UtilityServiceClient : IUtilitiy
    {
        Utilitiy service = new Utilitiy();

        public UtilityServiceClient(CallContext callContext = null, Action<string> logger = null)
        {
            service.CallContext = callContext;
        }

        public string GetCurrencyConfiguration(long ipAddress)
        {
            return service.GetCurrencyConfiguration(ipAddress);
        }

   
        public List<LanguageDTO> GetLanguages()
        {
            return service.GetLanguages();
        }

        public List<PropertyDTO> GetProperties(string ids)
        {
            return service.GetProperties(ids);
        }

        public List<PropertyDTO> GetPropertyTypes(string ids)
        {
            return service.GetPropertyTypes(ids);
        }

        public LanguageDTO GetLanguageCultureId(string languageCode)
        {
            return service.GetLanguageCultureId(languageCode);
        }

        public List<KeyValuePair<string, string>> GetCurrencies(int companyID)
        {
            return service.GetCurrencies(companyID);
        }

        public decimal GetExchangeRate(int companyID, string forCurrencyCode)
        {
            return service.GetExchangeRate(companyID, forCurrencyCode);
        }

        public CurrencyDTO GetCurrencyByID(long currencyID)
        {
            return service.GetCurrencyByID(currencyID);
        }
    }
}

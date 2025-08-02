using System;
using System.Collections.Generic;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Eduegates;

namespace Eduegate.Service.Client
{
    public class UtilityServiceClient : BaseClient, IUtilitiy
    {

        private static string serviceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string utilityService = string.Concat(serviceHost, Eduegate.Framework.Helper.Constants.UTILITY_SERVICE_NAME);

        public UtilityServiceClient(CallContext callContext = null, Action<string> logger = null)
            :base(callContext, logger)
        {

        }

        public string GetCurrencyConfiguration(long ipAddress)
        {
            throw new NotImplementedException();
        }

        public List<LanguageDTO> GetLanguages()
        {
            var uri = string.Format("{0}/{1}", utilityService, "GetLanguages");
            return (ServiceHelper.HttpGetRequest<List<LanguageDTO>>(uri, _callContext, _logger));
        }

        public List<PropertyDTO> GetProperties(string ids)
        {
            var uri = string.Format("{0}/{1}/ids={2}", utilityService, "GetProperties", ids);
            return (ServiceHelper.HttpGetRequest<List<PropertyDTO>>(uri, _callContext, _logger));
        }

        public List<PropertyDTO> GetPropertyTypes(string ids)
        {
            var uri = string.Format("{0}/{1}/{2}", utilityService, "GetPropertyTypes", ids);
            return (ServiceHelper.HttpGetRequest<List<PropertyDTO>>(uri, _callContext, _logger));
        }

        public LanguageDTO GetLanguageCultureId(string languageCode)
        {
            var uri = string.Format("{0}/{1}?languageCode={2}", utilityService, "GetLanguageCultureId", languageCode);
            return (ServiceHelper.HttpGetRequest<LanguageDTO>(uri, _callContext, _logger));
        }

        public List<KeyValuePair<string, string>> GetCurrencies(int companyID)
        {
            throw new NotImplementedException();
        }

        public decimal GetExchangeRate(int companyID, string forCurrencyCode)
        {
            var uri = string.Format("{0}/{1}?companyID={2}&forCurrencyCode={3}", utilityService, "GetExchangeRate", companyID, forCurrencyCode);
            return (ServiceHelper.HttpGetRequest<decimal>(uri, _callContext, _logger));
            
        }

        public CurrencyDTO GetCurrencyByID(long currencyID)
        {
            var uri = string.Format("{0}/{1}?currencyID={2}", utilityService, "GetCurrencyByID", currencyID);
            return (ServiceHelper.HttpGetRequest<CurrencyDTO>(uri, _callContext, _logger));
        }
    }
}

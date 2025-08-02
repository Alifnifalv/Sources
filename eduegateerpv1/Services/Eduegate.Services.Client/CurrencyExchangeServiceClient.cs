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

namespace Eduegate.Service.Client
{
    public class CurrencyExchangeServiceClient : ICurrencyExchange
    {
        private static string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string service = string.Concat(ServiceHost, Eduegate.Framework.Helper.Constants.CURRENCY_EXCHANGE_SERVICE);

        public List<CurrencyExchangeDTO> GetBaseCurrencyCodeList()
        {
            var uri = string.Format("{0}/{1}", service, "GetBaseCurrencyCodeList");
            return ServiceHelper.HttpGetRequest<List<CurrencyExchangeDTO>>(uri);
        }

        public bool UpdateExchangeRates(List<CurrencyExchangeDTO> currencyExchangeRates)
        {
            return bool.Parse(ServiceHelper.HttpPostRequest<List<CurrencyExchangeDTO>>(service + "UpdateExchangeRates", currencyExchangeRates));
        }

    }
}

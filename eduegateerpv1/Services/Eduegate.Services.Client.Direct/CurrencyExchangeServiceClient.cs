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
using Eduegate.Services;

namespace Eduegate.Service.Client
{
    public class CurrencyExchangeServiceClient : BaseClient, ICurrencyExchange
    {
        CurrencyExchange exchangeService = new CurrencyExchange();

        public CurrencyExchangeServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
            exchangeService.CallContext = context;
        }

        public List<CurrencyExchangeDTO> GetBaseCurrencyCodeList()
        {
            return exchangeService.GetBaseCurrencyCodeList();
        }

        public bool UpdateExchangeRates(List<CurrencyExchangeDTO> currencyExchangeRates)
        {
            return exchangeService.UpdateExchangeRates(currencyExchangeRates);
        }
    }
}

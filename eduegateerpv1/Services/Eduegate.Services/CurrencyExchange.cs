using Eduegate.Services.Contracts;
using Eduegate.Domain;
using Eduegate.Framework.Services;

namespace Eduegate.Services
{
    public class CurrencyExchange : BaseService, ICurrencyExchange
    {

        public List<CurrencyExchangeDTO> GetBaseCurrencyCodeList()
        {
            return new CountryMasterBL().GetBaseCurrencyCodes();
        }

        public bool UpdateExchangeRates(List<CurrencyExchangeDTO> currencyExchangeRates)
        {
            return new CountryMasterBL().UpdateExchangeRates(currencyExchangeRates);
        }
        
    }
}

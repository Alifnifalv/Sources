using System.Collections.Generic;

namespace Eduegate.Services.Contracts
{
    public interface ICurrencyExchange
    {
        List<CurrencyExchangeDTO> GetBaseCurrencyCodeList();

        bool UpdateExchangeRates(List<CurrencyExchangeDTO> currencyExchangeRates);
    }
}
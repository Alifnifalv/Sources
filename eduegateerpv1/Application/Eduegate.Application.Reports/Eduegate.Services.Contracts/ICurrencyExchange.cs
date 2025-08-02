using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.IO;

namespace Eduegate.Services.Contracts
{
    [ServiceContract]
    public interface ICurrencyExchange
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetBaseCurrencyCodeList")]
        List<CurrencyExchangeDTO> GetBaseCurrencyCodeList();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdateExchangeRates")]
        bool UpdateExchangeRates(List<CurrencyExchangeDTO> currencyExchangeRates);
    }
}

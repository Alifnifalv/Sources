using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Eduegate.Services.Contracts.Eduegates;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUtilitiy" in both code and config file together.
    [ServiceContract]
    public interface IUtilitiy
    {
        [OperationContract]
        string GetCurrencyConfiguration(long ipAddress);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetLanguages")]
        List<LanguageDTO> GetLanguages();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCurrencies?companyID={companyID}")]
        List<KeyValuePair<string, string>> GetCurrencies(int companyID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetExchangeRate?companyID={companyID}&forCurrencyCode={forCurrencyCode}")]
        decimal GetExchangeRate(int companyID, string forCurrencyCode);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProperties/{ids}")]
        List<PropertyDTO> GetProperties(string ids);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetPropertyTypes/{ids}")]
        List<PropertyDTO> GetPropertyTypes(string ids);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetLanguageCultureId?languageCode={languageCode}")]
        LanguageDTO GetLanguageCultureId(string languageCode);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCurrencyByID?currencyID={currencyID}")]
        CurrencyDTO GetCurrencyByID(long currencyID);
    }
}

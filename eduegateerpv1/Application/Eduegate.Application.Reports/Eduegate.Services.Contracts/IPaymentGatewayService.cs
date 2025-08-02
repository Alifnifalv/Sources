using Eduegate.Services.Contracts.Payments;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPaymentGatewayService" in both code and config file together.
    [ServiceContract]
    public interface IPaymentGatewayService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GenerateCardSessionID")]
        string GenerateCardSessionID();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GenerateCardSessionIDByTransactionNo?transactionNo={transactionNo}")]
        string GenerateCardSessionIDByTransactionNo(string transactionNo);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "PaymentValidation")]
        string PaymentValidation();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "LogPaymentLog")]
        void LogPaymentLog(PaymentLogDTO paymentLog);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "PaymentValidation")]
        string ValidatePaymentByTransaction(string transID, decimal? totalAmountCollected);

    }
}
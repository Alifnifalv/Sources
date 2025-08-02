using System.ServiceModel;
using System.ServiceModel.Web;
using Eduegate.Framework.Payment;
using Eduegate.Services.Contracts;

namespace Eduegate.Services.Payment
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPayment" in both code and config file together.
    [ServiceContract]
    public interface IPayment
    {
        
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "CreatePaymentRequest")]
        bool CreatePaymentRequest(PaymentDTO paymentTransaction);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdatePaymentResponse")]
        bool UpdatePaymentResponse(PaymentDTO paymentTransaction);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetReturnUrl/{trackId}/{gatewayType}")]
        string GetReturnUrl(string trackId, string gatewayType);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetPaymentDetails?orderID={orderID}&track={track}")]
        PaymentDTO GetPaymentDetails(long orderID, string track);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetPayPalPaymentDetail?trackID={trackID}&trackKey={trackKey}&customerID={customerID}")]
        PaypalPaymentDTO GetPayPalPaymentDetail(long trackID, long trackKey, long customerID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetFortCustomerID?trackKey={trackKey}&email={email}")]
        long GetFortCustomerID(string trackKey, string email);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdatePayPalIPNStatus")]
        bool UpdatePayPalIPNStatus(PaypalPaymentDTO paymentDto);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdatePDTData")]
        bool UpdatePDTData(PaypalPaymentDTO paymentDto);

    }
}

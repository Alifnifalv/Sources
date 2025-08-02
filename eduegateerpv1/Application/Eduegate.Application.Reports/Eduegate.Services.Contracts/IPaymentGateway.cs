using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Payment;

namespace Eduegate.Services.Contracts
{
    [ServiceContract]
    public interface IPaymentGateway
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
    }
}

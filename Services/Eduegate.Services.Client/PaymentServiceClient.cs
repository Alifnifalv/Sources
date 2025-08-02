using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Payment;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Services.Payment;

namespace Eduegate.Service.Client
{
    public class PaymentServiceClient : BaseClient, IPayment
    {
        private static string serviceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string paymentService = string.Concat(serviceHost, Eduegate.Framework.Helper.Constants.GLOBALPAYMENT_SERVICE_NAME);

        public PaymentServiceClient(CallContext callContext = null, Action<string> logger = null)
            :base(callContext, logger)
        {

        }

        public bool CreatePaymentRequest(PaymentDTO paymentTransaction)
        {
            throw new NotImplementedException();
        }

        public PaymentDTO GetPaymentDetails(long orderID)
        {
            var uri = string.Format("{0}/{1}?orderID={2}", paymentService, "GetPaymentDetails", orderID);
            return (ServiceHelper.HttpGetRequest<PaymentDTO>(uri, _callContext, _logger));
        }

        public string GetReturnUrl(string trackId, string gatewayType)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePaymentResponse(PaymentDTO paymentTransaction)
        {
            throw new NotImplementedException();
        }

        public PaymentDTO GetPaymentDetails(long orderID, string track)
        {
            var uri = string.Format("{0}/{1}?orderID={2}&track={3}", paymentService, "GetPaymentDetails", orderID, track);
            return (ServiceHelper.HttpGetRequest<PaymentDTO>(uri, _callContext, _logger));
        }

        public long GetFortCustomerID(string trackKey, string email)
        {
            var uri = string.Format("{0}/{1}?trackKey={2}&email={3}", paymentService, "GetFortCustomerID", trackKey, email);
            return (ServiceHelper.HttpGetRequest<long>(uri, _callContext, _logger));
        }


        public Services.Contracts.PaypalPaymentDTO GetPayPalPaymentDetail(long trackID, long trackKey, long customerID)
        {
            var uri = string.Format("{0}/{1}?trackID={2}&trackKey={3}&customerID={4}", paymentService, "GetPayPalPaymentDetail", trackID,trackKey, customerID);
            return (ServiceHelper.HttpGetRequest<Services.Contracts.PaypalPaymentDTO>(uri, _callContext, _logger));
        }

        public bool UpdatePayPalIPNStatus(PaypalPaymentDTO paymentDto)
        {
            string serviceResult = ServiceHelper.HttpPostRequest(string.Concat(paymentService, "UpdatePayPalIPNStatus"), paymentDto);
            return JsonConvert.DeserializeObject<bool>(serviceResult);
        }

        public bool UpdatePDTData(PaypalPaymentDTO paymentDto)
        {
            string serviceResult = ServiceHelper.HttpPostRequest(string.Concat(paymentService, "UpdatePDTData"), paymentDto);
            return JsonConvert.DeserializeObject<bool>(serviceResult);
        }


        
    }
}

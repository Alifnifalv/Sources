using System.ServiceModel;
using Eduegate.Domain.Payment;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;

namespace Eduegate.Services.Payment
{
    public class Payment : BaseService, IPayment
    {
        Payment paymentBL;

        public Payment()
        {
            //TODO: Check if this is needed
            //paymentBL = new PaymentBL(CallContext);
        }

        public bool CreatePaymentRequest(PaymentDTO paymentTransaction)
        {
            return paymentBL.CreatePaymentRequest(paymentTransaction);
        }

        public bool UpdatePaymentResponse(PaymentDTO paymentTransaction)
        {
            return paymentBL.UpdatePaymentResponse(paymentTransaction);
        }

        public string GetReturnUrl(string trackId, string gatewayType)
        {
            throw new NotImplementedException();
        }

        public PaymentDTO GetPaymentDetails(long orderID, string track)
        {
            try
            {
                var paymentDetail = new PaymentDTO();
                Eduegate.Logger.LogHelper<Payment>.Info("GetPaymentDetails called in PaymentBL. OrderID:" + orderID + " track:" + track);

                //TODO: Check if this is needed
                //paymentDetail = new PaymentBL(CallContext).GetPaymentDetails(orderID, track);


                Eduegate.Logger.LogHelper<Payment>.Info("GetPaymentDetails : " + Convert.ToString(paymentDetail));
                return paymentDetail;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Payment>.Fatal("GetPaymentDetails failed. error msg:" + exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public PaypalPaymentDTO GetPayPalPaymentDetail(long trackID, long trackKey, long customerID)
        {
            try
            {
                var paymentDetail = new PaymentBL(CallContext).GetPayPalPaymentDetail(trackID, trackKey, customerID);
                return paymentDetail;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Payment>.Fatal("GetPayPalPaymentDetail failed. error msg:" + exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public long GetFortCustomerID(string trackKey, string email)
        {
            try
            {
                Eduegate.Logger.LogHelper<Payment>.Info("GetFortCustomerID called in PaymentBL. trackKey:" + trackKey + " email:" + email);
                var customerID = new PaymentBL(CallContext).GetFortCustomerID(trackKey, email);
                Eduegate.Logger.LogHelper<Payment>.Info("GetFortCustomerID : " + Convert.ToString(customerID));
                return customerID;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Payment>.Fatal("GetFortCustomerID failed. error msg:" + exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool UpdatePayPalIPNStatus(PaypalPaymentDTO paymentDto)
        {
            try
            {
                Eduegate.Logger.LogHelper<Payment>.Info("UpdatePayPalIPNStatus called in PaymentBL.");
                var result = new PaymentBL(CallContext).UpdatePayPalIPNStatus(paymentDto);
                Eduegate.Logger.LogHelper<Payment>.Info("UpdatePayPalIPNStatus : " + Convert.ToString(result));
                return result;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Payment>.Fatal("UpdatePayPalIPNStatus failed. error msg:" + exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool UpdatePDTData(PaypalPaymentDTO paymentDto)
        {
            try
            {
                Eduegate.Logger.LogHelper<Payment>.Info("UpdatePDTData called in PaymentBL.");
                var result = new PaymentBL(CallContext).UpdatePDTData(paymentDto);
                Eduegate.Logger.LogHelper<Payment>.Info("UpdatePDTData : " + Convert.ToString(result));
                return result;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Payment>.Fatal("UpdatePDTData failed. error msg:" + exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
    }
}

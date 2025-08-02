using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContract =  Eduegate.Services.Contracts;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Framework.Logs;
using Eduegate.Framework.Extensions;
using Eduegate.Framework;
using Eduegate.Framework.Payment;
using Newtonsoft.Json;

namespace Eduegate.Domain
{
    public class PaymentBL
    {
        private PaymentRepository paymentRepository = new PaymentRepository();
        public bool CreatePaymentRequest(PaymentDTO payment)
        {
            bool status = false;
            switch (payment.PaymentGateway)
            {
                case PaymentGatewayType.KNET:
                    //KNETPaymentDTO knetPaymentDTO = Task.Factory.StartNew(() => JsonConvert.DeserializeObject<KNETPaymentDTO>(payment.CustomAttributes)).Result;
                    var knetPaymentDTO = JsonConvert.DeserializeObject<KNETPaymentDTO>(payment.CustomAttributes);
                    PaymentDetail paymentDetail = new PaymentDetail()
                    {
                        TrackID = Convert.ToInt64(payment.TrackID),
                        TrackKey = Convert.ToInt64(payment.TrackKey),
                        RefCustomerID = Convert.ToInt64(payment.CustomerID),
                        SessionID = payment.SessionID,
                        PaymentID = Convert.ToInt64(payment.PaymentID),
                        InitOn = DateTime.UtcNow,
                        InitStatus = payment.TransactionStatus,
                        InitIP = payment.InitiatedFromIP,
                        InitLocation = payment.InitiatedLocation,
                        PaymentAmount = Convert.ToDecimal(payment.Amount),
                        PaymentAction = knetPaymentDTO.PaymentAction,
                        PaymentCurrency = knetPaymentDTO.PaymentCurrency,
                        PaymentLang = knetPaymentDTO.PaymentLang,
                        InitPaymentPage = payment.PaymentGatewayUrl

                    };

                    status = paymentRepository.MakeKNETPaymentEntry(paymentDetail);
                    break;
                case PaymentGatewayType.MIGS:
                    ServiceContract.MIGSPaymentDTO migsPaymentDTO = JsonConvert.DeserializeObject<ServiceContract.MIGSPaymentDTO>(payment.CustomAttributes);
                    PaymentDetailsMasterVisa masterVisa = new PaymentDetailsMasterVisa()
                    {
                        SessionID = payment.SessionID,
                        RefCustomerID = Convert.ToInt64(payment.CustomerID),
                        TrackID = Convert.ToInt64(payment.TrackID),
                        TrackKey = Convert.ToInt64(payment.TrackKey),
                        PaymentID = Convert.ToInt64(payment.PaymentID),
                        InitOn = DateTime.UtcNow,
                        InitStatus = payment.TransactionStatus,
                        InitIP = payment.InitiatedFromIP,
                        InitLocation = payment.InitiatedLocation,
                        VpcURL = payment.PaymentGatewayUrl,
                        VpcVersion = migsPaymentDTO.VpcVersion,
                        VpcCommand = migsPaymentDTO.VpcCommand,
                        AccessCode = migsPaymentDTO.AccessCode,
                        MerchantID = migsPaymentDTO.MerchantID,
                        VpcLocale = migsPaymentDTO.VpcLocale,
                        PaymentAmount = Convert.ToDecimal(payment.Amount),
                        VirtualAmount = Convert.ToInt32(Convert.ToDecimal(payment.Amount) * 1000),
                        PaymentCurrency = migsPaymentDTO.PaymentCurrency,
                    };
                    status = paymentRepository.MakeMIGSPaymentEntry(masterVisa);
                    break;
                case PaymentGatewayType.PAYPAL:
                    ServiceContract.PaypalPaymentDTO payaplPaymentDTO = JsonConvert.DeserializeObject<ServiceContract.PaypalPaymentDTO>(payment.CustomAttributes);
                    PaymentDetailsPayPal paypal = new PaymentDetailsPayPal()
                    {
                        //SessionID = payment.SessionID,
                        RefCustomerID = Convert.ToInt64(payment.CustomerID),
                        TrackID = Convert.ToInt64(payment.TrackID),
                        TrackKey = Convert.ToInt64(payment.TrackKey),
                        PaymentID = Convert.ToInt64(payment.PaymentID),
                        InitOn = DateTime.UtcNow,
                        InitStatus = payment.TransactionStatus,
                        InitIP = payment.InitiatedFromIP,
                        InitLocation = payment.InitiatedLocation,
                        BusinessEmail = payment.EmailId,
                        InitAmount = Convert.ToDecimal(payment.Amount),
                        InitCurrency = payaplPaymentDTO.InitCurrency,
                        ExRateUSD = Convert.ToDecimal(payaplPaymentDTO.ExRateUSD),
                        InitAmountUSDActual = Convert.ToDouble(payaplPaymentDTO.InitAmountUSDActual),
                        InitAmountUSD = Convert.ToDecimal(payaplPaymentDTO.InitAmountUSD),
                        IpnVerificationRequired = Convert.ToBoolean(payaplPaymentDTO.IpnVerificationRequired),
                        InitCartTotalUSD = Convert.ToDecimal(payaplPaymentDTO.InitCartTotalUSD),

                    };
                    status = paymentRepository.MakePayPalPaymentEntry(paypal);
                    break;
            }

            return status;

        }

        public bool UpdatePaymentResponse(PaymentDTO payment)
        {
            bool status = false;
            PaymentDetail paymentDetail = default(PaymentDetail);
            switch (payment.PaymentGateway)
            {
                case PaymentGatewayType.KNET:
                    KNETPaymentDTO knetPaymentDTO = JsonConvert.DeserializeObject<KNETPaymentDTO>(payment.CustomAttributes);
                    paymentDetail = new PaymentDetail()
                    {
                        TrackID = payment.TrackID.IsNotNullOrEmpty() ? Convert.ToInt64(payment.TrackID) : default(long),
                        TrackKey = payment.TrackKey.IsNotNullOrEmpty() ? Convert.ToInt64(payment.TrackKey) : default(long),
                        RefCustomerID = payment.CustomerID.IsNotNullOrEmpty() ? Convert.ToInt64(payment.CustomerID) : default(long),
                        SessionID = payment.SessionID,
                        PaymentID = payment.PaymentID.IsNotNullOrEmpty() ? Convert.ToInt64(payment.PaymentID) : default(long),
                        InitStatus = payment.TransactionStatus,
                        TransID = knetPaymentDTO.TransID.IsNotNullOrEmpty() ? Convert.ToInt64(knetPaymentDTO.TransID) : default(long),
                        TransResult = knetPaymentDTO.TransResult,
                        TransPostDate = knetPaymentDTO.TransPostDate,
                        TransAuth = knetPaymentDTO.TransAuth,
                        TransRef = knetPaymentDTO.TransRef,
                        TransIP = payment.InitiatedFromIP,
                        TransLocation = payment.InitiatedLocation

                    };
                    if (paymentDetail.InitStatus == "7") //Log to error table
                    {
                        PaymentDetailsLog paymentErrorLog = new PaymentDetailsLog()
                        {
                            TrackID = payment.TrackID.IsNotNullOrEmpty()? Convert.ToInt64(payment.TrackID):default(long),
                            TrackKey = payment.TrackKey.IsNotNullOrEmpty() ? Convert.ToInt64(payment.TrackKey) : default(long),
                            CustomerID = payment.CustomerID.IsNotNullOrEmpty() ? Convert.ToInt64(payment.CustomerID) : default(long),
                            CustomerSessionID = payment.SessionID,
                            PaymentID = Convert.ToInt64(payment.PaymentID),
                            TransID = knetPaymentDTO.TransID.IsNotNullOrEmpty() ? Convert.ToInt64(knetPaymentDTO.TransID) : default(long),
                            TransResult = knetPaymentDTO.TransResult,
                            TransPostDate = knetPaymentDTO.TransPostDate,
                            TransAuth = knetPaymentDTO.TransAuth,
                            TransRef = knetPaymentDTO.TransRef,
                        };
                        var errorLogResult = Task<bool>.Factory.StartNew(() => paymentRepository.MakePaymentErrorEntry(paymentErrorLog));
                    }
                    status = paymentRepository.UpdateKNETPaymentEntry(paymentDetail);
                    break;

            }

            return status;

        }

        public string GetReturnUrl(string trackId, string gatewayType)
        {
            return paymentRepository.GetReturnUrl(trackId, gatewayType);
        }
    }
}

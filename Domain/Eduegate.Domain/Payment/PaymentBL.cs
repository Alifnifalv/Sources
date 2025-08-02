using System;
using System.Threading.Tasks;
using ServiceContract = Eduegate.Services.Contracts;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Framework.Logs;
using Eduegate.Framework.Extensions;
using Newtonsoft.Json;
using System.Linq;
using Eduegate.Domain.Mappers.Payments;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Framework.Payment;
using Eduegate.Services.Contracts.Payments;
using Eduegate.Framework;

namespace Eduegate.Domain.Payment
{
    public class PaymentBL
    {
        private Repository.Payment.PaymentRepository paymentRepository = new Repository.Payment.PaymentRepository();

        private Eduegate.Framework.CallContext _callContext { get; set; }

        public PaymentBL(CallContext callContext)
        {
            _callContext = callContext;
        }

        public bool CreatePaymentRequest(PaymentDTO payment)
        {
            bool status = false;
            switch (payment.PaymentGateway)
            {
                case ServiceContract.Enums.PaymentGatewayType.KNET:
                    #region KNET
                    //KNETPaymentDTO knetPaymentDTO = Task.Factory.StartNew(() => JsonConvert.DeserializeObject<KNETPaymentDTO>(payment.CustomAttributes)).Result;
                    var knetPaymentDTO = JsonConvert.DeserializeObject<Eduegate.Services.Contracts.KNETPaymentDTO>(payment.CustomAttributes);
                    var paymentDetail = new PaymentDetailsKnet()
                    {
                        TrackID = Convert.ToInt64(payment.TrackID),
                        TrackKey = Convert.ToInt64(payment.TrackKey),
                        CustomerID = Convert.ToInt64(payment.CustomerID),

                        PaymentID = Convert.ToInt64(payment.PaymentID),
                        InitOn = DateTime.UtcNow,
                        InitStatus = payment.TransactionStatus,
                        InitIP = payment.InitiatedFromIP,
                        InitLocation = payment.InitiatedLocation,
                        PaymentAmount = Convert.ToDecimal(payment.Amount),
                        PaymentAction = knetPaymentDTO.PaymentAction,
                        PaymentCurrency = knetPaymentDTO.PaymentCurrency,
                        PaymentLang = knetPaymentDTO.PaymentLang,
                        InitPaymentPage = payment.PaymentGatewayUrl,
                        CartID = Convert.ToInt64(payment.CartID),
                        AppKey = payment.AppKey.IsNotNullOrEmpty() ? Convert.ToInt64(payment.AppKey) : default(long?)
                    };

                    status = paymentRepository.MakeKNETPaymentEntry(paymentDetail);
                    #endregion
                    break;
                case ServiceContract.Enums.PaymentGatewayType.MIGS:
                    #region MIGS
                    Eduegate.Services.Contracts.MIGSPaymentDTO migsPaymentDTO = JsonConvert.DeserializeObject<Eduegate.Services.Contracts.MIGSPaymentDTO>(payment.CustomAttributes);

                    PaymentMasterVisa masterVisa = new PaymentMasterVisa()
                    {
                        CustomerID = Convert.ToInt64(payment.CustomerID),
                        TrackIID = Convert.ToInt64(payment.TrackID),
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
                        CartID = Convert.ToInt64(payment.CartID),
                    };
                    status = paymentRepository.MakeMIGSPaymentEntry(masterVisa);
                    #endregion
                    break;
                case ServiceContract.Enums.PaymentGatewayType.PAYPAL:
                    #region PAYPAL
                    ServiceContract.PaypalPaymentDTO paypalDTO = JsonConvert.DeserializeObject<ServiceContract.PaypalPaymentDTO>(payment.CustomAttributes);
                    PaymentDetailsPayPal paypal = new PaymentDetailsPayPal()
                    {
                        // From PaymentDTO
                        RefCustomerID = Convert.ToInt64(payment.CustomerID),
                        TrackID = Convert.ToInt64(payment.TrackID),
                        TrackKey = Convert.ToInt64(payment.TrackKey),
                        PaymentID = Convert.ToInt64(payment.PaymentID),
                        InitStatus = payment.TransactionStatus,

                        InitOn = DateTime.Now,

                        // From PaypalDTO
                        InitIP = paypalDTO.InitIP,
                        BusinessEmail = paypalDTO.BusinessEmail,
                        InitLocation = paypalDTO.InitLocation,
                        InitAmount = Convert.ToDecimal(paypalDTO.InitAmount),
                        InitCurrency = paypalDTO.InitCurrency,
                        ExRateUSD = Convert.ToDecimal(paypalDTO.ExRateUSD),
                        InitAmountUSDActual = Convert.ToDouble(paypalDTO.InitAmountUSDActual),
                        InitAmountUSD = Convert.ToDecimal(paypalDTO.InitAmountUSD),
                        IpnVerificationRequired = Convert.ToBoolean(paypalDTO.IpnVerificationRequired),
                        InitCartTotalUSD = Convert.ToDecimal(paypalDTO.InitCartTotalUSD),
                        CartID = paypalDTO.CartID,

                    };
                    status = paymentRepository.LogPayPalPaymentRequest(paypal); // TODO
                    #endregion
                    break;
                case ServiceContract.Enums.PaymentGatewayType.THEFORT:
                    #region THEFORT
                    var fortDTO = JsonConvert.DeserializeObject<ServiceContract.PaymentGateway.TheFortPaymentDTO>(payment.CustomAttributes);
                    PaymentDetailsTheFort fort = new PaymentDetailsTheFort()
                    {
                        // From PaymentDTO
                        CustomerID = Convert.ToInt64(payment.CustomerID),
                        TrackID = Convert.ToInt64(payment.TrackID),
                        TrackKey = Convert.ToInt64(payment.TrackKey),
                        PaymentID = Convert.ToInt64(payment.PaymentID),
                        InitStatus = payment.TransactionStatus,
                        InitOn = DateTime.Now, // this should give application server's current datetime irrespective of user's timezone.
                        InitIP = payment.InitiatedFromIP,
                        InitLocation = payment.InitiatedLocation,
                        InitAmount = Convert.ToDecimal(payment.Amount),

                        // From fortDTO
                        PShaRequestPhrase = fortDTO.PShaRequestPhrase,
                        PAccessCode = fortDTO.PAccessCode,
                        PMerchantIdentifier = fortDTO.PMerchantIdentifier,
                        PCommand = fortDTO.PCommand,
                        PCurrency = fortDTO.PCurrency,
                        PCustomerEmail = fortDTO.PCustomerEmail,
                        PLang = fortDTO.PLang,
                        PMerchantReference = fortDTO.PMerchantReference,
                        PSignatureText = fortDTO.PSignatureText,
                        PSignature = fortDTO.PSignature,
                        PAmount = fortDTO.PAmount,
                        PTransAmount = fortDTO.PTransAmount,
                        RefCountryID = fortDTO.RefCountryID,
                        CartID = fortDTO.CartID,
                        AdditionalDetails = fortDTO.AdditionalDetails,

                    };
                    status = paymentRepository.LogTheFortPaymentRequest(fort);
                    break;
                    #endregion
            }
            return status;
        }

        public PaymentDTO GetPaymentDetails(long orderID, string track = null)
        {

            var paymentGatewayType = new ServiceContract.Enums.PaymentGatewayType();
            var trackID = default(long);
            var paymentDTO = new PaymentDTO();

            if (orderID > 0)
            {
                paymentGatewayType = (ServiceContract.Enums.PaymentGatewayType)Enum.Parse(typeof(ServiceContract.Enums.PaymentGatewayType), new UserServiceBL(_callContext).GetOrderHistoryDetails("0", 0, 0, "0", null, orderID, false).FirstOrDefault().PaymentMethod);
            }
            else if (track.IsNotNullOrEmpty() && track.IndexOf('_') > 0)
            {
                //This method expect client to send TrackID_PaymentGateway(321321321_1) as track
                trackID = Convert.ToInt64(track.Split('_')[0]);
                paymentGatewayType = (ServiceContract.Enums.PaymentGatewayType)Enum.Parse(typeof(ServiceContract.Enums.PaymentGatewayType), track.Split('_')[1]);
            }
            else
            {
                return null;
            }


            // Get payment details if payment gateways has knet/migs included in it.
            switch (paymentGatewayType)
            {
                case ServiceContract.Enums.PaymentGatewayType.KNET:
                    var paymentDetailKNET = paymentRepository.GetPaymentDetailKNET(orderID, trackID);
                    paymentDTO.PaymentID = paymentDetailKNET.PaymentID.ToString();
                    paymentDTO.Amount = paymentDetailKNET.PaymentAmount.ToString();
                    paymentDTO.TrackID = paymentDetailKNET.TrackID.ToString();
                    paymentDTO.TrackKey = paymentDetailKNET.TrackKey.IsNotNull() ? paymentDetailKNET.TrackKey.ToString() : string.Empty;
                    paymentDTO.InitiatedOn = paymentDetailKNET.InitOn.ToString();
                    paymentDTO.ErrorMessage = paymentDetailKNET.TransResult;
                    paymentDTO.CartID = paymentDetailKNET.CartID.IsNotNull() ? paymentDetailKNET.CartID.ToString() : string.Empty;
                    break;
                case ServiceContract.Enums.PaymentGatewayType.MIGS:
                    var paymentDetailMIGS = paymentRepository.GetPaymentDetailMIGS(orderID, trackID);
                    paymentDTO.PaymentID = paymentDetailMIGS.PaymentID.ToString();
                    paymentDTO.Amount = paymentDetailMIGS.PaymentAmount.ToString();
                    paymentDTO.TrackID = paymentDetailMIGS.TrackIID.ToString();
                    paymentDTO.InitiatedOn = paymentDetailMIGS.InitOn.ToString();
                    paymentDTO.TrackKey = paymentDetailMIGS.TrackKey.ToString();
                    paymentDTO.ErrorMessage = paymentDetailMIGS.CodeDescription.IsNotNull() ? paymentDetailMIGS.CodeDescription.ToString() : string.Empty;
                    break;
                case ServiceContract.Enums.PaymentGatewayType.PAYPAL:
                    var paymentDetailPayPal = paymentRepository.GetPaymentDetailPaypal(orderID, trackID);
                    paymentDTO.PaymentID = paymentDetailPayPal.PaymentID.ToString();
                    paymentDTO.Amount = paymentDetailPayPal.InitAmount.ToString();
                    paymentDTO.TrackID = paymentDetailPayPal.TrackID.ToString();
                    paymentDTO.InitiatedOn = paymentDetailPayPal.InitOn.ToString();
                    paymentDTO.TrackKey = paymentDetailPayPal.TrackKey.ToString();
                    paymentDTO.ErrorMessage = paymentDetailPayPal.TransStatus.IsNotNull() && paymentDetailPayPal.TransStatus == Convert.ToString((int)Eduegate.Framework.Payment.TransactionStatus.Success) ? "" : "Transaction Cancelled";

                    break;

                case ServiceContract.Enums.PaymentGatewayType.THEFORT:
                    var paymentDetailfort = paymentRepository.GetPaymentDetailTheFort(orderID, trackID);
                    paymentDTO.PaymentID = paymentDetailfort.TransID.IsNotNull() ? paymentDetailfort.TransID : string.Empty;
                    paymentDTO.Amount = paymentDetailfort.InitAmount.ToString();
                    paymentDTO.TrackID = paymentDetailfort.TrackID.ToString();
                    paymentDTO.InitiatedOn = paymentDetailfort.InitOn.ToString();
                    paymentDTO.TrackKey = paymentDetailfort.TrackKey.ToString();
                    paymentDTO.ErrorMessage = paymentDetailfort.PTransResponseMessage.IsNotNull() ? paymentDetailfort.PTransResponseMessage.ToString() : "";
                    paymentDTO.OrderID = paymentDetailfort.OrderID.IsNotNull() ? Convert.ToString(paymentDetailfort.OrderID) : string.Empty;
                    paymentDTO.CartID = paymentDetailfort.CartID.IsNotNull() ? Convert.ToString(paymentDetailfort.CartID) : string.Empty;

                    break;
                case ServiceContract.Enums.PaymentGatewayType.QPAY:
                    var paymentDetailQPAY = paymentRepository.GetPaymentDetailMIGS(orderID, trackID);
                    paymentDTO.PaymentID = paymentDetailQPAY.PaymentID.ToString();
                    paymentDTO.Amount = paymentDetailQPAY.PaymentAmount.ToString();
                    paymentDTO.TrackID = paymentDetailQPAY.TrackIID.ToString();
                    paymentDTO.InitiatedOn = paymentDetailQPAY.InitOn.ToString();
                    paymentDTO.TrackKey = paymentDetailQPAY.TrackKey.ToString();
                    paymentDTO.ErrorMessage = paymentDetailQPAY.CodeDescription.IsNotNull() ? paymentDetailQPAY.CodeDescription.ToString() : string.Empty;
                    break;
                //case PaymentGatewayType.VOUCHER:
                //    break;
                //case PaymentGatewayType.COD:
                //    break;
                default:
                    paymentDTO = null;
                    break;
            }
            return paymentDTO;
        }

        public long GetKnetDetails(long appKey)
        {
            return paymentRepository.GetKnetDetails(appKey);
        }

        public bool UpdatePaymentResponse(PaymentDTO payment)
        {
            bool status = false;
            //var paymentDetail = default(PaymentDetailsKnet);
            switch (payment.PaymentGateway)
            {
                case ServiceContract.Enums.PaymentGatewayType.KNET:
                    #region KNET
                    Eduegate.Services.Contracts.KNETPaymentDTO knetPaymentDTO = JsonConvert.DeserializeObject<Eduegate.Services.Contracts.KNETPaymentDTO>(payment.CustomAttributes);
                    var paymentDetailKnet = new PaymentDetailsKnet()
                    {
                        TrackID = payment.TrackID.IsNotNullOrEmpty() ? Convert.ToInt64(payment.TrackID) : default(long),
                        TrackKey = payment.TrackKey.IsNotNullOrEmpty() ? Convert.ToInt64(payment.TrackKey) : default(long),
                        CustomerID = payment.CustomerID.IsNotNullOrEmpty() ? Convert.ToInt64(payment.CustomerID) : default(long),
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
                    //if (paymentDetailKnet.InitStatus == ((int)Eduegate.Framework.Payment.TransactionStatus.Error).ToString()) //Log to error table
                    //{
                    //    PaymentDetailsLog paymentErrorLog = new PaymentDetailsLog()
                    //    {
                    //        TrackID = payment.TrackID.IsNotNullOrEmpty() ? Convert.ToInt64(payment.TrackID) : default(long),
                    //        TrackKey = payment.TrackKey.IsNotNullOrEmpty() ? Convert.ToInt64(payment.TrackKey) : default(long),
                    //        CustomerID = payment.CustomerID.IsNotNullOrEmpty() ? Convert.ToInt64(payment.CustomerID) : default(long),
                    //        CustomerSessionID = payment.SessionID,
                    //        PaymentID = Convert.ToInt64(payment.PaymentID),
                    //        TransID = knetPaymentDTO.TransID.IsNotNullOrEmpty() ? Convert.ToInt64(knetPaymentDTO.TransID) : default(long),
                    //        TransResult = knetPaymentDTO.TransResult,
                    //        TransPostDate = knetPaymentDTO.TransPostDate,
                    //        TransAuth = knetPaymentDTO.TransAuth,
                    //        TransRef = knetPaymentDTO.TransRef,
                    //    };
                    //    var errorLogResult = Task<bool>.Factory.StartNew(() => paymentRepository.MakePaymentErrorEntry(paymentErrorLog));
                    //}
                    status = paymentRepository.UpdateKNETPaymentEntry(paymentDetailKnet);
                    #endregion
                    break;
                case ServiceContract.Enums.PaymentGatewayType.MIGS:
                    #region MIGS
                    Eduegate.Services.Contracts.MIGSPaymentDTO migsPayment = JsonConvert.DeserializeObject<Eduegate.Services.Contracts.MIGSPaymentDTO>(payment.CustomAttributes);
                    var paymentDetailMigs = new PaymentMasterVisa()
                    {
                        TrackIID = payment.TrackID.IsNotNullOrEmpty() ? Convert.ToInt64(payment.TrackID) : default(long),
                        TrackKey = payment.TrackKey.IsNotNullOrEmpty() ? Convert.ToInt64(payment.TrackKey) : default(long),
                        CustomerID = payment.CustomerID.IsNotNullOrEmpty() ? Convert.ToInt64(payment.CustomerID) : default(long),
                        PaymentID = payment.PaymentID.IsNotNullOrEmpty() ? Convert.ToInt64(payment.PaymentID) : default(long),
                        InitStatus = payment.TransactionStatus,
                        TransID = Convert.ToString(migsPayment.TransID),
                        //TransResult = knetPaymentDTO.TransResult,
                        //TransPostDate = knetPaymentDTO.TransPostDate,
                        //TransAuth = knetPaymentDTO.TransAuth,
                        //TransRef = knetPaymentDTO.TransRef,
                        InitIP = payment.InitiatedFromIP,
                        InitLocation = payment.InitiatedLocation,
                        AcquireResponseCode = migsPayment.AcquireResponseCode,
                        Message = migsPayment.Message.IsNotNullOrEmpty() ? migsPayment.Message : "",
                        //ResponseIP = migsPayment.ResponseIP,
                        ResponseOn = migsPayment.ResponseOn.IsNotNullOrEmpty() ? Convert.ToDateTime(migsPayment.ResponseOn) : default(DateTime),
                        ResponseCode = migsPayment.ResponseCode,
                        CodeDescription = migsPayment.CodeDescription,
                        ReceiptNumber = migsPayment.ReceiptNumber.IsNotNullOrEmpty() ? migsPayment.ReceiptNumber : "",
                        VpcCommand = migsPayment.VpcCommand,
                        //VpcURL = migsPayment.VpcURL,
                        VpcLocale = migsPayment.VpcLocale,
                        VpcVersion = migsPayment.VpcVersion,
                        MerchantID = migsPayment.MerchantID,
                        PaymentCurrency = migsPayment.PaymentCurrency,
                        PaymentAmount = Convert.ToDecimal(payment.Amount),
                        CardType = migsPayment.CardType,
                        BatchNo = migsPayment.BatchNo,
                        BankAuthorizationID = migsPayment.BankAuthorizationID,

                    };
                    //if (paymentDetailMigs.InitStatus == ((int)Eduegate.Framework.Payment.TransactionStatus.Error).ToString()) //Log to error table
                    //{
                    //    PaymentDetailsLog paymentErrorLog = new PaymentDetailsLog()
                    //    {
                    //        TrackID = payment.TrackID.IsNotNullOrEmpty() ? Convert.ToInt64(payment.TrackID) : default(long),
                    //        TrackKey = payment.TrackKey.IsNotNullOrEmpty() ? Convert.ToInt64(payment.TrackKey) : default(long),
                    //        CustomerID = payment.CustomerID.IsNotNullOrEmpty() ? Convert.ToInt64(payment.CustomerID) : default(long),
                    //        CustomerSessionID = payment.SessionID,
                    //        PaymentID = Convert.ToInt64(payment.PaymentID),
                    //        TransID = paymentDetailMigs.TransID.IsNotNullOrEmpty() ? Convert.ToInt64(paymentDetailMigs.TransID) : default(long),
                    //        //TransResult = paymentDetailMigs.TransRetrasult,
                    //        TransPostDate = Convert.ToString(paymentDetailMigs.InitOn),
                    //        //TransAuth = paymentDetailMigs.TransAuth,
                    //        //TransRef = paymentDetailMigs.TransRef,
                    //    };
                    //    var errorLogResult = Task<bool>.Factory.StartNew(() => paymentRepository.MakePaymentErrorEntry(paymentErrorLog));
                    //}
                    status = paymentRepository.UpdateMIGSPaymentEntry(paymentDetailMigs);
                    #endregion
                    break;
                case ServiceContract.Enums.PaymentGatewayType.PAYPAL:
                    #region PAYPAL
                    ServiceContract.PaypalPaymentDTO paypalDTO = JsonConvert.DeserializeObject<ServiceContract.PaypalPaymentDTO>(payment.CustomAttributes);



                    PaymentDetailsPayPal paypal = new PaymentDetailsPayPal();


                    // From PaymentDTO
                    paypal.RefCustomerID = Convert.ToInt64(payment.CustomerID);
                    paypal.TrackID = Convert.ToInt64(payment.TrackID);
                    paypal.TrackKey = Convert.ToInt64(payment.TrackKey);
                    paypal.PaymentID = Convert.ToInt64(payment.PaymentID);
                    paypal.TransStatus = Convert.ToString((int)payment.Status);
                    paypal.IpnVerificationRequired = Convert.ToBoolean(paypalDTO.IpnVerificationRequired);
                    paypal.TransDateTime = Convert.ToString(paypalDTO.TransDateTime);
                    paypal.TransMessage = paypalDTO.TransMessage;
                    //paypal.InitOn = DateTime.Now;
                    paypal.InitStatus = payment.TransactionStatus;


                    if (paypalDTO.TransMessage.Trim().ToUpper() == "SUCCESS")
                    {
                        // Transaction properties
                        paypal.TransID = paypalDTO.TransID;
                        paypal.TransAmount = paypalDTO.TransAmount;
                        paypal.TransCurrency = paypalDTO.TransCurrency;
                        paypal.TransPayerID = paypalDTO.TransPayerID;
                        paypal.TransPayerStatus = paypalDTO.TransPayerStatus;
                        paypal.TransPayerEmail = paypalDTO.TransPayerEmail;
                        paypal.TransPaymentType = paypalDTO.TransPaymentType;
                        paypal.TransReason = paypalDTO.TransReason;
                        paypal.TransNoOfCart = paypalDTO.TransNoOfCart;
                        paypal.TransAddressStatus = paypalDTO.TransAddressStatus;
                        paypal.TransAddressZip = paypalDTO.TransAddressZip;
                        paypal.TransAddressName = paypalDTO.TransAddressName;
                        paypal.TransAddressStreet = paypalDTO.TransAddressStreet;
                        paypal.TransAddressCountry = paypalDTO.TransAddressCountry;
                        paypal.TransAddressCity = paypalDTO.TransAddressCity;
                        paypal.TransAddressState = paypalDTO.TransAddressState;
                        paypal.TransResidenceCountry = paypalDTO.TransResidenceCountry;
                        paypal.TransAmountFee = paypalDTO.TransAmountFee;
                        paypal.ExRateUSD = Convert.ToDecimal(paypalDTO.ExRateUSD);
                    }
                    status = paymentRepository.UpdatePayPalPaymentEntry(paypal);
                    #endregion
                    break;
                case ServiceContract.Enums.PaymentGatewayType.THEFORT:
                    #region THEFORT
                    Services.Contracts.PaymentGateway.TheFortPaymentDTO fortDTO = JsonConvert.DeserializeObject<Services.Contracts.PaymentGateway.TheFortPaymentDTO>(payment.CustomAttributes);

                    PaymentDetailsTheFort fort = new PaymentDetailsTheFort();

                    // From paymentdto
                    if (payment.CustomerID.IsNotNullOrEmpty())
                    {
                        fort.CustomerID = Convert.ToInt64(payment.CustomerID);
                    }

                    fort.PTransCustomerEmail = payment.EmailId;
                    fort.TrackKey = Convert.ToInt64(fortDTO.PTransMerchantReference);
                    fort.PTransMerchantReference = fortDTO.PTransMerchantReference;
                    fort.PTransResponseMessage = fortDTO.PTransResponseMessage;
                    fort.PTransCommand = fortDTO.PTransCommand;
                    fort.PTransPaymentOption = fortDTO.PTransPaymentOption;
                    fort.TLanguage = fortDTO.TLanguage;
                    fort.PTransExpiryDate = fortDTO.PTransExpiryDate;
                    fort.PTransAuthorizationCode = fortDTO.PTransAuthorizationCode;
                    fort.PTransActualAmount = fortDTO.PTransActualAmount;
                    fort.PTransCurrency = fortDTO.PTransCurrency;
                    fort.PTransCustomerIP = fortDTO.PTransCustomerIP;
                    fort.PTransResponseCode = fortDTO.PTransResponseCode;
                    fort.PTransEci = fortDTO.PTransEci;
                    fort.PTransStatus = fortDTO.PTransStatus;
                    fort.PTransCardNumber = fortDTO.PTransCardNumber;
                    fort.PTransAccessCode = fortDTO.PTransAccessCode;
                    fort.PTransSignature = fortDTO.PTransSignature;
                    fort.PTransMerchantIdentifier = fortDTO.PTransMerchantIdentifier;
                    fort.TransID = fortDTO.FortID;

                    fort.InitStatus = payment.TransactionStatus;
                    fort.AdditionalDetails = fortDTO.AdditionalDetails;
                    fort.PTransCustomerEmail = fortDTO.PTransCustomerEmail;
                    fort.CardHolderName = fortDTO.CardHolderName;

                    status = paymentRepository.UpdateTheFortPaymentEntry(fort);

                    #endregion
                    break;
            }
            return status;
        }

        public PaymentDTO GetKnetTrackID(long customerID, long paymentID = default(long))
        {
            var paymentdetail = paymentRepository.GetKnetTrackID(customerID, paymentID);
            var paymentDTO = new PaymentDTO();
            if (paymentdetail.IsNotNull())
            {
                paymentDTO.PaymentID = paymentdetail.PaymentID.ToString();
                paymentDTO.Amount = paymentdetail.PaymentAmount.ToString();
                paymentDTO.TrackID = paymentdetail.TrackID.ToString();
                paymentDTO.InitiatedOn = paymentdetail.InitOn.ToString();
                paymentDTO.Status = (Eduegate.Services.Contracts.Enums.TransactionStatus)Enum.Parse(typeof(Framework.Payment.TransactionStatus), paymentdetail.InitStatus);
            }
            return paymentDTO;
        }


        public Eduegate.Services.Contracts.PaypalPaymentDTO GetPayPalPaymentDetail(long trackID, long trackKey, long customerID)
        {
            var paypalDTO = new Eduegate.Services.Contracts.PaypalPaymentDTO();
            return new PaypalMapper().ToDTO(paymentRepository.GetPayPalPaymentDetail(trackID, trackKey, customerID));
        }


        public bool UpdatePayPalIPNStatus(Eduegate.Services.Contracts.PaypalPaymentDTO paypalDto)
        {
            var payment = new PaymentDetailsPayPal();
            payment.TrackID = paypalDto.TrackID;
            payment.TrackKey = paypalDto.TrackKey;
            payment.PaymentID = paypalDto.PaymentID;
            payment.RefCustomerID = paypalDto.RefCustomerID;

            payment.IpnHandlerTransID = paypalDto.IpnHandlerTransID;
            payment.IpnHandlerUpdatedOn = paypalDto.IpnHandlerUpdatedOn;
            payment.IpnHandlerVerified = paypalDto.IpnHandlerVerified;
            payment.IpnVerified = paypalDto.IpnVerified;

            return paymentRepository.UpdatePayPalIPNStatus(payment);
        }


        public long GetFortCustomerID(string trackKey, string email)
        {
            return paymentRepository.GetFortCustomerID(trackKey, email);
        }

        public string[] GetPaymentDetailsTheFortByMerchantReference(string trackkey)
        {
            var stringArray = new string[3];
            var details = paymentRepository.GetPaymentDetailsTheFortByMerchantReference(trackkey);
            stringArray[0] = Convert.ToString(details.PTransStatus);
            stringArray[1] = Convert.ToString(details.PTransResponseCode);
            stringArray[2] = Convert.ToString(details.PTransResponseMessage);
            return stringArray;
        }


        public bool UpdatePDTData(Eduegate.Services.Contracts.PaypalPaymentDTO paypalDto)
        {
            var payment = new PaymentDetailsPayPal();
            payment.TrackID = paypalDto.TrackID;
            payment.TrackKey = paypalDto.TrackKey;
            payment.PaymentID = paypalDto.PaymentID;
            payment.RefCustomerID = paypalDto.RefCustomerID;
            payment.PaypalDataTransferData = paypalDto.PaypalData;
            return paymentRepository.UpdatePDTData(payment);
        }
    }
}

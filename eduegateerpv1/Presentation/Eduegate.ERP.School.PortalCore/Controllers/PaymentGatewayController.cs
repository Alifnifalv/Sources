using Eduegate.Application.Mvc;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.ViewModels.Payment;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using System;
using Eduegate.ERP.School.PortalCore.Common;
using System.Threading.Tasks;
using System.Globalization;
using System.Linq;
using System.Text;
using Eduegate.Services.Contracts.Payments;
using System.IO;
using Newtonsoft.Json;

namespace Eduegate.ERP.School.ParentPortal.Controllers
{
    public class PaymentGatewayController : BaseController
    {
        [HttpPost]
        public ActionResult SubmitAmountAsLog(decimal? totalAmount)
        {
            var logSave = ClientFactory.SchoolServiceClient(CallContext).SubmitAmountAsLog(totalAmount);

            if (logSave == null)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = logSave });
            }
        }

        public ActionResult Initiate(byte? paymentModeID = null)
        {
            ViewBag.PaymentModeID = paymentModeID;

            return View();
        }

        public ActionResult Response()
        {
            return View();
        }

        public ActionResult FeePaymentFromMobile(long? loginID, string emailID, string userID, byte? paymentModeID)
        {
            if (!CallContext.LoginID.HasValue)
            {
                CallContext.LoginID = loginID;
                CallContext.EmailID = emailID;
                CallContext.UserId = userID;
            }

            if (!paymentModeID.HasValue)
            {
                var masterVisaData = ClientFactory.SchoolServiceClient(CallContext).GetPaymentMasterVisaData();
                paymentModeID = !string.IsNullOrEmpty(masterVisaData.CardType) && masterVisaData.CardType?.ToLower() == Eduegate.Services.Contracts.Enums.PaymentGatewayType.QPAY.ToString()?.ToLower() ? (byte?)Eduegate.Services.Contracts.Enums.PaymentGatewayType.QPAY : null;
            }

            if (paymentModeID == (byte?)Eduegate.Services.Contracts.Enums.PaymentGatewayType.QPAY)
            {
                return InitiateQPAYPayment();
            }
            else
            {
                return InitiateCardFeePayment();
            }
        }

        public ActionResult InitiateCardFeePayment()
        {
            var settingBL = new Domain.Setting.SettingBL(CallContext);

            string defaultCurrency = settingBL.GetSettingValue<string>("DEFAULT_CURRENCY_CODE");

            string merchantID = settingBL.GetSettingValue<string>("MERCHANTID");

            string merchantName = settingBL.GetSettingValue<string>("MERCHANTNAME");

            string orderDescription = settingBL.GetSettingValue<string>("ONLINEFEEPAYMENTDESCRIPTION");

            string MerchantCheckoutJSLink = settingBL.GetSettingValue<string>("MERCHANTGATEWAYCHECKOUTJSLINK");

            string MerchantLogoURL = settingBL.GetSettingValue<string>("MERCHANTGATEWAYLOGOURL");

            var masterVisaViewModel = new PaymentMasterVisaViewModel()
            {
                PaymentCurrency = defaultCurrency,
                MerchantID = merchantID,
                MerchantName = merchantName,
                OrderDescription = orderDescription,
                MerchantCheckoutJS = MerchantCheckoutJSLink,
                MerchantLogoURL = MerchantLogoURL,
            };

            return OpenCardPaymentPage(masterVisaViewModel);
        }

        public ActionResult OpenCardPaymentPage(PaymentMasterVisaViewModel masterVisaData)
        {
            var data = ClientFactory.SchoolServiceClient(CallContext).GetPaymentMasterVisaData();

            if (data != null)
            {
                if (data.Response != null)
                {
                    //var nvc = HttpUtility.ParseQueryString(data.Response);
                    dynamic resultObject = JsonConvert.DeserializeObject(data.Response);

                    //if (nvc["result"] == "SUCCESS")
                    //{
                    if(resultObject.result == "SUCCESS")
                    {
                        string session = resultObject.session.id;

                        var masterVisaViewModel = new PaymentMasterVisaViewModel()
                        {
                            TrackIID = data.TrackIID,
                            CustomerID = data.CustomerID,
                            PaymentAmount = data.PaymentAmount,
                            TransID = data.TransID,
                            LogType = data.LogType,
                            BankSession = session,
                            PaymentCurrency = masterVisaData.PaymentCurrency,
                            MerchantID = masterVisaData.MerchantID,
                            MerchantName = masterVisaData.MerchantName,
                            OrderDescription = masterVisaData.OrderDescription,
                            MerchantCheckoutJS = masterVisaData.MerchantCheckoutJS,
                            MerchantLogoURL = masterVisaData.MerchantLogoURL,
                        };

                        ViewBag.MasterVisaDatas = masterVisaViewModel;

                        return View("MastercardPayment");
                    }
                }
            }

            return null;
        }

        public ActionResult Cancel()
        {
            return View();
        }

        public ActionResult Fail(string errorMessage)
        {
            var paymentMasterData = ClientFactory.SchoolServiceClient(CallContext).GetPaymentMasterVisaData();

            ClientFactory.PaymentGatewayServiceClient(null).LogPaymentLog(new PaymentLogDTO()
            {
                TrackID = paymentMasterData?.TrackIID,
                RequestType = "Initiating checkout",
                ResponseLog = "FAILURE",
                ValidationResult = errorMessage,
                Amount = paymentMasterData?.PaymentAmount,
                LoginID = paymentMasterData?.LoginID,
                TransNo = paymentMasterData?.TransID,
            });

            return View();
        }

        public ActionResult Validate()
        {
            return View();
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Pending()
        {
            return View();
        }

        public ActionResult RetryValidate(string transactionNumber, byte? paymentModeID = null)
        {
            ViewBag.PaymentTransactionNumber = transactionNumber;
            ViewBag.PaymentModeID = paymentModeID;

            return View();
        }

        //[HttpGet]
        public async Task<ActionResult> QPAYResponse()
        {
            try
            {
                string responseStatus = "";
                string responseStatusMessage = "";
                var responsePUN = "";
                long trackId = 0;
                string responseConfirmationID = "";
                string responseCardHolderName = "";
                string responseAcquirerID = "";
                string responseCardExpiryDate = "";
                string bodyAsString = await GetRequestBodyAsStringAsync(HttpContext.Request.Body);

                Eduegate.Logger.LogHelper<string>.Fatal("QPAYResponse bodyAsString", new Exception(bodyAsString));
                string[] items = bodyAsString.Split('&');

                // Access individual items:
                foreach (string item in items)
                {
                    var item2 = item.Split('=');

                    if (item2[0].Replace("\r\n", "") == "Response.Status")
                    {
                        responseStatus = item2[1];

                    }
                    if (item2[0].Replace("\r\n", "") == "Response.StatusMessage")
                    {
                        responseStatusMessage = item2[1];
                    }
                    if (item2[0].Replace("\r\n", "") == "Response.PUN")
                    {
                        responsePUN = item2[1];
                        if (item2[1] != null)
                            trackId = long.Parse(item2[1]);
                    }
                    if (item2[0].Replace("\r\n", "") == "Response.ConfirmationID")
                    {
                        responseConfirmationID = item2[1];

                    }
                    if (item2[0].Replace("\r\n", "") == "Response.CardHolderName")
                    {
                        responseCardHolderName = item2[1];
                    }
                    if (item2[0].Replace("\r\n", "") == "Response.AcquirerID")
                    {
                        responseAcquirerID = item2[1];
                    }
                    if (item2[0].Replace("\r\n", "") == "Response.CardExpiryDate")
                    {
                        responseCardExpiryDate = item2[1];
                    }
                }
                Eduegate.Logger.LogHelper<string>.Fatal("QPAYResponse responseStatus'" + responseStatus + "' StatusMessage '" + responseStatusMessage + "'", new Exception(responseStatus));

                var data = new PaymentMasterVisaDTO();
                data = ClientFactory.SchoolServiceClient(CallContext).GetPaymentMasterVisaDataByTrackID(trackId);
                data.Response = bodyAsString;
                data.ResponseStatusMessage = responseStatusMessage;
                data.ResponseStatus = responseStatus;
                data.ResponseConfirmationID = responseConfirmationID;
                data.ResponseCardHolderName = responseCardHolderName;
                data.ResponseAcquirerID = responseAcquirerID;
                data.ResponseCardExpiryDate = responseCardExpiryDate;

                ClientFactory.SchoolServiceClient(CallContext).UpdatePaymentMasterVisa(CreatePaymentMasterData(data));

                if (responseStatus != null && responseStatus == "0000")//Success
                {
                    Eduegate.Logger.LogHelper<string>.Fatal("QPAYResponse  responseStatus  is 0000  redirected to  PaymentGateway/Validate page", new Exception("QPAYResponse  responseStatus  is 0000  redirected to  PaymentGateway/Validate page "));
                    return Redirect("/PaymentGateway/Validate");
                }
                else if (responseStatus != null && (responseStatus == "2996" || responseStatus == "2997"))//Cancel
                {
                    Eduegate.Logger.LogHelper<string>.Fatal("QPAYResponse  responseStatus is 2996 or  2997;  redirected to  PaymentGateway/Cancel page", new Exception("QPAYResponse responseStatus is 2996 or  2997;  redirected to  PaymentGateway/Cancel page "));
                    return Redirect("/PaymentGateway/Cancel");
                }
                else //Failure
                {
                    Eduegate.Logger.LogHelper<string>.Fatal("QPAYResponse  responseStatus is null or    not equal to 0000;  redirected to  PaymentGateway/Fail page", new Exception("QPAYResponse  responseStatus  is 0000  redirected to  PaymentGateway/Fail page "));

                    return Redirect("/PaymentGateway/Fail");
                }
                Eduegate.Logger.LogHelper<string>.Fatal("QPAYResponse  PaymentMasterVisaData is null with trackID " + trackId, new Exception("QPAYResponse  PaymentMasterVisaData is null "));
                return View("Fail");
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<string>.Fatal("QPAYResponse Error Message:" + ex.Message.ToString(), ex);
                return View("Fail");
            }
        }

        private async Task<string> GetRequestBodyAsStringAsync(Stream body)
        {
            using (StreamReader reader = new StreamReader(body, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public ActionResult RetryCancelledPayment()
        {
            var masterVisaData = ClientFactory.SchoolServiceClient(CallContext).GetPaymentMasterVisaData();

            if (masterVisaData != null)
            {
                if (!string.IsNullOrEmpty(masterVisaData.CardType) && masterVisaData.CardType?.ToLower() == Eduegate.Services.Contracts.Enums.PaymentModes.QPAY.ToString()?.ToLower())
                {
                    return InitiateQPAYPayment();
                }
                else
                {
                    return InitiateCardFeePayment();
                }
            }
            else
            {
                return InitiateCardFeePayment();
            }
        }

        //To retry payment
        public ActionResult RetryPayment(string transactionNumber, byte? paymentModeID = null)
        {
            return RedirectToAction("GenerateMasterCardSessionIDByTransactionNo", "MasterCardPayment", new { transactionNo = transactionNumber, paymentModeID = paymentModeID });
        }

        public ActionResult InitiatePayment(byte? paymentModeID = null)
        {
            return RedirectToAction("GetMasterCardSessionID", "MasterCardPayment", new { paymentModeID = paymentModeID });
        }

        public ActionResult ValidatePayment()
        {
            if (CallContext.LoginID.HasValue)
            {
                var result = ClientFactory.PaymentGatewayServiceClient(CallContext).PaymentValidation();

                if (result != null)
                {
                    return Json(new { IsError = false, Response = result });
                }
                else
                {
                    return Json(new { IsError = true, Response = "There are some issues!" });
                }
            }
            else
            {
                return null;
            }
        }

        public ActionResult OnlinePaymentFromMobile(long? loginID, string emailID, string userID, byte? paymentMethodID)
        {
            if (!CallContext.LoginID.HasValue)
            {
                CallContext.LoginID = loginID;
                CallContext.EmailID = emailID;
                CallContext.UserId = userID;
            }
            if (paymentMethodID == (byte?)Eduegate.Services.Contracts.Enums.PaymentGatewayType.QPAY)
            {
                return InitiateQPAYPayment();
            }
            else
            {
                return InitiateCardFeePayment();
            }
        }

        public ActionResult InitiateQPAYPayment()
        {
            try
            {
                var settingBL = new Domain.Setting.SettingBL(CallContext);

                string defaultCurrency = settingBL.GetSettingValue<string>("QPAY_DEFAULT_CURRENCY_CODE");

                string merchantID = settingBL.GetSettingValue<string>("QPAY-MERCHANTID");

                string merchantName = settingBL.GetSettingValue<string>("QPAY-MERCHANTNAME");

                string orderDescription = settingBL.GetSettingValue<string>("QPAYDESCRIPTION");

                string MerchantCheckoutJSLink = settingBL.GetSettingValue<string>("QPAY-MERCHANTGATEWAYCHECKOUTJSLINK");

                string MerchantLogoURL = settingBL.GetSettingValue<string>("QPAY-MERCHANTGATEWAYLOGOURL");
                string secretKey = settingBL.GetSettingValue<string>("QPAY-SECRETKEY");
                string actionID = settingBL.GetSettingValue<string>("QPAY-ACTIONID");
                string bankID = settingBL.GetSettingValue<string>("QPAY_BANKID");
                string extraFields_f14 = settingBL.GetSettingValue<string>("QPAY-EXTRAFIELDS_F14");
                string merchantGatewayUrl = settingBL.GetSettingValue<string>("QPAY-MERCHANTGATEWAY");
                string paramType = settingBL.GetSettingValue<string>("QPAY-ParamType");
                string qPayCardTypeID = settingBL.GetSettingValue<string>("QPAY_CARDTYPE_ID");
                var transactionDatetime = DateTime.Now;

                var paymenQPAYViewModel = new PaymenQPAYViewModel()
                {
                    secretKey = secretKey,
                    TransactionDateTime = transactionDatetime,
                    transactionRequestDate = transactionDatetime.ToString("yyyyMMddHHmmss"),
                    ActionID = actionID,
                    BankID = bankID,
                    NationalID = "",
                    MerchantID = merchantID,
                    Lang = "En",
                    CurrencyCode = defaultCurrency,
                    ExtraFields_f14 = extraFields_f14,
                    Quantity = 1,
                    PaymentDescription = orderDescription,
                    MerchantGatewayUrl = merchantGatewayUrl,
                    QPayCardTypeID = qPayCardTypeID,
                };

                return OpenQPAYPaymentPage(paymenQPAYViewModel);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"InitiateQPAYPayment method: {errorMessage}", ex);

                return null;
            }
        }

        public ActionResult OpenQPAYPaymentPage(PaymenQPAYViewModel qPayData)
        {
            try
            {
                var data = ClientFactory.SchoolServiceClient(CallContext).GetPaymentMasterVisaData();

                if (data != null && data.TrackIID != 0)
                {
                    //if (data.Response != null)
                    //{

                    qPayData.PUN = data.TrackIID.ToString();
                    qPayData.MerchantModuleSessionID = data.TrackIID.ToString();
                    decimal amount = data.PaymentAmount ?? 0;
                    string formattedAmount = amount.ToString("N2");
                    int parsedAmount;
                    if (int.TryParse(formattedAmount.Replace(".", ""), NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out parsedAmount))
                        qPayData.Amount = parsedAmount;
                    else
                        qPayData.Amount = 0;
                    qPayData.ActualAmount = amount;

                    //}
                    qPayData.SecureHash = QPAYPaymentCommonFns.GenerateSecureHash
                                                (qPayData.secretKey, qPayData.ExtraFields_f14, qPayData.MerchantID, qPayData.PUN,
                                                 qPayData.Amount.ToString(), qPayData.PaymentDescription, qPayData.transactionRequestDate,
                                                 qPayData.BankID.ToString(), qPayData.NationalID.ToString(), qPayData.Lang, qPayData.CurrencyCode);

                    data.SecureHash = qPayData.SecureHash;
                    data.CardType = "QPAY";
                    data.CardTypeID = string.IsNullOrEmpty(qPayData.QPayCardTypeID) ? int.Parse(qPayData.QPayCardTypeID) : 2;
                    data.MerchantID = qPayData.MerchantID;

                    ViewBag.qPayData = qPayData;

                    ClientFactory.SchoolServiceClient(CallContext).UpdatePaymentMasterVisa(CreatePaymentMasterData(data));

                    //ClientFactory.SchoolServiceClient(CallContext).SaveQPayPayment(CreateQPAYData(qPayData));
                    return View("QPAYPayment");
                    //}
                }
                Eduegate.Logger.LogHelper<string>.Fatal("OpenQPAYPaymentPage  PaymentMasterVisaData is null  ", new Exception("OpenQPAYPaymentPage  PaymentMasterVisaData is null "));
                return View("Fail");

            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<string>.Fatal("OpenQPAYPaymentPage error message'" + ex.Message + "' InnerException: '" + ex.InnerException + "'", ex);
                return View("Fail");
            }
        }

        private PaymentMasterVisaDTO CreatePaymentMasterData(PaymentMasterVisaDTO paymenMasterVisa)
        {
            var data = new PaymentMasterVisaDTO()
            {
                SecureHash = paymenMasterVisa.SecureHash,
                TrackIID = paymenMasterVisa.TrackIID,
                TransID = paymenMasterVisa.TransID,
                CardType = paymenMasterVisa.CardType,
                CardTypeID = paymenMasterVisa.CardTypeID,
                ResponseCode = paymenMasterVisa.ResponseStatus,
                ResponseAcquirerID = paymenMasterVisa.ResponseCode,

                ResponseCardExpiryDate = paymenMasterVisa.ResponseCardExpiryDate,
                ResponseCardHolderName = paymenMasterVisa.ResponseCardHolderName,

                ResponseConfirmationID = paymenMasterVisa.ResponseConfirmationID,
                ResponseStatus = paymenMasterVisa.ResponseStatus,

                ResponseStatusMessage = paymenMasterVisa.ResponseStatusMessage,
                ResponseSecureHash = paymenMasterVisa.ResponseSecureHash,
                Response = paymenMasterVisa.Response,
            };

            return data;
        }

        private PaymentQPAYDTO CreateQPAYData(PaymenQPAYViewModel paymenQPAYViewModel)
        {
            var qPayData = new PaymentQPAYDTO()
            {
                LoginID = CallContext.LoginID,
                SecureKey = paymenQPAYViewModel.secretKey,
                SecureHash = paymenQPAYViewModel.SecureHash,
                PaymentAmount = paymenQPAYViewModel.ActualAmount,
                TransactionRequestDate = paymenQPAYViewModel.TransactionDateTime,
                ActionID = paymenQPAYViewModel.ActionID,

                BankID = paymenQPAYViewModel.BankID,
                NationalID = paymenQPAYViewModel.NationalID,
                MerchantID = paymenQPAYViewModel.MerchantID,
                Lang = paymenQPAYViewModel.Lang,


                CurrencyCode = paymenQPAYViewModel.CurrencyCode,
                ExtraFields_f14 = paymenQPAYViewModel.ExtraFields_f14,
                Quantity = paymenQPAYViewModel.Quantity,
                PaymentDescription = paymenQPAYViewModel.PaymentDescription,
                MerchantGatewayUrl = paymenQPAYViewModel.MerchantGatewayUrl,
            };

            return qPayData;
        }

        public IActionResult ValidateQPayPaymentResponse()
        {
            try
            {
                if (CallContext.LoginID.HasValue)
                {
                    var query = HttpContext.Request.Query;
                    var sortedParameters = query.OrderBy(kvp => kvp.Key).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                    var settingBL = new Domain.Setting.SettingBL(CallContext);
                    string secretKey = settingBL.GetSettingValue<string>("QPAY-SECRETKEY");
                    var stringBuilder = new StringBuilder();
                    stringBuilder.Append(secretKey);
                    foreach (var kvp in sortedParameters)
                    {
                        stringBuilder.Append(kvp.Value);
                    }

                    var hash = QPAYPaymentCommonFns.GenerateHash(stringBuilder.ToString());
                    var responseHash = Request.Form["Response.SecureHash"];

                    //  return hash.Equals(responseHash)

                    var result = "Success";
                    //ClientFactory.PaymentGatewayServiceClient(CallContext).PaymentValidation();

                    if (result != null)
                    {
                        return Json(new { IsError = false, Response = result });
                    }
                    else
                    {
                        return Json(new { IsError = true, Response = "There are some issues!" });
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                var respose = new QPAYResponseViewModel();
                respose.ErrorMessage = ex.Message;
                respose.InnerErrorMessage = ex.InnerException?.Message;
                respose.StackTrace = ex.StackTrace;
                ViewBag.QPAYResponse = respose;
                return View("QpayResponseOLD");
            }
        }

        public ActionResult InitiateCardOnlinePayment()
        {
            var settingBL = new Domain.Setting.SettingBL(CallContext);

            string defaultCurrency = settingBL.GetSettingValue<string>("DEFAULT_CURRENCY_CODE");

            string merchantID = settingBL.GetSettingValue<string>("MERCHANTID");

            string merchantName = settingBL.GetSettingValue<string>("MERCHANTNAME");

            string orderDescription = settingBL.GetSettingValue<string>("ONLINEPAYMENTDESCRIPTION");

            string MerchantCheckoutJSLink = settingBL.GetSettingValue<string>("MERCHANTGATEWAYCHECKOUTJSLINK");

            string MerchantLogoURL = settingBL.GetSettingValue<string>("MERCHANTGATEWAYLOGOURL");

            var masterVisaViewModel = new PaymentMasterVisaViewModel()
            {
                PaymentCurrency = defaultCurrency,
                MerchantID = merchantID,
                MerchantName = merchantName,
                OrderDescription = orderDescription,
                MerchantCheckoutJS = MerchantCheckoutJSLink,
                MerchantLogoURL = MerchantLogoURL,
            };

            return OpenCardPaymentPage(masterVisaViewModel);
        }

        public ActionResult errorCallback(string errorMessage)
        {
            var paymentMasterData = ClientFactory.SchoolServiceClient(CallContext).GetPaymentMasterVisaData();

            ClientFactory.PaymentGatewayServiceClient(null).LogPaymentLog(new PaymentLogDTO()
            {
                TrackID = paymentMasterData?.TrackIID,
                RequestType = "Initiating checkout",
                ResponseLog = "FAILURE",
                ValidationResult = errorMessage,
                Amount = paymentMasterData?.PaymentAmount,
                LoginID = paymentMasterData?.LoginID,
                TransNo = paymentMasterData?.TransID,
            });

            return View("Fail");
        }

        public ActionResult cancelCallback()
        {
            return View("Cancel");
        }

        public ActionResult completeCallback()
        {
            return View("Validate");
        }

    }
}
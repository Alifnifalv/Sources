using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Mappers.Payment;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Services.Contracts.Payments;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Security.Cryptography;
using System.Linq;
using RestSharp;
using Eduegate.Services.Contracts.Checkout;
using Eduegate.Domain.Mappers.ShoppingCartMapper;
using Eduegate.Domain.MobileAppWrapper;
using Eduegate.Services.Contracts.MobileAppWrapper;
using System.Net.Http.Headers;
using System.Net.Http;

namespace Eduegate.Domain.Payment
{
    public class PaymentGatewayBL
    {
        private CallContext Context { get; set; }

        public PaymentGatewayBL(CallContext context)
        {
            Context = context;
        }

        public string GenerateCardSessionID(byte? paymentModeID = null)
        {
            var loginID = Context.LoginID;

            var cartID = loginID.HasValue ? loginID : 0;

            var logData = PaymentLogMapper.Mapper(Context).GetLastLogData();

            var amount = Convert.ToDecimal(logData.Amount);

            var cardTypeIDQpay = new Domain.Setting.SettingBL(Context).GetSettingValue<int>("QPAY_CARDTYPE_ID");

            string responseFromServer;

            // Convert the ticks value to a string
            string timeBaseRandomNumber = DateTime.Now.Ticks.ToString();

            //string timeBaseRandomNumber = DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);

            var uniqueCartID = cartID.ToString() + "_" + timeBaseRandomNumber;

            var masterVisaData = new PaymentMasterVisaDTO();

            var isError = false;
            var errorMessage = string.Empty;

            if (paymentModeID == (byte?)Eduegate.Services.Contracts.Enums.PaymentModes.QPAY)
            {
                masterVisaData = new PaymentRepository().MakePaymentEntry(new PaymentMasterVisa()
                {
                    LoginID = Convert.ToInt64(loginID),
                    CustomerID = null,
                    PaymentAmount = amount,
                    CartID = cartID,
                    TransID = uniqueCartID,
                    Response = null,
                    LogType = "INITIATE_CHECKOUT",
                    InitOn = DateTime.Now,
                    CardTypeID = cardTypeIDQpay,
                    CardType = Eduegate.Services.Contracts.Enums.PaymentModes.QPAY.ToString(),
                    SuccessStatus = false,
                });

                isError = false;
            }
            else
            {
                var result = GetPaymentSessionFromCardAPI(uniqueCartID, amount, out responseFromServer);

                //reconfirm the session with api if any entry exists or not
                masterVisaData = new PaymentRepository().MakePaymentEntry(new PaymentMasterVisa()
                {
                    LoginID = Convert.ToInt64(loginID),
                    CustomerID = null,
                    PaymentAmount = amount,
                    //CartID = cartID,
                    TransID = uniqueCartID,
                    Response = responseFromServer,
                    LogType = "INITIATE_CHECKOUT",
                    InitOn = DateTime.Now,
                    SuccessStatus = false,
                });

                if (result != null)
                {
                    isError = false;
                }
                else
                {
                    isError = true;
                    errorMessage = "Generation failed!";
                }
            }

            if (masterVisaData != null && masterVisaData.IsMasterVisaSaved == true)
            {
                new PaymentRepository().UpdatePaymentLogs(masterVisaData);
            }

            if (isError == true)
            {
                return null;
            }
            else
            {
                return "Successfully generated";
            }
        }

        public string GetPaymentSessionFromCardAPI(string referenceID, decimal amount, out string serverResponse)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var settingBL = new Domain.Setting.SettingBL(Context);

            string defaultCurrency = settingBL.GetSettingValue<string>("DEFAULT_CURRENCY_CODE");

            string merchantID = settingBL.GetSettingValue<string>("MERCHANTID");
            string password = settingBL.GetSettingValue<string>("MERCHANTPASSWORD");
            string requestUri = settingBL.GetSettingValue<string>("MERCHANTGATEWAY");
            string merchantName = settingBL.GetSettingValue<string>("MERCHANTNAME");
            string orderDescription = settingBL.GetSettingValue<string>("MERCHANT_ORDER_DESCRIPTION");
            string MerchantLogoURL = settingBL.GetSettingValue<string>("MERCHANTGATEWAYLOGOURL");

            requestUri = requestUri.Replace("{merchant_ID}", merchantID);

            dynamic requestBody = new
            {
                apiOperation = "INITIATE_CHECKOUT",
                interaction = new
                {
                    operation = "PURCHASE",
                    merchant = new
                    {
                        name = merchantName,
                        logo = MerchantLogoURL
                    },
                    displayControl = new
                    {
                        billingAddress = "HIDE",
                        customerEmail = "HIDE",
                        shipping = "HIDE",
                    }
                },
                order = new
                {
                    id = referenceID,
                    amount = amount,
                    currency = defaultCurrency,
                    description = orderDescription
                }
            };

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"merchant.{merchantID}:{password}")));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response1 = client.PostAsync(requestUri, content).GetAwaiter().GetResult();

                string responseContent = response1.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                dynamic resultObject = JsonConvert.DeserializeObject(responseContent);

                serverResponse = responseContent;
                if (resultObject.result == "SUCCESS")
                {
                    return resultObject.session.id;
                }
                else
                {
                    return null;
                }
            }
        }

        public string GetPaymentSessionFromCardAPIOLD(string referenceID, decimal amount, out string serverResponse)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var settingBL = new Domain.Setting.SettingBL(Context);

            string defaultCurrency = settingBL.GetSettingValue<string>("DEFAULT_CURRENCY_CODE");

            string merchantID = settingBL.GetSettingValue<string>("MERCHANTID");
            string password = settingBL.GetSettingValue<string>("MERCHANTPASSWORD");
            string encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes("merchant." + merchantID + ":" + password));
            var requestUri = settingBL.GetSettingValue<string>("MERCHANTGATEWAY"); // "https://eu-gateway.mastercard.com/api/nvp/version/55";
            var webRequest = WebRequest.Create(requestUri);
            Uri uri = new Uri(requestUri);
            webRequest.Headers.Add(HttpRequestHeader.Authorization, "Basic " + encoded);
            webRequest.Method = "POST";

            //var postData = string.Format("apiOperation=INITIATE_CHECKOUT" +
            //    "&apiPassword={0}" +
            //    "&apiUsername=merchant.{1}" +
            //    "&merchant={1}" +
            //    "&interaction.operation=AUTHORIZE" +
            //    "&interaction.merchant={1}" +
            //    "&order.id={2}" +
            //    "&order.amount={3}" +
            //    "&order.currency={4}", password, merchantID, referenceID, amount, defaultCurrency);

            var postData = string.Format("apiOperation=CREATE_CHECKOUT_SESSION" +
                "&apiPassword={0}" +
                "&interaction.operation=PURCHASE" +
                "&apiUsername=merchant.{1}" +
                "&merchant={1}&order.id={2}" +
                "&order.amount={3}" +
                "&order.currency={4}", password, merchantID, referenceID, amount, defaultCurrency);

            var data = Encoding.ASCII.GetBytes(postData);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = data.Length;

            using (var stream = webRequest.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = webRequest.GetResponse();
            using (var dataStream = response.GetResponseStream())
            {
                var reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                //example : merchant=TEST7006259&result=SUCCESS&session.id=SESSION0002378433591N3262498M20&session.updateStatus=SUCCESS&session.version=030f712301&successIndicator=988d1c66ed53430d
                //save the response into the Logs
                serverResponse = responseFromServer;

                var nvc = HttpUtility.ParseQueryString(responseFromServer);

                if (nvc["result"] == "SUCCESS")
                {
                    return nvc["session.id"];
                }
                else
                {
                    return null;
                }
            }
        }

        public string PaymentValidation()
        {
            var validationStatus = string.Empty;
            var cartID = string.Empty;
            var merchantID = string.Empty;
            var settingBL = new Domain.Setting.SettingBL(Context);
            var masterVisaData = PaymentMasterVisaMapper.Mapper(Context).GetPaymentMasterVisaData();

            if (masterVisaData != null)
            {
                var amount = Convert.ToDecimal(masterVisaData.PaymentAmount);

                var paymentLog = new PaymentLogDTO
                {
                    RequestType = "Payment validation",
                    TrackID = masterVisaData.TrackIID,
                    TransNo = masterVisaData.TransID,
                    CartID = masterVisaData.CartID,
                    CustomerID = masterVisaData.CustomerID,
                    LoginID = masterVisaData.LoginID,
                    Amount = amount
                };

                if (masterVisaData.Response != null)
                {
                    if (!string.IsNullOrEmpty(masterVisaData.SecureHash) || masterVisaData.CardType == "QPAY")
                    {
                        var nvc = HttpUtility.ParseQueryString(masterVisaData.Response);

                        if (nvc["result"] == "SUCCESS")
                        {
                            cartID = masterVisaData.TransID;
                        }
                    }
                    else
                    {
                        dynamic resultObject = JsonConvert.DeserializeObject(masterVisaData.Response);
                        if (resultObject.result == "SUCCESS")
                        {
                            cartID = masterVisaData.TransID;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(masterVisaData.SecureHash) || masterVisaData.CardType == "QPAY")
                {
                    paymentLog.ResponseLog = "SUCCESS";

                    merchantID = settingBL.GetSettingValue<string>("QPAY-MERCHANTID");

                    string secretKey = settingBL.GetSettingValue<string>("QPAY-SECRETKEY");

                    string bankID = settingBL.GetSettingValue<string>("QPAY_BANKID");

                    string merchantGatewayUrl = settingBL.GetSettingValue<string>("QPAY-MERCHANTGATEWAY");

                    paymentLog = QPAYSendRequest(secretKey, merchantID, masterVisaData.TrackIID.ToString(), bankID, merchantGatewayUrl, "En", paymentLog);

                    validationStatus = paymentLog.ResponseLog;

                    if (paymentLog.ResponseLog.ToLower() == "success")
                    {
                        masterVisaData.MerchantID = merchantID;
                        masterVisaData.SuccessStatus = true;
                    }
                    else
                    {
                        masterVisaData.MerchantID = merchantID;
                        masterVisaData.SuccessStatus = false;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(cartID))
                    {
                        try
                        {
                            ServicePointManager.Expect100Continue = true;
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                            merchantID = settingBL.GetSettingValue<string>("MERCHANTID");
                            var password = settingBL.GetSettingValue<string>("MERCHANTPASSWORD");
                            string encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes("merchant." + merchantID + ":" + password));

                            var requestUri = string.Format("{0}/merchant/{1}/order/{2}",
                                settingBL.GetSettingValue<string>("MERCHANTGATEWAY2"),
                                settingBL.GetSettingValue<string>("MERCHANTID"),
                                cartID);
                            var webRequest = WebRequest.Create(requestUri);
                            Uri uri = new Uri(requestUri);
                            webRequest.Headers.Add(HttpRequestHeader.Authorization, "Basic " + encoded);
                            webRequest.Method = "GET";
                            var response = webRequest.GetResponse();

                            using (var dataStream = response.GetResponseStream())
                            {
                                var reader = new StreamReader(dataStream);
                                var responseFromServer = reader.ReadToEnd();
                                dynamic data = JsonConvert.DeserializeObject(responseFromServer);
                                var validation = data.result == "SUCCESS" && data.transaction[0].result == "SUCCESS" && (data.amount == amount ? true : false);

                                paymentLog.ValidationResult = data.ToString();
                                paymentLog.ResponseLog = data.transaction[0].result;

                                //if (validation == true)
                                //{
                                //    validationStatus = "Successfully validated";
                                //}
                                //else
                                //{
                                //    validationStatus = null;
                                //}

                                validationStatus = paymentLog.ResponseLog;

                                if (paymentLog.ResponseLog.ToLower() == "success")
                                {
                                    masterVisaData.MerchantID = merchantID;
                                    masterVisaData.SuccessStatus = true;
                                }
                                else
                                {
                                    masterVisaData.MerchantID = merchantID;
                                    masterVisaData.SuccessStatus = false;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            paymentLog.ResponseLog = ex.Message;

                            validationStatus = "FAILURE";

                            masterVisaData.MerchantID = merchantID;
                            masterVisaData.SuccessStatus = false;
                        }
                    }
                    else
                    {
                        paymentLog.ResponseLog = "Transaction number not received";

                        validationStatus = "FAILURE";

                        masterVisaData.MerchantID = merchantID;
                        masterVisaData.SuccessStatus = false;
                    }
                }

                PaymentLogMapper.Mapper(Context).LogPaymentLog(paymentLog);
                PaymentMasterVisaMapper.Mapper(Context).UpdatePaymentMasterVisa(masterVisaData);
            }

            return validationStatus;
        }

        public string PaymentQPayValidation()
        {
            var validationStatus = string.Empty;

            var cartID = string.Empty;
            var merchantID = string.Empty;

            var qPayDBData = PaymentQPayMapper.Mapper(Context).GetPaymentQpayData();

            var masterVisaData = PaymentMasterVisaMapper.Mapper(Context).GetPaymentMasterVisaData();

            if (qPayDBData != null)
            {
                var amount = Convert.ToDecimal(qPayDBData.PaymentAmount);

                //var qPayData = new PaymentQPAYDTO
                //{
                //    RequestType = "Payment validation",
                //    TransNo = masterVisaData.TransID,
                //    CartID = masterVisaData.CartID,
                //    Amount = amount
                //};

                //if (masterVisaData.Response != null)
                //{
                //    var nvc = HttpUtility.ParseQueryString(masterVisaData.Response);

                //    if (nvc["result"] == "SUCCESS")
                //    {
                //        cartID = masterVisaData.TransID;
                //    }
                //}

                if (!string.IsNullOrEmpty(cartID))
                {
                    try
                    {
                        ServicePointManager.Expect100Continue = true;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                        var settingBL = new Domain.Setting.SettingBL(Context);
                        merchantID = settingBL.GetSettingValue<string>("MERCHANTID");
                        var password = settingBL.GetSettingValue<string>("MERCHANTPASSWORD");
                        string encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes("merchant." + merchantID + ":" + password));

                        var requestUri = string.Format("{0}/merchant/{1}/order/{2}",
                            settingBL.GetSettingValue<string>("MERCHANTGATEWAY2"),
                            settingBL.GetSettingValue<string>("MERCHANTID"),
                            cartID);
                        var webRequest = WebRequest.Create(requestUri);
                        Uri uri = new Uri(requestUri);
                        webRequest.Headers.Add(HttpRequestHeader.Authorization, "Basic " + encoded);
                        webRequest.Method = "GET";
                        var response = webRequest.GetResponse();

                        using (var dataStream = response.GetResponseStream())
                        {
                            var reader = new StreamReader(dataStream);
                            var responseFromServer = reader.ReadToEnd();
                            dynamic data = JsonConvert.DeserializeObject(responseFromServer);
                            var validation = data.result == "SUCCESS" && data.transaction[0].result == "SUCCESS" && (data.amount == amount ? true : false);

                            //paymentLog.ValidationResult = data.ToString();
                            //paymentLog.ResponseLog = data.transaction[0].result;                           

                            validationStatus = "SUCCESS";// paymentLog.ResponseLog;

                            if (validationStatus.ToLower() == "success")
                            {
                                masterVisaData.MerchantID = merchantID;
                                masterVisaData.SuccessStatus = true;
                            }
                            else
                            {
                                masterVisaData.MerchantID = merchantID;
                                masterVisaData.SuccessStatus = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //paymentLog.ResponseLog = ex.Message;

                        validationStatus = "FAILURE";

                        masterVisaData.MerchantID = merchantID;
                        masterVisaData.SuccessStatus = false;
                    }
                }
                else
                {
                    //paymentLog.ResponseLog = "Transaction number not received";

                    validationStatus = "FAILURE";

                    masterVisaData.MerchantID = merchantID;
                    masterVisaData.SuccessStatus = false;
                }

                //PaymentLogMapper.Mapper(Context).LogPaymentLog(paymentLog);
                PaymentMasterVisaMapper.Mapper(Context).UpdatePaymentMasterVisa(masterVisaData);
            }

            return validationStatus;
        }

        public string GenerateCardSessionIDByTransactionNo(string transactionNo, byte? paymentModeID = null)
        {
            var settingBL = new Domain.Setting.SettingBL(Context);

            var paymentTransactionPrefix = string.Empty;

            if (paymentModeID == (byte?)Eduegate.Services.Contracts.Enums.PaymentModes.QPAY)
            {
                paymentTransactionPrefix = new Domain.Setting.SettingBL(null).GetSettingValue<string>("QPAY_TRANSACTION_PREFIX");
            }
            else
            {
                paymentTransactionPrefix = settingBL.GetSettingValue<string>("ONLINE_PAYMENT_TRANSACTION_PREFIX");
            }

            var transID = transactionNo.Replace(paymentTransactionPrefix, "");

            var loginID = Context.LoginID;

            //var cartID = loginID.HasValue ? loginID : 0;

            var logData = PaymentLogMapper.Mapper(Context).GetAndInsertLogDataByTransactionID(transID);

            var amount = Convert.ToDecimal(logData.Amount);

            string responseFromServer;

            //get session id
            var result = GetPaymentSessionFromCardAPI(transID, amount, out responseFromServer);

            //reconfirm the session with api if any entry exists or not
            var masterVisaData = new PaymentRepository().MakePaymentEntry(new PaymentMasterVisa()
            {
                LoginID = Convert.ToInt64(loginID),
                CustomerID = null,
                PaymentAmount = amount,
                TransID = transID,
                Response = responseFromServer,
                LogType = "INITIATE_CHECKOUT",
                InitOn = DateTime.Now,
                SuccessStatus = false
            });

            if (masterVisaData != null && masterVisaData.IsMasterVisaSaved == true)
            {
                new PaymentRepository().UpdatePaymentLogs(masterVisaData);
            }

            if (result != null)
            {
                return "Successfully generated";
            }
            else
            {
                return null;
            }
        }

        public string GenerateCardSessionIDByCart(long shoppingCartID, decimal totalAmount, byte? paymentMethodID = null)
        {
            var cartID = shoppingCartID;

            var loginID = Context.LoginID;

            var amount = totalAmount;

            string responseFromServer;

            // Convert the ticks value to a string
            string timeBaseRandomNumber = DateTime.Now.Ticks.ToString();
            //string timeBaseRandomNumber = DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);

            var uniqueCartID = cartID.ToString() + "_" + timeBaseRandomNumber;

            var masterVisaData = new PaymentMasterVisaDTO();
            if (paymentMethodID != (byte?)Eduegate.Services.Contracts.Enums.PaymentGatewayType.QPAY)
            {
                var result = GetPaymentSessionFromCardAPI(uniqueCartID, amount, out responseFromServer);
                masterVisaData = new PaymentRepository().MakePaymentEntry(new PaymentMasterVisa()
                {
                    LoginID = Convert.ToInt64(loginID),
                    CustomerID = null,
                    PaymentAmount = amount,
                    CartID = cartID,
                    TransID = uniqueCartID,
                    Response = responseFromServer,
                    LogType = "INITIATE_CHECKOUT",
                    InitOn = DateTime.Now,
                    SuccessStatus = false,
                });

                if (result != null)
                {
                    return "Successfully generated";
                }
                else
                {
                    return null;
                }
            }
            else
            {
                masterVisaData = new PaymentRepository().MakePaymentEntry(new PaymentMasterVisa()
                {
                    LoginID = Convert.ToInt64(loginID),
                    CustomerID = null,
                    PaymentAmount = amount,
                    CartID = cartID,
                    TransID = uniqueCartID,
                    Response = null,
                    LogType = "INITIATE_CHECKOUT",
                    InitOn = DateTime.Now,
                    CardTypeID = 2,
                    CardType = "QPAY",
                    SuccessStatus = false,
                });
            }

            if (masterVisaData != null && masterVisaData.IsMasterVisaSaved == true)
            {
                new PaymentRepository().UpdatePaymentLogs(masterVisaData);
            }

            return "Successfully generated";
        }

        public PaymentMasterVisaDTO GetPaymentMasterVisaDataByCartID(long cartID)
        {
            var masterVisaData = PaymentMasterVisaMapper.Mapper(Context).GetPaymentMasterVisaDataByCartID(cartID);
            return masterVisaData;
        }

        public bool PaymentValidationByCartID(long cartID, byte? paymentMethodID = null, long? totalCartAmount = null)
        {
            bool validation = false;

            var transID = string.Empty;
            var merchantID = string.Empty;

            var settingBL = new Domain.Setting.SettingBL(Context);
            var cardTypeIDQpay = settingBL.GetSettingValue<int>("QPAY_CARDTYPE_ID");

            try
            {
                var masterVisaData = PaymentMasterVisaMapper.Mapper(Context).GetPaymentMasterVisaDataByCartID(cartID);

                if (masterVisaData != null && masterVisaData.TrackIID != 0)
                {
                    var amount = Convert.ToDecimal(masterVisaData.PaymentAmount);
                    int cardTypeId = 0;
                    string cardtype = null;

                    if (totalCartAmount.HasValue)
                    {
                        if (totalCartAmount != amount)
                        {
                            return false;
                        }
                    }

                    if ((paymentMethodID != null && paymentMethodID == (byte?)Eduegate.Services.Contracts.Enums.PaymentModes.QPAY) || masterVisaData.CardType == Eduegate.Services.Contracts.Enums.PaymentModes.QPAY.ToString())
                    {
                        cardTypeId = cardTypeIDQpay;
                        cardtype = Eduegate.Services.Contracts.Enums.PaymentModes.QPAY.ToString();
                    }

                    var paymentLog = new PaymentLogDTO
                    {
                        RequestType = "Payment validation",
                        TransNo = masterVisaData.TransID,
                        CartID = masterVisaData.CartID,
                        Amount = amount,
                        CustomerID = masterVisaData.CustomerID,
                        LoginID = masterVisaData.LoginID,
                        CardType = cardtype,
                        CardTypeID = cardTypeId,
                        TrackID = masterVisaData.TrackIID
                    };

                    // if ((!string.IsNullOrEmpty(masterVisaData.SecureHash) || masterVisaData.CardType == "QPAY"))
                    if ((paymentMethodID.HasValue && paymentMethodID == (byte?)Eduegate.Services.Contracts.Enums.PaymentModes.QPAY) || masterVisaData.CardType == Eduegate.Services.Contracts.Enums.PaymentModes.QPAY.ToString())
                    {
                        paymentLog.ResponseLog = "SUCCESS";

                        merchantID = settingBL.GetSettingValue<string>("QPAY-MERCHANTID");

                        string secretKey = settingBL.GetSettingValue<string>("QPAY-SECRETKEY");

                        string bankID = settingBL.GetSettingValue<string>("QPAY_BANKID");

                        string merchantGatewayUrl = settingBL.GetSettingValue<string>("QPAY-MERCHANTGATEWAY");

                        paymentLog = QPAYSendRequest(secretKey, merchantID, masterVisaData.TrackIID.ToString(), bankID, merchantGatewayUrl, "En", paymentLog);
                        if (paymentLog.ResponseLog.ToLower() == "success")
                        {
                            validation = true;

                            masterVisaData.MerchantID = merchantID;
                            masterVisaData.SuccessStatus = true;
                        }
                        else
                        {
                            validation = false;

                            masterVisaData.MerchantID = merchantID;
                            masterVisaData.SuccessStatus = false;
                        }
                    }
                    else
                    {
                        if (masterVisaData.Response != null)
                        {
                            if (!string.IsNullOrEmpty(masterVisaData.SecureHash) || masterVisaData.CardType == "QPAY")
                            {
                                var nvc = HttpUtility.ParseQueryString(masterVisaData.Response);

                                if (nvc["result"] == "SUCCESS")
                                {
                                    transID = masterVisaData.TransID;
                                }
                            }
                            else
                            {
                                dynamic resultObject = JsonConvert.DeserializeObject(masterVisaData.Response);
                                if (resultObject.result == "SUCCESS")
                                {
                                    transID = masterVisaData.TransID;
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(transID))
                        {
                            try
                            {
                                ServicePointManager.Expect100Continue = true;
                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                                merchantID = settingBL.GetSettingValue<string>("MERCHANTID");
                                var password = settingBL.GetSettingValue<string>("MERCHANTPASSWORD");
                                string encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes("merchant." + merchantID + ":" + password));

                                var requestUri = string.Format("{0}/merchant/{1}/order/{2}",
                                    settingBL.GetSettingValue<string>("MERCHANTGATEWAY2"),
                                    settingBL.GetSettingValue<string>("MERCHANTID"),
                                    transID);
                                var webRequest = WebRequest.Create(requestUri);
                                Uri uri = new Uri(requestUri);
                                webRequest.Headers.Add(HttpRequestHeader.Authorization, "Basic " + encoded);
                                webRequest.Method = "GET";
                                var response = webRequest.GetResponse();

                                using (var dataStream = response.GetResponseStream())
                                {
                                    var reader = new StreamReader(dataStream);
                                    var responseFromServer = reader.ReadToEnd();
                                    dynamic data = JsonConvert.DeserializeObject(responseFromServer);

                                    validation = data.result == "SUCCESS" && data.transaction[0].result == "SUCCESS" && data.amount == amount ? true : false;

                                    paymentLog.ValidationResult = data.ToString();
                                    paymentLog.ResponseLog = data.transaction[0].result;

                                    if (paymentLog.ResponseLog.ToLower() == "success")
                                    {
                                        masterVisaData.MerchantID = merchantID;
                                        masterVisaData.SuccessStatus = true;
                                    }
                                    else
                                    {
                                        masterVisaData.MerchantID = merchantID;
                                        masterVisaData.SuccessStatus = false;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                paymentLog.ResponseLog = ex.Message;

                                validation = false;

                                masterVisaData.MerchantID = merchantID;
                                masterVisaData.SuccessStatus = false;
                            }
                        }
                        else
                        {
                            paymentLog.ResponseLog = "Transaction number not received";
                            Eduegate.Logger.LogHelper<string>.Fatal("PaymentValidationByCartID Transaction number not received", new Exception("PaymentValidationByCartID Transaction number not received"));
                            validation = false;

                            masterVisaData.MerchantID = merchantID;
                            masterVisaData.SuccessStatus = false;
                        }
                    }

                    PaymentLogMapper.Mapper(Context).LogPaymentLog(paymentLog);
                    PaymentMasterVisaMapper.Mapper(Context).UpdatePaymentMasterVisa(masterVisaData);
                }
                else
                {
                    validation = false;
                    Eduegate.Logger.LogHelper<string>.Fatal("PaymentValidationByCartID Master Visa data is null", new Exception("PaymentValidationByCartID Master Visa data is null"));
                }
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<string>.Fatal("PaymentValidationByCartID ex message" + ex.Message, ex);
            }

            return validation;
        }

        public PaymentLogDTO QPayInquiry(long? cartID = 0)
        {
            var transID = string.Empty;
            var settingBL = new Domain.Setting.SettingBL(Context);
            var paymentLog = new PaymentLogDTO();
            var logdata = new PaymentLogDTO();
            long cartDataID = 0;
            try
            {
                if (cartID == null && cartID == 0)
                {
                    logdata = PaymentLogMapper.Mapper(Context).GetLastQPAYLogData();
                    cartDataID = logdata.CartID ?? 0;
                }
                else
                {
                    cartDataID = cartID ?? 0;
                }


                if (cartDataID == 0)
                {
                    logdata.ResponseLog = "PaymentLog returns null";
                    PaymentLogMapper.Mapper(Context).LogPaymentLog(logdata);
                    Eduegate.Logger.LogHelper<string>.Fatal("QPayInquiry :PaymentLog returns null", new Exception("QPayInquiry PaymentLog returns null"));
                }

                var masterVisaData = PaymentMasterVisaMapper.Mapper(Context).GetPaymentMasterVisaDataByCartID(cartDataID);

                string qPayCardType = settingBL.GetSettingValue<string>("QPAY_CARDTYPE_ID");
                int? qPayCardTypeId = string.IsNullOrEmpty(qPayCardType) ? int.Parse(qPayCardType) : 2;
                if (masterVisaData != null)
                {
                    var amount = Convert.ToDecimal(masterVisaData.PaymentAmount);

                    paymentLog = new PaymentLogDTO()
                    {
                        RequestType = "Payment validation",
                        TransNo = masterVisaData.TransID,
                        CartID = masterVisaData.CartID,
                        TrackID = masterVisaData.TrackIID,
                        Amount = amount,
                        CustomerID = masterVisaData.CustomerID,
                        LoginID = masterVisaData.LoginID,
                    };

                    if (masterVisaData.CardTypeID.HasValue && masterVisaData.CardTypeID == qPayCardTypeId)
                    {
                        paymentLog.ResponseLog = "SUCCESS";

                        string merchantID = settingBL.GetSettingValue<string>("QPAY-MERCHANTID");

                        string secretKey = settingBL.GetSettingValue<string>("QPAY-SECRETKEY");

                        string bankID = settingBL.GetSettingValue<string>("QPAY_BANKID");

                        string merchantGatewayUrl = settingBL.GetSettingValue<string>("QPAY-MERCHANTGATEWAY");

                        paymentLog = QPAYSendRequest(secretKey, merchantID, masterVisaData.TrackIID.ToString(), bankID, merchantGatewayUrl, "En", paymentLog);
                    }

                    PaymentLogMapper.Mapper(Context).LogPaymentLog(paymentLog);

                }
                else
                {
                    paymentLog.ResponseLog = "FAILURE";
                    Eduegate.Logger.LogHelper<string>.Fatal("QPayInquiry Master Visa data is null", new Exception("QPayInquiry Master Visa data is null"));
                }

                return paymentLog;
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<string>.Fatal("QPayInquiry ex message" + ex.Message, ex);
            }

            return paymentLog;
        }

        public string PendingQPayPaymentProcess()
        {
            var transID = string.Empty;
            var settingBL = new Domain.Setting.SettingBL(Context);
            var paymentLog = new PaymentLogDTO();
            var logdata = new List<PaymentLogDTO>();
            var cartDataIDs = new List<long?>();
            try
            {
                logdata = PaymentLogMapper.Mapper(Context).GetPendingQPAYLogData();
                cartDataIDs = logdata.Select(x => x.CartID).ToList();

                if (cartDataIDs.Count() == 0)
                {
                    Eduegate.Logger.LogHelper<string>.Fatal("PendingQPayPaymentProcess :There is no Pending QPAY Logs", new Exception("PendingQPayPaymentProcess :There is no Pending QPAY Logs"));
                    return "There is no Pending QPAY Logs";
                }
                cartDataIDs.All(x =>
                {
                    var masterVisaData = PaymentMasterVisaMapper.Mapper(Context).GetPaymentMasterVisaDataByCartID(x.Value);

                    string qPayCardType = settingBL.GetSettingValue<string>("QPAY_CARDTYPE_ID");
                    int? qPayCardTypeId = string.IsNullOrEmpty(qPayCardType) ? int.Parse(qPayCardType) : 2;
                    if (masterVisaData != null)
                    {
                        var amount = Convert.ToDecimal(masterVisaData.PaymentAmount);

                        paymentLog = new PaymentLogDTO()
                        {
                            RequestType = "Payment validation",
                            TransNo = masterVisaData.TransID,
                            CartID = masterVisaData.CartID,
                            TrackID = masterVisaData.TrackIID,
                            Amount = amount,
                            CustomerID = masterVisaData.CustomerID,
                            LoginID = masterVisaData.LoginID,
                        };

                        if (masterVisaData.CardTypeID.HasValue && masterVisaData.CardTypeID == qPayCardTypeId)
                        {
                            paymentLog.ResponseLog = "SUCCESS";

                            string merchantID = settingBL.GetSettingValue<string>("QPAY-MERCHANTID");

                            string secretKey = settingBL.GetSettingValue<string>("QPAY-SECRETKEY");

                            string bankID = settingBL.GetSettingValue<string>("QPAY_BANKID");

                            string merchantGatewayUrl = settingBL.GetSettingValue<string>("QPAY-MERCHANTGATEWAY");

                            paymentLog = QPAYSendRequest(secretKey, merchantID, masterVisaData.TrackIID.ToString(), bankID, merchantGatewayUrl, "En", paymentLog);

                            if (paymentLog.ResponseLog == "SUCCESS")
                            {
                                GenerateQPAYOrder(new CheckoutPaymentDTO() { ShoppingCartID = masterVisaData.CartID.ToString(), SelectedPaymentOption = "30" });

                                masterVisaData.MerchantID = merchantID;
                                masterVisaData.SuccessStatus = false;
                            }
                        }

                        PaymentLogMapper.Mapper(Context).LogPaymentLog(paymentLog);
                        PaymentMasterVisaMapper.Mapper(Context).UpdatePaymentMasterVisa(masterVisaData);

                    }

                    return true;
                });

                return null;
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<string>.Fatal("QPayInquiry ex message" + ex.Message, ex);
                return ex.Message;
            }

            return null;
        }

        private CheckoutPaymentMobileDTO GenerateQPAYOrder(CheckoutPaymentDTO checkoutPaymentDTO)
        {
            var checkoutData = new AppDataBL(Context).OnlineStoreGenerateOrder(checkoutPaymentDTO);

            try
            {
                if (checkoutData.TransactionHeadIds != null && checkoutData.TransactionHeadIds.Count > 0)
                {
                    //var reportgenBL = new ReportGenerationBL(Context);

                    var studentData = new AppDataBL(Context).GetStudentDetailsByHeadID(checkoutData.TransactionHeadIds[0].ToString());
                    //var studentData = reportgenBL.GetStudentDetails(checkoutData.TransactionHeadIds[0].ToString(), checkoutData.TransactionNo.ToString());
                    var canteenSOPrefix = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CANTEEN_SO_PREFIX");

                    if (checkoutData.TransactionNo.Contains(canteenSOPrefix))
                    {
                        //string[] dataList = studentData.Split('#');
                        //var mailID = dataList[1];
                        //var admissionNo = dataList[0];
                        //var schoolID = Convert.ToByte(dataList[2]);

                        var mailSent = CartMapper.Mapper(Context).MailSendAfterCanteenOrderGeneration(studentData, checkoutData.TransactionNo);
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
            }

            return checkoutData;
        }

        private PaymentLogDTO QPAYSendRequest(String secretKey, String merchantId, String OriginalPUN, String bankId, string merchantGatewayUrl, String lang, PaymentLogDTO paymentLog)
        {
            try
            {
                var client = new RestSharp.RestClient(merchantGatewayUrl);
                // RestSharp.RestClient("https://pguat.qcb.gov.qa/qcbpg/api/gateway/2.0");

                var request = new RestRequest("", Method.Post);
                request.AddHeader("Accepts", "application/x-www-form-urlencoded");
                request.AddParameter("Action", "14"); request.AddParameter("BankID",
                bankId); request.AddParameter("Lang", lang);
                request.AddParameter("MerchantID", merchantId);
                request.AddParameter("OriginalPUN", OriginalPUN);
                String secureHash = GenerateSecureHash(secretKey, merchantId,
               OriginalPUN, bankId, lang);
                request.AddParameter("SecureHash", secureHash);
                RestResponse response = client.Execute(request);

                string originalStatus = string.Empty;
                string originalMessage = string.Empty;
                string originalSecureHash = string.Empty;
                if (response.Content != null)
                {
                    var nvc = HttpUtility.ParseQueryString(response.Content);

                    originalStatus = nvc["Response.OriginalStatus"];
                    originalMessage = nvc["Response.OriginalStatusMessage"];
                    originalSecureHash = nvc["Response.SecureHash"];
                }

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    paymentLog.ResponseLog = "StatusCode:" + response.StatusCode.ToString() + "#" + response.StatusDescription;
                    paymentLog.ValidationResult = response.ToString();
                    return paymentLog;
                }
                else
                {
                    NameValueCollection nvCollection =
                   HttpUtility.ParseQueryString(response.Content);
                    SortedDictionary<string, string> dictionary = new
                   SortedDictionary<String,
                    String>(StringComparer.Ordinal);
                    foreach (string k in nvCollection)
                    {
                        dictionary.Add(k, nvCollection[k]);
                    }
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append(secretKey);
                    String receivedHash = dictionary["Response.SecureHash"];
                    Console.WriteLine("Received Secure Hash " + receivedHash);
                    foreach (KeyValuePair<string, string> kv in dictionary)
                    {
                        Console.WriteLine(kv.Key);
                        if (kv.Key.Contains("Message"))
                        {
                            stringBuilder.Append(kv.Value.Replace(" ", "+"));
                            continue;
                        }
                        if (kv.Key != "Response.SecureHash")
                            stringBuilder.Append(kv.Value);
                    }

                    Console.WriteLine("Hash String: " + stringBuilder.ToString());
                    String calculatedSecureHash = ComputeSha256Hash(stringBuilder.ToString());
                    Console.WriteLine("Calculated Secure Hash " + calculatedSecureHash);

                    if (calculatedSecureHash == originalSecureHash && originalStatus == "0000")
                    {
                        paymentLog.ResponseLog = "SUCCESS";
                        paymentLog.ValidationResult = response.Content;
                    }
                    else
                    {
                        paymentLog.ResponseLog = "calculatedSecureHash:'"+ calculatedSecureHash+ "'#originalSecureHash: '"+ originalSecureHash + "'#originalStatus:" + originalStatus + "#originalMessage"+originalMessage+"";
                        paymentLog.ValidationResult = response.Content;
                    }
                }

                return paymentLog;
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<string>.Fatal("QPAYSendRequest ex message" + ex.Message, ex);
                paymentLog.ValidationResult = ex.Message;
                paymentLog.ResponseLog = "FAILURE";
            }

            return paymentLog;
        }


        private string GenerateSecureHash(string secretKey, string merchantId, string OriginalPUN, string bankId, string lang)
        {
            StringBuilder hashBuilder = new StringBuilder();
            hashBuilder.Append(secretKey);
            hashBuilder.Append("14"); hashBuilder.Append(bankId);
            hashBuilder.Append(lang);
            hashBuilder.Append(merchantId);
            hashBuilder.Append(OriginalPUN);
            string hashBuilderString = hashBuilder.ToString();
            Console.WriteLine("hashBuilderString: " + hashBuilderString);
            string secureHash = ComputeSha256Hash(hashBuilderString);

            return secureHash;
        }

        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public string ValidatePaymentByTransaction(string transID, byte? paymentModeID = null, decimal? totalAmountCollected = null)
        {
            var validationStatus = string.Empty;
            var cartID = string.Empty;
            var merchantID = string.Empty;
            var settingBL = new Domain.Setting.SettingBL(Context);

            var masterVisaData = PaymentMasterVisaMapper.Mapper(Context).GetPaymentMasterVisaDataByTransID(transID);

            if (!paymentModeID.HasValue)
            {
                paymentModeID = !string.IsNullOrEmpty(masterVisaData.CardType) && masterVisaData.CardType?.ToLower() == Eduegate.Services.Contracts.Enums.PaymentGatewayType.QPAY.ToString()?.ToLower() ? (byte?)Eduegate.Services.Contracts.Enums.PaymentGatewayType.QPAY : null;
            }

            if (masterVisaData != null)
            {
                var amount = Convert.ToDecimal(masterVisaData.PaymentAmount);

                var paymentLog = new PaymentLogDTO
                {
                    RequestType = "Payment validation",
                    TrackID = masterVisaData.TrackIID,
                    TransNo = masterVisaData.TransID,
                    CartID = masterVisaData.CartID,
                    CustomerID = masterVisaData.CustomerID,
                    LoginID = masterVisaData.LoginID,
                    Amount = amount
                };

                if (masterVisaData.Response != null)
                {
                    if (paymentModeID == (byte?)Eduegate.Services.Contracts.Enums.PaymentGatewayType.QPAY)
                    {
                        var nvc = HttpUtility.ParseQueryString(masterVisaData.Response);

                        if (nvc["result"] == "SUCCESS")
                        {
                            cartID = masterVisaData.TransID;
                        }
                    }
                    else
                    {
                        dynamic resultObject = JsonConvert.DeserializeObject(masterVisaData.Response);
                        if (resultObject.result == "SUCCESS")
                        {
                            cartID = masterVisaData.TransID;
                        }
                    }
                }

                if (paymentModeID == (byte?)Eduegate.Services.Contracts.Enums.PaymentGatewayType.QPAY)
                {
                    paymentLog.ResponseLog = "SUCCESS";

                    merchantID = settingBL.GetSettingValue<string>("QPAY-MERCHANTID");

                    string secretKey = settingBL.GetSettingValue<string>("QPAY-SECRETKEY");

                    string bankID = settingBL.GetSettingValue<string>("QPAY_BANKID");

                    string merchantGatewayUrl = settingBL.GetSettingValue<string>("QPAY-MERCHANTGATEWAY");

                    paymentLog = QPAYSendRequest(secretKey, merchantID, masterVisaData.TrackIID.ToString(), bankID, merchantGatewayUrl, "En", paymentLog);

                    validationStatus = paymentLog.ResponseLog;

                    if (paymentLog.ResponseLog.ToLower() == "success")
                    {
                        masterVisaData.MerchantID = merchantID;
                        masterVisaData.SuccessStatus = true;
                    }
                    else
                    {
                        masterVisaData.MerchantID = merchantID;
                        masterVisaData.SuccessStatus = false;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(cartID))
                    {
                        try
                        {
                            ServicePointManager.Expect100Continue = true;
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                            merchantID = settingBL.GetSettingValue<string>("MERCHANTID");
                            var password = settingBL.GetSettingValue<string>("MERCHANTPASSWORD");
                            string encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes("merchant." + merchantID + ":" + password));

                            var requestUri = string.Format("{0}/merchant/{1}/order/{2}",
                                settingBL.GetSettingValue<string>("MERCHANTGATEWAY2"),
                                settingBL.GetSettingValue<string>("MERCHANTID"),
                                cartID);
                            var webRequest = WebRequest.Create(requestUri);
                            Uri uri = new Uri(requestUri);
                            webRequest.Headers.Add(HttpRequestHeader.Authorization, "Basic " + encoded);
                            webRequest.Method = "GET";
                            var response = webRequest.GetResponse();

                            using (var dataStream = response.GetResponseStream())
                            {
                                var reader = new StreamReader(dataStream);
                                var responseFromServer = reader.ReadToEnd();
                                dynamic data = JsonConvert.DeserializeObject(responseFromServer);
                                var validation = data.result == "SUCCESS" && data.transaction[0].result == "SUCCESS" && (data.amount == amount ? true : false);

                                paymentLog.ValidationResult = data.ToString();
                                paymentLog.ResponseLog = data.transaction[0].result;

                                validationStatus = paymentLog.ResponseLog;

                                if (paymentLog.ResponseLog.ToLower() == "success")
                                {
                                    masterVisaData.MerchantID = merchantID;
                                    masterVisaData.SuccessStatus = true;
                                }
                                else
                                {
                                    masterVisaData.MerchantID = merchantID;
                                    masterVisaData.SuccessStatus = false;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            paymentLog.ResponseLog = ex.Message;

                            validationStatus = "FAILURE";

                            masterVisaData.MerchantID = merchantID;
                            masterVisaData.SuccessStatus = false;
                        }
                    }
                    else
                    {
                        paymentLog.ResponseLog = "Transaction number not received";

                        validationStatus = "FAILURE";

                        masterVisaData.MerchantID = merchantID;
                        masterVisaData.SuccessStatus = false;
                    }
                }

                PaymentLogMapper.Mapper(Context).LogPaymentLog(paymentLog);
                PaymentMasterVisaMapper.Mapper(Context).UpdatePaymentMasterVisa(masterVisaData);
            }

            return validationStatus;
        }

    }
}
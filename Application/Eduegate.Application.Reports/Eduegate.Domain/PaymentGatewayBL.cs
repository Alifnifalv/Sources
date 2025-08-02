using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Mappers.Payment;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Services.Contracts.Payments;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace Eduegate.Domain
{
    public class PaymentGatewayBL
    {
        private CallContext Context { get; set; }

        public PaymentGatewayBL(CallContext context)
        {
            Context = context;
        }

        public string GenerateCardSessionID()
        {
            var loginID = Context.LoginID;

            var cartID = loginID.HasValue ? loginID : 0;

            var logData = PaymentLogMapper.Mapper(Context).GetLastLogData();

            var amount = Convert.ToDecimal(logData.Amount);

            string responseFromServer;
            //get a unique number
            //Random ran = new Random();
            //int randomno = ran.Next(0, 100);
            //int randomno = ran.Next(0, (int)DateTime.Now.Ticks);
            //int randomno = ran.Next();

            string timeBaseRandomNumber = DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);

            //var uniqueCartID = cartID.ToString() + "_" + randomno.ToString();

            var uniqueCartID = cartID.ToString() + "_" + timeBaseRandomNumber;

            var result = GetPaymentSessionFromCardAPI(uniqueCartID, amount, out responseFromServer);

            //reconfirm the session with api if any entry exists or not
            var masterVisaData = new PaymentRepository().MakePaymentEntry(new PaymentMasterVisa()
            {
                CustomerID = Convert.ToInt64(loginID),
                PaymentAmount = amount,
                //CartID = cartID,
                TransID = uniqueCartID,
                Response = responseFromServer,
                LogType = "INITIATE_CHECKOUT",
                InitOn = DateTime.Now,
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

        public string GetPaymentSessionFromCardAPI(string referenceID, decimal amount, out string serverResponse)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var settingBL = new SettingBL(Context);

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

            var masterVisaData = PaymentMasterVisaMapper.Mapper(Context).GetPaymentMasterVisaData();

            if (masterVisaData != null)
            {
                var amount = Convert.ToDecimal(masterVisaData.PaymentAmount);

                var paymentLog = new PaymentLogDTO
                {
                    RequestType = "Payment validation",
                    TransNo = masterVisaData.TransID,
                    CartID = masterVisaData.CartID,
                    Amount = amount
                };

                if (masterVisaData.Response != null)
                {
                    var nvc = HttpUtility.ParseQueryString(masterVisaData.Response);

                    if (nvc["result"] == "SUCCESS")
                    {
                        cartID = masterVisaData.TransID;
                    }
                }

                if (!string.IsNullOrEmpty(cartID))
                {
                    try
                    {
                        ServicePointManager.Expect100Continue = true;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                        var settingBL = new SettingBL(Context);
                        var username = settingBL.GetSettingValue<string>("MERCHANTID");
                        var password = settingBL.GetSettingValue<string>("MERCHANTPASSWORD");
                        string encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes("merchant." + username + ":" + password));

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
                        }
                    }
                    catch (Exception ex)
                    {
                        paymentLog.ResponseLog = ex.Message;

                        validationStatus = "FAILURE";
                    }
                }
                else
                {
                    paymentLog.ResponseLog = "Transaction number not received";

                    validationStatus = "FAILURE";
                }

                PaymentLogMapper.Mapper(Context).LogPaymentLog(paymentLog);
            }

            return validationStatus;
        }

        public string GenerateCardSessionIDByTransactionNo(string transactionNo)
        {
            var settingBL = new SettingBL(Context);

            var paymentTransactionPrefix = settingBL.GetSettingValue<string>("ONLINE_PAYMENT_TRANSACTION_PREFIX");

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
                CustomerID = Convert.ToInt64(loginID),
                PaymentAmount = amount,
                TransID = transID,
                Response = responseFromServer,
                LogType = "INITIATE_CHECKOUT",
                InitOn = DateTime.Now,
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

        public string GenerateCardSessionIDByCart(long shoppingCartID, decimal totalAmount)
        {
            var cartID = shoppingCartID;

            var loginID = Context.LoginID;

            var amount = totalAmount;

            string responseFromServer;

            string timeBaseRandomNumber = DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);

            var uniqueCartID = cartID.ToString() + "_" + timeBaseRandomNumber;

            var result = GetPaymentSessionFromCardAPI(uniqueCartID, amount, out responseFromServer);

            //reconfirm the session with api if any entry exists or not
            var masterVisaData = new PaymentRepository().MakePaymentEntry(new PaymentMasterVisa()
            {
                CustomerID = Convert.ToInt64(loginID),
                PaymentAmount = amount,
                CartID = cartID,
                TransID = uniqueCartID,
                Response = responseFromServer,
                LogType = "INITIATE_CHECKOUT",
                InitOn = DateTime.Now,
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

        public bool PaymentValidationByCartID(long cartID)
        {
            bool validation = false;

            var transID = string.Empty;

            var masterVisaData = PaymentMasterVisaMapper.Mapper(Context).GetPaymentMasterVisaDataByCartID(cartID);

            if (masterVisaData != null)
            {
                var amount = Convert.ToDecimal(masterVisaData.PaymentAmount);

                var paymentLog = new PaymentLogDTO
                {
                    RequestType = "Payment validation",
                    TransNo = masterVisaData.TransID,
                    CartID = masterVisaData.CartID,
                    Amount = amount,
                    CustomerID = masterVisaData.CustomerID,
                };

                if (masterVisaData.Response != null)
                {
                    var nvc = HttpUtility.ParseQueryString(masterVisaData.Response);

                    if (nvc["result"] == "SUCCESS")
                    {
                        transID = masterVisaData.TransID;
                    }
                }

                if (!string.IsNullOrEmpty(transID))
                {
                    try
                    {
                        ServicePointManager.Expect100Continue = true;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                        var settingBL = new SettingBL(Context);
                        var username = settingBL.GetSettingValue<string>("MERCHANTID");
                        var password = settingBL.GetSettingValue<string>("MERCHANTPASSWORD");
                        string encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes("merchant." + username + ":" + password));

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
                        }
                    }
                    catch (Exception ex)
                    {
                        paymentLog.ResponseLog = ex.Message;

                        validation = false;
                    }
                }
                else
                {
                    paymentLog.ResponseLog = "Transaction number not received";

                    validation = false;
                }

                PaymentLogMapper.Mapper(Context).LogPaymentLog(paymentLog);
            }
            else
            {
                validation = false;
            }

            return validation;
        }

        public string ValidatePaymentByTransaction(string transID, decimal? totalAmountCollected)
        {
            var validationStatus = string.Empty;

            var cartID = transID;
            var amount = totalAmountCollected;

            if (!string.IsNullOrEmpty(cartID))
            {
                try
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    var settingBL = new SettingBL(Context);
                    var username = settingBL.GetSettingValue<string>("MERCHANTID");
                    var password = settingBL.GetSettingValue<string>("MERCHANTPASSWORD");
                    string encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes("merchant." + username + ":" + password));

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

                        validationStatus = data.transaction[0].result;
                    }
                }
                catch (Exception ex)
                {
                    _ = ex.Message;
                    validationStatus = "FAILURE";
                }
            }
            else
            {
                validationStatus = "FAILURE";
            }

            return validationStatus;
        }

    }
}
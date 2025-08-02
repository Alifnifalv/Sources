using Eduegate.Domain.Repository;
using Eduegate.Application.Mvc;
using Eduegate.Services.Client.Factory;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace Eduegate.ERP.School.ParentPortal.Controllers
{
    public class MasterCardPaymentController : BaseController
    {
        public ActionResult GetMasterCardSessionID(byte? paymentModeID = null)
        {
            var result = ClientFactory.PaymentGatewayServiceClient(CallContext).GenerateCardSessionID(paymentModeID);

            if (result != null)
            {
                return Json(new { IsError = false, Response = "Successfully generated" });
            }
            else
            {
                return Json(new { IsError = true, Response = "There are some issues!" });
            }

        }

        public ActionResult GenerateMasterCardSessionIDByTransactionNo(string transactionNo, byte? paymentModeID = null)
        {
            var result = ClientFactory.PaymentGatewayServiceClient(CallContext).GenerateCardSessionIDByTransactionNo(transactionNo, paymentModeID);

            if (result != null)
            {
                return Json(new { IsError = false, Response = "Successfully generated" });
            }
            else
            {
                return Json(new { IsError = true, Response = "There are some issues!" });
            }

        }

        #region Not used for fee payment
        public string GetPaymentSessionFromMasterCardAPI(string referenceID, decimal amount, out string serverResponse)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var settingBL = new Domain.Setting.SettingBL(CallContext);

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

        public string GetMasterCartSessionIDOld(long cartID)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var settingBL = new Domain.Setting.SettingBL(CallContext);
            string username = settingBL.GetSettingValue<string>("MERCHANTID");
            string password = settingBL.GetSettingValue<string>("MERCHANTPASSWORD");
            string encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
            var requestUri = string.Format("{0}/session", settingBL.GetSettingValue<string>("MERCHANTGATEWAY"));
            var webRequest = WebRequest.Create(requestUri);
            Uri uri = new Uri(requestUri);
            webRequest.Headers.Add(HttpRequestHeader.Authorization, "Basic " + encoded);
            webRequest.Method = "POST";
            var response = webRequest.GetResponse();
            using (var dataStream = response.GetResponseStream())
            {
                var reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                return responseFromServer;
            }
        }

        public bool ValidateMasterCardSuccessResponse(long cartID, string returnIndicator)
        {
            try
            {
                if (returnIndicator == null)
                {
                    var status1 = ValidateCartPayment(cartID, cartID.ToString());

                    if (!status1)
                    {
                        var payments = new PaymentRepository().GetAllMasterVisaDetails(cartID);

                        if (payments != null)
                        {
                            var paidStatus = false;
                            foreach (var payment in payments)
                            {
                                paidStatus = ValidateCartPayment(payment.CartID.Value, payment.TransID);

                                if (paidStatus)
                                {
                                    break;
                                }
                            }

                            return paidStatus;
                        }
                    }
                    else
                    {
                        return status1;
                    }
                }

                var paymentDetails = new PaymentRepository().GetMasterVisaDetails(cartID);
                if (paymentDetails != null)
                {
                    var nvc = HttpUtility.ParseQueryString(paymentDetails.Response);
                    var result = nvc["successIndicator"] == returnIndicator;

                    if (result)
                    {
                        return result;
                    }
                }

                var status = ValidateCartPayment(cartID, cartID.ToString());

                if (!status)
                {
                    var payment = new PaymentRepository().GetMasterVisaDetails(cartID);

                    if (payment != null)
                    {
                        return ValidateCartPayment(payment.CartID.Value, payment.TransID);
                    }
                }
                else
                {
                    return status;
                }

                return false;
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<MasterCardPaymentController>.Fatal(ex.Message, ex);
                return false;
            }
        }

        public bool ValidateCartPayment(long actualCartID, string cartID)
        {
            try
            {
                var logData = ClientFactory.SchoolServiceClient(CallContext).GetLastLogData();

                var amount = Convert.ToDecimal(logData.Amount);

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var settingBL = new Domain.Setting.SettingBL(CallContext);
                var username = settingBL.GetSettingValue<string>("MERCHANTID");
                var password = settingBL.GetSettingValue<string>("MERCHANTPASSWORD");
                string encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes("merchant." + username + ":" + password));

                //Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));


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
                    return data.result == "SUCCESS" && data.transaction[0].result == "SUCCESS" && data.amount == amount ? true : false;
                }
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<MasterCardPaymentController>.Fatal(ex.Message, ex);
                return false;
            }
        }
        #endregion

    }
}
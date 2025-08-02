using Eduegate.Domain;
using Eduegate.Application.Mvc;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.ViewModels.Payment;
using System.Web;
using Microsoft.AspNetCore.Mvc;

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

        public ActionResult Initiate()
        {
            return View();
        }

        public ActionResult FeePaymentFromMobile(long? loginID, string emailID, string userID)
        {
            if (!CallContext.LoginID.HasValue)
            {
                CallContext.LoginID = loginID;
                CallContext.EmailID = emailID;
                CallContext.UserId = userID;
            }

            return InitiateCardFeePayment();
        }

        public ActionResult InitiateCardFeePayment()
        {
            var settingBL = new SettingBL(CallContext);

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
                    var nvc = HttpUtility.ParseQueryString(data.Response);

                    if (nvc["result"] == "SUCCESS")
                    {
                        string session = nvc["session.id"];

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

        public ActionResult Fail()
        {
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

        public ActionResult RetryCancelledPayment()
        {
            return InitiateCardFeePayment();
        }

        //To retry payment
        public ActionResult RetryPayment(string transactionNumber)
        {
            return RedirectToAction("GenerateMasterCardSessionIDByTransactionNo", "MasterCardPayment", new { transactionNo = transactionNumber });
        }

        public ActionResult InitiatePayment()
        {
            return RedirectToAction("GetMasterCardSessionID", "MasterCardPayment");
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

        public ActionResult OnlinePaymentFromMobile(long? loginID, string emailID, string userID)
        {
            if (!CallContext.LoginID.HasValue)
            {
                CallContext.LoginID = loginID;
                CallContext.EmailID = emailID;
                CallContext.UserId = userID;
            }
            return InitiateCardOnlinePayment();
        }

        public ActionResult InitiateCardOnlinePayment()
        {
            var settingBL = new SettingBL(CallContext);

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

        public ActionResult errorCallback()
        {
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
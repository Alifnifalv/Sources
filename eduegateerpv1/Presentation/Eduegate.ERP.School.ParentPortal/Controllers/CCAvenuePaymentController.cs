using Eduegate.Domain;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Eduegate.ERP.School.ParentPortal.Controllers
{
    public class CCAvenuePaymentController : Controller
    {
        CallContext _callContext;

        public CCAvenuePaymentController(CallContext callContext)
        {
            _callContext = callContext;
        }

        public string GetEcryptedRequestValue(long cartID, long shippingAddressID)
        {
            var amount = 0;
            //var amount = CartManager
            //    .Manager(_callContext)
            //    .GetCartTotalAmount(cartID);
            //var address = new AppDataBL(_callContext).GetAddressByContactID(shippingAddressID).GetAwaiter().GetResult();
            //var name = address?.FirstName + (address?.LastName == null ? "" : " " + address?.LastName);
            //var email = address.AlternateEmailID1 != null ? address.AlternateEmailID1 : name.Trim().ToLower() + address.LoginID + "@nabasmarket.com";
            //var ccaCrypto = new CCACrypto();
            var settingBL = new SettingBL(_callContext);
            string marchantID = settingBL.GetSettingValue<string>("CCAVENUE_MARCHANTID");
            string redirectUrl = settingBL.GetSettingValue<string>("CCAVENUE_REDIRECTURL");
            string cancelUrl = settingBL.GetSettingValue<string>("CCAVENUE_CANCELURL");
            string workingKey = settingBL.GetSettingValue<string>("CCAVENUE_WORKINGKEY");//put in the 32bit alpha numeric key in the quotes provided here
            //string ccaRequest = $"merchant_id={marchantID}&order_id={cartID}&currency=AED&amount={amount}&redirect_url={redirectUrl}&cancel_url={cancelUrl}&language=EN&billing_email={email}&billing_tel={address.MobileNo1}&billing_country={"United Arab Emirates"}&billing_name={name}&billing_address={address.BuildingNo + "," + address.Location}&billing_city={address.AreaName}";
            //var strEncRequest = ccaCrypto.Encrypt(ccaRequest, workingKey);
            //return strEncRequest;
            return null;
        }

        public string GetAccessCode()
        {
            var settingBL = new SettingBL(_callContext);
            return settingBL.GetSettingValue<string>("CCAVENUE_ACCESSCODE");
        }

        public bool ValidateMasterCardSuccessResponse(long cartID)
        {
            var cartAmount = 0;
            //var cartAmount = CartManager
            //    .Manager(_callContext)
            //    .GetCartTotalAmount(cartID);
            //var ccaCrypto = new CCACrypto();
            var settingBL = new SettingBL(_callContext);
            var paymentLog = new PaymentRepository().GetEncriptedResponse(cartID);
            var encResponse = paymentLog.ResponseLog;
            string workingKey = settingBL.GetSettingValue<string>("CCAVENUE_WORKINGKEY"); //put in the 32bit alpha numeric key in the quotes provided here 	
            //string decResponse = ccaCrypto.Decrypt(encResponse, workingKey);
            //var parameters = decResponse.Split('&');
            //var responseAmount = Convert.ToDecimal(Array.Find(parameters, element => element.Contains("amount=")).Split('=')[1]);
            //return parameters.Contains("order_status=Success") && parameters.Contains("order_id=" + cartID) && responseAmount == cartAmount;
            return true;
        }

    }
}
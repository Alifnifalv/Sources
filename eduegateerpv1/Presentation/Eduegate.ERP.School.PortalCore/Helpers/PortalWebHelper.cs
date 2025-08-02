using Eduegate.Framework;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts;
using System.Text;

namespace Eduegate.ERP.Admin.Helpers
{
    public class PortalWebHelper
    {
        public static string InsertSpacesBetweenCapitalLetters(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var result = new StringBuilder();
            foreach (char c in input)
            {
                if (char.IsUpper(c) && result.Length > 0)
                    result.Append(' '); // Insert a space before capital letter
                result.Append(c);
            }
            return result.ToString();
        }

        public static void GetDefaultSettingsToViewBag(CallContext context, dynamic ViewBag)
        {
            var setting = ClientFactory.SettingServiceClient(context).GetSettingDetail("DEFAULT_CURRENCY_CODE");

            if (setting != null && setting.SettingValue != null)
            {
                ViewBag.DefaultCurrencyCode = setting.SettingValue;
            }
            else
            {
                ViewBag.DefaultCurrencyCode = "$";
            }

            setting = ClientFactory.SettingServiceClient(context).GetSettingDetail("DEFAULT_DECIMALPOINTS");

            if (setting != null && setting.SettingValue != null)
            {
                ViewBag.DefaultDecimalPoints = setting.SettingValue;
            }
            else
            {
                ViewBag.DefaultDecimalPoints = "2";
            }
        }

        public static string GetErrorMessage(string errorCode)
        {
            var userMessage = "Some error occured while processing! Please try again.";
            switch (errorCode)
            {
                case ErrorCodes.PurchaseInvoice.SI001:
                    userMessage = "Product quantity did not match.";
                    break;
                case ErrorCodes.PurchaseInvoice.SI002:
                    userMessage = "Serial Number length did not match for 1 or more products.";
                    break;
                case ErrorCodes.PurchaseInvoice.SI003:
                    userMessage = "Serial Number duplicated.";
                    break;
                case ErrorCodes.PurchaseInvoice.SI004:
                    userMessage = "No rights to make a digital product invoice.";
                    break;
                case ErrorCodes.Transaction.T001:
                    userMessage = "Transaction can not be completed manually.";
                    break;
                case ErrorCodes.Transaction.T002:
                    userMessage = "Transaction already processed/initiated processing!";
                    break;
                case ErrorCodes.Transaction.T003:
                    userMessage = "Entitlement Amount does not Match with Total Amount!";
                    break;
                case ErrorCodes.Transaction.T004:
                    userMessage = "Serial number required!";
                    break;
                case ErrorCodes.Payment.P001:
                    userMessage = "Payment initiated from merchant";
                    break;

                case ErrorCodes.Payment.P002:
                    userMessage = "Payment failed at processor";
                    break;

                case ErrorCodes.Payment.P003:
                    userMessage = "Payment response failed at merchant";
                    break;

                case ErrorCodes.Payment.P004:
                    userMessage = "Response SHA signature verification failed";
                    break;

                case ErrorCodes.Payment.P005:
                    userMessage = "Payment cancelled by user";
                    break;

                case ErrorCodes.Payment.P006:
                    userMessage = "Payment successful";
                    break;
                case ErrorCodes.Payment.P007:
                    userMessage = "Payment successful from payfort response URL";
                    break;
                case ErrorCodes.Payment.P008:
                    userMessage = "Payment successful from payfort notification URL";
                    break;
                case ErrorCodes.Transaction.T005:
                    userMessage = "Transaction cannot be processed/product is not available!";
                    break;
                case ErrorCodes.SKU.S001:
                    userMessage = "Profit percentage can not be less than 10%";
                    break;
                case ErrorCodes.Brand.B001:
                    userMessage = "Brand Name Already Exists";
                    break;
                default:
                    userMessage = "Transaction could not saved.";
                    break;
            }
            return userMessage;
        }

    }
}
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text;
using System.Text.Encodings.Web;
using System.Xml.Linq;

namespace Eduegate.ERP.Admin.Helpers
{
    public static class SkienHelperCore
    {
		public static string GetString(IHtmlContent content)
        {
            if(content == null)
            {
                return string.Empty;
            }

            using (var writer = new System.IO.StringWriter())
            {
                content.WriteTo(writer, HtmlEncoder.Default);
                return writer.ToString();
            }
        }

        public static HtmlString GetValidationAttributes(this IHtmlHelper helper, IHtmlContent htmlString, string namePrefix = "")
        {
            var attributes = new StringBuilder();
            var content = GetString(htmlString);

            try
            {
                XElement element = XElement.Parse("<r>" + content + "</r>");

                foreach (var attribs in element.Elements().Attributes())
                {
                    attributes.Append(attribs.Name.LocalName);
                    attributes.Append("=");

                    if (attribs.Name.LocalName.Equals("id") || attribs.Name.LocalName.Equals("name"))
                        attributes.Append("\"" + namePrefix + attribs.Value + "\"");
                    else
                        attributes.Append("\"" + attribs.Value + "\"");

                    attributes.Append(" ");
                }

                return new HtmlString(attributes.ToString());
            }
            catch
            {
                return new HtmlString(attributes.ToString());
            }
        }

        //public static string GetErrorMessage(string errorCode)
        //{
        //    var userMessage = "Some error occured while processing! Please try again.";
        //    switch (errorCode)
        //    {
        //        case ErrorCodes.PurchaseInvoice.SI001:
        //            userMessage = "Product quantity did not match.";
        //            break;
        //        case ErrorCodes.PurchaseInvoice.SI002:
        //            userMessage = "Serial Number length did not match for 1 or more products.";
        //            break;
        //        case ErrorCodes.PurchaseInvoice.SI003:
        //            userMessage = "Serial Number duplicated.";
        //            break;
        //        case ErrorCodes.PurchaseInvoice.SI004:
        //            userMessage = "No rights to make a digital product invoice.";
        //            break;
        //        case ErrorCodes.Transaction.T001:
        //            userMessage = "Transaction can not be completed manually.";
        //            break;
        //        case ErrorCodes.Transaction.T002:
        //            userMessage = "Transaction already processed/initiated processing!";
        //            break;
        //        case ErrorCodes.Transaction.T003:
        //            userMessage = "Entitlement Amount does not Match with Total Amount!";
        //            break;
        //        case ErrorCodes.Transaction.T004:
        //            userMessage = "Serial number required!";
        //            break;
        //        case ErrorCodes.Payment.P001:
        //            userMessage = "Payment initiated from merchant";
        //            break;

        //        case ErrorCodes.Payment.P002:
        //            userMessage = "Payment failed at processor";
        //            break;

        //        case ErrorCodes.Payment.P003:
        //            userMessage = "Payment response failed at merchant";
        //            break;

        //        case ErrorCodes.Payment.P004:
        //            userMessage = "Response SHA signature verification failed";
        //            break;

        //        case ErrorCodes.Payment.P005:
        //            userMessage = "Payment cancelled by user";
        //            break;

        //        case ErrorCodes.Payment.P006:
        //            userMessage = "Payment successful";
        //            break;
        //        case ErrorCodes.Payment.P007:
        //            userMessage = "Payment successful from payfort response URL";
        //            break;
        //        case ErrorCodes.Payment.P008:
        //            userMessage = "Payment successful from payfort notification URL";
        //            break;
        //        case ErrorCodes.Transaction.T005:
        //            userMessage = "Transaction cannot be processed/product is not available!";
        //            break;
        //        case ErrorCodes.SKU.S001:
        //            userMessage = "Profit percentage can not be less than 10%";
        //            break;
        //        case ErrorCodes.Brand.B001:
        //            userMessage = "Brand Name Already Exists";
        //            break;
        //        default:
        //            userMessage = "Transaction could not saved.";
        //            break;
        //    }
        //    return userMessage;
        //}

        public static dynamic GetPageViewBag(this IHtmlHelper html)
        {
            if (html == null || html.ViewContext == null) //this means that the page is root or parial view
            {
                return html.ViewBag;
            }

            return html.ViewContext.ViewBag;

            //while (controller.ControllerContext.IsChildAction)  //traverse hierachy to get root controller
            //{
            //    controller = controller.ControllerContext.ParentActionViewContext.Controller;
            //}

            //return controller.ViewBag;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "EmailNotificationTypes")]
    public enum EmailNotificationTypes
    {
        [Description("All")]
        [EnumMember]
        All = 0,
        [Description("Forget Password")]
        [EnumMember]
        ForgetPassword = 1,
        [Description("Registration")]
        [EnumMember]
        Registration = 2,
        [Description("Reset Password")]
        [EnumMember]
        ResetPassword = 3,
        [Description("Customer Registration")]
        [EnumMember]
        CustomerRegistration = 4,
        [Description("Contact us")]
        [EnumMember]
        ContactUs = 5,
        [Description("Order Confirmation")]
        [EnumMember]
        OrderConfirmation = 6,
        [Description("Order Delivery")]
        [EnumMember]
        OrderDelivery = 7,
        [Description("Order Notification For Supplier")]
        [EnumMember]
        OrderNotificationForSupplier = 9,
        [Description("Resend Email")]
        [EnumMember]
        ResendMail = 8,
        [Description("Order Dispatch")]
        [EnumMember]
        OrderDispatch = 10,
        [Description("Support Ticket Alert")]
        [EnumMember]
        SupportTicketAlert = 11,
    }
}

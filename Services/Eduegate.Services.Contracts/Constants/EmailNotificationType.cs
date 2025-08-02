using System.ComponentModel;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Eduegate.Services.Contracts.Constants
{
    [DataContract(Name = "EmailNotificationType")]
    public class EmailNotificationType
    {
        [DataMember]
        public const string Subject = "Subject";

        [DataContract(Name = "ForgetPassword")]
        public class ForgetPassword
        {
            [DataContract(Name = "Keys")]
            public class Keys
            {
                [DataMember]
                public const string ResetPasswordLink = "ResetPasswordLink";

            }
        }

        [DataContract(Name = "Registration")]
        public class Registration
        {
            [DataContract(Name = "Keys")]
            public class Keys
            {
                [DataMember]
                public const string RegistrationLink = "RegistrationLink";
                [DataMember]
                public const string Title = "Title";
                [DataMember]
                public const string FirstName = "FirstName";
                [DataMember]
                public const string LastName = "LastName";
                [DataMember]
                public const string DomainName = "DomainName";
            }
        }

        [DataContract(Name = "ContactUs")]
        public class ContactUs
        {
            [DataContract(Name = "Keys")]
            public class Keys
            {
                [DataMember]
                public const string Name = "Name";
                [DataMember]
                public const string EmailID = "EmailID";
                [DataMember]
                public const string PhoneNumber = "PhoneNumber";
                [DataMember]
                public const string Location = "Location";
                [DataMember]
                public const string Message = "Message";
            }
        }

        [DataContract(Name = "OrderConfirmation")]
        public class OrderConfirmation
        {
            [DataContract(Name = "Keys")]
            public class Keys
            {
                [DataMember]
                public const string OrderID = "OrderID";

                [DataMember]
                public const string OrderNumber = "OrderNumber";

                [DataMember]
                public const string OrderHistoryURL = "OrderHistoryURL";
                
            }
        }

        [DataContract(Name = "OrderNotificationForSupplier")]
        public class OrderNotificationForSupplier
        {
            [DataContract(Name = "Keys")]
            public class Keys
            {
                [DataMember]
                public const string HeadID = "HeadID";

                [DataMember]
                public const string Attachment = "Attachment";

                [DataMember]
                public const string ReportName = "reportName";

                [DataMember]
                public const string ReturnFile = "returnFile";

                [DataMember]
                public const string ErrorMessage = "ErrorMessage";

            }
        }

        [DataContract(Name = "ResendMail")]
        public class ResendMail
        {
            [DataContract(Name = "Keys")]
            public class Keys
            {
                [DataMember]
                public const string HeadID = "HeadID";

            }
        }

        [DataContract(Name = "OrderDispatch")]
        public class OrderDispatch
        {
            [DataContract(Name = "Keys")]
            public class Keys
            {
                [DataMember]
                public const string OrderID = "OrderID";

                [DataMember]
                public const string OrderNumber = "OrderNumber";

                [DataMember]
                public const string OrderHistoryURL = "OrderHistoryURL";

                [DataMember]
                public const string AirwayBillNo = "AirwayBillNo";

                [DataMember]
                public const string JobEntryHeadID = "JobEntryHeadID";
                [DataMember]
                public const string ServiceProviderID = "ServiceProviderID";

            }
        }

        [DataContract(Name = "SupportTicketAlert")]
        public class SupportTicketNotification
        {
            [DataContract(Name = "Keys")]
            public class Keys
            {
                [DataMember]
                public const string OrderID = "OrderID";

                [DataMember]
                public const string TicketID = "TicketID";

                [DataMember]
                public const string ToCCEmailID = "ToCCEmailID";

                [DataMember]
                public const string ToBCCEmailID = "ToBCCEmailID";
            }
        }

    }
}
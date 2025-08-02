using System.ComponentModel;
using System.Runtime.Serialization;
using System.ServiceModel;


namespace Eduegate.Services.Contracts.Constants
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    [DataContract(Name = "EmailNotificationType")]
    public class EmailNotificationType
    {

        [DataMember]
        public const string Subject = "Subject";

        [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
        [DataContract(Name = "ForgetPassword")]
        public class ForgetPassword
        {
            [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
            [DataContract(Name = "Keys")]
            public class Keys
            {
                [DataMember]
                public const string ResetPasswordLink = "ResetPasswordLink";

            }
        }

        [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
        [DataContract(Name = "Registration")]
        public class Registration
        {
            [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
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

        [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
        [DataContract(Name = "ContactUs")]
        public class ContactUs
        {
            [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
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

        [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
        [DataContract(Name = "OrderConfirmation")]
        public class OrderConfirmation
        {
            [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
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


        [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
        [DataContract(Name = "OrderNotificationForSupplier")]
        public class OrderNotificationForSupplier
        {
            [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
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


        [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
        [DataContract(Name = "ResendMail")]
        public class ResendMail
        {
            [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
            [DataContract(Name = "Keys")]
            public class Keys
            {
                [DataMember]
                public const string HeadID = "HeadID";

            }
        }

        [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
        [DataContract(Name = "OrderDispatch")]
        public class OrderDispatch
        {
            [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
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

        [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
        [DataContract(Name = "SupportTicketAlert")]
        public class SupportTicketNotification
        {
            [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
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

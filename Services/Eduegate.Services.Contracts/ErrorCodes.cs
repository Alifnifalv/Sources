using System.ComponentModel;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public static class ErrorCodes
    {
        public static class PurchaseInvoice
        {
            [DataMember]
            [Description("Quantity mismatch!")]
            public const string SI001 = "SI001";

            [DataMember]
            [Description("Serial Number Length mismatch!")]
            public const string SI002 = "SI002";

            [DataMember]
            [Description("Duplicate Serial Number!")]
            public const string SI003 = "SI003";

            [DataMember]
            [Description("No rights to enter serial key!")]
            public const string SI004 = "SI004";
        }

        public static class Transaction
        {
            [DataMember]
            [Description("Transaction can not be completed manually!")]
            public const string T001 = "T001";

            [DataMember]
            [Description("Transaction already processed/initiated processing!")]
            public const string T002 = "T002";

            [DataMember]
            [Description("Entitlement Amount does not Match with Total Amount!")]
            public const string T003 = "T003";

            [DataMember]
            [Description("Serial number required!")]
            public const string T004 = "T004";

            [DataMember]
            [Description("Transaction cannot be processed/product is not available!")]
            public const string T005 = "T005";

            [DataMember]
            [Description("Transaction can not be saved, invalid configurations.!")]
            public const string T006 = "T006";


            [DataMember]
            [Description("This Transaction can not be saved due to monthly closing!")]
            public const string T007 = "T007";
        }

        public static class Payment
        {
            [DataMember]
            [Description("Payment initiated from merchant.")]
            public const string P001 = "P001";

            [DataMember]
            [Description("Payment failed at processor")]
            public const string P002 = "P002";

            [DataMember]
            [Description("Payment response failed at merchant")]
            public const string P003 = "P003";

            [DataMember]
            [Description("Sha sign verification failed")]
            public const string P004 = "P004";

            [DataMember]
            [Description("Payment cancelled by user")]
            public const string P005 = "P005";

            [DataMember]
            [Description("Payment successful")]
            public const string P006 = "P006";

            [DataMember]
            [Description("Payment response received at Payfort Response URL")]
            public const string P007 = "P007";

            [DataMember]
            [Description("Payment response received at Payfort Notification URL")]
            public const string P008 = "P008";
        }

        public static class Voucher
        {
            [DataMember]
            [Description("Expiry Date can't be before today's date")]
            public const string V001 = "V001";
        }

        public static class SKU
        {
            [DataMember]
            [Description("Profit percentage can not be less than 10%")]
            public const string S001 = "S001";
        }

        public static class Brand 
        { 
            [DataMember]
            [Description("Brand Name Already Exists")]
            public const string B001 = "B001";
        }
    }
}

using Newtonsoft.Json;
using System.Runtime.Serialization;
using Eduegate.Framework.Payment;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class MIGSPaymentDTO
    {
        [DataMember]
        public string VpcVersion { get; set; }
        [DataMember]
        public string VpcCommand { get; set; }
        [DataMember]
        public string AccessCode { get; set; }
        [DataMember]
        public string MerchantID { get; set; }
        [DataMember]
        public string VpcLocale { get; set; }
        [DataMember]
        public string VirtualAmount { get; set; }
        [DataMember]
        public string PaymentCurrency { get; set; }
        [DataMember]
        public string ResponseOn { get; set; }
        [DataMember]
        public string ResponseCode { get; set; }
        [DataMember]
        public string CodeDescription { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string ReceiptNumber { get; set; }
        [DataMember]
        public string TransID { get; set; }
        [DataMember]
        public string AcquireResponseCode { get; set; }
        [DataMember]
        public string BankAuthorizationID { get; set; }
        [DataMember]
        public string BatchNo { get; set; }
        [DataMember]
        public string CardType { get; set; }
        [DataMember]
        public string ResponseAmount { get; set; }
    }

}

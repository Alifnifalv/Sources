using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class KNETPaymentDTO 
    {
        [DataMember]
        public string PaymentAction { get; set; }

        [DataMember]
        public string PaymentCurrency { get; set; }

        [DataMember]
        public string PaymentLang { get; set; }

        [DataMember]
        public string InitRawResponse { get; set; }

        [DataMember]
        public string InitErrorMsg { get; set; }

        [DataMember]
        public string AdditionalDetails { get; set; }

        [DataMember]
        public string TransID { get; set; }

        [DataMember]
        public string TransResult { get; set; }

        [DataMember]
        public string TransPostDate { get; set; }

        [DataMember]
        public string TransAuth { get; set; }

        [DataMember]
        public string TransRef { get; set; }
        
        [DataMember]
        public string TransErrorMsg { get; set; }       
    }
}
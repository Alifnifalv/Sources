using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.PaymentGateway
{
    [DataContract]
    public class TheFortPaymentDTO
    {
        [DataMember]
        public long TrackID { get; set; }
        [DataMember]
        public long TrackKey { get; set; }
        [DataMember]
        public long CustomerID { get; set; }
        [DataMember]
        public string SessionID { get; set; }
        [DataMember]
        public long PaymentID { get; set; }
        [DataMember]
        public System.DateTime InitOn { get; set; }
        [DataMember]
        public string InitStatus { get; set; }
        [DataMember]
        public string InitIP { get; set; }
        [DataMember]
        public string InitLocation { get; set; }
        [DataMember]
        public decimal InitAmount { get; set; }
        [DataMember]
        public string PShaRequestPhrase { get; set; }
        [DataMember]
        public string PAccessCode { get; set; }
        [DataMember]
        public string PMerchantIdentifier { get; set; }
        [DataMember]
        public string PCommand { get; set; }
        [DataMember]
        public string PCurrency { get; set; }
        [DataMember]
        public string PCustomerEmail { get; set; }
        [DataMember]
        public string PLang { get; set; }
        [DataMember]
        public string PMerchantReference { get; set; }
        [DataMember]
        public string PSignatureText { get; set; }
        [DataMember]
        public string PSignature { get; set; }
        [DataMember]
        public Nullable<int> PAmount { get; set; }
        [DataMember]
        public short RefCountryID { get; set; }
        [DataMember]
        public string PTransSignature { get; set; }
        [DataMember]
        public string TransID { get; set; }
        [DataMember]
        public Nullable<System.DateTime> POn { get; set; }
        [DataMember]
        public string PTransCommand { get; set; }
        [DataMember]
        public string PTransMerchantReference { get; set; }
        [DataMember]
        public Nullable<int> PTransAmount { get; set; }
        [DataMember]
        public string PTransAccessCode { get; set; }
        [DataMember]
        public string PTransMerchantIdentifier { get; set; }
        [DataMember]
        public string PTransCurrency { get; set; }
        [DataMember]
        public string PTransPaymentOption { get; set; }
        [DataMember]
        public string PTransEci { get; set; }
        [DataMember]
        public string PTransAuthorizationCode { get; set; }
        [DataMember]
        public string PTransOrderDesc { get; set; }
        [DataMember]
        public string PTransResponseMessage { get; set; }
        [DataMember]
        public Nullable<byte> PTransStatus { get; set; }
        [DataMember]
        public Nullable<int> PTransResponseCode { get; set; }
        [DataMember]
        public string PTransCustomerIP { get; set; }
        [DataMember]
        public string PTransCustomerEmail { get; set; }
        [DataMember]
        public Nullable<short> PTransExpiryDate { get; set; }
        [DataMember]
        public string PTransCardNumber { get; set; }
        [DataMember]
        public string PTransCustomerName { get; set; }
        [DataMember]
        public Nullable<decimal> PTransActualAmount { get; set; }
        [DataMember]
        public Nullable<System.DateTime> PTransOn { get; set; }
        [DataMember]
        public Nullable<System.DateTime> TransOn { get; set; }
        [DataMember]
        public Nullable<long> OrderID { get; set; }
        [DataMember]
        public string TServiceCommand { get; set; }
        [DataMember]
        public string TAccessCode { get; set; }
        [DataMember]
        public string TSignatureText { get; set; }
        [DataMember]
        public string TSignature { get; set; }
        [DataMember]
        public string TMerchantReference { get; set; }
        [DataMember]
        public string TLanguage { get; set; }
        [DataMember]
        public string TShaRequestPhrase { get; set; }
        [DataMember]
        public Nullable<int> TAmount { get; set; }
        [DataMember]
        public string TMerchantIdentifier { get; set; }
        [DataMember]
        public string AdditionalDetails { get; set; }
        [DataMember]
        public string FortID { get; set; }

        [DataMember]
        public long CartID { get; set; }
        [DataMember]
        public string CardHolderName { get; set; }
    }
}

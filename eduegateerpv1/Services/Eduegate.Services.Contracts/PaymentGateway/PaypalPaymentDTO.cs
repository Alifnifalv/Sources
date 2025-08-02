using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class PaypalPaymentDTO
    {
        [DataMember]
        public long TrackID { get; set; }

        [DataMember]
        public long TrackKey { get; set; }

        [DataMember]
        public long RefCustomerID { get; set; }

        [DataMember]
        public string BusinessEmail { get; set; }

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
        public string InitCurrency { get; set; }

        [DataMember]
        public bool IpnVerified { get; set; }

        [DataMember]
        public string TransID { get; set; }

        [DataMember]
        public Nullable<decimal> TransAmount { get; set; }

        [DataMember]
        public string TransCurrency { get; set; }

        [DataMember]
        public string TransStatus { get; set; }

        [DataMember]
        public string TransPayerID { get; set; }

        [DataMember]
        public string TransDateTime { get; set; }

        [DataMember]
        public string TransPayerStatus { get; set; }

        [DataMember]
        public string TransPayerEmail { get; set; }

        [DataMember]
        public string TransPaymentType { get; set; }

        [DataMember]
        public string TransMessage { get; set; }

        [DataMember]
        public string TransOn { get; set; }

        [DataMember]
        public string TransReason { get; set; }

        [DataMember]
        public string TransNoOfCart { get; set; }

        [DataMember]
        public string TransAddressStatus { get; set; }

        [DataMember]
        public string TransAddressCountryCode { get; set; }

        [DataMember]
        public string TransAddressZip { get; set; }

        [DataMember]
        public string TransAddressName { get; set; }

        [DataMember]
        public string TransAddressStreet { get; set; }

        [DataMember]
        public string TransAddressCountry { get; set; }

        [DataMember]
        public string TransAddressCity { get; set; }

        [DataMember]
        public string TransAddressState { get; set; }

        [DataMember]
        public string TransResidenceCountry { get; set; }

        [DataMember]
        public Nullable<long> OrderID { get; set; }

        [DataMember]
        public Nullable<long> CartID { get; set; }

        [DataMember]
        public Nullable<bool> IpnHandlerVerified { get; set; }

        [DataMember]
        public string IpnHandlerTransID { get; set; }

        [DataMember]
        public Nullable<System.DateTime> IpnHandlerUpdatedOn { get; set; }

        [DataMember]
        public decimal ExRateUSD { get; set; }

        [DataMember]
        public double InitAmountUSDActual { get; set; }

        [DataMember]
        public decimal InitAmountUSD { get; set; }

        [DataMember]
        public bool IpnVerificationRequired { get; set; }

        [DataMember]
        public decimal InitCartTotalUSD { get; set; }

        [DataMember]
        public decimal TransAmountActual { get; set; }

        [DataMember]
        public decimal TransAmountFee { get; set; }

        [DataMember]
        public double TransExchRateKWD { get; set; }

        [DataMember]
        public decimal TransAmountActualKWD { get; set; }

        [DataMember]
        public Nullable<System.DateTime> TransOn2 { get; set; }

        [DataMember]
        public string ReturnUrl { get; set; }

        [DataMember]
        public string PaymentGatewayUrl { get; set; }

        [DataMember]
        public string PaypalData { get; set; }
    }
}
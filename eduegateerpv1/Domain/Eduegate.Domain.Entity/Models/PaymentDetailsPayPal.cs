using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("PaymentDetailsPayPal", Schema = "payment")]
    public partial class PaymentDetailsPayPal
    {
        [Key]
        public long TrackID { get; set; }
        public long TrackKey { get; set; }
        public long RefCustomerID { get; set; }
        public string BusinessEmail { get; set; }
        public long PaymentID { get; set; }
        public System.DateTime InitOn { get; set; }
        public string InitStatus { get; set; }
        public string InitIP { get; set; }
        public string InitLocation { get; set; }
        public decimal InitAmount { get; set; }
        public string InitCurrency { get; set; }
        public bool IpnVerified { get; set; }
        public string TransID { get; set; }
        public Nullable<decimal> TransAmount { get; set; }
        public string TransCurrency { get; set; }
        public string TransStatus { get; set; }
        public string TransPayerID { get; set; }
        public string TransDateTime { get; set; }
        public string TransPayerStatus { get; set; }
        public string TransPayerEmail { get; set; }
        public string TransPaymentType { get; set; }
        public string TransMessage { get; set; }
        public Nullable<System.DateTime> TransOn { get; set; }
        public string TransReason { get; set; }
        public string TransNoOfCart { get; set; }
        public string TransAddressStatus { get; set; }
        public string TransAddressCountryCode { get; set; }
        public string TransAddressZip { get; set; }
        public string TransAddressName { get; set; }
        public string TransAddressStreet { get; set; }
        public string TransAddressCountry { get; set; }
        public string TransAddressCity { get; set; }
        public string TransAddressState { get; set; }
        public string TransResidenceCountry { get; set; }
        public Nullable<long> OrderID { get; set; }
        public Nullable<long> CartID { get; set; }
        public Nullable<bool> IpnHandlerVerified { get; set; }
        public string IpnHandlerTransID { get; set; }
        public Nullable<System.DateTime> IpnHandlerUpdatedOn { get; set; }
        public decimal ExRateUSD { get; set; }
        public double InitAmountUSDActual { get; set; }
        public decimal InitAmountUSD { get; set; }
        public bool IpnVerificationRequired { get; set; }
        public decimal InitCartTotalUSD { get; set; }
        public decimal TransAmountActual { get; set; }
        public decimal TransAmountFee { get; set; }
        public double TransExchRateKWD { get; set; }
        public decimal TransAmountActualKWD { get; set; }
        public Nullable<System.DateTime> TransOn2 { get; set; }
        public virtual Customer Customer { get; set; }
        public string PaypalDataTransferData { get; set; }
        public string ReturnUrl { get; set; }
    }
}

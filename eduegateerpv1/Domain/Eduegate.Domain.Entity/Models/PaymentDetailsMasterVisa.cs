using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class PaymentDetailsMasterVisa
    {
        [Key]
        public long TrackID { get; set; }
        public long TrackKey { get; set; }
        public long RefCustomerID { get; set; }
        public string SessionID { get; set; }
        public long PaymentID { get; set; }
        public System.DateTime InitOn { get; set; }
        public string InitStatus { get; set; }
        public string InitIP { get; set; }
        public string InitLocation { get; set; }
        public string VpcURL { get; set; }
        public string VpcVersion { get; set; }
        public string VpcCommand { get; set; }
        public string AccessCode { get; set; }
        public string MerchantID { get; set; }
        public string VpcLocale { get; set; }
        public decimal PaymentAmount { get; set; }
        public int VirtualAmount { get; set; }
        public string PaymentCurrency { get; set; }
        public Nullable<System.DateTime> ResponseOn { get; set; }
        public string ResponseCode { get; set; }
        public string CodeDescription { get; set; }
        public string Message { get; set; }
        public string ReceiptNumber { get; set; }
        public string TransID { get; set; }
        public string AcquireResponseCode { get; set; }
        public string BankAuthorizationID { get; set; }
        public string BatchNo { get; set; }
        public string CardType { get; set; }
        public Nullable<int> ResponseAmount { get; set; }
        public string ResponseIP { get; set; }
        public string ResponseLocation { get; set; }
        public Nullable<long> OrderID { get; set; }
        public string ReturnUrl { get; set; }
        public virtual CustomerMaster CustomerMaster { get; set; }
        public int CartID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class PaymentDetail
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
        public decimal PaymentAmount { get; set; }
        public string PaymentAction { get; set; }
        public string PaymentCurrency { get; set; }
        public string PaymentLang { get; set; }
        public string InitRawResponse { get; set; }
        public string InitPaymentPage { get; set; }
        public string InitErrorMsg { get; set; }
        public Nullable<long> TransID { get; set; }
        public Nullable<System.DateTime> TransOn { get; set; }
        public string TransResult { get; set; }
        public string TransPostDate { get; set; }
        public string TransAuth { get; set; }
        public string TransRef { get; set; }
        public string TransErrorMsg { get; set; }
        public string TransIP { get; set; }
        public string TransLocation { get; set; }
        public Nullable<long> OrderID { get; set; }
        public virtual CustomerMaster CustomerMaster { get; set; }
        public string ReturnUrl { get; set; }
    }
}

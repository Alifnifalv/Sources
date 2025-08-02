using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public class PaymentDetailsKnet
    {
        public long TrackID { get; set; }
        public long TrackKey { get; set; }
        public long CustomerID { get; set; }
        
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
        public Nullable<long> CartID { get; set; }

        public Nullable<long> AppKey { get; set; }
    }
}
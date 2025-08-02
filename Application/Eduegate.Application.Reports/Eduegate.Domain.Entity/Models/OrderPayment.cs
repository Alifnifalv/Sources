using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderPayment
    {
        public long PaymentID { get; set; }
        public long RefPaymentOrderID { get; set; }
        public long RefPaymentOrderItemID { get; set; }
        public byte PaymentMethod { get; set; }
        public decimal PaymentAmount { get; set; }
        public string TransID { get; set; }
        public Nullable<short> TransStatus { get; set; }
        public Nullable<System.DateTime> TransDate { get; set; }
        public virtual OrderMaster OrderMaster { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ShoppingCartSummary
    {
        public long ShoppingCartSummaryID { get; set; }
        public int RefCustomerID { get; set; }
        public string CustomerSessionID { get; set; }
        public decimal OrderTotal { get; set; }
        public Nullable<byte> PaymentType { get; set; }
        public Nullable<byte> InitStatus { get; set; }
        public Nullable<System.DateTime> InitDateTime { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<decimal> GrandTotalApp { get; set; }
        public string VoucherNo { get; set; }
        public Nullable<decimal> VoucherAmount { get; set; }
        public Nullable<decimal> CartTotal { get; set; }
        public Nullable<byte> DeliveryMethod { get; set; }
        public Nullable<decimal> DeliveryCharges { get; set; }
        public Nullable<decimal> ActualDeliveryCharges { get; set; }
        public Nullable<decimal> DeliveryDiscount { get; set; }
        public Nullable<decimal> ExpressDeliveryCharges { get; set; }
        public Nullable<decimal> ExpressDeliveryDiscount { get; set; }
        public Nullable<decimal> NextDayDeliveryCharges { get; set; }
        public Nullable<decimal> NextDayDeliveryDiscount { get; set; }
    }
}

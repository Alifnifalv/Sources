using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class IntlPoOrderMaster
    {
        public IntlPoOrderMaster()
        {
            this.IntlPoOrderDetails = new List<IntlPoOrderDetail>();
            this.IntlPoOrderMasterLogs = new List<IntlPoOrderMasterLog>();
            this.IntlPoOrderMasterPayments = new List<IntlPoOrderMasterPayment>();
            this.IntlPoOrderMasterPaymentAdditionals = new List<IntlPoOrderMasterPaymentAdditional>();
        }

        public int IntlPoOrderMasterID { get; set; }
        public short RefIntlPoVendorID { get; set; }
        public System.DateTime OrderDate { get; set; }
        public Nullable<short> CreatedForPM { get; set; }
        public decimal ItemTotalUSD { get; set; }
        public Nullable<decimal> AdditionalCostTotalUSD { get; set; }
        public Nullable<decimal> DiscountCostTotalUSD { get; set; }
        public Nullable<decimal> CancelledTotalUSD { get; set; }
        public decimal OrderTotalUSD { get; set; }
        public string ReferenceDetails { get; set; }
        public string OtherDetails { get; set; }
        public string IntlPoOrderStatus { get; set; }
        public short CreatedBy { get; set; }
        public Nullable<decimal> PaymentTotal { get; set; }
        public virtual ICollection<IntlPoOrderDetail> IntlPoOrderDetails { get; set; }
        public virtual ICollection<IntlPoOrderMasterLog> IntlPoOrderMasterLogs { get; set; }
        public virtual ICollection<IntlPoOrderMasterPayment> IntlPoOrderMasterPayments { get; set; }
        public virtual ICollection<IntlPoOrderMasterPaymentAdditional> IntlPoOrderMasterPaymentAdditionals { get; set; }
    }
}

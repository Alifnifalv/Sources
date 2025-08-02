using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class IntlPoOrderDetail
    {
        public IntlPoOrderDetail()
        {
            this.IntlPoGrnDetails = new List<IntlPoGrnDetail>();
            this.IntlPoOrderDetailsCancelledLogs = new List<IntlPoOrderDetailsCancelledLog>();
        }

        public int IntlPoOrderDetailsID { get; set; }
        public int RefIntlPoOrderMasterID { get; set; }
        public int RefIntlPoRequestID { get; set; }
        public short QtyOrder { get; set; }
        public decimal ItemCostUSD { get; set; }
        public decimal ItemTotalUSD { get; set; }
        public Nullable<short> QtyCancelled { get; set; }
        public Nullable<decimal> CancelledTotalUSD { get; set; }
        public Nullable<short> QtyReceived { get; set; }
        public virtual ICollection<IntlPoGrnDetail> IntlPoGrnDetails { get; set; }
        public virtual IntlPoOrderMaster IntlPoOrderMaster { get; set; }
        public virtual IntlPoRequest IntlPoRequest { get; set; }
        public virtual ICollection<IntlPoOrderDetailsCancelledLog> IntlPoOrderDetailsCancelledLogs { get; set; }
    }
}

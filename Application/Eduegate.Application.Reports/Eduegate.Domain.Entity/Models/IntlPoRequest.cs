using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class IntlPoRequest
    {
        public IntlPoRequest()
        {
            this.IntlPoOrderDetails = new List<IntlPoOrderDetail>();
            this.IntlPoRequestQuantityStatus = new List<IntlPoRequestQuantityStatu>();
            this.IntlPoRequestRemarks = new List<IntlPoRequestRemark>();
            this.IntlPoRequestStatusLogs = new List<IntlPoRequestStatusLog>();
        }

        public int IntlPoRequestID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public string ProductPartNo { get; set; }
        public string ProductName { get; set; }
        public Nullable<decimal> ProductPrice { get; set; }
        public short QtyActualRequested { get; set; }
        public Nullable<short> QtyRequested { get; set; }
        public Nullable<short> QtyOrdered { get; set; }
        public System.DateTime RequestDate { get; set; }
        public string RequestedBy { get; set; }
        public long RequesterID { get; set; }
        public string RequestStatus { get; set; }
        public Nullable<short> RefProductManagerID { get; set; }
        public virtual ICollection<IntlPoOrderDetail> IntlPoOrderDetails { get; set; }
        public virtual ICollection<IntlPoRequestQuantityStatu> IntlPoRequestQuantityStatus { get; set; }
        public virtual ICollection<IntlPoRequestRemark> IntlPoRequestRemarks { get; set; }
        public virtual ICollection<IntlPoRequestStatusLog> IntlPoRequestStatusLogs { get; set; }
    }
}

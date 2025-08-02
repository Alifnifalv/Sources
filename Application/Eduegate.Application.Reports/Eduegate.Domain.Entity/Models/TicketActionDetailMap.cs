using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class TicketActionDetailMap
    {
        public TicketActionDetailMap()
        {
            this.TicketActionDetailDetailMaps = new List<TicketActionDetailDetailMap>();
        }

        public long TicketActionDetailIID { get; set; }
        public long TicketID { get; set; }
        public Nullable<int> RefundTypeID { get; set; }
        public Nullable<decimal> RefundAmount { get; set; }
        public string Reason { get; set; }
        public string Remark { get; set; }
        public string ReturnNumber { get; set; }
        public Nullable<int> GiveItemTo { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> IssueType { get; set; }
        public Nullable<long> AssignedEmployee { get; set; }
        public byte[] Timestamps { get; set; }
        public virtual ICollection<TicketActionDetailDetailMap> TicketActionDetailDetailMaps { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class TicketActionDetailDetailMap
    {
        public long TicketActionDetailDetailMapIID { get; set; }
        public long TicketActionDetailMapID { get; set; }
        public Nullable<int> Notify { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public byte[] Timestamps { get; set; }
        public virtual TicketActionDetailMap TicketActionDetailMap { get; set; }
    }
}

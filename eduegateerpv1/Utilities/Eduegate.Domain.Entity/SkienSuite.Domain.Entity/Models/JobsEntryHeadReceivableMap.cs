using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class JobsEntryHeadReceivableMap
    {
        public long JobsEntryHeadReceivableMapIID { get; set; }
        public Nullable<long> ReceivableID { get; set; }
        public Nullable<long> JobEntryHeadID { get; set; }
        public virtual Receivable Receivable { get; set; }
        public virtual JobEntryHead JobEntryHead { get; set; }
    }
}

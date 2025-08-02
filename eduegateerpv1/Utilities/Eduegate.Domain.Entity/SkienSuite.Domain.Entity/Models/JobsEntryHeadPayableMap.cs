using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class JobsEntryHeadPayableMap
    {
        public long JobsEntryHeadPayableMapIID { get; set; }
        public Nullable<long> PayableID { get; set; }
        public Nullable<long> JobEntryHeadID { get; set; }
        public virtual Payable Payable { get; set; }
        public virtual JobEntryHead JobEntryHead { get; set; }
    }
}

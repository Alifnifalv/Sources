using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class TransactionHeadPayablesMap
    {
        public long TransactionHeadPayablesMapIID { get; set; }
        public Nullable<long> PayableID { get; set; }
        public Nullable<long> HeadID { get; set; }
        public virtual TransactionHead TransactionHead { get; set; }
    }
}

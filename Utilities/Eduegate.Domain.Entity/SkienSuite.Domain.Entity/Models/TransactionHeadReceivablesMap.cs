using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class TransactionHeadReceivablesMap
    {
        public long TransactionHeadReceivablesMapIID { get; set; }
        public long ReceivableID { get; set; }
        public long HeadID { get; set; }
        public virtual TransactionHead TransactionHead { get; set; }
    }
}

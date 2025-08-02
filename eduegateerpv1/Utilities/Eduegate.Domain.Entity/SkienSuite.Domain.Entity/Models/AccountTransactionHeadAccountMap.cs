using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class AccountTransactionHeadAccountMap
    {
        public long AccountTransactionHeadAccountMapIID { get; set; }
        public long AccountTransactionID { get; set; }
        public long AccountTransactionHeadID { get; set; }
        public virtual AccountTransactionHead AccountTransactionHead { get; set; }
        public virtual AccountTransaction AccountTransaction { get; set; }
    }
}

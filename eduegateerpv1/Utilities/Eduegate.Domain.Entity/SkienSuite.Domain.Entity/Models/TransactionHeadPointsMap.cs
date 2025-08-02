using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class TransactionHeadPointsMap
    {
        public long TransactionHeadPointsMapIID { get; set; }
        public long TransactionHeadID { get; set; }
        public long LoyaltyPoints { get; set; }
        public long CategorizationPoints { get; set; }
        public virtual TransactionHead TransactionHead { get; set; }
    }
}

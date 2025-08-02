using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CustomerPointsLog
    {
        [Key]
        public long LogID { get; set; }
        public Nullable<long> CustomerID { get; set; }
        public Nullable<int> PrevLoyaltyPoints { get; set; }
        public Nullable<int> PrevCategorizationPoints { get; set; }
        public Nullable<int> NextLoyaltyPoints { get; set; }
        public Nullable<int> NextCategorizationPoints { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}

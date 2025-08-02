using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CustomerSetting
    {
        public long CustomerSettingIID { get; set; }
        public Nullable<long> CustomerID { get; set; }
        public Nullable<decimal> CurrentLoyaltyPoints { get; set; }
        public Nullable<decimal> TotalLoyaltyPoints { get; set; }
        public Nullable<bool> IsVerified { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public Nullable<bool> IsBlocked { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Customer Customer { get; set; }
    }
}

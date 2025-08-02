using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CustomerAccountMap
    {
        public long CustomerAccountMapIID { get; set; }
        public long CustomerID { get; set; }
        public Nullable<long> AccountID { get; set; }
        public Nullable<byte> EntitlementID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual Account Account { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual EntityTypeEntitlement EntityTypeEntitlement { get; set; }
    }
}

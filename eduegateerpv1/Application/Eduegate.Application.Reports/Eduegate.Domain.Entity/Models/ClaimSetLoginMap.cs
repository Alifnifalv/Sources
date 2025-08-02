using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ClaimSetLoginMap
    {
        public long ClaimSetLoginMapIID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<long> LoginID { get; set; }
        public Nullable<long> ClaimSetID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual ClaimSet ClaimSet { get; set; }
        public virtual Company Company { get; set; }
        public virtual Login Login { get; set; }
    }
}

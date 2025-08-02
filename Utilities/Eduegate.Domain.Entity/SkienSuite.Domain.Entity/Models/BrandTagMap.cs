using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BrandTagMap
    {
        public long BrandTagMapIID { get; set; }
        public Nullable<long> BrandTagID { get; set; }
        public Nullable<long> BrandID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual BrandTag BrandTag { get; set; }
    }
}

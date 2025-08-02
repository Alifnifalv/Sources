using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductCategoryMap
    {
        public long ProductCategoryMapIID { get; set; }
        public Nullable<long> ProductID { get; set; }
        public Nullable<long> CategoryID { get; set; }
        public Nullable<bool> IsPrimary { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Category Category { get; set; }
        public virtual Product Product { get; set; }
    }
}

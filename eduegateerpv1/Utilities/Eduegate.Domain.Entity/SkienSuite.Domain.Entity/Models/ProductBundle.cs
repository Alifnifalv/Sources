using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductBundle
    {
        public long BundleIID { get; set; }
        public Nullable<long> FromProductID { get; set; }
        public Nullable<long> ToProductID { get; set; }
        public Nullable<long> FromProductSKUMapID { get; set; }
        public Nullable<long> ToProductSKUMapID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Product Product { get; set; }
        public virtual Product Product1 { get; set; }
        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}

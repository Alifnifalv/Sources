using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductSKUTagMap
    {
        public long ProductSKUTagMapIID { get; set; }
        public Nullable<long> ProductSKUTagID { get; set; }
        public Nullable<long> ProductSKuMapID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> CreatedBy { get; set; } 
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public virtual ProductSKUMap ProductSKUMap { get; set; }
        public virtual ProductSKUTag ProductSKUTag { get; set; }
    }
}

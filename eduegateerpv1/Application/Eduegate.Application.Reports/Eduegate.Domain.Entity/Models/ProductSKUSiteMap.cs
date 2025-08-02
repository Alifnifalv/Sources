using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductSKUSiteMap
    {
        public long ProductSKUSiteMapIID { get; set; }
        public Nullable<long> ProductSKUMapID { get; set; }
        public Nullable<int> SiteID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }
        public virtual ProductSKUMap ProductSKUMap { get; set; }
        public virtual Site Sites { get; set; }
    }
}

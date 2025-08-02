using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductPriceListProductQuantityMap
    {
        public long ProductPriceListProductQuantityMapIID { get; set; }
        public Nullable<long> ProductPriceListProductMapID { get; set; }
        public Nullable<long> ProductID { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> DiscountPrice { get; set; }
        public Nullable<decimal> DiscountPercentage { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual ProductPriceListProductMap ProductPriceListProductMap { get; set; }
        public virtual Product Product { get; set; }
    }
}

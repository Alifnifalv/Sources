using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductPriceListProductMap
    {
        public ProductPriceListProductMap()
        {
            this.ProductPriceListProductQuantityMaps = new List<ProductPriceListProductQuantityMap>();
        }

        public long ProductPriceListProductMapIID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<long> ProductPriceListID { get; set; }
        public Nullable<long> ProductID { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> DiscountPrice { get; set; }
        public Nullable<decimal> DiscountPercentage { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public virtual Company Company { get; set; }
        public virtual ProductPriceList ProductPriceList { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<ProductPriceListProductQuantityMap> ProductPriceListProductQuantityMaps { get; set; }
    }
}

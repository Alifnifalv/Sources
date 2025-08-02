using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductPriceListSKUMap
    {
        public ProductPriceListSKUMap()
        {
            this.ProductPriceListSKUQuantityMaps = new List<ProductPriceListSKUQuantityMap>();
        }

        public long ProductPriceListItemMapIID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<long> ProductPriceListID { get; set; }
        public Nullable<long> ProductSKUID { get; set; }
        public Nullable<long> UnitGroundID { get; set; }
        public Nullable<long> CustomerGroupID { get; set; }
        public Nullable<decimal> SellingQuantityLimit { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<byte> SortOrder { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public Nullable<decimal> PricePercentage { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> DiscountPercentage { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public virtual ProductPriceList ProductPriceList { get; set; }
        public virtual ProductSKUMap ProductSKUMap { get; set; }
        public virtual UnitGroup UnitGroup { get; set; }
        public virtual Company Company { get; set; }
        public virtual CustomerGroup CustomerGroup { get; set; }
        public virtual ICollection<ProductPriceListSKUQuantityMap> ProductPriceListSKUQuantityMaps { get; set; }
    }
}

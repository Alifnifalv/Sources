using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class UnitGroup
    {
        public UnitGroup()
        {
            this.ProductPriceListSKUMaps = new List<ProductPriceListSKUMap>();
            this.Products = new List<Product>();
            this.Units = new List<Unit>();
        }

        public long UnitGroupID { get; set; }
        public string UnitGroupCode { get; set; }
        public string UnitGroupName { get; set; }
        public Nullable<double> Fraction { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual ICollection<ProductPriceListSKUMap> ProductPriceListSKUMaps { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Unit> Units { get; set; }
    }
}

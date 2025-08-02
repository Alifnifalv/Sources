using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductStatu
    {
        public ProductStatu()
        {
            this.Products = new List<Product>();
            this.ProductSKUMaps = new List<ProductSKUMap>();
        }

        public byte ProductStatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ProductSKUMap> ProductSKUMaps { get; set; }
    }
}

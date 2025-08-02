using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductPriceListLevel
    {
        public ProductPriceListLevel()
        {
            this.ProductPriceLists = new List<ProductPriceList>();
        }

        public short ProductPriceListLevelID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ProductPriceList> ProductPriceLists { get; set; }
    }
}

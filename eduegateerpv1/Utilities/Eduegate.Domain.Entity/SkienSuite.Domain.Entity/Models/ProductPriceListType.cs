using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductPriceListType
    {
        public ProductPriceListType()
        {
            this.ProductPriceLists = new List<ProductPriceList>();
        }

        public short ProductPriceListTypeID { get; set; }
        public string PriceListTypeName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ProductPriceList> ProductPriceLists { get; set; }
    }
}

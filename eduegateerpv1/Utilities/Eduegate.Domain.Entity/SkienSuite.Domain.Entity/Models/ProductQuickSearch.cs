using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductQuickSearch
    {
        public long ProductSKUMapIID { get; set; }
        public string SKU { get; set; }
        public string ProductSKUCode { get; set; }
        public string PartNo { get; set; }
        public string BarCode { get; set; }
        public Nullable<decimal> ProductPrice { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductSkuSearchView
    {
        public long ProductIID { get; set; }
        public long ProductSKUMapIID { get; set; }
        public string SKU { get; set; }
        public string Barcode { get; set; }
        public string PartNo { get; set; }
        public Nullable<decimal> ProductPrice { get; set; }
    }
}

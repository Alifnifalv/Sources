using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class SupplierProductSKUSearchView
    {
        public long ProductIID { get; set; }
        public long ProductSKUMapIID { get; set; }
        public string SKU { get; set; }
        public string Barcode { get; set; }
        public string PartNo { get; set; }
        public Nullable<decimal> ProductPrice { get; set; }
        public Nullable<long> BranchID { get; set; }
        public string BranchName { get; set; }
    }
}

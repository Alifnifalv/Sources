using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class SKUSearchView
    {
        public long ProductSKUMapIID { get; set; }
        public Nullable<long> ProductID { get; set; }
        public string SKUName { get; set; }
        public string ProductSKUCode { get; set; }
        public string StatusName { get; set; }
        public string RowCategory { get; set; }
        public string ProductName { get; set; }
        public string partno { get; set; }
        public string barcode { get; set; }
        public string CategoryName { get; set; }
        public string SkuTag { get; set; }
        public string BrandName { get; set; }
        public Nullable<int> CompanyID { get; set; }
    }
}

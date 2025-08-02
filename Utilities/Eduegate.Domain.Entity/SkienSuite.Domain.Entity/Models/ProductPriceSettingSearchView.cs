using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductPriceSettingSearchView
    {
        public long ProductIID { get; set; }
        public string ProductSKUMapIID { get; set; }
        public string PartNumber { get; set; }
        public string CategoryName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string RowCategory { get; set; }
        public string ProductCode { get; set; }
        public Nullable<long> BrandID { get; set; }
        public string ProductName { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public string BrandName { get; set; }
        public string BrandCode { get; set; }
        public string StatusName { get; set; }
    }
}

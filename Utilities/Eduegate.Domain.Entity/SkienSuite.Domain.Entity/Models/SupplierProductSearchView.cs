using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class SupplierProductSearchView
    {
        public long ProductIID { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public string ProductName { get; set; }
        public Nullable<long> BranchID { get; set; }
        public string BranchName { get; set; }
        public string ProductSKUMapIID { get; set; }
        public string partno { get; set; }
        public Nullable<long> SupplierIID { get; set; }
        public string SupplierName { get; set; }
    }
}

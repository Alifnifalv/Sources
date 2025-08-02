using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class SupplierProductPriceSettingSearchView
    {
        public long ProductIID { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public string ProductName { get; set; }
        public Nullable<long> BranchID { get; set; }
        public string BranchName { get; set; }
    }
}

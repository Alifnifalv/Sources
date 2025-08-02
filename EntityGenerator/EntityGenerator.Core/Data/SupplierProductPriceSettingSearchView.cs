using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class SupplierProductPriceSettingSearchView
    {
        public long ProductIID { get; set; }
        public string CategoryName { get; set; }
        [StringLength(50)]
        public string BrandName { get; set; }
        [StringLength(1000)]
        public string ProductName { get; set; }
        public long? BranchID { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string BranchName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class SupplierProductSearchView
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
        public string ProductSKUMapIID { get; set; }
        public string partno { get; set; }
        public long? SupplierIID { get; set; }
        [StringLength(255)]
        public string SupplierName { get; set; }
    }
}

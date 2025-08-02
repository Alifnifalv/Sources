using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("WestBayInventoryExcelData")]
    public partial class WestBayInventoryExcelData
    {
        public double? BranchID { get; set; }
        [StringLength(255)]
        public string BranchName { get; set; }
        public double? ProductSKUMapID { get; set; }
        [StringLength(255)]
        public string ProductCode { get; set; }
        [StringLength(255)]
        public string ProductName { get; set; }
        public double? Quantity { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("ThumamaInventoryExcelData")]
    public partial class ThumamaInventoryExcelData
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
        [Column("Physical Stocks")]
        public double? Physical_Stocks { get; set; }
    }
}

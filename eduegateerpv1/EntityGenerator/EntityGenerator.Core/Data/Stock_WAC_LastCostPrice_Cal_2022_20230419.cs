using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Stock_WAC_LastCostPrice_Cal_2022_20230419
    {
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string CategoryCode { get; set; }
        public long CategoryIID { get; set; }
        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; }
        public int? AccountID { get; set; }
        [StringLength(500)]
        public string AccountName { get; set; }
        public long ProductIID { get; set; }
        public long ProductSKUMapIID { get; set; }
        [Required]
        [StringLength(150)]
        public string ProductSKUCode { get; set; }
        [StringLength(1000)]
        public string ProductName { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal CostPrice { get; set; }
        [Column(TypeName = "decimal(38, 6)")]
        public decimal LastCostPrice { get; set; }
    }
}

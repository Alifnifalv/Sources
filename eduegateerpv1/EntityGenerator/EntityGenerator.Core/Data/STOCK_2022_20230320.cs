using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class STOCK_2022_20230320
    {
        public int? CompanyID { get; set; }
        public long ProductIID { get; set; }
        public long? ProductSKUMapID { get; set; }
        [Required]
        [StringLength(150)]
        public string ProductSKUCode { get; set; }
        [StringLength(1000)]
        public string ProductName { get; set; }
        [Column(TypeName = "decimal(38, 6)")]
        public decimal OP_CostPrice { get; set; }
        [Column(TypeName = "decimal(38, 6)")]
        public decimal? OP_Stock { get; set; }
        [Column(TypeName = "decimal(38, 6)")]
        public decimal? OP_StockValue { get; set; }
        [Column(TypeName = "decimal(38, 6)")]
        public decimal CL_CostPrice { get; set; }
        [Column(TypeName = "decimal(38, 6)")]
        public decimal? TR_Stock { get; set; }
        [Column(TypeName = "decimal(38, 6)")]
        public decimal? TR_StockValue { get; set; }
        [Column(TypeName = "decimal(38, 6)")]
        public decimal? CL_Stock { get; set; }
        [Column(TypeName = "decimal(38, 6)")]
        public decimal? Cl_StockValue { get; set; }
    }
}

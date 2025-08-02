using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Audit_Consolidated_Stock_2024_20250218
    {
        public long? CompanyID { get; set; }
        [StringLength(100)]
        public string CompanyCode { get; set; }
        [StringLength(100)]
        public string CompanyName { get; set; }
        public long? BranchID { get; set; }
        [StringLength(100)]
        public string BranchCode { get; set; }
        [StringLength(100)]
        public string BranchName { get; set; }
        public long? AccountID { get; set; }
        [StringLength(100)]
        public string AccountName { get; set; }
        public long? CategoryIID { get; set; }
        [StringLength(100)]
        public string CategoryCode { get; set; }
        [StringLength(100)]
        public string CategoryName { get; set; }
        public long? ProductIID { get; set; }
        public long? ProductSKUMapID { get; set; }
        [StringLength(100)]
        public string ProductSKUCode { get; set; }
        [StringLength(100)]
        public string ProductName { get; set; }
        [Column(TypeName = "money")]
        public decimal? OP_CostPrice { get; set; }
        [Column(TypeName = "money")]
        public decimal? OP_Stock { get; set; }
        [Column(TypeName = "money")]
        public decimal? OP_StockValue { get; set; }
        [Column(TypeName = "money")]
        public decimal? TR_CostPrice { get; set; }
        [Column(TypeName = "money")]
        public decimal? TR_Stock { get; set; }
        [Column(TypeName = "money")]
        public decimal? TR_StockValue { get; set; }
        [Column(TypeName = "money")]
        public decimal? CL_CostPrice { get; set; }
        [Column(TypeName = "money")]
        public decimal? CL_Stock { get; set; }
        [Column(TypeName = "money")]
        public decimal? Cl_StockValue { get; set; }
        [Column(TypeName = "money")]
        public decimal? TR_InStock { get; set; }
        [Column(TypeName = "money")]
        public decimal? TR_InStockValue { get; set; }
        [Column(TypeName = "money")]
        public decimal? TR_OutStock { get; set; }
        [Column(TypeName = "money")]
        public decimal? TR_OutStockValue { get; set; }
    }
}

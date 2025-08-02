using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class STOCK_2023_20240210
    {
        public int? CompanyID { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string CompanyCode { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string CompanyName { get; set; }
        public int? BranchID { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string BranchCode { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string BranchName { get; set; }
        public int? AccountID { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string AccountName { get; set; }
        public int? CategoryIID { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string CategoryCode { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string CategoryName { get; set; }
        public long? ProductIID { get; set; }
        public long? ProductSKUMapID { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string ProductSKUCode { get; set; }
        [StringLength(100)]
        [Unicode(false)]
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
    }
}

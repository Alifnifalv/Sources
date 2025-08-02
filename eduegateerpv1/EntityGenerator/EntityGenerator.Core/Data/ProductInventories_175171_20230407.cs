using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("ProductInventories_175171_20230407", Schema = "inventory")]
    public partial class ProductInventories_175171_20230407
    {
        public long ProductSKUMapID { get; set; }
        public long Batch { get; set; }
        public int? CompanyID { get; set; }
        public long BranchID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Quantity { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExpiryDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? CostPrice { get; set; }
        public bool? IsMarketPlaceBranch { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? OriginalQty { get; set; }
        public int? isActive { get; set; }
        public long? HeadID { get; set; }
    }
}

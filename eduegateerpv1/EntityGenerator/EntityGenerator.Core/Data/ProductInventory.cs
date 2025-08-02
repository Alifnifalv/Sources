using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductInventories", Schema = "inventory")]
    public partial class ProductInventory
    {
        [Key]
        public long ProductSKUMapID { get; set; }
        [Key]
        public long Batch { get; set; }
        public int? CompanyID { get; set; }
        [Key]
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

        [ForeignKey("BranchID")]
        [InverseProperty("ProductInventories")]
        public virtual Branch Branch { get; set; }
        [ForeignKey("CompanyID")]
        [InverseProperty("ProductInventories")]
        public virtual Company Company { get; set; }
        [ForeignKey("ProductSKUMapID")]
        [InverseProperty("ProductInventories")]
        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}

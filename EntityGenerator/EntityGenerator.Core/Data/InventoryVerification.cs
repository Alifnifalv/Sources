using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("InventoryVerifications", Schema = "inventory")]
    public partial class InventoryVerification
    {
        [Key]
        public long InventoryVerificationIID { get; set; }
        public long? BranchID { get; set; }
        public long? EmployeeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? VerificationDate { get; set; }
        public long? ProductSKUMapID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? StockQuantity { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? VerifiedQuantity { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Created { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Updated { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("BranchID")]
        [InverseProperty("InventoryVerifications")]
        public virtual Branch Branch { get; set; }
        [ForeignKey("EmployeeID")]
        [InverseProperty("InventoryVerifications")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("ProductSKUMapID")]
        [InverseProperty("InventoryVerifications")]
        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}

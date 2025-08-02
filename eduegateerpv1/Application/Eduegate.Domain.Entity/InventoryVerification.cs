namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.InventoryVerifications")]
    public partial class InventoryVerification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long InventoryVerificationIID { get; set; }

        public long? BranchID { get; set; }

        public long? EmployeeID { get; set; }

        public DateTime? VerificationDate { get; set; }

        public long? ProductSKUMapID { get; set; }

        public decimal? StockQuantity { get; set; }

        public decimal? VerifiedQuantity { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Updated { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual ProductSKUMap ProductSKUMap { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual Employee Employee { get; set; }
    }
}

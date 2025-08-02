namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.TransactionAllocations")]
    public partial class TransactionAllocation
    {
        [Key]
        public long TransactionAllocationIID { get; set; }

        public long? TrasactionDetailID { get; set; }

        public long? BranchID { get; set; }

        public decimal? Quantity { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual TransactionAllocation TransactionAllocations1 { get; set; }

        public virtual TransactionAllocation TransactionAllocation1 { get; set; }

        public virtual TransactionDetail TransactionDetail { get; set; }
    }
}

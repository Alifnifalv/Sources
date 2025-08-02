using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TransactionAllocations", Schema = "inventory")]
    public partial class TransactionAllocation
    {
        [Key]
        public long TransactionAllocationIID { get; set; }
        public long? TrasactionDetailID { get; set; }
        public long? BranchID { get; set; }
        [Column(TypeName = "decimal(18, 5)")]
        public decimal? Quantity { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("TrasactionDetailID")]
        [InverseProperty("TransactionAllocations")]
        public virtual TransactionDetail TrasactionDetail { get; set; }
    }
}

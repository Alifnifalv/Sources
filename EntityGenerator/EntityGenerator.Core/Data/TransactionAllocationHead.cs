using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TransactionAllocationHead", Schema = "account")]
    public partial class TransactionAllocationHead
    {
        public TransactionAllocationHead()
        {
            TransactionAllocationDetails = new HashSet<TransactionAllocationDetail>();
        }

        [Key]
        public long TransactionAllocationIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransactionDate { get; set; }
        [StringLength(100)]
        public string TransactionNumber { get; set; }
        [StringLength(100)]
        public string InvoiceNumber { get; set; }
        [StringLength(1000)]
        public string Remarks { get; set; }
        [StringLength(200)]
        public string Reference { get; set; }
        public long? BranchID { get; set; }
        public long? CompanyID { get; set; }
        public int? TransactionStatusID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Required]
        public byte[] TimeStamps { get; set; }

        [InverseProperty("TransactionAllocation")]
        public virtual ICollection<TransactionAllocationDetail> TransactionAllocationDetails { get; set; }
    }
}

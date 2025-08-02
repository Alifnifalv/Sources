using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("BankReconciliationOpening", Schema = "account")]
    public partial class BankReconciliationOpening
    {
        [Key]
        public long BankReconciliationOpIID { get; set; }
        public long? BankReconciliationHeadID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransactionDate { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Debit { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Credit { get; set; }
        public string Description { get; set; }
        public string PartyName { get; set; }
        [StringLength(255)]
        public string ReferenceNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ChequeDate { get; set; }
        [StringLength(255)]
        public string ChequeNo { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public long? SlNO { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Balance { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TimeStamps { get; set; }
    }
}

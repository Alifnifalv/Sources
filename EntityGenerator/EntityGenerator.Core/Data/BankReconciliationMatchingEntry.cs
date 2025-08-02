using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("BankReconciliationMatchingEntries", Schema = "account")]
    public partial class BankReconciliationMatchingEntry
    {
        [Key]
        public long BankReconciliationMatchingEntryIID { get; set; }
        public long? TranHeadID { get; set; }
        public long? TranTailID { get; set; }
        public long? SlNo { get; set; }
        public long? AccountID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ReconciliationDate { get; set; }
        public string Remarks { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Amount { get; set; }
        public long? BankStatementEntryID { get; set; }
        public bool? IsReconciled { get; set; }
        public string PartyName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ChequeDate { get; set; }
        [StringLength(255)]
        public string ChequeNo { get; set; }
        [StringLength(255)]
        public string Reference { get; set; }
        [StringLength(255)]
        public string ReferenceGroupNO { get; set; }
        [StringLength(255)]
        public string ReferenceGroupName { get; set; }
        public long? BankReconciliationHeadID { get; set; }
        public long? BankReconciliationDetailID { get; set; }

        [ForeignKey("BankReconciliationDetailID")]
        [InverseProperty("BankReconciliationMatchingEntries")]
        public virtual BankReconciliationDetail BankReconciliationDetail { get; set; }
        [ForeignKey("BankStatementEntryID")]
        [InverseProperty("BankReconciliationMatchingEntries")]
        public virtual BankStatementEntry BankStatementEntry { get; set; }
        [ForeignKey("TranHeadID")]
        [InverseProperty("BankReconciliationMatchingEntries")]
        public virtual Tranhead TranHead { get; set; }
    }
}

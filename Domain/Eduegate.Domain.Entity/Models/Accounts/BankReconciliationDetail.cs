using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace Eduegate.Domain.Entity.Models
{
    [Table("BankReconciliationDetail", Schema = "account")]
    public partial class BankReconciliationDetail
    {
        public BankReconciliationDetail()
        {
            BankReconciliationMatchingEntries = new HashSet<BankReconciliationMatchingEntry>();
        }
        [Key]
        public long ReconciliationDetailIID { get; set; }
        public long BankReconciliationHeadID { get; set; }
        public long? TranHeadID { get; set; }
        public long? TranTailID { get; set; }
        public long? SlNo { get; set; }
        public long? AccountID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ReconciliationDate { get; set; }
        [Required]
        [StringLength(255)]
        public string Remarks { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Amount { get; set; }
        [StringLength(255)]
        public string ReferenceGroupNO { get; set; }
        [StringLength(255)]
        public string ReferenceGroupName { get; set; }
        public short? BankReconciliationMatchedStatusID { get; set; }
        public long? BankStatementEntryID { get; set; }
        public bool? IsOpening { get; set; }
        public bool? IsReconciled { get; set; }
        public string PartyName { get; set; }
        public string Reference { get; set; }
        public string ChequeDate { get; set; }
        public string ChequeNo { get; set; }

        [ForeignKey("BankReconciliationHeadID")]
        [InverseProperty("BankReconciliationDetails")]
        public virtual BankReconciliationHead BankReconciliationHead { get; set; }
        [ForeignKey("BankReconciliationMatchedStatusID")]
        [InverseProperty("BankReconciliationDetails")]
        public virtual BankReconciliationMatchedStatus BankReconciliationMatchedStatus { get; set; }
        [ForeignKey("BankStatementEntryID")]
        [InverseProperty("BankReconciliationDetails")]
        public virtual BankStatementEntry BankStatementEntry { get; set; }
        [ForeignKey("TranHeadID")]
        [InverseProperty("BankReconciliationDetails")]
        public virtual Tranhead TranHead { get; set; }
        public virtual ICollection<BankReconciliationMatchingEntry> BankReconciliationMatchingEntries { get; set; }
    }
}

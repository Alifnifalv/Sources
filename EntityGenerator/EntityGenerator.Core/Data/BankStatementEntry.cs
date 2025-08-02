using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("BankStatementEntries", Schema = "account")]
    public partial class BankStatementEntry
    {
        public BankStatementEntry()
        {
            BankReconciliationDetails = new HashSet<BankReconciliationDetail>();
            BankReconciliationMatchingEntries = new HashSet<BankReconciliationMatchingEntry>();
        }

        [Key]
        public long BankStatementEntryIID { get; set; }
        public long BankStatementID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PostDate { get; set; }
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

        [ForeignKey("BankStatementID")]
        [InverseProperty("BankStatementEntries")]
        public virtual BankStatement BankStatement { get; set; }
        [InverseProperty("BankStatementEntry")]
        public virtual ICollection<BankReconciliationDetail> BankReconciliationDetails { get; set; }
        [InverseProperty("BankStatementEntry")]
        public virtual ICollection<BankReconciliationMatchingEntry> BankReconciliationMatchingEntries { get; set; }
    }
}

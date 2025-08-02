using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Models
{
    [Table("BankStatements", Schema = "account")]
    public partial class BankStatement
    {
        public BankStatement()
        {
            BankReconciliationHeads = new HashSet<BankReconciliationHead>();
            BankStatementEntries = new HashSet<BankStatementEntry>();
        }

        [Key]
        public long BankStatementIID { get; set; }
        [Required]
        [StringLength(50)]
        public string ContentFileID { get; set; }
        [Required]
        [StringLength(250)]
        public string ContentFileName { get; set; }
        public string ExtractedTextData { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        //[Required]
        //public byte[] TimeStamps { get; set; }

        [InverseProperty("BankStatement")]
        public virtual ICollection<BankReconciliationHead> BankReconciliationHeads { get; set; }
        public virtual ICollection<BankStatementEntry> BankStatementEntries { get; set; }
    }
}

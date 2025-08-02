using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Models
{
    [Table("BankStatementEntries", Schema = "account")]
    public partial class BankStatementEntry
    {
        public BankStatementEntry()
        {
            BankReconciliationDetails = new HashSet<BankReconciliationDetail>();
         
        }

        [Key]
        public long BankStatementEntryIID { get; set; }
        public long BankStatementID { get; set; }
        //[Column(TypeName = "datetime")]
        public DateTime? PostDate { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Debit { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Credit { get; set; }
        [Required]
        [StringLength(255)]
        public string Description { get; set; }
        [Required]
        [StringLength(255)]
        public string PartyName { get; set; }
        //[Required]
        //[StringLength(255)]
        public string ReferenceNo { get; set; }
        
        public DateTime? ChequeDate { get; set; }
        [Required]
        [StringLength(255)]
        public string ChequeNo { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
       
        public DateTime? UpdatedDate { get; set; }
        //[Required]
        //public byte[] TimeStamps { get; set; }
        public long? SlNO { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Balance { get; set; }
        public virtual BankStatement BankStatement { get; set; }
        
        public virtual ICollection<BankReconciliationDetail> BankReconciliationDetails { get; set; }
       
    }
}

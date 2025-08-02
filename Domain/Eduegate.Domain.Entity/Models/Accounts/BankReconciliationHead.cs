using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Models
{
    [Table("BankReconciliationHead", Schema = "account")]
    public partial class BankReconciliationHead
    {
        public BankReconciliationHead()
        {
            BankReconciliationDetails = new HashSet<BankReconciliationDetail>();
        }

        [Key]
        public long BankReconciliationHeadIID { get; set; }
        public long? BankStatementID { get; set; }
        public long? BankAccountID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FromDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ToDate { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? OpeningBalanceAccount { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? OpeningBalanceBankStatement { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? ClosingBalanceAccount { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? ClosingBalanceBankStatement { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        //[Required]
        //public byte[] TimeStamps { get; set; }
        public short? BankReconciliationStatusID { get; set; }

        [ForeignKey("BankReconciliationStatusID")]
        [InverseProperty("BankReconciliationHeads")]
        public virtual BankReconciliationStatus BankReconciliationStatus { get; set; }
        [ForeignKey("BankStatementID")]
        [InverseProperty("BankReconciliationHeads")]
        public virtual BankStatement BankStatement { get; set; }
        [InverseProperty("BankReconciliationHead")]
        public virtual ICollection<BankReconciliationDetail> BankReconciliationDetails { get; set; }


    }
}

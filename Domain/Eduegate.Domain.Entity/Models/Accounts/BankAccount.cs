using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Models
{
    [Table("BankAccounts", Schema = "account")]
    public partial class BankAccount
    {
        [Key]
        public int BankAccountID { get; set; }
        [Required]
        [StringLength(500)]
        [Unicode(false)]
        public string AccountNumber { get; set; }
        [Required]
        [StringLength(500)]
        [Unicode(false)]
        public string IBAN { get; set; }
        [Required]
        [StringLength(500)]
        [Unicode(false)]
        public string AccountHolderName { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string AccountHolderAddress { get; set; }
        [Required]
        [StringLength(500)]
        [Unicode(false)]
        public string BankName { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string BranchName { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string IFSCCode { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string SWIFTCode { get; set; }
        public int? AccountTypeID { get; set; }
        public int? CurrencyID { get; set; }
        [Column(TypeName = "money")]
        public decimal? Balance { get; set; }
        [Column(TypeName = "money")]
        public decimal? OverdraftLimit { get; set; }
        [Column(TypeName = "money")]
        public decimal? InterestRate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateOpened { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateClosed { get; set; }
        public bool? IsJointAccount { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string JointAccountHolderName { get; set; }
        public int? StatusID { get; set; }
        public long? AccountID { get; set; }
        public long? BankID { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Required]
        public byte[] TimeStamps { get; set; }

        [ForeignKey("AccountID")]
        [InverseProperty("BankAccounts")]
        public virtual Account Account { get; set; }
        [ForeignKey("AccountTypeID")]
        [InverseProperty("BankAccounts")]
        public virtual BankAccountType AccountType { get; set; }
        [ForeignKey("BankID")]
        [InverseProperty("BankAccounts")]
        public virtual Bank Bank { get; set; }
        [ForeignKey("CurrencyID")]
        [InverseProperty("BankAccounts")]
        public virtual Currency Currency { get; set; }
        [ForeignKey("StatusID")]
        [InverseProperty("BankAccounts")]
        public virtual BankAccountStatus Status { get; set; }
    }
}

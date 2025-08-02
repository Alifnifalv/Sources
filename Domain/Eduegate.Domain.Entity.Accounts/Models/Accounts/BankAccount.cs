using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Eduegate.Domain.Entity.Accounts.Models.Mutual;
using Eduegate.Domain.Entity.Accounts.Models.AI;

namespace Eduegate.Domain.Entity.Accounts.Models.Accounts
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

        public decimal? Balance { get; set; }

        public decimal? OverdraftLimit { get; set; }

        public decimal? InterestRate { get; set; }

        public DateTime? DateOpened { get; set; }

        public DateTime? DateClosed { get; set; }

        public bool? IsJointAccount { get; set; }

        [StringLength(500)]
        [Unicode(false)]
        public string JointAccountHolderName { get; set; }

        public int? StatusID { get; set; }        

        public long? AccountID { get; set; }

        public long? BankID { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }
        public int? MatchingRuleSetID { get; set; }
        public int? DataExtractionRuleSetID { get; set; }
        public DateTime? UpdatedDate { get; set; }

        //public byte[] TimeStamps { get; set; }

        public virtual Account Account { get; set; }

        public virtual BankAccountType AccountType { get; set; }

       
        public virtual Bank Bank { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual BankAccountStatus Status { get; set; }
       
        public virtual RuleSet DataExtractionRuleSet { get; set; }
       
        public virtual RuleSet MatchingRuleSet { get; set; }
    }
}
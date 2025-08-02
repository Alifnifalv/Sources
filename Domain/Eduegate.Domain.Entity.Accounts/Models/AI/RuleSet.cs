using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Accounts;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Accounts.Models.AI
{
    [Table("RuleSets", Schema = "ai")]
    public partial class RuleSet
    {
        public RuleSet()
        {
            Rules = new HashSet<Rule>();
            BankAccountDataExtractionRuleSets = new HashSet<BankAccount>();
            BankAccountMatchingRuleSets = new HashSet<BankAccount>();
        }

        [Key]
        public int RuleSetID { get; set; }
        [Required]
        [StringLength(200)]
        [Unicode(false)]
        public string RuleSetName { get; set; }

        [InverseProperty("RuleSet")]
        public virtual ICollection<Rule> Rules { get; set; }

        [InverseProperty("DataExtractionRuleSet")]
        public virtual ICollection<BankAccount> BankAccountDataExtractionRuleSets { get; set; }
        [InverseProperty("MatchingRuleSet")]
        public virtual ICollection<BankAccount> BankAccountMatchingRuleSets { get; set; }
    }
}

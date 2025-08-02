using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("RuleSets", Schema = "ai")]
    public partial class RuleSet
    {
        public RuleSet()
        {
            BankAccountDataExtractionRuleSets = new HashSet<BankAccount>();
            BankAccountMatchingRuleSets = new HashSet<BankAccount>();
            Rules = new HashSet<Rule>();
        }

        [Key]
        public int RuleSetID { get; set; }
        [Required]
        [StringLength(200)]
        [Unicode(false)]
        public string RuleSetName { get; set; }

        [InverseProperty("DataExtractionRuleSet")]
        public virtual ICollection<BankAccount> BankAccountDataExtractionRuleSets { get; set; }
        [InverseProperty("MatchingRuleSet")]
        public virtual ICollection<BankAccount> BankAccountMatchingRuleSets { get; set; }
        [InverseProperty("RuleSet")]
        public virtual ICollection<Rule> Rules { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Accounts.Models.AI
{   
    [Table("RuleTypes", Schema = "ai")]
    public partial class RuleType
    {
        public RuleType()
        {
            Rules = new HashSet<Rule>();
        }

        [Key]
        public int RuleTypeID { get; set; }
        [Required]
        [StringLength(200)]
        [Unicode(false)]
        public string RuleTypeName { get; set; }

        [InverseProperty("RuleType")]
        public virtual ICollection<Rule> Rules { get; set; }
    }
}

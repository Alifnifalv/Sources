using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Accounts.Models.AI
{
    [Table("Rules", Schema = "ai")]
    public partial class Rule
    {
        [Key]
        public long RuleID { get; set; }
        public int RuleSetID { get; set; }
        public int RuleTypeID { get; set; }
        public int? RuleOrder { get; set; }
        [StringLength(200)]
        public string ColumnDataType { get; set; }
        [StringLength(200)]
        public string ColumnName { get; set; }
        public int PatternTypeID { get; set; }
        public string Pattern { get; set; }
        public string Description { get; set; }
        public string Expression { get; set; }

        [ForeignKey("PatternTypeID")]
        [InverseProperty("Rules")]
        public virtual PatternType PatternType { get; set; }
        [ForeignKey("RuleSetID")]
        [InverseProperty("Rules")]
        public virtual RuleSet RuleSet { get; set; }
        [ForeignKey("RuleTypeID")]
        [InverseProperty("Rules")]
        public virtual RuleType RuleType { get; set; }
    }
}

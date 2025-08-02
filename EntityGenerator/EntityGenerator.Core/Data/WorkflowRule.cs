using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("WorkflowRules", Schema = "workflow")]
    public partial class WorkflowRule
    {
        public WorkflowRule()
        {
            WorkflowLogMapRuleMaps = new HashSet<WorkflowLogMapRuleMap>();
            WorkflowRuleConditions = new HashSet<WorkflowRuleCondition>();
            WorkflowTransactionHeadRuleMaps = new HashSet<WorkflowTransactionHeadRuleMap>();
        }

        [Key]
        public long WorkflowRuleIID { get; set; }
        public long? WorkflowID { get; set; }
        public int? ConditionID { get; set; }
        [StringLength(50)]
        public string Value1 { get; set; }
        [StringLength(50)]
        public string Value2 { get; set; }
        [StringLength(50)]
        public string Value3 { get; set; }

        [ForeignKey("ConditionID")]
        [InverseProperty("WorkflowRules")]
        public virtual WorkflowCondition Condition { get; set; }
        [ForeignKey("WorkflowID")]
        [InverseProperty("WorkflowRules")]
        public virtual Workflow Workflow { get; set; }
        [InverseProperty("WorkflowRule")]
        public virtual ICollection<WorkflowLogMapRuleMap> WorkflowLogMapRuleMaps { get; set; }
        [InverseProperty("WorkflowRule")]
        public virtual ICollection<WorkflowRuleCondition> WorkflowRuleConditions { get; set; }
        [InverseProperty("WorkflowRule")]
        public virtual ICollection<WorkflowTransactionHeadRuleMap> WorkflowTransactionHeadRuleMaps { get; set; }
    }
}

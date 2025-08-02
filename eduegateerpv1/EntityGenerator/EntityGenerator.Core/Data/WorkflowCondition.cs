using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("WorkflowConditions", Schema = "workflow")]
    public partial class WorkflowCondition
    {
        public WorkflowCondition()
        {
            WorkflowLogMapRuleApproverMaps = new HashSet<WorkflowLogMapRuleApproverMap>();
            WorkflowLogMapRuleMaps = new HashSet<WorkflowLogMapRuleMap>();
            WorkflowRuleConditions = new HashSet<WorkflowRuleCondition>();
            WorkflowRules = new HashSet<WorkflowRule>();
            WorkflowTransactionHeadRuleMaps = new HashSet<WorkflowTransactionHeadRuleMap>();
            WorkflowTransactionRuleApproverMaps = new HashSet<WorkflowTransactionRuleApproverMap>();
        }

        [Key]
        public int WorkflowConditionID { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string ConditionType { get; set; }
        [StringLength(50)]
        public string ConditionName { get; set; }

        [InverseProperty("WorkflowCondition")]
        public virtual ICollection<WorkflowLogMapRuleApproverMap> WorkflowLogMapRuleApproverMaps { get; set; }
        [InverseProperty("WorkflowCondition")]
        public virtual ICollection<WorkflowLogMapRuleMap> WorkflowLogMapRuleMaps { get; set; }
        [InverseProperty("Condition")]
        public virtual ICollection<WorkflowRuleCondition> WorkflowRuleConditions { get; set; }
        [InverseProperty("Condition")]
        public virtual ICollection<WorkflowRule> WorkflowRules { get; set; }
        [InverseProperty("WorkflowCondition")]
        public virtual ICollection<WorkflowTransactionHeadRuleMap> WorkflowTransactionHeadRuleMaps { get; set; }
        [InverseProperty("WorkflowCondition")]
        public virtual ICollection<WorkflowTransactionRuleApproverMap> WorkflowTransactionRuleApproverMaps { get; set; }
    }
}

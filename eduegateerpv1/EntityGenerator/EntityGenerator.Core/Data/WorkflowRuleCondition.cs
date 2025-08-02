using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("WorkflowRuleConditions", Schema = "workflow")]
    public partial class WorkflowRuleCondition
    {
        public WorkflowRuleCondition()
        {
            WorkflowLogMapRuleApproverMaps = new HashSet<WorkflowLogMapRuleApproverMap>();
            WorkflowRuleApprovers = new HashSet<WorkflowRuleApprover>();
            WorkflowTransactionRuleApproverMaps = new HashSet<WorkflowTransactionRuleApproverMap>();
        }

        [Key]
        public long WorkflowRuleConditionIID { get; set; }
        public long? WorkflowRuleID { get; set; }
        public int? ConditionID { get; set; }

        [ForeignKey("ConditionID")]
        [InverseProperty("WorkflowRuleConditions")]
        public virtual WorkflowCondition Condition { get; set; }
        [ForeignKey("WorkflowRuleID")]
        [InverseProperty("WorkflowRuleConditions")]
        public virtual WorkflowRule WorkflowRule { get; set; }
        [InverseProperty("WorkflowRuleCondition")]
        public virtual ICollection<WorkflowLogMapRuleApproverMap> WorkflowLogMapRuleApproverMaps { get; set; }
        [InverseProperty("WorkflowRuleCondition")]
        public virtual ICollection<WorkflowRuleApprover> WorkflowRuleApprovers { get; set; }
        [InverseProperty("WorkflowRuleCondition")]
        public virtual ICollection<WorkflowTransactionRuleApproverMap> WorkflowTransactionRuleApproverMaps { get; set; }
    }
}

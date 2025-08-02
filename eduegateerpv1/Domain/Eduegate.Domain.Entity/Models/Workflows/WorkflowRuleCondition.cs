using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models.Workflows
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

        public virtual WorkflowCondition Condition { get; set; }
        public virtual WorkflowRule WorkflowRule { get; set; }
        public virtual ICollection<WorkflowLogMapRuleApproverMap> WorkflowLogMapRuleApproverMaps { get; set; }
        public virtual ICollection<WorkflowRuleApprover> WorkflowRuleApprovers { get; set; }
        public virtual ICollection<WorkflowTransactionRuleApproverMap> WorkflowTransactionRuleApproverMaps { get; set; }
    }
}
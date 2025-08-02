using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Worflow.WorkFlows
{
    [Table("workflow.WorkflowRuleApprovers")]
    public partial class WorkflowRuleApprover
    {
        [Key]
        public long WorkflowRuleApproverIID { get; set; }

        public long WorkflowRuleConditionID { get; set; }

        public long? EmployeeID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //public virtual Employee Employee { get; set; }

        public virtual WorkflowRuleCondition WorkflowRuleCondition { get; set; }
    }
}

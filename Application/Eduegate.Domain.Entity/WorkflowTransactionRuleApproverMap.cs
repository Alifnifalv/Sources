namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("workflow.WorkflowTransactionRuleApproverMaps")]
    public partial class WorkflowTransactionRuleApproverMap
    {
        [Key]
        public long WorkflowTransactionRuleApproverMapIID { get; set; }

        public long? WorkflowTransactionHeadRuleMapID { get; set; }

        public long? EmployeeID { get; set; }

        public long? WorkflowRuleConditionID { get; set; }

        public int? WorkflowConditionID { get; set; }

        public byte? WorkflowStatusID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Updated { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual WorkflowCondition WorkflowCondition { get; set; }

        public virtual WorkflowRuleCondition WorkflowRuleCondition { get; set; }

        public virtual WorkflowStatus WorkflowStatus { get; set; }

        public virtual WorkflowTransactionHeadRuleMap WorkflowTransactionHeadRuleMap { get; set; }
    }
}

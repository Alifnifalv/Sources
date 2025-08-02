namespace Eduegate.Domain.Entity.Models.Workflows
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("WorkflowTransactionRuleApproverMaps", Schema = "workflow")]
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
        [Column(TypeName = "datetime")]
        public DateTime? Created { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Updated { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual WorkflowCondition WorkflowCondition { get; set; }
        public virtual WorkflowRuleCondition WorkflowRuleCondition { get; set; }
        public virtual WorkflowStatus WorkflowStatus { get; set; }
        public virtual WorkflowTransactionHeadRuleMap WorkflowTransactionHeadRuleMap { get; set; }
    }
}

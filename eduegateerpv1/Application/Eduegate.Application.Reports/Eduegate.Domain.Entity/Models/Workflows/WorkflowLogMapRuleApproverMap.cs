namespace Eduegate.Domain.Entity.Models.Workflows
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("workflow.WorkflowLogMapRuleApproverMaps")]
    public partial class WorkflowLogMapRuleApproverMap
    {
        [Key]
        public long WorkflowLogMapRuleApproverMapIID { get; set; }

        public long? WorkflowLogMapRuleMapID { get; set; }

        public long? EmployeeID { get; set; }

        public long? WorkflowRuleConditionID { get; set; }

        public int? WorkflowConditionID { get; set; }

        public byte? WorkflowStatusID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual WorkflowCondition WorkflowCondition { get; set; }

        public virtual WorkflowLogMapRuleMap WorkflowLogMapRuleMap { get; set; }

        public virtual WorkflowRuleCondition WorkflowRuleCondition { get; set; }

        public virtual WorkflowStatus WorkflowStatus { get; set; }
    }
}

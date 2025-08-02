namespace Eduegate.Domain.Entity.Models.Workflows
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("WorkflowLogMapRuleMaps", Schema = "workflow")]
    public partial class WorkflowLogMapRuleMap
    {
        public WorkflowLogMapRuleMap()
        {
            WorkflowLogMapRuleApproverMaps = new HashSet<WorkflowLogMapRuleApproverMap>();
        }

        [Key]
        public long WorkflowLogMapRuleMapIID { get; set; }
        public long? WorkflowLogMapID { get; set; }
        public byte? WorkflowStatusID { get; set; }
        public long? WorkflowRuleID { get; set; }
        public int? WorkflowConditionID { get; set; }
        [StringLength(50)]
        public string Value1 { get; set; }
        [StringLength(50)]
        public string Value2 { get; set; }
        [StringLength(50)]
        public string Value3 { get; set; }
        [StringLength(2000)]
        public string Remarks { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public string Description { get; set; }
        public bool? IsFlowCompleted { get; set; }

        public virtual WorkflowCondition WorkflowCondition { get; set; }
        public virtual WorkflowLogMap WorkflowLogMap { get; set; }
        public virtual WorkflowRule WorkflowRule { get; set; }
        public virtual WorkflowStatus WorkflowStatus { get; set; }
        public virtual ICollection<WorkflowLogMapRuleApproverMap> WorkflowLogMapRuleApproverMaps { get; set; }
    }
}

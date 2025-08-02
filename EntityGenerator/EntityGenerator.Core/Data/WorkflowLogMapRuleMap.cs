using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("WorkflowLogMapRuleMaps", Schema = "workflow")]
    [Index("WorkflowLogMapID", Name = "IDX_WorkflowLogMapRuleMaps_WorkflowLogMapID_")]
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
        public byte[] TimeStamps { get; set; }
        public string Description { get; set; }
        public bool? IsFlowCompleted { get; set; }

        [ForeignKey("WorkflowConditionID")]
        [InverseProperty("WorkflowLogMapRuleMaps")]
        public virtual WorkflowCondition WorkflowCondition { get; set; }
        [ForeignKey("WorkflowLogMapID")]
        [InverseProperty("WorkflowLogMapRuleMaps")]
        public virtual WorkflowLogMap WorkflowLogMap { get; set; }
        [ForeignKey("WorkflowRuleID")]
        [InverseProperty("WorkflowLogMapRuleMaps")]
        public virtual WorkflowRule WorkflowRule { get; set; }
        [ForeignKey("WorkflowStatusID")]
        [InverseProperty("WorkflowLogMapRuleMaps")]
        public virtual WorkflowStatus WorkflowStatus { get; set; }
        [InverseProperty("WorkflowLogMapRuleMap")]
        public virtual ICollection<WorkflowLogMapRuleApproverMap> WorkflowLogMapRuleApproverMaps { get; set; }
    }
}

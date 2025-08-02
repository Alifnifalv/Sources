using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("WorkflowLogMapRuleApproverMaps", Schema = "workflow")]
    [Index("WorkflowLogMapRuleMapID", Name = "IDX_WorkflowLogMapRuleApproverMaps_WorkflowLogMapRuleMapID_")]
    [Index("WorkflowLogMapRuleMapID", Name = "IDX_WorkflowLogMapRuleApproverMaps_WorkflowLogMapRuleMapID_EmployeeID__WorkflowRuleConditionID__Wor")]
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("EmployeeID")]
        [InverseProperty("WorkflowLogMapRuleApproverMaps")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("WorkflowConditionID")]
        [InverseProperty("WorkflowLogMapRuleApproverMaps")]
        public virtual WorkflowCondition WorkflowCondition { get; set; }
        [ForeignKey("WorkflowLogMapRuleMapID")]
        [InverseProperty("WorkflowLogMapRuleApproverMaps")]
        public virtual WorkflowLogMapRuleMap WorkflowLogMapRuleMap { get; set; }
        [ForeignKey("WorkflowRuleConditionID")]
        [InverseProperty("WorkflowLogMapRuleApproverMaps")]
        public virtual WorkflowRuleCondition WorkflowRuleCondition { get; set; }
        [ForeignKey("WorkflowStatusID")]
        [InverseProperty("WorkflowLogMapRuleApproverMaps")]
        public virtual WorkflowStatus WorkflowStatus { get; set; }
    }
}

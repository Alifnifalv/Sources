using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
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

        [ForeignKey("EmployeeID")]
        [InverseProperty("WorkflowTransactionRuleApproverMaps")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("WorkflowConditionID")]
        [InverseProperty("WorkflowTransactionRuleApproverMaps")]
        public virtual WorkflowCondition WorkflowCondition { get; set; }
        [ForeignKey("WorkflowRuleConditionID")]
        [InverseProperty("WorkflowTransactionRuleApproverMaps")]
        public virtual WorkflowRuleCondition WorkflowRuleCondition { get; set; }
        [ForeignKey("WorkflowStatusID")]
        [InverseProperty("WorkflowTransactionRuleApproverMaps")]
        public virtual WorkflowStatus WorkflowStatus { get; set; }
        [ForeignKey("WorkflowTransactionHeadRuleMapID")]
        [InverseProperty("WorkflowTransactionRuleApproverMaps")]
        public virtual WorkflowTransactionHeadRuleMap WorkflowTransactionHeadRuleMap { get; set; }
    }
}

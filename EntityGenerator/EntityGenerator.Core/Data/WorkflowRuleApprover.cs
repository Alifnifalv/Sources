using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("WorkflowRuleApprovers", Schema = "workflow")]
    public partial class WorkflowRuleApprover
    {
        [Key]
        public long WorkflowRuleApproverIID { get; set; }
        public long WorkflowRuleConditionID { get; set; }
        public long? EmployeeID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("EmployeeID")]
        [InverseProperty("WorkflowRuleApprovers")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("WorkflowRuleConditionID")]
        [InverseProperty("WorkflowRuleApprovers")]
        public virtual WorkflowRuleCondition WorkflowRuleCondition { get; set; }
    }
}

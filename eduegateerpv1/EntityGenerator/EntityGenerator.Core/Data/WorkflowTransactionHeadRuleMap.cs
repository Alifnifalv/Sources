using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("WorkflowTransactionHeadRuleMaps", Schema = "workflow")]
    public partial class WorkflowTransactionHeadRuleMap
    {
        public WorkflowTransactionHeadRuleMap()
        {
            WorkflowTransactionRuleApproverMaps = new HashSet<WorkflowTransactionRuleApproverMap>();
        }

        [Key]
        public long WorkflowTransactionHeadRuleMapIID { get; set; }
        public long? WorkflowTransactionHeadMapID { get; set; }
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

        [ForeignKey("WorkflowConditionID")]
        [InverseProperty("WorkflowTransactionHeadRuleMaps")]
        public virtual WorkflowCondition WorkflowCondition { get; set; }
        [ForeignKey("WorkflowRuleID")]
        [InverseProperty("WorkflowTransactionHeadRuleMaps")]
        public virtual WorkflowRule WorkflowRule { get; set; }
        [ForeignKey("WorkflowStatusID")]
        [InverseProperty("WorkflowTransactionHeadRuleMaps")]
        public virtual WorkflowStatus WorkflowStatus { get; set; }
        [ForeignKey("WorkflowTransactionHeadMapID")]
        [InverseProperty("WorkflowTransactionHeadRuleMaps")]
        public virtual WorkflowTransactionHeadMap WorkflowTransactionHeadMap { get; set; }
        [InverseProperty("WorkflowTransactionHeadRuleMap")]
        public virtual ICollection<WorkflowTransactionRuleApproverMap> WorkflowTransactionRuleApproverMaps { get; set; }
    }
}

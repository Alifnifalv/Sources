using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Worflow.WorkFlows
{
    [Table("workflow.WorkflowTransactionHeadRuleMaps")]
    public partial class WorkflowTransactionHeadRuleMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual WorkflowCondition WorkflowCondition { get; set; }

        public virtual WorkflowRule WorkflowRule { get; set; }

        public virtual WorkflowStatus WorkflowStatus { get; set; }

        public virtual WorkflowTransactionHeadMap WorkflowTransactionHeadMap { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkflowTransactionRuleApproverMap> WorkflowTransactionRuleApproverMaps { get; set; }
    }
}

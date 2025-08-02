using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Worflow.WorkFlows
{
    [Table("workflow.WorkflowLogMapRuleMaps")]
    public partial class WorkflowLogMapRuleMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public virtual WorkflowCondition WorkflowCondition { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkflowLogMapRuleApproverMap> WorkflowLogMapRuleApproverMaps { get; set; }

        public virtual WorkflowLogMap WorkflowLogMap { get; set; }

        public virtual WorkflowRule WorkflowRule { get; set; }

        public virtual WorkflowStatus WorkflowStatus { get; set; }
    }
}
namespace Eduegate.Domain.Entity.Models.Workflows
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("workflow.WorkflowStatuses")]
    public partial class WorkflowStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WorkflowStatus()
        {
            WorkflowTransactionHeadMaps = new HashSet<WorkflowTransactionHeadMap>();
            WorkflowTransactionHeadRuleMaps = new HashSet<WorkflowTransactionHeadRuleMap>();
            WorkflowTransactionRuleApproverMaps = new HashSet<WorkflowTransactionRuleApproverMap>();
        }

        public byte WorkflowStatusID { get; set; }

        public int? WorkflowTypeID { get; set; }

        [StringLength(100)]
        public string StatusName { get; set; }

        public virtual WorkflowType WorkflowType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkflowTransactionHeadMap> WorkflowTransactionHeadMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkflowTransactionHeadRuleMap> WorkflowTransactionHeadRuleMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkflowTransactionRuleApproverMap> WorkflowTransactionRuleApproverMaps { get; set; }
    }
}

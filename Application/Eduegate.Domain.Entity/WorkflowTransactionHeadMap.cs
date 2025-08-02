namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("workflow.WorkflowTransactionHeadMaps")]
    public partial class WorkflowTransactionHeadMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WorkflowTransactionHeadMap()
        {
            WorkflowTransactionHeadRuleMaps = new HashSet<WorkflowTransactionHeadRuleMap>();
        }

        [Key]
        public long WorkflowTransactionHeadMapIID { get; set; }

        public long? WorkflowID { get; set; }

        public long? TransactionHeadID { get; set; }

        public byte? WorkflowStatusID { get; set; }

        public virtual TransactionHead TransactionHead { get; set; }

        public virtual Workflow Workflow { get; set; }

        public virtual WorkflowStatus WorkflowStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkflowTransactionHeadRuleMap> WorkflowTransactionHeadRuleMaps { get; set; }
    }
}

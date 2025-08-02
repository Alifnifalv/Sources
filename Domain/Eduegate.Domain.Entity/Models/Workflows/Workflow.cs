using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models.Workflows
{
    [Table("Workflows", Schema = "workflow")]
    public partial class Workflow
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Workflow()
        {
            DocumentTypes = new HashSet<DocumentType>();
            WorkflowLogMaps = new HashSet<WorkflowLogMap>();
            WorkflowRules = new HashSet<WorkflowRule>();
            WorkflowTransactionHeadMaps = new HashSet<WorkflowTransactionHeadMap>();
        }

        [Key]
        public long WorkflowIID { get; set; }

        [StringLength(100)]
        public string WokflowName { get; set; }

        public int? WorkflowTypeID { get; set; }

        public int? LinkedEntityTypeID { get; set; }

        public int? WorkflowApplyFieldID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DocumentType> DocumentTypes { get; set; }

        public virtual EntityType EntityType { get; set; }

        public virtual WorkflowFiled WorkflowFiled { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkflowLogMap> WorkflowLogMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkflowRule> WorkflowRules { get; set; }

        public virtual WorkflowType WorkflowType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkflowTransactionHeadMap> WorkflowTransactionHeadMaps { get; set; }
    }
}
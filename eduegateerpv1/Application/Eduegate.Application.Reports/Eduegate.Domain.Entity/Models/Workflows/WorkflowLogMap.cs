namespace Eduegate.Domain.Entity.Models.Workflows
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("workflow.WorkflowLogMaps")]
    public partial class WorkflowLogMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WorkflowLogMap()
        {
            WorkflowLogMapRuleMaps = new HashSet<WorkflowLogMapRuleMap>();
        }

        [Key]
        public long WorkflowLogMapIID { get; set; }

        public long? WorkflowID { get; set; }

        public long? ReferenceID { get; set; }

        public byte? WorkflowStatusID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkflowLogMapRuleMap> WorkflowLogMapRuleMaps { get; set; }

        public virtual WorkflowStatus WorkflowStatus { get; set; }

        public virtual Workflow Workflow { get; set; }
    }
}

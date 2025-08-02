using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Worflow.WorkFlows
{
    [Table("workflow.WorkflowTypes")]
    public partial class WorkflowType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WorkflowType()
        {
            WorkflowFileds = new HashSet<WorkflowFiled>();
            Workflows = new HashSet<Workflow>();
            WorkflowStatuses = new HashSet<WorkflowStatus>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WorkflowTypeID { get; set; }

        [StringLength(50)]
        public string WorkflowTypeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkflowFiled> WorkflowFileds { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Workflow> Workflows { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkflowStatus> WorkflowStatuses { get; set; }
    }
}

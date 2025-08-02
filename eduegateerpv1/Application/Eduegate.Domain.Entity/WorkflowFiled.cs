namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("workflow.WorkflowFileds")]
    public partial class WorkflowFiled
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WorkflowFiled()
        {
            Workflows = new HashSet<Workflow>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WorkflowFieldID { get; set; }

        public int? WorkflowTypeID { get; set; }

        [StringLength(100)]
        public string ColumnName { get; set; }

        [StringLength(100)]
        public string PhysicalColumnName { get; set; }

        [StringLength(50)]
        public string WorkflowFieldType { get; set; }

        public virtual WorkflowType WorkflowType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Workflow> Workflows { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("WorkflowEntitys", Schema = "mutual")]
    public partial class WorkflowEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WorkflowEntity()
        {
            ClassSubjectWorkflowEntityMaps = new HashSet<ClassSubjectWorkflowEntityMap>();
            ClassWorkFlowMaps = new HashSet<ClassWorkFlowMap>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WorkflowEntityID { get; set; }

        [StringLength(50)]
        public string WorkflowEntityName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClassSubjectWorkflowEntityMap> ClassSubjectWorkflowEntityMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClassWorkFlowMap> ClassWorkFlowMaps { get; set; }

    }
}

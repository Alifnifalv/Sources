namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.ClassSubjectWorkflowEntityMaps")]
    public partial class ClassSubjectWorkflowEntityMap
    {
        [Key]
        public long ClassSubjectWorkflowEntityMapIID { get; set; }

        public long ClassSubjectMapID { get; set; }

        public int? WorkflowEntityID { get; set; }

        public long? workflowID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public int? SubjectID { get; set; }

        public virtual WorkflowEntity WorkflowEntity { get; set; }

        public virtual ClassSubjectMap ClassSubjectMap { get; set; }

        public virtual Workflow Workflow { get; set; }

        public virtual Subject Subject { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ClassSubjectWorkflowEntityMaps", Schema = "schools")]
    public partial class ClassSubjectWorkflowEntityMap
    {
        [Key]
        public long ClassSubjectWorkflowEntityMapIID { get; set; }
        public long ClassSubjectMapID { get; set; }
        public int? WorkflowEntityID { get; set; }
        public long? workflowID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? SubjectID { get; set; }

        [ForeignKey("ClassSubjectMapID")]
        [InverseProperty("ClassSubjectWorkflowEntityMaps")]
        public virtual ClassSubjectMap ClassSubjectMap { get; set; }
        [ForeignKey("SubjectID")]
        [InverseProperty("ClassSubjectWorkflowEntityMaps")]
        public virtual Subject Subject { get; set; }
        [ForeignKey("WorkflowEntityID")]
        [InverseProperty("ClassSubjectWorkflowEntityMaps")]
        public virtual WorkflowEntity WorkflowEntity { get; set; }
        [ForeignKey("workflowID")]
        [InverseProperty("ClassSubjectWorkflowEntityMaps")]
        public virtual Workflow workflow { get; set; }
    }
}

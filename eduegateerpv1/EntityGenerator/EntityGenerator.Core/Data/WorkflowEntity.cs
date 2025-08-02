using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("WorkflowEntitys", Schema = "mutual")]
    public partial class WorkflowEntity
    {
        public WorkflowEntity()
        {
            ClassSubjectWorkflowEntityMaps = new HashSet<ClassSubjectWorkflowEntityMap>();
            ClassWorkFlowMaps = new HashSet<ClassWorkFlowMap>();
        }

        [Key]
        public int WorkflowEntityID { get; set; }
        [StringLength(50)]
        public string WorkflowEntityName { get; set; }

        [InverseProperty("WorkflowEntity")]
        public virtual ICollection<ClassSubjectWorkflowEntityMap> ClassSubjectWorkflowEntityMaps { get; set; }
        [InverseProperty("WorkflowEntity")]
        public virtual ICollection<ClassWorkFlowMap> ClassWorkFlowMaps { get; set; }
    }
}

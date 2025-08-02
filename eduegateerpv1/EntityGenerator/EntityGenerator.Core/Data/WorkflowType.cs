using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("WorkflowTypes", Schema = "workflow")]
    public partial class WorkflowType
    {
        public WorkflowType()
        {
            WorkflowFileds = new HashSet<WorkflowFiled>();
            WorkflowStatus = new HashSet<WorkflowStatus>();
            Workflows = new HashSet<Workflow>();
        }

        [Key]
        public int WorkflowTypeID { get; set; }
        [StringLength(50)]
        public string WorkflowTypeName { get; set; }

        [InverseProperty("WorkflowType")]
        public virtual ICollection<WorkflowFiled> WorkflowFileds { get; set; }
        [InverseProperty("WorkflowType")]
        public virtual ICollection<WorkflowStatus> WorkflowStatus { get; set; }
        [InverseProperty("WorkflowType")]
        public virtual ICollection<Workflow> Workflows { get; set; }
    }
}

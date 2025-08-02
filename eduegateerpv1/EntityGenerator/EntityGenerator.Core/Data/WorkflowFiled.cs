using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("WorkflowFileds", Schema = "workflow")]
    public partial class WorkflowFiled
    {
        public WorkflowFiled()
        {
            Workflows = new HashSet<Workflow>();
        }

        [Key]
        public int WorkflowFieldID { get; set; }
        public int? WorkflowTypeID { get; set; }
        [StringLength(100)]
        public string ColumnName { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string PhysicalColumnName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string WorkflowFieldType { get; set; }

        [ForeignKey("WorkflowTypeID")]
        [InverseProperty("WorkflowFileds")]
        public virtual WorkflowType WorkflowType { get; set; }
        [InverseProperty("WorkflowApplyField")]
        public virtual ICollection<Workflow> Workflows { get; set; }
    }
}

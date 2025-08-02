using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TaskStatuses", Schema = "task")]
    public partial class TaskStatus
    {
        public TaskStatus()
        {
            Tasks = new HashSet<Task>();
        }

        [Key]
        public byte TaskStatusID { get; set; }
        [StringLength(50)]
        public string TaskStausName { get; set; }

        [InverseProperty("TaskStatus")]
        public virtual ICollection<Task> Tasks { get; set; }
    }
}

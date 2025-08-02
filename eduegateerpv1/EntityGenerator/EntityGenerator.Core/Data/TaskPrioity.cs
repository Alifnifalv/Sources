using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TaskPrioities", Schema = "task")]
    public partial class TaskPrioity
    {
        public TaskPrioity()
        {
            Tasks = new HashSet<Task>();
        }

        [Key]
        public byte TaskPriorityID { get; set; }
        [StringLength(50)]
        public string TaskPriorityName { get; set; }

        [InverseProperty("TaskPrioity")]
        public virtual ICollection<Task> Tasks { get; set; }
    }
}

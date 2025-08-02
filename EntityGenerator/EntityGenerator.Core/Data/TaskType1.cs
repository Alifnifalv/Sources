using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TaskTypes", Schema = "task")]
    public partial class TaskType1
    {
        public TaskType1()
        {
            Tasks = new HashSet<Task>();
        }

        [Key]
        public byte TaskTypeID { get; set; }
        [StringLength(50)]
        public string TaskTypeName { get; set; }

        [InverseProperty("TaskType")]
        public virtual ICollection<Task> Tasks { get; set; }
    }
}

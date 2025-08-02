using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Tasks", Schema = "task")]
    public partial class Task
    {
        public Task()
        {
            EmployeeTimeSheets = new HashSet<EmployeeTimeSheet>();
            TaskAssingners = new HashSet<TaskAssingner>();
        }

        [Key]
        public long TaskIID { get; set; }
        public string Description { get; set; }
        public byte? TaskTypeID { get; set; }
        public byte? TaskStatusID { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string ColorCode { get; set; }
        public byte? TaskPrioityID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DueDate { get; set; }
        public int? ReferenceEntiyTypeID { get; set; }
        public long? ReferenceEntityID { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public bool? IsActive { get; set; }

        [ForeignKey("TaskPrioityID")]
        [InverseProperty("Tasks")]
        public virtual TaskPrioity TaskPrioity { get; set; }
        [ForeignKey("TaskStatusID")]
        [InverseProperty("Tasks")]
        public virtual TaskStatus TaskStatus { get; set; }
        [ForeignKey("TaskTypeID")]
        [InverseProperty("Tasks")]
        public virtual TaskType1 TaskType { get; set; }
        [InverseProperty("Task")]
        public virtual ICollection<EmployeeTimeSheet> EmployeeTimeSheets { get; set; }
        [InverseProperty("Task")]
        public virtual ICollection<TaskAssingner> TaskAssingners { get; set; }
    }
}

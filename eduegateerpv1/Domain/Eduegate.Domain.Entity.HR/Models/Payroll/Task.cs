namespace Eduegate.Domain.Entity.HR.Payroll
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tasks", Schema = "task")]
    public partial class Task
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Task()
        {
            EmployeeTimeSheets = new HashSet<EmployeeTimeSheet>();
            //TaskAssingners = new HashSet<TaskAssingner>();
        }

        [Key]
        public long TaskIID { get; set; }

        public string Description { get; set; }

        public byte? TaskTypeID { get; set; }

        public byte? TaskStatusID { get; set; }

        [StringLength(20)]
        public string ColorCode { get; set; }

        public byte? TaskPrioityID { get; set; }

        public DateTime? DueDate { get; set; }

        public int? ReferenceEntiyTypeID { get; set; }

        public long? ReferenceEntityID { get; set; }

        public long? CreatedBy { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public bool? IsActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeTimeSheet> EmployeeTimeSheets { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TaskAssingner> TaskAssingners { get; set; }

        //public virtual TaskPrioity TaskPrioity { get; set; }

        //public virtual TaskStatus TaskStatus { get; set; }

        //public virtual TaskType TaskType { get; set; }
    }
}

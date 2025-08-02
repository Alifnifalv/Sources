namespace  Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DepartmentStatuses", Schema = "mutual")]
    public partial class DepartmentStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DepartmentStatus()
        {
            Departments1 = new HashSet<Departments1>();
        }
        [Key]
        public byte DepartmentStatusID { get; set; }

        [StringLength(50)]
        public string StatusName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Departments1> Departments1 { get; set; }
    }
}


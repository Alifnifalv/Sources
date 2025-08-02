namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TimeTables", Schema = "schools")]
    public partial class TimeTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TimeTable()
        {
            TimeTableAllocations = new HashSet<TimeTableAllocation>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TimeTableID { get; set; }

        [StringLength(100)]
        public string TimeTableDescription { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? AcademicYearID { get; set; }

        public bool? IsActive { get; set; }
        public byte? SchoolID { get; set; }

        public virtual Schools School { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeTableAllocation> TimeTableAllocations { get; set; }
    }
}

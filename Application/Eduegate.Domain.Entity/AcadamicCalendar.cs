namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.AcadamicCalendars")]
    public partial class AcadamicCalendar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AcadamicCalendar()
        {
            Employees = new HashSet<Employee>();
            AcademicYearCalendarEvents = new HashSet<AcademicYearCalendarEvent>();
            CalendarEntries = new HashSet<CalendarEntry>();
        }

        [Key]
        public long AcademicCalendarID { get; set; }

        [StringLength(50)]
        public string CalenderName { get; set; }

        public int? AcademicYearID { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public int? CreatedBy { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public byte? AcademicCalendarStatusID { get; set; }

        public byte? SchoolID { get; set; }

        public byte? CalendarTypeID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees { get; set; }

        public virtual School School { get; set; }

        public virtual AcademicYearCalendarStatu AcademicYearCalendarStatu { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AcademicYearCalendarEvent> AcademicYearCalendarEvents { get; set; }

        public virtual CalendarType CalendarType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CalendarEntry> CalendarEntries { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AcadamicCalendars", Schema = "schools")]
    public partial class AcadamicCalendar
    {
        public AcadamicCalendar()
        {
            AcademicYearCalendarEvents = new HashSet<AcademicYearCalendarEvent>();
            CalendarEntries = new HashSet<CalendarEntry>();
            Employees = new HashSet<Employee>();
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? AcademicCalendarStatusID { get; set; }
        public byte? SchoolID { get; set; }
        public byte? CalendarTypeID { get; set; }

        [ForeignKey("AcademicCalendarStatusID")]
        [InverseProperty("AcadamicCalendars")]
        public virtual AcademicYearCalendarStatu AcademicCalendarStatus { get; set; }
        [ForeignKey("AcademicYearID")]
        [InverseProperty("AcadamicCalendars")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("CalendarTypeID")]
        [InverseProperty("AcadamicCalendars")]
        public virtual CalendarType CalendarType { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("AcadamicCalendars")]
        public virtual School School { get; set; }
        [InverseProperty("AcademicCalendar")]
        public virtual ICollection<AcademicYearCalendarEvent> AcademicYearCalendarEvents { get; set; }
        [InverseProperty("AcademicCalendar")]
        public virtual ICollection<CalendarEntry> CalendarEntries { get; set; }
        [InverseProperty("AcademicCalendar")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}

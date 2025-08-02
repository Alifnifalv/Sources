using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.HR.Payroll
{
    [Table("AcademicYearCalendarEvents", Schema = "schools")]
    public partial class AcademicYearCalendarEvent
    {
        [Key]
        public long AcademicYearCalendarEventIID { get; set; }

        public long AcademicCalendarID { get; set; }

        [StringLength(500)]
        public string EventTitle { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? IsThisAHoliday { get; set; }

        public bool IsEnableReminders { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        [StringLength(250)]
        public string ColorCode { get; set; }

        public int? AcademicYearCalendarEventTypeID { get; set; }

        public decimal? NoofHours { get; set; }

        public virtual AcadamicCalendar AcadamicCalendar { get; set; }

        public virtual AcademicYearCalendarEventType AcademicYearCalendarEventType { get; set; }
    }
}

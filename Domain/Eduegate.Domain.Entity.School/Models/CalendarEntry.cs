using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Eduegate.Domain.Entity.School.Models
{
    [Table("CalendarEntries", Schema = "schools")]
    public partial class CalendarEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long CalendarEntryID { get; set; }

        public long AcademicCalendarID { get; set; }

        public DateTime? CalendarDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public decimal? NoofHours { get; set; }

        //public virtual AcadamicCalendar AcadamicCalendar { get; set; }
    }
}

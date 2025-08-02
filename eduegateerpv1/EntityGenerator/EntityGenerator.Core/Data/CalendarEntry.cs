using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CalendarEntries", Schema = "schools")]
    public partial class CalendarEntry
    {
        [Key]
        public long CalendarEntryID { get; set; }
        public long AcademicCalendarID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CalendarDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal? NoofHours { get; set; }

        [ForeignKey("AcademicCalendarID")]
        [InverseProperty("CalendarEntries")]
        public virtual AcadamicCalendar AcademicCalendar { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class AcademicYearCalendarEvents_back
    {
        public long AcademicYearCalendarEventIID { get; set; }
        public long AcademicCalendarID { get; set; }
        [StringLength(500)]
        public string EventTitle { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        public bool? IsThisAHoliday { get; set; }
        public bool IsEnableReminders { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [StringLength(250)]
        public string ColorCode { get; set; }
        public int? AcademicYearCalendarEventTypeID { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal? NoofHours { get; set; }
    }
}

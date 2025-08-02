using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class AcademicYearCalenderSearchView
    {
        public long AcademicYearCalendarEventIID { get; set; }
        public long AcademicCalendarID { get; set; }
        [StringLength(50)]
        public string Event { get; set; }
        [StringLength(500)]
        public string EventTitle { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string StartDate { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string EndDate { get; set; }
        public bool? IsThisAHoliday { get; set; }
        public bool IsEnableReminders { get; set; }
    }
}

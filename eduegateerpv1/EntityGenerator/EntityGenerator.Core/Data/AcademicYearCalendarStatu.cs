using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AcademicYearCalendarStatus", Schema = "schools")]
    public partial class AcademicYearCalendarStatu
    {
        public AcademicYearCalendarStatu()
        {
            AcadamicCalendars = new HashSet<AcadamicCalendar>();
        }

        [Key]
        public byte AcademicYearCalendarStatusID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }

        [InverseProperty("AcademicCalendarStatus")]
        public virtual ICollection<AcadamicCalendar> AcadamicCalendars { get; set; }
    }
}

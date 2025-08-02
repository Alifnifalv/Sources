using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AcademicYearCalendarEventType", Schema = "schools")]
    public partial class AcademicYearCalendarEventType
    {
        public AcademicYearCalendarEventType()
        {
            AcademicYearCalendarEvents = new HashSet<AcademicYearCalendarEvent>();
        }

        [Key]
        public int AcademicYearCalendarEventTypeID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }

        [InverseProperty("AcademicYearCalendarEventType")]
        public virtual ICollection<AcademicYearCalendarEvent> AcademicYearCalendarEvents { get; set; }
    }
}

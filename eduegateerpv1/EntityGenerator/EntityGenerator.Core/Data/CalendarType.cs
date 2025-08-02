using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CalendarTypes", Schema = "schools")]
    public partial class CalendarType
    {
        public CalendarType()
        {
            AcadamicCalendars = new HashSet<AcadamicCalendar>();
            Employees = new HashSet<Employee>();
        }

        [Key]
        public byte CalendarTypeID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }

        [InverseProperty("CalendarType")]
        public virtual ICollection<AcadamicCalendar> AcadamicCalendars { get; set; }
        [InverseProperty("CalendarType")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}

using Eduegate.Domain.Entity.Models.HR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("AcademicYearCalendarEventType", Schema = "schools")]
    public partial class AcademicYearCalendarEventType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AcademicYearCalendarEventType()
        {
            AcademicYearCalendarEvents = new HashSet<AcademicYearCalendarEvent>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AcademicYearCalendarEventTypeID { get; set; }

        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AcademicYearCalendarEvent> AcademicYearCalendarEvents { get; set; }
    }
}

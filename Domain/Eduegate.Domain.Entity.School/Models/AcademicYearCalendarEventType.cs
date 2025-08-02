namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AcademicYearCalendarEventType", Schema = "schools")]
    public partial class AcademicYearCalendarEventType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AcademicYearCalendarEventType()
        {
            AcademicYearCalendarEvents = new HashSet<AcademicYearCalendarEvent>();
        }

        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AcademicYearCalendarEventTypeID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AcademicYearCalendarEvent> AcademicYearCalendarEvents { get; set; }
    }
}

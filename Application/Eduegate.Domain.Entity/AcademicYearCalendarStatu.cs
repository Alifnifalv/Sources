namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.AcademicYearCalendarStatus")]
    public partial class AcademicYearCalendarStatu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AcademicYearCalendarStatu()
        {
            AcadamicCalendars = new HashSet<AcadamicCalendar>();
        }

        [Key]
        public byte AcademicYearCalendarStatusID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AcadamicCalendar> AcadamicCalendars { get; set; }
    }
}

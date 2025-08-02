namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RouteTypes", Schema = "schools")]
    public partial class RouteType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RouteType()
        {
            Routes1 = new HashSet<Routes1>();
        }
        [Key]
        public byte RouteTypeID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }
        public virtual Schools School { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Routes1> Routes1 { get; set; }
    }
}

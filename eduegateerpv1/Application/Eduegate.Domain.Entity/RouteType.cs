namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.RouteTypes")]
    public partial class RouteType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RouteType()
        {
            Routes1 = new HashSet<Routes1>();
        }

        public byte RouteTypeID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public bool? IsVisible { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Routes1> Routes1 { get; set; }

        public virtual School School { get; set; }
    }
}

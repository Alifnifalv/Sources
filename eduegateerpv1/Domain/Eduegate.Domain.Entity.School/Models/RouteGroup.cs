using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("RouteGroups", Schema = "schools")]
    public partial class RouteGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RouteGroup()
        {
            Routes1 = new HashSet<Routes1>();
        }
        [Key]
        public int RouteGroupID { get; set; }

        public string Description { get; set; }

        public int? AcademicYearID { get; set; }

        public bool? IsActive { get; set; }

        public byte? SchoolID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Schools Schools { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Routes1> Routes1 { get; set; }
    }
}
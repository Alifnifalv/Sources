using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Eduegate.Domain.Entity.School.Models
{
    [Table("AcademicYearStatus", Schema = "schools")]
    public partial class AcademicYearStatu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AcademicYearStatu()
        {
            AcademicYears = new HashSet<AcademicYear>();
        }

        [Key]
        public byte AcademicYearStatusID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AcademicYear> AcademicYears { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Lms.Models
{
    [Table("AcademicYearStatus", Schema = "schools")]
    public partial class AcademicYearStatu
    {
        public AcademicYearStatu()
        {
            AcademicYears = new HashSet<AcademicYear>();
        }

        [Key]
        public byte AcademicYearStatusID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public virtual ICollection<AcademicYear> AcademicYears { get; set; }
    }
}
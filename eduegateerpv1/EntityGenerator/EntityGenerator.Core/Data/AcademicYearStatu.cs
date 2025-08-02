using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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

        [InverseProperty("AcademicYearStatus")]
        public virtual ICollection<AcademicYear> AcademicYears { get; set; }
    }
}

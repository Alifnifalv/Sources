using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Eduegate.Domain.Entity.Models
{
    [Table("schools.AllergyStudentMaps")]
    public partial class AllergyStudentMap
    {
        [Key]
        public long AllergyStudentMapIID { get; set; }

        public int? AllergyID { get; set; }

        public long? StudentID { get; set; }

        public string Remarks { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public byte? SeverityID { get; set; }

        public virtual Severity Severity { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Allergy Allergy { get; set; }

        public virtual Schools School { get; set; }

        public virtual Student Student { get; set; }
    }
}
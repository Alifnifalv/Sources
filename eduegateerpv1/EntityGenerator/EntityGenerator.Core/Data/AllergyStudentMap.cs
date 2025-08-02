using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AllergyStudentMaps", Schema = "schools")]
    public partial class AllergyStudentMap
    {
        [Key]
        public long AllergyStudentMapIID { get; set; }
        public int? AllergyID { get; set; }
        public long? StudentID { get; set; }
        public string Remarks { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public byte? SeverityID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("AllergyStudentMaps")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("AllergyID")]
        [InverseProperty("AllergyStudentMaps")]
        public virtual Allergy Allergy { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("AllergyStudentMaps")]
        public virtual School School { get; set; }
        [ForeignKey("SeverityID")]
        [InverseProperty("AllergyStudentMaps")]
        public virtual Severity Severity { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("AllergyStudentMaps")]
        public virtual Student Student { get; set; }
    }
}

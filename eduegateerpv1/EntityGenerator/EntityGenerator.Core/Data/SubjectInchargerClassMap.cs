using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SubjectInchargerClassMaps", Schema = "schools")]
    public partial class SubjectInchargerClassMap
    {
        [Key]
        public long SubjectInchargerClassMapIID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public int? SubjectID { get; set; }
        public long? EmployeeID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("SubjectInchargerClassMaps")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("SubjectInchargerClassMaps")]
        public virtual Class Class { get; set; }
        [ForeignKey("EmployeeID")]
        [InverseProperty("SubjectInchargerClassMaps")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("SubjectInchargerClassMaps")]
        public virtual School School { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("SubjectInchargerClassMaps")]
        public virtual Section Section { get; set; }
        [ForeignKey("SubjectID")]
        [InverseProperty("SubjectInchargerClassMaps")]
        public virtual Subject Subject { get; set; }
    }
}

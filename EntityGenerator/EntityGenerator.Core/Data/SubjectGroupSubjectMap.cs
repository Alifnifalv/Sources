using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SubjectGroupSubjectMaps", Schema = "schools")]
    public partial class SubjectGroupSubjectMap
    {
        [Key]
        public long SubjectGroupSubjectMapIID { get; set; }
        public int? ClassID { get; set; }
        public int? SubjectID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public byte? SubjectGroupID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("SubjectGroupSubjectMaps")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("SubjectGroupSubjectMaps")]
        public virtual Class Class { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("SubjectGroupSubjectMaps")]
        public virtual School School { get; set; }
        [ForeignKey("SubjectID")]
        [InverseProperty("SubjectGroupSubjectMaps")]
        public virtual Subject Subject { get; set; }
        [ForeignKey("SubjectGroupID")]
        [InverseProperty("SubjectGroupSubjectMaps")]
        public virtual SubjectGroup SubjectGroup { get; set; }
    }
}

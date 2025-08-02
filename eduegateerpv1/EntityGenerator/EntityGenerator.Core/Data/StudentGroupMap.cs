using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentGroupMaps", Schema = "schools")]
    public partial class StudentGroupMap
    {
        [Key]
        public long StudentGroupMapIID { get; set; }
        public long? StudentID { get; set; }
        public int? StudentGroupID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public bool? IsActive { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("StudentGroupMaps")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("StudentGroupMaps")]
        public virtual School School { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("StudentGroupMaps")]
        public virtual Student Student { get; set; }
        [ForeignKey("StudentGroupID")]
        [InverseProperty("StudentGroupMaps")]
        public virtual StudentGroup StudentGroup { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SignupAudienceMaps", Schema = "signup")]
    public partial class SignupAudienceMap
    {
        [Key]
        public long SignupAudienceMapIID { get; set; }
        public long? SignupID { get; set; }
        public long? StudentID { get; set; }
        public long? EmployeeID { get; set; }
        public long? ParentID { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("SignupAudienceMaps")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("EmployeeID")]
        [InverseProperty("SignupAudienceMaps")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("ParentID")]
        [InverseProperty("SignupAudienceMaps")]
        public virtual Parent Parent { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("SignupAudienceMaps")]
        public virtual School School { get; set; }
        [ForeignKey("SignupID")]
        [InverseProperty("SignupAudienceMaps")]
        public virtual Signup Signup { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("SignupAudienceMaps")]
        public virtual Student Student { get; set; }
    }
}

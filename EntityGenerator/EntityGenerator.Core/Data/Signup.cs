using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Signups", Schema = "signup")]
    public partial class Signup
    {
        public Signup()
        {
            SignupAudienceMaps = new HashSet<SignupAudienceMap>();
            SignupSlotMaps = new HashSet<SignupSlotMap>();
            SignupSlotRemarkMaps = new HashSet<SignupSlotRemarkMap>();
        }

        [Key]
        public long SignupIID { get; set; }
        [StringLength(50)]
        public string SignupName { get; set; }
        public byte? SchoolID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public long? OrganizerEmployeeID { get; set; }
        public string LocationInfo { get; set; }
        [StringLength(200)]
        public string Message { get; set; }
        public string Remarks { get; set; }
        public int? AcademicYearID { get; set; }
        public long? StudentID { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte? SignupCategoryID { get; set; }
        public byte? SignupTypeID { get; set; }
        public byte? SignupStatusID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateTo { get; set; }
        public int? SignupGroupID { get; set; }
        public bool? IsSlotShowToUser { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("Signups")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("Signups")]
        public virtual Class Class { get; set; }
        [ForeignKey("OrganizerEmployeeID")]
        [InverseProperty("Signups")]
        public virtual Employee OrganizerEmployee { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("Signups")]
        public virtual School School { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("Signups")]
        public virtual Section Section { get; set; }
        [ForeignKey("SignupCategoryID")]
        [InverseProperty("Signups")]
        public virtual SignupCategory SignupCategory { get; set; }
        [ForeignKey("SignupGroupID")]
        [InverseProperty("Signups")]
        public virtual SignupGroup SignupGroup { get; set; }
        [ForeignKey("SignupStatusID")]
        [InverseProperty("Signups")]
        public virtual SignupStatus SignupStatus { get; set; }
        [ForeignKey("SignupTypeID")]
        [InverseProperty("Signups")]
        public virtual SignupType SignupType { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("Signups")]
        public virtual Student Student { get; set; }
        [InverseProperty("Signup")]
        public virtual ICollection<SignupAudienceMap> SignupAudienceMaps { get; set; }
        [InverseProperty("Signup")]
        public virtual ICollection<SignupSlotMap> SignupSlotMaps { get; set; }
        [InverseProperty("Signup")]
        public virtual ICollection<SignupSlotRemarkMap> SignupSlotRemarkMaps { get; set; }
    }
}

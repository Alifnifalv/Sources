using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.SignUp.Models
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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public byte? SignupCategoryID { get; set; }

        public byte? SignupTypeID { get; set; }

        public byte? SignupStatusID { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public int? SignupGroupID { get; set; }

        public bool? IsSlotShowToUser { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Class Class { get; set; }

        public virtual Employee OrganizerEmployee { get; set; }

        public virtual School School { get; set; }

        public virtual Section Section { get; set; }

        public virtual SignupCategory SignupCategory { get; set; }

        public virtual SignupGroup SignupGroup { get; set; }

        public virtual SignupStatus SignupStatus { get; set; }

        public virtual SignupType SignupType { get; set; }

        public virtual Student Student { get; set; }

        public virtual ICollection<SignupAudienceMap> SignupAudienceMaps { get; set; }

        public virtual ICollection<SignupSlotMap> SignupSlotMaps { get; set; }

        public virtual ICollection<SignupSlotRemarkMap> SignupSlotRemarkMaps { get; set; }
    }
}
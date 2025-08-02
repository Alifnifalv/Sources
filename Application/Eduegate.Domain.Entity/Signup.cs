namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("signup.Signups")]
    public partial class Signup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Signup()
        {
            SignupAudienceMaps = new HashSet<SignupAudienceMap>();
            SignupSlotMaps = new HashSet<SignupSlotMap>();
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

        public virtual Employee Employee { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Class Class { get; set; }

        public virtual School School { get; set; }

        public virtual Section Section { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SignupAudienceMap> SignupAudienceMaps { get; set; }

        public virtual SignupCategory SignupCategory { get; set; }

        public virtual SignupType SignupType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SignupSlotMap> SignupSlotMaps { get; set; }
    }
}

namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("signup.SignupAudienceMaps")]
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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Parent Parent { get; set; }

        public virtual School School { get; set; }

        public virtual Student Student { get; set; }

        public virtual Signup Signup { get; set; }
    }
}

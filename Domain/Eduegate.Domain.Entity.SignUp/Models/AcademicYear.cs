using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.SignUp.Models
{
    [Table("AcademicYears", Schema = "schools")]
    public partial class AcademicYear
    {
        public AcademicYear()
        {
            MeetingRequests = new HashSet<MeetingRequest>();
            Parents = new HashSet<Parent>();
            Sections = new HashSet<Section>();
            SignupAudienceMaps = new HashSet<SignupAudienceMap>();
            SignupSlotAllocationMaps = new HashSet<SignupSlotAllocationMap>();
            SignupSlotMaps = new HashSet<SignupSlotMap>();
            Signups = new HashSet<Signup>();
            StudentAcademicYears = new HashSet<Student>();
            StudentSchoolAcademicyears = new HashSet<Student>();
        }

        [Key]
        public int AcademicYearID { get; set; }

        [StringLength(20)]
        public string AcademicYearCode { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public byte? SchoolID { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        public byte? AcademicYearStatusID { get; set; }

        public int? ORDERNO { get; set; }

        public virtual AcademicYearStatu AcademicYearStatus { get; set; }
        
        public virtual School School { get; set; }

        public virtual ICollection<Class> Classes { get; set; }

        public virtual ICollection<MeetingRequest> MeetingRequests { get; set; }

        public virtual ICollection<Parent> Parents { get; set; }
        
        public virtual ICollection<Section> Sections { get; set; }
        
        public virtual ICollection<SignupAudienceMap> SignupAudienceMaps { get; set; }

        public virtual ICollection<SignupSlotAllocationMap> SignupSlotAllocationMaps { get; set; }

        public virtual ICollection<SignupSlotMap> SignupSlotMaps { get; set; }

        public virtual ICollection<Signup> Signups { get; set; }
        
        public virtual ICollection<Student> StudentAcademicYears { get; set; }
        
        public virtual ICollection<Student> StudentSchoolAcademicyears { get; set; }
        
    }
}
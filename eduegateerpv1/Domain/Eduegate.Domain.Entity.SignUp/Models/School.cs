using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.SignUp.Models
{
    [Table("Schools", Schema = "schools")]
    public partial class School
    {
        public School()
        {
            AcademicYears = new HashSet<AcademicYear>();
            Classes = new HashSet<Class>();
            MeetingRequests = new HashSet<MeetingRequest>();
            Parents = new HashSet<Parent>();
            Sections = new HashSet<Section>();
            SignupAudienceMaps = new HashSet<SignupAudienceMap>();
            SignupSlotAllocationMaps = new HashSet<SignupSlotAllocationMap>();
            SignupSlotMaps = new HashSet<SignupSlotMap>();
            Signups = new HashSet<Signup>();
        }

        [Key]
        public byte SchoolID { get; set; }

        [StringLength(50)]
        public string SchoolName { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [StringLength(500)]
        public string Address1 { get; set; }

        [StringLength(500)]
        public string Address2 { get; set; }

        [StringLength(50)]
        public string RegistrationID { get; set; }

        public int? CompanyID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        [StringLength(50)]
        public string SchoolCode { get; set; }

        [StringLength(100)]
        public string Place { get; set; }

        public string EmployerEID { get; set; }

        public string PayerEID { get; set; }

        public string PayerQID { get; set; }

        [StringLength(10)]
        public string SchoolShortName { get; set; }

        public long? SchoolProfileID { get; set; }

        public long? SchoolSealID { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<AcademicYear> AcademicYears { get; set; }
        
        public virtual ICollection<Class> Classes { get; set; }

        public virtual ICollection<MeetingRequest> MeetingRequests { get; set; }

        public virtual ICollection<Parent> Parents { get; set; }
        
        public virtual ICollection<Section> Sections { get; set; }
        
        public virtual ICollection<SignupAudienceMap> SignupAudienceMaps { get; set; }

        public virtual ICollection<SignupSlotAllocationMap> SignupSlotAllocationMaps { get; set; }

        public virtual ICollection<SignupSlotMap> SignupSlotMaps { get; set; }

        public virtual ICollection<Signup> Signups { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        
    }
}
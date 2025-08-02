using EntityGenerator.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("JobSeekers", Schema = "hr")]
    public partial class JobSeeker
    {
        public JobSeeker()
        {
            JobApplications = new HashSet<JobApplication>();
            JobInterviewMaps = new HashSet<JobInterviewMap>();
            JobSeekerSkillMaps = new HashSet<JobSeekerSkillMap>();
        }

        [Key]
        public long SeekerID { get; set; }
        public long? LoginID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string EmailID { get; set; }
        public string ReferenceCode { get; set; }
        public string Education { get; set; }
        public int? TotalWorkExperience { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string LinkedIn { get; set; }
        public string Instagram { get; set; }
        public long? ProfileContentID { get; set; }
        public long? CVContentID { get; set; }
        public byte? GenderID { get; set; }           
        public DateTime? DateOfBirth { get; set; }
        public int? CountryID { get; set; }
        public byte? QualificationID { get; set; }
        public byte? Age { get; set; }
        public int? BloodGroupID { get; set; }
        public string PassportNumber { get; set; }
        public DateTime? PassportExpiry { get; set; }
        public int? PassportIssueCountryID { get; set; }
        public string NationalID { get; set; }
        public int? NationalityID { get; set; }

        public virtual Gender Gender { get; set; }
        public virtual RecruitmentLogin Login { get; set; }
        public virtual ICollection<JobApplication> JobApplications { get; set; }
        public virtual ICollection<JobInterviewMap> JobInterviewMaps { get; set; }
        public virtual Qualification Qualification { get; set; }
        public virtual Country Country { get; set; }
        public virtual Country PassportIssueCountry { get; set; }
        public virtual Nationality Nationality { get; set; }
        public virtual BloodGroup BloodGroup { get; set; }
        public virtual ICollection<JobSeekerSkillMap> JobSeekerSkillMaps { get; set; }


    }
}

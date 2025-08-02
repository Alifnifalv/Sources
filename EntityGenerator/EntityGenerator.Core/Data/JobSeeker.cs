using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string PhoneNumber { get; set; }
        [StringLength(50)]
        public string MobileNumber { get; set; }
        [StringLength(250)]
        public string EmailID { get; set; }
        [StringLength(250)]
        public string ReferenceCode { get; set; }
        [StringLength(250)]
        public string Education { get; set; }
        public int? TotalWorkExperience { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string LinkedIn { get; set; }
        public string Instagram { get; set; }
        public long? ProfileContentID { get; set; }
        public long? CVContentID { get; set; }
        public byte? GenderID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfBirth { get; set; }
        public string MiddleName { get; set; }
        public int? CountryID { get; set; }
        public byte? QualificationID { get; set; }
        public byte? Age { get; set; }
        public int? BloodGroupID { get; set; }
        [StringLength(250)]
        public string PassportNumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PassportExpiry { get; set; }
        public int? PassportIssueCountryID { get; set; }
        [StringLength(250)]
        public string NationalID { get; set; }
        public int? NationalityID { get; set; }

        [ForeignKey("BloodGroupID")]
        [InverseProperty("JobSeekers")]
        public virtual BloodGroup BloodGroup { get; set; }
        [ForeignKey("CountryID")]
        [InverseProperty("JobSeekerCountries")]
        public virtual Country Country { get; set; }
        [ForeignKey("GenderID")]
        [InverseProperty("JobSeekers")]
        public virtual Gender Gender { get; set; }
        [ForeignKey("LoginID")]
        [InverseProperty("JobSeekers")]
        public virtual RecruitmentLogin Login { get; set; }
        [ForeignKey("NationalityID")]
        [InverseProperty("JobSeekers")]
        public virtual Nationality Nationality { get; set; }
        [ForeignKey("PassportIssueCountryID")]
        [InverseProperty("JobSeekerPassportIssueCountries")]
        public virtual Country PassportIssueCountry { get; set; }
        [ForeignKey("QualificationID")]
        [InverseProperty("JobSeekers")]
        public virtual Qualification Qualification { get; set; }
        [InverseProperty("Applicant")]
        public virtual ICollection<JobApplication> JobApplications { get; set; }
        [InverseProperty("Applicant")]
        public virtual ICollection<JobInterviewMap> JobInterviewMaps { get; set; }
        [InverseProperty("Seeker")]
        public virtual ICollection<JobSeekerSkillMap> JobSeekerSkillMaps { get; set; }
    }
}

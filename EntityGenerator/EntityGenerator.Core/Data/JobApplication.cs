using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("JobApplications", Schema = "hr")]
    public partial class JobApplication
    {
        public JobApplication()
        {
            EmployeeJobDescriptions = new HashSet<EmployeeJobDescription>();
        }

        [Key]
        public long ApplicationIID { get; set; }
        public long? JobID { get; set; }
        public long? ApplicantID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AppliedDate { get; set; }
        [StringLength(250)]
        public string ReferenceCode { get; set; }
        public int? TotalYearOfExperience { get; set; }
        public long? CVContentID { get; set; }
        public string Remarks { get; set; }
        public bool? IsShortListed { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ShortListDate { get; set; }
        public int? CountryOfResidenceID { get; set; }

        [ForeignKey("ApplicantID")]
        [InverseProperty("JobApplications")]
        public virtual JobSeeker Applicant { get; set; }
        [ForeignKey("CountryOfResidenceID")]
        [InverseProperty("JobApplications")]
        public virtual Country CountryOfResidence { get; set; }
        [ForeignKey("JobID")]
        [InverseProperty("JobApplications")]
        public virtual AvailableJob Job { get; set; }
        [InverseProperty("JobApplication")]
        public virtual ICollection<EmployeeJobDescription> EmployeeJobDescriptions { get; set; }
    }
}

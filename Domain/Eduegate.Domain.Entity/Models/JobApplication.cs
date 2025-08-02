using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
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
        public DateTime? AppliedDate { get; set; }
        public string ReferenceCode { get; set; }
        public int? CountryOfResidenceID { get; set; }
        public int? TotalYearOfExperience { get; set; }
        public long? CVContentID { get; set; }
        public string Remarks { get; set; }
        public bool? IsShortListed { get; set; }
        public DateTime? ShortListDate { get; set; }
        public virtual JobSeeker Applicant { get; set; }
        public virtual AvailableJob Job { get; set; }
        public virtual Country CountryOfResidence { get; set; }
        public virtual ICollection<EmployeeJobDescription> EmployeeJobDescriptions { get; set; }

    }
}

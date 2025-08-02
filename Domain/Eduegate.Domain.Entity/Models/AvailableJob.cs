using EntityGenerator.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Eduegate.Domain.Entity.Models
{
    [Table("AvailableJobs", Schema = "hr")]
    public partial class AvailableJob
    {
        public AvailableJob()
        {
            AvailableJobTags = new HashSet<AvailableJobTag>();
            JobApplications = new HashSet<JobApplication>();
            AvailableJobCriteriaMaps = new HashSet<AvailableJobCriteriaMap>();
            AvailableJobSkillMaps = new HashSet<AvailableJobSkillMap>();
            JobInterviews = new HashSet<JobInterview>();
        }

        [Key]
        public long JobIID { get; set; }
        public string JobTitle { get; set; }
        public string TypeOfJob { get; set; }
        public string JobDescription { get; set; }
        public string JobDetails { get; set; }
        public string Status { get; set; }
        public Guid? Id { get; set; }
        public Guid? PageId { get; set; }
        public long? DepartmentId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public byte? SchoolID { get; set; }
        public int? JobTypeID { get; set; }
        public byte? StatusID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string MonthlySalary { get; set; }
        public int? TotalYearOfExperience { get; set; }
        public int? NoOfVacancies { get; set; }
        public int? CountryID { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public int? DesignationID { get; set; }
        public long? JDReferenceID { get; set; }
        public string Location { get; set; }
        public virtual Country Country { get; set; }
        public virtual JobType JobType { get; set; }
        public virtual Schools School { get; set; }
        public virtual Department1 Department { get; set; }
        public virtual Designation Designation { get; set; }
        public virtual JobOperationStatus StatusNavigation { get; set; }
        public virtual ICollection<AvailableJobTag> AvailableJobTags { get; set; }
        public virtual ICollection<JobApplication> JobApplications { get; set; }

        public virtual ICollection<AvailableJobCriteriaMap> AvailableJobCriteriaMaps { get; set; }
        public virtual ICollection<AvailableJobSkillMap> AvailableJobSkillMaps { get; set; }
        public virtual ICollection<JobInterview> JobInterviews { get; set; }
    }
}

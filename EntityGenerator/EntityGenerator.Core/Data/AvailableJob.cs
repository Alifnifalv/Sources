using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AvailableJobs", Schema = "hr")]
    public partial class AvailableJob
    {
        public AvailableJob()
        {
            AvailableJobCriteriaMaps = new HashSet<AvailableJobCriteriaMap>();
            AvailableJobSkillMaps = new HashSet<AvailableJobSkillMap>();
            AvailableJobTags = new HashSet<AvailableJobTag>();
            JobApplications = new HashSet<JobApplication>();
            JobInterviews = new HashSet<JobInterview>();
        }

        [Key]
        public long JobIID { get; set; }
        [StringLength(100)]
        public string JobTitle { get; set; }
        [StringLength(50)]
        public string TypeOfJob { get; set; }
        [StringLength(1000)]
        public string JobDescription { get; set; }
        public string JobDetails { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string Status { get; set; }
        public Guid? Id { get; set; }
        public Guid? PageId { get; set; }
        public long? DepartmentId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public byte? SchoolID { get; set; }
        public int? JobTypeID { get; set; }
        public byte? StatusID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(250)]
        public string MonthlySalary { get; set; }
        public int? TotalYearOfExperience { get; set; }
        public int? CountryID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PublishDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ClosingDate { get; set; }
        public int? DesignationID { get; set; }
        public int? NoOfVacancies { get; set; }
        public long? JDReferenceID { get; set; }

        [ForeignKey("CountryID")]
        [InverseProperty("AvailableJobs")]
        public virtual Country Country { get; set; }
        [ForeignKey("DepartmentId")]
        [InverseProperty("AvailableJobs")]
        public virtual Department1 Department { get; set; }
        [ForeignKey("DesignationID")]
        [InverseProperty("AvailableJobs")]
        public virtual Designation Designation { get; set; }
        [ForeignKey("JobTypeID")]
        [InverseProperty("AvailableJobs")]
        public virtual JobType JobType { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("AvailableJobs")]
        public virtual School School { get; set; }
        [ForeignKey("StatusID")]
        [InverseProperty("AvailableJobs")]
        public virtual JobOperationStatus StatusNavigation { get; set; }
        [InverseProperty("Job")]
        public virtual ICollection<AvailableJobCriteriaMap> AvailableJobCriteriaMaps { get; set; }
        [InverseProperty("Job")]
        public virtual ICollection<AvailableJobSkillMap> AvailableJobSkillMaps { get; set; }
        [InverseProperty("Job")]
        public virtual ICollection<AvailableJobTag> AvailableJobTags { get; set; }
        [InverseProperty("Job")]
        public virtual ICollection<JobApplication> JobApplications { get; set; }
        [InverseProperty("Job")]
        public virtual ICollection<JobInterview> JobInterviews { get; set; }
    }
}

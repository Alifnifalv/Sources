using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("JobInterviews", Schema = "hr")]
    public partial class JobInterview
    {
        public JobInterview()
        {
            JobInterviewMaps = new HashSet<JobInterviewMap>();
            JobInterviewRoundMaps = new HashSet<JobInterviewRoundMap>();
        }

        [Key]
        public long InterviewID { get; set; }
        public long? JobID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        public long? InterviewerID { get; set; }
        public string MeetingLink { get; set; }
        public int? Duration { get; set; }
        public int? NoOfRounds { get; set; }
        public string Remarks { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public bool? IsActive { get; set; }

        [ForeignKey("InterviewerID")]
        [InverseProperty("JobInterviews")]
        public virtual Employee Interviewer { get; set; }
        [ForeignKey("JobID")]
        [InverseProperty("JobInterviews")]
        public virtual AvailableJob Job { get; set; }
        [InverseProperty("Interview")]
        public virtual ICollection<JobInterviewMap> JobInterviewMaps { get; set; }
        [InverseProperty("Interview")]
        public virtual ICollection<JobInterviewRoundMap> JobInterviewRoundMaps { get; set; }
    }
}

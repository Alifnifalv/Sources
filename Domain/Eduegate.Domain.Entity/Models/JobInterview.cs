using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
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
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long? InterviewerID { get; set; }
        public string MeetingLink { get; set; }
        public int? Duration { get; set; }
        public int? NoOfRounds { get; set; }
        public string Remarks { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual Employee Interviewer { get; set; }
        public virtual AvailableJob Job { get; set; }
        public virtual ICollection<JobInterviewMap> JobInterviewMaps { get; set; }
        public virtual ICollection<JobInterviewRoundMap> JobInterviewRoundMaps { get; set; }
    }
}

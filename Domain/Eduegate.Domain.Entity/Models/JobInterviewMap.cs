using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("JobInterviewMap", Schema = "hr")]
    public partial class JobInterviewMap
    {
        [Key]
        public long MapID { get; set; }
        public long? InterviewID { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int? CompletedRoundID { get; set; }
        public bool? IsSelected { get; set; }
        public long? IsSelectedByLoginID { get; set; }
        public string Remarks { get; set; }
        public long? ApplicantID { get; set; }
        public bool? InterviewAccepted { get; set; }
        public bool? IsOfferLetterSent { get; set; }

        public virtual JobSeeker Applicant { get; set; }
        public virtual JobInterviewRound CompletedRound { get; set; }
        public virtual JobInterview Interview { get; set; }
    }
}

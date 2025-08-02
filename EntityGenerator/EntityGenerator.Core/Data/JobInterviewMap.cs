using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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
        public string Remarks { get; set; }
        public long? ApplicantID { get; set; }
        public bool? InterviewAccepted { get; set; }
        public long? IsSelectedByLoginID { get; set; }
        public long? OfferLetterContentID { get; set; }
        public bool? IsOfferLetterSent { get; set; }

        [ForeignKey("ApplicantID")]
        [InverseProperty("JobInterviewMaps")]
        public virtual JobSeeker Applicant { get; set; }
        [ForeignKey("CompletedRoundID")]
        [InverseProperty("JobInterviewMaps")]
        public virtual JobInterviewRound CompletedRound { get; set; }
        [ForeignKey("InterviewID")]
        [InverseProperty("JobInterviewMaps")]
        public virtual JobInterview Interview { get; set; }
    }
}

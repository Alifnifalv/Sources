using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class CandidateAnswersSearchView
    {
        public long CandidateAnswerIID { get; set; }
        public long? CandidateOnlineExamMapID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfAnswer { get; set; }
        public string Comments { get; set; }
        public long? QuestionOptionMapID { get; set; }
        public string OtherDetails { get; set; }
        public long? CandidateID { get; set; }
        public string OtherAnswers { get; set; }
        public long CandidateIID { get; set; }
        [StringLength(50)]
        public string CandidateName { get; set; }
        [StringLength(50)]
        public string CandidateEmail { get; set; }
        public long? StudentID { get; set; }
        [StringLength(555)]
        public string StudentName { get; set; }
        public long? OnlineExamID { get; set; }
        [StringLength(500)]
        public string ExamName { get; set; }
    }
}

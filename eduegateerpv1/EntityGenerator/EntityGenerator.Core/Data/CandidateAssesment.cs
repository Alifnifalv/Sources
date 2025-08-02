using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CandidateAssesments", Schema = "exam")]
    public partial class CandidateAssesment
    {
        [Key]
        public long CandidateAssesmentIID { get; set; }
        public long? CandidateOnlinExamMapID { get; set; }
        public long? SelectedQuestionOptionMapID { get; set; }
        public long? AnswerQuestionOptionMapID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("AnswerQuestionOptionMapID")]
        [InverseProperty("CandidateAssesmentAnswerQuestionOptionMaps")]
        public virtual QuestionOptionMap AnswerQuestionOptionMap { get; set; }
        [ForeignKey("CandidateOnlinExamMapID")]
        [InverseProperty("CandidateAssesments")]
        public virtual CandidateOnlineExamMap CandidateOnlinExamMap { get; set; }
        [ForeignKey("SelectedQuestionOptionMapID")]
        [InverseProperty("CandidateAssesmentSelectedQuestionOptionMaps")]
        public virtual QuestionOptionMap SelectedQuestionOptionMap { get; set; }
    }
}

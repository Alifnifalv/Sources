namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("exam.CandidateAssesments")]
    public partial class CandidateAssesment
    {
        [Key]
        public long CandidateAssesmentIID { get; set; }

        public long? CandidateOnlinExamMapID { get; set; }

        public long? SelectedQuestionOptionMapID { get; set; }

        public long? AnswerQuestionOptionMapID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual CandidateOnlineExamMap CandidateOnlineExamMap { get; set; }

        public virtual QuestionOptionMap QuestionOptionMap { get; set; }

        public virtual QuestionOptionMap QuestionOptionMap1 { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ExamQuestionGroupMaps", Schema = "exam")]
    public partial class ExamQuestionGroupMap
    {
        [Key]
        public long ExamQuestionGroupMapIID { get; set; }
        public long? OnlineExamID { get; set; }
        public int? QuestionGroupID { get; set; }
        public long? NumberOfQuestions { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? MaximumMarks { get; set; }

        [ForeignKey("OnlineExamID")]
        [InverseProperty("ExamQuestionGroupMaps")]
        public virtual OnlineExam OnlineExam { get; set; }
        [ForeignKey("QuestionGroupID")]
        [InverseProperty("ExamQuestionGroupMaps")]
        public virtual QuestionGroup QuestionGroup { get; set; }
    }
}

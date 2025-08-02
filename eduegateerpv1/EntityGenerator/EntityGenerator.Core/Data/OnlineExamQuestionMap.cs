using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("OnlineExamQuestionMaps", Schema = "exam")]
    public partial class OnlineExamQuestionMap
    {
        [Key]
        public long OnlineExamQuestionMapIID { get; set; }
        public long? OnlineExamID { get; set; }
        public int? QuestionGroupID { get; set; }
        public long? QuestionID { get; set; }
        public long? NoOfQuestionsLimit { get; set; }
        public int? AcademicYearID { get; set; }
        public byte? SchoolID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("OnlineExamQuestionMaps")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("OnlineExamID")]
        [InverseProperty("OnlineExamQuestionMaps")]
        public virtual OnlineExam OnlineExam { get; set; }
        [ForeignKey("QuestionID")]
        [InverseProperty("OnlineExamQuestionMaps")]
        public virtual Question Question { get; set; }
        [ForeignKey("QuestionGroupID")]
        [InverseProperty("OnlineExamQuestionMaps")]
        public virtual QuestionGroup QuestionGroup { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("OnlineExamQuestionMaps")]
        public virtual School School { get; set; }
    }
}

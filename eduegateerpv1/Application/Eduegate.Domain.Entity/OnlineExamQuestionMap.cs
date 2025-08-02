namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("exam.OnlineExamQuestionMaps")]
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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual OnlineExam OnlineExam { get; set; }

        public virtual Question Question { get; set; }

        public virtual QuestionGroup QuestionGroup { get; set; }

        public virtual School School { get; set; }
    }
}

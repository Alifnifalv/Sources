using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("QuestionGroups", Schema = "exam")]
    public partial class QuestionGroup
    {
        public QuestionGroup()
        {
            ExamQuestionGroupMaps = new HashSet<ExamQuestionGroupMap>();
            OnlineExamQuestionMaps = new HashSet<OnlineExamQuestionMap>();
            Questions = new HashSet<Question>();
        }

        [Key]
        public int QuestionGroupID { get; set; }
        [StringLength(50)]
        public string GroupName { get; set; }
        public int? SubjectID { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("SubjectID")]
        [InverseProperty("QuestionGroups")]
        public virtual Subject Subject { get; set; }
        [InverseProperty("QuestionGroup")]
        public virtual ICollection<ExamQuestionGroupMap> ExamQuestionGroupMaps { get; set; }
        [InverseProperty("QuestionGroup")]
        public virtual ICollection<OnlineExamQuestionMap> OnlineExamQuestionMaps { get; set; }
        [InverseProperty("QuestionGroup")]
        public virtual ICollection<Question> Questions { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("EntranceExam_English_Class_5", Schema = "exam")]
    public partial class EntranceExam_English_Class_5
    {
        [StringLength(50)]
        public string Exam_name { get; set; }
        [StringLength(50)]
        public string Class { get; set; }
        [StringLength(50)]
        public string Subject { get; set; }
        [Unicode(false)]
        public string Questions { get; set; }
        [StringLength(1)]
        public string Mark { get; set; }
        [StringLength(100)]
        public string Answers { get; set; }
        public bool? Answer_key { get; set; }
        public int? QuestionID { get; set; }
        public byte? PassageQuestionID { get; set; }
        public int RowIndex { get; set; }
    }
}

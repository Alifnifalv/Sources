using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("EntranceExam_Class_6_English_Questions", Schema = "exam")]
    public partial class EntranceExam_Class_6_English_Questions
    {
        [StringLength(50)]
        public string Exam_name { get; set; }
        [StringLength(50)]
        public string Class { get; set; }
        [StringLength(50)]
        public string Subject { get; set; }
        public string Questions { get; set; }
        [StringLength(1)]
        public string Mark { get; set; }
        [StringLength(1)]
        public string Time_allotted_Minutes { get; set; }
        public string Answers { get; set; }
        public bool? Answer_key { get; set; }
        public byte? QuestionID { get; set; }
        public byte? PassageQuestionID { get; set; }
    }
}

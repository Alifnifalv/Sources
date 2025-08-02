using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Class_6_English_Questions_20240813
    {
        [Column("Exam name")]
        [StringLength(2000)]
        public string Exam_name { get; set; }
        [StringLength(2000)]
        public string Class { get; set; }
        [StringLength(2000)]
        public string Subject { get; set; }
        [StringLength(2000)]
        public string Questions { get; set; }
        [StringLength(2000)]
        public string Mark { get; set; }
        [Column("Time allotted (Minutes)")]
        [StringLength(2000)]
        public string Time_allotted__Minutes_ { get; set; }
        [StringLength(2000)]
        public string Answers { get; set; }
        [Column("Answer key")]
        public bool Answer_key { get; set; }
        public double? QuestionID { get; set; }
        public double? PassageQuestionID { get; set; }
    }
}

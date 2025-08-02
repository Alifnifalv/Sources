using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Class_6_English_Questions_202408130318
    {
        [Column("Exam name")]
        [StringLength(255)]
        public string Exam_name { get; set; }
        [StringLength(255)]
        public string Class { get; set; }
        [StringLength(255)]
        public string Subject { get; set; }
        [StringLength(255)]
        public string Questions { get; set; }
        [StringLength(255)]
        public string Mark { get; set; }
        [Column("Time allotted (Minutes)")]
        [StringLength(255)]
        public string Time_allotted__Minutes_ { get; set; }
        [StringLength(255)]
        public string Answers { get; set; }
        public bool? Answer_key { get; set; }
        [StringLength(255)]
        public string QuestionID { get; set; }
        [StringLength(255)]
        public string PassageQuestionID { get; set; }
        public int RowIndex { get; set; }
    }
}

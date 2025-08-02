using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("Class_8_English_Entrance_Exam", Schema = "exam")]
    public partial class Class_8_English_Entrance_Exam
    {
        [Required]
        [StringLength(50)]
        public string Exam_name { get; set; }
        [Required]
        [StringLength(50)]
        public string Class { get; set; }
        [Required]
        [StringLength(50)]
        public string Subject { get; set; }
        [Required]
        [StringLength(150)]
        public string Questions { get; set; }
        [StringLength(1)]
        public string Mark { get; set; }
        [StringLength(1)]
        public string Time_allotted_Minutes { get; set; }
        [Required]
        [StringLength(150)]
        public string Answers { get; set; }
        public bool Answer_key { get; set; }
        public int? QuestionID { get; set; }
        public byte? PassageQuestionID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("Entrance_Exam_English_Class_3", Schema = "exam")]
    public partial class Entrance_Exam_English_Class_3
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
        public byte? QuestionID { get; set; }
        [StringLength(50)]
        public string Answers { get; set; }
        public bool? Answer_key { get; set; }
        public int RowIndex { get; set; }
    }
}

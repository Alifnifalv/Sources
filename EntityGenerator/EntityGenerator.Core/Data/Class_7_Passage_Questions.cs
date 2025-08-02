using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("Class_7_Passage_Questions", Schema = "exam")]
    public partial class Class_7_Passage_Questions
    {
        [Required]
        [StringLength(1850)]
        public string PassageQuestions { get; set; }
        [Required]
        [StringLength(50)]
        public string Short_Desc { get; set; }
        public int? PassageQuestionID { get; set; }
    }
}

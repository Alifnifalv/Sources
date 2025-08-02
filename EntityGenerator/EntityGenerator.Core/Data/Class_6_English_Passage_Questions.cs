using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("Class_6_English_Passage_Questions", Schema = "exam")]
    public partial class Class_6_English_Passage_Questions
    {
        public byte PassageQuestionID { get; set; }
        [Required]
        [StringLength(1500)]
        public string Question { get; set; }
    }
}

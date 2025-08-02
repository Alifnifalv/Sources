using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("Class_8_English_Passage_Questions", Schema = "exam")]
    public partial class Class_8_English_Passage_Questions
    {
        public byte PassageQuestionID { get; set; }
        [Required]
        [StringLength(2950)]
        public string PassageQuestion { get; set; }
        [Required]
        [StringLength(50)]
        public string Short_desc { get; set; }
    }
}

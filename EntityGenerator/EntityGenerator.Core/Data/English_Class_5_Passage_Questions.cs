using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("English_Class_5_Passage_Questions", Schema = "exam")]
    public partial class English_Class_5_Passage_Questions
    {
        public byte? PassageQuestionID { get; set; }
        [Unicode(false)]
        public string Question { get; set; }
    }
}

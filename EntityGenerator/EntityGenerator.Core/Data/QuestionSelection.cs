using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("QuestionSelections", Schema = "exam")]
    public partial class QuestionSelection
    {
        public QuestionSelection()
        {
            OnlineExams = new HashSet<OnlineExam>();
        }

        [Key]
        public byte QuestionSelectionID { get; set; }
        [StringLength(50)]
        public string SelectName { get; set; }

        [InverseProperty("QuestionSelection")]
        public virtual ICollection<OnlineExam> OnlineExams { get; set; }
    }
}

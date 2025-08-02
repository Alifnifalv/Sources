using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.OnlineExam.Models
{
    [Table("PassageQuestions", Schema = "exam")]
    public partial class PassageQuestion
    {
        public PassageQuestion()
        {
            Questions = new HashSet<Question>();
        }

        [Key]
        public long PassageQuestionIID { get; set; }

        [Column("PassageQuestion")]
        public string PassageQuestion1 { get; set; }
        [StringLength(255)]

        public string ShortDescription { get; set; }

        public decimal? MinimumMark { get; set; }

        public decimal? MaximumMark { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
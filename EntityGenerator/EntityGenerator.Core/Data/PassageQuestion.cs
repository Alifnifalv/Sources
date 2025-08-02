using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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
        [Unicode(false)]
        public string PassageQuestion1 { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string ShortDescription { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? MinimumMark { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? MaximumMark { get; set; }

        [InverseProperty("PassageQuestion")]
        public virtual ICollection<Question> Questions { get; set; }
    }
}

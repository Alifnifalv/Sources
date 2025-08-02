using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AnswerTypes", Schema = "exam")]
    public partial class AnswerType
    {
        public AnswerType()
        {
            Questions = new HashSet<Question>();
        }

        [Key]
        public byte AnswerTypeID { get; set; }
        [StringLength(50)]
        public string TypeName { get; set; }

        [InverseProperty("AnswerType")]
        public virtual ICollection<Question> Questions { get; set; }
    }
}

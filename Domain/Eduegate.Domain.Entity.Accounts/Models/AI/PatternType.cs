using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Accounts.Models.AI
{
    [Table("PatternTypes", Schema = "ai")]
    public partial class PatternType
    {
        public PatternType()
        {
            Rules = new HashSet<Rule>();
        }

        [Key]
        public int PatternTypeID { get; set; }
        [Required]
        [StringLength(200)]
        [Unicode(false)]
        public string PatternTypeName { get; set; }

        [InverseProperty("PatternType")]
        public virtual ICollection<Rule> Rules { get; set; }
    }
}

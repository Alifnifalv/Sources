using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CreatedByTypes", Schema = "communities")]
    public partial class CreatedByType
    {
        public CreatedByType()
        {
            Members = new HashSet<Member>();
        }

        [Key]
        public byte CreatedByTypeID { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }

        [InverseProperty("CreatedByNavigation")]
        public virtual ICollection<Member> Members { get; set; }
    }
}

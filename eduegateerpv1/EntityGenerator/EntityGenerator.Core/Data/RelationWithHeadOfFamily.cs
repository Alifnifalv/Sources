using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("RelationWithHeadOfFamilies", Schema = "communities")]
    public partial class RelationWithHeadOfFamily
    {
        public RelationWithHeadOfFamily()
        {
            Members = new HashSet<Member>();
        }

        [Key]
        public byte RelationWithHeadOfFamilyID { get; set; }
        [StringLength(500)]
        public string RelationDescription { get; set; }

        [InverseProperty("RelationWithHeadOfFamily")]
        public virtual ICollection<Member> Members { get; set; }
    }
}

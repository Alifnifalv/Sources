using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("VisitingPurposes", Schema = "schools")]
    public partial class VisitingPurpos
    {
        public VisitingPurpos()
        {
            VisitorBooks = new HashSet<VisitorBook>();
        }

        [Key]
        public byte VisitingPurposeID { get; set; }
        [StringLength(100)]
        public string PurpuseDescription { get; set; }

        [InverseProperty("VisitingPurpose")]
        public virtual ICollection<VisitorBook> VisitorBooks { get; set; }
    }
}

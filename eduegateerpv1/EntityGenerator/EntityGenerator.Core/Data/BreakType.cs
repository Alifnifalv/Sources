using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("BreakTypes", Schema = "schools")]
    public partial class BreakType
    {
        public BreakType()
        {
            ClassTimings = new HashSet<ClassTiming>();
        }

        [Key]
        public byte BreakTypeID { get; set; }
        [StringLength(50)]
        public string BreakTypeName { get; set; }

        [InverseProperty("BreakType")]
        public virtual ICollection<ClassTiming> ClassTimings { get; set; }
    }
}

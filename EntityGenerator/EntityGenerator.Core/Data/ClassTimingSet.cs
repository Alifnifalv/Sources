using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ClassTimingSets", Schema = "schools")]
    public partial class ClassTimingSet
    {
        public ClassTimingSet()
        {
            ClassTimings = new HashSet<ClassTiming>();
            WeekDays = new HashSet<WeekDay>();
        }

        [Key]
        public int ClassTimingSetID { get; set; }
        [StringLength(50)]
        public string TimingSetName { get; set; }

        [InverseProperty("ClassTimingSet")]
        public virtual ICollection<ClassTiming> ClassTimings { get; set; }
        [InverseProperty("ClassTimingSet")]
        public virtual ICollection<WeekDay> WeekDays { get; set; }
    }
}

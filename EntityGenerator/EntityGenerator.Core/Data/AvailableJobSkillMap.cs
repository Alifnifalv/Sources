using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AvailableJobSkillMaps", Schema = "hr")]
    public partial class AvailableJobSkillMap
    {
        [Key]
        public long SkillID { get; set; }
        public long? JobID { get; set; }
        [StringLength(250)]
        public string Skill { get; set; }

        [ForeignKey("JobID")]
        [InverseProperty("AvailableJobSkillMaps")]
        public virtual AvailableJob Job { get; set; }
    }
}

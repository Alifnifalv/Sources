using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("JobSeekerSkillMaps", Schema = "hr")]
    public partial class JobSeekerSkillMap
    {
        [Key]
        public long SkillMapID { get; set; }
        public long? SeekerID { get; set; }
        [StringLength(250)]
        public string Description { get; set; }

        [ForeignKey("SeekerID")]
        [InverseProperty("JobSeekerSkillMaps")]
        public virtual JobSeeker Seeker { get; set; }
    }
}

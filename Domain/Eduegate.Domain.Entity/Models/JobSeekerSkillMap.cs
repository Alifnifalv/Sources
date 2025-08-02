using EntityGenerator.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("JobSeekerSkillMaps", Schema = "hr")]
    public partial class JobSeekerSkillMap
    {
        [Key]
        public long SkillMapID { get; set; }
        public long? SeekerID { get; set; }
        public string Description { get; set; }
        public virtual JobSeeker Seeker { get; set; }
    }
}

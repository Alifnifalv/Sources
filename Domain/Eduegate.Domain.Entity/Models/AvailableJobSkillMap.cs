using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Models
{
    [Table("AvailableJobSkillMaps", Schema = "hr")]
    public partial class AvailableJobSkillMap
    {
        [Key]
        public long SkillID { get; set; }
        public long? JobID { get; set; }
        public string Skill { get; set; }
        public virtual AvailableJob Job { get; set; }
    }
}

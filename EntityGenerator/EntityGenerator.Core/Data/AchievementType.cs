using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AchievementTypes", Schema = "schools")]
    public partial class AchievementType
    {
        public AchievementType()
        {
            StudentAchievements = new HashSet<StudentAchievement>();
        }

        [Key]
        public int AchievementTypeID { get; set; }
        [StringLength(250)]
        public string TypeName { get; set; }
        [StringLength(100)]
        public string TypeTitle { get; set; }

        [InverseProperty("Type")]
        public virtual ICollection<StudentAchievement> StudentAchievements { get; set; }
    }
}

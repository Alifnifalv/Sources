using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AchievementRankings", Schema = "schools")]
    public partial class AchievementRanking
    {
        public AchievementRanking()
        {
            StudentAchievements = new HashSet<StudentAchievement>();
        }

        [Key]
        public int AchievementRankingID { get; set; }
        [StringLength(250)]
        public string RankingDescription { get; set; }
        [StringLength(100)]
        public string RankingTitle { get; set; }

        [InverseProperty("Ranking")]
        public virtual ICollection<StudentAchievement> StudentAchievements { get; set; }
    }
}

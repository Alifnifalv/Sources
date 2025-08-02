using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AchievementCategories", Schema = "schools")]
    public partial class AchievementCategory
    {
        public AchievementCategory()
        {
            StudentAchievements = new HashSet<StudentAchievement>();
        }

        [Key]
        public int AchievementCategoryID { get; set; }
        [StringLength(250)]
        public string CategoryName { get; set; }
        [StringLength(100)]
        public string CategoryTitle { get; set; }

        [InverseProperty("Category")]
        public virtual ICollection<StudentAchievement> StudentAchievements { get; set; }
    }
}

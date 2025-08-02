namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.AchievementRankings")]
    public partial class AchievementRanking
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AchievementRanking()
        {
            StudentAchievements = new HashSet<StudentAchievement>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AchievementRankingID { get; set; }

        [StringLength(250)]
        public string RankingDescription { get; set; }

        [StringLength(100)]
        public string RankingTitle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentAchievement> StudentAchievements { get; set; }
    }
}

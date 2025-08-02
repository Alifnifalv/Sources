using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("AchievementRankings", Schema = "schools")]
    public partial class AchievementRanking
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AchievementRanking()
        {
            StudentAchievements = new HashSet<StudentAchievement>();
        }
        [Key]

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
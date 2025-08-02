namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.AchievementCategories")]
    public partial class AchievementCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AchievementCategory()
        {
            StudentAchievements = new HashSet<StudentAchievement>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AchievementCategoryID { get; set; }

        [StringLength(250)]
        public string CategoryName { get; set; }

        [StringLength(100)]
        public string CategoryTitle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentAchievement> StudentAchievements { get; set; }
    }
}

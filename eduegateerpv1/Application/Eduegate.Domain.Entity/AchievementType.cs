namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.AchievementTypes")]
    public partial class AchievementType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AchievementType()
        {
            StudentAchievements = new HashSet<StudentAchievement>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AchievementTypeID { get; set; }

        [StringLength(250)]
        public string TypeName { get; set; }

        [StringLength(100)]
        public string TypeTitle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentAchievement> StudentAchievements { get; set; }
    }
}

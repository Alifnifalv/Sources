namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.StudentAchievements")]
    public partial class StudentAchievement
    {
        [Key]
        public long StudentAchievementIID { get; set; }

        public long? StudentID { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public int? CategoryID { get; set; }

        public int? TypeID { get; set; }

        public int? RankingID { get; set; }

        public string AchievementDescription { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual AchievementCategory AchievementCategory { get; set; }

        public virtual AchievementRanking AchievementRanking { get; set; }

        public virtual AchievementType AchievementType { get; set; }

        public virtual School School { get; set; }

        public virtual Student Student { get; set; }
    }
}

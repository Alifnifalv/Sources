using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentAchievements", Schema = "schools")]
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
        public string Venue { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public string Title { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HeldOnDate { get; set; }
        [Unicode(false)]
        public string AchievementDescription { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("StudentAchievements")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("CategoryID")]
        [InverseProperty("StudentAchievements")]
        public virtual AchievementCategory Category { get; set; }
        [ForeignKey("RankingID")]
        [InverseProperty("StudentAchievements")]
        public virtual AchievementRanking Ranking { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("StudentAchievements")]
        public virtual School School { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("StudentAchievements")]
        public virtual Student Student { get; set; }
        [ForeignKey("TypeID")]
        [InverseProperty("StudentAchievements")]
        public virtual AchievementType Type { get; set; }
    }
}

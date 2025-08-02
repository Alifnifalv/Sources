using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentAchievementSeachView
    {
        public long StudentAchievementIID { get; set; }
        public long? StudentID { get; set; }
        [StringLength(555)]
        public string StudentName { get; set; }
        public byte? SchoolID { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(123)]
        public string AcademicYear { get; set; }
        public int? CategoryID { get; set; }
        [StringLength(250)]
        public string CategoryName { get; set; }
        public int? TypeID { get; set; }
        [StringLength(250)]
        public string TypeName { get; set; }
        public int? RankingID { get; set; }
        [StringLength(250)]
        public string RankingDescription { get; set; }
        public string AchievementDescription { get; set; }
        public int? SectionID { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        public int? ClassID { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        public int? CreatedBy { get; set; }
        [StringLength(100)]
        public string CreatedUserName { get; set; }
        public int? UpdatedBy { get; set; }
        [StringLength(100)]
        public string UpdatedUserName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
    }
}

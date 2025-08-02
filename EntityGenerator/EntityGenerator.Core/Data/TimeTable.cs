using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TimeTables", Schema = "schools")]
    [Index("TimeTableID", "AcademicYearID", "IsActive", Name = "IX_TimeTables", IsUnique = true)]
    public partial class TimeTable
    {
        public TimeTable()
        {
            TimeTableAllocations = new HashSet<TimeTableAllocation>();
            TimeTableExtensions = new HashSet<TimeTableExtension>();
            TimeTableLogs = new HashSet<TimeTableLog>();
        }

        [Key]
        public int TimeTableID { get; set; }
        [StringLength(100)]
        public string TimeTableDescription { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? AcademicYearID { get; set; }
        /// <summary>
        /// 1-Active, 2-Inactive
        /// </summary>
        public bool? IsActive { get; set; }
        public byte? SchoolID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("TimeTables")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("TimeTables")]
        public virtual School School { get; set; }
        [InverseProperty("TimeTable")]
        public virtual ICollection<TimeTableAllocation> TimeTableAllocations { get; set; }
        [InverseProperty("TimeTable")]
        public virtual ICollection<TimeTableExtension> TimeTableExtensions { get; set; }
        [InverseProperty("TimeTable")]
        public virtual ICollection<TimeTableLog> TimeTableLogs { get; set; }
    }
}

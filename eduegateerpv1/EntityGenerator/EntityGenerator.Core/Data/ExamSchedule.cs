using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ExamSchedules", Schema = "schools")]
    public partial class ExamSchedule
    {
        public ExamSchedule()
        {
            ExamClassMaps = new HashSet<ExamClassMap>();
        }

        [Key]
        public long ExamScheduleIID { get; set; }
        public long? ExamID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Date { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExamStartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExamEndDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        [StringLength(50)]
        public string Room { get; set; }
        public double? FullMarks { get; set; }
        public double? PassingMarks { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("ExamID")]
        [InverseProperty("ExamSchedules")]
        public virtual Exam Exam { get; set; }
        [InverseProperty("ExamSchedule")]
        public virtual ICollection<ExamClassMap> ExamClassMaps { get; set; }
    }
}

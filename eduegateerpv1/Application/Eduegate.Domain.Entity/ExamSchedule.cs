namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.ExamSchedules")]
    public partial class ExamSchedule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ExamSchedule()
        {
            ExamClassMaps = new HashSet<ExamClassMap>();
        }

        [Key]
        public long ExamScheduleIID { get; set; }

        public long? ExamID { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? ExamStartDate { get; set; }

        public DateTime? ExamEndDate { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        [StringLength(50)]
        public string Room { get; set; }

        public double? FullMarks { get; set; }

        public double? PassingMarks { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamClassMap> ExamClassMaps { get; set; }

        public virtual Exam Exam { get; set; }
    }
}

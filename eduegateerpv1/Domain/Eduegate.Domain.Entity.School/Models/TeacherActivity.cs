namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TeacherActivities", Schema = "schools")]
    public partial class TeacherActivity
    {
        [Key]
        public long TeacherActivityIID { get; set; }

        public long? EmployeeID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ActivityDate { get; set; }

        public TimeSpan? TimeFrom { get; set; }

        public TimeSpan? TimeTo { get; set; }

        public int? SubjectID { get; set; }

        public int? ClassID { get; set; }

        public int? SectionID { get; set; }

        public byte? ShiftID { get; set; }

        public long? TopicID { get; set; }

        public long? SubTopicID { get; set; }

        public byte? PeriodID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Class Class { get; set; }

        public virtual Section Section { get; set; }

        public virtual Shift Shift { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual SubjectTopic SubjectTopic { get; set; }

        public virtual SubjectTopic SubjectTopic1 { get; set; }
    }
}

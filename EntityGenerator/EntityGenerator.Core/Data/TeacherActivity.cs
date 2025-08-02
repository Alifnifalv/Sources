using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("ClassID")]
        [InverseProperty("TeacherActivities")]
        public virtual Class Class { get; set; }
        [ForeignKey("EmployeeID")]
        [InverseProperty("TeacherActivities")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("TeacherActivities")]
        public virtual Section Section { get; set; }
        [ForeignKey("ShiftID")]
        [InverseProperty("TeacherActivities")]
        public virtual Shift Shift { get; set; }
        [ForeignKey("SubTopicID")]
        [InverseProperty("TeacherActivitySubTopics")]
        public virtual SubjectTopic SubTopic { get; set; }
        [ForeignKey("SubjectID")]
        [InverseProperty("TeacherActivities")]
        public virtual Subject Subject { get; set; }
        [ForeignKey("TopicID")]
        [InverseProperty("TeacherActivityTopics")]
        public virtual SubjectTopic Topic { get; set; }
    }
}

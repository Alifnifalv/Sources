using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SubjectTopics", Schema = "schools")]
    public partial class SubjectTopic
    {
        public SubjectTopic()
        {
            InverseParentTopic = new HashSet<SubjectTopic>();
            TeacherActivitySubTopics = new HashSet<TeacherActivity>();
            TeacherActivityTopics = new HashSet<TeacherActivity>();
        }

        [Key]
        public long SubjectTopicIID { get; set; }
        [StringLength(100)]
        public string TopicName { get; set; }
        [Column(TypeName = "decimal(12, 3)")]
        public decimal? Duration { get; set; }
        public int? SubjectID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public long? EmployeeID { get; set; }
        public long? ParentTopicID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("ClassID")]
        [InverseProperty("SubjectTopics")]
        public virtual Class Class { get; set; }
        [ForeignKey("EmployeeID")]
        [InverseProperty("SubjectTopics")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("ParentTopicID")]
        [InverseProperty("InverseParentTopic")]
        public virtual SubjectTopic ParentTopic { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("SubjectTopics")]
        public virtual Section Section { get; set; }
        [ForeignKey("SubjectID")]
        [InverseProperty("SubjectTopics")]
        public virtual Subject Subject { get; set; }
        [InverseProperty("ParentTopic")]
        public virtual ICollection<SubjectTopic> InverseParentTopic { get; set; }
        [InverseProperty("SubTopic")]
        public virtual ICollection<TeacherActivity> TeacherActivitySubTopics { get; set; }
        [InverseProperty("Topic")]
        public virtual ICollection<TeacherActivity> TeacherActivityTopics { get; set; }
    }
}

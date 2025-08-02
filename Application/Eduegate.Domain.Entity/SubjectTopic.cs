namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.SubjectTopics")]
    public partial class SubjectTopic
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SubjectTopic()
        {
            SubjectTopics1 = new HashSet<SubjectTopic>();
            TeacherActivities = new HashSet<TeacherActivity>();
            TeacherActivities1 = new HashSet<TeacherActivity>();
        }

        [Key]
        public long SubjectTopicIID { get; set; }

        [StringLength(100)]
        public string TopicName { get; set; }

        public decimal? Duration { get; set; }

        public int? SubjectID { get; set; }

        public int? ClassID { get; set; }

        public int? SectionID { get; set; }

        public long? EmployeeID { get; set; }

        public long? ParentTopicID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Class Class { get; set; }

        public virtual Section Section { get; set; }

        public virtual Subject Subject { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubjectTopic> SubjectTopics1 { get; set; }

        public virtual SubjectTopic SubjectTopic1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TeacherActivity> TeacherActivities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TeacherActivity> TeacherActivities1 { get; set; }
    }
}

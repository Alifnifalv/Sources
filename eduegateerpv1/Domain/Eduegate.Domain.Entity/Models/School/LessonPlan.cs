namespace Eduegate.Domain.Entity.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("LessonPlans", Schema = "schools")]
    public partial class LessonPlan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LessonPlan()
        {
           // LessonPlanTopicMaps = new HashSet<LessonPlanTopicMap>();
        }

        [Key]
        public long LessonPlanIID { get; set; }

        public int? ClassID { get; set; }

        public int? SectionID { get; set; }

        public int? SubjectID { get; set; }

        public decimal? TotalHours { get; set; }

        public DateTime? Date1 { get; set; }

        public DateTime? Date2 { get; set; }

        public DateTime? Date3 { get; set; }

        public byte? LessonPlanStatusID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        [StringLength(20)]
        public string LessonPlanCode { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        //public virtual AcademicYear AcademicYear { get; set; }

        //public virtual Class Class { get; set; }

        //public virtual LessonPlanStatus LessonPlanStatus { get; set; }

        //public virtual School School { get; set; }

        //public virtual Section Section { get; set; }

        //public virtual Subject Subject { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<LessonPlanTopicMap> LessonPlanTopicMaps { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("Agendas", Schema = "schools")]
    public partial class Agenda
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Agenda()
        {
            AgendaAttachmentMaps = new HashSet<AgendaAttachmentMap>();
            AgendaTaskMaps = new HashSet<AgendaTaskMap>();
            AgendaSectionMaps = new HashSet<AgendaSectionMap>();
            AgendaTopicMaps = new HashSet<AgendaTopicMap>();
        }

        [Key]
        public long AgendaIID { get; set; }

        public int? ClassID { get; set; }

        public int? SectionID { get; set; }

        public int? SubjectID { get; set; }

        public decimal? TotalHours { get; set; }

        public DateTime? Date1 { get; set; }

        public DateTime? Date2 { get; set; }

        public DateTime? Date3 { get; set; }

        public byte? AgendaStatusID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        [StringLength(20)]
        public string AgendaCode { get; set; }

        [StringLength(500)]
        public string LearningExperiences { get; set; }

        public byte? NumberOfPeriods { get; set; }

        public byte? NumberOfClassTests { get; set; }

        public byte? NumberOfActivityCompleted { get; set; }

        [StringLength(500)]
        public string Activity { get; set; }

        [StringLength(500)]
        public string HomeWorks { get; set; }

        public byte? TeachingAidID { get; set; }

        public byte? MonthID { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        public string Description { get; set; }

        public int? ReferenceID { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgendaAttachmentMap> AgendaAttachmentMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgendaTaskMap> AgendaTaskMaps { get; set; }

        public virtual AgendaStatus AgendaStatus { get; set; }

        public virtual Class Class { get; set; }

        public virtual Schools School { get; set; }

        public virtual Section Section { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual TeachingAid TeachingAid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgendaSectionMap> AgendaSectionMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgendaTopicMap> AgendaTopicMaps { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Agendas", Schema = "schools")]
    [Index("ClassID", "AgendaStatusID", "AcademicYearID", Name = "IDX_Agendas_ClassID__AgendaStatusID__AcademicYearID_SectionID__SubjectID__TotalHours__Date1__Date2_")]
    [Index("SchoolID", "AcademicYearID", Name = "IDX_Agendas_SchoolID__AcademicYearID_ClassID__SubjectID__AgendaStatusID")]
    [Index("SchoolID", "AcademicYearID", Name = "IDX_Agendas_SchoolID__AcademicYearID_ClassID__SubjectID__AgendaStatusID__CreatedBy__UpdatedBy__Crea")]
    public partial class Agenda
    {
        public Agenda()
        {
            AgendaAttachmentMaps = new HashSet<AgendaAttachmentMap>();
            AgendaSectionMaps = new HashSet<AgendaSectionMap>();
            AgendaTaskMaps = new HashSet<AgendaTaskMap>();
            AgendaTopicMaps = new HashSet<AgendaTopicMap>();
        }

        [Key]
        public long AgendaIID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public int? SubjectID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TotalHours { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Date1 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Date2 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Date3 { get; set; }
        public byte? AgendaStatusID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
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

        [ForeignKey("AcademicYearID")]
        [InverseProperty("Agenda")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("AgendaStatusID")]
        [InverseProperty("Agenda")]
        public virtual AgendaStatus AgendaStatus { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("Agenda")]
        public virtual Class Class { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("Agenda")]
        public virtual School School { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("Agenda")]
        public virtual Section Section { get; set; }
        [ForeignKey("SubjectID")]
        [InverseProperty("Agenda")]
        public virtual Subject Subject { get; set; }
        [ForeignKey("TeachingAidID")]
        [InverseProperty("Agenda")]
        public virtual TeachingAid TeachingAid { get; set; }
        [InverseProperty("Agenda")]
        public virtual ICollection<AgendaAttachmentMap> AgendaAttachmentMaps { get; set; }
        [InverseProperty("Agenda")]
        public virtual ICollection<AgendaSectionMap> AgendaSectionMaps { get; set; }
        [InverseProperty("Agenda")]
        public virtual ICollection<AgendaTaskMap> AgendaTaskMaps { get; set; }
        [InverseProperty("Agenda")]
        public virtual ICollection<AgendaTopicMap> AgendaTopicMaps { get; set; }
    }
}

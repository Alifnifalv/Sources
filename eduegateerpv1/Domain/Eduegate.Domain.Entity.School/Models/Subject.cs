namespace  Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Subjects", Schema = "schools")]
    public partial class Subject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Subject()
        {
            //Questions = new HashSet<Question>();
            //Questions1 = new HashSet<Question>();
            //AcademicNotes = new HashSet<AcademicNote>();
            Assignments = new HashSet<Assignment>();
            ClassSubjectMaps = new HashSet<ClassSubjectMap>();
            ClassSubjectSkillGroupMaps = new HashSet<ClassSubjectSkillGroupMap>();
            ExamSubjectMaps = new HashSet<ExamSubjectMap>();
            LessonPlans = new HashSet<LessonPlan>();
            MarkRegisterSubjectMaps = new HashSet<MarkRegisterSubjectMap>();
            RemarksEntryExamMaps = new HashSet<RemarksEntryExamMap>();
            StudentSkillRegisters = new HashSet<StudentSkillRegister>();
            SubjectTeacherMaps = new HashSet<SubjectTeacherMap>();
            SubjectTopics = new HashSet<SubjectTopic>();
            TeacherActivities = new HashSet<TeacherActivity>();
            TimeTableAllocations = new HashSet<TimeTableAllocation>();
            TimeTableLogs = new HashSet<TimeTableLog>();
            Students = new HashSet<Student>();
            Students1 = new HashSet<Student>();
            Students2 = new HashSet<Student>();
            ClassGroups = new HashSet<ClassGroup>();
            Agendas = new HashSet<Agenda>();
            StudentApplications = new HashSet<StudentApplication>();
            StudentApplications1 = new HashSet<StudentApplication>();
            StreamOptionalSubjectMaps = new HashSet<StreamOptionalSubjectMap>();
            StudentStreamOptionalSubjectMaps = new HashSet<StudentStreamOptionalSubjectMap>();
            StudentApplicationOptionalSubjectMaps = new HashSet<StudentApplicationOptionalSubjectMap>();
            MarkRegisterSkillGroups = new HashSet<MarkRegisterSkillGroup>();
            ClassSectionSubjectPeriodMaps = new HashSet<ClassSectionSubjectPeriodMap>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SubjectID { get; set; }

        public byte? SubjectTypeID { get; set; }

        [StringLength(500)]
        public string SubjectName { get; set; }

        [StringLength(20)]
        public string HexCodeUpper { get; set; }

        [StringLength(20)]
        public string HexCodeLower { get; set; }

        public bool? IsLanguage { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        [StringLength(500)]
        public string SubjectText { get; set; }

        public bool? IsActive { get; set; }

        public string ProgressReportHeader { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Question> Questions { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Question> Questions1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<AcademicNote> AcademicNotes { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Assignment> Assignments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClassSubjectMap> ClassSubjectMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClassSubjectSkillGroupMap> ClassSubjectSkillGroupMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamSubjectMap> ExamSubjectMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LessonPlan> LessonPlans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MarkRegisterSubjectMap> MarkRegisterSubjectMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RemarksEntryExamMap> RemarksEntryExamMaps { get; set; }

        public virtual Schools School { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentSkillRegister> StudentSkillRegisters { get; set; }

        public virtual SubjectType SubjectType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubjectTeacherMap> SubjectTeacherMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubjectTopic> SubjectTopics { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TeacherActivity> TeacherActivities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeTableAllocation> TimeTableAllocations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeTableLog> TimeTableLogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Students { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Students1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Students2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClassGroup> ClassGroups { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Agenda> Agendas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentApplication> StudentApplications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentApplication> StudentApplications1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StreamSubjectMap> StreamSubjectMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentStreamOptionalSubjectMap> StudentStreamOptionalSubjectMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StreamOptionalSubjectMap> StreamOptionalSubjectMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentApplicationOptionalSubjectMap> StudentApplicationOptionalSubjectMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MarkRegisterSkillGroup> MarkRegisterSkillGroups { get; set; }

        public virtual ICollection<ClassSectionSubjectPeriodMap> ClassSectionSubjectPeriodMaps { get; set; }
    }
}

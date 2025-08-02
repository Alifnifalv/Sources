using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Subjects", Schema = "schools")]
    public partial class Subject
    {
        public Subject()
        {
            AcademicNotes = new HashSet<AcademicNote>();
            Agenda = new HashSet<Agenda>();
            Assignments = new HashSet<Assignment>();
            Chapters = new HashSet<Chapter>();
            ClassClassTeacherMaps = new HashSet<ClassClassTeacherMap>();
            ClassGroups = new HashSet<ClassGroup>();
            ClassSectionSubjectPeriodMaps = new HashSet<ClassSectionSubjectPeriodMap>();
            ClassSubjectMaps = new HashSet<ClassSubjectMap>();
            ClassSubjectSkillGroupMaps = new HashSet<ClassSubjectSkillGroupMap>();
            ClassSubjectWorkflowEntityMaps = new HashSet<ClassSubjectWorkflowEntityMap>();
            ClassTeacherMaps = new HashSet<ClassTeacherMap>();
            ExamSubjectMaps = new HashSet<ExamSubjectMap>();
            LessonPlans = new HashSet<LessonPlan>();
            MarkRegisterSkillGroups = new HashSet<MarkRegisterSkillGroup>();
            MarkRegisterSubjectMaps = new HashSet<MarkRegisterSubjectMap>();
            OnlineExamResultSubjectMaps = new HashSet<OnlineExamResultSubjectMap>();
            OnlineExamSubjectMaps = new HashSet<OnlineExamSubjectMap>();
            QualificationCoreSubjectMaps = new HashSet<QualificationCoreSubjectMap>();
            QuestionGroups = new HashSet<QuestionGroup>();
            Questions = new HashSet<Question>();
            RemarksEntryExamMaps = new HashSet<RemarksEntryExamMap>();
            SkillGroupSubjectMaps = new HashSet<SkillGroupSubjectMap>();
            StreamOptionalSubjectMaps = new HashSet<StreamOptionalSubjectMap>();
            StreamSubjectMaps = new HashSet<StreamSubjectMap>();
            StudentApplicationOptionalSubjectMaps = new HashSet<StudentApplicationOptionalSubjectMap>();
            StudentApplicationSecondLangs = new HashSet<StudentApplication>();
            StudentApplicationThirdLangs = new HashSet<StudentApplication>();
            StudentSecondLangs = new HashSet<Student>();
            StudentSkillRegisters = new HashSet<StudentSkillRegister>();
            StudentStreamOptionalSubjectMaps = new HashSet<StudentStreamOptionalSubjectMap>();
            StudentSubjectMaps = new HashSet<Student>();
            StudentThirdLangs = new HashSet<Student>();
            SubjectGroupSubjectMaps = new HashSet<SubjectGroupSubjectMap>();
            SubjectInchargerClassMaps = new HashSet<SubjectInchargerClassMap>();
            SubjectTeacherMaps = new HashSet<SubjectTeacherMap>();
            SubjectTopics = new HashSet<SubjectTopic>();
            SubjectUnits = new HashSet<SubjectUnit>();
            TeacherActivities = new HashSet<TeacherActivity>();
            TimeTableAllocations = new HashSet<TimeTableAllocation>();
            TimeTableExtSubjects = new HashSet<TimeTableExtSubject>();
            TimeTableExtTeachers = new HashSet<TimeTableExtTeacher>();
            TimeTableLogs = new HashSet<TimeTableLog>();
        }

        [Key]
        public int SubjectID { get; set; }
        public byte? SubjectTypeID { get; set; }
        [StringLength(500)]
        public string SubjectName { get; set; }
        [StringLength(20)]
        public string HexCodeUpper { get; set; }
        [StringLength(20)]
        public string HexCodeLower { get; set; }
        public bool? IsLanguage { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(50)]
        public string SubjectCode { get; set; }
        [StringLength(500)]
        public string SubjectText { get; set; }
        public bool? IsActive { get; set; }
        [StringLength(50)]
        public string ProgressReportHeader { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string IconFileName { get; set; }
        public int? ReportSortOrder { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("Subjects")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("Subjects")]
        public virtual School School { get; set; }
        [ForeignKey("SubjectTypeID")]
        [InverseProperty("Subjects")]
        public virtual SubjectType SubjectType { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<AcademicNote> AcademicNotes { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<Agenda> Agenda { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<Assignment> Assignments { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<Chapter> Chapters { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<ClassClassTeacherMap> ClassClassTeacherMaps { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<ClassGroup> ClassGroups { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<ClassSectionSubjectPeriodMap> ClassSectionSubjectPeriodMaps { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<ClassSubjectMap> ClassSubjectMaps { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<ClassSubjectSkillGroupMap> ClassSubjectSkillGroupMaps { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<ClassSubjectWorkflowEntityMap> ClassSubjectWorkflowEntityMaps { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<ClassTeacherMap> ClassTeacherMaps { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<ExamSubjectMap> ExamSubjectMaps { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<LessonPlan> LessonPlans { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<MarkRegisterSkillGroup> MarkRegisterSkillGroups { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<MarkRegisterSubjectMap> MarkRegisterSubjectMaps { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<OnlineExamResultSubjectMap> OnlineExamResultSubjectMaps { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<OnlineExamSubjectMap> OnlineExamSubjectMaps { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<QualificationCoreSubjectMap> QualificationCoreSubjectMaps { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<QuestionGroup> QuestionGroups { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<Question> Questions { get; set; }
        [InverseProperty("subject")]
        public virtual ICollection<RemarksEntryExamMap> RemarksEntryExamMaps { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<SkillGroupSubjectMap> SkillGroupSubjectMaps { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<StreamOptionalSubjectMap> StreamOptionalSubjectMaps { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<StreamSubjectMap> StreamSubjectMaps { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<StudentApplicationOptionalSubjectMap> StudentApplicationOptionalSubjectMaps { get; set; }
        [InverseProperty("SecondLang")]
        public virtual ICollection<StudentApplication> StudentApplicationSecondLangs { get; set; }
        [InverseProperty("ThirdLang")]
        public virtual ICollection<StudentApplication> StudentApplicationThirdLangs { get; set; }
        [InverseProperty("SecondLang")]
        public virtual ICollection<Student> StudentSecondLangs { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<StudentSkillRegister> StudentSkillRegisters { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<StudentStreamOptionalSubjectMap> StudentStreamOptionalSubjectMaps { get; set; }
        [InverseProperty("SubjectMap")]
        public virtual ICollection<Student> StudentSubjectMaps { get; set; }
        [InverseProperty("ThirdLang")]
        public virtual ICollection<Student> StudentThirdLangs { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<SubjectGroupSubjectMap> SubjectGroupSubjectMaps { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<SubjectInchargerClassMap> SubjectInchargerClassMaps { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<SubjectTeacherMap> SubjectTeacherMaps { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<SubjectTopic> SubjectTopics { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<SubjectUnit> SubjectUnits { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<TeacherActivity> TeacherActivities { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<TimeTableAllocation> TimeTableAllocations { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<TimeTableExtSubject> TimeTableExtSubjects { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<TimeTableExtTeacher> TimeTableExtTeachers { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<TimeTableLog> TimeTableLogs { get; set; }
    }
}

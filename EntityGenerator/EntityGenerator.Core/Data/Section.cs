using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Sections", Schema = "schools")]
    public partial class Section
    {
        public Section()
        {
            AcademicNotes = new HashSet<AcademicNote>();
            Agenda = new HashSet<Agenda>();
            AgendaSectionMaps = new HashSet<AgendaSectionMap>();
            AssignmentSectionMaps = new HashSet<AssignmentSectionMap>();
            Assignments = new HashSet<Assignment>();
            CampusTransferFromSections = new HashSet<CampusTransfer>();
            CampusTransferToSections = new HashSet<CampusTransfer>();
            Chapters = new HashSet<Chapter>();
            CircularMaps = new HashSet<CircularMap>();
            ClassClassTeacherMaps = new HashSet<ClassClassTeacherMap>();
            ClassCoordinatorClassMaps = new HashSet<ClassCoordinatorClassMap>();
            ClassCoordinatorMaps = new HashSet<ClassCoordinatorMap>();
            ClassSectionMaps = new HashSet<ClassSectionMap>();
            ClassSectionSubjectPeriodMaps = new HashSet<ClassSectionSubjectPeriodMap>();
            ClassSubjectMaps = new HashSet<ClassSubjectMap>();
            ClassTeacherMaps = new HashSet<ClassTeacherMap>();
            CounselorHubMaps = new HashSet<CounselorHubMap>();
            EventAudienceMaps = new HashSet<EventAudienceMap>();
            ExamClassMaps = new HashSet<ExamClassMap>();
            FeeCollections = new HashSet<FeeCollection>();
            FinalSettlements = new HashSet<FinalSettlement>();
            HealthEntries = new HashSet<HealthEntry>();
            LessonPlanClassSectionMaps = new HashSet<LessonPlanClassSectionMap>();
            LessonPlans = new HashSet<LessonPlan>();
            MarkRegisters = new HashSet<MarkRegister>();
            MeetingRequests = new HashSet<MeetingRequest>();
            ProgressReports = new HashSet<ProgressReport>();
            Refunds = new HashSet<Refund>();
            RemarksEntries = new HashSet<RemarksEntry>();
            SchoolCreditNotes = new HashSet<SchoolCreditNote>();
            Signups = new HashSet<Signup>();
            StudentAttendences = new HashSet<StudentAttendence>();
            StudentClassHistoryMaps = new HashSet<StudentClassHistoryMap>();
            StudentFeeDues = new HashSet<StudentFeeDue>();
            StudentPromotionLogSections = new HashSet<StudentPromotionLog>();
            StudentPromotionLogShiftFromSections = new HashSet<StudentPromotionLog>();
            StudentRouteStopMapLogs = new HashSet<StudentRouteStopMapLog>();
            StudentRouteStopMaps = new HashSet<StudentRouteStopMap>();
            StudentSkillRegisters = new HashSet<StudentSkillRegister>();
            Students = new HashSet<Student>();
            SubjectInchargerClassMaps = new HashSet<SubjectInchargerClassMap>();
            SubjectTeacherMaps = new HashSet<SubjectTeacherMap>();
            SubjectTopics = new HashSet<SubjectTopic>();
            SubjectUnits = new HashSet<SubjectUnit>();
            TeacherActivities = new HashSet<TeacherActivity>();
            TimeTableAllocations = new HashSet<TimeTableAllocation>();
            TimeTableExtSections = new HashSet<TimeTableExtSection>();
            TimeTableLogs = new HashSet<TimeTableLog>();
        }

        [Key]
        public int SectionID { get; set; }
        [StringLength(50)]
        public string Code { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(10)]
        public string SectionCode { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("Sections")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("Sections")]
        public virtual School School { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<AcademicNote> AcademicNotes { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<Agenda> Agenda { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<AgendaSectionMap> AgendaSectionMaps { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<AssignmentSectionMap> AssignmentSectionMaps { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<Assignment> Assignments { get; set; }
        [InverseProperty("FromSection")]
        public virtual ICollection<CampusTransfer> CampusTransferFromSections { get; set; }
        [InverseProperty("ToSection")]
        public virtual ICollection<CampusTransfer> CampusTransferToSections { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<Chapter> Chapters { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<CircularMap> CircularMaps { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<ClassClassTeacherMap> ClassClassTeacherMaps { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<ClassCoordinatorClassMap> ClassCoordinatorClassMaps { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<ClassCoordinatorMap> ClassCoordinatorMaps { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<ClassSectionMap> ClassSectionMaps { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<ClassSectionSubjectPeriodMap> ClassSectionSubjectPeriodMaps { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<ClassSubjectMap> ClassSubjectMaps { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<ClassTeacherMap> ClassTeacherMaps { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<CounselorHubMap> CounselorHubMaps { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<EventAudienceMap> EventAudienceMaps { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<ExamClassMap> ExamClassMaps { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<FeeCollection> FeeCollections { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<FinalSettlement> FinalSettlements { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<HealthEntry> HealthEntries { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<LessonPlanClassSectionMap> LessonPlanClassSectionMaps { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<LessonPlan> LessonPlans { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<MarkRegister> MarkRegisters { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<MeetingRequest> MeetingRequests { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<ProgressReport> ProgressReports { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<Refund> Refunds { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<RemarksEntry> RemarksEntries { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<SchoolCreditNote> SchoolCreditNotes { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<Signup> Signups { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<StudentAttendence> StudentAttendences { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<StudentClassHistoryMap> StudentClassHistoryMaps { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<StudentFeeDue> StudentFeeDues { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<StudentPromotionLog> StudentPromotionLogSections { get; set; }
        [InverseProperty("ShiftFromSection")]
        public virtual ICollection<StudentPromotionLog> StudentPromotionLogShiftFromSections { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<StudentRouteStopMapLog> StudentRouteStopMapLogs { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<StudentRouteStopMap> StudentRouteStopMaps { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<StudentSkillRegister> StudentSkillRegisters { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<Student> Students { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<SubjectInchargerClassMap> SubjectInchargerClassMaps { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<SubjectTeacherMap> SubjectTeacherMaps { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<SubjectTopic> SubjectTopics { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<SubjectUnit> SubjectUnits { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<TeacherActivity> TeacherActivities { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<TimeTableAllocation> TimeTableAllocations { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<TimeTableExtSection> TimeTableExtSections { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<TimeTableLog> TimeTableLogs { get; set; }
    }
}

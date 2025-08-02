using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Classes", Schema = "schools")]
    public partial class Class
    {
        public Class()
        {
            AcademicClassMaps = new HashSet<AcademicClassMap>();
            AcademicNotes = new HashSet<AcademicNote>();
            AgeCriterias = new HashSet<AgeCriteria>();
            Agenda = new HashSet<Agenda>();
            Assignments = new HashSet<Assignment>();
            CampusTransferFromClasses = new HashSet<CampusTransfer>();
            CampusTransferToClasses = new HashSet<CampusTransfer>();
            Candidates = new HashSet<Candidate>();
            Categories = new HashSet<Category>();
            CircularMaps = new HashSet<CircularMap>();
            ClassClassGroupMaps = new HashSet<ClassClassGroupMap>();
            ClassClassTeacherMaps = new HashSet<ClassClassTeacherMap>();
            ClassCoordinatorClassMaps = new HashSet<ClassCoordinatorClassMap>();
            ClassCoordinatorMaps = new HashSet<ClassCoordinatorMap>();
            ClassFeeMasters = new HashSet<ClassFeeMaster>();
            ClassFeeStructureMaps = new HashSet<ClassFeeStructureMap>();
            ClassSectionMaps = new HashSet<ClassSectionMap>();
            ClassSectionSubjectPeriodMaps = new HashSet<ClassSectionSubjectPeriodMap>();
            ClassSubjectMaps = new HashSet<ClassSubjectMap>();
            ClassSubjectSkillGroupMaps = new HashSet<ClassSubjectSkillGroupMap>();
            ClassTeacherMaps = new HashSet<ClassTeacherMap>();
            ClassWorkFlowMaps = new HashSet<ClassWorkFlowMap>();
            CounselorHubMaps = new HashSet<CounselorHubMap>();
            EventAudienceMaps = new HashSet<EventAudienceMap>();
            ExamClassMaps = new HashSet<ExamClassMap>();
            FeeCollections = new HashSet<FeeCollection>();
            FinalSettlements = new HashSet<FinalSettlement>();
            HealthEntries = new HashSet<HealthEntry>();
            Leads = new HashSet<Lead>();
            LessonPlanClassSectionMaps = new HashSet<LessonPlanClassSectionMap>();
            LessonPlans = new HashSet<LessonPlan>();
            MarkRegisters = new HashSet<MarkRegister>();
            MeetingRequests = new HashSet<MeetingRequest>();
            OnlineExams = new HashSet<OnlineExam>();
            PackageConfigClassMaps = new HashSet<PackageConfigClassMap>();
            ProductClassMaps = new HashSet<ProductClassMap>();
            ProgressReports = new HashSet<ProgressReport>();
            Refunds = new HashSet<Refund>();
            RemarksEntries = new HashSet<RemarksEntry>();
            SchoolCreditNotes = new HashSet<SchoolCreditNote>();
            Signups = new HashSet<Signup>();
            StudentApplicationClasses = new HashSet<StudentApplication>();
            StudentApplicationPreviousSchoolClassCompleteds = new HashSet<StudentApplication>();
            StudentAttendences = new HashSet<StudentAttendence>();
            StudentClassHistoryMaps = new HashSet<StudentClassHistoryMap>();
            StudentClasses = new HashSet<Student>();
            StudentFeeDues = new HashSet<StudentFeeDue>();
            StudentLeaveApplications = new HashSet<StudentLeaveApplication>();
            StudentPreviousSchoolClassCompleteds = new HashSet<Student>();
            StudentPromotionLogClasses = new HashSet<StudentPromotionLog>();
            StudentPromotionLogShiftFromClasses = new HashSet<StudentPromotionLog>();
            StudentRouteStopMapLogs = new HashSet<StudentRouteStopMapLog>();
            StudentRouteStopMaps = new HashSet<StudentRouteStopMap>();
            StudentSkillRegisters = new HashSet<StudentSkillRegister>();
            SubjectGroupSubjectMaps = new HashSet<SubjectGroupSubjectMap>();
            SubjectInchargerClassMaps = new HashSet<SubjectInchargerClassMap>();
            SubjectTeacherMaps = new HashSet<SubjectTeacherMap>();
            SubjectTopics = new HashSet<SubjectTopic>();
            TeacherActivities = new HashSet<TeacherActivity>();
            TimeTableAllocations = new HashSet<TimeTableAllocation>();
            TimeTableLogs = new HashSet<TimeTableLog>();
            TransportApplctnStudentMaps = new HashSet<TransportApplctnStudentMap>();
        }

        [Key]
        public int ClassID { get; set; }
        [StringLength(50)]
        public string Code { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        public byte? ShiftID { get; set; }
        public byte? SchoolID { get; set; }
        public int? CostCenterID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public int? ORDERNO { get; set; }
        public int? AcademicYearID { get; set; }
        public long? ClassGroupID { get; set; }
        public bool? IsVisible { get; set; }
        public bool? IsActive { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("Classes")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("CostCenterID")]
        [InverseProperty("Classes")]
        public virtual CostCenter CostCenter { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("Classes")]
        public virtual School School { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<AcademicClassMap> AcademicClassMaps { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<AcademicNote> AcademicNotes { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<AgeCriteria> AgeCriterias { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<Agenda> Agenda { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<Assignment> Assignments { get; set; }
        [InverseProperty("FromClass")]
        public virtual ICollection<CampusTransfer> CampusTransferFromClasses { get; set; }
        [InverseProperty("ToClass")]
        public virtual ICollection<CampusTransfer> CampusTransferToClasses { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<Candidate> Candidates { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<Category> Categories { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<CircularMap> CircularMaps { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<ClassClassGroupMap> ClassClassGroupMaps { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<ClassClassTeacherMap> ClassClassTeacherMaps { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<ClassCoordinatorClassMap> ClassCoordinatorClassMaps { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<ClassCoordinatorMap> ClassCoordinatorMaps { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<ClassFeeMaster> ClassFeeMasters { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<ClassFeeStructureMap> ClassFeeStructureMaps { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<ClassSectionMap> ClassSectionMaps { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<ClassSectionSubjectPeriodMap> ClassSectionSubjectPeriodMaps { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<ClassSubjectMap> ClassSubjectMaps { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<ClassSubjectSkillGroupMap> ClassSubjectSkillGroupMaps { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<ClassTeacherMap> ClassTeacherMaps { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<ClassWorkFlowMap> ClassWorkFlowMaps { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<CounselorHubMap> CounselorHubMaps { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<EventAudienceMap> EventAudienceMaps { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<ExamClassMap> ExamClassMaps { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<FeeCollection> FeeCollections { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<FinalSettlement> FinalSettlements { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<HealthEntry> HealthEntries { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<Lead> Leads { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<LessonPlanClassSectionMap> LessonPlanClassSectionMaps { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<LessonPlan> LessonPlans { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<MarkRegister> MarkRegisters { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<MeetingRequest> MeetingRequests { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<OnlineExam> OnlineExams { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<PackageConfigClassMap> PackageConfigClassMaps { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<ProductClassMap> ProductClassMaps { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<ProgressReport> ProgressReports { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<Refund> Refunds { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<RemarksEntry> RemarksEntries { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<SchoolCreditNote> SchoolCreditNotes { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<Signup> Signups { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<StudentApplication> StudentApplicationClasses { get; set; }
        [InverseProperty("PreviousSchoolClassCompleted")]
        public virtual ICollection<StudentApplication> StudentApplicationPreviousSchoolClassCompleteds { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<StudentAttendence> StudentAttendences { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<StudentClassHistoryMap> StudentClassHistoryMaps { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<Student> StudentClasses { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<StudentFeeDue> StudentFeeDues { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<StudentLeaveApplication> StudentLeaveApplications { get; set; }
        [InverseProperty("PreviousSchoolClassCompleted")]
        public virtual ICollection<Student> StudentPreviousSchoolClassCompleteds { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<StudentPromotionLog> StudentPromotionLogClasses { get; set; }
        [InverseProperty("ShiftFromClass")]
        public virtual ICollection<StudentPromotionLog> StudentPromotionLogShiftFromClasses { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<StudentRouteStopMapLog> StudentRouteStopMapLogs { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<StudentRouteStopMap> StudentRouteStopMaps { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<StudentSkillRegister> StudentSkillRegisters { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<SubjectGroupSubjectMap> SubjectGroupSubjectMaps { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<SubjectInchargerClassMap> SubjectInchargerClassMaps { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<SubjectTeacherMap> SubjectTeacherMaps { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<SubjectTopic> SubjectTopics { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<TeacherActivity> TeacherActivities { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<TimeTableAllocation> TimeTableAllocations { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<TimeTableLog> TimeTableLogs { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<TransportApplctnStudentMap> TransportApplctnStudentMaps { get; set; }
    }
}

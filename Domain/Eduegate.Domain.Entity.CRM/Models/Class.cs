using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.CRM.Models
{
    [Table("Classes", Schema = "schools")]
    public partial class Class
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Class()
        {
            //EventAudienceMaps = new HashSet<EventAudienceMap>();
            Leads = new HashSet<Lead>();
            //AcademicNotes = new HashSet<AcademicNote>();
            //Assignments = new HashSet<Assignment>();
            //ClassFeeStructureMaps = new HashSet<ClassFeeStructureMap>();
            //ClassSectionMaps = new HashSet<ClassSectionMap>();
            //ClassSubjectMaps = new HashSet<ClassSubjectMap>();
            //ClassSubjectSkillGroupMaps = new HashSet<ClassSubjectSkillGroupMap>();
            //ClassTeacherMaps = new HashSet<ClassTeacherMap>();
            //ExamClassMaps = new HashSet<ExamClassMap>();
            //ClassFeeMasters = new HashSet<ClassFeeMaster>();
            //FeeCollections = new HashSet<FeeCollection>();
            //FinalSettlements = new HashSet<FinalSettlement>();
            //LessonPlans = new HashSet<LessonPlan>();
            //MarkRegisters = new HashSet<MarkRegister>();
            //PackageConfigClassMaps = new HashSet<PackageConfigClassMap>();
            //SchoolCreditNotes = new HashSet<SchoolCreditNote>();
            //StudentApplications = new HashSet<StudentApplication>();
            //StudentApplications1 = new HashSet<StudentApplication>();
            //StudentAttendences = new HashSet<StudentAttendence>();
            //StudentClassHistoryMaps = new HashSet<StudentClassHistoryMap>();
            //StudentFeeDues = new HashSet<StudentFeeDue>();
            //StudentLeaveApplications = new HashSet<StudentLeaveApplication>();
            //StudentPromotionLogs = new HashSet<StudentPromotionLog>();
            //StudentPromotionLogs1 = new HashSet<StudentPromotionLog>();
            //Students = new HashSet<Student>();
            //Students1 = new HashSet<Student>();
            //StudentSkillRegisters = new HashSet<StudentSkillRegister>();
            //SubjectTeacherMaps = new HashSet<SubjectTeacherMap>();
            //SubjectTopics = new HashSet<SubjectTopic>();
            //TeacherActivities = new HashSet<TeacherActivity>();
            //TimeTableAllocations = new HashSet<TimeTableAllocation>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ClassID { get; set; }

        [StringLength(50)]
        public string ClassDescription { get; set; }

        public byte? ShiftID { get; set; }

        public byte? SchoolID { get; set; }

        public int? CostCenterID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        public int? ORDERNO { get; set; }

        public int? AcademicYearID { get; set; }

        [StringLength(100)]
        public string Code { get; set; }

        //public virtual CostCenter CostCenter { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<EventAudienceMap> EventAudienceMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Lead> Leads { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<AcademicNote> AcademicNotes { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Assignment> Assignments { get; set; }

        //public virtual School School { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ClassFeeStructureMap> ClassFeeStructureMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ClassSectionMap> ClassSectionMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ClassSubjectMap> ClassSubjectMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ClassSubjectSkillGroupMap> ClassSubjectSkillGroupMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ClassTeacherMap> ClassTeacherMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ExamClassMap> ExamClassMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ClassFeeMaster> ClassFeeMasters { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<FeeCollection> FeeCollections { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<FinalSettlement> FinalSettlements { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<LessonPlan> LessonPlans { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<MarkRegister> MarkRegisters { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<PackageConfigClassMap> PackageConfigClassMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<SchoolCreditNote> SchoolCreditNotes { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StudentApplication> StudentApplications { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StudentApplication> StudentApplications1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StudentAttendence> StudentAttendences { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StudentClassHistoryMap> StudentClassHistoryMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StudentFeeDue> StudentFeeDues { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StudentLeaveApplication> StudentLeaveApplications { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StudentPromotionLog> StudentPromotionLogs { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StudentPromotionLog> StudentPromotionLogs1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Student> Students { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Student> Students1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StudentSkillRegister> StudentSkillRegisters { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<SubjectTeacherMap> SubjectTeacherMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<SubjectTopic> SubjectTopics { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TeacherActivity> TeacherActivities { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TimeTableAllocation> TimeTableAllocations { get; set; }
    }
}
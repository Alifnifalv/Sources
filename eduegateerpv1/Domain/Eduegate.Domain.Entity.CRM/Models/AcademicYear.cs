using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.CRM.Models
{
    [Table("AcademicYears", Schema = "schools")]
    public partial class AcademicYear
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AcademicYear()
        {
            Leads = new HashSet<Lead>();
            //AcadamicCalendars = new HashSet<AcadamicCalendar>();
            //Assignments = new HashSet<Assignment>();
            Classes = new HashSet<Class>();
            //ClassFeeStructureMaps = new HashSet<ClassFeeStructureMap>();
            //ClassSectionMaps = new HashSet<ClassSectionMap>();
            //ClassSubjectMaps = new HashSet<ClassSubjectMap>();
            //ClassTeacherMaps = new HashSet<ClassTeacherMap>();
            //ClassTimings = new HashSet<ClassTiming>();
            //CreditNoteFeeTypeMaps = new HashSet<CreditNoteFeeTypeMap>();
            //Exams1 = new HashSet<Exams1>();
            //ExamTypes = new HashSet<ExamType>();
            //ClassFeeMasters = new HashSet<ClassFeeMaster>();
            //FeeCollections = new HashSet<FeeCollection>();
            //FeeMasters = new HashSet<FeeMaster>();
            //FeePeriods = new HashSet<FeePeriod>();
            //FeeStructures = new HashSet<FeeStructure>();
            //FeeStructures1 = new HashSet<FeeStructure>();
            //FeeTypes = new HashSet<FeeType>();
            //FinalSettlements = new HashSet<FinalSettlement>();
            //FineMasters = new HashSet<FineMaster>();
            //LibraryBooks = new HashSet<LibraryBook>();
            //LibraryStaffRegisters = new HashSet<LibraryStaffRegister>();
            //LibraryStudentRegisters = new HashSet<LibraryStudentRegister>();
            //LibraryTransactions = new HashSet<LibraryTransaction>();
            //MarkGradeMaps = new HashSet<MarkGradeMap>();
            //MarkGrades = new HashSet<MarkGrade>();
            //MarkRegisters = new HashSet<MarkRegister>();
            //Mediums = new HashSet<Medium>();
            //PackageConfigs = new HashSet<PackageConfig>();
            //Parents = new HashSet<Parent>();
            //Routes1 = new HashSet<Routes1>();
            //RouteTypes = new HashSet<RouteType>();
            //RouteVehicleMaps = new HashSet<RouteVehicleMap>();
            //SchoolCalenders = new HashSet<SchoolCalender>();
            //SchoolCreditNotes = new HashSet<SchoolCreditNote>();
            //Sections = new HashSet<Section>();
            //Shifts = new HashSet<Shift>();
            //StaffAttendences = new HashSet<StaffAttendence>();
            //StaffRouteStopMaps = new HashSet<StaffRouteStopMap>();
            //StudentApplications = new HashSet<StudentApplication>();
            //StudentApplications1 = new HashSet<StudentApplication>();
            //StudentAttendences = new HashSet<StudentAttendence>();
            //StudentFeeDues = new HashSet<StudentFeeDue>();
            //StudentGroupFeeMasters = new HashSet<StudentGroupFeeMaster>();
            //StudentPromotionLogs = new HashSet<StudentPromotionLog>();
            //StudentPromotionLogs1 = new HashSet<StudentPromotionLog>();
            //Students = new HashSet<Student>();
            //StudentTransferRequestReasons = new HashSet<StudentTransferRequestReason>();
            //StudentTransferRequests = new HashSet<StudentTransferRequest>();
            //Subjects = new HashSet<Subject>();
            //SubjectTeacherMaps = new HashSet<SubjectTeacherMap>();
            //Syllabus = new HashSet<Syllabu>();
            //TimeTables = new HashSet<TimeTable>();
        }
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AcademicYearID { get; set; }

        [StringLength(20)]
        public string AcademicYearCode { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public byte? SchoolID { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        public byte? AcademicYearStatusID { get; set; }

        public int? ORDERNO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Lead> Leads { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<AcadamicCalendar> AcadamicCalendars { get; set; }

        //public virtual AcademicYearStatu AcademicYearStatu { get; set; }

        //public virtual School School { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Assignment> Assignments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Class> Classes { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ClassFeeStructureMap> ClassFeeStructureMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ClassSectionMap> ClassSectionMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ClassSubjectMap> ClassSubjectMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ClassTeacherMap> ClassTeacherMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ClassTiming> ClassTimings { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<CreditNoteFeeTypeMap> CreditNoteFeeTypeMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Exams1> Exams1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ExamType> ExamTypes { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ClassFeeMaster> ClassFeeMasters { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<FeeCollection> FeeCollections { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<FeeMaster> FeeMasters { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<FeePeriod> FeePeriods { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<FeeStructure> FeeStructures { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<FeeStructure> FeeStructures1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<FeeType> FeeTypes { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<FinalSettlement> FinalSettlements { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<FineMaster> FineMasters { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<LibraryBook> LibraryBooks { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<LibraryStaffRegister> LibraryStaffRegisters { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<LibraryStudentRegister> LibraryStudentRegisters { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<LibraryTransaction> LibraryTransactions { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<MarkGradeMap> MarkGradeMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<MarkGrade> MarkGrades { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<MarkRegister> MarkRegisters { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Medium> Mediums { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<PackageConfig> PackageConfigs { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Parent> Parents { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Routes1> Routes1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<RouteType> RouteTypes { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<RouteVehicleMap> RouteVehicleMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<SchoolCalender> SchoolCalenders { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<SchoolCreditNote> SchoolCreditNotes { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Section> Sections { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Shift> Shifts { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StaffAttendence> StaffAttendences { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StaffRouteStopMap> StaffRouteStopMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StudentApplication> StudentApplications { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StudentApplication> StudentApplications1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StudentAttendence> StudentAttendences { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StudentFeeDue> StudentFeeDues { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StudentGroupFeeMaster> StudentGroupFeeMasters { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StudentPromotionLog> StudentPromotionLogs { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StudentPromotionLog> StudentPromotionLogs1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Student> Students { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StudentTransferRequestReason> StudentTransferRequestReasons { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StudentTransferRequest> StudentTransferRequests { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Subject> Subjects { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<SubjectTeacherMap> SubjectTeacherMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Syllabu> Syllabus { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TimeTable> TimeTables { get; set; }
    }
}
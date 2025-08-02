using EntityGenerator.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("Students", Schema = "schools")]
    public partial class Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            //Candidates = new HashSet<Candidate>();
            TransactionHeads = new HashSet<TransactionHead>();
            AssignmentDocuments = new HashSet<AssignmentDocument>();
            CampusTransfers = new HashSet<CampusTransfers>();
            DriverScheduleLogs = new HashSet<DriverScheduleLog>();
            FeeCollections = new HashSet<FeeCollection>();
            FinalSettlements = new HashSet<FinalSettlement>();
            FineMasterStudentMaps = new HashSet<FineMasterStudentMap>();
            HealthEntryStudentMaps = new HashSet<HealthEntryStudentMap>();
            LibraryStudentRegisters = new HashSet<LibraryStudentRegister>();
            LibraryTransactions = new HashSet<LibraryTransaction>();
            MarkRegisters = new HashSet<MarkRegister>();
            PackageConfigStudentMaps = new HashSet<PackageConfigStudentMap>();
            ParentStudentMaps = new HashSet<ParentStudentMap>();
            RemarksEntryStudentMaps = new HashSet<RemarksEntryStudentMap>();
            SchoolCreditNotes = new HashSet<SchoolCreditNote>();
            SchoolPollAnswerLogs = new HashSet<SchoolPollAnswerLog>();
            StudentApplicationSiblingMaps = new HashSet<StudentApplicationSiblingMap>();
            StudentAssignmentMaps = new HashSet<StudentAssignmentMap>();
            StudentAttendences = new HashSet<StudentAttendence>();
            StudentClassHistoryMaps = new HashSet<StudentClassHistoryMap>();
            StudentFeeDues = new HashSet<StudentFeeDue>();
            StudentGroupMaps = new HashSet<StudentGroupMap>();
            StudentLeaveApplications = new HashSet<StudentLeaveApplication>();
            StudentMiscDetails = new HashSet<StudentMiscDetail>();
            StudentPassportDetails = new HashSet<StudentPassportDetail>();
            StudentPromotionLogs = new HashSet<StudentPromotionLog>();
            StudentRouteStopMapLogs = new HashSet<StudentRouteStopMapLog>();
            StudentRouteStopMaps = new HashSet<StudentRouteStopMap>();
            StudentStreamOptionalSubjectMaps = new HashSet<StudentStreamOptionalSubjectMap>();
            StudentPickupRequests = new HashSet<StudentPickupRequest>();
            StudentSiblingMaps = new HashSet<StudentSiblingMap>();
            StudentSiblingMaps1 = new HashSet<StudentSiblingMap>();
            StudentSkillRegisters = new HashSet<StudentSkillRegister>();
            StudentTransferRequests = new HashSet<StudentTransferRequest>();
            TransportApplctnStudentMaps = new HashSet<TransportApplctnStudentMap>();
            StudentApplicationDocumentMaps = new HashSet<StudentApplicationDocumentMap>();
            Refunds = new HashSet<Refund>();
            StudentStaffMaps = new HashSet<StudentStaffMap>();
            StudentPickerStudentMaps = new HashSet<StudentPickerStudentMap>();
            StudentPickLogs = new HashSet<StudentPickLog>();
            AllergyStudentMaps = new HashSet<AllergyStudentMap>();
            ProgressReports = new HashSet<ProgressReport>();
            StudentAchievements = new HashSet<StudentAchievement>();
            CounselorHubMaps = new HashSet<CounselorHubMap>();
            CounselorHubAttachmentMaps = new HashSet<CounselorHubAttachmentMap>();


        }

        [Key]
        public long StudentIID { get; set; }

        [StringLength(50)]
        public string AdmissionNumber { get; set; }

        [StringLength(50)]
        public string RollNumber { get; set; }

        public int? ClassID { get; set; }

        public byte? GradeID { get; set; }

        public int? SectionID { get; set; }

        [StringLength(200)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string MiddleName { get; set; }

        [StringLength(200)]
        public string LastName { get; set; }

        public byte? GenderID { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int? StudentCategoryID { get; set; }

        public byte? CastID { get; set; }

        public byte? RelegionID { get; set; }

        [StringLength(50)]
        public string MobileNumber { get; set; }

        [StringLength(50)]
        public string EmailID { get; set; }

        public DateTime? AdmissionDate { get; set; }

        [StringLength(500)]
        public string StudentProfile { get; set; }

        public int? BloodGroupID { get; set; }

        public int? StudentHouseID { get; set; }

        [StringLength(20)]
        public string Height { get; set; }

        [StringLength(20)]
        public string Weight { get; set; }

        public DateTime? AsOnDate { get; set; }

        public int? HostelID { get; set; }

        public long? HostelRoomID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public long? Account_Sub_ledger_ID { get; set; }

        public long? LoginID { get; set; }

        public long? ParentID { get; set; }

        public bool? IsActive { get; set; }

        public byte? Status { get; set; }

        [StringLength(20)]
        public string CurrentBuildingNo { get; set; }

        [StringLength(20)]
        public string CurrentFlatNo { get; set; }

        [StringLength(20)]
        public string CurrentStreetNo { get; set; }

        [StringLength(25)]
        public string CurrentStreetName { get; set; }

        [StringLength(20)]
        public string CurrentLocationNo { get; set; }

        [StringLength(25)]
        public string CurrentLocationName { get; set; }

        [StringLength(20)]
        public string CurrentZipNo { get; set; }

        [StringLength(25)]
        public string CurrentCity { get; set; }

        [StringLength(25)]
        public string CurrentPostBoxNo { get; set; }

        public int? CurrentCountryID { get; set; }

        public bool? IsAddressIsCurrentAddress { get; set; }

        [StringLength(20)]
        public string PermenentBuildingNo { get; set; }

        [StringLength(20)]
        public string PermenentFlatNo { get; set; }

        [StringLength(20)]
        public string PermenentStreetNo { get; set; }

        [StringLength(25)]
        public string PermenentStreetName { get; set; }

        [StringLength(20)]
        public string PermenentLocationNo { get; set; }

        [StringLength(25)]
        public string PermenentLocationName { get; set; }

        [StringLength(20)]
        public string PermenentZipNo { get; set; }

        [StringLength(25)]
        public string PermenentCity { get; set; }

        [StringLength(25)]
        public string PermenentPostBoxNo { get; set; }

        public int? PermenentCountryID { get; set; }

        public bool? IsAddressIsPermenentAddress { get; set; }

        public DateTime? FeeStartDate { get; set; }

        public byte? CommunityID { get; set; }

        public bool? IsMinority { get; set; }

        public bool? IsOnlyChildofParent { get; set; }

        public byte? PrimaryContactID { get; set; }

        public int? SecoundLanguageID { get; set; }

        public int? ThridLanguageID { get; set; }

        public bool? IsStudentStudiedBefore { get; set; }

        [StringLength(200)]
        public string PreviousSchoolName { get; set; }

        public byte? PreviousSchoolSyllabusID { get; set; }

        [StringLength(15)]
        public string PreviousSchoolAcademicYear { get; set; }

        public int? PreviousSchoolClassCompletedID { get; set; }

        [StringLength(250)]
        public string PreviousSchoolAddress { get; set; }

        public long? ApplicationID { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        [StringLength(500)]
        public string MigratedProfileName { get; set; }

        public int? SubjectMapID { get; set; }

        public int? SchoolAcademicyearID { get; set; }

        public int? SecondLangID { get; set; }

        public int? ThirdLangID { get; set; }

        public byte? StreamID { get; set; }

        public DateTime? InactiveDate { get; set; }

        public virtual Login Login { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Candidate> Candidates { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }

        public virtual BloodGroup BloodGroup { get; set; }

        public virtual Country Country { get; set; }

        public virtual Country Country1 { get; set; }

        public virtual Gender Gender { get; set; }

        public virtual Language Language { get; set; }

        public virtual Language Language1 { get; set; }

        public virtual Relegion Relegion { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual AcademicYear AcademicYear1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssignmentDocument> AssignmentDocuments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CampusTransfers> CampusTransfers { get; set; }

        public virtual Cast Cast { get; set; }

        public virtual Class Class { get; set; }

        public virtual Class Class1 { get; set; }

        public virtual Community Community { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DriverScheduleLog> DriverScheduleLogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeCollection> FeeCollections { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FinalSettlement> FinalSettlements { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FineMasterStudentMap> FineMasterStudentMaps { get; set; }

        public virtual Grade Grade { get; set; }

        public virtual GuardianType GuardianType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HealthEntryStudentMap> HealthEntryStudentMaps { get; set; }

        public virtual HostelRoom HostelRoom { get; set; }

        public virtual Hostel Hostel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LibraryStudentRegister> LibraryStudentRegisters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LibraryTransaction> LibraryTransactions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MarkRegister> MarkRegisters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PackageConfigStudentMap> PackageConfigStudentMaps { get; set; }

        public virtual Parent Parent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ParentStudentMap> ParentStudentMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RemarksEntryStudentMap> RemarksEntryStudentMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchoolCreditNote> SchoolCreditNotes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchoolPollAnswerLog> SchoolPollAnswerLogs { get; set; }

        public virtual Schools School { get; set; }

        public virtual Section Section { get; set; }

        public virtual Stream Stream { get; set; }

        public virtual StudentApplication StudentApplication { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentApplicationSiblingMap> StudentApplicationSiblingMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentAssignmentMap> StudentAssignmentMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentAttendence> StudentAttendences { get; set; }

        public virtual StudentCategory StudentCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentClassHistoryMap> StudentClassHistoryMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentFeeDue> StudentFeeDues { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentGroupMap> StudentGroupMaps { get; set; }

        public virtual StudentHouse StudentHouse { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentLeaveApplication> StudentLeaveApplications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentMiscDetail> StudentMiscDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentPassportDetail> StudentPassportDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentPromotionLog> StudentPromotionLogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentRouteStopMapLog> StudentRouteStopMapLogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentRouteStopMap> StudentRouteStopMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentStreamOptionalSubjectMap> StudentStreamOptionalSubjectMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentPickupRequest> StudentPickupRequests { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual Subject Subject1 { get; set; }

        public virtual Subject Subject2 { get; set; }

        public virtual Syllabu Syllabu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentSiblingMap> StudentSiblingMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentSiblingMap> StudentSiblingMaps1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentSkillRegister> StudentSkillRegisters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentTransferRequest> StudentTransferRequests { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransportApplctnStudentMap> TransportApplctnStudentMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentApplicationDocumentMap> StudentApplicationDocumentMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Refund> Refunds { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentStaffMap> StudentStaffMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentPickerStudentMap> StudentPickerStudentMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentPickLog> StudentPickLogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AllergyStudentMap> AllergyStudentMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProgressReport> ProgressReports { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentAchievement> StudentAchievements { get; set; }

        public virtual ICollection<CounselorHubMap> CounselorHubMaps { get; set; }

        public virtual ICollection<CounselorHubAttachmentMap> CounselorHubAttachmentMaps { get; set; }
      




    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Students", Schema = "schools")]
    [Index("AcademicYearID", Name = "IDX_Students_AcademicYearID_AdmissionNumber__FirstName__MiddleName__LastName")]
    [Index("AcademicYearID", Name = "IDX_Students_AcademicYearID_ClassID__IsActive__Status")]
    [Index("AcademicYearID", Name = "IDX_Students_AcademicYearID_ClassID__SectionID__ParentID__IsActive__Status__FeeStartDate")]
    [Index("AdmissionNumber", Name = "IDX_Students_AdmissionNumber_")]
    [Index("ApplicationID", Name = "IDX_Students_ApplicationID_")]
    [Index("ApplicationID", Name = "IDX_Students_ApplicationID_AdmissionNumber")]
    [Index("ClassID", "IsActive", "SchoolID", "AcademicYearID", Name = "IDX_Students_ClassID__IsActive__SchoolID__AcademicYearID_SectionID__ParentID")]
    [Index("ClassID", "SectionID", Name = "IDX_Students_ClassID__SectionID_IsActive__Status__AcademicYearID")]
    [Index("IsActive", "SchoolID", "AcademicYearID", Name = "IDX_Students_IsActive_SchoolID_AcademicYearID")]
    [Index("IsActive", "SchoolID", "Status", Name = "IDX_Students_IsActive__SchoolIDStatus_ParentID")]
    [Index("IsActive", "Status", "AcademicYearID", Name = "IDX_Students_IsActive__Status__AcademicYearID_AdmissionNumber__FirstName__MiddleName__LastName")]
    [Index("IsActive", "Status", "SchoolID", Name = "IDX_Students_IsActive__Status__SchoolID_ClassID__SectionID__GenderID__AcademicYearID__SecondLangID_")]
    [Index("ParentID", Name = "IDX_Students_ParentID_AdmissionNumber__RollNumber__ClassID__GradeID__SectionID__FirstName__MiddleNa")]
    [Index("ParentID", Name = "IDX_Students_ParentID_ClassID__SectionID__AcademicYearID")]
    [Index("ParentID", "IsActive", Name = "IDX_Students_ParentID__IsActive_AdmissionNumber__RollNumber__ClassID__GradeID__SectionID__FirstName")]
    [Index("SchoolID", "AcademicYearID", Name = "IDX_Students_SchoolID_AcademicYearID")]
    [Index("SchoolID", Name = "IDX_Students_SchoolID_AdmissionNumber__FirstName__MiddleName__LastName__IsActive__Status")]
    [Index("SchoolID", Name = "IDX_Students_SchoolID_IsActive__Status")]
    [Index("SchoolID", "AcademicYearID", Name = "IDX_Students_SchoolID__AcademicYearID_AdmissionNumber__ClassID__SectionID__FirstName__MiddleName__L")]
    [Index("LoginID", Name = "_dta_index_Students_7_1620304932__K32_4_6_73")]
    [Index("SchoolID", Name = "idx_StudentsSchoolID")]
    [Index("SchoolID", Name = "idx_StudentsSchoolIDInclAcademic")]
    [Index("SectionID", "IsActive", Name = "idx_StudentsSectionIDIsActive")]
    [Index("ClassID", "IsActive", Name = "idx_StudentsSectionIDIsActiveIncludeSectionID")]
    [Index("LoginID", Name = "idx_student_LogIn")]
    [Index("IsActive", "AcademicYearID", Name = "idx_studentisActAcademic")]
    public partial class Student
    {
        public Student()
        {
            AllergyStudentMaps = new HashSet<AllergyStudentMap>();
            AssignmentDocuments = new HashSet<AssignmentDocument>();
            CampusTransfers = new HashSet<CampusTransfer>();
            Candidates = new HashSet<Candidate>();
            CounselorHubAttachmentMaps = new HashSet<CounselorHubAttachmentMap>();
            CounselorHubMaps = new HashSet<CounselorHubMap>();
            Customers = new HashSet<Customer>();
            DriverScheduleLogs = new HashSet<DriverScheduleLog>();
            FeeCollections = new HashSet<FeeCollection>();
            FinalSettlements = new HashSet<FinalSettlement>();
            FineMasterStudentMaps = new HashSet<FineMasterStudentMap>();
            HealthEntryStudentMaps = new HashSet<HealthEntryStudentMap>();
            LibraryStudentRegisters = new HashSet<LibraryStudentRegister>();
            LibraryTransactions = new HashSet<LibraryTransaction>();
            MarkRegisters = new HashSet<MarkRegister>();
            MeetingRequests = new HashSet<MeetingRequest>();
            PackageConfigStudentMaps = new HashSet<PackageConfigStudentMap>();
            ParentStudentMaps = new HashSet<ParentStudentMap>();
            ProductStudentMaps = new HashSet<ProductStudentMap>();
            ProgressReports = new HashSet<ProgressReport>();
            Refunds = new HashSet<Refund>();
            RemarksEntryStudentMaps = new HashSet<RemarksEntryStudentMap>();
            SchoolCreditNotes = new HashSet<SchoolCreditNote>();
            SchoolPollAnswerLogs = new HashSet<SchoolPollAnswerLog>();
            ShoppingCart1 = new HashSet<ShoppingCart1>();
            SignupAudienceMaps = new HashSet<SignupAudienceMap>();
            SignupSlotAllocationMaps = new HashSet<SignupSlotAllocationMap>();
            Signups = new HashSet<Signup>();
            StudentAchievements = new HashSet<StudentAchievement>();
            StudentApplicationDocumentMaps = new HashSet<StudentApplicationDocumentMap>();
            StudentApplicationSiblingMaps = new HashSet<StudentApplicationSiblingMap>();
            StudentAssignmentMaps = new HashSet<StudentAssignmentMap>();
            StudentAttendences = new HashSet<StudentAttendence>();
            StudentClassHistoryMaps = new HashSet<StudentClassHistoryMap>();
            StudentFeeDues = new HashSet<StudentFeeDue>();
            StudentGroupMaps = new HashSet<StudentGroupMap>();
            StudentLeaveApplications = new HashSet<StudentLeaveApplication>();
            StudentMiscDetails = new HashSet<StudentMiscDetail>();
            StudentPassportDetails = new HashSet<StudentPassportDetail>();
            StudentPickLogs = new HashSet<StudentPickLog>();
            StudentPickerStudentMaps = new HashSet<StudentPickerStudentMap>();
            StudentPromotionLogs = new HashSet<StudentPromotionLog>();
            StudentRouteStopMapLogs = new HashSet<StudentRouteStopMapLog>();
            StudentRouteStopMaps = new HashSet<StudentRouteStopMap>();
            StudentSiblingMapSiblings = new HashSet<StudentSiblingMap>();
            StudentSiblingMapStudents = new HashSet<StudentSiblingMap>();
            StudentSkillRegisters = new HashSet<StudentSkillRegister>();
            StudentStaffMaps = new HashSet<StudentStaffMap>();
            StudentStreamOptionalSubjectMaps = new HashSet<StudentStreamOptionalSubjectMap>();
            StudentTransferRequests = new HashSet<StudentTransferRequest>();
            TransactionHeads = new HashSet<TransactionHead>();
            TransportApplctnStudentMaps = new HashSet<TransportApplctnStudentMap>();
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
        [Column(TypeName = "datetime")]
        public DateTime? DateOfBirth { get; set; }
        public int? StudentCategoryID { get; set; }
        public byte? CastID { get; set; }
        public byte? RelegionID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string MobileNumber { get; set; }
        [StringLength(50)]
        public string EmailID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AdmissionDate { get; set; }
        [StringLength(500)]
        public string StudentProfile { get; set; }
        public int? BloodGroupID { get; set; }
        public int? StudentHouseID { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string Height { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string Weight { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AsOnDate { get; set; }
        public int? HostelID { get; set; }
        public long? HostelRoomID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public long? Account_Sub_ledger_ID { get; set; }
        public long? LoginID { get; set; }
        public long? ParentID { get; set; }
        public bool? IsActive { get; set; }
        /// <summary>
        /// 1-Active
        /// 2-Transferred
        /// 3-Discontinue
        /// 
        /// </summary>
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
        [Column(TypeName = "datetime")]
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
        [Column(TypeName = "datetime")]
        public DateTime? InactiveDate { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("StudentAcademicYears")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ApplicationID")]
        [InverseProperty("Students")]
        public virtual StudentApplication Application { get; set; }
        [ForeignKey("BloodGroupID")]
        [InverseProperty("Students")]
        public virtual BloodGroup BloodGroup { get; set; }
        [ForeignKey("CastID")]
        [InverseProperty("Students")]
        public virtual Cast Cast { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("StudentClasses")]
        public virtual Class Class { get; set; }
        [ForeignKey("CommunityID")]
        [InverseProperty("Students")]
        public virtual Community Community { get; set; }
        [ForeignKey("CurrentCountryID")]
        [InverseProperty("StudentCurrentCountries")]
        public virtual Country CurrentCountry { get; set; }
        [ForeignKey("GenderID")]
        [InverseProperty("Students")]
        public virtual Gender Gender { get; set; }
        [ForeignKey("GradeID")]
        [InverseProperty("Students")]
        public virtual Grade Grade { get; set; }
        [ForeignKey("HostelID")]
        [InverseProperty("Students")]
        public virtual Hostel Hostel { get; set; }
        [ForeignKey("HostelRoomID")]
        [InverseProperty("Students")]
        public virtual HostelRoom HostelRoom { get; set; }
        [ForeignKey("LoginID")]
        [InverseProperty("Students")]
        public virtual Login Login { get; set; }
        [ForeignKey("ParentID")]
        [InverseProperty("Students")]
        public virtual Parent Parent { get; set; }
        [ForeignKey("PermenentCountryID")]
        [InverseProperty("StudentPermenentCountries")]
        public virtual Country PermenentCountry { get; set; }
        [ForeignKey("PreviousSchoolClassCompletedID")]
        [InverseProperty("StudentPreviousSchoolClassCompleteds")]
        public virtual Class PreviousSchoolClassCompleted { get; set; }
        [ForeignKey("PreviousSchoolSyllabusID")]
        [InverseProperty("Students")]
        public virtual Syllabu PreviousSchoolSyllabus { get; set; }
        [ForeignKey("PrimaryContactID")]
        [InverseProperty("Students")]
        public virtual GuardianType PrimaryContact { get; set; }
        [ForeignKey("RelegionID")]
        [InverseProperty("Students")]
        public virtual Relegion Relegion { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("Students")]
        public virtual School School { get; set; }
        [ForeignKey("SchoolAcademicyearID")]
        [InverseProperty("StudentSchoolAcademicyears")]
        public virtual AcademicYear SchoolAcademicyear { get; set; }
        [ForeignKey("SecondLangID")]
        [InverseProperty("StudentSecondLangs")]
        public virtual Subject SecondLang { get; set; }
        [ForeignKey("SecoundLanguageID")]
        [InverseProperty("StudentSecoundLanguages")]
        public virtual Language SecoundLanguage { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("Students")]
        public virtual Section Section { get; set; }
        [ForeignKey("StreamID")]
        [InverseProperty("Students")]
        public virtual Stream Stream { get; set; }
        [ForeignKey("StudentCategoryID")]
        [InverseProperty("Students")]
        public virtual StudentCategory StudentCategory { get; set; }
        [ForeignKey("StudentHouseID")]
        [InverseProperty("Students")]
        public virtual StudentHous StudentHouse { get; set; }
        [ForeignKey("SubjectMapID")]
        [InverseProperty("StudentSubjectMaps")]
        public virtual Subject SubjectMap { get; set; }
        [ForeignKey("ThirdLangID")]
        [InverseProperty("StudentThirdLangs")]
        public virtual Subject ThirdLang { get; set; }
        [ForeignKey("ThridLanguageID")]
        [InverseProperty("StudentThridLanguages")]
        public virtual Language ThridLanguage { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<AllergyStudentMap> AllergyStudentMaps { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<AssignmentDocument> AssignmentDocuments { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<CampusTransfer> CampusTransfers { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<Candidate> Candidates { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<CounselorHubAttachmentMap> CounselorHubAttachmentMaps { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<CounselorHubMap> CounselorHubMaps { get; set; }
        [InverseProperty("DefaultStudent")]
        public virtual ICollection<Customer> Customers { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<DriverScheduleLog> DriverScheduleLogs { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<FeeCollection> FeeCollections { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<FinalSettlement> FinalSettlements { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<FineMasterStudentMap> FineMasterStudentMaps { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<HealthEntryStudentMap> HealthEntryStudentMaps { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<LibraryStudentRegister> LibraryStudentRegisters { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<LibraryTransaction> LibraryTransactions { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<MarkRegister> MarkRegisters { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<MeetingRequest> MeetingRequests { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<PackageConfigStudentMap> PackageConfigStudentMaps { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<ParentStudentMap> ParentStudentMaps { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<ProductStudentMap> ProductStudentMaps { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<ProgressReport> ProgressReports { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<Refund> Refunds { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<RemarksEntryStudentMap> RemarksEntryStudentMaps { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<SchoolCreditNote> SchoolCreditNotes { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<SchoolPollAnswerLog> SchoolPollAnswerLogs { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<ShoppingCart1> ShoppingCart1 { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<SignupAudienceMap> SignupAudienceMaps { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<SignupSlotAllocationMap> SignupSlotAllocationMaps { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<Signup> Signups { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<StudentAchievement> StudentAchievements { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<StudentApplicationDocumentMap> StudentApplicationDocumentMaps { get; set; }
        [InverseProperty("Sibling")]
        public virtual ICollection<StudentApplicationSiblingMap> StudentApplicationSiblingMaps { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<StudentAssignmentMap> StudentAssignmentMaps { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<StudentAttendence> StudentAttendences { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<StudentClassHistoryMap> StudentClassHistoryMaps { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<StudentFeeDue> StudentFeeDues { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<StudentGroupMap> StudentGroupMaps { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<StudentLeaveApplication> StudentLeaveApplications { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<StudentMiscDetail> StudentMiscDetails { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<StudentPassportDetail> StudentPassportDetails { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<StudentPickLog> StudentPickLogs { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<StudentPickerStudentMap> StudentPickerStudentMaps { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<StudentPromotionLog> StudentPromotionLogs { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<StudentRouteStopMapLog> StudentRouteStopMapLogs { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<StudentRouteStopMap> StudentRouteStopMaps { get; set; }
        [InverseProperty("Sibling")]
        public virtual ICollection<StudentSiblingMap> StudentSiblingMapSiblings { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<StudentSiblingMap> StudentSiblingMapStudents { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<StudentSkillRegister> StudentSkillRegisters { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<StudentStaffMap> StudentStaffMaps { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<StudentStreamOptionalSubjectMap> StudentStreamOptionalSubjectMaps { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<StudentTransferRequest> StudentTransferRequests { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<TransportApplctnStudentMap> TransportApplctnStudentMaps { get; set; }
    }
}

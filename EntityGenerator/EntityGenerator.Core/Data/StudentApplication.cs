using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentApplications", Schema = "schools")]
    [Index("LoginID", Name = "IDX_StudentApplications_LoginID_")]
    [Index("SchoolID", Name = "IDX_StudentApplications_SchoolID_ApplicationStatusID")]
    [Index("ApplicationIID", "ApplicationNumber", "LoginID", "ClassID", "FirstName", "MiddleName", "LastName", "GenderID", "DateOfBirth", "MobileNumber", "CreatedDate", "SchoolID", "ProspectNumber", Name = "NonClusteredIndex-Application")]
    [Index("SchoolAcademicyearID", Name = "idx_StudentApplAcademic")]
    [Index("ProspectNumber", Name = "idx_StudentApplicationsProspectNumber")]
    [Index("SchoolID", "SchoolAcademicyearID", Name = "idx_StudentApplicationsSchoolIDSchoolAcademicyearID")]
    public partial class StudentApplication
    {
        public StudentApplication()
        {
            Candidates = new HashSet<Candidate>();
            StudentApplicationDocumentMaps = new HashSet<StudentApplicationDocumentMap>();
            StudentApplicationOptionalSubjectMaps = new HashSet<StudentApplicationOptionalSubjectMap>();
            StudentApplicationSiblingMaps = new HashSet<StudentApplicationSiblingMap>();
            Students = new HashSet<Student>();
        }

        [Key]
        public long ApplicationIID { get; set; }
        [StringLength(50)]
        public string ApplicationNumber { get; set; }
        public long? LoginID { get; set; }
        public int? ClassID { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string MiddleName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        public byte? GenderID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfBirth { get; set; }
        public byte? CastID { get; set; }
        public byte? RelegionID { get; set; }
        [StringLength(15)]
        [Unicode(false)]
        public string MobileNumber { get; set; }
        [StringLength(50)]
        public string EmailID { get; set; }
        public long? ProfileContentID { get; set; }
        [StringLength(200)]
        public string ParmenentAddress { get; set; }
        [StringLength(100)]
        public string CurrentAddress { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? ApplicationStatusID { get; set; }
        public byte? SchoolID { get; set; }
        public int? NationalityID { get; set; }
        [StringLength(15)]
        public string FatherNationalID { get; set; }
        [StringLength(50)]
        public string FatherFirstName { get; set; }
        [StringLength(50)]
        public string FatherMiddleName { get; set; }
        [StringLength(50)]
        public string FatherLastName { get; set; }
        public int? FatherCountryID { get; set; }
        [StringLength(20)]
        public string FatherPassportNumber { get; set; }
        [StringLength(15)]
        public string MotherNationalID { get; set; }
        [StringLength(50)]
        public string MotherFirstName { get; set; }
        [StringLength(50)]
        public string MotherMiddleName { get; set; }
        [StringLength(50)]
        public string MotherLastName { get; set; }
        public int? MotherCountryID { get; set; }
        [StringLength(20)]
        public string MotherPassportNumber { get; set; }
        [StringLength(25)]
        public string FatherOccupation { get; set; }
        public int? CountryID { get; set; }
        public int? StudentCategoryID { get; set; }
        [StringLength(100)]
        public string PreviousSchoolName { get; set; }
        public byte? PreviousSchoolSyllabusID { get; set; }
        [StringLength(15)]
        public string PreviousSchoolAcademicYear { get; set; }
        public int? PreviousSchoolClassCompletedID { get; set; }
        [StringLength(25)]
        public string StudentPassportNo { get; set; }
        [StringLength(25)]
        public string StudentNationalID { get; set; }
        [StringLength(15)]
        public string MotherMobileNumber { get; set; }
        [StringLength(50)]
        public string MotherEmailID { get; set; }
        public byte? MotherStudentRelationShipID { get; set; }
        public byte? FatherStudentRelationShipID { get; set; }
        public int? SchoolAcademicyearID { get; set; }
        [StringLength(25)]
        public string MotherOccupation { get; set; }
        [StringLength(20)]
        public string BuildingNo { get; set; }
        [StringLength(20)]
        public string FlatNo { get; set; }
        [StringLength(20)]
        public string StreetNo { get; set; }
        [StringLength(50)]
        public string StreetName { get; set; }
        [StringLength(20)]
        public string LocationNo { get; set; }
        [StringLength(50)]
        public string LocationName { get; set; }
        [StringLength(20)]
        public string ZipNo { get; set; }
        [StringLength(50)]
        public string City { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StudentNationalIDNoExpiryDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StudentNationalIDNoIssueDate { get; set; }
        public long? StudentVisaDetailNoID { get; set; }
        public long? FatherVisaDetailNoID { get; set; }
        public long? MotherVisaDetailNoID { get; set; }
        public long? StudentPassportDetailNoID { get; set; }
        public long? FatherPassportDetailNoID { get; set; }
        public long? MotherPassportDetailNoID { get; set; }
        public byte? CommunityID { get; set; }
        [StringLength(12)]
        public string AdhaarCardNo { get; set; }
        public bool? IsMinority { get; set; }
        public bool? IsOnlyChildofParent { get; set; }
        public int? BloodGroupID { get; set; }
        [StringLength(15)]
        public string FatherMobileNumberTwo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FatherNationalDNoIssueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FatherNationalDNoExpiryDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MotherNationalDNoIssueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MotherNationaIDNoExpiryDate { get; set; }
        public bool? IsStudentStudiedBefore { get; set; }
        public int? StudentCoutryOfBrithID { get; set; }
        public byte? CurriculamID { get; set; }
        public int? SecoundLanguageID { get; set; }
        public int? ThridLanguageID { get; set; }
        [StringLength(25)]
        public string PostBoxNo { get; set; }
        [StringLength(100)]
        public string FatherCompanyName { get; set; }
        [StringLength(100)]
        public string MotherCompanyName { get; set; }
        public int? CanYouVolunteerToHelpOneID { get; set; }
        public int? CanYouVolunteerToHelpTwoID { get; set; }
        [StringLength(250)]
        public string PreviousSchoolAddress { get; set; }
        public byte? PrimaryContactID { get; set; }
        public int? ApplicationTypeID { get; set; }
        [StringLength(20)]
        public string ProspectNumber { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ProspectFee { get; set; }
        [StringLength(500)]
        public string Remarks { get; set; }
        public int? SecondLangID { get; set; }
        public int? ThirdLangID { get; set; }
        [StringLength(50)]
        public string GuardianNationalID { get; set; }
        [StringLength(100)]
        public string GuardianFirstName { get; set; }
        [StringLength(100)]
        public string GuardianMiddleName { get; set; }
        [StringLength(100)]
        public string GuardianLastName { get; set; }
        public int? GuardianNationalityID { get; set; }
        [StringLength(200)]
        public string GuardianOccupation { get; set; }
        public byte? GuardianStudentRelationShipID { get; set; }
        public long? GuardianVisaDetailNoID { get; set; }
        public long? GuardianPassportDetailNoID { get; set; }
        [StringLength(20)]
        public string GuardianMobileNumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? GuardianNationalIDNoIssueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? GuardianNationalIDNoExpiryDate { get; set; }
        [StringLength(250)]
        public string GuardianCompanyName { get; set; }
        [StringLength(250)]
        public string GuardianEmailID { get; set; }
        public byte? StreamID { get; set; }
        public byte? StreamGroupID { get; set; }
        [StringLength(30)]
        public string GuardianWhatsappMobileNo { get; set; }
        [StringLength(30)]
        public string FatherWhatsappMobileNo { get; set; }
        [StringLength(30)]
        public string MotherWhatsappMobileNo { get; set; }

        [ForeignKey("ApplicationStatusID")]
        [InverseProperty("StudentApplications")]
        public virtual ApplicationStatus ApplicationStatus { get; set; }
        [ForeignKey("ApplicationTypeID")]
        [InverseProperty("StudentApplications")]
        public virtual ApplicationSubmitType ApplicationType { get; set; }
        [ForeignKey("CanYouVolunteerToHelpOneID")]
        [InverseProperty("StudentApplicationCanYouVolunteerToHelpOnes")]
        public virtual VolunteerType CanYouVolunteerToHelpOne { get; set; }
        [ForeignKey("CanYouVolunteerToHelpTwoID")]
        [InverseProperty("StudentApplicationCanYouVolunteerToHelpTwoes")]
        public virtual VolunteerType CanYouVolunteerToHelpTwo { get; set; }
        [ForeignKey("CastID")]
        [InverseProperty("StudentApplications")]
        public virtual Cast Cast { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("StudentApplicationClasses")]
        public virtual Class Class { get; set; }
        [ForeignKey("CountryID")]
        [InverseProperty("StudentApplicationCountries")]
        public virtual Country Country { get; set; }
        [ForeignKey("CurriculamID")]
        [InverseProperty("StudentApplicationCurriculams")]
        public virtual Syllabu Curriculam { get; set; }
        [ForeignKey("FatherCountryID")]
        [InverseProperty("StudentApplicationFatherCountries")]
        public virtual Nationality FatherCountry { get; set; }
        [ForeignKey("FatherPassportDetailNoID")]
        [InverseProperty("StudentApplicationFatherPassportDetailNoes")]
        public virtual PassportDetailMap FatherPassportDetailNo { get; set; }
        [ForeignKey("FatherStudentRelationShipID")]
        [InverseProperty("StudentApplicationFatherStudentRelationShips")]
        public virtual GuardianType FatherStudentRelationShip { get; set; }
        [ForeignKey("FatherVisaDetailNoID")]
        [InverseProperty("StudentApplicationFatherVisaDetailNoes")]
        public virtual VisaDetailMap FatherVisaDetailNo { get; set; }
        [ForeignKey("GenderID")]
        [InverseProperty("StudentApplications")]
        public virtual Gender Gender { get; set; }
        [ForeignKey("GuardianNationalityID")]
        [InverseProperty("StudentApplicationGuardianNationalities")]
        public virtual Nationality GuardianNationality { get; set; }
        [ForeignKey("GuardianPassportDetailNoID")]
        [InverseProperty("StudentApplicationGuardianPassportDetailNoes")]
        public virtual PassportDetailMap GuardianPassportDetailNo { get; set; }
        [ForeignKey("GuardianStudentRelationShipID")]
        [InverseProperty("StudentApplicationGuardianStudentRelationShips")]
        public virtual GuardianType GuardianStudentRelationShip { get; set; }
        [ForeignKey("GuardianVisaDetailNoID")]
        [InverseProperty("StudentApplicationGuardianVisaDetailNoes")]
        public virtual VisaDetailMap GuardianVisaDetailNo { get; set; }
        [ForeignKey("LoginID")]
        [InverseProperty("StudentApplications")]
        public virtual Login Login { get; set; }
        [ForeignKey("MotherCountryID")]
        [InverseProperty("StudentApplicationMotherCountries")]
        public virtual Nationality MotherCountry { get; set; }
        [ForeignKey("MotherPassportDetailNoID")]
        [InverseProperty("StudentApplicationMotherPassportDetailNoes")]
        public virtual PassportDetailMap MotherPassportDetailNo { get; set; }
        [ForeignKey("MotherStudentRelationShipID")]
        [InverseProperty("StudentApplicationMotherStudentRelationShips")]
        public virtual GuardianType MotherStudentRelationShip { get; set; }
        [ForeignKey("MotherVisaDetailNoID")]
        [InverseProperty("StudentApplicationMotherVisaDetailNoes")]
        public virtual VisaDetailMap MotherVisaDetailNo { get; set; }
        [ForeignKey("NationalityID")]
        [InverseProperty("StudentApplicationNationalities")]
        public virtual Nationality Nationality { get; set; }
        [ForeignKey("PreviousSchoolClassCompletedID")]
        [InverseProperty("StudentApplicationPreviousSchoolClassCompleteds")]
        public virtual Class PreviousSchoolClassCompleted { get; set; }
        [ForeignKey("PreviousSchoolSyllabusID")]
        [InverseProperty("StudentApplicationPreviousSchoolSyllabus")]
        public virtual Syllabu PreviousSchoolSyllabus { get; set; }
        [ForeignKey("PrimaryContactID")]
        [InverseProperty("StudentApplicationPrimaryContacts")]
        public virtual GuardianType PrimaryContact { get; set; }
        [ForeignKey("RelegionID")]
        [InverseProperty("StudentApplications")]
        public virtual Relegion Relegion { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("StudentApplications")]
        public virtual School School { get; set; }
        [ForeignKey("SchoolAcademicyearID")]
        [InverseProperty("StudentApplications")]
        public virtual AcademicYear SchoolAcademicyear { get; set; }
        [ForeignKey("SecondLangID")]
        [InverseProperty("StudentApplicationSecondLangs")]
        public virtual Subject SecondLang { get; set; }
        [ForeignKey("SecoundLanguageID")]
        [InverseProperty("StudentApplicationSecoundLanguages")]
        public virtual Language SecoundLanguage { get; set; }
        [ForeignKey("StreamID")]
        [InverseProperty("StudentApplications")]
        public virtual Stream Stream { get; set; }
        [ForeignKey("StreamGroupID")]
        [InverseProperty("StudentApplications")]
        public virtual StreamGroup StreamGroup { get; set; }
        [ForeignKey("StudentCategoryID")]
        [InverseProperty("StudentApplications")]
        public virtual StudentCategory StudentCategory { get; set; }
        [ForeignKey("StudentCoutryOfBrithID")]
        [InverseProperty("StudentApplicationStudentCoutryOfBriths")]
        public virtual Country StudentCoutryOfBrith { get; set; }
        [ForeignKey("StudentPassportDetailNoID")]
        [InverseProperty("StudentApplicationStudentPassportDetailNoes")]
        public virtual PassportDetailMap StudentPassportDetailNo { get; set; }
        [ForeignKey("StudentVisaDetailNoID")]
        [InverseProperty("StudentApplicationStudentVisaDetailNoes")]
        public virtual VisaDetailMap StudentVisaDetailNo { get; set; }
        [ForeignKey("ThirdLangID")]
        [InverseProperty("StudentApplicationThirdLangs")]
        public virtual Subject ThirdLang { get; set; }
        [ForeignKey("ThridLanguageID")]
        [InverseProperty("StudentApplicationThridLanguages")]
        public virtual Language ThridLanguage { get; set; }
        [InverseProperty("StudentApplication")]
        public virtual ICollection<Candidate> Candidates { get; set; }
        [InverseProperty("Application")]
        public virtual ICollection<StudentApplicationDocumentMap> StudentApplicationDocumentMaps { get; set; }
        [InverseProperty("Application")]
        public virtual ICollection<StudentApplicationOptionalSubjectMap> StudentApplicationOptionalSubjectMaps { get; set; }
        [InverseProperty("Application")]
        public virtual ICollection<StudentApplicationSiblingMap> StudentApplicationSiblingMaps { get; set; }
        [InverseProperty("Application")]
        public virtual ICollection<Student> Students { get; set; }
    }
}

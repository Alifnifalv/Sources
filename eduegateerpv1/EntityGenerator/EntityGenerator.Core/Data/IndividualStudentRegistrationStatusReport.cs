using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class IndividualStudentRegistrationStatusReport
    {
        public long StudentIID { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [StringLength(50)]
        public string RollNumber { get; set; }
        [StringLength(500)]
        public string StudentProfile { get; set; }
        public byte[] StudentPhoto { get; set; }
        public int? ClassID { get; set; }
        [StringLength(50)]
        public string ClassName { get; set; }
        public int? SectionID { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        [StringLength(502)]
        public string StudentName { get; set; }
        public byte? GenderID { get; set; }
        [StringLength(50)]
        public string Gender { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string DateOfBirth { get; set; }
        public byte? CastID { get; set; }
        [StringLength(50)]
        public string Cast { get; set; }
        public byte? RelegionID { get; set; }
        [StringLength(50)]
        public string Relegion { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string MobileNumber { get; set; }
        [StringLength(50)]
        public string EmailID { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string AdmissionDate { get; set; }
        public int? BloodGroupID { get; set; }
        [StringLength(100)]
        public string BloodGroup { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string Height { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string Weight { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string AsOnDate { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string CreatedDate { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string UpdatedDate { get; set; }
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
        [StringLength(50)]
        public string CurrentCountryName { get; set; }
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
        [StringLength(50)]
        public string PermenentCountryName { get; set; }
        public bool? IsAddressIsPermenentAddress { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string FeeStartDate { get; set; }
        public byte? CommunityID { get; set; }
        [StringLength(50)]
        public string Community { get; set; }
        public bool? IsMinority { get; set; }
        public bool? IsOnlyChildofParent { get; set; }
        public byte? PrimaryContactID { get; set; }
        [StringLength(50)]
        public string PrimatyContact { get; set; }
        public int? SecondLangID { get; set; }
        [StringLength(500)]
        public string SecoundLanguage { get; set; }
        public int? ThirdLangID { get; set; }
        [StringLength(500)]
        public string ThridLanguage { get; set; }
        public bool? IsStudentStudiedBefore { get; set; }
        [StringLength(200)]
        public string PreviousSchoolName { get; set; }
        public byte? PreviousSchoolSyllabusID { get; set; }
        [StringLength(50)]
        public string PreviousSchoolSyllabusName { get; set; }
        [StringLength(15)]
        public string PreviousSchoolAcademicYear { get; set; }
        public int? PreviousSchoolClassCompletedID { get; set; }
        [StringLength(50)]
        public string PreviousSchoolClassCompletedName { get; set; }
        [StringLength(250)]
        public string PreviousSchoolAddress { get; set; }
        public long? ApplicationID { get; set; }
        public int? NationalityID { get; set; }
        [StringLength(50)]
        public string NationalityName { get; set; }
        [StringLength(20)]
        public string PassportNo { get; set; }
        public int? CountryofIssueID { get; set; }
        [StringLength(50)]
        public string CountryOfIssueName { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string PassportNoExpiry { get; set; }
        public int? CountryofBirthID { get; set; }
        [StringLength(50)]
        public string CountryOfBirthName { get; set; }
        [StringLength(20)]
        public string VisaNo { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string VisaExpiry { get; set; }
        [StringLength(20)]
        public string NationalIDNo { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string NationalIDNoExpiry { get; set; }
        [StringLength(12)]
        public string AdhaarCardNo { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string StudentNationalIDNoIssueDate { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string StudentPassportNoIssueDate { get; set; }
        [StringLength(100)]
        public string BuildingNo { get; set; }
        [StringLength(100)]
        public string FlatNo { get; set; }
        [StringLength(100)]
        public string StreetNo { get; set; }
        [StringLength(100)]
        public string StreetName { get; set; }
        [StringLength(100)]
        public string LocationNo { get; set; }
        [StringLength(100)]
        public string LocationName { get; set; }
        [StringLength(100)]
        public string ZipNo { get; set; }
        [StringLength(100)]
        public string City { get; set; }
        [StringLength(100)]
        public string PostBoxNo { get; set; }
        public int? CountryID { get; set; }
        [StringLength(50)]
        public string Country { get; set; }
        [StringLength(302)]
        public string FatherName { get; set; }
        public int? FatherCountryID { get; set; }
        [StringLength(50)]
        public string FatherCountry { get; set; }
        [StringLength(100)]
        public string FatherPassportNumber { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string FatherPassportNoIssueDate { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string FatherPassportNoExpiryDate { get; set; }
        public int? FatherPassportCountryofIssueID { get; set; }
        public int? FatherPassportCountryofIssue { get; set; }
        [StringLength(100)]
        public string FatherNationalID { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string FatherNationalDNoIssueDate { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string FatherNationalDNoExpiryDate { get; set; }
        [StringLength(50)]
        public string FatherOccupation { get; set; }
        public byte? GuardianTypeID { get; set; }
        [StringLength(50)]
        public string GuardianType { get; set; }
        [StringLength(500)]
        public string GaurdianEmail { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string GuardianPhone { get; set; }
        [StringLength(100)]
        public string FatherMobileNumberTwo { get; set; }
        [StringLength(100)]
        public string FatherCompanyName { get; set; }
        public int? CanYouVolunteerToHelpOneID { get; set; }
        [StringLength(25)]
        public string FatherCanYouVolunteerToHelp { get; set; }
        [StringLength(302)]
        public string MotherName { get; set; }
        public int? MotherCountryID { get; set; }
        [StringLength(50)]
        public string MotherCountry { get; set; }
        [StringLength(100)]
        public string MotherPassportNumber { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string MotherPassportNoIssueDate { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string MotherPassportNoExpiryDate { get; set; }
        public int? MotherPassportCountryofIssueID { get; set; }
        [StringLength(50)]
        public string MotherPassportCountryofIssue { get; set; }
        [StringLength(100)]
        public string MotherNationalID { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string MotherNationalDNoIssueDate { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string MotherNationalDNoExpiryDate { get; set; }
        public byte? MotherStudentRelationShipID { get; set; }
        [StringLength(50)]
        public string MotherStudentRelationShip { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string MotherPhone { get; set; }
        [StringLength(50)]
        public string MotherOccupation { get; set; }
        [StringLength(100)]
        public string MotherEmailID { get; set; }
        [StringLength(100)]
        public string MotherCompanyName { get; set; }
        public int? CanYouVolunteerToHelpTwoID { get; set; }
        [StringLength(25)]
        public string MotherCanYouVolunteerToHelp { get; set; }
        [Required]
        [StringLength(302)]
        public string GuardianName { get; set; }
        [StringLength(50)]
        public string GuardianPassportNumber { get; set; }
        [StringLength(50)]
        public string GuradianCountryofIssue { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? GuardianPassportNoExpiryDate { get; set; }
        [StringLength(50)]
        public string GuardianNationalID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? GuardianNationalIDNoExpiryDate { get; set; }
        [StringLength(50)]
        public string GuardianMobileNumber { get; set; }
        [StringLength(500)]
        public string EmailGuardian { get; set; }
    }
}

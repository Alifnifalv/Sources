using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentApplicationStatusReportView
    {
        public long ApplicationIID { get; set; }
        [StringLength(50)]
        public string ApplicationNumber { get; set; }
        public int? ClassID { get; set; }
        [StringLength(50)]
        public string ClassName { get; set; }
        public byte[] StudentImg { get; set; }
        [Required]
        [StringLength(152)]
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
        [StringLength(15)]
        [Unicode(false)]
        public string MobileNumber { get; set; }
        [StringLength(50)]
        public string EmailID { get; set; }
        public byte? ApplicationStatusID { get; set; }
        [StringLength(50)]
        public string ApplicationStatus { get; set; }
        public byte? SchoolID { get; set; }
        [StringLength(50)]
        public string School { get; set; }
        public int? NationalityID { get; set; }
        [StringLength(50)]
        public string StudentNationality { get; set; }
        [StringLength(15)]
        public string FatherNationalID { get; set; }
        [StringLength(152)]
        public string FatherName { get; set; }
        public int? FatherCountryID { get; set; }
        [StringLength(50)]
        public string FatherNationality { get; set; }
        [StringLength(20)]
        public string FatherPassportNumber { get; set; }
        [StringLength(15)]
        public string MotherNationalID { get; set; }
        [StringLength(152)]
        public string MotherName { get; set; }
        public int? MotherCountryID { get; set; }
        [StringLength(50)]
        public string MotherNationality { get; set; }
        [StringLength(20)]
        public string MotherPassportNumber { get; set; }
        [StringLength(25)]
        public string FatherOccupation { get; set; }
        public int? CountryID { get; set; }
        [StringLength(50)]
        public string CountryName { get; set; }
        public int? StudentCategoryID { get; set; }
        [StringLength(50)]
        public string StudentCategory { get; set; }
        [StringLength(100)]
        public string PreviousSchoolName { get; set; }
        public byte? PreviousSchoolSyllabusID { get; set; }
        [StringLength(50)]
        public string PreviousSchoolSyllanusName { get; set; }
        [StringLength(15)]
        public string PreviousSchoolAcademicYear { get; set; }
        public int? PreviousSchoolClassCompletedID { get; set; }
        [StringLength(50)]
        public string PreviousSchoolClassName { get; set; }
        [StringLength(250)]
        public string PreviousSchoolAddress { get; set; }
        [StringLength(25)]
        public string StudentNationalID { get; set; }
        [StringLength(15)]
        public string MotherMobileNumber { get; set; }
        [StringLength(50)]
        public string MotherEmailID { get; set; }
        public byte? MotherStudentRelationShipID { get; set; }
        [StringLength(50)]
        public string MotherRelationShip { get; set; }
        public byte? FatherStudentRelationShipID { get; set; }
        [StringLength(50)]
        public string FatherRelationShip { get; set; }
        public int? SchoolAcademicyearID { get; set; }
        [StringLength(123)]
        public string SchoolAcademicYearName { get; set; }
        [StringLength(20)]
        public string SchoolAcademicYearCode { get; set; }
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
        [StringLength(10)]
        [Unicode(false)]
        public string StudentNationalIDNoExpiryDate { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string StudentNationalIDNoIssueDate { get; set; }
        public long? StudentVisaDetailNoID { get; set; }
        public long? FatherVisaDetailNoID { get; set; }
        public long? MotherVisaDetailNoID { get; set; }
        public long? StudentPassportDetailNoID { get; set; }
        public long? FatherPassportDetailNoID { get; set; }
        public long? MotherPassportDetailNoID { get; set; }
        public byte? CommunityID { get; set; }
        [StringLength(50)]
        public string CommunityName { get; set; }
        [StringLength(12)]
        public string AdhaarCardNo { get; set; }
        public bool? IsMinority { get; set; }
        public bool? IsOnlyChildofParent { get; set; }
        public int? BloodGroupID { get; set; }
        [StringLength(100)]
        public string BloodGroupName { get; set; }
        [StringLength(15)]
        public string FatherMobileNumberTwo { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string FatherNationalDNoIssueDate { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string FatherNationalDNoExpiryDate { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string MotherNationalDNoIssueDate { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string MotherNationaIDNoExpiryDate { get; set; }
        public bool? IsStudentStudiedBefore { get; set; }
        public int? StudentCoutryOfBrithID { get; set; }
        [StringLength(50)]
        public string StudentCoutryBrith { get; set; }
        public byte? CurriculamID { get; set; }
        [StringLength(50)]
        public string CurriculamName { get; set; }
        public int? SecondLangID { get; set; }
        [StringLength(500)]
        public string Secoundlanuage { get; set; }
        public int? ThirdLangID { get; set; }
        [StringLength(500)]
        public string ThridLanguage { get; set; }
        [StringLength(25)]
        public string PostBoxNo { get; set; }
        [StringLength(100)]
        public string FatherCompanyName { get; set; }
        [StringLength(100)]
        public string MotherCompanyName { get; set; }
        public int? CanYouVolunteerToHelpOneID { get; set; }
        [StringLength(25)]
        public string FatherVolunteer { get; set; }
        public int? CanYouVolunteerToHelpTwoID { get; set; }
        [StringLength(25)]
        public string MotherVolunteer { get; set; }
        public byte? PrimaryContactID { get; set; }
        [StringLength(50)]
        public string PrimaryContactName { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string CreatedDate { get; set; }
        [StringLength(20)]
        public string StudentpassportNo { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string StudentpassportNoExpiryDate { get; set; }
        public int? StudentCountryofIssueID { get; set; }
        [StringLength(50)]
        public string CountryIssueName { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string StudentpassportNoIssueDate { get; set; }
        [StringLength(20)]
        public string FatherpassportNo { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string FatherpassportNoExpriryDate { get; set; }
        public int? FatherCountryofIssueID { get; set; }
        [StringLength(50)]
        public string FatherCountryIssueName { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string FatherpassportNoIssueDate { get; set; }
        [StringLength(20)]
        public string MotherpassportNo { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string MotherpassportNoExpriyDate { get; set; }
        public int? MotherCountryofIssueID { get; set; }
        [StringLength(50)]
        public string MotherCountryIssueName { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string MotherpassportNoIssueDate { get; set; }
        [StringLength(20)]
        public string StudentVisaNo { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string StudentVisaNoExpiryDate { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string StudentVisaNoIssueDate { get; set; }
        [StringLength(20)]
        public string FatherVisaNo { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string FatherVisaNoExpiryDate { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string FatherVisaNoIssueDate { get; set; }
        [StringLength(20)]
        public string MotherVisaNo { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string MotherVisaNoExpiryDate { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string MotherVisaNoIssueDate { get; set; }
    }
}

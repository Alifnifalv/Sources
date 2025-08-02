using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentApplicationSearch
    {
        public long ApplicationIID { get; set; }
        [StringLength(50)]
        public string ApplicationNumber { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        public long? LoginID { get; set; }
        public int? ClassID { get; set; }
        [StringLength(50)]
        public string ClassName { get; set; }
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
        [StringLength(152)]
        public string FirstName { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        [StringLength(50)]
        public string CastDescription { get; set; }
        [StringLength(50)]
        public string RelegionName { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(123)]
        public string AcademicYearCode { get; set; }
        [StringLength(20)]
        public string ProspectNumber { get; set; }
        [StringLength(500)]
        public string Remarks { get; set; }
        [StringLength(50)]
        public string GenderDescription { get; set; }
        public byte? ApplicationStatusID { get; set; }
        [Required]
        [StringLength(50)]
        public string Status { get; set; }
        [Required]
        [StringLength(9)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        public byte? SchoolID { get; set; }
        public int? NationalityID { get; set; }
        [StringLength(50)]
        public string StudentNationality { get; set; }
        [StringLength(15)]
        public string FatherNationalID { get; set; }
        [Required]
        [StringLength(152)]
        public string GaurdianName { get; set; }
        public int? FatherCountryID { get; set; }
        [StringLength(50)]
        public string GaurdianCountry { get; set; }
        [StringLength(15)]
        public string MotherNationalID { get; set; }
        [Required]
        [StringLength(152)]
        public string MotherName { get; set; }
        public int? MotherCountryID { get; set; }
        [StringLength(50)]
        public string MotherCountryName { get; set; }
        [StringLength(25)]
        public string FatherOccupation { get; set; }
        [StringLength(25)]
        public string StudentPassportNo { get; set; }
        public long? StudentPassportDetailNoID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StudentPassportIssueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StudentPassportExpiryDate { get; set; }
        [StringLength(50)]
        public string StudentPassportIssueCountry { get; set; }
        [StringLength(25)]
        public string StudentNationalID { get; set; }
        [StringLength(15)]
        public string MotherMobileNumber { get; set; }
        [StringLength(50)]
        public string MotherEmailID { get; set; }
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
        [StringLength(20)]
        public string StudentVisaNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StudentVisaIssueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StudentVisaExpiryDate { get; set; }
        public long? FatherVisaDetailNoID { get; set; }
        [StringLength(20)]
        public string FatherVisaNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FatherVisaIssueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FatherVisaExpiryDate { get; set; }
        public long? MotherVisaDetailNoID { get; set; }
        [StringLength(20)]
        public string MotherVisaNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MotherVisaIssueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MotherVisaExpiryDate { get; set; }
        [StringLength(20)]
        public string FatherPassportNumber { get; set; }
        public long? FatherPassportDetailNoID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FatherPassportNoIssueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FatherPassportNoExpiryDate { get; set; }
        public long? MotherPassportDetailNoID { get; set; }
        [StringLength(20)]
        public string MotherPassportNumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MotherPassportNoIssueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MotherPassportNoExpiryDate { get; set; }
        [StringLength(12)]
        public string AdhaarCardNo { get; set; }
        public int? BloodGroupID { get; set; }
        [StringLength(100)]
        public string BloodGroupName { get; set; }
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
        public int? StudentCoutryOfBrithID { get; set; }
        [StringLength(50)]
        public string StudentCountryOfBirth { get; set; }
        public int? SecoundLanguageID { get; set; }
        [StringLength(50)]
        public string SecoundLanguage { get; set; }
        public int? ThridLanguageID { get; set; }
        [StringLength(50)]
        public string ThirdLanguage { get; set; }
        [StringLength(25)]
        public string PostBoxNo { get; set; }
        [StringLength(100)]
        public string FatherCompanyName { get; set; }
        [StringLength(100)]
        public string MotherCompanyName { get; set; }
        [Unicode(false)]
        public string Comment { get; set; }
    }
}

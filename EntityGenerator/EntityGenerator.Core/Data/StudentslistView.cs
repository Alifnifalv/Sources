using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentslistView
    {
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        public long StudentIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FeeStartDate { get; set; }
        public int? ClassID { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        public int? SectionID { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        [StringLength(200)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string MiddleName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AdmissionDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ADMDate { get; set; }
        public int? BloodGroupID { get; set; }
        public int? SchoolAcademicyearID { get; set; }
        [StringLength(100)]
        public string BloodGroupName { get; set; }
        [StringLength(200)]
        public string LastName { get; set; }
        public byte? GenderID { get; set; }
        [StringLength(50)]
        public string Gender { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfBirth { get; set; }
        public byte? CastID { get; set; }
        [StringLength(50)]
        public string CastDescription { get; set; }
        public byte? RelegionID { get; set; }
        [StringLength(50)]
        public string RelegionName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string MobileNumber { get; set; }
        [StringLength(50)]
        public string EmailID { get; set; }
        [StringLength(500)]
        public string StudentProfile { get; set; }
        public int? StudentHouseID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string Height { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string Weight { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AsOnDate { get; set; }
        [Required]
        [StringLength(50)]
        public string Nationality { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(123)]
        public string AcademicYear { get; set; }
        [StringLength(20)]
        public string StudentPassport { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StudentPassportExpiry { get; set; }
        [StringLength(50)]
        public string StudentCountryOfBirth { get; set; }
        [StringLength(20)]
        public string StudentVisaNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StudentVisaExpiry { get; set; }
        [StringLength(20)]
        public string StudentNationalID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StudentNatIDExpiry { get; set; }
        [StringLength(12)]
        public string AdhaarCardNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StudentNationalIDNoIssueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StudentPassportNoIssueDate { get; set; }
        public long? ParentID { get; set; }
        [StringLength(10)]
        public string ParentCode { get; set; }
        [Required]
        [StringLength(302)]
        public string GaurdianName { get; set; }
        [StringLength(50)]
        public string FatherOccupation { get; set; }
        [StringLength(500)]
        public string GaurdianEmail { get; set; }
        [StringLength(50)]
        public string FatherCountry { get; set; }
        [StringLength(100)]
        public string FatherPassportNumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FatherPassportNoIssueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FatherPassportNoExpiryDate { get; set; }
        [StringLength(100)]
        public string FatherNationalID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FatherNationalDNoIssueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FatherNationalDNoExpiryDate { get; set; }
        [StringLength(100)]
        public string FatherCompanyName { get; set; }
        [StringLength(50)]
        public string MotherCountry { get; set; }
        [StringLength(100)]
        public string MotherPassportNumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MotherPassportNoIssueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MotherPassportNoExpiryDate { get; set; }
        [StringLength(100)]
        public string MotherNationalID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MotherNationalDNoIssueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MotherNationalDNoExpiryDate { get; set; }
        [StringLength(100)]
        public string MotherEmailID { get; set; }
        [StringLength(100)]
        public string MotherCompanyName { get; set; }
        [StringLength(50)]
        public string GuardianPhone { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string MotherPhone { get; set; }
        [StringLength(50)]
        public string MotherOccupation { get; set; }
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
        [StringLength(50)]
        public string SchoolName { get; set; }
        public long? ApplicationID { get; set; }
        [StringLength(50)]
        public string ApplicationNumber { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        [Required]
        [StringLength(302)]
        public string MotherName { get; set; }
        [Required]
        [StringLength(8)]
        [Unicode(false)]
        public string IsActive { get; set; }
        [Required]
        [StringLength(5)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        [Required]
        [StringLength(3)]
        [Unicode(false)]
        public string IsUsingTransport { get; set; }
        [StringLength(500)]
        public string SecondLanguage { get; set; }
        [StringLength(500)]
        public string ThirdLanguage { get; set; }
        [StringLength(500)]
        public string IscMsc { get; set; }
        [StringLength(50)]
        public string Community { get; set; }
    }
}

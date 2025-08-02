using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("StudentApplications", Schema = "schools")]
    public partial class StudentApplication
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StudentApplication()
        {
            StudentApplicationSiblingMaps = new HashSet<StudentApplicationSiblingMap>();
            Students = new HashSet<Student>();
            StudentApplicationDocumentMaps = new HashSet<StudentApplicationDocumentMap>();
            StudentApplicationOptionalSubjectMaps = new HashSet<StudentApplicationOptionalSubjectMap>();
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

        public DateTime? DateOfBirth { get; set; }

        public byte? CastID { get; set; }

        public byte? RelegionID { get; set; }

        [StringLength(15)]
        public string MobileNumber { get; set; }

        [StringLength(50)]
        public string EmailID { get; set; }

        public long? ProfileContentID { get; set; }

        [StringLength(50)]
        public string ParmenentAddress { get; set; }

        [StringLength(50)]
        public string CurrentAddress { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

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

        public DateTime? StudentNationalIDNoExpiryDate { get; set; }

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

        public DateTime? FatherNationalDNoIssueDate { get; set; }

        public DateTime? FatherNationalDNoExpiryDate { get; set; }

        public DateTime? MotherNationalDNoIssueDate { get; set; }

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

        public string Remarks { get; set; }

        public decimal? ProspectFee { get; set; }

        public int? SecondLangID { get; set; }

        public int? ThirdLangID { get; set; }

        public int? GuardianNationalityID { get; set; }

        public byte? GuardianStudentRelationShipID { get; set; }

        public long? GuardianVisaDetailNoID { get; set; }

        public long? GuardianPassportDetailNoID { get; set; }

        public string GuardianNationalID { get; set; }

        public string GuardianFirstName { get; set; }

        public string GuardianMiddleName { get; set; }

        public string GuardianLastName { get; set; }

        public string GuardianOccupation { get; set; }

        public string GuardianMobileNumber { get; set; }

        public DateTime? GuardianNationalIDNoIssueDate { get; set; }

        public DateTime? GuardianNationalIDNoExpiryDate { get; set; }

        public string GuardianCompanyName { get; set; }
        public string GuardianEmailID { get; set; }
        public byte? StreamID { get; set; }

        public byte? StreamGroupID { get; set; }

        public virtual StreamGroup StreamGroup { get; set; }

        public virtual Stream Stream { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual Subject Subject1 { get; set; }

        public virtual Login Login { get; set; }

        public virtual Schools School { get; set; }

        public virtual Country Country { get; set; }

        public virtual Country Country1 { get; set; }

        public virtual Gender Gender { get; set; }

        public virtual Nationality Nationality { get; set; }

        public virtual Nationality Nationality1 { get; set; }

        public virtual Nationality Nationality2 { get; set; }

        public virtual Nationality Nationality3 { get; set; }

        public virtual Language Language { get; set; }

        public virtual Language Language1 { get; set; }

        public virtual Relegion Relegion { get; set; }

        public virtual AcademicYear AcademicYear{ get; set; }
        public virtual ApplicationStatus ApplicationStatus { get; set; }

        public virtual Cast Cast { get; set; }

        public virtual Class Class { get; set; }

        public virtual Class Class1 { get; set; }

        public virtual GuardianType GuardianType { get; set; }

        public virtual GuardianType GuardianType1 { get; set; }

        public virtual GuardianType GuardianType2 { get; set; }
        public virtual GuardianType GuardianType3 { get; set; }

        public virtual PassportDetailMap PassportDetailMap { get; set; }

        public virtual PassportDetailMap PassportDetailMap1 { get; set; }

        public virtual PassportDetailMap PassportDetailMap2 { get; set; }

        public virtual PassportDetailMap PassportDetailMap3 { get; set; }

        public virtual Syllabu Syllabu { get; set; }

        public virtual StudentCategory StudentCategory { get; set; }

        public virtual Syllabu Syllabu1 { get; set; }

        public virtual VisaDetailMap VisaDetailMap { get; set; }

        public virtual VisaDetailMap VisaDetailMap1 { get; set; }

        public virtual VisaDetailMap VisaDetailMap2 { get; set; }

        public virtual VisaDetailMap VisaDetailMap3 { get; set; }

        public virtual VolunteerType VolunteerType { get; set; }

        public virtual VolunteerType VolunteerType1 { get; set; }

        [StringLength(30)]
        public string GuardianWhatsappMobileNo { get; set; }

        [StringLength(30)]
        public string FatherWhatsappMobileNo { get; set; }

        [StringLength(30)]
        public string MotherWhatsappMobileNo { get; set; }
        public virtual ApplicationSubmitType ApplicationSubmitType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentApplicationSiblingMap> StudentApplicationSiblingMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Students { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentApplicationDocumentMap> StudentApplicationDocumentMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentApplicationOptionalSubjectMap> StudentApplicationOptionalSubjectMaps { get; set; }
    }
}


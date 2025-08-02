using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public class StudentApplicationDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public StudentApplicationDTO()
        {
            Class = new KeyValueDTO();
            PreviousSchoolClassCompleted = new KeyValueDTO();
            StudentSiblings = new List<KeyValueDTO>();
            StudentPassportDetails = new StudentPassportDetailsDTO();
            FatherPassportDetails = new FatherPassportDetailsDTO();
            MotherPassportDetails = new MotherPassportDetailsDTO();
            StudentVisaDetails = new StudentVisaDetailsDTO();
            FatherVisaDetails = new FatherVisaDetailsDTO();
            MotherVisaDetails = new MotherVisaDetailsDTO();
            StudentDocUploads = new StudentApplicationDocumentsUploadDTO();
            GuardianPassportDetails = new GuardianPassportDetailsDTO();
            //Prospectus = new ProspectusDTO();
            OptionalSubjects = new List<KeyValueDTO>();
        }

        [DataMember]
        public long ApplicationIID { get; set; }

        [DataMember]
        public string ApplicationNumber { get; set; }

        [DataMember]
        public long? LoginID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }
        [DataMember]
        public KeyValueDTO Class { get; set; }

        [DataMember]
        public int? ClassID { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string MiddleName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Nationality { get; set; }
        [DataMember]
        public int? NationalityID { get; set; }
        [DataMember]
        public KeyValueDTO StudentNationality { get; set; }
        [DataMember]
        public byte? GenderID { get; set; }
        [DataMember]
        public System.DateTime? DateOfBirth { get; set; }
        [DataMember]
        public int? CategoryID { get; set; }
        [DataMember]
        public byte? CastID { get; set; }
        [DataMember]
        public string Cast { get; set; }
        [DataMember]
        public byte? RelegionID { get; set; }
        [DataMember]
        public string MobileNumber { get; set; }
        [DataMember]
        public string EmailID { get; set; }
        [DataMember]
        public long? ProfileContentID { get; set; }
        [DataMember]
        public string ParmenentAddress { get; set; }
        [DataMember]
        public string CurrentAddress { get; set; }

        [DataMember]
        public string FatherFirstName { get; set; }
        [DataMember]
        public string FatherMiddleName { get; set; }
        [DataMember]
        public string FatherLastName { get; set; }

        [DataMember]
        public string MotherFirstName { get; set; }
        [DataMember]
        public string MotherMiddleName { get; set; }
        [DataMember]
        public string MotherLastName { get; set; }
        [DataMember]
        public string FatherCountry { get; set; }
        [DataMember]
        public int? FatherCountryID { get; set; }

        [DataMember]
        public string FatherNationalID { get; set; }
        [DataMember]
        public string FatherPassportNumber { get; set; }
        [DataMember]
        public string MotherCountry { get; set; }
        [DataMember]
        public int? MotherCountryID { get; set; }

        [DataMember]
        public string MotherNationalID { get; set; }
        [DataMember]
        public string MotherPassportNumber { get; set; }

        [DataMember]
        public string FatherOccupation { get; set; }
        [DataMember]
        public byte? ApplicationStatusID { get; set; }
        [DataMember]
        public int? ApplicationTypeID { get; set; }
        [DataMember]
        public string ApplicationStatusDescription { get; set; }

        [DataMember]
        public string PreviousSchoolName { get; set; }

        [DataMember]
        public byte? PreviousSchoolSyllabusID { get; set; }

        [DataMember]
        public string PreviousSchoolSyllabus { get; set; }

        [DataMember]
        public string PreviousSchoolAcademicYear { get; set; }

        [DataMember]
        public string Academicyear { get; set; }
        [DataMember]
        public string ProspectNumber { get; set; }
        [DataMember]
        public decimal? ProspectFee { get; set; }
        [DataMember]
        public string PreviousSchoolClassClassKey { get; set; }

        [DataMember]
        public int? PreviousSchoolClassCompletedID { get; set; }

        [DataMember]
        public KeyValueDTO PreviousSchoolClassCompleted { get; set; }

        [DataMember]
        public string StudentPassportNo { get; set; }

        [DataMember]
        public string StudentNationalID { get; set; }

        [DataMember]
        public string MotherMobileNumber { get; set; }

        [DataMember]
        public string MotherEmailID { get; set; }

        [DataMember]
        public string MotherOccupation { get; set; }

        [DataMember]
        public byte? FatherStudentRelationShipID { get; set; }

        [DataMember]
        public string FatherStudentRelationShip { get; set; }

        [DataMember]
        public byte? MotherStudentRelationShipID { get; set; }

        [DataMember]
        public string MotherStudentRelationShip { get; set; }

        [DataMember]
        public int? SchoolAcademicyearID { get; set; }

        [DataMember]
        public int? AcademicyearID { get; set; }


        [DataMember]
        public string SchoolAcademicyear { get; set; }

        [DataMember]
        public List<KeyValueDTO> StudentSiblings { get; set; }

        [DataMember]
        public string BuildingNo { get; set; }

        [DataMember]
        public string FlatNo { get; set; }

        [DataMember]
        public string StreetNo { get; set; }

        [DataMember]
        public string StreetName { get; set; }

        [DataMember]
        public string LocationNo { get; set; }

        [DataMember]
        public string LocationName { get; set; }

        [DataMember]
        public string ZipNo { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public int? CountryID { get; set; }

        [DataMember]
        public long? StudentVisaDetailNoID { get; set; }
        [DataMember]
        public long? FatherVisaDetailNoID { get; set; }
        [DataMember]
        public long? MotherVisaDetailNoID { get; set; }
        [DataMember]
        public long? StudentPassportDetailNoID { get; set; }
        [DataMember]
        public long? FatherPassportDetailNoID { get; set; }
        [DataMember]
        public long? MotherPassportDetailNoID { get; set; }

        [DataMember]
        public byte? CommunityID { get; set; }
        [DataMember]
        public string Community { get; set; }
        [DataMember]
        public string AdhaarCardNo { get; set; }
        [DataMember]
        public bool? IsMinority { get; set; }
        [DataMember]
        public bool? IsOnlyChildofParent { get; set; }
        [DataMember]
        public int? BloodGroupID { get; set; }
        [DataMember]
        public string BloodGroup { get; set; }

        [DataMember]
        public string FatherMobileNumberTwo { get; set; }
        [DataMember]
        public DateTime? FatherNationalDNoIssueDate { get; set; }
        [DataMember]
        public DateTime? FatherNationalDNoExpiryDate { get; set; }
        [DataMember]
        public DateTime? MotherNationalDNoIssueDate { get; set; }
        [DataMember]
        public DateTime? MotherNationaIDNoExpiryDate { get; set; }
        [DataMember]
        public bool? IsStudentStudiedBefore { get; set; }
        [DataMember]
        public DateTime? StudentNationalIDNoExpiryDate { get; set; }
        [DataMember]
        public DateTime? StudentNationalIDNoIssueDate { get; set; }
        [DataMember]
        public int? StudentCoutryOfBrithID { get; set; }
        [DataMember]
        public string StudentCoutryOfBrith { get; set; }
        [DataMember]
        public KeyValueDTO CoutryOfBrith { get; set; }
        [DataMember]
        public byte? CurriculamID { get; set; }
        [DataMember]
        public string Curriculam { get; set; }
        [DataMember]
        public int? SecoundLanguageID { get; set; }
        [DataMember]
        public string SecoundLanguage { get; set; }
        [DataMember]
        public int? ThridLanguageID { get; set; }
        [DataMember]
        public string ThridLanguage { get; set; }
        [DataMember]
        public string PostBoxNo { get; set; }

        [DataMember]
        public string FatherCompanyName { get; set; }

        [DataMember]
        public string MotherCompanyName { get; set; }
        [DataMember]
        public int? CanYouVolunteerToHelpOneID { get; set; }
        [DataMember]
        public string CanYouVolunteerToHelpOne { get; set; }
        [DataMember]
        public int? CanYouVolunteerToHelpTwoID { get; set; }
        [DataMember]
        public string CanYouVolunteerToHelpTwo { get; set; }
        [DataMember]
        public string PreviousSchoolAddress { get; set; }
        [DataMember]
        public byte? PrimaryContactID { get; set; }

        [DataMember]
        public string PrimaryContact { get; set; }

        [DataMember]
        public byte? StreamID { get; set; }

        [DataMember]
        public KeyValueDTO Stream { get; set; }


        [DataMember]
        public KeyValueDTO StreamGroup { get; set; }

        [DataMember]
        public byte? StreamGroupID { get; set; }

        [DataMember]
        public StudentPassportDetailsDTO StudentPassportDetails { get; set; }

        [DataMember]
        public FatherPassportDetailsDTO FatherPassportDetails { get; set; }

        [DataMember]
        public MotherPassportDetailsDTO MotherPassportDetails { get; set; }

        [DataMember]
        public StudentVisaDetailsDTO StudentVisaDetails { get; set; }

        [DataMember]
        public FatherVisaDetailsDTO FatherVisaDetails { get; set; }


        [DataMember]
        public MotherVisaDetailsDTO MotherVisaDetails { get; set; }

        [DataMember]
        public GuardianPassportDetailsDTO GuardianPassportDetails { get; set; }


        //For purpose of Studentpicking fill Parent login details
        [DataMember]
        public string ParentLoginUserID { get; set; }

        [DataMember]
        public string ParentLoginEmailID { get; set; }

        [DataMember]
        public string ParentLoginPassword { get; set; }

        [DataMember]
        public string ParentLoginPasswordSalt { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public string ParentCode { get; set; }

        [DataMember]
        public long? ParentID { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public StudentApplicationDocumentsUploadDTO StudentDocUploads { get; set; }

        //[DataMember]
        //public ProspectusDTO Prospectus { get; set; }

        //[DataMember]
        //public StudentApplicationDocumentsUploadDTO DocumentsUpload { get; set; }
        [DataMember]
        public int? GuardianNationalityID { get; set; }

        [DataMember]
        public string GuardianNationality { get; set; }

        [DataMember]
        public byte? GuardianStudentRelationShipID { get; set; }

        [DataMember]
        public string GuardianStudentRelationShip { get; set; }

        [DataMember]
        public long? GuardianVisaDetailNoID { get; set; }

        [DataMember]
        public long? GuardianPassportDetailNoID { get; set; }

        [DataMember]
        public string GuardianNationalID { get; set; }

        [DataMember]
        public string GuardianFirstName { get; set; }

        [DataMember]
        public string GuardianMiddleName { get; set; }

        [DataMember]
        public string GuardianLastName { get; set; }

        [DataMember]
        public string GuardianOccupation { get; set; }

        [DataMember]
        public string GuardianMobileNumber { get; set; }

        [DataMember]
        public DateTime? GuardianNationalIDNoIssueDate { get; set; }

        [DataMember]
        public DateTime? GuardianNationalIDNoExpiryDate { get; set; }

        [DataMember]
        public string GuardianCompanyName { get; set; }
        [DataMember]
        public string GuardianEmailID { get; set; }

        [DataMember]
        public string GuardianWhatsappMobileNo { get; set; }

        [DataMember]
        public string FatherWhatsappMobileNo { get; set; }

        [DataMember]
        public string MotherWhatsappMobileNo { get; set; }

        [DataMember]
        public List<KeyValueDTO> OptionalSubjects { get; set; }

        [DataMember]
        public bool onStreams { get; set; }

        [DataMember]
        public string AgeCriteriaWarningMsg { get; set; }
    }
}



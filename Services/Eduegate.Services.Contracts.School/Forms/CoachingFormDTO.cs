using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Forms
{
    [DataContract]
    public class CoachingFormDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public CoachingFormDTO()
        {

        }

        [DataMember]
        public string ApplicationNumber { get; set; }

        [DataMember]
        public string AdmissionNumber { get; set; }

        [DataMember]
        public string StudentName { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public string SchoolName { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public string AcademicYearName { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public string Curriculam { get; set; }

        [DataMember]
        public byte? CurriculamID { get; set; }

        [DataMember]
        public string Grade { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public string RegistrationDateString { get; set; }

        [DataMember]
        public DateTime? RegistrationDate { get; set; }

        [DataMember]
        public string BloodGroup { get; set; }

        [DataMember]
        public string Gender { get; set; }

        [DataMember]
        public byte? GenderID { get; set; }

        [DataMember]
        public string Religion { get; set; }

        [DataMember]
        public byte? ReligionID { get; set; }

        [DataMember]
        public string DateOfBirthString { get; set; }

        [DataMember]
        public DateTime? DateOfBirth { get; set; }

        [DataMember]
        public string BirthCountry { get; set; }

        [DataMember]
        public int? BirthCountryID { get; set; }

        [DataMember]
        public string Nationality { get; set; }

        [DataMember]
        public string NationalityID { get; set; }

        [DataMember]
        public string SecondLanguage { get; set; }

        [DataMember]
        public string SecondLanguageID { get; set; }

        [DataMember]
        public string ThirdLanguage { get; set; }

        [DataMember]
        public string ThirdLanguageID { get; set; }

        [DataMember]
        public string PassportNumber { get; set; }

        [DataMember]
        public string PassportExpiryDateString { get; set; }

        [DataMember]
        public DateTime? PassportExpiryDate { get; set; }

        [DataMember]
        public string NationalID { get; set; }

        [DataMember]
        public string NationalIDExpiryDateString { get; set; }

        [DataMember]
        public DateTime? NationalIDExpiryDate { get; set; }

        [DataMember]
        public string PrimaryContact { get; set; }

        [DataMember]
        public byte? PrimaryContactID { get; set; }

        [DataMember]
        public string POBoxNumber { get; set; }

        [DataMember]
        public string PrimaryAddress { get; set; }

        [DataMember]
        public long? ParentID { get; set; }

        [DataMember]
        public string FatherName { get; set; }

        [DataMember]
        public string FatherMobileNumber { get; set; }

        [DataMember]
        public string FatherDesignation { get; set; }

        [DataMember]
        public string FatherCompanyName { get; set; }

        [DataMember]
        public string FatherNationalID { get; set; }

        [DataMember]
        public string FatherEmailID { get; set; }

        [DataMember]
        public string MotherName { get; set; }

        [DataMember]
        public string MotherMobileNumber { get; set; }

        [DataMember]
        public string MotherDesignation { get; set; }

        [DataMember]
        public string MotherCompanyName { get; set; }

        [DataMember]
        public string MotherNationalID { get; set; }

        [DataMember]
        public string MotherEmailID { get; set; }

        [DataMember]
        public string SiblingName { get; set; }

        [DataMember]
        public long? SiblingStudentID { get; set; }

        [DataMember]
        public string SiblingAdmissionNumber { get; set; }

        [DataMember]
        public string SiblingGrade { get; set; }

        [DataMember]
        public int? SiblingClassID { get; set; }

        [DataMember]
        public int? SiblingSectionID { get; set; }

        [DataMember]
        public string PreviousSchoolName { get; set; }

        [DataMember]
        public string PreviousSchoolCurriculam { get; set; }

        [DataMember]
        public string PreviousSchoolGrade { get; set; }

        [DataMember]
        public string IsSelectTermsAndConditions { get; set; }

        [DataMember]
        public string ApplicationStatusID { get; set; }

        [DataMember]
        public string ApplicationStatus { get; set; }

        [DataMember]
        public DateTime? AppliedDate { get; set; }

        [DataMember]
        public string AppliedDateString { get; set; }
    }
}
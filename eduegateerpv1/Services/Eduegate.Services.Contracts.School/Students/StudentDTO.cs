using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Common;
using Eduegate.Services.Contracts.School.Inventory;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public class StudentDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public StudentDTO()
        {
            PreviousSchoolClassCompleted = new KeyValueDTO();
            StudentSiblings = new List<KeyValueDTO>();
            StudentStaffMaps = new List<KeyValueDTO>();
            OptionalSubjects = new List<KeyValueDTO>();
            Guardian = new GuardianDTO();
            AdditionalInfo = new AdditionalInfoDTO();
            StudentPassportDetails = new StudentPassportDetailDTO();
            Login = new LoginDTO();
            ParentLogin = new LoginDTO();
            StudentDocUploads = new StudentApplicationDocumentsUploadDTO();
            Allergy = new AllergyStudentDTO();
        }

        [DataMember]
        public long StudentIID { get; set; }

        [DataMember]
        public long? LoginID { get; set; }

        [DataMember]
        public string AdmissionNumber { get; set; }

        [DataMember]
        public string RollNumber { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public string SectionName { get; set; }

        [DataMember]
        public byte? GradID { get; set; }

        [DataMember]
        public string GradeName { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string MiddleName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public byte? GenderID { get; set; }

        [DataMember]
        public string GenderName { get; set; }

        [DataMember]
        public DateTime? DateOfBirth { get; set; }

        [DataMember]
        public int? CategoryID { get; set; }

        [DataMember]
        public string CategoryName { get; set; }

        [DataMember]
        public byte? CastID { get; set; }

        [DataMember]
        public string CastName { get; set; }

        [DataMember]
        public byte? RelegionID { get; set; }

        [DataMember]
        public string RelegionName { get; set; }

        [DataMember]
        public string MobileNumber { get; set; }

        [DataMember]
        public string EmailID { get; set; }

        [DataMember]
        public DateTime? AdmissionDate { get; set; }

        [DataMember]
        public string StudentProfile { get; set; }

        [DataMember]
        public int? BloodGroupID { get; set; }

        [DataMember]
        public string BloodGroupName { get; set; }

        [DataMember]
        public int? StudentHouseID { get; set; }

        [DataMember]
        public string StudentHouse { get; set; }

        [DataMember]
        public string Height { get; set; }

        [DataMember]
        public string Weight { get; set; }

        [DataMember]
        public DateTime? AsOnDate { get; set; }

        [DataMember]
        public List<KeyValueDTO> StudentSiblings { get; set; }

        [DataMember]
        public List<KeyValueDTO> OptionalSubjects { get; set; }

        [DataMember]
        public int? HostelID { get; set; }

        [DataMember]
        public long? RoomID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public string SchoolAcademicYearName { get; set; }

        [DataMember]
        public string SchoolName { get; set; }

        [DataMember]
        public GuardianDTO Guardian { get; set; }

        [DataMember]
        public AdditionalInfoDTO AdditionalInfo { get; set; }

        [DataMember]
        public StudentPassportDetailDTO StudentPassportDetails { get; set; }

        [DataMember]
        public bool IsSelected { get; set; }

        [DataMember]
        public LoginDTO Login { get; set; }

        [DataMember]
        public LoginDTO ParentLogin { get; set; }

        [DataMember]
        public long? ParentID { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public System.DateTime? FeeStartDate { get; set; }

        [DataMember]
        public bool? IsMinority { get; set; }

        [DataMember]
        public bool? IsOnlyChildofParent { get; set; }

        [DataMember]
        public byte? PrimaryContactID { get; set; }

        [DataMember]
        public string PrimaryContact { get; set; }

        [DataMember]
        public int? SecoundLanguageID { get; set; }

        [DataMember]
        public string SecoundLanguage { get; set; }

        [DataMember]
        public int? ThridLanguageID { get; set; }

        [DataMember]
        public string ThridLanguage { get; set; }

        [DataMember]
        public bool? IsStudentStudiedBefore { get; set; }

        [DataMember]
        public string PreviousSchoolName { get; set; }

        [DataMember]
        public byte? PreviousSchoolSyllabusID { get; set; }

        [DataMember]
        public string PreviousSchoolSyllabus { get; set; }

        [DataMember]
        public string PreviousSchoolAcademicYear { get; set; }

        [DataMember]
        public int? PreviousSchoolClassCompletedID { get; set; }

        [DataMember]
        public KeyValueDTO PreviousSchoolClassCompleted { get; set; }

        [DataMember]
        public string PreviousSchoolAddress { get; set; }

        [DataMember]
        public byte? CommunityID { get; set; }

        [DataMember]
        public string Community { get; set; }

        [DataMember]
        public string StudentFullName { get; set; }

        [DataMember]
        public long? ApplicationID { get; set; }

        [DataMember]
        public int? SubjectMapID { get; set; }

        [DataMember]
        public string SubjectMapString { get; set; }

        [DataMember]
        public int? SchoolAcademicyearID { get; set; }

        [DataMember]
        public byte? StreamID { get; set; }

        [DataMember]
        public string ClassCode { get; set; }

        [DataMember]
        public string AcademicYear { get; set; }

        [DataMember]
        public byte? Status { get; set; }

        [DataMember]
        public string StatusString { get; set; }

        [DataMember]
        public DateTime? InactiveDate { get; set; }

        [DataMember]
        public StudentApplicationDocumentsUploadDTO StudentDocUploads { get; set; }

        [DataMember]
        public bool onStreams { get; set; }

        [DataMember]
        public string StreamName { get; set; }

        [DataMember]
        public List<KeyValueDTO> StudentStaffMaps { get; set; }

        [DataMember]
        public string AdmissionDateString { get; set; }

        [DataMember]
        public string DateOfBirthString { get; set; }


        [DataMember]
        public string StudentProfileName { get; set; }

        [DataMember]
        public AllergyStudentDTO Allergy { get; set; }

        [DataMember]
        public string StudentInitials { get; set; }

        [DataMember]
        public string SchoolShortName { get; set; }

        #region Mobile app use
        [DataMember]
        public string ParentEmailID { get; set; }

        [DataMember]
        public int? StudentAge { get; set; }
        #endregion
    }
}
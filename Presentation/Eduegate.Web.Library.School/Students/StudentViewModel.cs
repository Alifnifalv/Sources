using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.School.Common;
using Eduegate.Services.Contracts.School.Common;
using System.Globalization;
using Eduegate.Web.Library.Common;
using Eduegate.Framework.Enums;
using Eduegate.Infrastructure.Enums;

namespace Eduegate.Web.Library.School.Students
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "Student", "CRUDModel.ViewModel")]
    [DisplayName("Details")]
    public class StudentViewModel : BaseMasterViewModel
    {
        public StudentViewModel()
        {
            AdditionlInfo = new StudentAdditionalDetailsViewModel();
            PreviousSchoolDetails = new PreviousSchoolDetailsViewModel();
            Addresses = new AddressViewModel();
            Guardians = new GaurdianViewModel();
            Hostels = new HostelViewModel();
            Sibling = new List<KeyValueViewModel>();
            Staff = new List<KeyValueViewModel>();
            //Document = new UploadDocumentViewModel();
            StudentName = new StudentNameViewModel();
            StudentPassportDetails = new StudentPassportDetailViewModel();
            StudentLogin = new StudentLoginViewModel();
            ParentLogin = new ParentLoginViewModel();
            IsActive = true;
            onStreams = true;
            DocumentsUpload = new List<StudentApplicationDocUploadViewModel>() { new StudentApplicationDocUploadViewModel() };
        }

        public long StudentIID { get; set; }

        public long? LoginID { get; set; }

        public long? ParentID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DataPicker)]
        [CustomDisplay("FromStudentApplication")]
        [DataPicker("StudentApplicationAdvancedSearch")]
        public string ReferenceStudentApplicationNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("AdmissionNo.")]
        public string AdmissionNumber { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBoxWithAutogeneration)]
        //[DisplayName("Roll Number")]
        public string RollNumber { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Student ID")]
        public string StudentID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine9 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='SchoolChanges($event, $element,CRUDModel.ViewModel)'")]
        [CustomDisplay("School")]
        [LookUp("LookUps.School")]
        public string School { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Academic Year")]
        [LookUp("LookUps.FullAcademicYears")]
        public string Academicyear { get; set; }
        public int? AcademicYearID { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[CustomDisplay("CurrentAcademicYear")]
        //public string Academicyear { get; set; }
        //public int? AcademicYearID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("JoinedAcademicYear")]
        public string SchoolAcademicyear { get; set; }
        public int? SchoolAcademicYearID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine8 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Classes", "Numeric", false, "ClassChangesforStream($event, $element, CRUDModel.ViewModel)")]
        [LookUp("LookUps.Classes")]
        [CustomDisplay("Class")]
        public KeyValueViewModel StudentClass { get; set; }
        public int? ClassID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Section", "Numeric", false, "")]
        [LookUp("LookUps.Section")]
        [CustomDisplay("Section")]
        public KeyValueViewModel Section { get; set; }

        public string SectionName { get; set; }
        public string ClassName { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public int? SectionID { get; set; }

        //public string Academicyear { get; set; }

        public string Grade { get; set; }

        public int? GradID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        public bool onStreams { get; set; }
        //[ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='StreamChanges($event, $element,CRUDModel.ViewModel)'")]
        //[DisplayName("Stream")]
        //[LookUp("LookUps.Streams")]
        //public string Stream { get; set; }
        //public int? StreamID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("ActiveStreams", "Numeric", false, "StreamChanges($event, $element, CRUDModel.ViewModel)", false, OptionalAttribute1 = "ng-disabled=CRUDModel.ViewModel.onStreams")]
        [LookUp("LookUps.ActiveStreams")]
        [CustomDisplay("Stream")]
        public KeyValueViewModel Stream { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("OptionalSubjects", "Numeric", true, "", optionalAttribute1: "ng-disabled=CRUDModel.ViewModel.onStreams")]
        [LookUp("LookUps.OptionalSubjects")]
        [CustomDisplay("Optional Subjects")]
        public List<KeyValueViewModel> OptionalSubjects { get; set; }
        public int? OptionalSubjectID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.FieldSet, "fullwidth", isFullColumn: false)]
        [DisplayName("")]
        public StudentNameViewModel StudentName { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.FileUpload)]
        [CustomDisplay("StudentProfile")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.UserProfile, "ProfileUrl", "")]
        public string ProfileUrl { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Gender")]
        [LookUp("LookUps.Gender")]
        public string Gender { get; set; }

        public byte? GenderID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("DateOfBirth")]
        public string DateOfBirthString { get; set; }

        public System.DateTime? DateOfBirth { get; set; }

        public byte? SchoolID { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("Classes", "Numeric", false, "")]
        //[LookUp("LookUps.StudentCategory")]
        //[DisplayName("Category")]
        //public KeyValueViewModel Category { get; set; }

        //public int? CategoryID { get; set; }



        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='RelegionChanges($event, $element,CRUDModel.ViewModel)'")]
        [CustomDisplay("Religion")]
        [LookUp("LookUps.Relegion")]
        public string Relegion { get; set; }
        public int? RelegionID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Caste")]
        [LookUp("LookUps.Cast")]
        public string Cast { get; set; }
        public int? CastID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Community")]
        [LookUp("LookUps.Community")]
        public string Community { get; set; }
        public byte? CommunityID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(13, ErrorMessage = "Maximum Length should be within 13!")]
        [CustomDisplay("MobileNo.")]
        public string MobileNumber { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox/*, attribs: "ng-crud-unique controllercall=" + "'Login/CheckStudentEmailIDAvailability?loginID={{CRUDModel.ViewModel.StudentIID}}&EmailID={{CRUDModel.ViewModel.EmailID}}'" + " message=' already exist.'"*/)]
        [CustomDisplay("EmailID")]
        public string EmailID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("AdmissionDate")]
        public string AdmissionDateString { get; set; }
        public System.DateTime? AdmissionDate { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("BloodGroup")]
        [LookUp("LookUps.BloodGroup")]
        public string BloodGroup { get; set; }

        public int? BloodGroupID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("StudentHouse")]
        [LookUp("LookUps.StudentHouse")]
        public string StudentHouse { get; set; }

        public int? StudentHouseID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(10, ErrorMessage = "Maximum Length should be within 10!")]
        [CustomDisplay("Height")]
        public string Height { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(10, ErrorMessage = "Maximum Length should be within 10!")]
        [CustomDisplay("Weight")]
        public string Weight { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("AsOnDate")]
        public string AsOnDateString { get; set; }

        public System.DateTime? AsOnDate { get; set; }

        public long? ApplicationID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Students", "Numeric", true, "")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Student", "LookUps.Students")]
        [CustomDisplay("Siblings")]
        public List<KeyValueViewModel> Sibling { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("EmployeeWB", "Numeric", true, "")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=EmployeeWB", "LookUps.EmployeeWB")]
        [CustomDisplay("Staff")]
        public List<KeyValueViewModel> Staff { get; set; }

        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "Hostels", "Hostels")]
        //[DisplayName("Hostels")]
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("FeeStartDate")]
        public string FeeStartDateString { get; set; }

        public System.DateTime? FeeStartDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }

        [Required]
        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("StudentStatus", "Numeric", false, "")]
        //[LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=StudentStatus", "LookUps.StudentStatus")]
        [CustomDisplay("Status")]
        [LookUp("LookUps.StudentStatus")]
        public KeyValueViewModel StudentStatus { get; set; }

        public string StatusString { get; set; }
        public int? Status { get; set; }


        //public string StudentStatus { get; set; }
        public int? StatusID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsMinority")]
        public bool? IsMinority { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsOnlyChildofParent")]
        public bool? IsOnlyChildofParent { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("PrimaryContact")]
        [LookUp("LookUps.GuardianType")]
        public string PrimaryContact { get; set; }
        public byte? PrimaryContactID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("SubjectMap")]
        [LookUp("LookUps.SubjectMap")]

        public string SubjectMapString { get; set; }
        public int? SubjectMapID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("SecondLanguage")]
        [LookUp("LookUps.SecondLanguage")]

        public string SecoundLanguageString { get; set; }
        public int? SecoundLanguageID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("ThirdLanguage")]
        [LookUp("LookUps.ThirdLanguage")]

        public string ThridLanguageString { get; set; }
        public int? ThridLanguageID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, attribs: "ng-disabled=CRUDModel.ViewModel.IsActive")]
        [CustomDisplay("InActiveDate")]
        public string InActiveDateString { get; set; }
        public System.DateTime? InActiveDate { get; set; }

        public HostelViewModel Hostels { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("StudentDocuments")]
        public List<StudentApplicationDocUploadViewModel> DocumentsUpload { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "StudentPassportDetails", "StudentPassportDetails")]
        [CustomDisplay("StudentOtherDetails")]
        public StudentPassportDetailViewModel StudentPassportDetails { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "Guardians", "Guardians")]
        [CustomDisplay("GuardianDetails")]
        public GaurdianViewModel Guardians { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "Addresses", "Addresses")]
        [CustomDisplay("Addresses")]
        public AddressViewModel Addresses { get; set; }

        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "AdditionlInfo", "AdditionlInfo")]
        //[DisplayName("Additional Info")]
        public StudentAdditionalDetailsViewModel AdditionlInfo { get; set; }

        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "Document", "Document")]
        //[DisplayName("Student Documents")]
        //public UploadDocumentViewModel Document { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "PreviousSchoolDetails", "PreviousSchoolDetails")]
        [CustomDisplay("PreviousSchoolDetails")]
        public PreviousSchoolDetailsViewModel PreviousSchoolDetails { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "StudentLogin", "StudentLogin")]
        [CustomDisplay("StudentLoginInfo")]
        public StudentLoginViewModel StudentLogin { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "ParentLogin", "ParentLogin")]
        [CustomDisplay("ParentLoginInfo")]
        public ParentLoginViewModel ParentLogin { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as StudentDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            Mapper<StudentDTO, StudentViewModel>.CreateMap();
            Mapper<GuardianDTO, GaurdianViewModel>.CreateMap();
            Mapper<AdditionalInfoDTO, StudentAdditionalDetailsViewModel>.CreateMap();
            Mapper<StudentPassportDetailDTO, StudentPassportDetailViewModel>.CreateMap();
            Mapper<LoginsDTO, StudentLoginViewModel>.CreateMap();
            Mapper<LoginsDTO, ParentLoginViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var studentDTO = dto as StudentDTO;
            var vm = Mapper<StudentDTO, StudentViewModel>.Map(studentDTO);

            vm.AdmissionNumber = studentDTO.AdmissionNumber;
            vm.StudentID = studentDTO.StudentIID.ToString();
            vm.StudentName.FirstName = studentDTO.FirstName;
            vm.StudentName.MiddleName = studentDTO.MiddleName;
            vm.StudentName.LastName = studentDTO.LastName;
            vm.ApplicationID = studentDTO.ApplicationID.HasValue ? studentDTO.ApplicationID : null;
            vm.StudentClass = studentDTO.ClassID.HasValue ? new KeyValueViewModel() { Key = studentDTO.ClassID.ToString(), Value = studentDTO.ClassName } : new KeyValueViewModel();
            vm.Section = studentDTO.SectionID.HasValue ? new KeyValueViewModel() { Key = studentDTO.SectionID.ToString(), Value = studentDTO.SectionName } : new KeyValueViewModel();
            vm.Grade = studentDTO.GradID.HasValue ? studentDTO.GradID.Value.ToString() : null;
            vm.Gender = studentDTO.GenderID.HasValue ? studentDTO.GenderID.Value.ToString() : null;
            vm.Stream = studentDTO.ClassID.HasValue ? new KeyValueViewModel() { Key = studentDTO.StreamID.ToString(), Value = studentDTO.StreamName } : new KeyValueViewModel();
            //vm.Category = studentDTO.CategoryID.HasValue ? new KeyValueViewModel() { Key = studentDTO.CategoryID.ToString(), Value = studentDTO.CategoryName } : new KeyValueViewModel();
            //vm.Cast = studentDTO.CastID.HasValue ? studentDTO.CastID.Value.ToString() : null;
            vm.SchoolAcademicyear = studentDTO.SchoolAcademicYearName;
            vm.SchoolAcademicYearID = studentDTO.SchoolAcademicyearID;
            vm.Academicyear = studentDTO.AcademicYearID.HasValue ? studentDTO.AcademicYearID.ToString() : null;
            vm.AcademicYearID = studentDTO.AcademicYearID;
            vm.School = studentDTO.SchoolID.HasValue ? studentDTO.SchoolID.Value.ToString() : null;
            vm.SchoolID = studentDTO.SchoolID;
            vm.Relegion = studentDTO.RelegionID.HasValue ? studentDTO.RelegionID.Value.ToString() : null;
            vm.BloodGroup = studentDTO.BloodGroupID.HasValue ? studentDTO.BloodGroupID.Value.ToString() : null;
            vm.StudentHouse = studentDTO.StudentHouseID.HasValue ? studentDTO.StudentHouseID.Value.ToString() : null;
            vm.DateOfBirthString = studentDTO.DateOfBirth.HasValue ? studentDTO.DateOfBirth.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.InActiveDateString = studentDTO.InactiveDate.HasValue ? studentDTO.InactiveDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.AsOnDateString = studentDTO.AsOnDate.HasValue ? studentDTO.AsOnDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.AdmissionDateString = studentDTO.AdmissionDate.HasValue ? studentDTO.AdmissionDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.Hostels.Hostel = !studentDTO.HostelID.HasValue ? null : studentDTO.HostelID.Value.ToString();
            vm.Hostels.HostelRoom = !studentDTO.RoomID.HasValue ? null : studentDTO.RoomID.Value.ToString();
            vm.LoginID = studentDTO.LoginID.HasValue ? studentDTO.LoginID : (long?)null;
            vm.ParentID = studentDTO.ParentID.HasValue ? studentDTO.ParentID : (long?)null;
            //vm.ProfileFile = studentDTO.StudentProfile;
            //vm.ProfileUrl = string.Format("{0}/{1}/{2}", new Domain.Setting.SettingBL().GetSettingValue<string>("ImageHostUrl"), EduegateImageTypes.UserProfile, vm.ProfileFile);
            vm.ProfileUrl = studentDTO.StudentProfile;
            vm.FeeStartDateString = studentDTO.FeeStartDate.HasValue ? studentDTO.FeeStartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.IsActive = studentDTO.IsActive;
            vm.IsMinority = studentDTO.IsMinority;
            vm.IsOnlyChildofParent = studentDTO.IsOnlyChildofParent;
            vm.SecoundLanguageString = studentDTO.SecoundLanguageID.ToString();
            vm.ThridLanguageString = studentDTO.ThridLanguageID.ToString();
            vm.Community = studentDTO.CommunityID.ToString();
            vm.PrimaryContact = studentDTO.PrimaryContactID.ToString();
            vm.Cast = studentDTO.CastID.ToString();
            vm.SubjectMapString = studentDTO.SubjectMapID.ToString();
            //vm.StudentStatus = studentDTO.Status.ToString();
            vm.StudentStatus = studentDTO.Status.HasValue ? new KeyValueViewModel() { Key = studentDTO.Status.ToString(), Value = studentDTO.StatusString } : new KeyValueViewModel();
            vm.MobileNumber = studentDTO.MobileNumber;
            vm.onStreams = studentDTO.onStreams;

            if (studentDTO.StudentSiblings.Count > 0)
            {
                foreach (KeyValueDTO kvm in studentDTO.StudentSiblings)
                {
                    vm.Sibling.Add(new KeyValueViewModel()
                    { Key = kvm.Key, Value = kvm.Value });
                }
            }

            vm.PreviousSchoolDetails = new PreviousSchoolDetailsViewModel()
            {
                IsStudentStudiedBefore = studentDTO.IsStudentStudiedBefore,
                PreviousSchoolName = studentDTO.PreviousSchoolName,
                PreviousSchoolSyllabus = studentDTO.PreviousSchoolSyllabusID.ToString(),
                PreviousSchoolSyllabusID = studentDTO.PreviousSchoolSyllabusID,
                PreviousSchoolAcademicYear = studentDTO.PreviousSchoolAcademicYear,
                PreviousSchoolClass = studentDTO.PreviousSchoolClassCompletedID.HasValue ? new KeyValueViewModel()
                {
                    Key = studentDTO.PreviousSchoolClassCompletedID.ToString(),
                    Value = studentDTO.PreviousSchoolClassCompleted.Value
                } : null,
                PreviousSchoolAddress = studentDTO.PreviousSchoolAddress,
            };

            vm.Guardians = new GaurdianViewModel()
            {
                //FatherName = studentDTO.Guardian.FatherName,
                ParentCode = studentDTO.Guardian.ParentCode,
                FatherFirstName = studentDTO.Guardian.FatherFirstName,
                FatherMiddleName = studentDTO.Guardian.FatherMiddleName,
                FatherLastName = studentDTO.Guardian.FatherLastName,
                FatherCompanyName = studentDTO.Guardian.FatherCompanyName,
                FatherOccupation = studentDTO.Guardian.FatherOccupation,
                //GuardianOccupation = studentDTO.Guardian.GuardianOccupation,
                FatherPhoneNumber = studentDTO.Guardian.PhoneNumber,
                FatherMobileNumberTwo = studentDTO.Guardian.FatherMobileNumberTwo,
                FatherWhatsappMobileNo = studentDTO.Guardian.FatherWhatsappMobileNo,
                FatherEmailID = studentDTO.Guardian.FatherEmailID,

                //FatherProfile = studentDTO.Guardian.FatherProfile,
                //FatherProfileUrl = studentDTO.Guardian.FatherProfile,

                FatherPassportNumber = studentDTO.Guardian.FatherPassportNumber,
                FatherCountryofIssueID = studentDTO.Guardian.FatherPassportCountryofIssueID == null ? (int?)null : studentDTO.Guardian.FatherPassportCountryofIssueID,
                FatherCountryofIssue = studentDTO.Guardian.FatherPassportCountryofIssueID.ToString(),
                FatherPassportNoIssueString = studentDTO.Guardian.FatherPassportNoIssueDate.HasValue ? studentDTO.Guardian.FatherPassportNoIssueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                FatherPassportNoExpiryString = studentDTO.Guardian.FatherPassportNoExpiryDate.HasValue ? studentDTO.Guardian.FatherPassportNoExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,

                FatherNationalID = studentDTO.Guardian.FatherNationalID,
                FatherNationalDNoIssueDateString = studentDTO.Guardian.FatherNationalDNoIssueDate.HasValue ? studentDTO.Guardian.FatherNationalDNoIssueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                FatherNationalDNoExpiryDateString = studentDTO.Guardian.FatherNationalDNoExpiryDate.HasValue ? studentDTO.Guardian.FatherNationalDNoExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,

                FatherCountryID = studentDTO.Guardian.FatherCountryID == null ? (int?)null : studentDTO.Guardian.FatherCountryID,
                FatherCountry = studentDTO.Guardian.FatherCountryID.ToString(),

                CanYouVolunteerToHelpOneString = studentDTO.Guardian.CanYouVolunteerToHelpOneID.ToString(),
                CanYouVolunteerToHelpOneID = studentDTO.Guardian.CanYouVolunteerToHelpOneID == null ? (byte?)null : studentDTO.Guardian.CanYouVolunteerToHelpOneID,

                //Guardian Details ToVm
                GuardianFirstName = studentDTO.Guardian.GuardianFirstName,
                GuardianMiddleName = studentDTO.Guardian.GuardianMiddleName,
                GuardianLastName = studentDTO.Guardian.GuardianLastName,
                GuardianType = studentDTO.Guardian.GuardianTypeID.HasValue ? studentDTO.Guardian.GuardianTypeID.ToString() : null,
                GuardianOccupation = studentDTO.Guardian.GuardianOccupation,
                GuardianCompanyName = studentDTO.Guardian.GuardianCompanyName,
                GuardianPhone = studentDTO.Guardian.GuardianPhone,
                GuardianWhatsappMobileNo = studentDTO.Guardian.GuardianWhatsappMobileNo,
                GaurdianEmail = studentDTO.Guardian.GaurdianEmail,
                GuardianNationalityID = studentDTO.Guardian.GuardianNationalityID == null ? (int?)null : studentDTO.Guardian.GuardianNationalityID,
                GuardianNationality = studentDTO.Guardian.GuardianNationalityID.ToString(),
                GuardianNationalID = studentDTO.Guardian.GuardianNationalID,
                GuardianNationalIDNoIssueDateString = studentDTO.Guardian.GuardianNationalIDNoIssueDate.HasValue ? studentDTO.Guardian.GuardianNationalIDNoIssueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                GuardianNationalIDNoExpiryDateString = studentDTO.Guardian.GuardianNationalIDNoExpiryDate.HasValue ? studentDTO.Guardian.GuardianNationalIDNoExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                GuardianPassportNumber = studentDTO.Guardian.GuardianPassportNumber,
                GuardianCountryofIssueID = studentDTO.Guardian.GuardianCountryofIssueID == null ? (int?)null : studentDTO.Guardian.GuardianCountryofIssueID,
                GuardianCountryofIssue = studentDTO.Guardian.GuardianCountryofIssueID.ToString(),
                GuardianPassportNoIssueString = studentDTO.Guardian.GuardianPassportNoIssueDate.HasValue ? studentDTO.Guardian.GuardianPassportNoIssueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                GuardianPassportNoExpiryString = studentDTO.Guardian.GuardianPassportNoExpiryDate.HasValue ? studentDTO.Guardian.GuardianPassportNoExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,

                //MotherName = studentDTO.Guardian.MotherName,
                MotherFirstName = studentDTO.Guardian.MotherFirstName,
                MotherMiddleName = studentDTO.Guardian.MotherMiddleName,
                MotherLastName = studentDTO.Guardian.MotherLastName,
                MotherCompanyName = studentDTO.Guardian.MotherCompanyName,
                MotherOccupation = studentDTO.Guardian.MotherOccupation,
                MotherPhone = studentDTO.Guardian.MotherPhone,
                MotherWhatsappMobileNo = studentDTO.Guardian.MotherWhatsappMobileNo,
                MotherEmailID = studentDTO.Guardian.MotherEmailID,

                MotherPassportNumber = studentDTO.Guardian.MotherPassportNumber,
                MotherCountryofIssueID = studentDTO.Guardian.MotherPassportCountryofIssueID == null ? (int?)null : studentDTO.Guardian.MotherPassportCountryofIssueID,
                MotherCountryofIssue = studentDTO.Guardian.MotherPassportCountryofIssueID.ToString(),
                MotherPassportNoIssueString = studentDTO.Guardian.MotherPassportNoIssueDate.HasValue ? studentDTO.Guardian.MotherPassportNoIssueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                MotherPassportNoExpiryString = studentDTO.Guardian.MotherPassportNoExpiryDate.HasValue ? studentDTO.Guardian.MotherPassportNoExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,

                MotherNationalID = studentDTO.Guardian.MotherNationalID,
                MotherNationalDNoIssueDateString = studentDTO.Guardian.MotherNationalDNoIssueDate.HasValue ? studentDTO.Guardian.MotherNationalDNoIssueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                MotherNationaIDNoExpiryDateString = studentDTO.Guardian.MotherNationalDNoExpiryDate.HasValue ? studentDTO.Guardian.MotherNationalDNoExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,

                MotherCountryID = studentDTO.Guardian.MotherCountryID == null ? (int?)null : studentDTO.Guardian.MotherCountryID,
                MotherCountry = studentDTO.Guardian.MotherCountryID.ToString(),

                //MotherPofile = studentDTO.Guardian.MotherPofile,
                //MotherPofileUrl = studentDTO.Guardian.MotherPofile,

                ParentIID = studentDTO.Guardian.ParentIID,
                ParentStudentMapIID = studentDTO.Guardian.ParentStudentMapIID,

                BuildingNo = studentDTO.Guardian.BuildingNo,
                FlatNo = studentDTO.Guardian.FlatNo,
                StreetNo = studentDTO.Guardian.StreetNo,
                StreetName = studentDTO.Guardian.StreetName,
                LocationNo = studentDTO.Guardian.LocationNo,
                LocationName = studentDTO.Guardian.LocationName,
                ZipNo = studentDTO.Guardian.ZipNo,
                PostBoxNo = studentDTO.Guardian.PostBoxNo,
                City = studentDTO.Guardian.City,
                //Country = studentDTO.Guardian.CountryID.ToString(),
                //CountryID = string.IsNullOrEmpty(studentDTO.Guardian.Country) ? (int?)null : int.Parse(studentDTO.Guardian.Country),
                Country = studentDTO.Guardian.CountryID.HasValue ? studentDTO.Guardian.CountryID.ToString() : null,

                CanYouVolunteerToHelpTwoString = studentDTO.Guardian.CanYouVolunteerToHelpTwoID.ToString(),
                CanYouVolunteerToHelpTwoID = studentDTO.Guardian.CanYouVolunteerToHelpTwoID == null ? (byte?)null : studentDTO.Guardian.CanYouVolunteerToHelpTwoID,
            };

            vm.AdditionlInfo = new StudentAdditionalDetailsViewModel()
            {
                StudentMiscDetailsIID = studentDTO.AdditionalInfo.StudentMiscDetailsIID,
                //Staff = new KeyValueViewModel()
                //{
                //    Key = studentDTO.AdditionalInfo.StaffID.ToString(),
                //    Value = studentDTO.AdditionalInfo.Staff.Value
                //},
                //Staff = studentDTO.AdditionalInfo.StaffID.HasValue ? new KeyValueViewModel()
                //{ 
                //    Key = studentDTO.AdditionalInfo.StaffID.ToString(), 
                //    Value = studentDTO.AdditionalInfo.Staff.Value
                //} : new KeyValueViewModel(),
                //GuardianType = studentDTO.AdditionalInfo.GuardianTypeID.HasValue ? studentDTO.AdditionalInfo.GuardianTypeID.ToString() : null,
                //StudentGroup = studentDTO.AdditionalInfo.StudentGroupID.HasValue ? studentDTO.AdditionalInfo.StudentGroupID.ToString() : null,
                //StudentGroup = studentDTO.AdditionalInfo.StudentGroupID.
                //StudentGroup = new List<KeyValueViewModel>()
                //if (studentDTO.AdditionalInfo.StudentGroups.Count > 0)
                //{
            };
            if (studentDTO.AdditionalInfo.StudentGroups.Count > 0)
            {
                foreach (KeyValueDTO kvm in studentDTO.AdditionalInfo.StudentGroups)
                {
                    vm.AdditionlInfo.StudentGroup.Add(new KeyValueViewModel()
                    {
                        Key = kvm.Key,
                        Value = kvm.Value
                    });
                }
            }

            vm.OptionalSubjects = new List<KeyValueViewModel>();
            if (studentDTO.OptionalSubjects.Count > 0)
            {
                foreach (KeyValueDTO opSubjt in studentDTO.OptionalSubjects)
                {
                    vm.OptionalSubjects.Add(new KeyValueViewModel()
                    { Key = opSubjt.Key, Value = opSubjt.Value });
                }
            }

            if (studentDTO.StudentStaffMaps.Count > 0)
            {
                foreach (KeyValueDTO kvm in studentDTO.StudentStaffMaps)
                {
                    vm.Staff.Add(new KeyValueViewModel()
                    { Key = kvm.Key, Value = kvm.Value });
                }
            }

            vm.Addresses = new AddressViewModel()
            {
                //CurrentAddress = studentDTO.AdditionalInfo.CurrentAddress,
                IsCurrentAddresIsGuardian = studentDTO.AdditionalInfo.IsCurrentAddresIsGuardian,
                IsPermenentAddresIsCurrent = studentDTO.AdditionalInfo.IsPermenentAddresIsCurrent,
                //PermenentAddress = studentDTO.AdditionalInfo.PermenentAddress,
                PermenentBuildingNo = studentDTO.AdditionalInfo.PermenentBuildingNo,
                PermenentFlatNo = studentDTO.AdditionalInfo.PermenentFlatNo,
                PermenentStreetNo = studentDTO.AdditionalInfo.PermenentStreetNo,
                PermenentStreetName = studentDTO.AdditionalInfo.PermenentStreetName,
                PermenentLocationNo = studentDTO.AdditionalInfo.PermenentLocationNo,
                PermenentLocationName = studentDTO.AdditionalInfo.PermenentLocationName,
                PermenentZipNo = studentDTO.AdditionalInfo.PermenentZipNo,
                PermenentPostBoxNo = studentDTO.AdditionalInfo.PermenentPostBoxNo,
                PermenentCity = studentDTO.AdditionalInfo.PermenentCity,
                PermenentCountry = studentDTO.AdditionalInfo.PermenentCountryID.HasValue ? studentDTO.AdditionalInfo.PermenentCountryID.ToString() : null,
                StudentMiscDetailsIID = studentDTO.AdditionalInfo.StudentMiscDetailsIID,
            };
            vm.StudentPassportDetails = new StudentPassportDetailViewModel()
            {
                StudentPassportDetailsIID = studentDTO.StudentPassportDetails.StudentPassportDetailsIID,
                StudentID = studentDTO.StudentPassportDetails.StudentID,
                NationalityID = studentDTO.StudentPassportDetails.NationalityID,
                Nationality = studentDTO.StudentPassportDetails.NationalityID.HasValue ? new KeyValueViewModel()
                {
                    Key = studentDTO.StudentPassportDetails.NationalityID.ToString(),
                    Value = studentDTO.StudentPassportDetails.National.Value
                } : new KeyValueViewModel(),
                PassportNo = studentDTO.StudentPassportDetails.PassportNo,
                AdhaarCardNo = studentDTO.StudentPassportDetails.AdhaarCardNo,
                CountryofIssueID = studentDTO.StudentPassportDetails.CountryofIssueID,
                CountryofIssue = studentDTO.StudentPassportDetails.CountryofIssueID.HasValue ? new KeyValueViewModel()
                {
                    Key = studentDTO.StudentPassportDetails.CountryofIssueID.ToString(),
                    Value = studentDTO.StudentPassportDetails.CountryofIssue.Value
                } : new KeyValueViewModel(),
                PassportNoString = studentDTO.StudentPassportDetails.PassportNoExpiry.HasValue ? studentDTO.StudentPassportDetails.PassportNoExpiry.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                CountryofBirthID = studentDTO.StudentPassportDetails.CountryofBirthID,
                CountryofBirth = studentDTO.StudentPassportDetails.CountryofBirthID.HasValue ? new KeyValueViewModel()
                {
                    Key = studentDTO.StudentPassportDetails.CountryofBirthID.ToString(),
                    Value = studentDTO.StudentPassportDetails.CountryofBirth.Value
                } : new KeyValueViewModel(),
                VisaNo = studentDTO.StudentPassportDetails.VisaNo,
                VisaExpiryString = studentDTO.StudentPassportDetails.VisaExpiry.HasValue ? studentDTO.StudentPassportDetails.VisaExpiry.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                NationalIDNo = studentDTO.StudentPassportDetails.NationalIDNo,
                NationalIDNoExpiryString = studentDTO.StudentPassportDetails.NationalIDNoExpiry.HasValue ? studentDTO.StudentPassportDetails.NationalIDNoExpiry.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,


            };
            vm.StudentLogin = new StudentLoginViewModel()
            {
                LoginIID = studentDTO.Login.LoginIID,
                LoginUserID = studentDTO.Login.LoginUserID,
                LoginEmailID = studentDTO.Login.LoginEmailID,
                LastLoginDate = studentDTO.Login.LastLoginDate,
                IsRequired = studentDTO.Login.IsRequired,
                Password = studentDTO.Login.Password,
                PasswordHint = studentDTO.Login.PasswordHint,
                //StatusID = studentDTO.Login.StatusID,
                Status = studentDTO.Login.StatusID.HasValue ?
                new KeyValueViewModel()
                {
                    Key = ((int)studentDTO.Login.StatusID.Value).ToString(),
                    Value = studentDTO.Login.StatusID.Value.ToString()
                } : null,
            };
            vm.ParentLogin = new ParentLoginViewModel()
            {
                LoginIID = studentDTO.ParentLogin.LoginIID,
                LoginUserID = studentDTO.ParentLogin.LoginUserID,
                LoginEmailID = studentDTO.ParentLogin.LoginEmailID,
                LastLoginDate = studentDTO.ParentLogin.LastLoginDate,
                IsRequired = studentDTO.ParentLogin.IsRequired,
                Password = studentDTO.ParentLogin.Password,
                PasswordHint = studentDTO.ParentLogin.PasswordHint,
                PasswordSalt = studentDTO.ParentLogin.PasswordSalt,
                //StatusID = studentDTO.Login.StatusID,
                Status = studentDTO.ParentLogin.StatusID.HasValue ?
                new KeyValueViewModel()
                {
                    Key = ((int)studentDTO.ParentLogin.StatusID.Value).ToString(),
                    Value = studentDTO.ParentLogin.StatusID.Value.ToString()
                } : null,
                LoginRoleMapIID = studentDTO.ParentLogin.LoginRoleMaps.LoginRoleMapIID,
            };

            vm.DocumentsUpload = new List<StudentApplicationDocUploadViewModel>();

            vm.DocumentsUpload.Add(new StudentApplicationDocUploadViewModel()
            {
                ApplicationDocumentIID = studentDTO.StudentDocUploads.ApplicationDocumentIID,
                ApplicationID = studentDTO.StudentDocUploads.ApplicationID,
                BirthCertificateReferenceID = studentDTO.StudentDocUploads.BirthCertificateReferenceID.HasValue ? studentDTO.StudentDocUploads.BirthCertificateReferenceID.ToString() : null,
                BirthCertificateAttach = studentDTO.StudentDocUploads.BirthCertificateAttach,
                StudentPassportReferenceID = studentDTO.StudentDocUploads.StudentPassportReferenceID.HasValue ? studentDTO.StudentDocUploads.StudentPassportReferenceID.ToString() : null,
                StudentPassportAttach = studentDTO.StudentDocUploads.StudentPassportAttach,
                TCReferenceID = studentDTO.StudentDocUploads.TCReferenceID.HasValue ? studentDTO.StudentDocUploads.TCReferenceID.ToString() : null,
                TCAttach = studentDTO.StudentDocUploads.TCAttach,
                FatherQIDReferenceID = studentDTO.StudentDocUploads.FatherQIDReferenceID.HasValue ? studentDTO.StudentDocUploads.FatherQIDReferenceID.ToString() : null,
                FatherQIDAttach = studentDTO.StudentDocUploads.FatherQIDAttach,
                MotherQIDReferenceID = studentDTO.StudentDocUploads.MotherQIDReferenceID.HasValue ? studentDTO.StudentDocUploads.MotherQIDReferenceID.ToString() : null,
                MotherQIDAttach = studentDTO.StudentDocUploads.MotherQIDAttach,
                StudentQIDReferenceID = studentDTO.StudentDocUploads.StudentQIDReferenceID.HasValue ? studentDTO.StudentDocUploads.StudentQIDReferenceID.ToString() : null,
                StudentQIDAttach = studentDTO.StudentDocUploads.StudentQIDAttach,
            });

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            Mapper<StudentViewModel, StudentDTO>.CreateMap();
            Mapper<GaurdianViewModel, GuardianDTO>.CreateMap();
            Mapper<StudentPassportDetailViewModel, StudentPassportDetailDTO>.CreateMap();
            Mapper<StudentAdditionalDetailsViewModel, AdditionalInfoDTO>.CreateMap();
            Mapper<StudentLoginViewModel, LoginsDTO>.CreateMap();
            Mapper<ParentLoginViewModel, LoginsDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<StudentViewModel, StudentDTO>.Map(this);

            dto.AdmissionNumber = this.AdmissionNumber;
            dto.StudentIID = this.StudentIID;
            dto.FirstName = this.StudentName.FirstName;
            dto.MiddleName = this.StudentName.MiddleName;
            dto.LastName = this.StudentName.LastName;
            dto.ApplicationID = this.ApplicationID.HasValue ? this.ApplicationID : null;
            dto.SchoolID = string.IsNullOrEmpty(this.School) ? (byte?)null : byte.Parse(this.School);
            //dto.AcademicYearID = this.AcademicYearID;
            dto.AcademicYearID = string.IsNullOrEmpty(this.Academicyear) ? (int?)null : int.Parse(this.Academicyear);
            //dto.SchoolAcademicyearID = string.IsNullOrEmpty(this.SchoolAcademicyear) ? (int?)null : int.Parse(this.SchoolAcademicyear);
            dto.SchoolAcademicyearID = this.SchoolAcademicYearID;
            dto.ClassID = this.StudentClass == null || string.IsNullOrEmpty(this.StudentClass.Key) ? (int?)null : int.Parse(this.StudentClass.Key);
            dto.SectionID = this.Section == null || string.IsNullOrEmpty(this.Section.Key) ? (int?)null : int.Parse(this.Section.Key);
            dto.GradID = string.IsNullOrEmpty(this.Grade) ? (byte?)null : byte.Parse(this.Grade);
            dto.GenderID = string.IsNullOrEmpty(this.Gender) ? (byte?)null : byte.Parse(this.Gender);
            dto.StreamID = this.Stream == null || string.IsNullOrEmpty(this.Stream.Key) ? (byte?)null : byte.Parse(this.Stream.Key);
            //dto.CategoryID = this.Category == null || string.IsNullOrEmpty(this.Category.Key) ? (int?)null : int.Parse(this.Category.Key);
            //dto.CastID = string.IsNullOrEmpty(this.Cast) ? (byte?)null : byte.Parse(this.Cast);
            dto.RelegionID = string.IsNullOrEmpty(this.Relegion) ? (byte?)null : byte.Parse(this.Relegion);
            dto.BloodGroupID = string.IsNullOrEmpty(this.BloodGroup) ? (int?)null : int.Parse(this.BloodGroup);
            dto.StudentHouseID = string.IsNullOrEmpty(this.StudentHouse) ? (int?)null : int.Parse(this.StudentHouse);
            dto.DateOfBirth = string.IsNullOrEmpty(this.DateOfBirthString) ? (DateTime?)null : DateTime.ParseExact(this.DateOfBirthString, dateFormat, CultureInfo.InvariantCulture);
            dto.InactiveDate = string.IsNullOrEmpty(this.InActiveDateString) ? (DateTime?)null : DateTime.ParseExact(this.DateOfBirthString, dateFormat, CultureInfo.InvariantCulture);
            dto.AsOnDate = string.IsNullOrEmpty(this.AsOnDateString) ? (DateTime?)null : DateTime.ParseExact(this.AsOnDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.AdmissionDate = string.IsNullOrEmpty(this.AdmissionDateString) ? (DateTime?)null : DateTime.ParseExact(this.AdmissionDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.HostelID = string.IsNullOrEmpty(this.Hostels.Hostel) ? (int?)null : int.Parse(this.Hostels.Hostel);
            dto.RoomID = string.IsNullOrEmpty(this.Hostels.HostelRoom) ? (int?)null : int.Parse(this.Hostels.HostelRoom);
            dto.LoginID = this.LoginID.HasValue ? this.LoginID : (long?)null;
            dto.ParentID = this.ParentID.HasValue ? this.ParentID : (long?)null;
            dto.StudentProfile = this.ProfileUrl;
            dto.FeeStartDate = string.IsNullOrEmpty(this.FeeStartDateString) ? (DateTime?)null : DateTime.ParseExact(this.FeeStartDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.IsActive = this.IsActive;
            dto.IsMinority = this.IsMinority;
            dto.IsOnlyChildofParent = this.IsOnlyChildofParent;
            dto.PrimaryContactID = string.IsNullOrEmpty(this.PrimaryContact) || this.PrimaryContact == "?" ? (byte?)null : byte.Parse(this.PrimaryContact);
            dto.CastID = string.IsNullOrEmpty(this.Cast) || this.Cast == "?" ? (byte?)null : byte.Parse(this.Cast);
            dto.SecoundLanguageID = string.IsNullOrEmpty(this.SecoundLanguageString) || this.SecoundLanguageString == "?" ? (int?)null : int.Parse(this.SecoundLanguageString);
            dto.ThridLanguageID = string.IsNullOrEmpty(this.ThridLanguageString) || this.ThridLanguageString == "?" ? (int?)null : int.Parse(this.ThridLanguageString);
            dto.CommunityID = string.IsNullOrEmpty(this.Community) || this.Community == "?" ? (byte?)null : byte.Parse(this.Community);
            dto.SubjectMapID = string.IsNullOrEmpty(this.SubjectMapString) || this.SubjectMapString == "?" ? (int?)null : int.Parse(this.SubjectMapString);
            dto.onStreams = this.onStreams;
            dto.IsStudentStudiedBefore = this.PreviousSchoolDetails.IsStudentStudiedBefore;
            dto.PreviousSchoolName = this.PreviousSchoolDetails.PreviousSchoolName;
            dto.PreviousSchoolAcademicYear = this.PreviousSchoolDetails.PreviousSchoolAcademicYear;
            dto.PreviousSchoolAddress = this.PreviousSchoolDetails.PreviousSchoolAddress;
            dto.PreviousSchoolSyllabusID = string.IsNullOrEmpty(this.PreviousSchoolDetails.PreviousSchoolSyllabus) || this.PreviousSchoolDetails.PreviousSchoolSyllabus == "?" ? (byte?)null : byte.Parse(this.PreviousSchoolDetails.PreviousSchoolSyllabus);
            //dto.Status = string.IsNullOrEmpty(this.StudentStatus) || this.StudentStatus == "?" ? (byte?)null : byte.Parse(this.StudentStatus);
            dto.Status = this.StudentStatus == null || string.IsNullOrEmpty(this.StudentStatus.Key) ? (byte?)null : byte.Parse(this.StudentStatus.Key);
            dto.MobileNumber = this.MobileNumber;

            //if (this.PreviousSchoolDetails.PreviousSchoolClass.Key != null)
            //{
            //    dto.PreviousSchoolClassCompletedID = int.Parse(this.PreviousSchoolDetails.PreviousSchoolClass.Key);
            //}

            //if (this.PreviousSchoolDetails.PreviousSchoolClassClassKey != null)
            //{
            //    dto.PreviousSchoolClassCompletedID = int.Parse(this.PreviousSchoolDetails.PreviousSchoolClassClassKey);
            //}
            dto.PreviousSchoolClassCompletedID = this.PreviousSchoolDetails.PreviousSchoolClass == null || string.IsNullOrEmpty(this.PreviousSchoolDetails.PreviousSchoolClass.Key) ? (int?)null : int.Parse(this.PreviousSchoolDetails.PreviousSchoolClass.Key);

            if (this.Sibling.Count > 0)
            {
                foreach (KeyValueViewModel kvm in this.Sibling)
                {
                    dto.StudentSiblings.Add(new KeyValueDTO()
                    { Key = kvm.Key, Value = kvm.Value });
                }
            }
            List<KeyValueDTO> StudentDiscountGroupList = new List<KeyValueDTO>();

            foreach (KeyValueViewModel vm in this.AdditionlInfo.StudentGroup)
            {
                StudentDiscountGroupList.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value }
                    );
            }
            dto.AdditionalInfo.StudentGroups = StudentDiscountGroupList;

            dto.OptionalSubjects = new List<KeyValueDTO>();
            if (this.OptionalSubjects != null && this.OptionalSubjects.Count > 0)
            {
                foreach (KeyValueViewModel optSubj in this.OptionalSubjects)
                {
                    dto.OptionalSubjects.Add(new KeyValueDTO()
                    { Key = optSubj.Key, Value = optSubj.Value });
                }
            }

            dto.StudentStaffMaps = new List<KeyValueDTO>();
            if (this.Staff.Count > 0)
            {
                foreach (KeyValueViewModel kvm in this.Staff)
                {
                    dto.StudentStaffMaps.Add(new KeyValueDTO()
                    { Key = kvm.Key, Value = kvm.Value });
                }
            }

            dto.Guardian = new GuardianDTO()
            {
                //FatherName = this.Guardians.FatherName,
                ParentCode = this.Guardians.ParentCode,
                FatherFirstName = this.Guardians.FatherFirstName,
                FatherMiddleName = this.Guardians.FatherMiddleName,
                FatherLastName = this.Guardians.FatherLastName,
                FatherCompanyName = this.Guardians.FatherCompanyName,
                FatherOccupation = this.Guardians.FatherOccupation,
                PhoneNumber = this.Guardians.FatherPhoneNumber,
                FatherWhatsappMobileNo = this.Guardians.FatherWhatsappMobileNo,
                FatherEmailID = this.Guardians.FatherEmailID,
                //PhoneNumber = this.Guardians.PhoneNumber,
                FatherMobileNumberTwo = this.Guardians.FatherMobileNumberTwo,
                //FatherProfile = this.Guardians.FatherProfileUrl,
                CanYouVolunteerToHelpOneID = string.IsNullOrEmpty(this.Guardians.CanYouVolunteerToHelpOneString) ? (int?)null : byte.Parse(this.Guardians.CanYouVolunteerToHelpOneString),

                FatherPassportNumber = this.Guardians.FatherPassportNumber,
                FatherPassportCountryofIssueID = string.IsNullOrEmpty(this.Guardians.FatherCountryofIssue) ? (int?)null : int.Parse(this.Guardians.FatherCountryofIssue),
                FatherPassportNoIssueDate = string.IsNullOrEmpty(this.Guardians.FatherPassportNoIssueString) ? (DateTime?)null : DateTime.ParseExact(this.Guardians.FatherPassportNoIssueString, dateFormat, CultureInfo.InvariantCulture),
                FatherPassportNoExpiryDate = string.IsNullOrEmpty(this.Guardians.FatherPassportNoExpiryString) ? (DateTime?)null : DateTime.ParseExact(this.Guardians.FatherPassportNoExpiryString, dateFormat, CultureInfo.InvariantCulture),

                FatherNationalID = this.Guardians.FatherNationalID,
                FatherNationalDNoIssueDate = string.IsNullOrEmpty(this.Guardians.FatherNationalDNoIssueDateString) ? (DateTime?)null : DateTime.ParseExact(this.Guardians.FatherNationalDNoIssueDateString, dateFormat, CultureInfo.InvariantCulture),
                FatherNationalDNoExpiryDate = string.IsNullOrEmpty(this.Guardians.FatherNationalDNoExpiryDateString) ? (DateTime?)null : DateTime.ParseExact(this.Guardians.FatherNationalDNoExpiryDateString, dateFormat, CultureInfo.InvariantCulture),
                FatherCountryID = string.IsNullOrEmpty(this.Guardians.FatherCountry) || this.Guardians.FatherCountry == "?" ? (int?)null : int.Parse(this.Guardians.FatherCountry),

                //MotherName = this.Guardians.MotherName,
                MotherFirstName = this.Guardians.MotherFirstName,
                MotherMiddleName = this.Guardians.MotherMiddleName,
                MotherLastName = this.Guardians.MotherLastName,
                MotherCompanyName = this.Guardians.MotherCompanyName,
                MotherOccupation = this.Guardians.MotherOccupation,
                MotherPhone = this.Guardians.MotherPhone,
                MotherWhatsappMobileNo = this.Guardians.MotherWhatsappMobileNo,
                CanYouVolunteerToHelpTwoID = string.IsNullOrEmpty(this.Guardians.CanYouVolunteerToHelpTwoString) ? (int?)null : byte.Parse(this.Guardians.CanYouVolunteerToHelpTwoString),
                MotherEmailID = this.Guardians.MotherEmailID,
                //MotherPofile = this.Guardians.MotherPofileUrl,

                MotherNationalID = this.Guardians.MotherNationalID,
                MotherNationalDNoIssueDate = string.IsNullOrEmpty(this.Guardians.MotherNationalDNoIssueDateString) ? (DateTime?)null : DateTime.ParseExact(this.Guardians.MotherNationalDNoIssueDateString, dateFormat, CultureInfo.InvariantCulture),
                MotherNationalDNoExpiryDate = string.IsNullOrEmpty(this.Guardians.MotherNationaIDNoExpiryDateString) ? (DateTime?)null : DateTime.ParseExact(this.Guardians.MotherNationaIDNoExpiryDateString, dateFormat, CultureInfo.InvariantCulture),

                MotherPassportNumber = this.Guardians.MotherPassportNumber,
                MotherPassportCountryofIssueID = string.IsNullOrEmpty(this.Guardians.MotherCountryofIssue) ? (int?)null : int.Parse(this.Guardians.MotherCountryofIssue),
                MotherPassportNoIssueDate = string.IsNullOrEmpty(this.Guardians.MotherPassportNoIssueString) ? (DateTime?)null : DateTime.ParseExact(this.Guardians.MotherPassportNoIssueString, dateFormat, CultureInfo.InvariantCulture),
                MotherPassportNoExpiryDate = string.IsNullOrEmpty(this.Guardians.MotherPassportNoExpiryString) ? (DateTime?)null : DateTime.ParseExact(this.Guardians.MotherPassportNoExpiryString, dateFormat, CultureInfo.InvariantCulture),
                MotherCountryID = string.IsNullOrEmpty(this.Guardians.MotherCountry) || this.Guardians.MotherCountry == "?" ? (int?)null : int.Parse(this.Guardians.MotherCountry),

                ParentIID = this.Guardians.ParentIID,
                LoginID = this.ParentLogin.LoginIID,

                //Guardian Details toDto
                GuardianFirstName = this.Guardians.GuardianFirstName,
                GuardianMiddleName = this.Guardians.GuardianMiddleName,
                GuardianLastName = this.Guardians.GuardianLastName,
                GuardianTypeID = !string.IsNullOrEmpty(this.Guardians.GuardianType) ? byte.Parse(this.Guardians.GuardianType) : (byte?)null,
                GuardianOccupation = this.Guardians.GuardianOccupation,
                GuardianCompanyName = this.Guardians.GuardianCompanyName,
                GuardianPhone = this.Guardians.GuardianPhone,
                GuardianWhatsappMobileNo = this.Guardians.GuardianWhatsappMobileNo,
                GaurdianEmail = this.Guardians.GaurdianEmail,
                GuardianNationalityID = string.IsNullOrEmpty(this.Guardians.GuardianNationality) || this.Guardians.GuardianNationality == "?" ? (int?)null : int.Parse(this.Guardians.GuardianNationality),
                GuardianNationalID = this.Guardians.GuardianNationalID,
                GuardianNationalIDNoIssueDate = string.IsNullOrEmpty(this.Guardians.GuardianNationalIDNoIssueDateString) ? (DateTime?)null : DateTime.ParseExact(this.Guardians.GuardianNationalIDNoIssueDateString, dateFormat, CultureInfo.InvariantCulture),
                GuardianNationalIDNoExpiryDate = string.IsNullOrEmpty(this.Guardians.GuardianNationalIDNoExpiryDateString) ? (DateTime?)null : DateTime.ParseExact(this.Guardians.GuardianNationalIDNoExpiryDateString, dateFormat, CultureInfo.InvariantCulture),
                GuardianPassportNumber = this.Guardians.GuardianPassportNumber,
                GuardianCountryofIssueID = string.IsNullOrEmpty(this.Guardians.GuardianCountryofIssue) || this.Guardians.GuardianCountryofIssue == "?" ? (int?)null : int.Parse(this.Guardians.GuardianCountryofIssue),
                GuardianPassportNoIssueDate = string.IsNullOrEmpty(this.Guardians.GuardianPassportNoIssueString) ? (DateTime?)null : DateTime.ParseExact(this.Guardians.GuardianPassportNoIssueString, dateFormat, CultureInfo.InvariantCulture),
                GuardianPassportNoExpiryDate = string.IsNullOrEmpty(this.Guardians.GuardianPassportNoExpiryString) ? (DateTime?)null : DateTime.ParseExact(this.Guardians.GuardianPassportNoExpiryString, dateFormat, CultureInfo.InvariantCulture),

                //Guardian Address
                BuildingNo = this.Guardians.BuildingNo,
                FlatNo = this.Guardians.FlatNo,
                StreetNo = this.Guardians.StreetNo,
                StreetName = this.Guardians.StreetName,
                LocationNo = this.Guardians.LocationNo,
                LocationName = this.Guardians.LocationName,
                ZipNo = this.Guardians.ZipNo,
                PostBoxNo = this.Guardians.PostBoxNo,
                City = this.Guardians.City,
                CountryID = string.IsNullOrEmpty(this.Guardians.Country) || this.Guardians.Country == "?" ? (int?)null : int.Parse(this.Guardians.Country),
            };

            dto.AdditionalInfo = new AdditionalInfoDTO()
            {
                StudentMiscDetailsIID = this.Addresses.StudentMiscDetailsIID,
                //    GuardianTypeID = !string.IsNullOrEmpty(this.AdditionlInfo.GuardianType) ? byte.Parse(this.AdditionlInfo.GuardianType) : (byte?)null,
                //    //StaffID = string.IsNullOrEmpty(this.AdditionlInfo.Staff.Key) ? (long?)null : long.Parse(this.AdditionlInfo.Staff.Key),
                //    StaffID = this.AdditionlInfo.Staff == null || string.IsNullOrEmpty(this.AdditionlInfo.Staff.Key) ? (long?)null : long.Parse(this.AdditionlInfo.Staff.Key),
                //    StudentGroupID = !string.IsNullOrEmpty(this.AdditionlInfo.StudentGroup) ? byte.Parse(this.AdditionlInfo.StudentGroup) : (byte?)null,
                StudentGroups = StudentDiscountGroupList,
                //StudentGroupID = StudentDiscountGroupList,
                PermenentBuildingNo = this.Addresses.PermenentBuildingNo,
                PermenentFlatNo = this.Addresses.PermenentFlatNo,
                PermenentStreetNo = this.Addresses.PermenentStreetNo,
                PermenentStreetName = this.Addresses.PermenentStreetName,
                PermenentLocationNo = this.Addresses.PermenentLocationNo,
                PermenentLocationName = this.Addresses.PermenentLocationName,
                PermenentZipNo = this.Addresses.PermenentZipNo,
                PermenentPostBoxNo = this.Addresses.PermenentPostBoxNo,
                PermenentCity = this.Addresses.PermenentCity,
                IsCurrentAddresIsGuardian = this.Addresses.IsCurrentAddresIsGuardian,
                IsPermenentAddresIsCurrent = this.Addresses.IsPermenentAddresIsCurrent,
                PermenentCountryID = string.IsNullOrEmpty(this.Addresses.PermenentCountry) || this.Guardians.Country == "?" ? (int?)null : int.Parse(this.Addresses.PermenentCountry),

            };

            dto.StudentPassportDetails = new StudentPassportDetailDTO();
            if (this.StudentPassportDetails != null)
            {
                dto.StudentPassportDetails = new StudentPassportDetailDTO()
                {
                    StudentPassportDetailsIID = this.StudentPassportDetails.StudentPassportDetailsIID,
                    StudentID = this.StudentPassportDetails.StudentID,
                    //NationalityID = this.StudentPassportDetails.NationalityID,
                    NationalityID = this.StudentPassportDetails.Nationality == null || string.IsNullOrEmpty(this.StudentPassportDetails.Nationality.Key) ? (int?)null : int.Parse(this.StudentPassportDetails.Nationality.Key),
                    PassportNo = this.StudentPassportDetails.PassportNo,
                    AdhaarCardNo = this.StudentPassportDetails.AdhaarCardNo,
                    //CountryofIssueID = this.StudentPassportDetails.CountryofIssueID,
                    CountryofIssueID = this.StudentPassportDetails.CountryofIssue == null || string.IsNullOrEmpty(this.StudentPassportDetails.CountryofIssue.Key) ? (int?)null : int.Parse(this.StudentPassportDetails.CountryofIssue.Key),
                    //CountryofBirthID = this.StudentPassportDetails.CountryofBirthID,
                    CountryofBirthID = this.StudentPassportDetails.CountryofBirth == null || string.IsNullOrEmpty(this.StudentPassportDetails.CountryofBirth.Key) ? (int?)null : int.Parse(this.StudentPassportDetails.CountryofBirth.Key),
                    VisaNo = this.StudentPassportDetails.VisaNo,
                    NationalIDNo = this.StudentPassportDetails.NationalIDNo,
                    PassportNoExpiry = string.IsNullOrEmpty(this.StudentPassportDetails.PassportNoString) ? (DateTime?)null : DateTime.ParseExact(this.StudentPassportDetails.PassportNoString, dateFormat, CultureInfo.InvariantCulture),
                    VisaExpiry = string.IsNullOrEmpty(this.StudentPassportDetails.VisaExpiryString) ? (DateTime?)null : DateTime.ParseExact(this.StudentPassportDetails.VisaExpiryString, dateFormat, CultureInfo.InvariantCulture),
                    NationalIDNoExpiry = string.IsNullOrEmpty(this.StudentPassportDetails.NationalIDNoExpiryString) ? (DateTime?)null : DateTime.ParseExact(this.StudentPassportDetails.NationalIDNoExpiryString, dateFormat, CultureInfo.InvariantCulture),
                };
            }

            if (this.StudentLogin.LoginUserID != null)
            {
                dto.Login = new LoginsDTO()
                {
                    LoginIID = this.StudentLogin.LoginIID,
                    LoginUserID = this.StudentLogin.LoginUserID,
                    LoginEmailID = this.StudentLogin.LoginEmailID,
                    LastLoginDate = this.StudentLogin.LastLoginDate,
                    IsRequired = this.StudentLogin.IsRequired,
                    Password = this.StudentLogin.Password,
                    PasswordHint = this.StudentLogin.PasswordHint,
                    //StatusID = this.Login.StatusID,
                    StatusID = !string.IsNullOrEmpty(this.StudentLogin.Status.Key) ? (Infrastructure.Enums.LoginUserStatus)long.Parse(this.StudentLogin.Status.Key) : (Infrastructure.Enums.LoginUserStatus?)null,
                };
            }

            if (this.ParentLogin.LoginUserID != null)
            {
                dto.ParentLogin = new LoginsDTO()
                {
                    LoginIID = this.ParentLogin.LoginIID,
                    LoginUserID = this.ParentLogin.LoginUserID,
                    LoginEmailID = this.ParentLogin.LoginEmailID,
                    LastLoginDate = this.ParentLogin.LastLoginDate,
                    IsRequired = this.ParentLogin.IsRequired,
                    Password = this.ParentLogin.Password,
                    PasswordHint = this.ParentLogin.PasswordHint,
                    PasswordSalt = this.ParentLogin.PasswordSalt,
                    //StatusID = this.Login.StatusID,
                    StatusID = !string.IsNullOrEmpty(this.ParentLogin.Status.Key) ?
                    (Infrastructure.Enums.LoginUserStatus)long.Parse(this.ParentLogin.Status.Key) : (Infrastructure.Enums.LoginUserStatus?)null,
                };

                dto.ParentLogin.LoginRoleMaps.LoginRoleMapIID = this.ParentLogin.LoginRoleMapIID;
            }

            dto.StudentDocUploads = new StudentApplicationDocumentsUploadDTO();
            foreach (var attachmentMap in this.DocumentsUpload)
            {
                dto.StudentDocUploads = new StudentApplicationDocumentsUploadDTO()
                {
                    ApplicationDocumentIID = attachmentMap.ApplicationDocumentIID != 0 ? attachmentMap.ApplicationDocumentIID : 0,
                    ApplicationID = attachmentMap.ApplicationID,
                    BirthCertificateReferenceID = attachmentMap.BirthCertificateReferenceID != null ? long.Parse(attachmentMap.BirthCertificateReferenceID) : (long?)null,
                    BirthCertificateAttach = attachmentMap.BirthCertificateAttach != null ? attachmentMap.BirthCertificateAttach : null,
                    StudentPassportReferenceID = attachmentMap.StudentPassportReferenceID != null ? long.Parse(attachmentMap.StudentPassportReferenceID) : (long?)null,
                    StudentPassportAttach = attachmentMap.StudentPassportAttach != null ? attachmentMap.StudentPassportAttach : null,
                    TCReferenceID = attachmentMap.TCReferenceID != null ? long.Parse(attachmentMap.TCReferenceID) : (long?)null,
                    TCAttach = attachmentMap.TCAttach != null ? attachmentMap.TCAttach : null,
                    FatherQIDReferenceID = attachmentMap.FatherQIDReferenceID != null ? long.Parse(attachmentMap.FatherQIDReferenceID) : (long?)null,
                    FatherQIDAttach = attachmentMap.FatherQIDAttach != null ? attachmentMap.FatherQIDAttach : null,
                    MotherQIDReferenceID = attachmentMap.MotherQIDReferenceID != null ? long.Parse(attachmentMap.MotherQIDReferenceID) : (long?)null,
                    MotherQIDAttach = attachmentMap.MotherQIDAttach != null ? attachmentMap.MotherQIDAttach : null,
                    StudentQIDReferenceID = attachmentMap.StudentQIDReferenceID != null ? long.Parse(attachmentMap.StudentQIDReferenceID) : (long?)null,
                    StudentQIDAttach = attachmentMap.StudentQIDAttach != null ? attachmentMap.StudentQIDAttach : null,
                };
            }

            return dto;
        }

        public StudentViewModel FromStudentApplicationVM(StudentApplicationViewModel applicationVM)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var vm = new StudentViewModel()
            {
                ApplicationID = applicationVM.ApplicationIID,
                AdmissionDate = DateTime.Now,
                AdmissionDateString = DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture),
                DateOfBirth = applicationVM.DateOfBirth,
                DateOfBirthString = applicationVM.DateOfBirth.HasValue ? applicationVM.DateOfBirth.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                Gender = applicationVM.GenderID.HasValue ? applicationVM.GenderID.Value.ToString() : null,
                Stream = applicationVM.StreamID.HasValue ? new KeyValueViewModel() { Key = applicationVM.StreamID.ToString(), Value = applicationVM.Stream.Value } : new KeyValueViewModel(),
                StudentClass = applicationVM.ClassID.HasValue ? new KeyValueViewModel() { Key = applicationVM.ClassID.ToString(), Value = applicationVM.StudentClass.Value } : new KeyValueViewModel(),
                Cast = applicationVM.CastID.HasValue ? applicationVM.CastID.Value.ToString() : null,
                Relegion = applicationVM.RelegionID.HasValue ? applicationVM.RelegionID.Value.ToString() : null,
                Community = applicationVM.CommunityID.HasValue ? applicationVM.CommunityID.Value.ToString() : null,
                MobileNumber = applicationVM.FatherMotherDetails.MobileNumber != null ? applicationVM.FatherMotherDetails.MobileNumber : null,
                BloodGroup = applicationVM.BloodGroupID.HasValue ? applicationVM.BloodGroupID.Value.ToString() : null,
                IsMinority = applicationVM.IsMinority,
                IsOnlyChildofParent = applicationVM.IsOnlyChildofParent,
                School = applicationVM.SchoolID.HasValue ? applicationVM.SchoolID.ToString() : null,
                Academicyear = applicationVM.AcademicyearID.HasValue ? applicationVM.AcademicyearID.ToString() : null,
                AcademicYearID = applicationVM.AcademicyearID,
                //Category = applicationVM.CategoryID.HasValue ? new KeyValueViewModel() { Key = applicationVM.CategoryID.ToString(), Value = applicationVM.Category } : new KeyValueViewModel(),
                EmailID = applicationVM.FatherMotherDetails.EmailID != null ? applicationVM.FatherMotherDetails.EmailID : null,
                PrimaryContact = applicationVM.PrimaryContactID.HasValue ? applicationVM.PrimaryContactID.Value.ToString() : null,
                SecoundLanguageString = applicationVM.SecoundLanguageID.HasValue ? applicationVM.SecoundLanguageID.Value.ToString() : null,
                ThridLanguageString = applicationVM.ThridLanguageID.HasValue ? applicationVM.ThridLanguageID.Value.ToString() : null,
                ProfileUrl = applicationVM.ProfileUrl,
                StudentName = new StudentNameViewModel()
                {
                    FirstName = applicationVM.FirstName,
                    MiddleName = applicationVM.MiddleName != null ? applicationVM.MiddleName : null,
                    LastName = applicationVM.LastName,
                },
                AdditionlInfo = new StudentAdditionalDetailsViewModel(),
                Sibling = applicationVM.Siblings,
                OptionalSubjects = applicationVM.OptionalSubjects,
                DocumentsUpload = new List<StudentApplicationDocUploadViewModel>(),
                StudentPassportDetails = new StudentPassportDetailViewModel()
                {
                    PassportNo = applicationVM.StudentPassportNo,
                    AdhaarCardNo = applicationVM.AdhaarCardNo,
                    CountryofIssue = applicationVM.CountryofIssueID.HasValue ? new KeyValueViewModel() { Key = applicationVM.CountryofIssue.Key, Value = applicationVM.CountryofIssue.Value } : null,
                    PassportNoString = applicationVM.PassportNoExpiryString,
                    Nationality = new KeyValueViewModel() { Key = applicationVM.StudentNationality.Key, Value = applicationVM.StudentNationality.Value },
                    CountryofBirth = new KeyValueViewModel() { Key = applicationVM.CoutryOfBrith.Key, Value = applicationVM.CoutryOfBrith.Value },
                    VisaNo = applicationVM.StudentVisaNo,
                    VisaExpiryString = applicationVM.VisaExpiryDateString,
                    NationalIDNo = applicationVM.StudentNationalID,
                    NationalIDNoExpiryString = applicationVM.StudentNationalIDNoExpiryDateString,
                },
                Addresses = new AddressViewModel()
                {
                    PermenentBuildingNo = applicationVM.Address.BuildingNo,
                    PermenentFlatNo = applicationVM.Address.FlatNo,
                    PermenentStreetNo = applicationVM.Address.StreetNo,
                    PermenentStreetName = applicationVM.Address.StreetName,
                    PermenentLocationNo = applicationVM.Address.LocationNo,
                    PermenentLocationName = applicationVM.Address.LocationName,
                    PermenentZipNo = applicationVM.Address.ZipNo,
                    PermenentPostBoxNo = applicationVM.Address.PostBoxNo,
                    PermenentCity = applicationVM.Address.City,
                    PermenentCountry = applicationVM.Address.Country,
                },
                Guardians = new GaurdianViewModel()
                {
                    ParentIID = applicationVM.ParentID.HasValue ? Convert.ToInt64(applicationVM.ParentID) : 0,
                    ParentCode = applicationVM.ParentCode != null ? applicationVM.ParentCode : null,
                    FatherFirstName = applicationVM.FatherMotherDetails.FatherFirstName,
                    FatherMiddleName = applicationVM.FatherMotherDetails.FatherMiddleName != null ? applicationVM.FatherMotherDetails.FatherMiddleName : null,
                    FatherLastName = applicationVM.FatherMotherDetails.FatherLastName,
                    FatherCompanyName = applicationVM.FatherMotherDetails.FatherCompanyName,
                    FatherMobileNumberTwo = applicationVM.FatherMotherDetails.FatherMobileNumberTwo,
                    FatherCountry = applicationVM.FatherMotherDetails.FatherCountryID.HasValue ? applicationVM.FatherMotherDetails.FatherCountryID.Value.ToString() : null,
                    FatherOccupation = applicationVM.FatherMotherDetails.FatherOccupation,
                    FatherPhoneNumber = applicationVM.FatherMotherDetails.MobileNumber,
                    FatherWhatsappMobileNo = applicationVM.FatherMotherDetails.FatherWhatsappMobileNo,
                    FatherEmailID = applicationVM.FatherMotherDetails.EmailID,

                    FatherNationalID = applicationVM.FatherMotherDetails.FatherNationalID,
                    FatherNationalDNoExpiryDate = applicationVM.FatherMotherDetails.FatherNationalDNoExpiryDate,
                    FatherNationalDNoIssueDate = applicationVM.FatherMotherDetails.FatherNationalDNoIssueDate,
                    FatherNationalDNoIssueDateString = applicationVM.FatherMotherDetails.FatherNationalDNoIssueDateString,
                    FatherNationalDNoExpiryDateString = applicationVM.FatherMotherDetails.FatherNationalDNoExpiryDateString,

                    FatherPassportNumber = applicationVM.FatherMotherDetails.FatherPassportNumber,
                    FatherCountryofIssue = applicationVM.FatherMotherDetails.FatherCountryofIssueID.HasValue ? applicationVM.FatherMotherDetails.FatherCountryofIssueID.Value.ToString() : null,
                    FatherPassportNoIssueString = applicationVM.FatherMotherDetails.FatherPassportNoIssueString,
                    FatherPassportNoExpiryString = applicationVM.FatherMotherDetails.FatherPassportNoExpiryString,

                    CanYouVolunteerToHelpOneString = applicationVM.FatherMotherDetails.CanYouVolunteerToHelpOneID.HasValue ? applicationVM.FatherMotherDetails.CanYouVolunteerToHelpOneID.Value.ToString() : null,

                    MotherFirstName = applicationVM.FatherMotherDetails.MotherFirstName,
                    MotherMiddleName = applicationVM.FatherMotherDetails.MotherMiddleName != null ? applicationVM.FatherMotherDetails.MotherMiddleName : null,
                    MotherLastName = applicationVM.FatherMotherDetails.MotherLastName,
                    MotherCompanyName = applicationVM.FatherMotherDetails.MotherCompanyName,
                    MotherEmailID = applicationVM.FatherMotherDetails.MotherEmailID,
                    CanYouVolunteerToHelpTwoString = applicationVM.FatherMotherDetails.CanYouVolunteerToHelpTwoID.HasValue ? applicationVM.FatherMotherDetails.CanYouVolunteerToHelpTwoID.Value.ToString() : null,
                    MotherCountry = applicationVM.FatherMotherDetails.MotherCountryID.HasValue ? applicationVM.FatherMotherDetails.MotherCountryID.Value.ToString() : null,

                    MotherNationalID = applicationVM.FatherMotherDetails.MotherNationalID,
                    //MotherNationaIDNoExpiryDate = applicationVM.FatherMotherDetails.MotherNationaIDNoExpiryDate,
                    //MotherNationalDNoIssueDate = applicationVM.FatherMotherDetails.MotherNationalDNoIssueDate,
                    MotherNationalDNoIssueDateString = applicationVM.FatherMotherDetails.MotherNationalDNoIssueDateString,
                    MotherNationaIDNoExpiryDateString = applicationVM.FatherMotherDetails.MotherNationaIDNoExpiryDateString,

                    MotherPassportNumber = applicationVM.FatherMotherDetails.MotherPassportNumber,
                    MotherCountryofIssue = applicationVM.FatherMotherDetails.MotherCountryofIssueID.HasValue ? applicationVM.FatherMotherDetails.MotherCountryofIssueID.Value.ToString() : null,
                    MotherPassportNoIssueString = applicationVM.FatherMotherDetails.MotherPassportNoIssueString,
                    MotherPassportNoExpiryString = applicationVM.FatherMotherDetails.MotherPassportNoExpiryString,
                    MotherPhone = applicationVM.FatherMotherDetails.MotherMobileNumber,
                    MotherWhatsappMobileNo = applicationVM.FatherMotherDetails.MotherWhatsappMobileNo,
                    MotherOccupation = applicationVM.FatherMotherDetails.MotherOccupation,

                    //Guardian details 
                    GuardianFirstName = applicationVM.GuardianDetails.GuardianFirstName,
                    GuardianMiddleName = applicationVM.GuardianDetails.GuardianMiddleName,
                    GuardianLastName = applicationVM.GuardianDetails.GuardianLastName,
                    GuardianTypeID = applicationVM.GuardianDetails.GuardianStudentRelationShipID,
                    GuardianType = applicationVM.GuardianDetails.GuardianStudentRelationShipID.ToString(),
                    GuardianOccupation = applicationVM.GuardianDetails.GuardianOccupation,
                    GuardianCompanyName = applicationVM.GuardianDetails.GuardianCompanyName,
                    GuardianPhone = applicationVM.GuardianDetails.GuardianMobileNumber,
                    GuardianWhatsappMobileNo = applicationVM.FatherMotherDetails.MotherWhatsappMobileNo,
                    GaurdianEmail = applicationVM.GuardianDetails.GuardianEmailID,
                    GuardianNationalityID = applicationVM.GuardianDetails.GuardianNationalityID,
                    GuardianNationality = applicationVM.GuardianDetails.GuardianNationalityID.ToString(),
                    GuardianNationalID = applicationVM.GuardianDetails.GuardianNationalID,
                    GuardianNationalIDNoIssueDateString = applicationVM.GuardianDetails.GuardianNationalIDNoIssueDateString,
                    GuardianNationalIDNoExpiryDateString = applicationVM.GuardianDetails.GuardianNationalIDNoExpiryDateString,
                    GuardianPassportNumber = applicationVM.GuardianDetails.GuardianPassportNumber,
                    GuardianCountryofIssueID = applicationVM.GuardianDetails.CountryofIssueID,
                    GuardianCountryofIssue = applicationVM.GuardianDetails.CountryofIssueID.ToString(),
                    GuardianPassportNoIssueString = applicationVM.GuardianDetails.GuardianPassportNoIssueString,
                    GuardianPassportNoExpiryString = applicationVM.GuardianDetails.GuardianPassportNoExpiryString,


                    BuildingNo = applicationVM.Address.BuildingNo,
                    FlatNo = applicationVM.Address.FlatNo,
                    StreetNo = applicationVM.Address.StreetNo,
                    StreetName = applicationVM.Address.StreetName,
                    LocationNo = applicationVM.Address.LocationNo,
                    LocationName = applicationVM.Address.LocationName,
                    ZipNo = applicationVM.Address.ZipNo,
                    PostBoxNo = applicationVM.Address.PostBoxNo,
                    City = applicationVM.Address.City,
                    Country = applicationVM.Address.CountryID.HasValue ? applicationVM.Address.CountryID.ToString() : null,
                },
                PreviousSchoolDetails = new PreviousSchoolDetailsViewModel()
                {
                    IsStudentStudiedBefore = applicationVM.PreviousSchoolDetails.IsStudentStudiedBefore,
                    PreviousSchoolAcademicYear = applicationVM.PreviousSchoolDetails.PreviousSchoolAcademicYear,
                    PreviousSchoolAddress = applicationVM.PreviousSchoolDetails.PreviousSchoolAddress,
                    PreviousSchoolName = applicationVM.PreviousSchoolDetails.PreviousSchoolName,
                    PreviousSchoolSyllabus = applicationVM.PreviousSchoolDetails.PreviousSchoolSyllabusID.HasValue ? applicationVM.PreviousSchoolDetails.PreviousSchoolSyllabusID.Value.ToString() : null,
                    PreviousSchoolClass = applicationVM.PreviousSchoolDetails.PreviousSchoolClassCompletedID.HasValue ? new KeyValueViewModel()
                    {
                        Key = applicationVM.PreviousSchoolDetails.PreviousSchoolClassCompletedID.ToString(),
                        Value = applicationVM.PreviousSchoolDetails.PreviousSchoolClass.Value
                    } : new KeyValueViewModel(),

                },

                //LoginID = applicationVM.LoginID.HasValue ? applicationVM.LoginID : (long?)null,

                ParentLogin = new ParentLoginViewModel()
                {
                    LoginIID = applicationVM.LoginID.HasValue ? Convert.ToInt64(applicationVM.LoginID) : 0,
                    LoginEmailID = applicationVM.ParentLoginEmailID != null ? applicationVM.ParentLoginEmailID : null,
                    LoginUserID = applicationVM.ParentLoginUserID != null ? applicationVM.ParentLoginUserID : null,
                    Password = applicationVM.ParentLoginPassword != null ? applicationVM.ParentLoginPassword : null,
                    PasswordSalt = applicationVM.ParentLoginPasswordSalt != null ? applicationVM.ParentLoginPasswordSalt : null,
                },

            };

            vm.DocumentsUpload = new List<StudentApplicationDocUploadViewModel>();
            foreach (var dat in applicationVM.DocumentsUpload)
            {
                vm.DocumentsUpload.Add(new StudentApplicationDocUploadViewModel()
                {
                    ApplicationID = dat.ApplicationID,
                    ApplicationDocumentIID = dat.ApplicationDocumentIID,
                    BirthCertificateReferenceID = dat.BirthCertificateReferenceID,
                    BirthCertificateAttach = dat.BirthCertificateAttach,
                    StudentPassportReferenceID = dat.StudentPassportReferenceID,
                    StudentPassportAttach = dat.StudentPassportAttach,
                    TCReferenceID = dat.TCReferenceID,
                    TCAttach = dat.TCAttach,
                    FatherQIDReferenceID = dat.FatherQIDReferenceID,
                    FatherQIDAttach = dat.FatherQIDAttach,
                    MotherQIDReferenceID = dat.MotherQIDReferenceID,
                    MotherQIDAttach = dat.MotherQIDAttach,
                    StudentQIDReferenceID = dat.StudentQIDReferenceID,
                    StudentQIDAttach = dat.StudentQIDAttach,
                });
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentDTO>(jsonString);
        }

    }
}
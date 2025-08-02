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
using System.Globalization;
using Eduegate.Web.Library.CRM.Leads;
using Eduegate.Web.Library.Common;
using System.Linq;
using Eduegate.Domain;

namespace Eduegate.Web.Library.School.Students
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "StudentApplication", "CRUDModel.ViewModel")]
    [DisplayName("Student Application")]
    public class StudentApplicationViewModel : BaseMasterViewModel
    {
        public StudentApplicationViewModel()
        {
            IndianNationalityID = int.Parse(new Domain.Setting.SettingBL(null).GetSettingValue<string>("NATIONALITY_ID_INDIAN"));
            //Class = new KeyValueViewModel();
            Siblings = new List<KeyValueViewModel>();
            IsMinority = false;
            IsOnlyChildofParent = false;
            //Prospectus = new ProspectusViewModel();
            //Communication = new CommunicationViewModel();
            GuardianDetails = new GuardianDetailsViewModel();
            FatherMotherDetails = new FatherMotherDetailsViewModel();
            DocumentsUpload = new List<StudentApplicationDocUploadViewModel>() { new StudentApplicationDocUploadViewModel() };
            PreviousSchoolDetails = new PreviousSchoolDetailsViewModel();
            Address = new StudentApplicationAddressViewModel();
        }

        [ControlType(Framework.Enums.ControlTypes.DataPicker)]
        [CustomDisplay("From Leads")]
        [DataPicker("LeadAdvancedSearchView")]
        public string ReferenceLeadNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Prospectus No.")]
        public string ProspectusNumber { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        //[DisplayName("")]
        //public string NewLine0 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Application No.")]
        public string ApplicationNumber { get; set; }
        public long ApplicationIID { get; set; }

        public long? SchoolID { get; set; }

        public string AdmissionNumber { get; set; }

        public string StudentID { get; set; }

        public long? LoginID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine6 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='SchoolChanges($event, $element,CRUDModel.ViewModel)'")]
        [CustomDisplay("School")]
        [LookUp("LookUps.School")]
        public string School { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Academic Year")]
        [LookUp("LookUps.AcademicYear")]

        public string SchoolAcademicyear { get; set; }
        public int? AcademicyearID { get; set; }

        public string Academicyear { get; set; }

        public byte? SchoolIID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine7 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Class", "Numeric", false, "ClassChangesforStream($event, $element, CRUDModel.ViewModel)")]
        [LookUp("LookUps.Classes")]
        [CustomDisplay("Class")]
        public KeyValueViewModel StudentClass { get; set; }
        public string ClassKey { get; set; }
        public int? ClassID { get; set; }

        public string Optional { get; set; }
        public long? OptionalID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Curriculam")]
        [LookUp("LookUps.SchoolSyllabus")]

        public string CurriculamString { get; set; }
        public byte? CurriculamID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        public bool onStreams { get; set; }
        public string StreamKey { get; set; }
        public byte? StreamID { get; set; }
        public int? StreamGroupID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("StreamGroup", "Numeric", false, "StreamGroupChanges($event, $element, CRUDModel.ViewModel)", false, OptionalAttribute1 = "ng-disabled=!CRUDModel.ViewModel.onStreams")]
        [LookUp("LookUps.StreamGroups")]
        [CustomDisplay("StreamGroup")]
        public KeyValueViewModel StreamGroup { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Stream", "Numeric", false, "StreamChanges($event, $element, CRUDModel.ViewModel)", false, OptionalAttribute1 = "ng-disabled=!CRUDModel.ViewModel.onStreams")]
        [LookUp("LookUps.StreamsForApplication")]
        [CustomDisplay("Stream")]
        public KeyValueViewModel Stream { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("OptionalSubjects", "Numeric", true, "", optionalAttribute1: "ng-disabled=!CRUDModel.ViewModel.onStreams")]
        [LookUp("LookUps.OptionalSubjects")]
        [CustomDisplay("Optional Subjects")]
        public List<KeyValueViewModel> OptionalSubjects { get; set; }
        public int? OptionalSubjectID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("First Name")]
        [StringLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [RegularExpression(@"^[a-zA-Z_ ]*$", ErrorMessage = "Use alphabets only")]//^[a-zA-Z_ ]*$
        public string FirstName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Middle Name")]
        [StringLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [RegularExpression(@"^[a-zA-Z_ ]*$", ErrorMessage = "Use alphabets only")]
        public string MiddleName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Last Name")]
        [StringLength(20, ErrorMessage = "Maximum Length should be within 20!")]

        [RegularExpression(@"^[a-zA-Z_ ]*$", ErrorMessage = "Use alphabets only")]
        public string LastName { get; set; }


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

        //[ControlType(Framework.Enums.ControlTypes.HtmlLabel, Attributes2 = "htmllabelinfo")]
        //[CustomDisplay("AgeCriteriaWarningMsg")]
        //public string AgeCriteriaWarningMsg { get; set; }

        public System.DateTime? DateOfBirth { get; set; }
        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine8 { get; set; }



        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[DisplayName("Category")]
        //[LookUp("LookUps.Category")]
        //public string Category { get; set; }

        //public int? CategoryID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='RelegionChanges($event, $element,CRUDModel.ViewModel)'")]
        [CustomDisplay("Religion")]
        [LookUp("LookUps.Relegion")]
        public string Relegion { get; set; }
        public byte? RelegionID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Caste")]
        [LookUp("LookUps.Cast")]
        public string Cast { get; set; }
        public byte? CastID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Community")]
        [LookUp("LookUps.Community")]
        public string Community { get; set; }
        public byte? CommunityID { get; set; }

        public long? StudentPassportDetailNoID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine9 { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Nationality")]
        [LookUp("LookUps.Nationality")]
        public string Nationality { get; set; }
        public int? NationalityID { get; set; }
        public KeyValueViewModel StudentNationality { get; set; }

        public int? IndianNationalityID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled = CRUDModel.ViewModel.Nationality!=CRUDModel.ViewModel.IndianNationalityID")]
        [CustomDisplay("Adhaar No")]
        [MaxLength(12, ErrorMessage = "Max 12 characters"), MinLength(12, ErrorMessage = "Min 12 characters")]
        [RegularExpression(@"^[0-9*#+]+$", ErrorMessage = "Use  digits only")]
        public string AdhaarCardNo { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Country Of Birth")]
        [LookUp("LookUps.Country")]
        public string StudentCoutryOfBrith { get; set; }
        public int? StudentCoutryOfBrithID { get; set; }
        public KeyValueViewModel CoutryOfBrith { get; set; }

        [Required]
        [MaxLength(12, ErrorMessage = "Maximum Length should be within 12!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Passport No.")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Use alphanumeric only")]
        public string StudentPassportNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Passport Issue Country")]
        [LookUp("LookUps.Country")]
        public string StudentCountryofIssue { get; set; }
        public int? CountryofIssueID { get; set; }
        public KeyValueViewModel CountryofIssue { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Passport Issue Date")]
        public string PassportNoIssueString { get; set; }
        public DateTime? PassportNoIssueDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Passport Expiry Date")]
        public string PassportNoExpiryString { get; set; }
        public DateTime? PassportNoExpiryDate { get; set; }

        public long? StudentVisaDetailNoID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(11, ErrorMessage = "Maximum Length should be within 11!")]
        [CustomDisplay("Visa No")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Use alphanumeric only")]
        public string StudentVisaNo { get; set; }


        //[ControlType(Framework.Enums.ControlTypes.DatePicker)]
        //[DisplayName("Visa Issue Date")]
        //public string VisaIssueDateString { get; set; }
        //public DateTime? VisaIssueDate { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("VisaExpiryDate")]
        public string VisaExpiryDateString { get; set; }
        public DateTime? VisaExpiryDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("NationalIDNo")]
        [MaxLength(11, ErrorMessage = "Max 11 characters"), MinLength(11, ErrorMessage = "Min 11 characters")]
        [RegularExpression(@"^[0-9*#+]+$", ErrorMessage = "Use  digits only")]
        public string StudentNationalID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("NationalIDIssueDate")]
        public string StudentNationalIDNoIssueDateString { get; set; }
        public DateTime? StudentNationalIDNoIssueDate { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("NationalIDExpiryDate")]
        public string StudentNationalIDNoExpiryDateString { get; set; }
        public DateTime? StudentNationalIDNoExpiryDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.FileUpload)]
        [CustomDisplay("StudentProfile")]
        [FileUploadInfo("Content/UploadContents", Framework.Enums.EduegateImageTypes.UserProfile, "ProfileUrl", "")]
        public string ProfileUrl { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("BloodGroup")]
        [LookUp("LookUps.BloodGroup")]
        public string BloodGroup { get; set; }
        public int? BloodGroupID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsMinority")]
        public bool IsMinority { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsOnlyChildofParent")]
        public bool IsOnlyChildofParent { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("PrimaryContact")]
        [LookUp("LookUps.GuardianType")]
        public string PrimaryContact { get; set; }
        public byte? PrimaryContactID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Students", "Numeric", true, "")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Student", "LookUps.Students")]
        [CustomDisplay("Siblings")]
        public List<KeyValueViewModel> Siblings { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine10 { get; set; }

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

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Parmenent Address")]
        //public string ParmenentAddress { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Current Address")]
        //public string CurrentAddress { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Status")]
        [LookUp("LookUps.ApplicationStatus")]
        public string ApplicationStatus { get; set; }

        public byte? ApplicationStatusID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[DisplayName("Application Type")]
        //[LookUp("LookUps.ApplicationSubmitType")]
        //public string ApplicationType { get; set; }

        //public int? ApplicationTypeID { get; set; }


        //For purpose of Studentpicking fill Parent login details

        public long? ParentID { get; set; }
        public string ParentCode { get; set; }
        public string ParentLoginUserID { get; set; }

        public string ParentLoginEmailID { get; set; }

        public string ParentLoginPassword { get; set; }

        public string ParentLoginPasswordSalt { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "FatherMotherDetails", "FatherMotherDetails")]
        [CustomDisplay("FatherMotherDetails")]
        public FatherMotherDetailsViewModel FatherMotherDetails { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "Address", "Address")]
        [CustomDisplay("Address")]
        public StudentApplicationAddressViewModel Address { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "GuardianDetails", "GuardianDetails")]
        [CustomDisplay("GuardianDetails")]
        public GuardianDetailsViewModel GuardianDetails { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "PreviousSchoolDetails", "PreviousSchoolDetails")]
        [CustomDisplay("PreviousSchoolDetails")]
        public PreviousSchoolDetailsViewModel PreviousSchoolDetails { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("UploadDocuments")]
        public List<StudentApplicationDocUploadViewModel> DocumentsUpload { get; set; }

        #region DropDowns Saving for ParentPortalApplication
        //Father Screen 
        public string FatherCountry { get; set; }
        public string FatherCountryofIssue { get; set; }
        public string CanYouVolunteerToHelpOneString { get; set; }
        //Mother Screen 
        public string MotherCountry { get; set; }
        public string MotherCountryofIssue { get; set; }
        public string CanYouVolunteerToHelpTwoString { get; set; }

        //GuardianScreen Screen 
        public string GuardianNationality { get; set; }
        public string GuardianCountryofIssue { get; set; }
        public string GuardianStudentRelationShip { get; set; }

        //LocationScreen Screen 
        public string Country { get; set; }
        //PreviousSchoolScreen 
        public string PreviousSchoolClassClassKey { get; set; }
        public string PreviousSchoolSyllabus { get; set; }
        #endregion

        #region DocumentUpload reset data pass
        public string ResetBirthCertificate { get; set; }
        public string ResetStudentPassportReference { get; set; }
        public string ResetTC { get; set; }
        public string ResetFatherQID { get; set; }
        public string ResetMotherQID { get; set; }
        public string ResetStudentQID { get; set; }

        #endregion

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as StudentApplicationDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentApplicationViewModel>(jsonString);
        }

        public StudentApplicationViewModel ToVM(StudentApplicationDTO dto)
        {
            Mapper<StudentDTO, StudentViewModel>.CreateMap();
            Mapper<StudentApplicationDTO, StudentApplicationViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            //Mapper<ProspectusDTO, ProspectusViewModel>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var applicationDto = dto as StudentApplicationDTO;
            var vm = Mapper<StudentApplicationDTO, StudentApplicationViewModel>.Map(applicationDto);
            //vm.ApplicationIID = Convert.ToInt64(applicationDto.ApplicationIID);
            vm.ApplicationNumber = applicationDto.ApplicationNumber;
            vm.Gender = applicationDto.GenderID.ToString();
            vm.School = applicationDto.SchoolID.ToString();
            vm.SchoolID = applicationDto.SchoolID;
            vm.Relegion = applicationDto.RelegionID.ToString();
            vm.Stream = applicationDto.StreamID.HasValue ? new KeyValueViewModel() { Key = applicationDto.StreamID.ToString(), Value = applicationDto.Stream.Value } : null;
            vm.StreamID = applicationDto.StreamID;
            vm.StreamGroupID = applicationDto.StreamGroupID;
            vm.StreamGroup = applicationDto.StreamGroupID.HasValue ? new KeyValueViewModel() { Key = applicationDto.StreamGroupID.ToString(), Value = applicationDto.StreamGroup.Value } : null;
            vm.SchoolAcademicyear = applicationDto.SchoolAcademicyear != null ? applicationDto.SchoolAcademicyear : applicationDto.SchoolAcademicyearID.HasValue ? applicationDto.SchoolAcademicyearID.ToString() : null;
            vm.Academicyear = applicationDto.Academicyear;
            vm.AcademicyearID = applicationDto.SchoolAcademicyearID == null ? (int?)null : applicationDto.SchoolAcademicyearID;
            vm.DateOfBirthString = applicationDto.DateOfBirth.HasValue ? applicationDto.DateOfBirth.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            //vm.Category = applicationDto.CategoryID.ToString();
            //vm.CategoryID = applicationDto.CategoryID;
            vm.StudentClass = applicationDto.ClassID.HasValue ? new KeyValueViewModel() { Key = applicationDto.ClassID.ToString(), Value = applicationDto.Class.Value } : null;
            //vm.ClassKey = applicationDto.ClassID.HasValue ? new KeyValueViewModel() { Key = applicationDto.ClassID.ToString(), Value = applicationDto.Class.Value } : null;
            if (applicationDto.ClassID.HasValue)
            {
                vm.ClassKey = applicationDto.ClassID.Value.ToString();
            }
            if (applicationDto.StreamGroupID.HasValue)
            {
                vm.ClassKey = applicationDto.StreamGroupID.Value.ToString();
            }
            vm.ProfileUrl = applicationDto.ProfileContentID.HasValue ? applicationDto.ProfileContentID.ToString() : null;
            vm.Cast = applicationDto.CastID.ToString();
            //vm.CastID = string.IsNullOrEmpty(applicationDto.Cast) ? (byte?)null : byte.Parse(applicationDto.Cast);
            vm.SecoundLanguageString = applicationDto.SecoundLanguageID.ToString();
            vm.SecoundLanguageID = string.IsNullOrEmpty(applicationDto.SecoundLanguage) ? (int?)null : int.Parse(applicationDto.SecoundLanguage);
            vm.ThridLanguageString = applicationDto.ThridLanguageID.ToString();
            vm.ThridLanguageID = string.IsNullOrEmpty(applicationDto.ThridLanguage) ? (int?)null : int.Parse(applicationDto.ThridLanguage);
            vm.Community = applicationDto.CommunityID.ToString();
            vm.CommunityID = string.IsNullOrEmpty(applicationDto.Community) ? (byte?)null : byte.Parse(applicationDto.Community);
            vm.BloodGroup = applicationDto.BloodGroupID.ToString();
            vm.BloodGroupID = string.IsNullOrEmpty(applicationDto.BloodGroup) ? (byte?)null : byte.Parse(applicationDto.BloodGroup);
            vm.RelegionID = applicationDto.RelegionID == null ? (byte?)null : applicationDto.RelegionID;
            vm.Nationality = applicationDto.NationalityID.ToString();
            vm.Remarks = applicationDto.Remarks;
            //vm.NationalityID = string.IsNullOrEmpty(applicationDto.Nationality) ? (int?)null : int.Parse(applicationDto.Nationality);
            vm.ApplicationStatus = applicationDto.ApplicationStatusID.HasValue ? applicationDto.ApplicationStatusID.ToString() : "1";
            //vm.ApplicationType = applicationDto.ApplicationTypeID.HasValue ? applicationDto.ApplicationTypeID.ToString() : "1";
            vm.LoginID = applicationDto.LoginID.HasValue ? applicationDto.LoginID : (long?)null;
            vm.StudentCoutryOfBrithID = string.IsNullOrEmpty(applicationDto.StudentCoutryOfBrith) ? (int?)null : int.Parse(applicationDto.StudentCoutryOfBrith);
            //vm.StudentPassportNo = applicationDto.StudentPassportDetails.PassportNo;
            vm.StudentNationalID = applicationDto.StudentNationalID;
            vm.NationalityID = string.IsNullOrEmpty(applicationDto.Nationality) ? (int?)null : int.Parse(applicationDto.Nationality);
            //vm.PreviousSchoolSyllabusID = string.IsNullOrEmpty(applicationDto.PreviousSchoolSyllabus) ? (byte?)null : byte.Parse(applicationDto.PreviousSchoolSyllabus);
            vm.CurriculamID = applicationDto.CurriculamID;
            vm.CurriculamString = applicationDto.Curriculam != null ? applicationDto.Curriculam : applicationDto.CurriculamID.HasValue ? applicationDto.CurriculamID.ToString() : null;
            //vm.SyllabusID = applicationDto.CurriculamID;
            //vm.PreviousSchoolAcademicYear = applicationDto.PreviousSchoolAcademicYearID.ToString();
            //vm.FatherStudentRelationShipID = string.IsNullOrEmpty(applicationDto.FatherStudentRelationShip) ? (byte?)null : byte.Parse(applicationDto.FatherStudentRelationShip);
            //vm.MotherStudentRelationShip = applicationDto.MotherStudentRelationShipID.ToString();
            //vm.MotherStudentRelationShipID = string.IsNullOrEmpty(applicationDto.MotherStudentRelationShip) ? (byte?)null : byte.Parse(applicationDto.MotherStudentRelationShip);
            //vm.MotherStudentRelationShipID = applicationDto.MotherStudentRelationShipID == null ? (byte?)null : applicationDto.MotherStudentRelationShipID;
            vm.PrimaryContactID = applicationDto.PrimaryContactID == null ? (byte?)null : applicationDto.PrimaryContactID;
            //vm.SchoolAcademicyear = applicationDto.SchoolAcademicyearID.ToString();
            if (applicationDto.StudentSiblings.Count > 0)
            {
                foreach (KeyValueDTO kvm in applicationDto.StudentSiblings)
                {
                    vm.Siblings.Add(new KeyValueViewModel()
                    { Key = kvm.Key, Value = kvm.Value });
                }
            }
            //vm.CountryID = string.IsNullOrEmpty(applicationDto.Country) ? (int?)null : int.Parse(applicationDto.Country);
            vm.ProspectusNumber = applicationDto.ProspectNumber != null ? applicationDto.ProspectNumber : null;
            vm.StudentPassportNo = applicationDto.StudentPassportDetails.PassportNo;
            vm.StudentCountryofIssue = applicationDto.StudentPassportDetails.CountryofIssueID.ToString();
            //vm.CountryofIssue = applicationDto.StudentPassportDetails.CountryofIssue.ToString();
            vm.CountryofIssueID = string.IsNullOrEmpty(applicationDto.StudentPassportDetails.StudentCountryofIssue) ? (int?)null : int.Parse(applicationDto.StudentPassportDetails.StudentCountryofIssue);
            vm.CountryofIssue = applicationDto.StudentPassportDetails.CountryofIssueID.HasValue ? new KeyValueViewModel() { Key = applicationDto.StudentPassportDetails.CountryofIssueID.ToString(), Value = applicationDto.StudentPassportDetails.CountryofIssue.Value } : null;
            vm.StudentPassportDetailNoID = applicationDto.StudentPassportDetails.PassportDetailsIID;
            vm.PassportNoIssueString = applicationDto.StudentPassportDetails.PassportNoIssueDate.HasValue ? applicationDto.StudentPassportDetails.PassportNoIssueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.PassportNoExpiryString = applicationDto.StudentPassportDetails.PassportNoExpiryDate.HasValue ? applicationDto.StudentPassportDetails.PassportNoExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            
            vm.StudentVisaDetailNoID = applicationDto.StudentVisaDetails.VisaDetailsIID;
            vm.StudentVisaNo = applicationDto.StudentVisaDetails.VisaNo;
            //vm.VisaIssueDateString = applicationDto.StudentVisaDetails.VisaIssueDate.HasValue ? applicationDto.StudentVisaDetails.VisaIssueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.VisaExpiryDateString = applicationDto.StudentVisaDetails.VisaExpiryDate.HasValue ? applicationDto.StudentVisaDetails.VisaExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;

            //vm.FatherVisaDetailNoID = applicationDto.FatherVisaDetails.VisaDetailsIID;
            //vm.FatherVisaNo = applicationDto.FatherVisaDetails.VisaNo;
            //vm.FatherVisaIssueDateString = applicationDto.FatherVisaDetails.VisaIssueDate.HasValue ? applicationDto.FatherVisaDetails.VisaIssueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            //vm.FatherVisaExpiryDateString = applicationDto.FatherVisaDetails.VisaExpiryDate.HasValue ? applicationDto.FatherVisaDetails.VisaExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;

            //vm.MotherVisaDetailNoID = applicationDto.MotherVisaDetails.VisaDetailsIID;
            //vm.MotherVisaNo = applicationDto.MotherVisaDetails.VisaNo;
            //vm.MotherVisaIssueDateString = applicationDto.MotherVisaDetails.VisaIssueDate.HasValue ? applicationDto.MotherVisaDetails.VisaIssueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            //vm.MotherVisaExpiryDateString = applicationDto.MotherVisaDetails.VisaExpiryDate.HasValue ? applicationDto.MotherVisaDetails.VisaExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.AdhaarCardNo = applicationDto.AdhaarCardNo;
            if (applicationDto.IsMinority != null)
            {
                vm.IsMinority = applicationDto.IsMinority.Value;
            }
            if (applicationDto.IsOnlyChildofParent != null)
            {
                vm.IsOnlyChildofParent = applicationDto.IsOnlyChildofParent.Value;
            }

            vm.StudentNationalIDNoIssueDateString = applicationDto.StudentNationalIDNoIssueDate.HasValue ? applicationDto.StudentNationalIDNoIssueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.StudentNationalIDNoExpiryDateString = applicationDto.StudentNationalIDNoExpiryDate.HasValue ? applicationDto.StudentNationalIDNoExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;

            //Guardian Details:
            vm.GuardianDetails = new GuardianDetailsViewModel()
            {
                GuardianVisaDetailNoID = applicationDto.GuardianVisaDetailNoID,
                GuardianPassportDetailNoID = applicationDto.GuardianPassportDetailNoID,
                GuardianFirstName = applicationDto.GuardianFirstName,
                GuardianMiddleName = applicationDto.GuardianMiddleName,
                GuardianLastName = applicationDto.GuardianLastName,
                GuardianStudentRelationShipID = applicationDto.GuardianStudentRelationShipID,
                GuardianStudentRelationShip = applicationDto.GuardianStudentRelationShipID.ToString(),
                GuardianOccupation = applicationDto.GuardianOccupation,
                GuardianCompanyName = applicationDto.GuardianCompanyName,
                GuardianMobileNumber = applicationDto.GuardianMobileNumber,
                GuardianWhatsappMobileNo = applicationDto.GuardianWhatsappMobileNo,
                GuardianEmailID = applicationDto.GuardianEmailID,
                GuardianNationalityID = applicationDto.GuardianNationalityID,
                GuardianNationality = applicationDto.GuardianNationalityID.ToString(),
                GuardianNationalID = applicationDto.GuardianNationalID,
                GuardianNationalIDNoIssueDateString = applicationDto.GuardianNationalIDNoIssueDate.HasValue ? applicationDto.GuardianNationalIDNoIssueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                GuardianNationalIDNoExpiryDateString = applicationDto.GuardianNationalIDNoExpiryDate.HasValue ? applicationDto.GuardianNationalIDNoExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                GuardianPassportNumber = applicationDto.GuardianPassportDetails.GuardianPassportNumber,
                CountryofIssueID = applicationDto.GuardianPassportDetails.CountryofIssueID,
                GuardianCountryofIssue = applicationDto.GuardianPassportDetails.CountryofIssueID.ToString(),
                GuardianPassportNoIssueString = applicationDto.GuardianPassportDetails.PassportNoIssueDate.HasValue ? applicationDto.GuardianPassportDetails.PassportNoIssueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                GuardianPassportNoExpiryString = applicationDto.GuardianPassportDetails.PassportNoExpiryDate.HasValue ? applicationDto.GuardianPassportDetails.PassportNoExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
            };
            //Address
            vm.Address = new StudentApplicationAddressViewModel()
            {
                BuildingNo = applicationDto.BuildingNo,
                FlatNo = applicationDto.FlatNo,
                StreetNo = applicationDto.StreetNo,
                StreetName = applicationDto.StreetName,
                LocationNo = applicationDto.LocationNo,
                LocationName = applicationDto.LocationName,
                ZipNo = applicationDto.ZipNo,
                PostBoxNo = applicationDto.PostBoxNo,
                City = applicationDto.City,
                Country = applicationDto.CountryID.HasValue ? applicationDto.CountryID.ToString() : null,
                CountryID = applicationDto.CountryID == null ? (int?)null : applicationDto.CountryID,

            };

            //Previous schoolDetails
            vm.PreviousSchoolDetails = new PreviousSchoolDetailsViewModel()
            {
                IsStudentStudiedBeforeForPortal = (bool)(applicationDto.IsStudentStudiedBefore.HasValue ? applicationDto.IsStudentStudiedBefore : false),
                PreviousSchoolAcademicYear = applicationDto.PreviousSchoolAcademicYear,
                SyllabusID = applicationDto.PreviousSchoolSyllabusID,
                PreviousSchoolSyllabus = applicationDto.PreviousSchoolSyllabusID.ToString(),
                PreviousSchoolName = applicationDto.PreviousSchoolName,
                PreviousSchoolSyllabusID = applicationDto.PreviousSchoolSyllabusID,
                PreviousSchoolAddress = applicationDto.PreviousSchoolAddress,
                PreviousSchoolClass = applicationDto.PreviousSchoolClassCompletedID.HasValue ? new KeyValueViewModel() { Key = applicationDto.PreviousSchoolClassCompletedID.ToString(), Value = applicationDto.PreviousSchoolClassCompleted.Value } : null,
                PreviousSchoolClassCompletedID = applicationDto.PreviousSchoolClassCompletedID,
                PreviousSchoolClassClassKey = applicationDto.PreviousSchoolClassCompletedID.HasValue ? applicationDto.PreviousSchoolClassCompletedID.Value.ToString() : null,

            };

            //Father,Mother Details----
            vm.FatherMotherDetails = new FatherMotherDetailsViewModel()
            {
                FatherFirstName = applicationDto.FatherFirstName,
                FatherMiddleName = applicationDto.FatherMiddleName,
                FatherLastName = applicationDto.FatherLastName,
                FatherOccupation = applicationDto.FatherOccupation,
                FatherCountryID = applicationDto.FatherCountryID,
                MotherCountryID = applicationDto.MotherCountryID,
                MobileNumber = applicationDto.MobileNumber,
                FatherWhatsappMobileNo = applicationDto.FatherWhatsappMobileNo,
                EmailID = applicationDto.EmailID,
                MotherFirstName = applicationDto.MotherFirstName,
                MotherMiddleName = applicationDto.MotherMiddleName,
                MotherLastName = applicationDto.MotherLastName,
                FatherNationalID = applicationDto.FatherNationalID,
                CanYouVolunteerToHelpOneID = applicationDto.CanYouVolunteerToHelpOneID.HasValue ? applicationDto.CanYouVolunteerToHelpOneID : null,
                CanYouVolunteerToHelpOneString = applicationDto.CanYouVolunteerToHelpOneID.HasValue ? applicationDto.CanYouVolunteerToHelpOneID.ToString() : null,
                CanYouVolunteerToHelpTwoID = applicationDto.CanYouVolunteerToHelpTwoID.HasValue ? applicationDto.CanYouVolunteerToHelpTwoID : null,
                CanYouVolunteerToHelpTwoString = applicationDto.CanYouVolunteerToHelpTwoID.HasValue ? applicationDto.CanYouVolunteerToHelpTwoID.ToString() : null,
                FatherCountry = applicationDto.FatherCountryID.ToString(),
                MotherCountry = applicationDto.MotherCountryID.ToString(),
                MotherMobileNumber = applicationDto.MotherMobileNumber,
                MotherWhatsappMobileNo = applicationDto.MotherWhatsappMobileNo,
                MotherOccupation = applicationDto.MotherOccupation,
                MotherEmailID = applicationDto.MotherEmailID,
                MotherNationalID = applicationDto.MotherNationalID,
                FatherStudentRelationShip = applicationDto.FatherStudentRelationShipID.ToString(),
                FatherStudentRelationShipID = applicationDto.FatherStudentRelationShipID == null ? (byte?)null : applicationDto.FatherStudentRelationShipID,
                FatherCompanyName = applicationDto.FatherCompanyName,
                MotherCompanyName = applicationDto.MotherCompanyName,
                FatherPassportDetailNoID = applicationDto.FatherPassportDetails.PassportDetailsIID,
                FatherPassportNumber = applicationDto.FatherPassportDetails.PassportNo,
                FatherCountryofIssueID = applicationDto.FatherPassportDetails.FatherCountryofIssueID == null ? (int?)null : applicationDto.FatherPassportDetails.FatherCountryofIssueID,
                FatherCountryofIssue = applicationDto.FatherPassportDetails.FatherCountryofIssueID.ToString(),
                FatherPassportNoIssueString = applicationDto.FatherPassportDetails.PassportNoIssueDate.HasValue ? applicationDto.FatherPassportDetails.PassportNoIssueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                FatherPassportNoExpiryString = applicationDto.FatherPassportDetails.PassportNoExpiryDate.HasValue ? applicationDto.FatherPassportDetails.PassportNoExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,

                MotherPassportDetailNoID = applicationDto.MotherPassportDetails.PassportDetailsIID,
                MotherPassportNumber = applicationDto.MotherPassportDetails.PassportNo,
                MotherCountryofIssueID = applicationDto.MotherPassportDetails.MotherCountryofIssueID == null ? (int?)null : applicationDto.MotherPassportDetails.MotherCountryofIssueID,
                MotherCountryofIssue = applicationDto.MotherPassportDetails.MotherCountryofIssueID.ToString(),
                MotherPassportNoIssueString = applicationDto.MotherPassportDetails.PassportNoIssueDate.HasValue ? applicationDto.MotherPassportDetails.PassportNoIssueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                MotherPassportNoExpiryString = applicationDto.MotherPassportDetails.PassportNoExpiryDate.HasValue ? applicationDto.MotherPassportDetails.PassportNoExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                FatherMobileNumberTwo = applicationDto.FatherMobileNumberTwo,
                FatherNationalDNoIssueDateString = applicationDto.FatherNationalDNoIssueDate.HasValue ? applicationDto.FatherNationalDNoIssueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                FatherNationalDNoExpiryDateString = applicationDto.FatherNationalDNoExpiryDate.HasValue ? applicationDto.FatherNationalDNoExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                MotherNationalDNoIssueDateString = applicationDto.MotherNationalDNoIssueDate.HasValue ? applicationDto.MotherNationalDNoIssueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                MotherNationaIDNoExpiryDateString = applicationDto.MotherNationaIDNoExpiryDate.HasValue ? applicationDto.MotherNationaIDNoExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
            };

            vm.DocumentsUpload = new List<StudentApplicationDocUploadViewModel>();

            vm.DocumentsUpload.Add(new StudentApplicationDocUploadViewModel()
            {
                ApplicationDocumentIID = applicationDto.StudentDocUploads.ApplicationDocumentIID,
                ApplicationID = applicationDto.StudentDocUploads.ApplicationID,
                BirthCertificateReferenceID = Convert.ToString(applicationDto.StudentDocUploads.BirthCertificateReferenceID),
                BirthCertificateAttach = applicationDto.StudentDocUploads.BirthCertificateAttach,
                StudentPassportReferenceID = Convert.ToString(applicationDto.StudentDocUploads.StudentPassportReferenceID),
                StudentPassportAttach = applicationDto.StudentDocUploads.StudentPassportAttach,
                TCReferenceID = Convert.ToString(applicationDto.StudentDocUploads.TCReferenceID),
                TCAttach = applicationDto.StudentDocUploads.TCAttach,
                FatherQIDReferenceID = Convert.ToString(applicationDto.StudentDocUploads.FatherQIDReferenceID),
                FatherQIDAttach = applicationDto.StudentDocUploads.FatherQIDAttach,
                MotherQIDReferenceID = Convert.ToString(applicationDto.StudentDocUploads.MotherQIDReferenceID),
                MotherQIDAttach = applicationDto.StudentDocUploads.MotherQIDAttach,
                StudentQIDReferenceID = Convert.ToString(applicationDto.StudentDocUploads.StudentQIDReferenceID),
                StudentQIDAttach = applicationDto.StudentDocUploads.StudentQIDAttach,
            });

            return vm;
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<StudentApplicationDTO, StudentApplicationViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            Mapper<ProspectusDTO, ProspectusViewModel>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var applicationDto = dto as StudentApplicationDTO;
            var vm = Mapper<StudentApplicationDTO, StudentApplicationViewModel>.Map(applicationDto);
            //vm.ApplicationIID = applicationDto.ApplicationIID;
            vm.ApplicationNumber = applicationDto.ApplicationNumber;
            vm.Gender = applicationDto.GenderID.ToString();
            vm.School = applicationDto.SchoolID.ToString();
            vm.SchoolID = applicationDto.SchoolID;
            vm.DateOfBirthString = applicationDto.DateOfBirth.HasValue ? applicationDto.DateOfBirth.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            //vm.Category = applicationDto.CategoryID.ToString();
            //vm.CategoryID = applicationDto.CategoryID;
            //vm.AcademicyearID = applicationDto.SchoolAcademicyearID;
            vm.CastID = applicationDto.CastID;
            vm.Cast = applicationDto.CastID.ToString();
            vm.Stream = applicationDto.StreamID.HasValue ? new KeyValueViewModel() { Key = applicationDto.StreamID.ToString(), Value = applicationDto.Stream.Value } : null;
            vm.StreamGroup = applicationDto.StreamGroupID.HasValue ? new KeyValueViewModel() { Key = applicationDto.StreamGroupID.ToString(), Value = applicationDto.StreamGroup.Value } : null;
            vm.SecoundLanguageString = applicationDto.SecoundLanguageID.ToString();
            vm.SecoundLanguageID = applicationDto.SecoundLanguageID;
            vm.ThridLanguageString = applicationDto.ThridLanguageID.ToString();
            vm.ThridLanguageID = applicationDto.ThridLanguageID;
            vm.Relegion = applicationDto.RelegionID.ToString();
            vm.RelegionID = applicationDto.RelegionID;
            vm.Community = applicationDto.CommunityID.ToString();
            vm.CommunityID = applicationDto.CommunityID;
            //vm.SyllabusID = applicationDto.CurriculamID;
            vm.NationalityID = applicationDto.NationalityID;
            vm.Nationality = applicationDto.Nationality;
            vm.StudentCoutryOfBrithID = applicationDto.StudentCoutryOfBrithID;
            vm.StudentCoutryOfBrith = applicationDto.StudentCoutryOfBrith;
            vm.ApplicationStatus = applicationDto.ApplicationStatusID.HasValue ? applicationDto.ApplicationStatusID.ToString() : "1";
            //vm.ApplicationType = applicationDto.ApplicationTypeID.HasValue ? applicationDto.ApplicationTypeID.ToString() : "1";
            vm.StudentClass = applicationDto.ClassID.HasValue ? new KeyValueViewModel() { Key = applicationDto.ClassID.ToString(), Value = applicationDto.Class.Value } : null;
            vm.ClassKey = null;
            vm.LoginID = applicationDto.LoginID.HasValue ? applicationDto.LoginID : (long?)null;
            vm.StudentPassportNo = applicationDto.StudentPassportNo;
            vm.StudentNationalID = applicationDto.StudentNationalID;
            //vm.CurriculamID = applicationDto.CurriculamID;
            vm.CurriculamString = applicationDto.CurriculamID.HasValue ? applicationDto.CurriculamID.ToString() : null;
            //vm.PreviousSchoolAcademicYear = applicationDto.PreviousSchoolAcademicYearID.ToString();
            vm.ProspectusNumber = applicationDto.ProspectNumber != null ? applicationDto.ProspectNumber : null;
            vm.ProfileUrl = applicationDto.ProfileContentID.HasValue ? applicationDto.ProfileContentID.ToString() : null;

            //if(applicationDto.MotherStudentRelationShipID.HasValue)
            //{ 
            //  vm.MotherStudentRelationShipID = applicationDto.MotherStudentRelationShipID;
            // vm.MotherStudentRelationShip = applicationDto.MotherStudentRelationShipID.ToString();
            //}

            if (applicationDto.PrimaryContactID.HasValue)
            { 
                vm.PrimaryContactID = applicationDto.PrimaryContactID;
                vm.PrimaryContact = applicationDto.PrimaryContactID.ToString();
            }
            
                vm.SchoolAcademicyear = applicationDto.SchoolAcademicyearID.ToString();
            
            if (applicationDto.StudentSiblings.Count > 0)
            {
                foreach (KeyValueDTO kvm in applicationDto.StudentSiblings)
                {
                    vm.Siblings.Add(new KeyValueViewModel()
                    { Key = kvm.Key, Value = kvm.Value });
                }
            }

            vm.OptionalSubjects = new List<KeyValueViewModel>();
            if (applicationDto.OptionalSubjects.Count > 0)
            {
                foreach (KeyValueDTO opSubjt in applicationDto.OptionalSubjects)
                {
                    vm.OptionalSubjects.Add(new KeyValueViewModel()
                    { Key = opSubjt.Key, Value = opSubjt.Value });
                }
            }

            //vm.CountryID = applicationDto.CountryID;
            vm.StudentPassportNo = applicationDto.StudentPassportDetails.PassportNo;
            vm.StudentCountryofIssue = applicationDto.StudentPassportDetails.StudentCountryofIssue;
            vm.CountryofIssueID = applicationDto.StudentPassportDetails.CountryofIssueID;
            vm.CountryofIssue = applicationDto.StudentPassportDetails.CountryofIssueID.HasValue ? new KeyValueViewModel() { Key = applicationDto.StudentPassportDetails.CountryofIssueID.ToString(), Value = applicationDto.StudentPassportDetails.CountryofIssue.Value } : null;
            vm.StudentPassportDetailNoID = applicationDto.StudentPassportDetails.PassportDetailsIID;
            vm.PassportNoIssueString = applicationDto.StudentPassportDetails.PassportNoIssueDate.HasValue ? applicationDto.StudentPassportDetails.PassportNoIssueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.PassportNoExpiryString = applicationDto.StudentPassportDetails.PassportNoExpiryDate.HasValue ? applicationDto.StudentPassportDetails.PassportNoExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            
            vm.StudentVisaDetailNoID = applicationDto.StudentVisaDetails.VisaDetailsIID;
            vm.StudentVisaNo = applicationDto.StudentVisaDetails.VisaNo;
            //vm.VisaIssueDateString = applicationDto.StudentVisaDetails.VisaIssueDate.HasValue ? applicationDto.StudentVisaDetails.VisaIssueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.VisaExpiryDateString = applicationDto.StudentVisaDetails.VisaExpiryDate.HasValue ? applicationDto.StudentVisaDetails.VisaExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            
            //vm.FatherVisaDetailNoID = applicationDto.FatherVisaDetails.VisaDetailsIID;
            //vm.FatherVisaNo = applicationDto.FatherVisaDetails.VisaNo;
            //vm.FatherVisaIssueDateString = applicationDto.FatherVisaDetails.VisaIssueDate.HasValue ? applicationDto.FatherVisaDetails.VisaIssueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            //vm.FatherVisaExpiryDateString = applicationDto.FatherVisaDetails.VisaExpiryDate.HasValue ? applicationDto.FatherVisaDetails.VisaExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            
            //vm.MotherVisaDetailNoID = applicationDto.MotherVisaDetails.VisaDetailsIID;
            //vm.MotherVisaNo = applicationDto.MotherVisaDetails.VisaNo;
            //vm.MotherVisaIssueDateString = applicationDto.MotherVisaDetails.VisaIssueDate.HasValue ? applicationDto.MotherVisaDetails.VisaIssueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            //vm.MotherVisaExpiryDateString = applicationDto.MotherVisaDetails.VisaExpiryDate.HasValue ? applicationDto.MotherVisaDetails.VisaExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.AdhaarCardNo = applicationDto.AdhaarCardNo;
            vm.StudentNationalIDNoIssueDateString = applicationDto.StudentNationalIDNoIssueDate.HasValue ? applicationDto.StudentNationalIDNoIssueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.StudentNationalIDNoExpiryDateString = applicationDto.StudentNationalIDNoExpiryDate.HasValue ? applicationDto.StudentNationalIDNoExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            
            if (applicationDto.IsMinority != null)
            {
                vm.IsMinority = applicationDto.IsMinority.Value;
            }
            if(applicationDto.IsOnlyChildofParent != null)
            { 
                vm.IsOnlyChildofParent = applicationDto.IsOnlyChildofParent.Value;
            }
            
            vm.BloodGroup = applicationDto.BloodGroupID.ToString();
            vm.BloodGroupID = applicationDto.BloodGroupID;

            //For purpose of Studentpicking fill Parent login details
            vm.ParentID = applicationDto.ParentID;
            vm.ParentCode = applicationDto.ParentCode;
            vm.ParentLoginEmailID = applicationDto.ParentLoginEmailID;
            vm.ParentLoginUserID = applicationDto.ParentLoginUserID;
            vm.ParentLoginPassword = applicationDto.ParentLoginPassword;
            vm.ParentLoginPasswordSalt = applicationDto.ParentLoginPasswordSalt;

            //Guardian Details:
            vm.GuardianDetails = new GuardianDetailsViewModel()
            {
                GuardianVisaDetailNoID = applicationDto.GuardianVisaDetailNoID,
                GuardianPassportDetailNoID = applicationDto.GuardianPassportDetailNoID,
                GuardianFirstName = applicationDto.GuardianFirstName,
                GuardianMiddleName = applicationDto.GuardianMiddleName,
                GuardianLastName = applicationDto.GuardianLastName,
                GuardianStudentRelationShipID = applicationDto.GuardianStudentRelationShipID,
                GuardianStudentRelationShip = applicationDto.GuardianStudentRelationShipID.ToString(),
                GuardianOccupation = applicationDto.GuardianOccupation,
                GuardianCompanyName = applicationDto.GuardianCompanyName,
                GuardianMobileNumber = applicationDto.GuardianMobileNumber,
                GuardianWhatsappMobileNo = applicationDto.GuardianWhatsappMobileNo,
                GuardianEmailID = applicationDto.GuardianEmailID,
                GuardianNationalityID = applicationDto.GuardianNationalityID,
                GuardianNationality = applicationDto.GuardianNationalityID.ToString(),
                GuardianNationalID = applicationDto.GuardianNationalID,
                GuardianNationalIDNoIssueDateString = applicationDto.GuardianNationalIDNoIssueDate.HasValue ? applicationDto.GuardianNationalIDNoIssueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                GuardianNationalIDNoExpiryDateString = applicationDto.GuardianNationalIDNoExpiryDate.HasValue ? applicationDto.GuardianNationalIDNoExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                GuardianPassportNumber = applicationDto.GuardianPassportDetails.GuardianPassportNumber,
                CountryofIssueID = applicationDto.GuardianPassportDetails.CountryofIssueID,
                GuardianCountryofIssue = applicationDto.GuardianPassportDetails.CountryofIssueID.ToString(),
                GuardianPassportNoIssueString = applicationDto.GuardianPassportDetails.PassportNoIssueDate.HasValue ? applicationDto.GuardianPassportDetails.PassportNoIssueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                GuardianPassportNoExpiryString = applicationDto.GuardianPassportDetails.PassportNoExpiryDate.HasValue ? applicationDto.GuardianPassportDetails.PassportNoExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
            };

            //Address
            vm.Address = new StudentApplicationAddressViewModel()
            {
                BuildingNo = applicationDto.BuildingNo,
                FlatNo = applicationDto.FlatNo,
                StreetNo = applicationDto.StreetNo,
                StreetName = applicationDto.StreetName,
                LocationNo = applicationDto.LocationNo,
                LocationName = applicationDto.LocationName,
                ZipNo = applicationDto.ZipNo,
                PostBoxNo = applicationDto.PostBoxNo,
                City = applicationDto.City,
                Country = applicationDto.CountryID.HasValue ? applicationDto.CountryID.ToString() : null,
                CountryID = applicationDto.CountryID == null ? (int?)null : applicationDto.CountryID,

            };

            //Previous schoolDetails
            vm.PreviousSchoolDetails = new PreviousSchoolDetailsViewModel()
            {
                IsStudentStudiedBefore = applicationDto.IsStudentStudiedBefore.HasValue ? applicationDto.IsStudentStudiedBefore : null,
                PreviousSchoolAcademicYear = applicationDto.PreviousSchoolAcademicYear,
                SyllabusID = applicationDto.PreviousSchoolSyllabusID,
                PreviousSchoolSyllabus = applicationDto.PreviousSchoolSyllabusID.ToString(),
                PreviousSchoolName = applicationDto.PreviousSchoolName,
                PreviousSchoolSyllabusID = applicationDto.PreviousSchoolSyllabusID,
                PreviousSchoolAddress = applicationDto.PreviousSchoolAddress,
                PreviousSchoolClass = applicationDto.PreviousSchoolClassCompletedID.HasValue ? new KeyValueViewModel() { Key = applicationDto.PreviousSchoolClassCompletedID.ToString(), Value = applicationDto.PreviousSchoolClassCompleted.Value } : null,
                PreviousSchoolClassCompletedID = applicationDto.PreviousSchoolClassCompletedID,
                PreviousSchoolClassClassKey = applicationDto.PreviousSchoolClassCompletedID.HasValue ? applicationDto.PreviousSchoolClassCompletedID.Value.ToString() : null,

            };
            //Father,Mother Details----
            vm.FatherMotherDetails = new FatherMotherDetailsViewModel()
            {
                FatherFirstName = applicationDto.FatherFirstName,
                FatherMiddleName = applicationDto.FatherMiddleName,
                FatherLastName = applicationDto.FatherLastName,
                FatherOccupation = applicationDto.FatherOccupation,
                MobileNumber = applicationDto.MobileNumber,
                FatherWhatsappMobileNo = applicationDto.FatherWhatsappMobileNo,
                EmailID = applicationDto.EmailID,
                MotherFirstName = applicationDto.MotherFirstName,
                MotherMiddleName = applicationDto.MotherMiddleName,
                MotherLastName = applicationDto.MotherLastName,
                FatherNationalID = applicationDto.FatherNationalID,
                CanYouVolunteerToHelpOneID = applicationDto.CanYouVolunteerToHelpOneID.HasValue ? applicationDto.CanYouVolunteerToHelpOneID : null,
                CanYouVolunteerToHelpOneString = applicationDto.CanYouVolunteerToHelpOneID.HasValue ? applicationDto.CanYouVolunteerToHelpOneID.ToString() : null,
                CanYouVolunteerToHelpTwoID = applicationDto.CanYouVolunteerToHelpTwoID.HasValue ? applicationDto.CanYouVolunteerToHelpTwoID : null,
                CanYouVolunteerToHelpTwoString = applicationDto.CanYouVolunteerToHelpTwoID.HasValue ? applicationDto.CanYouVolunteerToHelpTwoID.ToString() : null,
                FatherCountryID = applicationDto.FatherCountryID,
                MotherCountryID = applicationDto.MotherCountryID,
                FatherCountry = applicationDto.FatherCountryID.ToString(),
                MotherCountry = applicationDto.MotherCountryID.ToString(),
                MotherMobileNumber = applicationDto.MotherMobileNumber,
                MotherWhatsappMobileNo = applicationDto.MotherWhatsappMobileNo,
                MotherOccupation = applicationDto.MotherOccupation,
                MotherEmailID = applicationDto.MotherEmailID,
                MotherNationalID = applicationDto.MotherNationalID,
                FatherStudentRelationShip = applicationDto.FatherStudentRelationShipID.ToString(),
                FatherStudentRelationShipID = applicationDto.FatherStudentRelationShipID == null ? (byte?)null : applicationDto.FatherStudentRelationShipID,
                FatherCompanyName = applicationDto.FatherCompanyName,
                MotherCompanyName = applicationDto.MotherCompanyName,
                FatherPassportDetailNoID = applicationDto.FatherPassportDetails.PassportDetailsIID,
                FatherPassportNumber = applicationDto.FatherPassportDetails.PassportNo,
                FatherCountryofIssueID = applicationDto.FatherPassportDetails.FatherCountryofIssueID == null ? (int?)null : applicationDto.FatherPassportDetails.FatherCountryofIssueID,
                FatherCountryofIssue = applicationDto.FatherPassportDetails.FatherCountryofIssueID.ToString(),
                FatherPassportNoIssueString = applicationDto.FatherPassportDetails.PassportNoIssueDate.HasValue ? applicationDto.FatherPassportDetails.PassportNoIssueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                FatherPassportNoExpiryString = applicationDto.FatherPassportDetails.PassportNoExpiryDate.HasValue ? applicationDto.FatherPassportDetails.PassportNoExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,

                MotherPassportDetailNoID = applicationDto.MotherPassportDetails.PassportDetailsIID,
                MotherPassportNumber = applicationDto.MotherPassportDetails.PassportNo,
                MotherCountryofIssueID = applicationDto.MotherPassportDetails.MotherCountryofIssueID == null ? (int?)null : applicationDto.MotherPassportDetails.MotherCountryofIssueID,
                MotherCountryofIssue = applicationDto.MotherPassportDetails.MotherCountryofIssueID.ToString(),
                MotherPassportNoIssueString = applicationDto.MotherPassportDetails.PassportNoIssueDate.HasValue ? applicationDto.MotherPassportDetails.PassportNoIssueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                MotherPassportNoExpiryString = applicationDto.MotherPassportDetails.PassportNoExpiryDate.HasValue ? applicationDto.MotherPassportDetails.PassportNoExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                FatherMobileNumberTwo = applicationDto.FatherMobileNumberTwo,
                FatherNationalDNoIssueDateString = applicationDto.FatherNationalDNoIssueDate.HasValue ? applicationDto.FatherNationalDNoIssueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                FatherNationalDNoExpiryDateString = applicationDto.FatherNationalDNoExpiryDate.HasValue ? applicationDto.FatherNationalDNoExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                MotherNationalDNoIssueDateString = applicationDto.MotherNationalDNoIssueDate.HasValue ? applicationDto.MotherNationalDNoIssueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                MotherNationaIDNoExpiryDateString = applicationDto.MotherNationaIDNoExpiryDate.HasValue ? applicationDto.MotherNationaIDNoExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
            };

            vm.DocumentsUpload = new List<StudentApplicationDocUploadViewModel>();

            vm.DocumentsUpload.Add(new StudentApplicationDocUploadViewModel()
            {
                ApplicationDocumentIID = applicationDto.StudentDocUploads.ApplicationDocumentIID,
                ApplicationID = applicationDto.StudentDocUploads.ApplicationID,
                BirthCertificateReferenceID = applicationDto.StudentDocUploads.BirthCertificateReferenceID.HasValue ? applicationDto.StudentDocUploads.BirthCertificateReferenceID.ToString() : null,
                BirthCertificateAttach = applicationDto.StudentDocUploads.BirthCertificateAttach,
                StudentPassportReferenceID = applicationDto.StudentDocUploads.StudentPassportReferenceID.HasValue ? applicationDto.StudentDocUploads.StudentPassportReferenceID.ToString() : null,
                StudentPassportAttach = applicationDto.StudentDocUploads.StudentPassportAttach,
                TCReferenceID = applicationDto.StudentDocUploads.TCReferenceID.HasValue ? applicationDto.StudentDocUploads.TCReferenceID.ToString() : null,
                TCAttach = applicationDto.StudentDocUploads.TCAttach,
                FatherQIDReferenceID = applicationDto.StudentDocUploads.FatherQIDReferenceID.HasValue ? applicationDto.StudentDocUploads.FatherQIDReferenceID.ToString() : null,
                FatherQIDAttach = applicationDto.StudentDocUploads.FatherQIDAttach,
                MotherQIDReferenceID = applicationDto.StudentDocUploads.MotherQIDReferenceID.HasValue ? applicationDto.StudentDocUploads.MotherQIDReferenceID.ToString() : null,
                MotherQIDAttach = applicationDto.StudentDocUploads.MotherQIDAttach,
                StudentQIDReferenceID = applicationDto.StudentDocUploads.StudentQIDReferenceID.HasValue ? applicationDto.StudentDocUploads.StudentQIDReferenceID.ToString() : null,
                StudentQIDAttach = applicationDto.StudentDocUploads.StudentQIDAttach,
            });

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<StudentApplicationViewModel, StudentApplicationDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<ProspectusViewModel, ProspectusDTO>.CreateMap();
            Mapper<FatherMotherDetailsViewModel, StudentApplicationDTO>.CreateMap();
            Mapper<GuardianDetailsViewModel, StudentApplicationDTO>.CreateMap();
            Mapper<AddressViewModel, StudentApplicationDTO>.CreateMap();
            Mapper<PreviousSchoolDetailsViewModel, StudentApplicationDTO>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var dto = Mapper<StudentApplicationViewModel, StudentApplicationDTO>.Map(this);
            //dto.ApplicationIID = (this.ApplicationIID);
            dto.ApplicationNumber = (this.ApplicationNumber);
            dto.GenderID = string.IsNullOrEmpty(this.Gender) ? (byte?)null : byte.Parse(this.Gender);
            dto.SchoolID = string.IsNullOrEmpty(this.School) ? (byte?)null : byte.Parse(this.School);
            dto.DateOfBirth = string.IsNullOrEmpty(this.DateOfBirthString) ? (DateTime?)null : DateTime.ParseExact(this.DateOfBirthString, dateFormat, CultureInfo.InvariantCulture);
            //dto.CategoryID = string.IsNullOrEmpty(this.Category) ? (byte?)null : byte.Parse(this.Category);
            dto.CastID = string.IsNullOrEmpty(this.Cast) || this.Cast == "?" ? (byte?)null : byte.Parse(this.Cast);
            dto.SecoundLanguageID = string.IsNullOrEmpty(this.SecoundLanguageString) || this.SecoundLanguageString == "?" ? (int?)null : int.Parse(this.SecoundLanguageString);
            dto.ThridLanguageID = string.IsNullOrEmpty(this.ThridLanguageString) || this.ThridLanguageString == "?" ? (int?)null : int.Parse(this.ThridLanguageString);
            dto.CanYouVolunteerToHelpOneID = string.IsNullOrEmpty(this.FatherMotherDetails.CanYouVolunteerToHelpOneString) || this.FatherMotherDetails.CanYouVolunteerToHelpOneString == "?" ? (int?)null : int.Parse(this.FatherMotherDetails.CanYouVolunteerToHelpOneString);
            dto.CanYouVolunteerToHelpTwoID = string.IsNullOrEmpty(this.FatherMotherDetails.CanYouVolunteerToHelpTwoString) || this.FatherMotherDetails.CanYouVolunteerToHelpTwoString == "?" ? (int?)null : int.Parse(this.FatherMotherDetails.CanYouVolunteerToHelpTwoString);
            dto.StreamID = this.Stream == null || string.IsNullOrEmpty(this.Stream.Key) ? (byte?)null : byte.Parse(this.Stream.Key);
            //dto.ClassID = this.Class == null || string.IsNullOrEmpty(this.Class.Key) ? (int?)null : int.Parse(this.Class.Key);
            dto.RelegionID = string.IsNullOrEmpty(this.Relegion) || this.Relegion == "?" ? (byte?)null : byte.Parse(this.Relegion);
            dto.StreamGroupID = this.StreamGroup == null || string.IsNullOrEmpty(this.StreamGroup.Key) ? (byte?)null : byte.Parse(this.StreamGroup.Key);
            dto.ApplicationStatusID = string.IsNullOrEmpty(this.ApplicationStatus) ? (byte?)null : byte.Parse(this.ApplicationStatus);
            //dto.ApplicationTypeID = string.IsNullOrEmpty(this.ApplicationType) ? (int?)null : int.Parse(this.ApplicationType);
            dto.ProspectNumber = (this.ProspectusNumber);
            dto.Remarks = this.Remarks;
            dto.onStreams = this.onStreams;
            dto.ProfileContentID = string.IsNullOrEmpty(this.ProfileUrl) ? (long?)null : long.Parse(this.ProfileUrl);
            if (this.StudentClass != null && !string.IsNullOrEmpty(this.StudentClass.Key))
            {
                dto.ClassID = int.Parse(this.StudentClass.Key);
            }

            if (!string.IsNullOrEmpty(this.ClassKey))
            {
                dto.ClassID = int.Parse(this.ClassKey);
            }

            if (!string.IsNullOrEmpty(this.StreamKey))
            {
                dto.StreamID = byte.Parse(this.StreamKey);
            }

            //dto.ClassID = this.Class.Key == null || string.IsNullOrEmpty(this.Class.Key) ? int.Parse(this.ClassKey) : int.Parse(this.Class.Key);
            dto.StudentPassportNo = this.StudentPassportNo;
            dto.StudentNationalID = this.StudentNationalID;
            dto.EmailID = this.FatherMotherDetails.EmailID;
            dto.GuardianVisaDetailNoID = this.GuardianDetails.GuardianVisaDetailNoID;
            dto.GuardianPassportDetailNoID = this.GuardianDetails.GuardianPassportDetailNoID;
            dto.GuardianFirstName = this.GuardianDetails.GuardianFirstName;
            dto.GuardianMiddleName = this.GuardianDetails.GuardianMiddleName;
            dto.GuardianLastName = this.GuardianDetails.GuardianLastName;
            dto.GuardianStudentRelationShipID = string.IsNullOrEmpty(this.GuardianDetails.GuardianStudentRelationShip) || this.GuardianDetails.GuardianStudentRelationShip == "?" ? (byte?)null : byte.Parse(this.GuardianDetails.GuardianStudentRelationShip);
            dto.GuardianOccupation = this.GuardianDetails.GuardianOccupation;
            dto.GuardianCompanyName = this.GuardianDetails.GuardianCompanyName;
            dto.GuardianMobileNumber = this.GuardianDetails.GuardianMobileNumber;
            dto.GuardianWhatsappMobileNo = this.GuardianDetails.GuardianWhatsappMobileNo;
            dto.GuardianEmailID = this.GuardianDetails.GuardianEmailID;
            dto.GuardianNationalityID = string.IsNullOrEmpty(this.GuardianDetails.GuardianNationality) || this.GuardianDetails.GuardianNationality == "?" ? (int?)null : int.Parse(this.GuardianDetails.GuardianNationality);
            dto.GuardianNationalID = this.GuardianDetails.GuardianNationalID;
            dto.GuardianNationalIDNoIssueDate = string.IsNullOrEmpty(this.GuardianDetails.GuardianNationalIDNoIssueDateString) ? (DateTime?)null : DateTime.ParseExact(this.GuardianDetails.GuardianNationalIDNoIssueDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.GuardianNationalIDNoExpiryDate = string.IsNullOrEmpty(this.GuardianDetails.GuardianNationalIDNoExpiryDateString) ? (DateTime?)null : DateTime.ParseExact(this.GuardianDetails.GuardianNationalIDNoExpiryDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.FatherFirstName = this.FatherMotherDetails.FatherFirstName;
            dto.FatherLastName = this.FatherMotherDetails.FatherLastName;
            dto.FatherMiddleName = this.FatherMotherDetails.FatherMiddleName;
            dto.FatherOccupation = this.FatherMotherDetails.FatherOccupation;
            dto.MobileNumber = this.FatherMotherDetails.MobileNumber;
            dto.FatherWhatsappMobileNo = this.FatherMotherDetails.FatherWhatsappMobileNo;
            dto.FatherCountryID = string.IsNullOrEmpty(this.FatherMotherDetails.FatherCountry) || this.FatherMotherDetails.FatherCountry == "?" ? (int?)null : int.Parse(this.FatherMotherDetails.FatherCountry);
            dto.FatherNationalID = this.FatherMotherDetails.FatherNationalID;
            dto.FatherPassportNumber = this.FatherMotherDetails.FatherPassportNumber;
            dto.MotherFirstName = this.FatherMotherDetails.MotherFirstName;
            dto.MotherMiddleName = this.FatherMotherDetails.MotherMiddleName;
            dto.MotherLastName = this.FatherMotherDetails.MotherLastName;
            dto.MotherMobileNumber = this.FatherMotherDetails.MotherMobileNumber;
            dto.MotherWhatsappMobileNo = this.FatherMotherDetails.MotherWhatsappMobileNo;
            dto.MotherOccupation = this.FatherMotherDetails.MotherOccupation;
            dto.MotherEmailID = this.FatherMotherDetails.MotherEmailID;
            dto.MotherNationalID = this.FatherMotherDetails.MotherNationalID;
            dto.MotherPassportNumber = this.FatherMotherDetails.MotherPassportNumber;
            dto.MotherCountryID = string.IsNullOrEmpty(this.FatherMotherDetails.MotherCountry) || this.FatherMotherDetails.MotherCountry == "?" ? (int?)null : int.Parse(this.FatherMotherDetails.MotherCountry);
            dto.PreviousSchoolSyllabusID = string.IsNullOrEmpty(this.PreviousSchoolDetails.PreviousSchoolSyllabus) || this.PreviousSchoolDetails.PreviousSchoolSyllabus == "?" ? (byte?)null : byte.Parse(this.PreviousSchoolDetails.PreviousSchoolSyllabus);
            dto.CurriculamID = this.CurriculamID.HasValue ? this.CurriculamID : string.IsNullOrEmpty(this.CurriculamString) || this.CurriculamString == "?" ? (byte?)null : byte.Parse(this.CurriculamString);
            dto.PrimaryContactID = string.IsNullOrEmpty(this.PrimaryContact) || this.PrimaryContact == "?" ? (byte?)null : byte.Parse(this.PrimaryContact);
            //dto.PreviousSchoolAcademicYearID = string.IsNullOrEmpty(this.PreviousSchoolAcademicYear) ? (int?)null : int.Parse(this.PreviousSchoolAcademicYear);
            dto.PreviousSchoolAcademicYear = this.PreviousSchoolDetails.PreviousSchoolAcademicYear;
            //dto.FatherStudentRelationShipID = string.IsNullOrEmpty(this.FatherStudentRelationShip) || this.PreviousSchoolSyllabus == "?" ? (byte?)null : byte.Parse(this.FatherStudentRelationShip);
            //dto.MotherStudentRelationShipID = string.IsNullOrEmpty(this.MotherStudentRelationShip) || this.PreviousSchoolSyllabus == "?" ? (byte?)null : byte.Parse(this.MotherStudentRelationShip);
            if(!string.IsNullOrEmpty(this.SchoolAcademicyear))
            {
                dto.SchoolAcademicyearID = string.IsNullOrEmpty(this.SchoolAcademicyear) ? (int?)null : int.Parse(this.SchoolAcademicyear);
            }
            else
            {
                dto.SchoolAcademicyearID = this.AcademicyearID.HasValue ? this.AcademicyearID : (int?)null;
            }

                if (!string.IsNullOrEmpty(this.PreviousSchoolDetails.PreviousSchoolClassClassKey))
                {
                    dto.PreviousSchoolClassCompletedID = int.Parse(this.PreviousSchoolDetails.PreviousSchoolClassClassKey);
                }
                if (this.PreviousSchoolDetails.PreviousSchoolClass != null)
                {
                    dto.PreviousSchoolClassCompletedID = int.Parse(this.PreviousSchoolDetails.PreviousSchoolClass.Key);
                }
                dto.IsStudentStudiedBefore = this.PreviousSchoolDetails.PreviousSchoolName != null ? true : false;
                dto.PreviousSchoolName = this.PreviousSchoolDetails.PreviousSchoolName;
                dto.PreviousSchoolAddress = this.PreviousSchoolDetails.PreviousSchoolAddress;

            if (this.Siblings.Count > 0)
            {
                foreach (KeyValueViewModel kvm in this.Siblings)
                {
                    dto.StudentSiblings.Add(new KeyValueDTO()
                    { Key = kvm.Key, Value = kvm.Value });
                }
            }

            dto.OptionalSubjects = new List<KeyValueDTO>();

            if (this.OptionalSubjects != null)
            {
                if (this.OptionalSubjects.Count > 0)
                {
                    foreach (KeyValueViewModel optSubj in this.OptionalSubjects)
                    {
                        dto.OptionalSubjects.Add(new KeyValueDTO()
                        { Key = optSubj.Key, Value = optSubj.Value });
                    }
                }
            }

            dto.BuildingNo = this.Address.BuildingNo;
            dto.FlatNo = this.Address.FlatNo;
            dto.StreetNo = this.Address.StreetNo;
            dto.StreetName = this.Address.StreetName;
            dto.LocationNo = this.Address.LocationNo;
            dto.LocationName = this.Address.LocationName;
            dto.ZipNo = this.Address.ZipNo;
            dto.PostBoxNo = this.Address.PostBoxNo;
            dto.City = this.Address.City;
            //dto.CountryID = this.CountryID;
            dto.CountryID = string.IsNullOrEmpty(this.Address.Country) || this.Address.Country == "?" ? (int?)null : int.Parse(this.Address.Country);
            dto.FatherCompanyName = this.FatherMotherDetails.FatherCompanyName;
            dto.MotherCompanyName = this.FatherMotherDetails.MotherCompanyName;
            dto.AdhaarCardNo = this.AdhaarCardNo;
            dto.CommunityID = string.IsNullOrEmpty(this.Community) || this.Community == "?" ? (byte?)null : byte.Parse(this.Community);
            dto.StudentNationalIDNoIssueDate = string.IsNullOrEmpty(this.StudentNationalIDNoIssueDateString) ? (DateTime?)null : DateTime.ParseExact(this.StudentNationalIDNoIssueDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.StudentNationalIDNoExpiryDate = string.IsNullOrEmpty(this.StudentNationalIDNoExpiryDateString) ? (DateTime?)null : DateTime.ParseExact(this.StudentNationalIDNoExpiryDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.IsMinority = this.IsMinority;
            dto.IsOnlyChildofParent = this.IsOnlyChildofParent;
            dto.BloodGroupID = string.IsNullOrEmpty(this.BloodGroup) || this.BloodGroup == "?" ? (int?)null : int.Parse(this.BloodGroup);
            dto.StudentCoutryOfBrithID = string.IsNullOrEmpty(this.StudentCoutryOfBrith) ? (int?)null : int.Parse(this.StudentCoutryOfBrith);
            dto.FatherMobileNumberTwo = this.FatherMotherDetails.FatherMobileNumberTwo;
            dto.FatherNationalDNoIssueDate = string.IsNullOrEmpty(this.FatherMotherDetails.FatherNationalDNoIssueDateString) ? (DateTime?)null : DateTime.ParseExact(this.FatherMotherDetails.FatherNationalDNoIssueDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.FatherNationalDNoExpiryDate = string.IsNullOrEmpty(this.FatherMotherDetails.FatherNationalDNoExpiryDateString) ? (DateTime?)null : DateTime.ParseExact(this.FatherMotherDetails.FatherNationalDNoExpiryDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.MotherNationalDNoIssueDate = string.IsNullOrEmpty(this.FatherMotherDetails.MotherNationalDNoIssueDateString) ? (DateTime?)null : DateTime.ParseExact(this.FatherMotherDetails.MotherNationalDNoIssueDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.MotherNationaIDNoExpiryDate = string.IsNullOrEmpty(this.FatherMotherDetails.MotherNationaIDNoExpiryDateString) ? (DateTime?)null : DateTime.ParseExact(this.FatherMotherDetails.MotherNationaIDNoExpiryDateString, dateFormat, CultureInfo.InvariantCulture);

            dto.StudentPassportDetailNoID = this.StudentPassportDetailNoID;
            dto.StudentPassportDetails = new StudentPassportDetailsDTO()
            {
                PassportDetailsIID = this.StudentPassportDetailNoID.HasValue ? this.StudentPassportDetailNoID.Value : 0,
                PassportNo = this.StudentPassportNo,
                CountryofIssueID = string.IsNullOrEmpty(this.StudentCountryofIssue) ? (int?)null : int.Parse(this.StudentCountryofIssue),
                PassportNoIssueDate = string.IsNullOrEmpty(this.PassportNoIssueString) ? (DateTime?)null : DateTime.ParseExact(this.PassportNoIssueString, dateFormat, CultureInfo.InvariantCulture),
                PassportNoExpiryDate = string.IsNullOrEmpty(this.PassportNoExpiryString) ? (DateTime?)null : DateTime.ParseExact(this.PassportNoExpiryString, dateFormat, CultureInfo.InvariantCulture),
            
            };
            dto.GuardianPassportDetailNoID = this.GuardianDetails.GuardianPassportDetailNoID;
            dto.GuardianPassportDetails = new GuardianPassportDetailsDTO()
            {
                PassportDetailsIID = this.GuardianDetails.GuardianPassportDetailNoID.HasValue ? this.GuardianDetails.GuardianPassportDetailNoID.Value : 0,
                GuardianPassportNumber = this.GuardianDetails.GuardianPassportNumber,
                CountryofIssueID = string.IsNullOrEmpty(this.GuardianDetails.GuardianCountryofIssue) ? (int?)null : int.Parse(this.GuardianDetails.GuardianCountryofIssue),
                PassportNoIssueDate = string.IsNullOrEmpty(this.GuardianDetails.GuardianPassportNoIssueString) ? (DateTime?)null : DateTime.ParseExact(this.GuardianDetails.GuardianPassportNoIssueString, dateFormat, CultureInfo.InvariantCulture),
                PassportNoExpiryDate = string.IsNullOrEmpty(this.GuardianDetails.GuardianPassportNoExpiryString) ? (DateTime?)null : DateTime.ParseExact(this.GuardianDetails.GuardianPassportNoExpiryString, dateFormat, CultureInfo.InvariantCulture),

            };
            dto.FatherPassportDetails = new FatherPassportDetailsDTO()
            {
                PassportDetailsIID = this.FatherMotherDetails.FatherPassportDetailNoID.HasValue ? this.FatherMotherDetails.FatherPassportDetailNoID.Value : 0,
                PassportNo = this.FatherMotherDetails.FatherPassportNumber,
                FatherCountryofIssueID = string.IsNullOrEmpty(this.FatherMotherDetails.FatherCountryofIssue) ? (int?)null : int.Parse(this.FatherMotherDetails.FatherCountryofIssue),
                PassportNoIssueDate = string.IsNullOrEmpty(this.FatherMotherDetails.FatherPassportNoIssueString) ? (DateTime?)null : DateTime.ParseExact(this.FatherMotherDetails.FatherPassportNoIssueString, dateFormat, CultureInfo.InvariantCulture),
                PassportNoExpiryDate = string.IsNullOrEmpty(this.FatherMotherDetails.FatherPassportNoExpiryString) ? (DateTime?)null : DateTime.ParseExact(this.FatherMotherDetails.FatherPassportNoExpiryString, dateFormat, CultureInfo.InvariantCulture),

            };
            dto.FatherPassportDetailNoID = this.FatherMotherDetails.FatherPassportDetailNoID;
            dto.FatherPassportDetails = new FatherPassportDetailsDTO()
            {
                PassportDetailsIID = this.FatherMotherDetails.FatherPassportDetailNoID.HasValue ? this.FatherMotherDetails.FatherPassportDetailNoID.Value : 0,
                PassportNo = this.FatherMotherDetails.FatherPassportNumber,
                FatherCountryofIssueID = string.IsNullOrEmpty(this.FatherMotherDetails.FatherCountryofIssue) ? (int?)null : int.Parse(this.FatherMotherDetails.FatherCountryofIssue),
                PassportNoIssueDate = string.IsNullOrEmpty(this.FatherMotherDetails.FatherPassportNoIssueString) ? (DateTime?)null : DateTime.ParseExact(this.FatherMotherDetails.FatherPassportNoIssueString, dateFormat, CultureInfo.InvariantCulture),
                PassportNoExpiryDate = string.IsNullOrEmpty(this.FatherMotherDetails.FatherPassportNoExpiryString) ? (DateTime?)null : DateTime.ParseExact(this.FatherMotherDetails.FatherPassportNoExpiryString, dateFormat, CultureInfo.InvariantCulture),

            };
            dto.MotherPassportDetailNoID = this.FatherMotherDetails.MotherPassportDetailNoID;
            dto.MotherPassportDetails = new MotherPassportDetailsDTO()
            {
                PassportDetailsIID = this.FatherMotherDetails.MotherPassportDetailNoID.HasValue ? this.FatherMotherDetails.MotherPassportDetailNoID.Value : 0,
                PassportNo = this.FatherMotherDetails.MotherPassportNumber,
                MotherCountryofIssueID = string.IsNullOrEmpty(this.FatherMotherDetails.MotherCountryofIssue) ? (int?)null : int.Parse(this.FatherMotherDetails.MotherCountryofIssue),
                PassportNoIssueDate = string.IsNullOrEmpty(this.FatherMotherDetails.MotherPassportNoIssueString) ? (DateTime?)null : DateTime.ParseExact(this.FatherMotherDetails.MotherPassportNoIssueString, dateFormat, CultureInfo.InvariantCulture),
                PassportNoExpiryDate = string.IsNullOrEmpty(this.FatherMotherDetails.MotherPassportNoExpiryString) ? (DateTime?)null : DateTime.ParseExact(this.FatherMotherDetails.MotherPassportNoExpiryString, dateFormat, CultureInfo.InvariantCulture),

            };
            dto.StudentVisaDetailNoID = this.StudentVisaDetailNoID;
            dto.StudentVisaDetails = new StudentVisaDetailsDTO()
            {
                VisaDetailsIID = this.StudentVisaDetailNoID.HasValue ? this.StudentVisaDetailNoID.Value : 0,
                VisaNo = this.StudentVisaNo,
                //VisaIssueDate = string.IsNullOrEmpty(this.VisaIssueDateString) ? (DateTime?)null : DateTime.ParseExact(this.VisaIssueDateString, dateFormat, CultureInfo.InvariantCulture),
                VisaExpiryDate = string.IsNullOrEmpty(this.VisaExpiryDateString) ? (DateTime?)null : DateTime.ParseExact(this.VisaExpiryDateString, dateFormat, CultureInfo.InvariantCulture),

            };
            //dto.FatherPassportDetailNoID = this.FatherVisaDetailNoID;
            dto.FatherVisaDetails = new FatherVisaDetailsDTO()
            {
                //VisaDetailsIID = this.FatherVisaDetailNoID.HasValue ? this.FatherVisaDetailNoID.Value : 0,
                //VisaNo = this.FatherVisaNo,
                //VisaIssueDate = string.IsNullOrEmpty(this.FatherVisaIssueDateString) ? (DateTime?)null : DateTime.ParseExact(this.FatherVisaIssueDateString, dateFormat, CultureInfo.InvariantCulture),
                //VisaExpiryDate = string.IsNullOrEmpty(this.FatherVisaExpiryDateString) ? (DateTime?)null : DateTime.ParseExact(this.FatherVisaExpiryDateString, dateFormat, CultureInfo.InvariantCulture),

            };
            //dto.MotherPassportDetailNoID = this.MotherVisaDetailNoID;
            dto.MotherVisaDetails = new MotherVisaDetailsDTO()
            {
                //VisaDetailsIID = this.MotherVisaDetailNoID.HasValue ? this.MotherVisaDetailNoID.Value : 0,
                //VisaNo = this.MotherVisaNo,
                //VisaIssueDate = string.IsNullOrEmpty(this.MotherVisaIssueDateString) ? (DateTime?)null : DateTime.ParseExact(this.MotherVisaIssueDateString, dateFormat, CultureInfo.InvariantCulture),
                //VisaExpiryDate = string.IsNullOrEmpty(this.MotherVisaExpiryDateString) ? (DateTime?)null : DateTime.ParseExact(this.MotherVisaExpiryDateString, dateFormat, CultureInfo.InvariantCulture),

            };

            //dto.Prospectus = new ProspectusDTO()
            //{
            //    ProsNo = this.Prospectus != null || this.Prospectus.ProsNo != null ? this.Prospectus.ProsNo : null,
            //    ProsFee = this.Prospectus != null || this.Prospectus.ProsFee != null ? this.Prospectus.ProsFee : null,
            //    ProsRemarks = this.Prospectus != null || this.Prospectus.ProsRemarks != null ? this.Prospectus.ProsRemarks : null,
            //};

            dto.StudentDocUploads = new StudentApplicationDocumentsUploadDTO();
            foreach (var attachmentMap in this.DocumentsUpload)
            {
                dto.StudentDocUploads = new StudentApplicationDocumentsUploadDTO()
                {
                    ApplicationDocumentIID = attachmentMap.ApplicationDocumentIID,
                    ApplicationID = attachmentMap.ApplicationID,
                    BirthCertificateReferenceID = this.ResetBirthCertificate == "0" ? null : attachmentMap.BirthCertificateReferenceID != null  ? long.Parse(attachmentMap.BirthCertificateReferenceID) : (long?)null,
                    BirthCertificateAttach = this.ResetBirthCertificate == "0" ? null : attachmentMap.BirthCertificateAttach,
                    StudentPassportReferenceID = this.ResetStudentPassportReference == "0" ? null : attachmentMap.StudentPassportReferenceID != null ? long.Parse(attachmentMap.StudentPassportReferenceID) : (long?)null,
                    StudentPassportAttach = this.ResetStudentPassportReference == "0" ? null : attachmentMap.StudentPassportAttach,
                    TCReferenceID = this.ResetTC =="0" ? null : attachmentMap.TCReferenceID != null ?  long.Parse(attachmentMap.TCReferenceID) : (long?)null,
                    TCAttach = this.ResetTC == "0" ? null : attachmentMap.TCAttach,
                    FatherQIDReferenceID = this.ResetFatherQID == "0" ? null : attachmentMap.FatherQIDReferenceID != null ?  long.Parse(attachmentMap.FatherQIDReferenceID) : (long?)null,
                    FatherQIDAttach = this.ResetFatherQID == "0" ? null : attachmentMap.FatherQIDAttach,
                    MotherQIDReferenceID = this.ResetMotherQID == "0" ? null : attachmentMap.MotherQIDReferenceID != null ?  long.Parse(attachmentMap.MotherQIDReferenceID) : (long?)null,
                    MotherQIDAttach = this.ResetMotherQID == "0" ? null : attachmentMap.MotherQIDAttach,
                    StudentQIDReferenceID = this.ResetStudentQID == "0" ? null : attachmentMap.StudentQIDReferenceID != null ?  long.Parse(attachmentMap.StudentQIDReferenceID) : (long?)null,
                    StudentQIDAttach = this.ResetStudentQID == "0" ? null : attachmentMap.StudentQIDAttach,
                };
            }

            return dto;
        }

        //public StudentApplicationViewModel FromLeadVM(LeadViewModel applicationVM)
        //{
        //    var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
        //    var vm = new StudentApplicationViewModel()
        //    {
                
        //    };
        //    return vm;
        //}

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentApplicationDTO>(jsonString);
        }

        public StudentApplicationViewModel FromLeadApplicationVM(LeadViewModel applicationVM)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var vm = new StudentApplicationViewModel()
            {
                FirstName = applicationVM.ContactDetails.StudentName,
                ProspectusNumber = applicationVM.LeadCode,
                DateOfBirthString = applicationVM.ContactDetails.DateOfBirth.HasValue ? applicationVM.ContactDetails.DateOfBirth.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                CurriculamString = applicationVM.ContactDetails.CurriculamID.ToString(),
                Gender = applicationVM.ContactDetails.GenderID.HasValue ? applicationVM.ContactDetails.GenderID.Value.ToString() : null,
                StudentClass = applicationVM.ContactDetails.ClassID.HasValue ? new KeyValueViewModel() { Key = applicationVM.ContactDetails.ClassID.ToString(), Value = applicationVM.ContactDetails.Class } : null,
                School = applicationVM.SchoolID.ToString(),
                SchoolAcademicyear = applicationVM.ContactDetails.AcademicYearID != null ? applicationVM.ContactDetails.AcademicYearID.ToString() : null,
                Academicyear = applicationVM.ContactDetails.AcademicYearID != null ? applicationVM.ContactDetails.AcademicYearID.ToString() : null,
                AcademicyearID = applicationVM.ContactDetails.AcademicYearID,
            };
            vm.FatherMotherDetails = new FatherMotherDetailsViewModel()
            {
                MobileNumber = applicationVM.ContactDetails.MobileNumber,
                EmailID = applicationVM.ContactDetails.EmailAddress,
                FatherFirstName = applicationVM.ContactDetails.ParentName,
            };
            return vm;
        }

    }
}


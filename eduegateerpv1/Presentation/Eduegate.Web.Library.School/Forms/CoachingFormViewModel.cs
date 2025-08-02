using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Forms;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.School.Forms
{
    public class CoachingFormViewModel : BaseMasterViewModel
    {
        public CoachingFormViewModel()
        {

        }

        public long CoachingFormIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Application number")]
        public string ApplicationNumber { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Admission number")]
        public string AdmissionNumber { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Student")]
        public string StudentName { get; set; }
        public long? StudentID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("School")]
        public string SchoolName { get; set; }
        public byte? SchoolID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Academic year")]
        public string AcademicYearName { get; set; }
        public int? AcademicYearID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Curriculam")]
        public string Curriculam { get; set; }
        public byte? CurriculamID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Grade")]
        public string Grade { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, "", attribs: "ng-disabled=CRUDModel.ViewModel.CoachingFormIID != 0")]
        [DisplayName("Registration date")]
        public string RegistrationDateString { get; set; }
        public DateTime? RegistrationDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Blood group")]
        public string BloodGroup { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Gender")]
        public string Gender { get; set; }
        public byte? GenderID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Religion")]
        public string Religion { get; set; }
        public byte? ReligionID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, "", attribs: "ng-disabled=CRUDModel.ViewModel.CoachingFormIID != 0")]
        [DisplayName("Date of birth")]
        public string DateOfBirthString { get; set; }
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Birth country")]
        public string BirthCountry { get; set; }
        public int? BirthCountryID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Nationality")]
        public string Nationality { get; set; }
        public string NationalityID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Second language")]
        public string SecondLanguage { get; set; }
        public string SecondLanguageID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Third language")]
        public string ThirdLanguage { get; set; }
        public string ThirdLanguageID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Passport number")]
        public string PassportNumber { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, "", attribs: "ng-disabled=CRUDModel.ViewModel.CoachingFormIID != 0")]
        [DisplayName("Passport expiry date")]
        public string PassportExpiryDateString { get; set; }
        public DateTime? PassportExpiryDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("National ID")]
        public string NationalID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, "", attribs: "ng-disabled=CRUDModel.ViewModel.CoachingFormIID != 0")]
        [DisplayName("National ID expiry date")]
        public string NationalIDExpiryDateString { get; set; }
        public DateTime? NationalIDExpiryDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Primary contact")]
        public string PrimaryContact { get; set; }
        public byte? PrimaryContactID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("PO box number")]
        public string POBoxNumber { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [DisplayName("Primary address")]
        public string PrimaryAddress { get; set; }


        public long? ParentID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Father name")]
        public string FatherName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Father mobile number")]
        public string FatherMobileNumber { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Father designation")]
        public string FatherDesignation { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Father company name")]
        public string FatherCompanyName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Father national ID")]
        public string FatherNationalID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Father email ID")]
        public string FatherEmailID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Mother name")]
        public string MotherName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Mother mobile number")]
        public string MotherMobileNumber { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Mother designation")]
        public string MotherDesignation { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Mother company name")]
        public string MotherCompanyName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Mother national ID")]
        public string MotherNationalID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Mother email ID")]
        public string MotherEmailID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Sibling name")]
        public string SiblingName { get; set; }
        public long? SiblingStudentID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Sibling admission number")]
        public string SiblingAdmissionNumber { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Sibling grade")]
        public string SiblingGrade { get; set; }
        public int? SiblingClassID { get; set; }
        public int? SiblingSectionID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Previous school name")]
        public string PreviousSchoolName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Previous school curriculam")]
        public string PreviousSchoolCurriculam { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Previous school grade")]
        public string PreviousSchoolGrade { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("Is select terms and conditions")]
        public string IsSelectTermsAndConditions { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.StudentPickupRequestStatus")]
        [CustomDisplay("Status")]
        public string ApplicationStatusID { get; set; }
        public string ApplicationStatus { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as CoachingFormDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<CoachingFormViewModel>(jsonString);
        }

        public CoachingFormViewModel ToVM(CoachingFormDTO dto)
        {
            Mapper<CoachingFormDTO, CoachingFormViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var requestDTO = dto as CoachingFormDTO;
            var vm = Mapper<CoachingFormDTO, CoachingFormViewModel>.Map(requestDTO);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");            

            return vm;
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<CoachingFormDTO, CoachingFormViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var requestDTO = dto as CoachingFormDTO;
            var vm = Mapper<CoachingFormDTO, CoachingFormViewModel>.Map(requestDTO);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<CoachingFormViewModel, CoachingFormDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<CoachingFormViewModel, CoachingFormDTO>.Map(this);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<CoachingFormDTO>(jsonString);
        }

    }
}
using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Framework.Enums;
using Eduegate.Web.Library.Common;
namespace Eduegate.Web.Library.School.Students
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "StudentProfile", "CRUDModel.ViewModel.StudentProfile")]
    [DisplayName("Student")]
    public class StudentProfileViewModel : BaseMasterViewModel
    {
        public StudentProfileViewModel()
        {
            StudentClass = new KeyValueViewModel();
            Section = new KeyValueViewModel();
            Sibling = new List<KeyValueViewModel>();
            Category = new KeyValueViewModel();
            Gender = new KeyValueViewModel();
            Cast = new KeyValueViewModel();
            Relegion = new KeyValueViewModel();
            BloodGroup = new KeyValueViewModel();
            StudentHouse = new KeyValueViewModel();
            //StudentName = new StudentNameViewModel();
        }

        public long StudentIID { get; set; }
        public long? LoginID { get; set; }

        public KeyValueViewModel School { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBoxWithAutogeneration)]
        [DisplayName("Admission Number")]
        public string AdmissionNumber { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBoxWithAutogeneration)]
        [DisplayName("Roll Number")]
        public string RollNumber { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBoxWithAutogeneration)]
        [DisplayName("Student ID")]
        public string StudentID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Should be characters")]
        [MaxLength(200, ErrorMessage = "Maximum Length should be within 200!")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.FileUpload)]
        [CustomDisplay("StudentProfile")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.UserProfile, "ProfileUrl", "")]
        public string StudentProfile { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(200, ErrorMessage = "Maximum Length should be within 200!")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Class", "Numeric", false, "")]
        [LookUp("LookUps.Classes")]
        [DisplayName("Class")]
        public KeyValueViewModel StudentClass { get; set; }
        public int? ClassID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Section", "Numeric", false, "")]
        [DisplayName("Section")]
        [LookUp("LookUps.Section")]
        public KeyValueViewModel Section { get; set; }
        public int? SectionID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [DisplayName("Gender")]
        [LookUp("LookUps.Gender")]
        public KeyValueViewModel Gender { get; set; }
        public byte? GenderID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Date Of Birth")]
        public string DateOfBirthString { get; set; }

        public DateTime? DateOfBirth { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Classes", "Numeric", false, "")]
        [LookUp("LookUps.StudentCategory")]
        [DisplayName("Category")]
        public KeyValueViewModel Category { get; set; }

        public int? CategoryID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [DisplayName("Cast")]
        [LookUp("LookUps.Cast")]
        public KeyValueViewModel Cast { get; set; }

        public int? CastID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [DisplayName("Religion")]
        [LookUp("LookUps.Relegion")]
        public KeyValueViewModel Relegion { get; set; }

        public int? RelegionID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [DisplayName("Community")]
        [LookUp("LookUps.Community")]
        public KeyValueViewModel Community { get; set; }

        public byte? CommunityID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(13, ErrorMessage = "Maximum Length should be within 13!")]
        [DisplayName("Mobile Number")]
        public string MobileNumber { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("Email ID")]
        public string EmailID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DateTimePicker)]
        [DisplayName("Admission Date")]
        public string AdmissionDateString { get; set; }
        public DateTime? AdmissionDate { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [DisplayName("Blood Group")]
        [LookUp("LookUps.BloodGroup")]
        public KeyValueViewModel BloodGroup { get; set; }

        public int? BloodGroupID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [DisplayName("Student House")]
        [LookUp("LookUps.StudentHouse")]
        public KeyValueViewModel StudentHouse { get; set; }

        public int? StudentHouseID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(4, ErrorMessage = "Maximum Length should be within 4!")]
        [DisplayName("Height")]
        public string Height { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(4, ErrorMessage = "Maximum Length should be within 4!")]
        [DisplayName("Weight")]
        public string Weight { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DateTimePicker)]
        [DisplayName("As On Date")]
        public string AsOnDateString { get; set; }

        public DateTime? AsOnDate { get; set; }

        public string AdhaarCardNo { get; set; }
        public string PassportNo { get; set; }
        public DateTime? PassportNoExpiry { get; set; }
        public string VisaNo { get; set; }
        public DateTime? VisaExpiry { get; set; }
        public string NationalIDNo { get; set; }
        public DateTime? NationalIDNoExpiry { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Students", "Numeric", true, "")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Student", "LookUps.Students")]
        [DisplayName("Siblings")]
        public List<KeyValueViewModel> Sibling { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as StudentDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentProfileViewModel>(jsonString);
        }

    }
}

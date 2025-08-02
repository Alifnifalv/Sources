using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
//using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.School.Common;
using Eduegate.Web.Library.ViewModels;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Eduegate.Web.Library.Common;
using Microsoft.AspNetCore.Mvc;

namespace Eduegate.Web.Library.School.Common
{
    //TODO: Need to remove this class
    //[Bind(Exclude = "StatusID")]
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "StudentLogin", "CRUDModel.ViewModel.StudentLogin")]
    [DisplayName("Login Info")]
    public class StudentLoginViewModel : BaseMasterViewModel
    {
        public StudentLoginViewModel()
        {
            Status = new KeyValueViewModel() { Key = "1", Value = Eduegate.Services.Contracts.Enums.LoginUserStatus.Active.ToString() };
        }

        [DataPicker("ParentLogin")]
        [ControlType(Framework.Enums.ControlTypes.DataPicker)]
        [CustomDisplay("LoginID")]
        public long LoginIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("LoginUserID")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string LoginUserID { get; set; }

        [EmailAddress]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("LoginEmailID")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string LoginEmailID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("SetPassword")]
        public bool IsRequired { get; set; }

        [IsRequired("IsRequired")]
        [ControlType(Framework.Enums.ControlTypes.Password, attribs: "ng-disabled='!CRUDModel.ViewModel.ParentLogin.IsRequired'")]
        [CustomDisplay("Password")]
        public string Password { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Password Salt")]
        //public string PasswordSalt { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("PasswordHint")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string PasswordHint { get; set; }

        public string PasswordSalt { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("LoginUserStatus", "Numeric", false)]
        [LookUp("LookUps.LoginUserStatus")]
        [CustomDisplay("Status")]
        public KeyValueViewModel Status { get; set; }

        public LoginUserStatus? StatusID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Lastloginon")]
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        
        public Nullable<int> RegisteredCountryID { get; set; }
        public string RegisteredIP { get; set; }
        public Nullable<int> SiteID { get; set; }
        public string RegisteredIPCountry { get; set; }

        public long LoginRoleMapIID { get; set; }

        public static StudentLoginViewModel FromDTO(LoginDTO dto)
        {
            Mapper<LoginDTO, StudentLoginViewModel>.CreateMap();
            return Mapper<LoginDTO, StudentLoginViewModel>.Map(dto);
        }

        public static LoginDTO ToDTO(StudentLoginViewModel vm)
        {
            Mapper<StudentLoginViewModel, LoginDTO>.CreateMap();
            return Mapper<StudentLoginViewModel, LoginDTO>.Map(vm);
        }
    }
}

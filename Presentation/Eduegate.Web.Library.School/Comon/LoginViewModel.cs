using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Infrastructure.Enums;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.School.Common;
using Eduegate.Web.Library.ViewModels;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Eduegate.Web.Library.School.Common
{
    //TODO: Need to remove this class
    //[Bind(Exclude = "StatusID")]
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "CustomerLogin", "CRUDModel.ViewModel.Login")]
    [DisplayName("Login Info")]
    public class LoginViewModel : BaseMasterViewModel
    {
        public LoginViewModel()
        {
            Status = new KeyValueViewModel() { Key = "1", Value = Infrastructure.Enums.LoginUserStatus.Active.ToString() };
        }

        [DataPicker("Login")]
        [ControlType(Framework.Enums.ControlTypes.DataPicker)]
        [DisplayName("Login ID")]
        public long LoginIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Login User ID")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string LoginUserID { get; set; }

        [Required]
        [EmailAddress]
        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-crud-unique controllercall=" + "'Login/CheckCustomerEmailIDAvailability?loginID={{CRUDModel.ViewModel.Login.LoginIID}}&loginEmailID={{CRUDModel.ViewModel.Login.LoginEmailID}}'" + " message=' already exist.'")]
        [DisplayName("Login Email ID")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string LoginEmailID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("Set Password")]
        public bool IsRequired { get; set; }

        [IsRequired("IsRequired")]
        [ControlType(Framework.Enums.ControlTypes.Password, attribs: "ng-disabled='!CRUDModel.ViewModel.Login.IsRequired'")]
        [DisplayName("Password")]
        public string Password { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Password Salt")]
        //public string PasswordSalt { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Password Hint")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string PasswordHint { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("LoginUserStatus", "Numeric", false)]
        [LookUp("LookUps.LoginUserStatus")]
        [DisplayName("Status")]
        public KeyValueViewModel Status { get; set; }

        public Infrastructure.Enums.LoginUserStatus? StatusID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Last login on")]
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        
        public Nullable<int> RegisteredCountryID { get; set; }
        public string RegisteredIP { get; set; }
        public Nullable<int> SiteID { get; set; }
        public string RegisteredIPCountry { get; set; }

        public static LoginViewModel FromDTO(LoginDTO dto)
        {
            Mapper<LoginDTO, LoginViewModel>.CreateMap();
            return Mapper<LoginDTO, LoginViewModel>.Map(dto);
        }

        public static LoginDTO ToDTO(LoginViewModel vm)
        {
            Mapper<LoginViewModel, LoginDTO>.CreateMap();
            return Mapper<LoginViewModel, LoginDTO>.Map(vm);
        }
    }
}

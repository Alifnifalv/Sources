using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Web.Library.Common;
using Microsoft.AspNetCore.Mvc;

namespace Eduegate.Web.Library.ViewModels
{
    //TODO: Need to check "Exclude" error
    //[Bind(Exclude = "StatusID")]
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "CustomerLogin", "CRUDModel.ViewModel.Login")]
    [DisplayName("Login Info")]
    public class LoginViewModel : BaseMasterViewModel
    {
        public LoginViewModel()
        {
            Status = new KeyValueViewModel() { Key = "1", Value = Eduegate.Services.Contracts.Enums.LoginUserStatus.Active.ToString() };
        }

        [DataPicker("Login")]
        [ControlType(Framework.Enums.ControlTypes.DataPicker)]
        [CustomDisplay("LoginID")]
        public long LoginIID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("LoginUserID")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string LoginUserID { get; set; }

        //[Required]
        [EmailAddress]
        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-crud-unique controllercall=" + "'Login/CheckCustomerEmailIDAvailability?loginID={{CRUDModel.ViewModel.Login.LoginIID}}&loginEmailID={{CRUDModel.ViewModel.Login.LoginEmailID}}'" + " message=' already exist.'")]
        [CustomDisplay("LoginEmailID")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string LoginEmailID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("SetPassword")]
        public bool IsRequired { get; set; }

        [IsRequired("IsRequired")]
        [ControlType(Framework.Enums.ControlTypes.Password, attribs: "ng-disabled='!CRUDModel.ViewModel.Login.IsRequired'")]
        [CustomDisplay("Password")]
        public string Password { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Password Salt")]
        //public string PasswordSalt { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled='!CRUDModel.ViewModel.Login.IsRequired'")]
        [CustomDisplay("PasswordHint")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string PasswordHint { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("LoginUserStatus", "Numeric", false)]
        [LookUp("LookUps.LoginUserStatus")]
        [CustomDisplay("Status")]
        public KeyValueViewModel Status { get; set; }

        public LoginUserStatus? StatusID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Last login on")]
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

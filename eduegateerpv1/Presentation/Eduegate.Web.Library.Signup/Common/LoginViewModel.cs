using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Common;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Eduegate.Web.Library.SignUp.Common
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "Login", "CRUDModel.ViewModel.Login")]
    [DisplayName("Login Info")]
    public class LoginViewModel : BaseMasterViewModel
    {
        public LoginViewModel()
        {
        }

        [RegularExpression(
         @"^[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?\.)+[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?$|" +
         @"^[A-Za-z0-9]+$",
            ErrorMessage = "Invalid Email Address or User ID"
         )]
        public string Email { get; set; }

        //public string LoginType { get; set; } = "UserID";

        //[Display(Name = "User ID")]
        //public string LoginUserID { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        //[Required(ErrorMessage = "Company is required")]
        [Display(Name = "Company")]
        public string CompanyID { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public List<KeyValueViewModel> Companies { get; set; }

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
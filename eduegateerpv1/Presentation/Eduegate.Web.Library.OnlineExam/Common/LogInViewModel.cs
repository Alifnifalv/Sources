using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.OnlineExam.Common
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "LogIn", "CRUDModel.ViewModel")]
    [DisplayName("Login")]
    public class LogInViewModel : BaseMasterViewModel
    {
        public LogInViewModel()
        {
        }

        [Required(ErrorMessage = "*")]
        [Display(Name = "User Name")]
        [MaxLength(50, ErrorMessage = "Maxminum length of user name allowed is 50"), MinLength(5, ErrorMessage = "Mininmum length of user name  allowed is 5")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Please enter password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [MaxLength(50, ErrorMessage = "Maxminum length of password allowed is 50"), MinLength(5, ErrorMessage = "Mininmum length of password allowed is 5")]
        public string Password { get; set; }

        public bool RememberPassword { get; set; }

    }
}
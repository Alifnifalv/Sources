using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eduegate.Signup.Portal.ViewModel
{
    public class LogInViewModel
    {

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

    public class ResponseModel
    {
        // [Bind(Prefix = "some_prefix")]
        public string Message { set; get; }
        public object Data { set; get; }
        public bool isError { get; set; }
    }
}
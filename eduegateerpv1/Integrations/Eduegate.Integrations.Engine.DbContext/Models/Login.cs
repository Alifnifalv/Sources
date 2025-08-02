using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Eduegate.Integrations.Engine.DbContexts.Models
{
   public class Login
    {
        [Key]
        public string LoginUserID { get; set; }
        public string LoginEmailID { get; set; }
        public string UserName { get; set; }    
        public string EmployeeID { get; set; }
        public string PasswordHint { get; set; }
        public string PasswordSalt { get; set; }
        public string Password { get; set; }
        public int StatusID { get; set; }
    }
}

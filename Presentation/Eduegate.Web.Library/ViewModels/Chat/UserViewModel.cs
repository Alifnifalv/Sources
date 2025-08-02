using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eduegate.Web.Library.Chat.ViewModels
{
    public class UserViewModel
    {
        public string ConnectionID { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }

        public UserRoles UserRole { get; set; }

        public bool IsTitleRequired { get; set; }

        public bool IsInNewWindow { get; set; }

        public string CultureCode {get;set;}
        public string CountryCode { get; set; }

    }

    public enum UserRoles {
        None,
        Admin,
        Customer,
        Others,
        Operator
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eduegate.Web.Library.ViewModels
{
    public class UserInfoViewModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string IID { get; set; }
        public decimal UnreadMessageCount { get; set; }
        public string ProfileImageURL { get; set; }
    }
}
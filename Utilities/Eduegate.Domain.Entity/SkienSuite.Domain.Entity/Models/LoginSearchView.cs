using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class LoginSearchView
    {
        public long LoginIID { get; set; }
        public string LoginUserID { get; set; }
        public string LoginEmailID { get; set; }
        public string StatusName { get; set; }
        public string RoleName { get; set; }
    }
}

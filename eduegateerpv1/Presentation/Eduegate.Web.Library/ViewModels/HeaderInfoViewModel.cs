using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eduegate.Web.Library.ViewModels
{
    public class HeaderInfoViewModel
    {
        public UserInfoViewModel userInfo { get; set; }

        public List<EmailMessageViewModel> EmailMeassages { get; set; }

        public decimal UnreadEmailCount { get; set; }
    }
}
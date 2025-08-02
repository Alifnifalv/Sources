using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eduegate.Web.Library.ViewModels
{
    public class EmailMessageViewModel
    {
        public string Subject { get; set; }
        public DateTime SentDateTime { get; set; }
        public bool IsRead { get; set; }
    }
}
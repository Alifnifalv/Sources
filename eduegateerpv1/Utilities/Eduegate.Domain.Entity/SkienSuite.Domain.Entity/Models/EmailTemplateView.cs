using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class EmailTemplateView
    {
        public int EmailNotificationTypeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EmailSubject { get; set; }
        public string ToCCEmailID { get; set; }
        public string ToBCCEmailID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class MarketingEmail2015Temp
    {
        [Key]
        public long RowID { get; set; }
        public long CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public int Points { get; set; }
        public bool EmailSent { get; set; }
        public System.DateTime EmailSentOn { get; set; }
        public Nullable<bool> EmailOpened { get; set; }
    }
}

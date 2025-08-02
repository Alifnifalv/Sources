using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class UserVisitLog
    {
        [Key]
        public long UserVisitLogID { get; set; }
        public long RefUserID { get; set; }
        public int RefModuleID { get; set; }
        public string IPAddress { get; set; }
        public string PageName { get; set; }
        public Nullable<bool> VisitStatus { get; set; }
        public System.DateTime CreatedOn { get; set; }
    }
}

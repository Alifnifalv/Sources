using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class UserLog
    {
        [Key]
        public long LogID { get; set; }
        public long UserID { get; set; }
        public string UserAction { get; set; }
        public System.DateTime ActionTime { get; set; }
        public string IpLocation { get; set; }
        public string IpCountry { get; set; }
        public string ActionKey { get; set; }
        public virtual UserMaster UserMaster { get; set; }
    }
}

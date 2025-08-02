using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BannerSession
    {
        [Key]
        public long BannerID { get; set; }
        public string SessionID { get; set; }
        public long UniqueID { get; set; }
        public long LoopID { get; set; }
        public System.DateTime BannerTime { get; set; }
    }
}

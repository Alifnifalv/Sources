using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class WebsiteLog
    {
        [Key]
        public long LogID { get; set; }
        public string UserAction { get; set; }
        public System.DateTime ActionTime { get; set; }
        public string IpLocation { get; set; }
        public string IpCountry { get; set; }
    }
}

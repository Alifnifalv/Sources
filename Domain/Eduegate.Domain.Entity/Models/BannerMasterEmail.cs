using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BannerMasterEmail
    {
        [Key]
        public int BannerID { get; set; }
        public string BannerName { get; set; }
        public string BannerFile { get; set; }
        public string Link { get; set; }
        public bool Active { get; set; }
    }
}

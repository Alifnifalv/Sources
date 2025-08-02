using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BannerPage
    {
        [Key]
        public int BannerPageID { get; set; }
        public int BannerID { get; set; }
        public int PageID { get; set; }
        public virtual BannerMaster BannerMaster { get; set; }
    }
}

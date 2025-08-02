using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("BannerStatuses", Schema = "cms")]
    public partial class BannerStatus
    {
        public BannerStatus()
        {
            this.Banners = new List<Banner>();
        }

        [Key]
        public int BannerStatusID { get; set; }
        public string BannerStatusName { get; set; }
        public virtual ICollection<Banner> Banners { get; set; }
    }
}

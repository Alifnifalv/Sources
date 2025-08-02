using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("BannerStatuses", Schema = "cms")]
    public partial class BannerStatus
    {
        public BannerStatus()
        {
            Banners = new HashSet<Banner>();
        }

        [Key]
        public int BannerStatusID { get; set; }
        [StringLength(100)]
        public string BannerStatusName { get; set; }

        [InverseProperty("Status")]
        public virtual ICollection<Banner> Banners { get; set; }
    }
}

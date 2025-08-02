using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("BannerTypes", Schema = "cms")]
    public partial class BannerType
    {
        public BannerType()
        {
            this.Banners = new List<Banner>();
        }

        [Key]
        public int BannerTypeID { get; set; }
        public string BannerTypeName { get; set; }
        public virtual ICollection<Banner> Banners { get; set; }
    }
}

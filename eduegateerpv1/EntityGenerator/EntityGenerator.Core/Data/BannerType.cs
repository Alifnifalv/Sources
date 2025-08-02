using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("BannerTypes", Schema = "cms")]
    public partial class BannerType
    {
        public BannerType()
        {
            Banners = new HashSet<Banner>();
        }

        [Key]
        public int BannerTypeID { get; set; }
        [StringLength(100)]
        public string BannerTypeName { get; set; }

        [InverseProperty("BannerType")]
        public virtual ICollection<Banner> Banners { get; set; }
    }
}

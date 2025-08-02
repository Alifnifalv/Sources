using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Banners", Schema = "cms")]
    public partial class Banner
    {
        [Key]
        public long BannerIID { get; set; }
        [StringLength(100)]
        public string BannerName { get; set; }
        [StringLength(255)]
        public string BannerFile { get; set; }
        public int? BannerTypeID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ReferenceID { get; set; }
        public byte? Frequency { get; set; }
        [StringLength(255)]
        public string Link { get; set; }
        [StringLength(10)]
        public string Target { get; set; }
        public int? StatusID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? ActionLinkTypeID { get; set; }
        [StringLength(300)]
        public string BannerActionLinkParameters { get; set; }
        public long? SerialNo { get; set; }
        public int? CompanyID { get; set; }
        [StringLength(200)]
        public string TagValues { get; set; }

        [ForeignKey("BannerTypeID")]
        [InverseProperty("Banners")]
        public virtual BannerType BannerType { get; set; }
        [ForeignKey("StatusID")]
        [InverseProperty("Banners")]
        public virtual BannerStatus Status { get; set; }
    }
}

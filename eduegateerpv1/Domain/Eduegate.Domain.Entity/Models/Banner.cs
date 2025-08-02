using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Banners", Schema = "cms")]
    public partial class Banner
    {
        [Key]
        public long BannerIID { get; set; }
        public string BannerName { get; set; }
        public string BannerFile { get; set; }
        public Nullable<int> BannerTypeID { get; set; }
        public string ReferenceID { get; set; }
        public Nullable<byte> Frequency { get; set; }
        //public string Link { get; set; }
        public string Target { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> ActionLinkTypeID { get; set; }
        public string BannerActionLinkParameters { get; set; }
        public Nullable<long> SerialNo { get; set; }

        public virtual BannerType BannerType { get; set; }
        public virtual BannerStatus BannerStatus { get; set; }
        public virtual ActionLinkType ActionLinkType { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CategoryImageMap
    {
        public long CategoryImageMapIID { get; set; }
        public Nullable<long> CategoryID { get; set; }
        public Nullable<byte> ImageTypeID { get; set; }
        public string ImageFile { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public string ImageTitle { get; set; }
        public string ImageLink { get; set; }
        public string ImageTarget { get; set; }
        public Nullable<int> ActionLinkTypeID { get; set; }
        public string ImageLinkParameters { get; set; }
        public Nullable<long> SerialNo { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> SiteID { get; set; }
        public virtual Category Category { get; set; }
        public virtual ImageType ImageType { get; set; }
    }
}

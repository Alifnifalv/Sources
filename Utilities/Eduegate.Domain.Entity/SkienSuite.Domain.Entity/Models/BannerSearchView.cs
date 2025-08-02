using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BannerSearchView
    {
        public long BannerIID { get; set; }
        public string BannerName { get; set; }
        public string BannerFile { get; set; }
        public Nullable<int> BannerTypeID { get; set; }
        public Nullable<byte> Frequency { get; set; }
        public string Link { get; set; }
        public string Target { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string BannerStatusName { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public string ActualImage { get; set; }
        public Nullable<long> SerialNo { get; set; }
        public string BannerTypeName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class BannerSearchView
    {
        public long BannerIID { get; set; }
        [StringLength(100)]
        public string BannerName { get; set; }
        [StringLength(255)]
        public string BannerFile { get; set; }
        public int? BannerTypeID { get; set; }
        public byte? Frequency { get; set; }
        [StringLength(255)]
        public string Link { get; set; }
        [StringLength(10)]
        public string Target { get; set; }
        public int? StatusID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [StringLength(100)]
        public string BannerStatusName { get; set; }
        public int? CompanyID { get; set; }
        [StringLength(263)]
        public string ActualImage { get; set; }
        public long? SerialNo { get; set; }
        [StringLength(100)]
        public string BannerTypeName { get; set; }
    }
}

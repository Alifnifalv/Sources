using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CountryMaster
    {
        public CountryMaster()
        {
            this.AddressMasters = new List<AddressMaster>();
            this.BlinkLocationMasters = new List<BlinkLocationMaster>();
        }

        [Key]
        public long CountryID { get; set; }
        public int RefGroupID { get; set; }
        public string CountryCode { get; set; }
        public string CountryNameEn { get; set; }
        public string CountryNameAr { get; set; }
        public bool Active { get; set; }
        public Nullable<bool> Operation { get; set; }
        public string BaseCurrency { get; set; }
        public Nullable<decimal> ConversionRate { get; set; }
        public Nullable<decimal> ConversionRateSAR { get; set; }
        public Nullable<byte> NoofDecimals { get; set; }
        public Nullable<System.DateTime> DataFeedDateTime { get; set; }
        public virtual ICollection<AddressMaster> AddressMasters { get; set; }
        public virtual ICollection<BlinkLocationMaster> BlinkLocationMasters { get; set; }
        public virtual ICollection<CategoryBannerMaster> CategoryBannerMasters { get; set; }
        public virtual CountryGroup CountryGroup { get; set; }
    }
}

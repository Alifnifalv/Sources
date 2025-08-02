using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BrandTag
    {
        public BrandTag()
        {
            this.BrandTagMaps = new List<BrandTagMap>();
        }

        public long BrandTagIID { get; set; }
        public Nullable<long> BrandID { get; set; }
        public string TagName { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual ICollection<BrandTagMap> BrandTagMaps { get; set; }
    }
}

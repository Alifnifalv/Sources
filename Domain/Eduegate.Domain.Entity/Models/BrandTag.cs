using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("BrandTags", Schema = "catalog")]
    public partial class BrandTag
    {
        public BrandTag()
        {
            this.BrandTagMaps = new List<BrandTagMap>();
        }

        [Key]
        public long BrandTagIID { get; set; }
        public string TagName { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public virtual ICollection<BrandTagMap> BrandTagMaps { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ProductSKUTags", Schema = "catalog")]
    public partial class ProductSKUTag
    {
        public ProductSKUTag()
        {
            this.ProductSKUTagMaps = new List<ProductSKUTagMap>();
        }

        [Key]
        public long ProductSKUTagIID { get; set; }
        public string TagName { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public virtual ICollection<ProductSKUTagMap> ProductSKUTagMaps { get; set; }
    }
}

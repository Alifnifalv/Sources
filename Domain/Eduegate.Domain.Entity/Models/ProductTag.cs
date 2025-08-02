using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ProductTags", Schema = "catalog")]
    public partial class ProductTag
    {
        public ProductTag()
        {
            this.ProductTagMaps = new List<ProductTagMap>();
        }

        [Key]
        public long ProductTagIID { get; set; }
        public string TagName { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual ICollection<ProductTagMap> ProductTagMaps { get; set; }
    }
}

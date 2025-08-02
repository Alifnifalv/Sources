using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("CategoryTags", Schema = "catalog")]
    public partial class CategoryTag
    {
        public CategoryTag()
        {
            this.CategoryTagMaps = new List<CategoryTagMap>();
        }

        [Key]
        public long CategoryTagIID { get; set; }
        public string TagName { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public virtual ICollection<CategoryTagMap> CategoryTagMaps { get; set; }
    }
}

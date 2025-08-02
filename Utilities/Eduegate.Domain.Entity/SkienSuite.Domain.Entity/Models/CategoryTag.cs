using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CategoryTag
    {
        public CategoryTag()
        {
            this.CategoryTagMaps = new List<CategoryTagMap>();
        }

        public long CategoryTagIID { get; set; }
        public Nullable<long> CategoryID { get; set; }
        public string TagName { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<CategoryTagMap> CategoryTagMaps { get; set; }
    }
}

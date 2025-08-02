using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CategoryCultureData
    {
        public byte CultureID { get; set; }
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Category Category { get; set; }
        public virtual Culture Culture { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductSKUCultureData
    {
        public byte CultureID { get; set; }
        public long ProductSKUMapID { get; set; }
        public string ProductSKUName { get; set; }
        public string ProductSKUDescription { get; set; }
        public string ProductSKUDetails { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Culture Culture { get; set; }
        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}

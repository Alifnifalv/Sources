using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BrandCultureData
    {
        public byte CultureID { get; set; }
        public long BrandID { get; set; }
        public string BrandName { get; set; }
        public string Descirption { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Culture Culture { get; set; }
    }
}

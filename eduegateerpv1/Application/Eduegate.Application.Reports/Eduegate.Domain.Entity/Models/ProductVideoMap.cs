using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductVideoMap
    {
        [Key]
        public long ProductVideoMapIID { get; set; }
        public Nullable<long> ProductID { get; set; }
        public Nullable<long> ProductSKUMapID { get; set; }
        public string VideoFile { get; set; }
        public Nullable<byte> Sequence { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Product Product { get; set; }
        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductToProductMap
    {
        [Key]
        public long ProductToProductMapIID { get; set; }
        public Nullable<long> ProductID { get; set; }
        public Nullable<long> ProductIDTo { get; set; }
        public Nullable<byte> SalesRelationTypeID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Product Product { get; set; }
        public virtual Product Product1 { get; set; }
    }
}

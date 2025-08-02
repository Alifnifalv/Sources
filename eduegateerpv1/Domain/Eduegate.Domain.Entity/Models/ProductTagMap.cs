using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ProductTagMaps", Schema = "catalog")]
    public partial class ProductTagMap
    {
        [Key]
        public long ProductTagMapIID { get; set; }
        public Nullable<long> ProductTagID { get; set; }
        public Nullable<long> ProductID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public virtual Product Product { get; set; }
        public virtual ProductTag ProductTag { get; set; }
    }
}

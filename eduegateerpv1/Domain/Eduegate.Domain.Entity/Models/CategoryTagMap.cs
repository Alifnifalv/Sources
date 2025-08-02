using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("CategoryTagMaps", Schema = "catalog")]
    public partial class CategoryTagMap
    {
        [Key]
        public long CategoryTagMapIID { get; set; }
        public Nullable<long> CategoryID { get; set; }
        public Nullable<long> CategoryTagID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public virtual Category Category { get; set; }
        public virtual CategoryTag CategoryTag { get; set; }
    }
}

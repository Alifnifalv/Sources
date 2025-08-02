using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ProductFamilyPropertyMaps", Schema = "catalog")]
    public partial class ProductFamilyPropertyMap
    {
        [Key]
        public long ProductFamilyPropertyMapIID { get; set; }
        public Nullable<long> ProductFamilyID { get; set; }
        public Nullable<long> ProductPropertyID { get; set; }
        public string Value { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual ProductFamily ProductFamily { get; set; }
        public virtual Property Property { get; set; }
    }
}

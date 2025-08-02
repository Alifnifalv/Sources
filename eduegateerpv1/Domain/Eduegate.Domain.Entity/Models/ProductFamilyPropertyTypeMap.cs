using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ProductFamilyPropertyTypeMaps", Schema = "catalog")]
    public partial class ProductFamilyPropertyTypeMap
    {
        [Key]
        public long ProductFamilyPropertyTypeMapIID { get; set; }
        public Nullable<long> ProductFamilyID { get; set; }
        public Nullable<byte> PropertyTypeID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual ProductFamily ProductFamily { get; set; }
        public virtual PropertyType PropertyType { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ProductSerialMaps", Schema = "inventory")]
    public partial class ProductSerialMap
    {
        [Key]
        public long ProductSerialIID { get; set; }
        public string SerialNo { get; set; }
        public Nullable<long> DetailID { get; set; }
        public Nullable<long> ProductSKUMapID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual TransactionDetail TransactionDetail { get; set; }
        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}

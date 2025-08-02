using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductSerialMap
    {
        public long ProductSerialIID { get; set; }
        public string SerialNo { get; set; }
        public Nullable<long> DetailID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public Nullable<long> ProductSKUMapID { get; set; }
        public virtual ProductSKUMap ProductSKUMap { get; set; }
        public virtual TransactionDetail TransactionDetail { get; set; }
    }
}

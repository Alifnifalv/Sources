using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CultureData
    {
        public long CultureDataIID { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public Nullable<byte> CultureID { get; set; }
        public string Data { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Culture Culture { get; set; }
    }
}

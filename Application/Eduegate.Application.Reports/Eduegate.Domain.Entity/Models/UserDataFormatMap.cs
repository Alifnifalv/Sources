using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class UserDataFormatMap
    {
        public long UserDataFormatIID { get; set; }
        public Nullable<long> LoginID { get; set; }
        public Nullable<short> DataFormatTypeID { get; set; }
        public Nullable<int> DataFormatID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Login Login { get; set; }
        public virtual DataFormat DataFormat { get; set; }
        public virtual DataFormatType DataFormatType { get; set; }
    }
}

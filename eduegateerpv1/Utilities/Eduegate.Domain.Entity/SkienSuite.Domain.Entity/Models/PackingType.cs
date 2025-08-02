using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class PackingType
    {
        public short PackingTypeIID { get; set; }
        public string PackingType1 { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> PackingCost { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
    }
}

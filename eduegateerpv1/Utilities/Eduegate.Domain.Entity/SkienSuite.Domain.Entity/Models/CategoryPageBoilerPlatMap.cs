using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CategoryPageBoilerPlatMap
    {
        public long CategoryPageBoilerPlatMapIID { get; set; }
        public Nullable<long> CategoryID { get; set; }
        public Nullable<long> PageBoilerplateMapID { get; set; }
        public Nullable<int> SerialNumber { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual PageBoilerplateMap PageBoilerplateMap { get; set; }
    }
}

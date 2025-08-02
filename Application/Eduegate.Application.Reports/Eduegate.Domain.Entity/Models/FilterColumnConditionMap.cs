using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class FilterColumnConditionMap
    {
        public long FilterColumnConditionMapID { get; set; }
        public Nullable<byte> DataTypeID { get; set; }
        public Nullable<long> FilterColumnID { get; set; }
        public Nullable<byte> ConidtionID { get; set; }
        public virtual Condition Condition { get; set; }
        public virtual DataType DataType { get; set; }
        public virtual FilterColumn FilterColumn { get; set; }
    }
}

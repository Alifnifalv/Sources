using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class DataType
    {
        public DataType()
        {
            this.FilterColumnConditionMaps = new List<FilterColumnConditionMap>();
            this.FilterColumns = new List<FilterColumn>();
        }

        public byte DataTypeID { get; set; }
        public string DateTypeName { get; set; }
        public virtual ICollection<FilterColumnConditionMap> FilterColumnConditionMaps { get; set; }
        public virtual ICollection<FilterColumn> FilterColumns { get; set; }
    }
}

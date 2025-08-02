using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class FilterColumn
    {
        public FilterColumn()
        {
            this.FilterColumnConditionMaps = new List<FilterColumnConditionMap>();
            this.FilterColumnUserValues = new List<FilterColumnUserValue>();
        }

        public long FilterColumnID { get; set; }
        public Nullable<int> SequenceNo { get; set; }
        public Nullable<long> ViewID { get; set; }
        public string ColumnCaption { get; set; }
        public string ColumnName { get; set; }
        public Nullable<byte> DataTypeID { get; set; }
        public Nullable<byte> UIControlTypeID { get; set; }
        public string DefaultValues { get; set; }
        public Nullable<bool> IsQuickFilter { get; set; }
        public Nullable<int> LookupID { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public virtual DataType DataType { get; set; }
        public virtual ICollection<FilterColumnConditionMap> FilterColumnConditionMaps { get; set; }
        public virtual UIControlType UIControlType { get; set; }
        public virtual View View { get; set; }
        public virtual ICollection<FilterColumnUserValue> FilterColumnUserValues { get; set; }
    }
}
